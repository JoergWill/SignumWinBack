﻿'---------------------------------------------------------
'27.04.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Artikel Verwaltung
'---------------------------------------------------------

Imports System.Windows.Forms
Imports Signum.OrgaSoft.Common
Imports Signum.OrgaSoft.GUI
Imports Signum.OrgaSoft.AddIn.OrgasoftMain.wb_IniFile
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Artikel_Main
    Implements IExternalFormUserControl
    Dim ArtikelListe As New wb_Artikel_Liste
    Dim ArtikelDetails As New wb_Artikel_Details

    Private _ServiceProvider As Common.IOrgasoftServiceProvider
    Private _MenuService As Common.IMenuService
    Private _ViewProvider As IViewProvider

    Private _ContextTabs As List(Of GUI.ITab)

    ''' <summary>
    ''' Konstruktor
    ''' </summary>
    ''' <param name="ServiceProvider">ServiceProvider von OrgaSoft.NET</param>
    ''' <remarks></remarks>
    Public Sub New(ServiceProvider As Common.IOrgasoftServiceProvider)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ServiceProvider = ServiceProvider
        _MenuService = TryCast(ServiceProvider.GetService(GetType(Common.IMenuService)), Common.IMenuService)
        _ViewProvider = TryCast(ServiceProvider.GetService(GetType(IViewProvider)), IViewProvider)

    End Sub

    ''' <summary>
    ''' Routine wird aufgerufen, wenn das Fenster geladen wurde und angezeigt werden soll
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Die Caption des Fensters muss mit MyBase.Text gesetzt werde</remarks>
    Public Function Init() As Boolean Implements IBasicFormUserControl.Init
        MyBase.Text = "WinBack"
        Return True
    End Function

    Public Function ExecuteCommand(CommandId As String, Parameter As Object) As Object Implements IBasicFormUserControl.ExecuteCommand
        Return Nothing
    End Function

    ''' <summary>
    ''' Diese Function wird aufgerufen, wenn das Fenster geschlossen werden soll.
    ''' </summary>
    ''' <param name="Reason"></param>
    ''' <returns>
    ''' False, wenn das Fenster geschlossen werden darf
    ''' True, wenn das Fenster geöffnet bleiben muss
    ''' </returns>
    ''' <remarks></remarks>
    Public Function FormClosing(Reason As Short) As Boolean Implements IBasicFormUserControl.FormClosing
        Return False
    End Function
    Public Event Close(sender As Object, e As EventArgs) Implements IBasicFormUserControl.Close

    Public Sub FormClosed() Implements IBasicFormUserControl.FormClosed
    End Sub

    ''' <summary>
    ''' Eindeutiger Schlüssel für das Fenster, ggf. Firmenname.AddIn
    ''' </summary>
    Public ReadOnly Property FormKey As String Implements IBasicFormUserControl.FormKey
        Get
            Return "@WinBack Artikel-Verwaltung"
        End Get
    End Property
    ''' <summary>
    ''' Minimale Höhe des UserControls
    ''' </summary>
    Public ReadOnly Property MinHeight As Integer Implements IBasicFormUserControl.MinHeight
        Get
            Return Me.MinimumSize.Height
        End Get
    End Property

    ''' <summary>
    ''' Minimale Breite des UserControls
    ''' </summary>
    Public ReadOnly Property MinWidth As Integer Implements IBasicFormUserControl.MinWidth
        Get
            Return Me.MinimumSize.Width
        End Get
    End Property

    ''' <summary>
    ''' Gibt an, ob man die Größe dieses UserControls ändern darf
    ''' </summary>
    Public ReadOnly Property Sizable As Boolean Implements IBasicFormUserControl.Sizable
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Bezeichnung und Caption des UserControls
    ''' </summary>
    Public Shadows ReadOnly Property Text() As String Implements IBasicFormUserControl.Text
        Get
            Return MyBase.Text
        End Get
    End Property

    ''' <summary>
    ''' Erzeugt neue Tabs im Ribbon-Control
    ''' </summary>
    Public ReadOnly Property ContextTabs As GUI.ITab() Implements IExternalFormUserControl.ContextTabs
        Get
            'If _ContextTabs Is Nothing Then
            _ContextTabs = New List(Of GUI.ITab)
            '    ' Fügt dem Ribbon ein neues RibbonTab hinzu
            '    Dim oNewTab = _MenuService.AddContextTab("TestUserControlContextTab", "TestUserContext", "Eigenes ContextTab für TestUserControl")
            '    ' Das neue RibbonTab erhält eine Gruppe
            '    Dim oGrp = oNewTab.AddGroup("ContextFormTest", "ContextFormTest")
            '    ' ... und dieser Gruppe wird ein Button hinzugefügt
            '    ' oGrp.AddButton("TestUserControlButton1", "WhatEver", "Do Something", My.Resources.Search_16, My.Resources.Search_32, AddressOf Search)
            '    _ContextTabs.Add(oNewTab)
            'End If
            Return _ContextTabs.ToArray
        End Get
    End Property
    Private Function wbBuildDocContent(ByVal persistString As String) As WeifenLuo.WinFormsUI.Docking.DockContent
        Select Case persistString
            Case "ArtikelListe"
                Return ArtikelListe
            Case "ArtikelDetails"
                Return ArtikelDetails
            Case Else
                Return Nothing
        End Select
    End Function
    Private Sub wbArtikel_Close()
        DockPanel.SaveAsXml("C:\Users\will.WINBACK\AppData\Roaming\WinBack\test.xml")
    End Sub
    Private Sub wbArtikelLoad()
        Try
            DockPanel.LoadFromXml("C:\Users\will.WINBACK\AppData\Roaming\WinBack\test.xml", AddressOf wbBuildDocContent)
        Catch ex As Exception

        End Try

        ArtikelListe.Show(DockPanel, DockState.Document)
        ArtikelDetails.Show(DockPanel, DockState.DockRight)

        'ArtikelListe.ShowHint = DockState.DockLeft
        'ArtikelDetails.ShowHint = DockState.DockTop

        'ArtikelListe.Show(DockPanel)
        'ArtikelDetails.Show(DockPanel)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        wbArtikelLoad()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        wbArtikel_Close()
    End Sub
End Class