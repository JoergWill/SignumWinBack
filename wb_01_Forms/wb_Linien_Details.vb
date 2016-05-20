Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Linien_Details
    Inherits DockContent

    Private WithEvents LinienListe As wb_Linien_Liste

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

    Public Shared Event DetailInfoHasChanged()
    Private Sub Panel_Leave(sender As Object, e As EventArgs) Handles Panel.Leave
        RaiseEvent DetailInfoHasChanged()
    End Sub

    Public Sub DetailInfo() Handles LinienListe.ItemSelected
        aktBezeichnung = LinienListe.aktBezeichnung
        aktAdresse = LinienListe.aktAdresse
    End Sub

End Class