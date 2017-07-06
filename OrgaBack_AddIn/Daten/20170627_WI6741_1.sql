USE [ERH300]
GO

/****** Object:  Table [dbo].[ProduzierteWare]    Script Date: 27.06.2017 12:38:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProduzierteWare](
	[lfdNr] [int] NOT NULL,
	[FilialNr] [smallint] NOT NULL,
	[ProduktionsDatum] [varchar](8) NOT NULL,
	[SatzTyp] [varchar](1) NOT NULL,
	[ArtikelNr] [varchar](20) NOT NULL,
	[Einheit] [smallint] NOT NULL,
	[Farbe] [smallint] NOT NULL,
	[Groesse] [varchar](4) NOT NULL,
	[Menge] [money] NOT NULL,
	[ChargenNr] [varchar](15) NULL,
	[HaltbarkeitsDatum] [varchar](8) NULL,
 CONSTRAINT [PK_ProduzierteWare] PRIMARY KEY CLUSTERED 
(
	[lfdNr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProduzierteWare]  WITH CHECK ADD  CONSTRAINT [FK_ProduzierteWare_ArtikelFilialBestand] FOREIGN KEY([ArtikelNr], [Einheit], [Farbe], [Groesse], [FilialNr])
REFERENCES [dbo].[ArtikelFilialBestand] ([ArtikelNr], [Einheit], [Farbe], [Grösse], [Filialnummer])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[ProduzierteWare] CHECK CONSTRAINT [FK_ProduzierteWare_ArtikelFilialBestand]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Laufende Nummer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProduzierteWare', @level2type=N'COLUMN',@level2name=N'lfdNr'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Filialnummer der Produktionsfiliale' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProduzierteWare', @level2type=N'COLUMN',@level2name=N'FilialNr'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum YYYYMMDD an dem die Ware produziert wurde ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProduzierteWare', @level2type=N'COLUMN',@level2name=N'ProduktionsDatum'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'P – Produzierter Artikel; V – Unterartikel (Rohstoff)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProduzierteWare', @level2type=N'COLUMN',@level2name=N'SatzTyp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Produzierter Artikel oder Rohstoff' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProduzierteWare', @level2type=N'COLUMN',@level2name=N'ArtikelNr'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SatzTyp P: Menge des produzierten Artikels; SatzTyp V: Rohstoffmenge
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProduzierteWare', @level2type=N'COLUMN',@level2name=N'Menge'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Datum bis zu dem der Artikel noch haltbar ist' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProduzierteWare', @level2type=N'COLUMN',@level2name=N'HaltbarkeitsDatum'
GO

