USE [test]
GO

CREATE PROCEDURE DepositMoney (@accountNumber INT, @amount INT, @msg VARCHAR(MAX) OUT)
AS
	IF (SELECT COUNT(*) FROM Conto WHERE numero = @accountNumber) = 0
	BEGIN
		RETURN -1
		SET @msg = 'The account does not exist in database'
	END

	IF (SELECT bloccato FROM Conto WHERE numero = @accountNumber) = 1
	BEGIN
		SET @msg = 'Cannot operate on blocked account'
		RETURN -2
	END

	BEGIN
		DECLARE @now DATETIME = GETDATE()
		
		UPDATE Conto
		SET
			saldo = saldo + @amount,
			saldo_p = saldo,
			data_a = @now
		WHERE
			numero = @accountNumber

		INSERT INTO
			Operazione (descrizione, data, importo, numero_conto)
		VALUES(
			'Standard deposit operation',
			@now,
			@amount,
			@accountNumber
		)
	END