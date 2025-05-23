﻿Public Class wb_Global

    ''' <summary>
    ''' Wert ist undefiniert
    ''' </summary>
    Public Const UNDEFINED = -1
    Public Const wbFALSE = 0
    Public Const wbTRUE = 1
    Public Const wbNODATE = #11/22/1964 00:00:00#
    Public Const wbUnixDATE = #01/01/1970 00:00:00#
    Public Const NOSTRING = "X"
    Public Const NOFILTER = 0
    Public Const ALLFILTER = 99

    Public Const obDEFAULTCOLOR = 0
    Public Const obDEFAULTSIZE = "NULL"

    Public Const LogFileEntries = 20                    'Anzahl der Einträge im Puffer
    Public Const Log4NetConfigFile = "WinBack.log4net"  'Datei-Name des Log-Files
    Public Const MaxHistDays = 3                        'Druckhistorie nach x Tagen löschen

    Public Const LinienGruppeSauerteig = 98           'Liniengruppe Sauerteig-Anlage(Rezeptvariante gleich 0)
    Public Const LinienGruppeStandard = 1             'Liniengruppe für neue Rezepte
    Public Const RezeptVarianteStandard = 1           'Rezeptvariante für neue Rezepte
    Public Const MaxLinien = 99                       'Kocher-Linie (maximale Anzahl an WinBack-Linien)
    Public Const MaxWinBackLinien = 12                'Maximale Linien in der Produktion(WinBack)
    Public Const MaxRezeptGroesse = 999.9             'Maximale Rezeptgröße
    Public Const RS_Par1_QUID = "-1"                  'Komponente im Rezept ist QUID-Relevant (In RS_Par1)
    Public Const RS_Par1_NOQUID = ""                  'Komponente im Rezept ist nicht QUID-Relevant (In RS_Par1)
    Public Const TA_min = 0                           'TA Minimal-Wert (Berechnung Wassermenge bei TA-Änderung Rezeptur)
    Public Const TA_max = 400                         'TA Maximal-Wert (Berechnung Wassermenge bei TA-Änderung Rezeptur)
    Public Const MAXLAGERBESTAND = 99000000           'maximal gültiger Wert Bilanzmenge (winback.Lagerorte) 99t
    Public Const KomponTypeInt_Undefined = -99        'Integer-Wert für undefinierte KomponentenType

    Public Const MaxUserIDs = 56                      'Anzahl der Einträge in Tabelle IAListe und ItemIDs (BenutzerGruppenRechte)
    'Anlegen neuer Dummy-User (Felder vorbelegen)
    Public Const NewUserName = "Neu"
    Public Const NewUserPass = "1"
    Public Const NewUserGrpe = "1"
    Public Const AdminUserGrpe = 99
    Public Const SysKonfigGrpe = -1

    'Symbol Rezept-im-Rezept
    Public Const RezeptImRezept = "®"
    'Text Rohstoff wird nicht deklariert
    Public Const FlagKeineDeklaration = "NO DECLARATION"
    'Text keine Chargen-Nummer(Rohstoff)
    Public Const FlagKeineChargenNummer = "KEINE"
    'Text Zutatenliste auflösen
    Public Const FlagAufloesen = ">"
    'Trennzeichen in MFF155(Mehlanteile)
    Public Const TrennzMehlAnteil = " "
    ''Flag Rohstoff zählt nicht zum Rezeptgewicht
    Public Const ZaehltNichtZumNwtGewicht = "0"
    Public Const ZaehltZumNwtGewicht = "1"
    Public Const ZaehltNichtZumRezeptGewicht = "1"
    Public Const ZaehltZumRezeptGewicht = "0"

    'Filiale.Typ ist Produktions-Filiale
    Public Const ProduktionsFiliale = 4
    'Text Liniengruppen "Alle"
    Public Const TextAlle = "Alle"
    'Text Artikel/Rohstoff-Gruppe "keine"
    Public Const TextKeine = "keine"

    'Artikelgruppe Backorte (GruppenNr)
    Public Const GruppenNrBackorte = 3
    'Offset WinBack.Linien zu OrgaBack-Backorte (ArtikelMultifunktionsfeld.Hierarchie)
    Public Const OffsetBackorte = 100

    'WinBack Rezept.Parameter(String)
    Public Const wbRzptParamMin = 0
    Public Const wbRzptParamMax = 250

    'OrgaBack.Einheit [gr] aus dbo.Einheiten (Unit-Test)
    Public Const obEinheitStk As Short = 0
    Public Const obEinheitKilogramm As Short = 11
    Public Const obEinheitGramm As Short = 12
    Public Const obEinheitLiter As Short = 16
    Public Const obEinheitMeter As Short = 20
    'WinBack.Einheit aus winback.Einheiten (Unit-Test)
    Public Const wbEinheitLeer = 0
    Public Const wbEinheitKilogramm = 1
    Public Const wbEinheitLiter = 4
    Public Const wbEinheitStk = 11
    Public Const wbEinheitMeter = 30
    'Einträge AktionTimer-Tabelle
    Public Const obUpdateAll = -2                     'Eintrag in Aktions-Timer-Tabelle (alle Komponenten updaten)

    'OrgaBack.RezeptType (Produktions-Rezept variabel)
    Public Const RecipeTypeNoRecipe As Short = 0
    Public Const RecipeTypeProdVariabel As Short = 5

    'WinBack-Server-Task Port
    Public Const WinBackServerTaskPort = "22046"

    'WinBack-Server-Verzeichnis Produktionsdaten
    Public Const WinBackServerProdDirectory = "/bakelink/1101_Produktion.csv"
    'WinBack-Server-Verzeichnis Hinweise
    Public Const WinBackServerHinweisDirectory = "/hinweise/"

    'TTS-ChargenAuswertung Kommandozeile (Teil 1 & 2)
    Public Const TTSLogCmd_0 = "cat /home/herbst/log/0s1-s.dbg | grep "
    Public Const TTSLogCmd_1 = " | grep TTS:"
    'Datensicherung WinBack
    Public Const WinBackSaveServer = "winback_save_server"
    'Datenrücksicherung WinBack
    Public Const WinBackLoadServerDaten = "winback_load_server_daten"
    Public Const WinBackLoadServerChargen = "winback_load_server_chargen"

    'HTML-Hilfe anzeigen
    Public Const WindowsHelp = "/hh.exe"
    Public Const OrgaBackHtmlHelp = "WinBack.chm"
    Public Const OrgaBackProgrammName = "Orgasoft.NET.exe"

    'TeamViewer - Fernwartung
    Public Const TeamViewerExe = "WinBackTeamViewer.exe"
    'Putty - Terminalprogramm
    Public Const PuttyExe = "Putty.exe"

    'Verzeichnisse
    Public Const SubDir_dbu = "dbu\"            'Datenbank-Updates
    Public Const SubDir_dll = "dll\"            'dll-Files
    Public Const SubDir_log = "log\"            'log-Files
    Public Const SubDir_ll3 = "ls3\"            'List&Label Version30 - Redistributable Files
    Public Const SubDir_lst = "lst\"            'Vorlagen List&Label
    Public Const SubDir_pic = "pic\"            'Artikel-Bilder
    Public Const SubDir_upd = "update\"         'Programm-Updates - ProgrammLaucher (WinBackUpdate)
    Public Const SubDir_tmp = "tmp\"            'Programm Temp-Verzeichnis 
    Public Const SubDir_pdf = "tmp\pdf"         'Programm pdf-Verzeichnis 
    Public Const SubDir_zip = "tmp\zip"         'Programm zip-Verzeichnis zum Erzeugen der Update-Files"
    Public Const SubDir_xfc = "xfc\"            'Schnittstellendaten
    Public Const SubDir_xfd = "Schnittstelle\"  'Schnittstellendaten (Debug)

    'AssemblyResolve wird definiert in WinBackAddIn.Erweiterte Kompilierungsoptionen
#If NoAssemblyResolve Then
    Public Const AssemblyResolve = False
#Else
    Public Const AssemblyResolve = True
#End If

    Public Enum CompareVersionResult
        NoUpdate
        MinorUpdate
        VersionUpdate
        Err
    End Enum

    Public Enum Log4NetType
        File
        Udp
        MySQL
        Undef
    End Enum

    Public Enum StatistikType
        ChargenAuswertung
        ChargenAuswertungSauerteig
        StatistikRohstoffeVerbrauch
        StatistikRohstoffeDetails
        StatistikRezepte
        DontChange
    End Enum

    Public Enum ktTyp301Gruppen
        xxx
        Big4
        Big8
        Vitamine
        Kohlenhydrate
        Mineralstoffe
        SpurenElemente
        Allergene
        Gluten
        Schalenfrüchte
        Gesamtkennzahlen
        ErnaehrungsFormen
    End Enum

    Public Structure controlSizeandLocation
        Public cSize As Drawing.Size
        Public cLocation As Drawing.Point
    End Structure

    Public Enum XNumberType
        Artikel
        Rohstoffe
        Rezepte
    End Enum

    Public Structure obMandant
        Public MandantNr As Integer
        Public MandantName As String
        Public AdminDBName As String
    End Structure

    Public Enum wbMenuType
        BurgerMenu
        MainMenu
        SubMenu
    End Enum
    Public Structure wbMenuItem
        Public Text As String
        Public MenuType As wbMenuType
        Public Click As EventHandler
        Public Tag As String
        Public Bild As Drawing.Image
    End Structure

    Public Structure SiloParameter
        Public LinieNr As Integer           'Linie-Nummer
        Public LinieAktiv As Boolean        'Linie wird verwendet/aktiv
        Public ParamSatz As Integer         'Paramsatz aus WegeRouten
        Public Nachlauf As Double           'Parameter  2 - Nachlauf
        Public GrobFein As Double           'Parameter  3 - Umschaltpkt Grob/Fein
        Public Frequenz_Grob As Integer     'Parameter  8 - Frequenz Grobstrom
        Public Frequenz_Fein As Integer     'Parameter  9 - Frequenz Feinstrom
        Public Faktor_MengeZeit As Double   'Parameter 20 - Faktor Menge/Zeit
    End Structure

    Public Structure WaagenParameter
        Public WaageNr As Integer               'Waage-Nummer
        Public LinieNr As Integer               'Linie-Nummer
        Public LinieAktiv As Boolean            'Linie wird verwendet/aktiv
        Public WaagenGroesse As Double          'Parameter Waagengröße
        Public FoerderStrom As Integer          'Parameter Förderstromüberwachung
        Public AustrageFkt As Integer           'Parameter Austragenfunktion
        Public Anlauf_Transport As Integer      'Parameter Anlauf Transportgebläse
        Public Nachlauf_Transport As Integer    'Parameter Nachlauf Transportgebläse
        Public Ruettler_Ein As Integer          'Parameter Rüttler tEin in Sekunden
        Public Ruettler_Aus As Integer          'Parameter Rüttler tAus in Sekunden
        Public Duesen_Ein As Integer            'Parameter Düsen tEin in Sekunden
        Public Duesen_Aus As Integer            'Parameter Düesen tAus in Sekunden
        Public Filter_Ein As Integer            'Parameter Filter-Reinigung tEin in Sekunden
        Public Filter_Aus As Integer            'Parameter Filter-Reinigung tAus in Sekunden
        Public Impuls_Klappe As Integer         'Parameter Impuls Klappe in Sekunden
        Public Nachkomma As Integer             'Nachkomma-Stellen Istwert-Anzeige

        Public Analog_Kanal As Integer          'Nummer Analog-Kanal
        Public BC9000 As Integer                'BC9000 Analog-Kanal
        Public ParameterNr As Integer           'BC9000 Analog-Kanal-ParameterNr
    End Structure


    Public Enum KomponParams
        Sollwert = 1
        EinheitenIndex = 4
        Format = 9
        UntererGrenzwert = 10
        ObererGrenzwert = 11
        VerarbeitungsHinweisePfad = 20
        VerarbeitungsHinweiseDPI = 21
    End Enum

    Public Enum ktParam
        ktAlle = 0
        kt200 = 200
        kt201 = 201
        kt202 = 202
        kt210 = 210
        kt220 = 220
        kt300 = 300
        kt301 = 301
        kt303 = 303
    End Enum

    Public Structure ktTypXXXParam
        Public Type As Integer          'KomponentenType und/oder Parameter-Type
        Public ParamNr As Integer       'Parameter-Nummer
        Public Bezeichnung As String    'Bezeichnungs-Text
        Public Einheit As String        'Einheit aus winback.KomponTypen-winback.Einheiten
        Public eFormat As Integer       'Eingabe-Format (winback.Formate)
        Public eUG As String            'Eingabe-Grenzwert unten
        Public eOG As String            'Eingabe-Grenzwert oben
        Public Used As Boolean          'Parameter aktiv
    End Structure

    Public Structure ktTyp200Param
        Public ParamNr As Integer
        Public Bezeichnung As String
        Public Einheit As String
        Public Feld As String
        Public Used As Boolean
    End Structure

    Public Structure ktTyp300Param
        Public ParamNr As Integer
        Public Bezeichnung As String
        Public Einheit As String
        Public Used As Boolean
    End Structure

    Public Structure ktTyp301Param
        Public ParamNr As Integer
        Public Bezeichnung As String
        Public KurzBezeichnung As String
        Public Gruppe As ktTyp301Gruppen
        Public iEinheit As Integer
        Public Einheit As String
        Public Feld As String
        Public Used As Boolean
        Public oEinheit As String
        Public oUsed As Boolean
    End Structure

    Public Structure ktTyp303Param
        Public ParamNr As Integer
        Public Bezeichnung As String
        Public Einheit As String
        Public Used As Boolean
    End Structure

    Public Enum KneterParameter
        MischenRechts = 1
        MischenLinks = 2
        KnetenRechts = 3
        KnetenLinks = 4
        TeigTemperatur = 5
        Teigruhe = 6
        StartDosierung = 7
        Pause = 8
        KlappeAuf = 9
        StartDosOhneMehl = 14
    End Enum

    Public Structure Nwt
        Public Visible As Boolean
        Public Nr As Integer
        Public Text As String
        Public Wert As String
        Public Einheit As String
        Public Header As String
        Public FehlerText As String
        Public ErrIntern As Boolean
    End Structure

    Public Structure NwtCloud
        Public id As String
        Public name As String
        Public lieferant As String
        Public deklarationsname As String
        Public ean As String
    End Structure

    Enum MySqlCodepage
        UNDEFINED
        iso8859_15                  'Code-Wandlung von iso-8859-15 nach utf8 (Deutschland) - Keine Umwandlung
        iso8859_5                   'Code-Wandlung von iso-8859-5  nach utf8 (Russland)
        iso8859_1                   'Code-Wandlung von iso-8859-1  nach utf8 (Ungarisch)
    End Enum

    Enum ProgVariante
        OrgaBack                    'Programm läuft als Addin unter OrgaBack
        OBServerTask                'Programm läuft als Hintergrund-Task auf dem OrgaBack-Server
        WBServerTask                'Programm läuft als Hintergrund-Task auf dem WinBack-Server
        WinBack                     'Programm läuft als Standalone
        AnyWhere                    'Programm läuft als Standalone auf einem Tablet
        UnitTest                    'Programm läuft im Test-Modus (UnitTest)
        Setup                       'Setup-Routine OrgaBackAddIn
        Update                      'Update-Routine OrgaBackAddIn/OrgaBackOffice
        Undef                       'nicht definiert
    End Enum

    Enum ProgFunction
        Restart
        Upgrade
        Update
        Deploy
        Undef
    End Enum

    Enum ProgFunctionVariante
        OrgaBack
        OrgaBackOffice
        BackgroundTask
        AnyWhere
        Undef
    End Enum

    Enum FileUpdateMode
        UpdateAlways                'existierende Datei immer überschreiben
        UpdateIfNewer               'existierende Datei nur überschreiben wenn aktuelle
        UpdateNever                 'existierende Datei niemals überschreiben
        UpdateBak                   'wenn schon eine .bak-Datei vorhanden ist, nicht kopieren
    End Enum

    Enum RohSiloTypen
        M       'Mehlsilo
        MK      'Mittelkomponenten
        KKA     'Kleinkomponenten
        BW      'Bodenwaage/Flüssigverwiegung
        UNDEF   'nicht definiert
    End Enum

    Enum OrgaBackThemes
        Standard
        Blau
        Grau
        Anthrazit
    End Enum

    Enum OrgaBackDockPanelLayoutPath
        ProgrammGlobal              'Layout-File im Verzeichnis ..\Temp\00
        UserLokal                   'Layout-File im Verzeichnis ..\Temp\xx (xx - Arbeitsplatz-Nummer)
    End Enum

    Enum ChargenTypen
        CHRG_UNDEF = 0
        CHRG_REZEPT = 1
        CHRG_ARTIKEL = 2
        CHRG_KOMPONENTE = 3
        CHRG_KOMPSUMME = 4
    End Enum

    '''   0  -  Nicht bearbeitet
    '''   1  -  in Bearbeitung
    '''   2  -  Okay
    '''   3  -  Warnung
    '''   4  -  Fehler bei manueller Verwiegung
    '''   5  -  Fehler bei automatischer Verwiegung
    '''   6  -  Multistart markiert
    '''   7  -  Nachtstart
    '''   8  -  Start gespeichert (Multistart)
    Enum ChargenStatus
        CS_UNBEARBEITET = 0
        CS_IN_ARBEIT = 1
        CS_OK = 2
        CS_WARNUNG = 3
        CS_ERR_HAND = 4
        CS_ERR_AUTO = 5
        CS_MULTISTART = 6
        CS_NACHTSTART = 7
        CS_STARTGESPEICHERT = 8
    End Enum

    ''' <summary>
    ''' Status Silo-Füllstands-Korrektur
    ''' </summary>
    Enum KorrekturStatus
        SILO_NULLEN
        SILO_PLUS
        SILO_MINUS
        SILO_NOP
    End Enum

    ''' <summary>
    ''' Status LF_gebucht in Tabelle winback.Lieferungen
    ''' </summary>
    Enum LieferStatus
        LF_UNDEFINED = -1
        LF_AKTIV = 1
        LF_FERTIG = 3
    End Enum

    Enum KomponTypen
        KO_TYPE_UNDEFINED

        KO_ZEILE_ARTIKEL             '-1
        KO_ZEILE_REZEPT              '-2
        KO_ZEILE_KOMPONENTE          '-3
        KO_ZEILE_DUMMYARTIKEL        '-4

        KO_TYPE_ARTIKEL             '0
        KO_TYPE_AUTOKOMPONENTE      '101
        KO_TYPE_HANDKOMPONENTE      '102
        KO_TYPE_WASSERKOMPONENTE    '103
        KO_TYPE_EISKOMPONENTE       '104
        KO_TYPE_STUECK              '105
        KO_TYPE_METER               '106

        KO_TYPE_TEMPERATURERFASSUNG '111
        KO_TYPE_KNETER              '118
        KO_TYPE_TEIGZETTEL          '119
        KO_TYPE_KNETERREZEPT        '128
        KO_TYPE_KNETER_TEIGRUHE     '129 (Dummy)

        KO_TYPE_TEXTKOMPONENTE      '121
        KO_TYPE_PRODUKTIONSSTUFE    '122
        KO_TYPE_KESSEL              '123

        KO_TYPE_SAUER_MEHL          '1
        KO_TYPE_SAUER_WASSER        '3
        KO_TYPE_SAUER_TEMP          '4
        KO_TYPE_SAUER_DIGITAL       '10
        KO_TYPE_SAUER_ANALOG        '11
        KO_TYPE_SAUER_WARTEN        '16
        KO_TYPE_SAUER_RUEHREN       '17
        KO_TYPE_SAUER_ZUGABE        '19
        KO_TYPE_SAUER_STATUS        '20
        KO_TYPE_SAUER_TEXT          '21
        KO_TYPE_SAUER_AUTO_ZUGABE   '22
        KO_TYPE_SAUER_REZEPT_START  '30
        KO_TYPE_SAUER_REPEAT        '31

        KO_TYPE_LAGERLISTE_HAND     'Handkomponente für Lagerliste
        KO_TYPE_LAGERLISTE_AUTO     'Autokomponente für Lagerliste
    End Enum

    Enum ErnaehrungsForm
        X   'not used
        Y   'Yes(Ja)
        N   'No(Nein)
        ERR 'Fehler bei der Berechnung
    End Enum

    Enum AllergenInfo
        X   'not used
        N   'keine Angabe
        K   'keine
        T   'trace
        C   'contains
        ERR 'Fehler bei der Berechnung
    End Enum

    Enum wbAktionsTimerStatus
        Disabled = 0
        Enabled = 1
        Running = 2
        Undfined = -1
    End Enum

    Enum obSatzTyp
        ProduzierterArtikel
        Rohstoff
        UnDefined
    End Enum

    Enum wbSatzTyp
        Artikel
        Rezept
        UnDefined
    End Enum

    ''' <summary>
    ''' Markierung der Artikel/Komponentendaten (KA_PreisEinheit)
    '''     -   Nährwertinfo fehlerhaft/nicht vollständig
    '''     -   Nährwertinfo muss neu berechnet werden (Update Nährwerte Cloud)
    ''' </summary>
    Enum ArtikelMarker
        nwtOK = 0
        nwtFehlerhaft = 1
        nwtUpdate = 2
    End Enum

    ''' <summary>
    ''' Produktions-Parameter
    ''' </summary>
    Public Const maxTypXXX = 91
    Public Const T101_LagerOrt = 90

    Public Const T102_SollMenge = 1
    Public Const T102_SollProzent = 2
    Public Const T102_TolMinus = 3
    Public Const T102_TolPlus = 4
    Public Const T102_TolProzent = 5

    Public Const T118_KneterParamNr = 2

    ''' <summary>
    ''' Produkt-Information
    ''' </summary>
    Public Const maxTyp200 = 330
    Public Const T200_DateinameBild = 2
    Public Const T200_Verkaufsgewicht = 4
    Public Const T200_Haltbarkeit = 7
    Public Const T200_Lagerung = 8
    Public Const T200_VerkaufsTage = 9
    Public Const T200_Warengruppe = 17
    ''' <summary>
    ''' Verarbeitungshinweise
    ''' </summary>
    Public Const maxTyp201 = 12

    ''' <summary>
    ''' Kalkulation
    ''' </summary>
    Public Const maxTyp202 = 23

    ''' <summary>
    ''' Froster
    ''' </summary>
    Public Const maxTyp210 = 1

    ''' <summary>
    ''' Gare
    ''' </summary>
    Public Const maxTyp220 = 3

    ''' <summary>
    ''' Parameter Produktion
    ''' </summary>
    Public Const maxTyp300 = 15
    Public Const T300_Backverlust = 1
    Public Const T300_ProdVorlauf = 2
    Public Const T300_Zuschnitt = 3
    Public Const T300_LinienGruppe = 5
    Public Const T300_RzNr = 6
    Public Const T300_RezeptNummer = 7
    Public Const T300_RezeptName = 8
    Public Const T300_StkProBlech = 9
    Public Const T300_StkProStikken = 10
    Public Const T300_ZeitTeig = 11
    Public Const T300_ZeitAufarbeitung = 12
    Public Const T300_ZeitBacken = 13
    Public Const T300_ZeitAbkuehlen = 14
    Public Const T300_Ofengruppe = 15

    ''' <summary>
    ''' Nährwerte
    ''' </summary>
    Public Const maxTyp301 = 213
    Public Const minTyp301Allergen = 141
    Public Const maxTyp301Allergen = 189
    Public Const minTyp301Ernaehrung = 210
    Public Const maxTyp301Ernaehrung = 213

    Public Const T301_Kilokalorien = 1
    Public Const T301_KiloJoule = 2
    Public Const T301_Proteine = 3
    Public Const T301_Kohlenhydrate = 4
    Public Const T301_Fette = 5
    Public Const T301_Wasser = 6
    Public Const T301_Zucker = 11
    Public Const T301_gesFettsaeuren = 12
    Public Const T301_Ballaststoffe = 13
    Public Const T301_Natrium = 14
    Public Const T301_Alkohol = 15
    Public Const T301_GesamtKochsalz = 202

    Public Const T301_Gluten = 141
    Public Const T301_Weizen = 170
    Public Const T301_Roggen = 171
    Public Const T301_Gerste = 172
    Public Const T301_Dinkel = 173
    Public Const T301_Kamut = 174
    Public Const T301_Hafer = 175
    Public Const T301_Emmer = 176
    Public Const T301_Einkorn = 177

    Public Const T301_Krebstiere = 142
    Public Const T301_Eier = 143
    Public Const T301_Fische = 144
    Public Const T301_Erdnusserzeugnisse = 145
    Public Const T301_Sojaerzeugnisse = 146
    Public Const T301_Milcherzeugnisse = 147

    Public Const T301_Schalenfruechte = 148
    Public Const T301_Mandeln = 180
    Public Const T301_Haselnüsse = 181
    Public Const T301_Walnüsse = 182
    Public Const T301_Kaschunüsse = 183
    Public Const T301_Pecannüsse = 184
    Public Const T301_Paranüsse = 185
    Public Const T301_Pistazien = 186
    Public Const T301_Makadamianüsse = 187

    Public Const T301_Sellerie = 149
    Public Const T301_Senf = 150
    Public Const T301_Sesamsamen = 151
    Public Const T301_Sulfite = 152
    Public Const T301_Lupinen = 153
    Public Const T301_Weichtiere = 154

    Public Const T301_Broteinheiten = 201
    Public Const T301_Vegetarisch = 210
    Public Const T301_Vegan = 211
    Public Const T301_Koscher = 212
    Public Const T301_Halal = 213


    ''' <summary>
    ''' Parameter EU/Bio-Verbände
    ''' </summary>
    Public Const maxTyp303 = 11
    Public Const minTyp303EU = 1
    Public Const maxTyp303EU = 2
    Public Const minTyp303BioVerband = 10
    Public Const maxTyp303BioVerband = 10

    Public Const T303_EU_Butter = 1
    Public Const T303_EU_Alkohol = 2
    Public Const T303_BioVerband = 11

    ''' <summary>
    ''' Konstanten für MultifunktionsFelder Artikel/User
    ''' </summary>

    Public Const MFF_Value = 3


    '''not USED:
    '''
    '''Public Const MFF_Haltbarkeit = 102
    '''Public Const MFF_Lagerung = 103
    '''Public Const MFF_Verkaufstage = 104
    '''Public Const MFF_VerarbeitungsHinweisArtikel = 208
    '''Public Const MFF_MatchCode = 227
    '''Public Const MFF_Zutatenliste = 209
    '''Public Const MFF_Gebäckcharakeristik = 211
    '''Public Const MFF_Verzehrtipps = 212
    '''Public Const MFF_Wissenswertes = 213
    '''
    Public Const MFF_ProduktionsLinie = 200
    Public Const MFF_KO_Nr = 201
    Public Const MFF_MehlZusammensetzung = 155
    Public Const MFF_Kommentar = 156
    Public Const MFF_RezeptNummer = 202
    Public Const MFF_RezeptName = 203

    Public Const MFF_USerGruppe = 500


    ''' <summary>
    ''' Konstanten für die Berechnung der Teigausbeute
    ''' Werte für die Komponenten-Parameter
    ''' </summary>
    Public Const TA_Undefined = -2
    Public Const TA_Wasser = -1
    Public Const TA_Null = 0
    Public Const TA_Mehl = 100

    Enum Parameter
        Tx_AlNum               'Rohstoff/Artikelnummer(alphanumerisch)
        Tx_Bezeichnung         'Rohstoff/Artikelbezeichnung
        Tx_Kommentar           'Kommentar-Feld
        Tx_EANCode             'EAN-Nummer
        Tx_MatchCode           'Index WinBack-Cloud/Datenlink
        Tx_Lieferant           'Lieferant
        Tx_DeklarationIntern
        Tx_DeklarationExtern
        Tx_Mehlzusammensetzung 'Mehlzusammensetzung
    End Enum

    Enum Hinweise
        RezeptHinweise         '2/0/RzNr
        ArtikelHinweise        '3/0/ArtNr
        UserInfo               '4/0/UsrNr
        Konfiguration          '5/0/KonfigNr/KonfigGrp'
        ZutatenListe           '9/1/ArtNr
        MehlZusammensetzung    '9/2/ArtNr
        GebCharakteristik     '10/1/ArtNr
        Verzehrtipps          '10/2/ArtNr
        Wissenswertes         '10/3/ArtNr
        DeklBezRohstoff       '11/0/RohNr  
        DeklBezRohstoffIntern '11/1/RohNr  
        MessageTextLinie      '20/0/LNr
        MessageTextUser       '20/1/UsrNr
        NaehrwertUpdate       '21/0/RohNr
    End Enum

    Public Structure ENummern
        Public Nr As Integer                'E-Nummer als Integer
        Public Bezeichnung As String        'Bezeichnungstext
        Public Text As String               'E-Nummer als String
        Public Beschreibung As String       'Wirkung/Herstellung
        Public Bemerkung As String          'Hinweise/Gefahren
        Public Key As Char
        Public CleanLabel As Char           'Kann entfallen (J/N)
        Public MaxAnteilProzent As Double   'maximaler Anteil im Rohstoff in Prozent
    End Structure

    Public Structure OrgaBackSortiment
        Public Srt As String
        Public SName As String
        Public FName As String
    End Structure

    Public Structure WinBackBestand
        Public ArtikelNr As String
        Public Bezeichnung As String
        Public Lfd As String
        Public Datum As String
        Public Bestand As String
        Public ChargenNr As String
        Public Vorfall As String
    End Structure

    Enum EditState
        Invalid
        AddNew
        Edit
    End Enum

    Enum SyncState
        NOK                     'Datensatz ist noch nicht geprüft
        OK                      'Datensatz ist in  beiden Datenbanken vorhanden und identisch
        DBL                     'Datenfehler - Datensatz mehrfach vorhanden (Feld Bezeichnung identisch)
        OrgaBackWrite           'Datensatz ist nur in WinBack vorhanden und muss in OrgaBack geschrieben werden
        OrgaBackUpdate          'Datensatz ist in beiden Datenbanken vorhanden und muss in OrgaBack aktualisiert werden
        OrgaBackMiss            'Datensatz ist nur in WinBack vorhanden - Fehlt in OrgaBack, KEIN Update
        OrgaBackErr             'Datenfehler in OrgaBack - Datenintegrität, doppelte Einträge o.ä.
        WinBackWrite            'Datensatz ist nur in OrgaBack vorhanden und muss in WinBack geschrieben werden
        WinBackUpdate           'Datensatz ist in beiden Datenbanken vorhanden und muss in WinBack aktualisiert werden
        WinBackMiss             'Datensatz ist nur in OrgaBack vorhanden - Fehlt in WinBack, KEIN Update
        WinBackErr              'Datenfehler in WinBack - Datenintegrität, doppelte Einträge o.ä.
        WinBackNotUsed          'Datensatz wird in WinBack nicht verwendet (kann gelöscht werden)

        TryMatchWinBackUpdate   'Datensatz wurde nach TryMatch zugeordnet - Update in WinBack
        TryMatchOrgaBackUpdate  'Datensatz wurde nach TryMatch zugeordnet - Update in OrgaBack(?)
        TryMatchDel             'Datensatz kann nach TryMatch gelöscht werden
    End Enum

    Enum SyncType
        Undefined
        Benutzer
        BenutzerGruppen
        Artikel
        ArtikelGruppen
        Rohstoffe
        RohstoffGruppen
        Aufarbeitung
    End Enum

    Enum LogType
        X     'Unbestimmt
        Stm   'Stammdaten
        Prm   'Parameter
        Alg   'Allergen
        Nrw   'Nährwert
        Dkl   'Deklaration'
        Err   'Fehler
        Msg   'Message
    End Enum

    'Kocher-Nummer Master(Server)
    Public Const KocherMaster As Integer = 0
    'Toleranz Zeitstempel bei Sync Kocher (in Minuten)
    Public Const KocherSyncToleranz As Integer = 15
    'WinBack RezeptNummer Prefix
    Public Const KocherPreFix As String = "KCHR"
    'WinBack Liniengruppe Kocher/Röster
    Public Const KocherLinienGruppe As Integer = 98
    'Kocher maximale Anzahl Rezeptschitte
    Public Const KocherMaxSchritte = 5
    'Kocher Anzahl der Parameter pro RezeptSchritt (Rezept-Textfile)
    Public Const Kocher_IdxTeiler = 10
    'Kocher Offset Parameter (Rezept-Textfile)
    Public Const Kocher_IdxOffset = 7

    'Kocher Rezept-Hülle Nummer in HisRezepte/HisRezeptSchritte
    Public Const Kocher_HisRzNr As Integer = -98
    Public Const Kocher_HisVrnt As Integer = 1
    Public Const Kocher_HisAend As Integer = 0

    Enum Kocher_VerbindungsStatus
        UNDEF
        OK
        ERR
        CONNECT
    End Enum

    Enum Kocher_SyncStatus
        Ok
        CopyToMaster
        CopyNewFileToMaster
        CopyFromMaster
        CopyNewFileFromMaster
    End Enum

    Public Structure wb_ChangeLogEintrag
        Public Type As LogType
        Public ParamNr As Integer
        Public OldValue As String
        Public NewValue As String
    End Structure

    Public Structure wb_GruppenRechte
        Public OberBegriff As String  'IT_Bezeichnung
        Public Bezeichnung As String  'II_Kommentar
        Public sAttribut As String    'T_Text
        Public iAttribut As Integer   'AT_Wert2int
        Public iAttrGrp As Integer    'AT_Attr_Nr
        Public iTyp As Integer        'IP_ItemTyp
        Public iID As Integer         'IP_ItemID
        Public iWert1 As Integer      'IP_Wert1Int
        Public iLfdNr As Integer      'IP_Lfd_Nr
    End Structure

    Public Structure wb_Gruppe
        Public Nummer As Integer      'II_ItemID    -   Hierarchie
        Public Bezeichnung As String  'II_Kommentar -   Bezeichnung
        Public SyncOK As SyncState    'Synchronisations-Status
    End Structure

    Public Structure wb_GrpAttr
        Public Attr As Integer
        Public Text As String
        Public Wert As Integer
    End Structure

    Public Structure wb_LinienGruppe
        Public LinienGruppe As Integer
        Public Bezeichnung As String
        Public KurzName As String
        Public Linien As Array
        Public StartZeit As String
        Public Abteilung As String
        Public BackZettelDrucken As String
        Public TeigZettelDrucken As String
        Public TeigRezeptDrucken As String
        Public BackZettelSenden As String
        Public TeigZettelSenden As String
        Public bDrucken As Boolean
        Public bKommentar As Boolean
        Public bSonderText As Boolean
    End Structure

    Public Structure wb_Linien
        Public Linie As Integer
        Public Bezeichnung As String
        Public SegIdx As Integer
        Public Filiale As Integer
    End Structure

    Public Structure wb_Einheiten
        Public Nr As Integer
        Public Einheit As String
        Public Bezeichnung As String
        Public obNr As Integer
    End Structure

    Enum ModusTeigOptimierung
        NurTeigeKleinerMinChargen
        AlleTeige
        AlleTeigeAlleTouren
    End Enum

    Enum ModusChargenTeiler
        XGleiche            'Aufteilung in gleich große Chargen
        NurOptimal          'Aufteilung nur in Optimal-Chargen
        OptimalUndRest      'Aufteilung in Optimal- und Rest-Chargen
        MaximalUndRest      'Aufteilung in Maximal- und Rest-Chargen
        RezeptGroesse       'Aufteilung in Rezept-Größe (keine Chargen angegeben)
    End Enum

    Enum ChargenTeilerResult
        OK      'Chargenaufteilung in Ordnung
        EM1     'Nach Aufteilung in Optimalchargen bleibt eine Restmenge offen, die nicht produziert werden kann
        EM2     'Nach Aufteilung in Optimalchargen wird mehr produziert als gefordert
        EM3     'Nur eine Restcharge, Restmenge unterhalb Mindestchargen - Chargen gleicher Teige müssen zusammengefasst werden.
        EP1     'Sollmenge nicht erreicht, Restmenge unterhalb Mindestcharge
        EP2     'Sollmenge nicht erreicht, Restmenge unterhalb Mindestcharge
        EP9     'Keine Chargengrößen angegeben, Aufteilung nach Rezeptgröße
        ART     'Artikelnummer nicht gefunden
        REZ     'Rezeptnummer nicht gefunden
    End Enum

    Public Structure ChargenMengen
        Public AnzahlOpt As Integer
        Public MengeOpt As Double
        Public AnzahlRest As Integer
        Public MengeRest As Double
        Public Result As ChargenTeilerResult
        Public Modus As ModusChargenTeiler
    End Structure

    Enum SortOrder
        BackZettel
        ProdPlan
        OfenListe
        LagerBestand
        LagerEntnahmeListe
    End Enum

    Enum MinMaxOptChargenError
        NoError     'kein Fehler
        MinGrOpt    'Minimal-Charge größer als Optimal-Charge
        MinGrMax    'Minimal-Charge größer als Maximal-Charge
        OptGrMax    'Optimal-Charge größer als Maximal-Charge
        OptKlMin    'Optimal-Charge kleiner als Minimal-Charge
        MaxKlOpt    'Maximal-Charge kleiner als Optimal-Charge
        MaxKlMin    'Maximal-Charge kleiner als Minimal-Charge
    End Enum

    Enum ChargenListeSortKriterium
        Undefined = -1
        ArtikelNummer = 0
        ArtikelName = 1
        Produktion = 2
    End Enum

    Enum TPopupFunctions
        TP_NeueProduktionsStufe
        TP_NeueProduktionsStufe_Davor
        TP_NeueProduktionsStufe_Danach

        TP_NeuerKessel_Darunter
        TP_NeuerKessel_Davor
        TP_NeuerKessel_Danach

        TP_NeueTextKomponente
        TP_NeueTextKomponente_Darunter
        TP_NeueTextKomponente_Davor
        TP_NeueTextKomponente_Danach

        TP_NeueKomponente
        TP_NeueKomponente_Darunter
        TP_NeueKomponente_Davor
        TP_NeueKomponente_Danach

        TP_Editieren
        TP_KneterRezept_Speichern
        TP_Loeschen
        TP_TTS_loeschen

        TP_Verschieben_Oben
        TP_Verschieben_Unten

        TP_TeigTemp

        TP_RohstoffVerwaltung
        TP_Naehrwerte_Laden
        TP_QuidDeklaration
    End Enum


End Class
