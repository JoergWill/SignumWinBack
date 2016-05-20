Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig


<TestClass()> Public Class UnitTest_wb_Konfig

    <TestMethod()>
    Public Sub Test_wb_Konfig()
        ' Test Ini-File lesen/schreiben
        Dim IniFile As New Signum.OrgaSoft.AddIn.OrgasoftMain.wb_Konfig

        Dim bTest As Boolean
        Dim iTest As Integer
        Dim sTest, sLoremIpsum As String

        ' Test vorbereiten
        IniFile.SilentMode = True
        IniFile.DeleteIniFile()

        ' Test Read Boolean - Ini-File nicht vorhanden
        bTest = IniFile.ReadBool("Test", "TestBoolean", False)
        Assert.IsFalse(bTest)
        Assert.IsFalse(IniFile.ReadResult)
        bTest = IniFile.ReadBool("Test", "TestBoolean", True)
        Assert.IsTrue(bTest)
        Assert.IsFalse(IniFile.ReadResult)

        ' Test Write/Read Boolean 
        IniFile.WriteBool("Test", "TestBoolean", True)
        bTest = IniFile.ReadBool("Test", "TestBoolean", False)
        Assert.IsTrue(bTest)
        Assert.IsTrue(IniFile.ReadResult)
        IniFile.WriteBool("Test", "TestBoolean", False)
        bTest = IniFile.ReadBool("Test", "TestBoolean", True)
        Assert.IsFalse(bTest)
        Assert.IsTrue(IniFile.ReadResult)

        ' Test Read Boolean - Schlüssel nicht vorhanden
        bTest = IniFile.ReadBool("TestX", "TestBoolean", False)
        Assert.IsFalse(bTest)
        bTest = IniFile.ReadBool("TestX", "TestBoolean", True)
        Assert.IsTrue(bTest)

        ' Test Read Integer - Schlüssel nicht vorhanden
        iTest = IniFile.ReadInt("Test", "TestInteger")
        Equals(iTest = 0)
        iTest = IniFile.ReadInt("Test", "TestInteger", 99)
        Assert.AreEqual(iTest, 99)

        ' Test Write/Read Integer
        IniFile.WriteInt("Test", "TestInteger", -1234567)
        iTest = IniFile.ReadInt("Test", "TestInteger")
        Assert.AreEqual(iTest, -1234567)

        ' Test Read String - Schlüssel nicht vorhanden
        sTest = IniFile.ReadString("Test", "TestString")
        Equals(sTest = "")
        sTest = IniFile.ReadString("Test", "TestString", "LEER")
        Assert.AreEqual(sTest, "LEER")

        ' Test Read/Write String
        sLoremIpsum = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt " &
                     "ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo " &
                     "dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit " &
                     "amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt " &
                     "ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores " &
                     "et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
        IniFile.WriteString("Test", "TestString", sLoremIpsum)
        sTest = IniFile.ReadString("Test", "TestString")
        Assert.AreEqual(sTest, sLoremIpsum)

    End Sub

End Class