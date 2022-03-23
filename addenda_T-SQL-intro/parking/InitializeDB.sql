CREATE DATABASE parking
GO

USE [parking]
GO

CREATE TYPE LICENSE_PLATE FROM CHAR(7)
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'vehicle_type' and xtype = 'U')
	CREATE TABLE vehicle_type
	(
		code CHAR(1) PRIMARY KEY CHECK(code LIKE '[A-Z]'),
		description varchar(100) NULL
	)
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'slots' and xtype = 'U')
	CREATE TABLE slot
	(
		code_char CHAR(1) NOT NULL CHECK(code_char LIKE '[A-Z]'),
		code_int TINYINT CHECK(code_int > 0),

		license_plate LICENSE_PLATE NULL,
		entrance DATETIME NULL,
		vehicle VARCHAR(25) NULL,

		typology CHAR(1)
			CONSTRAINT FK_vehicle_type
				FOREIGN KEY REFERENCES vehicle_type(code)

		CONSTRAINT
			PK_slot PRIMARY KEY (code_char, code_int)
	)
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'payment' and xtype = 'U')
	CREATE TABLE payment
	(
		code CHAR(1) PRIMARY KEY,
		description VARCHAR(50) NOT NULL
	)
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'invoice' and xtype = 'U')
	CREATE TABLE invoice
	(
		--invoice_code CHAR(10) PRIMARY KEY,
		number INT IDENTITY(1,1) PRIMARY KEY,
		issue_time DATETIME NOT NULL,

		license_plate LICENSE_PLATE NOT NULL,
		arrival_time DATETIME NOT NULL,
		departure_time DATETIME NOT NULL,

		payed_amount MONEY NOT NULL,
		payment_code CHAR(1) CONSTRAINT FK_invoice FOREIGN KEY REFERENCES payment(code), -- Attention to comma!

		CONSTRAINT Check_positive_amount CHECK(payed_amount >= 0),
		CONSTRAINT Check_time_invoice CHECK(departure_time > arrival_time)
	)
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'price_list' and xtype = 'U')
	CREATE TABLE price_list
	(
		from_time_h TINYINT NOT NULL,
		to_time_h TINYINT NULL,
		price MONEY NOT NULL,
		code CHAR(1) NOT NULL

		CONSTRAINT PK_price_list PRIMARY KEY (from_time_h, code)
		CONSTRAINT FK_price_list FOREIGN KEY (code) REFERENCES vehicle_type(code)
	)
GO

IF (NOT EXISTS(SELECT 1 FROM vehicle_type))
	BEGIN
		INSERT INTO
			vehicle_type(code, description)
		VALUES
			('C', 'car'), ('M', 'motorcycle'), ('T', 'truck'), ('B', 'bus'), ('W', 'wagon'), ('X', 'all')
	END
GO

IF (NOT EXISTS(SELECT 1 FROM slot))
	BEGIN
		INSERT INTO
			slot (code_char, code_int, typology)
		VALUES
			('A', '1', 'C'), ('A', '2', 'C'), ('A', '3', 'C'), ('A', '4', 'C'), ('A', '5', 'C'), ('A', '6', 'C'), ('A', '7', 'C'),
			('B', '1', 'M'), ('B', '2', 'C'), ('B', '3', 'C'), ('B', '4', 'C'), ('B', '5', 'M'), ('B', '6', 'M'), ('B', '7', 'M'),
			('C', '1', 'C'), ('C', '2', 'C'), ('C', '3', 'C'), ('C', '4', 'C'), ('C', '5', 'C'), ('C', '6', 'C'), ('C', '7', 'C'),
			('D', '1', 'M'), ('D', '2', 'M'), ('D', '3', 'M'), ('D', '4', 'M'), ('D', '5', 'M'), ('D', '6', 'M'), ('D', '7', 'M'),
			('E', '1', 'W'), ('E', '2', 'W'), ('E', '3', 'W'), ('E', '4', 'W'), ('E', '5', 'W'), ('E', '6', 'W'), ('E', '7', 'W'),
			('F', '1', 'W'), ('F', '2', 'W'), ('F', '3', 'W'), ('F', '4', 'W'), ('F', '5', 'W'), ('F', '6', 'W'), ('F', '7', 'W'),
			('G', '1', 'X'), ('G', '2', 'X'), ('G', '3', 'X'), ('G', '4', 'X'), ('G', '5', 'X'), ('G', '6', 'X'), ('G', '7', 'X'),
			('H', '1', 'X'), ('H', '2', 'X'), ('H', '3', 'X'), ('H', '4', 'X'), ('H', '5', 'X'), ('H', '6', 'X'), ('H', '7', 'X')
	END
GO

IF (NOT EXISTS(SELECT 1 FROM payment))
	BEGIN
		INSERT INTO
			payment
		VALUES
			('D', 'Debit card'), ('C', 'Credit card')
	END
GO

IF (NOT EXISTS(SELECT 1 FROM price_list))
	BEGIN
		INSERT INTO
			price_list(from_time_h, to_time_h, price, code)
		VALUES
			(0, 1, 5, 'C'), (2, 3, 10, 'C'), (4, 5 , 13, 'C'), (6, 6, 18, 'C'), (7, NULL, 20, 'C'),
			(0, 1, 3, 'M'), (2, 3, 7, 'M'), (4, 5 , 10, 'M'), (6, 6, 13, 'M'), (7, NULL, 15, 'M'),
			(0, 1, 7, 'W'), (2, 3, 12, 'W'), (4, 5 , 15, 'W'), (6, 6, 20, 'W'), (7, NULL, 25, 'W')
	END
GO


SELECT * FROM vehicle_type
SELECT * FROM slot

----------
-- TODO
----------
----------------
---- Populate DB [DONE]
---------------------------------
---- Write STORED PROCEDURE that:
------ Given a vehicle, determines if it can access the parking (available  slots for given type)
------ If the vehicle is allowed, assigns it to first available slot
------ If the vehicle is not allowed, provides proper message
---------------------------------
---- Write STORED PROCEDURE that:
------ Allows a vehicle to leave assign parking slot
------ Computes total price due (multiplies unit price by elapsed time)
---------------------------------
---- Extend the database so that:
------ An invoice is issued every time a parking slot is freed
------ Due amount is computed based on a price list which:
-------- Determines price based on the total parking time
-------- Applies different prices based on vehicle type