Public MustInherit Class wb_Sync
    Friend _Data As New ArrayList
    Friend _Item As wb_SyncItem

    Public MustOverride Function DBRead() As Boolean
    Public MustOverride Function DBUpdate(Nr As String, Text As String) As Boolean
    Public MustOverride Function DBInsert(Nr As String, Text As String) As Boolean

    Public ReadOnly Property Data As IList(Of wb_SyncItem)
        Get
            Return _Data
        End Get
    End Property

    Public Sub CheckSync(SyncData As ArrayList)
        Debug.Print("Vor ADD")
        PrintSync()
        'beide Arrays aneinanderhängen
        _Data.AddRange(SyncData)
        Debug.Print("NACH ADD")
        PrintSync()
        'nach Nummer(Sortiertkriterium) sortieren
        _Data.Sort()
        Debug.Print("NACH SORT")
        PrintSync()
        'doppelte Einräge zusammenfassen
        DelDubletten()
        PrintSync()


    End Sub

    Private Sub DelDubletten()
        Dim _DataCount As Integer = _Data.Count
        Dim i As Integer = 1
        Dim y As wb_SyncItem = Nothing
        Dim x As wb_SyncItem = Nothing

        'Schleife über alle Datensätze
        While i < _DataCount
            x = _Data(i - 1)
            y = _Data(i)
            'wenn beide Nummern identisch sind
            If x.Sort = y.Sort Then
                'Daten zusammenfassen (WinBack und OrgaBack)
                x.Merge(y)
                'überflüssigen Datensatz löschen
                _Data.Remove(y)
                _DataCount -= 1
            End If
            'weiter mit dem nächsten Datensatz
            i += 1
        End While
    End Sub

    Public Sub PrintSync()
        For Each x As wb_SyncItem In _Data
            Debug.Print(" wb " & x.wb_Nummer & " " & x.wb_Bezeichnung & vbTab & "os " & x.os_Nummer & " " & x.os_Bezeichnung)
        Next
    End Sub
End Class
