USE [InjectionMouldMAN]
GO
/****** Object:  StoredProcedure [dbo].[MAN_Item_ups]    Script Date: 31/03/2025 2:03:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================================
-- Author      : Casey W Little
-- Create date : 03/31/2025
-- Revised date: 
-- Description : Upsert MAN_Item
-- ===================================================================

ALTER PROCEDURE [dbo].[MAN_Item_ups]
(
  @ITEMNMBR char(31),
  @ItemID int=NULL OUTPUT,
  @ITEMDESC char(101)=NULL,
  @AltCode varchar(31)=NULL,
  @ProductType varchar(2)=NULL,
  @GradeID int=NULL,
  @ImageFile varchar(200)=NULL,
  @ComponentWeight decimal=NULL,
  @SprueRunnerTotal decimal=NULL,
  @TotalShotWeight decimal=NULL,
  @CompDB varchar(5)=NULL,
  @ITMCLSCD char(11)=NULL,
  @CtnQty int=NULL,
  @CartonID int=NULL,
  @LabelTypeID int=NULL,
  @BottleSize char(11)=NULL,
  @Style char(11)=NULL,
  @NeckSize char(11)=NULL,
  @Colour char(11)=NULL,
  @DangerousGood bit=NULL,
  @StockLine bit=NULL,
  @last_updated_on datetime2=NULL OUTPUT,
  @last_updated_by varchar(50)=NULL OUTPUT 
)
AS
BEGIN
  IF @ItemID IS NULL OR @ItemID <= 0
  BEGIN
    INSERT INTO [dbo].[MAN_Item]
    (
        [ITEMNMBR],
        [ITEMDESC],
        [AltCode],
        [ProductType],
        [GradeID],
        [ImageFile],
        [ComponentWeight],
        [SprueRunnerTotal],
        [TotalShotWeight],
        [CompDB],
        [ITMCLSCD],
        [CtnQty],
        [CartonID],
        [LabelTypeID],
        [BottleSize],
        [Style],
        [NeckSize],
        [Colour],
        [DangerousGood],
        [StockLine] 
    )
    VALUES
    (
        @ITEMNMBR,
        @ITEMDESC,
        @AltCode,
        @ProductType,
        @GradeID,
        @ImageFile,
        @ComponentWeight,
        @SprueRunnerTotal,
        @TotalShotWeight,
        @CompDB,
        @ITMCLSCD,
        @CtnQty,
        @CartonID,
        @LabelTypeID,
        @BottleSize,
        @Style,
        @NeckSize,
        @Colour,
        @DangerousGood,
        @StockLine 
    )

    SELECT @ItemID =  @@IDENTITY
    SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
      FROM [dbo].[MAN_Item] WHERE ItemID=@ItemID

   END

   ELSE
   BEGIN
     UPDATE [dbo].[MAN_Item]
     SET 
        [ITEMNMBR]=@ITEMNMBR,
        [ITEMDESC]=@ITEMDESC,
        [AltCode]=@AltCode,
        [ProductType]=@ProductType,
        [GradeID]=@GradeID,
        [ImageFile]=@ImageFile,
        [ComponentWeight]=@ComponentWeight,
        [SprueRunnerTotal]=@SprueRunnerTotal,
        [TotalShotWeight]=@TotalShotWeight,
        [CompDB]=@CompDB,
        [ITMCLSCD]=@ITMCLSCD,
        [CtnQty]=@CtnQty,
        [CartonID]=@CartonID,
        [LabelTypeID]=@LabelTypeID,
        [BottleSize]=@BottleSize,
        [Style]=@Style,
        [NeckSize]=@NeckSize,
        [Colour]=@Colour,
        [DangerousGood]=@DangerousGood,
        [StockLine]=@StockLine 
     WHERE ([ItemID] = @ItemID)

     SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
       FROM [dbo].[MAN_Item] WHERE ItemID=@ItemID

  END
END
