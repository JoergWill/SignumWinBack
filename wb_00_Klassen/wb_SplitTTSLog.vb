Public Class wb_SplitTTSLog

    Private _Token As String = ""
    Private _SubToken As String = ""
    Private _ChargeNr As String
    Private _Content As String = ""
    Private _LogString As String = ""

    Private _ResultList As New List(Of String)
    Private _TokenList As List(Of String)
    Private _ValueList As List(Of String)

    Public Sub New(LogZeile As String)
        _ResultList.Clear()
        GetToken(LogZeile)
    End Sub

    Public Property Token As String
        Get
            Return _Token
        End Get
        Set(value As String)
            _Token = value
        End Set
    End Property

    Public Property SubToken As String
        Get
            Return _SubToken
        End Get
        Set(value As String)
            _SubToken = value
        End Set
    End Property

    Public ReadOnly Property Values(Trennzeichen As String) As List(Of String)
        Get
            If _ResultList.Count = 0 Then
                Return GetValues(Trennzeichen)
            Else
                Return _ResultList
            End If
        End Get
    End Property

    Public Property LogString As String
        Get
            Return _LogString
        End Get
        Set(value As String)
            _LogString = value
        End Set
    End Property

    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Private Sub GetToken(z As String)
        'die ersten Zeichen bis "TTS:" eliminieren
        Dim s() As String = Split(LTrim(z), " ", 5)
        If s.Count = 5 Then
            'String für Log-Ausgabe zusammensetzen
            _LogString = s(1) & " " & s(2) & " " & s(4)
            Dim m() As String = Split(s(4), ":", 3)
            'Chargen-Nummer
            _ChargeNr = m(1)
            'Inhalt nach TTS:XXXX:
            _Content = m(2)

            'Sonderzeichen eliminieren
            _Content = Replace(_Content, "--->", "")
            _Content = Replace(_Content, "-->", "")
            _Content = Replace(_Content, ">>>>", "")
            _Content = Replace(_Content, "<<<<", "")

            'Fehlermeldungen ausfiltern
            If Not GetErrors(_Content) Then

                'Array für Tokens
                Dim t() As String
                'Token suchen 
                t = Split(_Content, "/")
                If t.Count > 1 Then
                    _Token = Trim(t(0))
                Else
                    t = Split(_Content, "=")
                    If t.Count > 1 Then
                        _Token = Trim(t(0))
                        If _Token = "" Then
                            _Token = Trim(t(1))
                        End If
                    Else
                        t = Split(_Content, ":")
                        If t.Count > 1 Then
                            _Token = Trim(t(0))
                            _SubToken = Trim(t(1))
                        Else
                            'kein Token gefunden
                            Exit Sub
                        End If
                    End If
                End If

                'SubToken suchen
                t = Split(_Token, ".")
                If t.Count > 1 Then
                    _Token = t(0)
                    _SubToken = Trim(t(1))
                End If

                'Sonderzeichen aus Token eliminieren
                _Token = Replace(_Token, "+", "")
                _Token = Replace(_Token, "-", "")
                _Token = Replace(_Token, Chr(34), "")
            End If
        End If
    End Sub

    Private Function GetValues(Trennzeichen As String) As List(Of String)
        Dim v() As String
        Select Case Trennzeichen
            Case ":"
                v = Split(_Content, ":")
                If v.Count > 1 Then
                    _ResultList.Add(Trim(v(1)))
                End If
            Case " "
                v = Split(_Content, " ")
                For Each s In v
                    _ResultList.Add(Trim(s))
                Next
            Case "="
                v = Split(_Content, "=")
                If v.Count > 1 Then
                    _ResultList.Add(Trim(v(1)))
                End If
                If v.Count > 2 Then
                    _ResultList.Add(Trim(v(2)))
                End If
            Case "/"
                v = Split(_Content, ":")
                If v.Count > 1 Then
                    v = Split(v(1), "/")
                    For Each s In v
                        _ResultList.Add(Trim(s))
                    Next
                End If
            Case Else
                _ResultList.Add("")
        End Select
        Return _ResultList
    End Function

    Private Function GetErrors(LogZeile As String) As Boolean
        'Ergebnis
        Dim Result As Boolean = False
        'Array für Tokens
        Dim t() As String
        'String zerlegen
        LogZeile = Trim(_Content)

        'exit_grund ausfiltern
        If LogZeile.Contains("(exit_grund") Then
            LogZeile = LogZeile.Substring(0, LogZeile.Length - "(exit_grund=x)".Length)
            _Token = "exit_grund"

            'Fehlermeldung extrahieren
            t = Split(LogZeile, ":")
            If t.Count > 1 Then
                _SubToken = Trim(t(2)) & " !"
            Else
                _SubToken = Trim(t(0)) & " !"
            End If

            'Fehlermeldung in dieser Zeile im Log gefunden
            Result = True
        End If

        'Wassermenge wurde geändert
        If LogZeile.Contains("Wassermenge: Keine") Then
            _Token = "Wassermenge"
            'Fehlermeldung extrahieren
            t = Split(LogZeile, ":")
            If t.Count > 1 Then
                _SubToken = Trim(t(1))
            Else
                _SubToken = LogZeile
            End If

            'Fehlermeldung in dieser Zeile im Log gefunden
            Result = True
        End If

        'Wassertemperatur wurde geändert
        If LogZeile.Contains("Wassertemp.: Keine") Then
            _Token = "Wassertemp"
            'Fehlermeldung extrahieren
            t = Split(LogZeile, ":")
            If t.Count > 1 Then
                _SubToken = Trim(t(1))
            Else
                _SubToken = LogZeile
            End If

            'Fehlermeldung in dieser Zeile im Log gefunden
            Result = True
        End If

        Return Result
    End Function
End Class
