﻿''' <summary>
''' Ableitung der Klasse Windows.Forms.ComboBox
'''
''' wbComboBox.Fill füllt die ComboBox mit Texten
''' aus einer Hash-Table. Der entsprechende Keys
''' der HashTable setzt/liest den Selectierten Text.
'''
''' </summary>
Partial Class wb_ComboBox
    Inherits Windows.Forms.ComboBox
    Dim ht As SortedList

    ''' <summary>
    '''´Gibt den Key aus HashTable zurück, der dem selektierten Text entspricht
    ''' </summary>
    ''' <returns>Integer Key</returns>
    Public Function GetKeyFromSelection() As Integer
        Return GetKeyFromText(Me.SelectedItem)
    End Function

    ''' <summary>
    '''´Gibt den Key aus HashTable zurück, der dem übergebenen Text entspricht
    ''' </summary>
    ''' <returns>Integer Key</returns>
    Public Function GetKeyFromText(Text As String) As Integer
        Dim i As Integer = 0
        If ht IsNot Nothing Then
            For Each item As DictionaryEntry In ht
                If item.Value = Text Then
                    i = item.Key
                    Exit For
                End If
            Next
        End If
        Return i
    End Function

    Public Function GetKeyFromIndex(Index As Integer) As Integer
        If Index <= Items.Count Then
            Return GetKeyFromText(Items(Index).ToString)
        Else
            Return wb_Global.UNDEFINED
        End If
    End Function

    ''' <summary>
    ''' Setzt den selektierten Text entsprechend dem Key aus HashTable
    ''' </summary>
    ''' <param name="Key">Schlüssel</param>
    Public Sub SetTextFromKey(Key As Integer)
        Dim i As Integer
        For i = 0 To Items.Count - 1
            If ht(Key) = Items(i).ToString Then
                SelectedIndex = i
                Me.Text = ht(Key)
                Exit Sub
            End If
        Next
        SelectedIndex = -1
    End Sub

    ''' <summary>
    ''' ComboBox mit Texten aus HashTable füllen.
    ''' </summary>
    ''' <param name="HashTable"></param>
    Public Sub Fill(HashTable As SortedList, Optional FilterAlle As Boolean = False)
        'alte Einträge löschen
        Items.Clear()
        Text = ""
        'per Default Filterauswahl Alle
        If FilterAlle Then
            Text = wb_Global.TextAlle
            Items.Add(Text)
        End If
        'HashTable aus SortedList
        ht = HashTable
        'Combo-Box mit Werten füllen
        For Each item As DictionaryEntry In ht
            Items.Add(item.Value)
        Next
    End Sub

End Class
