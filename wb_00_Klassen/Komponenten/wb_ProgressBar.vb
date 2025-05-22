Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_ProgressBar
    Inherits Label

    Private _CustomText As String = ""
    Private _CustomColor As Color = Color.Lime
    Private _CustomValue As Integer = 0
    Private _CustomFont As Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    Public Property CustomText As String
        Get
            Return _CustomText
        End Get
        Set(value As String)
            If _CustomText <> value Then
                _CustomText = value
                Me.Text = _CustomText
                ProgressBar.Text = " " & _CustomText
            End If
        End Set
    End Property

    Public Property CustomColor As Color
        Get
            Return _CustomColor
        End Get
        Set(value As Color)
            _CustomColor = value
            ProgressBar.BackColor = value
        End Set
    End Property

    Public Property CustomValue As Integer
        Get
            Return _CustomValue
        End Get
        Set(value As Integer)
            _CustomValue = value
            ProgressBar.Width = Me.Width * wb_Functions.SaveDiv(_CustomValue, 100)
        End Set
    End Property

    Public Property CustomFont As Font
        Get
            Return _CustomFont
        End Get
        Set(value As Font)
            _CustomFont = value
            Me.Font = _CustomFont
            ProgressBar.Font = _CustomFont
            ProgressBar.Size = Me.Size
        End Set
    End Property
End Class
