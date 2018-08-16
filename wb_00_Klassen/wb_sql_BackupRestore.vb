Imports System.Text
Imports WinBack

Public Class wb_sql_BackupRestore

    Public Function datensicherung(Filename As String) As Boolean
        Dim SaveFileExtension As String
        Dim DumpFileName As String
        Dim DBName As String

        'Datenbank-Name winback oder wbdaten - abhängig vom aktuell eingestellten Mandanten
        If Filename.Contains("Chargen") Then
            DBName = wb_GlobalSettings.MySQLWbDaten
        Else
            DBName = wb_GlobalSettings.MySQLWinBack
        End If

        'Cursor umschalten
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        SaveFileExtension = IO.Path.GetExtension(Filename)

        'Datensicherung soll anschliessend komprimiert werden
        If SaveFileExtension = ".bz2" Then
            DumpFileName = IO.Path.GetDirectoryName(Filename) + "\" + IO.Path.GetFileNameWithoutExtension(Filename) + ".sql"

            'Datensicherung starten
            Trace.WriteLine("Start Datensicherung WinBack")
            wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Dump.bat", DumpFileName, DBName, True)

            'File komprimieren
            Trace.WriteLine("Datei komprimieren WinBack")
            wb_Functions.bz2CompressFile(DumpFileName, Filename)

            'ursprüngliche Datensicherung löschen
            My.Computer.FileSystem.DeleteFile(DumpFileName)
            Trace.WriteLine("Ende  Datensicherung WinBack")
        Else
            'Datensicherung starten
            Trace.WriteLine("Start Datensicherung WinBack")
            wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Dump.bat", Filename, True)
            Trace.WriteLine("Ende  Datensicherung WinBack")
        End If

        'Cursor wieder zurücksetzen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default

        Return True
    End Function

    Public Function datenruecksicherung(FileName As String) As Boolean
        Dim OpenFileExtension As String
        Dim DumpFileName As String
        Dim DBName As String

        'Cursor umschalten
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        OpenFileExtension = IO.Path.GetExtension(FileName)

        'Datenbank-Name winback oder wbdaten - abhängig vom Mandanten
        If FileName.Contains("Chargen") Then
            DBName = wb_GlobalSettings.MySQLWbDaten
        Else
            DBName = wb_GlobalSettings.MySQLWinBack
        End If

        'Datenrücksicherung muss vorher dekomprimiert werden
        If OpenFileExtension = ".bz2" Then
            DumpFileName = IO.Path.GetDirectoryName(FileName) + "\" + IO.Path.GetFileNameWithoutExtension(FileName) + ".sql"

            'File Dekomprimieren
            Trace.WriteLine("Datei dekomprimieren WinBack")
            wb_Functions.bz2DecompressFile(FileName, DumpFileName)
            'Kommentare aus .sql-File entfernen (Inkompatibilität MySql 3.xx nach 5.xx)
            Trace.WriteLine("Datensicherung bearbeiten (MySql 3.x.xx)")
            PrepareSQLFile(DumpFileName, DBName)
            'Datenrücksicherung starten
            Trace.WriteLine("Start Datenrücksicherung WinBack")
            wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Restore.bat", DumpFileName, True)
            Trace.WriteLine("Ende  Datenrücksicherung WinBack")
            'dekomprimierte Datei löschen
            My.Computer.FileSystem.DeleteFile(DumpFileName)
        Else
            'Kommentare aus .sql-File entfernen (Inkompatibilität MySql 3.xx nach 5.xx)
            Trace.WriteLine("Datensicherung bearbeiten (MySql 3.x.xx)")
            PrepareSQLFile(FileName, DBName)
            'Datenrücksicherung starten
            Trace.WriteLine("Start Datenrücksicherung WinBack")
            wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Restore.bat", FileName, True)
            Trace.WriteLine("Ende  Datenrücksicherung WinBack")
        End If

        'Cursor wieder zurücksetzen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Return True

    End Function

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
