USE winback;
UPDATE AktionsTimer SET AT_Str1='TW Segment 0' WHERE AT_idx=0;
UPDATE AktionsTimer SET AT_Str1='TW Segment 1' WHERE AT_idx=1;
UPDATE AktionsTimer SET AT_Str1='TW Segment 2' WHERE AT_idx=2;
UPDATE AktionsTimer SET AT_Str1='TW Segment 3' WHERE AT_idx=3;
UPDATE AktionsTimer SET AT_Str1='TW Segment 4' WHERE AT_idx=4;
UPDATE AktionsTimer SET AT_Str1='TW Segment 5' WHERE AT_idx=5;
UPDATE AktionsTimer SET AT_Str1='TW Segment 6' WHERE AT_idx=6;
UPDATE AktionsTimer SET AT_Str1='TW Segment 7' WHERE AT_idx=7;
UPDATE AktionsTimer SET AT_Str1='TW Segment 8' WHERE AT_idx=8;
UPDATE AktionsTimer SET AT_Str1='TW Segment 9' WHERE AT_idx=9;
UPDATE AktionsTimer SET AT_Str1='TW Segment 9' WHERE AT_idx=9;

UPDATE AktionsTimer SET AT_Str1='WB Backup' WHERE AT_Quelle_Typ='winback_cron_save';
UPDATE AktionsTimer SET AT_Str1='WB Index' WHERE AT_Quelle_Typ='winback_cron_index';
UPDATE AktionsTimer SET AT_Str1='WB ArbRez' WHERE AT_Quelle_Typ='winback_cron_clear';
UPDATE AktionsTimer SET AT_Str1='WB Chargen' WHERE AT_Quelle_Typ='winback_cron_chargen';
UPDATE AktionsTimer SET AT_Str1='WB Test' WHERE AT_Quelle_Typ='winback_cron_test';
UPDATE AktionsTimer SET AT_Str1='WB Liefer' WHERE AT_Quelle_Typ='winback_cron_liefer';

INSERT IGNORE INTO `AktionsTimer` (`AT_idx`, `AT_Quelle_Art`, `AT_Quelle_Nr`, `AT_Quelle_Typ`, `AT_Ziel_Art`, `AT_Ziel_Nr`, `AT_Ziel_Aktion`, `AT_Ziel_Verzoegerung`, `AT_TimingArt`, `AT_Periode`, `AT_Str1`, `AT_Kommentar`) VALUES(81, 0, 0, 'office_chargen', 0, 0, 0, 0, 0, 3600, 'OB Chargen', 'Auswertung produzierte Chargen und Verbräuche');
INSERT IGNORE INTO `AktionsTimer` (`AT_idx`, `AT_Quelle_Art`, `AT_Quelle_Nr`, `AT_Quelle_Typ`, `AT_Ziel_Art`, `AT_Ziel_Nr`, `AT_Ziel_Aktion`, `AT_Ziel_Verzoegerung`, `AT_TimingArt`, `AT_Periode`, `AT_Str1`, `AT_Kommentar`) VALUES(82, 0, 0, 'office_nwt', 0, 0, 0, 0, 0, 3600, 'OB Nährwerte', 'Aktualisierung der Nährwerte und Allergene aus der Cloud');

