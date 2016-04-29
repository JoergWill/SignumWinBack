Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Linien_Details
    Inherits DockContent

    Public Property aktBezeichnung As String
        Set(value As String)
            tBezeichnung.Text = value
        End Set
        Get
            Return tBezeichnung.Text
        End Get
    End Property

    Public Property aktAdresse As String
        Set(value As String)
            tAdresse.Text = value
        End Set
        Get
            Return tAdresse.Text
        End Get
    End Property

    Public Event DetailInfoHasChanged()
    Private Sub Panel_Leave(sender As Object, e As EventArgs) Handles Panel.Leave
        RaiseEvent DetailInfoHasChanged()
    End Sub
End Class