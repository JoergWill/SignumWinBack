Imports System.Windows.Forms

Public Class wb_Kneter

    Private _KneterNr As Int16 = wb_Global.UNDEFINED
    Private _Index As Integer = wb_Global.UNDEFINED
    Private _RezeptBezeichnung As String
    Private _RezeptNummer As String
    Private _KompBezeichnung As String
    Private _ChargeNr As UInt16
    Private _Schritte As New List(Of wb_KneterSchritt)
    Private _PlcParaNr As wb_Global.KneterParameter
    Private _PlcSollwert As Double
    Private _PlcIstwert As Double
    Private _PlcStatus As UInt16

    Public Property KneterNr As Short
        Get
            Return _KneterNr
        End Get
        Set(value As Short)
            _KneterNr = value
        End Set
    End Property

    Public Property ChargeNr As UShort
        Get
            Return _ChargeNr
        End Get
        Set(value As UShort)
            'wenn sich die Chargen-Nummer geändert hat
            If value <> _ChargeNr Then
                _ChargeNr = value
                'alle Kneter-Schritte aus der Datenbank lesen
                ReadFromArbRezepte()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Summe aller (Zeit)Sollwerte aus den Kneter-Schritten (Datenbank)
    ''' 
    ''' Der Sollwert aus dem aktuellen Kneter-Schritt(SPS) wird nicht mit berechnet, dieser
    ''' Wert kommt aus der SPS, da eventuelle Änderungen der Knet-Sollzeit erst NACH dem
    ''' Abarbeiten des Knetschrittes in der Datenbank aktualisiert werden.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property dSollwert As Double
        Get
            Dim _dSollwert As Integer = 0
            For Each KnS As wb_KneterSchritt In _Schritte
                If ParaIsTime(KnS.ParaNr) AndAlso KnS.Index <> Index Then
                    _dSollwert += KnS.dSollwert
                End If
            Next
            'Kneterschritt Sollwert aus der SPS-Steuerung
            'da Änderungen der Knetzeit erst NACH dem Abarbeiten in die Datenbank zurückgeschrieben werden, wird hier die Sollzeit aus der SPS verwendet
            If ParaIsTime(PlcParaNr) Then
                _dSollwert += PlcSollwert
            End If
            Return _dSollwert
        End Get
    End Property

    Public ReadOnly Property sSollwert As String
        Get
            'Summe aller (Zeit)Sollwerte
            Return TimeToString(dSollwert)
        End Get
    End Property

    ''' <summary>
    ''' Summe aller (Zeit)Istwerte aus den Kneterschritten (Datenbank)
    ''' die schon als abgearbeitet markiert sind Status = OK(2)
    ''' 
    ''' ZUsätzlich wird noch der aktuelle Istwert aus der SPS-Steuerung dazu addiert.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property dIstwert
        Get
            Dim _dIstwert As Integer = 0

            'Kneter-Schritte aus der Datenbank
            For Each KnS As wb_KneterSchritt In _Schritte
                If ParaIsTime(KnS.ParaNr) AndAlso KnS.Status = wb_Global.ChargenStatus.CS_OK Then
                    _dIstwert += KnS.dIstwert
                End If
            Next
            'Kneterschritt aus der SPS-Steuerung
            If ParaIsTime(PlcParaNr) Then
                _dIstwert += PlcIstwert
            End If
            Return _dIstwert
        End Get
    End Property

    Public ReadOnly Property sIstwert As String
        Get
            'Summe aller (Zeit)Istwerte
            Return TimeToString(dIstwert)
        End Get
    End Property

    ''' <summary>
    ''' Summe aller (Teigruhe)Sollwerte aus den Kneterschritten (Datenbank)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property dTeigruhe
        Get
            Dim _dTeigruhe As Integer = 0

            'Kneter-Schritte aus der Datenbank
            For Each KnS As wb_KneterSchritt In _Schritte
                If ParaIsTeigruhe(KnS.ParaNr) Then
                    _dTeigruhe += KnS.dSollwert
                End If
            Next
            Return _dTeigruhe
        End Get
    End Property

    Public ReadOnly Property sTeigruhe As String
        Get
            'Summe aller Teigruhe-Sollwerte
            Return TimeToString(dTeigruhe)
        End Get
    End Property

    Private ReadOnly Property dFertig As Double
        Get
            Return dSollwert - dIstwert + dTeigruhe
        End Get
    End Property

    Public ReadOnly Property sFertig As String
        Get
            Return TimeToString(dFertig)
        End Get
    End Property

    Public ReadOnly Property sFertigUhrzeit As String
        Get
            Return TimeToString(Now.Hour * 3600 + Now.Minute * 60 + Now.Second + dFertig)
        End Get
    End Property

    Public ReadOnly Property dProzent As Integer
        Get
            Return Math.Min(100, wb_Functions.ProzentSatz(dSollwert, dIstwert))
        End Get
    End Property

    Public Property PlcSollwert As Double
        Get
            Return _PlcSollwert
        End Get
        Set(value As Double)
            _PlcSollwert = value
        End Set
    End Property

    Public Property PlcIstwert As Double
        Get
            Return _PlcIstwert
        End Get
        Set(value As Double)
            _PlcIstwert = value
        End Set
    End Property

    Public Property PlcParaNr As UShort
        Get
            Return _PlcParaNr
        End Get
        Set(value As UShort)
            If value <> _PlcParaNr Then
                _PlcParaNr = value
                'alle Kneter-Schritte aus der Datenbank lesen
                ReadFromArbRezepte()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Status der Kneter-Steuerung auf der SPS
    ''' </summary>
    ''' <returns></returns>
    Public Property PlcStatus As UShort
        Get
            Return _PlcStatus
        End Get
        Set(value As UShort)
            _PlcStatus = value
        End Set
    End Property

    ''' <summary>
    ''' Zeiger auf den aktuell in der SPS bearbeiteten Kneterschritt
    ''' </summary>
    ''' <returns></returns>
    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
        End Set
    End Property

    ''' <summary>
    ''' Komponenten-Bezeichnung des aktuell laufenden Kneterschrittes in der SPS
    ''' </summary>
    ''' <returns></returns>
    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
        End Set
    End Property

    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = value
        End Set
    End Property

    Public Property RezeptBezeichnung As String
        Get
            Return _RezeptBezeichnung
        End Get
        Set(value As String)
            _RezeptBezeichnung = value
        End Set
    End Property

    Private Function ParaIsTime(ParaNr As Integer) As Boolean
        If ParaNr <= wb_Global.KneterParameter.KnetenLinks Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ParaIsTeigruhe(ParaNr As Integer) As Boolean
        If ParaNr = wb_Global.KneterParameter.Teigruhe Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function TimeToString(d As Double) As String
        'Timespan
        Dim ts As New TimeSpan(d * 10000000)
        'Umwandeln in HH:MM:SS
        Return ts.ToString("c")
    End Function

    ''' <summary>
    ''' Daten per ADS aus der Kneter-Steuerung lesen
    ''' Die Daten stehen nacheinander im BinaryStream und werden sequenziell gelesen
    ''' Nach jedem Lesevorgang(BinaryReader) wird der Lesezeiger um x Zeichen vorgerückt.
    ''' 
    ''' Die Daten müssen in der Daten-Struktur in der SPS in dieser Reihenfolge 
    ''' abgelegt sein.
    ''' 
    '''     ChargenNummer       UINT        2 Bytes
    '''     Parameter           UINT        2 Bytes
    '''     Sollwert            REAL        4 Bytes
    '''     Istwert             REAL        4 Bytes
    '''     Status              UINT        2 Bytes
    '''                                 =============
    '''                                    14 Bytes pro Kneter
    '''     
    ''' SPS.TYPE Kn_ADS
    ''' ===============
    '''     STRUCT
    '''         ChargenNummer   : UINT;
    '''         PlcParaNr       : UINT;
    '''         Sollwert        : DINT;
    '''         Istwert         : DINT;
    '''         Status          : INT;
    '''     END_STRUCT
    ''' 
    ''' SPS.ComADS
    ''' ==========
    ''' 	(* Istwerte und Status Kneter 1 *)
    '''	    KneterADS[1].ChargenNummer  := DWORD_TO_UINT(KneterDaten[1].ChargenNummer);
    '''		KneterADS[1].PlcParaNr      := DINT_TO_UINT(Kneter_Main.Kneter[1].SendPara);
    '''		KneterADS[1].Sollwert       := KneterDaten[1].Soll[Kneter_Main.Kneter[1].SendPara];
    '''		KneterADS[1].Istwert        := Kneter_Main.Kneter[1].SendIstwert;
    '''		KneterADS[1].Status         := Kneter_Main.State_Kn1;
    ''' 
    ''' </summary>
    Public Sub ReadFromStream(ByRef BinRead As IO.BinaryReader, Kneter As Integer)
        'Kneter-Nummer
        KneterNr = Kneter
        'aktuelle Chargen-Nummer des Teiges
        ChargeNr = BinRead.ReadUInt16
        'Kneter-Funktion (Mischen/Kneten Rechts/Links...)
        PlcParaNr = BinRead.ReadUInt16
        'Sollwert (Zeit in Sekunden)
        PlcSollwert = BinRead.ReadUInt32
        'Istwert (Zeit in Sekunden)
        PlcIstwert = BinRead.ReadUInt32
        'Status der Kneter-Ablauf-Steuerung
        PlcStatus = BinRead.ReadInt16
    End Sub

    ''' <summary>
    ''' Die Chargen-Nummer hat sich geändert.
    ''' 
    ''' Liest alle Rezept-Daten zur aktuellen Chargen-Nummer aus der Tabelle winback.ArbRezepte und ArbRZSchritte
    ''' Damit sind alle Sollwerte für diesen Kneterablauf bekannt und die Gesamtzeit für alle Knetschritte kann berechnet werden.
    ''' </summary>
    Private Function ReadFromArbRezepte() As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String
        'Liste der Kneterschritte löschen
        _Schritte.Clear()
        'Aktueller Index
        _Index = wb_Global.UNDEFINED

        'Suche nach dem ersten Datensatz mit Komponenten-Type 118 und dieser Chargen-Nummer
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKneterArbRezepte, ChargeNr.ToString)

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(sql) AndAlso winback.Read Then
            Do
                'alle Daten zu diesem Kneterschritt einlesen
                Dim KneterSchritt As New wb_KneterSchritt
                KneterSchritt.MySQLdbRead(winback.MySqlRead)
                'zur Liste aller Kneter-Schritte hinzufügen
                _Schritte.Add(KneterSchritt)

                'der erste Datensatz mit Status unbearbeitet(0) ist der aktuelle Satz, der in der SPS bearbeitet wird
                If Index = wb_Global.UNDEFINED AndAlso KneterSchritt.Status = wb_Global.ChargenStatus.CS_UNBEARBEITET Then
                    Index = KneterSchritt.Index
                    KompBezeichnung = KneterSchritt.KompBezeichnung
                    RezeptBezeichnung = KneterSchritt.RezeptBezeichnung
                    RezeptNummer = KneterSchritt.RezeptNummer
                End If

            Loop While winback.MySqlRead.Read
            winback.Close()
            Return True
        End If
        winback.Close()
        Return False
    End Function

End Class
