Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Sync


Public Class wb_Admin_Datensicherung
    Inherits DockContent

    Private Sub Btn_SaveFile_Click(sender As Object, e As EventArgs) Handles Btn_SaveFile.Click
        If SaveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            SaveFileName.Text = SaveFileDialog.FileName
        End If
    End Sub

    Private Sub Btn_LoadFile_Click(sender As Object, e As EventArgs) Handles Btn_LoadFile.Click
        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            LoadFileName.Text = OpenFileDialog.FileName
        End If
    End Sub

    Private Sub Btn_DatenSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenSicherung.Click
        Dim SaveFileExtension As String
        Dim DumpFileName As String

        'Cursor umschalten
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        SaveFileExtension = IO.Path.GetExtension(SaveFileDialog.FileName)

        'Datensicherung soll anschliessend komprimiert werden
        If SaveFileExtension = ".bz2" Then
            DumpFileName = IO.Path.GetDirectoryName(SaveFileDialog.FileName) + "\" + IO.Path.GetFileNameWithoutExtension(SaveFileDialog.FileName) + ".sql"
            'Datensicherung starten
            wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Dump.bat", DumpFileName, True)
            'File komprimieren
            wb_Functions.bz2CompressFile(DumpFileName, SaveFileDialog.FileName)
            'ursprüngliche Datensicherung löschen
            My.Computer.FileSystem.DeleteFile(DumpFileName)
        Else
            'Datensicherung starten
            wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Dump.bat", SaveFileDialog.FileName, True)
        End If

        'Cursor wieder zurücksetzen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
    End Sub

    Private Sub Btn_DatenRueckSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenRueckSicherung.Click
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Restore.bat", LoadFileName.Text, True)
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
    End Sub
End Class