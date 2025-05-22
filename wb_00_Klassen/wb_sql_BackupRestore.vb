Imports System.Text
Imports ICSharpCode.SharpZipLib
Imports Signum.OrgaSoft.Common
Imports WinBack

Public Class wb_sql_BackupRestore

    Public Function DatenSicherungRemote(FilePath As String, ByRef ProgressBar As System.Windows.Forms.ProgressBar) As Boolean

        'Datensicherung Daten/Chargen
        Dim cmd As String = wb_Global.WinBackSaveServer & " " & Now.ToString("yyyyMMdd")

        'Datensicherung starten
        Trace.WriteLine("@I_Start Datensicherung WinBack")
        ProgressBar.Value = 20
        'Datensicherung über ssh
        Dim Result As String = wb_Functions.ExecSSH(wb_Credentials.SSHUser, wb_Credentials.SSHPass, wb_GlobalSettings.MySQLServerIP, cmd)
        Dim ResultLines() As String = Result.Split(vbLf)

        'Files per FTP ins Zielverzeichnis kopieren (/home/herbst ist in ftp herbst:herbst schon vorab eingestellt)
        Dim FNameDaten As String = "/backup/WinBack-" & Now.ToString("yyyyMMdd") & "-Daten.bz2"
        Trace.WriteLine("@I_FTP-Übertragung Datensicherung WinBack-" & Now.ToString("yyyyMMdd") & "-Daten.bz2")
        ProgressBar.Value = 60
        Result = wb_Functions.FTP_Download_File(FNameDaten, FilePath)
        If Result IsNot Nothing Then
            MsgBox("Fehler bei der Datenübertragung vom WinBack-Server" & vbCrLf & Result, MsgBoxStyle.Critical, "Datensicherung WinBack-Daten")
            Return False
        End If

        Dim FNameChargen As String = "/backup/WinBack-" & Now.ToString("yyyyMMdd") & "-Chargen.bz2"
        Trace.WriteLine("@I_FTP-Übertragung Datensicherung WinBack-" & Now.ToString("yyyyMMdd") & "-Chargen.bz2")
        ProgressBar.Value = 80
        Result = wb_Functions.FTP_Download_File(FNameChargen, FilePath)
        If Result IsNot Nothing Then
            MsgBox("Fehler bei der Datenübertragung vom WinBack-Server" & vbCrLf & Result, MsgBoxStyle.Critical, "Datensicherung WinBack-Chargen")
            Return False
        End If

        'Datenübertragung war fehlerfrei
        Return True
    End Function

    Public Function DatenSicherung(Filename As String, MySql3_2 As Boolean, ByRef ProgressBar As System.Windows.Forms.ProgressBar) As Boolean
        Dim DumpFileName As String
        Dim DBName As String
        Dim Result As Boolean

        'Datenbank-Name winback oder wbdaten - abhängig vom aktuell eingestellten Mandanten
        DBName = DatenBankName(Filename)
        'Cursor umschalten
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Datensicherung soll anschliessend komprimiert werden
        If IO.Path.GetExtension(Filename) = ".bz2" Then
            DumpFileName = IO.Path.GetDirectoryName(Filename) + "\" + IO.Path.GetFileNameWithoutExtension(Filename) + ".sql"

            'Datensicherung starten
            Trace.WriteLine("@I_Start Datensicherung WinBack")
            ProgressBar.Value = 20
            Result = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Dump.bat", DumpFileName, DBName, True)

            'Datensicherung umwandeln so dass MySQL 3.2 die Daten lesen kann
            If MySql3_2 Then
                Trace.WriteLine("@I_Datensicherung bearbeiten (MySql 3.x.xx)")
                ProgressBar.Value = 40

                If Filename.Contains("Chargen") Then
                    PrepareSQLFile(DumpFileName, "wbdaten", True, True)
                Else
                    PrepareSQLFile(DumpFileName, "winback", True, True)
                End If
            End If

            'File komprimieren
            Trace.WriteLine("@I_Datei komprimieren WinBack")
            ProgressBar.Value = 60
            wb_Functions.bz2CompressFile(DumpFileName, Filename)

            'ursprüngliche Datensicherung löschen (Try/Catch für den Fall, dass die Datei von anderen Programmen geöffnet ist und nicht gelöscht werden kan)
            Try
                Trace.WriteLine("@I_Ende  Datensicherung WinBack")
                ProgressBar.Value = 100
                My.Computer.FileSystem.DeleteFile(DumpFileName)
            Catch
                Trace.WriteLine("@E_Fehler beim Löschen der Temp-Dateien(ursprüngliche Datensicherung")
            End Try
        Else
            'Datensicherung starten
            Trace.WriteLine("@I_Start Datensicherung WinBack")
            ProgressBar.Value = 50
            Result = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Dump.bat", Filename, True)
            Trace.WriteLine("@I_Ende  Datensicherung WinBack")
            ProgressBar.Value = 100
        End If

        'Cursor wieder zurücksetzen
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Return Result
    End Function

    Public Function DatenRuecksicherungRemote(FileName As String, MySql5_0 As Boolean, ByRef ProgressBar As System.Windows.Forms.ProgressBar) As Boolean

        'File-Extension (muss .bz2 sein!)
        Dim OpenFileExtension As String = IO.Path.GetExtension(FileName)

        'Datenrücksicherung muss vorher dekomprimiert werden
        If OpenFileExtension <> ".bz2" Then
            MsgBox("Die Datensicherung muss als komprimierte Datei vorliegen (.bz2)", MsgBoxStyle.Critical, "Datenrücksicherung WinBack")
            Return False
        End If

        'Datenrücksicherung nur -Daten.bz2 oder -Chargen.bz2
        Dim cmd As String = "winback_load --from server "
        If FileName.Contains("-Daten.bz2") Then
            cmd &= "--daten "
        ElseIf FileName.Contains("-Chargen.bz2") Then
            cmd &= "--chargen "
        Else
            MsgBox("Die Datensicherung muss Daten oder Chargen enthalten!", MsgBoxStyle.Critical, "Datenrücksicherung WinBack")
            Return False
        End If

        'Datei upload per FTP
        Dim FilePathAndName As String = "/backup/" & IO.Path.GetFileName(FileName)
        Trace.WriteLine("@I_Datei upload WinBack")
        ProgressBar.Value = 20
        Dim Result As String = wb_Functions.FTP_Upload_File(FileName, FilePathAndName)
        If Result IsNot Nothing Then
            MsgBox("Fehler beim Upload der Daten auf den WinBack-Server" & vbCrLf & Result, MsgBoxStyle.Critical, "Datenrücksicherung WinBack-Server")
            Return False
        End If

        'Datenrücksicherung über ssh
        Dim f() As String = IO.Path.GetFileName(FileName).Split("-")
        If f.Length = 3 Then
            cmd &= f(1)
        Else
            MsgBox("Der Dateiname muss WinBack-XXX-Daten/Chargen.bz2 sein !" & vbCrLf & Result, MsgBoxStyle.Critical, "Datenrücksicherung WinBack-Server")
            Return False
        End If
        Trace.WriteLine("@I_Datei laden WinBack")
        ProgressBar.Value = 60
        Result = wb_Functions.ExecSSH(wb_Credentials.SSHUser, wb_Credentials.SSHPass, wb_GlobalSettings.MySQLServerIP, cmd)
        Dim ResultLines() As String = Result.Split(vbLf)
        MsgBox(Result, MsgBoxStyle.Information, "Datenrücksicherung WinBack-Server")

        'Cursor umschalten
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Result.Contains("Fehler") Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Function DatenRuecksicherung(FileName As String, MySql5_0 As Boolean, ByRef ProgressBar As System.Windows.Forms.ProgressBar, Optional PrepareSQL As Boolean = True) As Boolean
        Dim OpenFileExtension As String
        Dim DumpFileName As String
        Dim DBName As String
        Dim Result As Boolean

        'Cursor umschalten
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            OpenFileExtension = IO.Path.GetExtension(FileName)

            'Datenbank-Name winback oder wbdaten - abhängig vom Mandanten
            DBName = DatenBankName(FileName)

            'Datenrücksicherung muss vorher dekomprimiert werden
            If OpenFileExtension = ".bz2" Then
                DumpFileName = IO.Path.GetDirectoryName(FileName) + "\" + IO.Path.GetFileNameWithoutExtension(FileName) + ".sql"

                'File Dekomprimieren
                Trace.WriteLine("@I_Datei dekomprimieren WinBack")
                ProgressBar.Value = 20
                'TODO als TASK laufen lassen (wegen Prozessorzeit)
                wb_Functions.bz2DecompressFile(FileName, DumpFileName)
                'Kommentare aus .sql-File entfernen (Inkompatibilität MySql 3.xx nach 5.xx)
                Trace.WriteLine("@I_Datensicherung bearbeiten (MySql 3.x.xx)")
                ProgressBar.Value = 40
                If PrepareSQL Then
                    PrepareSQLFile(DumpFileName, DBName, MySql5_0, True)
                End If
                'Datenrücksicherung starten
                Trace.WriteLine("@I_Start Datenrücksicherung WinBack")
                ProgressBar.Value = 60

                Result = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Restore.bat", DumpFileName, True)
                Trace.WriteLine("@I_Ende  Datenrücksicherung WinBack")
                ProgressBar.Value = 100
                'dekomprimierte Datei löschen
                My.Computer.FileSystem.DeleteFile(DumpFileName)
            Else
                'Kommentare aus .sql-File entfernen (Inkompatibilität MySql 3.xx nach 5.xx)
                Trace.WriteLine("@I_Datensicherung bearbeiten (MySql 3.x.xx)")
                ProgressBar.Value = 20
                'Die Original-Datei bleibt erhalten
                If PrepareSQL Then
                    PrepareSQLFile(FileName, DBName, MySql5_0, False)
                Else
                    My.Computer.FileSystem.CopyFile(FileName, FileName & ".tmp", True)
                End If
                'Datenrücksicherung starten
                Trace.WriteLine("@I_Start Datenrücksicherung WinBack")
                ProgressBar.Value = 60
                Result = wb_Functions.DoBatch(wb_GlobalSettings.MySQLPath, "MySQL_Restore.bat", FileName & ".tmp", True)
                Trace.WriteLine("@I_Ende  Datenrücksicherung WinBack")
                ProgressBar.Value = 90
                'Ursprungs-Datei löschen
                My.Computer.FileSystem.DeleteFile(FileName & ".tmp")
                ProgressBar.Value = 100
            End If

            'Cursor wieder zurücksetzen
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Return Result
        Catch ex As Exception
            'Cursor wieder zurücksetzen
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Return False
        End Try
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
    Private Sub PrepareSQLFile(FileName As String, DataBase As String, MySql3_2 As Boolean, RemoveOriginalFile As Boolean)
        Dim xFileName As String
        Dim Zeile As String
        Dim encCharSet As String

        'Datensicherung zeilenweise kopieren nach x
        xFileName = FileName + ".tmp"
        'Datei zum Schreiben öffnen
        Dim sw As New System.IO.StreamWriter(xFileName, False, Encoding.GetEncoding("iso-8859-1"), 255)

        'SQL-Header in Datensicherungs-File eintragen
        sw.WriteLine("DROP DATABASE IF EXISTS " + DataBase + ";")
        sw.WriteLine("CREATE DATABASE " + DataBase + ";")
        sw.WriteLine("USE " + DataBase + ";")

        'Source-File zum Lesen öffnen
        If MySql3_2 Then
            encCharSet = "utf-8"
        Else
            encCharSet = "iso-8859-1"
        End If
        encCharSet = "utf-8"

        For Each Zeile In System.IO.File.ReadAllLines(FileName, Encoding.GetEncoding(encCharSet))
            'Kommentare, "USE" und "CREATE" entfernen
            If ((Strings.Left(Zeile, 2) <> "--") AndAlso Not (Strings.Left(Zeile, 3) = "/*!") AndAlso Not (Zeile.Contains("USE")) AndAlso Not (Zeile.Contains("CREATE DATABASE")) AndAlso Not (Zeile.Contains("DROP DATABASE"))) Then
                'Konvertierung MySQL 3.x
                Zeile = ConvertToMySQL3(Zeile)
                'Konvertierung Sonderzeichen und Umlaute
                If MySql3_2 Then
                    Zeile = wb_Functions.UTF8toMySql(Zeile)
                End If
                sw.WriteLine((Zeile))
                'Debug.Print("... " & Zeile)
            End If
        Next
        sw.Close()

        'Ursprungs-Datei löschen
        If RemoveOriginalFile Then
            'Ursprungs-Datei löschen
            My.Computer.FileSystem.DeleteFile(FileName)
            'Erzeugte Datei umbenennen
            My.Computer.FileSystem.RenameFile(xFileName, IO.Path.GetFileName(FileName))
        End If
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

    ''' <summary>
    ''' Gibt abhängig vom Dateinamen die enstprechende Datenbank zurück.
    ''' Die Datenbank wird aus den Angaben der winback.ini ausgelesen (Mandanten). Enthält der Dateiname den Text Chargen, wird die 
    ''' entsprechende Archiv-Datenbank(wbchargen) zurückgegeben, ansonsten die Arbeits-Datenbank(winback)
    ''' 
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    Private Function DatenBankName(FileName As String) As String
        If FileName.Contains("Chargen") Then
            Return wb_GlobalSettings.MySQLWbDaten
        Else
            Return wb_GlobalSettings.MySQLWinBack
        End If
    End Function

    Private Function CheckRemoteName(Name() As String) As Boolean
        'Filename auf Gültigkeit prüfen
        If Name.Length = 3 Then
            If Name(0) = "WinBack" AndAlso (Name(2) = "Daten" OrElse Name(2) = "Chargen") Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function MSSQL_Datensicherung(FileName As String, TableName As String, Optional Zip As Boolean = True) As Boolean
        'Datenbank-Verbindung öffnen
        Dim OrgaSoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Filename ohne Extension 
        FileName = System.IO.Path.GetFileNameWithoutExtension(FileName)

        'Datenbank-Tabelle sichern
        Dim sql As String = "BACKUP Database " & TableName & " TO DISK='" & FileName & ".bak'"
        OrgaSoftMain.sqlCommand(sql)
        'Datenbank-Verbindung wieder schliessen
        OrgaSoftMain.Close()

        'wenn notwendig wird die Datei komprimiert
        If Zip Then
            wb_Functions.bz2CompressFile(FileName & ".bz2", FileName & ".bak")
        End If

        'Datensicherung beendet
        Return True
    End Function

    Public Function MSSQL_DatenRuecksicherung(FileName As String, TableName As String) As Boolean
        'Datenbank-Verbindung öffnen
        Dim OrgaSoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'wenn notwendig wird die Datei dekomprimiert
        Dim Bz2 As Boolean = FileName.Contains(".bz2")
        'Filename ohne Extension 
        FileName = System.IO.Path.GetDirectoryName(FileName) & "\" & System.IO.Path.GetFileNameWithoutExtension(FileName)

        'Unzip
        If Bz2 Then
            Debug.Print("BUnzip2...")
            wb_Functions.bz2DecompressFile(FileName & ".bz2", FileName & ".bak")
        End If

        'Datenbank-Tabelle Restore
        Dim sql As String = "RESTORE Database " & TableName & " FROM DISK='" & FileName & ".bak'"
        Debug.Print("Restore...")
        'Sql-Kommando Restore - Timeout 15 Minuten
        OrgaSoftMain.sqlCommand(sql,,, 900)
        'Datenbank-Verbindung wieder schliessen
        OrgaSoftMain.Close()

        'unzip-File wieder löschen
        If Bz2 Then
            Debug.Print("Delete...")
            System.IO.File.Delete(FileName & ".bak")
        End If

        'Datensicherung beendet
        Return True
    End Function
End Class
