use [parking]
GO

SELECT
	I.time AS invoice_time
FROM
	invoice AS I
JOIN
	payment AS P
ON
	P.code = I.payment_code
ORDER BY
	invoice_time DESC