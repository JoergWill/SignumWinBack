''' <summary>
'''Ableitung der Klasse wb_DataGridView.
'''Anzeige der Rohstoff-Verwendung in Rezepturen
''' </summary>
Public Class wb_DataGridViewVerwendung
    Inherits Global.WinBack.wb_DataGridView
    Private VerwendungRezept As Boolean = False
    'Die RezeptNummer steht in Spalte 1
    Const ColRezNr As Integer = 0

    Public Sub LoadVerwendung(Nr As Integer)
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet.
        'Die Rezept-Variante wird nicht mit ausgegeben, da sonst eine Exception auftritt
        Dim sColNames As New List(Of String) From {"", "Nr", "&Bezeichnung"}
        For Each sName In sColNames
            ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        MyBase.x8859_5_FieldName = "RZ_Bezeichnung"
        'DataGrid füllen
        LoadData(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRohstoffUse, Nr), "RohstoffVerwendung")
    End Sub

    Public Sub LoadRezeptVerwendung(Nr As Integer)
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten ohne Bezeichnung werden ausgeblendet.
        Dim sColNames As New List(Of String) From {"Nr", "&Bezeichnung", "Kommentar"}
        For Each sName In sColNames
            ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        MyBase.x8859_5_FieldName = "KO_Bezeichnung"
        'DataGrid füllen
        LoadData(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptVerwendung, Nr), "RezeptVerwendung")
        'Anzeige Verwendung Rezept im Artikel - Kein Doppelclick auf die Liste erlaubt
        VerwendungRezept = True
    End Sub

    Public Sub ClearVerwendung()
        ClearData()
    End Sub

    Private Overloads Sub DataGridView_CellDoubleClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellDoubleClick
        If Not VerwendungRezept Then
            'Zeile im Grid
            Dim eRow As Integer = e.RowIndex
            'Die RezeptNummer steht in Spalte 1
            Dim RezeptNr As Integer = Item(ColRezNr, eRow).Value
            'Wenn die Rezeptnummer gültig ist
            If RezeptNr > 0 Then
                Me.Cursor = Windows.Forms.Cursors.WaitCursor
                'Beim Erzeugen des Fensters werden die Daten aus der Datenbank gelesen (immer Variante 1)
                Dim Rezeptur As New wb_Rezept_Rezeptur(RezeptNr, wb_Global.RezeptVarianteStandard)
                Rezeptur.Show()
                Me.Cursor = Windows.Forms.Cursors.Default
            End If
        End If
    End Sub

End Class
