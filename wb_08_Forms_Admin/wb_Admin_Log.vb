Imports System.IO
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Admin_Log
    Inherits DockContent

    Private Sub wb_Admin_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Logger - Konfiguration
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
        BtnLog4Viewer.Enabled = Enable
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
            Process.Start("notepad.exe", dlg.FileName)
        End If
    End Sub

    Private Sub LoadLoggerFromTextFile()
        ''wenn schon eine Textdatei existiert
        'If cbLogTextFile.Checked And File.Exists(myDocPathLogFile) Then

        '    'alle Einträge aus dem Textfile laden
        '    Dim objStreamReader As StreamReader
        '    Dim strLine As String

        '    'Pass the file path and the file name to the StreamReader constructor.
        '    objStreamReader = New StreamReader(myDocPathLogFile)

        '    'Read the first line of text.
        '    strLine = objStreamReader.ReadLine

        '    'Continue to read until you reach the end of the file.
        '    Do While Not strLine Is Nothing
        '        tbLogger.Text = tbLogger.Text + strLine + vbCrLf
        '        'Read the next line.
        '        strLine = objStreamReader.ReadLine
        '    Loop
        '    'Close the file.
        '    objStreamReader.Close()
        'End If
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
End Class