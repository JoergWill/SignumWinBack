
Public Class wb_AktRechte
    Private Shared _UserGruppenRechte As New Dictionary(Of Integer, Boolean)
    Private Shared _NoEntryInItemParameter As Boolean = False
    Private Shared _ErrorText As String = ""
    Private Shared _UpdateDatabaseFile As String = ""

    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public Shared ReadOnly Property UpdateDatabaseFile As String
        Get
            Return _UpdateDatabaseFile
        End Get
    End Property

    Friend Shared Sub UpdateUserGruppenRechteTabelle(UserGruppe As Integer)
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim Tag As Integer
        Dim GrpRecht As Integer

        _UserGruppenRechte.Clear()
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserGrpRechte, UserGruppe))

        'falls keine Einträge vorhanden sind wird immer True zurückgegeben
        _NoEntryInItemParameter = True
        While winback.Read
            Tag = winback.iField("IP_ItemID")
            GrpRecht = winback.iField("IP_Wert2int")
            If Not _UserGruppenRechte.ContainsKey(Tag) Then
                _UserGruppenRechte.Add(Tag, (GrpRecht > 0))
                _NoEntryInItemParameter = False
            End If
        End While

        'Verbindung wieder schliessen
        winback.Close()
    End Sub

    Friend Shared Function RechtOK(Tag As String, SuperUser As Boolean) As Boolean
        Dim t As Integer = wb_Functions.StrToInt(Tag)
        If t = 0 Or SuperUser Or _NoEntryInItemParameter Then
            Return True
        Else
            If _UserGruppenRechte.TryGetValue(t - 100, RechtOK) Then
                Return RechtOK
            Else
                Return False
            End If
        End If
    End Function

    ''' <summary>
    ''' Prüft ob die Datenbank alle notwendigen Daten und Einträge enthält.
    ''' Die Datenbank muss Einträge für die Benutzerrechte(Gruppe -1) enthalten:
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CheckDB() As Boolean
        'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
        _UpdateDatabaseFile = "2.30_AktSysKonfig.sql"

        'alle Parameter(Update) prüfen
        If _NoEntryInItemParameter Then
            _ErrorText = "Fehler in Tabelle ItemParameter UserGruppe(-1) - Datensätze fehlen !"
            Trace.WriteLine(_ErrorText)
            Return False
        End If

        If Not _UserGruppenRechte.ContainsKey(130) Then
            _ErrorText = "Fehler in Tabelle ItemParameter - User-Rechte Produktion(Tag130) - Datensätze fehlen !"
            Trace.WriteLine(_ErrorText)
            Return False
        End If

        'Kein Fehler 
        _ErrorText = ""
        Return True
    End Function

End Class
