Public Class wb_Global

    Enum ProgVariante
        OrgaBack                    'Programm läuft als Addin unter OrgaBack
        WinBack                     'Programm läuft als Standalone
    End Enum

    Enum KomponTypen
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

        KO_TYPE_UNDEFINED
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
        MessageTextLinie      '20/0/LNr
        MessageTextUser       '20/1/UsrNr
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
End Class
