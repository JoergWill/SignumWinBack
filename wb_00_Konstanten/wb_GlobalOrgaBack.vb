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
    ''' Farbschema für Fenster-Docking. In der Programm-Variante OrgaBack wird immer VS2015BlueTheme zurückgegeben
    ''' </summary>
    ''' <returns>Theme - ThemeBase</returns>
    Public Shared ReadOnly Property Theme As ThemeBase
        Get
            'Einstellung aus Desktop.DockingTheme auslesen
            If _OrgaBackTheme < 0 And wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                DbReadSetting("Desktop")
            Else
                _OrgaBackTheme = wb_Global.OrgaBackThemes.Blau
            End If

            'Rückgabe-Wert abhängig von der Einstellung in OrgaBack
            Select Case _OrgaBackTheme
                Case wb_Global.OrgaBackThemes.Standard
                    Return New VS2005Theme
                Case wb_Global.OrgaBackThemes.Blau
                    Return New VS2015BlueTheme
                Case wb_Global.OrgaBackThemes.Grau
                    Return New VS2015LightTheme
                Case wb_Global.OrgaBackThemes.Anthrazit
                    Return New VS2015DarkTheme
                Case Else
                    Return New VS2015BlueTheme
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Gibt den Speicherort der Fenster-Definitionen zurück. Die Daten werden aus der OrgaBack-DB
    ''' gelesen. (OrgaBack.Admin.dbo.Settings)
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property OrgaBackDockPanelPath As String
        Get
            If _OrgaBackDockPanelPath Is Nothing Then
                DbReadSetting("Verzeichnisse")
            End If
            Return _OrgaBackDockPanelPath
        End Get
    End Property

    'TODO steht in wb_globalsettings
    Public Shared Property OrgaBackAddinPath As String
        Get
            If _OrgaBackAddinPath Is Nothing Then
                DbReadSetting("Verzeichnisse")
            End If
            Return _OrgaBackAddinPath
        End Get
        Set(value As String)
            _OrgaBackAddinPath = value
        End Set
    End Property

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
                    Trace.WriteLine("OrgaBackAdminConString=    " & wb_GlobalSettings.OrgaBackAdminConString)
                    Trace.WriteLine("OrgaBackWorkStationNumber= " & _OrgaBackWorkStationNumber)
                End If
                Return _OrgaBackWorkStationNumber
            Else
                Return "00"
            End If
        End Get
    End Property

    ''' <summary>
    ''' Liest alle benötigten Einträge aus der OrgaBack.Admin-Tabelle dbo.Settings
    ''' </summary>
    Private Shared Sub DbReadSetting(Category As String)
        'TODO kann auch aus wb_Main.settings-object gelesen werden (bei Programmstart)
        'REFAKTORIEREN !!

        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackAdminConString, wb_Sql.dbType.msSql)

        If OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlSettings, Category)) Then
            While OrgasoftMain.Read

                Select Case OrgasoftMain.sField("Entry")
                    Case "TempPfad"
                        _OrgaBackDockPanelPath = OrgasoftMain.sField("Content")
                        Trace.WriteLine("OrgaBackDockPanelPath " & _OrgaBackDockPanelPath)

                    Case "DockingTheme"
                        _OrgaBackTheme = OrgasoftMain.iField("Content")
                        Trace.WriteLine("_OrgaBackTheme " & _OrgaBackTheme)

                    Case "AddInPfad"
                        _OrgaBackAddinPath = OrgasoftMain.sField("Content")
                        Trace.WriteLine("_OrgaBackAddinPath " & _OrgaBackTheme)

                End Select
            End While
            Trace.WriteLine("OrgaBackAdminConString=    " & wb_GlobalSettings.OrgaBackAdminConString)
            Trace.WriteLine("OrgaBackDockPanelPath=     " & _OrgaBackDockPanelPath)
            Trace.WriteLine("OrgaBackTheme=             " & _OrgaBackTheme)
        End If
    End Sub

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
