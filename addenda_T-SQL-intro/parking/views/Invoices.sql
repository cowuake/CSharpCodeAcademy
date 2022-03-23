USE [parking]
GO

CREATE OR ALTER VIEW Invoices
AS
	SELECT -- I invoice, P payment, S slot, T vehicle typology
		I.issue_time AS 'Issue time',
		I.arrival_time AS 'Arrival time',
		I.departure_time AS 'Departure time',
		I.license_plate AS 'License plate',
		I.payed_amount AS 'Payed amount',
		CONCAT(P.code, ' - ', P.description) AS 'Payment detail',
		T.description AS 'Description',
		T.code AS 'Code'
	FROM
		invoice AS I
	JOIN
		payment AS P ON P.code = I.payment_code
	JOIN
		slot AS S ON S.code_char = I.payment_code --AND S.code_int = I.
	JOIN
		vehicle_type AS T ON S.typology = T.code