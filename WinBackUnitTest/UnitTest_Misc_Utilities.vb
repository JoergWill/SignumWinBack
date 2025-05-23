Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global
Imports System.Collections
Imports System.Text

<TestClass()>
Public Class UnitTest_Misc_Utilities

    <TestInitialize>
    Sub TestInitialize()
        ' Ensure WinBack.ini settings are initialized if necessary
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()>
    Public Sub Test_DatenLinkToIndex()
        Assert.AreEqual(1, wb_Functions.DatenLinkToIndex("GCAL"))
        Assert.AreEqual(2, wb_Functions.DatenLinkToIndex("NAEHRWERT"))
        Assert.AreEqual(3, wb_Functions.DatenLinkToIndex("NAEHRSTOFF"))
        Assert.AreEqual(141, wb_Functions.DatenLinkToIndex("GLUTEN"))
        Assert.AreEqual(142, wb_Functions.DatenLinkToIndex("KREBSTIERE"))
        Assert.AreEqual(143, wb_Functions.DatenLinkToIndex("EIER"))
        Assert.AreEqual(144, wb_Functions.DatenLinkToIndex("FISCH"))
        Assert.AreEqual(145, wb_Functions.DatenLinkToIndex("ERDNUESSE"))
        Assert.AreEqual(146, wb_Functions.DatenLinkToIndex("SOJA"))
        Assert.AreEqual(147, wb_Functions.DatenLinkToIndex("MILCH"))
        Assert.AreEqual(148, wb_Functions.DatenLinkToIndex("SCHALENFRUECHTE"))
        Assert.AreEqual(149, wb_Functions.DatenLinkToIndex("SELLERIE"))
        Assert.AreEqual(150, wb_Functions.DatenLinkToIndex("SENF"))
        Assert.AreEqual(151, wb_Functions.DatenLinkToIndex("SESAMSAMEN"))
        Assert.AreEqual(152, wb_Functions.DatenLinkToIndex("SCHWEFELDIOXID"))
        Assert.AreEqual(153, wb_Functions.DatenLinkToIndex("LUPINEN"))
        Assert.AreEqual(154, wb_Functions.DatenLinkToIndex("WEICHTIERE"))
        Assert.AreEqual(155, wb_Functions.DatenLinkToIndex("VEGETARISCH"))
        Assert.AreEqual(156, wb_Functions.DatenLinkToIndex("VEGAN"))
        Assert.AreEqual(157, wb_Functions.DatenLinkToIndex("OHNE_SCHWEINEFLEISCH"))
        Assert.AreEqual(158, wb_Functions.DatenLinkToIndex("OHNE_RINDFLEISCH"))
        Assert.AreEqual(159, wb_Functions.DatenLinkToIndex("OHNE_ALKOHOL"))
        Assert.AreEqual(160, wb_Functions.DatenLinkToIndex("HALAL"))
        Assert.AreEqual(161, wb_Functions.DatenLinkToIndex("KOSCHER"))
        Assert.AreEqual(162, wb_Functions.DatenLinkToIndex("BIO"))

        Assert.AreEqual(-1, wb_Functions.DatenLinkToIndex("UNKNOWN_VALUE"))
        Assert.AreEqual(-1, wb_Functions.DatenLinkToIndex(""))
        Assert.AreEqual(-1, wb_Functions.DatenLinkToIndex(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_OpenFoodFactsToIndex()
        Assert.AreEqual(1, wb_Functions.OpenFoodFactsToIndex("ENERGIEKJ"))
        Assert.AreEqual(2, wb_Functions.OpenFoodFactsToIndex("ENERGIEKCAL"))
        Assert.AreEqual(3, wb_Functions.OpenFoodFactsToIndex("FETT"))
        Assert.AreEqual(4, wb_Functions.OpenFoodFactsToIndex("GESAETTIGTEFETTSAEUREN"))
        Assert.AreEqual(5, wb_Functions.OpenFoodFactsToIndex("KOHLENHYDRATE"))
        Assert.AreEqual(6, wb_Functions.OpenFoodFactsToIndex("ZUCKER"))
        Assert.AreEqual(7, wb_Functions.OpenFoodFactsToIndex("BALLASTSTOFFE"))
        Assert.AreEqual(8, wb_Functions.OpenFoodFactsToIndex("EIWEISS"))
        Assert.AreEqual(9, wb_Functions.OpenFoodFactsToIndex("SALZ"))
        Assert.AreEqual(10, wb_Functions.OpenFoodFactsToIndex("NATRIUM"))

        Assert.AreEqual(-1, wb_Functions.OpenFoodFactsToIndex("UNKNOWN_VALUE"))
        Assert.AreEqual(-1, wb_Functions.OpenFoodFactsToIndex(""))
        Assert.AreEqual(-1, wb_Functions.OpenFoodFactsToIndex(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_PistorToIndex()
        Assert.AreEqual(1, wb_Functions.PistorToIndex("Artikel-Nr."))
        Assert.AreEqual(2, wb_Functions.PistorToIndex("Bezeichnung"))
        Assert.AreEqual(10, wb_Functions.PistorToIndex("Energie kJ"))
        Assert.AreEqual(20, wb_Functions.PistorToIndex("davon Zucker"))
        Assert.AreEqual(27, wb_Functions.PistorToIndex("Natrium"))

        Assert.AreEqual(-1, wb_Functions.PistorToIndex("UNKNOWN_VALUE"))
        Assert.AreEqual(-1, wb_Functions.PistorToIndex(""))
        Assert.AreEqual(-1, wb_Functions.PistorToIndex(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_PistorToText()
        Assert.AreEqual("Artikel-Nr.", wb_Functions.PistorToText(1))
        Assert.AreEqual("Bezeichnung", wb_Functions.PistorToText(2))
        Assert.AreEqual("Energie kJ", wb_Functions.PistorToText(10))
        Assert.AreEqual("davon Zucker", wb_Functions.PistorToText(20))
        Assert.AreEqual("Natrium", wb_Functions.PistorToText(27))
        Assert.AreEqual("?", wb_Functions.PistorToText(999)) ' Unknown index
        Assert.AreEqual("?", wb_Functions.PistorToText(-1))
    End Sub

    <TestMethod()>
    Public Sub Test_StringToDBType()
        Assert.AreEqual(wb_Sql.dbType.mySql, wb_Functions.StringToDBType("MYSQL"))
        Assert.AreEqual(wb_Sql.dbType.MSSql, wb_Functions.StringToDBType("MSSQL"))
        Assert.AreEqual(wb_Sql.dbType.OleDB, wb_Functions.StringToDBType("OLEDB"))
        Assert.AreEqual(wb_Sql.dbType.Unbekannt, wb_Functions.StringToDBType("UNKNOWN"))
        Assert.AreEqual(wb_Sql.dbType.Unbekannt, wb_Functions.StringToDBType(""))
        Assert.AreEqual(wb_Sql.dbType.Unbekannt, wb_Functions.StringToDBType(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_MinMaxOptChargeToString()
        Assert.AreEqual("min", wb_Functions.MinMaxOptChargeToString(wb_Global.MinMaxOptCharge.MinCharge))
        Assert.AreEqual("max", wb_Functions.MinMaxOptChargeToString(wb_Global.MinMaxOptCharge.MaxCharge))
        Assert.AreEqual("opt", wb_Functions.MinMaxOptChargeToString(wb_Global.MinMaxOptCharge.OptCharge))
        Assert.AreEqual("?", wb_Functions.MinMaxOptChargeToString(CType(99, wb_Global.MinMaxOptCharge))) ' Unknown
    End Sub

    <TestMethod()>
    Public Sub Test_SaveDiv()
        Assert.AreEqual(5.0, wb_Functions.SaveDiv(10.0, 2.0))
        Assert.AreEqual(0.0, wb_Functions.SaveDiv(10.0, 0.0)) ' Division by zero
        Assert.AreEqual(3.3333333333333335, wb_Functions.SaveDiv(10.0, 3.0)) ' Repeating decimal
        Assert.AreEqual(0.0, wb_Functions.SaveDiv(0.0, 5.0))
    End Sub

    <TestMethod()>
    Public Sub Test_ProzentSatz()
        Assert.AreEqual(50.0, wb_Functions.ProzentSatz(100.0, 50.0))
        Assert.AreEqual(0.0, wb_Functions.ProzentSatz(0.0, 50.0))
        Assert.AreEqual(0.0, wb_Functions.ProzentSatz(100.0, 0.0)) ' Grundwert is 0
        Assert.AreEqual(200.0, wb_Functions.ProzentSatz(50.0, 100.0))
    End Sub

    Private Function GetMockStackTrace() As String
        Dim sb As New Text.StringBuilder()
        sb.AppendLine("   bei System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)")
        sb.AppendLine("   bei System.Environment.get_StackTrace()")
        sb.AppendLine("   bei WinBack.wb_TraceListener.WriteLine(String message) in C:\WinBack\wb_00_Klassen\wb_TraceListener.vb:Zeile 36")
        sb.AppendLine("   bei WinBack.wb_Global.Log(String sText, LogType LogTyp, String sMemberName, String sSourceFilePath, Int32 iSourceLineNumber) in C:\WinBack\wb_00_Klassen\wb_Global.vb:Zeile 123")
        sb.AppendLine("   bei WinBack.Login.btnLogin_Click(Object sender, EventArgs e) in C:\WinBack\WinBack\01_WinBack\Login.Designer.vb:Zeile 45")
        sb.AppendLine("   bei WinBack.MyApplication.Main() in C:\WinBack\WinBack\My Project\Application.Designer.vb:Zeile 20")
        sb.AppendLine("   bei WinBack.SubModule.Lambda$_1() in D:\Project\WinBack\SubModule.vb:Zeile 78")
        Return sb.ToString()
    End Function

    <TestMethod()>
    Public Sub Test_GetLocalStackTrace()
        Dim mockStackTrace As String = GetMockStackTrace()

        Dim resultOnlyOneLine As ArrayList = wb_Functions.GetLocalStackTrace(mockStackTrace, True)
        Assert.AreEqual(1, resultOnlyOneLine.Count, "Should return only one line when OnlyOneLine is true.")
        Assert.IsTrue(resultOnlyOneLine(0).ToString().Contains("Login.btnLogin_Click"), "The single line should be from Login.btnLogin_Click")

        Dim resultAllRelevantLines As ArrayList = wb_Functions.GetLocalStackTrace(mockStackTrace, False)
        Assert.AreEqual(2, resultAllRelevantLines.Count, "Should return all relevant WinBack lines.")
        Assert.IsTrue(resultAllRelevantLines(0).ToString().Contains("wb_Global.Log"), "First relevant line.")
        Assert.IsTrue(resultAllRelevantLines(1).ToString().Contains("Login.btnLogin_Click"), "Second relevant line.")
    End Sub
    
    <TestMethod()>
    Public Sub Test_GetStackTraceTree()
        Dim mockStackTrace As String = GetMockStackTrace()
        Dim expectedTree As String = "Login.btnLogin_Click" & vbCrLf & "  wb_Global.Log" & vbCrLf

        Dim resultTree As String = wb_Functions.GetStackTraceTree(mockStackTrace)
        Assert.AreEqual(expectedTree, resultTree)

        Dim emptyStackTrace As String = ""
        Assert.AreEqual("", wb_Functions.GetStackTraceTree(emptyStackTrace))

        Dim stackTraceNoWinBack As String = " at System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)" & vbCrLf & " at System.Environment.get_StackTrace()"
        Assert.AreEqual("", wb_Functions.GetStackTraceTree(stackTraceNoWinBack))
    End Sub

End Class
