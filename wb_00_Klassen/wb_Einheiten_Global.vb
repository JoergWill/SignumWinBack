Public Class wb_Einheiten_Global
    Private Shared obEinheiten As New Dictionary(Of String, wb_Global.wb_Einheiten)
    Private Shared Einheiten As New Dictionary(Of String, wb_Global.wb_Einheiten)
    Private Shared EinhText As New Dictionary(Of String, wb_Global.wb_Einheiten)

    Shared Sub New()
        Dim E As wb_Global.wb_Einheiten = Nothing
        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlEinheiten)
        Einheiten.Clear()

        'Tabelle Einheiten
        While winback.Read
            'Einheit Index in WinBack
            E.Nr = winback.iField("E_LfdNr")
            'Einheit
            E.Einheit = winback.sField("E_Einheit")
            'Bezeichnung
            E.Bezeichnung = winback.sField("E_Bezeichnung")

            'entsprechende Einheit in OrgaBack - wenn das Datenfeld vorhanden ist
            If winback.FieldCount > 6 Then
                E.obNr = winback.iField("E_obNr")
                If Not obEinheiten.ContainsKey(E.obNr) Then
                    obEinheiten.Add(E.obNr, E)
                End If
            Else
                E.obNr = wb_Global.obEinheitKilogramm
                Trace.WriteLine("Tabelle WinBack.Einheiten muss erweitert werden! (OrgaBack Einheiten)")
            End If

            'zur Liste hinzufügen
            Einheiten.Add(E.Nr, E)
            If Not EinhText.ContainsKey(E.Einheit) Then
                EinhText.Add(E.Einheit, E)
            End If
        End While

    End Sub

    Shared Function GetobEinheitNr(eNr As Integer) As Integer
        If Einheiten.ContainsKey(eNr) Then
            Return Einheiten(eNr).obNr
        Else
            Return wb_Global.obEinheitKilogramm
        End If
    End Function

    Shared Function getEinheitFromText(eBez As String) As Integer
        If EinhText.ContainsKey(eBez) Then
            Return EinhText(eBez).Einheit
        Else
            Return wb_Global.UNDEFINED
        End If
    End Function

    Shared Function getobEinheitFromText(eBez As String) As Integer
        If EinhText.ContainsKey(eBez) Then
            Return EinhText(eBez).obNr
        Else
            Return wb_Global.obEinheitKilogramm
        End If
    End Function

    Shared Function getobEinheitFromNr(oNr As String, Optional DefaultEinheit As Integer = wb_Global.obEinheitKilogramm) As String
        If obEinheiten.ContainsKey(oNr) Then
            Return obEinheiten(oNr).Einheit
        Else
            If obEinheiten.ContainsKey(DefaultEinheit) Then
                Return obEinheiten(DefaultEinheit).Einheit
            Else
                Return wb_Global.wbEinheitKilogramm
        End If
        End If
    End Function

    Shared Function getEinheitFromKompType(KompType As wb_Global.KomponTypen) As Integer
        'TODO Einheiten aus Tabelle KomponParams lesen und zu WinBack.Komponenten-Type einheit zurückmelden
        'ACHTUNG Kneter-Komponenten !
        If KompType = wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
            Return wb_Global.wbEinheitStk
        Else
            Return wb_Global.wbEinheitKilogramm
        End If
    End Function
End Class
