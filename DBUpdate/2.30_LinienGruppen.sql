USE winback;
ALTER TABLE Liniengruppen ADD LG_BZ_Drucken VARCHAR(5);
ALTER TABLE Liniengruppen ADD LG_TZ_Drucken VARCHAR(5);
ALTER TABLE Liniengruppen ADD LG_TR_Drucken VARCHAR(5);
ALTER TABLE Liniengruppen ADD LG_BZ_Senden VARCHAR(1);
ALTER TABLE Liniengruppen ADD LG_TZ_Senden VARCHAR(1);
