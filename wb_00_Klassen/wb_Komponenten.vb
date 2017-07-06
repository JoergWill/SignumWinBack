Imports MySql.Data.MySqlClient
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_Komponenten
    Inherits wb_ChangeLog

    Private KO_Nr As Integer
    Private KO_Type As KomponTypen
    Private KO_Nr_AlNum As String
    Private KO_Bezeichnung As String
    Private KO_Kommentar As String
    Private LF_Lieferant As String
    Private KO_Backverlust As Double
    Private KO_IdxCloud As String
    Private KA_Rz_Nr As Integer
    Private KA_Lagerort As String

    Private KO_DeklBezeichungExtern As New wb_Hinweise(Hinweise.DeklBezRohstoff)
    Private KO_DeklBezeichungIntern As New wb_Hinweise(Hinweise.DeklBezRohstoffIntern)

    Public NwtUpdate As New wb_Hinweise(Hinweise.NaehrwertUpdate)
    Public ktTyp301 As New wb_KomponParam301

    Public Property Nr As Integer
        Set(value As Integer)
            KO_Nr = value
            'Komponenten-Nummer an Hinweise.NaehrwertUpdate weitergeben
            NwtUpdate.KompNr = value
        End Set
        Get
            Return KO_Nr
        End Get
    End Property

    Public ReadOnly Property Type As KomponTypen
        Get
            Return KO_Type
        End Get
    End Property

    ''' <summary>
    ''' Rohstoff/Artikel-Nummer (alpha-numerisch)
    ''' </summary>
    ''' <returns></returns>
    Public Property Nummer As String
        Set(value As String)
            'Änderungen loggen
            KO_Nr_AlNum = ChangeLogAdd(LogType.Prm, Parameter.Tx_AlNum, KO_Nr_AlNum, value)
        End Set
        Get
            Return KO_Nr_AlNum
        End Get
    End Property

    Public Property Bezeichung As String
        Set(value As String)
            'Änderungen loggen
            KO_Bezeichnung = ChangeLogAdd(LogType.Prm, Parameter.Tx_Bezeichnung, KO_Bezeichnung, value)
        End Set
        Get
            Return KO_Bezeichnung
        End Get
    End Property

    Public Property Kommentar As String
        Set(value As String)
            'Änderungen loggen
            KO_Kommentar = ChangeLogAdd(LogType.Prm, Parameter.Tx_Kommentar, KO_Kommentar, value)
        End Set
        Get
            Return KO_Kommentar
        End Get
    End Property

    Public Property Lieferant As String
        Set(value As String)
            'Änderungen loggen
            LF_Lieferant = ChangeLogAdd(LogType.Prm, Parameter.Tx_Lieferant, LF_Lieferant, value)
        End Set
        Get
            Return LF_Lieferant
        End Get
    End Property

    Public Property TimeStamp As Date
    Public Property BestellNummer As String
    Public Property ZutatenListe As String

    Public Property MatchCode As String
        Get
            Return KO_IdxCloud
        End Get
        Set(value As String)
            KO_IdxCloud = value
        End Set
    End Property

    Public Sub ClearReport()
        ChangeLogClear()
        ktTyp301.ClearReport()
    End Sub

    Public Sub SaveReport()
        Dim Ueberschrift As String = "Änderungen für Rohstoff " & Nummer & " " & Bezeichung & " " & vbNewLine
        Dim Strich = New String("="c, Len(Ueberschrift)) & vbNewLine
        NwtUpdate.Memo = Ueberschrift & Strich & GetReport()
        NwtUpdate.Write()
    End Sub

    Public ReadOnly Property GetReport(Optional ReportAll As Boolean = vbFalse) As String
        Get
            Return ChangeLogReport(ReportAll) & ktTyp301.GetReport(ReportAll)
        End Get
    End Property

    Public Property Deklaration As String
        Get
            'TODO Hier muss unterschieden werden, welche Deklaration benutzt werden soll (Intern/Extern) - kundenspezifisch
            Deklaration = DeklBezeichungExtern
        End Get
        Set(value As String)
            'TODO Hier muss unterschieden werden, welche Deklaration benutzt werden soll (Intern/Extern) - kundenspezifisch
            DeklBezeichungExtern = value
        End Set
    End Property

    Public Property DeklBezeichungExtern As String
        Get
            'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
            If Not KO_DeklBezeichungExtern.ReadOK Then
                KO_DeklBezeichungExtern.Read(KO_Nr)
            End If
            Return KO_DeklBezeichungExtern.Memo
        End Get
        Set(value As String)
            KO_DeklBezeichungExtern.Memo = ChangeLogAdd(LogType.Dkl, Parameter.Tx_DeklarationExtern, DeklBezeichungExtern, value)
        End Set
    End Property

    ''' <summary>
    ''' Liest alle Datenfelder zu der angegebenen Komponenten-Nummer aus der WinBack-Datenbank. Wenn die interne Komponenten-Nummer nicht angegeben ist
    ''' (Kleiner oder gleich Null) dann wird versucht, anhand der Artikel-Nummer den Datensatz zu finden.
    ''' 
    ''' Gibt True zurück, wenn der Datensatz gefunden wurde.
    ''' TODO Was ist zu tun, wenn mehr als ein Datensatz gefunden wurde
    ''' TODO Die interne Nummer an OrgaBack zurückschreiben
    ''' </summary>
    Public Function MySQLdbRead(InterneKomponentenNummer As Integer, Optional KomponentenNummer As String = "") As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach KO_Nr oder KO_AlNum
        If InterneKomponentenNummer > 0 Then
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_KO_Nr, InterneKomponentenNummer)
        Else
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_AlNum, KomponentenNummer)
        End If

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(Sql) Then
            If winback.Read Then
                MySQLdbRead(winback.MySqlRead)
                'TODO ... weiter Parameter lesen
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder aus dem aktuellen Datensatz in das Komponenten-Objekt
    ''' Die Daten werden anhand der Feldbezeichnung in die einzelnen Properties eingetragen
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns>True wenn kein Fehler aufgetreten ist</returns>
    Public Function MySQLdbRead(ByRef sqlReader As MySqlDataReader) As Boolean

        'Stammdaten - Anzahl der Felder im DataSet
        For i = 0 To sqlReader.FieldCount - 1
            MySQLdbRead_StammDaten(sqlReader.GetName(i), sqlReader.GetValue(i))
        Next

        'Schleife über alle Parameter-Datensätze
        'Bis alle Datensätze eingelesen sind
        Do
            'Parameter - Anzahl der Felder im DataSet
            For i = 0 To sqlReader.FieldCount - 1
                MySQLdbRead_Parameter(sqlReader.GetName(i), sqlReader.GetValue(i))
            Next
        Loop While sqlReader.Read
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
        Try
            Select Case Name

                'Nummer(intern)
                Case "KO_Nr"
                    Nr = CInt(Value)
               'Type
                Case "KO_Type"
                    KO_Type = IntToKomponType(CInt(Value))
                'Bezeichnung
                Case "KO_Bezeichnung"
                    KO_Bezeichnung = Value
                'Kommentar
                Case "KO_Kommentar"
                    KO_Kommentar = Value
                'Nummer(alphanumerisch)
                Case "KO_Nr_AlNum"
                    KO_Nr_AlNum = Value
                'Backverlust(Rezept im Rezept)
                Case "KO_Temp_Korr"
                    KO_Backverlust = Value
                'Index WinBack-Cloud
                Case "KA_Matchcode"
                    KO_IdxCloud = Value
                'Index Rezeptnummer(Rezept im Rezept)
                Case "KA_RZ_Nr"
                    KA_Rz_Nr = CInt(Value)
                'Lagerort
                Case "KA_Lagerort"
                    KA_Lagerort = Value

            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Anhand von 
    ''' Parameter-Nummer und Parameter-Typ wird der Wert in das entsprechende Feld
    ''' eingetragen.
    ''' ParamNr und ParamWert müssen definiert sein, bevor der Wert geschrieben werden
    ''' kann!
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Parameter(Name As String, Value As Object) As Boolean
        Static ParamNr, ParamTyp As Integer

        'Feldname aus der Datenbank
        Select Case Name

            'Parameter-Nummer
            Case "RP_ParamNr"
                ParamNr = CInt(Value)

            'Parameter-Typ
            Case "RP_Typ_Nr"
                ParamTyp = CInt(Value)

            'Parameter-Wert
            Case "RP_Wert"
                Select Case ParamTyp
                    Case 200
                        'Produktinformationen
                    Case 201
                        'Verarbeitungs-Hinweise
                    Case 202
                        'Kalkulation/Preise
                    Case 201
                        'Froster
                    Case 220
                        'Teig-Gare
                    Case 301
                        'Nährwert-Informationen
                        ktTyp301.Wert(ParamNr) = Value.ToString
                End Select

            'TimeStamp
            Case "RP_Timestamp"
                ktTyp301.TimeStamp = CDate(Value.ToString)

        End Select
        Return True
    End Function

    ''' <summary>
    ''' schreibt alle Datenfelder aus dem Komponenten-Objekt mir der angegebenen Komponenten-Nummer in die Datenbank.
    ''' TODO wenn KO-Nummer nicht definiert
    ''' TODO INSERT MYSQL
    ''' </summary>
    ''' <param name="InterneKomponentenNummer"></param>
    ''' <returns></returns>
    Public Function MySQLdbWrite(InterneKomponentenNummer As Integer) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Update-Statement wird dynamisch erzeugt    
        sql = "KO_Nr_AlNum = '" & Nummer & "'," &
              "KO_Bezeichnung = '" & Bezeichung & "'"
        'Update ausführen
        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKomp_KO_Nr, Nr, sql)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub print()
        Debug.Print("Nummer      " & KO_Nr_AlNum)
        Debug.Print("Bezeichung  " & KO_Bezeichnung)
        Debug.Print("Lieferant   " & LF_Lieferant)
        Debug.Print("Deklaration " & Deklaration)
    End Sub

End Class
