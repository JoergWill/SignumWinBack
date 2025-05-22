USE winback;
ALTER TABLE ArbRZSchritte ADD ARS_ARZ_Index INTEGER(11) AFTER ARS_Beh_Nr;
CREATE INDEX ARS_ARZ_Index on ArbRZSchritte (ARS_ARZ_Index);
