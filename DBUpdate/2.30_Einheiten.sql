USE winback;

INSERT IGNORE INTO `Einheiten` (`E_LfdNr`, `E_Typ`, `E_Einheit`, `E_Bezeichnung`, `E_Timestamp`) VALUES ('28', '1', 'memo', 'Memo', '2022-07-26 10:01:35');
INSERT IGNORE INTO `Einheiten` (`E_LfdNr`, `E_Typ`, `E_Einheit`, `E_Bezeichnung`, `E_Timestamp`) VALUES ('29', '1', 'amp', '', '2022-07-26 10:01:35');
INSERT IGNORE INTO `Einheiten` (`E_LfdNr`, `E_Typ`, `E_Einheit`, `E_Bezeichnung`, `E_Timestamp`) VALUES ('30', '1', 'm', 'Meter', '2022-07-26 10:01:35');
INSERT IGNORE INTO `Einheiten` (`E_LfdNr`, `E_Typ`, `E_Einheit`, `E_Bezeichnung`, `E_Timestamp`) VALUES ('31', '1', 'kg/Stk', '', '2022-07-26 10:01:35');

ALTER TABLE Einheiten ADD E_obNr INTEGER DEFAULT -99;
UPDATE Einheiten SET E_obNr=-99;
UPDATE Einheiten SET E_obNr=0 WHERE E_LfdNr=11;
UPDATE Einheiten SET E_obNr=11 WHERE E_LfdNr=1;
UPDATE Einheiten SET E_obNr=12 WHERE E_LfdNr=15;
UPDATE Einheiten SET E_obNr=16 WHERE E_LfdNr=4;
UPDATE Einheiten SET E_obNr=20 WHERE E_LfdNr=30;