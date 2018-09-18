USE winback;
ALTER TABLE LinienGruppen ADD LG_BZ_Drucken VARCHAR(5);
ALTER TABLE LinienGruppen ADD LG_TZ_Drucken VARCHAR(5);
ALTER TABLE LinienGruppen ADD LG_TR_Drucken VARCHAR(5);
ALTER TABLE LinienGruppen ADD LG_BZ_Senden VARCHAR(1);
ALTER TABLE LinienGruppen ADD LG_TZ_Senden VARCHAR(1);
