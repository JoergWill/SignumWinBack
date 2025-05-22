Imports System.Windows.Forms
Imports System.IO
Imports WeifenLuo.WinFormsUI.Docking


Public Class wb_Admin_EditIni
    Inherits DockContent

    Private _MasterIniFile As Boolean = False
    Private _WinBackIniPath As String = ""

    Public Property MasterIniFile As Boolean
        Get
            Return _MasterIniFile
        End Get
        Set(value As Boolean)
            _MasterIniFile = value
        End Set
    End Property

    Private Sub wb_Admin_EditIni_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _MasterIniFile Then
            'Lesen der winback.in aus dem Programm-Verzeichnis (Master)
            _WinBackIniPath = wb_GlobalSettings.pProgrammPath & "\WinBack.ini"
            Me.Text = "Systemkonfiguration Master"
        Else
            'Lesen der winback.in aus dem User-Verzeichnis (Default)
            _WinBackIniPath = wb_GlobalSettings.pWinBackIniPath
        End If

        'Daten aus winback.ini lesen 
        tbIniFile.Text = File.ReadAllText(_WinBackIniPath, System.Text.Encoding.UTF8)
        lblPathToWinBackIni.Text = _WinBackIniPath
        'Mandant/Name/Version
        lblMandant.Text = "Mandant " & wb_GlobalSettings.MandantNr & "/" & wb_GlobalSettings.MandantName & "/Version " & wb_GlobalSettings.WinBackVersion

        'Keine Änderungen, die gesichert werden müssen
        btnSave.Enabled = False
    End Sub

    Private Sub wb_Admin_EditIni_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
            Dim f As String = _WinBackIniPath & "." & Date.Now.ToString("yyyyMMddHHmmss")
            Try
                File.Move(_WinBackIniPath, f)
                File.WriteAllText(_WinBackIniPath, tbIniFile.Text, System.Text.Encoding.UTF8)
            Catch e As Exception
                MsgBox("WinBack.ini konnte nicht geschrieben werden !" & vbCr & e.Message, MsgBoxStyle.Critical, "Speichern Konfiguration")
                File.Move(f, _WinBackIniPath)
            End Try
            btnSave.Enabled = False

            'OrgaBack muss neu gestartet werden nach Änderung in der WinBack.ini
            If MsgBox("Die WinBack Konfiguration wurde geändert." & vbCrLf & "Programm neu starten?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                wb_Functions.Restart()
            End If
        End If
    End Sub

    Private Sub tbIniFile_Enter(sender As Object, e As EventArgs) Handles tbIniFile.Enter
        btnSave.Enabled = True
        tbIniFile.SelectionStart = tbIniFile.Text.Length
        tbIniFile.DeselectAll()
    End Sub
End Class