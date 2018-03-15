Public NotInheritable Class About_WinBack

    Private WithEvents Admin_ColorTheme As New Admin_ThemeControl
    Public Event Ab_LoadColorTable(FileName As String)

    ''' <summary>
    ''' Event LoadColorTable wird weitergegeben an WinBack-Main
    ''' </summary>
    ''' <param name="Filename"></param>
    Private Sub LoadColorTable(Filename As String) Handles Admin_ColorTheme.Ac_LoadColorTable
        RaiseEvent Ab_LoadColorTable(Filename)
    End Sub

    ''' <summary>
    ''' Programm-Name und Version ausgeben
    ''' Änderungs-Historie aus Textfile ausgeben.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub About_WinBack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright & " " & My.Application.Info.CompanyName

        'Versions-Historie aus Version.txt anzeigen
        lbVersionsHistorie.Text = "Änderungen und Updates" & vbCrLf & "Weitere Informationen unter www.winback.de"
        lbVersionsHistorie.Links.Clear()
        lbVersionsHistorie.Links.Add(52, 16)

        'Zeilenvorschub
        lbVersionsHistorie.Text = lbVersionsHistorie.Text & vbCrLf & vbCrLf

        'Datei einlesen und anzeigen
        If System.IO.File.Exists(wb_GlobalSettings.pVersionTxtPath) Then
            lbVersionsHistorie.Text = lbVersionsHistorie.Text & System.IO.File.ReadAllText(wb_GlobalSettings.pVersionTxtPath)
        End If
    End Sub

    ''' <summary>
    ''' Fenster wieder schliessen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Fenster zu Farb-Verwaltung (Themes) aufrufen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnColors_Click(sender As Object, e As EventArgs) Handles BtnColors.Click
        If Admin_ColorTheme.ShowDialog() = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class
