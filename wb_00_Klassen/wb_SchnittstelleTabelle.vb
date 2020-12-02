Public Class wb_SchnittstelleTabelle
    Private _TabName As String
    Private _Import As Boolean = True
    Private _Export As Boolean = False
    Private _Enabled As Boolean
    Private _KonfigLocked As Boolean
    Private _FirstRealLine As Integer = 1
    Private _CheckString As String = ""
    Private _StringFelder() As String
    Private _ResultFelder() As String
    Private _TabFelder As New List(Of wb_SchnittstelleFeld)

    Private _TrennzeichenKomma As Boolean
    Private _TrennzeichenSemikolon As Boolean
    Private _TrennzeichenTab As Boolean
    Private _TrennzeichenCRLF As Boolean
    Private _TrennzeichenSonder As Boolean
    Private _Trennzeichen As String

    Public Property TabName As String
        Get
            Return _TabName
        End Get
        Set(value As String)
            _TabName = value
        End Set
    End Property

    Public Property Import As Boolean
        Get
            Return _Import
        End Get
        Set(value As Boolean)
            _Import = value
            _Export = Not value
        End Set
    End Property

    Public Property Export As Boolean
        Get
            Return _Export
        End Get
        Set(value As Boolean)
            _Export = value
            _Import = Not value
        End Set
    End Property

    Public Property KonfigLocked As Boolean
        Get
            Return _KonfigLocked
        End Get
        Set(value As Boolean)
            _KonfigLocked = value
        End Set
    End Property

    Public Property Enabled As Boolean
        Get
            Return _Enabled
        End Get
        Set(value As Boolean)
            _Enabled = value
        End Set
    End Property

    Public Property TabFelder As List(Of wb_SchnittstelleFeld)
        Get
            Return _TabFelder
        End Get
        Set(value As List(Of wb_SchnittstelleFeld))
            _TabFelder = value
        End Set
    End Property

    ''' <summary>
    ''' Erstmalig Initialisierung der TabellenFelder wenn keine Definition vorhanden ist
    ''' Legt x Felder (leer) an
    ''' </summary>
    ''' <param name="x"></param>
    Private Sub TabFelderInit(x As Integer)
        _TabFelder.Clear()

        For i = 1 To x
            Dim TabFeld As New wb_SchnittstelleFeld
            TabFeld.Name = ConvertToLetter(i)
            _TabFelder.Add(TabFeld)
        Next
    End Sub

    Private Function TabFelderConvert() As String()
        'alle (vorhandenen) Felder konvertieren
        Dim MaxFelder As Integer = Math.Min(_StringFelder.Count, TabFelder.Count) - 1
        Dim ResultArray(MaxFelder) As String
        For i = 0 To MaxFelder
            ResultArray(i) = (TabFelder(i).Convert(_StringFelder(i)))
        Next

        'Ergebnis zurückgeben
        Return ResultArray
    End Function

    Public ReadOnly Property StringFelder As String()
        Get
            Return _StringFelder
        End Get
    End Property

    Public Property ResultFelder As String()
        Get
            Return _ResultFelder
        End Get
        Set(value As String())
            _ResultFelder = value
        End Set
    End Property

    Public Sub CheckFormat(v As String)
        'String zum Prüfen der Schnittstellen-Definition
        _CheckString = v
        CheckFormat()
    End Sub

    Public Sub CheckFormat()
        'Anzahl der Felder
        _StringFelder = Split(_CheckString, _Trennzeichen)
        'Ergebnis-Array dimensionieren
        If TabFelder.Count = 0 Or (Not KonfigLocked And (_StringFelder.Count <> TabFelder.Count)) Then
            TabFelderInit(_StringFelder.Count)
        End If
        _ResultFelder = TabFelderConvert()
    End Sub

    Public Property TrennzeichenKomma As Boolean
        Get
            Return _TrennzeichenKomma
        End Get
        Set(value As Boolean)
            _TrennzeichenKomma = value
            If value Then
                _Trennzeichen = ","
            End If
        End Set
    End Property

    Public Property TrennzeichenSemikolon As Boolean
        Get
            Return _TrennzeichenSemikolon
        End Get
        Set(value As Boolean)
            _TrennzeichenSemikolon = value
            If value Then
                _Trennzeichen = ";"
            End If
        End Set
    End Property

    Public Property TrennzeichenTab As Boolean
        Get
            Return _TrennzeichenTab
        End Get
        Set(value As Boolean)
            _TrennzeichenTab = value
            If value Then
                _Trennzeichen = vbTab
            End If
        End Set
    End Property

    Public Property TrennzeichenSpace As Boolean
        Get
            Return _TrennzeichenCRLF
        End Get
        Set(value As Boolean)
            _TrennzeichenCRLF = value
            If value Then
                _Trennzeichen = " "
            End If
        End Set
    End Property

    Public Property TrennzeichenSonder As Boolean
        Get
            Return _TrennzeichenSonder
        End Get
        Set(value As Boolean)
            _TrennzeichenSonder = value
        End Set
    End Property

    Public Property Trennzeichen As String
        Get
            Return _Trennzeichen
        End Get
        Set(value As String)
            _Trennzeichen = value
        End Set
    End Property

    Public Property FirstRealLine As Integer
        Get
            Return _FirstRealLine
        End Get
        Set(value As Integer)
            _FirstRealLine = value
        End Set
    End Property

    Private Function ConvertToLetter(iCol As Integer) As String
        Dim a As Integer
        Dim b As Integer
        a = iCol
        ConvertToLetter = ""
        Do While iCol > 0
            a = Int((iCol - 1) / 26)
            b = (iCol - 1) Mod 26
            ConvertToLetter = Chr(b + 65) & ConvertToLetter
            iCol = a
        Loop
    End Function

End Class
