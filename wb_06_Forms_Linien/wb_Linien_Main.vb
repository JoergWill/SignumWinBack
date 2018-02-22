Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking
Imports Signum.OrgaSoft

''' <summary>
''' Anzeige der WinBack-Produktions-Linien über VNC-Viewr
''' </summary>
Public Class wb_Linien_Main
    Implements IExternalFormUserControl

    'Default-Fenster
    Private LinienListe As New wb_Linien_Liste
    Private LinienDetails As New wb_Linien_Details

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
        'verhindert Warnung BC40054
        InitializeComponent()
    End Sub

    Public Overrides Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        LinienListe.LinienInfo()
        LinienListe.SaveItems()
        Return False
    End Function

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Produktions-Linien"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Return "Linien"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        LinienListe.Show(DockPanel, DockState.DockLeft)
        LinienDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("LinienVerwaltung", "WinBack-Produktion Linien", "Winback-Produktion Linien verwalten")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpLinien", "WinBack Linien")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnLinienNew", "Linie Neu", "Neue Linie anlegen", My.Resources.LinienNeu_32x32, My.Resources.LinienNeu_32x32, AddressOf BtnLinienNew)
                oGrp.AddButton("BtnLinienRename", "Bearbeiten", "Linie umbenennen/IP-Adresse einstellen", My.Resources.LinienBearbeiten_32x32, My.Resources.LinienBearbeiten_32x32, AddressOf BtnLinien)
                oGrp.AddButton("BtnLinienRemove", "Löschen", "Linie löschen", My.Resources.LinienLoeschen_32x32, My.Resources.LinienLoeschen_32x32, AddressOf BtnLinienRemove)
                oGrp.AddButton("BtnLinienAutoInstall", "AutoInstall", "Alle Linien automatisch installieren", My.Resources.LinienAutoInstall_32x32, My.Resources.LinienAutoInstall_32x32, AddressOf btnLinienAutoInstall)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Linien_Liste"
                Return LinienListe
            Case "WinBack.wb_Linien_Details"
                Return LinienDetails
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub BtnLinienNew()
        LinienListe.AddItems("", "Neuer Eintrag")
        LinienListe.SelectLastItem()
        LinienDetails.DetailInfo()
        BtnLinien()
    End Sub

    Private Sub BtnLinien()
        LinienDetails.DetailEdit()
    End Sub

    Private Sub BtnLinienRemove()
        LinienListe.RemoveItem()
    End Sub

    Private Sub btnLinienAutoInstall()
        LinienListe.AddFromDataBase()
    End Sub
End Class

