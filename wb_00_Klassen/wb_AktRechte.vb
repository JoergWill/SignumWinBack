
Public Class wb_AktRechte
    Private Shared _UserGruppenRechte As New Dictionary(Of Integer, Boolean)
    Private Shared _NoEntryInItemParameter As Boolean = False

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
End Class
