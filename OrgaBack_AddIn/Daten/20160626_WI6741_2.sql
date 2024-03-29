USE [TEST300]
GO
/****** Object:  StoredProcedure [dbo].[pq_Produktionsauftrag]    Script Date: 26.06.2017 16:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alexandra Sefrin
-- Create date: 16.06.2017/AS
-- Change date: 26.06.2017/AS Feld ArtikelNrShort gelöscht, da es nicht benötigt wird 
-- Description:	Ermittelt die Produktionsaufträge aller Produktionsfilialen für ein bestimmtes Lieferdatum.
-- Parameter:   ProdFilNr1 - Filialnummer der Produktionsfiliale 1
--              ProdFilNr2 - Filialnummer zweiten Produktionsfiliale sofern vorhanden, sonst -1
--              VKuerzel1 - Vorfallkürzel der für die Filialbestellungen verwendet wird
--              VKuerzel2 - Weiteres Vorfallkürzel der für die Kundenbestellungen verwendet wird, sonst Leerstring
--              LieferDatum - Produktionsaufträge für dieses Lieferdatum ermitteln
-- =============================================
CREATE PROCEDURE [dbo].[pq_Produktionsauftrag] 
	@ProdFilNr1 SMALLINT = -1, 
	@ProdFilNr2 SMALLINT = -1,
	@VKuerzel1 VARCHAR(10) = '',
	@VKuerzel2 VARCHAR(10) = '',
	@LieferDatum as VARCHAR(8)  
		
AS
BEGIN
	SELECT GV.VorfallKürzel,                              -- Filialbedarfsmeldungen und Kundenbestellungen
		   GV.VorfallNr,
		   Pos.PositionNummer AS VorfallPosNr,    
		   Pos.LieferDatum,        
		   GV.Filialnummer AS FilialNr,                   -- Produktionsfilialnummer                                        
		   GV.KorrNr,                                     -- Korrespondenznummer der bestellenden Filiale oder des Kunden 
		   Pos.ArtikelNr,
		   Pos.Einheit,
		   Pos.Farbe,
		   Pos.Groesse,
		   Art.Kurztext AS ArtikelBezeichnung,                                 
		   Pos.Menge,
		   NULL AS ProduzierteMenge
	FROM GeschäftsVorfall AS GV

	INNER JOIN GeschäftsvorfallPosition AS Pos
	ON GV.VorfallKürzel= Pos.VorfallKürzel AND
	   GV.VorfallNr = Pos.VorfallNr AND
	   GV.Arbeitsplatz = Pos.ArbeitsPlatz

	   INNER JOIN Artikel AS Art
	   ON Pos.ArtikelNr = Art.ArtikelNr

	WHERE GV.Status ='VBU' AND
	      GV.FilialNummer IN (@ProdFilNr1, @ProdFilNr2) AND
	      Pos.LieferDatum = @LieferDatum AND 
	      GV.VorfallKürzel IN (@VKuerzel1, @VKuerzel2)
	
END
