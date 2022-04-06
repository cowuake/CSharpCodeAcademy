--------------------------------------------
-- Create the database and all of its tables
--------------------------------------------

CREATE DATABASE ticketing;
GO

USE [ticketing]
GO

DROP TABLE category;
GO

CREATE TABLE category
(
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	name VARCHAR(50) NOT NULL,
	description VARCHAR(200) NULL,
	ticket_id INT NULL
);
GO

INSERT
	INTO
		category (name, description)
	VALUES
		('NOT ASSIGNED', 'Still to be assigned'),
		('Bullying' , 'Bullying is still common in workplaces!'),
		('Hardware' , 'Hardware-related issues, including defective peripherals'),
		('PowerShell' , 'All sorts of difficulties in PowerShell scripting'),
		('Security' , 'Security problems and reports, including asset-related issues'),
		('Windows' , 'Windows problems, including issues with automatic update')
GO

DROP TABLE ticket
GO

CREATE TABLE ticket
(
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	description VARCHAR(500) NOT NULL,
	opened DATE NOT NULL,
	customer VARCHAR(100) NOT NULL,
	state VARCHAR(10) NOT NULL DEFAULT 'new',
	category_id INT NOT NULL,

	CONSTRAINT CK_ticket_state CHECK(state in ('new', 'ongoing', 'resolved')),
	CONSTRAINT FK_ticket_category FOREIGN KEY (category_id) REFERENCES category(id)
)
GO

CREATE OR ALTER PROC InsertTicket(@description VARCHAR(500), @customer VARCHAR(100), @category VARCHAR(50), @msg VARCHAR(MAX) OUT)
AS
	BEGIN
		INSERT INTO
			ticket(description, opened, customer, category_id)
		VALUES
			(@description, GETDATE(), @customer, (SELECT id FROM category WHERE name = @category))
	END
GO

DECLARE @error_message VARCHAR(MAX)

-- Populate the ticket table with previously defined stored procedure
EXEC dbo.InsertTicket 'My machine does not run a UNIX-like operating system', 'Riccardo Mura', 'Windows', @error_message
EXEC dbo.InsertTicket 'My students keep using passwords in clear texts', 'Davide Maggiulli', 'Security',  @error_message
EXEC dbo.InsertTicket 'Accenture guys continuosly bully me', 'SomeAvanadeGuy', 'Bullying', @error_message
EXEC dbo.InsertTicket 'Not able to install Chocolatey on my Windows machine', 'SeriousGuy', 'PowerShell', @error_message
GO