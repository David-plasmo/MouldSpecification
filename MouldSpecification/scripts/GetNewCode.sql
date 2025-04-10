USE [InjectionMouldMAN]
GO
/****** Object:  UserDefinedFunction [dbo].[GetNewCode]    Script Date: 10/04/2025 7:31:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
ALTER FUNCTION [dbo].[GetNewCode]
(
	-- Add the parameters for the function here
	--@SqlDbType varchar(50)
)
RETURNS varchar(31)
AS
BEGIN
	DECLARE @LastFormattedCode VARCHAR(10) = NULL
	DECLARE @NewFormattedCode varchar(10) = '[CODE-001]'
	DECLARE @TestNum int
	DECLARE @NumStr VARCHAR(3)

	SELECT @LastFormattedCode = MAX(ITEMNMBR) FROM MAN_Item WHERE CHARINDEX('[CODE-', ITEMNMBR) > 0
	IF @LastFormattedCode IS NOT NULL
	BEGIN
	  IF ISNUMERIC(SUBSTRING(@LastFormattedCode, 7,3)) = 1
	  BEGIN
	    SET @TestNum = CONVERT(int, SUBSTRING(@LastFormattedCode, 7,3)) + 1
		IF @TestNum < 1000
		  SET @NewFormattedCode = '[CODE-' + FORMAT(@TestNum, 'd3') + ']'
		ELSE
		BEGIN
		  SET @NewFormattedCode = NULL
		END
	  END
	END
	 
	RETURN @NewFormattedCode

END
