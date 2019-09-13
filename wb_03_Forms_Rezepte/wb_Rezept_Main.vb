Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Rezept_Main
    Implements IExternalFormUserControl

    'Default Fenster
    Public RezeptListe As New wb_Rezept_Liste
    Public RezeptDetails As New wb_Rezept_Details

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public RezeptHinweise As wb_Rezept_Hinweise
    Public RezeptHistorie As wb_Rezept_Historie
    Public RezeptVerwendung As wb_Rezept_Verwendung

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Rezeptverwaltung"
        End Get
    End Property

    ''' <summary>
    ''' Eindeutiger Name für die Basis-Form. 
    ''' Unter diesem Namen werden die Einstellungen in der winback.ini gespeichert.
    ''' 
    ''' Die DockPanel-Konfiguration wird gespeichert unter wbXXXXYYYY.xml, dabei ist XXXX der FormName und YYYY der Layout-Name.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormName As String
        Get
            Return "Rezepte"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        RezeptDetails.Show(DockPanel, DockState.DockTop)
        RezeptDetails.CloseButtonVisible = False
        RezeptListe.Show(DockPanel, DockState.DockLeft)
        RezeptListe.CloseButtonVisible = False
    End Sub

    ''' <summary>
    ''' Diese Function wird aufgerufen, wenn das Fenster geschlossen werden soll.
    ''' </summary>
    ''' <param name="Reason"></param>
    ''' <returns>
    ''' False, wenn das Fenster geschlossen werden darf
    ''' True, wenn das Fenster geöffnet bleiben muss
    ''' </returns>
    ''' <remarks></remarks>
    Public Overrides Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        'Rohstoff-Liste (ordentlich) schliessen - Speichert die Grid-Einstellungen
        If RezeptListe IsNot Nothing Then
            RezeptListe.Close()
        End If
        'Fenster darf geschlossen werden
        Return False
    End Function

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("RezeptVerwaltung", "WinBack-Rezepte", "Verwaltung der WinBack-Rezepturen")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrpRzpt = oNewTab.AddGroup("Verwaltung", "Rezepte")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpRzpt.AddButton("BtnRezeptNeu", "Neu anlegen", "Leeres Rezept neu anlegen", My.Resources.RezeptNeu_32x32, My.Resources.RezeptNeu_32x32, AddressOf BtnRezeptNew)
                oGrpRzpt.AddButton("BtnRezeptRemove", "Rezept(e) löschen", "Rezept/Rezepte löschen", My.Resources.RezeptLoeschen_32x32, My.Resources.RezeptLoeschen_32x32, AddressOf BtnRezeptRemove)
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpRezepte", "Ansicht")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnRezeptListe", "Rezepte Liste", "Liste aller Rezepte anzeigen", My.Resources.RezeptListe_32x32, My.Resources.RezeptListe_32x32, AddressOf BtnRezeptListe)
                oGrp.AddButton("BtnRezeptDetails", "Rezept Details", "Fenster Rezept-Details", My.Resources.RezeptDetails_32x32, My.Resources.RezeptDetails_32x32, AddressOf BtnRezeptDetails)
                oGrp.AddButton("BtnRezeptHinweise", "Rezept Hinweise", "Rezept Verarbeitungshinweise", My.Resources.RezeptHinweise_32x32, My.Resources.RezeptHinweise_32x32, AddressOf BtnRezeptHinweise)
                oGrp.AddButton("BtnRezeptHistorie", "Rezept Historie", "Anzeige der Änderungshistorie der Rezeptur", My.Resources.RezeptHistorie_32x32, My.Resources.RezeptHistorie_32x32, AddressOf BtnRezeptHistorie)
                oGrp.AddButton("BtnRezeptVerwendung", "Rezept Verwendung", "Liste aller Artikel mit Bezug zu diesem Rezept", My.Resources.RezeptVerwendung_32x32, My.Resources.RezeptVerwendung_32x32, AddressOf BtnRezeptVerwendung)
                Dim oGrpPrnt = oNewTab.AddGroup("Printer", "Drucken")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrpPrnt.AddButton("BtnRezeptListeDrucken", "Drucke Rezeptliste", "Liste aller Rezepte drucken", My.Resources.RezeptDruckenListe_32x32, My.Resources.RezeptDruckenListe_32x32, AddressOf BtnRezeptListeDrucken)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub BtnRezeptNew()
        Dim Rezept As New wb_Rezept
        Dim RezeptNrNeu As Integer = Rezept.MySQLdbNew(1)
        RezeptListe.RefreshData(RezeptNrNeu)
        Rezept = Nothing
    End Sub

    Private Sub BtnRezeptRemove()
        Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.Rezept.RezeptNr, 1)
        Rezeptur.RzptLoeschen()
    End Sub

    Private Sub BtnRezeptListe()
        RezeptListe.Show(DockPanel, DockState.DockLeft)
    End Sub

    Private Sub BtnRezeptDetails()
        RezeptDetails.Show(DockPanel)
    End Sub

    Private Sub BtnRezeptHinweise()
        If IsNothingOrDisposed(RezeptHinweise) Then
            RezeptHinweise = New wb_Rezept_Hinweise
        End If
        RezeptHinweise.Show(DockPanel)
    End Sub

    Private Sub BtnRezeptHistorie()
        If IsNothingOrDisposed(RezeptHistorie) Then
            RezeptHistorie = New wb_Rezept_Historie
        End If
        RezeptHistorie.Show(DockPanel)
    End Sub

    Private Sub BtnRezeptVerwendung()
        If IsNothingOrDisposed(RezeptVerwendung) Then
            RezeptVerwendung = New wb_Rezept_Verwendung
        End If
        RezeptVerwendung.Show(DockPanel)
    End Sub

    Private Sub BtnRezeptListeDrucken()
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "WinBack.wb_Rezept_Liste"
                RezeptListe.CloseButtonVisible = False
                _DockPanelList.Add(RezeptListe)
                Return RezeptListe
            Case "WinBack.wb_Rezept_Details"
                RezeptDetails.CloseButtonVisible = True
                _DockPanelList.Add(RezeptListe)
                Return RezeptDetails
            Case "WinBack.wb_Rezept_Hinweise"
                RezeptHinweise = New wb_Rezept_Hinweise
                _DockPanelList.Add(RezeptHinweise)
                Return RezeptHinweise
            Case "WinBack.wb_Rezept_Historie"
                RezeptHistorie = New wb_Rezept_Historie
                _DockPanelList.Add(RezeptHistorie)
                Return RezeptHistorie
            Case "WinBack.wb_Rezept_Verwendung"
                RezeptVerwendung = New wb_Rezept_Verwendung
                _DockPanelList.Add(RezeptVerwendung)
                Return RezeptVerwendung
            Case Else
                Return Nothing
        End Select
    End Function

End Class
