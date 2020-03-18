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
    ''' <param name="TwNr">Integer Tageswechsel-Nummer</param>
    Public Function MySQLdbSelect_ChargenSchritte(TwNr As Integer)
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
        Dim sql As String

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

        'Abfrage nach Tageswechsel-Nummer
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlChargenDetails, TwNr, TwSort)

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

End Class
