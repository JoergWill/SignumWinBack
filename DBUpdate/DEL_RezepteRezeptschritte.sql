USE winback;
DELETE FROM Rezepte WHERE RZ_Variante_Nr > 0;
DELETE FROM RezeptSchritte WHERE RS_RZ_Variante_Nr > 0;
USE wbdaten;
DELETE FROM HisRezepte WHERE H_RZ_Variante_Nr > 0;
DELETE FROM HisRezeptSchritte WHERE H_RS_RZ_Variante_Nr > 0;
