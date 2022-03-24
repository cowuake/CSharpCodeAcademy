USE[RicoveriOspedale]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'drug_price_history' and xtype = 'U')
	CREATE TABLE drug_price_history
	(
		code VARCHAR(50) NOT NULL,
		update_date DATETIME NOT NULL,
		previous_price MONEY NOT NULL,
		new_price MONEY NOT NULL,
		increment MONEY NOT NULL,

		CONSTRAINT PK_drug_price_history PRIMARY KEY (code, update_date)
	)
GO

CREATE OR ALTER TRIGGER RegisterDrugPriceChanges 
ON
	Medicina
AFTER
	UPDATE, INSERT
AS 
	INSERT INTO
		drug_price_history
		(
			code,
			update_date,
			previous_price,
			new_price,
			increment
		)
	SELECT
		I.codice,
		GETDATE(),
		ISNULL(D.costo, 0),
		I.costo,
		I.costo - ISNULL(D.costo, 0)
	FROM
		INSERTED I 
	LEFT JOIN
		DELETED D
	ON
		I.codice = D.codice
GO

ENABLE TRIGGER dbo.RegisterDrugPriceChanges ON Medicina
GO

CREATE OR ALTER TRIGGER AvoidMoreThan10percentDrugPriceIncrement
ON
	drug_price_history
AFTER
	UPDATE, INSERT
AS
	--DECLARE query = (SELECT H.previous_price FROM drug_price_history AS H JOIN Medicina as D ON  D.codice = H.code)
	UPDATE
		Medicina
	SET
		costo = SELECT H.previous_price FROM drug_price_history AS H JOIN Medicina as D ON  D.codice = H.code
GO