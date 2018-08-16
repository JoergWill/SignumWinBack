Imports WinBack

Public Class wb_SyncItem
    Implements IComparable

    Private _sort As String
    Private _syncOK As wb_Global.SyncState = wb_Global.SyncState.NOK    'Synchronisations-Status
    Private _toolTipText As String                                      'Hilfe-Anzeige bei Fehlern in der Synchronistion
    Private _wb_Nummer As String                                        'Nummer in WinBack
    Private _wb_Bezeichnung As String                                   'Bezeichnung in WinBack
    Private _wb_Gruppe As String                                        'Gruppe in WinBack
    Private _os_Nummer As String                                        'Nummer in OrgaSoft
    Private _os_Bezeichnung As String                                   'Bezeichnung in OrgaSoft
    Private _os_Gruppe As String                                        'Gruppe in OrgaSoft

    Public Property Os_Nummer As String
        Get
            Return _os_Nummer
        End Get
        Set(value As String)
            _os_Nummer = value
        End Set
    End Property

    Public Property Sort As String
        Get
            Return _sort
        End Get
        Set(value As String)
            _sort = value
        End Set
    End Property

    Public Property SyncOK As wb_Global.SyncState
        Get
            Return _syncOK
        End Get
        Set(value As wb_Global.SyncState)
            _syncOK = value
        End Set
    End Property

    Public Property ToolTipText As String
        Get
            Return _toolTipText
        End Get
        Set(value As String)
            _toolTipText = value
        End Set
    End Property

    Public Property Wb_Nummer As String
        Get
            Return _wb_Nummer
        End Get
        Set(value As String)
            _wb_Nummer = value
        End Set
    End Property

    Public Property Wb_Bezeichnung As String
        Get
            Return _wb_Bezeichnung
        End Get
        Set(value As String)
            _wb_Bezeichnung = value
        End Set
    End Property

    Public Property Wb_Gruppe As String
        Get
            Return _wb_Gruppe
        End Get
        Set(value As String)
            _wb_Gruppe = value
        End Set
    End Property

    Public Property Os_Bezeichnung As String
        Get
            Return _os_Bezeichnung
        End Get
        Set(value As String)
            _os_Bezeichnung = value
        End Set
    End Property

    Public Property Os_Gruppe As String
        Get
            Return _os_Gruppe
        End Get
        Set(value As String)
            _os_Gruppe = value
        End Set
    End Property

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        Return String.Compare(Sort, DirectCast(obj, wb_SyncItem).Sort)
    End Function

    Public Function Merge(obj As wb_SyncItem) As Boolean
        If Os_Bezeichnung = "" And obj.Os_Bezeichnung <> "" Then
            Os_Bezeichnung = obj.Os_Bezeichnung
            Os_Gruppe = obj.Os_Gruppe
            Os_Nummer += obj.Os_Nummer
            Return True
        End If
        If Wb_Bezeichnung = "" And obj.Wb_Bezeichnung <> "" Then
            Wb_Bezeichnung = obj.Wb_Bezeichnung
            Wb_Gruppe = obj.Wb_Gruppe
            Wb_Nummer += obj.Wb_Nummer
            Return True
        End If
        Return False
    End Function
End Class
