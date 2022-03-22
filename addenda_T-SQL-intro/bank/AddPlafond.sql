ALTER TABLE Conto
ADD plafond INT NOT NULL DEFAULT(1000)

ALTER TABLE Conto
ADD CONSTRAINT CheckPlafond CHECK(plafond > 0 AND plafond % 500 = 0)

GO
--SELECT DATEADD(m, DATEDIFF(month, 0, GETDATE()),0)
--SELECT EOMONTH(GETDATE())

CREATE FUNCTION OutForCurrentMonth(@accountNumber INT) RETURNS money
BEGIN
	DECLARE @total INT

	SET @total = (
		SELECT
			SUM(importo)
		FROM Operazione
		WHERE numero_conto = @accountNumber AND descrizione = 'Withdrawal'
			AND (data BETWEEN 
					DATEADD(m,DATEDIFF(month, 0, GETDATE()),0)
						AND EOMONTH(GETDATE()))
	)
	RETURN ISNULL(@total, 0)
END