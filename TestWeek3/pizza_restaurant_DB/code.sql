--------------------------------------------
-- Create the database and all of its tables
--------------------------------------------

CREATE DATABASE pizza_restaurant
GO

USE [pizza_restaurant]
GO

DROP TABLE pizza
GO

CREATE TABLE pizza
(
	id TINYINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	name VARCHAR(25) NOT NULL UNIQUE,
	price MONEY CONSTRAINT NonNegativePrice CHECK(price >= 0)
)
GO

DROP TABLE ingredient
GO

CREATE TABLE ingredient
(
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	name VARCHAR(50) NOT NULL UNIQUE,
	price MONEY NOT NULL,
	qty_stored INT NOT NULL
)
GO

DROP TABLE pizza_x_ingredient
GO

-- NOTE: Since there would be a many-to-many relation between the pizza
--       table and the ingredient table (each pizza can include many
--       ingredients, the same ingredient can be common to different pizza),
--       the relation is resolved by means of this additional table
CREATE TABLE pizza_x_ingredient
(
	pizza_id TINYINT FOREIGN KEY REFERENCES pizza(id) NOT NULL,
	ingredient_id INT FOREIGN KEY REFERENCES ingredient(id) NOT NULL,
	ingredient_qty TINYINT CHECK(ingredient_qty >= 0) NOT NULL

	CONSTRAINT FK_pizza_x_ingredient PRIMARY KEY (pizza_id, ingredient_id)
)
GO


---------------------------
-- Create requested INDEXES
---------------------------

-- NOTE: A clustered index has already been added to the code field
--	     of the pizza table since it is set as primary key
CREATE NONCLUSTERED INDEX IX_pizza_name
    ON pizza(name)
GO

---------------------------------------
-- Populate tables in DB with some data
---------------------------------------

-- NOTE: much will be eventually done by means of stored procedures
--       (still to be created at this point)

INSERT INTO
	ingredient(name, price, qty_stored)
VALUES
	('Artichokes',             0.95, 500),
	('Basil',                  0.50, 3000),
	('Bresaola',               1.50, 200),
	('Cherry tomatoes',        0.40, 2000),
	('Fresh tomato',           0.65, 3000),
	('Gorgonzola cheese',      0.98, 1250),
	('Grana cheese',           0.88, 1400),
	('Ham',                    0.35, 2200),
	('Mozzarella',             0.24, 200),
	('Mozzarella di bufala',   0.70, 50),
	('Mushrooms',              1.00, 1000),
	('Olives',                 0.75, 700),
	('Porcini mushrooms',      1.25, 2000),
	('Potatoes',               0.30, 3000),
	('Provola cheese',         0.80, 750),
	('Ricotta cheese',         0.80, 3000),
	('Rocked salad',           0.70, 2000),
	('Sausage',                0.50, 1000),
	('Seasonal vegetables',    1.00, 500),
	('Smoked ham',             0.45, 1000),
	('Soft cheese',            1.00, 1000),
	('Spicy salami',           0.35, 1500),
	('Tomato',                 0.10, 100)
GO

-------------------------------------
-- Create requested STORED PROCEDURES
-------------------------------------

-- Stored procedure for adding a pizza record given name and price
CREATE OR ALTER PROC AddNewPizza(@name VARCHAR(25), @price MONEY, @msg VARCHAR(MAX) OUT)
AS
	BEGIN
		IF EXISTS (SELECT name FROM pizza WHERE name = @name)
			BEGIN
				SET @msg = 'Pizza already present in database'
				RETURN -1
			END

		INSERT INTO
			pizza(name, price)
		VALUES
			(@name, @price)
	END
GO

DECLARE @error_message VARCHAR(MAX)

-- Populate the pizza table with previously defined stored procedure
EXEC dbo.AddNewPizza 'Margherita',         5.00, @error_message
EXEC dbo.AddNewPizza 'Bufala',             7.00, @error_message
EXEC dbo.AddNewPizza 'Diavola',            6.00, @error_message
EXEC dbo.AddNewPizza 'Quattro stagioni',   6.50, @error_message
EXEC dbo.AddNewPizza 'Porcini',            7.00, @error_message
EXEC dbo.AddNewPizza 'Dioniso',            8.00, @error_message
EXEC dbo.AddNewPizza 'Ortolana',           8.00, @error_message
EXEC dbo.AddNewPizza 'Patate e salsiccia', 6.00, @error_message
EXEC dbo.AddNewPizza 'Pomodorini',         6.00, @error_message
EXEC dbo.AddNewPizza 'Quattro formaggi',   7.50, @error_message
EXEC dbo.AddNewPizza 'Caprese',            7.50, @error_message
EXEC dbo.AddNewPizza 'Zeus',               7.50, @error_message
GO

-- Stored procedure for assigning an ingredient to a pizza given pizza id and ingredient id
CREATE OR ALTER PROC AssignIngredientToPizza(@pizza_id TINYINT, @ingredient_id INT, @qty INT, @msg VARCHAR(MAX) OUT)
AS
	BEGIN
		DECLARE @matching_combos INT =
		(
			SELECT
				COUNT(*)
			FROM
				pizza_x_ingredient
			WHERE
				pizza_id = @pizza_id AND ingredient_id = @ingredient_id
		)

		IF @matching_combos > 0
			BEGIN
				UPDATE
					pizza_x_ingredient
				SET
					ingredient_qty = @qty
				WHERE
					pizza_id = @pizza_id AND ingredient_id = @ingredient_id
			END
		ELSE
			BEGIN
				INSERT INTO
					pizza_x_ingredient(pizza_id, ingredient_id, ingredient_qty)
				VALUES
					(@pizza_id, @ingredient_id, @qty)
			END
	END
GO

-- Populate the pizza_x_ingredient table with previously defined stored procedure
DECLARE @error_message VARCHAR(MAX)
EXEC dbo.AssignIngredientToPizza  1, 23, 1, @error_message
EXEC dbo.AssignIngredientToPizza  1,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  2, 23, 1, @error_message
EXEC dbo.AssignIngredientToPizza  2, 10, 1, @error_message
EXEC dbo.AssignIngredientToPizza  3, 23, 1, @error_message
EXEC dbo.AssignIngredientToPizza  3,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  3, 22, 1, @error_message
EXEC dbo.AssignIngredientToPizza  4, 23, 1, @error_message
EXEC dbo.AssignIngredientToPizza  4,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  4, 11, 1, @error_message
EXEC dbo.AssignIngredientToPizza  4,  1, 1, @error_message
EXEC dbo.AssignIngredientToPizza  4,  8, 1, @error_message
EXEC dbo.AssignIngredientToPizza  4, 12, 1, @error_message
EXEC dbo.AssignIngredientToPizza  5, 23, 1, @error_message
EXEC dbo.AssignIngredientToPizza  5,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  5, 13, 1, @error_message
EXEC dbo.AssignIngredientToPizza  6, 23, 1, @error_message
EXEC dbo.AssignIngredientToPizza  6,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  6, 21, 1, @error_message
EXEC dbo.AssignIngredientToPizza  6, 20, 1, @error_message
EXEC dbo.AssignIngredientToPizza  6,  7, 1, @error_message
EXEC dbo.AssignIngredientToPizza  7, 23, 1, @error_message
EXEC dbo.AssignIngredientToPizza  7,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  7, 19, 1, @error_message
EXEC dbo.AssignIngredientToPizza  8,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  8, 14, 1, @error_message
EXEC dbo.AssignIngredientToPizza  8, 18, 1, @error_message
EXEC dbo.AssignIngredientToPizza  9,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza  9,  4, 1, @error_message
EXEC dbo.AssignIngredientToPizza  9, 16, 1, @error_message
EXEC dbo.AssignIngredientToPizza 10,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza 10, 15, 1, @error_message
EXEC dbo.AssignIngredientToPizza 10,  6, 1, @error_message
EXEC dbo.AssignIngredientToPizza 10,  7, 1, @error_message
EXEC dbo.AssignIngredientToPizza 11,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza 11,  5, 1, @error_message
EXEC dbo.AssignIngredientToPizza 11,  3, 1, @error_message
EXEC dbo.AssignIngredientToPizza 12,  9, 1, @error_message
EXEC dbo.AssignIngredientToPizza 12,  3, 1, @error_message
EXEC dbo.AssignIngredientToPizza 12, 17, 1, @error_message
GO

-- Stored procedure for removing an ingredient from a pizza given pizza id and ingredient id
CREATE OR ALTER PROC RemoveIngredientFromPizza(@pizza_id TINYINT, @ingredient_id INT, @msg VARCHAR(MAX) OUT)
AS
	BEGIN
		IF (SELECT COUNT(*) FROM pizza_x_ingredient WHERE pizza_id = @pizza_id AND ingredient_id = @ingredient_id) = 0
			BEGIN
				SET @msg = 'The association between pizza and ingredient is not present in database'
				RETURN -1
			END

		DELETE FROM
			pizza_x_ingredient
		WHERE
			pizza_id = @pizza_id AND ingredient_id = @ingredient_id
	END
GO

-- Stored procedure for increasing price of all pizzas having a certain ingredient, givent ingredient id
CREATE OR ALTER PROC IncreaseBy10percentHavingIngredient(@ingredient_id INT, @msg VARCHAR(MAX) OUT)
AS
	BEGIN
		IF (SELECT COUNT(*) FROM ingredient WHERE id = @ingredient_id) = 0
			BEGIN
				SET @msg = 'Ingredient not present in database'
				RETURN -1
			END

		UPDATE
			pizza
		SET
			price = 1.1 * price
		WHERE
			id IN
			(
				SELECT
					pizza_id
				FROM
					pizza_x_ingredient
				WHERE
					ingredient_id = @ingredient_id
			)
	END
GO

-----------------------------
-- Create requested FUNCTIONS
-----------------------------

-- Function for returning the menu, ordered alphabetically
CREATE OR ALTER FUNCTION Menu () RETURNS @menu TABLE
(
	Pizza VARCHAR(25),
	Price MONEY,
	Ingredients VARCHAR(MAX)
)
AS
	BEGIN
		DECLARE @temp VARCHAR(MAX)

		INSERT INTO
			@menu
		SELECT
			P.name,
			P.price,
			STRING_AGG(I.name, ', ')
		FROM
			pizza AS P
		LEFT JOIN
			pizza_x_ingredient AS PXI ON P.id = PXI.pizza_id
		JOIN
			ingredient AS I ON PXI.ingredient_id = I.id
		GROUP BY P.price, P.name

		RETURN
	END
GO

SELECT * FROM Menu()
GO

-- Function for returning all pizzas in menu that contain an ingredient, given ingredient id
CREATE OR ALTER FUNCTION MenuWithIngredient(@ingredient_id INT) RETURNS TABLE
AS
	RETURN
		SELECT
			name AS 'Pizza',
			price AS 'Price'
		FROM
			pizza
		WHERE
			id IN
			(
				SELECT
					pizza_id
				FROM
					pizza_x_ingredient
				WHERE
					ingredient_id = @ingredient_id
			)
GO

-- Function for returning all pizzas in menu not containing an ingredient, given ingredient id
CREATE OR ALTER FUNCTION MenuWithoutIngredient(@ingredient_id INT) RETURNS TABLE
AS
	RETURN
		SELECT
			name AS 'Pizza',
			price AS 'Price'
		FROM
			pizza
		WHERE
			id NOT IN
			(
				SELECT
					pizza_id
				FROM
					pizza_x_ingredient
				WHERE
					ingredient_id = @ingredient_id
			)
GO

-- Function for returning the number of pizzas which contain an ingredient, given ingredient id
CREATE OR ALTER FUNCTION NumberOfPizzasWithIngredient(@ingredient_id INT) RETURNS TINYINT
AS
	BEGIN
		DECLARE @number TINYINT

		SELECT
			@number = COUNT(*)
		FROM
			dbo.MenuWithIngredient(@ingredient_id)

		RETURN @number
	END
GO

-- Function for returning the number of pizzas not containing an ingredient, given ingredient id
CREATE OR ALTER FUNCTION NumberOfPizzasWithoutIngredient(@ingredient_id INT) RETURNS TINYINT
AS
	BEGIN
		DECLARE @number TINYINT

		SELECT
			@number = COUNT(*)
		FROM
			dbo.MenuWithouthIngredient(@ingredient_id)

		RETURN @number
	END
GO

-------------------------
-- Create requested VIEWS
-------------------------

-- Create view for displaying the menu
CREATE OR ALTER VIEW ViewMenu
AS
	SELECT
			P.name AS 'Pizza',
			P.price AS 'Price',
			STRING_AGG(I.name, ', ') AS 'Ingredients'
		FROM
			pizza AS P
		LEFT JOIN
			pizza_x_ingredient AS PXI ON P.id = PXI.pizza_id
		JOIN
			ingredient AS I ON PXI.ingredient_id = I.id
		GROUP BY P.price, P.name
GO

--BEGIN TRANSACTION;
--	DECLARE
--		@deleted table (pizza_id TINYINT);

--	DELETE
--		PXI
--	OUTPUT
--		deleted.pizza_id INTO @deleted
--	FROM
--		pizza AS P
--	JOIN
--		pizza_x_ingredient AS PXI ON P.id = PXI.pizza_id
--	JOIN
--		ingredient AS I ON I.id = PXI.ingredient_id
--	WHERE
--		I.name = 'Artichokes';

--	DELETE
--		P
--	FROM
--		pizza AS P
--	JOIN
--		@deleted AS D ON D.pizza_id = P.id;
--COMMIT TRANSACTION;