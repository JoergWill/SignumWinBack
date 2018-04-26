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
        UnitTest_Init.Init_WinBackIni()
    End Sub

    <TestMethod()> Public Sub TestExportChargen()
        'Programm-Variante Unit-Test
        wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest

        'Tabelle dbo.ProduzierteWare komplett leeren
        Dim OrgaSoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        OrgaSoftMain.sqlCommand("DELETE FROM ProduzierteWare")
        'Tabelle BAK_ArbRezepte vorbereiten
        Dim wbdaten As New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        wbdaten.sqlCommand("Update BAK_ArbRezepte SET B_ARZ_Status = ''")

        'Export Chargen ab TW-Nr.2
        Dim Export As New ob_ChargenProduziert
        Dim e As Integer = Export.ExportChargen(2)

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
        Assert.AreEqual(26, i)
        'als Tageswechesel-Nummer wird 2 zurückgegeben
        Assert.AreEqual(2, e)

        'Tabelle dbo.ProduzierteWare komplett leeren
        'Dim OrgaSoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        OrgaSoftMain.sqlCommand("DELETE FROM ProduzierteWare")

        'ein erneuter Export darf keine neuen Datensätze erzeugen
        e = Export.ExportChargen(2)
        Assert.AreEqual(-1, e)
        'keine Datensätze in Tabelle ProduzierterWare
        OrgaSoftMain.sqlSelect("Select * FROM ProduzierteWare")
        Assert.IsFalse(OrgaSoftMain.Read)
        OrgaSoftMain.Close()
    End Sub


End Class