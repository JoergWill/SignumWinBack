Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

<TestClass()>
Public Class UnitTest_BusinessLogic_Helpers

    <TestInitialize>
    Sub TestInitialize()
        ' Ensure WinBack.ini settings are initialized if necessary for any functions
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()>
    Public Sub Test_TypeIstSollWert()
        ' Types that are always TRUE for SollWert (Param does not matter or is implicitly 0/irrelevant for the check)
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_KNETER, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_TEIGZETTEL, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_METER, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_STUECK, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_TEMP, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_WARTEN, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_RUEHREN, 0))
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_TEXT, 0))

        ' KO_TYPE_WASSERKOMPONENTE: True only if Param = 3
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 3))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 0))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 1))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 2))

        ' KO_TYPE_SAUER_WASSER: True only if Param = 3
        Assert.IsTrue(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 3))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 0))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 1))


        ' Types that are FALSE for SollWert, regardless of Param
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_ARTIKEL, 0))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE, 1))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE, 1))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL, 0))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE, 0))
        Assert.IsFalse(wb_Functions.TypeIstSollWert(wb_Global.KomponTypen.KO_TYPE_UNDEFINED, 0))
    End Sub

    <TestMethod()>
    Public Sub Test_TypeIstMeterStk()
        Assert.IsTrue(wb_Functions.TypeIstMeterStk(wb_Global.KomponTypen.KO_TYPE_METER))
        Assert.IsTrue(wb_Functions.TypeIstMeterStk(wb_Global.KomponTypen.KO_TYPE_STUECK))
        Assert.IsFalse(wb_Functions.TypeIstMeterStk(wb_Global.KomponTypen.KO_TYPE_ARTIKEL))
        Assert.IsFalse(wb_Functions.TypeIstMeterStk(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE))
        Assert.IsFalse(wb_Functions.TypeIstMeterStk(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE))
        Assert.IsFalse(wb_Functions.TypeIstMeterStk(wb_Global.KomponTypen.KO_TYPE_UNDEFINED))
    End Sub

    <TestMethod()>
    Public Sub Test_TypeHatNwt()
        Assert.IsTrue(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL))
        Assert.IsTrue(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER))
        Assert.IsTrue(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE))

        Assert.IsFalse(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_ARTIKEL))
        Assert.IsFalse(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_METER))
        Assert.IsFalse(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_STUECK))
        Assert.IsFalse(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE))
        Assert.IsFalse(wb_Functions.TypeHatNwt(wb_Global.KomponTypen.KO_TYPE_UNDEFINED))
    End Sub

    <TestMethod()>
    Public Sub Test_TypeIstText()
        Assert.IsTrue(wb_Functions.TypeIstText(wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeIstText(wb_Global.KomponTypen.KO_TYPE_SAUER_TEXT))
        Assert.IsFalse(wb_Functions.TypeIstText(wb_Global.KomponTypen.KO_TYPE_ARTIKEL))
        Assert.IsFalse(wb_Functions.TypeIstText(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE))
        Assert.IsFalse(wb_Functions.TypeIstText(wb_Global.KomponTypen.KO_TYPE_UNDEFINED))
    End Sub

    <TestMethod()>
    Public Sub Test_TypeIstWasserSollmenge()
        ' True for KO_TYPE_WASSERKOMPONENTE if TA = 1
        Assert.IsTrue(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 1))
        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 0))
        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 2))
        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE, 3))

        ' True for KO_TYPE_SAUER_WASSER if TA = 1
        Assert.IsTrue(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 1))
        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 0))
        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, 3))


        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_ARTIKEL, 1))
        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE, 1))
        Assert.IsFalse(wb_Functions.TypeIstWasserSollmenge(wb_Global.KomponTypen.KO_TYPE_UNDEFINED, 1))
    End Sub

    <TestMethod()>
    Public Sub Test_TypeHatEinheit()
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_STUECK))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_METER))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_SAUER_TEMP))
        Assert.IsTrue(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE))

        Assert.IsFalse(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_ARTIKEL))
        Assert.IsFalse(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_KNETER))
        Assert.IsFalse(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE))
        Assert.IsFalse(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_SAUER_WARTEN))
        Assert.IsFalse(wb_Functions.TypeHatEinheit(wb_Global.KomponTypen.KO_TYPE_UNDEFINED))
    End Sub

    <TestMethod()>
    Public Sub Test_TypeHasChildSteps()
        Assert.IsTrue(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT))
        Assert.IsTrue(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_SAUER_REZEPT_START))

        Assert.IsFalse(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_ARTIKEL))
        Assert.IsFalse(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE))
        Assert.IsFalse(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE))
        Assert.IsFalse(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_METER))
        Assert.IsFalse(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL))
        Assert.IsFalse(wb_Functions.TypeHasChildSteps(wb_Global.KomponTypen.KO_TYPE_UNDEFINED))
    End Sub

End Class
