Public Class wb_Einheiten_Global
    Private Shared obEinheiten As New Dictionary(Of String, wb_Global.wb_Einheiten)
    Private Shared Einheiten As New Dictionary(Of String, wb_Global.wb_Einheiten)
    Private Shared EinhText As New Dictionary(Of String, wb_Global.wb_Einheiten)

    Private Shared LosArtIdx As New Dictionary(Of Integer, String)
    Private Shared LosArtText As New Dictionary(Of String, Integer)

    Private Shared _ErrorText As String = ""
    Private Shared _UpdateDatabaseFile As String = ""
    Private Shared _OBEinheiten As Boolean = True

    Shared Sub New()
        'Dictionary Einheiten aus winback.Einheiten
        ReadWinBackEinheiten()
        'Dictionary LosArt aus dbo.LosArten
        If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Or wb_GlobalSettings.pVariante = wb_Global.ProgVariante.UnitTest Then
            ReadOrgaBackLosArt()
        End If
    End Sub

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

    Shared Function GetEinheitFromNr(eNr As Integer) As String
        If Einheiten.ContainsKey(eNr) Then
            Return Einheiten(eNr).Einheit
        Else
            Return wb_Global.wbEinheitKilogramm
        End If
    End Function

    Shared Function getobEinheitFromText(eBez As String, Optional obDefault As Integer = wb_Global.obEinheitKilogramm) As Integer
        If eBez IsNot Nothing Then
            If EinhText.ContainsKey(eBez) Then
                Return EinhText(eBez).obNr
            Else
                Return obDefault
            End If
        End If
        Return obDefault
    End Function

    Shared Function getobEinheitFromNr(oNr As Integer, Optional DefaultEinheit As Integer = wb_Global.obEinheitKilogramm) As String
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
        Select Case KompType
            Case wb_Global.KomponTypen.KO_TYPE_ARTIKEL, wb_Global.KomponTypen.KO_TYPE_STUECK
                Return wb_Global.wbEinheitStk
            Case wb_Global.KomponTypen.KO_TYPE_METER
                Return wb_Global.wbEinheitMeter
            Case wb_Global.KomponTypen.KO_TYPE_SAUER_WASSER, wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE
                Return wb_Global.wbEinheitLiter
            Case Else
                Return wb_Global.wbEinheitKilogramm
        End Select
    End Function

    ''' <summary>
    ''' Gibt den Text der LosArt zurück. Wenn als Such-Text schon die LosArt als Text übergeben wird,
    ''' liefert die Funktion den SuchText zurück.
    ''' Ist die LosArt gleich "Stück" (oder entsprechend der Fremdsprache) oder liefert der Index Null
    ''' zurück wird als Ergebnis False zurückgegeben, ist die LosArt ungleich "Stück" wird True zurückgegeben.
    ''' 
    ''' Wenn die LosArt nicht im Dictionary enthalten ist, wird False zurückgegeben. Die LosArt wird
    ''' dann nicht geändert.
    ''' </summary>
    ''' <param name="LosArt"></param>
    ''' <returns></returns>
    Shared Function getLosArtText(ByRef LosArt As String) As String
        'wenn LosArt ein numerischer Wert ist, wird der Index ermittelt
        Dim idx = wb_Functions.StrToInt(LosArt)

        'Prüfen ob die LosArt gleich "0" ist. Exit mit False
        If LosArt = "0" Then
            LosArt = GetLosArtFromIdx(idx)
            Return False
        End If

        'Wenn LosArt ein numerischer Wert ist, wird der entsprechende String ermittelt
        If idx > 0 Then
            LosArt = GetLosArtFromIdx(idx)
            Return True
        End If

        'LosArt wurde schon als Text übergeben - Prüfen ob der Text gültig ist
        If LosArtText.ContainsKey(LosArt) Then
            If LosArtText(LosArt) = 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Public Shared Function GetLosArtFromIdx(Idx As Integer) As String
        If LosArtIdx.ContainsKey(Idx) Then
            Return LosArtIdx(Idx)
        Else
            Return ""
        End If
    End Function

    Private Shared Sub ReadWinBackEinheiten()
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
                _OBEinheiten = False
            End If

            'zur Liste hinzufügen
            Einheiten.Add(E.Nr, E)
            If Not EinhText.ContainsKey(E.Einheit) Then
                EinhText.Add(E.Einheit, E)
            End If
        End While
    End Sub

    Private Shared Sub ReadOrgaBackLosArt()
        Dim LosIdx As Integer
        Dim LosText As String

        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Lese alle Datensätz aus dbo.LosArten
        If OrgasoftMain.sqlSelect(wb_Sql_Selects.mssqlLosArt) Then
            While OrgasoftMain.Read
                LosIdx = OrgasoftMain.iField("LosArt")
                LosText = OrgasoftMain.sField("Bezeichnung")
                'Dictionary aufbauen
                LosArtIdx.Add(LosIdx, LosText)
                LosArtText.Add(LosText, LosIdx)
            End While
        End If
        'Datenbank-Verbindung wieder schliessen
        OrgasoftMain.Close()
    End Sub

    ''' <summary>
    ''' Prüft ob die Datenbank alle notwendigen Daten und Einträge enthält.
    ''' Die Datenbank muss Einträge für die Benutzerrechte(Gruppe -1) enthalten:
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CheckDB() As Boolean
        'Datenbank-UpdateFile (Update WinBack.Datenbank kann das Problem lösen)
        _UpdateDatabaseFile = "2.30_Einheiten.sql"

        'alle Parameter(Update) prüfen
        If Not _OBEinheiten Then
            _ErrorText = "Fehler in Tabelle winback.Einheiten. Spalte Einheiten.E_obNr fehlt !"
            Trace.WriteLine("@E_" & _ErrorText)
            Return False
        Else
            _ErrorText = ""
            Return True
        End If
    End Function

End Class
