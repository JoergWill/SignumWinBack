Public Class wb_SyncItem
    Implements IComparable

    Public Sort As String
    Public SyncOK As wb_Global.SyncState    'Synchronisations-Status
    Public wb_Nummer As String              'Nummer in WinBack
    Public wb_Bezeichnung As String         'Bezeichnung in WinBack
    Public os_Nummer As String              'Nummer in OrgaSoft
    Public os_Bezeichnung As String         'Bezeichnung in OrgaSoft

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return String.Compare(Sort, DirectCast(obj, wb_SyncItem).Sort)
    End Function

    Public Sub Merge(obj As wb_SyncItem)
        os_Bezeichnung += obj.os_Bezeichnung
        os_Nummer += obj.os_Nummer
        wb_Bezeichnung += obj.wb_Bezeichnung
        wb_Nummer += obj.wb_Nummer
    End Sub
End Class
