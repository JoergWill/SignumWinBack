Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_ob_ProduzierteWare

    ''' <summary>
    ''' Initialisiert die Datenbank-Einstellungen.
    ''' Falls notwendig werden die Datensicherungen aus ... in die DB eingespielt
    ''' </summary>
    ''' <param name="testContext"></param>
    <ClassInitialize()> Public Shared Sub InitDBTests(ByVal testContext As TestContext)
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub TestExportChargen()

        'Tabelle dbo.ProduzierteWare komplett leeren
        Dim OrgaSoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        OrgaSoftMain.sqlCommand("DELETE FROM ProduzierteWare")

        'Tabelle BAK_ArbRezepte vorbereiten
        '26 Datensätze mit TW-Nummer 99999 in wbdaten.BAK_ArbRezepte anlegen
        Dim UpdateDatabase As New wb_Admin_UpdateDatabase
        Dim wbdaten As New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        UpdateDatabase.UpdateSqlFile(wbdaten, wb_GlobalSettings.pDBUpdatePath & "2.30_UnitTest_TestExportChargen.sql")
        'wbdaten.sqlCommand("Update BAK_ArbRezepte SET B_ARZ_Status = ''")

        'Export Chargen ab TW-Nr.99999
        Dim Export As New ob_Chargen_Produziert
        Dim e As Integer = Export.ExportChargen(99999)

        'Danach müssen in der Tabelle dbo.ProduzierteWare 26 Datensätze stehen
        OrgaSoftMain.sqlSelect("Select * FROM ProduzierteWare")
        'Anzahl aller Datensätze
        Dim i As Integer = 0
        While OrgaSoftMain.Read
            i += 1
        End While
        'Lesen aus DB beenden
        OrgaSoftMain.CloseRead()

        'insgesamt 26 Datensätze
        Assert.AreEqual(24, i)
        'als Tageswechesel-Nummer wird 2 zurückgegeben
        Assert.AreEqual(99999, e)

        'Tabelle dbo.ProduzierteWare komplett leeren
        'Dim OrgaSoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        OrgaSoftMain.sqlCommand("DELETE FROM ProduzierteWare")

        'ein erneuter Export darf keine neuen Datensätze erzeugen
        e = Export.ExportChargen(99999)
        Assert.AreEqual(99999, e)
        'keine Datensätze in Tabelle ProduzierterWare
        OrgaSoftMain.sqlSelect("Select * FROM ProduzierteWare")
        Assert.IsFalse(OrgaSoftMain.Read)
        OrgaSoftMain.Close()
    End Sub


End Class