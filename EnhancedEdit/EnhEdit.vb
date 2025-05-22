Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Text
Imports EnhEdit.EnhEdit_Global
Imports System.Reflection

Public Class EnhEdit

    Private _eFormat As wb_Format = 0
    Private _eUg As Double
    Private _eOG As Double

    Private _eValue As String = ""
    Private _oValue As Object = Nothing
    Private _Init As Boolean = True
    Private _NoKeyPressed As Boolean = True
    Private _TextBoxSize As New Size
    Private _Handle As String

    Private ReadOnly sqlFormatProvider As CultureInfo

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()

        'Dieser Aufruf ist für den Komponenten-Designer erforderlich.
        InitializeComponent()

        'aus wb_functions
        'Formateinstellungen für die Konvertierung von String-Dezimalwerten nach Float aus der sql-Datenbank
        sqlFormatProvider = CultureInfo.CreateSpecificCulture("de-DE")
        'Bei Sollwerten in der MySQL-Datenbank ist der Dezimaltrenner IMMER ein Komma, unabhängig von den
        'Einstellungen in Windows.
        sqlFormatProvider.NumberFormat.NumberDecimalSeparator = ","

        'Me.SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)
        Me.BorderStyle = BorderStyle.None
        'Me.BackColor = Color.Transparent

        'Handle merken (Debug)
        _Handle = Me.Handle.ToString
        'Debug.Print("New() Handle " & _Handle)
    End Sub

    ''' <summary>
    ''' EnhEdit (abgeleitet von UniversalEditBox) verwendet zur Anzeige eine unterlagerte Textbox.
    ''' Die Anordnung der Textbox wird bestimmt durch die Werte aus ClientSize.
    ''' 
    ''' Die passende Größe(Verschiebung) wird hier eingestellt.
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnGotFocus(e As EventArgs)
        'Debug.Print("OnGotFocus " & Me.Value & "/" & Me.Handle.ToString & "/" & Me.eFormat)
        'Anzeigewert zurücksetzen
        _eValue = ""
        _NoKeyPressed = True

        'Grenzwerte und Format aus EnhEdit_Global
        eFormat = GLeFormat
        oValue = GLoValue
        eUG = GLeUG
        eOG = GLeOG

        'Ausrichtung und Format der unterlagerten Textbox
        If _eFormat = wb_Format.fString Then
            Me.TextBox.TextAlign = HorizontalAlignment.Left
        ElseIf _eFormat = wb_Format.fAllergen Or _eFormat = wb_Format.fYesNo Then
            Me.TextBox.TextAlign = HorizontalAlignment.Center
        Else
            Me.TextBox.TextAlign = HorizontalAlignment.Right
        End If

        Me.TextBox.BorderStyle = BorderStyle.None
        Me.TextBox.AcceptsReturn = False

        'Größe der Textbox vorgeben
        _TextBoxSize.Width = ClientSize.Width - 2
        _TextBoxSize.Height = ClientSize.Height + 2

        MyBase.OnGotFocus(e)
    End Sub

    Private Property oValue As Object
        Get
            Return _oValue
        End Get
        Set(value As Object)
            _oValue = value
            Init = True
            Me.TextBox.Text = value
            Me.TextBox.SelectAll()
        End Set
    End Property

    Private Property eFormat As wb_Format
        Get
            Return _eFormat
        End Get
        Set(value As wb_Format)
            _eFormat = value
            'Debug.Print("Handle " & _Handle & " Format set to " & value.ToString)
        End Set
    End Property

    Private Property eUG As String
        Get
            Return Convert.ToString(_eUg)
        End Get
        Set(value As String)
            If value IsNot Nothing And value <> "" Then
                _eUg = StrToDouble(value)
            Else
                _eUg = 0
            End If
        End Set
    End Property

    Private Property eOG As String
        Get
            Return Convert.ToString(_eOG)
        End Get
        Set(value As String)
            If value IsNot Nothing And value <> "" Then
                _eOG = StrToDouble(value)
            Else
                _eOG = 0
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gibt, abhängig vom Format den Wert formatiert zurück
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property eValue As Object
        Get
            Select Case _eFormat

                'Gleitkomma-Zahlen mit 3 Nachkommastellen
                Case wb_Format.fReal, wb_Format.fInteger
                    Try
                        If _eValue <> "" Then
                            'Return Convert.ToDouble(_eValue).ToString("F3")
                            Return FormatStr(_eValue, 3)
                        Else
                            Return 0.ToString("F3")
                        End If
                    Catch ex As Exception
                        Return 0.ToString("F3")
                    End Try

                'String
                Case wb_Format.fString
                    Return _eValue

                'Zeit
                Case wb_Format.fTime
                    Return FormatTimeStr(_eValue)

                'Allergen
                Case wb_Format.fAllergen
                    Return _eValue

                'ErnährungsForm
                Case wb_Format.fYesNo
                    Return _eValue

                    'nicht definiert
                Case Else
                    Return ""

            End Select
        End Get
    End Property

    Public Property Init As Boolean
        Get
            'Debug.Print("GetInit " & _Init.ToString & "/" & Me.Handle.ToString)
            Return _Init
        End Get
        Set(value As Boolean)
            _Init = value
            'Debug.Print("SetInit " & value.ToString & "/" & Me.Handle.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Wird aufgerufen, wenn sich der Wert des Edit-Feldes (Value) ändert.
    ''' Beim ersten Aufruf der Edit-Routine.
    ''' </summary>
    Protected Overrides Sub OnValueChanged()
        If Init Then
            'Debug.Print("OnValueChanged(Init) " & Me.Value & "/" & Me.Handle.ToString & "/" & Me.eFormat)
            Init = False
            '_oValue = Value
            'Me.TextBox.Text = Value
            'Me.TextBox.SelectAll()
        Else
            Me.Value = eValue
            'Debug.Print("OnValueChanged (eValue)" & Me.Value & "/" & Me.Handle.ToString & "/" & Me.eFormat)
        End If
        'MyBase.OnValueChanged()
    End Sub

    ''' <summary>
    ''' Wird vor onKeyDown() aufgerufen. Im Gegensatz zu OnKeyDown() werden hier auch die Events von Return und Escape
    ''' verarbeitet.
    ''' Siehe auch: https://social.msdn.microsoft.com/Forums/windows/en-US/575ea120-036a-4e68-877a-f22a68de9689/detecting-esc-key-stroke-in-toolstriptextbox?forum=winforms
    ''' 
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="keyData"></param>
    ''' <returns></returns>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        'Debug.Print("ProcessCmdKey " & keyData.ToString)

        If keyData = Keys.Escape Then
            'Orginalwert wieder eintragen
            _eValue = _oValue
            'entspricht der Return-Taste
            Me.ValidateText()
        End If

        'Pfeil nach unten - Simuliert TAB
        If keyData = Keys.Down Then
            Me.ValidateText()
            keyData = Keys.Tab
        End If

        'Pfeil nach oben - Simuliert Shift-TAB
        If keyData = Keys.Up Then
            Me.ValidateText()
            keyData = Keys.Tab + Keys.Shift
        End If

        'Enter-Taste - Edit nächste Zeile
        'If keyData = Keys.Return Then
        '    Return MyBase.ProcessCmdKey(msg, Keys.Tab)
        'Else
        '    Return MyBase.ProcessCmdKey(msg, keyData)
        'End If

        'Enter-Taste - Ende Edit
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        'Debug.Print("OnKeyDown " & e.KeyCode & "/" & e.KeyData & "/" & e.KeyValue & "/" & Me.Handle.ToString & "/" & Me.eFormat)

        'Reset Flag NoKeyPressed
        _NoKeyPressed = False

        'Fehlermeldung ausgeben - wenn notwendig
        Select Case EnhEdit_Global.GetKey(e, _eValue, _eFormat, _eUg, _eOG)

            Case wb_Result.ValueErrMax
                If _eFormat = wb_Format.fString Then
                    'Eingabestring ist zu lang
                    MsgBox("Eingabewert ist zu lang", MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                Else
                    'Eingabewert ist zu groß (Numerisch)
                    MsgBox("Der Eingabewert ist zu groß!" & vbCrLf & vbCrLf & "Zulässige Werte sind: " & vbCrLf & _eUg.ToString & " < Eingabe < " & _eOG, MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                End If

            Case wb_Result.ValueErrMin
                If _eUg <= 0.0 Then
                    'Eingabewert ist zu klein (Numerisch) nur wenn die Untergrenze auf 0,00 eingstellt ist
                    MsgBox("Der Eingabewert ist zu klein!" & vbCrLf & vbCrLf & "Zulässige Werte sind: " & vbCrLf & _eUg.ToString & " < Eingabe < " & _eOG, MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                End If

            Case wb_Result.ValueErrFormat
                If _eFormat = wb_Format.fTime Then
                    'Eingabestring ist zu lang
                    MsgBox("Eingabewert ist zu lang", MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                Else
                    'Falsches Format
                    MsgBox("Eingabewert nicht zulässig", MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                End If

            Case wb_Result.ValueErrException
                'Exception bei der Eingabe-Prüfung
                MsgBox("Unbekannter Fehler bei der Eingabe", MsgBoxStyle.Critical, "Fehler bei der Eingabe")

            Case wb_Result.KeyReturn
                'Eingabewert übernehmen
                Value = eValue

            Case wb_Result.KeyEscape
                'ursprünglichen Eingabewert wieder eintragen
                Value = _oValue

        End Select

        'da der rechte Rand des unterlagerten Steuerelementes verschoben ist, muss ein Offset eingebaut werden
        Me.ClientSize = _TextBoxSize
        Me.BorderStyle = BorderStyle.None
        TextBox.BorderStyle = BorderStyle.None

        'Anzeige formatieren
        Select Case _eFormat

            Case wb_Format.fReal
                'Formatieren auf 3-Nachkommastellen
                TextBox.Text = FormatStr(_eValue, 3)
                TextBox.Select(_eValue.Length, 0)

            Case wb_Format.fInteger
                'Formatieren auf 0-Nachkommastellen
                TextBox.Text = FormatStr(_eValue, 0)
                TextBox.Select(_eValue.Length, 0)

            Case wb_Format.fTime
                'Formatieren TimeString
                TextBox.Text = FormatTimeStr(_eValue)
                TextBox.Select(_eValue.Length, 0)

            Case Else
                'alle anderen Formate
                TextBox.Text = _eValue
                TextBox.Select(_eValue.Length, 0)
        End Select

        'weitere Eingabe unterdrücken
        e.SuppressKeyPress = True

        'weitere Funktionen werden nicht aufgerufen
        'MyBase.OnKeyDown(e)
    End Sub

    ''' <summary>
    ''' Eingabe beendet. Ergebnis wird in Value übertragen.
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnValidating(e As CancelEventArgs)
        If _NoKeyPressed Then
            Value = _oValue
            'Debug.Print("OnValidating(NoKeyPressed) " & Value & "/" & Me.Handle.ToString & "/" & Me.eFormat)
        Else
            Value = eValue
            'Debug.Print("OnValidating " & Value & "/" & Me.Handle.ToString & "/" & Me.eFormat)
        End If
    End Sub

    ''' <summary>
    ''' Wandelt einen String sicher in Float um. Das Zahlenformat kann US/DE sein. Punkte werden vor der Konvertierung in Koma umgewandelt.
    ''' 1000er - Trennzeichen sind nicht erlaubt.
    ''' Die Umwandlung erfolgt unabhängig von der eingestellten Länderkennung!
    ''' Wenn die Umwandlung per TryParse fehlschlägt (Result=False) wird die einfache Umwandlung per val() versucht. Damit können auch Werte
    ''' umgewandelt werden, die Strings enthalten (z.B. 10kg)
    ''' 
    ''' Aus WinBackAddIn.wb_functions
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns>Konvertierten String im Format Double</returns>
    Public Function StrToDouble(value As String) As Double
        If value IsNot Nothing AndAlso value IsNot "" Then
            Dim d As Double
            Try
                value = value.Replace(".", ",")
                If Double.TryParse(value, NumberStyles.Number, sqlFormatProvider, d) Then
                    Return d
                Else
                    'mögliche Strings oder Sonderzeichen entfernen
                    value = New System.Text.RegularExpressions.Regex("[a-zA-ZüöäÜÖÄß%°\\s\\n]").Replace(value, String.Empty)
                    If Double.TryParse(value, NumberStyles.Number, sqlFormatProvider, d) Then
                        Return d
                    Else
                        Return 0.0F
                    End If
                End If
            Catch ex As Exception
                Return 0.0F
            End Try
        Else
            Return 0.0F
        End If
    End Function

    ''' <summary>
    ''' Formatiert einen String mit der angegebenen Vorkomma und Nachkomma-Stelle
    ''' Wenn als Culture "sql" angegeben wird, erfolgt die Umwandlung IMMER mit Dezimaltrenner Komma, unabhängig von
    ''' der Windows-Ländereinstellung.
    ''' 
    ''' Aus WinBackAddIn.wb_functions
    ''' </summary>
    ''' <param name="value">Zahlenwert als String</param>
    ''' <param name="VorKomma">Anzahl der Vorkomma-Stellen</param>
    ''' <param name="NachKomma">Anzahl der Nachkomma-Stellen</param>
    ''' <param name="Culture">Ländereinstellung (Default de-DE)</param>
    ''' <returns></returns>
    <CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of functions should not be too high", Justification:="<Ausstehend>")>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Public Function FormatStr(value As String, NachKomma As Integer, Optional VorKomma As Integer = -1, Optional ByVal Culture As String = Nothing) As String
        Dim wert As Double
        Try
            If value IsNot Nothing AndAlso value <> "" AndAlso value <> "-" Then
                ' Für Datenbank-Felder muss unabhängig von der Ländereinstellung die Umwandlung mit
                ' der Einstellung de-DE erfolgen
                If Culture IsNot Nothing Then
                    'Sonderbehandlung für Werte aus der MySQL-Datenbank (Dezimaltrenner ist IMMER Komma)
                    If Culture = "sql" Then
                        wert = StrToDouble(value)
                    Else
                        wert = Convert.ToDouble(value, New System.Globalization.CultureInfo(Culture))
                    End If
                Else
                    wert = Convert.ToDouble(value)
                End If
            Else
                Return "-"
                Exit Function
            End If

            If NachKomma <> 0 Then
                If VorKomma < 0 Then
                    Return wert.ToString("F" & NachKomma.ToString)
                Else
                    Return Strings.Right(Space(VorKomma) & wert.ToString("F" & NachKomma.ToString), VorKomma + NachKomma + 1)
                End If
            Else
                If VorKomma < 0 Then
                    Return wert.ToString("F0")
                Else
                    Return Strings.Right(Space(VorKomma) & wert.ToString("F" & NachKomma.ToString), VorKomma)
                End If
            End If
        Catch
            Return "-"
        End Try
    End Function

    ''' <summary>
    ''' Formatiert einen String im Muster 00:00:00
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Public Function FormatTimeStr(Value As String) As String
        Dim ts As String() = Value.Split(":")
        Dim ti(3) As Integer

        'alle Bestandteile in Integer wandeln (sicherheitshalber)
        For i = 0 To ts.Length - 1
            ti(i) = StrToInt(ts(i))
        Next

        'Uhrzeit auf sinnvolle Werte begrenzen

        'Sekunden maximal 59
        If (ts.Length > 2) AndAlso (ti(2) > 59) Then
            ti(2) = ti(2) - 60
            ti(1) = ti(1) + 1
        End If

        'Minuten maximal 59
        If (ts.Length > 1) AndAlso (ti(1) > 59) Then
            ti(1) = ti(1) - 60
            ti(0) = ti(0) + 1
        End If

        'Stunden maximal 23h
        If ti(0) > 23 Then
            ti(0) = 23
        End If

        Select Case ts.Length
            Case 0
                Return "00:00:00"
            Case 1
                Return Strings.Right("00" & ti(0).ToString, 2) & ":00:00"
            Case 2
                Return Strings.Right("00" & ti(0).ToString, 2) & ":" & Strings.Right("00" & ti(1).ToString, 2) & ":00"
            Case Else
                Return Strings.Right("00" & ti(0).ToString, 2) & ":" & Strings.Right("00" & ti(1).ToString, 2) & ":" & Strings.Right("00" & ti(2).ToString, 2)
        End Select
    End Function

    ''' <summary>
    ''' Wandelt einen String sicher in Integer um. Wenn die Umwandlung fehlschlägt wird 0 zurückgegeben.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns>Konvertierten String im Format Integer</returns>
    Public Function StrToInt(value As String) As Integer
        Dim i As Integer
        If value IsNot Nothing Then
            Try
                value = value.Replace(".", ",")
                If Integer.TryParse(value, NumberStyles.Number, sqlFormatProvider, i) Then
                    Return i
                Else
                    Try
                        Return Int(Val(value))
                    Catch
                        Return 0
                    End Try
                End If
            Catch ex As Exception
                Return 0
            End Try
        Else
            Return 0
        End If
    End Function
End Class

