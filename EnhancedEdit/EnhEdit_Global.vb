Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class EnhEdit_Global
    Const KOMMA = 2
    Const NACHKOMMA = 1

    Const HH = 0
    Const MM = 1
    Const SEK = 2
    Const MAXMINSEK = 59

    Const MAPVK_VK_TO_VSC = 0
    Private Shared keyblayoutID As Integer

    ''' <summary>
    ''' Format-Information aus winback.Format
    ''' FM_Index	FM_Bezeichnung
    '''    1            String
    '''    2            Integer
    '''    3            Real
    '''    4            Time
    '''    5            Boolean
    ''' </summary>
    Public Enum wb_Format
        FUndefined = 0
        fString = 1
        fInteger = 2
        fReal = 3
        fTime = 4
        fBoolean = 5
        fAllergen = 6
        fYesNo = 7
    End Enum

    Public Enum wb_Result
        ValueOK
        ValueErrMin
        ValueErrMax
        ValueErrFormat

        KeyReturn
        KeyEscape
        KeyArrowUp
        KeyArrowDown

        Undefined
    End Enum

    ''' <summary>
    ''' Ermittelt beim Start der Routine (Shared) einmalig das Keyboard-Layout
    ''' Routine aus: http://www.vbforums.com/showthread.php?632922-vb2008-convert-Oem-keys-in-REAL-keyboard-value
    ''' 
    ''' </summary>
    Shared Sub New()
        keyblayoutID = GetKeyboardLayout(0)
    End Sub

    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
    Public Shared Function GetKeyboardLayout(ByVal dwLayout As Integer) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function MapVirtualKeyEx(ByVal uCode As Integer, ByVal nMapType As Integer, ByVal dwhkl As Integer) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function ToUnicodeEx(ByVal wVirtKey As Integer, ByVal wScanCode As Integer, ByVal lpKeyState As Byte(), ByVal pwszBuff As String, ByVal cchBuff As Integer, ByVal wFlags As Integer, ByVal dwhkl As Integer) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function ToAsciiEx(ByVal uVirtKey As Integer, ByVal uScanCode As Integer, ByVal lpKeyState As Byte(), ByVal lpChar As String, ByVal wFlags As Integer, ByVal dwhkl As Integer) As Integer
    End Function

    ''' <summary>
    ''' Auswertung der Key-Codes. Abhängig vom Format wird der entsprechende Wert in Value eingetragen
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="Value"></param>
    ''' <param name="Format"></param>
    ''' <param name="ug"></param>
    ''' <param name="og"></param>
    ''' <returns></returns>
    Public Shared Function GetKey(e As KeyEventArgs, ByRef Value As String, Format As wb_Format, ug As Double, og As Double) As wb_Result
        Dim oValue As String = Value
        Debug.Print("EnhEdit_Global.GetKey " & e.KeyCode & "/" & e.KeyData)

        Select Case e.KeyCode

            'Backspace/Delete
            Case Keys.Back, Keys.Delete
                If Value <> "" Then
                    Value = Left(Value, Len(Value) - 1)
                End If

            'Escape (Edit abbrechen)
            Case Keys.Escape, Keys.F1
                Return wb_Result.KeyEscape

            'Return/Enter
            Case Keys.Enter, Keys.Return
                Return wb_Result.KeyReturn

            Case Else
                'alle anderen Zeichen (abhängig vom Format)
                Select Case Format

                    'Numerische Eingabe
                    Case wb_Format.fReal, wb_Format.fInteger, wb_Format.fTime
                        Select Case e.KeyCode
                            'Numerische Eingabe
                            Case Keys.D0 To Keys.D9
                                Value = AddStr(Value, e)
                                Return CheckBounds(Value, oValue, Format, ug, og)
                            'Num-Pad
                            Case Keys.NumPad0 To Keys.NumPad9
                                Value = AddStr(Value, e, True)
                                Return CheckBounds(Value, oValue, Format, ug, og)
                            'Dezimal-Trennzeichen (Komma/Punkt)
                            Case Keys.Decimal, Keys.Oemcomma, Keys.OemPeriod
                                If Format = wb_Format.fReal Then
                                    Value = Value & Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                                End If
                        End Select

                    'Allergene
                    Case wb_Format.fAllergen
                        Select Case e.KeyCode
                            Case Keys.C, Keys.K, Keys.N, Keys.T, Keys.X
                                Value = AddStr("", e)
                        End Select

                    'Ernährung
                    Case wb_Format.fYesNo
                        Select Case e.KeyCode
                            Case Keys.Y, Keys.N, Keys.X
                                Value = AddStr("", e)
                        End Select

                    'Strings
                    Case wb_Format.fString
                        Select Case e.KeyCode
                            'Umlaute und Sonderzeichen
                            Case 186, 192, 222
                                Value = AddStr(Value, e)
                            'Alpha-Numerische Eingabe
                            Case Keys.A To Keys.Z, Keys.Space
                                Value = AddStr(Value, e)
                            'Zahlen NumBlock
                            Case Keys.NumPad0 To Keys.NumPad9
                                Value = AddStr(Value, e, True)
                            'Zahlen 
                            Case Keys.D0 To Keys.D9, Keys.Decimal, Keys.Oemcomma, Keys.OemPeriod
                                Value = AddStr(Value, e)
                        End Select
                End Select
        End Select

        Return wb_Result.Undefined
    End Function

    Private Shared Function AddStr(Value As String, e As KeyEventArgs, Optional NumPad As Boolean = False) As String
        Dim ckey = GetAscci(e)
        'Dim ckey = e.KeyCode
        If NumPad Then
            Value = Value & ckey
        Else
            If e.Shift Then
                Value = Value & ckey.ToUpper
            Else
                Value = Value & ckey.ToLower
            End If
        End If
        Return Value
    End Function

    ''' <summary>
    ''' Ermittelt den ASCII-Code zum Key-Code der Tastatur. Abhängig vom Layout der Tastatur.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    Public Shared Function GetAscci(e As KeyEventArgs) As String
        Dim ScanCode As Integer = MapVirtualKeyEx(e.KeyCode, MAPVK_VK_TO_VSC, keyblayoutID)
        Dim keystate(255) As Byte
        Dim buff As String = New String(ControlChars.NullChar, 256)
        Dim bufflen As Integer = buff.Length
        Dim ret As Integer = ToAsciiEx(e.KeyCode, ScanCode, keystate, buff, 0, keyblayoutID)

        Select Case ret
            Case -1 ' diactric
            Case 0 ' no translation
            Case Else ' How many characters are written into buffer
                buff = buff.Substring(0, ret)
                'Debug.Print("GetAscci " & buff)
        End Select
        Return buff
    End Function

    Private Shared Function CheckBounds(ByRef Value As String, oValue As String, Format As wb_Format, ug As Double, og As Double) As wb_Result

        'Abhängig vom Eingabeformat
        Select Case Format

            Case wb_Format.fReal
                Try
                    'Eingabe auf maximale Länge (3 Nachkommastellen) prüfen
                    Dim DezimalZahl As String() = Value.Split(Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                    'Eingabewert enthält ein Dezimaltrennzeichen
                    If DezimalZahl.Length = KOMMA Then
                        'mehr als 3 Nachkomma-Stellen sind nicht erlaubt
                        If DezimalZahl(NACHKOMMA).Length > 3 Then
                            Value = oValue
                            Return wb_Result.ValueErrFormat
                        End If
                    End If

                    'Grenzen prüfen
                    Select Case Convert.ToDouble(Value)
                        Case < ug
                            Value = oValue
                            Return wb_Result.ValueErrMin
                        Case > og
                            Value = oValue
                            Return wb_Result.ValueErrMax
                        Case Else
                            Return wb_Result.ValueOK
                    End Select
                Catch
                    Value = oValue
                    Return wb_Result.ValueErrFormat
                End Try

            Case wb_Format.fInteger
                Try
                    'Grenzen prüfen
                    Select Case Convert.ToInt32(Value)
                        Case < ug
                            Value = oValue
                            Return wb_Result.ValueErrMin
                        Case > og
                            Value = oValue
                            Return wb_Result.ValueErrMax
                        Case Else
                            Return wb_Result.ValueOK
                    End Select
                Catch ex As Exception
                    Value = oValue
                    Return wb_Result.ValueErrFormat
                End Try

            Case wb_Format.fTime
                'Eingabewert in einzelne Teilstrings zerlegen
                Dim ZeitWert As String() = Value.Split(Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator)

                'Länge prüfen
                Select Case Value.Length
                    Case 1, 4, 7
                        Return wb_Result.ValueOK
                    Case 2
                        If (ZeitWert(HH) > og) And (og > 0) Then
                            Value = oValue
                            Return wb_Result.ValueErrMax
                        Else
                            Value = Value & Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator
                            Return wb_Result.ValueOK
                        End If
                    Case 5
                        If ZeitWert(MM) > MAXMINSEK Then
                            Value = oValue
                            Return wb_Result.ValueErrMax
                        Else
                            Value = Value & Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator
                            Return wb_Result.ValueOK
                        End If
                    Case 8
                        If ZeitWert(SEK) > MAXMINSEK Then
                            Value = oValue
                            Return wb_Result.ValueErrMax
                        Else
                            Return wb_Result.ValueOK
                        End If

                    Case Else
                        Value = oValue
                        Return wb_Result.ValueErrFormat
                End Select

            Case Else
                Return wb_Result.Undefined

        End Select
        Return wb_Result.Undefined
    End Function


End Class
