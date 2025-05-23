﻿
Public Class wb_AktRechte
    Private Shared _UserGruppenRechte As New Dictionary(Of Integer, Boolean)
    Private Shared _SysGruppenRechte As New Dictionary(Of Integer, Boolean)
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
        If UserGruppe = wb_Global.SysKonfigGrpe Then
            UpdateUserGruppenRechteTabelle(UserGruppe, _SysGruppenRechte)
        Else
            UpdateUserGruppenRechteTabelle(UserGruppe, _UserGruppenRechte)
        End If
    End Sub

    Private Shared Sub UpdateUserGruppenRechteTabelle(UserGruppe As Integer, ByRef RechteTabelle As Dictionary(Of Integer, Boolean))
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        Dim Tag As Integer
        Dim GrpRecht As Integer

        RechteTabelle.Clear()
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUserGrpRechte, UserGruppe))

        'falls keine Einträge vorhanden sind wird immer True zurückgegeben
        _NoEntryInItemParameter = True
        While winback.Read
            Tag = winback.iField("IP_ItemID")
            GrpRecht = winback.iField("IP_Wert2int")
            If Not RechteTabelle.ContainsKey(Tag) Then
                RechteTabelle.Add(Tag, (GrpRecht > 0))
                _NoEntryInItemParameter = False
            End If
        End While

        'Verbindung wieder schliessen
        winback.Close()
    End Sub

    Friend Shared Function RechtOK(Tag As String, SuperUser As Boolean, Optional UserGruppe As Integer = 0) As Boolean
        Dim Result As Boolean = False
        Dim t As Integer = wb_Functions.StrToInt(Tag)

        If t = 0 OrElse SuperUser OrElse _NoEntryInItemParameter Then
            Result = True
        Else
            If UserGruppe = wb_Global.SysKonfigGrpe Then
                If Not _SysGruppenRechte.TryGetValue(t - 100, Result) Then
                    Result = False
                End If
            Else
                If Not _UserGruppenRechte.TryGetValue(t - 100, Result) Then
                    Result = False
                End If
            End If
        End If
        Return Result
    End Function

    ''' <summary>
    ''' Prüft ob die Datenbank alle notwendigen Daten und Einträge enthält.
    ''' Die Datenbank muss Einträge für die Benutzerrechte(Gruppe -1) enthalten:
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CheckDB_User() As Boolean
        'alle Parameter(Update) prüfen
        If _NoEntryInItemParameter Then
            'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
            _UpdateDatabaseFile = "2.30_AktSysKonfig.sql"
            _ErrorText = "Fehler in Tabelle ItemParameter UserGruppe(" & wb_Global.SysKonfigGrpe & ") - Datensätze fehlen !"
            Trace.WriteLine("@E_" & _ErrorText)
            Return False
        End If

        'Kein Fehler 
        _ErrorText = ""
        Return True
    End Function

    Public Shared Function CheckDB_Prod() As Boolean
        'User-Rechte Produktion (Tag130) in Tabelle winback.ItemParameter.IP_ItemID
        If Not _SysGruppenRechte.ContainsKey(30) Then
            'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
            _UpdateDatabaseFile = "2.30_Produktionsplanung.sql"
            _ErrorText = "Fehler in Tabelle ItemParameter - User-Rechte Produktion(Tag130) - Datensätze fehlen !"
            Trace.WriteLine("@E_" & _ErrorText)
            Return False
        End If

        'Kein Fehler 
        _ErrorText = ""
        Return True
    End Function

End Class
