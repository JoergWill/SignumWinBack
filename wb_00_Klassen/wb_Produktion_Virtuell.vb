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

                For Each RezeptZeile As wb_Produktionsschritt In ArtikelZeile.ChildSteps
                    'Fortlaufender Index BAK_ArbRezepte
                    LfdRzp += 1
                    'Debug.Print("Produktion Rezept " & RezeptZeile.RezeptNummer & vbTab & RezeptZeile.RezeptBezeichnung)

                    For Each KomponentenZeile As wb_Produktionsschritt In RezeptZeile.ChildSteps
                        'Fortlaufender Index BAK_ArbRZSchritte
                        LfdIdx += 1
                        'Debug.Print("Produktion Zeile " & KomponentenZeile.ArtikelNummer & vbTab & KomponentenZeile.ArtikelBezeichnung & vbTab & KomponentenZeile.Sollwert)
                        'Rezept-Schritt wird (virtuell) bearbeitet und als fertig markiert
                        VirtProduktionRezeptSchritt(KomponentenZeile, LinieNr, winback, wbDaten, TWNr, LfdIdx)
                    Next

                    'Rezept-Zeile wird (virtuell) beabeitet und als fertig markiert
                    VirtProduktionRezept(RezeptZeile, LinieNr, winback, wbDaten, TWNr, LfdRzp)
                Next
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
                  "B_ARS_Charge_Nr = " & Zeile.ChargenNummer & ", B_ARS_Art_Index = " & Zeile.ArtikelIndex & ", " &
                  "B_ARS_Schritt_Nr = " & Zeile.Schritt & ", B_ARS_Schritt_SubNr = 0, B_ARS_Ko_Nr = " & Zeile.KO_Nr & ", " &
                  "B_ARS_ParamNr = " & Zeile.ParamNr & ", B_ARS_Wert = '" & Zeile.Sollwert & "', B_ARS_Wert_org = '0', B_ARS_RS_Wert = '0', " &
                  "B_ARS_RS_Par1 = '', B_ARS_RS_Par2 = '', B_ARS_RS_Par3 = '', B_ARS_Istwert = '" & Zeile.Istwert & "', " &
                  "B_ARS_Gestartet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', B_ARS_Beendet = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "B_ARS_User_Nr = -1, B_ARS_User_Name = '', B_ARS_Status = " & Zeile.Status & ", " &
                  "B_KO_Nr_AlNum = '" & Zeile.KO_Nummer & "', B_KO_Bezeichnung = '" & Zeile.KO_Bezeichnung & "', " &
                  "B_KO_Temp_Korr = 0, B_KT_Rezept = 'R', B_KT_Bezeichnung = '', B_KT_KurzBez = '', B_KT_EinheitIndex = 0, " &
                  "B_KT_Format = 0, B_KT_Laenge = 0, B_KT_UnterGW = 0, B_KT_OberGW = 0, B_ARS_Preis = '', B_ARS_PreisEinheit = 0, " &
                  "B_E_Einheit = '" & Zeile.Einheit & "', B_ARS_BF_Charge = '" & Zeile.KO_Charge & "'"
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
            'Rezeptschritte gleich in wbdaten.BAK_ArbRezepte schreiben
            sql = "B_ARZ_TW_Nr = " & TWNr & ", B_ARZ_TW_Idx = " & LfdIdx & ", B_ARZ_LiBeh_Nr = " & LinieNr + wb_Global.OffsetBackorte & ", " &
                  "B_ARZ_Index = " & Zeile.ARZ_Index & ", B_ARZ_Charge_Nr = " & Zeile.ChargenNummer & ", B_ARZ_Best_Nr = '" & Zeile.AuftragsNummer & "', " &
                  "B_ARZ_Nr = " & Zeile.RezeptNr & ", B_ARZ_RZ_Variante_Nr = " & Zeile.RezeptVar & ", B_ARZ_Bezeichnung = '" & Zeile.RezeptBezeichnung & "', " &
                  "B_ARZ_Typ = 1, B_ARZ_Erststart = '" & wb_sql_Functions.MySQLdatetime(Now) & "', " &
                  "B_ARZ_Art_Einheit = 1, B_ARZ_Sollmenge_kg = '" & Zeile.Sollwert_kg & "', B_ARZ_Sollmenge_stueck = " & Zeile.Sollmenge_Stk & ", " &
                  "B_ARZ_Anstellgut_kg = '100', " &
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

    Public Sub VirtProduktionStart(ByRef Zeile As wb_Produktionsschritt, LinieNr As Integer, winback As wb_Sql)

        'alle Zeilen mit Sollwert
        If wb_Functions.TypeHatEinheit(Zeile.KO_Typ) Then

            'Istwert gleich Sollwert
            Zeile.Istwert = Zeile.Sollwert

            'Komponenten-Nummer


        End If

        'Zeile wurde bearbeitet (Hand/Quittiert)
        Zeile.Status = wb_Global.ChargenStatus.CS_WARNUNG
    End Sub

    ''' <summary>
    ''' Liest alle Datensätze aus winback.ArbRezepte und winback.ArbRZSchritte mit Tageswechselnummer = 0 sortiert nach Chargen-Nummer ein 
    ''' </summary>
    ''' <param name="LinieNr">Integer Tageswechsel-Nummer</param>
    Public Function MySQLdbSelect_ArbRzSchritte(LinieNr As Integer, VarianteNr As Integer)
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

                        'Artikelnummer merken
                        ArtikelNummer = _SQLProduktionsSchritt.ArtikelNummer
                    End If

                    'Rezeptzeile anfügen
                    If ChargenNummer <> _SQLProduktionsSchritt.ChargenNummer Then

                        'Gesamtstück(Artikelzeile) berechnen
                        GesamtStueck += _SQLProduktionsSchritt.Sollmenge_Stk
                        'Neue Zeile  einfügen (RezeptZeile)
                        _RezeptProduktionsSchritt = New wb_Produktionsschritt(Root, _SQLProduktionsSchritt.ArtikelBezeichnung)
                        'Daten aus MySQL in Produktionsschritt kopieren
                        _SQLProduktionsSchritt.Typ = wb_Global.KomponTypen.KO_ZEILE_REZEPT
                        _RezeptProduktionsSchritt.CopyFrom(_SQLProduktionsSchritt)

                        'ChargenNummer merken
                        ChargenNummer = _SQLProduktionsSchritt.ChargenNummer
                    End If

                    'Komponentenzeile anfügen
                    'Neue Zeile  einfügen (RezeptZeile)
                    _ProduktionsSchritt = New wb_Produktionsschritt(_RezeptProduktionsSchritt, _SQLProduktionsSchritt.KO_Bezeichnung)
                    'Daten aus MySQL in Produktionsschritt kopieren
                    _SQLProduktionsSchritt.Typ = wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE
                    _ProduktionsSchritt.CopyFromRezeptschritt(_SQLProduktionsSchritt)

                Loop While winback.MySqlRead.Read

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
                    _SQLProduktionsSchritt.ARZ_Index = Value
                'Index
                Case "ARS_Index"
                    _SQLProduktionsSchritt.ARS_Index = Value
                Case "ARZ_Art_Index"
                    _SQLProduktionsSchritt.ArtikelIndex = Value
                'Schritt
                Case "ARS_Schritt_Nr"
                    _SQLProduktionsSchritt.Schritt = Value
               'ParamNr
                Case "ARS_ParamNr"
                    _SQLProduktionsSchritt.ParamNr = Value
                'Index (Run-Index)
                Case "ARS_RunIdx"
                    _SQLProduktionsSchritt.RunIndex = Value
                'Produktionsauftrags-Nummer
                Case "ARZ_Best_Nr"
                    _SQLProduktionsSchritt.AuftragsNummer = Value
                'Typ (Artikel oder Rezept-Zeile)
                Case "ARZ_Typ" 'TODO in genormten Typ umsetzen wb_global.wbArtikel...
                    _SQLProduktionsSchritt.Typ = Value
                Case "ARS_KT_Typ_Nr" 'TODO in genormten Typ umsetzen wb_global.wbArtikel...
                    _SQLProduktionsSchritt.KO_Typ = wb_Functions.IntToKomponType(Value)
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
                    _SQLProduktionsSchritt.KO_Nr = Value
                'Rezeptbezeichnung
                Case "ARZ_RZ_Bezeichnung"
                    _SQLProduktionsSchritt.RezeptBezeichnung = Value
               'Rezeptnummer(alpha)
                Case "ARZ_RZ_Nr_AlNum"
                    _SQLProduktionsSchritt.RezeptNummer = Value
                'Rezeptnummer(intern)
                Case "ARZ_Nr"
                    _SQLProduktionsSchritt.RezeptNr = Value
                'Rezeptvariante - wird auf 1 gesetzt falls keine Variante angeben ist
                Case "ARZ_RZ_Variante_Nr"
                    _SQLProduktionsSchritt.RezeptVar = Value
                'Linie
                Case "ARZ_LiBeh_Nr"
                    _SQLProduktionsSchritt.LinienGruppe = wb_Functions.StrToInt(Value) - 100
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
