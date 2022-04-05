--------------------------------------------
-- Create the database and all of its tables
--------------------------------------------

CREATE DATABASE ticketing;
GO

USE [ticketing]
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

	CONSTRAINT CK_ticket_state CHECK(state in ('new', 'ongoing', 'resolved'))
)
GO


CREATE OR ALTER PROC InsertTicket(@description VARCHAR(500), @customer VARCHAR(100), @msg VARCHAR(MAX) OUT)
AS
	BEGIN
		INSERT INTO
			ticket(description, opened, customer)
		VALUES
			(@description, GETDATE(), @customer)
	END
GO

DECLARE @error_message VARCHAR(MAX)

-- Populate the ticket table with previously defined stored procedure
EXEC dbo.InsertTicket 'My machine does not run a UNIX-like operating system', 'Riccardo Mura', @error_message
EXEC dbo.InsertTicket 'My students keep using passwords in clear texts', 'Davide Maggiulli', @error_message
EXEC dbo.InsertTicket 'Accenture guys continuosly bully me', 'SomeAvanadeGuy', @error_message
EXEC dbo.InsertTicket 'Not able to install Chocolatey on my Windows machine', 'SeriousGuy', @error_message
GO

--CREATE OR ALTER PROC DeleteTicket(@id INT, @msg VARCHAR(MAX) OUT)
--AS
--	BEGIN
--		IF (SELECT COUNT(*) FROM ticket WHERE id = @id) = 0
--			BEGIN
--				SET @msg = 'Ticket with provided ID not present in database'
--				RETURN -1
--			END
				
		
--		DELETE
--			FROM
--				ticket
--			WHERE
--				id = @id

--		RETURN 0 -- Not needed
--	END
--GO