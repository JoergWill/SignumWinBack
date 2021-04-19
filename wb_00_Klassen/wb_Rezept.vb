Imports MySql.Data.MySqlClient

''' <summary>
''' Enthält die komplette Rezeptur als Liste von Rezeptschritten (wb_Rezeptschritt).
''' Jeder Rezeptschritt hat Parent und Child. Die Rezeptur beginnt am Knoten(0) ohne Parent.
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
Public Class wb_Rezept
    Implements IDisposable

    Private _RezeptNummer As String
    Private _RezeptBezeichnung As String
    Private _RezeptKommentar As String
    Private _AenderungNummer As Integer
    Private _AenderungDatum As String
    Private _AenderungName As String
    Private _AenderungUserNr As Integer
    Private _RezeptTeigTemperatur As Double = wb_Global.UNDEFINED
    Private _LinienGruppe As Integer
    Private _RZ_Type As String
    Private _KneterKennLinie As Integer
    Private _NoMessage As Boolean
    Private _ReadCalcPreis As Boolean = True
    Private _Varianten As Hashtable = Nothing

    Public TeigChargen As New wb_MinMaxOptCharge
    Private _DataHasChanged As Boolean = False

    Private _RootRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _SQLRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _RezeptSchritt As wb_Rezeptschritt

    Private _BruttoRezeptGewicht As Double = wb_Global.UNDEFINED
    Private _NwtRezeptGewicht As Double = wb_Global.UNDEFINED
    Private _RezeptPreis As Double = wb_Global.UNDEFINED
    Private _RezeptGesamtMehlmenge As Double = wb_Global.UNDEFINED
    Private _RezeptGesamtWasserMenge As Double = wb_Global.UNDEFINED
    Private _RezeptNr As Integer
    Private _RezeptVariante As Integer
    Private _Rezeptgruppe As Integer
    Private _ktTyp301 As New wb_KomponParam301
    Private _Zutaten As New wb_ZutatenListe
    Private _LLRezeptur As New ArrayList
    Private _LLBig4 As New ArrayList

    Public _Parent As Object

    ''' <summary>
    ''' Setzt alle Variablen wieder auf Null,Nothing oder Undefined.
    ''' Wird aufgerufen, wenn eine neue(andere) Komponente geladen werden soll
    ''' </summary>
    Public Sub Invalid()
        _RezeptNr = wb_Global.UNDEFINED
        _RezeptNummer = ""
        _RezeptBezeichnung = ""
        _RezeptKommentar = ""
        _AenderungNummer = wb_Global.UNDEFINED
        _LinienGruppe = wb_Global.UNDEFINED
        _Varianten = Nothing
        _DataHasChanged = False
    End Sub

    Public WriteOnly Property DataHasChanged As Boolean
        Set(value As Boolean)
            _DataHasChanged = value
        End Set
    End Property

    Public ReadOnly Property RezeptNr As Integer
        Get
            Return _RezeptNr
        End Get
    End Property

    ''' <summary>
    ''' Erster (unsichtbarer) Rezept-Schritt (Root-Node)
    ''' </summary>
    ''' <returns>wb_RezeptSchritt - Root-Node des Rezeptes</returns>
    Public ReadOnly Property RootRezeptSchritt As wb_Rezeptschritt
        Get
            Return _RootRezeptSchritt
        End Get
    End Property

    ''' <summary>
    ''' Das Rezept-Gesamtgewicht steht als Gewichtswert im Root-Node
    ''' Die Berechnung erfolgt über RezeptSchritt.Gewicht(Get). Wenn keine Child-Steps vorhanden sind wird der Wert auf Null gesetzt
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamtgewicht</returns>
    Public ReadOnly Property RezeptGewicht As Double
        Get
            'wenn der Wert noch nicht berechnet wurde
            If TeigChargen.TeigGewicht = wb_Global.UNDEFINED Then
                If _RootRezeptSchritt.ChildSteps.Count > 0 Then
                    TeigChargen.TeigGewicht = _RootRezeptSchritt.Gewicht
                Else
                    TeigChargen.TeigGewicht = 0
                End If
            End If
            Return TeigChargen.TeigGewicht
        End Get
    End Property

    ''' <summary>
    ''' Das Brutto-Rezept-Gesamtgewicht steht als Gewichtswert im Root-Node
    ''' Die Berechnung erfolgt über RezeptSchritt.BruttoGewicht(Get)
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamtgewicht</returns>
    Public ReadOnly Property BruttoRezeptGewicht As Double
        Get
            'wenn der Wert noch nicht berechnet wurde
            If _BruttoRezeptGewicht = wb_Global.UNDEFINED Then
                _BruttoRezeptGewicht = _RootRezeptSchritt.BruttoGewicht
            End If
            Return _BruttoRezeptGewicht
        End Get
    End Property

    ''' <summary>
    ''' Das Nwt-Rezept-Gesamtgewicht steht als Gewichtswert im Root-Node
    ''' Die Berechnung erfolgt über RezeptSchritt.NwtGewicht(Get)
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamtgewicht</returns>
    Public ReadOnly Property NwtRezeptGewicht As Double
        Get
            'wenn der Wert noch nicht berechnet wurde
            If _NwtRezeptGewicht = wb_Global.UNDEFINED Then
                _NwtRezeptGewicht = _RootRezeptSchritt.NwtGewicht
            End If
            Return _NwtRezeptGewicht
        End Get
    End Property

    ''' <summary>
    ''' Umrechnung aller Komponenten auf ein neues Rezept-Gesamtgewicht.
    ''' Der Umrechnungsfaktor wird berechnet aus RezeptgewichtNue/RezeptgewichtAlt
    ''' </summary>
    ''' <param name="RzGewichtNeu"></param>
    Public Sub RecalcRezeptGewicht(RzGewichtNeu As Double)
        'Dividion durch Null abfangen
        If RezeptGewicht > 0 Then

            'Umrechnungsfaktor
            Dim F As Double = RzGewichtNeu / RezeptGewicht
            'alle Child-Rezeptschritte umrechnen (Rekursiv)
            RecalcRezeptGewicht(_RootRezeptSchritt, F)

            'Rezept-Gesamtgewicht muss neu berechnet werden
            TeigChargen.TeigGewicht = wb_Global.UNDEFINED
            _BruttoRezeptGewicht = wb_Global.UNDEFINED
        End If
    End Sub

    Private Sub RecalcRezeptGewicht(ByRef RootRezeptSchritt As wb_Rezeptschritt, F As Double)
        'alle Rezeptschritt (nur das flache Rezept) umrechnen
        For Each rs As wb_Rezeptschritt In RootRezeptSchritt.ChildSteps

            'alle Rezept-Zeilen mit Sollwerten umrechnen
            If wb_Functions.TypeIstSollMenge(rs.Type, rs.ParamNr) Then
                rs.Sollwert = rs.Sollwert * F
            Else
                'Wenn die Rezept-Zeile keinen Sollwert hat, werden alle Child-Steps umgerechnent (Produktions-Stufe...)
                If rs.ChildSteps.Count > 0 Then
                    RecalcRezeptGewicht(rs, F)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Umrechnung der Wassermenge aller Rezeptschritte auf die neue TA
    ''' Wird keine Wasserkomponente in der (flachen)Rezeptur gefunden wird false zurückgegeben
    ''' </summary>
    ''' <param name="RzTANeu"></param>
    Public Function RecalcWasserMengeFromTA(RzTANeu As Integer) As Boolean
        'TA(alt)
        Dim RzTAalt As Integer = _RootRezeptSchritt.TA

        'Gesamt-Wassermenge (aus der TA-Berechnung)
        Dim WasserMenge As Double = _RootRezeptSchritt.TA_Wassermenge
        'Gesamt-Mehlmenge (aus der TA-Berechnung)
        Dim Mehlmenge As Double = _RootRezeptSchritt.TA_Mehlmenge
        'Wassermenge neu (Gesamt)
        Dim WasserMengeNeu As Double = Mehlmenge * (RzTANeu / 100 - 1)
        'Differenz zur bisherigen Wassermenge
        Dim WasserMengeDiff As Double = WasserMengeNeu - WasserMenge

        'WasserGesamtmenge im flachen Rezept ermitteln
        Dim WasserMengeFlach As Double = CalcWasserMenge(_RootRezeptSchritt)
        Dim WasserMengeRest As Double = WasserMenge - WasserMengeFlach

        'wenn eine Wasser-Komponente im (flachen) Rezept enthalten ist - Umrechnen, sonst Fehlermeldung ausgeben
        If WasserMengeFlach > 0 Then
            'Umrechnungsfaktor
            Dim f As Double = (WasserMengeNeu - WasserMengeRest) / WasserMengeFlach
            'alle Rezeptschritt mit Wasser-Sollmenge umrechnen
            CalcWasserMenge(_RootRezeptSchritt, f)
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CalcWasserMenge(ByRef RootRezeptSchritt As wb_Rezeptschritt) As Double
        'WasserGesamtmenge im flachen Rezept ermitteln
        Dim WasserMengeFlach As Double = 0
        'alle Rezeptschritt (nur das flache Rezept) umrechnen
        For Each rs As wb_Rezeptschritt In RootRezeptSchritt.ChildSteps

            'alle Rezept-Zeilen mit Sollwerten umrechnen
            If wb_Functions.TypeIstSollMenge(rs.Type, rs.ParamNr) Then
                'alle Rezept-Zeilen mit Wasser-Sollmenge
                If wb_Functions.TypeIstWasserSollmenge(rs.Type, rs.ParamNr, rs.TA) Then
                    WasserMengeFlach += rs.Sollwert
                End If
            Else
                'Wenn die Rezept-Zeile keinen Sollwert hat, werden alle Child-Steps umgerechnent (Produktions-Stufe...)
                If rs.ChildSteps.Count > 0 Then
                    WasserMengeFlach += CalcWasserMenge(rs)
                End If
            End If
        Next
        Return WasserMengeFlach
    End Function

    Private Sub CalcWasserMenge(ByRef RootRezeptSchritt As wb_Rezeptschritt, f As Double)
        'alle Rezeptschritt (nur das flache Rezept) umrechnen
        For Each rs As wb_Rezeptschritt In RootRezeptSchritt.ChildSteps

            'alle Rezept-Zeilen mit Sollwerten umrechnen
            If wb_Functions.TypeIstSollMenge(rs.Type, rs.ParamNr) Then
                'alle Rezept-Zeilen mit Wasser-Sollmenge
                If wb_Functions.TypeIstWasserSollmenge(rs.Type, rs.ParamNr, rs.TA) Then
                    rs.Sollwert = rs.Sollwert * f
                End If
            Else
                'Wenn die Rezept-Zeile keinen Sollwert hat, werden alle Child-Steps umgerechnent (Produktions-Stufe...)
                If rs.ChildSteps.Count > 0 Then
                    CalcWasserMenge(rs, f)
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Der Rezept-Gesamtpreis steht als Preis im Root-Node
    ''' Die Berechnung erfolgt über RezeptSchritt.Preis(Get)
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamtpreis</returns>
    Public ReadOnly Property RezeptPreis As Double
        Get
            'wenn der Wert noch nicht berechnet wurde
            If _RezeptPreis = wb_Global.UNDEFINED Then
                _RezeptPreis = _RootRezeptSchritt.Preis
            End If
            Return _RezeptPreis
        End Get
    End Property

    ''' <summary>
    ''' Die Rezept-Gesamt-Mehlmenge steht als TA_Mehlmenge im Root-Node
    ''' Die Berechnung erfolgt über Rezeptschritt.TA_Mehlmenge(Get)
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamtmehlmenge</returns>
    Public ReadOnly Property RezeptGesamtMehlmenge
        Get
            'wenn der Wert noch nicht berechnet wurde
            If _RezeptGesamtMehlmenge = wb_Global.UNDEFINED Then
                _RezeptGesamtMehlmenge = _RootRezeptSchritt.TA_Mehlmenge
            End If
            Return _RezeptGesamtMehlmenge
        End Get
    End Property

    ''' <summary>
    ''' Die Rezept-Gesamt-Wassermenge steht als TA_Wassermenge im Root-Node
    ''' Die Berechnung erfolgt über Rezeptschritt.TA_Wassermenge(Get)
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamt-Wassermenge</returns>
    Public ReadOnly Property RezeptGesamtWassermenge
        Get
            'wenn der Wert noch nicht berechnet wurde
            If _RezeptGesamtWasserMenge = wb_Global.UNDEFINED Then
                _RezeptGesamtWasserMenge = _RootRezeptSchritt.TA_Wassermenge
            End If
            Return _RezeptGesamtWasserMenge
        End Get
    End Property

    ''' <summary>
    ''' Die Rezept-TA wird berechnet aus Mehl-Gesamtmenge und Wasser-Gesamtmenge
    ''' der Rezeptur.
    '''                 (Wasser * 100)
    '''     TA = 100 +  --------------
    '''                     Mehl
    ''' </summary>
    ''' <returns>Double - Rezept-TA</returns>
    Public ReadOnly Property RezeptTA As Double
        Get
            'Gesamt-Mehlmenge berechnen
            Dim TA_Mehlmenge As Double = RezeptGesamtMehlmenge
            Dim TA_Wassermenge As Double = RezeptGesamtWassermenge
            'Gesamt-TA der Rezeptur nur berechnen, wenn Mehlmenge ungleich Null
            If TA_Mehlmenge > 0.1 Then
                Return 100 + (TA_Wassermenge * 100) / TA_Mehlmenge
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property LLBig4() As IList
        Get
            Dim wert As String
            _LLBig4.Clear()
            For i = 1 To wb_Global.maxTyp301Allergen
                wert = KtTyp301.Wert(i)
                _LLBig4.Add(wert)

            Next
            Return _LLBig4
        End Get
    End Property

    Public ReadOnly Property LLRezept As IList
        Get
            Return _RootRezeptSchritt.Steps
        End Get
    End Property

    ''' <summary>
    ''' Neuberechnung aller Werte erzwingen. z.B. nach Rezeptänderung
    ''' 
    ''' Die Berechnung der Rezeptur-Gesamt-Werte (Gewicht, TA, Mehlanteil...) erfolgt nur einmal, wenn der entsprechende Wert abgefragt wird.
    ''' Um eine Berechnung zu erzwingen kann Recalculate gesetzt werden.
    ''' </summary>
    Public WriteOnly Property Recalculate As Boolean
        Set(value As Boolean)
            TeigChargen.ErrorCheck = False
            TeigChargen.TeigGewicht = wb_Global.UNDEFINED
            _BruttoRezeptGewicht = wb_Global.UNDEFINED
            _RezeptPreis = wb_Global.UNDEFINED
            _RezeptGesamtMehlmenge = wb_Global.UNDEFINED
            _RezeptGesamtWasserMenge = wb_Global.UNDEFINED
            'TODO Recalculate an die Allergen/Nährwerte weitergeben
            _Zutaten.Clear()
        End Set
    End Property

    ''' <summary>
    ''' Nährwerte und Allergene. Die Rezeptwerte stehen als Array im Root-Node.
    ''' Die Berechnung erfolgt über den Rezeptschritt.ktTyp301(Get)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property KtTyp301 As wb_KomponParam301
        Get
            'der Root-Knoten hat keine Nährwerte und wird als Vegan,Vegatarisch... deklariert
            _RootRezeptSchritt.InitKT301Params()
            'Nährwerte berechnen
            Return _RootRezeptSchritt.ktTyp301
        End Get
    End Property

    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = wb_Functions.XRemoveSonderZeichen(value)
            _DataHasChanged = True
        End Set
    End Property

    Public Property RezeptBezeichnung As String
        Get
            Return _RezeptBezeichnung
        End Get
        Set(value As String)
            _RezeptBezeichnung = wb_Functions.XRemoveSonderZeichen(value)
            _DataHasChanged = True
        End Set
    End Property

    Public ReadOnly Property Variante As String
        Get
            Return _RezeptVariante
        End Get
    End Property

    Public Property RezeptGruppe As Integer
        Get
            Return _Rezeptgruppe
        End Get
        Set(value As Integer)
            _Rezeptgruppe = value
            _DataHasChanged = True
        End Set
    End Property

    ''' <summary>
    ''' Gibt die Liniengruppe der Rezeptur zurück. 
    ''' Wenn die Rezeptvariante gleich 0 ist (Sauerteig) , wird als Liniengruppe 999 zurückgegeben
    ''' </summary>
    ''' <returns></returns>
    Public Property LinienGruppe As Integer
        Get
            If _RezeptVariante = 0 Then
                Return wb_Global.LinienGruppeSauerteig
            Else
                Return _LinienGruppe
            End If
        End Get
        Set(value As Integer)
            _LinienGruppe = value
            _DataHasChanged = True
        End Set
    End Property

    ''' <summary>
    ''' Flag AnstellgutRework
    ''' Bestimmt, ob bei der Berechnung der Produktions-Mengen(Vorproduktion) die Anstellgutmenge als Restmenge für den nächsten Tag
    ''' übrig bleiben soll.
    ''' Die Anstellgutmenge wird aus der Sauerteig-Rezeptur ermittelt.
    ''' 
    ''' Datenbank-Feld RZ_Type
    '''     
    '''     A   -   AnstellgutRewort 
    '''     T   -   Reset TTS beim nächsten Start in WinBack-Produktion
    ''' </summary>
    ''' <returns></returns>
    Public Property AnstellGutReWork As Boolean
        Get
            Return (_RZ_Type = "A")
        End Get
        Set(value As Boolean)
            If value Then
                _RZ_Type = "A"
            Else
                _RZ_Type = ""
            End If
        End Set
    End Property

    Public Property ResetTTS As Boolean
        Get
            Return (_RZ_Type = "T")
        End Get
        Set(value As Boolean)
            If value Then
                _RZ_Type = "T"
            Else
                _RZ_Type = ""
            End If
        End Set
    End Property

    Public Property RezeptKommentar As String
        Get
            Return _RezeptKommentar
        End Get
        Set(value As String)
            _RezeptKommentar = wb_Functions.Truncate(wb_Functions.XRemoveSonderZeichen(value), 30, True)
            _DataHasChanged = True
        End Set
    End Property

    Public Property AenderungNummer As Integer
        Get
            Return _AenderungNummer
        End Get
        Set(value As Integer)
            _AenderungNummer = value
        End Set
    End Property

    Public Property AenderungDatum As String
        Get
            Return _AenderungDatum
        End Get
        Set(value As String)
            _AenderungDatum = value
        End Set
    End Property

    Public Property AenderungName As String
        Get
            Return _AenderungName
        End Get
        Set(value As String)
            _AenderungName = value
        End Set
    End Property

    Public Property AenderungUserNr As Integer
        Get
            Return _AenderungUserNr
        End Get
        Set(value As Integer)
            _AenderungUserNr = value
        End Set
    End Property

    Public Property RezeptTeigTemperatur As Double
        Get
            'suche nach Teigtemperatur-Messung-Komponenten in der Rezeptur
            _RezeptTeigTemperatur = FindeTeigTempKomponente(RootRezeptSchritt, _RezeptTeigTemperatur)
            Return _RezeptTeigTemperatur
        End Get
        Set(value As Double)
            _RezeptTeigTemperatur = value
        End Set
    End Property

    ''' <summary>
    ''' Sucht in allen Child-Rezeptschritten nach einer Teigtemperatur-Komponente
    ''' Wenn eine solche Komponente gefunden wird, dann wird der Sollwert zurückgegeben. Falls keine
    ''' Komponente gefunden werden kann, wird der übergebene Tempraturwert wieder zurückgegeben.
    ''' Damit kann der letzte Eintrag in der (flachen) Rezeptur ermittelt werden.
    ''' </summary>
    ''' <param name="RootRezeptSchritt"></param>
    ''' <param name="TeigTemperatur"></param>
    ''' <returns></returns>
    Private Function FindeTeigTempKomponente(RootRezeptSchritt As wb_Rezeptschritt, ByRef TeigTemperatur As Double) As Double
        'alle Rezeptschritte (nur das flache Rezept) umrechnen
        For Each rs As wb_Rezeptschritt In RootRezeptSchritt.ChildSteps
            'alle Rezept-Zeilen ohne Sollwert finden
            If Not wb_Functions.TypeIstSollMenge(rs.Type, rs.ParamNr) Then
                'prüfe ob die Rezept-Zeilen eine Teigtemperatur-Komponente enthält
                If wb_Functions.TypeIstTeigTemperaturSollwert(rs.RohNr) Then
                    TeigTemperatur = rs.Sollwert
                Else
                    'Wenn die Rezept-Zeile keinen Sollwert hat, werden alle Child-Steps umgerechnent (Produktions-Stufe...)
                    If rs.ChildSteps.Count > 0 Then
                        TeigTemperatur = FindeTeigTempKomponente(rs, TeigTemperatur)
                    End If
                End If
            End If
        Next
        Return TeigTemperatur
    End Function

    Public ReadOnly Property ZutatenListe(ShowENummern As Boolean, Optimize As Boolean, ReCalc As Boolean) As String
        Get
            'Zutatenliste erstellen, wenn notwendig
            If CalcZutatenListe(ReCalc) Then
                'Zutatenliste optimieren
                If Optimize Then
                    _Zutaten.Opt()
                End If
            End If
            'Druckfähige Zutatenliste 
            Return _Zutaten.Print(ShowENummern)
        End Get
    End Property

    Public ReadOnly Property MehlZusammensetzung(TrennZeichen As String) As String
        Get
            CalcZutatenListe()
            Return _Zutaten.PrintMehlZusammenSetzung(TrennZeichen)
        End Get
    End Property

    Private Function CalcZutatenListe(Optional ReCalc As Boolean = False) As Boolean
        'Zutatenliste löschen wenn neu berechnet werden soll
        If ReCalc Then
            _Zutaten.Clear()
        End If
        'Zutatenliste berechnen (wenn notwendig)
        If (_Zutaten.Liste.Count = 0) Then
            'Zutatenliste aller Rezeptschritte berechnen
            RootRezeptSchritt.CalcZutaten(_Zutaten.Liste, ReCalc)
            Return True
        Else
            Return False
        End If
    End Function

    Public Property c As Integer
        Get
            Return _KneterKennLinie
        End Get
        Set(value As Integer)
            _KneterKennLinie = value
        End Set
    End Property


    ''' <summary>
    ''' Flag bestimmt, ob bei der Abfrage des Preises, die Berechnung neu
    ''' angestossen wird (Bei OrgaBack Abfrage der Preis-Information aus dem Arikel-Objekt)
    ''' 
    ''' Wird z.B. bei der Produktionsplanung abgeschaltet um Rechenzeit zu sparen !
    ''' </summary>
    ''' <returns></returns>
    Public Property ReadCalcPreis As Boolean
        Get
            Return _ReadCalcPreis
        End Get
        Set(value As Boolean)
            _ReadCalcPreis = value
        End Set
    End Property

    ''' <summary>
    ''' Erzeugt ein neues Rezeptur-Objekt.
    ''' Nach dem Einlesen der Rezeptschritte aus der Datenbank wird das Rezept-Gesamtgewicht berechnet.
    ''' (Anzeige der Rezeptbestandteile bezogen auf die Rezept-Gesamtmenge.
    ''' 
    ''' Optional können noch Rohstoff-Nummer und Rohstoff-Bezeichnung übergeben werden, diese werden bei
    ''' Fehlermeldungen (Rekursiver Aufruf von Rezept-im-Rezept) zur Information mit als Fehlermeldung
    ''' ausgegeben.
    ''' </summary>
    Public Sub New(RzNr As Integer, Parent As Object, Backverlust As Double, Optional ByRef RzVariante As Integer = 1, Optional KNummer As String = "", Optional KBezeichnung As String = "", Optional NoMessage As Boolean = False, Optional ReadCalcPreis As Boolean = True)
        'Zeiger auf die aufrufende Klasse
        _Parent = Parent
        _RezeptNr = RzNr
        _RezeptVariante = RzVariante
        _NoMessage = NoMessage
        _ReadCalcPreis = ReadCalcPreis
        TeigChargen.ErrorCheck = False
        TeigChargen.TeigGewicht = wb_Global.UNDEFINED

        'Rekursion begrenzen - Parent ermitteln
        Dim x As wb_Rezept = Me._Parent
        While x IsNot Nothing
            If x.RezeptNr = RzNr Then
                If Not NoMessage Then
                    Throw New Exception("Rezept verweist auf sich selbst bei Komponente " & KNummer & " " & KBezeichnung)
                End If
                Exit Sub
            End If
            x = x._Parent
        End While

        'Rezeptkopf mit Variante x aus der Datenbank einlesen
        MySQLdbSelect_RzKopf(_RezeptNr, _RezeptVariante)

        'alle Rezeptschritte aus der Datenbank einlesen
        MySQLdbSelect_RzSchritt(_RezeptNr, _RezeptVariante)
        'nach Einlesen aus der Datenbank müssen alle Werte neu berechnet werden
        Recalculate = True
        'Rezeptgesamtgewicht berechnen und an alle Rezeptschritte propagieren
        'wird benötigt zur Berechnung des prozentualen Anteils der Komponenten(Rezeptschritte) am Rezeptgewicht
        _RootRezeptSchritt.RezGewicht = RezeptGewicht
        'Brutto-Rezeptgesamtgewicht berechnen und an alle Rezeptschritte propagieren
        'wird benötigt zur Berechnung der Nährwerte
        _RootRezeptSchritt.BruttoRezGewicht = BruttoRezeptGewicht
        _RootRezeptSchritt.NwtRezGewicht = NwtRezeptGewicht
        _RootRezeptSchritt.Sollwert = NwtRezeptGewicht
        'Root-Rezeptschritt kennzeichnen (war -1 !!!)
        _RootRezeptSchritt.SchrittNr = 0
        'Backverlust aus der übergeordneten Komponente 
        _RootRezeptSchritt.Backverlust = Backverlust

    End Sub

    Public Sub New(RzNr As Integer)
        'Rezeptnr und Variante merken
        _RezeptNr = RzNr
        _RezeptVariante = 1
        _Rezeptgruppe = 0
        'Rezeptkopf mit Variante x aus der Datenbank einlesen
        MySQLdbSelect_RzKopf(_RezeptNr, _RezeptVariante)
    End Sub

    ''' <summary>
    ''' Einlesen Rezeptkopf und Rezeptur anhand der alphanumerischen Rezeptnummer. 
    ''' Optional wird die Liniengruppe mit übergeben (Kocher). Wenn das Rezept nicht vorhanden ist,
    ''' wird eine leere Hülle erzeugt.
    ''' </summary>
    ''' <param name="Rezeptnummer"></param>
    ''' <param name="RzLinienGruppe"></param>
    Public Sub New(Rezeptnummer As String, Optional RzLinienGruppe As Integer = wb_Global.LinienGruppeStandard, Optional RzVariante As Integer = 1)
        'Rezeptkopf aus Datenbank lesen
        If MySQLdbSelect_RzKopf(Rezeptnummer, RzVariante) Then
            'wenn ein Rezept vorhanden ist, Rezeptschritte einlesen
            MySQLdbSelect_RzSchritt(RezeptNr, RzVariante)
        Else
            'Wenn kein Rezept gefunden wurde, wird ein neues leeres Rezept angelegt (neue Rezeptnummer)
            MySQLdbNew(RzVariante, RzLinienGruppe)
        End If

    End Sub

    ''' <summary>
    ''' Einlesen Rezeptkopf und Rezeptur aus der Historie.
    ''' Wenn eine Rezept-Im-Rezept Rekursion auftritt, wird das Einlesen abgebrochen.
    ''' Eine Rückmeldung erfolgt nicht!
    ''' </summary>
    ''' <param name="RzNr"></param>
    ''' <param name="RzVariante"></param>
    ''' <param name="RzAendIndex"></param>
    Public Sub New(RzNr As Integer, Parent As Object, RzVariante As Integer, RzAendIndex As Integer)
        'Rezeptnr und Variante merken
        _RezeptNr = RzNr
        _RezeptVariante = RzVariante

        'Rekursion begrenzen - Parent ermitteln
        Dim x As wb_Rezept = Me._Parent
        While x IsNot Nothing
            If x.RezeptNr = RzNr Then
                Exit Sub
            End If
            x = x._Parent
        End While

        'Rezeptkopf mit Variante x aus der Datenbank einlesen
        MySQLdbSelect_RzKopf(RzNr, RzVariante, RzAendIndex)

        'alle Rezeptschritte aus der Datenbank einlesen
        MySQLdbSelect_RzSchritt(RzNr, RzVariante, RzAendIndex)
    End Sub

    Public Sub New()
        'Erzeugt eine zunächst 'leere' Hülle ohne Daten
    End Sub

    ''' <summary>
    ''' Kopiert alle Rezeptschritte vom angegebenen Rezept in die aktuelle Rezeptur und speichert diese anschliessend
    ''' (Rezept_Main Rezept kopieren)
    ''' </summary>
    ''' <param name="CopyFrom_RzNr"></param>
    ''' <param name="CopyFromRz_Variante"></param>
    Public Sub Copy(CopyFrom_RzNr As Integer, CopyFromRz_Variante As Integer)
        'Rezept einlesen (alte Nummer/Variante)
        MySQLdbSelect_RzSchritt(CopyFrom_RzNr, CopyFromRz_Variante)
        'Rezept speichern (neue Nummer/Variante)
        MySQLdbWrite_RzSchritt(_RezeptNr, 1)
    End Sub

    Friend Sub LoadData(dataGridView As wb_DataGridView)
        _RezeptNr = dataGridView.iField("RZ_Nr")
        _RezeptVariante = dataGridView.iField("RZ_Variante_Nr")

        RezeptNummer = dataGridView.Field("RZ_Nr_AlNum")
        RezeptBezeichnung = dataGridView.Field("RZ_Bezeichnung")
        RezeptKommentar = dataGridView.Field("RZ_Kommentar")
        LinienGruppe = dataGridView.iField("RZ_Liniengruppe")
        RezeptGruppe = dataGridView.iField("RZ_Gruppe")

        AenderungNummer = dataGridView.iField("RZ_Aenderung_Nr")
        AenderungDatum = dataGridView.Field("RZ_Aenderung_Datum")
        AenderungName = dataGridView.Field("RZ_Aenderung_Name")
        _RZ_Type = dataGridView.Field("RZ_Type")

        'vor dem Einlesen der neuen Werte löschen - sonst Fehler bei der Berechnung
        TeigChargen.Invalidate()

        TeigChargen.TeigGewicht = dataGridView.Field("RZ_Gewicht")
        TeigChargen.MaxCharge.MengeInkg = dataGridView.Field("RZ_Charge_Max")
        TeigChargen.MinCharge.MengeInkg = dataGridView.Field("RZ_Charge_Min")
        TeigChargen.OptCharge.MengeInkg = dataGridView.Field("RZ_Charge_Opt")

        _DataHasChanged = False
    End Sub

    Friend Function SaveData(dataGridView As wb_DataGridView) As Boolean
        If _DataHasChanged Then
            dataGridView.Field("RZ_Nr_AlNum") = RezeptNummer
            dataGridView.Field("RZ_Bezeichnung") = RezeptBezeichnung
            dataGridView.Field("RZ_Kommentar") = wb_Functions.Truncate(RezeptKommentar, 30, True)
            dataGridView.Field("RZ_Liniengruppe") = LinienGruppe
            dataGridView.Field("RZ_Gruppe") = RezeptGruppe
            dataGridView.Field("RZ_Type") = _RZ_Type

            dataGridView.Field("RZ_Charge_Max") = TeigChargen.MaxCharge.MengeInkg
            dataGridView.Field("RZ_Charge_Min") = TeigChargen.MinCharge.MengeInkg
            dataGridView.Field("RZ_Charge_Opt") = TeigChargen.OptCharge.MengeInkg
            _DataHasChanged = False
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Prüft ob diese Rezept-Variante exisitiert und gibt ein entsprechenes Ergebnis zurück.
    ''' Die zu diesem Rezept existierenden Varianten werden in einer Hash-Table gespeichert. Ist die Hash-Table leer (erster Aufruf)
    ''' wird per SQL-Abfrage ermittelt, welche Varianten existieren
    ''' </summary>
    ''' <param name="Variante"></param>
    ''' <returns></returns>
    Public Property HasVariante(Variante As Integer) As Boolean
        Get
            'prüfen ob Varianten schon geladen sind
            If _Varianten Is Nothing Then
                'Hashtable löschen (sicherheitshalber)
                _Varianten = New Hashtable
                _Varianten.Clear()

                Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
                Dim RzVariante As Integer

                'Datensätze aus Tabelle Rezepte lesen
                If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptVarianten, RezeptNr)) Then
                    While winback.Read
                        RzVariante = winback.iField("RZ_Variante_Nr")
                        _Varianten.Add(RzVariante, True)
                    End While
                End If
            End If

            'Variante vorhanden
            If _Varianten.ContainsKey(Variante) Then
                Return _Varianten(Variante)
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If Not _Varianten.ContainsKey(Variante) Then
                _Varianten.Add(Variante, True)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gibt eine Liste von Artikelnummern zurück, die mit dieser Rezeptur verknüpft sind
    ''' </summary>
    ''' <returns></returns>
    Public Function ArtikelVerwendung() As IList
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim ArtikelListe As New List(Of Integer)

        'Datensätze aus Tabelle Komponenten lesen
        If winback.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptVerwendung, RezeptNr)) Then
            While winback.Read
                ArtikelListe.Add(winback.iField("KO_Nr"))
            End While
        End If

        'Verbindung wieder schliessen
        winback.Close()
        Return ArtikelListe
    End Function

    ''' <summary>
    ''' Die Nährwerte aller mit dieser Rezeptur verknüpften Artikel müssen neu berechnet und an OrgaBack geschrieben werden (Flag setzen)
    ''' Rezept-im-Rezept Strukturen werden im Hintergrund-Task auf dem Servre aufgelöst: 
    '''     Komponenten die markiert worden sind ergeben eine Rezeptliste, deren zugehörige 
    '''     Artikel/Rohstoffe dann markiert werden.
    ''' </summary>
    Public Sub ArtikelMarkieren()
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Markiere alle Artikel die diese Rezeptur enthalten
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompSetMarkerRzNr, _RezeptNr, wb_Global.ArtikelMarker.nwtUpdate))
        'Verbindung wieder schliessen
        winback.Close()
    End Sub


    ''' <summary>
    ''' Rezeptkopf-Datensatz neu anlegen
    ''' Es wird nur die Rezept-Nummer (intern) angelegt.
    ''' Die Rezept-Bezeichnung ist "Neu angelegt " mit Datum/Uhrzeit
    ''' 
    ''' Wird eine Variante größer als Eins übergeben, wird das aktuelle Rezept auf diese Variante kopiert und der Rezeptkopf neu angelegt!
    ''' 
    ''' Alle weiteren Rezept-Daten werden mit MySQLdbUpdate eingetragen.
    ''' </summary>
    ''' <returns>Integer - neu anglegte (interne) Rezept-Nummer</returns>
    Public Function MySQLdbNew(Variante As Integer, Optional LinienGruppe As Integer = wb_Global.LinienGruppeStandard) As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'wenn Variante größer Eins wird eine neue Variante angelegt
        If Variante <= 1 Then
            'interne Rezept-Nummer ermitteln aus max(RZ_NR)
            _RezeptNr = wb_sql_Functions.getNewRezeptNummer()
        End If
        'Bezeichnung
        _RezeptBezeichnung = "Rezept neu angelegt " & Date.Now
        'Rezept-Nummer(alpha)
        _RezeptNummer = ""
        'Variante
        _RezeptVariante = Variante
        'Liniengruppe
        _LinienGruppe = LinienGruppe

        'Änderungsdatum ist das aktuelle Datum
        AenderungDatum = Date.Now
        'aktuellen Benutzer NUmmer/Name eintragen
        AenderungUserNr = wb_GlobalSettings.AktUserNr
        AenderungName = wb_GlobalSettings.AktUserName
        'Änderungs-Index ist gleich 0
        AenderungNummer = 0
        'Rezeptgewicht ist gleich 0
        TeigChargen.TeigGewicht = 0

        'sql-Kommando INSERT bilden
        Dim sqlFeld = "RZ_Nr, RZ_Variante_Nr, RZ_Nr_AlNum, RZ_Bezeichnung, RZ_Liniengruppe, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Aenderung_User, RZ_Aenderung_Nr"
        Dim sqlData = _RezeptNr & "," & _RezeptVariante & ", '" & _RezeptNummer & "', '" & _RezeptBezeichnung & "'," & _LinienGruppe & ",'" &
                      wb_sql_Functions.MySQLdatetime(_AenderungDatum) & "','" & _AenderungName & "'," & _AenderungUserNr & "," & _AenderungNummer

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewRezept, sqlFeld, sqlData))
        winback.Close()
        'neuen KompNummer zurückgeben
        Return _RezeptNr
    End Function

    ''' <summary>
    ''' Liest die Rezeptkopfdaten der RezeptNummer/Rezeptvariante aus der winback.Rezepte-Tabelle. Wenn die vorgegebene Rezeptvariante nicht
    ''' existiert, wird die Variante 1 gelesen (Standard-Variante). Wenn Variante 1 nicht exisitiert (Sauerteig-Rezept) wird Variante 0
    ''' gelesen. Die entsprechende Variante wird (byRef) korrigiert.
    ''' Wenn kein Rezeptkopf existiert, wird False zurückgegeben.
    ''' </summary>
    ''' <param name="RezeptNummer"></param>
    ''' <param name="Variante"></param>
    ''' <returns></returns>
    Private Function MySQLdbSelect_RzKopf(RezeptNummer As String, ByRef Variante As Integer, Optional CheckOnly As Boolean = False) As Boolean
        Dim sql As String
        Dim winback As wb_Sql

        'Lesen Rezeptdaten
        'Datenbank-Verbindung öffnen - MySQL-winback
        winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Suche nach Rz_Nr
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptNummer, RezeptNummer, Variante)

        'Datensätze aus Tabelle Rezepte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                If Not CheckOnly Then
                    For i = 0 To winback.MySqlRead.FieldCount - 1
                        MySQLdbRead_Fields(winback.MySqlRead.GetName(i), winback.MySqlRead.GetValue(i))
                    Next
                End If
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Prüft ob die übergebene Rezeptnummer schon existiert
    '''     Wenn die Nummer schon vorhanden ist, wird True zurückgegeben, ist die Nummer frei, False
    ''' </summary>
    ''' <param name="RezeptNummer"></param>
    ''' <returns></returns>
    Public Function MySQLdbCheck_RzKopf(RezeptNummer As String) As Boolean
        'prüfen ob Rezeptnummer(Alphanumerisch) in Variante 1 exisitert. (nur prüfen, nicht laden)
        Return MySQLdbSelect_RzKopf(RezeptNummer, 1, True)
    End Function

    ''' <summary>
    ''' Liest die Rezeptkopfdaten der alphanumerischen RezeptNummer/Rezeptvariante aus der winback.Rezepte-Tabelle. Wenn die vorgegebene Rezeptvariante nicht
    ''' existiert, wird die Variante 1 gelesen (Standard-Variante). Wenn Variante 1 nicht exisitiert (Sauerteig-Rezept) wird Variante 0
    ''' gelesen. Die entsprechende Variante wird (byRef) korrigiert.
    ''' Wenn kein Rezeptkopf existiert, wird False zurückgegeben.
    ''' Ist ein Änderungsindex angegeben wird das Rezept aus wbdaten.HisRezepte (Rezepthistorie) gelesen.
    ''' 
    ''' Mit dem gesetzten Flag CheckOnly wird nur geprüft, ob eine Rezeptur mit dieser Nummer vorhanden ist, es wird nichts gelesen
    ''' </summary>
    ''' <param name="RezeptNr"></param>
    ''' <param name="Variante"></param>
    ''' <param name="AendIndex"></param>
    ''' <returns></returns>
    Private Function MySQLdbSelect_RzKopf(RezeptNr As Integer, ByRef Variante As Integer, Optional AendIndex As Integer = wb_Global.UNDEFINED) As Boolean
        Dim sql As String
        Dim winback As wb_Sql

        'Lesen Rezeptdaten
        If AendIndex = wb_Global.UNDEFINED Then
            'Datenbank-Verbindung öffnen - MySQL-winback
            winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptKopf, RezeptNr, Variante)
        Else
            'Datenbank-Verbindung öffnen - MySQL-wbdaten
            winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr in His_Rezepte
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlHisRezeptKopf, RezeptNr, Variante, AendIndex)
        End If

        'Datensätze aus Tabelle Rezepte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                'Flag Checkonly prüft nur ob ein Rezept mit dieser Nummer vorhanden ist.
                For i = 0 To winback.MySqlRead.FieldCount - 1
                    MySQLdbRead_Fields(winback.MySqlRead.GetName(i), winback.MySqlRead.GetValue(i))
                Next
            End If
                winback.Close()
                Return True
            End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder zu der angegebenen Rezept-Nummer aus der WinBack-Datenbank. 
    ''' 
    ''' Gibt True zurück, wenn der Datensatz gefunden wurde.
    ''' </summary>
    Public Function MySQLdbSelect_RzSchritt(RezeptNr As Integer, Variante As Integer, Optional AendIndex As Integer = wb_Global.UNDEFINED) As Boolean
        Dim sql As String
        Dim winback As wb_Sql

        'Lesen Rezeptdaten
        If AendIndex = wb_Global.UNDEFINED Then
            'Datenbank-Verbindung öffnen - MySQL
            winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptur, RezeptNr, Variante)
        Else
            'Datenbank-Verbindung öffnen - MySQL-wbdaten (Rezept-Historie)
            winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr in His_Rezeptschritte
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlHisRezeptur, RezeptNr, Variante, AendIndex)
        End If

        'Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                MySQLdbRead_RzSchritt(winback.MySqlRead)
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Einlesen aller Datenfelder aus der Datenbank in den Rezeptschritt (SQLRezeptschritt)
    ''' Nach dem Einlesen wird das komplette Objekt SQLRezeptschritt in die iListe(Rezeptschritt kopiert)
    ''' Über GetParentNode wird, abhängig von aktuellen und von vorhergehenden Rezeptschritt festgelegt,
    ''' wie der nächste Schritt in die Kette eingehängt wird (Child/Parent-Node)
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns></returns>
    Private Function MySQLdbRead_RzSchritt(ByRef sqlReader As MySqlDataReader) As Boolean
        Dim Root As wb_Rezeptschritt = _RootRezeptSchritt
        Dim Preis As Double

        'Schleife über alle Rezeptschritt-Datensätze
        'bis alle Datensätze eingelesen sind
        Do
            'Rezeptschritt - Anzahl der Felder im DataSet
            For i = 0 To sqlReader.FieldCount - 1
                MySQLdbRead_Fields(sqlReader.GetName(i), sqlReader.GetValue(i))
            Next
            'aktuellen Preis aus OrgaBack abfragen - Nicht bei Produktionsplanung !
            If (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack) And _ReadCalcPreis Then
                Preis = ob_Artikel_Services.GetArtikelPreis(_SQLRezeptSchritt.Nummer, _SQLRezeptSchritt.Type)
                If Preis > 0.0 Then
                    _SQLRezeptSchritt.PreisProKg = Preis
                End If
            End If

            'Root-Knoten bestimmen abhängig vom aktuellen und vom vorhergehenden Typ
            If _RezeptSchritt IsNot Nothing And _SQLRezeptSchritt IsNot Nothing Then
                Root = GetParentNode(Root, _SQLRezeptSchritt.Type, _SQLRezeptSchritt.ParamNr, _RezeptSchritt.Type, _RezeptSchritt.ParamNr)
            End If

            'neuen Rezeptschritt anlegen
            _RezeptSchritt = New wb_Rezeptschritt(Root, _SQLRezeptSchritt.Bezeichnung)
            'Daten aus MySQL in Rezeptschritt kopieren
            _RezeptSchritt.CopyFrom(_SQLRezeptSchritt)

            'Rezept im Rezept
            If _RezeptSchritt.RezeptNr > 0 Then
                'TODO welche Variante soll hier gelesen werden? (Standart ist Variante Eins)
                Try
                    _RezeptSchritt.RezeptImRezept = New wb_Rezept(_RezeptSchritt.RezeptNr, Me, _RezeptSchritt.Backverlust, _RezeptVariante, _RezeptSchritt.Nummer, _RezeptSchritt.Bezeichnung, _NoMessage, _ReadCalcPreis)
                Catch ex As Exception
                    If Not _NoMessage Then
                        'MsgBox(ex.Message)
                        _RezeptSchritt.RezeptImRezept = Nothing
                    End If
                End Try
                'Sonderfall Kneterschritte
            ElseIf (_RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_KNETER) Or (_RezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT) Then
                'Eingabeformat und Grenzwerte stehen in winback.KomponParams
                _RezeptSchritt.SetType118()
            End If

        Loop While sqlReader.Read
        Return True
    End Function

    ''' <summary>
    ''' Gibt den Parent-Node für einen neuen Rezeptschritt abhängig vom Komponenten.Typ zurück. In einem flachen Rezept ist der Parent-Node
    ''' immer der RootRezeptSchritt.
    ''' 
    ''' Eine Produktions-Stufe hat als Parent-Node immer den RootRezeptschritt
    ''' Eine Kessel-Stufe hat als Parent-Node immer die Produktions-Stufe
    ''' Kneter-Komponenten haben immer einen Parent-Node Kneter-Kopfzeile
    ''' Wasser-Komponenten haben als Parent-Node immer die Wasser-Komponente mit Parameter-Nummer Eins
    ''' 
    ''' </summary>
    ''' <param name="TypeNow">Kompoenten-Type des aktuellen Rezeptschrittes</param>
    ''' <param name="TypeLast">Komponenten-Type des letzten Rezeptscchrittes</param>
    ''' <returns>wb_Rezeptschritt Parent-Node(Rezeptschritt) des aktuellen Rezeptschrittes</returns>
    Private Function GetParentNode(DefaultNode As wb_Rezeptschritt, TypeNow As wb_Global.KomponTypen, ParamNrNow As Integer, TypeLast As wb_Global.KomponTypen, ParamNrLast As Integer) As wb_Rezeptschritt
        Static PST_Node As wb_Rezeptschritt = Nothing
        Static AKT_Node As wb_Rezeptschritt = Nothing

        'aktueller Rezeptschritt ist Produktions-Stufe - der aktuelle Schritt ist Child vom Root-Node
        If TypeNow = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Then
            Return RootRezeptSchritt
            Exit Function
        End If

        'letzter Rezeptschritt war Produktions-Stufe - der neue Rezeptschritt ist Child der Produktions-Stufe
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE Then
            PST_Node = _RezeptSchritt
            AKT_Node = _RezeptSchritt
            Return _RezeptSchritt
        End If

        'aktueller Rezeptschritt ist Kessel-Stufe - der aktuelle Schritt ist Child vom letzten Produktions-Stufen-Node
        If TypeNow = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
            If PST_Node IsNot Nothing Then
                Return PST_Node
            Else
                Return RootRezeptSchritt
                Exit Function
            End If
        End If

        'letzter Rezeptschritt war Kessel - der aktuelle Schritt ist Child vom vorhergehenden Schritt
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_KESSEL Then
            AKT_Node = _RezeptSchritt
            Return _RezeptSchritt
            Exit Function
        End If

        'letzter Rezeptschritt war Wasser
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE Then
            If TypeNow <> wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE Then
                If AKT_Node Is Nothing Then
                    Return RootRezeptSchritt
                Else
                    Return AKT_Node
                End If
                Exit Function
            Else
                'Sonderfall zweimal Wasser nacheinander !!
                If ParamNrNow < ParamNrLast Then
                    Return DefaultNode.ParentStep
                    Exit Function
                End If

                If ParamNrLast = 1 Or ParamNrLast = 3 Then
                    Return _RezeptSchritt
                    Exit Function
                End If
            End If
        End If

        'letzter Rezeptschritt war Kneter-Kopfzeile
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
            If TypeNow <> wb_Global.KomponTypen.KO_TYPE_KNETER Then
                If AKT_Node Is Nothing Then
                    Return RootRezeptSchritt
                Else
                    Return AKT_Node
                End If
                Exit Function
            Else
                Return _RezeptSchritt
                Exit Function
            End If
        End If

        'letzter Rezeptschritt war Kneter-Zeile
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_KNETER And TypeNow <> wb_Global.KomponTypen.KO_TYPE_KNETER Then
            If AKT_Node Is Nothing Then
                Return RootRezeptSchritt
            Else
                Return AKT_Node
            End If
            Exit Function
        End If

        Return DefaultNode

    End Function

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Obejkt-Eigenschaften
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
        'Debug.Print("Feld/Value " & Name & "/" & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                'Nummer(intern)
                Case "KO_Nr", "H_RS_Ko_Nr"
                    _SQLRezeptSchritt.RohNr = Value
                'Nummer(alpha)
                Case "KO_Nr_AlNum", "H_KO_Nr_AlNum"
                    _SQLRezeptSchritt.Nummer = Value
                'Backverlust (SmallInt - Prozent * 100)
                Case "KO_Temp_Korr"
                    _SQLRezeptSchritt.Backverlust = wb_Functions.StrToDouble(Value) / 100
                'Zuschnitt-Verlust (SmallInt - Prozent * 100)
                Case "KA_Artikel_Typ"
                    _SQLRezeptSchritt.Zuschnitt = wb_Functions.StrToDouble(Value) / 100
                'Freigabe Produktion
                Case "KA_Art"
                    If Value = 1 Then
                        _SQLRezeptSchritt.FreigabeProduktion = True
                    Else
                        _SQLRezeptSchritt.FreigabeProduktion = False
                    End If
                'Schritt-Nummer
                Case "RS_Schritt_Nr", "H_RS_Schritt_Nr"
                    _SQLRezeptSchritt.SchrittNr = Value
                'Parameter-Nummer
                Case "RS_ParamNr", "H_RS_ParamNr"
                    _SQLRezeptSchritt.ParamNr = Value
                'Komponenten-Type
                Case "KO_Type", "H_KO_Type"
                    _SQLRezeptSchritt.Type = wb_Functions.IntToKomponType(Value)
                'Bezeichnung
                Case "KO_Bezeichnung", "H_KO_Bezeichnung"
                    _SQLRezeptSchritt.Bezeichnung = wb_Functions.MySqlToUtf8(Value)
                'Kommentar
                Case "KO_Kommentar", "H_KO_Kommentar"
                    _SQLRezeptSchritt.Kommentar = Value
                'Sollwert
                Case "RS_Wert", "H_RS_Wert"
                    'Anstellgut Sauerteig kann auch in Prozent angegeben werden !!
                    If (_SQLRezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_SAUER_ZUGABE) And (_SQLRezeptSchritt.ParamNr = 2) Then
                        _SQLRezeptSchritt.SollwertProzent = Value
                    Else
                        _SQLRezeptSchritt.Sollwert = Value
                    End If

                'Sollwert Produktion (nur Wasser-Temp-Satz - RMF-Basis-Wert) 
                Case "RS_Wert_Prod"
                    If (_SQLRezeptSchritt.Type = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE) And (_SQLRezeptSchritt.ParamNr = 3) Then
                        _SQLRezeptSchritt.WertProd = Value
                    End If
                'Sollwert Produktion (nur His_Rezepte)
                Case "H_RS_Wert_Prod"
                    _SQLRezeptSchritt.WertProd = Value
                'Par1
                Case "RS_Par1", "H_RS_Par1"
                    _SQLRezeptSchritt.Par1 = Value
                'Par2
                Case "RS_Par2", "H_RS_Par2"
                    _SQLRezeptSchritt.Par2 = Value
                'Par3
                Case "RS_Par3", "H_RS_Par3"
                    _SQLRezeptSchritt.Par3 = Value
                'Einheit
                Case "E_Einheit", "H_E_Einheit"
                    _SQLRezeptSchritt.Einheit = wb_Language.TextFilter(wb_Functions.MySqlToUtf8(Value))
                'Eingabeformat
                Case "KT_Format"
                    _SQLRezeptSchritt.Format = wb_Functions.StrToFormat(Value)
                'Eingabe oberer Grenzwert
                Case "KT_OberGW"
                    _SQLRezeptSchritt.OberGW = wb_Functions.StrToDouble(Value)
                'Eingabe unterer Grenzwert
                Case "KT_UnterGW"
                    _SQLRezeptSchritt.UnterGW = wb_Functions.StrToDouble(Value)
                'zählt NICHT zum Rezeptgesamtgewicht
                'KA_zaehlt_zu_RZ_Gesamtmenge = 1    - zählt nicht zu RezGewicht -> True     zählt trotzdem zur Nährwertberechnung -> False
                'KA_zaehlt_zu_RZ_Gesamtmenge = 0    - zählt zu RezGewicht -> False
                'KA_zaehlt_zu_RZ_Gesamtmenge = NULL - zählt zu RezGewicht -> False
                Case "KA_zaehlt_zu_RZ_Gesamtmenge", "H_KA_zaehlt_zu_RZ_Gesamtmenge"
                    _SQLRezeptSchritt.KA_zaehlt_zu_RZ_Gesamtmenge = Value
                'KA_zaehlt_zu_NWT_Gesamtmenge = 1    - zählt nicht zu RezGewicht -> True     zählt trotzdem zur Nährwertberechnung -> True
                Case "KA_zaehlt_zu_NWT_Gesamtmenge", "H_KA_zaehlt_zu_NWT_Gesamtmenge"
                    _SQLRezeptSchritt.KA_zaehlt_zu_NWT_Gesamtmenge = Value

                'Lagerort
                Case "KA_Lagerort"
                    _SQLRezeptSchritt.LagerOrt = Value
                'Preis
                Case "KA_Preis", "H_RS_Preis"
                    _SQLRezeptSchritt.PreisProKg = wb_Functions.StrToDouble(Value)
                'RezeptNr (Rezept im Rezept)
                Case "KA_RZ_Nr", "H_KA_RZ_Nr"
                    _SQLRezeptSchritt.RezeptNr = Value

                'Rohstoff-Gruppe 1
                Case "KA_Grp1"
                    _SQLRezeptSchritt.RohstoffGruppe1 = Value
                'Rohstoff-Gruppe 1
                Case "KA_Grp2"
                    _SQLRezeptSchritt.RohstoffGruppe2 = Value

                'Rezeptkopf - Rezept-Alphanummer
                Case "RZ_Nr_AlNum", "H_RZ_Nr_AlNum"
                    RezeptNummer = Value
                'Rezeptkopf - Rezept-Variante
                Case "RZ_Variante_Nr", "H_RZ_Variante_Nr"
                    _RezeptVariante = Value
                'Rezeptkopf - Rezept-Bezeichnung
                Case "RZ_Bezeichnung", "H_RZ_Bezeichnung"
                    RezeptBezeichnung = wb_Functions.MySqlToUtf8(Value)
                'Rezeptkopf - Rezept-Kommentar
                Case "RZ_Kommentar", "H_RZ_Kommentar"
                    RezeptKommentar = wb_Functions.MySqlToUtf8(Value)
                'Rezeptkopf Teigtemperatur
                Case "RZ_Teigtemperatur", "H_RZ_Teigtemperatur"
                    _RezeptTeigTemperatur = wb_Functions.ValueToDouble(Value)
                'Rezeptkopf - Änderung-Nummer
                Case "RZ_Aenderung_Nr", "H_RZ_Aenderung_Nr"
                    _AenderungNummer = wb_Functions.ValueToInt(Value)
                'Rezeptkopf - Änderung-Datum
                Case "RZ_Aenderung_Datum", "H_RZ_Aenderung_Datum"
                    _AenderungDatum = Value
                'Rezeptkopf - Änderung-Name
                Case "RZ_Aenderung_Name", "H_RZ_Aenderung_Name"
                    _AenderungName = Value
                'Rezeptkopf - Änderung-UserNummer
                Case "RZ_Aenderung_User", "H_RZ_Aenderung_User"
                    _AenderungUserNr = wb_Functions.ValueToInt(Value)
                'Rezeptkopf - Liniengruppe
                Case "RZ_Liniengruppe", "H_RZ_Liniengruppe"
                    _LinienGruppe = wb_Functions.ValueToInt(Value)
                'Rezeptkopf - Rezeptgruppe
                Case "RZ_Gruppe", "H_RZ_Gruppe"
                    _Rezeptgruppe = wb_Functions.ValueToInt(Value)

                    'Rezeptkopf - MinCharge in kg
                Case "RZ_Charge_Min"
                    '_Charge_Min = wb_Functions.StrToDouble(Value)
                    TeigChargen.MinCharge.MengeInkg = wb_Functions.StrToDouble(Value)
                'Rezeptkopf - MaxCharge in kg
                Case "RZ_Charge_Max"
                    '_Charge_Max = wb_Functions.StrToDouble(Value)
                    TeigChargen.MaxCharge.MengeInkg = wb_Functions.StrToDouble(Value)
                'Rezeptkopf - OptCharge in kg
                Case "RZ_Charge_Opt"
                    '_Charge_Opt = wb_Functions.StrToDouble(Value)
                    TeigChargen.OptCharge.MengeInkg = wb_Functions.StrToDouble(Value)
                'Rezeptkopf - Rezeptgewicht
                Case "RZ_Gewicht", "H_RZ_Gewicht"
                    '_RZ_Gewicht = wb_Functions.StrToDouble(Value)
                    TeigChargen.TeigGewicht = wb_Functions.StrToDouble(Value)
                'Rezeptkopf - Kneterkennlinie
                Case "RZ_Kneterkennlinie", "H_RZ_Kneterkennlinie"
                    _KneterKennLinie = wb_Functions.StrToInt(Value)
                Case "RZ_Type"
                    _RZ_Type = Value

            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function

    Public Function MySQLdbWrite_RzSchritt(RezeptNummer As Integer, Variante As Integer) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String
        Dim sqlFelder As String
        Dim sqlData As String

        'Rezeptschritte neu durchnumerieren (sicherheitshalber)
        RootRezeptSchritt.ReCalcRzSteps(0)

        'alte Rezeptur in Historie speichern - Rezeptkopf und Rezeptschritte

        'wenn schon ein Rezeptkopf in His_Rezepte vorhanden ist, wird er gelöscht
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRezKopfInHisRezepte, RezeptNummer, Variante, AenderungNummer)
        winback.sqlCommand(sql)
        'bestehenden Rezeptkopf in wb_daten.His_Rezepte kopieren
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlCopyRezKopfInHisRezepte, RezeptNummer, Variante)
        winback.sqlCommand(sql)

        'wenn schon Rezeptschritte in His_Rezeptschritte vorhanden sind werden diese gelöscht
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRezSchritteInHisRezepte, RezeptNummer, Variante, AenderungNummer)
        winback.sqlCommand(sql)
        'bestehende Rezeptschritte in wb_daten.His_Rezeptschritte kopieren
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlCopyRezSchritteInHisRezepte, RezeptNummer, Variante, AenderungNummer)
        winback.sqlCommand(sql)

        'vorhandene Rezeptur in Datenbank löschen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRzptSchr, RezeptNummer, Variante))

        'sql-Kommando INSERT bilden
        sqlFelder = "RS_RZ_NR, RS_RZ_Variante_Nr, RS_Index, RS_Schritt_Nr, RS_Ko_Nr, RS_ParamNr, " &
                    "RS_Wert, RS_Wert_Prod, RS_Par1, RS_Par2, RS_Par3"
        'Index
        Dim Idx As Integer = 0

        'Schleife über alle Rezeptschritte
        For Each rz As wb_Rezeptschritt In RootRezeptSchritt.Steps
            Idx += 1
            sqlData = RezeptNummer.ToString & "," & Variante.ToString & "," & Idx.ToString & "," &
                      rz.SchrittNr.ToString & "," & rz.RohNr.ToString & "," & rz.ParamNr.ToString & ",'" &
                      rz.Sollwert & "','" & rz.WertProd & "','" & rz.Par1 & "','" & rz.Par2 & "','" & rz.Par3 & "'"
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddRZSchritt, sqlFelder, sqlData)
            winback.sqlCommand(sql)

            Debug.Print("Rezept Schreiben " & rz.SchrittNr & "-" & rz.RohNr & "/" & rz.Nummer & " " & rz.Bezeichnung & " " & rz.Sollwert)
        Next
        winback.Close()
        Return True
    End Function

    Public Function MySQLdbWrite_Rezept(RezepturChanged As Boolean) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String
        Dim sqlData As String

        'wenn die Rezeptur geändert wurde, wird der Änderungs-Index hochgezählt
        If RezepturChanged Then
            'Änderungsdatum ist das aktuelle Datum
            AenderungDatum = Date.Now
            'aktuellen Benutzer NUmmer/Name eintragen
            AenderungUserNr = wb_GlobalSettings.AktUserNr
            AenderungName = wb_GlobalSettings.AktUserName
            'Änderungs-Index wird um Eins erhöht
            AenderungNummer += 1
        End If

        'sql-Kommando UPDATE bilden
        sqlData = "RZ_Nr_AlNum = '" & _RezeptNummer & "', RZ_Bezeichnung = '" & wb_Functions.Truncate(_RezeptBezeichnung, 30) & "', RZ_Gewicht = '" & wb_Functions.FormatStr(RezeptGewicht, 3) & "', " &
                  "RZ_Charge_Opt = '" & wb_Functions.FormatStr(TeigChargen.OptCharge.MengeInkg, 3) & "', " &
                  "RZ_Charge_Min = '" & wb_Functions.FormatStr(TeigChargen.MinCharge.MengeInkg, 3) & "', " &
                  "RZ_Charge_Max = '" & wb_Functions.FormatStr(TeigChargen.MaxCharge.MengeInkg, 3) & "', " &
                  "RZ_Liniengruppe = " & LinienGruppe & ", RZ_TYPE = '" & _RZ_Type & "', RZ_Teigtemperatur = '" & _RezeptTeigTemperatur & "', " &
                  "RZ_Kommentar = '" & wb_Functions.Truncate(_RezeptKommentar, 30, True) & "', RZ_Aenderung_Datum = '" & wb_sql_Functions.MySQLdatetime(_AenderungDatum) & "', " &
                  "RZ_Aenderung_Name = '" & _AenderungName & "', RZ_Aenderung_User = " & _AenderungUserNr & ", RZ_Aenderung_Nr = " & _AenderungNummer
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptUpdate, _RezeptNr, _RezeptVariante, sqlData)
        winback.sqlCommand(sql)

        winback.Close()
        Return True
    End Function

    Public Function MySQLdbDelete_Rezept()
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Return (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRzptKopf, _RezeptNr, _RezeptVariante)) > 0)
    End Function

    Public Function MySQLdbDelete_RezeptSchritte()
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Return (winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRzptSchr, _RezeptNr, _RezeptVariante)) > 0)
    End Function

    Public Function MySQLdbDelete_HisRezept()
        Dim wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        Return (wbdaten.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelHRzptKopf, _RezeptNr, _RezeptVariante)) > 0)
    End Function

    Public Function MySQLdbDelete_HisRezeptSchritte()
        Dim wbdaten = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        Return (wbdaten.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelHRzptSchr, _RezeptNr, _RezeptVariante)) > 0)
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        _RezeptSchritt = Nothing
        _RootRezeptSchritt = Nothing
        _SQLRezeptSchritt = Nothing
        TeigChargen = Nothing
    End Sub
End Class

'RS_RZ_Nr	
'RS_RZ_Variante_Nr
'RS_Index
'RS_Schritt_Nr
'RS_Ko_Nr
'RS_ParamNr
'RS_Wert
'RS_Wert_Prod
'RS_Par1
'RS_Par2
'RS_Par3
'RS_Preis
'RS_PreisEinheit
'RS_Timestamp

'RZ_Nr
'RZ_Variante_Nr
'RZ_Nr_AlNum
'RZ_Bezeichnung
'RZ_Gewicht
'RZ_Kommentar
'RZ_Kurzname
'RZ_Matchcode
'RZ_Type
'RZ_Charge_Opt
'RZ_Charge_Min
'RZ_Charge_Max
'RZ_Aenderung_Datum
'RZ_Aenderung_User
'RZ_Aenderung_Name
'RZ_Aenderung_Nr
'RZ_Teigtemperatur
'RZ_Kneterkennlinie
'RZ_Verarbeitungshinweise
'RZ_Liniengruppe
'RZ_Gruppe
'KA_Gruppe
'RZ_Timestamp

