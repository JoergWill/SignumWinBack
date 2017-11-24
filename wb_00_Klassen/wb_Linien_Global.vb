Public Class wb_Linien_Global
    Private Shared LinienGruppen As New Dictionary(Of String, wb_Global.wb_LinienGruppe)

    Shared Sub New()
        Dim L As wb_Global.wb_LinienGruppe = Nothing
        Dim Linien As String

        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlLinienGruppen)
        LinienGruppen.Clear()

        While winback.Read
            'Liniengruppe
            L.LinienGruppe = winback.iField("LG_Nr")
            L.Bezeichnung = winback.sField("LG_Bezeichnung")
            L.Abteilung = winback.sField("LG_Abteilung")

            'Linien in der Liniengruppe
            Linien = winback.sField("LG_Linien")
            L.Linien = Linien.Split(",")

            'Formularsteuerung
            Try
                L.BackZettelDrucken = winback.sField("LG_BZ_Drucken")
                L.TeigZettelDrucken = winback.sField("LG_TZ_Drucken")
                L.TeigRezeptDrucken = winback.sField("LG_TR_Drucken")
                L.BackZettelSenden = winback.sField("LG_BZ_Senden")
                L.TeigZettelSenden = winback.sField("LG_TZ_Senden")
            Catch ex As Exception
                'Erweiterung Tabelle Liniengruppen ist notwendig !
                'TODO Fehler in Log-File ausgeben
            End Try

            'zum Dictonary hinzufügen
            LinienGruppen.Add(L.LinienGruppe, L)
        End While
        winback.Close()
    End Sub

    Shared Function GetBezeichnung(LinienGruppe As Integer) As String
        If LinienGruppen.ContainsKey(LinienGruppe) Then
            Return LinienGruppen(LinienGruppe).Bezeichnung
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' Gibt die erste Produktions-Linie der Liniengruppe zurück.
    ''' </summary>
    ''' <param name="LinienGruppe"></param>
    ''' <returns></returns>
    Shared Function GetLinieFromLinienGruppe(LinienGruppe As Integer) As Integer
        If LinienGruppen.ContainsKey(LinienGruppe) Then
            Return wb_Functions.StrToInt(LinienGruppen(LinienGruppe).Linien(0))
        Else
            Return wb_Global.UNDEFINED
        End If

    End Function

    ''' <summary>
    ''' Gibt die erste Liniengruppe zurück, welche die übergegebene Linie enthält
    ''' </summary>
    ''' <param name="Linie"></param>
    ''' <returns></returns>
    Friend Shared Function GetLinienGruppeFromLinie(Linie As Integer) As Integer
        For Each lg In LinienGruppen
            For Each l As Integer In lg.Value.Linien
                If l = Linie Then
                    Return lg.Value.LinienGruppe
                End If
            Next
        Next
        Return wb_Global.UNDEFINED
    End Function
End Class
