USE [InjectionMouldMAN]
GO
/****** Object:  StoredProcedure [dbo].[Customer_ups]    Script Date: 31/03/2025 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Customer_ups]
(
  @CUSTNAME char(65),
  @CustomerID int=NULL OUTPUT,
  @CompDB char(2)=NULL,
  @CUSTNMBR char(15)=NULL,
  @CUSTCLAS char(15)=NULL,
  @CNTCPRSN char(61)=NULL,
  @ADRSCODE char(15)=NULL,
  @SHIPMTHD char(15)=NULL,
  @ADDRESS1 char(61)=NULL,
  @ADDRESS2 char(61)=NULL,
  @ADDRESS3 char(61)=NULL,
  @COUNTRY char(61)=NULL,
  @CITY char(35)=NULL,
  @STATE char(29)=NULL,
  @ZIP char(11)=NULL,
  @PHONE1 char(21)=NULL,
  @PHONE2 char(21)=NULL,
  @PHONE3 char(21)=NULL,
  @FAX char(21)=NULL,
  @PYMTRMID char(21)=NULL,
  @LOCNCODE char(11)=NULL,
  @last_updated_by varchar(50)=NULL OUTPUT,
  @last_updated_on datetime2=NULL OUTPUT 
)
AS
BEGIN
  IF @CustomerID IS NULL OR @CustomerID <= 0
  BEGIN
    INSERT INTO [dbo].[Customer]
    (
        [CUSTNAME],
        [CompDB],
        [CUSTNMBR],
        [CUSTCLAS],
        [CNTCPRSN],
        [ADRSCODE],
        [SHIPMTHD],
        [ADDRESS1],
        [ADDRESS2],
        [ADDRESS3],
        [COUNTRY],
        [CITY],
        [STATE],
        [ZIP],
        [PHONE1],
        [PHONE2],
        [PHONE3],
        [FAX],
        [PYMTRMID],
        [LOCNCODE] 
    )
    VALUES
    (
        @CUSTNAME,
        @CompDB,
        @CUSTNMBR,
        @CUSTCLAS,
        @CNTCPRSN,
        @ADRSCODE,
        @SHIPMTHD,
        @ADDRESS1,
        @ADDRESS2,
        @ADDRESS3,
        @COUNTRY,
        @CITY,
        @STATE,
        @ZIP,
        @PHONE1,
        @PHONE2,
        @PHONE3,
        @FAX,
        @PYMTRMID,
        @LOCNCODE 
    )

    SELECT @CustomerID =  @@IDENTITY
    SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
      FROM [dbo].[Customer] WHERE CustomerID=@CustomerID

   END

   ELSE
   BEGIN
     UPDATE [dbo].[Customer]
     SET 
        [CUSTNAME]=@CUSTNAME,
        [CompDB]=@CompDB,
        [CUSTNMBR]=@CUSTNMBR,
        [CUSTCLAS]=@CUSTCLAS,
        [CNTCPRSN]=@CNTCPRSN,
        [ADRSCODE]=@ADRSCODE,
        [SHIPMTHD]=@SHIPMTHD,
        [ADDRESS1]=@ADDRESS1,
        [ADDRESS2]=@ADDRESS2,
        [ADDRESS3]=@ADDRESS3,
        [COUNTRY]=@COUNTRY,
        [CITY]=@CITY,
        [STATE]=@STATE,
        [ZIP]=@ZIP,
        [PHONE1]=@PHONE1,
        [PHONE2]=@PHONE2,
        [PHONE3]=@PHONE3,
        [FAX]=@FAX,
        [PYMTRMID]=@PYMTRMID,
        [LOCNCODE]=@LOCNCODE 
     WHERE ([CustomerID] = @CustomerID)

     SELECT @last_updated_on = last_updated_on, @last_updated_by = last_updated_by 
       FROM [dbo].[Customer] WHERE CustomerID=@CustomerID

  END
END