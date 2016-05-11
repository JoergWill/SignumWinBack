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
            Case 32, 33, 35 To 43, 45, 47, 64 To 93, 97 To 122, 192 To 223
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

End Class
