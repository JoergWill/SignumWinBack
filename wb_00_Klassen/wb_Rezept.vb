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
    Private _RootRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _SQLRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _RezeptSchritt As wb_Rezeptschritt

    Private _RezeptGewicht As Double
    Private _BruttoRezeptGewicht As Double
    Private _RezeptPreis As Double
    Private _RezeptGesamtMehlmenge As Double
    Private _RezeptGesamtWasserMenge As Double
    Private _RezeptNummer As Integer
    Private _ktTyp301 As New wb_KomponParam301
    Public _Parent As Object

    Public ReadOnly Property RezeptNummer As Integer
        Get
            Return _RezeptNummer
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
    ''' Die Berechnung erfolgt über RezeptSchritt.Gewicht(Get)
    ''' </summary>
    ''' <returns>Double - Rezept-Gesamtgewicht</returns>
    Public ReadOnly Property RezeptGewicht As Double
        Get
            'wenn der Wert noch nicht berechnet wurde
            If _RezeptGewicht = wb_Global.UNDEFINED Then
                _RezeptGewicht = _RootRezeptSchritt.Gewicht
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

    ''' <summary>
    ''' Erzeugt ein neues Rezeptur-Objekt.
    ''' Nach dem Einlesen der Rezeptschritte aus der Datenbank wird das Rezept-Gesamtgewicht berechnet.
    ''' (Anzeige der Rezeptbestandteile bezogen auf die Rezept-Gesamtmenge.
    ''' 
    ''' Optional können noch Rohstoff-Nummer und Rohstoff-Bezeichnung übergeben werden, diese werden bei
    ''' Fehlermeldungen (Rekursiver Aufruf von Rezept-im-Rezept) zur Information mit als Fehlermeldung
    ''' ausgegeben.
    ''' </summary>
    Public Sub New(RzNr As Integer, Parent As Object, Optional RzVariante As Integer = 1, Optional KNummer As String = "", Optional KBezeichnung As String = "")
        'Zeiger auf die aufrufende Klasse
        _Parent = Parent
        _RezeptNummer = RzNr

        'Rekursion begrenzen - Parent ermitteln
        Dim x As wb_Rezept = Me._Parent
        While x IsNot Nothing
            If x.RezeptNummer = RzNr Then
                Throw New Exception("Rezept verweist auf sich selbst bei Komponente " & KNummer & " " & KBezeichnung)
                Exit Sub
            End If
            x = x._Parent
        End While

        'alle Rezeptschritte aus der Datenbank einlesen
        MySQLdbRead(RzNr, RzVariante)
        'nach Einlesen aus der Datenbank müssen alle Werte neu berechnet werden
        Recalculate = True
        'Rezeptgesamtgewicht berechnen und an alle Rezeptschritte propagieren
        'wird benötigt zur Berechnung des prozentualen Anteils der Komponenten(Rezeptschritte) am Rezeptgewicht
        _RootRezeptSchritt.RezGewicht = RezeptGewicht
        'Brutto-Rezeptgesamtgewicht berechnen und an alle Rezeptschritte propagieren
        'wird benötigt zur Berechnung der Nährwerte
        _RootRezeptSchritt.BruttoRezGewicht = BruttoRezeptGewicht

    End Sub

    ''' <summary>
    ''' Liest alle Datenfelder zu der angegebenen Rezept-Nummer aus der WinBack-Datenbank. 
    ''' 
    ''' Gibt True zurück, wenn der Datensatz gefunden wurde.
    ''' </summary>
    Private Function MySQLdbRead(RezeptNummer As Integer, Variante As Integer) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach Rz_Nr
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptur, RezeptNummer, Variante)

        'Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                MySQLdbRead(winback.MySqlRead)
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
    Private Function MySQLdbRead(ByRef sqlReader As MySqlDataReader) As Boolean
        Dim Root As wb_Rezeptschritt = RootRezeptSchritt

        'Schleife über alle Rezeptschritt-Datensätze
        'Bis alle Datensätze eingelesen sind
        Do
            'Rezeptschritt - Anzahl der Felder im DataSet
            For i = 0 To sqlReader.FieldCount - 1
                MySQLdbRead_RzSchritte(sqlReader.GetName(i), sqlReader.GetValue(i))
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
                    _RezeptSchritt.RezeptImRezept = New wb_Rezept(_RezeptSchritt.RezeptNr, Me,, _RezeptSchritt.Nummer, _RezeptSchritt.Bezeichnung)
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
                Return AKT_Node
                Exit Function
            Else
                If ParamNrLast = 1 Then
                    Return _RezeptSchritt
                    Exit Function
                End If
            End If
        End If

        'letzter Rezeptschritt war Kneter-Kopfzeile
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_KNETERREZEPT Then
            If TypeNow <> wb_Global.KomponTypen.KO_TYPE_KNETER Then
                Return AKT_Node
                Exit Function
            Else
                Return _RezeptSchritt
                Exit Function
            End If
        End If

        'letzter Rezeptschritt war Kneter-Zeile
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_KNETER And TypeNow <> wb_Global.KomponTypen.KO_TYPE_KNETER Then
            Return AKT_Node
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
    Private Function MySQLdbRead_RzSchritte(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        Debug.Print("Feldname/Wert " & Name & "/" & Value.ToString)
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
                    _SQLRezeptSchritt.Bezeichnung = Value

                'Sollwert
                Case "RS_Wert"
                    _SQLRezeptSchritt.Sollwert = Value

                'Einheit
                Case "E_Einheit"
                    _SQLRezeptSchritt.Einheit = Value

                'zählt NICHT zum Rezeptgesamtgewicht
                Case "KA_zaehlt_zu_RZ_Gesamtmenge"
                    _SQLRezeptSchritt.ZaehltNichtZumRezeptGewicht = wb_sql_Functions.MySQLBoolean(Value)

                    'Preis
                Case "KA_Preis"
                    _SQLRezeptSchritt.PreisProKg = wb_Functions.StrToDouble(Value)

                'RezeptNr (Rezept im Rezept)
                Case "KA_RZ_Nr"
                    _SQLRezeptSchritt.RezeptNr = Value

            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function

End Class
