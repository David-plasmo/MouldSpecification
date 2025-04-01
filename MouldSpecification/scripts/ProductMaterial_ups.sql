USE [PlasmoIntegration];
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================
-- Author      : Casey W Little
-- Create date : 03/31/2025
-- Revised date: 
-- Description : Upsert ProductMaterial
-- ===================================================================
ALTER PROCEDURE [dbo].[ProductMaterial_ups]
(
  @PmID int=NULL OUTPUT,
  @Code varchar(31)=NULL,
  @CompanyCode varchar(10)=NULL,
  @MaterialID int=NULL,
  @GradeID int=NULL,
  @DGNumber varchar(10)=NULL,
  @last_updated_on datetime2=NULL OUTPUT,
  @last_updated_by varchar(50)=NULL OUTPUT 
)
AS
BEGIN
  IF @PmID IS NULL OR @PmID <= 0
  BEGIN
    INSERT INTO [dbo].[ProductMaterial]
    (
        [Code],
        [CompanyCode],
        [MaterialID],
        [GradeID],
        [DGNumber] 
    )
    VALUES
    (
        @Code,
        @CompanyCode,
        @MaterialID,
        @GradeID,
        @DGNumber 
    )

    SELECT @PmID =  @@IDENTITY
    SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
      FROM [dbo].[ProductMaterial] WHERE PmID=@PmID

   END

   ELSE
   BEGIN
     UPDATE [dbo].[ProductMaterial]
     SET 
        [Code]=@Code,
        [CompanyCode]=@CompanyCode,
        [MaterialID]=@MaterialID,
        [GradeID]=@GradeID,
        [DGNumber]=@DGNumber 
     WHERE ([PmID] = @PmID)

     SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
       FROM [dbo].[ProductMaterial] WHERE PmID=@PmID

  END
END