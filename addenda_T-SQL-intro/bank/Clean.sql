USE [test]
GO

SELECT * FROM Conto
SELECT * FROM Indirizzo
SELECT * FROM Operazione
SELECT * FROM Cliente


DELETE FROM Conto
DELETE FROM Cliente
DELETE FROM Indirizzo
DELETE FROM Operazione

-- Truncate also restores auto-incrementing (IDENTITY) keys
-- Will fail because of foreign keys (?)
--TRUNCATE TABLE Conto
--TRUNCATE TABLE Cliente
--TRUNCATE TABLE Indirizzo

ALTER TABLE Conto
	DROP COLUMN iban

ALTER TABLE Conto
	ADD iban char(27) NOT NULL UNIQUE
	-- ADD UNIQUE(iban)