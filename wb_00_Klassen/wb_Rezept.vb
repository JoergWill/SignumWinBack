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
    Private _RezeptNummer As String
    Private _RezeptBezeichnung As String
    Private _RezeptKommentar As String
    Private _AenderungNummer As Integer
    Private _AenderungDatum As String
    Private _AenderungName As String
    Private _AenderungUserNr As Integer
    Private _RezeptTeigTemperatur As Double
    Private _LinienGruppe As Integer
    Private _KneterKennLinie As Integer

    Public TeigChargen As New wb_MinMaxOptCharge
    Private _DataHasChanged As Boolean = False

    Private _RootRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _SQLRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private _RezeptSchritt As wb_Rezeptschritt

    'Private _RZ_Gewicht As Double
    'Private _RezeptGewicht As Double = wb_Global.UNDEFINED
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

    Public Property RezeptKommentar As String
        Get
            Return _RezeptKommentar
        End Get
        Set(value As String)
            _RezeptKommentar = wb_Functions.XRemoveSonderZeichen(value)
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

    Public Property c As Integer
        Get
            Return _KneterKennLinie
        End Get
        Set(value As Integer)
            _KneterKennLinie = value
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
        TeigChargen.TeigGewicht = wb_Global.UNDEFINED

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
    ''' Einlesen Rezeptkopf und Rezeptur aus der Historie
    ''' </summary>
    ''' <param name="RzNr"></param>
    ''' <param name="RzVariante"></param>
    ''' <param name="RzAendIndex"></param>
    Public Sub New(RzNr As Integer, Parent As Object, RzVariante As Integer, RzAendIndex As Integer)
        'Rezeptkopf mit Variante x aus der Datenbank einlesen
        MySQLdbSelect_RzKopf(RzNr, RzVariante, RzAendIndex)

        'alle Rezeptschritte aus der Datenbank einlesen
        MySQLdbSelect_RzSchritt(RzNr, RzVariante, RzAendIndex)
    End Sub

    Public Sub New()
        'Erzeugt eine zunächst 'leere' Hülle ohne Daten
    End Sub

    Friend Sub LoadData(dataGridView As wb_DataGridView)
        _RezeptNr = dataGridView.iField("RZ_Nr")
        _RezeptVariante = dataGridView.iField("RZ_Variante_Nr")

        RezeptNummer = dataGridView.Field("RZ_Nr_AlNum")
        RezeptBezeichnung = dataGridView.Field("RZ_Bezeichnung")
        RezeptKommentar = dataGridView.Field("RZ_Kommentar")
        LinienGruppe = dataGridView.iField("RZ_Liniengruppe")

        AenderungNummer = dataGridView.iField("RZ_Aenderung_Nr")
        AenderungDatum = dataGridView.Field("RZ_Aenderung_Datum")
        AenderungName = dataGridView.Field("RZ_Aenderung_Name")

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
            dataGridView.Field("RZ_Kommentar") = RezeptKommentar
            dataGridView.Field("RZ_Liniengruppe") = LinienGruppe

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
    ''' Rezeptkopf-Datensatz neu anlegen
    ''' Es wird nur die Rezept-Nummer (intern) angelegt.
    ''' Die Komponenten-Bezeichnung ist "Neu angelegt " mit Datum/Uhrzeit
    ''' 
    ''' Alle weiteren Rezept-Daten werden mit MySQLdbUpdate eingetragen.
    ''' </summary>
    ''' <returns>Integer - neu anglegte (interne) Rezept-Nummer</returns>
    Public Function MySQLdbNew(Variante As Integer) As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'interne Rezept-Nummer ermitteln aus max(RZ_NR)
        _RezeptNr = wb_sql_Functions.getNewRezeptNummer()
        _RezeptBezeichnung = "Neu angelegt " & Date.Now

        'Variante
        _RezeptVariante = Variante
        'Liniengruppe
        _LinienGruppe = wb_Global.LinienGruppeStandard

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
        Dim sqlFeld = "RZ_Nr, RZ_Variante_Nr, RZ_Bezeichnung, RZ_Liniengruppe, RZ_Aenderung_Datum, RZ_Aenderung_Name, RZ_Aenderung_User, RZ_Aenderung_Nr"
        Dim sqlData = _RezeptNr & "," & _RezeptVariante & ", '" & _RezeptBezeichnung & "'," & _LinienGruppe & ",'" &
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
    ''' Wenn kein Rezeptkopf existiert, wird False zurückgegeben
    ''' </summary>
    ''' <param name="RezeptNummer"></param>
    ''' <param name="Variante"></param>
    ''' <returns></returns>
    Private Function MySQLdbSelect_RzKopf(RezeptNummer As Integer, ByRef Variante As Integer, Optional AendIndex As Integer = wb_Global.UNDEFINED) As Boolean
        Dim sql As String
        Dim winback As wb_Sql

        'Lesen Rezeptdaten
        If AendIndex = wb_Global.UNDEFINED Then
            'Datenbank-Verbindung öffnen - MySQL-winback
            winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptKopf, RezeptNummer, Variante)
        Else
            'Datenbank-Verbindung öffnen - MySQL-wbdaten
            winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr in His_Rezepte
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlHisRezeptKopf, RezeptNummer, Variante, AendIndex)
        End If

        'Datensätze aus Tabelle Rezepte lesen
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
    Private Function MySQLdbSelect_RzSchritt(RezeptNummer As Integer, Variante As Integer, Optional AendIndex As Integer = wb_Global.UNDEFINED) As Boolean
        Dim sql As String
        Dim winback As wb_Sql

        'Lesen Rezeptdaten
        If AendIndex = wb_Global.UNDEFINED Then
            'Datenbank-Verbindung öffnen - MySQL
            winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptur, RezeptNummer, Variante)
        Else
            'Datenbank-Verbindung öffnen - MySQL-wbdaten (Rezept-Historie)
            winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
            'Suche nach Rz_Nr in His_Rezeptschritte
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlHisRezeptur, RezeptNummer, Variante, AendIndex)
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
                    _SQLRezeptSchritt.Sollwert = Value
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
                'zählt NICHT zum Rezeptgesamtgewicht
                Case "KA_zaehlt_zu_RZ_Gesamtmenge", "H_KA_zaehlt_zu_RZ_Gesamtmenge"
                    _SQLRezeptSchritt.ZaehltNichtZumRezeptGewicht = wb_sql_Functions.MySQLBoolean(Value)
                    'Preis
                Case "KA_Preis", "H_RS_Preis"
                    _SQLRezeptSchritt.PreisProKg = wb_Functions.StrToDouble(Value)
                'RezeptNr (Rezept im Rezept)
                Case "KA_RZ_Nr", "H_KA_RZ_Nr"
                    _SQLRezeptSchritt.RezeptNr = Value

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
        sqlData = "RZ_Nr_AlNum = '" & _RezeptNummer & "', RZ_Bezeichnung = '" & _RezeptBezeichnung & "', RZ_Gewicht = '" & wb_Functions.FormatStr(RezeptGewicht, 3) & "', " &
                  "RZ_Charge_Opt = '" & wb_Functions.FormatStr(TeigChargen.OptCharge.MengeInkg, 3) & "', " &
                  "RZ_Charge_Min = '" & wb_Functions.FormatStr(TeigChargen.MinCharge.MengeInkg, 3) & "', " &
                  "RZ_Charge_Max = '" & wb_Functions.FormatStr(TeigChargen.MaxCharge.MengeInkg, 3) & "', " &
                  "RZ_Kommentar = '" & _RezeptKommentar & "', RZ_Aenderung_Datum = '" & wb_sql_Functions.MySQLdatetime(_AenderungDatum) & "', " &
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

