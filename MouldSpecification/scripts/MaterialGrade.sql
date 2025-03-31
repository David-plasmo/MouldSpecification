USE [InjectionMouldMAN];
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================
-- Author      : Casey W Little
-- Create date : 03/31/2025
-- Revised date: 
-- Description : Upsert MaterialGrade
-- ===================================================================

ALTER PROCEDURE [dbo].[MaterialGrade_ups]
(
  @MaterialGradeID int=NULL OUTPUT,
  @MaterialID int=NULL,
  @MaterialGrade varchar(500)=NULL,
  @CostPerKg decimal=NULL,
  @Supplier varchar(100)=NULL,
  @Comment varchar(100)=NULL,
  @AdditionalNotes nvarchar(255)=NULL,
  @MachineType varchar(2)=NULL,
  @last_updated_by varchar(50)=NULL OUTPUT,
  @last_updated_on datetime2=NULL OUTPUT 
)
AS
BEGIN
  IF @MaterialGradeID IS NULL OR @MaterialGradeID <= 0
  BEGIN
    INSERT INTO [dbo].[MaterialGrade]
    (
        [MaterialID],
        [MaterialGrade],
        [CostPerKg],
        [Supplier],
        [Comment],
        [AdditionalNotes],
        [MachineType] 
    )
    VALUES
    (
        @MaterialID,
        @MaterialGrade,
        @CostPerKg,
        @Supplier,
        @Comment,
        @AdditionalNotes,
        @MachineType 
    )

    SELECT @MaterialGradeID =  @@IDENTITY
    SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
      FROM [dbo].[MaterialGrade] WHERE MaterialGradeID=@MaterialGradeID

   END

   ELSE
   BEGIN
     UPDATE [dbo].[MaterialGrade]
     SET 
        [MaterialID]=@MaterialID,
        [MaterialGrade]=@MaterialGrade,
        [CostPerKg]=@CostPerKg,
        [Supplier]=@Supplier,
        [Comment]=@Comment,
        [AdditionalNotes]=@AdditionalNotes,
        [MachineType]=@MachineType 
     WHERE ([MaterialGradeID] = @MaterialGradeID)

     SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
       FROM [dbo].[MaterialGrade] WHERE MaterialGradeID=@MaterialGradeID

  END
END