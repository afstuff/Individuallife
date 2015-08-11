USE [ABS_LIFE]
GO
/****** Object:  StoredProcedure [dbo].[SPIL_PRG_LI_CLM_MATURE]    Script Date: 08/05/2015 13:34:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SPIL_PRG_LI_CLM_MATURE]
--@pSTYPE NVARCHAR(35),
 @pTBIL_CLM_PAID_POLY_NO NVARCHAR(35)

AS
BEGIN

	SET NOCOUNT ON;
SELECT * FROM TBIL_CLAIM_PAID inner JOIN TBIL_POLICY_DET ON TBIL_CLM_PAID_POLY_NO = TBIL_POLY_POLICY_NO WHERE (TBIL_CLM_PAID_POLY_NO = @pTBIL_CLM_PAID_POLY_NO) AND ((TBIL_POLY_STATUS='A') OR (TBIL_POLY_STATUS='P')) 

END
