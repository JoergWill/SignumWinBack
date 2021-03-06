﻿Imports Signum.OrgaSoft
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports WeifenLuo.WinFormsUI.Docking

Public Class UserControl1
    Implements IExternalFormUserControl

    'Default-Fenster
    Public RohstoffListe As New wb_Rohstoffe_Liste
    Public RohstoffDetails As New wb_Rohstoffe_Details

    'alle anderen Fenster werden zur Laufzeit erzeugt
    Public RohstoffVerwendung As wb_Rohstoffe_Verwendung
    Public RohstoffParameter As wb_Rohstoffe_Parameter

    Public Sub New(ServiceProvider As IOrgasoftServiceProvider)
        MyBase.New(ServiceProvider)
    End Sub

    ''' <summary>
    ''' Fenster-Name (Caption). Wird von Init() aufgerufen
    ''' </summary>
    ''' <returns></returns>
    Public Overrides ReadOnly Property FormText As String
        Get
            Return "WinBack Rohstoff-Verwaltung"
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
            Me.Tag = "Rohstoffe"
            Return "Rohstoffe"
        End Get
    End Property

    Public Overrides Sub SetDefaultLayout()
        RohstoffListe.Show(DockPanel, DockState.DockLeft)
        RohstoffDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Public Shadows ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            If _ContextTabs Is Nothing Then
                _ContextTabs = New List(Of GUI.ITab)
                ' Fügt dem Ribbon ein neues RibbonTab hinzu
                Dim oNewTab = _MenuService.AddContextTab("RohstoffVerwaltung", "WinBack-Rohstoffe", "Verwaltung der WinBack-Rohstoffe")
                ' Das neue RibbonTab erhält eine Gruppe
                Dim oGrp = oNewTab.AddGroup("GrpRohstoffe", "WinBack Rohstoffe")
                ' ... und dieser Gruppe wird ein Button hinzugefügt
                oGrp.AddButton("BtnRohstoffDetails", "Details", "weitere Rohstoff-Daten", My.Resources.RohstoffeDetails_32x32, My.Resources.RohstoffeDetails_32x32, AddressOf BtnRohstoffDetails)
                oGrp.AddButton("BtnRohstoffParameter", "Parameter", "Rohstoffparameter und Nährwert-Angaben", My.Resources.RohstoffeParameter_32x32, My.Resources.RohstoffeParameter_32x32, AddressOf BtnRohstoffParameter)
                oGrp.AddButton("BtnRohstoffVerwendung", "Verwendung", "Verwendung des Rohstoffes in Rezepturen", My.Resources.RohstoffeVerwendung_32x32, My.Resources.RohstoffeVerwendung_32x32, AddressOf BtnRohstoffVerwendung)
                _ContextTabs.Add(oNewTab)
            End If
            Return _ContextTabs.ToArray
        End Get
    End Property

    Private Sub BtnRohstoffDetails()
        RohstoffDetails = New wb_Rohstoffe_Details
        RohstoffDetails.Show(DockPanel, DockState.DockTop)
    End Sub

    Private Sub BtnRohstoffParameter()
        RohstoffParameter = New wb_Rohstoffe_Parameter
        RohstoffParameter.Show(DockPanel, DockState.Document)
    End Sub

    Private Sub BtnRohstoffVerwendung()
        RohstoffVerwendung = New wb_Rohstoffe_Verwendung
        RohstoffVerwendung.Show(DockPanel, DockState.Document)
    End Sub

    Protected Overrides Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString

            Case "WinBack.wb_Rohstoffe_Liste"
                RohstoffListe.CloseButtonVisible = False
                _DockPanelList.Add(RohstoffListe)
                Return RohstoffListe

            Case "WinBack.wb_Rohstoffe_Details"
                RohstoffDetails = New wb_Rohstoffe_Details
                _DockPanelList.Add(RohstoffDetails)
                Return RohstoffDetails

            Case "WinBack.wb_Rohstoffe_Verwendung"
                RohstoffVerwendung = New wb_Rohstoffe_Verwendung
                _DockPanelList.Add(RohstoffVerwendung)
                Return RohstoffVerwendung

            Case "WinBack.wb_Rohstoffe_Parameter"
                RohstoffParameter = New wb_Rohstoffe_Parameter
                _DockPanelList.Add(RohstoffParameter)
                Return RohstoffParameter
            Case Else
                Return Nothing
        End Select
    End Function
End Class
