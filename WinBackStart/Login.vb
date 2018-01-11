Imports System.Globalization
Imports System.Threading

Public Class Login
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        'User-Nummer suchen
        Dim UserNummer As Integer = wb_Functions.StrToInt(tUserNummer.Text)
        If Not wb_GlobalSettings.AktUserLogin(UserNummer) Then
            MsgBox("Unbekannter Benutzer. Bitte Eingabe wiederholen", MsgBoxStyle.Critical)
        Else
            Close()
        End If
    End Sub
End Class