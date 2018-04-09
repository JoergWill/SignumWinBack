Imports MySql.Data.MySqlClient
Imports WinBack

Public Class ob_ProduzierteWare
    Private _FilialNummer As Integer = wb_Global.UNDEFINED
    Private _Linie As Integer = wb_Global.UNDEFINED
    Private _ProduktionsDatum As DateTime = Now
    Private _SatzTyp As wb_Global.obSatzTyp = wb_Global.obSatzTyp.Rohstoff
    Private _Typ As wb_Global.wbSatzTyp = wb_Global.wbSatzTyp.Artikel
    Private _ChargeNr As String = "UNDEF"
    Private _TWNr As Long = wb_Global.UNDEFINED
    Private _ArtikelNr As String = ""
    Private _Type As wb_Global.KomponTypen
    Private _ParamNr As Integer
    Private _Einheit As Integer = wb_Global.UNDEFINED
    Private _Unit As Integer = wb_Global.obEinheitKilogramm
    Private _Color As String = "0"
    Private _Size As String = "NULL"
    Private _Menge As Double = 0.0
    Private _ChargenNummer As String = ""
    Private _Haltbarkeit As DateTime = Now

    Public ReadOnly Property sFilialNummer As String
        Get
            Return _FilialNummer.ToString
        End Get
    End Property

    Public Property ProduktionsDatum As Date
        Get
            Return _ProduktionsDatum
        End Get
        Set(value As Date)
            _ProduktionsDatum = value
        End Set
    End Property

    Public ReadOnly Property sProduktionsDatum As String
        Get
            Return "10102017"
            Return _ProduktionsDatum.ToShortDateString
        End Get
    End Property

    Public Property SatzTyp As wb_Global.obSatzTyp
        Get
            Return _SatzTyp
        End Get
        Set(value As wb_Global.obSatzTyp)
            _SatzTyp = value
        End Set
    End Property

    Public ReadOnly Property sSatzTyp As String
        Get
            Select Case _SatzTyp
                Case wb_Global.obSatzTyp.ProduzierterArtikel
                    Return ""
                Case wb_Global.obSatzTyp.Rohstoff
                    Return "V"
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public Property ArtikelNr As String
        Get
            Return "1100"
            Return _ArtikelNr
        End Get
        Set(value As String)
            _ArtikelNr = value
        End Set
    End Property

    Public ReadOnly Property Unit As String
        Get
            Return _Unit.ToString
        End Get
    End Property

    Public ReadOnly Property Color As String
        Get
            Return _Color.ToString
        End Get
    End Property

    Public ReadOnly Property Size As String
        Get
            Return _Size
        End Get
    End Property

    Public Property Menge As Double
        Get
            Return _Menge
        End Get
        Set(value As Double)
            _Menge = value
        End Set
    End Property

    Public Property ChargenNummer As String
        Get
            Return _ChargenNummer
        End Get
        Set(value As String)
            _ChargenNummer = value
        End Set
    End Property

    Public Property Haltbarkeit As Date
        Get
            Return _Haltbarkeit
        End Get
        Set(value As Date)
            _Haltbarkeit = value
        End Set
    End Property

    Public ReadOnly Property sHaltbarkeit As String
        Get
            Return "10102017"
            Return _Haltbarkeit.ToShortDateString
        End Get
    End Property

    Public Property Linie As Integer
        Get
            Return _Linie
        End Get
        Set(value As Integer)
            _Linie = value
            _FilialNummer = wb_Linien_Global.GetFiliale(_Linie)
        End Set
    End Property

    Public Property Type As wb_Global.KomponTypen
        Get
            Return _Type
        End Get
        Set(value As wb_Global.KomponTypen)
            _Type = value
        End Set
    End Property

    Public Property ParamNr As Integer
        Get
            Return _ParamNr
        End Get
        Set(value As Integer)
            _ParamNr = value
        End Set
    End Property

    Public Property Einheit As Integer
        Get
            Return _Einheit
        End Get
        Set(value As Integer)
            _Einheit = value
            _Unit = wb_Einheiten_Global.GetobEinheitNr(_Einheit)
        End Set
    End Property

    Public Property TWNr As Long
        Get
            Return _TWNr
        End Get
        Set(value As Long)
            _TWNr = value
        End Set
    End Property

    Public Property Typ As wb_Global.wbSatzTyp
        Get
            Return _Typ
        End Get
        Set(value As wb_Global.wbSatzTyp)
            _Typ = value
        End Set
    End Property

    ''' <summary>
    ''' Konstruktor übergibt die Chargen-Nummer des vorhergehenden Datensatzes. Ist die Chargen-Nummer identisch, ist der 
    ''' Satztyp 'V' also eine Rohstoff-Zeile (Verbrauchsdaten)
    ''' Ist die Chargen-Nummer geändert, ist der Satztyp NULL (Produzierter Artikel)
    ''' 
    ''' </summary>
    ''' <param name="ChargeNr"></param>
    Public Sub New(ChargeNr As String)
        'Chargen-Nummer des letzten Datensatzes
        _ChargeNr = ChargeNr
    End Sub

    ''' <summary>
    ''' Einlesen aller Datenfelder aus der Datenbank wbdaten in ob_ProduzierteWare
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns></returns>
    Public Function MySQLdbRead_Chargen(ByRef sqlReader As MySqlDataReader) As Boolean

        'Chargendaten - Anzahl der Felder im DataSet
        For i = 0 To sqlReader.FieldCount - 1
            MySQLdbRead_Fields(sqlReader.GetName(i), sqlReader.GetValue(i))
        Next

        Return True
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
        'Debug.Print("Feld/Value " & Name & "/" & Value.ToString)

        'Feldname aus der Datenbank
        Try
            Select Case Name

                'wenn sich die Chargen-Nummer nicht geändert hat ist der
                'Satztyp ein Rohstoff-Verbrauch
                Case "B_ARZ_Charge_Nr"
                    If _ChargeNr = Value.ToString Then
                        SatzTyp = wb_Global.obSatzTyp.Rohstoff
                    Else
                        SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel
                        ChargenNummer = Value.ToString
                    End If
                'Rohstoff-Chargen-Nummer
                Case "B_ARS_BF_Charge"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        ChargenNummer = Value.ToString
                    End If
                'Tageswechsel-Nummer
                Case "B_ARZ_TW_Nr"
                    TWNr = wb_Functions.StrToInt(Value)

                'Datensatz Chargen-Type (Rezept/Artikel)
                Case "B_ARZ_Typ"
                    Typ = wb_Functions.IntToProduktionsTyp(wb_Functions.StrToInt(Value))

                'Einheit (berechnet OrgaBack-Einheiten-Index)
                Case "B_KT_EinheitIndex"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        Einheit = wb_Functions.StrToInt(Value)
                    End If

                'Produktions-Linie (berechnet Produktions-Filiale)
                Case "Linie"
                    Linie = wb_Functions.StrToInt(Value)

                'Artikel-Nummer (Produzierter Artikel)
                Case "B_ARZ_KA_NrAlNum"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        ArtikelNr = Value
                    End If
                'Rohstoff-Nummer
                Case "B_KO_Nr_AlNum"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        ArtikelNr = Value
                    End If

                'Sollwert Parameter-Nummer
                Case "B_ARS_ParamNr"
                    ParamNr = wb_Functions.StrToInt(Value)
                'Komponenten-Type
                Case "B_KT_Typ_Nr"
                    Type = wb_Functions.IntToKomponType(wb_Functions.StrToInt(Value))

                'Sollmenge produzierter Artikel in Stk (Aufruf über Artikel in WinBack)
                Case "B_ARZ_Sollmenge_stueck"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel And Typ = wb_Global.wbSatzTyp.Artikel Then
                        Menge = wb_Functions.StrToDouble(Value)
                        'Einheit = wb_Global.wbEinheitStk
                    End If
                'Sollmenge produzierter Artikel in Stk (Aufruf über Rezept in WinBack)
                Case "B_ARZ_Sollmenge_kg"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel And Typ = wb_Global.wbSatzTyp.Rezept Then
                        Menge = wb_Functions.StrToDouble(Value)
                        'Einheit = wb_Global.wbEinheitKilogramm
                    End If
                'Einheit (Satztyp Produzierter Artikel)
                Case "B_ARZ_Art_Einheit"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        Einheit = wb_Functions.StrToInt(Value)
                    End If
                'Rohstoff Istwert Verwiegung (Verbrauch)
                Case "B_ARS_Istwert"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        Menge = wb_Functions.StrToDouble(Value)
                    End If

                'Produktions-Datum
                Case "B_ARZ_Erststart"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        ProduktionsDatum = Value
                    End If
                Case "B_ARS_Gestartet"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        ProduktionsDatum = Value
                    End If

                Case Else
                    Debug.Print("Field-Name " & Name & " wird nicht ausgewertet")
            End Select
        Catch ex As Exception
        End Try
        Return True
    End Function


End Class
