Imports System.Windows.Forms
''' <summary>
''' Ableitung der Klasse System.Windows.Forms.ComboBox
'''
''' wbComboBox.Fill füllt die ComboBox mit Texten
''' aus einer Hash-Table. Der entsprechende Keys
''' der HashTable setzt/liest den Selectierten Text.
'''
''' </summary>
Partial Class wb_ComboBox
    Inherits System.Windows.Forms.ComboBox
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
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
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
    ''' Setzt den selektierten Text entsprechend dem Key aus HashTable.
    ''' Achtung: Beim Ändern von Index oder Text wird der Focus auf die Combo-Box gesetzt
    ''' </summary>
    ''' <param name="Key">Schlüssel</param>
    <CodeAnalysis.SuppressMessage("Major Code Smell", "S3385:""Exit"" statements should not be used", Justification:="<Ausstehend>")>
    Public Sub SetTextFromKey(Key As Integer)
        Dim i As Integer
        For i = 0 To Items.Count - 1
            If ht(Key) = Items(i).ToString Then
                SelectedIndex = i
                Me.Text = ht(Key)
                Exit Sub
            End If
        Next
        Me.Text = ""
        SelectedIndex = -1
    End Sub

    ''' <summary>
    ''' ComboBox mit Texten aus HashTable füllen.
    ''' </summary>
    ''' <param name="HashTable"></param>
    Public Sub Fill(HashTable As SortedList, Optional FilterAlle As Boolean = False, Optional SelectNo As Boolean = False)
        'alte Einträge löschen
        Items.Clear()
        Text = ""
        'per Default Filterauswahl Alle
        If FilterAlle Then
            Items.Add(wb_Global.TextAlle)
        End If
        'per Default Filterauswahl keine
        If SelectNo Then
            Items.Add(wb_Global.TextKeine)
        End If
        'HashTable aus SortedList
        ht = HashTable
        'Combo-Box mit Werten füllen
        For Each item As DictionaryEntry In ht
            Items.Add(item.Value)
        Next
    End Sub

End Class
