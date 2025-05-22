Imports MySql.Data.MySqlClient

Public Class wb_KneterSchritt

    Private _Index As Integer
    Private _ParaNr As wb_Global.KneterParameter
    Private _Status As wb_Global.ChargenStatus
    Private _RezeptBezeichnung As String
    Private _RezeptNummer As String
    Private _KompBezeichnung As String
    Private _Sollwert As String
    Private _Istwert As String

    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
        End Set
    End Property

    ''' <summary>
    ''' Parameter.
    ''' Abhängig vom Parameter werden Soll-/Istwert interpretiert
    ''' 
    '''     1   -   Mischen Rechtslauf          hh:mm:ss
    '''     2   -   Mischen Linkslauf           hh:mm:ss
    '''     3   -   Kneten Rechtslauf           hh:mm:ss
    '''     4   -   Kneten Linkslauf            hh:mm:ss
    '''     5   -   Teigtemperaturmessung       ##,# °C
    ''' </summary>
    ''' <returns></returns>
    Public Property ParaNr As UShort
        Get
            Return _ParaNr
        End Get
        Set(value As UShort)
            _ParaNr = value
        End Set
    End Property

    Public Property Status As wb_Global.ChargenStatus
        Get
            Return _Status
        End Get
        Set(value As wb_Global.ChargenStatus)
            _Status = value
        End Set
    End Property

    Public Property Sollwert As String
        Get
            Return _Sollwert
        End Get
        Set(value As String)
            _Sollwert = value
        End Set
    End Property

    Public Property dSollwert As UInt32
        Get
            Return ConvertKneterParameter(ParaNr, _Sollwert)
        End Get
        Set(value As UInt32)

        End Set
    End Property

    Public Property dIstwert As UInt32
        Get
            Return ConvertKneterParameter(ParaNr, _Istwert)
        End Get
        Set(value As UInt32)

        End Set
    End Property

    Public ReadOnly Property dProzent As Double
        Get
            Return Math.Min(100, wb_Functions.ProzentSatz(dSollwert, dIstwert))
        End Get
    End Property

    Public Property Istwert As String
        Get
            Return _Istwert
        End Get
        Set(value As String)
            _Istwert = value
        End Set
    End Property

    Public Property KompBezeichnung As String
        Get
            Return _KompBezeichnung
        End Get
        Set(value As String)
            _KompBezeichnung = value
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

    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = value
        End Set
    End Property

    Private Function ConvertKneterParameter(ParaNr As wb_Global.KneterParameter, Wert As String) As UInt32
        Select Case ParaNr
            Case wb_Global.KneterParameter.MischenRechts, wb_Global.KneterParameter.MischenLinks, wb_Global.KneterParameter.KnetenRechts, wb_Global.KneterParameter.KnetenLinks, wb_Global.KneterParameter.Teigruhe
                Return wb_Functions.ConvertTimeStringToSeconds(Wert)
            Case wb_Global.KneterParameter.TeigTemperatur
                Return wb_Functions.StrToInt(Wert) * 10
            Case Else
                Return wb_Functions.StrToInt(Wert)
        End Select
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder aus dem aktuellen Datensatz in das Kneter-Objekt
    ''' Die Daten werden anhand der Feldbezeichnung in die einzelnen Properties eingetragen.
    ''' 
    ''' Das letzte Datenfeld ist der TimeStamp und wird NICHT eingelesen, da es Probleme mit
    ''' der Konvertierung von MySQLDateTime in DateTime gibt
    ''' (https://bugs.mysql.com/bug.php?id=87120)
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns>True wenn kein Fehler aufgetreten ist</returns>
    Public Function MySQLdbRead(ByRef sqlReader As MySqlDataReader) As Boolean
        'Stammdaten - Anzahl der Felder im DataSet
        'FieldCount-2 unterdrückt das Feld TimeStamp
        For i = 0 To sqlReader.FieldCount - 2
            Try
                If sqlReader.GetDataTypeName(i) <> "TIMESTAMP" Then
                    MySQLdbRead_StammDaten(sqlReader.GetName(i), sqlReader.GetValue(i))
                End If

            Catch ex As Exception
                Debug.Print("Exception MySQLdbReadStammdaten" & ex.Message)
            End Try
        Next
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Der Feldname bestimmt das Ziel
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_StammDaten(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        'Debug.Print("ReadStammdaten " & Name & "/" & Value)
        Try
            Select Case Name

                'Index
                Case "ARS_Index"
                    Index = Value

                'Status
                Case "ARS_Status"
                    Status = Value

                'Rezept-Bezeichnung
                Case "ARZ_Bezeichnung"
                    RezeptBezeichnung = Value

                'Rezept-Nummer
                Case "ARZ_RZ_Nr_AlNum"
                    RezeptNummer = Value

                'Komponenten-Bezeichnung (Kneterschritt)
                Case "ARS_KO_Bezeichnung"
                    KompBezeichnung = Value

                'Sollwert (korrigiert und berechnet)
                Case "ARS_Wert"
                    Sollwert = Value

                'Sollwert (korrigiert und berechnet)
                Case "ARS_Istwert"
                    Istwert = Value

                'Parameter-Nummer (aus INNER JOIN KomponParams ARS_KO_Nr=KP_Ko_Nr) 
                Case "KP_Wert"
                    ParaNr = CInt(Value)
            End Select

        Catch ex As Exception
            Trace.WriteLine("I@_Fehler beim Lesen der Arbeitsrezept-Daten Kneter")
        End Try
        Return True
    End Function


End Class
