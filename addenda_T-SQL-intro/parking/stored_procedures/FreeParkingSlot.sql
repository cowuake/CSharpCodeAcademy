USE [parking]
GO

CREATE OR ALTER PROC FreeParkingSlot (@license_plate CHAR(7), @mgs VARCHAR(MAX) OUT)
AS
	DECLARE
		@unit_price int = 4

	DECLARE
		--@elapsed_time_h INT = DATEPART(hour, GETDATE() - (SELECT entrance FROM slots WHERE license_plate = @license_plate))
		@elapsed_time_h INT = DATEDIFF(hour, (SELECT entrance FROM slot WHERE license_plate = @license_plate), GETDATE())

	SET
		@mgs = CONCAT('Due payment: ', @unit_price * @elapsed_time_h, '$')

	UPDATE slot
		SET
			license_plate = NULL,
			vehicle = NULL,
			entrance = NULL
		WHERE
			license_plate = @license_plate