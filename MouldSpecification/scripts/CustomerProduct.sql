USE [InjectionMouldMAN]
GO
/****** Object:  StoredProcedure [dbo].[CustomerProduct_ups]    Script Date: 31/03/2025 4:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================
-- Author      : Casey W Little
-- Create date : 01/10/2025
-- Revised date: 
-- Description : Upsert CustomerProduct
-- ===================================================================

ALTER PROCEDURE [dbo].[CustomerProduct_ups]
(
  @CustomerID int,
  @ItemID int,
  @CustomerProductID int=NULL OUTPUT 
)
AS
BEGIN
  IF @CustomerProductID IS NULL OR @CustomerProductID = 0
  BEGIN
    INSERT INTO [dbo].[CustomerProduct]
    (
        [CustomerID],
        [ItemID] 
    )
    VALUES
    (
        @CustomerID,
        @ItemID 
    )

    SELECT @CustomerProductID =  @@IDENTITY
    --SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
    --  FROM [dbo].[CustomerProduct] WHERE CustomerProductID=@CustomerProductID

   END

   ELSE
   BEGIN
     UPDATE [dbo].[CustomerProduct]
     SET 
        [CustomerID]=@CustomerID,
        [ItemID]=@ItemID 
     WHERE ([CustomerProductID] = @CustomerProductID)

     --SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
     --  FROM [dbo].[CustomerProduct] WHERE CustomerProductID=@CustomerProductID

  END
END
