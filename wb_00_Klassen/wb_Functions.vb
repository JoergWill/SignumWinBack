Imports System.Globalization
Imports System.IO
Imports ICSharpCode.SharpZipLib.BZip2
Imports Tamir.SharpSsh

''' <summary>
''' Beschreibung:
''' Sammlung von Statischen Funktionen
''' </summary>

Public Class wb_Functions
    ''' <summary>
    ''' Erzeugt einen String aus Key-Down-Ereignissen
    ''' alle gültigen Zeichen werden an den String angehängt,
    ''' ungültige und Steuerzeichen werden mit False zurück-
    ''' gegeben (KeyDown-Handler = False)
    ''' </summary>
    ''' <param name="KeyCode"> - Char KeyCode der gedrückten Taste</param>
    ''' <param name="s">- String alle gültigen Zeichen aus KeyCode</param>
    ''' <returns>False wenn Steuerzeichen erkannt werden</returns>
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

    ''' <summary>
    ''' Wandelt einen String im WinBack-Cloud-Datumsformat (yyyymmddHHmmss) in DateTime um.
    ''' Wenn die Konvertierung feherhaft ist, wird der 22.11.1964 00:00:00 zurückgegeben.
    ''' </summary>
    ''' <param name="JSONTimeString"></param>
    ''' <returns></returns>
    Public Shared Function ConvertJSONTimeStringToDateTime(JSONTimeString As String) As DateTime
        Try
            Dim dt As String = JSONTimeString.Substring(0, 4) & JSONTimeString.Substring(6, 2) & JSONTimeString.Substring(4, 2)
            dt = dt & JSONTimeString.Substring(8, 6)
            Return DateTime.ParseExact(dt, "yyyyddMMHHmmss", Nothing)
        Catch ex As Exception
            Return #11/22/1964 00:00:00#
        End Try
    End Function

    ''' <summary>
    ''' Wandelt die Datum/Uhrzeit-Angabe aus DataLink (Created 2013-09-03T08:14:03)
    ''' in DateTime um
    ''' </summary>
    ''' <param name="DataLinkTimeString"></param>
    ''' <returns></returns>
    Public Shared Function ConvertDataLinkTimeStringToDateTime(DataLinkTimeString As String) As DateTime
        Try
            Dim dt As String = DataLinkTimeString.Substring(0, 10) & DataLinkTimeString.Substring(11, 8)
            Return DateTime.ParseExact(dt, "yyyy-dd-MMHH:mm:ss", Nothing)
        Catch ex As Exception
            Return #11/22/1964 00:00:00#
        End Try
    End Function

    ''' <summary>
    ''' Wandelt einen String in AllergenInfo um. Wenn der String umgültig ist wird ERR zurückgegeben
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function StringtoAllergen(s As String) As wb_Global.AllergenInfo
        Select Case s.ToUpper
            Case "C"
                Return wb_Global.AllergenInfo.C
            Case "K"
                Return wb_Global.AllergenInfo.K
            Case "N"
                Return wb_Global.AllergenInfo.N
            Case "T"
                Return wb_Global.AllergenInfo.T
            Case "X", ""
                Return wb_Global.AllergenInfo.X
            Case Else
                Return wb_Global.AllergenInfo.ERR
        End Select
    End Function

    ''' <summary>
    ''' Wandelt die AllergenInfo in einen String um.
    ''' </summary>
    ''' <param name="a"></param>
    ''' <returns></returns>
    Public Shared Function AllergenToString(a As wb_Global.AllergenInfo) As String
        Select Case a
            Case wb_Global.AllergenInfo.C
                Return "C"
            Case wb_Global.AllergenInfo.K
                Return "K"
            Case wb_Global.AllergenInfo.T
                Return "T"
            Case wb_Global.AllergenInfo.N
                Return "N"
            Case Else
                Return "ERR"
        End Select
    End Function

    ''' <summary>
    ''' Wandelt LogType in String
    ''' </summary>
    ''' <param name="LogType"></param>
    ''' <returns></returns>
    Public Shared Function LogTypeToString(LogType As wb_Global.LogType) As String
        Select Case LogType
            Case wb_Global.LogType.X     'Unbestimmt
                Return "XXX"
            Case wb_Global.LogType.Stm   'Stammdaten
                Return "Stm"
            Case wb_Global.LogType.Prm   'Parameter
                Return "Prm"
            Case wb_Global.LogType.Alg   'Allergen
                Return "Alg"
            Case wb_Global.LogType.Nrw   'Nährwert
                Return "Nrw"
            Case Else
                Return "xxx"
        End Select
    End Function

    ''' <summary>
    ''' Wandelt einen Integer-Wert in einen Komponenten-Typ um. Wenn der Integer-Wert ungültig ist,
    ''' wird KO_TYPE_UNDEFINED zurückgegeben
    ''' </summary>
    ''' <param name="KO_Type"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Wandelt einen String in kt301Gruppen um. Beim Einlesen der Hash-Table aus der Tabelle KomponTypen
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Shared Function StringTokt301Gruppe(s As String) As wb_Global.ktTyp301Gruppen
        Select Case s
            Case "Big4"
                Return wb_Global.ktTyp301Gruppen.Big4
            Case "Big8"
                Return wb_Global.ktTyp301Gruppen.Big8
            Case "Vitamine"
                Return wb_Global.ktTyp301Gruppen.Vitamine
            Case "Kohlenhydratzusammen"
                Return wb_Global.ktTyp301Gruppen.Kohlenhydrate
            Case "Mineralstoffe"
                Return wb_Global.ktTyp301Gruppen.Mineralstoffe
            Case "Spurenelemente"
                Return wb_Global.ktTyp301Gruppen.SpurenElemente
            Case "Allergene"
                Return wb_Global.ktTyp301Gruppen.Allergene
            Case "Gluten"
                Return wb_Global.ktTyp301Gruppen.Gluten
            Case "Schalenfrüchte"
                Return wb_Global.ktTyp301Gruppen.Schalenfrüchte
            Case "Gesamtkennzahlen"
                Return wb_Global.ktTyp301Gruppen.Gesamtkennzahlen
            Case Else
                Return wb_Global.ktTyp301Gruppen.xxx
        End Select
    End Function

    ''' <summary>
    ''' Wandelt die DatenLink-Nährwert und Allergen-Bezeichnungen in 
    ''' WinBack-Index-Nummern um
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns>index (Integer)</returns>
    Public Shared Function DatenLinkToIndex(name As String) As Integer
        Select Case name

            'Nährwerte
            Case "GCAL"
                Return 1
            Case "GJ"
                Return 2
            Case "ZE"
                Return 3
            Case "ZK"
                Return 4
            Case "ZF"
                Return 5
            Case "KD"
                Return 11
            Case "FS"
                Return 12
            Case "ZB"
                Return 13
            Case "GMKO"
                Return 202

            'Allergene
            Case "GLUTEN"
                Return 141
            Case "GLUTEN_WHEAT"
                Return 170
            Case "GLUTEN_RYE"
                Return 171
            Case "GLUTEN_BARLEY"
                Return 172
            Case "GLUTEN_SPELT"
                Return 173
            Case "GLUTEN_KAMUT"
                Return 174
            Case "GLUTEN_OAT"
                Return 175
            Case "CRUSTACEANS"
                Return 142
            Case "EGGS"
                Return 143
            Case "SEAFOOD"
                Return 144
            Case "PEANUTS"
                Return 145
            Case "SOY"
                Return 146
            Case "MILK"
                Return 147
            Case "EDIBLE_NUTS"
                Return 148
            Case "EDIBLE_NUTS_ALMONDS"
                Return 180
            Case "EDIBLE_NUTS_HAZELNUTS"
                Return 181
            Case "EDIBLE_NUTS_WALNUTS"
                Return 182
            Case "EDIBLE_NUTS_CASHEW"
                Return 183
            Case "EDIBLE_NUTS_PECANNUTS"
                Return 184
            Case "EDIBLE_NUTS_BRASIL_NUTS"
                Return 185
            Case "EDIBLE_NUTS_PISTACIOS"
                Return 186
            Case "EDIBLE_NUTS_MACADAMIA_NUTS"
                Return 187
            Case "CELERY"
                Return 149
            Case "MUSTARD"
                Return 150
            Case "SESAME"
                Return 151
            Case "SULFOR_DIOXIDE_SULFITE"
                Return 152
            Case "LUPINES"
                Return 153
            Case "MOLLUSCS"
                Return 154

            Case Else
                Return -1
        End Select
    End Function

    ''' <summary>
    ''' Formatiert einen String mit der angegebenen Vorkomma und Nachkomma-Stelle
    ''' </summary>
    ''' <param name="value">Zahlenwert als String</param>
    ''' <param name="VorKomma">Anzahl der Vorkomma-Stellen</param>
    ''' <param name="NachKomma">Anzahl der Nachkomma-Stellen</param>
    ''' <param name="Culture">Ländereinstellung (Default de-DE)</param>
    ''' <returns></returns>
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
    ''' Wandelt einen String sicher in Float um. Das Zahlenformat kann US/DE sein. Punkte werden vor der Konvertierung in Koma umgewandelt.
    ''' 1000er - Trennzeichen sind nicht erlaubt
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns>Kovertierten String im Format Double</returns>
    Public Shared Function StrToDouble(value As String) As Double
        Dim d As Double
        Try
            value = value.Replace(".", ",")
            Double.TryParse(value, NumberStyles.Number, CultureInfo.CreateSpecificCulture("de-DE"), d)
            Return d
        Catch ex As Exception
            Return 0.0F
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
                    If wb_Konfig.TexteTabelle.ContainsKey(Hash) Then
                        Return wb_Konfig.TexteTabelle(Hash).ToString
                    End If
                Catch
                End Try
                Return Mid(Text, Len(Hash) + 1)
            End If
        End If
        Return Text
    End Function

    ''' <summary>
    ''' Für ein Batch-File im Verzeichnis MySQLBatch aus. Über Argument wird %2 an das Batch-File übergeben
    ''' </summary>
    ''' <param name="Directory"></param>
    ''' <param name="BatchFile"></param>
    ''' <param name="Argument"></param>
    ''' <param name="WaitUntilReady"></param>
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

    ''' <summary>
    ''' Startet eine ssh-Sitzung und führt ein Kommando auf dem Linux-Rechner mit der angegebenen IP-Adresse aus
    ''' </summary>
    ''' <param name="User">String - Username</param>
    ''' <param name="Pass">String - Passwort</param>
    ''' <param name="Host">String - IP-Adresse</param>
    ''' <param name="Command">String - Shell-Kommando</param>
    ''' <returns>Ausgabe der Command-Shell</returns>
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

End Class
