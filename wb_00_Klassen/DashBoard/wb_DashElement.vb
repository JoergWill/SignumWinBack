Imports System.Windows.Forms

Public Class wb_DashElement

    Dim MyTimer As System.Timers.Timer
    Dim _Index As Integer
    Dim _FlowLayoutIndex As Integer
    Dim _ShowMe As Boolean

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property Icon As Drawing.Image
        Set(value As Drawing.Image)
            pBox.Image = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return lblTitel.Text
        End Get
        Set(value As String)
            lblTitel.Text = value
        End Set
    End Property

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property Wert As String
        Set(value As String)
            lblWert.Text = value
            If value = "" Then
                lblWert.Visible = False
            Else
                lblWert.Visible = True
            End If
        End Set
    End Property

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property WertBackColor As Drawing.Color
        Set(value As Drawing.Color)
            lblWert.BackColor = value
        End Set
    End Property

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
    Public WriteOnly Property WertTimer As Double
        Set(value As Double)
            StartupTimer.Interval = value
            StartupTimer.Enabled = True
        End Set
    End Property

    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
        End Set
    End Property

    Public Property CheckState As CheckState
        Get
            If _ShowMe Then
                Return CheckState.Checked
            Else
                Return CheckState.Unchecked
            End If
        End Get
        Set(value As CheckState)
            If value = CheckState.Checked Then
                _ShowMe = True
            Else
                _ShowMe = False
            End If
        End Set
    End Property

    Public Property ShowMe As Boolean
        Get
            Return _ShowMe
        End Get
        Set(value As Boolean)
            _ShowMe = value
            Me.Visible = value
        End Set
    End Property

    Public Property FlowLayoutIndex As Integer
        Get
            Return _FlowLayoutIndex
        End Get
        Set(value As Integer)
            _FlowLayoutIndex = value
        End Set
    End Property

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        wb_Dashboard_Shared.DashBoards.Add(Me)
    End Sub

    ''' <summary>
    ''' Click auf Dashboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Overridable Sub wb_DashElement_Click(sender As Object, e As EventArgs) Handles MyBase.Click, pBox.Click
    End Sub

    Public Overridable Sub wb_DashElement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Verzögerung bis zum Start der Auswertung(Datenbank-Abfrage)
        StartupTimer.Enabled = True
        Wert = ""
    End Sub

    Public Overridable Sub StartupTimer_Tick(sender As Object, e As EventArgs) Handles StartupTimer.Tick
        StartupTimer.Enabled = False
        StartupTimer.Dispose()
    End Sub

End Class
