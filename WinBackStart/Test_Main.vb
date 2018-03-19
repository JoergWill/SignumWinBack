Imports WinBack

Public Class Test_Main
    Implements IMainMenu

    Public Property DkPnlConfigFileName As String Implements IMainMenu.DkPnlConfigFileName
        Get
            Return ""
        End Get
        Set(value As String)
        End Set
    End Property

    Public Function ExecuteCmd(Cmd As String, Prm As String) As Boolean Implements IMainMenu.ExecuteCmd
        Debug.Print("ExecuteCmd " & Cmd & " / " & Prm)
        Return True
    End Function
End Class