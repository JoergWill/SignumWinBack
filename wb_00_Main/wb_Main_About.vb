Imports System.Windows.Forms

Public NotInheritable Class Wb_Main_About

    Private WithEvents Admin_ColorTheme As New wb_Admin_ThemeControl
    Public Event Ab_LoadColorTable(FileName As String)
    Dim UpdateWinBack As New wb_Admin_UpdateWinBack

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
    ''' 
    ''' Neu in .NET8
    ''' Die entsprechenden Einträge stehen in der Datei .\My Project\AssemblyInfo.vb
    '''     ...
    '''     Assembly: AssemblyTrademark("OrgaBack")
    '''     Assembly: AssemblyVersion("4.0.1.0")
    '''     Assembly: AssemblyFileVersion("4.0.1.1")
    '''     Assembly: AssemblyCopyright("©2025")
    '''     Assembly: AssemblyCompany("WinBack GmbH")
    '''     Assembly: AssemblyProduct("OrgaBack-Office")
    '''     ...
    '''     
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub About_WinBack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright & " " & My.Application.Info.CompanyName
        Me.Text = Me.LabelProductName.Text

        'Versions-Historie aus Version.txt anzeigen
        lbVersionsHistorie.Text = "Änderungen und Updates" & vbCrLf & "Weitere Informationen unter www.winback.de"
        lbVersionsHistorie.Links.Clear()
        lbVersionsHistorie.Links.Add(51, 17, "http://www.winback.de")

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
        UpdateWinBack = Nothing
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

    Private Sub BtnUpgrade_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        'Falls die Update-Form nicht(mehr) existiert
        If UpdateWinBack Is Nothing Then
            UpdateWinBack = New wb_Admin_UpdateWinBack
        End If

        'Windows-Installer starten (Update)
        If UpdateWinBack.ExecuteUpdateVersion(BtnUpdate.Tag) Then
            'Programm beenden - wenn Update erfolgreich
            wb_Functions.ExitProgram()
        End If
    End Sub

    Private Sub Wb_Main_About_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Falls die Update-Form nicht(mehr) existiert
        If UpdateWinBack Is Nothing Then
            UpdateWinBack = New wb_Admin_UpdateWinBack
        End If

        Select Case UpdateWinBack.CheckUpdate(False, False)
            Case wb_Global.CompareVersionResult.MinorUpdate
                BtnUpdate.Text = "Upgrade"
                BtnUpdate.Enabled = True
                BtnUpdate.Tag = wb_Global.CompareVersionResult.MinorUpdate
            Case wb_Global.CompareVersionResult.VersionUpdate
                BtnUpdate.Text = "Update"
                BtnUpdate.Enabled = True
                BtnUpdate.Tag = wb_Global.CompareVersionResult.VersionUpdate
            Case Else
                BtnUpdate.Enabled = False
        End Select

        If BtnUpdate.Enabled Then
            LabelOnline.Text = "Update auf Version " & UpdateWinBack.WinBackUpdateVersion
            LabelOnline.Visible = True
        End If
    End Sub

    Private Sub BtnHelp_Click(sender As Object, e As EventArgs) Handles BtnHelp.Click
        'Hilfeseite anzeigen
        wb_Functions.ShowHelp()
        'Dialog-Fenster schliessen
        Close()
    End Sub

    Private Sub lbVersionsHistorie_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbVersionsHistorie.LinkClicked
        System.Diagnostics.Process.Start("explorer.exe", e.Link.LinkData.ToString)
    End Sub

End Class
