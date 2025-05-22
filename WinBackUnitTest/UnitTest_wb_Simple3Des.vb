Imports System.Text
Imports WinBack.wb_Credentials
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest_wb_Simple3Des

    ''' <summary>
    ''' Initialisiert die globalen Einstellungen.
    ''' </summary>
    <TestInitialize>
    Sub TestInitialize()
        'Einstellungen in WinBack.ini für den Testlauf vornehmen
        UnitTest_Init.Init_WinBackIni_Settings()
    End Sub

    <TestMethod()> Public Sub TestEncryption()

        Dim plainText As String = "ThisIsPlainText"

        Dim wrapper As New WinBack.wb_Simple3Des(Passwd3Des)
        Dim cipherText As String = wrapper.EncryptData(plainText)

        Assert.AreNotEqual(Passwd3Des, cipherText)
        Assert.AreNotEqual(plainText, cipherText)

        Dim NewText As String = wrapper.DecryptData(cipherText)
        Assert.AreEqual(plainText, NewText)

        Dim TestText As String = wrapper.DecryptData(plainText)
        Assert.AreEqual("NOTENCRYPTED", TestText)
    End Sub

    <TestMethod()> Public Sub TestReadencrypted()
        Dim IniFile As New WinBack.wb_IniFile
        Dim Passwd As String

        IniFile.WriteString("Test", "Passwd", "OrgaBack.NET")
        Passwd = IniFile.ReadString("Test", "Passwd", "OrgaBack.NET")
        Assert.AreEqual("OrgaBack.NET", Passwd)

        Dim wrapper As New WinBack.wb_Simple3Des(Passwd3Des)
        Passwd = IniFile.ReadEncryptedString("Test", "Passwd", "OrgaBack.NET")
        Assert.AreEqual("OrgaBack.NET", Passwd)
        Passwd = IniFile.ReadString("Test", "Passwd", "OrgaBack.NET")
        Assert.AreNotEqual("OrgaBack.NET", Passwd)
        Passwd = IniFile.ReadEncryptedString("Test", "Passwd", "OrgaBack.NET")
        Assert.AreEqual("OrgaBack.NET", Passwd)

    End Sub


End Class