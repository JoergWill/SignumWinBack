Imports WeifenLuo.WinFormsUI.Docking

Public Class wb_Planung_Drucken
    Inherits DockContent

    Private Sub wb_Planung_Drucken_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        clbLiniengruppe.Fill(wb_Linien_Global.RezeptLinienGruppen)
        clbArtikelLinienGruppe.Fill(wb_Linien_Global.ArtikelLinienGruppen)


        'alte Einträge löschen
        ListBox1.Items.Clear()
        Text = ""
        'HashTable aus SortedList
        Dim ht As SortedList
        ht = wb_Linien_Global.ArtikelLinienGruppen
        'Combo-Box mit Werten füllen
        For Each item As DictionaryEntry In ht
            ListBox1.Items.Add(item.Value)
        Next



    End Sub
End Class