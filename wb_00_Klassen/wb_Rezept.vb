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
    Private _RootRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private SQLRezeptSchritt As New wb_Rezeptschritt(Nothing, "")
    Private RezeptSchritt As wb_Rezeptschritt

    Private _RezeptGewicht As Double

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
            Return _RootRezeptSchritt.Gewicht
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' 'TODO Rezeptnummer und Variante beim Konstruktor übergeben
    Public Sub New()
        'alle Rezeptschritte aus der Datenbank einlesen
        MySQLdbRead(wb_Rezept_Shared.aktRzNr, 1)
        'Rezeptgesamtgewicht berechnen und an alle Rezeptschritte propagieren#
        _RootRezeptSchritt.RezGewicht = RezeptGewicht
    End Sub

    ''' <summary>
    ''' Liest alle Datenfelder zu der angegebenen Rezept-Nummer aus der WinBack-Datenbank. 
    ''' 
    ''' Gibt True zurück, wenn der Datensatz gefunden wurde.
    ''' </summary>
    Private Function MySQLdbRead(InterneKomponentenNummer As Integer, Variante As Integer) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_Konfig.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach Rz_Nr
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRezeptur, InterneKomponentenNummer, Variante)

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
            If RezeptSchritt IsNot Nothing And SQLRezeptSchritt IsNot Nothing Then
                Root = GetParentNode(Root, SQLRezeptSchritt.Type, SQLRezeptSchritt.ParamNr, RezeptSchritt.Type, RezeptSchritt.ParamNr)
            End If

            'neuen Rezeptschritt anlegen
            RezeptSchritt = New wb_Rezeptschritt(Root, SQLRezeptSchritt.Bezeichnung)
            'Daten aus MySQL in Rezeptschritt kopieren
            RezeptSchritt.CopyFrom(SQLRezeptSchritt)

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
            PST_Node = RezeptSchritt
            AKT_Node = RezeptSchritt
            Return RezeptSchritt
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
            AKT_Node = RezeptSchritt
            Return RezeptSchritt
            Exit Function
        End If

        'letzter Rezeptschritt war Wasser
        If TypeLast = wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE Then
            If TypeNow <> wb_Global.KomponTypen.KO_TYPE_WASSERKOMPONENTE Then
                Return AKT_Node
                Exit Function
            Else
                If ParamNrLast = 1 Then
                    Return RezeptSchritt
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
                Return RezeptSchritt
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

    Private Function MySQLdbRead_RzSchritte(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Testausgabe
        Debug.Print("Feld/Wert " & Name & " " & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                'Nummer(alpha)
                Case "KO_Nr_AlNum"
                    SQLRezeptSchritt.Nummer = Value

                'Schritt-Nummer
                Case "RS_Schritt_Nr"
                    SQLRezeptSchritt.SchrittNr = Value

                'Parameter-Nummer
                Case "RS_ParamNr"
                    SQLRezeptSchritt.ParamNr = Value

                'Komponenten-Type
                Case "KO_Type"
                    SQLRezeptSchritt.Type = wb_Functions.IntToKomponType(Value)

                'Bezeichnung
                Case "KO_Bezeichnung"
                    SQLRezeptSchritt.Bezeichnung = Value

                'Sollwert
                Case "RS_Wert"
                    SQLRezeptSchritt.Sollwert = Value

                'Einheit
                Case "E_Einheit"
                    SQLRezeptSchritt.Einheit = Value

                'TA
                Case "E_Einheit"
                    SQLRezeptSchritt.TA = Value

                'Preis
                Case "KA_Preis"
                    SQLRezeptSchritt.PreisProKg = wb_Functions.StrToDouble(Value)

                'RezeptNr (Rezept im Rezept)
                Case "KA_RZ_Nr"
                    SQLRezeptSchritt.RezeptNr = Value

            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function

End Class
