Imports MySql.Data.MySqlClient
Imports WinBack
Imports WinBack.wb_Functions
Imports WinBack.wb_Global

Public Class wb_Komponente
    Inherits wb_ChangeLog

    Private KO_Nr As Integer
    Private KO_Type As KomponTypen
    Private KA_Art As String
    Private KO_Nr_AlNum As String
    Private KO_Bezeichnung As String
    Private KO_Kommentar As String
    Private KA_Kurzname As String
    Private LF_Lieferant As String
    Private KO_Backverlust As Double
    Private KA_ProdVorlauf As Integer
    Private KO_IdxCloud As String
    Private KA_Rz_Nr As Integer
    Private KA_Lagerort As String
    Private KA_Preis As String
    Private KA_Grp1 As Integer
    Private KA_Grp2 As Integer
    Private KO_DeklBezeichnungExtern As New wb_Hinweise(Hinweise.DeklBezRohstoff)
    Private KO_DeklBezeichnungIntern As New wb_Hinweise(Hinweise.DeklBezRohstoffIntern)

    Private _DataHasChanged As Boolean = False
    Private _LastErrorText As String
    Private _RezeptNummer As String = Nothing
    Private _RezeptName As String = Nothing
    Private _LinienGruppe As Integer = wb_Global.UNDEFINED
    Private _ArtikelLinienGruppe As Integer = wb_Global.UNDEFINED

    Private _RootParameter As New wb_KomponParam(Nothing, 0, 0, "")
    Private _Parameter As wb_KomponParam

    Public ktTypXXX As New wb_KomponParamXXX
    Public ktTyp200 As New wb_KomponParam200
    Public ktTyp201 As New wb_KomponParam201
    Public ktTyp202 As New wb_KomponParam202
    Public ktTyp210 As New wb_KomponParam210
    Public ktTyp220 As New wb_KomponParam220
    Public ktTyp300 As New wb_KomponParam300
    Public ktTyp301 As New wb_KomponParam301

    Public NwtUpdate As New wb_Hinweise(Hinweise.NaehrwertUpdate)
    Public ArtikelChargen As New wb_MinMaxOptCharge
    Public TeigChargen As New wb_MinMaxOptCharge

    Private Shared _ProduktionsStufe As wb_Komponente
    Private Shared _Kessel As wb_Komponente
    Private Shared _TextKomponente As wb_Komponente

    ''' <summary>
    ''' Eine der Komponenten-Eigenschaften wurde geändert
    ''' </summary>
    ''' <returns>
    ''' True  - Eingenschaften sind geändert worden, der Datensatz muss gespeichert werden
    ''' False - keine Änderung, kein Speichern notwendig
    '''     </returns>
    Public ReadOnly Property Changed As Boolean
        Get
            Return _DataHasChanged
        End Get
    End Property

    Public Sub New()
        AddHandler wb_Rohstoffe_Shared.eParam_Changed, AddressOf SaveParameterArray
    End Sub

    ''' <summary>
    ''' Setzt alle Variablen wieder auf Null,Nothing oder Undefined.
    ''' Wird aufgerufen, wenn eine neue(andere) Komponente geladen werden soll
    ''' </summary>
    Public Sub Invalid()
        KO_Nr = wb_Global.UNDEFINED
        KO_Type = wb_Global.KomponTypen.KO_TYPE_UNDEFINED
        KA_Art = wb_Global.UNDEFINED
        KO_Nr_AlNum = "-1"
        KO_Bezeichnung = ""
        KO_Kommentar = ""
        KA_Kurzname = ""

        KA_Rz_Nr = wb_Global.UNDEFINED
        _RezeptNummer = Nothing
        _RezeptName = Nothing
        _LinienGruppe = wb_Global.UNDEFINED
        _ArtikelLinienGruppe = wb_Global.UNDEFINED

        KO_DeklBezeichnungExtern.Invalid()
        KO_DeklBezeichnungIntern.Invalid()

        ArtikelChargen.Invalidate()
        TeigChargen.Invalidate()

        ktTypXXX = New wb_KomponParamXXX
        ktTyp200 = New wb_KomponParam200
        ktTyp201 = New wb_KomponParam201
        ktTyp202 = New wb_KomponParam202
        ktTyp210 = New wb_KomponParam210
        ktTyp220 = New wb_KomponParam220
        ktTyp300 = New wb_KomponParam300
        ktTyp301 = New wb_KomponParam301

        ktTyp301.IsCalculated = False
    End Sub

    Public Property Nr As Integer
        Set(value As Integer)
            KO_Nr = value
            'Komponenten-Nummer an Hinweise.NaehrwertUpdate weitergeben
            NwtUpdate.KompNr = value
            _DataHasChanged = True
        End Set
        Get
            Return KO_Nr
        End Get
    End Property

    Public ReadOnly Property Type As KomponTypen
        Get
            Return KO_Type
        End Get
    End Property

    ''' <summary>
    ''' Rohstoff/Artikel-Nummer (alpha-numerisch)
    ''' </summary>
    ''' <returns></returns>
    Public Property Nummer As String
        Set(value As String)
            'Änderungen loggen
            KO_Nr_AlNum = ChangeLogAdd(LogType.Prm, Parameter.Tx_AlNum, KO_Nr_AlNum, value)
            _DataHasChanged = True
        End Set
        Get
            Return KO_Nr_AlNum
        End Get
    End Property

    Public Property Bezeichnung As String
        Set(value As String)
            'Änderungen loggen
            KO_Bezeichnung = ChangeLogAdd(LogType.Prm, Parameter.Tx_Bezeichnung, KO_Bezeichnung, wb_Functions.XRemoveSonderZeichen(value))
            _DataHasChanged = True
        End Set
        Get
            Return KO_Bezeichnung
        End Get
    End Property

    Public Property Kommentar As String
        Set(value As String)
            'Änderungen loggen
            KO_Kommentar = ChangeLogAdd(LogType.Prm, Parameter.Tx_Kommentar, KO_Kommentar, wb_Functions.XRemoveSonderZeichen(value))
            _DataHasChanged = True
        End Set
        Get
            Return KO_Kommentar
        End Get
    End Property

    Public Property Kurzname As String
        Set(value As String)
            'Änderungen loggen
            KA_Kurzname = ChangeLogAdd(LogType.Prm, Parameter.Tx_Kommentar, KA_Kurzname, wb_Functions.XRemoveSonderZeichen(value))
        End Set
        Get
            Return KA_Kurzname
        End Get
    End Property

    Public Property Preis As String
        Set(value As String)
            KA_Preis = value
            _DataHasChanged = True
        End Set
        Get
            'Zahlenwerte aus der Datenbank immer inm Format de-DE
            Return FormatStr(KA_Preis, 3, 4, "de-DE")
        End Get
    End Property

    Public ReadOnly Property Gruppe1 As Integer
        Get
            Return KA_Grp1
        End Get
    End Property

    Public ReadOnly Property Gruppe2 As Integer
        Get
            Return KA_Grp2
        End Get
    End Property

    ''' <summary>
    ''' Erster (unsichtbarer) Parameter-Knoten (Root-Node)
    ''' Die Child-Nodes enthalten eine Liste aller Parameter sortiert nach Type und Parameter-Nummer
    ''' 
    '''     Tabelle KomponTypen
    '''     Type XXX    -  Verwiege-Parameter(Produktion) 
    '''     
    '''     Tabelle RohParams
    '''     Type 200    -  Produktinformation
    '''     Type 201    -  Verarbeitungshinweise
    '''     Type 202    -  Kalkulation
    '''     Type 210    -  Froster
    '''     Type 220    -  Gare
    '''     Type 300    -  Produktion
    '''     Type 301    -  Nährwerte und Allergene bezogen aus 100gr
    '''     Type 401    -  Nährwerte bezogen auf Stk
    ''' </summary>
    ''' <returns>wb_KomponParam - Root-Parameter der Komponente</returns>
    Public ReadOnly Property RootParameter As wb_KomponParam
        Get
            'Parameter-Liste aufbauen
            BuildParameterArray()
            Return _RootParameter
        End Get
    End Property

    ''' <summary>
    ''' Parameter-Array für die Anzeige der Rohstoff/Artikel-Parameter im VirtualTree aufbauen
    ''' Ausgehend vom Root-Parameter (unsichtbar) wird ein Parameter-Baum abhängig von der Komponenten-Type 
    ''' aufgebaut.
    ''' 
    ''' Die Daten stehen im Array ktxxx aus winback.KomponParams
    ''' Die erweiterten Daten kommen aus den einzelnen Parameter-Arrays. (kt200..kt301) aus winback.RohParams
    ''' 
    ''' Die Datenhülle wird in wb_KomponParam..._Global aus winback.KomponTypen einmalig beim ersten Aufruf
    ''' gebildet und als Array gespeichert.
    ''' </summary>
    Private Sub BuildParameterArray()
        'Root-Knoten neu initialisieren
        _RootParameter = New wb_KomponParam(Nothing, 0, 0, "")
        'Komponenten-Type WinBack
        Dim t As Integer = wb_Functions.KomponTypeToInt(KO_Type)

        'abhängig von der Komponenten-Type werden weitere Parameter angezeigt
        Select Case KO_Type
            Case wb_Global.KomponTypen.KO_TYPE_ARTIKEL
                'abhängig von der Komponenten-Type werden die einzelnen Parameter durchlaufen
                AddParamNodes("Artikel", t)
                'Parameter Verarbeitungshinweise
                AddParamNodes("", wb_Global.ktParam.kt201)
                'Parameter Kalkulation (nicht OrgaBack)
                If wb_GlobalSettings.pVariante <> wb_Global.ProgVariante.OrgaBack Then
                    AddParamNodes("", wb_Global.ktParam.kt202)
                End If
                'Parameter Nährwerte
                AddParamNodes("", wb_Global.ktParam.kt301)

            Case KomponTypen.KO_TYPE_AUTOKOMPONENTE, KomponTypen.KO_TYPE_EISKOMPONENTE, KomponTypen.KO_TYPE_HANDKOMPONENTE, KomponTypen.KO_TYPE_WASSERKOMPONENTE
                'abhängig von der Komponenten-Type werden die einzelnen Parameter durchlaufen
                AddParamNodes("Produktion", t)
                'Parameter Nährwerte (nur wenn kein Rezept verknüpft)
                'TODO Nährwerte des unterlagerten Rezeptes anzeigen(muss vorher berechnet werden)
                If RzNr <= 0 Then
                    AddParamNodes("", wb_Global.ktParam.kt301)
                End If

            Case KomponTypen.KO_TYPE_SAUER_MEHL, KomponTypen.KO_TYPE_SAUER_WASSER
                'abhängig von der Komponenten-Type werden die einzelnen Parameter durchlaufen
                AddParamNodes("Sauerteig", t)
                'Parameter Nährwerte
                AddParamNodes("", wb_Global.ktParam.kt301)

            Case KomponTypen.KO_TYPE_KNETER
                If ktTypXXX.Wert(T118_KneterParamNr) = "6" Then
                    'Sonderfall Parameter Teigruhe(Kneter)
                    AddParamNodes("Teigruhe", wb_Functions.KomponTypeToInt(KomponTypen.KO_TYPE_KNETER_TEIGRUHE))
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Fügt eine neue Gruppe von Parameter-Knoten an den Root-Parameter an. Der erste Child-Knoten bildet die Überschrift über die 
    ''' folgenden Parameter-Gruppen.
    ''' </summary>
    ''' <param name="RootName"></param>
    ''' <param name="t"></param>
    Private Sub AddParamNodes(RootName As String, t As Integer)
        'Sub-Knoten - Überschrift
        If wb_KomponParam_Global.IsValidParameter(t, 0) Then
            RootName = wb_KomponParam_Global.ktXXXParam(t, 0).Bezeichnung
        End If
        'Sub-Knoten für alle folgenden Parameter-Schritte
        Dim Root As New wb_KomponParam(_RootParameter, t, 0, RootName)

        'Liste aller Parameter zum Sub-Knoten
        For p = 1 To wb_KomponParam_Global.MaxParam(t)
            If wb_KomponParam_Global.IsValidParameter(t, p) Then
                'neuen Parameter-Datensatz anlegen
                _Parameter = New wb_KomponParam(Root, t, p, wb_KomponParam_Global.ktXXXParam(t, p).Bezeichnung)

                'Sollwert-Daten aus den Komponenten-Parametern
                Select Case t
                    Case < wb_Global.ktParam.kt200
                        _Parameter.Wert = ktTypXXX.Wert(p)
                    Case wb_Global.ktParam.kt301
                        _Parameter.Wert = ktTyp301.Wert(p)
                End Select
            End If
        Next
    End Sub

    ''' <summary>
    ''' Sichert alle geänderten Parameter ausgehen vom RootParameter.
    ''' Abhängig vom KomponParam-Typ wird die entsprechende Funktion zum Sichern der Daten in
    ''' der Klasse ausgerufen.
    ''' Die Parameter-Werte werden vorher von VirtualTree-Knoten in die KomponParam...-Daten übertragen
    ''' </summary>
    Public Sub SaveParameterArray()
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As wb_Sql = Nothing
        If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
            'Datenbank-Verbindung öffnen - MsSQL
            OrgasoftMain = New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        End If

        'Ausgehend vom Root-Knoten werden alle Child-Knoten durchlaufen
        For Each p As wb_KomponParam In _RootParameter.ChildSteps
            'alle ChildKonoten
            For Each x As wb_KomponParam In p.ChildSteps
                'Parameter wurde geändert
                If x.Changed Then
                    'abhängig vom KomponentenParameter-Typ
                    Select Case p.TypNr

                        'Allgemeine Parameter
                        Case < wb_Global.ktParam.kt200
                            'TODO ktTYpXXX mysqldbsave !
                            Debug.Print("SaveParameterArray xxx " & x.Bezeichnung & " ParamNr " & x.ParamNr)

                        'Allergene und Nährwerte
                        Case wb_Global.ktParam.kt301
                            'Wert in kt301-Array übertragen
                            ktTyp301.Wert(x.ParamNr) = x.Wert
                            'Update einzelner Datensatz in winback-Datenbank
                            ktTyp301.MySQLdbUpdate(Nr, x.ParamNr, winback)
                            'Update Parametersatz in OrgaBack
                            If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
                                ktTyp301.MsSQLdbUpdate(KO_Nr_AlNum, x.ParamNr, wb_Einheiten_Global.GetobEinheitNr(Einheit), OrgasoftMain)
                            End If
                            'Flag zurücksetzen
                            x.Changed = False

                        Case Else
                            'Fehler - Parameter-Type nicht im Programm vorgesehen
                            Debug.Print("SaveParameterArray UNDEF " & x.TypNr & "/" & x.Bezeichnung)

                    End Select
                End If
            Next
        Next
        'Datenbank-Verbindung wieder schliessen
        winback.Close()
        'Datenbank-Verbindung wieder schliessen
        OrgasoftMain.Close()
    End Sub

    Friend Sub LoadData(dataGridView As wb_DataGridView)
        'eventuell vorhandene Daten löschen
        Invalid()
        _DataHasChanged = False
        'Daten aus DataGridView laden
        KO_Nr = CInt(dataGridView.Field("KO_Nr"))
        KO_Type = wb_Functions.IntToKomponType(dataGridView.Field("KO_Type"))
        KO_Nr_AlNum = dataGridView.Field("KO_Nr_AlNum")
        KO_Bezeichnung = dataGridView.Field("KO_Bezeichnung")
        KO_Kommentar = dataGridView.Field("KO_Kommentar")
        KA_Preis = dataGridView.Field("KA_Preis")
        KO_IdxCloud = dataGridView.Field("KA_Matchcode")
        KA_Rz_Nr = dataGridView.Field("KA_Rz_Nr")
        KA_Grp1 = wb_Functions.StrToInt(dataGridView.Field("KA_Grp1"))
        KA_Grp2 = wb_Functions.StrToInt(dataGridView.Field("KA_Grp2"))
    End Sub

    Friend Function SaveData(dataGridView As wb_DataGridView) As Boolean
        'Rohstoff-Detaildaten wurden geändert
        If _DataHasChanged Then
            dataGridView.Field("KO_Nr_AlNum") = KO_Nr_AlNum
            dataGridView.Field("KO_Bezeichnung") = KO_Bezeichnung
            dataGridView.Field("KO_Kommentar") = KO_Kommentar
            dataGridView.Field("KA_Preis") = KA_Preis
            dataGridView.Field("KA_Matchcode") = KO_IdxCloud
            dataGridView.Field("KA_Rz_Nr") = KA_Rz_Nr
            _DataHasChanged = False
            Return True
        Else
            Return False
        End If
    End Function

    Public Property RzNr As Integer
        Get
            Return KA_Rz_Nr
        End Get
        Set(value As Integer)
            KA_Rz_Nr = value
            'KA_Art setzen (Für Artikel immmer gleich Eins)
            If KA_Rz_Nr > 0 Or KO_Type = KomponTypen.KO_TYPE_ARTIKEL Then
                KA_Art = "1"
            Else
                KA_Art = "0"
            End If
        End Set
    End Property

    Public Property RezeptNummer As String
        Get
            If _RezeptNummer Is Nothing Then
                GetProduktionsDaten()
            End If
            Return _RezeptNummer
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property RezeptName As String
        Get
            If _RezeptName Is Nothing Then
                GetProduktionsDaten()
            End If
            Return _RezeptName
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property LinienGruppe As Integer
        Get
            If _LinienGruppe = wb_Global.UNDEFINED Then
                GetProduktionsDaten()
            End If
            Return _LinienGruppe
        End Get
        Set(value As Integer)
            _LinienGruppe = value
        End Set
    End Property

    Public Property iArtikelLinienGruppe As Integer
        Get
            If _ArtikelLinienGruppe = wb_Global.UNDEFINED Then
                GetProduktionsDaten()
            End If
            'prüfen ob die Artikelgruppe gültig ist
            If _ArtikelLinienGruppe < wb_Global.OffsetBackorte Then
                _ArtikelLinienGruppe = wb_Global.OffsetBackorte
            End If
            Return _ArtikelLinienGruppe
        End Get
        Set(value As Integer)
            _ArtikelLinienGruppe = value
            'TODO wenn ein Rezept in Param6 angegeben ist, muss die Rezept-Liniengruppe angepasst werden
            ktTyp300.Liniengruppe = value
        End Set
    End Property

    Public Property sArtikeLinienGruppe As String
        Get
            Dim sValue As String = "0000" & iArtikelLinienGruppe.ToString - wb_Global.OffsetBackorte
            Return Right(sValue, 4)
        End Get
        Set(value As String)
            Dim iValue As Integer = wb_Functions.StrToInt(value) + wb_Global.OffsetBackorte
            If iValue <> 0 Then
                _ArtikelLinienGruppe = iValue
            End If
        End Set
    End Property

    Public Property Lieferant As String
        Set(value As String)
            'Änderungen loggen
            LF_Lieferant = ChangeLogAdd(LogType.Prm, Parameter.Tx_Lieferant, LF_Lieferant, value)
        End Set
        Get
            Return LF_Lieferant
        End Get
    End Property

    Public ReadOnly Property Einheit As Integer
        Get
            Return wb_Einheiten_Global.getEinheitFromKompType(Type)
        End Get
    End Property

    Public Property TimeStamp As Date
    Public Property BestellNummer As String
    Public Property ZutatenListe As String
    Public Property Mehlzusammensetzung As String

    Public Property MatchCode As String
        Get
            Return KO_IdxCloud
        End Get
        Set(value As String)
            KO_IdxCloud = value
        End Set
    End Property

    ''' <summary>
    ''' Daten für die Produktion dieser Komponente ermitteln. 
    '''     Teig-Rezept aus Rezept-im-Rezept-Struktur
    '''     Liniengruppe aus RohParams(5)
    '''     Artikel-Rezept aus RohParams(6)
    ''' </summary>
    Private Sub GetProduktionsDaten()
        If RzNr > 0 Then
            'Teig-Rezeptur
            Dim Rezept As New wb_Rezept(RzNr)
            _RezeptNummer = Rezept.RezeptNummer
            _RezeptName = Rezept.RezeptBezeichnung
            _LinienGruppe = Rezept.LinienGruppe
            'Chargengrößen aus Rezept
            TeigChargen.CopyFrom(Rezept.TeigChargen)
            ArtikelChargen.TeigGewicht = Rezept.RezeptGewicht
            Rezept.Dispose()
            Rezept = Nothing
        Else
            'normale Komponente ohne Produktion
            _RezeptName = ""
            _RezeptNummer = ""
            _LinienGruppe = wb_Global.UNDEFINED
        End If

        'Artikel-Typ = 1 für Auto/Handkomponenten mit anhängender Rezeptur
        If KO_Type = KomponTypen.KO_TYPE_ARTIKEL Or RzNr > 0 Then
            KA_Art = 1
        Else
            KA_Art = 0
        End If

        'Aufarbeitung Artikel (Artikel-Liniengruppe) aus RohParams.Parameter.5
        'wenn ein Artikel-Rezept in RohParams.Parameter.6 angegeben ist wird die Artikel-Liniengruppe aus der Artikel-Rezeptur bestimmt
        If ktTyp300.Liniengruppe > 0 Then
            'Produktions-Liniengruppe aus RohParams(5)
            _ArtikelLinienGruppe = ktTyp300.Liniengruppe
        End If
        If ktTyp300.RzNr > 0 Then
            'Artikel-Rezeptur
            Dim Rezept As New wb_Rezept(ktTyp300.RzNr)
            _ArtikelLinienGruppe = Rezept.LinienGruppe
            Rezept.Dispose()
            Rezept = Nothing
        End If
    End Sub

    Public Sub SaveProduktionsDaten()
        If RzNr > 0 Then
            'Teig-Rezeptur
            Dim Rezept As New wb_Rezept(RzNr)
            'geänderte Teigchargen aus Komponenten.Teigchargen sichern (in winback.Rezepte)
            Rezept.TeigChargen.CopyFrom(TeigChargen)
            Rezept.LinienGruppe = LinienGruppe
            'Rezeptkopfdaten sichern
            Rezept.MySQLdbWrite_Rezept(True)
            Rezept.Dispose()
            Rezept = Nothing
        End If
    End Sub

    Public Sub ClearReport()
        ChangeLogClear()
        ktTyp301.ClearReport()
    End Sub

    Public Sub SaveReport()
        Dim Ueberschrift As String
        If Type = wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
            Ueberschrift = "Änderungen für Artikel " & Nummer & " " & Bezeichnung & " " & vbNewLine
        Else
            Ueberschrift = "Änderungen für Rohstoff " & Nummer & " " & Bezeichnung & " " & vbNewLine
        End If

        Dim Strich = New String("="c, Len(Ueberschrift)) & vbNewLine
        NwtUpdate.Memo = Ueberschrift & Strich & GetReport()
        NwtUpdate.Write()
    End Sub

    Public ReadOnly Property GetReport(Optional ReportAll As Boolean = vbFalse) As String
        Get
            Return ChangeLogReport(ReportAll) & ktTyp301.GetReport(ReportAll)
        End Get
    End Property

    Public Property Deklaration As String
        Get
            'TODO Hier muss unterschieden werden, welche Deklaration benutzt werden soll (Intern/Extern) - kundenspezifisch
            Deklaration = DeklBezeichungExtern
        End Get
        Set(value As String)
            'TODO Hier muss unterschieden werden, welche Deklaration benutzt werden soll (Intern/Extern) - kundenspezifisch
            DeklBezeichungExtern = value
        End Set
    End Property

    Public Property DeklBezeichungExtern As String
        Get
            'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
            If Not KO_DeklBezeichnungExtern.ReadOK Then
                KO_DeklBezeichnungExtern.Read(KO_Nr)
            End If
            Return KO_DeklBezeichnungExtern.Memo
        End Get
        Set(value As String)
            KO_DeklBezeichnungExtern.Memo = ChangeLogAdd(LogType.Dkl, Parameter.Tx_DeklarationExtern, DeklBezeichungExtern, value)
        End Set
    End Property

    Public Property DeklBezeichungIntern As String
        Get
            'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
            If Not KO_DeklBezeichnungIntern.ReadOK Then
                KO_DeklBezeichnungIntern.Read(KO_Nr)
            End If
            Return KO_DeklBezeichnungIntern.Memo
        End Get
        Set(value As String)
            KO_DeklBezeichnungIntern.Memo = ChangeLogAdd(LogType.Dkl, Parameter.Tx_DeklarationIntern, DeklBezeichungIntern, value)
        End Set
    End Property

    Public ReadOnly Property LastErrorText As String
        Get
            Return _LastErrorText
        End Get
    End Property

    ''' <summary>
    ''' Standard-Komponente für Produktions-Stufen.
    ''' Es wird versucht, die erste Komponente mit der passenden Komponenten-Type aus dem Komponenten-Stamm zu lesen. Wenn keine
    ''' passende Komponente gefunden wurde, wird ein Dummy neu angelegt.
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property ProduktionsStufe As wb_Komponente
        Get
            If _ProduktionsStufe Is Nothing Then
                _ProduktionsStufe = New wb_Komponente
                If Not _ProduktionsStufe.MysqldbRead(wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) Then
                    _ProduktionsStufe.KO_Type = wb_Global.KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
                    _ProduktionsStufe.Bezeichnung = "Produktions-Stufe"
                    _ProduktionsStufe.Nummer = "PST"
                    _ProduktionsStufe.Nr = wb_sql_Functions.getNewKomponNummer()
                End If
            End If
            Return _ProduktionsStufe
        End Get
    End Property

    Public Shared ReadOnly Property Kessel As wb_Komponente
        Get
            _Kessel = New wb_Komponente
            If Not _Kessel.MysqldbRead(wb_Global.KomponTypen.KO_TYPE_KESSEL) Then
                _Kessel.KO_Type = wb_Global.KomponTypen.KO_TYPE_KESSEL
                _Kessel.Bezeichnung = "Kessel"
                _Kessel.Nummer = "KSL"
                _Kessel.Nr = wb_sql_Functions.getNewKomponNummer()
            End If
            Return _Kessel
        End Get
    End Property

    Public Shared ReadOnly Property TextKomponente As wb_Komponente
        Get
            _TextKomponente = New wb_Komponente
            If Not _TextKomponente.MysqldbRead(wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE) Then
                _TextKomponente.KO_Type = wb_Global.KomponTypen.KO_TYPE_TEXTKOMPONENTE
                _TextKomponente.Bezeichnung = "Text"
                _TextKomponente.Nummer = "TXT"
                _TextKomponente.Nr = wb_sql_Functions.getNewKomponNummer()
            End If
            Return _TextKomponente
        End Get
    End Property

    ''' <summary>
    ''' Backverlust in %
    ''' Der Backverlust wird in der Datenbank im Feld (winback.Komponenten.KO_Temp_Korr) mit Faktor 100 als Integer gespeichern.
    ''' </summary>
    ''' <returns></returns>
    Public Property Backverlust As Double
        Get
            Return KO_Backverlust / 100
        End Get
        Set(value As Double)
            KO_Backverlust = value * 100
        End Set
    End Property

    ''' <summary>
    ''' Produktions-Vorlauf in [h].
    ''' Wird für die Produktionsplanung für Rohstoffe mit anhängender Rezeptur verwendet. Der Produktionsvorlauf
    ''' definiert, wie weit im Voraus die Produktion für den Rohstoff gestartet werden muss. (Reifezeiten....)
    ''' 
    ''' Datenfeld winback.Komponenten.KA_Prod_Linie (Unsigned TinyInt)
    ''' </summary>
    ''' <returns></returns>
    Public Property ProdVorlauf As Integer
        Get
            Return KA_ProdVorlauf
        End Get
        Set(value As Integer)
            KA_ProdVorlauf = value
        End Set
    End Property

    ''' <summary>
    ''' Objekt ist ungültig. Vor der nächsten Verwendung muss wieder neu eingelesen werden.
    ''' </summary>
    Public Sub Invalidate()
        _RezeptName = Nothing
        _RezeptNummer = Nothing
    End Sub

    ''' <summary>
    ''' Speichert alle geänderten Komponenten-Daten in der Datenbank
    '''     Stammdaten  (Tabelle Komponenten)
    '''     Teigchargen (Tabelle Rezeptur)
    ''' </summary>
    Public Sub UpdateDB()
        'geänderten Datensatz(Stammdaten) in WinBack-DB schreiben
        MySQLdbUpdate()
        'schreibt auch die Artikel-Chargen-Daten
        ArtikelChargen.HasChanged = False

        'geänderte Komponentendaten(Rezeptur) in WinBack-DB schreiben
        If TeigChargen.HasChanged Then
            SaveProduktionsDaten()
            TeigChargen.HasChanged = False
        End If

        'TODO geänderte Parameter in WinBack-DB schreiben (KomponParams 200)
        'TODO geänderte Parameter in WinBack-DB schreiben (KomponParams 300)
        MySQLdbUpdate_Parameter(wb_Global.ktParam.kt300)
        'TODO geänderte Parameter in WinBack-DB schreiben (KomponParams 301)
        MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
    End Sub

    ''' <summary>
    ''' Prüft ob der Rohstoff/Artikel noch verwendet wird. (Prüfung ob Löschen zulässig ist)
    ''' 
    ''' Abhängig von der Komponenten-Type wird geprüft ob:
    '''     Artikel     -   Verwendung in Arbeits-Rezepte-Tabelle
    '''     Rohstoff    -   Verwendung in Arbeits-Rezepte-Tabelle
    '''                     Verwendung in Rezeptschritte-Tabelle
    ''' </summary>
    ''' <returns>False - Rohstoff/Artikel wird verwendet
    ''' True - Rohstoff/Artikel wird nicht mehr verwendet (kann gelöscht werder)</returns>
    Public Function MySQLdbCanBeDeleted(InterneKomponentenNummer As Integer, Optional KomponentenNummer As String = "") As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach KO_Nr oder KO_AlNum
        If InterneKomponentenNummer > 0 Then
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_KO_Nr, InterneKomponentenNummer)
        Else
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_AlNum, KomponentenNummer)
        End If

        'ersten Datensatz aus Tabelle Komponenten lesen
        If Not winback.sqlSelect(sql) Then
            winback.Close()
            Return True
        Else
            If Not winback.Read Then
                winback.Close()
                Debug.Print("Datensatz nicht gefunden - Löschen freigegeben")
                Return True
            Else
                'Stammdaten - Anzahl der Felder im DataSet
                For i = 0 To winback.MySqlRead.FieldCount - 1
                    MySQLdbRead_StammDaten(winback.MySqlRead.GetName(i), winback.MySqlRead.GetValue(i))
                Next
            End If
        End If

        'Datenbank-Verbindung wieder schliessen
        winback.Close()

        Debug.Print("Anfrage Löschen Komponente " & KO_Nr & "/" & KO_Nr_AlNum)

        'Abhängig von der Komponenten-Type
        Select Case Type

            'Automatik-Rohstoffe dürfen nicht gelöscht werden
            Case KomponTypen.KO_TYPE_AUTOKOMPONENTE, KomponTypen.KO_TYPE_WASSERKOMPONENTE, KomponTypen.KO_TYPE_EISKOMPONENTE
                _LastErrorText = "Rohstoffe, die automatisch dosiert werden, können nicht gelöscht werden !"
                Return False

            'Sauerteig-Rohstoffe dürfen nicht gelöscht werden
            Case KomponTypen.KO_TYPE_SAUER_MEHL, KomponTypen.KO_TYPE_SAUER_WASSER, KomponTypen.KO_TYPE_SAUER_ZUGABE, KomponTypen.KO_TYPE_SAUER_AUTO_ZUGABE
                _LastErrorText = "Sauerteig-Rohstoffe, die automatisch dosiert werden, können nicht gelöscht werden !"
                Return False

            'Verkaufs-Artikel - Verwendung in der Produktion prüfen
            Case KomponTypen.KO_TYPE_ARTIKEL
                If MySQLIsUsedInProduction(KO_Nr) Then
                    _LastErrorText = "Dieser Artikel wird In der Produktion noch verwendet und kann nicht gelöscht werden"
                    Return False
                Else
                    Return True
                End If

            'Rohstoff - Verwendung in der Produktion und in Rezepten prüfen
            Case KomponTypen.KO_TYPE_HANDKOMPONENTE
                If MySQLIsUsedInProduction(KO_Nr) Then
                    _LastErrorText = "Dieser Rohstoff wird In der Produktion noch verwendet und kann nicht gelöscht werden"
                    Return False
                ElseIf MySQLIsUsedInRecipe(KO_Nr) Then
                    _LastErrorText = "Dieser Rohstoff wird noch In Rezepturen verwendet und kann nicht gelöscht werden"
                    Return False
                Else
                    Return True
                End If

            Case Else
                Return True
        End Select
    End Function

    ''' <summary>
    ''' Ermittelt die Anzahl der Datensätze in der Tabelle wbdaten.ArbRzSchritte mit der übergebenen Komponenten-Nummer
    ''' Ist die Anzahl der Datensätze gleich Null, wird True zurückgegeben sonst False.
    ''' </summary>
    ''' <param name="InterneKomponentenNummer">Integer - Interne Komponenten-Nummer</param>
    ''' <returns>Boolean - Löschen ist erlaubt</returns>
    Private Function MySQLIsUsedInProduction(InterneKomponentenNummer As Integer) As Boolean
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWbDaten, wb_Sql.dbType.mySql)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompInArbRzp, InterneKomponentenNummer)
        Dim Count As Integer = -1

        'Suche nach KO_Nr
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                Count = winback.iField("Used")
            End If
        End If
        'Datenbank wieder schliessen
        winback.Close()
        Debug.Print("MySQLIsUsedInProduction " & Count.ToString)

        'Löschen erlaubt, wenn die Anzahl der Datensätze gleich Null ist
        Return (Count <> 0)
    End Function

    ''' <summary>
    ''' Ermittelt die Anzahl der Datensätze in der Tabelle winback.RezeptSchritte mit der übergebenen Komponenten-Nummer
    ''' Ist die Anzahl der Datensätze gleich Null, wird True zurückgegeben sonst False.
    ''' </summary>
    ''' <param name="InterneKomponentenNummer">Integer - Interne Komponenten-Nummer</param>
    ''' <returns>Boolean - Löschen ist erlaubt</returns>
    Private Function MySQLIsUsedInRecipe(InterneKomponentenNummer) As Boolean
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompInRezept, InterneKomponentenNummer)
        Dim Count As Integer = -1

        'Suche nach KO_Nr
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                Count = winback.iField("Used")
            End If
        End If
        'Datenbank wieder schliessen
        winback.Close()
        Debug.Print("MySQLIsUsedInRecipe " & Count.ToString)

        'Löschen erlaubt, wenn die Anzahl der Datensätze gleich Null ist
        Return (Count <> 0)
    End Function

    ''' <summary>
    ''' Markiert die aktuelle Komponente (Update Nährwert-Info notwendig oder Nährwertinfo fehlerhaft)
    ''' </summary>
    ''' <param name="Marker"></param>
    Public Sub MySQLdbSetMarker(Marker As wb_Global.ArtikelMarker)
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Interne Komponenten-Nummer muss definiert sein
        If KO_Nr > 0 Then
            'Update Komponente in winback.Komponenten
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompSetMarker, KO_Nr, Marker))
        End If
        winback.Close()
    End Sub

    ''' <summary>
    ''' Markiert alle Rohstoffe(Komponenten), die mit Rezepturen verknüpft sind, welche die Komponente
    ''' enthalten. (Update Nährwert-Info notwendig oder Nährwertinfo fehlerhaft)
    ''' 
    ''' Die einfache Variante UPDATE mit INNER JOIN funktioniert mit MySQL 3.2 nicht !!
    ''' deshalb muss zunächst eine Liste aller Rezepturen erzeugt werden, welche die aktuelle Komponente enthalten
    ''' Anhand dieser Liste werden dann alle Artikel markiert, die ein Rezept aus der Liste referenzieren.
    ''' 
    ''' </summary>
    Public Sub MySQLdbSetMarkerRzptListe(Marker As wb_Global.ArtikelMarker)
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Interne Komponenten-Nummer muss definiert sein
        If KO_Nr > 0 Then
            'Select über alle Rezeptschritte die KO_nr enthalten (Liste aller Rezepturen)
            Dim sql As String = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompSetMarkerRzListe, KO_Nr)
            Dim RezeptListe As New ArrayList
            If winback.sqlSelect(sql) Then
                While winback.Read
                    RezeptListe.Add(winback.iField("RS_RZ_Nr"))
                End While
            End If
            'Datenbank wieder schliessen
            winback.CloseRead()

            'alle Einträge in der Liste abarbeiten (Markieren Komponenten-Datensatz)
            For Each RzNr As Integer In RezeptListe
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKompSetMarkerRzNr, RzNr, Marker))
            Next

        End If
        winback.Close()
    End Sub

    ''' <summary>
    ''' Löscht alle Einträge zur aktuellen Komponenten-Nummer aus der WinBack-Datenbank
    '''     - winback.Lagerorte     (LG_Ort)
    '''     - winback.Lieferungen   (LF_LG_Ort)
    '''     - winback.Komponenten   (KO_Nr)
    '''     - winback.KomponParams  (KO_Nr)
    '''     - winback.Hinweise2     (KO_Nr)
    '''     - winback.RohParams     (KO_Nr)
    '''     
    ''' Die Datenfelder KO_Nr und KA_Lagerort müssen in MySQLdbRead_StammDaten vorab gelesen worden sein.
    ''' (Routine MySQLdbCanBeDeleted) 
    ''' </summary>
    Public Sub MySQLdbDelete()
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)

        'Interne Komponenten-Nummer muss definiert sein
        If KO_Nr > 0 Then
            'Löschen Komponente in winback.Komponenten
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelKomponenten, KO_Nr))
            'Löschen Komponente in winback.KomponParams
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelKomponParams, KO_Nr))
            'Löschen Komponente in winback.Hinweise2
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelKompHinweise, KO_Nr))
            'Löschen Komponente in winbackRohParams
            winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelRohParams, KO_Nr))

            'Der Lagerort muss definiert sein
            If KA_Lagerort <> "" Then
                'Löschen winback.LagerOrte
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelLagerOrte, KA_Lagerort))
                'Löschen winback.KLieferungen
                winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlDelLieferungen, KA_Lagerort))
            End If

        End If
        winback.Close()
    End Sub

    ''' <summary>
    ''' Komponenten-Datensatz neu anlegen
    ''' Es werden nur die Komponenten-Nummern (intern/extern) und die Komponenten-Type angelegt.
    ''' Die Komponenten-Bezeichnung ist "Neu angelegt " mit Datum/Uhrzeit
    ''' 
    ''' Alle weiteren Komponenten-Daten werden mit MySQLdbUpdate eingetragen.
    ''' </summary>
    ''' <param name="KType">KomponTypen - Komponenten-Type der anzulegenden Komponente</param>
    ''' <returns>Integer - neu anglegte (interne) Komponenten-Nummer</returns>
    Public Function MySQLdbNew(KType As wb_Global.KomponTypen) As Integer
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'interne Komponenten-Nummer ermitteln aus max(KO_NR)
        KO_Nr = wb_sql_Functions.getNewKomponNummer()
        'Komponenten-Type (Artikel/Handkomponente)
        KO_Type = KType
        'Komponenten-Art (vorab) festlegen
        If KO_Type = KomponTypen.KO_TYPE_ARTIKEL Then
            KA_Art = "1"
        Else
            KA_Art = "0"
        End If

        'Datensatz neu anlegen
        winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlAddNewKompon, KO_Nr, KO_Nr_AlNum, wb_Functions.KomponTypeToInt(KO_Type), "Neu angelegt " & Date.Now))
        winback.Close()
        'neuen KompNummer zurückgeben
        Return KO_Nr
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder zu der angegebenen Komponenten-Nummer aus der WinBack-Datenbank. Wenn die interne Komponenten-Nummer nicht angegeben ist
    ''' (Kleiner oder gleich Null) dann wird versucht, anhand der Artikel-Nummer den Datensatz zu finden.
    ''' 
    ''' Gibt True zurück, wenn der Datensatz gefunden wurde.
    ''' TODO Was ist zu tun, wenn mehr als ein Datensatz gefunden wurde
    ''' TODO Die interne Nummer an OrgaBack zurückschreiben
    ''' </summary>
    Public Function MySQLdbRead(InterneKomponentenNummer As Integer, Optional KomponentenNummer As String = "") As Boolean
        'Alle (eventuell noch) bestehenden Daten löschen
        Me.Invalid()

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach KO_Nr oder KO_AlNum
        If InterneKomponentenNummer > 0 Then
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_KO_Nr, InterneKomponentenNummer)
        Else
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_AlNum, KomponentenNummer)
        End If

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                MySQLdbRead(winback.MySqlRead)
                winback.CloseRead()

                'weitere Parameter einlesen - Tabelle KomponParams(Parameter Produktion)
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlKomponParamsXXX, Nr)
                If winback.sqlSelect(sql) Then
                    If winback.Read Then
                        MySQLdbRead(winback.MySqlRead)
                    End If
                End If
                winback.CloseRead()

                'weitere Parameter einlesen - Tabelle RohParams(erweiterte Parameter/Nährwerte)
                sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlRohParamsXXX, Nr)
                If winback.sqlSelect(sql) Then
                    If winback.Read Then
                        MySQLdbRead(winback.MySqlRead)
                    End If
                End If
                winback.Close()
                Return True

            Else
                'Sonderfall - Es wurde eine interne Komponenten-Nummer angegeben die nicht gefunden wurde
                'Rohstoff/Artikel wurde gelöscht (in WinBack)
                If (InterneKomponentenNummer > 0) And (KomponentenNummer <> "") Then
                    'bestehende Verbindung schliessen
                    winback.Close()
                    'Suche nach alphanumrischer Nummer
                    Return MySQLdbRead(0, KomponentenNummer)
                End If
            End If
        End If
        winback.Close()
        Return False
    End Function

    Public Function MysqldbRead(KomponType As wb_Global.KomponTypen)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String
        'Suche nach dem ersten Datensatz mit dieser Komponenten-Type
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlSelectKomp_KO_Type, wb_Functions.KomponTypeToInt(KomponType))

        'ersten Datensatz aus Tabelle Komponenten lesen
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
    ''' Liest alle Datenfelder aus dem aktuellen Datensatz in das Komponenten-Objekt
    ''' Die Daten werden anhand der Feldbezeichnung in die einzelnen Properties eingetragen.
    ''' 
    ''' Das letzte Datenfeld ist der TimeStamp und wird NICHT eingelesen, da es Probleme mit
    ''' der Konvertierung von MySQLDateTime in DateTime gibt
    ''' (https://bugs.mysql.com/bug.php?id=87120)
    ''' </summary>
    ''' <param name="sqlReader"></param>
    ''' <returns>True wenn kein Fehler aufgetreten ist</returns>
    Public Function MySQLdbRead(ByRef sqlReader As MySqlDataReader) As Boolean
        'Stammdaten - Anzahl der Felder im DataSet
        'FieldCount-2 unterdrückt das Feld TimeStamp
        For i = 0 To sqlReader.FieldCount - 2
            Try
                MySQLdbRead_StammDaten(sqlReader.GetName(i), sqlReader.GetValue(i))
            Catch ex As Exception
                Debug.Print("Exception MySQLdbReadStammdaten" & ex.Message)
            End Try
        Next

        'Schleife über alle Parameter-Datensätze
        'Bis alle Datensätze eingelesen sind
        Do
            'Parameter - Anzahl der Felder im DataSet
            'FieldCount-2 unterdrückt das Feld TimeStamp
            For i = 0 To sqlReader.FieldCount - 2
                Try
                    MySQLdbRead_Parameter(sqlReader.GetName(i), sqlReader.GetValue(i))
                Catch ex As Exception
                    Debug.Print("Exception MySQLdbRead " & sqlReader.GetName(i))
                End Try
            Next
        Loop While sqlReader.Read
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Der Feldname bestimmt das Ziel
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_StammDaten(Name As String, Value As Object) As Boolean
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        'Debug.Print("ReadStammdaten " & Name & "/" & Value)
        Try
            Select Case Name

                'Nummer(intern)
                Case "KO_Nr"
                    Nr = CInt(Value)
               'Type
                Case "KO_Type"
                    KO_Type = IntToKomponType(CInt(Value))
               'Type
                Case "KA_Art"
                    KA_Art = Value
                'Bezeichnung
                Case "KO_Bezeichnung"
                    KO_Bezeichnung = wb_Functions.MySqlToUtf8(Value)
                'Kommentar
                Case "KO_Kommentar"
                    KO_Kommentar = Value
                'Nummer(alphanumerisch)
                Case "KO_Nr_AlNum"
                    KO_Nr_AlNum = Value
                'Backverlust(Rezept im Rezept)
                Case "KO_Temp_Korr"
                    KO_Backverlust = Value
                'Produktions-Vorlauf in [h]
                Case "KA_Prod_Linie"
                    KA_ProdVorlauf = Value
                'Index WinBack-Cloud
                Case "KA_Matchcode"
                    KO_IdxCloud = Value
                'Index Rezeptnummer(Rezept im Rezept)
                Case "KA_RZ_Nr"
                    KA_Rz_Nr = CInt(Value)
                'Lagerort
                Case "KA_Lagerort"
                    KA_Lagerort = Value
                    ktTypXXX.Wert(T101_LagerOrt) = Value

                'Stückgewicht in Gramm
                Case "KA_Stueckgewicht"
                    'If Type = KomponTypen.KO_TYPE_ARTIKEL Then
                    ArtikelChargen.StkGewicht = Value
                    'End If
            End Select

            'Artikel - Chargengrößen in Stück
            If Type = KomponTypen.KO_TYPE_ARTIKEL Then

                Select Case Name
                'Minimal-Charge
                    Case "KA_Charge_Min"
                        ArtikelChargen.MinCharge.MengeInStk = Value
                'Maximal-Charge
                    Case "KA_Charge_Max"
                        ArtikelChargen.MaxCharge.MengeInStk = Value
                'Optimal-Charge
                    Case "KA_Charge_Opt"
                        ArtikelChargen.OptCharge.MengeInStk = Value
                End Select
            End If

            'Rohstoffe - Chargengrößen in kg
            If Type = KomponTypen.KO_TYPE_HANDKOMPONENTE Or Type = KomponTypen.KO_TYPE_AUTOKOMPONENTE Then

                Select Case Name
                'Minimal-Charge
                    Case "KA_Charge_Min_kg"
                        ArtikelChargen.MinCharge.MengeInkg = Value
                'Maximal-Charge
                    Case "KA_Charge_Max_kg"
                        ArtikelChargen.MaxCharge.MengeInkg = Value
                'Optimal-Charge
                    Case "KA_Charge_Opt_kg"
                        ArtikelChargen.OptCharge.MengeInkg = Value
                End Select
            End If

        Catch ex As Exception
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in die entprechende Property der Klasse. Anhand von 
    ''' Parameter-Nummer und Parameter-Typ wird der Wert in das entsprechende Feld
    ''' eingetragen.
    ''' ParamNr und ParamWert müssen definiert sein, bevor der Wert geschrieben werden
    ''' kann!
    ''' </summary>
    ''' <param name="Name">String - Bezeichnung Datenbankfeld</param>
    ''' <param name="Value">Object - Wert Datenbankfeld(Inhalt)</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Parameter(Name As String, Value As Object) As Boolean
        Static ParamNr, ParamTyp As Integer

        'Try
        '    Debug.Print("ReadParameter/Value " & Name & "/" & Value)
        'Catch
        'End Try

        'Feldname aus der Datenbank
        Select Case Name

            'Parameter-Nummer
            Case "RP_ParamNr", "KP_ParamNr"
                ParamNr = CInt(Value)

            'Parameter-Typ
            Case "RP_Typ_Nr"
                ParamTyp = CInt(Value)

            'Parameter-Wert(RohParams)
            Case "RP_Wert"
                Select Case ParamTyp
                    Case 200
                        'Produktinformationen
                    Case 201
                        'Verarbeitungs-Hinweise
                    Case 202
                        'Kalkulation/Preise
                    Case 210
                        'Froster
                    Case 220
                        'Teig-Gare
                    Case 300
                        'Parameter Produktion
                        ktTyp300.Wert(ParamNr) = Value.ToString
                    Case 301
                        'Nährwert-Informationen
                        ktTyp301.Wert(ParamNr) = Value.ToString
                End Select

            'Parameter-Wert(KomponParams)
            Case "KP_Wert"
                ktTypXXX.Wert(ParamNr) = Value.ToString

                'TimeStamp
                'TODO WIRD DAS HIER RICHTIG EINGELESEN ??
                'BREAK
            Case "RP_Timestamp"
                Select Case ParamTyp
                    Case 301
                        'Nährwert-Informationen
                        ktTyp301.TimeStamp = CDate(Value.ToString)
                End Select
        End Select
        Return True
    End Function

    ''' <summary>
    ''' schreibt alle Datenfelder aus dem Komponenten-Objekt mit der angegebenen Komponenten-Nummer in die Datenbank.
    ''' </summary>
    ''' <returns></returns>
    Public Function MySQLdbUpdate() As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Update-Statement wird dynamisch erzeugt    
        sql = "KO_Nr_AlNum = '" & Nummer & "'," &
              "KO_Bezeichnung = '" & Bezeichnung & "'," &
              "KO_Kommentar = '" & Kommentar & "'," &
              "KO_Temp_Korr = '" & KO_Backverlust & "'," &
              "KA_Prod_Linie = '" & KA_ProdVorlauf & "'," &
              "KA_Matchcode = '" & KO_IdxCloud & "'," &
              "KA_Lagerort = '" & KA_Lagerort & "'," &
              "KA_Stueckgewicht = '" & ArtikelChargen.StkGewicht & "'," &
              "KA_Art = '" & KA_Art & "'"

        'Rezeptnummer nur updaten wenn gültig
        If KA_Rz_Nr <> wb_Global.UNDEFINED Then
            sql = sql & "," &
                        "KA_RZ_Nr = " & KA_Rz_Nr.ToString
        End If

        'Artikel - Chargengrößen in Stk
        If Type = wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
            sql = sql & "," &
                        "KA_Charge_Min = '" & ArtikelChargen.MinCharge.MengeInStk & "'," &
                        "KA_Charge_Max = '" & ArtikelChargen.MaxCharge.MengeInStk & "'," &
                        "KA_Charge_Opt = '" & ArtikelChargen.OptCharge.MengeInStk & "'"
        End If

        'Rohstoffe - Chargengrößen in kg
        If Type = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE Or Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE Then
            sql = sql & "," &
                        "KA_Charge_Min_kg = '" & ArtikelChargen.MinCharge.MengeInkg & "'," &
                        "KA_Charge_Max_kg = '" & ArtikelChargen.MaxCharge.MengeInkg & "'," &
                        "KA_Charge_Opt_kg = '" & ArtikelChargen.OptCharge.MengeInkg & "'"
        End If

        'Update ausführen
        'Debug.Print("Komponente.MysqldbUpdate " & sql)

        If winback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateKomp_KO_Nr, Nr, sql)) Then
            winback.Close()
            Return True
        Else
            winback.Close()
            Return False
        End If
    End Function

    ''' <summary>
    ''' Schreibt die  WinBack-Komponenten-Parameter in die WinBack-Datenbank. 
    ''' </summary>
    ''' <param name="ktTyp"></param>
    ''' <returns></returns>
    Public Function MySQLdbUpdate_Parameter(Optional ktTyp As wb_Global.ktParam = wb_Global.ktParam.ktAlle) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Result vorbelegen
        MySQLdbUpdate_Parameter = True

        'Update Parameter-300 (Parameter Produktion)
        If ktTyp = wb_Global.ktParam.kt300 Or ktTyp = wb_Global.ktParam.ktAlle Then
            If Not ktTyp300.MySQLdbUpdate(Nr, winback) Then
                MySQLdbUpdate_Parameter = False
            End If
        End If

        'Update Parameter-301 (Nährwerte)
        If ktTyp = wb_Global.ktParam.kt301 Or ktTyp = wb_Global.ktParam.ktAlle Then
            If Not ktTyp301.MySQLdbUpdate(Nr, winback) Then
                MySQLdbUpdate_Parameter = False
            End If
        End If

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
    End Function

    ''' <summary>
    ''' Sichert die Zutatenliste in der Datenbank
    ''' </summary>
    ''' <returns></returns>
    Public Function MySqldbUpdate_Zutatenliste() As Boolean
        'TODO festlegen ob externe oder interne Deklaration
        Return KO_DeklBezeichnungExtern.Write()
    End Function

    ''' <summary>
    ''' Schreibt alle Parameter zur Komponente in die OrgaBack-Datenbank(Zugriff über KO_Nr_AlNum!)
    '''     -kt301  dbo.ArtikelNaehrwerte
    '''     
    ''' Vor Beginn der INSERT/UPDATES wird geprüft ob der Artikel in OrgaBack existiert.
    ''' Wenn nicht wird abgebrochen und False zurückgegeben.
    ''' 
    ''' </summary>
    ''' <param name="ktTyp"></param>
    ''' <returns></returns>
    Public Function MsSQLdbUpdate_Parameter(Optional ktTyp As wb_Global.ktParam = wb_Global.ktParam.ktAlle) As Boolean
        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)

        'Prüfen ob der Artikel in OrgaBack existiert
        OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlSelArtikel, KO_Nr_AlNum))
        If Not OrgasoftMain.Read Then
            'Artikel nicht gefunden in OrgaSoft
            Debug.Print("Artikel " & Nummer & " nicht in OrgaBack gefunden")
            ChangeLogAdd(LogType.Err, Nr, "", "Artikel/Komponente nicht in OrgaBack gefunden")
            OrgasoftMain.CloseRead()
            Return False
        Else
            'Standard-Einheit aus Artikelstamm OrgaBack
            Dim StdEinheit As Integer = OrgasoftMain.iField("StdEinheit")
            'wenn beide Einheiten identisch sind, können die Parameter geschreiben werden
            If StdEinheit = wb_Einheiten_Global.GetobEinheitNr(Einheit) Then
                'Lesen beendet
                OrgasoftMain.CloseRead()
                'Update Parameter-301 (Nährwerte)
                If ktTyp = wb_Global.ktParam.kt301 Or ktTyp = wb_Global.ktParam.ktAlle Then
                    ktTyp301.MsSQLdbUpdate(KO_Nr_AlNum, wb_Einheiten_Global.GetobEinheitNr(Einheit), OrgasoftMain)
                    ChangeLogAdd(LogType.Msg, Nr, "", "Update der Nährwerte/Allergene in OrgaBack-DB - Nr " & KO_Nr_AlNum)
                End If
            Else
                Debug.Print("Einheitenkonflikt Artikel beim Schreiben der Parameter in OrgaBack " & KO_Nr_AlNum & " " & KO_Bezeichnung)
                ChangeLogAdd(LogType.Err, Nr, "", "Einheitenkonflikt Artikel beim Schreiben der Parameter in OrgaBack - Nr " & KO_Nr_AlNum)
                Return False
            End If
        End If

        'Verbindung zur Datenbank wieder schliessen
        OrgasoftMain.Close()
        Return True
    End Function

    ''' <summary>
    ''' Schreibt die Deklarationstexte in die OrgaBack-Datenbank(Zugriff über KO_Nr_AlNum!)
    ''' Die Daten werden nur geschrieben, wenn sie nicht fixiert sind. Vor dem Schreiben erfolgt immer
    ''' ein Lesezugriff.
    '''
    ''' [dbo].[ArtikelDeklarationsTexte]
    '''   ->[ArtikelNr]
    '''   ->[StuecklistenVariantenNr]
    '''   D [LaenderCode] !!!!
    '''   D [SprachenCode]
    '''   ->[AllergenDeklarationEnthalten]
    '''   ->[AllergenDeklarationSpuren]
    '''   ->[AllergenKurzDeklarationEnthalten]
    '''   ->[AllergenKurzDeklarationSpuren]
    '''   [ZusatzstoffDeklaration]
    '''   [ZusatzstoffKurzDeklaration]
    '''   [ZusatzstoffDeklarationUVP]
    '''   [ZusatzstoffDeklarationErgaenzung]
    '''   [ZusatzstoffDeklarationENummern]
    '''   [DeklarationsText]
    '''   ->[Zutaten]
    '''   ->[AllergenDeklarationFix]
    '''   [ZusatzstoffDeklarationFix]
    '''   ->[ZutatenDeklarationFix]
    ''' </summary>
    ''' <returns></returns>
    Public Function MsSqldbUpdate_Zutatenliste() As Boolean
        'Default Rückgabewert
        MsSqldbUpdate_Zutatenliste = False
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Zutaten- und Allergenlisten
        Dim ZutatenListe As String = ""
        Dim AllergenListeEnthalten As String = ""
        Dim AllergenListeSpuren As String = ""
        Dim AllergenKurzListeEnthalten As String = ""
        Dim AllergenKurzListeSpuren As String = ""

        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)

        'Daten aus dbo.ArtikelDeklarationstexte lesen (Artikelnummer/Variante 0)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlReadDeklaration, KO_Nr_AlNum, 0, wb_GlobalSettings.osLaendercode, wb_GlobalSettings.osSprachcode)
        'Daten lesen
        OrgasoftMain.sqlSelect(sql)
        Dim DatenSatzVorhanden As Boolean = OrgasoftMain.Read

        'wenn schon ein Datensatz vorhanden ist, Flags einlesen (Fixiert)
        If DatenSatzVorhanden Then
            'Flags Zutaten-/Allergenliste ist fixiert
            Dim ZutatenListeFixiert As Boolean = OrgasoftMain.iField("ZutatenDeklarationFix") > 0
            Dim AllergenListeFixiert As Boolean = OrgasoftMain.iField("AllergenDeklarationFix") > 0

            'Datenfelder lesen
            ZutatenListe = OrgasoftMain.sField("Zutaten")
            AllergenListeEnthalten = OrgasoftMain.sField("AllergenDeklarationEnthalten")
            AllergenListeSpuren = OrgasoftMain.sField("AllergenDeklarationSpuren")
            AllergenKurzListeEnthalten = OrgasoftMain.sField("AllergenKurzDeklarationEnthalten")
            AllergenKurzListeSpuren = OrgasoftMain.sField("AllergenKurzDeklarationSpuren")
            'Verbindung Lesen schliessen
            OrgasoftMain.CloseRead()

            'Zutatenliste aktualisieren
            If Not ZutatenListeFixiert Then
                ZutatenListe = wb_Functions.XRemoveSonderZeichen(Deklaration, True)
                'Meldungen im Log ausgeben
                ChangeLogAdd(LogType.Msg, Nr, "", "Update der Zutatenliste in OrgaBack-DB - Nr " & KO_Nr_AlNum)
            End If

            'Liste der Allergen aktualisieren
            If Not AllergenListeFixiert Then
                AllergenListeEnthalten = ktTyp301.AllergenListe_C
                AllergenListeSpuren = ktTyp301.AllergenListe_T
                AllergenKurzListeEnthalten = ktTyp301.AllergenKurzListe_C
                AllergenKurzListeSpuren = ktTyp301.AllergenKurzListe_T
                'Meldungen im Log ausgeben
                ChangeLogAdd(LogType.Msg, Nr, "", "Update der Allergenliste in OrgaBack-DB - Nr " & KO_Nr_AlNum)
            End If

            'Update Datenbank - Update-Statement
            sql = "[Zutaten] = '" & ZutatenListe & "', " &
                  "[AllergenDeklarationEnthalten] = '" & AllergenListeEnthalten & "', " &
                  "[AllergenDeklarationSpuren] = '" & AllergenListeSpuren & "', " &
                  "[AllergenKurzDeklarationEnthalten] = '" & AllergenKurzListeEnthalten & "', " &
                  "[AllergenKurzDeklarationSpuren] = '" & AllergenKurzListeSpuren & "'"
            'Update durchführen
            MsSqldbUpdate_Zutatenliste = OrgasoftMain.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateDeklaration, sql, KO_Nr_AlNum, 0, wb_GlobalSettings.osLaendercode, wb_GlobalSettings.osSprachcode))
        Else
            'Verbindung Lesen schliessen
            OrgasoftMain.CloseRead()
            'noch kein Datensatz vorhanden
            sql = "'" & Deklaration & "', '" & ktTyp301.AllergenListe_C & "', '" & ktTyp301.AllergenListe_T & "', " &
                  "'" & ktTyp301.AllergenKurzListe_C & "', '" & ktTyp301.AllergenKurzListe_T & "'"
            'Insert durchführen
            MsSqldbUpdate_Zutatenliste = OrgasoftMain.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlInsertDeklaration, sql, KO_Nr_AlNum, 0, wb_GlobalSettings.osLaendercode, wb_GlobalSettings.osSprachcode))
            'Meldungen im Log ausgeben
            ChangeLogAdd(LogType.Msg, Nr, "", "Neu Anlegen der Zutaten und Allergenliste in OrgaBack-DB - Nr " & KO_Nr_AlNum)
        End If

        'Verbindung zur Datenbank wieder schliessen
        OrgasoftMain.Close()
    End Function

End Class
