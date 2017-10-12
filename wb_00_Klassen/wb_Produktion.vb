Public Class wb_Produktion

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

    Private _RootProduktionsSchritt As New wb_Produktionsschritt(Nothing, "")
    Private _SQLProduktionsSchritt As New wb_Produktionsschritt(Nothing, "")
    Private _ProduktionsSchritt As wb_Produktionsschritt

    ''' <summary>
    ''' Erster (unsichtbarer) Produktions-Schritt (Root-Node)
    ''' </summary>
    ''' <returns>wb_RezeptSchritt - Root-Node des Rezeptes</returns>
    Public ReadOnly Property RootRezeptSchritt As wb_Produktionsschritt
        Get
            Return _RootProduktionsSchritt
        End Get
    End Property

    ''' <summary>
    ''' Fügt eine (Artikel)Charge an die bestehende Liste an.
    ''' Der Artikel-Datensatz wird aus der Datenbank eingelesen und als Kopfdatensatz eingefügt. Die Rezeptzeilen werden entsprechend der Chargen-Aufteilung angefügt.
    ''' </summary>
    ''' <param name="Nummer">String - Artikelnummer(alpha)</param>
    ''' <param name="Nr">Integer - interne Artikelnummer</param>
    ''' <param name="Sollwert">Double - Sollmenge in Stück</param>
    ''' <returns></returns>
    Public Function AddArtikelCharge(Tour As String, Nummer As String, Nr As Integer, Sollwert As Double, Optional Bestellmenge As Double = wb_Global.UNDEFINED, Optional Bestellt_SonderText As String = "", Optional Sollwert_TeilungText As String = "") As Boolean
        Dim Artikel As New wb_Komponenten
        If Artikel.MySQLdbRead(Nr, Nummer) Then
            'Artikelzeilen hängen immer am ersten (Dummy)Schritt
            Dim Root As wb_Produktionsschritt = _RootProduktionsSchritt
            Dim Rzpt As wb_Produktionsschritt

            'Neue Zeile  einfügen (ArtikelZeile)
            Root = New wb_Produktionsschritt(Root, Artikel.Bezeichung)

            'Daten aus dem Komponenten-Stamm in Produktionsschritt kopieren
            Root.CopyFromKomponenten(Artikel, wb_Global.wbDatenArtikel)
            Root.Sollwert_Stk = Sollwert
            Root.Tour = Tour
            Root.Bestellt_Stk = CalcBestellMenge(Bestellmenge, Sollwert)
            Root.Bestellt_SonderText = Bestellt_SonderText
            Root.Sollwert_TeilungText = Sollwert_TeilungText

            'Chargengrößen berechnen



            'Rezeptkopf-Zeile anhängen
            Rzpt = New wb_Produktionsschritt(Root, Artikel.RezeptName)
            'Daten aus dem Komponenten-Stamm in Produktionsschritt kopieren
            Rzpt.CopyFromKomponenten(Artikel, wb_Global.wbDatenRezept)
            Rzpt.Sollwert_kg = 10
            Rzpt.Tour = Tour
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Gibt einen Wert für die Bestellmenge zurück. Ist die Bestellmenge undefiniert wird die Bestellmenge gleich der Sollmenge gesetzt.
    ''' </summary>
    ''' <param name="BestellMenge"></param>
    ''' <param name="Sollwert"></param>
    ''' <returns>BestellMenge - Double - Wenn die Bestellmenge undefiniert ist, wird die Sollmenge zurückgegeben</returns>
    Private Function CalcBestellMenge(BestellMenge As Double, Sollwert As Double) As Double
        If BestellMenge > 0 Then
            Return BestellMenge
        Else
            Return Sollwert
        End If
    End Function

    ''' <summary>
    ''' Berechnung der Chargengrößen abhängig von Min/Max/Optimal
    ''' Charge. Zurückgegeben werden die Anzahl der Optimal- und
    ''' Restchargen und Größe von Optimal/Restchargen.
    ''' 
    ''' Modus
    '''     wb_global.XGleiche            '(M1) Aufteilung in gleich große Chargen
    '''     wb_global.NurOptimal          '(00) Aufteilung nur in Optimal-Chargen
    '''     wb_global.OptimalUndRest      '(01) Aufteilung in Optimal- und Rest-Chargen
    '''     wb_global.MaximalUndRest      '(02) Aufteilung in Maximal- und Rest-Chargen
    '''     wb_global.RezeptGroesse       '(09) Aufteilung in Rezept-Größe (keine Chargen angegeben)
    '''     
    ''' Result
    '''     wb_global.OK                  'Chargenaufteilung in Ordnung
    '''     wb_global.EM1                 'Nach Aufteilung in Optimalchargen bleibt eine Restmenge offen, die nicht produziert werden kann
    '''     wb_global.EM2                 'Nach Aufteilung in Optimalchargen wird mehr produziert als gefordert
    '''     wb_global.EP1                 'Sollmenge nicht erreicht, Restmenge unterhalb Mindestcharge
    '''     wb_global.EP9                 'Keine Chargengrößen angegeben, Aufteilung nach Rezeptgröße
    ''' 
    ''' Wenn das Flag Vorproduktion auf True gesetzt ist, wird
    ''' mindestens eine Charge (je nach Modus Min/Opt) produziert
    ''' damit auf alle Fälle die entsprechende Zeile In der
    ''' Produktionsplanung gesetzt wird.
    ''' </summary>
    ''' <param name="Sollwert"></param>
    ''' <param name="ChargeMin"></param>
    ''' <param name="ChargeMax"></param>
    ''' <param name="ChargeOpt"></param>
    ''' <param name="Modus"></param>
    ''' <param name="VorProduktion"></param>
    ''' <returns></returns>
    Private Function CalcChargenMenge(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, Modus As wb_Global.ModusChargenTeiler, VorProduktion As Boolean) As wb_Global.ChargenMengen
        Select Case Modus
            Case wb_Global.ModusChargenTeiler.NurOptimal
                Return CalcBatchM1(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case wb_Global.ModusChargenTeiler.NurOptimal
                Return CalcBatch00(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case wb_Global.ModusChargenTeiler.NurOptimal
                Return CalcBatch02(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case wb_Global.ModusChargenTeiler.NurOptimal
                Return CalcBatch02(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case Else
                Return CalcBatch09(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
        End Select
    End Function

    Private Function CalcBatchM1(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen
        'Es gibt keine Restchargen
        CalcBatchM1.MengeRest = 0
        CalcBatchM1.AnzahlRest = 0

        'Anzahl der Chargen - Menge durch Maximalchargen teilen
        Dim x As Integer = Math.Round(SaveDiv(Sollwert, ChargeMax), 0)

        'Teiler plus 1 ergibt die Anzahl der Chargen wenn ein Rest auftritt
        If (Sollwert - ChargeMax * x) > 0.1 Then
            CalcBatchM1.AnzahlOpt = x + 1
        Else
            CalcBatchM1.AnzahlOpt = x
        End If

        'Chargengröße berechnen
        CalcBatchM1.MengeOpt = SaveDiv(Sollwert, CalcBatchM1.AnzahlOpt)
        CalcBatchM1.Result = wb_Global.ChargenTeilerResult.OK

        'Prüfen ob wir innerhalb der Chargengrenzen sind
        If (CalcBatchM1.MengeOpt < ChargeMin) Then
            'Abweichung ermitteln wenn Minimal-Charge verwendet wird
            Dim m1 As Double = CalcBatchM1.AnzahlOpt * ChargeMin
            Dim a1 As Double = Math.Abs(Sollwert - m1)

            'Abweichung ermitteln wenn eine Charge weniger gemacht wird
            Dim m2 As Double = (CalcBatchM1.AnzahlOpt - 1) * ChargeMax
            Dim a2 As Double = Math.Abs(Sollwert - m2)

            'Wenn die Abweichung bei einer Charge weniger kleiner ist - Ergebnis korrigieren
            If (a2 <= a1) And (CalcBatchM1.AnzahlOpt > 1) Then
                CalcBatchM1.AnzahlOpt = CalcBatchM1.AnzahlOpt - 1
                CalcBatchM1.MengeOpt = ChargeMax
                CalcBatchM1.Result = wb_Global.ChargenTeilerResult.EM1
            Else
                CalcBatchM1.MengeOpt = ChargeMin
                CalcBatchM1.Result = wb_Global.ChargenTeilerResult.EM2
            End If
        End If
    End Function

    Private Function CalcBatch00(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen

    End Function

    Private Function CalcBatch01(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen

    End Function

    Private Function CalcBatch02(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen

    End Function

    Private Function CalcBatch09(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen

    End Function

    Private Function SaveDiv(Divident As Double, Divisor As Double) As Double
        If (Divisor > 0) Then
            Return Divident / Divisor
        Else
            Return 0
        End If
    End Function


    ''' <summary>
    ''' Liest alle Datensätze aus wbdaten zur angegeben Tageswechselnummer sortiert nach Produktionsdatum ein 
    ''' </summary>
    ''' <param name="TwNr">Integer Tageswechsel-Nummer</param>
    Public Function MySQLdbSelect_ArbRzSchritte(TwNr As Integer)
        Dim Root As wb_Produktionsschritt = _RootProduktionsSchritt
        Dim ArtikelNummer As String = ""
        Dim GesamtStueck As Integer = 0

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        Dim Value As Object
        Dim sql As String

        'Abfrage nach Tageswechsel-Nummer
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.bakArbRezepte, TwNr)

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
                    If ArtikelNummer <> _SQLProduktionsSchritt.ArtikelNummer Then
                        'Der Root-Knoten enthält die Summe aller Chargen in Stück
                        Root.Sollwert_Stk = GesamtStueck
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
                    _SQLProduktionsSchritt.Typ = wb_Global.wbDatenRezept
                    GesamtStueck += _SQLProduktionsSchritt.Sollwert_Stk
                    _ProduktionsSchritt = New wb_Produktionsschritt(Root, _SQLProduktionsSchritt.ArtikelBezeichnung)
                    'Daten aus MySQL in Produktionsschritt kopieren
                    _ProduktionsSchritt.CopyFrom(_SQLProduktionsSchritt)

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

        'Feldname aus der Datenbank
        Debug.Print("Feldname/Wert " & Name & "/" & Value.ToString)
        Try
            Select Case Name

                'Produktionsauftrags-Nummer
                Case "B_ARZ_Best_Nr"
                    _SQLProduktionsSchritt.AuftragsNummer = Value
                'Typ (Artikel oder Rezept-Zeile)
                Case "B_ARZ_Typ"
                    _SQLProduktionsSchritt.Typ = Value
               'Artikelnummer(alpha)
                Case "B_ARZ_KA_NrAlNum"
                    _SQLProduktionsSchritt.ArtikelNummer = Value
               'Bezeichnung
                Case "B_ARZ_Bezeichnung"
                    _SQLProduktionsSchritt.ArtikelBezeichnung = Value
                Case "B_RZ_Bezeichnung"
                    _SQLProduktionsSchritt.RezeptBezeichnung = Value
               'Rezeptnummer(alpha)
                Case "B_RZ_Nr_AlNum"
                    _SQLProduktionsSchritt.RezeptNummer = Value
                'Rezeptnummer(intern)
                Case "B_ARZ_Nr"
                    _SQLProduktionsSchritt.RezeptNr = Value
                'Rezeptvariante - wird auf 1 gesetzt falls keine Variante angeben ist
                Case "B_ARZ_RZ_Variante_Nr"
                    _SQLProduktionsSchritt.RezeptVar = Value
                'Linie
                Case "B_ARZ_LiBeh_Nr"
                    _SQLProduktionsSchritt.LinienGruppe = wb_Functions.StrToInt(Value) - 100
                'Sollwert
                Case "B_ARZ_Sollmenge_kg"
                    _SQLProduktionsSchritt.Sollwert_kg = wb_Functions.StrToDouble(Value)
                Case "B_ARZ_Sollmenge_stueck"
                    _SQLProduktionsSchritt.Sollwert_Stk = wb_Functions.StrToDouble(Value)


            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function


End Class

'     // Flag Vorproduktion initialisieren
'      VorProduktion := false;
'      AuftragsNr := '';
'      Try
'        // Produktionsauftrags-Nummer
'        AuftragsNr := AQArbRezepte.fieldbyName('B_ARZ_Best_Nr').Value;
'        // wenn die Auftrags-Nummer mit 'VP' beginnt war dies eine Vorproduktions-Charge
'        If leftStr(AuftragsNr, 2) = 'VP' then
'          begin
'            // eingelesene Charge war eine Vorproduktion
'            VorProduktion := true;
'            // die ersten 2 Zeichen löschen -> Auftragsnummer gleich interne Komponenten-Nummer
'            Delete(AuftragsNr, 1, 2);
'          End
'Else
'AuftragsNr := '';

'      except;
'      End;

'      // Artikel oder Rezept-Zeile
'      Typ := AQArbRezepte.fieldbyName('B_ARZ_Typ').Value;
'      // Artikelnummer(alpha)
'      ArtikelNr := AQArbRezepte.fieldbyName('B_ARZ_KA_NrAlNum').Value;
'      // Rezeptnummer(alpha)
'      Al_RezNr := AQArbRezepte.fieldbyName('B_RZ_Nr_AlNum').Value;
'      // Rezeptnummer(intern)
'      RezeptNr := AQArbRezepte.fieldbyName('B_ARZ_Nr').Value;
'      // Rezeptvariante
'      RezeptVar := AQArbRezepte.fieldbyName('B_ARZ_RZ_Variante_Nr').Value;
'      // wenn keine Rezeptvariante angegeben ist - Variante 1
'      If RezeptVar = '0' then
'        RezeptVar := '1';
'      // Linie
'      Linie := AQArbRezepte.fieldbyName('B_ARZ_LiBeh_Nr').Value - 100;

'      // Schleife über alle ausgewählten Chargen
'      repeat
'begin
'If Typ = '1' then
'            // Sollwert der Charge In kg
'            Sollwert := AQArbRezepte.fieldbyName('B_ARZ_Sollmenge_kg').Value
'          Else
'            // Sollwert der Charge in Stück
'            Sollwert := AQArbRezepte.fieldbyName('B_ARZ_Sollmenge_stueck').Value;

'          // dyn. Array vergrößern
'          l := Length(ChargenSollwerte);
'          SetLength(ChargenSollwerte, l + 1);
'          // Sollwert der Charge
'          ChargenSollwerte[l]:= StrToFloatDef(Sollwert,0,wbdm.wbFormat);

'          // Prüfen ob Rezeptnummer noch identisch
'          // Nächster Datensatz
'          AQArbRezepte.Next;

'        End; // While Not eof()
'      until AQArbRezepte.eof Or (RezeptNr <> AQArbRezepte.fieldbyName('B_ARZ_Nr').Value);

'    // Arbeitsrezepte erzeugen
'    If Typ = '1' then
'      // Arbeitsrezept ohne Artikelnummer - Mengen In kg
'      AddnewBatch('',Al_RezNr,RezeptNr,RezeptVar,IntToStr(Linie),AuftragsNr,Startzeit,false,VorProduktion,0)
'    else
'      // Arbeitsrezept mit Artikelnummer - Mengen in Stück
'      AddnewBatch(ArtikelNr, Al_RezNr, RezeptNr, RezeptVar, IntToStr(Linie), AuftragsNr, Startzeit, False, VorProduktion, 0);

'    // Array Sollwerte wieder löschen
'    SetLength(ChargenSollwerte, 0);

'  End; // If
