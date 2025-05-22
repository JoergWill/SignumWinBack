Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Functions

Public Class wb_Admin_Datensicherung
    Inherits DockContent

    Dim mysql As New Global.WinBack.wb_sql_BackupRestore

    ''' <summary>
    ''' Laden des Formulars.
    ''' Filename für Datensicherung und Daten-Rücksicherung aus winback.ini laden
    ''' </summary>
    Private Sub wb_Admin_Datensicherung_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Konfiguration aus winback.ini
        Dim IniFile As New wb_IniFile
        'Pfade und Einstellungen Datensicherung
        BackupFileName.Text = IniFile.ReadString("Backup", "BackupFileName", "")
        BackupTimeStamp.Text = IniFile.ReadString("Backup", "BackupTimeStamp", "")
        'Pfade und Einstellungen Datenrücksicherung
        RestoreFileName.Text = IniFile.ReadString("Backup", "RestoreFileName", "")
        RestoreTimeStamp.Text = IniFile.ReadString("Backup", "RestoreTimeStamp", "")
        'IniFile wieder freigeben
        IniFile = Nothing
    End Sub

    ''' <summary>
    ''' Datensicherung starten.
    ''' Anhand der MySQl-Version wird unteschieden zwischen lokaler(Windows) und Linux-Datenbank.
    ''' 
    ''' Wird der Remote-Rechner gesichert(Linux) werden Daten und Chargen gesichert. Gespeichert werden
    ''' die Dateien im Format WinBack-YYYYMMDD-Daten/Chargen.bz2.
    ''' Nach der Datensicherung werden die Files per FTP in das angegebene Verzeichnis kopiert.
    ''' </summary>
    Private Sub Btn_DatenSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenSicherung.Click
        'Datensicherung Lokal oder Remote
        If wb_GlobalSettings.LocalMySql Then
            'Datensicherung lokal Daten oder Chargen
            Dim dlg As New SaveFileDialog()
            dlg.RestoreDirectory = False
            If BackupFileName.Text <> "" Then
                dlg.InitialDirectory = Path.GetDirectoryName(BackupFileName.Text)
            End If
            dlg.FileName = "*.bz2" ' Default file name
            dlg.DefaultExt = ".bz2" ' Default file extension
            dlg.Filter = "sql-Backup-Files (.sql)|*.bz2" ' Filter files by extension

            If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                BackupFileName.Text = dlg.FileName
                lblFortschrittSave.Focus()
                Datensicherung_Local(dlg.FileName)
            End If
        Else
            'Datensicherung Remote in Verzeichnis...
            Dim dlg As New FolderBrowserDialog()
            If BackupFileName.Text <> "" Then
                dlg.SelectedPath = Path.GetDirectoryName(BackupFileName.Text)
            End If
            dlg.Description = "Daten/Chargen von " & wb_GlobalSettings.MySQLServerIP & " sichern nach ..."
            If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    BackupFileName.Text = dlg.SelectedPath
                    lblFortschrittSave.Focus()
                    'Datensicherung Remote Daten und Chargen
                    Datensicherung_Remote(dlg.SelectedPath)
                End If
            End If
    End Sub

    ''' <summary>
    ''' Datensicherung lokale(Windows) MySql-Datenbank.
    ''' </summary>
    ''' <param name="FileName"></param>
    Private Sub Datensicherung_Local(FileName As String)
        'Datensicherung starten
        If mysql.DatenSicherung(FileName, cbFormatMySQL3_2.Checked, ProgressBackup) Then
            WriteIni("Backup", FileName)
            MsgBox("Die WinBack-Daten wurden erfolgreich gesichert", MsgBoxStyle.Exclamation)
        Else
            MsgBox("Fehler bei der WinBack-Daten-Sicherung", MsgBoxStyle.Exclamation)
        End If
    End Sub

    ''' <summary>
    ''' Datensicherung Linux-Datenbank per ssh-Kommando
    ''' </summary>
    ''' <param name="FilePath"></param>
    Private Sub Datensicherung_Remote(FilePath As String)
        'Datensicherung starten
        If mysql.DatenSicherungRemote(FilePath, ProgressBackup) Then
            WriteIni("Backup", FilePath)
            MsgBox("Die WinBack-Daten wurden erfolgreich gesichert", MsgBoxStyle.Exclamation)
        Else
            MsgBox("Fehler bei der WinBack-Daten-Sicherung", MsgBoxStyle.Exclamation)
        End If

    End Sub

    ''' <summary>
    ''' Daten-Rücksicherung starten
    ''' </summary>
    Private Sub Btn_DatenRueckSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenRueckSicherung.Click
        ' Configure open file dialog box 
        Dim dlg As New OpenFileDialog()
        dlg.RestoreDirectory = False
        If RestoreFileName.Text <> "" Then
            dlg.InitialDirectory = Path.GetDirectoryName(RestoreFileName.Text)
        End If
        dlg.FileName = Path.GetFileName(RestoreFileName.Text) ' Default file name
        dlg.DefaultExt = ".bz2" ' Default file extension
        dlg.Filter = "sql-Backup-Files (.sql)|*.bz2" ' Filter files by extension

        If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            RestoreFileName.Text = dlg.FileName
            lblFortschrittRestore.Focus()

            If wb_GlobalSettings.LocalMySql Then
                DatenRuecksicherung_Local(dlg.FileName)
            Else
                DatenRuecksicherung_Remote(dlg.FileName)
            End If
        End If
    End Sub

    Private Sub DatenRuecksicherung_Local(FileName As String)
        'Datenrücksicherung starten
        If mysql.DatenRuecksicherung(RestoreFileName.Text, cbFormatMySQL5_0.Checked, ProgressRestore) Then
            'Konfiguration in winback.ini schreiben
            WriteIni("Restore", FileName)
            'OrgaBack/WinBack-Office neustarten
            If MsgBox("Datensicherung WinBack wurde erfolgreich geladen" & vbCrLf & vbCrLf & "Programm neu starten ?", MsgBoxStyle.YesNo, "WinBack") = MsgBoxResult.Ok Then
                wb_Functions.Restart()
            End If
        Else
            MsgBox("Fehler bei der WinBack-Daten-Rücksicherung" & vbCrLf &
                   "Damit der Import in die mysql-Datenbank (V5.xxx) funktioniert muss in der my.ini" & vbCrLf &
                   "der SQL-Mode STRICT abgeschaltet werden: (sql-mode='NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION')", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub DatenRuecksicherung_Remote(FileName As String)
        'Datenrücksicherung starten
        If mysql.DatenRuecksicherungRemote(RestoreFileName.Text, cbFormatMySQL5_0.Checked, ProgressRestore) Then
            'Konfiguration in winback.ini schreiben
            WriteIni("Restore", FileName)
            'OrgaBack/WinBack-Office neustarten
            MsgBox("Datensicherung WinBack wurde erfolgreich geladen" & vbCrLf & vbCrLf & "WinBack-Produktion muss neu gestartet werden !", MsgBoxStyle.Exclamation, "WinBack")
        Else
            MsgBox("Fehler bei der WinBack-Daten-Rücksicherung", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub WriteIni(Item As String, FileName As String)
        'Konfiguration in winback.ini schreiben
        Dim IniFile As New wb_IniFile
        'Pfade und Einstellungen Datensicherung
        Dim LocalDate = DateTime.Now
        IniFile.WriteString("Backup", Item & "FileName", FileName)
        IniFile.WriteString("Backup", Item & "TimeStamp", DateTime.Now.ToString)
        'IniFile wieder freigeben
        IniFile = Nothing
    End Sub
End Class