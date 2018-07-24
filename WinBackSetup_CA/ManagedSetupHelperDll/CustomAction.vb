Public Class CustomActions

    <CustomAction()>
    Public Shared Function VBCustomAction(ByVal session As Session) As ActionResult
        session.Log("Begin VBCustomAction")

        Dim sLogonUser As String

        sLogonUser = session("LogonUser")

        'Username ausgeben in MessageBox
        MsgBox("Usernamen: " + sLogonUser, vbOKOnly, "VB.NET Custom Action")

        Return ActionResult.Success
    End Function
End Class
