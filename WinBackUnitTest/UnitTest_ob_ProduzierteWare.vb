Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_ob_ProduzierteWare

    <TestMethod()> Public Sub TextExportChargen()
        Dim Export As New ob_ChargenProduziert

        'Export Chargen ab TW-Nr.10
        Export.ExportChargen(1993)
    End Sub


End Class