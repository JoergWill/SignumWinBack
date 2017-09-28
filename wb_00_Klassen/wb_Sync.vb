Imports WinBack

Public MustInherit Class wb_Sync
    Friend _Data As New ArrayList
    Friend _Item As wb_SyncItem

    'Entscheidungs-Matrix vorbelegen
    Private _Case_01 As wb_Global.SyncState = wb_Global.SyncState.WinBackWrite
    Private _Case_10 As wb_Global.SyncState = wb_Global.SyncState.OrgaBackWrite
    Private _Case_11 As wb_Global.SyncState = wb_Global.SyncState.WinBackUpdate

    Friend MustOverride Function DBRead() As Boolean
    Friend MustOverride Function DBUpdate(Nr As String, Text As String) As Boolean
    Friend MustOverride Function DBInsert(Nr As String, Text As String) As Boolean

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

    Friend Overridable Sub CheckData(_CheckDataErrFlag As wb_Global.SyncState)
        Dim LastNummer As String = "x"

        'WinBack-Check
        If _CheckDataErrFlag = wb_Global.SyncState.WinBackErr Then
            'Prüfen auf doppelte Einträge bei Nummern
            For Each x As wb_SyncItem In _Data
                If x.wb_Nummer = "0" Or x.wb_Nummer = "" Or x.wb_Nummer = LastNummer Then
                    x.SyncOK = wb_Global.SyncState.WinBackErr
                    LastNummer = x.wb_Nummer
                End If
            Next
        End If

        'OrgaBack-Check
        If _CheckDataErrFlag = wb_Global.SyncState.OrgaBackErr Then
            'Prüfen auf doppelte Einträge bei Nummern
            For Each x As wb_SyncItem In _Data
                If x.os_Nummer = "0" Or x.os_Nummer = "" Or x.os_Nummer = LastNummer Then
                    x.SyncOK = wb_Global.SyncState.OrgaBackErr
                    LastNummer = x.os_Nummer
                End If
            Next
        End If
    End Sub

    Public Sub CheckSync(SyncData As ArrayList)
        'beide Arrays aneinanderhängen
        _Data.AddRange(SyncData)
        'nach Nummer(Sortiertkriterium) sortieren
        _Data.Sort()
        'doppelte Einträge zusammenfassen
        DelDubletten()
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
        For Each x As wb_SyncItem In _Data
            If x.SyncOK = wb_Global.SyncState.NOK Then
                x.SyncOK = GetSyncResult(x)
            End If
        Next
    End Sub

    Friend Overridable Function GetSyncResult(ByVal x As wb_SyncItem) As wb_Global.SyncState

        'Beide Nummern sind identisch
        If x.wb_Nummer = x.os_Nummer Then
            If x.wb_Bezeichnung = x.os_Bezeichnung Then
                'Beide Bezeichnungen sind identisch
                Return wb_Global.SyncState.OK
            Else
                'Beide Bezeichnungen sind unterschiedlich
                Return _Case_11
            End If
        End If

        'Nummern sind unterschiedlich oder eine Nummer ist leer       
        If x.wb_Bezeichnung = "" Then
            'WinBack-Bezeichnung ist leer
            Return _Case_01
        ElseIf x.os_Bezeichnung = "" Then
            'OrgaBack-Bezeichnung ist leer
            Return _Case_10
        End If

        Return wb_Global.SyncState.NOK
    End Function

    Public Sub PrintSync()
        For Each x As wb_SyncItem In _Data
            Debug.Print(" wb " & x.wb_Nummer & " " & x.wb_Bezeichnung & vbTab & "os " & x.os_Nummer & " " & x.os_Bezeichnung)
        Next
    End Sub
End Class
