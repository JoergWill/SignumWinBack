Imports MySql.Data.MySqlClient

Public Class ob_ProduzierteWare
    Private _FilialNummer As Integer = wb_Global.UNDEFINED
    Private _ProduktionsDatum As DateTime = Now
    Private _SatzTyp As wb_Global.obSatzTyp = wb_Global.obSatzTyp.Rohstoff
    Private _ChargeNr As String = "UNDEF"
    Private _ArtikelNr As String = ""
    Private _Type As wb_Global.KomponTypen
    Private _ParamNr As Integer
    Private _Unit As Integer = wb_Global.EinheitKilogramm
    Private _Color As Integer = 0
    Private _Size As Integer = vbNull
    Private _Menge As Double = 0
    Private _ChargenNummer As String = ""
    Private _Haltbarkeit As DateTime = Now

    Public Property FilialNummer As Integer
        Get
            Return _FilialNummer
        End Get
        Set(value As Integer)
            _FilialNummer = value
        End Set
    End Property

    Public Property ProduktionsDatum As Date
        Get
            Return _ProduktionsDatum
        End Get
        Set(value As Date)
            _ProduktionsDatum = value
        End Set
    End Property

    Public Property SatzTyp As wb_Global.obSatzTyp
        Get
            Return _SatzTyp
        End Get
        Set(value As wb_Global.obSatzTyp)
            _SatzTyp = value
        End Set
    End Property

    Public Property ArtikelNr As String
        Get
            Return _ArtikelNr
        End Get
        Set(value As String)
            _ArtikelNr = value
        End Set
    End Property

    Public Property Unit As Integer
        Get
            Return _Unit
        End Get
        Set(value As Integer)
            _Unit = value
        End Set
    End Property

    Public Property Color As Integer
        Get
            Return _Color
        End Get
        Set(value As Integer)
            _Color = value
        End Set
    End Property

    Public Property Size As Integer
        Get
            Return _Size
        End Get
        Set(value As Integer)
            _Size = value
        End Set
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

                Case "B_ARS_BF_Charge"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        ChargenNummer = Value
                    End If

                Case "B_ARZ_TW_Nr"
                Case "B_ARZ_Status"
                Case "Linie"
                    FilialNummer = wb_Functions.StrToInt(Value)

                Case "B_ARZ_KA_NrAlNum"
                    If SatzTyp = wb_Global.obSatzTyp.ProduzierterArtikel Then
                        ArtikelNr = Value
                    End If

                Case "B_KO_Nr_AlNum"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        ArtikelNr = Value
                    End If

                Case "B_ARZ_Art_Einheit"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        Unit = wb_Functions.StrToInt(Value)
                    End If

                Case "B_ARZ_Sollmenge_kg"
                Case "B_ARZ_Sollmenge_stueck"
                Case "B_ARS_Istwert"
                    If SatzTyp = wb_Global.obSatzTyp.Rohstoff Then
                        Menge = wb_Functions.StrToDouble(Value)
                    End If

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
