USE [test]
GO

CREATE PROCEDURE UnblockAccount (@accountNumber INT)
AS
	IF (SELECT COUNT(*) FROM Conto WHERE numero = @accountNumber) = 0
		RETURN -1

	IF (SELECT bloccato FROM Conto WHERE numero = @accountNumber) = 0
		RETURN -2

	UPDATE Conto
	SET bloccato = 0, tentativi = 0 -- Unblock and reset attempts
	WHERE numero = @accountNumber
GO