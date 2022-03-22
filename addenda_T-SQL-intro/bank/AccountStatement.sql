CREATE PROCEDURE AccountStatement(@accountNumber INT, @beginDate DATE = NULL, @endDate DATE = NULL, @msg VARCHAR(MAX) OUT)
AS
	IF NOT EXISTS(SELECT * FROM Conto where numero = @accountNumber)
	BEGIN
		SET @msg = 'Account not present in database'
		RETURN -1
	END

	DECLARE @first DATE = ISNULL(@beginDate, CAST(DATEADD(m, -3, GETDATE()) AS DATE))
	DECLARE @last DATE = ISNULL(@endDate, CAST(GETDATE() AS DATE))

	IF (@endDate < @beginDate)
	BEGIN
		DECLARE @temp DATE = @beginDate
		SET @beginDate = @endDate
		SET @endDate = @temp
	END

	--DECLARE @first DATE = @beginDate
	--IF @first IS NULL
	--	SET @f = CAST(DATEADD(m, -3, GETDATE()) AS DATE)

	SELECT
		descrizione, data, importo
	FROM
		Operazione
	WHERE
		numero_conto = @accountNumber
			AND data BETWEEN @beginDate AND @endDate
	ORDER BY
		data