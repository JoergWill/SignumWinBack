Imports WinBack

Public MustInherit Class wb_Sync
    Friend _Data As New ArrayList
    Friend _Item As wb_SyncItem

    'Entscheidungs-Matrix vorbelegen
    Private _Case_01 As wb_Global.SyncState = wb_Global.SyncState.WinBackWrite
    Private _Case_10 As wb_Global.SyncState = wb_Global.SyncState.OrgaBackWrite
    Private _Case_11 As wb_Global.SyncState = wb_Global.SyncState.WinBackUpdate

    'Zähler für Statistik
    Private _iSyncNOK As Integer
    Private _iSyncOK As Integer
    Private _iSync_Count As Integer

    Private _iSync_osErr As Integer
    Private _iSync_wbErr As Integer

    Private _iSync_osWrite As Integer
    Private _iSync_osUpdate As Integer
    Private _iSync_osMiss As Integer

    Private _iSync_wbWrite As Integer
    Private _iSync_wbUpdate As Integer
    Private _iSync_wbMiss As Integer


    Friend MustOverride Function DBRead() As Boolean
    Friend MustOverride Function DBUpdate(Nr As String, Text As String, Gruppe As String) As Boolean
    Friend MustOverride Function DBInsert(Nr As String, Text As String, Gruppe As String) As Boolean
    Friend MustOverride Function DBNumber(Nr_Alt As String, Nr_Neu As String, Gruppe As String) As Boolean

    ''' <summary>
    ''' Die WinBack.Bezeichnung ist leer.
    '''     - wb_Global.SysncState.WinBackWrite - Datensatz ist nur in OrgaBack vorhanden und muss in WinBack geschrieben werden
    '''     - wb_Global.SysncState.WinBackMiss  - Datensatz ist nur in OrgaBack vorhanden - Fehlt in WinBack, KEIN Update
    ''' </summary>
    Public WriteOnly Property Case_01 As wb_Global.SyncState
        Set(value As wb_Global.SyncState)
            _Case_01 = value
        End Set
    End Property

    ''' <summary>
    ''' Die OrgaBack-Bezeichnung ist leer
    '''     - wb_Global.SysncState.OrgaBackWrite - Datensatz ist nur in WinBack vorhanden und muss in OrgaBack geschrieben werden
    '''     - wb_Global.SysncState.OrgaBackMiss  - Datensatz ist nur in WinBack vorhanden - Fehlt in OrgaBack, KEIN Update
    ''' </summary>
    Public WriteOnly Property Case_10 As wb_Global.SyncState
        Set(value As wb_Global.SyncState)
            _Case_10 = value
        End Set
    End Property

    ''' <summary>
    ''' Die Nummern sind identisch, aber beide Bezeichnungen sind unterschiedlich.
    '''     - wb_Global.SysncState.OrgaBackUpdate   - Datensatz ist in beiden Datenbanken vorhanden und muss in OrgaBack aktualisiert werden
    '''     - wb_Global.SysncState.WinBackUpdate    - Datensatz ist in beiden Datenbanken vorhanden und muss in WinBack aktualisiert werden
    ''' </summary>
    Public WriteOnly Property Case_11 As wb_Global.SyncState
        Set(value As wb_Global.SyncState)
            _Case_11 = value
        End Set
    End Property

    Public ReadOnly Property Data As ArrayList
        Get
            Return _Data
        End Get
    End Property

    Public ReadOnly Property CntSyncAll As Integer
        Get
            Return _iSync_Count
        End Get
    End Property

    Public ReadOnly Property CntSyncNOK As Integer
        Get
            Return _iSyncNOK
        End Get
    End Property

    Public ReadOnly Property CntSyncOK As Integer
        Get
            Return _iSyncOK
        End Get
    End Property

    Public ReadOnly Property CntSync_osErr As Integer
        Get
            Return _iSync_osErr
        End Get
    End Property

    Public ReadOnly Property CntSync_wbErr As Integer
        Get
            Return _iSync_wbErr
        End Get
    End Property

    Public ReadOnly Property CntSync_osWrite As Integer
        Get
            Return _iSync_osWrite
        End Get
    End Property

    Public ReadOnly Property CntSync_osUpdate As Integer
        Get
            Return _iSync_osUpdate
        End Get
    End Property

    Public ReadOnly Property CntSync_osMiss As Integer
        Get
            Return _iSync_osMiss
        End Get
    End Property

    Public ReadOnly Property CntSync_wbWrite As Integer
        Get
            Return _iSync_wbWrite
        End Get
    End Property

    Public ReadOnly Property CntSync_wbUpdate As Integer
        Get
            Return _iSync_wbUpdate
        End Get
    End Property

    Public ReadOnly Property CntSync_wbMiss As Integer
        Get
            Return _iSync_wbMiss
        End Get
    End Property

    Friend Overridable Sub CheckData(_CheckDataErrFlag As wb_Global.SyncState)
        Dim LastNummer As String = "x"
        _iSync_Count = _Data.Count

        'WinBack-Check
        _iSync_wbErr = 0
        If _CheckDataErrFlag = wb_Global.SyncState.WinBackErr Then
            'Prüfen auf doppelte Einträge bei Nummern
            For Each x As wb_SyncItem In _Data
                If x.Wb_Nummer = "0" Or x.Wb_Nummer = "" Or x.Wb_Nummer = LastNummer Then
                    x.SyncOK = wb_Global.SyncState.WinBackErr
                    _iSync_wbErr += 1
                    x.ToolTipText = "Nummer gleich Null oder doppelt vorhanden"
                    LastNummer = x.Wb_Nummer
                End If
            Next
        End If

        'OrgaBack-Check
        _iSync_osErr = 0
        If _CheckDataErrFlag = wb_Global.SyncState.OrgaBackErr Then
            'Prüfen auf doppelte Einträge bei Nummern
            For Each x As wb_SyncItem In _Data
                If x.Os_Nummer = "0" Or x.Os_Nummer = "" Or x.Os_Nummer = LastNummer Then
                    x.SyncOK = wb_Global.SyncState.OrgaBackErr
                    _iSync_osErr += 1
                    x.ToolTipText = "Nummer gleich Null oder doppelt vorhanden"
                End If
                LastNummer = x.Os_Nummer
            Next
        End If
    End Sub

    Public Sub CheckSync(SyncData As ArrayList)
        'beide Arrays aneinanderhängen
        _Data.AddRange(SyncData)
        PrintSync()
        'nach Nummer(Sortiertkriterium) sortieren
        _Data.Sort()
        PrintSync()
        'doppelte Einträge zusammenfassen
        DelDubletten()
        PrintSync()
        'Prüfen ob beide Einträge im Array identisch sind
        CheckSyncResult()
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
                If x.Merge(y) Then
                    'überflüssigen Datensatz löschen
                    _Data.Remove(y)
                    _DataCount -= 1
                End If
            End If
            'weiter mit dem nächsten Datensatz
            i += 1
        End While
    End Sub

    Friend Overridable Sub CheckSyncResult()
        'Zusammenfassung Sync-Result löschen
        ClearSyncCounter()

        For Each x As wb_SyncItem In _Data
            If x.SyncOK = wb_Global.SyncState.NOK Then
                x.SyncOK = GetSyncResult(x)
                x.ToolTipText = GetSyncToolTipText(x.SyncOK)
                'Zähler aktualisiere
                addSyncCounter(x.SyncOK)
            End If
        Next
    End Sub

    Friend Overridable Function GetSyncToolTipText(x As wb_Global.SyncState) As String
        Select Case x
            Case wb_Global.SyncState.NOK
                Return "Datensatz wurde noch nicht ausgewertet"
            Case wb_Global.SyncState.OK
                Return "Beide Datensätze sind in beiden Datenbanken identisch"

            Case wb_Global.SyncState.OrgaBackWrite
                Return "Datensätze wird in OrgaBack angelegt"
            Case wb_Global.SyncState.OrgaBackUpdate
                Return "Datensatz ist in OrgaBack vorhanden, aber nicht identisch. Wird in OrgaBack überschrieben"
            Case wb_Global.SyncState.OrgaBackMiss
                Return "Datensatz fehlt in OrgaBack und wird NICHT angelegt!"

            Case wb_Global.SyncState.WinBackWrite
                Return "Datensätze wird in WinBack angelegt"
            Case wb_Global.SyncState.WinBackUpdate
                Return "Datensatz ist in WinBack vorhanden, aber nicht identisch. Wird in WinBack überschrieben"
            Case wb_Global.SyncState.WinBackMiss
                Return "Datensatz fehlt in WinBack und wird NICHT angelegt!"

            Case Else
                Return ""
        End Select
    End Function

    Friend Overridable Function GetSyncResult(ByVal x As wb_SyncItem) As wb_Global.SyncState

        'Beide Nummern sind identisch
        If x.Wb_Nummer = x.Os_Nummer Then
            If x.Wb_Bezeichnung = x.Os_Bezeichnung Then
                'Beide Bezeichnungen sind identisch
                Return wb_Global.SyncState.OK
            Else
                'Beide Bezeichnungen sind unterschiedlich
                Return _Case_11
            End If
        End If

        'Nummern sind unterschiedlich oder eine Nummer ist leer       
        If x.Wb_Bezeichnung = "" Then
            'WinBack-Bezeichnung ist leer
            Return _Case_01
        ElseIf x.Os_Bezeichnung = "" Then
            'OrgaBack-Bezeichnung ist leer
            Return _Case_10
        End If

        Return wb_Global.SyncState.NOK
    End Function

    Private Sub ClearSyncCounter()
        _iSyncNOK = 0
        _iSyncOK = 0
        _iSync_osWrite = 0
        _iSync_osUpdate = 0
        _iSync_osMiss = 0
        _iSync_wbWrite = 0
        _iSync_wbUpdate = 0
        _iSync_wbMiss = 0
    End Sub

    Private Sub addSyncCounter(x As wb_Global.SyncState)
        Select Case x
            Case wb_Global.SyncState.NOK
                _iSyncNOK += 1
            Case wb_Global.SyncState.OK
                _iSyncOK += 1

            Case wb_Global.SyncState.OrgaBackWrite
                _iSync_osWrite += 1
            Case wb_Global.SyncState.OrgaBackUpdate
                _iSync_osUpdate += 1
            Case wb_Global.SyncState.OrgaBackMiss
                _iSync_osMiss += 1

            Case wb_Global.SyncState.WinBackWrite
                _iSync_wbWrite += 1
            Case wb_Global.SyncState.WinBackUpdate
                _iSync_wbUpdate += 1
            Case wb_Global.SyncState.WinBackMiss
                _iSync_wbMiss += 1
        End Select

    End Sub

    Public Sub PrintSync()
        For Each x As wb_SyncItem In _Data
            Debug.Print(" wb " & x.Wb_Nummer & " " & x.Wb_Bezeichnung & vbTab & "os " & x.Os_Nummer & " " & x.Os_Bezeichnung)
        Next
    End Sub
End Class
