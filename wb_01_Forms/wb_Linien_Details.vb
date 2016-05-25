Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Linien_Details
    Inherits DockContent

    Private Sub LinienDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler wb_Linien.eListe_Click, AddressOf DetailInfo
    End Sub

    Private Sub Panel_Leave(sender As Object, e As EventArgs) Handles Panel.Leave
        wb_Linien.aktBezeichnung = tBezeichnung.Text
        wb_Linien.aktAdresse = tAdresse.Text
        wb_Linien.Edit_Leave(sender)
    End Sub

    Public Sub DetailInfo()
        tBezeichnung.Text = wb_Linien.aktBezeichnung
        tAdresse.Text = wb_Linien.aktAdresse
    End Sub

    Public Sub DetailEdit()
        tBezeichnung.Focus()
    End Sub

End Class