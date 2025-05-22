Imports System.IO
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Schnittstelle_Konfig
    Inherits DockContent

    Private KonfigFiles As String()

    Private Sub wb_Schnittstelle_Konfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Liste der verfügbaren Schnittstellen
        cbFormatSchnittstelle.Items.Clear()
        'Prüfen ob das Verzeichnis mit den Schnittstelle-Konfigurations-Files existiert
        If Directory.Exists(wb_GlobalSettings.pXConfigPath) Then

            'alle Definitions-Dateien auflisten
            Try
                'Suche alle Konfigurations-Files (*.csv)
                KonfigFiles = System.IO.Directory.GetFiles(wb_GlobalSettings.pXConfigPath, "*.csv")

                'Namen der Schnittstellen entspricht dem Filenamen ohne Pfad und Extension
                For Each s As String In KonfigFiles
                    'Schnittstellen-Dateien mit _Txxxx enthalten die Tabellen-Definitionen
                    If Not s.Contains("_T") Then
                        cbFormatSchnittstelle.Items.Add(Path.GetFileNameWithoutExtension(s))
                    End If
                Next
            Catch ex As Exception
            End Try
        End If

        'Wenn eine Default-Schnittstelle definiert ist, dann wird diese Konfiguration als erstes geladen
        If wb_GlobalSettings.DefaultSchnittstelle <> "" Then
            cbFormatSchnittstelle.SelectedItem = wb_GlobalSettings.DefaultSchnittstelle
        End If
        'Verzeichnisse einstellen
        If wb_GlobalSettings.ImportPath <> "" Then
            tbImportVerz.Text = wb_GlobalSettings.ImportPath
        End If
        If wb_GlobalSettings.ExportPath <> "" Then
            tbExportVerz.Text = wb_GlobalSettings.ExportPath
        End If

        'Log-Level einstellen
        cbLogLevel.SelectedIndex = 0
        cbLogLevel.Enabled = False

        'Admin hat immer den Experten Modus
        If wb_AktUser.SuperUser Then
            chkExpert.Checked = True
        End If

        'TESTTEST
        chkDebug.Checked = True
    End Sub

    Private Sub wb_Schnittstelle_Konfig_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Default-Werte speichern
        wb_GlobalSettings.DefaultSchnittstelle = cbFormatSchnittstelle.SelectedItem
        wb_GlobalSettings.ExportPath = tbExportVerz.Text
        wb_GlobalSettings.ImportPath = tbImportVerz.Text
    End Sub

    Private Sub cbFormatSchnittstelle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFormatSchnittstelle.SelectedIndexChanged
        'Filename der Konfigurations-Datei
        Dim FName As String = wb_GlobalSettings.pXConfigPath & "\" & cbFormatSchnittstelle.SelectedItem & ".csv"
        'Daten einlesen
        wb_Schnittstelle_Shared.FormatChanged(FName)
    End Sub


    Private Sub BtnImportVerz_Click(sender As Object, e As EventArgs) Handles BtnImportVerz.Click
        If FolderBrowserDialog.ShowDialog = DialogResult.OK Then
            tbImportVerz.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub BtnExportVerz_Click(sender As Object, e As EventArgs) Handles BtnExportVerz.Click
        If FolderBrowserDialog.ShowDialog = DialogResult.OK Then
            tbExportVerz.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' Explorer öffnen. Start im Import-Verzeichnis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnImportExplorer_Click(sender As Object, e As EventArgs) Handles BtnImportExplorer.Click
        Process.Start("explorer.exe", tbImportVerz.Text)
    End Sub

    ''' <summary>
    ''' Explorer öffnet. Start im Export-Verzeichnis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnExportExplorer_Click(sender As Object, e As EventArgs) Handles BtnExportExplorer.Click
        Process.Start("explorer.exe", tbExportVerz.Text)
    End Sub

    Private Sub tbImportVerz_TextChanged(sender As Object, e As EventArgs) Handles tbImportVerz.TextChanged
        wb_Schnittstelle_Shared.ImportVerzeichnis = tbImportVerz.Text
    End Sub

    Private Sub tbExportVerz_TextChanged(sender As Object, e As EventArgs) Handles tbExportVerz.TextChanged
        wb_Schnittstelle_Shared.ExportVerzeichnis = tbExportVerz.Text
    End Sub

    Private Function CheckAddComboBoxItem(cbItem As String, cbBox As ComboBox) As Boolean
        cbBox.SelectedItem = cbItem
        If cbBox.SelectedItem = cbItem Then
            Return False
        Else
            cbBox.Items.Add(cbItem)
            cbBox.SelectedIndex = cbBox.Items.Count - 1
            Return True
        End If
    End Function

    Private Sub chkExpert_CheckedChanged(sender As Object, e As EventArgs) Handles chkExpert.CheckedChanged

        BtnExportVerz.Enabled = chkExpert.Checked
        BtnImportVerz.Enabled = chkExpert.Checked

        tbExportVerz.ReadOnly = Not chkExpert.Checked
        tbImportVerz.ReadOnly = Not chkExpert.Checked

        wb_Schnittstelle_Shared.ExpertMode = chkExpert.Checked
        'Event eFormatChanged auslösen ohne erneutes Laden der Import/Export-Rules
        wb_Schnittstelle_Shared.FormatChanged()
    End Sub

    Private Sub chkDebug_CheckedChanged(sender As Object, e As EventArgs) Handles chkDebug.CheckedChanged
        wb_Schnittstelle_Shared.DebugMode = chkDebug.Checked
        cbLogLevel.Enabled = chkDebug.Checked
    End Sub

    Private Sub cbLogLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLogLevel.SelectedIndexChanged
        wb_Schnittstelle_Shared.DebugLevel = cbLogLevel.SelectedIndex
    End Sub

    Private Sub cbSimulation_CheckedChanged(sender As Object, e As EventArgs) Handles cbSimulation.CheckedChanged
        wb_Schnittstelle_Shared.Simulation = cbSimulation.Checked
    End Sub
End Class