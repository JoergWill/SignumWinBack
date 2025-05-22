Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

''' <summary>
''' Verwaltung der Textbausteine für:
'''     Produktions-Stufen
'''     Kessel
'''     Textkomponenten
'''     
''' Für die einzelnen Komponenten können Texte vorbelegt werden. Diese Texte werden beim
''' Bearbeiten der Rezepturen als Popup-SubMenu als Default-Text vorgegeben.
''' 
''' Die einzelnen Texte stehen in der Datenbank winback.ItemParameter:
''' 
'''     IP_ItemTyp      IP_ItemID       IP_fdNr     IP_Wert4Str
'''      3010               0            0...x      Texte Produktions-Stufen
'''      3010               1            0...x      Texte Kessel-Stufen
'''      3010               2            0...x      Texte Textkomponente
'''      
''' </summary>
Public Class wb_StammDaten_TextBausteine
    Inherits DockContent

    Const ColNr = 0    'Spalte Nummer
    Const ColBez = 1   'Spalte Bezeichnung
    Const ColID = 2    'Spalte ItemID

    Private _TextBausteinType As Integer = 0    'ItemID

    Private Sub wb_Gruppen_Liste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Liste der Tabellen-Überschriften
        'die mit & gekennzeichnete Spalte wird bei Größenänderung automatisch angepasst
        'Spalten, die mit + beginnen sind editierbar
        'Spalten ohne Bezeichnung werden ausgeblendet
        Dim sColNames As New List(Of String) From {"", "+&Text", ""}
        For Each sName In sColNames
            DataGridView.ColNames.Add(sName)
        Next

        'DataGrid-Felder mit (russischen)Inhalten, bei denen der Zeichensatz konvertiert werden muss
        DataGridView.x8859_5_FieldName = "IP_Wert4str"

        'DataGrid füllen
        DataGridView.LoadData(wb_Sql_Selects.sqlTextBausteine, "TextBausteine")

        'Spalte Nummer zentriert darstellen
        DataGridView.Columns(ColNr).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        'Default Spaltenbreite für Bezeichnung und Wert
        DataGridView.Columns(ColBez).Width = 350
        DataGridView.Columns(ColNr).Width = 150

        'Multi-Select nicht zulassen
        DataGridView.MultiSelect = False
        'Filter
        DataGridView.Filter = "IP_ItemID = " & _TextBausteinType.ToString
    End Sub

    Private Sub wb_StammDaten_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Layout sichern
        DataGridView.SaveToDisk("TextBausteine")
        'Daten aktualisieren
        wb_Rezept_Shared.Reload()
    End Sub

    Private Sub BtnTextBausteinNeu_Click(sender As Object, e As EventArgs) Handles BtnTextBausteinNeu.Click
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'neuen Datensatz anlegen
        Dim TextBausteinNr As Integer = wb_Rezept_Shared.Add_TextBaustein(_TextBausteinType)
        'neuen Datensatz anzeigen
        DataGridView.RefreshData()

        'auf den neuen Datensatz positionieren
        DataGridView.ClearSelection()
        For i = 0 To DataGridView.RowCount - 1
            If DataGridView.Rows(i).Cells(ColNr).Value = TextBausteinNr Then
                DataGridView.Rows(i).Selected = True
            End If
        Next
    End Sub

    Private Sub BtnLoeschen_Click(sender As Object, e As EventArgs) Handles BtnLoeschen.Click
        'Aktuellen Datensatz löschen
        If DataGridView.SelectedRows.Count > 0 Then
            'aktuelle Textbaustein Nummer
            Dim Nr As String = DataGridView.SelectedRows(0).Cells(ColNr).Value.ToString
            'Daten in Datenbank sichern
            DataGridView.UpdateDataBase()
            'Textbaustein löschen
            If wb_Rezept_Shared.Delete_TextBaustein(Nr, _TextBausteinType) Then
                'neuen Datensatz anzeigen
                DataGridView.RefreshData()
            Else
                'Fehlermeldung ausgeben
                MsgBox(wb_Rezept_Shared.ErrorText, MsgBoxStyle.Exclamation, "Text-Baustein löschen")
            End If
        End If
    End Sub

    Private Sub BtnFilterProdStufen_Click(sender As Object, e As EventArgs) Handles BtnFilterProdStufen.Click
        Me.Text = "Texte Produktions-Stufen"
        _TextBausteinType = 0
        RefreshFilter()
    End Sub

    Private Sub BtnFilterKessel_Click(sender As Object, e As EventArgs) Handles BtnFilterKessel.Click
        Me.Text = "Texte Kessel"
        _TextBausteinType = 1
        RefreshFilter()
    End Sub

    Private Sub BtnFilterTexte_Click(sender As Object, e As EventArgs) Handles BtnFilterTexte.Click
        Me.Text = "Texte Textkomponenten"
        _TextBausteinType = 2
        RefreshFilter()
    End Sub

    Private Sub RefreshFilter()
        'Daten in Datenbank sichern
        DataGridView.UpdateDataBase()
        'Filter
        DataGridView.Filter = "IP_ItemID = " & _TextBausteinType.ToString
        'Anzeige Refresh
        DataGridView.RefreshData()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub
End Class


