Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports WinBack.wb_Rezept_Shared

Public Class wb_Rezept_Liste
    Inherits DockContent
    Const ColumnRzNr = 0

    Private _Anzeige As AnzeigeFilter = AnzeigeFilter.Undefined
    Private _AnzeigeLinienGruppe As Integer = wb_Global.UNDEFINED
    Private _AnzeigeRezeptGruppe As Integer = wb_Global.UNDEFINED

    <CodeAnalysis.SuppressMessage("Major Code Smell", "S2376:Write-only properties should not be used", Justification:="<Ausstehend>")>
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
                Case AnzeigeFilter.LinienGruppe
                    FilterText = "WinBack Rezeptliste Liniengruppe " & _AnzeigeLinienGruppe
                Case AnzeigeFilter.RezeptGruppe
                    FilterText = "WinBack Rezeptliste Rezeptgruppe " & _AnzeigeRezeptGruppe
                Case AnzeigeFilter.Papierkorb
                    FilterText = "WinBack Rezeptliste - alle gelöschten Rezepte"
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
                Filter = "RZ_Liniengruppe > 0"
            Case AnzeigeFilter.Produktion       ' alle aktiven Rohstoffe Typ > 100
                Filter = "RZ_Variante_Nr >= 1 AND RZ_Liniengruppe > 0"
            Case AnzeigeFilter.Sauerteig        ' alle aktiven Rohstoffe Typ 102
                Filter = "RZ_Variante_Nr = 0 AND RZ_Liniengruppe > 0"
            Case AnzeigeFilter.LinienGruppe     ' alle Rezepte mit Liniengruppe X
                Filter = "RZ_Liniengruppe = " & _AnzeigeLinienGruppe.ToString
            Case AnzeigeFilter.RezeptGruppe     ' alle Rezepte mit Rezeptgruppe X
                Filter = "RZ_Gruppe = " & _AnzeigeRezeptGruppe.ToString & " AND RZ_Liniengruppe > 0"
            Case AnzeigeFilter.Papierkorb
                Filter = "RZ_Liniengruppe < 0"
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
        Dim DropDownItems() As ToolStripMenuItem

        'DataGrid Popup-Menu Filter
        Dim evH As New EventHandler(AddressOf DataGridView_PopupClick)
        Dim i As Integer = 0

        DataGridView.PopupItemAdd("Filter:", "Flt", Nothing, evH, True, False)
        DataGridView.PopupItemAdd("nur Produktion", "Prod", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("nur Sauerteig", "Sauer", Nothing, evH, False, True)
        DataGridView.PopupItemAdd("Papierkorb", "Deleted", Nothing, evH, False, True)

        'DataGridView Popup-Menu Filter Liniengruppen SubMenu
        ReDim DropDownItems(Math.Max(wb_Linien_Global.RezeptLinienGruppen.Count - 1, 1))
        i = 0
        'Schleife über alle Linengruppen
        For Each LinienGruppe In wb_Linien_Global.RezeptLinienGruppen
            DropDownItems(i) = New ToolStripMenuItem(LinienGruppe.Value, Nothing, evH)
            DropDownItems(i).Tag = "LGrp#" & LinienGruppe.Key
            i += 1
        Next
        'DataGridView.PopupItemAdd("Liniengruppe", "LGrp", Nothing, False, DirectCast(DropDownItems, ToolStripItem()))

        'DataGridView Popup-Menu Filter Rezeptgruppen SubMenu (der erste Eintrag der Liste ist 0-Leer)
        ReDim DropDownItems(Math.Max(wb_Rezept_Shared.RzGruppe.Count - 2, 1))
        i = 0
        'Schleife über alle Rezeptgruppen
        For Each RezeptGruppe In wb_Rezept_Shared.RzGruppe
            If RezeptGruppe.key > 0 Then
                DropDownItems(i) = New ToolStripMenuItem(RezeptGruppe.Value, Nothing, evH)
                DropDownItems(i).Tag = "RzGrp#" & RezeptGruppe.Key
                i += 1
            End If
        Next
        'DataGridView.PopupItemAdd("Rezeptgruppe", "RzGrp", Nothing, True, DirectCast(DropDownItems, ToolStripItem()))

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlRezeptListe, "RezeptListe")
        'Anzeige-Filter
        Anzeige = AnzeigeFilter.Alle

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
    Private Sub wb_Rezept_Liste_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Event-Handler freigeben
        RemoveHandler wb_Rezept_Shared.eListe_Refresh, AddressOf RefreshData
        RemoveHandler wb_Rezept_Shared.eEdit_Leave, AddressOf SaveData
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("RezeptListe")
    End Sub

    'Datensatz in Datenbank sichern
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused procedure parameters should be removed", Justification:="<Ausstehend>")>
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
        'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen - Kopieren der Rezeptur ist erlaubt
        Dim Rezeptur As New wb_Rezept_Rezeptur(wb_Rezept_Shared.Rezept.RezeptNr, wb_Rezept_Shared.Rezept.Variante, wb_Global.UNDEFINED, True)
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
        'Flag Produktion/Sauerteig/Liniengruppe/Rezeptgruppe
        Dim Tag() As String = Split(CType(sender, System.Windows.Forms.ToolStripMenuItem).Tag, "#")
        Select Case Tag(0)

            Case "Flt"
                'Anzeige alle Rezepte (Filter löschen)
                Me.Anzeige = AnzeigeFilter.Alle
                Dim UnCheckName As New List(Of String) From {"Prod", "Sauer", "Deleted", "LGrp", "RzGrp"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Prod"
                'Anzeige Rezepte Produktion
                Me.Anzeige = AnzeigeFilter.Produktion
                Dim UnCheckName As New List(Of String) From {"Sauer", "Deleted", "LGrp", "RzGrp"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Sauer"
                'Anzeige Rezepte Sauerteig
                Me.Anzeige = AnzeigeFilter.Sauerteig
                Dim UnCheckName As New List(Of String) From {"Prod", "Deleted", "LGrp", "RzGrp"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "Deleted"
                'Anzeige gelöschte Rezepte (Papierkorb)
                Me.Anzeige = AnzeigeFilter.Papierkorb
                Dim UnCheckName As New List(Of String) From {"Prod", "Sauer", "LGrp", "RzGrp"}
                DataGridView.PopupItemsUncheck(UnCheckName)

            Case "LGrp"
                'wenn eine Liniengruppe angegeben ist
                If Tag.Length > 1 Then
                    'Anzeige Rezepte mit Liniengruppe X
                    _AnzeigeLinienGruppe = Tag(1)
                    Me.Anzeige = AnzeigeFilter.LinienGruppe
                    Dim UnCheckName As New List(Of String) From {"Prod", "Sauer", "Deleted", "RzGrp"}
                    DataGridView.PopupItemsUncheck(UnCheckName)
                Else
                    'Anzeige alle Rezepte (Filter löschen)
                    Me.Anzeige = AnzeigeFilter.Alle
                    Dim UnCheckName As New List(Of String) From {"Prod", "Sauer", "Deleted", "LGrp", "RzGrp"}
                    DataGridView.PopupItemsUncheck(UnCheckName)
                End If

            Case "RzGrp"
                'wenn eine Rezeptgruppe angegeben ist
                If Tag.Length > 1 Then
                    'Anzeige Rezepte mit Rezeptgruppe X
                    _AnzeigeRezeptGruppe = Tag(1)
                    Me.Anzeige = AnzeigeFilter.RezeptGruppe
                    Dim UnCheckName As New List(Of String) From {"Prod", "Sauer", "Deleted", "LGrp"}
                    DataGridView.PopupItemsUncheck(UnCheckName)
                Else
                    'Anzeige alle Rezepte (Filter löschen)
                    Me.Anzeige = AnzeigeFilter.Alle
                    Dim UnCheckName As New List(Of String) From {"Prod", "Sauer", "Deleted", "LGrp", "RzGrp"}
                    DataGridView.PopupItemsUncheck(UnCheckName)
                End If

            Case Else
                'Anzeige alle Rezepte (Filter löschen)
                Me.Anzeige = AnzeigeFilter.Undefined

        End Select
    End Sub
End Class