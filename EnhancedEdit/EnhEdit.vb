﻿Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports EnhEdit.EnhEdit_Global
Imports WinBack

Public Class EnhEdit

    Private _eFormat As wb_Format = 0
    Private _eUg As Double
    Private _eOG As Double

    Private _eValue As String = ""
    Private _oValue As Object = Nothing
    Private _Init As Boolean = True
    Private _NoKeyPressed As Boolean = True
    Private _TextBoxSize As New Size

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()

        'Dieser Aufruf ist für den Komponenten-Designer erforderlich.
        InitializeComponent()
        'SetStyle(ControlStyles.UserPaint, True)
        Me.BorderStyle = BorderStyle.None

        'Debug.Print("Enhanced Edit New() " & Me.Handle.ToString)
    End Sub

    ''' <summary>
    ''' EnhEdit (abgeleitet von UniversalEditBox) verwendet zur Anzeige eine unterlagerte Textbox.
    ''' Die Anordnung der Textbox wird bestimmt durch die Werte aus ClientSize.
    ''' 
    ''' Die passende Größe(Verschiebung) wird hier eingestellt.
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnGotFocus(e As EventArgs)
        'Debug.Print("Enhanced Edit OnGotFocus " & Me.Value)
        'Debug.Print("Enhanced Edit eFormat    " & Me._eFormat.ToString)
        'Anzeigewert zurücksetzen
        _eValue = ""
        _NoKeyPressed = True

        'Ausrichtung und Format der unterlagerten Textbox
        If _eFormat = wb_Format.fString Then
            Me.TextBox.TextAlign = HorizontalAlignment.Left
        Else
            Me.TextBox.TextAlign = HorizontalAlignment.Right
        End If

        Me.TextBox.BorderStyle = BorderStyle.None
        Me.TextBox.AcceptsReturn = True

        'Größe der Textbox vorgeben
        _TextBoxSize.Width = ClientSize.Width - 2
        _TextBoxSize.Height = ClientSize.Height + 2

        MyBase.OnGotFocus(e)
    End Sub

    Public Property eFormat As wb_Format
        Get
            Return _eFormat
        End Get
        Set(value As wb_Format)
            _eFormat = value
        End Set
    End Property

    Public Property eUG As String
        Get
            Return Convert.ToString(_eUg)
        End Get
        Set(value As String)
            If value IsNot Nothing And value <> "" Then
                _eUg = wb_Functions.StrToDouble(value)
            Else
                _eUg = 0
            End If
        End Set
    End Property

    Public Property eOG As String
        Get
            Return Convert.ToString(_eOG)
        End Get
        Set(value As String)
            If value IsNot Nothing And value <> "" Then
                _eOG = wb_Functions.StrToDouble(value)
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
                            Return Convert.ToDouble(_eValue).ToString("F3")
                        Else
                            Return 0.ToString("F3")
                        End If
                    Catch ex As Exception
                        Return 0.ToString("F3")
                    End Try

                'String/Zeit
                Case wb_Format.fString, wb_Format.fTime
                    Return _eValue

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
            'Debug.Print("Enhanced Edit GetInit " & _Init.ToString & "/" & Me.Handle.ToString)
            Return _Init
        End Get
        Set(value As Boolean)
            _Init = value
            'Debug.Print("Enhanced Edit SetInit " & value.ToString & "/" & Me.Handle.ToString)
        End Set
    End Property

    ''' <summary>
    ''' Wird aufgerufen, wenn sich der Wert des Edit-Feldes (Value) ändert.
    ''' Beim ersten Aufruf der Edit-Routine.
    ''' </summary>
    Protected Overrides Sub OnValueChanged()
        If Init Then
            'Debug.Print("Enhanced Edit OnValueChanged(Init) " & Me.Value & "/" & Me.Handle.ToString)
            Init = False
            _oValue = Value
            Me.TextBox.Text = Value
            Me.TextBox.SelectAll()
        Else
            Me.Value = eValue
            'Debug.Print("Enhanced Edit OnValueChanged " & Me.Value & "/" & Me.Handle.ToString)
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

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        'Debug.Print("OnKeyDown " & e.KeyCode & "/" & e.KeyData & "/" & e.KeyValue)

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
                    MsgBox("Eingabewert ist zu groß", MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                End If

            Case wb_Result.ValueErrMin
                If _eUg <= 0.0 Then
                    'Eingabewert ist zu klein (Numerisch) nur wenn die Untergrenze auf 0,00 eingstellt ist
                    MsgBox("Eingabewert ist zu klein", MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                End If

            Case wb_Result.ValueErrFormat
                If _eFormat = wb_Format.fTime Then
                    'Eingabestring ist zu lang
                    MsgBox("Eingabewert ist zu lang", MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                Else
                    'Falsches Format
                    MsgBox("Eingabewert nicht zulässig", MsgBoxStyle.Critical, "Fehler bei der Eingabe")
                End If

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
                TextBox.Text = wb_Functions.FormatStr(_eValue, 3)
                TextBox.Select(_eValue.Length, 0)

            Case wb_Format.fTime
                'Formatieren TimeString
                TextBox.Text = wb_Functions.FormatTimeStr(_eValue)
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
            'Debug.Print("Enhanced Edit OnValidating(NoKeyPressed) " & Value)
        Else
            Value = eValue
            _oValue = Value
            'Debug.Print("Enhanced Edit OnValidating " & Value)
        End If
    End Sub

End Class

