Public Class wb_Admin_Shared
    Shared _LogEvents As New List(Of String)

    Public Shared ReadOnly Property LogEvents As List(Of String)
        Get
            Return _LogEvents
        End Get
    End Property

    Public Shared Sub GetTraceListenerText(Txt As String)
        _LogEvents.Add(Txt)
    End Sub

End Class
