﻿Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json.Linq
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
    Private KO_Zuschnitt As Integer
    Private KA_Verarbeitungshinweise As String
    Private KA_VerarbeitungshinweisePfad As String
    Private KA_VerarbeitungshinweiseDPI As String
    Private KO_IdxCloud As String
    Private KA_Rz_Nr As Integer
    Private KA_Rz_Nr_HasChanged As Boolean = False
    Private KA_Lagerort As String
    Private KA_Preis As String
    Private KA_Grp1 As Integer
    Private KA_Grp2 As Integer
    Private KA_Grp_HasChanged As Boolean = False
    Private KA_Charge_Opt_kg As String
    Private KA_zaehlt_zu_RZ_Gesamtmenge As String
    Private KA_zaehlt_zu_NWT_Gesamtmenge As String
    Private KA_aktiv As Integer
    Private KA_alternativ_RS As String
    Private KA_PreisEinheit As Integer

    Private KO_DeklBezeichnungExtern As New wb_Hinweise(Hinweise.DeklBezRohstoff)
    Private KO_DeklBezeichnungIntern As New wb_Hinweise(Hinweise.DeklBezRohstoffIntern)
    Private KO_MehlZusammenSetzung As New wb_Hinweise(Hinweise.MehlZusammensetzung)

    Private _DataHasChanged As Boolean = False
    Private _LastErrorText As String
    Private _RezeptNummer As String = Nothing
    Private _RezeptName As String = Nothing
    Private _LinienGruppe As Integer = wb_Global.UNDEFINED
    Private _ArtikelLinienGruppe As Integer = wb_Global.UNDEFINED
    Private _ReadCalcPreis As Boolean = True

    Private _RootParameter As New wb_KomponParam(Nothing, 0, 0, "")
    Private _Parameter As wb_KomponParam
    Private _Lager As wb_LagerOrt
    Private _CalcVerkaufsgewicht As Double = wb_Global.UNDEFINED

    Public ktTypXXX As New wb_KomponParamXXX
    Public ktTyp200 As New wb_KomponParam200
    Public ktTyp201 As New wb_KomponParam201
    Public ktTyp202 As New wb_KomponParam202
    Public ktTyp210 As New wb_KomponParam210
    Public ktTyp220 As New wb_KomponParam220
    Public ktTyp300 As New wb_KomponParam300
    Public ktTyp301 As New wb_KomponParam301
    Public ktTyp303 As New wb_KomponParam303

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
        KA_Charge_Opt_kg = ""
        KA_zaehlt_zu_RZ_Gesamtmenge = Nothing
        KA_zaehlt_zu_NWT_Gesamtmenge = Nothing
        KA_VerarbeitungshinweisePfad = ""

        KA_Rz_Nr = wb_Global.UNDEFINED
        _RezeptNummer = Nothing
        _RezeptName = Nothing
        _LinienGruppe = wb_Global.UNDEFINED
        _ArtikelLinienGruppe = wb_Global.UNDEFINED
        _Lager = Nothing

        KO_DeklBezeichnungExtern.Invalid()
        KO_DeklBezeichnungIntern.Invalid()
        KO_MehlZusammenSetzung.Invalid()

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
        ktTyp303 = New wb_KomponParam303

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
    ''' Komponenten-Type ändern. 
    ''' Ändert die (Readonly) Komponenten-Type falls in OrgaBack die Artikelgruppe geändert wird. (Artikel/Rohstoff)
    ''' Gibt true zurück wenn eine Änderung stattgefunden hat.
    ''' 
    ''' Geändert wird nur von Artikel zu Rohstoff und umgekehrt.
    ''' (Fehler bei Niehaves/Fonk - Automatik-Komponente wird Handkomponente)
    ''' </summary>
    ''' <param name="KType"></param>
    ''' <returns></returns>
    Public Function SetKType(KType As KomponTypen) As Boolean
        Select Case KType
            Case KomponTypen.KO_TYPE_ARTIKEL
                If Type <> KomponTypen.KO_TYPE_ARTIKEL Then
                    KO_Type = KType
                    Return True
                End If
            Case KomponTypen.KO_TYPE_HANDKOMPONENTE, KomponTypen.KO_TYPE_METER, KomponTypen.KO_TYPE_STUECK
                If Type = KomponTypen.KO_TYPE_ARTIKEL OrElse
                    Type = KomponTypen.KO_TYPE_STUECK OrElse
                    Type = KomponTypen.KO_TYPE_METER OrElse
                    Type = KomponTypen.KO_TYPE_HANDKOMPONENTE Then
                    KO_Type = KType
                    Return True
                End If
            Case KomponTypen.KO_TYPE_UNDEFINED
                KO_Type = KomponTypen.KO_TYPE_HANDKOMPONENTE
                Return True
            Case Else
                If KType <> Type Then
                    KO_Type = KType
                    Return True
                End If
        End Select
        Return False
    End Function

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
            'Änderungen loggen (geändert 15-05-2020 Weigmann)
            'KO_Bezeichnung = ChangeLogAdd(LogType.Prm, Parameter.Tx_Bezeichnung, KO_Bezeichnung, wb_Functions.XRemoveSonderZeichen(value))
            KO_Bezeichnung = value
            'Flag setzen wenn sich die Daten geändert haben
            If ChangeLogChanged Then
                _DataHasChanged = True
            End If
        End Set
        Get
            Return KO_Bezeichnung
        End Get
    End Property

    ''' <summary>
    ''' Kommentar zum Rohstoff.
    ''' Wenn sich der Kommentar ändert, wird im zugehörigen Lagerort der entsprechende Kommentar ebenfalls gesetzt.
    ''' Damit wird auch im WinBack-Material-Fenster (Produktion) der Kommentar zum Rohstoff angezeigt. (Silo-Nummer bei mehreren Rohstoffen)
    ''' </summary>
    ''' <returns></returns>
    Public Property Kommentar As String
        Set(value As String)
            'Änderungen loggen
            KO_Kommentar = ChangeLogAdd(LogType.Prm, Parameter.Tx_Kommentar, KO_Kommentar, wb_Functions.XRemoveSonderZeichen(value))
            'Flag setzen wenn sich die Daten geändert haben
            If ChangeLogChanged Then
                _DataHasChanged = True
                'Kommentar wird auch im Lagerort gespeichert
                If _Lager IsNot Nothing Then
                    _Lager.Kommentar = KO_Kommentar
                End If
            End If
        End Set
        Get
            Return KO_Kommentar
        End Get
    End Property

    ''' <summary>
    ''' Bemerkungsfeld zum Rohstoff. Wird in der Produktion im Materialbild angezeigt
    ''' Dient z.B. bei identischen Silo-Rohstoffen zur Unterscheidung der Silo's, da in OrgaBack nur eine Rohstoff-Nummer und 
    ''' eine eindeutige Rohstoff-Bezeichnung zulässig ist.
    ''' 
    '''     Nummer  150002  Brötchenmehl 550    kann in mehreren Silos vorhanden sein!
    '''     
    ''' Die gleiche Nummer dient in WinBack zur Umschaltung zwischen den Silos.
    ''' 
    ''' Das Bemerkungsfeld kommt aus der Tabelle winback.Lagerorte.LG_Kommentar
    ''' </summary>
    ''' <returns></returns>
    Public Property Bemerkung As String
        Get
            If _Lager Is Nothing Then
                _Lager = New wb_LagerOrt(KA_Lagerort)
            End If
            Return _Lager.Kommentar
        End Get
        Set(value As String)
            If _Lager IsNot Nothing Then
                _Lager.Kommentar = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' EAN-Code. Verwendet wird das Datenbank-Feld Kurzname.
    ''' </summary>
    ''' <returns></returns>
    Public Property EANCode As String
        Set(value As String)
            'Änderungen loggen
            KA_Kurzname = ChangeLogAdd(LogType.Prm, Parameter.Tx_EANCode, KA_Kurzname, wb_Functions.XRemoveSonderZeichen(value))
            'Flag setzen wenn sich die Daten geändert haben
            If ChangeLogChanged Then
                _DataHasChanged = True
            End If
        End Set
        Get
            Return KA_Kurzname
        End Get
    End Property

    ''' <summary>
    ''' Dateiname Artikel-Verarbeitungshinweis (pdf/html)
    ''' Enthält den Dateinamen der Verarbeitungshinweis-Datei für den Artikel ohne(!) Extension.
    ''' 
    ''' In WinBack-Produktion wird die entsprechende Datei als html-File im Verzeichnis /home/herbst/hinweise gesucht und 
    ''' angezeigt.
    ''' </summary>
    ''' <returns></returns>
    Public Property VerarbeitungsHinweise As String
        Get
            Return KA_Verarbeitungshinweise
        End Get
        Set(value As String)
            KA_Verarbeitungshinweise = value
        End Set
    End Property

    ''' <summary>
    ''' Der absolute Pfad zur Quelldatei der Artikel-Verarbeitungshinweise steht in den Komponenten-Parametern(20)
    ''' Wenn hier kein Pfad eingetragen ist, wird der Pfad aus der winback.ini verwendet.
    ''' 
    ''' Die Sonderzeichen für die Pfadangabe werden vor dem Speichern in der Datenbank umgewandelt in bcksl
    ''' </summary>
    ''' <returns></returns>
    Public Property VerarbeitungsHinweisePfad As String
        Get
            'nur gültig wenn auch eine Hinweis-Datei vorhanden ist.
            If KA_Verarbeitungshinweise <> "" AndAlso KA_VerarbeitungshinweisePfad = "" Then
                KA_VerarbeitungshinweisePfad = wb_Functions.XRestoreSonderZeichen(wb_sql_Functions.getKomponParam(Nr, KomponParams.VerarbeitungsHinweisePfad))
            End If
            Return KA_VerarbeitungshinweisePfad
        End Get
        Set(value As String)
            'nur gültig wenn auch eine Hinweis-Datei vorhanden ist.
            If value <> "" AndAlso KA_Verarbeitungshinweise <> "" Then
                KA_VerarbeitungshinweisePfad = value
                wb_sql_Functions.setKomponParam(Nr, KomponParams.VerarbeitungsHinweisePfad, wb_Functions.XRemoveSonderZeichen(KA_VerarbeitungshinweisePfad), "Pfad Verarbeitungshinweise")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Die Auflösung der Umwandlung von pdf in png steht in den Komponenten-Parametern(21)
    ''' </summary>
    ''' <returns></returns>
    Public Property VerarbeitungsHinweise_DPI As String
        Get
            'nur gültig wenn auch eine Hinweis-Datei vorhanden ist.
            If KA_Verarbeitungshinweise <> "" AndAlso KA_VerarbeitungshinweisePfad = "" Then
                KA_VerarbeitungshinweiseDPI = wb_sql_Functions.getKomponParam(Nr, KomponParams.VerarbeitungsHinweiseDPI)
            End If
            Return KA_VerarbeitungshinweiseDPI
        End Get
        Set(value As String)
            'nur gültig wenn auch eine Hinweis-Datei vorhanden ist.
            If value <> "" AndAlso KA_Verarbeitungshinweise <> "" Then
                KA_VerarbeitungshinweiseDPI = value
                wb_sql_Functions.setKomponParam(Nr, KomponParams.VerarbeitungsHinweiseDPI, KA_VerarbeitungshinweiseDPI, "DPI Verarbeitungshinweise")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Preis für diese Komponente. Der Preis kann auf verschiedene Arten ermittelt werden:
    ''' 
    '''     -   Prog-Variante OrgaBack
    '''         Preis-Information aus Interface
    '''         
    '''     -   Prog-Variante WinBack
    '''         - einfacher Rohstoff:   Preis aus Datenbank-Feld KA_Preis
    '''         - verknüpfter Rohstoff: Preis aus der Berechnung _RootRezeptschritt.Preis
    ''' </summary>
    ''' <returns></returns>
    Public Property Preis As String
        Set(value As String)
            KA_Preis = value
            _DataHasChanged = True
        End Set
        Get
            'Wenn das Flag ReadCalcPreis gesetzt ist (Rechenzeit z.B. Prodplanung)
            'sonst wird der letzte gespeicherte Wert aus KA_Preis zurückgegeben

            'in OrgaBack-Office darf der Preis nicht berechnet werden, da sonst eine Signum-dll fehlt
            If _ReadCalcPreis Then
                If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                    KA_Preis = wb_KomponentePreis.GetArtikelPreis(Nummer, Type).ToString
                Else
                    If RzNr > 0 Then
                        'TODO - Rechenzeit ?
                        KA_Preis = CalculatePreis(RzNr).ToString
                    End If
                End If
            End If

            'Zahlenwerte aus der Datenbank immer inm Format de-DE
            Return FormatStr(KA_Preis, 3, 4, "sql")
        End Get
    End Property

    Private Function CalculatePreis(RzNr As Integer) As Double
        If RzNr > 0 Then
            'Teig-Rezeptur komplett einlesen
            'TODO evtl zusammenlegen mit GetProduktionsDaten ?? (Rechenzeit gegen einfaches Einlesen der Kopfdaten)
            'Lösung: Kalkulation gezielt anstossen
            Dim Rezept As New wb_Rezept(RzNr, Nothing)
            Return Rezept.RootRezeptSchritt.Preis
        Else
            Return 0
        End If
    End Function

    Public Property Gruppe1 As Integer
        Set(value As Integer)
            If KA_Grp1 <> value Then
                KA_Grp_HasChanged = True
            End If
            KA_Grp1 = value
            _DataHasChanged = True
        End Set
        Get
            Return KA_Grp1
        End Get
    End Property

    Public Property Gruppe2 As Integer
        Set(value As Integer)
            If KA_Grp1 <> value Then
                KA_Grp_HasChanged = True
            End If
            KA_Grp2 = value
            _DataHasChanged = True
        End Set
        Get
            Return KA_Grp2
        End Get
    End Property

    Public Property GebindeGroesse As String
        Set(value As String)
            KA_Charge_Opt_kg = value
            _DataHasChanged = True
        End Set
        Get
            Return wb_Functions.FormatStr(KA_Charge_Opt_kg, 3)
        End Get
    End Property

    ''' <summary>
    ''' Datenbankfeld
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = 1    - zählt nicht zu RezGewicht -> True
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = 0    - zählt zu RezGewicht -> False
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = NULL - zählt zu RezGewicht -> False
    '''     
    ''' Wurdebis V1.8.4 auch für ZaehltTrotzdemZumNwtGewicht verwendet, ist aber nicht kompatibel zu WinBack-Produktion
    ''' </summary>
    ''' <returns></returns>
    Public Property ZaehltNichtZumRezeptGewicht As Boolean
        Set(value As Boolean)
            If value Then
                KA_zaehlt_zu_RZ_Gesamtmenge = wb_Global.ZaehltNichtZumRezeptGewicht
            Else
                KA_zaehlt_zu_RZ_Gesamtmenge = wb_Global.ZaehltZumRezeptGewicht
            End If
        End Set
        Get
            If (KA_zaehlt_zu_RZ_Gesamtmenge = wb_Global.ZaehltNichtZumRezeptGewicht) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    ''' <summary>
    ''' Datenbankfeld
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = 1  KA_zaehlt_zu_NWT_Gesamtmenge = 1   - zählt zu NwtGewicht -> True
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = 1  KA_zaehlt_zu_NWT_Gesamtmenge = 0   - zählt nicht zu NwtGewicht -> False
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = 0    - zählt zu NwtGewicht -> True
    '''     KA_zaehlt_zu_RZ_Gesamtmenge = NULL - zählt zu NwtGewicht -> True
    ''' </summary>
    ''' <returns></returns>
    Public Property ZaehltTrotzdemZumNwtGewicht As Boolean
        Set(value As Boolean)
            If value Then
                KA_zaehlt_zu_NWT_Gesamtmenge = wb_Global.ZaehltZumNwtGewicht
            Else
                KA_zaehlt_zu_NWT_Gesamtmenge = wb_Global.ZaehltNichtZumNwtGewicht
            End If
        End Set
        Get
            If (KA_zaehlt_zu_NWT_Gesamtmenge = wb_Global.ZaehltZumNwtGewicht) AndAlso ZaehltNichtZumRezeptGewicht Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Property Aktiv As Boolean
        Get
            If KA_aktiv > 0 Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value Then
                KA_aktiv = 1
            Else
                KA_aktiv = 0
            End If
        End Set
    End Property

    Public Property AlternativRohstoff As String
        Get
            Return KA_alternativ_RS
        End Get
        Set(value As String)
            KA_alternativ_RS = value
        End Set
    End Property

    Public Property FreigabeProduktion As Boolean
        Get
            Return (KA_Art = "1")
        End Get
        Set
            If Value Then
                KA_Art = "1"
            Else
                KA_Art = "0"
            End If
        End Set
    End Property

    Public Property NwtMarker As wb_Global.ArtikelMarker
        Get
            Return KA_PreisEinheit
        End Get
        Set(value As wb_Global.ArtikelMarker)
            KA_PreisEinheit = value
        End Set
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
                'Parameter Produkinformation (nicht OrgaBack)
                If wb_GlobalSettings.pVariante <> wb_Global.ProgVariante.OrgaBack Then
                    AddParamNodes("", wb_Global.ktParam.kt200)
                End If
                'Parameter Verarbeitungshinweise
                AddParamNodes("", wb_Global.ktParam.kt201)
                'Parameter Kalkulation (nicht OrgaBack)
                If wb_GlobalSettings.pVariante <> wb_Global.ProgVariante.OrgaBack Then
                    AddParamNodes("", wb_Global.ktParam.kt202)
                End If
                'Parameter Produktion
                AddParamNodes("", wb_Global.ktParam.kt300)

                'Parameter/Nährwerte neu berechnen (auf Basis der zugeordneten Rezeptur)
                'Rezept mit allen Rezeptschritten lesen (NoMessage=True unterdrückt die Meldung "Rezept verweist auf sich selbst")
                Dim Rzpt As New wb_Rezept(KA_Rz_Nr, Nothing, Backverlust, 1, "", "", True, False)
                'Änderungs-Log löschen
                ClearReport()

                'Nährwert-Information berechnen
                ktTyp301 = Rzpt.KtTyp301
                'Parameter Nährwerte
                AddParamNodes("", wb_Global.ktParam.kt301)

                'Parameter EU und Bio-Verbände
                AddParamNodes("EU/Bio", wb_Global.ktParam.kt303)

            Case KomponTypen.KO_TYPE_AUTOKOMPONENTE, KomponTypen.KO_TYPE_EISKOMPONENTE, KomponTypen.KO_TYPE_HANDKOMPONENTE, KomponTypen.KO_TYPE_WASSERKOMPONENTE
                'abhängig von der Komponenten-Type werden die einzelnen Parameter durchlaufen
                AddParamNodes("Produktion", t)
                'Parameter Nährwerte (nur wenn kein Rezept verknüpft)
                'TODO Nährwerte des unterlagerten Rezeptes anzeigen(muss vorher berechnet werden)
                If RzNr <= 0 Then
                    'Parameter Nährwerte
                    AddParamNodes("", wb_Global.ktParam.kt301)
                    'Parameter EU und Bio-Verbände
                    AddParamNodes("", wb_Global.ktParam.kt303)
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
                    Case = wb_Global.ktParam.kt201
                        _Parameter.Wert = ktTyp201.Wert(p)
                    Case = wb_Global.ktParam.kt300
                        _Parameter.Wert = ktTyp300.Wert(p)
                    Case wb_Global.ktParam.kt301
                        _Parameter.Wert = ktTyp301.Wert(p)
                    Case wb_Global.ktParam.kt303
                        _Parameter.Wert = ktTyp303.Wert(p)
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

        'Flag Allergen-Info wurde geändert
        Dim OrgaBackUpdateAllergene As Boolean = False
        'Stücklisten-Varianten
        Dim StkLstVar As List(Of Integer) = Nothing
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As wb_Sql = Nothing
        If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
            'Datenbank-Verbindung öffnen - MsSQL
            OrgasoftMain = New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
            'Stücklisten-Varianten
            StkLstVar = ktTyp301.GetStkListenVarianten(KO_Nr_AlNum, OrgasoftMain)
        End If

        'Ausgehend vom Root-Knoten werden alle Child-Knoten durchlaufen
        For Each p As wb_KomponParam In _RootParameter.ChildSteps

            'Parameter Produktion prüfen/reparieren (Fehler bei Niehaves nach Neuanlegen Rohstoff)
            If (KO_Type = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE) AndAlso (p.TypNr < wb_Global.ktParam.kt200) AndAlso (p.ChildSteps.Count < 7) Then
                'Parameter in winback.KomponParams anlegen
                ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_SollMenge, winback)
                ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_SollProzent, winback)
                ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_TolMinus, winback)
                ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_TolPlus, winback)
                ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_TolProzent, winback)
            End If

            'alle Child-Knoten
            For Each x As wb_KomponParam In p.ChildSteps
                'Parameter wurde geändert
                If x.Changed Then
                    'abhängig vom KomponentenParameter-Typ
                    Select Case p.TypNr

                        'Allgemeine Parameter
                        Case < wb_Global.ktParam.kt200
                            'Wert in ktxxx-Array übertragen
                            ktTypXXX.Wert(x.ParamNr) = x.Wert
                            'Update einzelner Datensatz in winback-Datenbank
                            ktTypXXX.MySQLdbUpdate(Nr, x.ParamNr, winback)
                            'Flag zurücksetzen
                            x.Changed = False

                        'Artikel Parameter
                        Case wb_Global.ktParam.kt201
                            'Wert in kt201-Array übertragen
                            ktTyp201.Wert(x.ParamNr) = x.Wert
                            'Update einzelner Datensatz in winback-Datenbank
                            ktTyp201.MySQLdbUpdate(Nr, x.ParamNr, winback)
                            'Flag zurücksetzen
                            x.Changed = False

                        'Parameter Produktion
                        Case wb_Global.ktParam.kt300
                            'Wert in kt300-Array übertragen
                            ktTyp300.Wert(x.ParamNr) = x.Wert
                            'Update einzelner Datensatz in winback-Datenbank
                            ktTyp300.MySQLdbUpdate(Nr, x.ParamNr, winback)
                            'Flag zurücksetzen
                            x.Changed = False

                        'Allergene und Nährwerte
                        Case wb_Global.ktParam.kt301
                            'Wert in kt301-Array übertragen
                            ktTyp301.Wert(x.ParamNr) = x.Wert
                            'Update einzelner Datensatz in winback-Datenbank
                            ktTyp301.MySQLdbUpdate(Nr, x.ParamNr, winback)
                            'Update einzelner Parametersatz in OrgaBack
                            If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
                                ktTyp301.MsSQLdbUpdate(KO_Nr_AlNum, x.ParamNr, wb_Einheiten_Global.GetobEinheitNr(Einheit), OrgasoftMain, StkLstVar)
                                'Flag setzen - Update AllergenListe in OrgaBack notwendig
                                OrgaBackUpdateAllergene = True
                            End If
                            'Flag zurücksetzen
                            x.Changed = False

                        'EU und Bio-Verbände
                        Case wb_Global.ktParam.kt303
                            'Wert in kt303-Array übertragen
                            ktTyp303.Wert(x.ParamNr) = x.Wert
                            'Update einzelner Datensatz in winback-Datenbank
                            ktTyp303.MySQLdbUpdate(Nr, x.ParamNr, winback)
                            'Update einzelner Parametersatz in OrgaBack
                            If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
                                ktTyp303.MsSQLdbUpdate(KO_Nr_AlNum, x.ParamNr, wb_Einheiten_Global.GetobEinheitNr(Einheit), OrgasoftMain)
                                'Flag setzen - Update AllergenListe in OrgaBack notwendig
                                OrgaBackUpdateAllergene = True
                            End If
                            'Flag zurücksetzen
                            x.Changed = False

                        Case Else
                            'Fehler - Parameter-Type nicht im Programm vorgesehen
                            'Debug.Print("SaveParameterArray UNDEF " & x.TypNr & "/" & x.Bezeichnung)

                    End Select
                End If
            Next
        Next

        'Datenbank-Verbindung wieder schliessen
        winback.Close()

        'Programmvariante OrgaBack
        If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
            'wenn sich die Allergen-Info geändert hat - OrgaBack Allergen-Texte updaten
            If OrgaBackUpdateAllergene Then
                'Zutatenliste in OrgaBack speichern
                MsSqldbUpdate_Zutatenliste(OrgasoftMain)
                'Update-Flag für alle Artikel setzen, deren Rezepte diesen Rohstoff enthalten
                MySQLdbSetMarkerRzptListe(wb_Global.ArtikelMarker.nwtUpdate)
            End If
            'Datenbank-Verbindung wieder schliessen
            OrgasoftMain.Close()
        End If
    End Sub

    Public Sub LoadData(dataGridView As wb_DataGridView)
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
        KA_Rz_Nr = wb_Functions.StrToInt(dataGridView.Field("KA_Rz_Nr"))
        KA_Grp1 = wb_Functions.StrToInt(dataGridView.Field("KA_Grp1"))
        KA_Grp2 = wb_Functions.StrToInt(dataGridView.Field("KA_Grp2"))
        KA_Charge_Opt_kg = dataGridView.Field("KA_Charge_Opt_kg")
        KA_zaehlt_zu_RZ_Gesamtmenge = dataGridView.Field("KA_zaehlt_zu_RZ_Gesamtmenge")
        KA_zaehlt_zu_NWT_Gesamtmenge = dataGridView.Field("KA_zaehlt_zu_NWT_Gesamtmenge")
        KA_aktiv = wb_Functions.StrToInt(dataGridView.Field("KA_aktiv"))
        KA_alternativ_RS = dataGridView.Field("KA_alternativ_RS")
    End Sub

    Friend Function SaveData(dataGridView As wb_DataGridView) As Boolean
        'Rohstoff-Detaildaten wurden geändert
        If _DataHasChanged Then
            dataGridView.Field("KO_Nr_AlNum") = KO_Nr_AlNum
            dataGridView.Field("KO_Bezeichnung") = wb_Functions.Truncate(KO_Bezeichnung, 60)
            dataGridView.Field("KO_Kommentar") = wb_Functions.Truncate(KO_Kommentar, 50, True)
            dataGridView.Field("KA_Preis") = KA_Preis
            dataGridView.Field("KA_Matchcode") = KO_IdxCloud
            dataGridView.Field("KA_Rz_Nr") = KA_Rz_Nr
            dataGridView.Field("KA_Grp1") = KA_Grp1
            dataGridView.Field("KA_Grp2") = KA_Grp2
            dataGridView.Field("KA_Charge_Opt_kg") = KA_Charge_Opt_kg
            dataGridView.Field("KA_zaehlt_zu_RZ_Gesamtmenge") = KA_zaehlt_zu_RZ_Gesamtmenge
            dataGridView.Field("KA_zaehlt_zu_NWT_Gesamtmenge") = KA_zaehlt_zu_NWT_Gesamtmenge
            dataGridView.Field("KA_aktiv") = KA_aktiv
            dataGridView.Field("KA_alternativ_RS") = KA_alternativ_RS

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
            'Flag RZ-Nummer wurde geändert
            If value <> KA_Rz_Nr Then
                KA_Rz_Nr_HasChanged = True
            End If
            KA_Rz_Nr = value
            'nur wenn KA_Art nicht definiert ist (Fehler bei Niehaves 21.02.2024)
            '                                    (Fehler bei Niehaves 27.01.2025)
            If (KA_Art = wb_Global.UNDEFINED) OrElse (KO_Type = KomponTypen.KO_TYPE_ARTIKEL) Then
                'KA_Art setzen wenn eine Rezeptnummer definiert ist (Für Artikel immmer gleich Eins)
                If (KA_Rz_Nr > 0) Then
                    KA_Art = "1"
                Else
                    KA_Art = "0"
                End If
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
            _RezeptNummer = value
            'Änderung direkt in die Tabelle dbo.ArtikelHatMultiFeld schreiben
            MFFWriteDB(wb_Global.MFF_RezeptNummer, _RezeptNummer)
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
            _RezeptName = value
            'Änderung direkt in die Tabelle dbo.ArtikelHatMultiFeld schreiben
            MFFWriteDB(wb_Global.MFF_RezeptName, _RezeptName)
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
            '09.04.2020/JW Artikel-Liniengruppe(Aufarbeitungsplatz) wird nicht per Default auf 100 gesetzt
            'If (_ArtikelLinienGruppe < wb_Global.OffsetBackorte) And (_ArtikelLinienGruppe <> 0) Then
            '    _ArtikelLinienGruppe = wb_Global.OffsetBackorte
            'End If
            If _ArtikelLinienGruppe <> 0 Then
                Return _ArtikelLinienGruppe
            Else
                Return wb_Global.UNDEFINED
            End If
        End Get
        Set(value As Integer)
            _ArtikelLinienGruppe = value
            'Änderung direkt in die Tabelle dbo.ArtikelHatMultiFeld schreiben
            '2024-07-18/JW  Nur schreiben, wenn auch eine Artikel-Liniengruppe eingetragen ist!
            '               Wenn ein Leerstring geschrieben wird, interpretiert OrgaBack das als
            '               Aufarbeitung 0000
            If sArtikeLinienGruppe <> "" Then
                MFFWriteDB(wb_Global.MFF_ProduktionsLinie, sArtikeLinienGruppe)
            End If
            'TODO wenn ein Rezept in Param6 angegeben ist, muss die Rezept-Liniengruppe angepasst werden
            ktTyp300.Liniengruppe = value
        End Set
    End Property

    Public Property sArtikeLinienGruppe As String
        Get
            If iArtikelLinienGruppe >= wb_Global.OffsetBackorte Then
                Dim sValue As String = "0000" & iArtikelLinienGruppe.ToString - wb_Global.OffsetBackorte
                sValue = Right(sValue, 4)
                'Mail vom 04.02.2020 JErhardt
                '09.04.2020/JW Backort(Aufarbeitung) 100 ist zulässig - OrgaBack Aufarbeitung 0000
                'If sValue <> "0000" Then
                '   Return sValue
                'Else
                '    Return ""
                'End If
                Return sValue
            End If
            Return ""
        End Get
        Set(value As String)
            Dim iValue As Integer = wb_Functions.StrToInt(value) + wb_Global.OffsetBackorte
            If iValue <> 0 Then
                _ArtikelLinienGruppe = iValue
            End If
        End Set
    End Property

    Public Property OfenGruppe As Integer
        Get
            Return ktTyp300.OfenGruppe
        End Get
        Set(value As Integer)
            ktTyp300.OfenGruppe = value
        End Set
    End Property

    Public Property Lieferant As String
        Set(value As String)
            'Änderungen loggen (geändert 15-05-2020 Weigmann)
            'LF_Lieferant = ChangeLogAdd(LogType.Prm, Parameter.Tx_Lieferant, LF_Lieferant, value)
            LF_Lieferant = value
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

    Public Property Mehlzusammensetzung As String
        Get
            'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
            If Not KO_MehlZusammenSetzung.ReadOK Then
                KO_MehlZusammenSetzung.Read(KO_Nr)
            End If
            Return KO_MehlZusammenSetzung.Memo
        End Get
        Set(value As String)
            KO_MehlZusammenSetzung.Memo = ChangeLogAdd(LogType.Dkl, Parameter.Tx_Mehlzusammensetzung, Mehlzusammensetzung, value)
            'Datenänderung in Datenbank sichern
            KO_MehlZusammenSetzung.Write()
        End Set
    End Property

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
        Else
            'normale Komponente ohne Produktion
            _RezeptName = ""
            _RezeptNummer = ""
            _LinienGruppe = wb_Global.UNDEFINED
        End If

        'Artikel-Typ = 1 für Auto/Handkomponenten mit anhängender Rezeptur
        'TODO war auskommentiert - teilweise existieren Artikel mit KA_ART=0, diese werden nicht angezeigt!!
        '2023-05-15 Wieder auskommentiert. Fehler bei Schaufler: KA_ART=1 wird angezeigt aber nicht gespeichert !
        'If (KO_Type = KomponTypen.KO_TYPE_ARTIKEL) OrElse (RzNr > 0) Then
        '    KA_Art = 1
        'Else
        '    KA_Art = 0
        'End If

        'Default-Wert für Artikel-Liniengruppe (sonst wird immer wieder die Routine GetProduktionsDaten() aufgerufen
        _ArtikelLinienGruppe = 0
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
        End If

        If KA_Rz_Nr_HasChanged OrElse KA_Grp_HasChanged Then
            'Alle Artikel, deren Rezeptur diesen Rohstoff enthält, als geändert markieren
            MySQLdbSetMarkerRzptListe(ArtikelMarker.nwtUpdate)
        End If
    End Sub

    Public Sub ClearReport()
        ChangeLogClear()
        ktTyp301.ClearReport()
    End Sub

    Public Sub SaveReport()
        Dim Ueberschrift As String
        If Type = wb_Global.KomponTypen.KO_TYPE_ARTIKEL Then
            Ueberschrift = "Änderungen für Artikel " & Nummer & " " & Bezeichnung & " " & vbCrLf
        Else
            Ueberschrift = "Änderungen für Rohstoff " & Nummer & " " & Bezeichnung & " " & vbCrLf
        End If

        Dim Strich = New String("="c, Len(Ueberschrift)) & vbCrLf
        NwtUpdate.Memo = Ueberschrift & Strich & GetReport()
        NwtUpdate.Write()
    End Sub

    Public ReadOnly Property GetReport(Optional ReportAll As Boolean = vbFalse) As String
        Get
            Return ChangeLogReport(ReportAll) & ktTyp301.GetReport(ReportAll)
        End Get
    End Property

    Public Property Deklaration(Optional DbWrite As Boolean = True) As String
        Get
            If wb_GlobalSettings.NwtInterneDeklaration Then
                'Wenn die interne Deklarations-Bezeichnung leer ist
                If DeklBezeichungIntern = "" Then
                    'verwende die externe Deklarations-Bezeichung
                    Return DeklBezeichungExtern
                Else
                    Return DeklBezeichungIntern
                End If
            Else
                Return DeklBezeichungExtern
            End If
        End Get
        Set(value As String)
            If wb_GlobalSettings.NwtInterneDeklaration Then
                DeklBezeichungIntern(DbWrite) = value
            Else
                DeklBezeichungExtern(DbWrite) = value
            End If
        End Set
    End Property

    Public Property DeklBezeichungExtern(Optional DbWrite As Boolean = True) As String
        Get
            'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
            If Not KO_DeklBezeichnungExtern.ReadOK Then
                KO_DeklBezeichnungExtern.Read(KO_Nr)
            End If
            Return KO_DeklBezeichnungExtern.Memo
        End Get
        Set(value As String)
            KO_DeklBezeichnungExtern.Memo = ChangeLogAdd(LogType.Dkl, Parameter.Tx_DeklarationExtern, DeklBezeichungExtern, value)
            'wenn Daten geändert worden sind - wird von ChangeLogAdd ausgewertet
            If ChangeLogChanged AndAlso DbWrite Then
                'Datenänderung in Datenbank sichern
                KO_DeklBezeichnungExtern.Write()
                'Deklarations-Bezeichnung in OrgaBack DB schreiben
                If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
                    MsSqldbUpdate_Zutatenliste()
                End If
            End If

            'Alle Artikel, deren Rezeptur diesen Rohstoff enthält, als geändert markieren
            MySQLdbSetMarkerRzptListe(ArtikelMarker.nwtUpdate)
        End Set
    End Property

    Public Property DeklBezeichungIntern(Optional DbWrite As Boolean = True) As String
        Get
            'Wenn noch nicht gelesen wurde, dann erst aus DB einlesen
            If Not KO_DeklBezeichnungIntern.ReadOK Then
                KO_DeklBezeichnungIntern.Read(KO_Nr)
            End If
            Return KO_DeklBezeichnungIntern.Memo
        End Get
        Set(value As String)
            KO_DeklBezeichnungIntern.Memo = ChangeLogAdd(LogType.Dkl, Parameter.Tx_DeklarationIntern, DeklBezeichungIntern, value)
            'wenn Daten geändert worden sind - wird von ChangeLogAdd ausgewertet
            If ChangeLogChanged AndAlso DbWrite Then
                'Datenänderung in Datenbank sichern
                KO_DeklBezeichnungIntern.Write()
                'Deklarations-Bezeichnung in OrgaBack DB schreiben
                If wb_GlobalSettings.pVariante = ProgVariante.OrgaBack Then
                    MsSqldbUpdate_Zutatenliste()
                End If

                'Alle Artikel, deren Rezeptur diesen Rohstoff enthält, als geändert markieren
                MySQLdbSetMarkerRzptListe(ArtikelMarker.nwtUpdate)
            End If
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
        'TODO funktioniert das? Oder wird mit jedem Start eine neue Komponente angelegt ??
        Get
            If _ProduktionsStufe Is Nothing Then
                _ProduktionsStufe = New wb_Komponente
                If Not _ProduktionsStufe.MysqldbRead(KomponTypen.KO_TYPE_PRODUKTIONSSTUFE) Then
                    _ProduktionsStufe.KO_Type = KomponTypen.KO_TYPE_PRODUKTIONSSTUFE
                    _ProduktionsStufe.Bezeichnung = "Produktions-Stufe"
                    _ProduktionsStufe.Nummer = "PST"
                    _ProduktionsStufe.Nr = wb_sql_Functions.getNewKomponNummer()
                End If
            End If
            Return _ProduktionsStufe
        End Get
    End Property

    Public Shared ReadOnly Property Kessel As wb_Komponente
        'TODO funktioniert das? Oder wird mit jedem Start eine neue Komponente angelegt ??
        'TODO fehlt hier die Abfrag Nach NOTHING?
        Get
            _Kessel = New wb_Komponente
            If Not _Kessel.MysqldbRead(KomponTypen.KO_TYPE_KESSEL) Then
                _Kessel.KO_Type = KomponTypen.KO_TYPE_KESSEL
                _Kessel.Bezeichnung = "Kessel"
                _Kessel.Nummer = "KSL"
                _Kessel.Nr = wb_sql_Functions.getNewKomponNummer()
            End If
            Return _Kessel
        End Get
    End Property

    Public Shared ReadOnly Property TextKomponente As wb_Komponente
        'TODO funktioniert das? Oder wird mit jedem Start eine neue Komponente angelegt ??
        'TODO fehlt hier die Abfrag Nach NOTHING?
        Get
            _TextKomponente = New wb_Komponente
            If Not _TextKomponente.MysqldbRead(KomponTypen.KO_TYPE_TEXTKOMPONENTE) Then
                _TextKomponente.KO_Type = KomponTypen.KO_TYPE_TEXTKOMPONENTE
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
    ''' Muss bei der Nährwert-Berechnung mit berücksichtigt werden !
    ''' 
    ''' Ist der Backverlust nicht angegeben, wird er berechnet aus Verkaufs- und Nassgewicht!
    ''' ACHTUNG: Hier muss mit dem Wert aus KtTyp200.Verkaufsgewicht gerechnet werden - sonst stürzt das System mit Stack-Overflow ab!
    ''' 
    ''' Bei Artikeln (KO_Type=0) steht der Backverlust in RohParams 300/1  !!
    ''' Bei Rohstoffen in der Tabelle Komponenten.KO_Temp_Korr
    ''' </summary>
    ''' <returns></returns>
    Public Property Backverlust As Double
        Get
            If Type = KomponTypen.KO_TYPE_ARTIKEL Then
                'Backverlust berechnen, falls nicht angegeben
                If ktTyp300.Backverlust = 0 AndAlso ktTyp200.Verkaufsgewicht > 0 Then
                    Backverlust = 100 * (1 - ArtikelChargen.StkGewicht / (ktTyp200.Verkaufsgewicht * 1000))
                End If
                Return ktTyp300.Backverlust
            Else
                Return KO_Backverlust / 100
            End If
        End Get
        Set(value As Double)
            If Type = KomponTypen.KO_TYPE_ARTIKEL Then
                ktTyp300.Backverlust = value
            Else
                KO_Backverlust = value * 100
            End If
        End Set
    End Property

    ''' <summary>
    ''' Zuschnittverlust in %
    ''' Der Zuschnittverlust wird in der Datenbank im Feld winback.Komponenten.KA_Artikel_Typ mit Faktor 100 als Integer gespeichert.
    ''' Im Gegensatz zum Backverlust wird der Zuschnittverlust NICHT bei der Nährwert-Berechnung mit berücksichtigt!
    ''' </summary>
    ''' <returns></returns>
    Public Property Zuschnittverlust As Double
        Get
            If Type = KomponTypen.KO_TYPE_ARTIKEL Then
                Return ktTyp300.Zuschnitt
            Else
                Return CDbl(KO_Zuschnitt) / 100
            End If
        End Get
        Set(value As Double)
            If Type = KomponTypen.KO_TYPE_ARTIKEL Then
                ktTyp300.Zuschnitt = value
            Else
                KO_Zuschnitt = CInt(value * 100)
            End If
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

    Public Property Bilanzmenge(Optional Reload As Boolean = False) As String
        Get
            If _Lager Is Nothing OrElse Reload Then
                _Lager = New wb_LagerOrt(KA_Lagerort)
            End If
            Return _Lager.Bilanzmenge
        End Get
        Set(value As String)
            If _Lager IsNot Nothing Then
                _Lager.Bilanzmenge = value
            End If
        End Set
    End Property

    Public Property MindestMenge As String
        Get
            If _Lager Is Nothing Then
                _Lager = New wb_LagerOrt(KA_Lagerort)
            End If
            Return _Lager.Mindestmenge
        End Get
        Set(value As String)
            _Lager.Mindestmenge = value
        End Set
    End Property

    Public ReadOnly Property MindestmengeUnterschritten As Boolean
        Get
            Return _Lager.MindestmengeUnterschritten
        End Get
    End Property

    Public Property LagerOrtAktiv As String
        Get
            If _Lager Is Nothing Then
                _Lager = New wb_LagerOrt(KA_Lagerort)
            End If
            Return _Lager.Aktiv
        End Get
        Set(value As String)
            _Lager.Aktiv = value
        End Set
    End Property

    Public ReadOnly Property SiloNummer As String
        Get
            Return _Lager.SiloNummer
        End Get
    End Property

    ''' <summary>
    ''' Verkaufsgewicht in kg(!) aus OrgBack dbo.HandelsArtikel.Gewicht
    ''' Wird benötigt zur Berechnung des Nassgewichts in WinBack aus 
    ''' Verkaufsgewicht, Zuschnitt-Verlust, Backverlust
    ''' </summary>
    ''' <returns></returns>
    Public Property VerkaufsGewicht As Double
        Get
            'Daten aus den Komponenten-Parametern
            If ktTyp200.Verkaufsgewicht > wb_Global.UNDEFINED Then
                Return ktTyp200.Verkaufsgewicht
            Else
                'Verkaufsgewicht berechnet sich aus Nassgewicht und Backverlust
                'TODO Zuschnitt berücksichtigen
                If Backverlust > 0 AndAlso Backverlust <= 100 Then
                    Return (ArtikelChargen.StkGewicht * (1 - Backverlust / 100)) / 1000
                Else
                    Return ArtikelChargen.StkGewicht / 1000
                End If
            End If
        End Get
        Set(value As Double)
            'In Datenbank speichern
            ktTyp200.Verkaufsgewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Verkaufsgewicht in kg(!) aus OrgBack dbo.HandelsArtikel.Gewicht 
    ''' Wird benötigt zur Berechnung der Nährwerte vor der Übertragung
    ''' in OrgaBack.
    ''' Die Nährwerte in OrgaBack sind bezogen auf das Gewicht (abhängig von der Einheit) gespeichert und NICHT bezogen auf 100gr
    ''' Deshalb muss vor dem Speichern der Daten das rechnerische Verkaufsgewicht aus der Datenbank ermittelt werden!
    ''' Die Angaben in GetPropertyValue("NettoInhalt") sind nicht richtig
    ''' </summary>
    ''' <returns></returns>
    Public Property CalcVerkaufsGewicht As Double
        Get
            'Daten aus den Komponenten-Parametern
            If _CalcVerkaufsgewicht > wb_Global.UNDEFINED Then
                Return _CalcVerkaufsgewicht
            Else
                Return ktTyp200.Verkaufsgewicht
            End If
        End Get
        Set(value As Double)
            _CalcVerkaufsgewicht = value
        End Set
    End Property

    ''' <summary>
    ''' Wenn in der Tabelle [dbo].[StuecklistenVariante] ein Verkaufsgewicht für eine Einheit/Variante steht,
    ''' dann wird der Default-Eintrag (Nettogewicht) mit diesem Wert überschrieben
    '''
    ''' Sonst funktioniert die Nährwert-Berechnung in OrgaBack (Übertragung aus WinBack) nicht!
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCalcVerkaufsgewichtFromStkLstVariante(Unit As String) As Boolean
        Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        Dim sql As String
        Dim Result As Boolean = False

        'Suche nach Eintrag in der Tabelle dbo.HandelsArtikel
        sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlHandelsArtikelGewicht, Nummer, Unit)
        If orgaback.sqlSelect(sql) AndAlso orgaback.Read Then
            Dim NettoInhalt As Integer = orgaback.iField("NettoInhalt")
            'wenn ein gültiges Gewicht [in Gramm] eingetragen ist, wird das Verkaufsgewicht [kg] entsprechend korrigiert
            If NettoInhalt > 0 Then
                CalcVerkaufsGewicht = NettoInhalt / 1000
                Result = True
            End If
        End If
        orgaback.CloseRead()

        'Suche nach Eintrag in der Tabelle dbo.StuecklistenVariante
        sql = wb_sql_Selects.setParams(wb_sql_Selects.mssqlStkListeGewicht, Nummer, Unit)
        If orgaback.sqlSelect(sql) AndAlso orgaback.Read Then
            Dim Gewicht As Integer = orgaback.iField("Gewicht")
            'wenn ein gültiges Gewicht [in Gramm] eingetragen ist, wird das Verkaufsgewicht [kg] entsprechend korrigiert
            If Gewicht > 0 Then
                CalcVerkaufsGewicht = Gewicht / 1000
                Result = True
            End If
        End If

        'Datenbank immer schliessen
        orgaback.Close()
        Return Result
    End Function

    Public ReadOnly Property Lagerort As String
        Get
            Return KA_Lagerort
        End Get
    End Property

    ''' <summary>
    ''' Pfad und Dateiname Artikel-Bild
    ''' 
    ''' OrgaBack    - Dateiname aus dbo.ArtikelBild
    '''             - Pfad aus Settings
    '''             
    ''' WinBack     - Dateiname aus Winback.RohParams (200,2)
    '''             - Pfad aus winback.ini
    ''' </summary>
    ''' <returns></returns>
    Public Property Bild As String
        Get
            If wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack Then
                'TODO Datei aus msSQL auslesen
                Return "C:\Dokumente\Projekte\WinBackOffice\InfoTerminal\img\sonnenblumenbrot.jpg"
            Else
                'TODO Test
                Return "C:\Dokumente\Projekte\WinBackOffice\InfoTerminal\img\sonnenblumenbrot.jpg"
                Return wb_GlobalSettings.pPicturePath & DateinameBild
            End If
        End Get
        Set(value As String)
            'TODO Pfad-Information abschneiden - Bildinfo in DateinameBild speichern
        End Set
    End Property

    ''' <summary>
    ''' WinBack-Filename Artikel-Bild.
    ''' Der Pfad wird User-Global eingestellt und steht in der winback.ini
    ''' </summary>
    ''' <returns></returns>
    Public Property DateinameBild As String
        Get
            Return ktTyp200.DateinameBild
        End Get
        Set(value As String)
            ktTyp200.DateinameBild = value
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
    Public Sub UpdateDB(Optional UpdateAll As Boolean = True)
        'geänderten Datensatz(Stammdaten) in WinBack-DB schreiben
        MySQLdbUpdate(UpdateAll)
        'schreibt auch die Artikel-Chargen-Daten
        ArtikelChargen.HasChanged = False

        'geänderte Komponentendaten(Rezeptur) in WinBack-DB schreiben
        If TeigChargen.HasChanged Then
            SaveProduktionsDaten()
            TeigChargen.HasChanged = False
        End If

        MySQLdbUpdate_Parameter(wb_Global.ktParam.kt200)
        MySQLdbUpdate_Parameter(wb_Global.ktParam.kt300)
        MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
    End Sub

    ''' <summary>
    ''' Löscht alle Einträge in der Tabelle Lieferungen. Setzt die Bilanzmenge auf Null.
    ''' Update Tabelle Lagerorte.
    ''' Erzeugt den ersten Datensatz in der Tabelle Lieferungen (Null setzen)
    ''' 
    ''' ACHTUNG:    Wenn Halbprodukte in WinBack hergestellt werden und die interne Chargen-Nummer weiter verwendet
    '''             werden soll, muss als erster Datensatz in der Tabelle Lieferungen eine abgeschlossene Zeile (Nullsetzen)
    '''             stehen. Sonst werden von WinBack-Produktion fehlerhafte Chargen eingebucht !!
    ''' </summary>
    Public Sub InitLieferungen()
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Bilanzmenge auf Null setzen
        Bilanzmenge = "0"
        sql = "LG_Bilanzmenge = '0'"
        'Tabelle Lagerorte
        winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlUpdateLagerOrte, Lagerort, sql))
        'alle Datensätze in Tabelle Lieferungen löschen
        winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlDelLieferungen, Lagerort))
        'erster Eintrag in Tabelle Lieferungen (notwendig für WinBack-Produktion)
        sql = "'" & Lagerort & "', 0, '" & wb_sql_Functions.MySQLdatetime(Now) & "', " & "'0', '" & wb_GlobalSettings.AktUserName &
              "', '3', 'Null setzen', 0, NULL, NULL, NULL, 0, " & wb_GlobalSettings.AktUserNr & ", '0.000', 0"

        'INSERT ausführen
        winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlInsertWE, sql))


        'Datenbank-Verbindung wieder schliessen
        winback.Close()
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
            sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlSelectKomp_KO_Nr, InterneKomponentenNummer)
        Else
            sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlSelectKomp_AlNum, KomponentenNummer)
        End If

        'ersten Datensatz aus Tabelle Komponenten lesen
        If Not winback.sqlSelect(sql) Then
            winback.Close()
            Return True
        Else
            If Not winback.Read Then
                winback.Close()
                'Debug.Print("Datensatz nicht gefunden - Löschen freigegeben")
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

        'Debug.Print("Anfrage Löschen Komponente " & KO_Nr & "/" & KO_Nr_AlNum)

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
        Dim sql As String = wb_sql_Selects.setParams(wb_sql_Selects.sqlKompInArbRzp, InterneKomponentenNummer)
        Dim Count As Integer = -1

        'Suche nach KO_Nr
        If winback.sqlSelect(sql) AndAlso winback.Read Then
            Count = winback.iField("Used")
        End If
        'Datenbank wieder schliessen
        winback.Close()
        'Debug.Print("MySQLIsUsedInProduction " & Count.ToString)

        'Löschen erlaubt, wenn die Anzahl der Datensätze gleich Null ist
        Return (Count <> 0)
    End Function

    ''' <summary>
    ''' Ermittelt die Anzahl der Datensätze in der Tabelle winback.RezeptSchritte mit der übergebenen Komponenten-Nummer
    ''' Ist die Anzahl der Datensätze gleich Null, wird True zurückgegeben sonst False.
    ''' </summary>
    ''' <param name="InterneKomponentenNummer">Integer - Interne Komponenten-Nummer</param>
    ''' <returns>Boolean - Löschen ist erlaubt</returns>
    Public Function MySQLIsUsedInRecipe(InterneKomponentenNummer) As Boolean
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String = wb_sql_Selects.setParams(wb_sql_Selects.sqlKompInRezept, InterneKomponentenNummer)
        Dim Count As Integer = -1

        'Suche nach KO_Nr
        If winback.sqlSelect(sql) AndAlso winback.Read Then
            Count = winback.iField("Used")
        End If
        'Datenbank wieder schliessen
        winback.Close()
        'Debug.Print("MySQLIsUsedInRecipe " & Count.ToString)

        'Löschen erlaubt, wenn die Anzahl der Datensätze gleich Null ist
        Return (Count <> 0)
    End Function

    ''' <summary>
    ''' Markiert die aktuelle Komponente (Update Nährwert-Info notwendig oder Nährwertinfo fehlerhaft)
    ''' </summary>
    ''' <param name="Marker"></param>
    Public Sub MySQLdbSetMarker(Marker As wb_Global.ArtikelMarker)
        'Interne Komponenten-Nummer muss definiert sein
        If KO_Nr > 0 Then
            'Update Komponente in winback.Komponenten
            wb_Rohstoffe_Shared.MySQLdbSetMarker(Marker, KO_Nr)
        End If
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
            Dim sql As String = wb_sql_Selects.setParams(wb_sql_Selects.sqlKompSetMarkerRzListe, KO_Nr)
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
                winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlKompSetMarkerRzNr, RzNr, Marker))
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
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlDelKomponenten, KO_Nr))
            'Löschen Komponente in winback.KomponParams
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlDelKomponParams, KO_Nr))
            'Löschen Komponente in winback.Hinweise2
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlDelKompHinweise, KO_Nr))
            'Löschen Komponente in winbackRohParams
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlDelRohParams, KO_Nr))

            'Der Lagerort muss definiert sein
            If KA_Lagerort <> "" Then
                'Löschen winback.LagerOrte
                winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlDelLagerOrte, KA_Lagerort))
                'Löschen winback.Lieferungen
                winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlDelLieferungen, KA_Lagerort))
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
            'Artikel
            KA_Art = "1"
        Else
            'Rohstoff
            KA_Art = "0"
        End If

        'Datensatz neu anlegen
        winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlAddNewKompon, KO_Nr, KO_Nr_AlNum, wb_Functions.KomponTypeToInt(KO_Type), "Neu angelegt " & Date.Now))
        'Rohstoff-Handkomponente - Lagerort neu anlegen
        If KType = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE Then

            'Datensatz in winback.Lagerorte anlegen
            KA_Lagerort = "KT102_" & KO_Nr.ToString
            winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlInsertLagerOrte, KA_Lagerort))

            'Datensatz in winback.KomponParams anlegen
            ktTypXXX.Wert(T102_TolMinus) = "0,020"
            ktTypXXX.Wert(T102_TolPlus) = "0,020"
            ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_SollMenge, winback)
            ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_SollProzent, winback)
            ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_TolMinus, winback)
            ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_TolPlus, winback)
            ktTypXXX.MySQLdbUpdate(KO_Nr, wb_Global.T102_TolProzent, winback)
        End If

        winback.Close()
        'neuen KompNummer zurückgeben
        Return KO_Nr
    End Function

    ''' <summary>
    ''' Liest alle Datenfelder zu der angegebenen Komponenten-Nummer aus der WinBack-Datenbank. Wenn die interne Komponenten-Nummer nicht angegeben ist
    ''' (Kleiner oder gleich Null) dann wird versucht, anhand der Artikel-Nummer den Datensatz zu finden.
    ''' 
    ''' Gibt True zurück, wenn der Datensatz gefunden wurde.
    ''' Wenn das Read-Kommando von der OrgaBack-Artikelverwaltung kommt wird die interne KoNr bei Automatik-Komponenten ignoriert (Silo-Zuordnung!)
    ''' damit Alternativ-Silos in WinBack verwaltet werden können.
    ''' In diesem Fall wird die erste Automatik-Komponente gelesen (sortiert nach Lagerort)
    ''' </summary>
    Public Function xMySQLdbRead(InterneKomponentenNummer As Integer, Optional KomponentenNummer As String = "", Optional OrgaBackRead As Boolean = False) As Boolean
        'Alle (eventuell noch) bestehenden Daten löschen
        Me.Invalid()

        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Suche nach KO_Nr oder KO_AlNum
        If InterneKomponentenNummer > 0 Then
            sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlSelectKomp_KO_Nr, InterneKomponentenNummer)
        Else
            sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlSelectKomp_AlNum, KomponentenNummer)
        End If

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                MySQLdbRead(winback.MySqlRead)

                'Sonderfall Automatik-Komponente oder Artikelnummern nicht identisch - INTERNE KOMPONENTEN-NR IGNORIEREN
                If OrgaBackRead Then
                    If (InterneKomponentenNummer > 0) AndAlso ((KO_Type = KomponTypen.KO_TYPE_AUTOKOMPONENTE) OrElse (Nummer <> KomponentenNummer)) Then
                        winback.Close()
                        'Suche nach alphanumerischer Nummer
                        Dim Result As Boolean = xMySQLdbRead(0, KomponentenNummer)
                        Return Result
                    End If
                End If

                'weitere Parameter einlesen - Tabelle KomponParams(Parameter Produktion)
                winback.CloseRead()
                sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlKomponParamsXXX, Nr)
                If winback.sqlSelect(sql) AndAlso winback.Read Then
                    MySQLdbRead(winback.MySqlRead)
                End If
                winback.CloseRead()

                'weitere Parameter einlesen - Tabelle RohParams(erweiterte Parameter/Nährwerte)
                sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlRohParamsXXX, Nr)
                If winback.sqlSelect(sql) AndAlso winback.Read Then
                    MySQLdbRead(winback.MySqlRead)
                End If
                winback.Close()
                Return True

            Else
                'Sonderfall - Es wurde eine interne Komponenten-Nummer angegeben die nicht gefunden wurde
                'Rohstoff/Artikel wurde gelöscht (in WinBack)
                If (InterneKomponentenNummer > 0) AndAlso (KomponentenNummer <> "") Then
                    'bestehende Verbindung schliessen
                    winback.Close()
                    'Suche nach alphanumerischer Nummer
                    Dim Result As Boolean = xMySQLdbRead(0, KomponentenNummer)
                    Return Result
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
        sql = wb_sql_Selects.setParams(wb_sql_Selects.sqlSelectKomp_KO_Type, wb_Functions.KomponTypeToInt(KomponType))

        'ersten Datensatz aus Tabelle Komponenten lesen
        If winback.sqlSelect(sql) AndAlso winback.Read Then
            MySQLdbRead(winback.MySqlRead)
            winback.Close()
            Return True
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
                If sqlReader.GetDataTypeName(i) <> "TIMESTAMP" Then
                    MySQLdbRead_StammDaten(sqlReader.GetName(i), sqlReader.GetValue(i))
                End If
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
                    If sqlReader.GetDataTypeName(i) <> "TIMESTAMP" Then
                        MySQLdbRead_Parameter(sqlReader.GetName(i), sqlReader.GetValue(i))
                    End If
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
                'Zuschnittverlust(Rezept im Rezept)
                Case "KA_Artikel_Typ"
                    KO_Zuschnitt = wb_Functions.StrToInt(Value)
                'Produktions-Vorlauf in [h]
                Case "KA_Prod_Linie"
                    KA_ProdVorlauf = Value
                'Dateiname Verabeitungshinweis Artikel (pdf/html)
                Case "KA_Verarbeitungshinweise"
                    KA_Verarbeitungshinweise = Value
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
                'Flag Nährwert-Berechnung OK/fehlerhaft/update
                Case "KA_PreisEinheit"
                    KA_PreisEinheit = Value
                'alternativ Rohstoff
                Case "KA_alternativ_RS"
                    KA_alternativ_RS = Value

                'Stückgewicht in Gramm
                Case "KA_Stueckgewicht"
                    'If Type = KomponTypen.KO_TYPE_ARTIKEL Then
                    ArtikelChargen.StkGewicht = Value
                    'End If

                'Flag zählt zum Rezeptgewicht
                Case "KA_zaehlt_zu_RZ_Gesamtmenge"
                    KA_zaehlt_zu_RZ_Gesamtmenge = Value
                'Flag zählt zum NWT-Gewicht
                Case "KA_zaehlt_zu_NWT_Gesamtmenge"
                    KA_zaehlt_zu_NWT_Gesamtmenge = Value

            End Select

            'Artikel - Chargengrößen in Stück
            If Type = KomponTypen.KO_TYPE_ARTIKEL OrElse Type = KomponTypen.KO_TYPE_METER OrElse Type = KomponTypen.KO_TYPE_STUECK Then

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

            'Rohstoffe - Chargengrößen in kg (wenn mit Rezept verknüpft)
            If Type = KomponTypen.KO_TYPE_HANDKOMPONENTE OrElse Type = KomponTypen.KO_TYPE_AUTOKOMPONENTE OrElse Type = KomponTypen.KO_TYPE_METER OrElse Type = KomponTypen.KO_TYPE_STUECK Then

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
                        KA_Charge_Opt_kg = Value
                End Select
            End If

        Catch ex As Exception
            Trace.WriteLine("I@_Fehler beim Lesen der Stammdaten")
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


                'TODO !!!!!
                'REMEMBER Me - HIER STIMMT WAS GRUNDSÄTZLICH NICHT !!!
                'KomponParams und ROHPARAMS UNTERSCHEIDEN

            'Parameter-Wert(RohParams)
            Case "RP_Wert"
                Select Case ParamTyp
                    Case Int(200)
                        'Produktinformationen
                        ktTyp200.Wert(ParamNr) = Value.ToString
                    Case Int(201)
                        'Verarbeitungs-Hinweise
                        ktTyp201.Wert(ParamNr) = Value.ToString
                    Case Int(202)
                        'Kalkulation/Preise
                        ktTyp202.Wert(ParamNr) = Value.ToString
                    Case Int(210)
                        'Froster
                        ktTyp210.Wert(ParamNr) = Value.ToString
                    Case Int(220)
                        'Teig-Gare
                        ktTyp220.Wert(ParamNr) = Value.ToString
                    Case Int(300)
                        'Parameter Produktion
                        ktTyp300.Wert(ParamNr) = Value.ToString
                    Case Int(301)
                        'Nährwert-Informationen
                        ktTyp301.Wert(ParamNr) = Value.ToString
                    Case Int(303)
                        'EU/Bio-Verband
                        ktTyp303.Wert(ParamNr) = Value.ToString
                End Select

            'Parameter-Wert(KomponParams)
            Case "KP_Wert"
                ktTypXXX.Wert(ParamNr) = Value.ToString

                'TimeStamp
                'TODO WIRD DAS HIER RICHTIG EINGELESEN ??
                'BREAK
            Case "RP_Timestamp"
                Select Case ParamTyp
                    Case Int(301)
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
    Public Function MySQLdbUpdate(Optional UpdateAll As Boolean = True) As Boolean
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_GlobalSettings.SqlConWinBack, wb_Sql.dbType.mySql)
        Dim sql As String

        'Update-Statement wird dynamisch erzeugt 

        If UpdateAll Then
            sql = "KO_Nr_AlNum = '" & Nummer & "'," &
                  "KO_Type = '" & wb_Functions.KomponTypeToInt(Type) & "'," &
                  "KO_Bezeichnung = '" & wb_Functions.Truncate(wb_Functions.XRemoveSonderZeichen(Bezeichnung), 60) & "'," &
                  "KO_Kommentar = '" & wb_Functions.Truncate(wb_Functions.XRemoveSonderZeichen(Kommentar), 50, True) & "'," &
                  "KO_Temp_Korr = '" & KO_Backverlust & "'," &
                  "KA_Artikel_Typ = " & KO_Zuschnitt & "," &
                  "KA_Prod_Linie = " & KA_ProdVorlauf & "," &
                  "KA_Matchcode = '" & KO_IdxCloud & "'," &
                  "KA_Lagerort = '" & KA_Lagerort & "'," &
                  "KA_Stueckgewicht = '" & ArtikelChargen.StkGewicht & "'," &
                  "KA_Art = '" & KA_Art & "'," &
                  "KA_PreisEinheit = " & KA_PreisEinheit.ToString & "," &
                  "KA_Verarbeitungshinweise = '" & wb_Functions.Truncate(wb_Functions.XRemoveSonderZeichen(KA_Verarbeitungshinweise), 100, True) & "'," &
                  "KA_alternativ_RS = '" & KA_alternativ_RS & "'"
        Else
            sql = "KO_Temp_Korr = '" & KO_Backverlust & "'," &
                  "KA_Artikel_Typ = " & KO_Zuschnitt & "," &
                  "KA_Prod_Linie = " & KA_ProdVorlauf & "," &
                  "KA_Stueckgewicht = '" & ArtikelChargen.StkGewicht & "'," &
                  "KA_Verarbeitungshinweise = '" & wb_Functions.Truncate(wb_Functions.XRemoveSonderZeichen(KA_Verarbeitungshinweise), 100, True) & "'," &
                  "KA_alternativ_RS = '" & KA_alternativ_RS & "'"
        End If

        'Rezeptnummer nur updaten wenn gültig
        If KA_Rz_Nr <> wb_Global.UNDEFINED Then
            sql = sql & "," & "KA_RZ_Nr = " & KA_Rz_Nr.ToString
        End If

        'Die Zuordnung Rezept zu Artikel/Rohstoff wurde geändert
        If KA_Rz_Nr_HasChanged Then

            'Wenn die Zuordnung zum Rezept gelöscht wurde müssen auch alle Parameter zurückgesetzt werden
            If KA_Rz_Nr = 0 Then
                'alle Nährwerte löschen/auf Null setzen
                ktTyp301.Clear()
                ktTyp301.MySQLdbUpdate(Nr, winback)

                'Deklarationsbezeichnungen löschen
                DeklBezeichungExtern = ""
                DeklBezeichungIntern = ""
                Mehlzusammensetzung = ""
            Else
                'Rohstoff-Gruppe der Komponente auf Null setzen
                Gruppe1 = 0
                Gruppe2 = 0

                'Parameter/Nährwerte neu berechnen (auf Basis der neu zugeordneten Rezeptur)
                'Rezept mit allen Rezeptschritten lesen (NoMessage=True unterdrückt die Meldung "Rezept verweist auf sich selbst")
                Dim Rzpt As New wb_Rezept(KA_Rz_Nr, Nothing, Backverlust, 1, "", "", True, False)

                'Änderungs-Log löschen
                ClearReport()
                'Nährwert-Information berechnen
                ktTyp301 = Rzpt.KtTyp301
                Dim FlagAufloesen As Boolean = Deklaration.StartsWith(wb_Global.FlagAufloesen)

                'Zutatenliste erzeugen
                Deklaration(False) = wb_Functions.XRemoveSonderZeichen(Rzpt.ZutatenListe(wb_GlobalSettings.NwtENummerZutatenListe, wb_GlobalSettings.NwtCalcQuid, wb_GlobalSettings.NwtOptimizeZutatenListe, True), True)
                'Mehlzusammensetzung berechnen
                Mehlzusammensetzung = Rzpt.MehlZusammensetzung(wb_Global.TrennzMehlAnteil)
                'Änderungen der Nährwerte in Komponente(Rohstoff) sichern
                MySQLdbUpdate_Parameter(wb_Global.ktParam.kt301)
            End If

            'Änderungen der Komponenten-Parameter(Rohstoff) in OrgaBack-DB schreiben
            'Gibt true zurück, wenn der Artikel in OrgaBack existiert
            If MsSQLdbUpdate_Parameter(wb_Global.ktParam.kt301) Then
                'Zutaten-und Allergenliste in OrgaBack updaten
                MsSqldbUpdate_Zutatenliste()

                'Flag Meldung Update Nährwerte anzeigen
                If wb_GlobalSettings.ShowMsg_UpdateOrgaBackNWT Then
                    'Die Daten sind in OrgaBack erst nach Laden des Artikels sichtbar
                    If MsgBox("Die aktualisierten Nährwerte und Allergen" & vbCrLf & "sind erst nach erneutem Aufruf des Artikels in OrgaBack sichtbar" &
                          vbCrLf & vbCrLf & "Diese Meldung beim nächsten Mal nicht mehr anzeigen?", MsgBoxStyle.YesNo, "WinBack-AddIn") = MsgBoxResult.Yes Then
                        'Meldung beim nächsten Mal nicht mehr anzeigen
                        wb_GlobalSettings.ShowMsg_UpdateOrgaBackNWT = False
                    End If
                End If

            End If
        End If

        'Artikel - Chargengrößen in Stk
        If (Type = wb_Global.KomponTypen.KO_TYPE_ARTIKEL) OrElse
           (FreigabeProduktion AndAlso (Type = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE OrElse Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE)) OrElse
           (FreigabeProduktion AndAlso (Type = wb_Global.KomponTypen.KO_TYPE_METER OrElse Type = wb_Global.KomponTypen.KO_TYPE_STUECK)) Then
            sql = sql & "," &
                        "KA_Charge_Min = '" & ArtikelChargen.MinCharge.MengeInStk & "'," &
                        "KA_Charge_Max = '" & ArtikelChargen.MaxCharge.MengeInStk & "'," &
                        "KA_Charge_Opt = '" & ArtikelChargen.OptCharge.MengeInStk & "'"
        End If

        'Rohstoffe - Chargengrößen in kg
        If Type = wb_Global.KomponTypen.KO_TYPE_HANDKOMPONENTE OrElse Type = wb_Global.KomponTypen.KO_TYPE_AUTOKOMPONENTE OrElse
           Type = wb_Global.KomponTypen.KO_TYPE_METER OrElse Type = wb_Global.KomponTypen.KO_TYPE_STUECK Then
            sql = sql & "," &
                        "KA_Charge_Min_kg = '" & ArtikelChargen.MinCharge.MengeInkg & "'," &
                        "KA_Charge_Max_kg = '" & ArtikelChargen.MaxCharge.MengeInkg & "'," &
                        "KA_Charge_Opt_kg = '" & ArtikelChargen.OptCharge.MengeInkg & "'"
        End If

        'Update ausführen
        If winback.sqlCommand(wb_sql_Selects.setParams(wb_sql_Selects.sqlUpdateKomp_KO_Nr, Nr, sql)) Then
            _DataHasChanged = False
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
        Dim Result As Boolean = True

        'Update Parameter-200 (Parameter Verkauf)
        If ktTyp = wb_Global.ktParam.kt200 OrElse ktTyp = wb_Global.ktParam.ktAlle Then
            If Not ktTyp200.MySQLdbUpdate(Nr, winback) Then
                Result = False
            End If
        End If

        'Update Parameter-300 (Parameter Produktion)
        If ktTyp = wb_Global.ktParam.kt300 OrElse ktTyp = wb_Global.ktParam.ktAlle Then
            If Not ktTyp300.MySQLdbUpdate(Nr, winback) Then
                Result = False
            End If
        End If

        'Update Parameter-301 (Nährwerte)
        If ktTyp = wb_Global.ktParam.kt301 OrElse ktTyp = wb_Global.ktParam.ktAlle Then
            If Not ktTyp301.MySQLdbUpdate(Nr, winback) Then
                Result = False
            End If
        End If

        'Update Parameter-303 (EU/Bio-Verband)
        If ktTyp = wb_Global.ktParam.kt303 OrElse ktTyp = wb_Global.ktParam.ktAlle Then
            If Not ktTyp303.MySQLdbUpdate(Nr, winback) Then
                Result = False
            End If
        End If

        'Datenbank-Verbindung wieder schliessen
        winback.Close()
        Return Result
    End Function

    ''' <summary>
    ''' Sichert die Zutatenliste in der Datenbank. Gesichert wird die externe und die interne Deklarations-Bezeichnung
    ''' </summary>
    ''' <returns></returns>
    Public Function MySqldbUpdate_Zutatenliste() As Boolean
        Return KO_DeklBezeichnungExtern.Write() AndAlso KO_DeklBezeichnungIntern.Write()
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
        'Umrechnungs-Faktor Nährwerte (Netto-Gewicht Artikel)
        ktTyp301.FaktorStkGewicht = Me.CalcVerkaufsGewicht * 10

        'Prüfen ob der Artikel in OrgaBack existiert
        OrgasoftMain.sqlSelect(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlSelArtikel, KO_Nr_AlNum))
        If Not OrgasoftMain.Read Then
            'Artikel nicht gefunden in OrgaSoft
            'Debug.Print("Artikel " & Nummer & " nicht in OrgaBack gefunden")
            ChangeLogAdd(LogType.Err, Nr, "", "Artikel/Komponente nicht in OrgaBack gefunden")
            OrgasoftMain.Close()
            Return False
        Else
            'Standard-Einheit aus Artikelstamm OrgaBack
            Dim StdEinheit As Integer = OrgasoftMain.iField("StdEinheit")
            'wenn beide Einheiten identisch sind, können die Parameter geschreiben werden
            If StdEinheit = wb_Einheiten_Global.GetobEinheitNr(Einheit) Then
                'Lesen beendet
                OrgasoftMain.CloseRead()

                'Update Parameter-301 (Nährwerte)
                If ktTyp = wb_Global.ktParam.kt301 OrElse ktTyp = wb_Global.ktParam.ktAlle Then
                    ktTyp301.MsSQLdbUpdate(KO_Nr_AlNum, wb_Einheiten_Global.GetobEinheitNr(Einheit), OrgasoftMain)
                    ChangeLogAdd(LogType.Msg, Nr, "", "Update der Nährwerte/Allergene in OrgaBack-DB - Nr " & KO_Nr_AlNum)
                End If

                'Update Parameter-303 (EU/Bio-Verband)
                If ktTyp = wb_Global.ktParam.kt303 OrElse ktTyp = wb_Global.ktParam.ktAlle Then
                    ktTyp303.MsSQLdbUpdate(KO_Nr_AlNum, wb_Einheiten_Global.GetobEinheitNr(Einheit), OrgasoftMain)
                    ChangeLogAdd(LogType.Msg, Nr, "", "Update der Bio-Verband-Information in OrgaBack-DB - Nr " & KO_Nr_AlNum)
                End If
                'Lesen beendet
                OrgasoftMain.CloseRead()

            Else
                'Debug.Print("Einheitenkonflikt Artikel beim Schreiben der Parameter in OrgaBack " & KO_Nr_AlNum & " " & KO_Bezeichnung)
                ChangeLogAdd(LogType.Err, Nr, "", "Einheitenkonflikt Artikel beim Schreiben der Parameter in OrgaBack - Nr " & KO_Nr_AlNum)
                Return False
            End If
        End If

        'Verbindung zur Datenbank wieder schliessen
        OrgasoftMain.Close()
        Return True
    End Function

    ''' <summary>
    ''' Schreibt den Wert aus Value in das Artikel-MFF in OrgaBack. Die Daten werden direkt in die Tabelle dbo.ArtikelHatMultiFeld geschrieben.
    ''' Die Filial-Nummer ist immer 0
    ''' </summary>
    ''' <param name="MFF"></param>
    ''' <param name="Value"></param>
    Private Sub MFFWriteDB(MFF As Integer, Value As String)
        'Schreiben MFF nur OrgaBack
        If (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OrgaBack) OrElse (wb_GlobalSettings.pVariante = wb_Global.ProgVariante.OBServerTask) Then
            Dim orgaback As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)

            'Falls der Befehl UPDATE fehlschlägt wird mit INSERT ein neuer Datensatz angelegt
            If orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlUpdateArtikelMFF, Nummer, MFF.ToString, "0", Value)) <= 0 Then
                'Datensatz mit INSERT schreiben
                orgaback.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlInsertArtikelMFF, Nummer, MFF.ToString, "0", Value), False, False)
            End If
            orgaback.Close()
        End If
    End Sub

    ''' <summary>
    ''' Schreibt die Deklarationstexte in die OrgaBack-Datenbank(Zugriff über KO_Nr_AlNum!)
    ''' Die Daten werden nur geschrieben, wenn sie nicht fixiert sind. Vor dem Schreiben erfolgt immer
    ''' ein Lesezugriff.
    ''' </summary>
    ''' <returns></returns>
    Public Function MsSqldbUpdate_Zutatenliste() As Boolean
        'Datenbank-Verbindung öffnen - MsSQL
        Dim OrgasoftMain As New wb_Sql(wb_GlobalSettings.OrgaBackMainConString, wb_Sql.dbType.msSql)
        'Update Zutatenliste
        Return MsSqldbUpdate_Zutatenliste(OrgasoftMain)
        'Verbindung zur Datenbank wieder schliessen
        OrgasoftMain.Close()
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
    ''' <param name="OrgasoftMain"></param>
    ''' <returns></returns>
    Public Function MsSqldbUpdate_Zutatenliste(OrgasoftMain As wb_Sql) As Boolean
        'Default Rückgabewert
        Dim Result As Boolean = False
        'Update-Statement wird dynamisch erzeugt    
        Dim sql As String
        'Zutaten- und Allergenlisten
        Dim ZutatenListe As String
        Dim AllergenListeEnthalten As String
        Dim AllergenListeSpuren As String
        Dim AllergenKurzListeEnthalten As String
        Dim AllergenKurzListeSpuren As String

        'Daten aus dbo.ArtikelDeklarationstexte lesen (Artikelnummer/Variante 0/Ländercode/Sprachencode)
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.sqlReadDeklaration, KO_Nr_AlNum, 0, wb_GlobalSettings.osLaendercode, wb_GlobalSettings.osSprachcode)
        OrgasoftMain.sqlSelect(sql)
        'wenn schon ein Datensatz vorhanden ist, Flags einlesen (Fixiert)
        If OrgasoftMain.Read Then
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
            Result = OrgasoftMain.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlUpdateDeklaration, sql, KO_Nr_AlNum, 0, wb_GlobalSettings.osLaendercode, wb_GlobalSettings.osSprachcode))
        Else
            'Verbindung Lesen schliessen
            OrgasoftMain.CloseRead()
            'noch kein Datensatz vorhanden - Prüfen ob überhaupt ein Artikel mit dieser Nummer in OrgaBack existiert
            sql = wb_Sql_Selects.setParams(wb_Sql_Selects.mssqlSelArtikel, KO_Nr_AlNum)
            OrgasoftMain.sqlSelect(sql)
            'wenn schon ein Artikel-Datensatz vorhanden ist - Deklaration neu anlegen (INSERT INTO dbo.ArtikelDeklarationstexte...)
            If OrgasoftMain.Read Then
                'Verbindung Lesen schliessen
                OrgasoftMain.CloseRead()

                sql = "'" & Deklaration & "', '" & ktTyp301.AllergenListe_C & "', '" & ktTyp301.AllergenListe_T & "', " &
                  "'" & ktTyp301.AllergenKurzListe_C & "', '" & ktTyp301.AllergenKurzListe_T & "'"
                'Insert durchführen
                Result = OrgasoftMain.sqlCommand(wb_Sql_Selects.setParams(wb_Sql_Selects.sqlInsertDeklaration, sql, KO_Nr_AlNum, 0, wb_GlobalSettings.osLaendercode, wb_GlobalSettings.osSprachcode))
                'Meldungen im Log ausgeben
                ChangeLogAdd(LogType.Msg, Nr, "", "Neu Anlegen der Zutaten und Allergenliste in OrgaBack-DB - Nr " & KO_Nr_AlNum)
            End If
        End If

        'Datenbankverbindung immer schliessen
        OrgasoftMain.Close()
        Return Result
    End Function

End Class
