Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Rezept_Shared

Public Class wb_Rezept_Liste
    Inherits DockContent
    Const ColumnRzNr = 0

    Private _Anzeige As AnzeigeFilter = AnzeigeFilter.Undefined

    Public WriteOnly Property Anzeige As AnzeigeFilter
        Set(value As AnzeigeFilter)
            _Anzeige = value
            SetDataGridViewFilter()
        End Set
    End Property

    Public ReadOnly Property FilterText As String
        Get
            Select Case _Anzeige
                Case AnzeigeFilter.Alle
                    FilterText = "WinBack Rezeptliste"
                Case AnzeigeFilter.Produktion
                    FilterText = "WinBack Rezeptliste Produktion"
                Case AnzeigeFilter.Sauerteig
                    FilterText = "WinBack Rezeptliste Sauerteig"
                Case Else
                    FilterText = "WinBack Rezeptliste"
            End Select
        End Get
    End Property

    Private Sub SetDataGridViewFilter()
        'Filterstring
        Dim Filter As String = ""

        'Filter Hand/Auto/Sauer/Installation
        Select Case _Anzeige
            Case AnzeigeFilter.Alle
                Filter = ""
            Case AnzeigeFilter.Produktion        ' alle aktiven Rohstoffe Typ > 100
                Filter = "RZ_Variante_Nr >= 1"
            Case AnzeigeFilter.Sauerteig        ' alle aktiven Rohstoffe Typ 102
                Filter = "RZ_Variante_Nr = 0"
        End Select

        'Filter anwenden
        DataGridView.Filter = Filter
        'FensterText anzeigen
        Me.Text = FilterText
    End Sub

    'Event Form wird geladen
    Private Sub wb_Rezept_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "Nummer", "&Name", "Variante"}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "RZ_Bezeichnung"

        'DataGrid Popup-Menu Filter
        Dim evH As New EventHandler(AddressOf DataGridView_PopupClick)
        DataGridView.PopupItemAdd("Filter:", "Flt", Nothing, evH, True, False)
        DataGridView.PopupItemAdd("nur Produktion", "Prod", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("nur Sauerteig", "Sauer", Nothing, evH, True, True)

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRezeptListe, "RezeptListe")

        AddHandler wb_Rezept_Shared.eEdit_Leave, AddressOf SaveData
        AddHandler wb_Rezept_Shared.eListe_Refresh, AddressOf RefreshData
    End Sub

    ''' <summary>
    ''' Liste neu laden nach Löschen/Ändern Rezeptur
    ''' </summary>
    Public Sub RefreshData(sender As Object)
        'Daten neu einlesen
        DataGridView.RefreshData()
    End Sub

    Public Sub RefreshData(RezeptNr As Integer)
        DataGridView.RefreshData()
        DataGridView.SelectData(ColumnRzNr, RezeptNr.ToString)
        'Event auslösen - Aktualisierung der Anzeige in den Detail-Fenstern
        wb_Rezept_Shared.Liste_Click(Nothing)
    End Sub

    'Event Form wird geschlossen
    Private Sub wb_Rezept_Liste_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Event-Handler freigeben
        RemoveHandler wb_Rezept_Shared.eListe_Refresh, AddressOf RefreshData
        RemoveHandler wb_Rezept_Shared.eEdit_Leave, AddressOf SaveData
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("RezeptListe")
    End Sub

    'Datensatz in Datenbank sichern
    Private Sub SaveData(sender As Object)
        'Daten in Datenbank sichern
        If wb_Rezept_Shared.Rezept.SaveData(DataGridView) Then
            DataGridView.UpdateDataBase()
        End If
    End Sub

    Private Sub DataGridView_HasChanged(sender As Object, e As EventArgs) Handles DataGridView.HasChanged
        wb_Rezept_Shared.Rezept.LoadData(DataGridView)
        wb_Rezept_Shared.Liste_Click(Nothing)
    End Sub

    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView.DoubleClick
        Me.Cursor = Cursors.WaitCursor
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen
        Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.Rezept.RezeptNr, wb_Rezept_Shared.Rezept.Variante)
        Rezeptur.Show()
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' PopUp-Menu Click.
    ''' Filter Rezept-Liste Produktion/Sauerteig
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_PopupClick(ByVal sender As Object, ByVal e As EventArgs)
        'Flag Produktion/Sauerteig
        Select Case CType(sender, Windows.Forms.ToolStripMenuItem).Tag

            Case "All"
                'Anzeige alle Rezepte (Filter löschen)
                Me.Anzeige = AnzeigeFilter.Alle
                Dim UnCheckName As New List(Of String) From {"Prod", "Sauer"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Prod"
                'Anzeige Rezepte Produktion
                Me.Anzeige = AnzeigeFilter.Produktion
                Dim UnCheckName As New List(Of String) From {"Sauer"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Sauer"
                'Anzeige Rezepte Sauerteig
                Me.Anzeige = AnzeigeFilter.Sauerteig
                Dim UnCheckName As New List(Of String) From {"Prod"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case Else
                'Anzeige alle Rezepte (Filter löschen)
                Me.Anzeige = AnzeigeFilter.Undefined

        End Select
    End Sub
End Class