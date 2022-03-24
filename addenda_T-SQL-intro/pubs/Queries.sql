USE [pubs]
GO

-- 0. Column type is of type CHAR(12). Transform it so that padding spaces are deleted
ALTER TABLE titles DROP CONSTRAINT DF__titles__type__2D27B809
GO
ALTER TABLE titles ALTER COLUMN type VARCHAR(12) NOT NULL -- Will fail because of some FK constraint
GO
ALTER TABLE titles ADD CONSTRAINT DF_title_type_default DEFAULT('UNDECIDED') FOR type
GO
UPDATE titles SET type = RTRIM(type)
GO

---------------------------------
-- 1. Select all Psychology books
---------------------------------
SELECT
	*
FROM
	titles
WHERE
	type = '%psychology%'
GO

--------------------------------------------------------
-- 2. Select all books with a price of at least 20 Euros
--------------------------------------------------------
SELECT
	*
FROM
	titles
WHERE
	price >= 20
GO

--------------------------------------------------------------------------------
-- 3. Write function which returns all book with a price between 'min' and 'max'
--------------------------------------------------------------------------------
CREATE OR ALTER FUNCTION GetBooksInPriceRange_inline(@min MONEY, @max MONEY) RETURNS TABLE
AS
	RETURN SELECT * FROM titles WHERE price BETWEEN ISNULL(@min, 0) AND @max
GO
-- 3b
CREATE OR ALTER FUNCTION GetBooksInPriceRange(@min MONEY, @max MONEY) RETURNS @books_in_price_range TABLE
(
	id VARCHAR(6)
)
AS
	BEGIN
		IF @min IS NULL OR @min < 0
			SET @min = (SELECT MIN(price) FROM titles)

		IF @max IS NULL OR @max < @min
			SET @max = (SELECT MAX(price) FROM titles)

		INSERT INTO
			@books_in_price_range(id)
		SELECT
			title_id
		FROM
			titles
		WHERE
			price BETWEEN @min and @max

		RETURN
	END
GO

--SELECT * FROM GetBooksInPriceRange(NULL, 20)
--SELECT T.* FROM GetBooksInPriceRange(10, 20) AS A
--JOIN titles T ON T.title_id = A.id

--SELECT * FROM titles WHERE title_id IN
--(
--	SELECT id FROM GetBooksInPriceRange(10, 20)
--)

-------------------------------------------
-- 4. Select all book which were never sold
-------------------------------------------
SELECT
	*
FROM
	titles
WHERE
	title_id NOT IN (SELECT DISTINCT title_id FROM sales)
GO
-- 4b
SELECT
	T.*
FROM
	titles AS T
LEFT JOIN
	sales S ON S.title_id = T.title_id
WHERE
	S.ord_num IS NULL
GO

---------------------------------------
-- 5. Select books published by 'GGG&G'
---------------------------------------
SELECT
	*
FROM
	titles AS T
JOIN
	publishers AS P ON P.pub_id = T.pub_id
WHERE
	P.pub_name = 'GGG&G'
GO

-------------------------------------------------------------
-- 6. Select publishers which have published at least 6 books
-------------------------------------------------------------
SELECT
	P.pub_id, P.pub_name
FROM 
	titles AS T
JOIN
	publishers AS P ON T.pub_id = P.pub_id
GROUP BY
	P.pub_id
HAVING COUNT(*) >= 6
GO

-------------------------------------------------------
-- 7. Find titles sold in 1993 and order alphabetically
-------------------------------------------------------
SELECT
	T.title
FROM 
	titles AS T
JOIN
	sales AS S ON S.title_id = T.title_id
WHERE
	YEAR(S.ord_date) = 1993
ORDER BY
	T.title ASC
GO

---------------------------------------------------------------------------
-- 8. Name of the shops where the book 'Secrets of Silicon Valley' was sold
---------------------------------------------------------------------------
SELECT
	ST.stor_name
FROM
	sales AS S
JOIN
	stores ST ON ST.stor_id = S.stor_id
JOIN
	titles T ON S.title_id = T.title_id
WHERE
	T.title = 'Secrets of Silicon Valley'
GO

--------------------------------------------------
-- 9. Find titles of books sold in Washington (WA)
--------------------------------------------------
SELECT
	DISTINCT T.title
FROM
	sales AS S
JOIN
	stores ST ON ST.stor_id = S.stor_id
JOIN
	titles AS T ON T.title_id = S.title_id
WHERE
	ST.state = 'WA'
GO

---------------------------------------------------------------------------------
-- 10. Find titles of books by Sheryl Hunter and Anne Ringer, also display author
---------------------------------------------------------------------------------
SELECT
	T.title AS 'Book title',
	CONCAT(A.au_lname, ', ', A.au_fname) AS 'Author full name'
FROM
	titles AS T
JOIN
	titleauthor AS TA ON TA.title_id = T.title_id
JOIN
	authors AS A ON A.au_id = TA.au_id
WHERE
	(
		(A.au_lname = 'Hunter' AND  A.au_fname = 'Sheryl')
		OR
		(A.au_lname = 'Ringer' AND  A.au_fname = 'Anne')
	)
ORDER BY
	A.au_lname
GO

SELECT * FROM authors WHERE au_lname = 'Hunter'

-----------------------------------------------
-- 11. How many books written by Hunter Sheryl?
-----------------------------------------------
SELECT
	COUNT(*) AS 'Number of books written by Sheryl Hunter'
FROM
	titles AS T
JOIN
	titleauthor AS TA ON TA.title_id = T.title_id
JOIN
	authors AS A ON A.au_id = TA.au_id
WHERE
	A.au_lname = 'Hunter' AND  A.au_fname = 'Sheryl'
GO

-------------------------------------------------------------
-- 12. For each store, find more the worst and the best sales
-------------------------------------------------------------
SELECT
	ST.stor_name AS 'Store name',
	MIN(qty) AS 'Worst sale',
	MAX(qty) AS 'Best sale'
FROM
	stores AS ST
JOIN
	sales AS S ON ST.stor_id = S.stor_id
GROUP BY
	ST.stor_id, ST.stor_name
GO

--------------------------------------------------------------------------------
-- 12b. For each store, find more the worst and the best sales in terms of money
--------------------------------------------------------------------------------
SELECT
	ST.stor_name AS 'Store name',
	MIN(qty * T.price) AS 'Worst sale',
	MAX(qty * T.price) AS 'Best sale'
FROM
	stores AS ST
JOIN
	sales AS S ON ST.stor_id = S.stor_id
JOIN
	titles AS T ON T.title_id = S.title_id
GROUP BY
	ST.stor_id, ST.stor_name
GO

-----------------------------------------------------------------
-- 13. Apply 10% discount to products sold by Barnum's in Bologna
-----------------------------------------------------------------
INSERT INTO
	discounts(discounttype, stor_id, lowqty, highqty, discount)
 VALUES
(
	'10% discount',
	(SELECT TOP(1) stor_id FROM stores WHERE stor_name = 'Barnum''s' AND city = 'Bologna')
)


-----------------------------------------------
-- 14. Show stores wihout any discounts applied
-----------------------------------------------
SELECT
	*
FROM
	stores AS S
LEFT JOIN
	discounts AS D ON D.stor_id = S.stor_id
WHERE
	D.discount IS NULL
GO

----------------------------------------------------------------------
-- 15. Show stores wihout any discounts applied (without using JOINs!)
----------------------------------------------------------------------
SELECT
	*
FROM
	stores AS S
WHERE S.stor_id NOT IN
(
	SELECT
		stor_id
	FROM
		discounts
	WHERE
		stor_id IS NOT NULL
)
GO

-----------------------------------------------------------------------------------------
-- 16. Create view with book having at least 2 authors (include book id, title and price)
-- (Consider 0 for NULL prices)
-----------------------------------------------------------------------------------------
SELECT
	T.title_id AS 'Book ID',
	T.title AS 'Title',
	ISNULL(T.price, 0) AS 'Price'
FROM
	titles AS T
JOIN
	titleauthor TA ON TA.title_id = T.title_id
GROUP BY
	T.title_id,
	T.title,
	T.price
HAVING COUNT(*) >= 2

----------------------------------------------
-- 17. Show total sales for each month in 1993
----------------------------------------------
SELECT
	MONTH(ord_date) AS 'Month',
	SUM(qty) AS 'Total sales'
FROM
	sales
WHERE
	YEAR(ord_date) = 1993
GROUP BY
	MONTH(ord_date)
GO
	
--------------------------------------------------------------------------------
-- 18. Find number of books published by every publisher outside California (CA)
--------------------------------------------------------------------------------
SELECT
	P.pub_name AS 'Publisher',
	P.state AS 'State',
	COUNT(*) AS 'Number of published books'
FROM
	publishers AS P
JOIN
	titles AS T ON T.pub_id = P.pub_id
WHERE
	state <> 'CA'
GROUP BY
	P.pub_id, P.pub_name, P.state

---------------------------------------------------------------------------------------------------
-- 19. Show list of titles (not books, titles!) published by publishers having at least 8 employees
---------------------------------------------------------------------------------------------------
SELECT
	DISTINCT T.title AS 'Book title'
FROM
	titles as T
JOIN
	publishers AS P ON P.pub_id = T.pub_id
JOIN
	employee AS E ON E.pub_id = P.pub_id
GROUP BY
	T.title_id, T.title
HAVING
	COUNT(*) >= 8
GO

-------------------------------------------------------------------------------------------------------------------------------
-- 20. Show list of titles (not books, titles!) published by publishers whose employees have average retribution of at least 25
-------------------------------------------------------------------------------------------------------------------------------
-- IT'S A TRAP!!!
GO

----------------------------------------------------------------------------------------
-- 21. Modify the DB so that a price increase of over 15% after an UPDATE is NOT allowed
----------------------------------------------------------------------------------------
CREATE TRIGGER InhibitPriceIncreasesOver15percent ON titles FOR UPDATE
AS
	IF EXISTS
	(
		SELECT
			*
		FROM
			INSERTED AS I
		JOIN
			DELETED AS D ON D.title_id = I.title_id
		WHERE ABS(I.price - D.price) / I.price >= 0.15
	)
	BEGIN
		RAISERROR
		(
			'Impossible to update with a price increase over 15%',
			18, -- severity
			-1  -- state
		)
		ROLLBACK TRANSACTION
	END
GO

------------------------------------------------------------------------
-- 22. For every invoice, retrieve total price with and without discount
------------------------------------------------------------------------
SELECT
	S.ord_num AS 'Invoice code',
	FORMAT(SUM(S.qty * T.price), 'F2') AS 'Total price',
	FORMAT(SUM(S.qty * T.price * (1 - ISNULL(D.discount, 0) / 100)), 'F2') AS 'Discounted price',
	(CASE WHEN D.discount IS NOT NULL THEN 'YES' ELSE 'NO' END) AS 'Discount'
FROM
	sales AS S
JOIN
	titles T ON T.title_id = S.title_id
JOIN
	stores ST ON ST.stor_id = S.stor_id
LEFT JOIN
	discounts D ON D.stor_id = S.stor_id
GROUP BY
	S.ord_num, D.discount
ORDER BY
	S.ord_num
GO