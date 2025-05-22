Imports WinBack
''' <summary>
''' Enthält den komplette Produktionsplan als Liste von Produktionschritten (wb_Produktionsschritt).
''' Jeder Produktionschritt hat Parent und Child. Die Produktion beginnt am Knoten(0) ohne Parent.
''' 
'''     Schritt 0                                            (Child = Schritt 1, Schritt 2...)
'''         +   Schritt 1               (Parent = Schritt 0)
'''         +   Schritt 2               (Parent = Schritt 0)
'''         +   Schritt 3               (Parent = Schritt 0) (Child = Schritt 3.1, Schritt 3.2)
'''                 +   Schritt 3.1     (Parent = Schritt 3)
'''                 +   Schritt 3.2     (Parent = Schritt 3)
'''         +   Schritt 4               (Parent = Schritt 0)
'''         + ...
'''         
''' Die Anzeige erfolgt im VirtualTree direkt mit der Angabe des Root-Nodes
''' </summary>
Public Class wb_Produktion_virtuell

    Private _RootProduktionsSchritt As New wb_Produktionsschritt(Nothing, "")
    Private _SQLProduktionsSchritt As New wb_Produktionsschritt(Nothing, "")
    Private _ProduktionsSchritt As wb_Produktionsschritt
    Private _RezeptProduktionsSchritt As wb_Produktionsschritt


    ''' <summary>
    ''' Erster (unsichtbarer) Produktions-Schritt (Root-Node)
    ''' </summary>
    ''' <returns>wb_Produktionsschritt - Root-Node des Rezeptes</returns>
    Public ReadOnly Property RootProduktionsSchritt As wb_Produktionsschritt
        Get
            Return _RootProduktionsSchritt
        End Get
    End Property

    ''' <summary>
    ''' Alle offenen Produktions-Schritte ausgehen von RootProduktionsSchritt
    ''' virtuell durchführen.
    ''' Die einzelnen Chargen werden nacheinander als quittiert (Status = 3) markiert und im mit dem Lagerbestand verrechent.
    ''' In winback.Lieferungen werden die entsprechenden Rohstoff-Chargen als verbraucht eingetragen.
    ''' 
    ''' Wenn alle Schritte einer Rezeptur durchlaufen sind, wird die TW-Nummer für diese Charge auf -1 gesetzt und mit dem 
    ''' nächsten Tageswechsel (WinBack-Produktion) dann in die wbdaten geschrieben.
    ''' 
    ''' Ist das Flag Tageswechsel auf True gesetzt, werden die Daten gleich in die wbdaten geschrieben (Tageswechsel)
    ''' und die Datensätze aus winback.ArbRezepte und winback.ArbRZSchritte gelöscht.
    ''' </summary>
    ''' <returns></returns>
    Public Function VirtProduktion(LinieNr As Integer, VarianteNr As Integer, Optional Tageswechsel As Boolean = False) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback As wb_Sql = Nothing
        Dim wbDaten As wb_Sql = Nothing

        Dim TWNr As Integer = wb_Global.UNDEFINED
        Dim LfdIdx As Integer = 0
        Dim LfdRzp As Integer = 0

        'DB-Verbindung winback
        winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Daten gleich in die wbdaten schreiben (Tageswechsel)
        If Tageswechsel Then
            'DB-Verbindung wbdaten
            wbDaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
            'neue Tageswechselnummer
            TWNr = wb_sql_Functions.getNewTWNummer(wbDaten, LinieNr, Now, Now)
        End If

        'prüfen ob die TW-Nummer gültig ist
        If TWNr > 0 Or Not Tageswechsel Then

            'alle Produktions-Schritte ausgehend vom Root-Knoten
            For Each ArtikelZeile As wb_Produktionsschritt In RootProduktionsSchritt.ChildSteps
                'Debug.Print("Produktion Artikel " & ArtikelZeile.ArtikelNummer & vbTab & ArtikelZeile.ArtikelBezeichnung)

                'Artikelzeilen ohne Rezeptur (Aufarbeitung)
                If ArtikelZeile.ChildSteps.Count = 0 Then
                    'Fortlaufender Index BAK_ArbRezepte
                    LfdRzp += 1
                    'Fortlaufender Index BAK_ArbRZSchritte
                    LfdIdx += 1

                    'Soll/Ist-Produktions-Menge in kg
                    ArtikelZeile.Sollwert = ArtikelZeile.Sollwert_kg
                    ArtikelZeile.Istwert = ArtikelZeile.Sollwert
                    'Einheit anpassen

                    'Artikel-Zeile wird (virtuell) beabeitet und als fertig markiert (abgeleitet von der Artikelzeile, da eine Rezeptzeile nicht vorhanden ist)
                    VirtProduktionRezept(ArtikelZeile, LinieNr, winback, wbDaten, TWNr, LfdRzp)
                    'Komponenten-Zeile wird (virtuell) beabeitet und als fertig markiert (abgeleitet von der Artikelzeile, da eine Komponentenzeile nicht vorhanden ist)
                    VirtProduktionRezeptSchritt(ArtikelZeile, LinieNr, winback, wbDaten, TWNr, LfdIdx)
                Else
                    For Each RezeptZeile As wb_Produktionsschritt In ArtikelZeile.ChildSteps
                        'Fortlaufender Index BAK_ArbRezepte
                        LfdRzp += 1
                        'Debug.Print("Produktion Rezept " & RezeptZeile.RezeptNummer & vbTab & RezeptZeile.RezeptBezeichnung)

                        For Each KomponentenZeile As wb_Produktionsschritt In RezeptZeile.ChildSteps
                            'Fortlaufender Index BAK_ArbRZSchritte
                            LfdIdx += 1
                            Debug.Print("Produktion Zeile " & KomponentenZeile.ArtikelNummer & vbTab & KomponentenZeile.ArtikelBezeichnung & vbTab & KomponentenZeile.Sollwert)
                            'Rezept-Schritt wird (virtuell) bearbeitet und als fertig markiert
                            VirtProduktionRezeptSchritt(KomponentenZeile, LinieNr, winback, wbDaten, TWNr, LfdIdx)
                        Next

                        'Rezept-Zeile wird (virtuell) beabeitet und als fertig markiert
                        VirtProduktionRezept(RezeptZeile, LinieNr, winback, wbDaten, TWNr, LfdRzp)
                    Next
                End If

                'Artikel/Halbfertig-Produkt als Zugang in WinBack-Lager einbuchen
                Dim LagerOrt As String = VirtProduktionLager(ArtikelZeile, winback)
                'wenn ein LagerOrt definiert ist(Artikel/Rezept gefunden)
                If LagerOrt <> "" Then
                    'Artikel/Halbfertig-Produkt als Zugang in WinBack-Lieferungen einbuchen (alle Rezeptschritte mit Chargen-Nummer)
                    VirtProduktionLieferung(ArtikelZeile, LagerOrt, TWNr, winback)
                End If

            Next
        End If

        'wenn die Produktions-Schritte gleich in wbdaten geschrieben worden sind, Datensätze aus ArbRezepte und ArbRZSchritte löschen
        If Tageswechsel Then
            VirtProduktionDelete(LinieNr, winback)
        End If

        'Datenbank-Verbindungen wieder schliessen
        winback.Close()
        wbDaten.Close()
        Return True
    End Function

    Private Sub VirtProduktionRezeptSchritt(ByRef Zeile As wb_Produktionsschritt, LinieNr As Integer, winback As wb_Sql, wbdaten As wb_Sql, TWNr As Integer, LfdIdx As Integer)
        'Update/Insert-Kommando
        Dim sql As String

        'Produktion Zeile verarbeiten
        VirtProduktionStart(Zeile, LinieNr, winback)

        'wenn eine gültige Tageswechsel-Nummer übergeben wurde 
        If TWNr <> wb_Global.UNDEFINED Then
            'Rezeptschritte gleich in wbdaten.BAK_ArbRZSchritte schreiben
            sql = "B_ARS_TW_Nr = " & TWNr & ", B_ARS_TW_Idx = " & LfdIdx & ", B_ARS_Beh_Nr = " & LinieNr + wb_Global.OffsetBackorte & ", " &
                  "B_ARS_RunIdx = " & Zeile.RunIndex & ", B_ARS_RZ_Nr = " & Zeile.RezeptNr & ", B_ARS_Index = " & Zeile.ARS_Index & ", " &
                  "B_ARS_Charge_Nr = " & Zeile.iChargenNummer & ", B_ARS_Art_Index = " & Zeile.ArtikelIndex & ", " &
                  "B_ARS_Schritt_Nr = " & Zeile.Schritt & ", B_ARS_Schritt_SubNr = 0, B_ARS_Ko_Nr = " & Zeile.KO_Nr & ", " &
                  "B_ARS_ParamNr = " & Zeile.ParamNr & ", B_ARS_Wert = '" & Zeile.Sollwert & "', B_ARS_Wert_org = '0', B_ARS_RS_Wert = '0', " &
                  "B_ARS_RS_Par1 = '', B_ARS_RS_Par2 = '', B_ARS_RS_Par3 = '', B_ARS_Istwert = '" & Zeile.Istwert & "', " &
                  "B_ARS_Gestartet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', B_ARS_Beendet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "B_ARS_User_Nr = " & wb_GlobalSettings.AktUserNr & ", B_ARS_User_Name = '" & wb_GlobalSettings.AktUserName & "', B_ARS_Status = " & Zeile.Status & ", " &
                  "B_KO_Nr_AlNum = '" & Zeile.KO_Nummer & "', B_KO_Bezeichnung = '" & Zeile.KO_Bezeichnung & "', " &
                  "B_KO_Temp_Korr = 0, B_KT_Rezept = 'R', B_KT_Bezeichnung = 'Sollmenge', B_KT_KurzBez = 'Menge', B_KT_EinheitIndex = 1, " &
                  "B_KT_Format = 3, B_KT_Laenge = 3, B_KT_UnterGW = '0,000', B_KT_OberGW = '999,9', B_ARS_Preis = '', B_ARS_PreisEinheit = 0, " &
                  "B_KT_Typ_Nr = " & wb_Functions.KomponTypeToInt(Zeile.Typ) & ", B_E_Einheit = '" & Zeile.Einheit & "', B_ARS_BF_Charge = '" & Zeile.KO_Charge & "'"
            'Insert-Kommando
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.InsertBAKArbRZSchritte, sql)

            'Datensatz aktualisieren/einfügen
            wbdaten.sqlCommand(sql)
        Else
            'Rezeptschritte in winback.ArbRZSchritte aktualisieren
            sql = "ARS_TW_Nr = -2, ARS_Istwert = '" & Zeile.Istwert & "', " &
                  "ARS_Gestartet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "ARS_Beendet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "ARS_Status = " & Zeile.Status & ", ARS_BF_Charge = '" & Zeile.ChargenNummer & "'"
            'Update-Kommando
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.UpdateArbRZSchritte, LinieNr + wb_Global.OffsetBackorte, Zeile.ChargenNummer, Zeile.RunIndex, sql)

            'Datensatz aktualisieren/einfügen
            winback.sqlCommand(sql)
        End If
    End Sub

    Private Sub VirtProduktionRezept(ByRef Zeile As wb_Produktionsschritt, LinieNr As Integer, winback As wb_Sql, wbdaten As wb_Sql, TWNr As Integer, LfdIdx As Integer)
        'Update/Insert-Kommando
        Dim sql As String

        'wenn eine gültige Tageswechsel-Nummer übergeben wurde 
        If TWNr <> wb_Global.UNDEFINED Then
            'Rezeptschritte gleich in wbdaten.BAK_ArbRezepte schreiben. B_ARZ_Status darf nicht (NULL) sein
            sql = "B_ARZ_TW_Nr = " & TWNr & ", B_ARZ_TW_Idx = " & LfdIdx & ", B_ARZ_LiBeh_Nr = " & LinieNr + wb_Global.OffsetBackorte & ", " &
                  "B_ARZ_Index = " & Zeile.ARZ_Index & ", B_ARZ_Charge_Nr = " & Zeile.ChargenNummer & ", B_ARZ_Best_Nr = '" & Zeile.AuftragsNummer & "', " &
                  "B_ARZ_Nr = " & Zeile.RezeptNr & ", B_ARZ_RZ_Variante_Nr = " & Zeile.RezeptVar & ", B_ARZ_Bezeichnung = '" & Zeile.RezeptBezeichnung & "', " &
                  "B_ARZ_Typ = " & Zeile.Typ & ", B_ARZ_Erststart = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "B_ARZ_Art_Einheit = 11, B_ARZ_Sollmenge_kg = '" & Zeile.Sollwert_kg & "', B_ARZ_Sollmenge_stueck = " & Zeile.Sollmenge_Stk & ", " &
                  "B_ARZ_Anstellgut_kg = '100', B_ARZ_Status = 'V', " &
                  "B_ARZ_Zp_Gestartet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', B_ARZ_Zp_Beendet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "B_ARZ_KA_NrAlNum ='" & Zeile.ArtikelNummer & "', B_ARZ_Art_Index = " & Zeile.ArtikelIndex & ", B_ARZ_RZ_Typ = " & Zeile.RezeptVar & ", " &
                  "B_RZ_Nr_AlNum = '" & Zeile.RezeptNummer & "', B_RZ_Bezeichnung = '" & Zeile.RezeptBezeichnung & "', " &
                  "B_RZ_Gewicht ='0'"
            'Insert-Kommando
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.InsertBAKArbRezepte, sql)

            'Datensatz in ArbRezepte aktualisieren/einfügen
            wbdaten.sqlCommand(sql)
        Else
            'Rezeptschritte in winback.ArbRZSchritte aktualisieren
            sql = "ARZ_TW_Nr = -2, ARZ_Zp_Gestartet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "ARZ_Zp_Beendet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', ARZ_Status = '" & Zeile.Status & "', ARZ_geloescht = 1"
            'Update-Kommando
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.UpdateArbRezepte, LinieNr + wb_Global.OffsetBackorte, Zeile.ARZ_Index, sql)

            'Datensatz in ArbRezepte aktualisieren/einfügen
            winback.sqlCommand(sql)
        End If
    End Sub

    ''' <summary>
    ''' Löscht alle Datensätze zu dieser Linie mit TW-Nummer = 0 aus winback.ArbRezepte und winback.ArbRZSchritte
    ''' </summary>
    ''' <param name="LinieNr"></param>
    ''' <param name="winback"></param>
    Public Sub VirtProduktionDelete(LinieNr As Integer, winback As wb_Sql)
        'Update/Insert-Kommando
        Dim sql As String

        'Löscht alle Datensätze aus ArbRZSchritte
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.DelArbRZSchritte, LinieNr + wb_Global.OffsetBackorte)
        winback.sqlCommand(sql)

        'Löscht alle Datensätze aus ArbRezepte
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.DelArbRezepte, LinieNr + wb_Global.OffsetBackorte)
        winback.sqlCommand(sql)

    End Sub

    ''' <summary>
    ''' Setzt Istwert gleich Sollwert und bucht die entsprechende Menge vom Produktions-Lager ab.
    ''' Die aktuelle Chargen-Nummer wird aus der Tabelle Lieferungen ermittelt und in die wbDaten-Tabellen eingetragen
    ''' Abschliessend wird die Bilanzmenge in der Tabelle Lagerorte korrigiert.
    ''' </summary>
    ''' <param name="Zeile"></param>
    ''' <param name="LinieNr"></param>
    ''' <param name="winback"></param>
    Public Sub VirtProduktionStart(ByRef Zeile As wb_Produktionsschritt, LinieNr As Integer, winback As wb_Sql)

        'alle Zeilen mit Sollwert
        If wb_Functions.TypeHatEinheit(Zeile.KO_Typ) Then

            'Istwert gleich Sollwert
            Zeile.Istwert = Zeile.Sollwert

            'alle Zeilen mit Verwiegung
            If wb_Functions.TypeIstSollMenge(Zeile.KO_Typ, Zeile.ParamNr) Then
                'Objekt Lieferungen erzeugen und dort den Verbrauch verbuchen. Aktualisiert auch die Bilanzmenge im Lagerort.
                Dim Lieferungen As New wb_Lieferungen
                'Wenn eine Rohstoff-Chargen-Nummer existiert, wird diese zurückgemeldet und in die Charge eingetragen
                Zeile.KO_Charge = Lieferungen.ProduktionVerbuchen(Zeile.LagerOrt, Zeile.Istwert)
                'Speicher wieder freigeben
                Lieferungen = Nothing
            End If

        End If

        'Artikel-Zeilen (Aufarbeitung)
        If Zeile.KO_Typ = wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
            Zeile.Einheit = wb_Einheiten_Global.GetEinheitFromNr(wb_Global.wbEinheitKilogramm)
            Zeile.Typ = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE
            Zeile.KO_Bezeichnung = Zeile.ArtikelBezeichnung
            Zeile.KO_Nummer = Zeile.ArtikelNummer
            Zeile.Istwert = Zeile.Sollwert
        End If

        'Zeile wurde bearbeitet (Hand/Quittiert)
        Zeile.Status = wb_Global.ChargenStatus.CS_WARNUNG
    End Sub

    ''' <summary>
    ''' Aktualisiert die Tabelle winback.Lagerort
    ''' Die Bilanzmenge wird entsprechend der (virtuell) produzierten Menge erhöht.
    ''' </summary>
    ''' <param name="ArtikelZeile"></param>
    ''' <param name="winback"></param>
    ''' <returns></returns>
    Private Function VirtProduktionLager(ByRef ArtikelZeile As wb_Produktionsschritt, winback As wb_Sql) As String
        'Lagerort und Bilanzmenge zu ArtikelNummer(alpha) und RezeptNr(Intern) ermitteln
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.SelectArtikelLagerOrt, ArtikelZeile.ArtikelNummer, ArtikelZeile.RezeptNr)
        Dim LagerOrt As String = ""

        'wenn es einen Artikel/Rohstoff mit Lagerort zu diesem Produktions-Schritt(Artikel-Zeile) gibt
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                'LagerOrt
                LagerOrt = winback.sField("KA_Lagerort")
                'BilanzMenge
                Dim BilanzMenge As Double = wb_Functions.StrToDouble(winback.sField("LG_Bilanzmenge"))
                'Verbindung wieder freigeben
                winback.CloseRead()

                'Bilanzmenge aktualisieren
                BilanzMenge += ArtikelZeile.VirtTreeSumSollwerte
                'und wieder in den winback.Lagerorte zurückschreiben
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateLagerort, wb_Global.UNDEFINED, wb_Functions.FormatStr(BilanzMenge, 3), LagerOrt)
                winback.sqlCommand(sql)
            End If
        End If
        Return LagerOrt
    End Function

    ''' <summary>
    ''' Aktualisiert die Tabelle winback.Lieferungen.
    ''' Pro Rezept-Zeile (Charge) wird eine neue Zeile mit Produktions-Menge und Chargen-Nummer erzeugt.
    ''' </summary>
    ''' <param name="ArtikelZeile"></param>
    ''' <param name="LagerOrt"></param>
    ''' <param name="TWNr"></param>
    ''' <param name="winback"></param>
    Private Sub VirtProduktionLieferung(ByRef ArtikelZeile As wb_Produktionsschritt, LagerOrt As String, TWNr As Integer, winback As wb_Sql)
        'nächste Nummer in winback.Lieferungen zu diesem LagerOrt
        Dim LfdNr As Integer = 1
        'der INSERT-Befehl wird dynamisch erzeugt
        Dim sql, sql_Insert As String        '

        'letzten Eintrag aus winback.Lieferungen
        winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlLieferLfd, LagerOrt))
        'wenn Datensätze vorhanden sind
        If winback.Read Then
            'laufende Nummer plus Eins
            LfdNr = winback.iField("LF_Nr") + 1
        End If
        winback.CloseRead()

        'Schleife über alle Rezeptschritte
        For Each RezeptZeile As wb_Produktionsschritt In ArtikelZeile.ChildSteps
            'der INSERT-Befehl wird dynamisch erzeugt
            sql_Insert = "'" & LagerOrt & "', " & LfdNr & ", '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                         "'" & RezeptZeile.VirtTreeIstwert & "', 'RzNr: " & RezeptZeile.RezeptNummer &
                         "', '1', 'RzBez: " & RezeptZeile.RezeptBezeichnung & "', " & "0" & ", '" &
                         TWNr.ToString & "-" & RezeptZeile.ChargenNummer & "'," & RezeptZeile.LinienGruppe &
                         ", NULL, '0,0', " & wb_GlobalSettings.AktUserNr & ", '', " & "0"

            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlInsertWE, sql_Insert)
            'INSERT ausführen
            winback.sqlCommand(sql)
            'nächste Lieferung
            LfdNr += 1
        Next
    End Sub

    ''' <summary>
    ''' Liest alle Datensätze aus winback.ArbRezepte und winback.ArbRZSchritte mit Tageswechselnummer = 0 sortiert nach Chargen-Nummer ein 
    ''' </summary>
    ''' <param name="LinieNr">Integer Tageswechsel-Nummer</param>
    Public Function MySQLdbSelect_ArbRzSchritte(LinieNr As Integer, VarianteNr As Integer) As Boolean
        Dim Root As wb_Produktionsschritt = _RootProduktionsSchritt
        Dim ArtikelNummer As String = ""
        Dim ChargenNummer As String = ""
        Dim GesamtStueck As Integer = 0

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim Value As Object
        Dim sql As String

        'Abfrage nach Linie 
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.ArbRezepte, LinieNr + wb_Global.OffsetBackorte)

        'Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                'Schleife über alle Datensätze
                Do
                    For i = 0 To winback.MySqlRead.FieldCount - 1
                        'Felder mit Typ DateTime müssen speziell eingelesen werden
                        If winback.MySqlRead.GetFieldType(i).Name = "DateTime" Then
                            '                            Value = winback.MySqlRead.GetMySqlDateTime(i)
                        Else
                            Value = winback.MySqlRead.GetValue(i)
                            'Felder einlesen
                            MySQLdbRead_Fields(winback.MySqlRead.GetName(i), Value)
                        End If
                    Next

                    'Chargen mit gleicher Artikel/Rezeptnummer zusammenfassen
                    If ArtikelNummer <> _SQLProduktionsSchritt.ArtikelNummer Then
                        'Der Root-Knoten enthält die Summe aller Chargen in Stück
                        Root.Sollmenge_Stk = GesamtStueck
                        GesamtStueck = 0

                        'Artikelzeilen hängen immer am ersten (Dummy)Schritt
                        Root = _RootProduktionsSchritt
                        'Neue Zeile  einfügen (ArtikelZeile)
                        Root = New wb_Produktionsschritt(Root, _SQLProduktionsSchritt.ArtikelBezeichnung)
                        'Daten aus MySQL in Produktionsschritt kopieren
                        Root.CopyFrom(_SQLProduktionsSchritt)
                        Root.Typ = wb_Global.KomponTypen.KO_ZEILE_ARTIKEL

                        'Artikelnummer merken
                        ArtikelNummer = _SQLProduktionsSchritt.ArtikelNummer
                    End If

                    'Rezeptzeile anfügen
                    If ChargenNummer <> _SQLProduktionsSchritt.ChargenNummer Then
                        'Gesamtstück(Artikelzeile) berechnen
                        GesamtStueck += _SQLProduktionsSchritt.Sollmenge_Stk
                        'ChargenNummer merken
                        ChargenNummer = _SQLProduktionsSchritt.ChargenNummer

                        'Rezeptzeile anfügen - wenn eine Rezeptur vorhanden ist
                        If _SQLProduktionsSchritt.KO_Nr > 0 Then
                            'Neue Zeile  einfügen (RezeptZeile)
                            _RezeptProduktionsSchritt = New wb_Produktionsschritt(Root, _SQLProduktionsSchritt.ArtikelBezeichnung)
                            'Daten aus MySQL in Produktionsschritt kopieren
                            _SQLProduktionsSchritt.Typ = wb_Global.KomponTypen.KO_ZEILE_REZEPT
                            _RezeptProduktionsSchritt.CopyFrom(_SQLProduktionsSchritt)
                        End If
                    End If

                    'Komponentenzeile anfügen - wenn eine Rezeptur vorhanden ist
                    If _SQLProduktionsSchritt.KO_Nr > 0 Then
                        'Neue Zeile  einfügen (RezeptZeile)
                        _ProduktionsSchritt = New wb_Produktionsschritt(_RezeptProduktionsSchritt, _SQLProduktionsSchritt.KO_Bezeichnung)
                        'Daten aus MySQL in Produktionsschritt kopieren
                        _SQLProduktionsSchritt.Typ = wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE
                        _ProduktionsSchritt.CopyFromRezeptschritt(_SQLProduktionsSchritt)
                    End If

                Loop While winback.MySqlRead.Read

                'Gesamtstückmenge in den (letzten) Root-Knoten eintragen
                Root.Sollmenge_Stk = GesamtStueck

                'alle Datensätze eingelesen
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Objekt-Eigenschaften
    ''' </summary>
    ''' <param name="Name">String - Spalten-Name aus Datenbank</param>
    ''' <param name="Value">Object - Wert aus Datenbank</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Fields(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If
        Dim iValue As Integer = wb_Functions.StrToInt(Value)

        'Debug
        'Debug.Print("ArbRezepte-ArbRZ_Schritte " & Name & " " & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name
                'Chargen-Nummer
                Case "ARZ_Charge_Nr"
                    _SQLProduktionsSchritt.ChargenNummer = Value
                'Index
                Case "ARZ_Index"
                    _SQLProduktionsSchritt.ARZ_Index = iValue
                'Index
                Case "ARS_Index"
                    _SQLProduktionsSchritt.ARS_Index = iValue
                Case "ARZ_Art_Index"
                    _SQLProduktionsSchritt.ArtikelIndex = iValue
                'Schritt
                Case "ARS_Schritt_Nr"
                    _SQLProduktionsSchritt.Schritt = iValue
               'ParamNr
                Case "ARS_ParamNr"
                    _SQLProduktionsSchritt.ParamNr = iValue
                'Index (Run-Index)
                Case "ARS_RunIdx"
                    _SQLProduktionsSchritt.RunIndex = iValue
                'Produktionsauftrags-Nummer
                Case "ARZ_Best_Nr"
                    _SQLProduktionsSchritt.AuftragsNummer = Value
                'Typ (Artikel oder Rezept-Zeile)
                Case "ARZ_Typ" 'TODO in genormten Typ umsetzen wb_global.wbArtikel...
                    _SQLProduktionsSchritt.Typ = Value
                Case "ARS_KT_Typ_Nr" 'TODO in genormten Typ umsetzen wb_global.wbArtikel...
                    _SQLProduktionsSchritt.KO_Typ = wb_Functions.IntToKomponType(iValue)
               'Artikelnummer(alpha)
                Case "ARZ_KA_NrAlNum"
                    _SQLProduktionsSchritt.ArtikelNummer = Value
                'Komponentennummer(alpha) 
                Case "ARS_KO_Nr_AlNum"
                    _SQLProduktionsSchritt.KO_Nummer = Value
                'Bezeichnung
                Case "ARZ_Bezeichnung"
                    _SQLProduktionsSchritt.ArtikelBezeichnung = Value
                'Komponentenbezeichnung
                Case "ARS_KO_Bezeichnung"
                    _SQLProduktionsSchritt.KO_Bezeichnung = Value
               'KomponentenNr(intern)
                Case "ARS_Ko_Nr"
                    _SQLProduktionsSchritt.KO_Nr = iValue
                'Lagerort
                Case "KA_Lagerort"
                    _SQLProduktionsSchritt.LagerOrt = Value
                'Rezeptbezeichnung
                Case "ARZ_RZ_Bezeichnung"
                    _SQLProduktionsSchritt.RezeptBezeichnung = Value
               'Rezeptnummer(alpha)
                Case "ARZ_RZ_Nr_AlNum"
                    _SQLProduktionsSchritt.RezeptNummer = Value
                'Rezeptnummer(intern)
                Case "ARZ_Nr"
                    _SQLProduktionsSchritt.RezeptNr = iValue
                'Rezeptvariante - wird auf 1 gesetzt falls keine Variante angeben ist
                Case "ARZ_RZ_Variante_Nr"
                    _SQLProduktionsSchritt.RezeptVar = iValue
                'Linie
                Case "ARZ_LiBeh_Nr"
                    _SQLProduktionsSchritt.LinienGruppe = iValue - 100
                'Sollwert
                Case "ARZ_Sollmenge_kg"
                    _SQLProduktionsSchritt.Sollwert_kg = wb_Functions.StrToDouble(Value)
                'Sollwert Komponente 
                Case "ARS_Wert"
                    _SQLProduktionsSchritt.Sollwert = Value
                'Einheit Komponente 
                Case "ARS_E_Einheit"
                    _SQLProduktionsSchritt.KO_Einheit = Value
                'Sollmenge Stück
                Case "ARZ_Sollmenge_stueck"
                    _SQLProduktionsSchritt.Sollmenge_Stk = wb_Functions.StrToDouble(Value)

            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function

End Class
