Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Lieferungen

    ''' <summary>
    ''' Initialisiert die Datenbank-Einstellungen.
    ''' Falls notwendig werden die Datensicherungen aus ... in die DB eingespielt
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub InitDBTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen (OrgaBackDemo - Mandant 3)
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    ''' <summary>
    ''' Löscht alle Einträge in winback.Lieferungen für Komponente mit KO_Nr=3281 (Bienenhonig)
    ''' Die Sub-Routine Import ChargenBestand liest alle entsprechenden Daten aus der Tabelle dbo.ArtikelLagerkarte
    ''' und verarbeitet diese.
    ''' 
    ''' In winback.Lieferungen müssen dann die entsprechenden Datensätze vorhanden sein.
    ''' Beim zweiten Durchgang darf kein Eintrage mehr erzeugt werden (Zurückschreiben der lfd in winback.Lagerorte)
    ''' </summary>
    <TestMethod()> Public Sub Test_WE()
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Daten aus winback.Lieferungen löschen
        winback.sqlCommand("DELETE FROM Lieferungen")
        winback.sqlCommand("UPDATE Lagerorte SET LG_LF_NR = 0 WHERE LG_Ort='KT102_3281'")
        winback.sqlCommand("UPDATE Lagerorte SET LG_Bilanzmenge = '0' WHERE LG_Ort='KT102_3281'")

        'Daten in dbo.ArtikelLagerkarte schreiben (DBUpdate)
        Dim UpdateDatabase As New wb_Admin_UpdateDatabase
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        UpdateDatabase.UpdateMsSqlFile(orgaback, wb_GlobalSettings.pDBUpdatePath & "O.30_UnitTest_Test_WE.sql")

        'Daten aus dbo.ArtikelLagerkarte verarbeiten (für Komponente 3281-Bienenhonig)
        Dim ChargenBestand As New ob_ChargenBestand

        Dim lfd As Integer
        '3281(Bienenhonig) ist die nachfolgende Komponenten-Nummer nach 3280...
        lfd = ChargenBestand.ImportChargenBestand(3280)
        Assert.AreEqual(3281, lfd)

        'in winback.Lieferungen sind 11 Einträge vorhanden
        winback.sqlSelect("Select COUNT(*) FROM Lieferungen WHERE LF_LG_Ort='KT102_3281'")
        Assert.AreEqual(True, winback.Read)
        Assert.AreEqual(1, winback.iField("COUNT(*)"))
        winback.CloseRead()
        'in winback.Lagerorte ist die Bilanzmenge korrigiert worden
        winback.sqlSelect("Select * FROM Lagerorte WHERE LG_Ort='KT102_3281'")
        Assert.AreEqual(True, winback.Read)
        Assert.AreEqual("10,000", winback.sField("LG_Bilanzmenge"))
        winback.CloseRead()

        'beim nächsten Durchlauf dürfen keine neuen Einträge mehr dazu kommen
        lfd = ChargenBestand.ImportChargenBestand(3280)
        Assert.AreEqual(3281, lfd)
        'in winback.Lieferungen sind immer noch 11 Einträge vorhanden
        winback.sqlSelect("Select COUNT(*) FROM Lieferungen WHERE LF_LG_Ort='KT102_3281'")
        Assert.AreEqual(True, winback.Read)
        Assert.AreEqual(1, winback.iField("COUNT(*)"))
        winback.CloseRead()

        'Initialisierung der Bestände (einmalig bei Einrichten OrgaBack)
        lfd = ChargenBestand.ImportChargenBestand(3280, True)
        Assert.AreEqual(3281, lfd)
        'in winback.Lieferungen ist nur noch 1 Eintrag vorhanden
        winback.sqlSelect("Select COUNT(*) FROM Lieferungen WHERE LF_LG_Ort='KT102_3281'")
        Assert.AreEqual(True, winback.Read)
        Assert.AreEqual(1, winback.iField("COUNT(*)"))
        winback.CloseRead()

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Sub

End Class