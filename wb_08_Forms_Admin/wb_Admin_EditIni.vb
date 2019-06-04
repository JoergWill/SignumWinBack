Imports System.IO
Imports WeifenLuo.WinFormsUI.Docking


Public Class wb_Admin_EditIni
    Inherits DockContent

    Private Sub wb_Admin_EditIni_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbIniFile.Text = File.ReadAllText(wb_GlobalSettings.PWinBackIniPath)
        lblPathToWinBackIni.Text = wb_GlobalSettings.pWinBackIniPath
        btnSave.Enabled = False
        'TODO Remove after Test !!!
        Throw New System.Exception("Exceptions TEST")
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
            Dim s As String = wb_GlobalSettings.pWinBackIniPath
            Dim f As String = s & "." & Date.Now.ToString("yyyyMMddHHmmss")
            Try
                File.Move(s, f)
                File.WriteAllText(s, tbIniFile.Text)
            Catch e As Exception
                MsgBox("WinBack.ini konnte nicht geschrieben werden !" & vbCr & e.Message, MsgBoxStyle.Critical, "Speichern Konfiguration")
                File.Move(f, s)
            End Try
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub tbIniFile_Enter(sender As Object, e As EventArgs) Handles tbIniFile.Enter
        btnSave.Enabled = True
        tbIniFile.SelectionStart = tbIniFile.Text.Length
        tbIniFile.DeselectAll()
    End Sub
End Class