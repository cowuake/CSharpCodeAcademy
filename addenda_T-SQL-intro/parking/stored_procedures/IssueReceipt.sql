USE [parking]
GO

CREATE OR ALTER PROCEDURE IssueReceipt
(
	@slot_code_char CHAR(1),
	@slot_code_int CHAR(1),
	@departure_time DATETIME = NULL,
	@payment_code CHAR(1),
	@msg VARCHAR(MAX) OUT
)
AS
	DECLARE
		@license_plate LICENSE_PLATE, -- CHAR(7)
		@arrival_time  DATETIME,
		@vehicle_type CHAR(1)

	SELECT
		@license_plate = license_plate,
		@arrival_time = entrance,
		@vehicle_type = typology
	FROM
		slot
	WHERE
		code_char = @slot_code_char AND code_int = @slot_code_int

	IF @license_plate IS NOT NULL
	BEGIN
		-- Compute elapsed (parking) time for applying the price list
		DECLARE
			@parking_time_h INT = CEILING(DATEDIFF(minute, @arrival_time, @departure_time)) / 60.0

		-- Compute due amount of money (which is assumed to be paid simultaneously...)
		DECLARE
			@amount MONEY =
			(
				SELECT
					TOP(1) price
				FROM
					price_list
				WHERE
					@parking_time_h >= to_time_h AND code = @vehicle_type
			)

		-- This actually makes FreeParkingSlot deprecated...
		UPDATE slot
			SET
				license_plate = NULL,
				vehicle = NULL,
				entrance = NULL
			WHERE
			code_char = @slot_code_char

		-- Add proper record to invoice table
		INSERT INTO
			invoice (issue_time, license_plate, arrival_time, departure_time, payed_amount, payment_code) 
		VALUES
			(GETDATE(), @license_plate, @arrival_time, @departure_time, @amount, @payment_code)
	END