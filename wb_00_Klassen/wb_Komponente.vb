﻿Imports MySql.Data.MySqlClient
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_Komponente
    Inherits wb_ChangeLog

    Private KO_Nr As Integer
    Private KO_Type As KomponTypen
    Private KA_Art As String
    Private KO_Nr_AlNum As String
    Private KO_Bezeichnung As String
    Private KO_Kommentar As String
    Private KA_Kurzname As String
    Private LF_Lieferant As String
    Private KO_Backverlust As Double
    Private KO_IdxCloud As String
    Private KA_Rz_Nr As Integer
    Private KA_Lagerort As String
    Private _LastErrorText As String
    Private _RezeptNummer As String = Nothing
    Private _RezeptName As String = Nothing
    Private _LinienGruppe As Integer = wb_Global.UNDEFINED
    Private _ArtikelLinienGruppe As Integer = wb_Global.UNDEFINED

    Private KO_DeklBezeichnungExtern As New wb_Hinweise(Hinweise.DeklBezRohstoff)
    Private KO_DeklBezeichnungIntern As New wb_Hinweise(Hinweise.DeklBezRohstoffIntern)

    Public NwtUpdate As New wb_Hinweise(Hinweise.NaehrwertUpdate)
    Public ktTyp300 As New wb_KomponParam300
    Public ktTyp301 As New wb_KomponParam301

    Public ArtikelChargen As New wb_MinMaxOptCharge
    Public TeigChargen As New wb_MinMaxOptCharge

    Private Shared _ProduktionsStufe As wb_Komponente
    Private Shared _Kessel As wb_Komponente
    Private Shared _TextKomponente As wb_Komponente


    Public Sub Invalid()
        KO_Nr = wb_Global.UNDEFINED
        KO_Type = wb_Global.KomponTypen.KO_TYPE_UNDEFINED
        KA_Art = wb_Global.UNDEFINED
        KO_Nr_AlNum = "-1"
        KO_Bezeichnung = ""
        KO_Kommentar = ""
        KA_Kurzname = ""

        KA_Rz_Nr = wb_Global.UNDEFINED
        _RezeptNummer = Nothing
        _RezeptName = Nothing
        _LinienGruppe = wb_Global.UNDEFINED
        _ArtikelLinienGruppe = wb_Global.UNDEFINED
    End Sub

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

    Public Property Bezeichnung As String
        Set(value As String)
            'Änderungen loggen
            KO_Bezeichnung = ChangeLogAdd(LogType.Prm, Parameter.Tx_Bezeichnung, KO_Bezeichnung, wb_Functions.XRemoveSonderZeichen(value))
        End Set
        Get
            Return KO_Bezeichnung
        End Get
    End Property

    Public Property Kommentar As String
        Set(value As String)
            'Änderungen loggen
            KO_Kommentar = ChangeLogAdd(LogType.Prm, Parameter.Tx_Kommentar, KO_Kommentar, wb_Functions.XRemoveSonderZeichen(value))
        End Set
        Get
            Return KO_Kommentar
        End Get
    End Property

    Public Property Kurzname As String
        Set(value As String)
            'Änderungen loggen
            KA_Kurzname = ChangeLogAdd(LogType.Prm, Parameter.Tx_Kommentar, KA_Kurzname, wb_Functions.XRemoveSonderZeichen(value))
        End Set
        Get
            Return KA_Kurzname
        End Get
    End Property

    Public Property RzNr As Integer
        Get
            Return KA_Rz_Nr
        End Get
        Set(value As Integer)
            KA_Rz_Nr = value
            'KA_Art setzen (Für Artikel immmer gleich Eins)
            If KA_Rz_Nr > 0 Or KO_Type = KomponTypen.KO_TYPE_ARTIKEL Then
                KA_Art = "1"
            Else
                KA_Art = "0"
            End If
        End Set
    End Property

    Public Property RezeptNummer As String
        Get
            If _RezeptNummer Is Nothing Then
                GetProduktionsDaten()
            End If
            Return _RezeptNummer
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property RezeptName As String
        Get
            If _RezeptName Is Nothing Then
                GetProduktionsDaten()
            End If
            Return _RezeptName
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property LinienGruppe As Integer
        Get
            If _LinienGruppe = wb_Global.UNDEFINED Then
                GetProduktionsDaten()
            End If
            Return _LinienGruppe
        End Get
        Set(value As Integer)
            _LinienGruppe = value
        End Set
    End Property

    Public Property ArtikelLinienGruppe As Integer
        Get
            If _ArtikelLinienGruppe = wb_Global.UNDEFINED Then
                GetProduktionsDaten()
            End If
            Return _ArtikelLinienGruppe
        End Get
        Set(value As Integer)
            _ArtikelLinienGruppe = value
        End Set
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

    ''' <summary>
    ''' Daten für die Produktion dieser Komponente ermitteln. 
    '''     Teig-Rezept aus Rezept-im-Rezept-Struktur
    '''     Liniengruppe aus RohParams(5)
    '''     Artikel-Rezept aus RohParams(6)
    ''' </summary>
    Private Sub GetProduktionsDaten()
        If RzNr > 0 Then
            'Teig-Rezeptur
            Dim Rezept As New wb_Rezept(RzNr)
            _RezeptNummer = Rezept.RezeptNummer
            _RezeptName = Rezept.RezeptBezeichnung
            _LinienGruppe = Rezept.LinienGruppe
            ArtikelChargen.TeigGewicht = Rezept.RezeptGewicht
            TeigChargen = Rezept.TeigChargen

        Else
            'normale Komponente ohne Produktion
            _RezeptName = ""
            _RezeptNummer = ""
            _LinienGruppe = wb_Global.UNDEFINED
        End If

        'Artikel-Typ = 1 für Auto/Handkomponenten mit anhängender Rezeptur
        If KO_Type = KomponTypen.KO_TYPE_ARTIKEL Or RzNr > 0 Then
            KA_Art = 1
        Else
            KA_Art = 0
        End If

        If ktTyp300.Liniengruppe > 0 Then
            'Produktions-Liniengruppe aus RohParams(5)
            _ArtikelLinienGruppe = ktTyp300.Liniengruppe
        End If
        If ktTyp300.RzNr > 0 Then
            'Artikel-Rezeptur
            Dim Rezept As New wb_Rezept(ktTyp300.RzNr)
            _ArtikelLinienGruppe = Rezept.LinienGruppe
        End If
    End Sub

    Public Sub ClearReport()
        ChangeLogClear()
        ktTyp301.ClearReport()
    End Sub

    Public Sub SaveReport()
        Dim Ueberschrift As String = "Änderungen für Rohstoff " & Nummer & " " & Bezeichnung & " " & vbNewLine
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
            If Not KO_DeklBezeichnungExtern.ReadOK Then
                KO_DeklBezeichnungExtern.Read(KO_Nr)
            End If
            Return KO_DeklBezeichnungExtern.Memo
        End Get
        Set(value As String)
            KO_DeklBezeichnungExtern.Memo = ChangeLogAdd(LogType.Dkl, Parameter.Tx_DeklarationExtern, DeklBezeichungExtern, value)
        End Set
    End Property

    Public ReadOnly Property LastErrorText As String
        Get
            Return _LastErrorText
        End Get
    End Property

    ''' <summary>
    ''' Standard-Komponente für Produktions-Stufen.
    ''' Es wird versucht, die erste Komponente mit der passenden Komponenten-Type aus dem Komponenten-Stamm zu lesen. Wenn keine
    ''' passende Komponente gefunden wurde, wird ein Dummy neu angelegt.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property ProduktionsStufe As wb_Komponente
        Get
            If _ProduktionsStufe Is Nothing Then
                _ProduktionsStufe = New wb_Komponente
                If Not _ProduktionsStufe.MysqldbRead(wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) Then
                    _ProduktionsStufe.KO_Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
                    _ProduktionsStufe.Bezeichnung = "Produktions-Stufe"
                    _ProduktionsStufe.Nummer = "PST"
                    _ProduktionsStufe.Nr = wb_sql_Functions.getNewKomponNummer()
                End If
            End If
            Return _ProduktionsStufe
        End Get
    End Property

    Public Shared ReadOnly Property Kessel As wb_Komponente
        Get
            _Kessel = New wb_Komponente
            If Not _Kessel.MysqldbRead(wb_Global.KomponTypen.KO_TYPE_KESSEL) Then
                _Kessel.KO_Type = wb_Global.KomponTypen.KO_TYPE_KESSEL
                _Kessel.Bezeichnung = "Kessel"
                _Kessel.Nummer = "KSL"
                _Kessel.Nr = wb_sql_Functions.getNewKomponNummer()
            End If
            Return _Kessel
        End Get
    End Property

    Public Shared ReadOnly Property TextKomponente As wb_Komponente
        Get
            _TextKomponente = New wb_Komponente
            If Not _TextKomponente.MysqldbRead(wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE) Then
                _TextKomponente.KO_Type = wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
                _TextKomponente.Bezeichnung = "Text"
                _TextKomponente.Nummer = "TXT"
                _TextKomponente.Nr = wb_sql_Functions.getNewKomponNummer()
            End If
            Return _TextKomponente
        End Get
    End Property

    ''' <summary>
    ''' Objekt ist ungültig. Vor der nächsten Verwendung muss wieder neu eingelesen werden.
    ''' </summary>
    Public Sub Invalidate()
        _RezeptName = Nothing
        _RezeptNummer = Nothing
    End Sub

    ''' <summary>
    ''' Prüft ob der Rohstoff/Artikel noch verwendet wird. (Prüfung ob Löschen zulässig ist)
    ''' 
    ''' Abhängig von der Komponenten-Type wird geprüft ob:
    '''     Artikel     -   Verwendung in Arbeits-Rezepte-Tabelle
    '''     Rohstoff    -   Verwendung in Arbeits-Rezepte-Tabelle
    '''                     Verwendung in Rezeptschritte-Tabelle
    ''' </summary>
    ''' <returns>False - Rohstoff/Artikel wird verwendet
    ''' True - Rohstoff/Artikel wird nicht mehr verwendet (kann gelöscht werder)</returns>
    Public Function MySQLdbCanBeDeleted(InterneKomponentenNummer As Integer, Optional KomponentenNummer As String = "") As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach KO_Nr oder KO_AlNum
        If InterneKomponentenNummer > 0 Then
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_KO_Nr, InterneKomponentenNummer)
        Else
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_AlNum, KomponentenNummer)
        End If

        'ersten Datensatz aus Tabelle Komponenten lesen
        If Not winback.sqlSelect(sql) Then
            winback.Close()
            Return True
        Else
            If Not winback.Read Then
                winback.Close()
                Debug.Print("Datensatz nicht gefunden - Löschen freigegeben")
                Return True
            Else
                'Stammdaten - Anzahl der Felder im DataSet
                For i = 0 To winback.MySqlRead.FieldCount - 1
                    MySQLdbRead_StammDaten(winback.MySqlRead.GetName(i), winback.MySqlRead.GetValue(i))
                Next
            End If
        End If

        'Datenbank-Verbindung wieder schliessen
        winback.Close()

        Debug.Print("Anfrage Löschen Komponente " & KO_Nr & "/" & KO_Nr_AlNum)

        'Abhängig von der Komponenten-Type
        Select Case Type

            'Automatik-Rohstoffe dürfen nicht gelöscht werden
            Case KomponTypen.KO_TYPE_AUTOKOMPONENTE, KomponTypen.KO_TYPE_WASSERKOMPONENTE, KomponTypen.KO_TYPE_EISKOMPONENTE
                _LastErrorText = "Rohstoffe, die automatisch dosiert werden, können nicht gelöscht werden !"
                Return False

            'Sauerteig-Rohstoffe dürfen nicht gelöscht werden
            Case KomponTypen.KO_TYPE_SAUER_MEHL, KomponTypen.KO_TYPE_SAUER_WASSER, KomponTypen.KO_TYPE_SAUER_ZUGABE, KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE
                _LastErrorText = "Sauerteig-Rohstoffe, die automatisch dosiert werden, können nicht gelöscht werden !"
                Return False

            'Verkaufs-Artikel - Verwendung in der Produktion prüfen
            Case KomponTypen.KO_TYPE_ARTIKEL
                If MySQLIsUsedInProduction(KO_Nr) Then
                    _LastErrorText = "Dieser Artikel wird In der Produktion noch verwendet und kann nicht gelöscht werden"
                    Return False
                Else
                    Return True
                End If

            'Rohstoff - Verwendung in der Produktion und in Rezepten prüfen
            Case KomponTypen.KO_TYPE_HANDKOMPONENTE
                If MySQLIsUsedInProduction(KO_Nr) Then
                    _LastErrorText = "Dieser Rohstoff wird In der Produktion noch verwendet und kann nicht gelöscht werden"
                    Return False
                ElseIf MySQLIsUsedInRecipe(KO_Nr) Then
                    _LastErrorText = "Dieser Rohstoff wird noch In Rezepturen verwendet und kann nicht gelöscht werden"
                    Return False
                Else
                    Return True
                End If

            Case Else
                Return True
        End Select
    End Function

    ''' <summary>
    ''' Ermittelt die Anzahl der Datensätze in der Tabelle wbdaten.ArbRzSchritte mit der übergebenen Komponenten-Nummer
    ''' Ist die Anzahl der Datensätze gleich Null, wird True zurückgegeben sonst False.
    ''' </summary>
    ''' <param name="InterneKomponentenNummer">Integer - Interne Komponenten-Nummer</param>
    ''' <returns>Boolean - Löschen ist erlaubt</returns>
    Private Function MySQLIsUsedInProduction(InterneKomponentenNummer As Integer) As Boolean
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompInArbRzp, InterneKomponentenNummer)
        Dim Count As Integer = -1

        'Suche nach KO_Nr
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                Count = winback.iField("Used")
            End If
        End If
        'Datenbank wieder schliessen
        winback.Close()
        Debug.Print("MySQLIsUsedInProduction " & Count.ToString)

        'Löschen erlaubt, wenn die Anzahl der Datensätze gleich Null ist
        Return (Count <> 0)
    End Function

    ''' <summary>
    ''' Ermittelt die Anzahl der Datensätze in der Tabelle winback.RezeptSchritte mit der übergebenen Komponenten-Nummer
    ''' Ist die Anzahl der Datensätze gleich Null, wird True zurückgegeben sonst False.
    ''' </summary>
    ''' <param name="InterneKomponentenNummer">Integer - Interne Komponenten-Nummer</param>
    ''' <returns>Boolean - Löschen ist erlaubt</returns>
    Private Function MySQLIsUsedInRecipe(InterneKomponentenNummer) As Boolean
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompInRezept, InterneKomponentenNummer)
        Dim Count As Integer = -1

        'Suche nach KO_Nr
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                Count = winback.iField("Used")
            End If
        End If
        'Datenbank wieder schliessen
        winback.Close()
        Debug.Print("MySQLIsUsedInRecipe " & Count.ToString)

        'Löschen erlaubt, wenn die Anzahl der Datensätze gleich Null ist
        Return (Count <> 0)
    End Function

    ''' <summary>
    ''' Löscht alle Einträge zur aktuellen Komponenten-Nummer aus der WinBack-Datenbank
    '''     - winback.Lagerorte     (LG_Ort)
    '''     - winback.Lieferungen   (LF_LG_Ort)
    '''     - winback.Komponenten   (KO_Nr)
    '''     - winback.KomponParams  (KO_Nr)
    '''     - winback.Hinweise2     (KO_Nr)
    '''     - winback.RohParams     (KO_Nr)
    '''     
    ''' Die Datenfelder KO_Nr und KA_Lagerort müssen in MySQLdbRead_StammDaten vorab gelesen worden sein.
    ''' (Routine MySQLdbCanBeDeleted) 
    ''' </summary>
    Public Sub MySQLdbDelete()
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Interne Komponenten-Nummer muss definiert sein
        If KO_Nr > 0 Then
            'Löschen Komponente in winback.Komponenten
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelKomponenten, KO_Nr))
            'Löschen Komponente in winback.KomponParams
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelKomponParams, KO_Nr))
            'Löschen Komponente in winback.Hinweise2
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelKompHinweise, KO_Nr))
            'Löschen Komponente in winbackRohParams
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRohParams, KO_Nr))

            'Der Lagerort muss definiert sein
            If KA_Lagerort <> "" Then
                'Löschen winback.LagerOrte
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelLagerOrte, KA_Lagerort))
                'Löschen winback.KLieferungen
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelLieferungen, KA_Lagerort))
            End If

        End If
        winback.Close()
    End Sub

    ''' <summary>
    ''' Komponenten-Datensatz neu anlegen
    ''' Es werden nur die Komponenten-Nummern (intern/extern) und die Komponenten-Type angelegt.
    ''' Die Komponenten-Bezeichnung ist "Neu angelegt " mit Datum/Uhrzeit
    ''' 
    ''' Alle weiteren Komponenten-Daten werden mit MySQLdbUpdate eingetragen.
    ''' </summary>
    ''' <param name="KType">KomponTypen - Komponenten-Type der anzulegenden Komponente</param>
    ''' <returns>Integer - neu anglegte (interne) Komponenten-Nummer</returns>
    Public Function MySQLdbNew(KType As wb_Global.KomponTypen) As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'interne Komponenten-Nummer ermitteln aus max(KO_NR)
        KO_Nr = wb_sql_Functions.getNewKomponNummer()
        'Komponenten-Type (Artikel/Handkomponente)
        KO_Type = KType
        'Komponenten-Art (vorab) festlegen
        If KO_Type = KomponTypen.KO_TYPE_ARTIKEL Then
            KA_Art = "1"
        Else
            KA_Art = "0"
        End If

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewKompon, KO_Nr, KO_Nr_AlNum, wb_Functions.KomponTypeToInt(KO_Type), "Neu angelegt " & Date.Now))
        winback.Close()
        'neuen KompNummer zurückgeben
        Return KO_Nr
    End Function

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
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach KO_Nr oder KO_AlNum
        If InterneKomponentenNummer > 0 Then
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_KO_Nr, InterneKomponentenNummer)
        Else
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_AlNum, KomponentenNummer)
        End If

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                MySQLdbRead(winback.MySqlRead)
                winback.CloseRead()
                'weitere Parameter einlesen
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompTypXXX, Nr)
                If winback.sqlSelect(sql) Then
                    If winback.Read Then
                        MySQLdbRead(winback.MySqlRead)
                    End If
                End If
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    Public Function MysqldbRead(KomponType As wb_Global.KomponTypen)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String
        'Suche nach dem ersten Datensatz mit dieser Komponenten-Type
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_KO_Type, wb_Functions.KomponTypeToInt(KomponType))

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                MySQLdbRead(winback.MySqlRead)
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder aus dem aktuellen Datensatz in das Komponenten-Objekt
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
                MySQLdbRead_StammDaten(sqlReader.GetName(i), sqlReader.GetValue(i))
            Catch ex As Exception
            End Try
        Next

        'Schleife über alle Parameter-Datensätze
        'Bis alle Datensätze eingelesen sind
        Do
            'Parameter - Anzahl der Felder im DataSet
            'FieldCount-2 unterdrückt das Feld TimeStamp
            For i = 0 To sqlReader.FieldCount - 2
                Try
                    MySQLdbRead_Parameter(sqlReader.GetName(i), sqlReader.GetValue(i))
                Catch ex As Exception
                End Try
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
        'Debug.Print("ReadStammdaten " & Name)
        Try
            Select Case Name

                'Nummer(intern)
                Case "KO_Nr"
                    Nr = CInt(Value)
               'Type
                Case "KO_Type"
                    KO_Type = IntToKomponType(CInt(Value))
               'Type
                Case "KA_Art"
                    KA_Art = Value
                'Bezeichnung
                Case "KO_Bezeichnung"
                    KO_Bezeichnung = wb_Functions.MySqlToUtf8(Value)
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

                'Stückgewicht in Gramm
                Case "KA_Stueckgewicht"
                    If Type = KomponTypen.KO_TYPE_ARTIKEL Then
                        ArtikelChargen.StkGewicht = Value
                    End If
            End Select

            'Artikel - Chargengrößen in Stück
            If Type = KomponTypen.KO_TYPE_ARTIKEL Then

                Select Case Name
                'Minimal-Charge
                    Case "KA_Charge_Min"
                        ArtikelChargen.MinCharge.MengeInStk = Value
                'Maximal-Charge
                    Case "KA_Charge_Max"
                        ArtikelChargen.MaxCharge.MengeInStk = Value
                'Optimal-Charge
                    Case "KA_Charge_Opt"
                        ArtikelChargen.OptCharge.MengeInStk = Value
                End Select
            End If

            'Rohstoffe - Chargengrößen in kg
            If Type = KomponTypen.KO_TYPE_HANDKOMPONENTE Or Type = KomponTypen.KO_TYPE_AUTOKOMPONENTE Then

                Select Case Name
                'Minimal-Charge
                    Case "KA_Charge_Min_kg"
                        ArtikelChargen.MinCharge.MengeInkg = Value
                'Maximal-Charge
                    Case "KA_Charge_Max_kg"
                        ArtikelChargen.MaxCharge.MengeInkg = Value
                'Optimal-Charge
                    Case "KA_Charge_Opt_kg"
                        ArtikelChargen.OptCharge.MengeInkg = Value
                End Select
            End If

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
        'Debug.Print("ReadParameter " & Name)
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
                    Case 300
                        'Parameter Produktion
                        ktTyp300.Wert(ParamNr) = Value.ToString
                    Case 301
                        'Nährwert-Informationen
                        ktTyp301.Wert(ParamNr) = Value.ToString
                End Select

            'TimeStamp
            Case "RP_Timestamp"
                Select Case ParamTyp
                    Case 301
                        'Nährwert-Informationen
                        ktTyp301.TimeStamp = CDate(Value.ToString)
                End Select
        End Select
        Return True
    End Function

    ''' <summary>
    ''' schreibt alle Datenfelder aus dem Komponenten-Objekt mit der angegebenen Komponenten-Nummer in die Datenbank.
    ''' </summary>
    ''' <param name="InterneKomponentenNummer"></param>
    ''' <returns></returns>
    Public Function MySQLdbUpdate(InterneKomponentenNummer As Integer) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Update-Statement wird dynamisch erzeugt    
        sql = "KO_Nr_AlNum = '" & Nummer & "'," &
              "KO_Bezeichnung = '" & Bezeichnung & "'," &
              "KO_Kommentar = '" & Kommentar & "'," &
              "KO_Temp_Korr = '" & KO_Backverlust & "'," &
              "KA_Matchcode = '" & KO_IdxCloud & "'," &
              "KA_Lagerort = '" & KA_Lagerort & "'," &
              "KA_Stueckgewicht = '" & ArtikelChargen.StkGewicht & "'," &
              "KA_Art = '" & KA_Art & "'"

        'Rezeptnummer nur updaten wenn gültig
        If KA_Rz_Nr <> wb_Global.UNDEFINED Then
            sql = sql & "," &
                        "KA_RZ_Nr = " & KA_Rz_Nr.ToString
        End If

        'Artikel - Chargengrößen in Stk
        If Type = wb_Functions.IntToKomponType(wb_Global.KomponTypen.KO_TYPE_ARTIKEL) Then
            sql = sql & "," &
                        "KA_Charge_Min = '" & ArtikelChargen.MinCharge.MengeInStk & "'," &
                        "KA_Charge_Max = '" & ArtikelChargen.MaxCharge.MengeInStk & "'," &
                        "KA_Charge_Opt = '" & ArtikelChargen.OptCharge.MengeInStk & "'"
        End If

        'Rohstoffe - Chargengrößen in kg
        If Type = wb_Functions.IntToKomponType(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE) _
        Or Type = wb_Functions.IntToKomponType(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE) Then
            sql = sql & "," &
                        "KA_Charge_Min_kg = '" & ArtikelChargen.MinCharge.MengeInkg & "'," &
                        "KA_Charge_Max_kg = '" & ArtikelChargen.MaxCharge.MengeInkg & "'," &
                        "KA_Charge_Opt_kg = '" & ArtikelChargen.OptCharge.MengeInkg & "'"
        End If

        'Update ausführen
        'Debug.Print("Komponente.MysqldbUpdate " & sql)

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