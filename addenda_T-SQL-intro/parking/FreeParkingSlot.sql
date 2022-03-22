USE [parking]
GO

CREATE PROC FreeParkingSlot (@license_plate CHAR(7), @mgs VARCHAR(MAX) OUT)
AS
	DECLARE @unit_price int = 4

	DECLARE @elapsed_time_h INT = DATEPART(hour, GETDATE() - (SELECT entrance FROM slots WHERE license_plate = @license_plate))

	SET @mgs = CONCAT('Due payment: ', @unit_price * @elapsed_time_h, '$')

	UPDATE slots
		SET
			license_plate = NULL,
			vehicle = NULL,
			entrance = NULL
		WHERE
			license_plate = @license_plate