Imports MySql.Data.MySqlClient
Imports WinBack

Public Class ob_ProduzierteWare
    Private _FilialNummer As Integer = wb_Global.UNDEFINED
    Private _Linie As Integer = wb_Global.UNDEFINED
    Private _ProduktionsDatum As DateTime = Now
    Private _SatzTyp As wb_Global.obSatzTyp = wb_Global.obSatzTyp.Rohstoff
    Private _Typ As wb_Global.wbSatzTyp = wb_Global.wbSatzTyp.Artikel
    Private _ChargeNr As String = "UNDEF"
    Private _TWNr As Long = wb_Global.UNDEFINED
    Private _ArtikelNr As String = ""
    Private _Type As wb_Global.KomponTypen
    Private _ParamNr As Integer
    Private _Einheit As Integer = wb_Global.UNDEFINED
    Private _Unit As Integer = wb_Global.obEinheitKilogramm
    Private _Color As String = "0"
    Private _Size As String = "NULL"
    Private _Menge As Double = 0.0
    Private _ChargenNummer As String = ""
    Private _HaltbarkeitsDatum As DateTime = Now
    Private _Gestartet As Boolean = False
    Private _ProdDatumGueltig As DateTime

    Public ReadOnly Property sFilialNummer As String
        Get
            Return _FilialNummer.ToString
        End Get
    End Property

    Public Property ProduktionsDatum As Date
        Get
            Return _ProduktionsDatum
        End Get
        Set(value As Date)
            _ProduktionsDatum = value
        End Set
    End Property

    Public ReadOnly Property Gestartet As Boolean
        Get
            Return _Gestartet
        End Get
    End Property

    Public ReadOnly Property sProduktionsDatum As String
        Get
            Return wb_sql_Functions.MsSQLShortDate(_ProduktionsDatum)
        End Get
    End Property

    Public ReadOnly Property sProduktionsUhrzeit As String
        Get
            'TODO Hier die Uhrzeit Im Format HH:MM dekodieren aus Produktionsdatum
            'Future-Use sobald die Datenbank in OrgaBack erweitert ist.
            Return wb_sql_Functions.MsSQLShortTime(_ProduktionsDatum)
        End Get
    End Property

    Public Property SatzTyp As wb_Global.obSatzTyp
        Get
            Return _SatzTyp
        End Get
        Set(value As wb_Global.obSatzTyp)
            _SatzTyp = value
        End Set
    End Property

    Public ReadOnly Property sSatzTyp As String
        Get
            Select Case _SatzTyp
                Case wb_Global.obSatzTyp.ProduzierterArtikel
                    Return ""
                Case wb_Global.obSatzTyp.Rohstoff
                    Return "V"
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public Property ArtikelNr As String
        Get
            Return _ArtikelNr
        End Get
        Set(value As String)
            _ArtikelNr = value
        End Set
    End Property

    Public ReadOnly Property Unit As String
        Get
            Return _Unit.ToString
        End Get
    End Property

    Public ReadOnly Property Color As String
        Get
            Return _Color.ToString
        End Get
    End Property

    Public ReadOnly Property Size As String
        Get
            Return _Size
        End Get
    End Property

    Public Property Menge As Double
        Get
            Return _Menge
        End Get
        Set(value As Double)
            _Menge = value
        End Set
    End Property

    Public ReadOnly Property sMenge As String
        Get
            Return wb_sql_Functions.MsDoubleToString(Menge)
        End Get
    End Property

    ''' <summary>
    ''' Chargen-Nummer begrenzt auf maximal 15 Zeichen.
    ''' Ist der Satztyp ein Produktions-Artikel wird die Chargen-Nummer erweitert:
    '''     - Produktions-Datum
    '''     - TW-Nummer (5-stellig)
    ''' </summary>
    ''' <returns></returns>
    Public Property ChargenNummer As String
        Get
            'wenn der Satztyp ein produzierter Artikel ist 
            If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                'Chargen-Nummer erweitern
                Return _ChargenNummer & TWNr.ToString("D5")
            Else
                'Länge begrenzen (Rohstoff-Chargen-Nummer)
                If _ChargenNummer.Length > 15 Then
                    Return Left(_ChargenNummer, 15)
                Else
                    Return _ChargenNummer
                End If
            End If
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public ReadOnly Property WinBackChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
    End Property

    Public Property HaltbarkeitsDatum As Date
        Get
            Return _HaltbarkeitsDatum
        End Get
        Set(value As Date)
            _HaltbarkeitsDatum = value
        End Set
    End Property

    Public ReadOnly Property sHaltbarkeitsDatum As String
        Get
            Return wb_sql_Functions.MsSQLShortDate(_HaltbarkeitsDatum)
        End Get
    End Property

    Public Property Linie As Integer
        Get
            Return _Linie
        End Get
        Set(value As Integer)
            _Linie = value
            _FilialNummer = wb_Linien_Global.GetFiliale(_Linie)
        End Set
    End Property

    Public Property Type As wb_Global.KomponTypen
        Get
            Return _Type
        End Get
        Set(value As wb_Global.KomponTypen)
            _Type = value
        End Set
    End Property

    Public Property ParamNr As Integer
        Get
            Return _ParamNr
        End Get
        Set(value As Integer)
            _ParamNr = value
        End Set
    End Property

    Public Property Einheit As Integer
        Get
            Return _Einheit
        End Get
        Set(value As Integer)
            _Einheit = value
            _Unit = wb_Einheiten_Global.GetobEinheitNr(_Einheit)
        End Set
    End Property

    Public Property TWNr As Long
        Get
            Return _TWNr
        End Get
        Set(value As Long)
            _TWNr = value
        End Set
    End Property

    Public Property Typ As wb_Global.wbSatzTyp
        Get
            Return _Typ
        End Get
        Set(value As wb_Global.wbSatzTyp)
            _Typ = value
        End Set
    End Property

    ''' <summary>
    ''' Konstruktor übergibt die Chargen-Nummer des vorhergehenden Datensatzes. Ist die Chargen-Nummer identisch, ist der 
    ''' Satztyp 'V' also eine Rohstoff-Zeile (Verbrauchsdaten)
    ''' Ist die Chargen-Nummer geändert, ist der Satztyp NULL (Produzierter Artikel)
    ''' 
    ''' Die Länge wird begrenzt auf 15 Zeichen, da sonst ein Überlauf beim Insert in die Datenbank auftritt.
    ''' </summary>
    ''' <param name="ChargeNr"></param>
    Public Sub New(ChargeNr As String)
        'Chargen-Nummer des letzten Datensatzes
        _ChargeNr = ChargeNr
        'Produktions-Datum ist gültig ab...
        _ProdDatumGueltig = Convert.ToDateTime("22.11.1964")
    End Sub

    ''' <summary>
    ''' Einlesen aller Datenfelder aus der Datenbank wbdaten in ob_ProduzierteWare
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns></returns>
    Public Function MySQLdbRead_Chargen(ByRef sqlReader As MySqlDataReader) As Boolean

        'Chargendaten - Anzahl der Felder im DataSet
        For i = 0 To sqlReader.FieldCount - 1
            MySQLdbRead_Fields(sqlReader.GetName(i), sqlReader.GetValue(i))
        Next

        'wenn in der Produktion eine Rezeptur aufgerufen wurde, wird ein Dummy-Artikel angelegt
        If ((SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel) AndAlso (Typ = wb_Global.wbSatzTyp.Rezept)) Then
            ArtikelNr = wb_GlobalSettings.ProduktionDummyArtikel
            Einheit = wb_Global.wbEinheitStk
            'ElseIf ((SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel) And ((Type = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE) Or (Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE))) Then
            '    'wenn in der Produktion eine Halbfabrikat hergestellt wurde, muss die Einheit angepasst werden
            '    Einheit = wb_Global.wbEinheitKilogramm
        End If

        Return True
    End Function

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Objekt-Eigenschaften
    ''' </summary>
    ''' <param name="Name">String - Spalten-Name aus Datenbank</param>
    ''' <param name="Value">Object - Wert aus Datenbank</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Fields(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Debug
        'Debug.Print("Feld/Value " & Name & "/" & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                'wenn sich die Chargen-Nummer nicht geändert hat ist der
                'Satztyp ein Rohstoff-Verbrauch
                Case "B_ARZ_Charge_Nr"
                    If _ChargeNr = Value.ToString Then
                        SatzTyp = wb_Global.obSatzTyp.Rohstoff
                    Else
                        SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel
                        ChargenNummer = Value.ToString
                    End If
                'Rohstoff-Chargen-Nummer
                Case "B_ARS_BF_Charge"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        ChargenNummer = Value.ToString
                    End If
                'Tageswechsel-Nummer
                Case "B_ARZ_TW_Nr"
                    TWNr = wb_Functions.StrToInt(Value)

                'Datensatz Chargen-Type (Rezept/Artikel)
                Case "B_ARZ_Typ"
                    Typ = wb_Functions.IntToProduktionsTyp(wb_Functions.StrToInt(Value))

                'Einheit (berechnet OrgaBack-Einheiten-Index)
                Case "B_KT_EinheitIndex"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        Einheit = wb_Functions.StrToInt(Value)
                    End If

                'Produktions-Linie (berechnet Produktions-Filiale)
                Case "Linie"
                    Linie = wb_Functions.StrToInt(Value)

                'Artikel-Nummer (Produzierter Artikel) wenn in WinBack ein Artikel produziert wurde
                Case "B_ARZ_KA_NrAlNum"
                    If (SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel) And (Typ = wb_Global.wbSatzTyp.Artikel) Then
                        ArtikelNr = Value
                    End If
                'Rohstoff-Nummer (oder die Rohstoff-Nummer des verknüpften Rezeptes bei Pseudo-Artikel)
                Case "B_KO_Nr_AlNum"
                    If (SatzTyp = wb_Global.obSatzTyp.Rohstoff) Then
                        ArtikelNr = Value
                    End If

                'Sollwert Parameter-Nummer
                Case "B_ARS_ParamNr"
                    ParamNr = wb_Functions.StrToInt(Value)
                'Komponenten-Type
                Case "B_KT_Typ_Nr"
                    Type = wb_Functions.IntToKomponType(wb_Functions.StrToInt(Value))

                'Sollmenge produzierter Artikel in Stk (Aufruf über Artikel in WinBack)
                Case "B_ARZ_Sollmenge_stueck"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel And Typ = wb_Global.wbSatzTyp.Artikel Then
                        Menge = wb_Functions.StrToDouble(Value)
                        'Einheit = wb_Global.wbEinheitStk
                    End If
                'Sollmenge produzierter Artikel in Stk (Aufruf über Rezept in WinBack)
                Case "B_ARZ_Sollmenge_kg"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel And Typ = wb_Global.wbSatzTyp.Rezept Then
                        Menge = wb_Functions.StrToDouble(Value)
                        'Einheit = wb_Global.wbEinheitKilogramm
                    End If
                'Einheit (Satztyp Produzierter Artikel)
                Case "B_ARZ_Art_Einheit"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        Einheit = wb_Functions.StrToInt(Value)
                    End If
                'Rohstoff Istwert Verwiegung (Verbrauch)
                Case "B_ARS_Istwert"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        Menge = wb_Functions.StrToDouble(Value)
                    End If

                'Produktions-Datum (ACHTUNG Schaufler: Zp_Beendet)
                'Case "B_ARZ_Erststart"
                Case "B_ARZ_Zp_Beendet"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        ProduktionsDatum = Value
                    End If
                Case "B_ARS_Gestartet"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        ProduktionsDatum = Value
                    End If
                    'Prüfen ob die Charge gestartet wurde
                    _Gestartet = ProduktionDatumGueltig(Value)


                Case Else
                    Debug.Print("Field-Name " & Name & " wird nicht ausgewertet")
            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Prüft auf gültiges Produktions-Datum
    ''' (Chargen wurde produziert)
    ''' </summary>
    ''' <param name="d"></param>
    ''' <returns></returns>
    Private Function ProduktionDatumGueltig(d As DateTime) As Boolean
        If d < _ProdDatumGueltig Then
            Return False
        Else
            Return True
        End If
    End Function

End Class
