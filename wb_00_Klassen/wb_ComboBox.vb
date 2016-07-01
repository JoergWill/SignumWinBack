'---------------------------------------------------------
'19.05.2016/ V0.9/JW            :Neuanlage
'Bearbeitet von                 :Will
'
'Änderungen:
'---------------------------------------------------------
'Beschreibung:
'Ableitung der Klasse Windows.Forms.ComboBox
'
'wbComboBox.Fill füllt die ComboBox mit Texten
'aus einer Hash-Table. Der entsprechende Keys
'der HashTable setzt/liest den Selectierten Text.
'

Partial Class wb_ComboBox
    Inherits Windows.Forms.ComboBox
    Dim ht As SortedList

    'Gibt den Key aus HashTable zurück, der dem selektierten Text entspricht
    Function GetKeyFromSelection() As Integer
        Dim i As Integer = 0
        For Each item As DictionaryEntry In ht
            If item.Value = Me.Text Then
                i = item.Key
                Exit For
            End If
        Next
        Return i
    End Function

    'Setzt den selektierten Text entsprechend dem Key aus HashTable
    Public Sub SetTextFromKey(Key As Integer)
        Dim i As Integer
        For i = 0 To Items.Count - 1
            If ht(Key) = Items(i).ToString Then
                SelectedIndex = i
                Exit Sub
            End If
        Next
        SelectedIndex = -1
    End Sub

    'ComboBox mit Texten aus HashTable füllen.
    Public Sub Fill(HashTable As SortedList)
        ht = HashTable
        'Combo-Box mit Werten füllen
        For Each item As DictionaryEntry In ht
            Items.Add(item.Value)
        Next
    End Sub

End Class
