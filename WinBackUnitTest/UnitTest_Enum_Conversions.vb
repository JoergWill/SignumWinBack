Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

<TestClass()>
Public Class UnitTest_Enum_Conversions

    <TestInitialize>
    Sub TestInitialize()
        ' Ensure WinBack.ini settings are initialized if necessary for any functions
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()>
    Public Sub Test_StringtoErnaehrungsForm()
        Assert.AreEqual(wb_Global.ErnaehrungsForm.N, wb_Functions.StringtoErnaehrungsForm("N"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.N, wb_Functions.StringtoErnaehrungsForm("FALSE"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.N, wb_Functions.StringtoErnaehrungsForm("n"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.N, wb_Functions.StringtoErnaehrungsForm("false"))

        Assert.AreEqual(wb_Global.ErnaehrungsForm.Y, wb_Functions.StringtoErnaehrungsForm("Y"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.Y, wb_Functions.StringtoErnaehrungsForm("J"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.Y, wb_Functions.StringtoErnaehrungsForm("TRUE"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.Y, wb_Functions.StringtoErnaehrungsForm("y"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.Y, wb_Functions.StringtoErnaehrungsForm("j"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.Y, wb_Functions.StringtoErnaehrungsForm("true"))


        Assert.AreEqual(wb_Global.ErnaehrungsForm.X, wb_Functions.StringtoErnaehrungsForm("-"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.X, wb_Functions.StringtoErnaehrungsForm(""))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.X, wb_Functions.StringtoErnaehrungsForm(Nothing))

        Assert.AreEqual(wb_Global.ErnaehrungsForm.ERR, wb_Functions.StringtoErnaehrungsForm("Invalid"))
        Assert.AreEqual(wb_Global.ErnaehrungsForm.ERR, wb_Functions.StringtoErnaehrungsForm(" Test "))
    End Sub

    <TestMethod()>
    Public Sub Test_ErnaehrungToString()
        Assert.AreEqual("N", wb_Functions.ErnaehrungToString(wb_Global.ErnaehrungsForm.N))
        Assert.AreEqual("J", wb_Functions.ErnaehrungToString(wb_Global.ErnaehrungsForm.Y))
        Assert.AreEqual("-", wb_Functions.ErnaehrungToString(wb_Global.ErnaehrungsForm.X))
        Assert.AreEqual("?", wb_Functions.ErnaehrungToString(wb_Global.ErnaehrungsForm.ERR))
        ' Test with an integer value that might correspond to an enum member if not strictly typed
        Assert.AreEqual("?", wb_Functions.ErnaehrungToString(CType(99, wb_Global.ErnaehrungsForm))) ' Invalid enum
    End Sub

    <TestMethod()>
    Public Sub Test_ErnaehrungToInteger()
        Assert.AreEqual(0, wb_Functions.ErnaehrungToInteger(wb_Global.ErnaehrungsForm.N))
        Assert.AreEqual(1, wb_Functions.ErnaehrungToInteger(wb_Global.ErnaehrungsForm.Y))
        Assert.AreEqual(2, wb_Functions.ErnaehrungToInteger(wb_Global.ErnaehrungsForm.X))
        Assert.AreEqual(-1, wb_Functions.ErnaehrungToInteger(wb_Global.ErnaehrungsForm.ERR))
        Assert.AreEqual(-1, wb_Functions.ErnaehrungToInteger(CType(99, wb_Global.ErnaehrungsForm))) ' Invalid enum
    End Sub

    <TestMethod()>
    Public Sub Test_ModusChargenTeilerToString()
        Assert.AreEqual("Unbekannt", wb_Functions.ModusChargenTeilerToString(wb_Global.ModusChargenTeiler.Unbekannt))
        Assert.AreEqual("Prozent", wb_Functions.ModusChargenTeilerToString(wb_Global.ModusChargenTeiler.Prozent))
        Assert.AreEqual("Gewicht", wb_Functions.ModusChargenTeilerToString(wb_Global.ModusChargenTeiler.Gewicht))
        Assert.AreEqual("?", wb_Functions.ModusChargenTeilerToString(CType(99, wb_Global.ModusChargenTeiler))) ' Invalid enum
    End Sub

    <TestMethod()>
    Public Sub Test_ModusTeigOptimierungToString()
        'This function takes wb_Global.ModusChargenTeiler as parameter, per definition:
        'Public Shared Function ModusTeigOptimierungToString(c As wb_Global.ModusChargenTeiler) As String
        Assert.AreEqual("Unbekannt", wb_Functions.ModusTeigOptimierungToString(wb_Global.ModusChargenTeiler.Unbekannt))
        Assert.AreEqual("Prozent", wb_Functions.ModusTeigOptimierungToString(wb_Global.ModusChargenTeiler.Prozent))
        Assert.AreEqual("Gewicht", wb_Functions.ModusTeigOptimierungToString(wb_Global.ModusChargenTeiler.Gewicht))
        Assert.AreEqual("?", wb_Functions.ModusTeigOptimierungToString(CType(99, wb_Global.ModusChargenTeiler))) ' Invalid enum
    End Sub
    
    <TestMethod()>
    Public Sub Test_LogTypeToString()
        Assert.AreEqual("ERR", wb_Functions.LogTypeToString(wb_Global.LogType.ERR))
        Assert.AreEqual("INF", wb_Functions.LogTypeToString(wb_Global.LogType.INF))
        Assert.AreEqual("WAR", wb_Functions.LogTypeToString(wb_Global.LogType.WAR))
        Assert.AreEqual("DEB", wb_Functions.LogTypeToString(wb_Global.LogType.DEB))
        Assert.AreEqual("?", wb_Functions.LogTypeToString(CType(99, wb_Global.LogType))) ' Invalid enum
    End Sub

    <TestMethod()>
    Public Sub Test_StrToFormat()
        Assert.AreEqual(wb_Global.FormatList. Prozent, wb_Functions.StrToFormat("%"))
        Assert.AreEqual(wb_Global.FormatList.FixGewicht, wb_Functions.StrToFormat("g"))
        Assert.AreEqual(wb_Global.FormatList.FixGewicht, wb_Functions.StrToFormat("kg"))
        Assert.AreEqual(wb_Global.FormatList.FixGewicht, wb_Functions.StrToFormat("KG"))
        Assert.AreEqual(wb_Global.FormatList.FixGewicht, wb_Functions.StrToFormat("GR")) ' Assuming GR for gram
        Assert.AreEqual(wb_Global.FormatList.Liter, wb_Functions.StrToFormat("L"))
        Assert.AreEqual(wb_Global.FormatList.Liter, wb_Functions.StrToFormat("l"))
        Assert.AreEqual(wb_Global.FormatList.Stueck, wb_Functions.StrToFormat("Stk"))
        Assert.AreEqual(wb_Global.FormatList.Stueck, wb_Functions.StrToFormat("stk"))
        Assert.AreEqual(wb_Global.FormatList.Undefined, wb_Functions.StrToFormat("Invalid"))
        Assert.AreEqual(wb_Global.FormatList.Undefined, wb_Functions.StrToFormat(""))
        Assert.AreEqual(wb_Global.FormatList.Undefined, wb_Functions.StrToFormat(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_IntToProduktionsTyp()
        Assert.AreEqual(wb_Global.ProduktionsTypen.Produktion, wb_Functions.IntToProduktionsTyp(0))
        Assert.AreEqual(wb_Global.ProduktionsTypen.Teigbereitung, wb_Functions.IntToProduktionsTyp(1))
        Assert.AreEqual(wb_Global.ProduktionsTypen.Abwiegen, wb_Functions.IntToProduktionsTyp(2))
        Assert.AreEqual(wb_Global.ProduktionsTypen.Unbekannt, wb_Functions.IntToProduktionsTyp(99)) ' Invalid int
        Assert.AreEqual(wb_Global.ProduktionsTypen.Unbekannt, wb_Functions.IntToProduktionsTyp(-1))
    End Sub

    <TestMethod()>
    Public Sub Test_kt301GruppeToString()
        Assert.AreEqual("Big 4", wb_Functions.kt301GruppeToString(wb_Global.ktTyp301Gruppen.Big4))
        Assert.AreEqual("LMIV", wb_Functions.kt301GruppeToString(wb_Global.ktTyp301Gruppen.LMIV))
        Assert.AreEqual("Freiwillig", wb_Functions.kt301GruppeToString(wb_Global.ktTyp301Gruppen.Freiwillig))
        Assert.AreEqual("Unbekannt", wb_Functions.kt301GruppeToString(CType(99, wb_Global.ktTyp301Gruppen))) ' Invalid enum
    End Sub

End Class
