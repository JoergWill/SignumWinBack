'---------------------------------------------------------
'11.05.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Sammlung von Statischen Funktionen

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

    '-----------------------------------------------------------------------------
    ' Text aus Datenbank lesen - Übersetzung
    ' von Herbert Bsteh aus winback (Kylix)
    ' Erste Zahl (Texttyp), zweite Zahl (Textindex)

    ' Gibt den Text ohne Klammer zurück wenn
    ' kein Text in der Datenbak gefunden wurde
    '-----------------------------------------------------------------------------
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
End Class
