USE [RicoveriOspedale]
GO

SELECT
	nome AS 'Drug',
	AVG(costo) AS 'Average price'
FROM
	Medicina
GROUP BY
	nome
GO

SELECT
	nome AS 'Drug',
	MIN(costo) AS 'Minimum price',
	MAX(Costo) AS 'Maximum price',
	AVG(costo) AS 'Average price'
FROM
	Medicina
GROUP BY
	nome
GO

-- Drugs having average price equal to minimum price
SELECT
	nome AS 'Drug',
	MIN(costo) AS 'Unique price',
	COUNT(*) AS 'Number of drugs'
FROM
	Medicina
GROUP BY
	nome
HAVING
	MIN(costo) = AVG(costo) AND COUNT(*) > 1
GO