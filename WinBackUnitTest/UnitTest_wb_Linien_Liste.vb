Imports WinBack


<TestClass()>
Public Class UnitTest_wb_Linien_Liste
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
    Public Sub Test_wb_Linien_Liste()
        Dim LinienListe As New wb_Linien_Liste

        'Die neu erstellte Liste sollte leer sein
        Assert.AreEqual(0, LinienListe.countItems)
        'Eintrag einfügen
        LinienListe.AddItems("1", "Test")
        'Jetzt sollte ein neuer Eintrag vorhanden sein
        Assert.AreEqual(1, LinienListe.countItems)
        'Eintrag wieder löschen
        LinienListe.SelectLastItem()
        LinienListe.RemoveItem()
        'Die Liste sollte wiederleer sein
        Assert.AreEqual(0, LinienListe.countItems)

        'zwei neue Einträge einfügen
        LinienListe.AddItems("1", "Test")
        LinienListe.AddItems("1", "Test")
        Assert.AreEqual(2, LinienListe.countItems)
        LinienListe.SelectLastItem()
        LinienListe.RemoveItem()
        'Assert.AreEqual("Test1", LinienListe.aktBezeichnung)
        Assert.AreEqual(1, LinienListe.countItems)

        'Acht neue einträge einfügen
        Dim i As Integer
        For i = 1 To 7
            LinienListe.AddItems("1", "Test")
        Next
        Assert.AreEqual(8, LinienListe.countItems)
        For i = 1 To 8
            LinienListe.SelectLastItem()
            LinienListe.RemoveItem()
        Next
        Assert.AreEqual(0, LinienListe.countItems)

    End Sub

End Class
