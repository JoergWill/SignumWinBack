Imports MySql.Data.MySqlClient
Imports WinBack

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
    Private _RezeptNummer As String
    Private _RezeptBezeichnung As String
    Private _RezeptKommentar As String
    Private _AenderungNummer As Integer
    Private _AenderungDatum As String
    Private _AenderungName As String
    Private _RezeptTeigTemperatur As Double
    Private _LinienGruppe As Integer
    Private _Charge_Min As Double
    Private _Charge_Max As Double
    Private _Charge_Opt As Double

    Private _RootRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _SQLRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _RezeptSchritt As wb_Rezeptschritt

    Private _RZ_Gewicht As Double
    Private _RezeptGewicht As Double = wb_Global.UNDEFINED
    Private _BruttoRezeptGewicht As Double = wb_Global.UNDEFINED
    Private _RezeptPreis As Double = wb_Global.UNDEFINED
    Private _RezeptGesamtMehlmenge As Double = wb_Global.UNDEFINED
    Private _RezeptGesamtWasserMenge As Double = wb_Global.UNDEFINED
    Private _RezeptNr As Integer
    Private _RezeptVariante As Integer
    Private _ktTyp301 As New wb_KomponParam301
    Private _Zutaten As New wb_ZutatenListe

    Private _LLRezeptur As New ArrayList
    Private _LLBig4 As New ArrayList

    Public _Parent As Object
    Public TeigChargen As New wb_MinMaxOptCharge

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
    ''' Die Berechnung erfolgt über RezeptSchritt.Gewicht(Get). Wenn keine Child-Steps vorhanden sind wird der Wert aus dem Rezept-Kopf(Datenbank) zurückgegeben
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamtgewicht</returns>
    Public ReadOnly Property RezeptGewicht As Double
        Get
            'wenn der Wert noch nicht berechnet wurde
            If _RezeptGewicht = wb_Global.UNDEFINED Then
                If _RootRezeptSchritt.ChildSteps.Count = 0 Then
                    _RezeptGewicht = _RZ_Gewicht
                Else
                    _RezeptGewicht = _RootRezeptSchritt.Gewicht
                    TeigChargen.TeigGewicht = _RezeptGewicht
                End If
            End If
            Return _RezeptGewicht
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
            _RezeptGewicht = wb_Global.UNDEFINED
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
            Return _RootRezeptSchritt.ktTyp301
        End Get
    End Property

    Public Property RezeptNummer As String
        Get
            Return _RezeptNummer
        End Get
        Set(value As String)
            _RezeptNummer = value
        End Set
    End Property

    Public Property RezeptBezeichnung As String
        Get
            Return _RezeptBezeichnung
        End Get
        Set(value As String)
            _RezeptBezeichnung = value
        End Set
    End Property

    Public ReadOnly Property Variante As String
        Get
            Return _RezeptVariante
        End Get
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
        End Set
    End Property

    Public Property RezeptKommentar As String
        Get
            Return _RezeptKommentar
        End Get
        Set(value As String)
            _RezeptKommentar = value
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

    Public Property RezeptTeigTemperatur As Double
        Get
            Return _RezeptTeigTemperatur
        End Get
        Set(value As Double)
            _RezeptTeigTemperatur = value
        End Set
    End Property

    Public ReadOnly Property ZutatenListe(Mode As wb_Global.ZutatenListeMode) As String
        Get
            If _Zutaten.Liste.Count = 0 Then
                'Zutatenliste aller Rezeptschritte berechnen
                RootRezeptSchritt.CalcZutaten(_Zutaten.Liste)
                'Zutatenliste optimieren
                _Zutaten.Opt()
            End If
            'Druckfähige Zutatenliste 
            Return _Zutaten.Print(Mode)
        End Get
    End Property

    Public Property MinChargekg As Double
        Get
            If _Charge_Min > 0 Then
                Return _Charge_Min
            Else
                Return RezeptGewicht
            End If
        End Get
        Set(value As Double)
            _Charge_Min = value
        End Set
    End Property

    Public Property MaxChargekg As Double
        Get
            If _Charge_Max > 0 Then
                Return _Charge_Max
            Else
                Return RezeptGewicht
            End If
        End Get
        Set(value As Double)
            _Charge_Max = value
        End Set
    End Property

    Public Property OptChargekg As Double
        Get
            If _Charge_Opt > 0 Then
                Return _Charge_Opt
            Else
                Return RezeptGewicht
            End If
        End Get
        Set(value As Double)
            _Charge_Opt = value
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
    Public Sub New(RzNr As Integer, Parent As Object, Optional ByRef RzVariante As Integer = 1, Optional KNummer As String = "", Optional KBezeichnung As String = "")
        'Zeiger auf die aufrufende Klasse
        _Parent = Parent
        _RezeptNr = RzNr
        _RezeptVariante = RzVariante

        'Rekursion begrenzen - Parent ermitteln
        Dim x As wb_Rezept = Me._Parent
        While x IsNot Nothing
            If x.RezeptNr = RzNr Then
                Throw New Exception("Rezept verweist auf sich selbst bei Komponente " & KNummer & " " & KBezeichnung)
                Exit Sub
            End If
            x = x._Parent
        End While

        'Rezeptkopf mit Variante x aus der Datenbank einlesen
        MySQLdbSelect_RzKopf(_RezeptNr, _RezeptVariante)
        'Rezept-Nummer/Name/Variante im Fenster-Titel 


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
        _RootRezeptSchritt.Sollwert = BruttoRezeptGewicht
        'Root-Rezeptschritt kennzeichnen (war -1 !!!)
        _RootRezeptSchritt.SchrittNr = 0

    End Sub

    Public Sub New(RzNr As Integer)
        'Rezeptkopf mit Variante x aus der Datenbank einlesen
        MySQLdbSelect_RzKopf(RzNr, 1)
    End Sub

    ''' <summary>
    ''' Liest die Rezeptkopfdaten der RezeptNummer/Rezeptvariante aus der winback.Rezepte-Tabelle. Wenn die vorgegebene Rezeptvariante nicht
    ''' existiert, wird die Variante 1 gelesen (Standard-Variante). Wenn Variante 1 nicht exisitiert (Sauerteig-Rezept) wird Variante 0
    ''' gelesen. Die entsprechende Variante wird (byRef) korrigiert.
    ''' Wenn kein Rezeptkopf existiert, wird False zurückgegeben
    ''' </summary>
    ''' <param name="RezeptNummer"></param>
    ''' <param name="Variante"></param>
    ''' <returns></returns>
    Private Function MySQLdbSelect_RzKopf(RezeptNummer As Integer, ByRef Variante As Integer) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach Rz_Nr
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptKopf, RezeptNummer, Variante)

        'Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                For i = 0 To winback.MySqlRead.FieldCount - 1
                    MySQLdbRead_Fields(winback.MySqlRead.GetName(i), winback.MySqlRead.GetValue(i))
                Next
                winback.Close()
                Return True
            End If
        End If
        winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder zu der angegebenen Rezept-Nummer aus der WinBack-Datenbank. 
    ''' 
    ''' Gibt True zurück, wenn der Datensatz gefunden wurde.
    ''' </summary>
    Private Function MySQLdbSelect_RzSchritt(RezeptNummer As Integer, Variante As Integer) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach Rz_Nr
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptur, RezeptNummer, Variante)

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

        'Schleife über alle Rezeptschritt-Datensätze
        'bis alle Datensätze eingelesen sind
        Do
            'Rezeptschritt - Anzahl der Felder im DataSet
            For i = 0 To sqlReader.FieldCount - 1
                MySQLdbRead_Fields(sqlReader.GetName(i), sqlReader.GetValue(i))
            Next

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
                    _RezeptSchritt.RezeptImRezept = New wb_Rezept(_RezeptSchritt.RezeptNr, Me, _RezeptVariante, _RezeptSchritt.Nummer, _RezeptSchritt.Bezeichnung)
                Catch ex As Exception
                    MsgBox(ex.Message)
                    _RezeptSchritt.RezeptImRezept = Nothing
                End Try
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
                Case "KO_Nr"
                    _SQLRezeptSchritt.RohNr = Value
                'Nummer(alpha)
                Case "KO_Nr_AlNum"
                    _SQLRezeptSchritt.Nummer = Value
                'Schritt-Nummer
                Case "RS_Schritt_Nr"
                    _SQLRezeptSchritt.SchrittNr = Value
                'Parameter-Nummer
                Case "RS_ParamNr"
                    _SQLRezeptSchritt.ParamNr = Value
                'Komponenten-Type
                Case "KO_Type"
                    _SQLRezeptSchritt.Type = wb_Functions.IntToKomponType(Value)
                'Bezeichnung
                Case "KO_Bezeichnung"
                    _SQLRezeptSchritt.Bezeichnung = wb_Functions.MySqlToUtf8(Value)
                'Kommentar
                Case "KO_Kommentar"
                    _SQLRezeptSchritt.Kommentar = Value
                'Sollwert
                Case "RS_Wert"
                    _SQLRezeptSchritt.Sollwert = Value
                'Sollwert Produktion
                Case "RS_Wert_Prod"
                    _SQLRezeptSchritt.WertProd = Value
                'Par1
                Case "RS_Par1"
                    _SQLRezeptSchritt.Par1 = Value
                'Par2
                Case "RS_Par2"
                    _SQLRezeptSchritt.Par2 = Value
                'Par3
                Case "RS_Par3"
                    _SQLRezeptSchritt.Par3 = Value
                'Einheit
                Case "E_Einheit"
                    _SQLRezeptSchritt.Einheit = wb_Language.TextFilter(Value)
                'zählt NICHT zum Rezeptgesamtgewicht
                Case "KA_zaehlt_zu_RZ_Gesamtmenge"
                    _SQLRezeptSchritt.ZaehltNichtZumRezeptGewicht = wb_sql_Functions.MySQLBoolean(Value)
                    'Preis
                Case "KA_Preis"
                    _SQLRezeptSchritt.PreisProKg = wb_Functions.StrToDouble(Value)
                'RezeptNr (Rezept im Rezept)
                Case "KA_RZ_Nr"
                    _SQLRezeptSchritt.RezeptNr = Value

                'Rezeptkopf - Rezept-Alphanummer
                Case "RZ_Nr_AlNum"
                    RezeptNummer = Value
                'Rezeptkopf - Rezept-Variante
                Case "RZ_Variante_Nr"
                    _RezeptVariante = Value
                'Rezeptkopf - Rezept-Bezeichnung
                Case "RZ_Bezeichnung"
                    RezeptBezeichnung = wb_Functions.MySqlToUtf8(Value)
                'Rezeptkopf Teigtemperatur
                Case "RZ_Teigtemperatur"
                    _RezeptTeigTemperatur = wb_Functions.ValueToDouble(Value)
                'Rezeptkopf - Änderung-Nummer
                Case "RZ_Aenderung_Nr"
                    _AenderungNummer = wb_Functions.ValueToInt(Value)
                'Rezeptkopf - Änderung-Datum
                Case "RZ_Aenderung_Datum"
                    _AenderungDatum = Value
                'Rezeptkopf - Änderung-Name
                Case "RZ_Aenderung_Name"
                    _AenderungName = Value
                'Rezeptkopf - Liniengruppe
                Case "RZ_Liniengruppe"
                    _LinienGruppe = wb_Functions.ValueToInt(Value)

                'Rezeptkopf - MinCharge in kg
                Case "RZ_Charge_Min"
                    _Charge_Min = wb_Functions.StrToDouble(Value)
                    TeigChargen.MinCharge.MengeInkg = wb_Functions.StrToDouble(Value)
                'Rezeptkopf - MaxCharge in kg
                Case "RZ_Charge_Max"
                    _Charge_Max = wb_Functions.StrToDouble(Value)
                    TeigChargen.MaxCharge.MengeInkg = wb_Functions.StrToDouble(Value)
                'Rezeptkopf - OptCharge in kg
                Case "RZ_Charge_Opt"
                    _Charge_Opt = wb_Functions.StrToDouble(Value)
                    TeigChargen.OptCharge.MengeInkg = wb_Functions.StrToDouble(Value)
                'Rezeptkopf - Rezeptgewicht
                Case "RZ_Gewicht"
                    _RZ_Gewicht = wb_Functions.StrToDouble(Value)
                    TeigChargen.TeigGewicht = wb_Functions.StrToDouble(Value)

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

        'alte Rezeptur in Historie speichern

        'vorhandene Rezeptur in Datenbank löschen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRZSchritt, RezeptNummer, Variante))

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

            Debug.Print("Rezept Schreiben " & rz.SchrittNr & "-" & rz.RohNr & "/" & rz.Nummer & " " & rz.Bezeichnung)
        Next
        winback.Close()
        Return True
    End Function


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
