Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_wb_Mail

    ''' <summary>
    ''' Startet das Standard-Mail-Programm (Outlook) mit vorausgefüllten Daten:
    '''     Empfänger-Adresse
    '''     Betreff
    '''     Text
    ''' </summary>
    <TestMethod()> Public Sub TestStartMail()
        Dim TestMail As New wb_Mail
        TestMail.StartMail("joerg.will@web.de", "UnitTest", "Diese Mail wird automatisch von UnitTest_wb_Mail.vb verschickt")
    End Sub

    <TestMethod()> Public Sub TestStartMail_CloudAnforderung()
        Dim TestMail As New wb_Mail
        TestMail.StartMail_CloudAnforderung("TestRohstoff", "TestLieferant")
    End Sub
End Class