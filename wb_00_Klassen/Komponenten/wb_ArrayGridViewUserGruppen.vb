Imports System.Drawing
Imports System.Windows.Forms

Public Class wb_ArrayGridViewUserGruppen
    Inherits wb_ArrayGridView

    Public GridArray As Array
    Private _RowIndex As Integer = wb_Global.UNDEFINED
    Private _Changed As Boolean = False

    Const COLBez = 0   'Bezeichnung GruppenRecht
    Const COLTyp = 1   'Hauptgruppe (1,2,200)
    Const COLIDx = 2   'Untergruppe (1..x)
    Const COLInp = 3   'EingabeTyp (403,405,406...)
    Const COLGrp = 4   'Erste Spalte Gruppe 1....

    ''' <summary>
    ''' Flag Daten wurden geändert.
    ''' Beim Schliessen des Formulars muss gespeichert werden.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Changed As Boolean
        Get
            Return _Changed
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
        'Daten anzeigen 
        InitData()
    End Sub

    Public Overrides Sub FillGrid()

        'Spalten erstellen
        MyBase.FillColumns()

        ' Die Arraydaten werden in das GridView eingetragen
        Dim rows As DataGridViewRowCollection = MyBase.Rows
        'Letzter Eintrag Gruppe 99
        Dim Grp99 As Integer = GridArray.Length - 1
        Dim MaxRowCount As Integer = TryCast(GridArray(Grp99), wb_User_Gruppe).count
        SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' Daten Löschen
        MyBase.Rows.Clear()
        MyBase.RowCount = 0

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
                    .Cells(COLBez).Value = DirectCast(GridArray(Grp99), wb_User_Gruppe).UserRechte(r).Bezeichnung
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
        'Funktions-Type (402..405)
        Dim Tpe As Integer = Rows(Row).Cells(COLInp).Value
        'Value (0,1,2..)
        Dim Val As Integer = wb_Functions.StrToInt(CurrentCell.Value.ToString)

        'wenn die Zeile schon markiert war
        If (Row = _RowIndex) And (Col > COLInp) Then
            CurrentCell.Value = wb_User_Rechte_Shared.Click(Tpe, Val)
            'Flag Daten wurden geändert
            _Changed = True
        End If

        'Zeile merken
        _RowIndex = e.RowIndex
    End Sub

End Class
