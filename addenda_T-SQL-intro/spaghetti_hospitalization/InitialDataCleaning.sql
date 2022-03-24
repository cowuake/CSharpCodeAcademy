USE [RicoveriOspedale]
GO

UPDATE
	Medicina
SET
	descrizione = TRIM(REPLACE(descrizione,'"',' '))