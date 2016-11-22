Imports System.Text
Imports WeifenLuo.WinFormsUI.Docking


Public Class wb_Admin_Datensicherung
    Inherits DockContent

    ''' <summary>
    ''' Datensicherung starten
    ''' </summary>
    Private Sub Btn_DatenSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenSicherung.Click
        Dim SaveFileExtension As String
        Dim DumpFileName As String

        If SaveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim FileName As String = SaveFileName.Text
            FileName = SaveFileDialog.FileName
            SaveFileName.Focus()

            'Cursor umschalten
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            SaveFileExtension = IO.Path.GetExtension(FileName)

            'Datensicherung soll anschliessend komprimiert werden
            If SaveFileExtension = ".bz2" Then
                DumpFileName = IO.Path.GetDirectoryName(FileName) + "\" + IO.Path.GetFileNameWithoutExtension(FileName) + ".sql"
                'Datensicherung starten
                Trace.WriteLine("Start Datensicherung WinBack -> " + DumpFileName)
                wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Dump.bat", DumpFileName, True)
                Trace.WriteLine("Ende  Datensicherung WinBack -> " + DumpFileName)
                'File komprimieren
                Trace.WriteLine("Datei komprimieren   WinBack -> " + FileName)
                wb_Functions.bz2CompressFile(DumpFileName, FileName)
                'ursprüngliche Datensicherung löschen
                My.Computer.FileSystem.DeleteFile(DumpFileName)
            Else
                'Datensicherung starten
                Trace.WriteLine("Start Datensicherung WinBack -> " + FileName)
                wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Dump.bat", FileName, True)
                Trace.WriteLine("Ende  Datensicherung WinBack -> " + FileName)
            End If

            'Cursor wieder zurücksetzen
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        End If

    End Sub

    ''' <summary>
    ''' Daten-Rücksicherung starten
    ''' </summary>
    Private Sub Btn_DatenRueckSicherung_Click(sender As Object, e As EventArgs) Handles Btn_DatenRueckSicherung.Click
        Dim OpenFileExtension As String
        Dim DumpFileName As String

        If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim fileName As String = OpenFileDialog.FileName
            LoadFileName.Text = fileName
            LoadFileName.Focus()

            'Cursor umschalten
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
            OpenFileExtension = IO.Path.GetExtension(LoadFileName.Text)

            'Datenrücksicherung muss vorher dekomprimiert werden
            If OpenFileExtension = ".bz2" Then
                DumpFileName = IO.Path.GetDirectoryName(fileName) + "\" + IO.Path.GetFileNameWithoutExtension(fileName) + ".sql"

                'File Dekomprimieren
                Trace.WriteLine("Datei dekomprimieren WinBack -> " + fileName)
                wb_Functions.bz2DecompressFile(fileName, DumpFileName)
                'Kommentare aus .sql-File entfernen (Inkompatibilität MySql 3.xx nach 5.xx)
                Trace.WriteLine("Datensicherung bearbeiten (MySql 3.x.xx) " + DumpFileName)
                PrepareSQLFile(DumpFileName, "winback")
                'Datenrücksicherung starten
                Trace.WriteLine("Start Datenrücksicherung WinBack -> " + DumpFileName)
                wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Restore.bat", DumpFileName, True)
                Trace.WriteLine("Ende  Datenrücksicherung WinBack -> " + DumpFileName)
                'dekomprimierte Datei löschen
                My.Computer.FileSystem.DeleteFile(DumpFileName)
            Else
                'Kommentare aus .sql-File entfernen (Inkompatibilität MySql 3.xx nach 5.xx)
                Trace.WriteLine("Datensicherung bearbeiten (MySql 3.x.xx) " + LoadFileName.Text)
                PrepareSQLFile(LoadFileName.Text, "winback")
                'Datenrücksicherung starten
                Trace.WriteLine("Start Datenrücksicherung WinBack -> " + LoadFileName.Text)
                wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Restore.bat", LoadFileName.Text, True)
                Trace.WriteLine("Ende  Datenrücksicherung WinBack -> " + LoadFileName.Text)
            End If

            'Cursor wieder zurücksetzen
            Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default

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

    ''' <summary>
    ''' Datensicherung xxx.sql für mySQL-Import vorbereiten. 
    ''' Fügt 3 Zeilen am Anfang der Datei ein:
    ''' - DROP DATABASE winback;
    ''' - CREATE DATABASE winback;
    ''' - USE winback;
    ''' 
    ''' Löscht alle SQL-Kommentare in der Import-Datei
    ''' </summary>
    ''' <remarks>
    ''' Damit der Import in die mysql-Datenbank (V5.xxx) funktioniert muss in der my.ini
    ''' der SQL-Mode STRICT abgeschaltet werden: (C:\Program Files\MySQL\MySQL Server 5.0)
    ''' 
    ''' # Set the SQL mode to NO strict (14.11.2016/JW)
    ''' sql-mode="NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION"
    ''' </remarks>
    ''' <param name="FileName"> String Dateiname und Pfad inlusive Extension</param>
    ''' <param name="DataBase"> String Datenbank (winback/wbdaten)</param>
    Private Sub PrepareSQLFile(FileName As String, DataBase As String)
        Dim xFileName As String
        Dim Zeile As String

        'Datensicherung zeilenweise kopieren nach x
        xFileName = FileName + ".tmp"
        'Datei zum Schreiben öffnen
        Dim sw As New System.IO.StreamWriter(xFileName, False, Encoding.GetEncoding("iso-8859-1"), 255)

        'SQL-Header in Datensicherungs-File eintragen
        sw.WriteLine("DROP DATABASE " + DataBase + ";")
        sw.WriteLine("CREATE DATABASE " + DataBase + ";")
        sw.WriteLine("USE " + DataBase + ";")

        'Source-File zum Lesen öffnen
        For Each Zeile In System.IO.File.ReadAllLines(FileName, Encoding.GetEncoding("iso-8859-1"))
            If Strings.Left(Zeile, 2) <> "--" Then
                sw.WriteLine((Zeile))
            End If
        Next
        sw.Close()
        'Ursprungs-Datei löschen
        My.Computer.FileSystem.DeleteFile(FileName)
        'Erzeugte Datei umbenennen
        My.Computer.FileSystem.RenameFile(xFileName, IO.Path.GetFileName(FileName))
    End Sub

End Class