Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global
Imports System.Collections.Generic

<TestClass()>
Public Class UnitTest_String_Manipulation

    <TestInitialize>
    Sub TestInitialize()
        ' Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()>
    Public Sub Test_SetParams()
        Assert.AreEqual("Hello World", wb_Functions.SetParams("Hello [0]", "World"))
        Assert.AreEqual("Test A B C", wb_Functions.SetParams("[0] [1] [2]", "Test", "A", "B", "C")) 'Added C for [2]
        Assert.AreEqual("Value: X, Y, Z, END", wb_Functions.SetParams("Value: [0], [1], [2], [3]", "X", "Y", "Z", "END"))
        Assert.AreEqual("No params", wb_Functions.SetParams("No params"))
        Assert.AreEqual("Hello  B", wb_Functions.SetParams("Hello [0] [1]", "", "B")) ' Empty Param0
        Assert.AreEqual("Test with [1] backslash", wb_Functions.SetParams("Test with [1] backslash", "\")) ' Param0 is "\", [1] remains
        Assert.AreEqual("Index out of bounds", wb_Functions.SetParams("[5]", "Test")) ' Index too high
        Assert.AreEqual("Hello [0]", wb_Functions.SetParams("Hello [0]")) ' Not enough parameters
        Assert.AreEqual("Param: Test, default, default", wb_Functions.SetParams("Param: [0], [1], [2]", "Test"))
    End Sub

    <TestMethod()>
    Public Sub Test_FormatSqlStr()
        Assert.AreEqual("123,45", wb_Functions.FormatSqlStr("123.45"))
        Assert.AreEqual("123,45", wb_Functions.FormatSqlStr("123,45"))
        Assert.AreEqual("test", wb_Functions.FormatSqlStr("test"))
        Assert.AreEqual("O''Neil", wb_Functions.FormatSqlStr("O'Neil")) ' Apostrophe handling
        Assert.AreEqual("", wb_Functions.FormatSqlStr(""))
        Assert.AreEqual(String.Empty, wb_Functions.FormatSqlStr(CType(Nothing, String))) ' Handles null by returning empty
    End Sub

    <TestMethod()>
    Public Sub Test_FormatZutatenListe()
        Assert.AreEqual("Zutat1" & vbCrLf & "Zutat2" & vbCrLf & "Zutat3", wb_Functions.FormatZutatenListe("Zutat1, Zutat2, Zutat3"))
        Assert.AreEqual("Zutat1" & vbCrLf & "Zutat2", wb_Functions.FormatZutatenListe("Zutat1,Zutat2")) ' No space
        Assert.AreEqual("Zutat1", wb_Functions.FormatZutatenListe("Zutat1"))
        Assert.AreEqual("", wb_Functions.FormatZutatenListe(""))
        Assert.AreEqual("", wb_Functions.FormatZutatenListe(Nothing))
        Assert.AreEqual("  Zutat1  " & vbCrLf & "  Zutat2  ", wb_Functions.FormatZutatenListe("  Zutat1  ,  Zutat2  ")) ' Spaces around elements
    End Sub

    <TestMethod()>
    Public Sub Test_XRenameToExcelTabName()
        Assert.AreEqual("Sheet1", wb_Functions.XRenameToExcelTabName("Sheet1"))
        Assert.AreEqual("A_B_C_D_E_F_G_H_I_J_K_L_M_N_O", wb_Functions.XRenameToExcelTabName("A/B\C*D?E[F]G:H|I#J+K<L>M.N!O")) ' Special chars
        Assert.AreEqual("Short", wb_Functions.XRenameToExcelTabName("Short"))
        Assert.AreEqual("ThisIsAVeryLongSheetNameThatW", wb_Functions.XRenameToExcelTabName("ThisIsAVeryLongSheetNameThatWillBeTruncated")) ' Max 31 chars
        Assert.AreEqual("NoSpacesAllowed", wb_Functions.XRenameToExcelTabName("No Spaces Allowed"))
        Assert.AreEqual("Sheet_1", wb_Functions.XRenameToExcelTabName("Sheet 1"))
        Assert.AreEqual("A_B", wb_Functions.XRenameToExcelTabName("A:B"))
        Assert.AreEqual("", wb_Functions.XRenameToExcelTabName(""))
        Assert.AreEqual("", wb_Functions.XRenameToExcelTabName(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_XRemoveUmlaute()
        Assert.AreEqual("AeOeUeaeoeuess", wb_Functions.XRemoveUmlaute("ÄÖÜäöüß"))
        Assert.AreEqual("AEoeUEaeoeuess", wb_Functions.XRemoveUmlaute("ÆøŒæœß")) ' Other ligatures/chars
        Assert.AreEqual("NormalText", wb_Functions.XRemoveUmlaute("NormalText"))
        Assert.AreEqual("", wb_Functions.XRemoveUmlaute(""))
        Assert.AreEqual(Nothing, wb_Functions.XRemoveUmlaute(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_XReplaceSonderZeichen_ImportExport()
        Assert.AreEqual("Ae_Oe_Ue_ae_oe_ue_ss", wb_Functions.XReplaceSonderZeichen_ImportExport("Ä_Ö_Ü_ä_ö_ü_ß"))
        Assert.AreEqual("A B C", wb_Functions.XReplaceSonderZeichen_ImportExport("A/B:C")) ' Slash and Colon
        Assert.AreEqual("Text_with_spaces", wb_Functions.XReplaceSonderZeichen_ImportExport("Text with spaces"))
        Assert.AreEqual("NoChange", wb_Functions.XReplaceSonderZeichen_ImportExport("NoChange"))
        Assert.AreEqual("", wb_Functions.XReplaceSonderZeichen_ImportExport(""))
        Assert.AreEqual(Nothing, wb_Functions.XReplaceSonderZeichen_ImportExport(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_XRemoveSonderZeichen_ImportExport()
        Assert.AreEqual("AeOeUeaeoeuess", wb_Functions.XRemoveSonderZeichen_ImportExport("ÄÖÜäöüß"))
        Assert.AreEqual("ABC", wb_Functions.XRemoveSonderZeichen_ImportExport("A/B:C")) ' Slash and Colon removed
        Assert.AreEqual("Textwithspaces", wb_Functions.XRemoveSonderZeichen_ImportExport("Text with spaces")) ' Spaces removed
        Assert.AreEqual("NoChange", wb_Functions.XRemoveSonderZeichen_ImportExport("NoChange"))
        Assert.AreEqual("", wb_Functions.XRemoveSonderZeichen_ImportExport(""))
        Assert.AreEqual(Nothing, wb_Functions.XRemoveSonderZeichen_ImportExport(Nothing))
        Assert.AreEqual("a0b1c2", wb_Functions.XRemoveSonderZeichen_ImportExport("a0_b1-c2.d3"))
    End Sub

    <TestMethod()>
    Public Sub Test_XRestoreSonderZeichen()
        Assert.AreEqual("ÄÖÜäöüß", wb_Functions.XRestoreSonderZeichen("AeOeUeaeoeuess"))
        Assert.AreEqual("A/B:C", wb_Functions.XRestoreSonderZeichen("A_SLASH_B_COLON_C"))
        Assert.AreEqual("Text with spaces", wb_Functions.XRestoreSonderZeichen("Text_WHITESPACE_with_WHITESPACE_spaces"))
        Assert.AreEqual("NoChange", wb_Functions.XRestoreSonderZeichen("NoChange"))
        Assert.AreEqual("A_B_C", wb_Functions.XRestoreSonderZeichen("A_UNDERSCORE_B_UNDERSCORE_C"))
        Assert.AreEqual("", wb_Functions.XRestoreSonderZeichen(""))
        Assert.AreEqual(Nothing, wb_Functions.XRestoreSonderZeichen(Nothing))
    End Sub

    <TestMethod()>
    Public Sub Test_GetStringFromArray()
        Dim arr1() As String = {"Apfel", "Birne", "Kirsche"}
        Assert.AreEqual("Apfel;Birne;Kirsche", wb_Functions.GetStringFromArray(arr1, ";"))
        Dim arr2() As String = {"Test"}
        Assert.AreEqual("Test", wb_Functions.GetStringFromArray(arr2, ","))
        Dim arr3() As String = {}
        Assert.AreEqual("", wb_Functions.GetStringFromArray(arr3, ";"))
        Dim arr4() As String = {"A", "", "B"}
        Assert.AreEqual("A;;B", wb_Functions.GetStringFromArray(arr4, ";"))
        Assert.AreEqual("A-B-C", wb_Functions.GetStringFromArray(New String() {"A", "B", "C"}, "-"))
    End Sub

    <TestMethod()>
    Public Sub Test_AddCSV()
        Dim list1 As New List(Of String) From {"Wert1", "Wert2", "Wert3"}
        Assert.AreEqual("""Wert1"";""Wert2"";""Wert3"";", wb_Functions.AddCSV(list1))

        Dim list2 As New List(Of String) From {"Wert1"}
        Assert.AreEqual("""Wert1"";", wb_Functions.AddCSV(list2))

        Dim list3 As New List(Of String)
        Assert.AreEqual("", wb_Functions.AddCSV(list3)) ' Empty list

        Dim list4 As New List(Of String) From {"Wert ""mit"" Anführungszeichen", "Wert,mit,Komma"}
        Assert.AreEqual("""Wert """"mit"""" Anführungszeichen"";""Wert,mit,Komma"";", wb_Functions.AddCSV(list4))
        
        Dim list5 As List(Of String) = Nothing
        Assert.AreEqual("", wb_Functions.AddCSV(list5)) ' Null list
    End Sub

    <TestMethod()>
    Public Sub Test_Truncate()
        Assert.AreEqual("Test", wb_Functions.Truncate("TestString", 4))
        Assert.AreEqual("TestString", wb_Functions.Truncate("TestString", 10))
        Assert.AreEqual("TestString", wb_Functions.Truncate("TestString", 15))
        Assert.AreEqual("", wb_Functions.Truncate("TestString", 0))
        Assert.AreEqual("", wb_Functions.Truncate("", 5))
        Assert.AreEqual(Nothing, wb_Functions.Truncate(Nothing, 5))
        Assert.AreEqual("Negativ", wb_Functions.Truncate("Negativ", -1)) ' Based on code, length < 0 returns original
    End Sub

End Class
