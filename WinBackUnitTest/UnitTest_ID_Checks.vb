Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

<TestClass()>
Public Class UnitTest_ID_Checks

    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()>
    Public Sub Test_IsDatenLinkID()
        Assert.IsTrue(wb_Functions.IsDatenLinkID("DL-12345"))
        Assert.IsTrue(wb_Functions.IsDatenLinkID("DL-ABC"))
        Assert.IsFalse(wb_Functions.IsDatenLinkID("DL12345")) ' Missing hyphen
        Assert.IsFalse(wb_Functions.IsDatenLinkID("D-12345"))
        Assert.IsFalse(wb_Functions.IsDatenLinkID("NotDL-123"))
        Assert.IsFalse(wb_Functions.IsDatenLinkID(""))
        Assert.IsFalse(wb_Functions.IsDatenLinkID(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_IsOpenFoodFactsID()
        Assert.IsTrue(wb_Functions.IsOpenFoodFactsID("EAN12345"))
        Assert.IsTrue(wb_Functions.IsOpenFoodFactsID("EAN-ABC")) 'Hyphenated EAN
        Assert.IsFalse(wb_Functions.IsOpenFoodFactsID("EAN 12345")) ' Space after EAN
        Assert.IsFalse(wb_Functions.IsOpenFoodFactsID("EA-12345"))
        Assert.IsFalse(wb_Functions.IsOpenFoodFactsID("NotEAN-123"))
        Assert.IsFalse(wb_Functions.IsOpenFoodFactsID(""))
        Assert.IsFalse(wb_Functions.IsOpenFoodFactsID(Nothing))
    End Sub

End Class
