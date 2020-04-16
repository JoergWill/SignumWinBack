Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Functions

Public Class wb_Admin_Datensicherung
    Inherits DockContent

    ''' <summary>
    ''' Datensicherung starten
    ''' </summary>
    Private Sub Btn_DatenSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenSicherung.Click
        ' Configure open file dialog box 
        Dim dlg As New SaveFileDialog()
        dlg.RestoreDirectory = False
        If BackupFileName.Text <> "" Then
            dlg.InitialDirectory = Path.GetDirectoryName(BackupFileName.Text)
        End If
        dlg.FileName = "*.bz2" ' Default file name
        dlg.DefaultExt = ".bz2" ' Default file extension
        dlg.Filter = "sql-Backup-Files (.sql)|*.bz2" ' Filter files by extension

        If dlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then

            Dim mysql As New Global.WinBack.wb_sql_BackupRestore
            BackupFileName.Text = dlg.FileName
            BackupFileName.Focus()

            'Konfiguration in winback.ini schreiben
            Dim IniFile As New wb_IniFile
            'Pfade und Einstellungen Datensicherung
            Dim LocalDate = DateTime.Now
            IniFile.WriteString("Backup", "BackupFileName", BackupFileName.Text)
            IniFile.WriteString("Backup", "BackupTimeStamp", DateTime.Now.ToString)
            'IniFile wieder freigeben
            IniFile = Nothing

            'Datensicherung starten
            mysql.DatenSicherung(BackupFileName.Text, cbFormatMySQL3_2.Checked)
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
            Dim mysql As New Global.WinBack.wb_sql_BackupRestore
            RestoreFileName.Focus()
            'Datenrücksicherung starten
            If mysql.DatenRuecksicherung(RestoreFileName.Text) Then

                'Konfiguration in winback.ini schreiben
                Dim IniFile As New wb_IniFile
                'Pfade und Einstellungen Datensicherung
                IniFile.WriteString("Backup", "RestoreFileName", RestoreFileName.Text)
                IniFile.WriteString("Backup", "RestoreTimeStamp", DateTime.Now.ToString)
                'IniFile wieder freigeben
                IniFile = Nothing

                'OrgaBack/WinBack-Office neustarten
                If MsgBox("Datensicherung wurde erfolgreich geladen" & vbCrLf & vbCrLf & "Programm neu starten ?", MsgBoxStyle.YesNo, "WinBack") Then
                    Application.Restart()
                End If
            Else
                MsgBox("Fehler bei der Daten-Rücksicherung", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

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

End Class