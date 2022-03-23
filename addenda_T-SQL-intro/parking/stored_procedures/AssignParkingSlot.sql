USE [parking]
GO

CREATE OR ALTER PROC AssignParking (@license_plate CHAR(7), @vehicle VARCHAR(25), @typology TINYINT, @msg VARCHAR(MAX) OUT)
AS
	DECLARE
		@avail_code_char CHAR(1),
		@avail_code_int INT

	SELECT TOP(1)
		@avail_code_char = code_char,
		@avail_code_int = code_int
	FROM
		slots
	WHERE
		(typology = @typology OR typology = 'all') AND license_plate IS NULL

	--IF (EXISTS(SELECT * FROM slots WHERE (typology = @typology OR typology = 'all') AND license_plate IS NULL))
	IF @avail_code_char IS NULL
	BEGIN
		SET
			@msg = 'No parking slots available for your class of vehicle'
		RETURN -1
	END

	DECLARE
		@parking_slot_display VARCHAR(5) = CONCAT(@avail_code_char, '-', CAST(@avail_code_int AS VARCHAR(3)))

	SET @msg = CONCAT('You have been assigned to parking slot ', @parking_slot_display)

	UPDATE slots
		SET
			license_plate = @license_plate,
			vehicle = @vehicle,
			entrance = GETDATE()
		WHERE
			code_char = @avail_code_char AND code_int = @avail_code_int