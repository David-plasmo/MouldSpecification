USE [ProdPlasmoProduct]
GO
/****** Object:  StoredProcedure [dbo].[tools_UPSERT_Builder]    Script Date: 1/04/2025 9:15:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
This is a simple, single-table CRUD Generator. It does not have a bunch of 
bells and whistles, and is easy to follow and modify. I wrote this to make
my job easier, and I am sharing it with you to do with it as you wish.

The Basics:
The TSQL below will create 3 procedures:
    1. An Upsert Procedure: Suffix _ups 
    2. A Select Procedure: Suffix _sel
    3. A Delete Procedure: Suffix _del

A Little More Detail:
Things you should know:
    All 3 procedures have a parameter called @MyID which is used to set 
    the Context, so that my audit procedures get the validated user. If you
    Have no use for it, you'll need to remove the piece of generator code 
    that adds it as a parameter to each of the 3 procedures. You will also
    need to remove the PRINT statement for each procedure that looks like:

            PRINT N'  SET CONTEXT_INFO @MyID;' + CHAR(13) + CHAR(10) 

    This generator expects to perform inserts, updates, and deletes on a
    table, and selects from a view. If you want to perform selects directly
    from the table, simply use the table name in both @TableName and 
    @ViewName.

The Upsert Procedure:
    If ID (Primary Key) is supplied it will perform an Update. Otherwise it 
    will perform an Insert. This generator is hard-coded to avoid inserting
    or updating these particular fields:
                                        Created
                                        CreatedBy
                                        Modified
                                        ModifiedBy
                                        RowVersion
                                        <The Primary Key Field>
    That's because in my databases I use those field names for audit, and they
    are never modified except internally within the database. You can modify
    the part of this procedure that performs this function to suit your needs.

    This generator always uses the Parameter name @ID to represent the Primary
    key defined for the table. This is my preference but you can modify to suit.

The Select Procedure:
    If ID (Primary Key) is supplied it will select a single row from the View 
    (Table) whose name you provide. Otherwise it will select all rows. If the 
    @ISACTIVE_SEL variable is set to 1 (True), then the Select Procedure expects
    your View (Table) to have a bit-type field named 'IsActive'. My tables are 
    standardized to this. If @ISACTIVE_SEL = 1 the Select Procedure will have an
    additional parameter called @IsActive (bit). When @ID is not supplied, and 
    @IsActive is not supplied, the procedure selects all rows. When @ID is not
    supplied, and @IsActive is supplied, the procedure selects all rows where
    the field IsActive matches the parameter @IsActive

The Delete Procedure:
    The Delete Pocedure requires that the Key value and the RowVersion value
    be supplied. I use an Int type RowVersion, so if you use TimeStamp (varbinary(128))
    then you will need to tweak the generator.


    --Casey W Little
    --Kaciree Software Solutions, LLC
    Version 1.00
*/

--Type Your Database Name in this Use statement:
--USE [PlasmoAdmin]
--GO

--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

ALTER PROCEDURE [dbo].[tools_UPSERT_Builder]
(
  
  --@ProcName nvarchar(100)=N'Customer';
  --@DBRoleName nvarchar(100)=N'db_owner';
  @TableName nvarchar(100),
  --@ViewName nvarchar(100)=N'Customer' --N'<Your View Name>';
  @OrderBy nvarchar(100)=NULL --N'<Your Field Name>';
  --@OrderByDir nvarchar(4)=NULL --N'ASC';
  --@AUTHOR nvarchar(50) ='Casey W Little';
  --@DESC nvarchar(100) = @ProcName; -- Ex. 'User Data' will return 'Description : Upsert User Data'
)
AS
/*MODIFY THE VALUES BELOW TO SUIT YOUR NEEDS*/
--DECLARE @DBName nvarchar(100)=N'Shopify';
DECLARE @DBName nvarchar(100) = 'ProdPlasmoProduct'
DECLARE @ProcName nvarchar(100)=@TableName;
DECLARE @DBRoleName nvarchar(100)=N'db_owner';
--DECLARE @TableName nvarchar(100)=N'Customer';
DECLARE @ViewName nvarchar(100)=@TableName --N'<Your View Name>';
--DECLARE @OrderBy nvarchar(100)=NULL --N'<Your Field Name>';
DECLARE @OrderByDir nvarchar(4)=NULL --N'ASC';
DECLARE @AUTHOR nvarchar(50) ='Casey W Little';
DECLARE @DESC nvarchar(100) = @ProcName; -- Ex. 'User Data' will return 'Description : Upsert User Data'
DECLARE @ISACTIVE_SEL bit =0; --Set to 1 if your table has a Bit field named IsActive
DECLARE @PKDataType nvarchar(50)
/*DO NOT MODIFY BELOW THIS LINE!!!*/

DECLARE @NNND char(23) ='NOT_NULLABLE_NO_DEFAULT';
DECLARE @NNWD char(22) ='NOT_NULLABLE_W_DEFAULT';
DECLARE @NBLE char(8) ='NULLABLE';
DECLARE @LEGEND nvarchar(max);
DECLARE @PRIMARY_KEY nvarchar(100);

DECLARE @last_updated_on datetime2 = null;
DECLARE @last_updated_by varchar(50) = null;
DECLARE @last_updated bit = 0;


--Set up Legend
    SET @LEGEND = N'USE [' + @DBName + N'];' + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'GO' + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'SET ANSI_NULLS ON' + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'GO' + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'SET QUOTED_IDENTIFIER ON' + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'GO' + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + CHAR(13) + CHAR(10) 

    SET @LEGEND = @LEGEND + N'-- ===================================================================' + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'-- Author      : ' + @AUTHOR + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'-- Create date : ' + CONVERT(nvarchar(30),GETDATE(),101) + CHAR(13) + CHAR(10) 
    SET @LEGEND = @LEGEND + N'-- Revised date: ' + CHAR(13) + CHAR(10) 

--Get Primary Key Field
SELECT TOP 1 @PRIMARY_KEY = COLUMN_NAME 
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND TABLE_NAME = @TableName AND TABLE_CATALOG = @DBName;

DECLARE TableCol Cursor FOR 
SELECT c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH
    , IIF(c.COLUMN_NAME='RowVersion',@NBLE,IIF(c.COLUMN_NAME=@PRIMARY_KEY,@NBLE,IIF(c.IS_NULLABLE = 'NO' AND c.COLUMN_DEFAULT IS NULL,@NNND,IIF(c.IS_NULLABLE = 'NO' AND c.COLUMN_DEFAULT IS NOT NULL,@NNWD,@NBLE)))) AS [NULLABLE_TYPE]
FROM INFORMATION_SCHEMA.Columns c INNER JOIN
    INFORMATION_SCHEMA.Tables t ON c.TABLE_NAME = t.TABLE_NAME
WHERE t.Table_Catalog = @DBName
    AND t.TABLE_TYPE = 'BASE TABLE'
    AND t.TABLE_NAME = @TableName
ORDER BY [NULLABLE_TYPE], c.ORDINAL_POSITION;

DECLARE @TableSchema varchar(100), @cTableName varchar(100), @ColumnName varchar(100);
DECLARE @DataType varchar(30), @CharLength int, @NullableType varchar(30);

DECLARE @PARAMETERS nvarchar(max);
DECLARE @INSERT_FIELDS nvarchar(max),@INSERT_VALUES nvarchar(max);
DECLARE @UPDATE_VALUES nvarchar(max);
--DECLARE @UPDATE_VALUES2 nvarchar(max);
DECLARE @LastCommaPos int 

--SET @PARAMETERS = '@MyID int,';
SET @PARAMETERS = '' --CAST('' AS nvarchar(max))
SET @INSERT_FIELDS = '' --CAST('' AS nvarchar(max))
SET @INSERT_VALUES = '' --CAST('' AS nvarchar(max))
SET @UPDATE_VALUES = '' --CAST('' AS nvarchar(max))

-- open the cursor
OPEN TableCol

-- get the first row of cursor into variables
FETCH NEXT FROM TableCol INTO @TableSchema, @cTableName, @ColumnName, @DataType, @CharLength, @NullableType

WHILE @@FETCH_STATUS = 0
    BEGIN
        --IF @ColumnName NOT IN('Created','CreatedBy','Modified','ModifiedBy')
        --BEGIN
            --SET @PARAMETERS=@PARAMETERS + '@' + IIF(@ColumnName=@PRIMARY_KEY,'ID',@ColumnName) + ' ' + iif(@CharLength IS NULL,@DataType,@DataType + '(' + 
            --    CAST(@CharLength AS nvarchar(10)) + ')') +  IIF(@NullableType=@NNND OR @NullableType=@NNWD,',','=NULL,');			
			SET @PARAMETERS=@PARAMETERS 
			+ '  @' + IIF(@ColumnName=@PRIMARY_KEY,
			              @PRIMARY_KEY,
						  @ColumnName) 
			+ ' ' + iif(@CharLength IS NULL,
			              @DataType,
						  @DataType + '(' + CAST(@CharLength AS nvarchar(10)) 
									+ ')') +  IIF(@NullableType=@NNND OR @NullableType=@NNWD,
												    '',
												    '=NULL')
		          +  iif(@ColumnName=@PRIMARY_KEY OR @ColumnName IN('last_updated_by','last_updated_on'),' OUTPUT,', ',')
				  + CHAR(13) + CHAR(10) 
            --Save primary key datatype for future reference
            IF @ColumnName=@PRIMARY_KEY	
			BEGIN
			  SET @PKDataType = @DataType
			END

			--set flag for tracking the updating user and time
			IF SUBSTRING(@ColumnName,1,12) = 'last_updated'
			  SET @last_updated = 1

            IF @ColumnName <> @PRIMARY_KEY AND @ColumnName <> N'RowVersion'	
			  AND @ColumnName <> 'last_updated_on' AND @ColumnName <> N'last_updated_by'	
            BEGIN
                    SET @INSERT_FIELDS=@INSERT_FIELDS + '        [' + @ColumnName + '],' + CHAR(13) + CHAR(10);
                    --SET @INSERT_VALUES=@INSERT_VALUES + '@' + IIF(@ColumnName=@PRIMARY_KEY,'ID',@ColumnName) + ',';
					SET @INSERT_VALUES=@INSERT_VALUES + '        @' + IIF(@ColumnName=@PRIMARY_KEY,@PRIMARY_KEY,@ColumnName) + ',' + CHAR(13) + CHAR(10);
                    --IF LEN(@UPDATE_VALUES) < 3800
					  SET @UPDATE_VALUES=@UPDATE_VALUES + '        [' + @ColumnName + ']=@' 
						+ IIF(@ColumnName=@PRIMARY_KEY,@PRIMARY_KEY,@ColumnName) + ','  + CHAR(13) + CHAR(10);
					--ELSE
					--  SET @UPDATE_VALUES=@UPDATE_VALUES + '[' + @ColumnName + ']=@' 
					--	+ IIF(@ColumnName=@PRIMARY_KEY,@PRIMARY_KEY,@ColumnName) + ','  + CHAR(13) + CHAR(10);

            END
        --END

        FETCH NEXT FROM TableCol INTO @TableSchema, @cTableName, @ColumnName, @DataType, @CharLength, @NullableType
    END;

	
	--SET @PARAMETERS = LEFT(@PARAMETERS, LEN(@PARAMETERS) - 1);
	--SET @INSERT_FIELDS=LEFT(@INSERT_FIELDS,LEN(@INSERT_FIELDS)-1)
	--SET @INSERT_VALUES=LEFT(@INSERT_VALUES,LEN(@INSERT_VALUES)-1)
	--SET @UPDATE_VALUES=LEFT(@UPDATE_VALUES,LEN(@UPDATE_VALUES)-1)
	
	-- remove last comma from Parameters, taking account of CR-LF
	SET @PARAMETERS = RTRIM(@PARAMETERS)
	SET @LastCommaPos =  CHARINDEX(',', REVERSE(@PARAMETERS))  
	SET @PARAMETERS = STUFF(@PARAMETERS,LEN(@PARAMETERS) - @LastCommaPos + 1, 1, ' ') 	
	
	SET @INSERT_FIELDS = RTRIM(@INSERT_FIELDS)
	SET @LastCommaPos =  CHARINDEX(',', REVERSE(@INSERT_FIELDS))
	SET @INSERT_FIELDS = STUFF(@INSERT_FIELDS,LEN(@INSERT_FIELDS) - @LastCommaPos + 1, 1, ' ') 

	SET @INSERT_VALUES = RTRIM(@INSERT_VALUES)
	SET @LastCommaPos =  CHARINDEX(',', REVERSE(@INSERT_VALUES))
	SET @INSERT_VALUES = STUFF(@INSERT_VALUES,LEN(@INSERT_VALUES) - @LastCommaPos + 1, 1, ' ') 
	
	--IF LEN(@UPDATE_VALUES2) = 0
	--BEGIN
		SET @UPDATE_VALUES = RTRIM(@UPDATE_VALUES)
		SET @LastCommaPos =  CHARINDEX(',', REVERSE( @UPDATE_VALUES))
		SET @UPDATE_VALUES = STUFF( @UPDATE_VALUES,LEN( @UPDATE_VALUES) - @LastCommaPos + 1, 1, ' ') 
	--END
 --   ELSE
	--BEGIN
	--	SET @UPDATE_VALUES2 = RTRIM(@UPDATE_VALUES2)
	--	SET @LastCommaPos =  CHARINDEX(',', REVERSE( @UPDATE_VALUES2))
	--	SET @UPDATE_VALUES2 = STUFF( @UPDATE_VALUES2,LEN( @UPDATE_VALUES2) - @LastCommaPos + 1, 1, '') 
	--END
	

-- ----------------
-- clean up cursor
-- ----------------
CLOSE TableCol;
DEALLOCATE TableCol;

--Print Upsert Statement
    PRINT N'/****** Object:  StoredProcedure [dbo].[' + @ProcName + '_ups]    Script Date: ' + CAST(GETDATE() AS nvarchar(30)) + '  ******/' + CHAR(13) + CHAR(10) 
    PRINT @LEGEND;
    PRINT N'-- Description : Upsert ' + @DESC + CHAR(13) + CHAR(10) 
    PRINT N'-- ===================================================================' + CHAR(13) + CHAR(10) 
    PRINT CHAR(13) + CHAR(10) 
    PRINT N'CREATE PROCEDURE [dbo].[' + @ProcName  + '_ups]' + CHAR(13) + CHAR(10) 
	PRINT N'(' + CHAR(13) + CHAR(10) ;
	    
	EXEC LongPrint @PARAMETERS

	PRINT N')' + CHAR(13) + CHAR(10);
    PRINT N'AS' + CHAR(13) + CHAR(10) 
    PRINT N'BEGIN' + CHAR(13) + CHAR(10) 
    --PRINT N'  SET CONTEXT_INFO @MyID;' + CHAR(13) + CHAR(10) 
    --PRINT N'  IF @ID IS NULL OR @ID = 0' + CHAR(13) + CHAR(10) 
	PRINT N'  IF @' + @PRIMARY_KEY + ' IS NULL OR @' + @PRIMARY_KEY  + ' <= 0' + CHAR(13) + CHAR(10) 
	PRINT N'  BEGIN' + CHAR(13) + CHAR(10) 
	PRINT N'    INSERT INTO [dbo].[' + @TableName + ']' + CHAR(13) + CHAR(10) 
	PRINT N'    (' + CHAR(13) + CHAR(10) 

	EXEC LongPrint @INSERT_FIELDS  

	PRINT N'    )' 
	PRINT N'    VALUES' + CHAR(13) + CHAR(10) 
	PRINT N'    (' + CHAR(13) + CHAR(10) 
	
	EXEC LongPrint @INSERT_VALUES
		
	PRINT N'    )' 

	PRINT CHAR(13) + CHAR(10) 

	--PRINT N'      SELECT * FROM [dbo].[' + @ViewName + '] WHERE [ID] = SCOPE_IDENTITY();' + CHAR(13) + CHAR(10) 
	PRINT N'    SELECT @' + @PRIMARY_KEY + ' =  @@IDENTITY'  + CHAR(13) + CHAR(10) 
	IF @last_updated = 1
	BEGIN
	  PRINT N'    SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by '  + CHAR(13) + CHAR(10) 
	  PRINT N'      FROM [dbo].[' + @TableName + '] WHERE ' + @PRIMARY_KEY + '=@' + @PRIMARY_KEY + CHAR(13) + CHAR(10) 
	  PRINT CHAR(13) + CHAR(10) 
	END
	
	PRINT N'   END' + CHAR(13) + CHAR(10) 
	PRINT CHAR(13) + CHAR(10) 
	PRINT N'   ELSE' + CHAR(13) + CHAR(10) 
	PRINT N'   BEGIN' + CHAR(13) + CHAR(10) 	
    PRINT N'     UPDATE [dbo].[' + @TableName + ']' + CHAR(13) + CHAR(10) 
    PRINT N'     SET ' 

	EXEC LongPrint @UPDATE_VALUES 
	
    PRINT N'     WHERE ([' + @PRIMARY_KEY + '] = @' + @PRIMARY_KEY + ')'  + CHAR(13) + CHAR(10) 
    --PRINT N'      SELECT * FROM [dbo].[' + @ViewName + '] WHERE [ID] = @ID;' + CHAR(13) + CHAR(10) 
	PRINT CHAR(13) + CHAR(10) 
	IF @last_updateD = 1
	BEGIN
		PRINT N'     SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by '  + CHAR(13) + CHAR(10) 
		PRINT N'       FROM [dbo].[' + @TableName + '] WHERE ' + @PRIMARY_KEY + '=@' + @PRIMARY_KEY + CHAR(13) + CHAR(10) 
		PRINT CHAR(13) + CHAR(10) 
	END
	
    PRINT N'  END' + CHAR(13) + CHAR(10) 
    PRINT N'END' + CHAR(13) + CHAR(10) 
    PRINT N'GO' + CHAR(13) + CHAR(10) 
    PRINT CHAR(13) + CHAR(10) 

------Now add GRANT and DENY permissions to the Role
--    PRINT N'GRANT EXECUTE ON [dbo].[' + @ProcName + '_ups] TO [' + @DBRoleName + ']' + CHAR(13) + CHAR(10) 
--    PRINT N'GO' + CHAR(13) + CHAR(10) 
--    PRINT N'DENY VIEW DEFINITION ON [dbo].[' + @ProcName + '_ups] TO [' + @DBRoleName + ']' + CHAR(13) + CHAR(10) 
--    PRINT N'GO' + CHAR(13) + CHAR(10) 
--    PRINT CHAR(13) + CHAR(10) 
--    PRINT CHAR(13) + CHAR(10) 

    --Print Select Statement
    PRINT N'/****** Object:  StoredProcedure [dbo].[' + @ProcName + '_sel]    Script Date: ' + CAST(GETDATE() AS nvarchar(30)) + '  ******/' + CHAR(13) + CHAR(10) 
    PRINT @LEGEND;
    PRINT N'-- Description : Select ' + @DESC + CHAR(13) + CHAR(10) 
    PRINT N'-- ===================================================================' + CHAR(13) + CHAR(10) 
    PRINT CHAR(13) + CHAR(10) 
    PRINT N'CREATE PROCEDURE [dbo].[' + @ProcName  + '_sel]' + CHAR(13) + CHAR(10);
    --PRINT N'(@MyID int, @ID int=NULL' + IIF(@ISACTIVE_SEL = 1,', @IsActive bit=NULL','') + ')' + CHAR(13) + CHAR(10);
	PRINT N'(@' + @PRIMARY_KEY + ' ' + @PKDataType + ' = NULL)'  + CHAR(13) + CHAR(10)
    PRINT N'AS' + CHAR(13) + CHAR(10) 
    --PRINT N'BEGIN' + CHAR(13) + CHAR(10) 
    --PRINT N'  SET CONTEXT_INFO @MyID;' + CHAR(13) + CHAR(10) 
    --PRINT N'  IF @ID IS NULL OR @ID = 0' + CHAR(13) + CHAR(10) 

    --IF @ISACTIVE_SEL = 1
    --    BEGIN
    --        PRINT N'    BEGIN' + CHAR(13) + CHAR(10) 
    --        PRINT N'      IF @IsActive IS NULL' + CHAR(13) + CHAR(10) 
    --        PRINT N'        SELECT * FROM [dbo].[' + @ViewName + '] ORDER BY [' + @OrderBy + '] ' + @OrderByDir + ';' + CHAR(13) + CHAR(10) 
    --        PRINT N'      ELSE' + CHAR(13) + CHAR(10) 
    --        PRINT N'        SELECT * FROM [dbo].[' + @ViewName + '] WHERE [isActive] = @IsActive ORDER BY [' + @OrderBy + '] ' + @OrderByDir + ';' + CHAR(13) + CHAR(10) 
    --        PRINT N'    END' + CHAR(13) + CHAR(10) 
    --    END
    --ELSE
    PRINT N'SELECT * FROM [dbo].[' + @ViewName  + ']' + CHAR(13) + CHAR(10)  
	  + ' WHERE [' +  @PRIMARY_KEY + '] = @' + @PRIMARY_KEY + ' OR @' + @PRIMARY_KEY + ' IS NULL'  + CHAR(13) + CHAR(10)  
	IF @OrderBy IS NOT NULL
	BEGIN
		PRINT 'ORDER BY [' + @OrderBy + ']' + CHAR(13) + CHAR(10) 
    END
    --PRINT N'  ELSE' + CHAR(13) + CHAR(10) 
    --PRINT N'    SELECT * FROM [dbo].[' + @ViewName + '] WHERE [ID] = @ID;' + CHAR(13) + CHAR(10) 
    --PRINT N'END' + CHAR(13) + CHAR(10) 
    PRINT N'GO' + CHAR(13) + CHAR(10) 
    PRINT CHAR(13) + CHAR(10) 

----Now add GRANT and DENY permissions to the Role
    --PRINT N'GRANT EXECUTE ON [dbo].[' + @ProcName + '_sel] TO [' + @DBRoleName + ']' + CHAR(13) + CHAR(10) 
    --PRINT N'GO' + CHAR(13) + CHAR(10) 
    --PRINT N'DENY VIEW DEFINITION ON [dbo].[' + @ProcName +'_sel] TO [' + @DBRoleName + ']' + CHAR(13) + CHAR(10) 
    --PRINT N'GO' + CHAR(13) + CHAR(10) 
    --PRINT CHAR(13) + CHAR(10) 
    --PRINT CHAR(13) + CHAR(10) 

    --Print Delete Statement
    PRINT N'/****** Object:  StoredProcedure [dbo].[' + @ProcName + '_del]    Script Date: ' + CAST(GETDATE() AS nvarchar(30)) + '  ******/' + CHAR(13) + CHAR(10) 
    PRINT @LEGEND;
    PRINT N'-- Description : Delete ' + @DESC + CHAR(13) + CHAR(10) 
    PRINT N'-- ===================================================================' + CHAR(13) + CHAR(10) 
    PRINT CHAR(13) + CHAR(10) 
    PRINT N'CREATE PROCEDURE [dbo].[' + @ProcName  + '_del]' + CHAR(13) + CHAR(10);
    --PRINT N'  (@MyID int, @ID int, @RowVersion int)' + CHAR(13) + CHAR(10);
	PRINT N'(@' + @PRIMARY_KEY + ' ' + @PKDataType + ')'  + CHAR(13) + CHAR(10)
    PRINT N'AS' + CHAR(13) + CHAR(10) 
    --PRINT N'BEGIN' + CHAR(13) + CHAR(10) 
    --PRINT N'  SET CONTEXT_INFO @MyID;' + CHAR(13) + CHAR(10) 
    PRINT N'  SET NOCOUNT ON;' + CHAR(13) + CHAR(10) 
    --PRINT N'  DELETE FROM [dbo].[' + @TableName + '] WHERE [' + @PRIMARY_KEY + ']=@ID AND [RowVersion]=@RowVersion;' + CHAR(13) + CHAR(10) 
    --PRINT N'  SELECT @@ROWCOUNT as [Rows Affected];' + CHAR(13) + CHAR(10) 
    --PRINT N'END' + CHAR(13) + CHAR(10) 
	PRINT N'  DELETE FROM [dbo].[' + @TableName + '] WHERE [' + @PRIMARY_KEY + ']=@'  + @PRIMARY_KEY + CHAR(13) + CHAR(10) 
    PRINT N'GO' + CHAR(13) + CHAR(10) 
    PRINT CHAR(13) + CHAR(10) 

----Now add GRANT and DENY permissions to the Role
    --PRINT N'GRANT EXECUTE ON [dbo].[' + @ProcName + '_del] TO [' + @DBRoleName + ']' + CHAR(13) + CHAR(10) 
    --PRINT N'GO' + CHAR(13) + CHAR(10) 
    --PRINT N'DENY VIEW DEFINITION ON [dbo].[' + @ProcName +'_del] TO [' + @DBRoleName + ']' + CHAR(13) + CHAR(10) 
    --PRINT N'GO' + CHAR(13) + CHAR(10) 
