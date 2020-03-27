''' <summary>
''' Enthält die komplette Chargenliste als Liste von ChargenSchritten (wb_ChargenSchritt).
''' Jeder Chargenschritt hat Parent und Child. Die Chargenliste beginnt am Knoten(0) ohne Parent.
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
Public Class wb_Chargen

    Private _RootChargenSchritt As New wb_ChargenSchritt(Nothing)
    Private _SQLChargenSchritt As New wb_ChargenSchritt(Nothing)
    Private _ChargenSchritt As wb_ChargenSchritt

    ''' <summary>
    ''' Erster (unsichtbarer) Chargen-Schritt (Root-Node)
    ''' </summary>
    ''' <returns>wb_ChargenSchritt - Root-Node des Rezeptes</returns>
    Public ReadOnly Property RootChargenSchritt As wb_ChargenSchritt
        Get
            Return _RootChargenSchritt
        End Get
    End Property


    ''' <summary>
    ''' Liest alle Datensätze aus wbdaten zur angegeben Tageswechselnummer sortiert nach Produktionsdatum ein 
    ''' </summary>
    ''' <param name="StatistikType">Integer Tageswechsel-Nummer</param>
    Public Function MySQLdbSelect_ChargenSchritte(StatistikType As wb_Global.StatistikType)
        Dim ArtikelKopfZeile As wb_ChargenSchritt = _RootChargenSchritt
        Dim RezeptKopfZeile As wb_ChargenSchritt = Nothing
        Dim RezeptNr As Integer = wb_Global.UNDEFINED
        Dim RezeptIdx As Integer = wb_Global.UNDEFINED

        'Istwerte Chargen/Rezeptzeile
        Dim GesamtStueck As Double = 0.0
        Dim GesamtMenge As Double = 0.0
        'Sollwerte Chargezeile
        Dim Sollmenge_Stk As Double = 0.0

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        Dim Value As Object
        Dim sql As String = ""

        'Sortier-Kriterium
        Dim TwSort As String
        Select Case wb_Chargen_Shared.SortKriterium
            Case wb_Global.ChargenListeSortKriterium.ArtikelName
                TwSort = "B_ARZ_Bezeichnung"
            Case wb_Global.ChargenListeSortKriterium.ArtikelNummer
                TwSort = "B_ARZ_KA_NrAlNum"
            Case wb_Global.ChargenListeSortKriterium.Produktion
                TwSort = "B_ARZ_Timestamp"
            Case Else
                TwSort = "B_ARZ_Timestamp"
        End Select

        'Abfrage aus wbdaten abhängig vom Statistik-Typ
        Select Case StatistikType
            Case wb_Global.StatistikType.ChargenAuswertung
                'Abfrage nach Tageswechsel-Nummer
                Dim TwNr As Integer = wb_Chargen_Shared.Liste_TagesWechselNummer
                sql = SqlStatistikChargen(TwNr, TwSort)
            Case wb_Global.StatistikType.StatistikRezepte
                'Abfrage nach Rezeptnummern aus Liste
                Dim TwStrt As Integer = MySQLdbStrtTWNr(wb_Chargen_Shared.FilterVon, wb_Chargen_Shared.UhrzeitVon)
                Dim twEnde As Integer = MySQLdbEndeTWNr(wb_Chargen_Shared.FilterBis, wb_Chargen_Shared.UhrzeitBis)
                sql = SqlStatistikRezepte(TwStrt, TwEnde, TwSort)

            Case Else
                'Abfrage nach Tageswechsel-Nummer (-1)
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlChargenDetails, -1, TwSort)
        End Select

        'Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                'Schleife über alle Datensätze
                Do
                    For i = 0 To winback.MySqlRead.FieldCount - 1
                        'Felder mit Typ DateTime müssen speziell eingelesen werden
                        If winback.MySqlRead.GetFieldType(i).Name = "DateTime" Then
                            Value = winback.MySqlRead.GetMySqlDateTime(i)
                        Else
                            Value = winback.MySqlRead.GetValue(i)
                        End If
                        'Felder einlesen
                        MySQLdbRead_Fields(winback.MySqlRead.GetName(i), Value)
                    Next

                    'Chargen mit gleicher Artikel/Rezeptnummer zusammenfassen
                    If RezeptNr <> _SQLChargenSchritt.RezeptNr Then
                        If _SQLChargenSchritt.ChrgType = wb_Global.ChargenTypen.CHRG_ARTIKEL Then
                            _SQLChargenSchritt.Type = wb_Global.KomponTypen.KO_ZEILE_ARTIKEL
                        Else
                            _SQLChargenSchritt.Type = wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
                        End If
                        'Artikelzeilen hängen immer am ersten (Dummy)Schritt
                        ArtikelKopfZeile = New wb_ChargenSchritt(_RootChargenSchritt)
                        'Daten aus MySQL in Produktionsschritt kopieren
                        ArtikelKopfZeile.CopyFrom(_SQLChargenSchritt)
                        ArtikelKopfZeile.Status = wb_Global.UNDEFINED

                        'Rezeptnummer merken
                        RezeptNr = _SQLChargenSchritt.RezeptNr
                        RezeptIdx = wb_Global.UNDEFINED
                        'Sollmenge in Stück gesamt
                        Sollmenge_Stk = 0
                    End If

                    'Zeile Rezeptkopf anfügen
                    If (RezeptIdx >= _SQLChargenSchritt.SchrittIndex) Or (RezeptIdx = wb_Global.UNDEFINED) Then
                        _SQLChargenSchritt.Type = wb_Global.KomponTypen.KO_ZEILE_REZEPT
                        'Rezeptzeilen hängen immer an der Artikelzeile
                        RezeptKopfZeile = New wb_ChargenSchritt(ArtikelKopfZeile)
                        'Soll-Stückzahl in ArtikelKopfZeile
                        Sollmenge_Stk += _SQLChargenSchritt.Sollmenge_Stk
                        ArtikelKopfZeile.Sollmenge_Stk_gesamt = Sollmenge_Stk
                        'Daten aus MySQL in Produktionsschritt kopieren
                        RezeptKopfZeile.CopyFrom(_SQLChargenSchritt)
                        RezeptKopfZeile.Status = wb_Global.UNDEFINED

                        'Rezeptindex merken
                        RezeptIdx = _SQLChargenSchritt.SchrittIndex
                    End If

                    'Zeile Rezeptschritt anfügen
                    _SQLChargenSchritt.Type = wb_Global.KomponTypen.KO_ZEILE_KOMPONENTE
                    _ChargenSchritt = New wb_ChargenSchritt(RezeptKopfZeile)
                    'Daten aus MySQL in Produktionsschritt kopieren
                    _ChargenSchritt.CopyFrom(_SQLChargenSchritt)

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
    ''' 
    ''' B_ARS_Status
    '''   0  -  Nicht bearbeitet
    '''   1  -  in Bearbeitung
    '''   2  -  Okay
    '''   3  -  Warnung
    '''   4  -  Fehler bei manueller Verwiegung
    '''   5  -  Fehler bei automatischer Verwiegung
    '''   6  -  Multistart markiert
    '''   7  -  Nachtstart
    '''   8  -  Start gespeichert (Multistart)
    '''   
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
        'Debug.Print("BAK_ArbRezepte-BAK_ArbRZ_Schritte " & Name & " " & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                'Produktionsauftrags-Nummer
                Case "B_ARZ_Best_Nr"
                    _SQLChargenSchritt.AuftragsNummer = Value
                'Chargen-Nummer
                Case "B_ARZ_Charge_Nr"
                    _SQLChargenSchritt.ChargenNummer = Value
                'Typ (Artikel oder Rezept-Zeile)
                Case "B_ARZ_Typ"
                    _SQLChargenSchritt.ChrgType = wb_Functions.StrToInt(Value)
                'Status
                Case "B_ARS_Status"
                    _SQLChargenSchritt.Status = wb_Functions.StrToInt(Value)
               'Artikelnummer(alpha)
                Case "B_ARZ_KA_NrAlNum"
                    _SQLChargenSchritt.ArtikelNummer = Value
               'Bezeichnung
                Case "B_ARZ_Bezeichnung"
                    _SQLChargenSchritt.ArtikelBezeichnung = Value
                Case "B_RZ_Bezeichnung"
                    _SQLChargenSchritt.RezeptBezeichnung = Value
                Case "B_KO_Bezeichnung"
                    _SQLChargenSchritt.KomponentenBezeichnung = Value
               'Rezeptnummer(alpha)
                Case "B_RZ_Nr_AlNum"
                    _SQLChargenSchritt.RezeptNummer = Value
                'Komponenten-Nummer(alpha)
                Case "B_KO_Nr_AlNum"
                    _SQLChargenSchritt.KomponentenNummer = Value
                'Komponentennummer(intern)
                Case "B_ARS_Ko_Nr"
                    _SQLChargenSchritt.KomponentenNr = wb_Functions.StrToInt(Value)
                'Komponententype
                Case "B_KT_Typ_Nr"
                    _SQLChargenSchritt.KomponentenType = wb_Functions.IntToKomponType(wb_Functions.StrToInt(Value))
                'Parameter-Nummer
                Case "B_ARS_ParamNr"
                    _SQLChargenSchritt.KomponentenParamNr = wb_Functions.StrToInt(Value)

                'Linie
                Case "B_ARZ_LiBeh_Nr"
                    _SQLChargenSchritt.LinienGruppe = wb_Functions.StrToInt(Value) - 100

                'Rezeptnummer(intern)
                Case "B_ARZ_Nr"
                    _SQLChargenSchritt.RezeptNr = wb_Functions.StrToInt(Value)
                'Rezeptvariante - wird auf 1 gesetzt falls keine Variante angeben ist
                Case "B_ARZ_RZ_Variante_Nr"
                    _SQLChargenSchritt.RezeptVar = Value

                'Rezeptschritt
                Case "B_ARS_Schritt_Nr"
                    _SQLChargenSchritt.Schritt = wb_Functions.StrToInt(Value)
                'Rezeptschritt-Index
                Case "B_ARS_Index"
                    _SQLChargenSchritt.SchrittIndex = wb_Functions.StrToInt(Value)

                'Sollwert
                Case "B_ARZ_Sollmenge_kg"
                    _SQLChargenSchritt.Sollmenge_kg = wb_Functions.StrToDouble(Value)
                Case "B_ARZ_Sollmenge_stueck"
                    _SQLChargenSchritt.Sollmenge_Stk = wb_Functions.StrToDouble(Value)
                Case "B_ARS_Wert"
                    _SQLChargenSchritt.Sollwert = Value

                'Istwert
                Case "B_ARS_Istwert"
                    _SQLChargenSchritt.Istwert = Value

                'Parameter
                Case "B_ARS_RS_Par1"
                    _SQLChargenSchritt.RS_Par1 = Value
                Case "B_ARS_RS_Par2"
                    _SQLChargenSchritt.RS_Par2 = Value
                Case "B_ARS_RS_Par3"
                    _SQLChargenSchritt.RS_Par3 = Value

                    'Einheit
                Case "B_E_Einheit"
                    _SQLChargenSchritt.Einheit = Value

                'Zeit
                Case "B_ARS_Gestartet"
                    _SQLChargenSchritt.StartZeit = wb_sql_Functions.MySQLDateTimeToDate(Value)
                Case "B_ARS_Beendet"
                    _SQLChargenSchritt.EndeZeit = wb_sql_Functions.MySQLDateTimeToDate(Value)

               'User
                Case "B_ARS_User_Name"
                    _SQLChargenSchritt.User = Value
               'User-Nummer
                Case "B_ARS_User_Nr"
                    _SQLChargenSchritt.UserNummer = wb_Functions.StrToInt(Value)
            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function

    ''' <summary>
    ''' SQL-Statement erstellen für Statistik Chargen
    ''' </summary>
    ''' <param name="TwNr"></param>
    ''' <param name="TwSort"></param>
    ''' <returns></returns>
    Private Function SqlStatistikChargen(TwNr As Integer, TwSort As String) As String
        'TODO ANFANG ENDE TW-NUMMER
        Return wb_Sql_Selects.setParams(wb_Sql_Selects.sqlChargenDetails, TwNr, TwSort)
    End Function

    ''' <summary>
    ''' SQL-Statement erstellen für Statistik Rezepte
    ''' </summary>
    ''' <param name="TwStrt"></param>
    ''' <param name="TwEnde"></param>
    ''' <param name="TwSort"></param>
    ''' <returns></returns>
    Private Function SqlStatistikRezepte(TwStrt As Integer, TwEnde As Integer, TwSort As String) As String

        'sql-Anweisung - Liste aller Rezepte aus der Liste (Idx)
        Dim sqlRezepte As String = ""
        For Each rzNr In wb_Chargen_Shared.NrListe
            If sqlRezepte <> "" Then
                sqlRezepte += " OR "
            End If
            sqlRezepte += "BAK_ArbRezepte.B_ARZ_Nr=" & rzNr.ToString
        Next

        'sql-Anweisung - Liste aller Linien aus der Liste (Idx)
        Dim sqlLinien As String = ""
        For Each LNr In wb_Chargen_Shared.NrLinien
            If sqlLinien <> "" Then
                sqlLinien += " OR "
            End If
            sqlLinien += "BAK_ArbRezepte.B_ARZ_LiBeh_Nr =" & LNr.ToString
        Next

        'sql-Abfrage abhängig von den Filter-Kriterien erstellen
        Dim sql As String = wb_Sql_Selects.sqlStatRezepte

        'Wassertemperatur mit anzeigen
        If wb_Chargen_Shared.WasserTempAusblenden Then
            sql += " WHERE (BAK_ArbRZSchritte.B_ARS_ParamNr = 1)"
        Else
            sql += " WHERE ((BAK_ArbRZSchritte.B_ARS_ParamNr = 1) OR BAK_ArbRZSchritte.B_ARS_ParamNr = 3)"
        End If

        'Istwert = Null unterdrücken
        If wb_Chargen_Shared.IstwertNullAusblenden Then
            sql += " AND (BAK_ArbRZSchritte.B_ARS_Gestartet > 0)"
        End If

        'Datum/Uhrzeit einschränken
        'FormatDateTime('yyyymmddhhnnss', eDatumVon.date + dtStartTime.Time)
        If wb_Chargen_Shared.UhrzeitVon <> wb_Global.wbNODATE Then
            sql += " AND B_ARS_Gestartet >= '" & wb_Chargen_Shared.FilterVon.ToString("yyyy-MM-dd") & " " & wb_Chargen_Shared.UhrzeitVon.ToString("HH:mm:ss") & "'"
        End If
        If wb_Chargen_Shared.UhrzeitBis <> wb_Global.wbNODATE Then
            sql += " AND B_ARS_Gestartet <= '" & wb_Chargen_Shared.FilterBis.ToString("yyyy-MM-dd") & " " & wb_Chargen_Shared.UhrzeitBis.ToString("HH:mm:ss") & "'"
        End If

        'Nach Linien filtern
        If Not wb_Chargen_Shared.AlleLinien Then
            sql += " AND (" & sqlLinien & ")"
        End If

        'Nach Rezepten filtern
        If sqlRezepte <> "" Then
            sql += " AND (" & sqlRezepte & ")"
        End If

        'Tageswechsel-Nummern
        sql += " AND (Tageswechsel.TW_Nr >= " & TwStrt.ToString & " AND Tageswechsel.TW_Nr <= " & TwEnde.ToString & ") "

        'Sortierkriterium
        sql += "ORDER BY B_ARZ_Bezeichnung, B_ARZ_TW_Nr, B_ARZ_TW_Idx, B_ARS_TW_Idx, B_ARZ_Charge_Nr"

        Return sql
    End Function

    Private Function MySQLdbStrtTWNr(FilterVon As Date, UhrzeitVon As Date) As String
        'SQL-Abfrage startet einen Tag früher wenn die Startzeit angegeben wurde
        If UhrzeitVon <> wb_Global.wbNODATE Then
            FilterVon = FilterVon.AddDays(-1)
        End If
        'sql-Abfrage Tageswechsel-Nummer 
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlTWNrStrt, FilterVon.ToString("yyyy-MM-dd"))
        Return MySQLdbGetTWNr(sql)
    End Function

    Private Function MySQLdbEndeTWNr(FilterBis As Date, UhrzeitBis As Date) As String
        'SQL-Abfrage endet einen Tag später wenn die Endezeit angegeben wurde
        If UhrzeitBis <> wb_Global.wbNODATE Then
            FilterBis = FilterBis.AddDays(1)
        End If
        'sql-Abfrage Tageswechsel-Nummer 
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlTWNrEnde, FilterBis.ToString("yyyy-MM-dd"))
        Return MySQLdbGetTWNr(sql)
    End Function

    Private Function MySQLdbGetTWNr(sql) As String
        'Datenbank-Verbindung öffnen - MySQL
        Dim wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)

        'Datensätze aus Tabelle Tageswechsel lesen
        If wbdaten.sqlSelect(sql) Then
            If wbdaten.Read Then
                Return wbdaten.iField("TW_Nr").ToString
            End If
        End If

        'Fehler bei der Abfrage der Daten oder keine Daten vorhanden
        Return "-1"
    End Function
End Class
