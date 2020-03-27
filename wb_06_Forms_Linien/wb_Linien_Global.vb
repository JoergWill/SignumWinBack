Public Class wb_Linien_Global
    Private Shared _LGruppen As New Dictionary(Of String, wb_Global.wb_LinienGruppe)
    Private Shared _Linien As New Dictionary(Of String, wb_Global.wb_Linien)
    Private Shared _LinienListe As New ArrayList
    Private Shared _LinienGruppen As New SortedList
    Private Shared _ArtikelLinienGruppen As New SortedList
    Private Shared _RezeptLinienGruppen As New SortedList
    Private Shared _ErrorText As String = ""
    Private Shared _TabelleLinienGruppenOK As Boolean = True
    Private Shared _NoEntryInItemParameter As Boolean = False
    Public Shared DefaultProdFiliale As Integer = wb_Global.UNDEFINED

    ''' <summary>
    ''' Alle Liniengruppen mit Index größer oder gleich 100 [wb_global.OffsetBackorte]. Entspricht allen Liniengruppen für die Aufarbeitung
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property ArtikelLinienGruppen As SortedList
        Get
            Return _ArtikelLinienGruppen
        End Get
    End Property

    ''' <summary>
    ''' Alle Liniengruppen mit Index kleiner 100 [wb_global.OffsetBackorte]. Entspricht allen Liniengruppen für die Teigherstellung
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property RezeptLinienGruppen As SortedList
        Get
            Return _RezeptLinienGruppen
        End Get
    End Property

    Public Shared ReadOnly Property ErrorText As String
        Get
            Return _ErrorText
        End Get
    End Property

    Public Shared ReadOnly Property UpdateDatabaseFile As String
        Get
            Return "2.30_Liniengruppen_*.sql"
        End Get
    End Property

    Public Shared ReadOnly Property CheckDB() As Boolean
        Get
            If _TabelleLinienGruppenOK Then
                Return True
            Else
                _ErrorText = "Tabelle WinBack.Liniengruppen muss erweitert werden! (Formular-Steuerung)"
                Return False
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Linien As ArrayList
        Get
            Return _LinienListe
        End Get
    End Property
    ''' <summary>
    ''' Array Liniengruppen aufbauen
    ''' Array Linien aufbauen
    ''' </summary>
    Shared Sub New()
        If wb_GlobalSettings.pVariante <> wb_Global.ProgVariante.WinBack Then
            '05.07.2019/Fonk - MFF200 ist kein Auswahlfeld mehr (nur noch Text)
            '                  Daten aus dbo.ArtikelMultifunktionsfeld werden nicht mehr berücksichtigt
            'GetOrgaBackOrte()
        End If
        InitLinienGruppen()
        InitLinien()
    End Sub

    ''' <summary>
    ''' Kopiert alle Backorte aus OrgaBack in die Tabelle winback.Liniengruppen. 
    ''' Der Backort steht in OrgaBack in Artikel-Multifunktionsfeld (Auswahlfeld) zum Artikel
    ''' 
    ''' Die Auswahlfeld-Inhalte stehen in der Tabelle dbo.ArtikelMultifunktionsfeld mit Gruppen-Nr=3
    ''' Zur Linien-Nummer wird in WinBack ein Offset(100) dazu addiert)
    ''' 
    '''     05.07.2019/Fonk
    '''     Daten aus dbo.ArtikelMultifunktionsfeld werden nicht mehr berücksichtigt
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
        _LGruppen.Clear()

        'alle Linien-Gruppen
        _LinienGruppen.Clear()
        'Rezept - Liniengruppen mit Index < 100 (Teigmacherei)
        _RezeptLinienGruppen.Clear()
        'Artikel - Liniengruppen mit Index >= 100 (Produktion/Aufarbeitung)
        _ArtikelLinienGruppen.Clear()
        'Check Tabelle Liniengruppe ist aktuell
        If (winback.FieldCount <= 11) And (winback.FieldCount > 5) Then
            Trace.WriteLine("Tabelle WinBack.Liniengruppen muss erweitert werden! (Startzeit)")
            _TabelleLinienGruppenOK = False
        End If

        While winback.Read
            Try
                'Liniengruppe
                L.LinienGruppe = winback.iField("LG_Nr")
                L.Bezeichnung = winback.sField("LG_Bezeichnung")
                L.Abteilung = winback.sField("LG_Abteilung")
                'Startzeit Aufarbeitungs-Linie
                L.StartZeit = winback.sField("LG_StartZeit")

                'Linien in der Liniengruppe
                Linien = winback.sField("LG_Linien")
                L.Linien = Linien.Split(",")

                'Formularsteuerung
                If winback.FieldCount > 5 Then
                    'Liniengruppe KurzName
                    Try
                        L.KurzName = winback.sField("LG_KurzName")
                    Catch
                    End Try
                    L.BackZettelDrucken = winback.sField("LG_BZ_Drucken")
                    L.TeigZettelDrucken = winback.sField("LG_TZ_Drucken")
                    L.TeigRezeptDrucken = winback.sField("LG_TR_Drucken")
                    L.BackZettelSenden = winback.sField("LG_BZ_Senden")
                    L.TeigZettelSenden = winback.sField("LG_TZ_Senden")
                Else
                    'Kurzname wird aus Linien gebildet
                    L.KurzName = Linien
                    'Erweiterung Tabelle Liniengruppen ist notwendig !
                    Trace.WriteLine("Tabelle WinBack.Liniengruppen muss erweitert werden! (Formular-Steuerung)")
                    _TabelleLinienGruppenOK = False
                End If

                'zum Dictonary hinzufügen
                _LGruppen.Add(L.LinienGruppe, L)
                'SortedList
                _LinienGruppen.Add(L.LinienGruppe, L.Bezeichnung)

                'SortedList für Teigmacherei/Produktion-Aufarbeitung
                If L.LinienGruppe >= wb_Global.OffsetBackorte Then
                    _ArtikelLinienGruppen.Add(L.LinienGruppe, L.Bezeichnung)
                Else
                    _RezeptLinienGruppen.Add(L.LinienGruppe, L.Bezeichnung)
                End If
            Catch
                Trace.WriteLine("Fehler beim Lesen der Tabelle WinBack.Liniengruppen ")
                _TabelleLinienGruppenOK = False
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
        _Linien.Clear()
        _LinienListe.Clear()

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
                    Linie.Filiale = DefaultProdFiliale
                    Trace.WriteLine("Tabelle WinBack.Linien muss erweitert werden! (Filiale)")
                End If
                'zum Dictonary hinzufügen
                _Linien.Add(Linie.Linie, Linie)
                'zur Liste aller Linien hinzufügen
                _LinienListe.Add(Linie)
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
        If _LGruppen.ContainsKey(LinienGruppe) Then
            Return _LGruppen(LinienGruppe).Bezeichnung
        Else
            Return "- " & LinienGruppe.ToString & " -"
        End If
    End Function

    ''' <summary>
    ''' Gibt die Startzeit der Liniengruppe zurück. 
    ''' Wenn keine Liniengruppe vorhanden ist, oder keine Startzeit eingetragen wurde, wird ein wbNODATE zurückgegeben.
    ''' </summary>
    ''' <param name="LinienGruppe"></param>
    ''' <returns></returns>
    Shared Function GetStartzeit(LinienGruppe As Integer) As DateTime
        If _LGruppen.ContainsKey(LinienGruppe) Then
            Dim Result As DateTime
            If DateTime.TryParse(_LGruppen(LinienGruppe).StartZeit, Result) Then
                Return Result
            Else
                Return wb_Global.wbNODATE
            End If
        Else
            Return wb_Global.wbNODATE
        End If
    End Function

    ''' <summary>
    ''' Gibt die erste Produktions-Linie der Liniengruppe zurück.
    ''' </summary>
    ''' <param name="LinienGruppe"></param>
    ''' <returns></returns>
    Shared Function GetLinieFromLinienGruppe(LinienGruppe As Integer) As Integer
        If _LGruppen.ContainsKey(LinienGruppe) Then
            Return wb_Functions.StrToInt(_LGruppen(LinienGruppe).Linien(0))
        Else
            Return wb_Global.UNDEFINED
        End If
    End Function

    Shared Function GetLinienGruppeFromName(Bezeichnung As String) As Integer
        For Each L In _LGruppen
            If L.Value.Bezeichnung = Bezeichnung Then
                Return L.Value.LinienGruppe
            End If
        Next
        Return wb_Global.UNDEFINED
    End Function

    ''' <summary>
    ''' Gibt den Kurznamen der Liniengruppe zurück.
    ''' </summary>
    ''' <param name="LinienGruppe"></param>
    ''' <returns></returns>
    Shared Function GetKurzNameFromLinienGruppe(LinienGruppe As Integer) As String
        If _LGruppen.ContainsKey(LinienGruppe) Then
            Return _LGruppen(LinienGruppe).KurzName
        Else
            Return "-"
        End If
    End Function

    ''' <summary>
    ''' Gibt die erste Liniengruppe zurück, welche die übergegebene Linie enthält
    ''' </summary>
    ''' <param name="Linie"></param>
    ''' <returns></returns>
    Friend Shared Function GetLinienGruppeFromLinie(Linie As Integer) As Integer
        For Each lg In _LGruppen
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
        If _Linien.ContainsKey(Linie) Then
            Return _Linien(Linie).Filiale
        Else
            Return wb_Global.UNDEFINED
        End If

    End Function

    ''' <summary>
    ''' Prüft ob eine Liniengruppe in der Liste schon existiert
    ''' </summary>
    ''' <param name="LinienGruppe"></param>
    ''' <returns></returns>
    Public Shared Function ExistLinienGruppe(LinienGruppe As Integer) As Boolean
        Return _LGruppen.ContainsKey(LinienGruppe)
    End Function

    ''' <summary>
    ''' Ändert die Liniengruppen-Einträge in winback.Rezepte wenn
    ''' in der Liniengruppen-Tabelle die Nummer geändert wurde
    ''' </summary>
    ''' <param name="LinienGruppeAlt"></param>
    ''' <param name="LinienGruppeNeu"></param>
    ''' <returns></returns>
    Public Shared Function ChangeLinienGruppe(LinienGruppeAlt As String, LinienGruppeNeu As String) As Boolean
        'Ergebnis vorbelegen
        _ErrorText = ""

        'Prüfen ob die Linien-Gruppen-Nummer gültig ist
        If (wb_Functions.StrToInt(LinienGruppeNeu) < wb_Global.OffsetBackorte) And (wb_Functions.StrToInt(LinienGruppeNeu) > 0) Then
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'Datensatz neu anlegen - Result True nur wenn kein Fehler auftritt
            If LinienGruppeAlt <> "" Then
                If (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlChangeLinienGruppe, LinienGruppeNeu, LinienGruppeAlt)) < 0) Then
                    _ErrorText = "Fehler beim Anpassen der Rezeptkopfdaten !" & vbCrLf & "Bitte die Liniengruppen in den Rezepturen überprüfen"
                    ChangeLinienGruppe = False
                Else
                    'kein Fehler
                    _ErrorText = ""
                    ChangeLinienGruppe = True
                End If
            End If

            'Verbindung wieder schliessen
            winback.Close()
            'Liste neu aufbauen
            InitLinienGruppen()
            Return ChangeLinienGruppe
        Else
            _ErrorText = "Die Liniengruppen-Nummer ist ungültig [1..99]"
            Return False
        End If
    End Function

    ''' <summary>
    ''' Ändert die Liniengruppen-Einträge in winback.Rezepte wenn
    ''' in der Liniengruppen-Tabelle die Nummer geändert wurde
    ''' </summary>
    ''' <param name="BackortAlt"></param>
    ''' <param name="BackortNeu"></param>
    ''' <returns></returns>
    Public Shared Function ChangeBackort(BackortAlt As String, BackortNeu As String) As Boolean
        'Ergebnis vorbelegen
        _ErrorText = ""

        'Prüfen ob der neue Backort gültig ist
        If (wb_Functions.StrToInt(BackortNeu) >= wb_Global.OffsetBackorte) And (wb_Functions.StrToInt(BackortNeu) < 255) Then

            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Datensatz neu anlegen - Result True nur wenn kein Fehler auftritt
            If BackortAlt <> "" Then
                If (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlChangeBackort, BackortNeu, BackortAlt, wb_Global.T300_LinienGruppe)) < 0) Then
                    _ErrorText = "Fehler beim Anpassen der Artikeldaten !" & vbCrLf & "Bitte die Backorte in den Artikeldaten überprüfen"
                    ChangeBackort = False
                Else
                    'kein Fehler
                    _ErrorText = ""
                    ChangeBackort = True
                End If
            End If

            'Verbindung wieder schliessen
            winback.Close()
            'Liste neu aufbauen
            InitLinienGruppen()
            Return ChangeBackort
        Else
            _ErrorText = "Der Backort ist ungültig [100..254]"
            Return False
        End If
    End Function

    ''' <summary>
    ''' Neue Liniengruppe in WinBack.Liniengruppen anlegen.
    ''' Wenn keine Liniengruppen-Nummer übergeben wird (-1) dann wird die nächsthöhere LG-Nummer angelegt.
    ''' </summary>
    ''' <param name="LG_Nr"></param>
    ''' <param name="Backort"></param>
    ''' <returns></returns>
    Shared Function AddLinienGruppe(Optional Backort As Boolean = False, Optional ByVal LG_Nr As Integer = wb_Global.UNDEFINED) As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Anlegen der neuen Liniengruppe"

        'Wenn die neue Linie-Gruppen-Nummer nicht vorgegeben ist
        If LG_Nr = wb_Global.UNDEFINED Then
            'neue Linie-Gruppen-Nummer ermitteln
            For Each lg In _LGruppen
                If (lg.Value.LinienGruppe < wb_Global.OffsetBackorte) Or Backort Then
                    LG_Nr = Math.Max(lg.Value.LinienGruppe, LG_Nr) + 1
                End If
            Next
            'Prüfen ob die maximale Anzahl der Liniengruppen erreicht ist
            If Not Backort And (LG_Nr = wb_Global.OffsetBackorte) Then
                While _LGruppen.ContainsKey(LG_Nr)
                    LG_Nr -= 1
                End While
            End If
        End If

        'Prüfen ob die Linien-Gruppen-Nummer noch nicht verwendet ist
        If Not _LGruppen.ContainsKey(LG_Nr) Then
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'sql-Kommando INSERT bilden
            Dim sqlFeld = "LG_Nr"
            Dim sqlData = LG_Nr.ToString

            'Wenn ein Aufarbeitungsplatz/Backort angefügt wird, ist die Linien-Nummer gleich der Liniengruppen-Nummer
            If Backort Then
                sqlFeld = sqlFeld & ", LG_Linien"
                sqlData = sqlData & "," & LG_Nr.ToString
            End If

            'Datensatz neu anlegen
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewLinienGruppe, sqlFeld, sqlData))
            winback.Close()

            'Liste neu aufbauen
            InitLinienGruppen()
            'Einfügen erfolgreich
            _ErrorText = ""
            Return True
        Else
            'Fehler beim Anlegen der neuen Liniengruppe
            _ErrorText = "Liniengruppe " & LG_Nr & " ist schon vorhanden !"
            Return False
        End If

    End Function

    Shared Function DeleteLinienGruppe(Lg_Nr As Integer) As Boolean
        'Default Fehlermeldung
        _ErrorText = "Fehler beim Löschen der Liniengruppe"
        'Prüfen ob die Linien-Gruppen-Nummer existiert
        If _LGruppen.ContainsKey(Lg_Nr) Then
            'Datenbank-Verbindung öffnen - MySQL
            Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

            'Prüfen ob die Liniengruppe noch verwendet wird
            If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptLinienGruppe, Lg_Nr.ToString)) Then
                If winback.Read Then
                    If winback.iField("Used") = 0 Then
                        'Liniengruppe löschen
                        winback.CloseRead()
                        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDeleteLinienGruppe, Lg_Nr.ToString)) Then
                            _ErrorText = ""
                            Return True
                        End If
                    Else
                        'Liniengruppe wird noch verwendet
                        _ErrorText = "Liniengruppe " & Lg_Nr & " wird noch verwendet !"
                        Return False
                    End If
                    Return False
                End If
            Else
                Return False
            End If
        Else
            'Liniengruppe existiert nicht
            Return False
        End If
        Return False
    End Function
End Class
