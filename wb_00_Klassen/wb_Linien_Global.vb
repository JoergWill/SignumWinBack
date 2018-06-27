Public Class wb_Linien_Global
    Private Shared LGruppen As New Dictionary(Of String, wb_Global.wb_LinienGruppe)
    Private Shared Linien As New Dictionary(Of String, wb_Global.wb_Linien)
    Public Shared LinienGruppen As New SortedList
    Public Shared DefaultProdFiliale As Integer = wb_Global.UNDEFINED

    ''' <summary>
    ''' Array Liniengruppen aufbauen
    ''' Array Linien aufbauen
    ''' </summary>
    Shared Sub New()
        GetOrgaBackOrte()
        InitLinienGruppen()
        InitLinien()
    End Sub

    ''' <summary>
    ''' Kopiert alle Backorte aus OrgaBack in die Tabelle winback.Liniengruppen. 
    ''' Der Backort steht in OrgaBack in Artikel-Multifunktionsfeld (Auswahlfeld) zum Artikel
    ''' 
    ''' Die Auswahlfeld-Inhalte stehen in der Tabelle dbo.ArtikelMultifunktionsfeld mit Gruppen-Nr=3
    ''' Zur Linien-Nummer wird in WinBack ein Offset(100) dazu addiert)
    ''' </summary>
    Private Shared Sub GetOrgaBackOrte()
        Dim LinieNummer As Integer
        Dim LinieBezeichnung As String
        Dim sql As String

        'Datenbank-Verbindung öffnen - MsSQL
        Dim orgasoft As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Daten aus Tabelle ArtikelMultifunktionsfeld lesen
        If orgasoft.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlBackorte, wb_Global.GruppenNrBackorte)) Then
            While orgasoft.Read
                LinieNummer = wb_Functions.StrToInt(orgasoft.sField("Hierarchie")) + wb_Global.OffsetBackorte
                LinieBezeichnung = orgasoft.sField("Bezeichnung")

                'Backorte in Tabelle winback.Linien eintragen. Bestehende Einträge werden aktualisiert
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdteLinien, LinieNummer, LinieBezeichnung)
                'Update ausführen
                winback.sqlCommand(sql)
            End While
        End If

        'Kanal wieder schliessen
        orgasoft.Close()
        winback.Close()
    End Sub

    Private Shared Sub InitLinienGruppen()
        Dim L As wb_Global.wb_LinienGruppe = Nothing
        Dim Linien As String

        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlLinienGruppen)
        LGruppen.Clear()
        LinienGruppen.Clear()

        While winback.Read
            Try
                'Liniengruppe
                L.LinienGruppe = winback.iField("LG_Nr")
                L.Bezeichnung = winback.sField("LG_Bezeichnung")
                L.Abteilung = winback.sField("LG_Abteilung")

                'Linien in der Liniengruppe
                Linien = winback.sField("LG_Linien")
                L.Linien = Linien.Split(",")

                'Formularsteuerung
                If winback.FieldCount > 5 Then

                    L.BackZettelDrucken = winback.sField("LG_BZ_Drucken")
                    L.TeigZettelDrucken = winback.sField("LG_TZ_Drucken")
                    L.TeigRezeptDrucken = winback.sField("LG_TR_Drucken")
                    L.BackZettelSenden = winback.sField("LG_BZ_Senden")
                    L.TeigZettelSenden = winback.sField("LG_TZ_Senden")
                Else
                    'Erweiterung Tabelle Liniengruppen ist notwendig !
                    Trace.WriteLine("Tabelle WinBack.Liniengruppen muss erweitert werden! (Formular-Steuerung)")
                End If

                'zum Dictonary hinzufügen
                LGruppen.Add(L.LinienGruppe, L)
                'SortedList
                LinienGruppen.Add(L.LinienGruppe, L.Bezeichnung)
            Catch
            End Try
        End While

        winback.Close()
    End Sub

    Private Shared Sub InitLinien()
        Dim Linie As wb_Global.wb_Linien = Nothing

        'Default OrgaBack-Produktions-Filiale (Nummer)
        If wb_Filiale.ProduktionsFilialen.Count > 0 Then
            DefaultProdFiliale = wb_Filiale.ProduktionsFilialen.GetKey(0)
        End If

        Dim winback As New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_GlobalSettings.WinBackDBType)
        winback.sqlSelect(wb_Sql_Selects.sqlLinien)
        Linien.Clear()

        While winback.Read
            Try
                'Linien
                Linie.Linie = winback.iField("L_Nr")
                Linie.Bezeichnung = winback.sField("L_Bezeichnung")
                Linie.SegIdx = winback.sField("L_Seg_Idx")

                'Zuordnung Linien - OrgaBack.Filiale
                If winback.FieldCount > 8 Then
                    Linie.Filiale = winback.iField("L_ProdFiliale", DefaultProdFiliale)
                Else
                    'Erweiterung Tabelle Linien ist notwendig !
                    Trace.WriteLine("Tabelle WinBack.Linien muss erweitert werden! (Filiale)")
                End If
                'zum Dictonary hinzufügen
                Linien.Add(Linie.Linie, Linie)
            Catch
            End Try
        End While

        winback.Close()
    End Sub

    ''' <summary>
    ''' Gibt die Bezeichnung der Liniengruppe zurück
    ''' </summary>
    ''' <param name="LinienGruppe"></param>
    ''' <returns></returns>
    Shared Function GetBezeichnung(LinienGruppe As Integer) As String
        If LGruppen.ContainsKey(LinienGruppe) Then
            Return LGruppen(LinienGruppe).Bezeichnung
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
        If LGruppen.ContainsKey(LinienGruppe) Then
            Return wb_Functions.StrToInt(LGruppen(LinienGruppe).Linien(0))
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
        For Each lg In LGruppen
            For Each l As Integer In lg.Value.Linien
                If l = Linie Then
                    Return lg.Value.LinienGruppe
                End If
            Next
        Next
        Return wb_Global.UNDEFINED
    End Function

    ''' <summary>
    ''' Gibt die OrgaBack Produktions-Filiale zur WinBack-Linie zurück
    ''' </summary>
    ''' <param name="Linie"></param>
    ''' <returns></returns>
    Shared Function GetFiliale(Linie As Integer) As Integer
        If Linien.ContainsKey(Linie) Then
            Return Linien(Linie).Filiale
        Else
            Return wb_Global.UNDEFINED
        End If

    End Function
End Class
