Imports WeifenLuo.WinFormsUI.Docking
''' <summary>
''' Liest die Einstellungen aus der Tabelle (Admin)dbo.Settings. Die Aufschlüsselung der Index-Nummern steht in der
''' Tabelle (Admin)dbo.Configuration.
''' 
''' Die Verbindung zur Admin-Datenbank wird über den Admin-Connection-String hergestellt.
''' </summary>
Public Class wb_GlobalOrgaBack

    Private Shared _OrgaBackDockPanelPath As String = Nothing
    Private Shared _OrgaBackAddinPath As String = Nothing
    Private Shared _OrgaBackWorkStationNumber As String = Nothing
    Private Shared _OrgaBackTheme As Integer = -1

    ''' <summary>
    ''' Ermittelt die (OrgaBack)Arbeitsplatz-Nummer aus dem MicroSoft Workstation-Namen.
    ''' Die Nummer wird aus der Tabelle dbo.Workstations gelesen. 
    ''' Wenn die Arbeitsplatz-Nummer nicht gefunden wird, wird "00" zurückgegeben.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property OrgaBackWorkStationNumber As String
        Get
            If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                If _OrgaBackWorkStationNumber Is Nothing Then
                    'Datenbank-Verbindung öffnen - MsSQL
                    Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackAdminConString, wb_Sql.dbType.msSql)
                    If OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlWrkStations, WorkStationName)) Then
                        If OrgasoftMain.Read Then
                            _OrgaBackWorkStationNumber = OrgasoftMain.sField("WorkStationNo")
                        Else
                            _OrgaBackWorkStationNumber = "00"
                        End If
                    End If
                    Trace.WriteLine("@I_OrgaBackAdminConString=    " & wb_GlobalSettings.OrgaBackAdminConString)
                    Trace.WriteLine("@I_OrgaBackWorkStationNumber= " & _OrgaBackWorkStationNumber)
                End If
                Return _OrgaBackWorkStationNumber
            Else
                Return "00"
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gibt der MicroSoft Workstation-Namen zurück. Bei Zugriffen über Remote-Desktop wird
    ''' der Workstation-Name des Desktop-Client zurückgegeben.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property WorkStationName As String
        Get
            WorkStationName = Environment.GetEnvironmentVariable("clientname")
            If WorkStationName Is Nothing Then
                WorkStationName = Environment.GetEnvironmentVariable("computername")
            End If
        End Get
    End Property

End Class
