Imports System.Text
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Functions


Public Class wb_Admin_Datensicherung
    Inherits DockContent

    ''' <summary>
    ''' Datensicherung starten
    ''' </summary>
    Private Sub Btn_DatenSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenSicherung.Click

        If SaveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim FileName As String = SaveFileName.Text
            Dim mysql As New WinBack.wb_sql_BackupRestore
            FileName = SaveFileDialog.FileName
            SaveFileName.Focus()
            'Datensicherung starten
            mysql.datensicherung(FileName)
        End If
    End Sub

    ''' <summary>
    ''' Daten-Rücksicherung starten
    ''' </summary>
    Private Sub Btn_DatenRueckSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenRueckSicherung.Click

        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim fileName As String = OpenFileDialog.FileName
            Dim mysql As New WinBack.wb_sql_BackupRestore
            LoadFileName.Text = fileName
            LoadFileName.Focus()
            'Datenrücksicherung starten
            mysql.datenruecksicherung(fileName)
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
        SaveFileName.Text = IniFile.ReadString("winback", "DatenSicherungSaveFileName", "C:\Temp")
        SaveFileDialog.FileName = SaveFileName.Text
        'Pfade und Einstellungen Datenrücksicherung
        LoadFileName.Text = IniFile.ReadString("winback", "DatenSicherungLoadFileName", "C:\Temp")
        OpenFileDialog.FileName = LoadFileName.Text
        'IniFile wieder freigeben
        IniFile = Nothing
    End Sub

    ''' <summary>
    ''' Filename der Daten-Sicherung in winback.ini schreiben
    ''' </summary>
    Private Sub SaveFileName_Validated(sender As Object, e As EventArgs) Handles SaveFileName.Validated
        'Konfiguration in winback.ini schreiben
        Dim IniFile As New wb_IniFile
        'Pfade und Einstellungen Datensicherung
        IniFile.WriteString("winback", "DatenSicherungSaveFileName", SaveFileName.Text)
        'IniFile wieder freigeben
        IniFile = Nothing
    End Sub

    ''' <summary>
    ''' Filename der Daten-Rücksicherung in winback.ini schreiben
    ''' </summary>
    Private Sub LoadFileName_Validated(sender As Object, e As EventArgs) Handles LoadFileName.Validated
        'Konfiguration in winback.ini schreiben
        Dim IniFile As New wb_IniFile
        'Pfade und Einstellungen Datensicherung
        IniFile.WriteString("winback", "DatenSicherungLoadFileName", LoadFileName.Text)
        'IniFile wieder freigeben
        IniFile = Nothing
    End Sub
End Class