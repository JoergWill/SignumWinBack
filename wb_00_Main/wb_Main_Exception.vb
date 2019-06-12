Public Class wb_Main_Exception
    Private _StackTrace As String
    Private _Message As String

    ''' <summary>
    ''' Beim Initialisieren werden gleich die notwendigen Parameter für
    ''' Stacktrace und Message übergeben
    ''' </summary>
    ''' <param name="StackTrace"></param>
    ''' <param name="Message"></param>
    Public Sub New(StackTrace As String, Message As String)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _StackTrace = StackTrace
        _Message = Message
    End Sub

    ''' <summary>
    ''' Dialog-Fenster wird geladen.
    ''' Texte der Exit-Buttons anpassen an die Programm-Variante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_Main_Exception_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Texte auf Exit-Buttons anpassen - Variante OrgaBack/WinBack
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
            BtnContinue.Text = "WinBack-AddIn fortsetzen"
            BtnRestart.Text = "OrgaBack neu starten"
            BtnExit.Text = "OrgaBack beenden"
        End If
    End Sub

    ''' <summary>
    ''' Sende den Fehlerbericht mit Stacktrace und Fehlermeldung an software@winback.de
    ''' Das Versenden der Mail erfolgt über das Windows-Standard-eMail-Programm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnMail_Click(sender As Object, e As EventArgs) Handles BtnMail.Click
        Dim Mail As New wb_Mail
        Mail.StartMail_Exception(_StackTrace, _Message)
    End Sub

    Private Sub BtnShow_Click(sender As Object, e As EventArgs) Handles BtnShow.Click
        'Textbox positionieren
        PnlPicture.Width = tbException.Width
        tbException.Dock = Windows.Forms.DockStyle.Fill
        lblText.Visible = False
        'Exception-Text einfügen
        tbException.Text = _Message & vbCrLf & vbCrLf & _StackTrace
        'und anzeigen
        tbException.Visible = True
    End Sub
End Class