Imports System.Drawing
Imports System.Windows.Forms
Imports WinBack.wb_User_Rechte_Shared

Public Class wb_ArrayGridViewUserGruppen
    Inherits wb_ArrayGridView

    Const COLTyp = 0   'Hauptgruppe (1,2,200)
    Const COLIDx = 1   'Untergruppe (1..x)
    Const COLInp = 2   'EingabeTyp (403,405,406...)
    Const COLGrp = 3   'Erste Spalte Gruppe 1....
    Const COLAdm = 11  'Spalte Admin-Rechte

    Public GridArray As Array
    Private _Changed As Boolean = False
    Private _HeaderChanged As Boolean = False

    ''' <summary>
    ''' Flag Daten wurden geändert.
    ''' Beim Schliessen des Formulars muss gespeichert werden.
    ''' </summary>
    ''' <returns></returns>
    Public Property Changed As Boolean
        Get
            Return _Changed
        End Get
        Set(value As Boolean)
            _Changed = value
        End Set
    End Property

    ''' <summary>
    ''' Flag Gruppen-Bezeichnungen wurden geändert.
    ''' Beim Schliessen des Formulars muss gespeichert werden.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HeaderChanged As Boolean
        Get
            Return _HeaderChanged
        End Get
    End Property

    Public Sub New(ByVal xArray As ArrayList, ByVal sColNames As List(Of String), Optional ShowTooltips As Boolean = True)
        'Spalten-Überschriften
        ColNames = sColNames
        'Daten in das lokale Array übertragen
        GridArray = xArray.ToArray
        'Grid Grundeinstellungen
        _ShowTooltips = ShowTooltips
        'Grid initialisieren
        InitGrid()
        'Scrollbar vertikal und horizontal einblenden
        Me.ScrollBars = System.Windows.Forms.ScrollBars.Both
        'Daten anzeigen 
        InitData()
    End Sub

    ''' <summary>
    ''' Grid User-Gruppe-Rechte füllen. Die einzelnen Zeilen werden aus Gruppe 99 erzeugt. 
    ''' Gruppe99 ist die letzte UserGruppe !
    ''' Wichtig: In Gruppe 99 (alle Rechte) müssen alle notwendigen Datensätze richtig eingetragen sein.
    ''' </summary>
    Public Overrides Sub FillGrid()
        'Spalten erstellen
        MyBase.FillColumns()

        ' Die Arraydaten werden in das GridView eingetragen
        Dim rows As DataGridViewRowCollection = MyBase.Rows
        'Letzter Eintrag Gruppe 99
        Dim Grp99 As Integer = GridArray.Length - 1
        Dim MaxRowCount As Integer
        If Grp99 > 0 Then
            MaxRowCount = TryCast(GridArray(Grp99), wb_User_Gruppe).Count
        Else
            MaxRowCount = 0
        End If

        RowHeadersVisible = True
        RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        ColumnHeadersDefaultCellStyle.BackColor = Color.Gray
        RowHeadersDefaultCellStyle = ColumnHeadersDefaultCellStyle
        RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft

        ' Daten Löschen
        MyBase.Rows.Clear()
        RowCount = 0

        ' Die erforderliche Anzahl Zeilen in einem Rutsch erstellen:
        If MaxRowCount > 0 Then
            MyBase.Rows.Add(MaxRowCount)

            'Überschriften ins DatagridView eintragen
            For r = 0 To MaxRowCount - 1
                With rows(r)
                    ' Zeileneigenschaften festlegen: Keine 'verschwindende' Zeile zulassen
                    .MinimumHeight = 20
                    ' Strich zwischen den Zeilen  
                    .DividerHeight = 0
                    'Bezeichnung Gruppen Rechte
                    .HeaderCell.Value = DirectCast(GridArray(Grp99), wb_User_Gruppe).UserRechte(r).Bezeichnung
                    '.Cells(COLBez).Value = DirectCast(GridArray(Grp99), wb_User_Gruppe).UserRechte(r).Bezeichnung
                    'Hauptgruppe
                    .Cells(COLTyp).Value = DirectCast(GridArray(Grp99), wb_User_Gruppe).UserRechte(r).iTyp
                    .Cells(COLIDx).Value = DirectCast(GridArray(Grp99), wb_User_Gruppe).UserRechte(r).iID
                    .Cells(COLInp).Value = DirectCast(GridArray(Grp99), wb_User_Gruppe).iAttrGrp(.Cells(COLTyp).Value, .Cells(COLIDx).Value)

                    'Daten ins DatagridView eintragen
                    For i = 0 To Grp99
                        .Cells(i + COLGrp).Value = DirectCast(GridArray(i), wb_User_Gruppe).iAtttribut(.Cells(COLTyp).Value, .Cells(COLIDx).Value)
                    Next
                End With
            Next
        End If
    End Sub

    Private Sub UserGruppen_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        'Debug.Print("UserGrpCell " & c.ToString & "/" & r.ToString & " - " & v.ToString)
        Dim Col As Integer = e.ColumnIndex
        Dim Row As Integer = e.RowIndex
        'Funktions-Type (402..405)
        Dim Tpe As Integer = Rows(Row).Cells(COLInp).Value
        'Value (0,1,2..)
        If e.Value IsNot Nothing Then
            Dim Val As Integer = wb_Functions.StrToInt(e.Value.ToString)

            'Text farbig und formatiert darstellen
            If Col > COLInp Then
                If Val = 0 Then
                    'Hintergrund-Farbe rot
                    e.CellStyle.BackColor = Color.LightSalmon
                    'Kein Text
                    e.Value = ""
                Else
                    'Hintergrund-Farbe grün
                    e.CellStyle.BackColor = Color.LightGreen
                    'Text aus Tabelle Texte in der richtigen Sprache
                    e.Value = wb_User_Rechte_Shared.Text(Tpe, Val)
                    'Text zentriert darstellen
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Flag setzen/rücksetzen.
    ''' Wenn die Zeile schon markiert war, wird das Flag getoggelt.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub UserGruppen_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellClick
        'Debug.Print("Cell Click " & e.ColumnIndex & "/" & e.RowIndex)
        Dim Col As Integer = e.ColumnIndex
        Dim Row As Integer = e.RowIndex

        If Row >= 0 AndAlso Col >= COLGrp Then
            'Funktions-Type (402..405)
            Dim Tpe As Integer = Rows(Row).Cells(COLInp).Value
            'Value (0,1,2..)
            Dim Val As Integer = wb_Functions.StrToInt(CurrentCell.Value.ToString)

            'wenn die Zeile schon markiert war
            CurrentCell.Value = wb_User_Rechte_Shared.Click(Tpe, Val)
            'Flag Daten wurden geändert
            _Changed = True
            'geänderte Spalte merken
            Columns(e.ColumnIndex).HeaderCell.Tag = "C"
        End If
    End Sub

    ''' <summary>
    ''' Gruppen-Bezeichnung editieren.
    ''' Die Texte zur User-Gruppe stehen in der Tabelle Texte T_Typ=500
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub wb_User_Gruppen_ColumnHeaderDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.ColumnHeaderMouseDoubleClick
        Dim NewHeaderText As String = InputBox("Neue Gruppen-Bezeichnung", "Gruppen-Bezeichnung ändern")
        If NewHeaderText <> "" Then
            Columns(e.ColumnIndex).HeaderText = NewHeaderText
            _HeaderChanged = True
        End If
    End Sub

End Class
