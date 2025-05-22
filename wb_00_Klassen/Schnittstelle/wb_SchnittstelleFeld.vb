Imports MySql.Data.MySqlClient

Public Class wb_SchnittstelleFeld
    Implements IComparable

    Private _Tabelle As String = ""
    Private _Name As String
    Private _Value As String = ""
    Private _DefaultValue As String = ""
    Private _Fnc As Char = ""
    Private _Idx As Integer = wb_Global.UNDEFINED
    Private _DBIdx As Integer = wb_Global.UNDEFINED
    Private _Pos As Integer = wb_Global.UNDEFINED
    Private _Len As Integer = wb_Global.UNDEFINED
    Private _Calc As String = ""
    Private _ExportOption As String = ""

    Const cName = 0
    Const cIndex = 1
    Const cDefaultValue = 2
    Const cDBIndex = 2
    Const cPos = 3
    Const cLen = 4
    Const cCalc = 5
    Const COption = 5

    Public ReadOnly Property Tabelle As String
        Get
            Return _Tabelle
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Public Property Value As String
        Get
            If _Value = "" Then
                Return DefaultValue
            Else
                Return Convert(_Value)
            End If
        End Get
        Set(value As String)
            _Value = value
        End Set
    End Property
    Private WriteOnly Property DBValue As Object
        Set(value As Object)
            If value IsNot DBNull.Value Then
                Me.Value = value.ToString
            End If
        End Set
    End Property

    Public Property DefaultValue As String
        Get
            Return _DefaultValue
        End Get
        Set(value As String)
            _DefaultValue = value
        End Set
    End Property

    Public Property Idx As Integer
        Get
            Return _Idx
        End Get
        Set(value As Integer)
            _Idx = value
        End Set
    End Property

    Public Property DBIdx As Integer
        Get
            Return _DBIdx
        End Get
        Set(value As Integer)
            _DBIdx = value
        End Set
    End Property

    Public Property Calc As String
        Get
            Return _Calc
        End Get
        Set(value As String)
            _Calc = value
        End Set
    End Property

    Public Property ExportOption As String
        Get
            Return _ExportOption
        End Get
        Set(value As String)
            _ExportOption = value
        End Set
    End Property

    Public Property Len As Integer
        Get
            Return _Len
        End Get
        Set(value As Integer)
            _Len = value
        End Set
    End Property

    Public Property Pos As Integer
        Get
            Return _Pos
        End Get
        Set(value As Integer)
            _Pos = value
        End Set
    End Property

    Public Property Fnc As Char
        Get
            Return _Fnc
        End Get
        Set(value As Char)
            _Fnc = value
        End Set
    End Property

    ''' <summary>
    ''' Setze den Wert dieses Feldes für den Export (der Index bestimmt die Stelle im Export-String)
    ''' Der Wert wird aus dem übergebenen DataReader über den DBIndex bestimmt
    ''' </summary>
    Public Sub SetDBValue(sqlReader As MySqlDataReader)
        Try
            If _DBIdx > 0 Then
                'der Datenbank-Index beginnt bei Eins(!)
                DBValue = sqlReader.GetValue(_DBIdx - 1)
            Else
                DBValue = ""
            End If
        Catch ex As Exception
            DBValue = ""
        End Try
    End Sub

    ''' <summary>
    ''' Setze das Datenfeld für den Import
    ''' Der Wert wird aus dem übergebenen String-Datenfeld() über den Index bestimmt
    ''' </summary>
    ''' <param name="X"></param>
    Public Sub SetValue(X As String())
        If ExportOption = "" Then
            If _Idx > wb_Global.UNDEFINED Then
                Value = X(Idx)
            Else
                Value = ""
            End If
        Else
            If _DBIdx > wb_Global.UNDEFINED Then
                Value = X(_DBIdx)
            Else
                Value = ""
            End If
        End If
    End Sub

    ''' <summary>
    ''' Setze das Datenfeld.
    ''' Der Wert wird aus dem übergebenen String von Anfang(Pos) und Länge(Len) extrahiert 
    ''' </summary>
    ''' <param name="x"></param>
    Public Sub SetValue(X As String)
        If Pos > 0 AndAlso Len > 0 Then
            Value = X.Substring(Pos - 1, Len).Trim
        Else
            Value = ""
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return Value
    End Function

    Public Function ToInt() As Integer
        Return wb_Functions.StrToInt(Value)
    End Function

    Public Function toDouble() As Double
        Return wb_Functions.StrToDouble(Value)
    End Function

    ''' <summary>
    ''' Initialisiert ein neues Tabellenfeld
    ''' Alle Tabellenfelder werden in einer Liste in wb_Schnittstelle_shared gehalten.
    ''' </summary>
    ''' <param name="FeldName"></param>
    ''' <param name="IdxDefault"></param>
    Sub New(FeldName As String, Optional IdxDefault As Integer = wb_Global.UNDEFINED, Optional ValueDefault As String = "", Optional CalcDefault As String = "")
        'Default-Werte setzen
        _DefaultValue = ValueDefault
        _Name = FeldName
        _Idx = IdxDefault
        _DBIdx = 0
        _Calc = CalcDefault
        _Tabelle = FeldName.Substring(0, 5)
        _Fnc = ""

        'Das neue Tabellen-Feld in die Liste eintragen
        wb_Schnittstelle_Shared.Fxxxx.Add(Me)
    End Sub

    ''' <summary>
    ''' Initialisierung mit Werten aus der Konfigurations-Tabelle
    ''' </summary>
    ''' <param name="Items"></param>
    Public Sub InitImport(Items As String())
        Fnc = "I"
        Idx = wb_Functions.StrToInt(Items(cIndex))  'Index Feld-Reihenfolge oder Start-Position
        DefaultValue = Items(cDefaultValue)         'Default-Wert
        Pos = wb_Functions.StrToInt(Items(cPos))    'Feld Start-Position
        Len = wb_Functions.StrToInt(Items(cLen))    'Feld Länge
        Calc = Items(cCalc)                         'Berechnungs-Schema

        'Debug-Ausgabe im Log-Fenster
        If wb_Schnittstelle_Shared.DebugMode_Max Then
            Trace.WriteLine("Initialisierung Import-Feld " & Name & " " & Items(cIndex))
        End If
    End Sub

    ''' <summary>
    ''' Initialisierung mit Werten aus der Konfigurations-Tabelle
    ''' </summary>
    ''' <param name="Items"></param>
    Public Sub InitExport(Items As String())
        Fnc = "E"
        Idx = wb_Functions.StrToInt(Items(cIndex))      'Index Feld-Reihenfolge oder Start-Position
        DBIdx = wb_Functions.StrToInt(Items(cDBIndex))  'Index Datenbank-Feld
        Pos = wb_Functions.StrToInt(Items(cPos))        'Feld Start-Position
        Len = wb_Functions.StrToInt(Items(cLen))        'Feld Länge
        ExportOption = Items(COption)                   'Berechnungs-Schema

        'Debug-Ausgabe im Log-Fenster
        If wb_Schnittstelle_Shared.DebugMode_Max Then
            Trace.WriteLine("Initialisierung Export-Feld " & Name & " " & Items(cIndex))
        End If
    End Sub

    ''' <summary>
    ''' Wandelt den übergebenen Import-String abhängig vom Parameter Calc in den entsprechenden neuen Wert um.
    '''     
    '''     STR - Konvertierung String(Sonderzeichen werden gelöscht/ersetzt)
    '''     TXT - Konvertierung String(Sonderzeichen/Zeilenvorschub werden gelöscht/ersetzt)
    '''     
    '''     ADD - Umwandlung in Integer-Wert und Festwert addieren
    '''     
    ''' Die einzelnen Rechenregeln werden von in der Reihenfolge von links nach rechts abgearbeitet
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Function Convert(s As String) As String

        'Aufteilen in die einzelnen Rechen-Regeln, getrennt durch _
        For Each CalcStep In Split(Calc, "_")

            'die einzelnen Parameter der Rechenegeln werden durch / getrennt
            Dim Param As String() = Split(CalcStep, "/")

            Select Case Trim(Param(0))

                Case "STR"
                    'alle Sonderzeichen aus dem String entfernen
                    s = wb_Functions.XRemoveSonderZeichen(s)

                Case "TXT"
                    'alle Sonderzeichen aus dem String entfernen
                    s = wb_Functions.XRemoveSonderZeichen(s)
                    s = wb_Functions.XReplaceSonderZeichen_ImportExport(s)

                Case "ADD", "MAX", "MIN"
                    'Umwandlung in Integer
                    s = ConvertCalcInt(Param, s)

            End Select
        Next

        Return s
    End Function

    Private Function ConvertCalcInt(Params As String(), s As String) As String
        Dim Num As Integer = wb_Functions.StrToInt(s)

        Select Case Params(0)
            Case "ADD"
                Num = Num + wb_Functions.StrToInt(Params(1))
            Case "MIN"
                Num = Math.Min(Num, wb_Functions.StrToInt(Params(1)))
            Case "MAX"
                Num = Math.Max(Num, wb_Functions.StrToInt(Params(1)))
        End Select

        Return Num.ToString
    End Function

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Dim Feld As wb_SchnittstelleFeld
        Feld = CType(obj, wb_SchnittstelleFeld)

        'Sortieren nach Funktion (Import/Export)
        If Feld.Fnc > Fnc Then
            Return -1
        ElseIf Feld.Fnc < Fnc Then
            Return 1
        Else
            'Sortieren nach Tabelle
            If Feld.Tabelle > Tabelle Then
                Return -1
            ElseIf Feld.Tabelle < Tabelle Then
                Return 1
            Else
                'Sortieren nach Index
                If Feld.Idx > Idx Then
                    Return -1
                ElseIf Feld.Idx < Idx Then
                    Return 1
                Else
                    Return 0
                End If
            End If
        End If
    End Function
End Class
