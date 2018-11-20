Imports System.Windows.Forms

Public Class EnhEdit_Global
    Const KOMMA = 2
    Const NACHKOMMA = 1

    Const HH = 0
    Const MM = 1
    Const SEK = 2
    Const MAXMINSEK = 59

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

            'Numerische Eingabe
            Case Keys.D0 To Keys.D9
                Value = AddStr(Value, e)
                Return CheckBounds(Value, oValue, Format, ug, og)

            Case Keys.NumPad0 To Keys.NumPad9
                Value = AddStr("", e, True)
                Return CheckBounds(Value, oValue, Format, ug, og)

            'Dezimal-Trennzeichen (Komma/Punkt)
            Case Keys.Decimal, Keys.Oemcomma, Keys.OemPeriod
                If Format = wb_Format.fReal Then
                    Value = Value & Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                End If

            'Alpha-Numerisch (Allergene)
            Case Keys.C, Keys.K, Keys.N, Keys.T, Keys.X
                If Format = wb_Format.fAllergen Then
                    Value = AddStr("", e)
                End If
                If Format = wb_Format.fString Then
                    Value = AddStr(Value, e)
                End If

            'Alpha-Numerische Eingabe
            Case Keys.A To Keys.Z, Keys.Space
                If Format = wb_Format.fString Then
                    Value = AddStr(Value, e)
                End If

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

        End Select

        Return wb_Result.Undefined
    End Function

    Private Shared Function AddStr(Value As String, e As KeyEventArgs, Optional NumPad As Boolean = False) As String
        Dim ckey = e.KeyCode
        If Not e.Shift Then
            Value = Value & Chr(ckey).ToString.ToLower
        ElseIf NumPad Then
            Value = Value & Chr(ckey - 48)
        Else
            Value = Value & Chr(ckey)
        End If
        Return Value
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
