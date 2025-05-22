Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Schnittstelle_Setup
    Inherits DockContent
    Private FeldListe As wb_ArrayGridViewSchnittstelle
    Private sColNames As New List(Of String)
    Private _Index As Integer = wb_Global.UNDEFINED


    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
        End Set
    End Property

    Private Sub wb_Schnittstelle_Setup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Combo-Box Schnittstellen-Tabellen
        cbTabellen.Items.Clear()
        For Each T In wb_Schnittstelle_Shared.Txxxx
            cbTabellen.Items.Add(T.TabName)
        Next

        'Liste der Tabellen-Überschriften
        sColNames.Clear()
        sColNames.AddRange({"&Datenfeld", "Index", "Default", "Pos", "Len", "Calc"})

        'Beim Öffnen der Form (einmalig) aktualisieren
        ShowDetails(sender)

        'Event-Handler (Aktualisierung der Vorschau)
        AddHandler wb_Schnittstelle_Shared.eVorschauAktualisieren, AddressOf ShowFeldListe
        AddHandler wb_Schnittstelle_Shared.eFormatChanged, AddressOf ShowDetails

    End Sub

    Public Sub ShowDetails(sender As Object)
        'erst mal alle Daten laden (in wb_Schnittstelle_Shared)
        Application.DoEvents()

        'Einträge in der Tabelle-Detail-Ansicht löschen
        cbTabellen.SelectedIndex = wb_Global.UNDEFINED

        'Daten aus dem Header
        tbName.Text = wb_Schnittstelle_Shared.Bezeichnung
        tbVersion.Text = wb_Schnittstelle_Shared.Version
        tbDate.Text = wb_Schnittstelle_Shared.Datum
        tbUser.Text = wb_Schnittstelle_Shared.User
        tbFormat.Text = wb_Schnittstelle_Shared.Format
        tbZeichensatz.Text = wb_Schnittstelle_Shared.sEncoding

        'wenn vorhanden wird die erste Tabelle angezeigt
        cbTabellen.SelectedIndex = 0
    End Sub

    Public Sub ShowFeldListe()
        'falls schon Daten vorhanden sind, löschen
        If Not IsNothing(FeldListe) Then
            FeldListe.Dispose()
            FeldListe = Nothing
        End If

        'Daten im Grid anzeigen
        FeldListe = New wb_ArrayGridViewSchnittstelle(wb_Schnittstelle_Shared.getFxxx, sColNames)
        FeldListe.ScrollBars = ScrollBars.Vertical
        FeldListe.BackgroundColor = Me.BackColor
        FeldListe.GridLocation(pnlFelder)
        FeldListe.PerformLayout()
        FeldListe.Refresh()
    End Sub

    Private Sub wb_Schnittstelle_Setup_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler wb_Schnittstelle_Shared.eFormatChanged, AddressOf ShowDetails
        'RemoveHandler eVorschauAktualisieren, AddressOf VorschauAktualisieren
    End Sub

    Private Sub cbTabellen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTabellen.SelectedIndexChanged
        'da die Tabellen in der richtigen Reihenfolge erzeugt werden, ist der Index der Tabellen
        'gleich dem Index der Combo-Box
        Index = cbTabellen.SelectedIndex
        'Index merken für Vorschau/Setup
        wb_Schnittstelle_Shared.SetupTabelleIndex = Index

        If Index >= 0 Then
            'alle Daten zur Tabelle anzeigen - Die Konfiguration wurde in wb_Schnittstelle_shared schon gelesen
            chkImport.Checked = wb_Schnittstelle_Shared.Txxxx(Index).Import
            chkExport.Checked = wb_Schnittstelle_Shared.Txxxx(Index).Export

            tbImportReihenfolge.Text = wb_Schnittstelle_Shared.Txxxx(Index).ImportReihenfolge
            tbExportReihenfolge.Text = wb_Schnittstelle_Shared.Txxxx(Index).ExportReihenfolge

            tbImportMaske.Text = wb_Schnittstelle_Shared.Txxxx(Index).FileMask
            tbExportName.Text = wb_Schnittstelle_Shared.Txxxx(Index).FileName

            tbImportPfad.Text = wb_Schnittstelle_Shared.Txxxx(Index).KonfigImportPath
            tbExportPfad.Text = wb_Schnittstelle_Shared.Txxxx(Index).KonfigExportPath

            ShowFeldListe()
        Else
            chkImport.Checked = False
            chkExport.Checked = False

            tbImportReihenfolge.Text = ""
            tbExportReihenfolge.Text = ""

            tbImportMaske.Text = ""
            tbExportName.Text = ""

            tbImportPfad.Text = ""
            tbExportPfad.Text = ""
        End If
    End Sub
End Class


