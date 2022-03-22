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
		entrance DATETIME NULL,
		vehicle VARCHAR(25) NULL,

		typology INT
			CONSTRAINT Fk_vehicle_type
				FOREIGN KEY REFERENCES vehicle_type(code)

		CONSTRAINT Pk_slot PRIMARY KEY (code_char, code_int)
	)
GO

--IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'price_list' and xtype = 'U')
--	CREATE TABLE price_list
--	(
--		max_parking_time_h TINYINT,
--		price INT
--	)
--GO

INSERT INTO
	vehicle_type(description)
VALUES
	('car'), ('motorcycle'), ('truck'), ('bus'), ('wagon'), ('bicycles')

INSERT INTO
	slots (code_char, code_int, typology)
VALUES 
	('A', '1', 1), ('A', '2', 1), ('A', '3', 1), ('A', '4', 1), ('A', '5', 1), ('A', '6', 1), ('A', '7', 1),
	('B', '1', 2), ('B', '2', 2), ('B', '3', 2), ('B', '4', 2), ('B', '5', 2), ('B', '6', 2), ('B', '7', 2),
	('C', '1', 3), ('C', '2', 3), ('C', '3', 3), ('C', '4', 3), ('C', '5', 3), ('C', '6', 3), ('C', '7', 3),
	('D', '1', 4), ('D', '2', 4), ('D', '3', 4), ('D', '4', 4), ('D', '5', 4), ('D', '6', 4), ('D', '7', 4),
	('E', '1', 5), ('E', '2', 5), ('E', '3', 5), ('E', '4', 5), ('E', '5', 5), ('E', '6', 5), ('E', '7', 5),
	('F', '1', 6), ('F', '2', 6), ('F', '3', 6), ('F', '4', 6), ('F', '5', 6), ('F', '6', 6), ('F', '7', 6),
	('G', '1', NULL), ('G', '2', NULL), ('G', '3', NULL), ('G', '4', NULL), ('G', '5', NULL), ('G', '6', NULL), ('G', '7', NULL),
	('H', '1', NULL), ('H', '2', NULL), ('H', '3', NULL), ('H', '4', NULL), ('H', '5', NULL), ('H', '6', NULL), ('H', '7', NULL)

SELECT * FROM vehicle_type
SELECT * FROM slots

---------
-- TODO
---------
---- Populate DB [DONE]
---- Write STORED PROCEDURE that:
------ Given a vehicle, determines if it can access the parking (available  slots for given type)
------ If the vehicle is allowed, assigns it to first available slot
------ If the vehicle is not allowed, provides proper message
---- Write STORED PROCEDURE that:
------ Allows a vehicle to leave assign parking slot
------ Computes total price due (multiplies unit price by elapsed time / according to price list)