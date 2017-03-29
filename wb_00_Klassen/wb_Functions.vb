'---------------------------------------------------------
'11.05.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Sammlung von Statischen Funktionen

Imports System.IO
Imports System.Text
Imports ICSharpCode.SharpZipLib.BZip2
Imports Tamir.SharpSsh

Public Class wb_Functions

    '---------------------------------------------------------
    '11.05.2016/ V0.9/JW            :Neuanlage
    'Bearbeitet von                 :Will
    '
    'Änderungen:
    '---------------------------------------------------------
    'Beschreibung:
    'Erzeugt einen String aus Key-Down-Ereignissen
    'alle gültigen Zeichen werden an den String angehängt,
    'ungültige und Steuerzeichen werden mit False zurück-
    'gegeben (KeyDown-Handler = False)
    '---------------------------------------------------------
    Public Shared Function KeyToString(KeyCode As Char, ByRef s As String) As Boolean
        Select Case Convert.ToUInt16(KeyCode)
                'normale Buchstaben
            Case 32, 33, 35 To 43, 45, 47, 64 To 93, 97 To 122, 129 To 154, 192 To 223, 228, 246, 252
                s = s + KeyCode.ToString
                Return True
                'Ziffern 0 bis 9
            Case 48 To 57
                s = s + KeyCode.ToString
                Return True
                'Backspace (Gibt True zurück wenn ein Zeichen gelöscht wurde)
            Case 8
                If s.Length > 0 Then
                    s = s.Remove(s.Length - 1)
                    Return True
                Else
                    Return False
                End If

                'alle anderen Zeichen sind nicht zulässig
            Case Else
                Return False
        End Select
    End Function

    Public Shared Function IntToKomponType(KO_Type As Integer) As wb_Global.KomponTypen
        Select Case KO_Type
            Case -1
                Return wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
            Case -2
                Return wb_Global.KomponTypen.KO_ZEILE_CHARGE
            Case 0
                Return wb_Global.KomponTypen.KO_TYPE_ARTIKEL

            Case 101
                Return wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE
            Case 102
                Return wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE
            Case 103
                Return wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
            Case 104
                Return wb_Global.KomponTypen.KO_TYPE_EISKOMPONENTE
            Case 105
                Return wb_Global.KomponTypen.KO_TYPE_STUECK
            Case 106
                Return wb_Global.KomponTypen.KO_TYPE_METER

            Case 111
                Return wb_Global.KomponTypen.KO_TYPE_TEMPERATURERFASSUNG
            Case 118
                Return wb_Global.KomponTypen.KO_TYPE_KNETER
            Case 119
                Return wb_Global.KomponTypen.KO_TYPE_TEIGZETTEL
            Case 128
                Return wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT

            Case 121
                Return wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
            Case 122
                Return wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
            Case 123
                Return wb_Global.KomponTypen.KO_TYPE_KESSEL

            Case 1
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_MEHL
            Case 3
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER
            Case 4
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_TEMP
            Case 10
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_DIGITAL
            Case 11
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_ANALOG
            Case 16
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_WARTEN
            Case 17
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_RUEHREN
            Case 19
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE
            Case 20
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_STATUS
            Case 21
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_TEXT
            Case 22
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE
            Case 30
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_REZEPT_START
            Case 31
                Return wb_Global.KomponTypen.KO_TYPE_SAUER_REPEAT

            Case Else
                Return wb_Global.KomponTypen.KO_TYPE_UNDEFINED
        End Select

    End Function

    Public Shared Function FormatStr(value As String, VorKomma As Integer, NachKomma As Integer, Optional ByVal Culture As String = Nothing) As String
        Dim wert As Double
        Try
            If value IsNot "" Then
                ' Für Datenbank-Felder muss unabhängig von der Ländereinstellung die Umwandlung mit
                ' der Einstellung de-DE erfolgen
                If Culture IsNot Nothing Then
                    wert = Convert.ToDouble(value, New System.Globalization.CultureInfo(Culture))
                Else
                    wert = Convert.ToDouble(value)
                End If
            Else
                Return "-"
                Exit Function
            End If

            If NachKomma <> 0 Then
                Return Right(Space(VorKomma) & CDbl(wert).ToString("F" & NachKomma.ToString), VorKomma + NachKomma + 1)
            Else
                Return Right(Space(VorKomma) & CDbl(wert).ToString("F" & NachKomma.ToString), VorKomma)
            End If
        Catch
            Return "-"
        End Try
    End Function

    ''' <summary>
    '''Text aus Datenbank lesen - Übersetzung
    ''' von Herbert Bsteh aus winback (Kylix)
    ''' Erste Zahl (Texttyp), zweite Zahl (Textindex)
    '''
    ''' Gibt den Text ohne Klammer zurück wenn
    ''' kein Text in der Datenbank gefunden wurde
    ''' </summary>
    ''' <param name="Text">String im Format @[Typ,Index]</param>
    ''' <returns>String - Übersetzung aus winback.Texte</returns>
    Public Shared Function TextFilter(Text As String) As String
        Dim Hash As String

        If Len(Text) > 6 Then
            If Left(Text, 2) = "@[" Then
                Hash = Left(Text, InStr(Text, "]"))
                Try
                    Return wb_Konfig.TexteTabelle(Hash).ToString
                Catch
                    Return Mid(Text, Len(Hash) + 1)
                End Try
            Else
                Return Text
            End If
        Else
            Return Text
        End If
    End Function

    Public Shared Sub DoBatch(Directory As String, BatchFile As String, Argument As String, WaitUntilReady As Boolean)
        Dim cmd As String = Chr(34) + My.Settings.MySQLBatch + "\" + BatchFile + Chr(34)
        Dim arg As String = Chr(34) + Directory + Chr(34) + " " + Chr(34) + Argument + Chr(34)

        Dim p As New Process()
        p.StartInfo = New ProcessStartInfo(cmd, arg)
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.WorkingDirectory = Directory
        p.Start()
        p.WaitForExit()

    End Sub

    Public Shared Function DoShell(User As String, Pass As String, Host As String, Command As String) As String
        Dim Output As String
        Dim Exec As New SshExec(Host, User, Pass)

        Exec.Connect()
        Output = Exec.RunCommand(Command)
        Exec.Close()

        Return Output
    End Function

    ''' <summary>
    ''' Datei komprimieren in .bz2
    ''' Der File-Typ ist beliebig. Das Zielverzeichniss muss Schreib-Rechte haben. Nach erfolgreicher Operation wird
    ''' True zurückgeliefert.
    ''' </summary>
    ''' <remarks>
    ''' SharpZipLibrary samples
    '''  Copyright (c) 2007, AlphaSierraPapa
    '''  All rights reserved.
    ''' 
    ''' ' Redistribution and use in source and binary forms, with or without modification, are
    '''  permitted provided that the following conditions are met:
    ''' 
    '''  - Redistributions of source code must retain the above copyright notice, this list
    '''    of conditions and the following disclaimer.
    ''' 
    '''  - Redistributions in binary form must reproduce the above copyright notice, this list
    '''    of conditions and the following disclaimer in the documentation and/or other materials
    '''    provided with the distribution.
    ''' 
    '''  - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
    '''    endorse or promote products derived from this software without specific prior written
    '''    permission.
    ''' 
    '''  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS AS IS AND ANY EXPRESS
    ''' OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
    ''' AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
    ''' CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    ''' DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
    ''' DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
    ''' IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
    ''' OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    ''' </remarks>
    ''' <param name="InFileName"> String Dateiname und Pfad inlusive Extension</param>
    ''' <param name="OutFileName"> String Dateiname und Pfad inlusive Extension (.bz2)</param>
    ''' <returns>
    ''' True - Komprimieren war erfolgreich
    ''' False - Fehler beim Lesen/Schreiben
    ''' </returns>
    Public Shared Function bz2CompressFile(InFileName As String, OutFileName As String) As Boolean
        'Compression of single-file archive
        Dim fsInputFile As FileStream, fsBZ2Archive As FileStream
        Try
            fsInputFile = File.OpenRead(InFileName)
            fsBZ2Archive = File.Create(OutFileName)
            BZip2.Compress(fsInputFile, fsBZ2Archive, True, 4026)
            fsInputFile.Close()
        Catch
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Datei dekomprimieren aus .bz2
    ''' Der File-Typ ist beliebig. Das Zielverzeichniss muss Schreib-Rechte haben. Nach erfolgreicher Operation wird
    ''' True zurückgeliefert.
    ''' </summary>
    ''' <remarks>
    ''' SharpZipLibrary samples
    '''  Copyright (c) 2007, AlphaSierraPapa
    '''  All rights reserved.
    ''' 
    ''' ' Redistribution and use in source and binary forms, with or without modification, are
    '''  permitted provided that the following conditions are met:
    ''' 
    '''  - Redistributions of source code must retain the above copyright notice, this list
    '''    of conditions and the following disclaimer.
    ''' 
    '''  - Redistributions in binary form must reproduce the above copyright notice, this list
    '''    of conditions and the following disclaimer in the documentation and/or other materials
    '''    provided with the distribution.
    ''' 
    '''  - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
    '''    endorse or promote products derived from this software without specific prior written
    '''    permission.
    ''' 
    '''  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS AS IS AND ANY EXPRESS
    ''' OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
    ''' AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
    ''' CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    ''' DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
    ''' DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
    ''' IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
    ''' OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    ''' </remarks>
    ''' <param name="InFileName"> String Dateiname und Pfad inlusive Extension (.bz2)</param>
    ''' <param name="OutFileName"> String Dateiname und Pfad inlusive Extension</param>
    ''' <returns>
    ''' True - Dekomprimieren war erfolgreich
    ''' False - Fehler beim Lesen/Schreiben/Dekomprimieren
    ''' </returns>
    Public Shared Function bz2DecompressFile(InFileName As String, OutFileName As String) As Boolean
        Dim fsBZ2Archive As FileStream, fsOutput As FileStream
        Try
            fsBZ2Archive = File.OpenRead(InFileName)
            fsOutput = File.Create(OutFileName)
            BZip2.Decompress(fsBZ2Archive, fsOutput, True)
            fsBZ2Archive.Close()
            fsOutput.Close()
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Shared Function wb_sql_datensicherung(Filename As String) As Boolean
        Dim SaveFileExtension As String
        Dim DumpFileName As String

        'Cursor umschalten
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        SaveFileExtension = IO.Path.GetExtension(Filename)

        'Datensicherung soll anschliessend komprimiert werden
        If SaveFileExtension = ".bz2" Then
            DumpFileName = IO.Path.GetDirectoryName(Filename) + "\" + IO.Path.GetFileNameWithoutExtension(Filename) + ".sql"
            'Datensicherung starten
            Trace.WriteLine("Start Datensicherung WinBack -> " + DumpFileName)
            wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Dump.bat", DumpFileName, True)
            Trace.WriteLine("Ende  Datensicherung WinBack -> " + DumpFileName)
            'File komprimieren
            Trace.WriteLine("Datei komprimieren   WinBack -> " + Filename)
            wb_Functions.bz2CompressFile(DumpFileName, Filename)
            'ursprüngliche Datensicherung löschen
            My.Computer.FileSystem.DeleteFile(DumpFileName)
        Else
            'Datensicherung starten
            Trace.WriteLine("Start Datensicherung WinBack -> " + Filename)
            wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Dump.bat", Filename, True)
            Trace.WriteLine("Ende  Datensicherung WinBack -> " + Filename)
        End If

        'Cursor wieder zurücksetzen
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default

        Return True
    End Function

    Public Shared Function wb_sql_datenruecksicherung(FileName As String) As Boolean
        Dim OpenFileExtension As String
        Dim DumpFileName As String

        'Cursor umschalten
        Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor
        OpenFileExtension = IO.Path.GetExtension(FileName)

        'Datenrücksicherung muss vorher dekomprimiert werden
        If OpenFileExtension = ".bz2" Then
            DumpFileName = IO.Path.GetDirectoryName(FileName) + "\" + IO.Path.GetFileNameWithoutExtension(FileName) + ".sql"

            'File Dekomprimieren
            Trace.WriteLine("Datei dekomprimieren WinBack -> " + FileName)
            wb_Functions.bz2DecompressFile(FileName, DumpFileName)
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
            Trace.WriteLine("Datensicherung bearbeiten (MySql 3.x.xx) " + FileName)
            PrepareSQLFile(FileName, "winback")
            'Datenrücksicherung starten
            Trace.WriteLine("Start Datenrücksicherung WinBack -> " + FileName)
            wb_Functions.DoBatch(My.Settings.MySQLPath, "MySQL_Restore.bat", FileName, True)
            Trace.WriteLine("Ende  Datenrücksicherung WinBack -> " + FileName)
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
    Private Shared Sub PrepareSQLFile(FileName As String, DataBase As String)
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
