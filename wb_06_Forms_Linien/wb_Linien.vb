Public Class wb_Linien
    Public Shared Event eListe_Click(sender As Object)
    Public Shared Event eEdit_Leave(sender As Object)

    Public Shared aktBezeichnung As String
    Public Shared aktAdresse As String

    Public Shared Sub Liste_Click(sender As Object)
        RaiseEvent eListe_Click(sender)
    End Sub

    Public Shared Sub Edit_Leave(sender As Object)
        RaiseEvent eEdit_Leave(sender)
    End Sub
End Class
