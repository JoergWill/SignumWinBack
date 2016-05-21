Public Class wb_Linien
    Public Shared Event SpecialEventRaised(ByVal sender As Object, ByVal type As String, ByVal msg As String)

    Public Shared Sub SpecialEvent(ByVal sender As Object, ByVal type As String, ByVal msg As String)
        RaiseEvent SpecialEventRaised(sender, type, msg)
    End Sub
End Class
