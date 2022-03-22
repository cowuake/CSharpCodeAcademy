USE [test]
GO

CREATE PROCEDURE AccountAuthentication (@accountNumber INT, @pin CHAR(5), @msg VARCHAR(MAX) OUT)
AS
	IF (SELECT COUNT(*) FROM Conto WHERE numero = @accountNumber) = 0
	BEGIN
		SET @msg = 'Account does not exist in the database'
		RETURN -4
	END

	-- Return error if
	IF (SELECT bloccato FROM Conto WHERE numero = @accountNumber) = 1
	BEGIN
		SET @msg = 'The account has been already blocked'
		RETURN -3 -- BEGIN/END not required for single instruction
	END

	DECLARE @hashed varchar(max) = 
		CONVERT(varchar(max), HashBytes('SHA2_256', @pin), 2)

	DECLARE @userHash varchar(max)

	SET @userHash = (
		SELECT pin FROM Conto
		WHERE numero = @accountNumber
		)

	IF @hashed <> @userHash
	BEGIN
		UPDATE Conto
		SET tentativi = tentativi + 1
		WHERE numero = @accountNumber
			
		-- The account must be blocked after 3 successive failed accesses
		IF (SELECT tentativi FROM Conto WHERE numero = @accountNumber) = 3
		BEGIN
			UPDATE Conto
			SET bloccato = 1
			WHERE numero = @accountNumber

			SET @msg = ''
			RETURN -1
		END
		-- Failed login, account still accessible
		RETURN -2
	END

	UPDATE Conto
	SET tentativi = 0
	WHERE numero = @accountNumber
	--RETURN 0 (not needed, implicit)

--SELECT * FROM Conto