USE winback;
ALTER TABLE Komponenten ADD KA_zaehlt_zu_NWT_Gesamtmenge CHAR(1) NULL DEFAULT NULL AFTER KA_zaehlt_zu_RZ_Gesamtmenge;
UPDATE Komponenten SET KA_zaehlt_zu_NWT_Gesamtmenge='1' WHERE KA_zaehlt_zu_RZ_Gesamtmenge=3;
UPDATE Komponenten SET KA_zaehlt_zu_RZ_Gesamtmenge='1' WHERE KA_zaehlt_zu_RZ_Gesamtmenge=3;
