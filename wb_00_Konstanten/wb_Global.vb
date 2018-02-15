Public Class wb_Global

    ''' <summary>
    ''' Wert ist undefiniert
    ''' </summary>
    Public Const UNDEFINED = -1
    Public Const wbFALSE = 0
    Public Const wbTRUE = 1

    Public Const LogFileName = "\OrgaBack.log"        'Datei-Name des Log-Files
    Public Const LogFileEntries = 20                  'Anzahl der Einträge im Puffer

    Public Const LinienGruppeSauerteig = 999          'Liniengruppe Sauerteig-Anlage(Rezeptvariante gleich 0)
    Public Const MaxLinien = 99

    'Anlegen neuer Dummy-User (Felder vorbelegen)
    Public Const NewUserName = "Neu"
    Public Const NewUserPass = "-1"
    Public Const NewUserGrpe = "1"

    'Filiale.Typ ist Produktions-Filiale
    Public Const ProduktionsFiliale = 4

    'WinBack-Server-Task Port
    Public Const WinBackServerTaskPort = "22046"

    'WinBack-Server-Verzeichnis Produktionsdaten
    Public Const WinBackServerProdDirectory = "/bakelink/1101_Produktion.csv"

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
    End Enum

    Public Structure ktTyp301Param
        Public ParamNr As Integer
        Public Bezeichnung As String
        Public KurzBezeichnung As String
        Public Gruppe As ktTyp301Gruppen
        Public Einheit As String
        Public Feld As String
        Public Used As Boolean
    End Structure

    Public Structure Nwt
        Public Visible As Boolean
        Public Nr As Integer
        Public Text As String
        Public Wert As String
        Public Einheit As String
        Public Header As String
        Public FehlerText As String
    End Structure

    Enum MySqlCodepage
        iso8859_15                  'Code-Wandlung von iso-8859-15 nach utf8 (Deutschland) - Keine Umwandlung
        iso8859_5                   'Code-Wandlung von iso-8859-5  nach utf8 (Russland)
        iso8859_1                   'Code-Wandlung von iso-8859-1  nach utf8 (Ungarisch)
    End Enum

    Enum ProgVariante
        OrgaBack                    'Programm läuft als Addin unter OrgaBack
        WinBack                     'Programm läuft als Standalone
        Undef                       'nicht definiert
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

    'Public Const wbDatenRezept = 1      'Chargendaten Rezeptzeile
    'Public Const wbDatenArtikel = 2     'Chargendaten Artikelzeile
    'Public Const wbDatenKomponente = 3  'Chargendaten KomponentenZeile

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
    End Enum

    Enum AllergenInfo
        X   'not used
        N   'keine Angabe
        K   'keine
        T   'trace
        C   'contains
        ERR 'Fehler bei der Berechnung
    End Enum

    Public Const maxTyp300 = 14

    Public Const T300_LinienGruppe = 5
    Public Const T300_RzNr = 6
    Public Const T300_RezeptNummer = 7
    Public Const T300_RezeptName = 8

    Public Const maxTyp301 = 211
    Public Const minTyp301Allergen = 141
    Public Const maxTyp301Allergen = 189

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


    ''' <summary>
    ''' Konstanten für MultifunktionsFelder Artikel
    ''' </summary>
    Public Const MFF_Haltbarkeit = 102
    Public Const MFF_Lagerung = 103
    Public Const MFF_Verkaufstage = 104

    Public Const MFF_VerarbeitungsHinweisArtikel = 208
    Public Const MFF_Zutatenliste = 209
    Public Const MFF_MehlZusammensetzung = 210
    Public Const MFF_Gebäckcharakeristik = 211
    Public Const MFF_Verzehrtipps = 212
    Public Const MFF_Wissenswertes = 213

    Public Const MFF_Kurzname = 224
    Public Const MFF_Kommentar = 225
    Public Const MFF_KO_Nr = 226
    Public Const MFF_MatchCode = 227
    Public Const MFF_RezeptNummer = 228
    Public Const MFF_RezeptName = 229



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
        Tx_MatchCode           'Index WinBack-Cloud/Datenlink
        Tx_Lieferant           'Lieferant
        Tx_DeklarationIntern
        Tx_DeklarationExtern
    End Enum

    Enum Hinweise
        RezeptHinweise         '2/0/RzNr
        ArtikelHinweise        '3/0/ArtNr
        UserInfo               '4/0/UsrNr
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

    Public Structure ZutatenListe
        Public Zutaten As String
        Public eNr As Integer
        Public FettDruck As Boolean
        Public SollMenge As Double
        Public SortMenge As Double
        Public Grp1 As Integer
        Public Grp2 As Integer
        Public Quid As Boolean
        Public QuidProzent As Double
    End Structure

    Enum ZutatenListeMode
        Hide_ENummer
        Show_ENummer
        Optimize
    End Enum
    Public Structure ENummern
        Public Nr As Integer            'E-Nummer als Integer
        Public Bezeichnung As String    'Bezeichnungstext
        Public Text As String           'E-Nummer als String
        Public Beschreibung As String   'Wirkung/Herstellung
        Public Bemerkung As String      'Hinweise/Gefahren
        Public Key As Char              '
        Public CleanLabel As Char       'Kann entfallen (J/N)
    End Structure

    Enum EditState
        Invalid
        AddNew
        Edit
    End Enum

    Enum SyncState
        NOK                     'Datensatz ist noch nicht geprüft
        OK                      'Datensatz ist in  beiden Datenbanken vorhanden und identisch
        OrgaBackWrite           'Datensatz ist nur in WinBack vorhanden und muss in OrgaBack geschrieben werden
        OrgaBackUpdate          'Datensatz ist in beiden Datenbanken vorhanden und muss in OrgaBack aktualisiert werden
        OrgaBackMiss            'Datensatz ist nur in WinBack vorhanden - Fehlt in OrgaBack, KEIN Update
        OrgaBackErr             'Datenfehler in OrgaBack - Datenintegrität, doppelte Einträge o.ä.
        WinBackWrite            'Datensatz ist nur in OrgaBack vorhanden und muss in WinBack geschrieben werden
        WinBackUpdate           'Datensatz ist in beiden Datenbanken vorhanden und muss in WinBack aktualisiert werden
        WinBackMiss             'Datensatz ist nur in OrgaBack vorhanden - Fehlt in WinBack, KEIN Update
        WinBackErr              'Datenfehler in WinBack - Datenintegrität, doppelte Einträge o.ä.

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
    End Enum

    Enum LogType
        X     'Unbestimmt
        Stm   'Stammdaten
        Prm   'Parameter
        Alg   'Allergen
        Nrw   'Nährwert
        Dkl   'Deklaration'
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
    End Structure

    Public Structure wb_Gruppe
        Public Nummer As Integer      'II_ItemID    -   Hierarchie
        Public Bezeichnung As String  'II_Kommentar -   Bezeichnung
        Public SyncOK As SyncState    'Synchronisations-Status
    End Structure

    Public Structure wb_LinienGruppe
        Public LinienGruppe As Integer
        Public Bezeichnung As String
        Public Linien As Array
        Public Abteilung As String
        Public BackZettelDrucken As String
        Public TeigZettelDrucken As String
        Public TeigRezeptDrucken As String
        Public BackZettelSenden As String
        Public TeigZettelSenden As String
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
