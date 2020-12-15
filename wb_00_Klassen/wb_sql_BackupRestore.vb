Imports System.Text
Imports WinBack

Public Class wb_sql_BackupRestore

    Public Function DatenSicherung(Filename As String, MySql3_2 As Boolean) As Boolean
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
            DatenSicherung = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Dump.bat", DumpFileName, DBName, True)

            'Datensicherung umwandeln so dass MySQL 3.2 die Daten lesen kann
            If MySql3_2 Then
                Trace.WriteLine("Datensicherung bearbeiten (MySql 3.x.xx)")
                If Filename.Contains("Chargen") Then
                    PrepareSQLFile(DumpFileName, "wbdaten", True)
                Else
                    PrepareSQLFile(DumpFileName, "winback", True)
                End If
            End If

            'File komprimieren
            Trace.WriteLine("Datei komprimieren WinBack")
            wb_Functions.bz2CompressFile(DumpFileName, Filename)

            'ursprüngliche Datensicherung löschen
            My.Computer.FileSystem.DeleteFile(DumpFileName)
            Trace.WriteLine("Ende  Datensicherung WinBack")
        Else
            'Datensicherung starten
            Trace.WriteLine("Start Datensicherung WinBack")
            DatenSicherung = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Dump.bat", Filename, True)
            Trace.WriteLine("Ende  Datensicherung WinBack")
        End If

        'Cursor wieder zurücksetzen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
        Return DatenSicherung
    End Function

    Public Function DatenRuecksicherung(FileName As String) As Boolean
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
            PrepareSQLFile(DumpFileName, DBName, False)
            'Datenrücksicherung starten
            Trace.WriteLine("Start Datenrücksicherung WinBack")
            DatenRuecksicherung = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Restore.bat", DumpFileName, True)
            Trace.WriteLine("Ende  Datenrücksicherung WinBack")
            'dekomprimierte Datei löschen
            My.Computer.FileSystem.DeleteFile(DumpFileName)
        Else
            'Kommentare aus .sql-File entfernen (Inkompatibilität MySql 3.xx nach 5.xx)
            Trace.WriteLine("Datensicherung bearbeiten (MySql 3.x.xx)")
            PrepareSQLFile(FileName, DBName, False)
            'Datenrücksicherung starten
            Trace.WriteLine("Start Datenrücksicherung WinBack")
            DatenRuecksicherung = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Restore.bat", FileName, True)
            Trace.WriteLine("Ende  Datenrücksicherung WinBack")
        End If

        'Cursor wieder zurücksetzen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
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
    Private Sub PrepareSQLFile(FileName As String, DataBase As String, MySql3_2 As Boolean)
        Dim xFileName As String
        Dim Zeile As String
        Dim encCharSet As String

        'Datensicherung zeilenweise kopieren nach x
        xFileName = FileName + ".tmp"
        'Datei zum Schreiben öffnen
        Dim sw As New System.IO.StreamWriter(xFileName, False, Encoding.GetEncoding("iso-8859-1"), 255)

        'SQL-Header in Datensicherungs-File eintragen
        sw.WriteLine("DROP DATABASE " + DataBase + ";")
        sw.WriteLine("CREATE DATABASE " + DataBase + ";")
        sw.WriteLine("USE " + DataBase + ";")

        'Source-File zum Lesen öffnen
        If MySql3_2 Then
            encCharSet = "utf-8"
        Else
            encCharSet = "iso-8859-1"
        End If
        For Each Zeile In System.IO.File.ReadAllLines(FileName, Encoding.GetEncoding(encCharSet))
            'Kommentare, "USE" und "CREATE" entfernen
            If ((Strings.Left(Zeile, 2) <> "--") And Not (Strings.Left(Zeile, 3) = "/*!") And Not (Zeile.Contains("USE")) And Not (Zeile.Contains("CREATE DATABASE"))) Then
                'Konvertierung MySQL 3.x
                Zeile = ConvertToMySQL3(Zeile)
                'Konvertierung Sonderzeichen und Umlaute
                If MySql3_2 Then
                    Zeile = wb_Functions.UTF8toMySql(Zeile)
                End If
                sw.WriteLine((Zeile))
            End If
        Next
        sw.Close()
        'Ursprungs-Datei löschen
        My.Computer.FileSystem.DeleteFile(FileName)
        'Erzeugte Datei umbenennen
        My.Computer.FileSystem.RenameFile(xFileName, IO.Path.GetFileName(FileName))
    End Sub

    Private Function ConvertToMySQL3(s As String) As String
        Dim r As String

        'TimeStamp
        r = s.Replace("timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,", "timestamp,")
        'Engine
        If r.Contains("ENGINE") Then
            r = ");"
        End If

        'Tabellen-Namen korrigieren (MySQL 3.x verwendet Gross/Kleinbuchstaben
        r = ConvertTableName(r, "Abfragen")
        r = ConvertTableName(r, "AktionsTimer")
        r = ConvertTableName(r, "AnalogKanaele")
        r = ConvertTableName(r, "AnlagenParameter")
        r = ConvertTableName(r, "AnlagParamTypen")
        r = ConvertTableName(r, "APTypen")
        r = ConvertTableName(r, "ArbRezepte")
        r = ConvertTableName(r, "ArbRZParams")
        r = ConvertTableName(r, "ArbRZSchritte")
        r = ConvertTableName(r, "ASegmente")
        r = ConvertTableName(r, "BC9000Bits")
        r = ConvertTableName(r, "BC9000Liste")
        r = ConvertTableName(r, "BC9Segmente")
        r = ConvertTableName(r, "BC9SegParams")
        r = ConvertTableName(r, "BCParameter")
        r = ConvertTableName(r, "BCParamTypen")
        r = ConvertTableName(r, "BCWegParams")
        r = ConvertTableName(r, "PreisEinheiten")
        r = ConvertTableName(r, "Einheiten")
        r = ConvertTableName(r, "ENummern")
        r = ConvertTableName(r, "Fehler")
        r = ConvertTableName(r, "Formate")
        r = ConvertTableName(r, "GeraeteParams")
        r = ConvertTableName(r, "GeraeteTypen")
        r = ConvertTableName(r, "Geraete")
        r = ConvertTableName(r, "Hinweise2")
        r = ConvertTableName(r, "HMenue")
        r = ConvertTableName(r, "HRechte")
        r = ConvertTableName(r, "IAListe")
        r = ConvertTableName(r, "IAttrGruppen")
        r = ConvertTableName(r, "IAttribute")
        r = ConvertTableName(r, "IAttrParams")
        r = ConvertTableName(r, "ItemIDs")
        r = ConvertTableName(r, "ItemParameter")
        r = ConvertTableName(r, "ItemSubTypen")
        r = ConvertTableName(r, "ItemTypen")
        r = ConvertTableName(r, "Komponenten")
        r = ConvertTableName(r, "KomponParams")
        r = ConvertTableName(r, "KomponTypen")
        r = ConvertTableName(r, "Konfiguration")
        r = ConvertTableName(r, "Lagerorte")
        r = ConvertTableName(r, "Lieferungen")
        r = ConvertTableName(r, "LinienGruppen")
        r = ConvertTableName(r, "LinienBCs")
        r = ConvertTableName(r, "Linien")
        r = ConvertTableName(r, "Mehlprotokoll")
        r = ConvertTableName(r, "VHRezepte")
        r = ConvertTableName(r, "Rezepte")
        r = ConvertTableName(r, "RezeptSchritte")
        r = ConvertTableName(r, "RezeptVarianten")
        r = ConvertTableName(r, "RohParams")
        r = ConvertTableName(r, "RohTemp")
        r = ConvertTableName(r, "RohTypen")
        r = ConvertTableName(r, "Sprachen")
        r = ConvertTableName(r, "SubMenue")
        r = ConvertTableName(r, "Symbole")
        r = ConvertTableName(r, "Texte")
        r = ConvertTableName(r, "TextTypen")
        r = ConvertTableName(r, "Waagen")
        r = ConvertTableName(r, "WegeRouten")
        r = ConvertTableName(r, "Wege")
        r = ConvertTableName(r, "WegParams")
        r = ConvertTableName(r, "Zeiten")

        Return r
    End Function

    Private Function ConvertTableName(s As String, name As String) As String
        Return s.Replace(name.ToLower, name)
    End Function
End Class
