USE winback;
ALTER TABLE Einheiten ADD E_obNr INTEGER DEFAULT 0;
UPDATE Einheiten SET E_obNr=0 WHERE E_LfdNr=11;
UPDATE Einheiten SET E_obNr=11 WHERE E_LfdNr=1;
UPDATE Einheiten SET E_obNr=12 WHERE E_LfdNr=15;
UPDATE Einheiten SET E_obNr=16 WHERE E_LfdNr=4;
UPDATE Einheiten SET E_obNr=20 WHERE E_LfdNr=30;