Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WinBack

<TestClass()> Public Class UnitTest_CASetup

    <TestMethod()> Public Sub Test_InitSetup()
        Dim a As Microsoft.Deployment.WindowsInstaller.ActionResult
        a = CustomActions.VBCustomAction_InstallPath(Nothing)
        Assert.AreEqual(Microsoft.Deployment.WindowsInstaller.ActionResult.Success, a)


    End Sub

End Class