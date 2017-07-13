Public Class wb_Global

    ''' <summary>
    ''' Wert ist undefiniert
    ''' </summary>
    Public Const UNDEFINED = -1

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
    End Structure

    Enum ProgVariante
        OrgaBack                    'Programm läuft als Addin unter OrgaBack
        WinBack                     'Programm läuft als Standalone
    End Enum

    Enum KomponTypen
        KO_TYPE_UNDEFINED

        KO_ZEILE_ARTIKEL             '-1
        KO_ZEILE_CHARGE              '-2

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
        K   'keine
        T   'trace
        C   'contains
        N   'keine Angabe
        ERR 'Fehler bei der Berechnung
    End Enum

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
    Public Const MFF_Zutatenliste = 209
    Public Const MFF_VerarbeitungsHinweisArtikel = 208
    Public Const MFF_MatchCode = 281
    Public Const MFF_KO_Nr = 280


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
        OrgaBackErr             'Datensatz ist nur in WinBack vorhanden - Fehlt in OrgaBack, KEIN Update
        WinBackWrite            'Datensatz ist nur in OrgaBack vorhanden und muss in WinBack geschrieben werden
        WinBackUpdate           'Datensatz ist in beiden Datenbanken vorhanden und muss in WinBack aktualisiert werden
        WinBackErr              'Datensatz ist nur in OrgaBack vorhanden - Fehlt in WinBack, KEIN Update
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

    'Anlegen neuer Dummy-User (Felder vorbelegen)
    Public Const NewUserName = "Neu"
    Public Const NewUserPass = "-1"
    Public Const NewUserGrpe = "1"

    'Filiale.Typ ist Produktions-Filiale
    Public Const ProduktionsFiliale = 4

    'WinBack-Server-Task Port
    Public Const WinBackServerTaskPort = "22046"
End Class
