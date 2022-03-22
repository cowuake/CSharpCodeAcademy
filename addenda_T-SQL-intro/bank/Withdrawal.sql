USE [test]
GO

CREATE PROCEDURE WithdrawMoney (@accountNumber INT, @amount MONEY, @msg VARCHAR(MAX) OUT)
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

	DECLARE @balance MONEY = (SELECT saldo FROM CONTO WHERE numero = @accountNumber)

	BEGIN
		IF @balance < @amount
		BEGIN
			SET @msg = 'Impossibile to withdraw the specified amount, available balance is too low'
			RETURN -3
		END

		if (dbo.OutForCurrentMonth(@accountNumber) >= (SELECT plafond FROM Conto WHERE numero = @accountNumber) + @amount
		BEGIN
			SET @msg = 'Monthly plafond exceed, impossible to withdraw'
			RETURN -4
		END

		DECLARE @now DATETIME = GETDATE()

		UPDATE Conto
		SET
			saldo = saldo - @amount,
			saldo_p = saldo,
			data_a = @now
		WHERE
			numero = @accountNumber

		INSERT INTO
			Operazione (descrizione, data, importo, numero_conto)
		VALUES(
			'Standard withdrawal operation',
			@now,
			@amount,
			@accountNumber
		)
	END