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

        'Checkboxen Debug-Level
        cbDebug.Checked = wb_GlobalSettings.Log4net_DebugLevel
        cbWarn.Checked = wb_GlobalSettings.Log4net_WarnLevel
        cbInfo.Checked = wb_GlobalSettings.Log4net_InfoLevel
        cbError.Checked = wb_GlobalSettings.Log4net_ErrorLevel

        'Checkboxen List&Label
        cbLL_DataProvider.Checked = wb_GlobalSettings.Log4net_LL_EnableDataProvider
        cbLL_DotNet.Checked = wb_GlobalSettings.Log4net_LL_EnableDotNetComponent
        cbLL_ApiCalls.Checked = wb_GlobalSettings.Log4net_LL_EnableApiCalls
        cbLL_Licensing.Checked = wb_GlobalSettings.Log4net_LL_EnableLicensing
        cbLL_PrinterInformation.Checked = wb_GlobalSettings.Log4net_LL_EnablePrinterInformation
        cbLL_Other.Checked = wb_GlobalSettings.Log4net_LL_EnableOther

        cbLL_Debug.Checked = wb_GlobalSettings.Log4net_LL_DebugLevel
        cbLL_Warn.Checked = wb_GlobalSettings.Log4net_LL_WarnLevel
        cbLL_Info.Checked = wb_GlobalSettings.Log4net_LL_InfoLevel
        cbLL_Error.Checked = wb_GlobalSettings.Log4net_LL_ErrorLevel

        'alle aktuellen Einträge aus TraceListener anzeigen
        loadLoggerFromStringArray()
        'Scroll zum Ende des Textes
        tbLogger.SelectionStart = tbLogger.Text.Length
        tbLogger.SelectionLength = 0
        tbLogger.ScrollToCaret()

        'Logger (TraceListener) einbinden
        AddHandler wb_Admin_Shared.NewLogText, AddressOf wb_Admin_LogEvent

        'List&Label Debug-Flags ein/ausblenden
#If DebugLL = False Then
        grpListLabel.Visible = False
#Else
        grpListLabel.Visible = True
#End If
    End Sub

    Private Sub ShowFields(Enable As Boolean)
        tbLogger.Enabled = Enable
        BtnEditKonfig.Enabled = Enable
        BtnLoadKonfig.Enabled = Enable
        'Button "Start Log4View" einblenden
        BtnLog4Viewer.Enabled = Enable AndAlso wb_Functions.CheckProgramInstalled("PROSA")
    End Sub

    ''' <summary>
    ''' Kopiert die ausgewählte Logger-Konfig-Datei in das AddIn/Programm-Verzeichnis und aktiviert die Logger-Konfiguration.
    ''' 
    ''' ACHTUNG
    ''' Damit die MySQL-Konfig funktioniert, muss die mysql.dll im Signum-Programm-Verzeichnis vorhanden sein!
    ''' sonst läd diese Konfiguration nicht.
    ''' </summary>
    Private Sub LoadLoggerKonfigFile()
        'Bezeichnung Logger
        tbAktLogger.Text = wb_GlobalSettings.Log4netKonfigFile
        'wenn eine Konfig-Datei existiert
        If File.Exists(wb_GlobalSettings.pLog4netPath & wb_GlobalSettings.Log4netKonfigFile) Then
            'auf Default Logger-Konfig kopieren
            File.Copy(wb_GlobalSettings.pLog4netPath & wb_GlobalSettings.Log4netKonfigFile, wb_GlobalSettings.pAddInPath & wb_Global.Log4NetConfigFile, True)
        End If

        'Konfiguration laden
        wb_Admin_Shared.LoadLoggerKonfigFile()

        'Buttons zum Laden der Log-Files/Datenbank 
        BtnLoadTextFile.Enabled = False
        BtnLoadFromDataBase.Enabled = False

        'Logger-Type in der Titel-Zeile anzeigen
        Select Case wb_Admin_Shared.Log4NetKonfigTyp
            Case wb_Global.Log4NetType.File
                Me.Text = "Ausgabe Log-Daten in Text-File"
                BtnLoadTextFile.Enabled = True
            Case wb_Global.Log4NetType.Udp
                Me.Text = "Ausgabe Log-Daten über Netzwerk(Udp)"
            Case wb_Global.Log4NetType.MySQL
                Me.Text = "Ausgabe Log-Daten in Datenbank(MySQL)"
                BtnLoadFromDataBase.Enabled = True
            Case wb_Global.Log4NetType.Undef
                Me.Text = "Ausgabe Log-Daten"
            Case Else
                Me.Text = ""
        End Select

        'Konfiguration - Voraussetzungen prüfen
        If Not wb_Admin_Shared.CheckLog4NetEnvironment() Then
            Me.Text &= " - Fehler"
        End If

        'Buttons
    End Sub

    Private Sub wb_Admin_LogEvent(txt As String)
        tbLogger.Text = tbLogger.Text & txt & vbCr
        'Scroll zum Ende des Textes
        tbLogger.SelectionStart = tbLogger.Text.Length
        tbLogger.SelectionLength = 0
        tbLogger.ScrollToCaret()
    End Sub

    ''' <summary>
    ''' Fügt den passenden Connection-String zur winback-Datenbank in die Logger-Konfiguration ein.
    ''' </summary>
    Private Sub BuildLoggerConnectionString()
        Dim Zeilen As New List(Of String)
        Dim StReader As New StreamReader(wb_GlobalSettings.pAddInPath & wb_Global.Log4NetConfigFile)
        Dim ConnString As String = "server=" & wb_GlobalSettings.MySQLServerIP & ";database=" & wb_GlobalSettings.MySQLWinBack & ";UID=herbst;password=herbst;port=3306"

        'alle Zeilen aus der Config-Datei ins Array einlesen
        While Not StReader.EndOfStream
            'zeilenweise einlesen
            Zeilen.Add(StReader.ReadLine())
        End While

        'Speicher und Datei wieder freigeben
        StReader.Close()
        StReader.Dispose()

        'alle Zeilen wieder in die Datei zurückschreiben
        Dim StWriter As New StreamWriter(wb_GlobalSettings.pAddInPath & wb_Global.Log4NetConfigFile)
        For Each ConfigZeile In Zeilen
            'Config-Zeile mit connectionsString finden
            If ConfigZeile.Contains("<connectionString") Then
                'Eintrag manipulieren
                ConfigZeile = "<connectionString value=" & Chr(34) & ConnString & Chr(34) & " />"
            End If
            'Zeile wieder in Config-Datei schreiben
            StWriter.WriteLine(ConfigZeile)
        Next

        'Speicher und Datei wieder freigeben
        StWriter.Flush()
        StWriter.Close()
        StWriter.Dispose()

    End Sub

    Private Sub BtnLoadTextFile_Click(sender As Object, e As EventArgs) Handles BtnLoadTextFile.Click
        ' Configure open file dialog box 
        Dim dlg As New OpenFileDialog()
        dlg.Title = "Log-File laden (Log4net)"
        dlg.RestoreDirectory = False
        dlg.InitialDirectory = wb_GlobalSettings.pTmpPath
        dlg.FileName = "winback.log" ' Default file name
        dlg.DefaultExt = ".log" ' Default file extension
        dlg.Filter = "Log-Files |*.log" ' Filter files by extension

        'Auswahl Log-File
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
            tbLogger.Text = tbLogger.Text & x
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

            'wenn die MySQL-Konfiguration aufgerufen wurde, muss der Connection-String angepasst werden.
            If wb_GlobalSettings.Log4netKonfigFile.Contains("MySQL") Then
                'Connection String anpassen
                BuildLoggerConnectionString()
                'Mysql.Data.dll ins Signum-Programm-Verzeichnis kopieren (wenn notwendig)
                FileCopy(wb_GlobalSettings.pAddInPath & wb_Global.SubDir_dll & "Mysql.Data.dll", wb_GlobalSettings.pProgrammPath & "Mysql.Data.dll")
            End If

            'Prüfen ob die Tabelle winback.Log4Net in der Datenbank vorhanden ist
            If Not wb_Admin_Shared.CheckLog4NetEnvironment Then
                Select Case MsgBox("Datenbank-Fehler: Die Tabelle winback.Log4Net exisitiert nicht" & vbCrLf & "Soll die Tabelle automatisch erzeugt werden?", MsgBoxStyle.YesNoCancel, "Fehler in Datenbank")
                    Case MsgBoxResult.Yes
                        Dim Admin_CheckDatabase As New wb_Admin_CheckDatabase
                        Admin_CheckDatabase.DoDBUpdate(wb_Admin_Shared.UpdateDatabaseFile)
                    Case MsgBoxResult.No
                    Case MsgBoxResult.Cancel
                End Select
            End If

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

    ''' <summary>
    ''' Laden der gespeicherten Log-Daten aus der winback.log4Net-Datenbank Tabelle
    ''' Die Daten werden sortiert nach den letzten Einträgen geladen.
    ''' Das Fenster wird als Dialog-Fenster geöffnet.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnLoadFromDataBase_Click(sender As Object, e As EventArgs) Handles BtnLoadFromDataBase.Click
        Dim AdminLogMysql As New wb_Admin_LogMysql
        AdminLogMysql.ShowDialog()
        AdminLogMysql = Nothing
    End Sub

    Private Sub cbLL_DataProvider_Click(sender As Object, e As EventArgs) Handles cbLL_DataProvider.Click
        wb_GlobalSettings.Log4net_LL_EnableDataProvider = cbLL_DataProvider.Checked
    End Sub

    Private Sub cbLL_DotNet_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_DotNet.Click
        wb_GlobalSettings.Log4net_LL_EnableDotNetComponent = cbLL_DotNet.Checked
    End Sub

    Private Sub cbLL_ApiCalls_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_ApiCalls.Click
        wb_GlobalSettings.Log4net_LL_EnableDotNetComponent = cbLL_DotNet.Checked
    End Sub

    Private Sub cbLL_Licensing_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_Licensing.Click
        wb_GlobalSettings.Log4net_LL_EnableApiCalls = cbLL_ApiCalls.Checked
    End Sub

    Private Sub cbLL_PrinterInformation_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_PrinterInformation.Click
        wb_GlobalSettings.Log4net_LL_EnablePrinterInformation = cbLL_PrinterInformation.Checked
    End Sub

    Private Sub cbLL_Other_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_Other.Click
        wb_GlobalSettings.Log4net_LL_EnableOther = cbLL_Other.Checked
    End Sub

    Private Sub cbLL_Debug_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_Debug.Click
        wb_GlobalSettings.Log4net_LL_DebugLevel = cbLL_Debug.Checked
    End Sub

    Private Sub cbLL_Info_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_Info.Click
        wb_GlobalSettings.Log4net_LL_InfoLevel = cbLL_Info.Checked
    End Sub

    Private Sub cbLL_Warn_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_Warn.Click
        wb_GlobalSettings.Log4net_LL_WarnLevel = cbLL_Warn.Checked
    End Sub

    Private Sub cbLL_Error_CheckedChanged(sender As Object, e As EventArgs) Handles cbLL_Error.Click
        wb_GlobalSettings.Log4net_LL_ErrorLevel = cbLL_Error.Checked
    End Sub

    Private Sub cbDebug_Click(sender As Object, e As EventArgs) Handles cbDebug.Click
        wb_GlobalSettings.Log4net_DebugLevel = cbDebug.Checked
    End Sub

    Private Sub cbInfo_CheckedChanged(sender As Object, e As EventArgs) Handles cbInfo.Click
        wb_GlobalSettings.Log4net_InfoLevel = cbInfo.Checked
    End Sub

    Private Sub cbWarn_CheckedChanged(sender As Object, e As EventArgs) Handles cbWarn.Click
        wb_GlobalSettings.Log4net_WarnLevel = cbWarn.Checked
    End Sub

    Private Sub cbError_CheckedChanged(sender As Object, e As EventArgs) Handles cbError.Click
        wb_GlobalSettings.Log4net_ErrorLevel = cbError.Checked
    End Sub

End Class