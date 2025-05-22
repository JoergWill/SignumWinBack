Public Class wb_Planung_Vorlage
    Public TWNr As Integer = -1

    Private Sub BtnLoadVorlage_Click(sender As Object, e As EventArgs) Handles BtnLoadVorlage.Click
        TWNr = 24
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        TWNr = -1
        Me.Close()
    End Sub
End Class