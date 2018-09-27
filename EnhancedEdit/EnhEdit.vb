Imports System.Drawing
Imports System.Windows.Forms

Public Class EnhEdit
    Private _eFont As Font
    Private _eBackColor As Color
    Private _eBorderColor As Color
    Private _Value As String = ""
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
        'Ausrichtung und Format der unterlagerten Textbox
        Me.TextBox.TextAlign = HorizontalAlignment.Right
        Me.TextBox.BorderStyle = BorderStyle.None
        Me.TextBox.Font = _eFont

        'Größe der Textbox vorgeben
        _TextBoxSize.Width = ClientSize.Width - 2
        _TextBoxSize.Height = ClientSize.Height

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

    Public Property eBackcolor As Color
        Get
            Return _eBackColor
        End Get
        Set(value As Color)
            _eBackColor = value
        End Set
    End Property

    Public Property eBorderColor As Color
        Get
            Return _eBorderColor
        End Get
        Set(value As Color)
            _eBorderColor = value
        End Set
    End Property

    ''' <summary>
    ''' Wird aufgerufen, wenn sich der Wert des Edit-Feldes (Value) ändert.
    ''' Beim ersten Aufruf der Edit-Routine.
    ''' </summary>
    Protected Overrides Sub OnValueChanged()
        Debug.Print("Enhanced Edit OnValueChanged " & Me.Value & " " & ClientSize.Width & "/" & ClientSize.Height)
        'Me.Value = ""
        'MyBase.OnValueChanged()
    End Sub

    ''' <summary>
    ''' Wird nicht aufgerufen/verwendet
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        Debug.Print("Enhanced Edit OnKeyPress " & Me.Value & " " & ClientSize.Width & "/" & ClientSize.Height)
        'MyBase.OnKeyPress(e)
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        Debug.Print("Enhanced Edit OnKeyDown " & e.KeyData)
        'TODO Hier wird je nach Eingabe-Taste und Format entschieden, wie der Wert (_Value) gesetzt werden soll
        '     siehe Edit in Delphi (abhängig von KomponType ...)
        _Value = _Value & Chr(e.KeyValue)

        'weitere Eingabe unterdrücken
        e.SuppressKeyPress = True

        'da der rechte Rand des unterlagerten Steuerelementes verschoben ist, muss ein Offset eingebaut werden
        Me.ClientSize = _TextBoxSize
        'Anzeigewert
        Me.TextBox.Text = _Value

        'weitere Funktionen werden nicht aufgerufen
        ' MyBase.OnKeyDown(e)
    End Sub
End Class

