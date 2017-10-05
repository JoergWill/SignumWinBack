Imports System.IO
Imports WeifenLuo.WinFormsUI.Docking


Public Class wb_Admin_EditIni
    Inherits DockContent

    Private Sub wb_Admin_EditIni_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbIniFile.Text = File.ReadAllText(wb_GlobalSettings.PWinBackIniPath)
        lblPathToWinBackIni.Text = wb_GlobalSettings.PWinBackIniPath
        btnSave.Enabled = False
    End Sub
    Private Sub wb_Admin_EditIni_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        SaveToFile()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveToFile()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub SaveToFile()
        If btnSave.Enabled Then
            Try
                Dim f As String = wb_GlobalSettings.PWinBackIniPath & "." & Date.Now.ToString("yyyyMMddHHmmss")
                File.Move(wb_GlobalSettings.PWinBackIniPath, f)
                File.WriteAllText(wb_GlobalSettings.PWinBackIniPath, tbIniFile.Text)
            Catch e As Exception
                MsgBox("WinBack.ini konnte nicht geschrieben werden !" & vbCr & e.Message, MsgBoxStyle.Critical, "Speichern Konfiguration")
            End Try
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub tbIniFile_Enter(sender As Object, e As EventArgs) Handles tbIniFile.Enter
        btnSave.Enabled = True
    End Sub
End Class