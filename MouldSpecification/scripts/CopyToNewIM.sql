USE [InjectionMouldMAN]
GO
/****** Object:  StoredProcedure [dbo].[AppendMAN_Item]    Script Date: 10/04/2025 5:30:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CopyToNewIM]
(	@FromItemID int,			-- identifier for row to copy
	@CustomerID int,			-- identifier for existing customer
	@ItemID int = NULL OUTPUT	-- identifier for new item
)
AS

--
-- Append a new record into MAN_Item, from existing
--
INSERT INTO InjectionMouldMAN.dbo.MAN_Item
(      
       [ITEMNMBR]
      ,[ITEMDESC]
      ,[AltCode]
      ,[ProductType]
      ,[GradeID]
      ,[ImageFile]
      ,[ComponentWeight]
      ,[SprueRunnerTotal]
      ,[TotalShotWeight]
      ,[CompDB]
      ,[ITMCLSCD]
      ,[CtnQty]
      ,[CartonID]
      ,[LabelTypeID]
      --,[BottleSize]
      --,[Style]
      --,[NeckSize]
      --,[Colour]
      --,[DangerousGood]
      --,[StockLine]
      --,[last_updated_on]
      --,[last_updated_by]
)  

(  SELECT        
       InjectionMouldMAN.[dbo].GetNewCode() AS [ITEMNMBR]
      ,'Copied from ' + RTRIM([ITEMDESC]) AS [ITEMDESC]
      ,[AltCode]
      ,[ProductType]
      ,[GradeID]
      ,[ImageFile]
      ,[ComponentWeight]
      ,[SprueRunnerTotal]
      ,[TotalShotWeight]
      ,[CompDB]
      ,[ITMCLSCD]
      ,[CtnQty]
      ,[CartonID]
      ,[LabelTypeID]
      --,[BottleSize]
      --,[Style]
      --,[NeckSize]
      --,[Colour]
      --,[DangerousGood]
      --,[StockLine]
      --,[last_updated_on]
      --,[last_updated_by]
  FROM InjectionMouldMAN.[dbo].[MAN_Item]
  WHERE ItemID = @FromItemID
)

SET  @ItemID =  SCOPE_IDENTITY()


--
-- Append a Customer Product for newly inserted item
--
INSERT INTO [dbo].[CustomerProduct]
           ([CustomerID]
           ,[ItemID])
     VALUES
           (@CustomerID
           ,@ItemID)

--
-- Material Composition
--
INSERT INTO [InjectionMouldMAN].[dbo].[MaterialComp]
 (          [MaterialGradeID]
           ,[ItemID]
           ,[Polymer123]
           ,[PolymerPercent]
           ,[RegrindMaxPC]
           ,[IsActive]
           --,[last_updated_by]
           --,[last_updated_on]
 )
 (
     SELECT 
       [MaterialGradeID]
      ,@ItemID
      ,[Polymer123]
      ,[PolymerPercent]
      ,[RegrindMaxPC]
      ,[IsActive]
      --,[last_updated_by]
      --,[last_updated_on]
  FROM [InjectionMouldMAN].[dbo].[MaterialComp]
 WHERE ItemID = @ItemID
) 

--
-- Masterbatch composition
--
INSERT INTO [dbo].[MasterBatchComp]
(           [MBID]
           ,[ItemID]
           ,[MB123]
           ,[MBPercent]
           ,[IsPreferred]
           ,[AdditiveID]
           ,[AdditivePC]
           --,[last_updated_by]
           --,[last_updated_on]
)
(
	SELECT [MBID]
      ,@ItemID
      ,[MB123]
      ,[MBPercent]
      ,[IsPreferred]
      ,[AdditiveID]
      ,[AdditivePC]
      --,[last_updated_by]
      --,[last_updated_on]
  FROM [InjectionMouldMAN].[dbo].[MasterBatchComp]
  WHERE ItemID = @ItemID
)

--
-- Injection Mould Specification
--
INSERT INTO [InjectionMouldMAN].[dbo].[InjectionMouldSpecification]
(           [ItemID]
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
           ,[AdditionalLabourReqd]
           --,[last_updated_by]
           --,[last_updated_on]
)

( SELECT 
       @ItemID
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
      ,[AdditionalLabourReqd]
      --,[last_updated_by]
      --,[last_updated_on]
  FROM  [InjectionMouldMAN].[dbo].[InjectionMouldSpecification]
  WHERE ItemID = @ItemID
)

--
-- Quality Control
--

INSERT INTO [InjectionMouldMAN].[dbo].[QualityControl]
           ([ItemID]
           ,[FinishedPTQC]
           ,[ProductSample]
           ,[CertificateOfConformance]
           ,[Notes]
           ,[LabelIcon]
           ,[Costing]
           --,[last_updated_by]
           --,[last_updated_on]
		   )
( SELECT 
       @ItemID
      ,[FinishedPTQC]
      ,[ProductSample]
      ,[CertificateOfConformance]
      ,[Notes]
      ,[LabelIcon]
      ,[Costing]
      --,[last_updated_by]
      --,[last_updated_on]
  FROM [InjectionMouldMAN].[dbo].[QualityControl]
  WHERE ItemID = @ItemID
)
    


     






