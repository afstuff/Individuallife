USE [ABS_LIFE]
GO
/****** Object:  StoredProcedure [dbo].[SPIL_INS_CLAIMSREQUEST]    Script Date: 07/16/2015 14:55:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SPIL_SEL_CLAIMS_POL]
      @TBIL_CLM_RPTD_POLY_NO nvarchar(35)
       
AS
BEGIN
     SELECT * FROM TBIL_CLAIM_REPTED WHERE TBIL_CLM_RPTD_POLY_NO=@TBIL_CLM_RPTD_POLY_NO
END

