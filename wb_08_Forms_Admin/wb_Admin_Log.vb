Imports System.IO
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Log
    Inherits DockContent

    Private Sub wb_Admin_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Logger-Konfiguration (Konfig-Datei kopieren)
        If wb_GlobalSettings.Log4netKonfigFile <> "" Then
            LoadLoggerKonfigFile()
        End If

        'CheckBox AutoStart
        cbLogAutoStart.Checked = wb_GlobalSettings.Log4netAutoStart
        'CheckBox Aktiv
        cbLogAktiv.Checked = wb_Admin_Shared.LoggerAktiv
        'Felder ein/ausblenden
        ShowFields(cbLogAktiv.Checked)

        'alle aktuellen Einträge aus TraceListener anzeigen
        loadLoggerFromStringArray()

        'Logger (TraceListener) einbinden
        AddHandler wb_Admin_Shared.NewLogText, AddressOf wb_Admin_LogEvent
    End Sub

    Private Sub ShowFields(Enable As Boolean)
        tbLogger.Enabled = Enable
        BtnEditKonfig.Enabled = Enable
        BtnLoadKonfig.Enabled = Enable
        'Button "Start Log4View" einblenden
        BtnLog4Viewer.Enabled = wb_Functions.CheckProgramInstalled("PROSA") And Enable
    End Sub

    Private Sub LoadLoggerKonfigFile()
        'Bezeichnung Logger
        tbAktLogger.Text = wb_GlobalSettings.Log4netKonfigFile
        'wenn eine Konfig-Datei existiert
        If File.Exists(wb_GlobalSettings.pLog4netPath & wb_GlobalSettings.Log4netKonfigFile) Then
            'auf Default Logger-Konfig kopieren
            File.Copy(wb_GlobalSettings.pLog4netPath & wb_GlobalSettings.Log4netKonfigFile, wb_GlobalSettings.pAddInPath & wb_Global.Log4NetConfigFile, True)
        End If
    End Sub

    Private Sub wb_Admin_LogEvent(txt As String)
        tbLogger.Text = tbLogger.Text + txt & vbCr
    End Sub

    Private Sub BtnLoadTextFile_Click(sender As Object, e As EventArgs) Handles BtnLoadTextFile.Click
        ' Configure open file dialog box 
        Dim dlg As New OpenFileDialog()
        dlg.Title = "Log-File laden (Log4net)"
        dlg.RestoreDirectory = False
        dlg.InitialDirectory = wb_GlobalSettings.pTmpPath
        dlg.FileName = "*.log" ' Default file name
        dlg.DefaultExt = ".log" ' Default file extension
        dlg.Filter = "Log-Files |*.log" ' Filter files by extension

        'Auswahl Konfigurations-File
        If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(wb_GlobalSettings.NotePadPlusExe) Then
                Process.Start(wb_GlobalSettings.NotePadPlusExe, dlg.FileName)
            Else
                Process.Start("notepad.exe", dlg.FileName)
            End If
        End If
    End Sub

    Private Sub loadLoggerFromStringArray()
        'alle aktuellen Einträge aus den Log-Events
        For Each x As String In wb_Admin_Shared.LogEvents
            tbLogger.Text = tbLogger.Text + x
        Next
    End Sub

    Private Sub cbLogAktiv_Click(sender As Object, e As EventArgs) Handles cbLogAktiv.Click
        wb_Admin_Shared.LoggerAktiv = cbLogAktiv.Checked
        'Felder ein/ausblenden
        ShowFields(cbLogAktiv.Checked)
    End Sub

    Private Sub cbLogAutoStart_Click(sender As Object, e As EventArgs) Handles cbLogAutoStart.Click
        wb_GlobalSettings.Log4netAutoStart = cbLogAutoStart.Checked
    End Sub

    Private Sub BtnLoadKonfig_Click(sender As Object, e As EventArgs) Handles BtnLoadKonfig.Click
        ' Configure open file dialog box 
        Dim dlg As New OpenFileDialog()
        dlg.Title = "Logger-Konfiguration laden (Log4net)"
        dlg.RestoreDirectory = False
        dlg.InitialDirectory = wb_GlobalSettings.pLog4netPath
        dlg.FileName = "*.log4net" ' Default file name
        dlg.DefaultExt = ".log4net" ' Default file extension
        dlg.Filter = "Log4net-Files |*.log4net" ' Filter files by extension

        'Auswahl Konfigurations-File
        If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            wb_GlobalSettings.Log4netKonfigFile = dlg.SafeFileName
            LoadLoggerKonfigFile()
        End If
    End Sub

    Private Sub BtnLog4Viewer_Click(sender As Object, e As EventArgs) Handles BtnLog4Viewer.Click
        If Not wb_Functions.GetProcessRunning("Log4View") Then
            Process.Start(wb_GlobalSettings.Log4ViewExe)
        Else
            MsgBox("Log4View läuft schon", MsgBoxStyle.Exclamation, "Log4View")
        End If
    End Sub

    Private Sub BtnEditKonfig_Click(sender As Object, e As EventArgs) Handles BtnEditKonfig.Click
        ' Configure open file dialog box 
        Dim dlg As New OpenFileDialog()
        dlg.Title = "Logger-Konfiguration laden (Log4net)"
        dlg.RestoreDirectory = False
        dlg.InitialDirectory = wb_GlobalSettings.pLog4netPath
        dlg.FileName = "*.log4net" ' Default file name
        dlg.DefaultExt = ".log4net" ' Default file extension
        dlg.Filter = "Log4net-Files |*.log4net" ' Filter files by extension

        'Auswahl Konfigurations-File
        If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(wb_GlobalSettings.NotePadPlusExe) Then
                Process.Start(wb_GlobalSettings.NotePadPlusExe, dlg.FileName)
            Else
                Process.Start("notepad.exe", dlg.FileName)
            End If
        End If
    End Sub

End Class