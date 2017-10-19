Public Class wb_Linien_Global
    Private Shared LinienGruppen As New Dictionary(Of String, wb_Global.wb_LinienGruppe)

    Shared Sub New()
        Dim L As wb_Global.wb_LinienGruppe
        Dim Linien As String

        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlLinienGruppen)
        LinienGruppen.Clear()

        While winback.Read
            'Liniengruppe
            L.LinienGruppe = winback.sField("LG_Nr")
            L.Bezeichnung = winback.sField("LG_Bezeichnung")
            L.Abteilung = winback.sField("LG_Abteilung")

            'Linien in der Liniengruppe
            Linien = winback.sField("LG_Linien")
            L.Linien = Linien.Split(",")

            'Formularsteuerung
            L.BackZettelDrucken = winback.sField("LG_BZ_Drucken")
            L.TeigZettelDrucken = winback.sField("LG_TZ_Drucken")
            L.TeigRezeptDrucken = winback.sField("LG_TR_Drucken")
            L.BackZettelSenden = winback.sField("LG_BZ_Senden")
            L.TeigZettelSenden = winback.sField("LG_TZ_Senden")

            'zum Dictonary hinzufügen
            LinienGruppen.Add(L.LinienGruppe, L)
        End While
        winback.Close()
    End Sub

    Shared Function GetBezeichnung(LinienGruppe As String) As String
        If LinienGruppen.ContainsKey(LinienGruppe) Then
            Return LinienGruppen(LinienGruppe).Bezeichnung
        Else
            Return ""
        End If

    End Function
End Class
