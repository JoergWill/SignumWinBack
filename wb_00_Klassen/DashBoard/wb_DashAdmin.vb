Imports System.Timers

Public Class wb_DashAdmin
    Inherits wb_DashElement

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'Titeltext der Kachel
        Title = "Info"
        'User-Rechte werden über Tag abgebildet
        Tag = 120
    End Sub
    Private Sub wb_Dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Hintergrundbild der Kachel
        Icon = WinBack.My.Resources.Resources.orgaback_logo_header_jpg_64x64
        'Wert(Anzahl der Artikel) Hintergrund-Farbe
        WertBackColor = Drawing.Color.LightGray
    End Sub

    ''' <summary>
    ''' Click auf Artikel - Main-Form-Artikel aufrufen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Overrides Sub wb_DashElement_Click(sender As Object, e As EventArgs)
        'Artikelverwaltung aufrufen
        wb_Main_Shared.OpenForm(sender, "Admin")
    End Sub

    ''' <summary>
    ''' Nach dem Anzeigen des Info-Dash wird die Versions-Nummer angezeigt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Overrides Sub StartupTimer_Tick(sender As Object, e As EventArgs)
        ' Timer beenden (OneShot)
        MyBase.StartupTimer_Tick(sender, e)

        Wert = "V " & wb_GlobalSettings.WinBackVersion
    End Sub
End Class
