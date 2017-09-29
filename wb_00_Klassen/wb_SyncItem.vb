Public Class wb_SyncItem
    Implements IComparable

    Public Sort As String
    Public SyncOK As wb_Global.SyncState    'Synchronisations-Status
    Public ToolTipText As String            'Hilfe-Anzeige bei Fehlern in der Synchronistion
    Public wb_Nummer As String              'Nummer in WinBack
    Public wb_Bezeichnung As String         'Bezeichnung in WinBack
    Public wb_Gruppe As String              'Gruppe in WinBack
    Public os_Nummer As String              'Nummer in OrgaSoft
    Public os_Bezeichnung As String         'Bezeichnung in OrgaSoft
    Public os_Gruppe As String              'Gruppe in OrgaSoft

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return String.Compare(Sort, DirectCast(obj, wb_SyncItem).Sort)
    End Function

    Public Function Merge(obj As wb_SyncItem) As Boolean
        If os_Bezeichnung = "" And obj.os_Bezeichnung <> "" Then
            os_Bezeichnung = obj.os_Bezeichnung
            os_Gruppe = obj.os_Gruppe
            os_Nummer += obj.os_Nummer
            Return True
        End If
        If wb_Bezeichnung = "" And obj.wb_Bezeichnung <> "" Then
            wb_Bezeichnung = obj.wb_Bezeichnung
            wb_Gruppe = obj.wb_Gruppe
            wb_Nummer += obj.wb_Nummer
            Return True
        End If
        Return False
    End Function
End Class
