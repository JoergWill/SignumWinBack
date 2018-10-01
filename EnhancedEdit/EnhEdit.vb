Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports EnhEdit.EnhEdit_Global

Public Class EnhEdit
    Private _eFont As Font
    Private _eFormat As wb_Format
    Private _eUg As Double
    Private _eOG As Double

    Private _Value As String = ""
    Private _Init As Boolean = True
    Private _TextBoxSize As New Size

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()

        'Dieser Aufruf ist für den Komponenten-Designer erforderlich.
        InitializeComponent()
        'SetStyle(ControlStyles.UserPaint, True)
        Me.BorderStyle = BorderStyle.None
    End Sub

    ''' <summary>
    ''' EnhEdit (abgeleitet von UniversalEditBox) verwendet zur Anzeige eine unterlagerte Textbox.
    ''' Die Anordnung der Textbox wird bestimmt durch die Werte aus ClientSize.
    ''' 
    ''' Die passende Größe(Verschiebung) wird hier eingestellt.
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnGotFocus(e As EventArgs)
        Debug.Print("Enhanced Edit OnGotFocus " & Me.Value)
        'Anzeigewert zurücksetzen
        _Value = ""

        'Ausrichtung und Format der unterlagerten Textbox
        Me.TextBox.TextAlign = HorizontalAlignment.Right
        Me.TextBox.BorderStyle = BorderStyle.None
        Me.TextBox.Font = _eFont
        'Cursor ausblenden
        'Me.TextBox.Enabled = False

        'Größe der Textbox vorgeben
        _TextBoxSize.Width = ClientSize.Width - 2
        _TextBoxSize.Height = ClientSize.Height + 2

        MyBase.OnGotFocus(e)
    End Sub
    Public Property eFont As Font
        Get
            Return _eFont
        End Get
        Set(value As Font)
            _eFont = value
        End Set
    End Property

    Public Property eFormat As Integer
        Get
            Return _eFormat
        End Get
        Set(value As Integer)
            _eFormat = value
        End Set
    End Property

    Public Property eUG As String
        Get
            Return Convert.ToString(_eUg)
        End Get
        Set(value As String)
            If value IsNot Nothing And value <> "" Then
                _eUg = Convert.ToDouble(value)
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
                _eOG = Convert.ToDouble(value)
            Else
                _eOG = 0
            End If
        End Set
    End Property

    ''' <summary>
    ''' Wird aufgerufen, wenn sich der Wert des Edit-Feldes (Value) ändert.
    ''' Beim ersten Aufruf der Edit-Routine.
    ''' </summary>
    Protected Overrides Sub OnValueChanged()
        Debug.Print("Enhanced Edit OnValueChanged " & Me.Value)
        If _Init Then
            _Init = False
            Me.TextBox.Text = Value
            Me.TextBox.SelectAll()
        Else
            Me.Value = _Value
        End If
        'MyBase.OnValueChanged()
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)

        'abhängig von Eingabeformat und Tasten-Code
        Select Case EnhEdit_Global.GetKey(e.KeyData, _Value, _eFormat, _eUg, _eOG)

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

            Case wb_Result.KeyReturn
                'Eingabewert übernehmen
                Value = _Value

        End Select

        'weitere Eingabe unterdrücken
        e.SuppressKeyPress = True

        'da der rechte Rand des unterlagerten Steuerelementes verschoben ist, muss ein Offset eingebaut werden
        Me.ClientSize = _TextBoxSize
        'Anzeigewert
        Me.TextBox.Text = _Value
        Me.TextBox.SelectAll()

        'weitere Funktionen werden nicht aufgerufen
        ' MyBase.OnKeyDown(e)
    End Sub

    ''' <summary>
    ''' Enter-Taste nach Eingabe
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnValidating(e As CancelEventArgs)
        Value = _Value
        Debug.Print("Enhanced Edit OnValidating " & Value)
    End Sub
End Class

