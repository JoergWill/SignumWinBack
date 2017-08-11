Public Class wb_DockBarPanelSaveAs
    Private _FormName As String
    Public Event eSaveAs_Click(sender As Object, FileName As String, DefaultPath As wb_Global.OrgaBackDockPanelLayoutPath)

    Public Sub New(FormName As String)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _FormName = FormName
    End Sub

    Private Sub wb_DockBarPanelSaveAs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste alle Konfigurations-Dateien im Verzeichnis
        Dim ConfigFileNames As New List(Of String)

        'Globales Verzeichnis ..\Temp\00
        ConfigFileNames = wb_DockBarPanelMain.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal), _FormName)
        For Each x In ConfigFileNames
            clLayouts.Items.Add(x)
            clLayouts.SetItemChecked(clLayouts.Items.Count - 1, True)
        Next

        'Arbeitsplatz Verzeichnis ..\Temp\xx
        ConfigFileNames = wb_DockBarPanelMain.GetDkPnlConfigNameList(wb_GlobalSettings.DockPanelPath(wb_Global.OrgaBackDockPanelLayoutPath.UserLokal), _FormName)
        For Each x In ConfigFileNames
            clLayouts.Items.Add(x)
        Next

        'Liste sortieren
        'clLayouts.Sorted = vbTrue
    End Sub

    Private Sub clLayouts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clLayouts.SelectedIndexChanged
        tbBezeichnung.Text = clLayouts.SelectedItem
        cbGlobal.Checked = clLayouts.GetItemChecked(clLayouts.SelectedIndex)
        cbGlobal.Enabled = False
    End Sub

    Private Sub BtnSpeichern_Click(sender As Object, e As EventArgs) Handles BtnSpeichern.Click
        'wenn sich die Bezeichnung geändert hat, unter einem anderen Namen abspeichern
        If tbBezeichnung.Text <> "" And tbBezeichnung.Text <> clLayouts.Text Then
            If cbGlobal.Checked Then
                RaiseEvent eSaveAs_Click(sender, tbBezeichnung.Text, wb_Global.OrgaBackDockPanelLayoutPath.ProgrammGlobal)
            Else
                RaiseEvent eSaveAs_Click(sender, tbBezeichnung.Text, wb_Global.OrgaBackDockPanelLayoutPath.UserLokal)
            End If
        End If
        'Form schliessen
        Me.Close()
    End Sub

    Private Sub BtnAbbruch_Click(sender As Object, e As EventArgs) Handles BtnAbbruch.Click
        'Form schliessen
        Me.Close()
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click

    End Sub

    Private Sub tbBezeichnung_KeyUp(sender As Object, e As Windows.Forms.KeyEventArgs) Handles tbBezeichnung.KeyUp
        cbGlobal.Enabled = True
    End Sub
End Class