Imports WinBack

Public MustInherit Class wb_OrgaBackProcess

    Private _VorfallKürzel As String
    Private _VorfallNr As String
    Private _ProcPositions As New List(Of wb_OrgaBackProcessPosition)

    Public Property ProcPositions As List(Of wb_OrgaBackProcessPosition)
        Get
            Return _ProcPositions
        End Get
        Set(value As List(Of wb_OrgaBackProcessPosition))
            _ProcPositions = value
        End Set
    End Property

    Public Sub New(ProcessCode As String, ProcessNumber As String)
        'OrgaBack Vorfall
        _VorfallKürzel = ProcessCode
        _VorfallNr = ProcessNumber
        'Daten aus [dbo].[GeschäftsvorfallPosition] einlesen
        MsSQLdbReadProcessPositions(ProcessCode, ProcessNumber)
    End Sub

    Public MustOverride Function DoAction(PositionNummer As Integer, Action As Signum.OrgaSoft.ERP.ProcessChangedAction) As Boolean

    Private Function MsSQLdbReadProcessPositions(ProcessCode As String, ProcessNumber As String) As Integer
        'Datenbankverbindung öffnen MsSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'alte Einträge löschen
        _ProcPositions.Clear()

        'alle Positionen zum Vorgang und Vorgangs-Nummer einlesen
        If orgaback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlVorfallPositionen, ProcessCode, ProcessNumber)) Then
            While orgaback.Read
                Dim ProcPos As New wb_OrgaBackProcessPosition
                'Alle Felder einlesen
                For i = 0 To orgaback.msRead.FieldCount - 1
                    'Daten in die ProcessPosition einlesen
                    ProcPos.MsSQLdbRead_Fields(orgaback.msRead.GetName(i), orgaback.msRead.GetValue(i))
                Next
                'Prozess-Position zu Liste hinzufügen
                _ProcPositions.Add(ProcPos)
            End While
        End If

        'Datenbankverbindung wieder schliessen
        orgaback.Close()
        'Anzahl der eingelesenen Datensätze
        Return ProcPositions.Count
    End Function
End Class

