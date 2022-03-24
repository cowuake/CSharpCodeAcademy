USE [RicoveriOspedale]
GO

-- Example of multiline function
CREATE OR ALTER FUNCTION GetPatientKMedicalExaminations(@k INT) RETURNS @patients TABLE
(
	cf CHAR(16),
	first_name VARCHAR(200),
	last_name VARCHAR(200),
	medical_examinations INT
)
AS
	BEGIN
		SET @k = ISNULL(@k, 0)
		if @k = 0
			SET @k = 1
			INSERT INTO
				@patients
			SELECT
				P.cf, P.nome, P.cognome
			FROM
				Paziente AS P
			JOIN
				Visita E ON P.cf = E.cf
			GROUP BY
				P.cf, P.nome, P.cognome -- Otherwise, cannot use these with SELECT (?)
										-- GROUP BY without KEY can...
			HAVING
				COUNT(*) >= @k
			RETURN
	END