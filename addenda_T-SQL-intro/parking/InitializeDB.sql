CREATE DATABASE parking
GO

USE [parking]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'vehicle_type' and xtype = 'U')
	CREATE TABLE vehicle_type
	(
		code INT IDENTITY PRIMARY KEY,
		description varchar(100) NULL
	)
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'slots' and xtype = 'U')
	CREATE TABLE slots
	(
		code_char CHAR(1) NOT NULL CHECK(code_char LIKE '[A-Z]'),
		code_int TINYINT CHECK(code_int > 0),

		license_plate CHAR(7) NULL,
		entrance TIME NULL,
		vehicle VARCHAR(25) NULL,

		typology INT
			CONSTRAINT Fk_vehicle_type
				FOREIGN KEY REFERENCES vehicle_type(code)

		CONSTRAINT Pk_slot PRIMARY KEY (code_char, code_int)
	)
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'price_list' and xtype = 'U')
	CREATE TABLE price_list
	(
		
	)
GO

INSERT INTO
	slots (code_char, code_int)
VALUES 
	('A', '1'), ('A', '2'), ('A', '3'), ('A', '4'), ('A', '5'), ('A', '6'), ('A', '7'),
	('B', '1'), ('B', '2'), ('B', '3'), ('B', '4'), ('B', '5'), ('B', '6'), ('B', '7'),
	('C', '1'), ('C', '2'), ('C', '3'), ('C', '4'), ('C', '5'), ('C', '6'), ('C', '7'),
	('D', '1'), ('D', '2'), ('D', '3'), ('D', '4'), ('D', '5'), ('D', '6'), ('D', '7'),
	('E', '1'), ('E', '2'), ('E', '3'), ('E', '4'), ('E', '5'), ('E', '6'), ('E', '7'),
	('F', '1'), ('F', '2'), ('F', '3'), ('F', '4'), ('F', '5'), ('F', '6'), ('F', '7'),
	('G', '1'), ('G', '2'), ('G', '3'), ('G', '4'), ('G', '5'), ('G', '6'), ('G', '7'),
	('H', '1'), ('H', '2'), ('H', '3'), ('H', '4'), ('H', '5'), ('H', '6'), ('H', '7')

SELECT * FROM vehicle_type
SELECT * FROM slots

---------
-- TODO
---------
---- Populate DB [DONE]
---- Write STORED PROCEDURE that:
------ Given a vehicle, determines if it can access the parking (available  slots for given type)
------ If the vehicle is allowed, assigns slot to it
------ If the vehicle is not allowed, provides proper message
---- Write STORED PROCEDURE that:
------ Allows a vehicle to leave assign parking slot
------ Computes total price due (multiplies unit price by elapsed time - according to price list)