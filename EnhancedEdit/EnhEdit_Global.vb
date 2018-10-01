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
        Dim dValue As Double
        Select Case cKey

            'Numerische Eingabe
            Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9
                If Format = wb_Format.fInteger Or Format = wb_Format.fReal Or Format = wb_Format.fString Then
                    Value = Value & Chr(cKey)

                    'In Zahl umwandeln
                    Try
                        dValue = Convert.ToDouble(Value)
                    Catch
                        Return wb_Result.ValueErrFormat
                    End Try

                    'Grenzen prüfen
                    Select Case dValue
                        Case < ug
                            Return wb_Result.ValueErrMin
                        Case > og
                            Return wb_Result.ValueErrMin
                        Case Else
                            Return wb_Result.ValueOK
                    End Select

                    If Int(Value) < ug Then
                        Return wb_Result.ValueErrMin
                    End If
                End If

            'Dezimal-Trennzeichen (Komma/Punkt)
            Case Keys.Decimal
                If Format = wb_Format.fReal Then
                    Value = Value & Chr(cKey)
                End If

            'Alpha-Numerische Eingabe
            Case Keys.A To Keys.Z
                If Format = wb_Format.fString Then
                    Value = Value & Chr(cKey)
                End If

           'Return/Enter
            Case Keys.Enter, Keys.Return
                Return wb_Result.KeyReturn

        End Select


        Return wb_Result.Undefined
    End Function

End Class
