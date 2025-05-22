Imports System.Timers

Public Class wb_DashKneter
    Inherits wb_DashElement

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'Titeltext der Kachel
        Title = "Kneter"
        'User-Rechte werden über Tag abgebildet
        Tag = 120
    End Sub
    Private Sub wb_Dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Hintergrundbild der Kachel
        Icon = WinBack.My.Resources.Resources.MainSync_32x32
        'Wert(Anzahl der Artikel) Hintergrund-Farbe
        WertBackColor = Drawing.Color.LightGray
    End Sub

    ''' <summary>
    ''' Click auf Artikel - Main-Form-Artikel aufrufen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Overrides Sub wb_DashElement_Click(sender As Object, e As EventArgs)
        'Linien-Anzeige(VNC) aufrufen
        wb_Main_Shared.OpenForm(sender, "Linien")
    End Sub

    ''' <summary>
    ''' Nach dem Anzeigen des Kneter-Dash
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub StartupTimer_Tick(sender As Object, e As EventArgs)
        ' Timer beenden (OneShot)
        MyBase.StartupTimer_Tick(sender, e)
    End Sub
End Class
