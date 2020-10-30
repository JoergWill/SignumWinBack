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
Public Class wb_Produktion

    Private _RootProduktionsSchritt As New wb_Produktionsschritt(Nothing, "")
    Private _SQLProduktionsSchritt As New wb_Produktionsschritt(Nothing, "")
    Private _ProduktionsSchritt As wb_Produktionsschritt
    Private _ChargenNummer() As Integer

    Private _VorProduktion As New ArrayList

    Public ReadOnly Property GetNewChargenNummer(Linie As Integer) As String
        Get
            If CheckLinie(Linie) Then
                If _ChargenNummer(Linie) = 0 Then
                    _ChargenNummer(Linie) = Linie * 1000
                Else
                    _ChargenNummer(Linie) = _ChargenNummer(Linie) + 1
                End If
                Return _ChargenNummer(Linie).ToString
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property GetChargenNummer(Linie As Integer) As String
        Get
            If CheckLinie(Linie) Then
                Return _ChargenNummer(Linie).ToString
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property GetNextChargenNummer(Linie As Integer) As String
        Get
            If CheckLinie(Linie) Then
                Return (_ChargenNummer(Linie) + 1).ToString
            Else
                Return ""
            End If
        End Get
    End Property

    Public Function CheckLinie(Linie As Integer) As Boolean
        If (Linie > 0) And (Linie < wb_Global.MaxLinien) Then
            If _ChargenNummer Is Nothing Then
                ReDim _ChargenNummer(Linie + 1)
            End If
            If _ChargenNummer.Length <= Linie Then
                ReDim _ChargenNummer(Linie + 1)
            End If
            Return True
        Else
            Return False
        End If
    End Function

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
    ''' Berechnet alle Vorproduktions-Chargen.
    ''' Die Funktion interiert über alle Einträge in der Liste Produktions-Schritte und summiert nach Rezeptnummer die 
    ''' Vorproduktions-Chargen. Der Suchlauf erfolgt rekursiv über alle Child-Produktions-Schritte beginnend von Root-Node
    ''' </summary>
    ''' <returns></returns>
    Public Function CalcVorproduktion(ps As wb_Produktionsschritt) As Boolean
        'Liste löschen
        _VorProduktion.Clear()

        'Liste aller Vorproduktions-Chargen erstellen
        _VorProduktionListe_Erzeugen(ps)

        'Vorproduktions-Liste sortieren nach Produktions-Datum und Rezept-Nummer
        _VorProduktion.Sort()

        'Vorproduktions-Liste Mengen zusammenfassen
        _VorProduktionListe_Optimieren()

        'Schleife über alle Vorproduktions-Chargen
        For Each v As wb_VorProduktionsSchritt In _VorProduktion
            'Debug.Print("Vorproduktion " & v.ArtikelNr & "/" & v.RezeptNr & "/" & v.Sollwert_kg)
            'Produktions-Auftrag zu Liste hinzufügen (auch Restchargen < MinCharge einfügen [Vorproduktion=True])
            AddChargenZeile("", "", v.ArtikelNr, 0.0, v.Sollwert_kg, 0.0, wb_Global.ModusChargenTeiler.OptimalUndRest, True, True)
        Next

        Return True
    End Function

    Public Function ReCalcVorproduktion(ps As wb_Produktionsschritt)
        'Liste löschen
        _VorProduktion.Clear()
        'Mengen Vorproduktion neu berechnen
        _VorProduktionMenge_NeuBerechnen(RootProduktionsSchritt)
        'Vorproduktions-Liste sortieren nach Produktions-Datum und Rezept-Nummer
        _VorProduktion.Sort()
        'Mengen zusammenfassen
        _VorProduktionListe_Optimieren()
        'Vorproduktion Mengen korrigieren
        _VorProduktion_MengenKorrektur()
        Return True
    End Function

    Private Function _VorProduktionListe_Erzeugen(ps As wb_Produktionsschritt)

        'Iteration über alle Produktions-Schritte
        For Each p As wb_Produktionsschritt In ps.ChildSteps
            'Debug.Print("Calc Vorproduktion " & p.ArtikelNummer & "/" & p.ArtikelBezeichnung & "/" & p.RezeptNummer & "/" & p.RezeptBezeichnung)
            If p.ChargenNummer = "VP" Then
                Dim vp As New wb_VorProduktionsSchritt
                vp.RezeptNr = p.RezeptNr
                vp.LinienGruppe = p.LinienGruppe
                vp.RezeptGroesse = 0
                vp.RezeptVar = p.RezeptVar
                vp.Sollwert_kg = p.Sollwert_kg
                vp.ArtikelNr = p.ArtikelNr
                vp.TeigChargen = p.TeigChargen
                vp.Aufloesen = p.Aufloesen
                _VorProduktion.Add(vp)
                'Charge als erledigt markieren
                p.ChargenNummer = "VP-OK"
            End If
            'Rekursiver Aufruf für die Child-Nodes
            If p.ChildSteps IsNot Nothing Then
                _VorProduktionListe_Erzeugen(p)
            End If
        Next

        Return True
    End Function

    Private Sub _VorProduktionListe_Optimieren()
        Dim l As Integer = _VorProduktion.Count
        Dim i As Integer = 1
        Dim v1 As wb_VorProduktionsSchritt
        Dim v2 As wb_VorProduktionsSchritt

        While i < l
            v1 = _VorProduktion(i - 1)
            v2 = _VorProduktion(i)

            If v1.RezeptNr = v2.RezeptNr Then
                v1.Sollwert_kg = v1.Sollwert_kg + v2.Sollwert_kg
                _VorProduktion(i - 1) = v1
                _VorProduktion.RemoveAt(i)
                l = l - 1
            Else
                i = i + 1
            End If
        End While
    End Sub

    ''' <summary>
    ''' Durchläuft alle Produktions-Schritte der Liste bis zum Schritt mit der Markierung "VP-OK"
    '''     Dieser Schritt wird gespeichert und die Rekursion an dieser Stelle beendet
    '''     Alle so ermittelten Schritte werden sortiert und zusammengefasst.
    '''     Die neu berechneten Mengen werden in die vorhandenen Chargen eingetragen !
    '''     
    ''' Dieser Schritt ist notwendig, da im ersten Iterations-Schritt nur die Mengen erfasst werden,
    ''' die aufgrund der Bestellungen benötigt werden.
    ''' Die tatsächlichen Vorproduktionsmengen können je nach Chargengrößen abweichen.
    ''' </summary>
    ''' <param name="ps"></param>
    Private Sub _VorProduktionMenge_NeuBerechnen(ps As wb_Produktionsschritt)
        'alle Child-Schritte 
        For i = 0 To ps.ChildSteps.Count - 1
            Dim p As wb_Produktionsschritt = ps.ChildSteps(i)
            'der aktuelle Schritt ist ein Vorproduktions-Schritt
            If p.ChargenNummer = "VP-OK" Then
                'Mengen und Datensatz erfassen
                Dim vp As New wb_VorProduktionsSchritt
                vp.RezeptNr = p.RezeptNr
                vp.LinienGruppe = p.LinienGruppe
                vp.RezeptGroesse = 0
                vp.RezeptVar = p.RezeptVar
                vp.Sollwert_kg = p.Sollwert_kg
                vp.ArtikelNr = p.ArtikelNr
                vp.TeigChargen = p.TeigChargen
                vp.Aufloesen = p.Aufloesen
                _VorProduktion.Add(vp)
            Else
                'prüfen ob Rohstoff mit Rezept
                If p.RezeptNr > 0 Then
                    'Rekursion - Nur wenn keine VP-OK-Markierung !
                    _VorProduktionMenge_NeuBerechnen(p)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Durchläuft alle als Vorproduktion markierten Schritte in der Liste und korrigiert die Mengen entsprechend der Liste
    ''' mit den neu berechneten Mengen
    ''' </summary>
    Private Sub _VorProduktion_MengenKorrektur()
        'Die Vorproduktions-Schritte hängen als Child am Root-Knoten
        For Each p As wb_Produktionsschritt In RootProduktionsSchritt.ChildSteps

            'Mengen-Anpassung nur für berechnet Vorproduktions-Schritte
            If p.IsVorProduktion Then
                'Suche die entsprechende Mengenzeile in der Vorproduktions-Liste
                For Each v As wb_VorProduktionsSchritt In _VorProduktion
                    'Vergleich über Artikelnummer
                    If p.RezeptNr = v.RezeptNr Then
                        'Sollwert in kg (Charge aus der ersten Iteration)
                        If p.Sollwert_kg = 0 Then
                            p.Sollwert_kg = p.StkGewicht * p.Sollmenge_Stk / 1000
                        End If
                        'Test-Ausgabe
                        Debug.Print("Charge alt " & p.RezeptNr & "/" & p.Sollwert_kg & "/(" & p.ArtikelBezeichnung & ")")
                        Debug.Print("Charge neu " & v.RezeptNr & "/" & v.Sollwert_kg)
                        'alle Child-Sätze löschen
                        p.ChildSteps.Clear()

                        'Artikeldaten aus DB lesen
                        Dim Artikel As New wb_Komponente
                        If Artikel.MySQLdbRead(p.ArtikelNr, p.ArtikelNummer) Then

                            'Chargen berechnen - Aufteilung in Optimal- und Restchargen
                            p.TeigChargen = CalcChargenMenge(v.Sollwert_kg, Artikel.ArtikelChargen.MinCharge.fMengeInkg, Artikel.ArtikelChargen.MaxCharge.fMengeInkg, Artikel.ArtikelChargen.OptCharge.fMengeInkg, wb_Global.ModusChargenTeiler.OptimalUndRest, True)

                            'Rezept-Kopf-Zeilen anhängen (Optimal-Chargen)
                            For i = 1 To p.TeigChargen.AnzahlOpt
                                AddArtikelRezeptCharge(p, Artikel, p.AuftragsNummer, p.Tour, p.TeigChargen.MengeOpt, p.TeigChargen, True)
                            Next

                            'Rezept-Kopf-Zeilen anhängen (Rest-Chargen)
                            For i = 1 To p.TeigChargen.AnzahlRest
                                AddArtikelRezeptCharge(p, Artikel, p.AuftragsNummer, p.Tour, p.TeigChargen.MengeRest, p.TeigChargen, True)
                            Next
                        End If

                    End If
                Next
            End If

        Next
    End Sub

    ''' <summary>
    ''' Fasst alle (Rest-)Teige zu einem/mehreren Teigchargen zusammen (Optimierung der Produktions-Liste)
    ''' Die Produktions-Liste ist vorher schon nach TeigNr/Tour/Artikelnr sortiert.
    ''' </summary>
    Friend Sub TeigeZusammenfassen(Modus As wb_Global.ModusTeigOptimierung)

        'Teigmenge Summe
        Dim TeigMenge As Double = 0
        'Anzahl der Produktions-Schritte die zu optimieren sind
        Dim ProdChildSteps As Integer = RootProduktionsSchritt.ChildSteps.Count - 1

        For i = 1 To ProdChildSteps
            '(Erster)Teig der vorhergehenenden Artikelzeile
            Dim p1 As wb_Produktionsschritt = RootProduktionsSchritt.ChildSteps(i - 1)
            Dim p1c0 As wb_Produktionsschritt = p1.ChildSteps(0)
            '(Erster)Teig der folgenden Artikelzeile
            Dim p2 As wb_Produktionsschritt = RootProduktionsSchritt.ChildSteps(i)
            Dim p2c0 As wb_Produktionsschritt = p2.ChildSteps(0)

            'Prüfen ob ein Zusammenfassen der Teig möglich ist (wenn die Teige der 2 Artikel identisch sind)
            'wenn die Artikel-Zeile mehrere Teig-Zeilen hat, werden diese in der Funktion 'Zusammenfassen' je nach Modus addiert
            If Zusammenfassen(p1c0, p2c0, Modus) Then
                'Teig schon berücksichtigt
                If Not p1c0.Optimiert Then
                    p1c0.Optimiert = True
                    p1c0.ChargenNummer = GetNextChargenNummer(p1.LinienGruppe)
                    TeigMenge = TeigMenge + ZusammenfassenTeigSumme(p1, Modus, p1c0.ChargenNummer) 'p1.Sollwert_kg
                    'Debug.Print("Optimiere Produktion " & p1c0.Tour & "/" & p1c0.ArtikelNummer & "/" & p1c0.ArtikelBezeichnung & "/" & p1c0.RezeptNummer & "/" & p1c0.RezeptBezeichnung & "/" & p1c0.Sollwert_kg)
                End If
                'Teig schon berücksichtigt
                If Not p2c0.Optimiert Then
                    p2c0.Optimiert = True
                    p2c0.ChargenNummer = GetNextChargenNummer(p2.LinienGruppe)
                    TeigMenge = TeigMenge + ZusammenfassenTeigSumme(p2, Modus, p2c0.ChargenNummer) 'p2.Sollwert_kg
                    'Debug.Print("Optimiere Produktion " & p2c0.Tour & "/" & p2c0.ArtikelNummer & "/" & p2c0.ArtikelBezeichnung & "/" & p2c0.RezeptNummer & "/" & p2c0.RezeptBezeichnung & "/" & p2c0.Sollwert_kg)
                End If
                'Letzter Teig
                If i = ProdChildSteps Then
                    'TODO Chargenteiler auch aus Rezeptur berücksichtigen
                    AddChargenZeile(TourInfo(p2c0.Tour, Modus), p2c0.RezeptNr, TeigMenge, wb_GlobalSettings.ChargenTeiler, p2c0.Aufloesen)
                    'Debug.Print("AddCharge LAST " & p2c0.Tour & "/" & p2c0.RezeptNr & "/" & p2c0.RezeptNummer & "/" & p2c0.RezeptBezeichnung & "/" & TeigMenge)
                End If
            Else
                'Wenn mehrere Teige zusammengefasst werden sollen
                If TeigMenge > 0 Then
                    'neuen Produktions - Schritt anhängen
                    'TODO Chargenteiler auch aus Rezeptur berücksichtigen
                    AddChargenZeile(TourInfo(p1c0.Tour, Modus), p1c0.RezeptNr, TeigMenge, wb_GlobalSettings.ChargenTeiler, p1c0.Aufloesen)
                    'Debug.Print("AddCharge NEW " & p1c0.Tour & "/" & p1c0.RezeptNr & "/" & p1c0.RezeptNummer & "/" & p1c0.RezeptBezeichnung & "/" & TeigMenge)
                    TeigMenge = 0
                End If
            End If
        Next i

    End Sub

    ''' <summary>
    ''' Wenn alle Touren zusammengefasst werden - Tour-Info anpassen
    ''' </summary>
    ''' <param name="Tour"></param>
    ''' <param name="Modus"></param>
    ''' <returns></returns>
    Private Function TourInfo(Tour As String, Modus As wb_Global.ModusTeigOptimierung) As String
        If Modus = wb_Global.ModusTeigOptimierung.AlleTeigeAlleTouren Then
            Return "1..."
        Else
            Return Tour
        End If
    End Function

    Private Function Zusammenfassen(p1c0 As wb_Produktionsschritt, p2c0 As wb_Produktionsschritt, Modus As wb_Global.ModusTeigOptimierung) As Boolean
        'Modus Teig-Optimierung
        Select Case Modus

            Case wb_Global.ModusTeigOptimierung.AlleTeigeAlleTouren
                'optimiere alle Teige mit gleicher Rezeptnummer und gleicher Tour
                Return (p1c0.RezeptNr = p2c0.RezeptNr)

            Case wb_Global.ModusTeigOptimierung.AlleTeige
                'optimiere alle Teige mit gleicher Rezeptnummer und gleicher Tour
                Return (p1c0.RezeptNr = p2c0.RezeptNr) And (p1c0.Tour = p2c0.Tour)

            Case wb_Global.ModusTeigOptimierung.NurTeigeKleinerMinChargen
                'optimiere alle Teige mit Restcharge kleiner Minimal-Charge
                Return (p1c0.RezeptNr = p2c0.RezeptNr And p1c0.Tour = p2c0.Tour And p1c0.TeigChargen.Result = wb_Global.ChargenTeilerResult.EM3 And p2c0.TeigChargen.Result = wb_Global.ChargenTeilerResult.EM3)

            Case Else
                Return False
        End Select
    End Function

    Private Function ZusammenfassenTeigSumme(p As wb_Produktionsschritt, Modus As wb_Global.ModusTeigOptimierung, ChargenNummer As String) As Double
        'Default-Rückgabeert
        ZusammenfassenTeigSumme = 0
        'Modus Teig-Optimierung
        Select Case Modus

            Case wb_Global.ModusTeigOptimierung.AlleTeige, wb_Global.ModusTeigOptimierung.AlleTeigeAlleTouren
                'optimiere alle Teige mit gleicher Rezeptnummer und gleicher Tour
                For Each c As wb_Produktionsschritt In p.ChildSteps
                    ZusammenfassenTeigSumme = ZusammenfassenTeigSumme + c.Sollwert_kg
                    c.Optimiert = True
                    c.ChargenNummer = ChargenNummer
                    'Debug.Print("Teigsumme " & p.Tour & "/" & p.RezeptNr & "/" & p.RezeptNummer & "/" & p.RezeptBezeichnung & "/" & ZusammenfassenTeigSumme)
                Next
                p.Optimiert = True

            Case wb_Global.ModusTeigOptimierung.NurTeigeKleinerMinChargen
                'optimiere alle Teige mit Restcharge kleiner Minimal-Charge
                ZusammenfassenTeigSumme = p.Sollwert_kg
                'Debug.Print("Teigsumme " & p.Tour & "/" & p.RezeptNr & "/" & p.RezeptNummer & "/" & p.RezeptBezeichnung & "/" & ZusammenfassenTeigSumme)

            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Fügt eine Rezept(Charge) an die bestehende Liste an.
    ''' Der Rezept-Datensatz wird aus der Datenbank eingelesen. Für die Artikelzeile wird ein Dummy-Artikel angelegt.
    ''' </summary>
    ''' <param name="Tour"></param>
    ''' <param name="RzNr"></param>
    ''' <param name="TeigMenge"></param>
    ''' <param name="Modus"></param>
    ''' <returns></returns>
    Function AddChargenZeile(Tour As String, RzNr As Integer, TeigMenge As Double, Modus As wb_Global.ModusChargenTeiler, Aufloesen As Boolean) As Boolean
        Dim Artikel As New wb_Komponente
        Dim Rezept As New wb_Rezept(RzNr)

        Artikel.Bezeichnung = Rezept.RezeptBezeichnung
        Artikel.Nummer = "K"
        Artikel.RzNr = RzNr
        'Preisberechnung abschalten - Rechenzeit
        Artikel.ReadCalcPreis = False

        'Artikelzeilen hängen immer am ersten (Dummy)Schritt
        Dim Root As wb_Produktionsschritt = _RootProduktionsSchritt

        'Neue Zeile  einfügen (ArtikelZeile)
        Root = New wb_Produktionsschritt(Root, Artikel.Bezeichnung)
        Root.Aufloesen = Aufloesen

        'Daten aus dem Komponenten-Stamm in Produktionsschritt kopieren
        Root.CopyFromKomponenten(Artikel, wb_Global.KomponTypen.KO_ZEILE_ARTIKEL)
        Root.Sollwert_kg = TeigMenge
        Root.Tour = Tour
        Root.Typ = wb_Global.KomponTypen.KO_ZEILE_DUMMYARTIKEL
        'Chargen-Größen aus Rezept
        Root.OptChargekg = Rezept.TeigChargen.OptCharge.fMengeInkg
        Root.MaxChargekg = Rezept.TeigChargen.MaxCharge.fMengeInkg
        Root.MinChargekg = Rezept.TeigChargen.MinCharge.fMengeInkg

        'Chargen berechnen - Aufteilung in Optimal- und Restchargen
        Root.TeigChargen = CalcChargenMenge(TeigMenge, Root.MinChargekg, Root.MaxChargekg, Root.OptChargekg, Modus, True)
        'Root.Aufloesen = True

        'Rezept-Kopf-Zeilen anhängen (Optimal-Chargen)
        For i = 1 To Root.TeigChargen.AnzahlOpt
            AddArtikelRezeptCharge(Root, Artikel, "", Tour, Root.TeigChargen.MengeOpt, Root.TeigChargen, False)
        Next

        'Rezept-Kopf-Zeilen anhängen (Rest-Chargen)
        For i = 1 To Root.TeigChargen.AnzahlRest
            AddArtikelRezeptCharge(Root, Artikel, "", Tour, Root.TeigChargen.MengeRest, Root.TeigChargen, False)
        Next

        Return True
    End Function

    ''' <summary>
    ''' Fügt eine (Artikel)Charge an die bestehende Liste an.
    ''' Der Artikel-Datensatz wird aus der Datenbank eingelesen und als Kopfdatensatz eingefügt. Die Rezeptzeilen werden entsprechend der Chargen-Aufteilung angefügt.
    ''' </summary>
    ''' <param name="Nummer">String - Artikelnummer(alpha)</param>
    ''' <param name="Nr">Integer - interne Artikelnummer</param>
    ''' <param name="Sollmenge_Stk">Double - Sollmenge in Stück</param>
    ''' <returns></returns>
    Public Function AddChargenZeile(Tour As String, Nummer As String, Nr As Integer, Sollmenge_Stk As Double, Sollmenge_kg As Double, MengeInProduktion As Double, Modus As wb_Global.ModusChargenTeiler,
                                     Aufloesen As Boolean, Optional Vorproduktion As Boolean = False, Optional AuftragsNummer As String = "", Optional Bestellmenge As Double = wb_Global.UNDEFINED,
                                     Optional Bestellt_SonderText As String = "", Optional Sollwert_TeilungText As String = "") As Boolean

        Dim Artikel As New wb_Komponente
        Dim TeigMenge As Double

        If Artikel.MySQLdbRead(Nr, Nummer) Then
            'Artikelzeilen hängen immer am ersten (Dummy)Schritt
            Dim Root As wb_Produktionsschritt = _RootProduktionsSchritt
            'Artikel-Stückgewicht in kg
            Dim StkGewicht As Double = Artikel.ArtikelChargen.StkGewicht / 1000
            If StkGewicht = 0 Then
                StkGewicht = 1
            End If

            'Neue Zeile  einfügen (ArtikelZeile)
            Root = New wb_Produktionsschritt(Root, Artikel.Bezeichnung)

            'Daten aus dem Komponenten-Stamm in Produktionsschritt kopieren
            Root.CopyFromKomponenten(Artikel, wb_Global.KomponTypen.KO_ZEILE_ARTIKEL)
            Root.Sollmenge_Stk = Sollmenge_Stk
            Root.Aufloesen = Aufloesen
            If Vorproduktion Then
                Root.Tour = "V" & Root.iTour.ToString
            Else
                Root.Tour = Tour
            End If
            Root.AuftragsNummer = AuftragsNummer
            Root.Bestellt_Stk = CalcBestellMenge(Bestellmenge, Sollmenge_Stk)
            Root.Bestellt_SonderText = Bestellt_SonderText
            Root.Sollwert_TeilungText = Sollwert_TeilungText

            'Teig-Gesamtmenge berechnen
            If Sollmenge_kg <= 0 Then
                TeigMenge = CalcTeigMenge(Sollmenge_Stk, StkGewicht)
            Else
                TeigMenge = Sollmenge_kg
            End If

            'Stkzahl in der Artikelzeile berechnen
            If Root.Sollmenge_Stk = 0 Then
                Root.Sollmenge_Stk = Sollmenge_kg / StkGewicht
            End If

            'Chargen berechnen - Aufteilung in Optimal- und Restchargen
            Root.TeigChargen = CalcChargenMenge(TeigMenge, Artikel.ArtikelChargen.MinCharge.fMengeInkg, Artikel.ArtikelChargen.MaxCharge.fMengeInkg, Artikel.ArtikelChargen.OptCharge.fMengeInkg, Modus, Vorproduktion)

            'Rezept-Kopf-Zeilen anhängen (Optimal-Chargen)
            For i = 1 To Root.TeigChargen.AnzahlOpt
                AddArtikelRezeptCharge(Root, Artikel, AuftragsNummer, Tour, Root.TeigChargen.MengeOpt, Root.TeigChargen, Vorproduktion)
            Next

            'Rezept-Kopf-Zeilen anhängen (Rest-Chargen)
            For i = 1 To Root.TeigChargen.AnzahlRest
                AddArtikelRezeptCharge(Root, Artikel, AuftragsNummer, Tour, Root.TeigChargen.MengeRest, Root.TeigChargen, Vorproduktion)
            Next

            'Stückzahl korrigieren
            Root.Bestellt_Stk = Root.Sollmenge_Stk
            Root.Sollmenge_Stk = (Root.TeigChargen.AnzahlOpt * Root.TeigChargen.MengeOpt + Root.TeigChargen.AnzahlRest * Root.TeigChargen.MengeRest) / StkGewicht

            'Aufteilung in Chargen ist ohne Fehler möglich
            If Root.TeigChargen.Result = wb_Global.ChargenTeilerResult.OK Then
                Return True
            Else
                Return False
            End If
        Else
                Return False
        End If
    End Function

    Private Sub AddArtikelRezeptCharge(ByRef Root As wb_Produktionsschritt, Artikel As wb_Komponente, AuftragsNummer As String, Tour As String, Menge As Double, TeigChargen As wb_Global.ChargenMengen, Vorproduktion As Boolean)
        Dim Rzpt As New wb_Produktionsschritt(Root, Artikel.RezeptName)

        'Daten aus dem Komponenten-Stamm in Produktionsschritt kopieren
        Rzpt.CopyFromKomponenten(Artikel, wb_Global.KomponTypen.KO_ZEILE_REZEPT)
        Rzpt.Sollwert_kg = Menge
        Rzpt.AuftragsNummer = GetAuftragsNummer(AuftragsNummer, Tour)
        Rzpt.Tour = Tour
        'Chargenaufteilung wird jedem Schritt mitgegeben
        Rzpt.TeigChargen = TeigChargen
        'Rezeptur auflösen
        Rzpt.Aufloesen = Root.Aufloesen

        'Rezeptur anhängen (Die Linengruppe wird aus der Rezeptur ermittelt)
        Rzpt.LinienGruppe = AddRezeptSchritte(Rzpt, Menge, Nothing, Vorproduktion)
        'Rezept-Sollwert Menge korrigieren (Anstellgut Sauerteig wird erst in AddRezeptSchritte berechnet)
        Rzpt.Sollwert_kg = Menge
        'Startzeit Vorlauf - Die Berechnung erfolgt erst bei der Ausgabe der Daten 
        Rzpt.ProdVorlauf = Root.ProdVorlauf
        '(vorläufige Chargen-Nummer)
        Rzpt.ChargenNummer = GetNewChargenNummer(Rzpt.LinienGruppe)
        'Zusatztext auf Teigzettel
        If Rzpt.LinienGruppe = wb_Global.LinienGruppeSauerteig Then
            Rzpt.ParentStep.LinienGruppeZusatzText = "Start spätestens " & Rzpt.VirtTreeStart
        End If

    End Sub

    ''' <summary>
    ''' Fügt alle Rezeptschritte als Child-ProduktionsSchritte hinzu.
    ''' Bei Sauerteig-Rezepten (Liniengruppe 127) wird die Sollmenge um das Anstellgut erhöht (Niehaves)
    ''' Voraussetzung, der Haken "Anstellgut für den nächsten Tag aufheben" ist gesetzt
    ''' </summary>
    ''' <param name="Rzpt"></param>
    ''' <param name="Menge"></param>
    ''' <param name="Parent"></param>
    ''' <returns></returns>
    Private Function AddRezeptSchritte(ByRef Rzpt As wb_Produktionsschritt, ByRef Menge As Double, Parent As Object, Vorproduktion As Boolean) As Integer
        'Rezeptur einlesen
        'TODO Muss hier ein Backverlust übertragen werden oder nicht ? PRÜFEN !!!
        '(NoMessage=True unterdrückt die Meldung "Rezept verweist auf sich selbst")
        Dim Rezeptur As New wb_Rezept(Rzpt.RezeptNr, Parent, 0.0, 1, "", "", True, False)

        'Bei Sauerteig-Rezepturen Anstellgut berechnen (wenn der Haken "Anstellgut" gesetzt ist)
        Dim Anstellgut As Double = 0.0
        If (Rzpt.LinienGruppe = wb_Global.LinienGruppeSauerteig) Then
            'alle Rezeptschritte durchlaufen
            For Each rs As wb_Rezeptschritt In Rezeptur.LLRezept
                If rs.Type = wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE Then
                    Select Case rs.ParamNr
                        Case 1
                            Anstellgut += rs.Sollwert
                        Case 2
                            Anstellgut += rs.SollwertProzent * Rezeptur.RezeptGewicht / 100
                    End Select
                End If
            Next
        End If

        'Umrechnungsfaktor Rezeptmenge
        Dim Faktor As Double
        If (Rzpt.LinienGruppe = wb_Global.LinienGruppeSauerteig) And Rezeptur.AnstellGutReWork Then
            Faktor = SaveDiv(Menge, Rezeptur.RezeptGewicht - Anstellgut)
        Else
            Faktor = SaveDiv(Menge, Rezeptur.RezeptGewicht)
        End If

        'die Sollmenge erhöht sich um die Anstellgut-Menge, diese muss für den nächsten Tage wieder zur Verfügung stehen
        If Rzpt.LinienGruppe = wb_Global.LinienGruppeSauerteig Then
            'Anstellgutmenge formatieren auf 1 Nachkommastelle
            Anstellgut = Int(Anstellgut * Faktor * 10) / 10
            'Sollmenge korrigieren
            Menge = Menge + Anstellgut
            'Anstellgutmenge als Kommentar ausgeben
            Rzpt.Bestellt_SonderText = "Anstellgut " & wb_Functions.FormatStr(Anstellgut, 1) & " kg"
        End If

        'Rückgabewert ist die Liniengruppe aus dem Rezeptkopf
        AddRezeptSchritte = Rezeptur.LinienGruppe

        'alle Rezeptschritte an Child-Knoten anhängen
        For Each rs As wb_Rezeptschritt In Rezeptur.LLRezept

            Dim RzSchritt As New wb_Produktionsschritt(Rzpt, rs.Bezeichnung)
            RzSchritt.CopyFromRezeptSchritt(rs, Faktor)
            RzSchritt.Aufloesen = Rzpt.Aufloesen

            'Rezept-im-Rezept-Struktur auflösen - Nur wenn kein Teigling (2020-09-25)
            If (RzSchritt.RezeptNr > 0) And RzSchritt.FreigabeProduktion And Rzpt.Aufloesen Then
                If Vorproduktion Then
                    RzSchritt.ChargenNummer = "VP-OK"
                Else
                    RzSchritt.ChargenNummer = "VP"
                End If
                RzSchritt.LinienGruppe = AddRezeptSchritte(RzSchritt, RzSchritt.Sollwert_kg, Rezeptur, Vorproduktion)
            End If
        Next
    End Function

    ''' <summary>
    ''' Gibt einen Wert für die Auftrags-Nummer zurück. Ist keine Auftrags-Nummer definiert, wird die Tour zurückgegeben.
    ''' </summary>
    ''' <param name="AuftragsNummer"></param>
    ''' <param name="Tour"></param>
    ''' <returns></returns>
    Private Function GetAuftragsNummer(AuftragsNummer As String, Tour As String) As String
        If AuftragsNummer = "" Then
            Return "Tour " & Tour
        Else
            Return AuftragsNummer & "-" & Tour
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
    ''' Berechnung der Teigmenge aus Stück-Gewicht und Soll-Stückzahl
    ''' Der Backverlust wird prozentual abgezogen.
    ''' </summary>
    ''' <param name="SollStk"></param>
    ''' <param name="StkGewicht"></param>
    ''' <param name="Backverlust"></param>
    ''' <returns></returns>
    Private Function CalcTeigMenge(SollStk As Integer, StkGewicht As Double, Optional Backverlust As Double = 0) As Double
        CalcTeigMenge = SollStk * StkGewicht
        Return CalcTeigMenge - ((CalcTeigMenge * Backverlust) / 100)
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
    '''     wb_global.EM3                 'Nur eine Restcharge kleiner als die Minimalcharge. Muss zusammengefasst werden.
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
    Public Function CalcChargenMenge(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, Modus As wb_Global.ModusChargenTeiler, VorProduktion As Boolean) As wb_Global.ChargenMengen
        Select Case Modus
            Case wb_Global.ModusChargenTeiler.XGleiche
                Return CalcBatchM1(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case wb_Global.ModusChargenTeiler.NurOptimal
                Return CalcBatch00(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case wb_Global.ModusChargenTeiler.OptimalUndRest
                Return CalcBatch01(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case wb_Global.ModusChargenTeiler.MaximalUndRest
                Return CalcBatch02(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
            Case Else
                Return CalcBatch09(Sollwert, ChargeMin, ChargeMax, ChargeOpt, VorProduktion)
        End Select
    End Function

    ''' <summary>
    ''' Aufteilung in gleiche Chargen mit möglichst großer Chargengröße
    ''' 
    ''' Result.Error ist  0 wenn die Sollmenge komplett produziert werden kann.
    ''' Result.Error ist +1 wenn die Anzahl der Chargen reduziert wurde damit die Sollmenge ungefähr erreicht wird.
    ''' Result.Error ist +2 wenn die Chargengröße verringert wurde damit die Sollmenge ungefährt erreicht wird.
    ''' </summary>
    ''' <param name="Sollwert"></param>
    ''' <param name="ChargeMin"></param>
    ''' <param name="ChargeMax"></param>
    ''' <param name="ChargeOpt"></param>
    ''' <param name="VorProduktion"></param>
    ''' <returns></returns>
    Private Function CalcBatchM1(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen
        'Es gibt keine Restchargen
        CalcBatchM1.MengeRest = 0
        CalcBatchM1.AnzahlRest = 0
        CalcBatchM1.Modus = wb_Global.ModusChargenTeiler.XGleiche

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
                CalcBatchM1.Result = wb_Global.ChargenTeilerResult.EP1
            Else
                CalcBatchM1.MengeOpt = ChargeMin
                CalcBatchM1.Result = wb_Global.ChargenTeilerResult.EP2
            End If
        End If
    End Function

    ''' <summary>
    ''' Aufteilung in OptimalChargen
    ''' 
    ''' Result.Error ist  0 wenn die Sollmenge komplett produziert werden kann.
    ''' Result.Error ist -1 wenn eine Restmenge übrig bleibt
    ''' Result.Error ist -2 wenn mehr produziert wird als gefordert (@V3.0.5)
    ''' </summary>
    ''' <param name="Sollwert"></param>
    ''' <param name="ChargeMin"></param>
    ''' <param name="ChargeMax"></param>
    ''' <param name="ChargeOpt"></param>
    ''' <param name="VorProduktion"></param>
    ''' <returns></returns>
    Private Function CalcBatch00(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen
        'es gibt keine Restchargen
        CalcBatch00.MengeOpt = ChargeOpt
        CalcBatch00.MengeRest = 0
        CalcBatch00.AnzahlRest = 0
        CalcBatch00.Result = wb_Global.ChargenTeilerResult.OK
        CalcBatch00.Modus = wb_Global.ModusChargenTeiler.NurOptimal

        'erlaubter Rundungsfehler in Prozent der Chargengröße
        Dim ErlaubterRundungsfehler As Double = ChargeOpt / 10 ' (1% der Chargengröße)

        'Anzahl der Chargen - Menge durch Maximalchargen teilen
        CalcBatch00.AnzahlOpt = Math.Round(Int(SaveDiv(Sollwert, ChargeOpt)))

        'Größe der Restcharge berechnen
        Dim MengeRest As Double = Sollwert - (CalcBatch00.AnzahlOpt * CalcBatch00.MengeOpt)

        'Wie groß wäre die Abweichung wenn eine Charge mehr prodziert werden würde
        Dim OptimierterRest As Double = Sollwert - ((CalcBatch00.AnzahlOpt + 1) * CalcBatch00.MengeOpt)
        If Math.Abs(OptimierterRest) < Math.Abs(MengeRest) Then
            'Anzahl der OptimalChargen um Eins erhöhen wenn damit ein besseres Ergebnis erreicht werden kann
            CalcBatch00.AnzahlOpt = CalcBatch00.AnzahlOpt + 1
            MengeRest = Sollwert - (CalcBatch00.AnzahlOpt * CalcBatch00.MengeOpt)
        End If

        If (Math.Abs(MengeRest) > ErlaubterRundungsfehler) Then
            If (MengeRest < 0) Then
                CalcBatch00.Result = wb_Global.ChargenTeilerResult.EM2
            Else
                CalcBatch00.Result = wb_Global.ChargenTeilerResult.EM1
            End If
        Else
            CalcBatch00.Result = wb_Global.ChargenTeilerResult.OK
        End If

        'Vorproduktion (immer eine Charge anlegen)
        If VorProduktion And ((CalcBatch00.AnzahlOpt = 0) Or CalcBatch00.Result = wb_Global.ChargenTeilerResult.EM1) Then
            CalcBatch00.AnzahlOpt = CalcBatch00.AnzahlOpt + 1
            CalcBatch00.Result = wb_Global.ChargenTeilerResult.EM2
        End If

    End Function

    ''' <summary>
    ''' Aufteilung in Optimal-Chargen und Rest
    '''   
    ''' Result.Error ist  0 wenn die Sollmenge komplett produziert werden kann.
    ''' Result.Error ist -1 Sollmenge kann nicht erreicht werden da die Restcharge die Mindestmenge unterschreitet
    ''' 
    ''' Ist das Flag RestKleinerMin gesetzt, wird die Rest-Charge auch dann berechnet, wenn Sie kleiner als die Minimal-Charge ist.
    ''' Dies ist notwendig für die Berechnung der Vorproduktion oder die Kalkulation der bestellten Mengen aus den OrgaBack-Bestellungen,
    ''' da hier sonst Teigmengen verloren gehen können!
    ''' </summary>
    ''' <param name="Sollwert"></param>
    ''' <param name="ChargeMin"></param>
    ''' <param name="ChargeMax"></param>
    ''' <param name="ChargeOpt"></param>
    ''' <param name="VorProduktion"></param>
    ''' <returns></returns>
    Private Function CalcBatch01(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen
        ' Variablen initialisieren
        CalcBatch01.MengeOpt = ChargeOpt
        CalcBatch01.Modus = wb_Global.ModusChargenTeiler.OptimalUndRest

        ' Anzahl der Chargen - Menge durch Optimalchargen teilen
        CalcBatch01.AnzahlOpt = Math.Round(Int(SaveDiv(Sollwert, ChargeOpt)))
        CalcBatch01.Result = wb_Global.ChargenTeilerResult.OK
        If CalcBatch01.AnzahlOpt = 0 Then
            CalcBatch01.MengeOpt = 0
        End If

        ' Größe der Restcharge berechnen
        CalcBatch01.MengeRest = Sollwert - (CalcBatch01.AnzahlOpt * CalcBatch01.MengeOpt)
        CalcBatch01.AnzahlRest = 1

        ' Prüfen ob innerhalb der Chargengrenzen
        If (CalcBatch01.MengeRest < ChargeMin) Then
            ' Wenn keine Restmenge vorhanden - keinen Fehler ausgeben
            If (CalcBatch01.MengeRest = 0) Then
                CalcBatch01.AnzahlRest = 0
                CalcBatch01.Result = wb_Global.ChargenTeilerResult.OK
            Else
                If VorProduktion Then
                    CalcBatch01.AnzahlRest = 1
                    CalcBatch01.MengeRest = ChargeMin
                    If CalcBatch01.AnzahlOpt = 0 Then
                        CalcBatch01.Result = wb_Global.ChargenTeilerResult.EM3
                    Else
                        CalcBatch01.Result = wb_Global.ChargenTeilerResult.EM2
                    End If
                Else
                    ' Sollmenge kann nicht erreicht werden weil die
                    ' Restcharge kleiner als die Min-Charge ist
                    CalcBatch01.AnzahlRest = 0
                    CalcBatch01.MengeRest = 0
                    CalcBatch01.Result = wb_Global.ChargenTeilerResult.EM1
                End If
            End If
            ' (@V3.0.5) Vorproduktion
            If (CalcBatch01.AnzahlOpt = 0) And (CalcBatch01.AnzahlRest = 0) And VorProduktion Then
                CalcBatch01.AnzahlRest = 1
                CalcBatch01.MengeRest = ChargeMin
                CalcBatch01.MengeOpt = 0
            End If
        End If
    End Function

    ''' <summary>
    ''' Aufteilung in möglichst große Chargen und Rest
    ''' 
    ''' CalcBatch01.Result ist  0 wenn die Sollmenge komplett produziert werden kann.
    ''' CalcBatch01.Result ist -1 Sollmenge kann nicht erreicht werden da die Restcharge die Mindestmenge unterschreitet
    ''' </summary>
    ''' <param name="Sollwert"></param>
    ''' <param name="ChargeMin"></param>
    ''' <param name="ChargeMax"></param>
    ''' <param name="ChargeOpt"></param>
    ''' <param name="VorProduktion"></param>
    ''' <returns></returns>
    Private Function CalcBatch02(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen
        ' Variablen initialisieren
        CalcBatch02.Result = wb_Global.ChargenTeilerResult.OK
        CalcBatch02.AnzahlRest = 0
        CalcBatch02.Modus = wb_Global.ModusChargenTeiler.MaximalUndRest

        ' Anzahl der Chargen - Menge durch Maximalchargen teilen
        CalcBatch02.AnzahlOpt = Math.Round(Int(SaveDiv(Sollwert, ChargeMax)))
        CalcBatch02.MengeOpt = SaveDiv(Sollwert, CalcBatch02.AnzahlOpt)

        ' Wenn keine Optimalchargen produziert werden - Menge auf Null setzen
        If CalcBatch02.AnzahlOpt = 0 Then
            CalcBatch02.MengeOpt = 0
        End If

        ' auf Chargengrenzen prüfen
        If (CalcBatch02.MengeOpt > ChargeMax) Then
            CalcBatch02.MengeOpt = ChargeMax
        End If
        If (CalcBatch02.MengeOpt < ChargeMin) Then
            CalcBatch02.MengeOpt = ChargeMin
        End If

        ' Restmenge berechnen
        CalcBatch02.MengeRest = Sollwert - CalcBatch02.MengeOpt * CalcBatch02.AnzahlOpt
        If (CalcBatch02.MengeRest > 0) Then
            CalcBatch02.AnzahlRest = 1

            ' Prüfen ob die Restmenge kleiner als die Minimalcharge ist
            If (CalcBatch02.MengeRest < ChargeMin) Then
                ' neue Sollmenge berechnen
                Dim SollNeu As Double = Sollwert - ChargeMin
                ' Chargengrößen neu berechnen
                CalcBatch02.MengeOpt = SaveDiv(SollNeu, CalcBatch02.AnzahlOpt)

                ' auf Chargengrenzen prüfen
                If (CalcBatch02.MengeOpt > ChargeMax) Then
                    CalcBatch02.MengeOpt = ChargeMax
                End If
                If (CalcBatch02.MengeOpt < ChargeMin) Then
                    CalcBatch02.MengeOpt = ChargeMin
                End If

                ' Restmenge berechnen
                Dim Rest As Double = Sollwert - (CalcBatch02.MengeOpt * CalcBatch02.AnzahlOpt)
                ' Anzahl der Chargen - Menge durch Maximalchargen teilen
                CalcBatch02.AnzahlRest = Math.Round(Int(SaveDiv(Rest, ChargeMax)))
                If (CalcBatch02.AnzahlRest = 0) Then
                    CalcBatch02.AnzahlRest = 1
                End If
                CalcBatch02.MengeRest = SaveDiv(Rest, CalcBatch02.AnzahlRest)

                ' auf Chargengrenzen prüfen
                If (CalcBatch02.MengeRest < ChargeMin) Then
                    CalcBatch02.MengeRest = 0
                    CalcBatch02.AnzahlRest = 0
                    CalcBatch02.Result = wb_Global.ChargenTeilerResult.EM1
                End If
            End If ' (CalcBatch02.MengeRest < CMin)
        End If ' (CalcBatch02.MengeRest > 0)

        ' (@V3.0.5) Vorproduktion
        If (CalcBatch02.AnzahlOpt = 0) And (CalcBatch02.AnzahlRest = 0) And VorProduktion Then
            CalcBatch02.AnzahlRest = 1
            CalcBatch02.MengeRest = ChargeMin
            CalcBatch02.MengeOpt = 0
        End If

    End Function

    ''' <summary>
    ''' Aufteilung nach Rezeptgröße (Maximal-Charge)
    ''' 
    ''' CalcBatch09.Result ist immer 9
    ''' </summary>
    ''' <param name="Sollwert"></param>
    ''' <param name="ChargeMin"></param>
    ''' <param name="ChargeMax"></param>
    ''' <param name="ChargeOpt"></param>
    ''' <param name="VorProduktion"></param>
    ''' <returns></returns>
    Private Function CalcBatch09(Sollwert As Double, ChargeMin As Double, ChargeMax As Double, ChargeOpt As Double, VorProduktion As Boolean) As wb_Global.ChargenMengen
        ' es gibt keine Restchargen
        CalcBatch09.MengeRest = 0
        CalcBatch09.AnzahlRest = 0
        CalcBatch09.Modus = wb_Global.ModusChargenTeiler.RezeptGroesse

        ' Anzahl der Chargen - Menge durch Rezeptgröße (hier CMax) teilen
        Dim x As Double = Math.Round(SaveDiv(Sollwert, ChargeMax))

        ' Teiler plus 1 ergibt die Anzahl der Chargen wenn ein Rest auftritt
        If (Sollwert - ChargeMax * x) > 0.1 Then
            CalcBatch09.AnzahlOpt = x + 1
        Else
            CalcBatch09.AnzahlOpt = x
        End If

        ' Chargengröße berechnen
        CalcBatch09.MengeOpt = SaveDiv(Sollwert, CalcBatch09.AnzahlOpt)
        ' Ergebnis ist immer 9
        CalcBatch09.Result = wb_Global.ChargenTeilerResult.EP9

        ' (@V3.0.5) Vorproduktion
        If (CalcBatch09.AnzahlOpt = 0) And VorProduktion Then
            CalcBatch09.AnzahlOpt = 1
            CalcBatch09.MengeOpt = Sollwert
        End If
    End Function

    Private Function SaveDiv(Divident As Double, Divisor As Double) As Double
        If (Divisor > 0) Then
            Return Divident / Divisor
        Else
            Return 0
        End If
    End Function

    Friend Function MsSQLdbProcedure_Produktionsauftrag(ProduktionsDatum As String, Filiale As String) As Boolean
        Dim Root As wb_Produktionsschritt = _RootProduktionsSchritt
        Dim ArtikelNummer As String = ""
        Dim GesamtStueck As Integer = 0
        Dim ErrorList As New List(Of String)
        MsSQLdbProcedure_Produktionsauftrag = False

        'Datenbankverbindung öffnen MsSQL
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        Dim p(1) As wb_Sql.StoredProceduresParameter

        'Filiale Produktion
        p(0).Parameter = "Filiale"
        p(0).Value = Filiale
        'Produktionsdatum
        p(1).Parameter = "LieferDatum"
        p(1).Value = ProduktionsDatum
        'Stored Procedure ausführen
        orgaback.sqlExecStoredProcedure("pq_Produktionsauftrag", p)

        'Produktionsdaten aufbauen
        Dim BestellDaten As New wb_BestellDatenSchritt
        Dim DebugCounter As Integer = 0
        ErrorList.Clear()
        'Chargen-Teiler (Vorgabe aus Einstellungen/Planung-Teiler)
        BestellDaten.ChargenTeiler = wb_GlobalSettings.ChargenTeiler

        'alle Datensätze einlesen
        While orgaback.Read
            DebugCounter += 1
            'Progressbar einblenden (nur wenn noch nicht sichtbar)
            wb_Main_Shared.ShowProgressBar()
            'Alle Datensätze einlesen
            For i = 0 To orgaback.msRead.FieldCount - 1
                'Debug.Print("OrgaBack StoredProcedure Read " & orgaback.msRead.GetName(i) & "/" & orgaback.msRead.GetValue(i))
                MsSQLdbProcedure_Produktionsauftrag = True
                'Daten aus der view einlesen
                BestellDaten.MsSQLdbRead_Fields(orgaback.msRead.GetName(i), orgaback.msRead.GetValue(i))
                'Progressbar Anzeige refresh
                Windows.Forms.Application.DoEvents()
            Next
            'Produktions-Auftrag zu Liste hinzufügen (auch Restchargen < MinCharge einfügen [Vorproduktion=True])
            If Not AddChargenZeile(BestellDaten.TourNr, BestellDaten.ArtikelNummer, 0, BestellDaten.Produktionsmenge, 0.0, BestellDaten.MengeInProduktion, BestellDaten.ChargenTeiler, BestellDaten.Aufloesen, False, BestellDaten.AuftragsNummer, BestellDaten.BestellMenge, BestellDaten.SonderText, BestellDaten.SollwertTeilungText) Then
                ErrorList.Add(BestellDaten.ArtikelNummer)
            End If
        End While
        'Progressbar ausblenden
        wb_Main_Shared.HideProgressBar()
        'Datenbankverbindung wieder schliessen
        orgaback.Close()

        'Falls Fehler aufgetreten sind, Meldung anzeigen
        If ErrorList.Count > 0 Then
            'Fehlermeldung wird dynamisch erzeugt
            Dim ErrorText As String = ""
            For Each ErrorArtikelNummer In ErrorList
                If ErrorText <> "" Then
                    ErrorText = ErrorText & ", " & ErrorArtikelNummer
                Else
                    ErrorText = ErrorArtikelNummer
                End If
            Next
            'Fehler beim Einlesen der Produktionsdaten aus OrgaBack (Artikel in WinBack nicht vorhanden)
            ErrorText = "Fehler beim Einlesen der Produktions-Liste " & vbCrLf & ErrorList.Count.ToString & " Artikelnummer(n) konnten nicht verabeitet werden: " & vbCrLf & vbCrLf & ErrorText
            MsgBox(ErrorText, MsgBoxStyle.Exclamation, "Einlesen der Produktionsdaten")
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
                    _SQLProduktionsSchritt.Typ = wb_Global.KomponTypen.KO_ZEILE_REZEPT
                    GesamtStueck += _SQLProduktionsSchritt.Sollmenge_Stk
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
        Try
            Select Case Name

                'Produktionsauftrags-Nummer
                Case "B_ARZ_Best_Nr"
                    _SQLProduktionsSchritt.AuftragsNummer = Value
                'Typ (Artikel oder Rezept-Zeile)
                Case "B_ARZ_Typ" 'TODO in genormten Typ umsetzen wb_global.wbArtikel...
                    _SQLProduktionsSchritt.Typ = Value
               'Artikelnummer(alpha)
                Case "B_ARZ_KA_NrAlNum", "ArtikelNr"
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
                Case "B_ARZ_Sollmenge_stueck", "Produktionsmenge"
                    _SQLProduktionsSchritt.Sollmenge_Stk = wb_Functions.StrToDouble(Value)

            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function


End Class

