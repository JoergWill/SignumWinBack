Imports Signum.OrgaSoft.AddIn
Imports MySql.Data.MySqlClient
Imports Signum.OrgaSoft.AddIn.wb_CreateSQLTables

<TestClass()>
Public Class UnitTest_wb_Sql

    Private testContextInstance As TestContext

    '''<summary>
    '''Ruft den Textkontext mit Informationen über
    '''den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Zusätzliche Testattribute"
    '
    ' Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
    '
    ' Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Mit TestCleanup können Sie nach jedem Test Code ausführen.
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <TestMethod()>
    Public Sub TestMySQL()
        Dim iInt, sString As String

        'Test wird nur ausgeführt, wenn die Datenbank verfügbar ist
        If My.Settings.TestMsSQL Then

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback As New wb_Sql("server=172.16.17.208;user id=herbst;password=herbst;database=winback;", wb_Sql.dbType.mySql)

            'Tabelle Test erstellen
            winback.sqlCommand("DROP TABLE IF EXISTS Test;")
            winback.sqlCommand("CREATE TABLE Test(L_Nr int(5), L_Bezeichnung varchar(40)) TYPE=MYISAM;")
            winback.sqlCommand("INSERT INTO Test VALUES (1,'T1');")
            winback.sqlCommand("INSERT INTO Test VALUES (2,'T2');")

            'Daten aus Tabelle Test lesen
            If winback.sqlSelect("SELECT * FROM Test ORDER BY L_Nr") Then
                If winback.Read Then
                    iInt = (winback.iField("L_Nr") + 10).ToString
                    Assert.AreEqual("11", iInt)
                    sString = winback.sField("L_Bezeichnung")
                    Assert.AreEqual("T1", sString)
                Else
                    Assert.Fail()
                End If
                If winback.Read Then
                    iInt = (winback.iField("L_Nr") + 10).ToString
                    Assert.AreEqual("12", iInt)
                    sString = winback.sField("L_Bezeichnung")
                    Assert.AreEqual("T2", sString)
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

            'Datenbank WinBack erstellen - MS-SQL
            DataBaseWinBack("Data Source=127.0.0.1\SIGNUM; Database=OrgaSoftMain; Integrated Security=True")
            'Tabelle Komponenten erstellen
            Komponenten("Data Source=127.0.0.1\SIGNUM; Database=WinBack; Integrated Security=True")

            'Datenbank-Verbindung öffnen - MySQL
            Dim OrgasoftMain As New wb_Sql("Data Source=127.0.0.1\SIGNUM; Database=WinBack; Integrated Security=True", wb_Sql.dbType.msSql)

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
        Dim myConn As New MySqlConnection("server=172.16.17.231;user id=herbst;password=herbst;database=winback;")
        Dim myDataAdapter As New MySqlDataAdapter()
        myDataAdapter.SelectCommand = New MySqlCommand(mySelectQuery, myConn)
        ' Test-Routine prüft ob die DLL-Version zur Datenbank-Version passt
        ' Der Command-Builder muss anhand der 'SELECT'-Anweisung das Update-Kommando
        ' automatisch erzeugen können
        Dim myCommandBuilder As MySqlCommandBuilder = New MySqlCommandBuilder(myDataAdapter)
        'Verbindung öffnen
        myConn.Open()

        Dim myDataSet As DataSet = New DataSet
        myDataAdapter.Fill(myDataSet, "Komponenten")

        ' Code to modify data in DataSet here
        Debug.Print("DataSet-Data " & myDataSet.Tables(0).Rows(0).Item("KO_Bezeichnung"))
        myDataSet.Tables(0).Rows(0).Item("KO_Bezeichnung") = "TEST" & Now.ToLongTimeString
        Debug.Print("DataSet-Data " & myDataSet.Tables(0).Rows(0).Item("KO_Bezeichnung"))

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
