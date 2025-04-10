USE [AccessImport]
GO
/****** Object:  StoredProcedure [dbo].[AppendInjectionMouldSpecification]    Script Date: 10/04/2025 2:20:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AppendInjectionMouldSpecification]
AS
TRUNCATE TABLE 
[InjectionMouldMAN].[dbo].[InjectionMouldSpecification]

INSERT INTO [InjectionMouldMAN].[dbo].[InjectionMouldSpecification]
           ([ItemID]
           ,[MouldNumber]
           ,[MouldLocation]
           ,[MouldOwner]
           ,[FamilyMould]
           ,[NoOfCavities]
           ,[NoOfPart]
           ,[PartSummary]
           ,[Operation]
           ,[OtherFeatures]
           ,[FixedHalf]
           ,[FixedHalfTemp]
           ,[MovingHalf]
           ,[MovingHalfTemp]
           ,[PremouldReq]
           ,[PostMouldReq]
           --,[AdditionalLabourReqd]
           --,[last_updated_by]
           --,[last_updated_on]
		  )
 (    
	SELECT [ItemID]
      ,[MouldNumber]
      ,[MouldLocation]
      ,[MouldOwner]
      ,[FamilyMould]
      ,[NoOfCavities]
      ,[NoOfPart]
      ,[PartSummary]
      ,[Operation]
      ,[OtherFeatures]
      ,[FixedHalf]
      ,[FixedHalfTemp]
      ,[MovingHalf]
      ,[MovingHalfTemp]
      ,[PremouldReq]
      ,[PostMouldReq]
  FROM [AccessImport].[dbo].[vuAccessMouldSpecification]
  --WHERE [MouldNumber] IS NOT NULL
 )
