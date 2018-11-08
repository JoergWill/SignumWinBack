Imports System.Windows.Forms

Public Class EnhEdit_Global

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
        fString = 1
        fInteger = 2
        fReal = 3
        fTime = 4
        fBoolean = 5
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
    ''' <param name="cKey"></param>
    ''' <param name="Value"></param>
    ''' <param name="Format"></param>
    ''' <param name="ug"></param>
    ''' <param name="og"></param>
    ''' <returns></returns>
    Public Shared Function GetKey(cKey As Keys, ByRef Value As String, Format As wb_Format, ug As Double, og As Double) As wb_Result
        Dim oValue As String = Value
        Debug.Print("EnhEdit_Global.GetKey " & cKey.ToString)

        Select Case cKey

            'Numerische Eingabe
            Case Keys.D0 To Keys.D9
                Value = Value & Chr(cKey)
                Return CheckBounds(Value, oValue, Format, ug, og)

            Case Keys.NumPad0 To Keys.NumPad9
                Value = Value & Chr(cKey - 48)
                Return CheckBounds(Value, oValue, Format, ug, og)

            'Dezimal-Trennzeichen (Komma/Punkt)
            Case Keys.Decimal, Keys.Oemcomma, Keys.OemPeriod
                If Format = wb_Format.fReal Then
                    Value = Value & Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                End If

            'Alpha-Numerische Eingabe
            Case Keys.A To Keys.Z
                If Format = wb_Format.fString Then
                    Value = Value & Chr(cKey)
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

    Private Shared Function CheckBounds(ByRef Value As String, oValue As String, Format As wb_Format, ug As Double, og As Double) As wb_Result

        'Abhängig vom Eingabeformat
        Select Case Format

            Case wb_Format.fReal
                Try
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

            Case Else
                Return wb_Result.Undefined

        End Select
    End Function

End Class
