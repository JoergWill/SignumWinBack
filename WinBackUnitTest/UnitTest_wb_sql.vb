Imports MySql.Data.MySqlClient
Imports WinBack.wb_CreateSQLTables
Imports WinBack
Imports System.Data

<TestClass()> Public Class UnitTest_wb_Sql

    ''' <summary>
    ''' Initialisiert die globalen Einstellungen.
    ''' </summary>
    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()>
    Public Sub TestMySQLPing()
        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql

            Debug.Print("Test Ping MySQL aktiv " & wb_GlobalSettings.SqlConWinBack & " dauert ca.200 Sekunden !!")
            'Text anzeigen
            System.Windows.Forms.Application.DoEvents()

            For i = 1 To 1000
                wb_sql_Functions.ping()
            Next
        End If
    End Sub

    <TestMethod()>
    Public Sub TestMySQL()
        Dim iInt, sString As String
        Dim dDate As Date

        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMySQL Then

            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.mySql
            Debug.Print("Test MySQL aktiv " & wb_GlobalSettings.SqlConWinBack)

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'Tabelle Test erstellen
            winback.sqlCommand("DROP TABLE IF EXISTS Test;")
            winback.sqlCommand("CREATE TABLE Test(L_Nr int(5), L_Bezeichnung varchar(40), LDate datetime) TYPE=MYISAM;")
            winback.sqlCommand("INSERT INTO Test VALUES (1,'T1','2011-01-01 17:35:10');")
            winback.sqlCommand("INSERT INTO Test VALUES (2,'T2','1964-11-22 12:10:00');")
            'ungültiges Datum/Zeit-Format
            winback.sqlCommand("INSERT INTO Test VALUES (3,'T3','1964-22-11 12:10:00');")

            'Daten aus Tabelle Test lesen
            If winback.sqlSelect("SELECT * FROM Test ORDER BY L_Nr") Then
                If winback.Read Then
                    iInt = (winback.iField("L_Nr") + 10).ToString
                    Assert.AreEqual("11", iInt)
                    sString = winback.sField("L_Bezeichnung")
                    Assert.AreEqual("T1", sString)
                    dDate = winback.dField("LDate")
                    'TODO Server 5.0.6 Community-Edition gibt hier ein falsches DATUMSFORMAT zurück
                    'Assert.AreEqual("01.01.2011 17:35:10", dDate.ToString)

                Else
                    Assert.Fail()
                End If
                If winback.Read Then
                    iInt = (winback.iField("L_Nr") + 10).ToString
                    Assert.AreEqual("12", iInt)
                    sString = winback.sField("L_Bezeichnung")
                    Assert.AreEqual("T2", sString)
                    dDate = winback.dField("LDate")
                    Dim xDate As Date = #1964/11/22 12:10:00#
                    Assert.AreEqual(xDate, dDate)
                Else
                    Assert.Fail()
                End If
                If winback.Read Then
                    iInt = (winback.iField("L_Nr") + 10).ToString
                    Assert.AreEqual("13", iInt)
                    sString = winback.sField("L_Bezeichnung")
                    Assert.AreEqual("T3", sString)
                    dDate = winback.dField("LDate")
                    Dim xDate As Date = #1964/11/22 12:00:00#
                    Assert.AreEqual(xDate, dDate)
                Else
                    Assert.Fail()
                End If
            End If

            'Lesen beenden
            winback.CloseRead()
            'Tabelle Test wieder löschen
            winback.sqlCommand("DROP TABLE IF EXISTS Test;")
            'Verbindung schliessen
            winback.Close()
        End If
    End Sub

    <TestMethod()>
    Public Sub TestmsSQL()
        Dim iInt, sString As String

        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMsSQL Then

            'Datenbank Verbindung Einstellungen setzen
            '(Muss in wb_Konfig gesetzt werden, weil My.Setting hier nicht funktioniert)
            wb_GlobalSettings.WinBackDBType = wb_Sql.dbType.msSql
            Debug.Print("Test MSsql aktiv " & wb_GlobalSettings.SqlConWinBack)

            'Datenbank WinBack erstellen - MS-SQL (WinBack darf nicht geöffnet sein)
            DataBaseWinBack(wb_GlobalSettings.OrgaBackMainConString)
            'Datenbank WbDaten erstellen - MS-SQL
            DataBaseWbDaten(wb_GlobalSettings.OrgaBackMainConString)

            'Tabelle WinBack.Komponenten erstellen
            Komponenten(wb_GlobalSettings.SqlConWinBack)
            'Tabelle WinBack.Rezepte erstellen
            Rezepte(wb_GlobalSettings.SqlConWinBack)

            'Tabelle WbDaten.His_Rezepte erstellen
            His_Rezepte(wb_GlobalSettings.SqlConWinBack)

            'Datenbank-Verbindung öffnen - MsSQL
            Dim OrgasoftMain As New wb_Sql(My.Settings.MsSQLConWinBack, wb_Sql.dbType.msSql)

            'Tabelle Test erstellen
            OrgasoftMain.sqlCommand("IF OBJECT_ID('Test', 'U') IS NOT NULL DROP TABLE Test;")
            OrgasoftMain.sqlCommand("CREATE TABLE Test(L_Nr int, L_Bezeichnung varchar(40))")
            OrgasoftMain.sqlCommand("INSERT INTO Test VALUES (1,'T1');")
            OrgasoftMain.sqlCommand("INSERT INTO Test VALUES (2,'T2');")

            'Daten aus Tabelle Test lesen
            If OrgasoftMain.sqlSelect("SELECT * FROM Test ORDER BY L_Nr") Then
                If OrgasoftMain.Read Then
                    iInt = (OrgasoftMain.iField("L_Nr") + 10).ToString
                    Assert.AreEqual("11", iInt)
                    sString = OrgasoftMain.sField("L_Bezeichnung")
                    Assert.AreEqual("T1", sString)
                Else
                    Assert.Fail()
                End If
                If OrgasoftMain.Read Then
                    iInt = (OrgasoftMain.iField("L_Nr") + 10).ToString
                    Assert.AreEqual("12", iInt)
                    sString = OrgasoftMain.sField("L_Bezeichnung")
                    Assert.AreEqual("T2", sString)
                Else
                    Assert.Fail()
                End If
            End If

            'Lesen beenden
            OrgasoftMain.CloseRead()
            'Tabelle Test wieder löschen
            OrgasoftMain.sqlCommand("IF OBJECT_ID('Test', 'U') IS NOT NULL DROP TABLE Test;")
            'Verbindung schliessen
            OrgasoftMain.Close()
        End If

    End Sub

    <TestMethod()>
    Public Sub TestCommandBuilder()
        Dim mySelectQuery As String = "SELECT KO_Nr, KO_Type, KO_Bezeichnung, KO_Kommentar FROM Komponenten;"

        Dim myConn As New MySqlConnection(wb_GlobalSettings.SqlConWinBack)
        Dim myDataAdapter As New MySqlDataAdapter With {.SelectCommand = New MySqlCommand(mySelectQuery, myConn)}
        ' Test-Routine prüft ob die DLL-Version zur Datenbank-Version passt
        ' Der Command-Builder muss anhand der 'SELECT'-Anweisung das Update-Kommando
        ' automatisch erzeugen können
        Dim myCommandBuilder As New MySqlCommandBuilder(myDataAdapter)
        'Verbindung öffnen
        myConn.Open()

        Dim myDataSet As New DataSet
        myDataAdapter.Fill(myDataSet, "Komponenten")

        ' Code to modify data in DataSet here
        myDataSet.Tables(0).Rows(0).Item("KO_Bezeichnung") = "TEST" & Now.ToLongTimeString

        ' Without the MySqlCommandBuilder this line would fail.
        Try
            myDataAdapter.Update(myDataSet, "Komponenten")
        Catch
            Assert.Fail()
        End Try

        ' Verbindung schliessen
        myConn.Close()

    End Sub

End Class
