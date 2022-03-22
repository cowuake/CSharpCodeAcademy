USE [parking]
GO

CREATE PROC AssignParking (@license_plate CHAR(7), @vehicle VARCHAR(25), @typology TINYINT, @msg VARCHAR(MAX) OUT)
AS
	IF (SELECT COUNT(*) FROM slots WHERE typology = @typology AND license_plate IS NULL) = 0
	BEGIN
		SET @msg = 'No parking slots available for your class of vehicle'
		RETURN -1
	END

	DECLARE @avail_code_char CHAR(1) = (SELECT TOP(1) code_char FROM slots WHERE typology = @typology AND license_plate IS NULL)
	DECLARE @avail_code_int INT = (SELECT TOP(1) code_int FROM slots WHERE typology = @typology AND license_plate IS NULL)

	DECLARE @parking_slot_display VARCHAR(5) = CONCAT(@avail_code_char, '-', CAST(@avail_code_int AS VARCHAR(3)))

	SET @msg = CONCAT('You have been assigned to parking slot ', @parking_slot_display)

	UPDATE slots
		SET
			license_plate = @license_plate,
			vehicle = @vehicle,
			entrance = GETDATE()
		WHERE
			code_char = @avail_code_char AND code_int = @avail_code_int