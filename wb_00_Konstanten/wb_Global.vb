Public Class wb_Global
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
End Class
