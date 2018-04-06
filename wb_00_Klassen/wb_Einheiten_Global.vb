Public Class wb_Einheiten_Global
    Private Shared Einheiten As New Dictionary(Of String, wb_Global.wb_Einheiten)

    Shared Sub New()
        Dim E As wb_Global.wb_Einheiten = Nothing
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlEinheiten)
        Einheiten.Clear()

        'Tabelle Einheiten
        While winback.Read
            'Einheit Index in WinBack
            E.Nr = winback.iField("E_LfdNr")
            'entsprechende Einheit in OrgaBack - wenn das Datenfeld vorhanden ist
            If winback.FieldCount > 6 Then
                E.obNr = winback.iField("E_obNr")
            Else
                E.obNr = wb_Global.obEinheitKilogramm
                Trace.WriteLine("Tabelle WinBack.Einheiten muss erweitert werden! (OrgaBack Einheiten)")
            End If
            'Einheit
            E.Einheit = winback.sField("E_Einheit")
            'Bezeichnung
            E.Bezeichnung = winback.sField("E_Bezeichnung")

            'zur Liste hinzufügen
            Einheiten.Add(E.Nr, E)
        End While

    End Sub

    Shared Function GetobEinheitNr(eNr As Integer) As Integer
        If Einheiten.ContainsKey(eNr) Then
            Return Einheiten(eNr).obNr
        Else
            Return wb_Global.obEinheitKilogramm
        End If
    End Function

End Class
