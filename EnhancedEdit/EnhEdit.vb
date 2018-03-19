Imports System.Drawing
Imports System.Windows.Forms

Public Class EnhEdit
    Private _eFont As Font
    Private _eBackColor As Color
    Private _eBorderColor As Color

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()

        'Dieser Aufruf ist für den Komponenten-Designer erforderlich.
        InitializeComponent()
        SetStyle(ControlStyles.UserPaint, True)
        Me.BorderStyle = BorderStyle.None
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


    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim Rect As New Rectangle(-1, +2, ClientSize.Width - 1, ClientSize.Height - 1)
        Dim Format As New StringFormat
        Format.Alignment = StringAlignment.Near
        Dim B As New SolidBrush(Me.BackColor)

        Using p As Pen = New Pen(_eBorderColor, 1)
            e.Graphics.FillRectangle(B, Rect)
            'e.Graphics.DrawRectangle(p, Rect)
            e.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), Rect)
        End Using
    End Sub

End Class

