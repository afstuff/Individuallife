
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE SPIL_PRG_LI_PART_MATURITY_GET_CLAIMS
	@pTBIL_CLM_RPTD_CLM_NO NVARCHAR(35)
	
AS
BEGIN

	SET NOCOUNT ON;

  SELECT TBIL_CLAIM_REPTED.*, TBIL_PRDCT_DTL_CAT, TBIL_POLICY_PREM_INFO.*, TBIL_POLICY_DET.*, tbil_ins_detail.*, TBIL_POL_PRM_SA_LC,  TBIL_POLY_ASSRD_CD, TBIL_INSRD_SURNAME +' '+ RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME, '')) AS 'ASSURED NAME', TBIL_PRDCT_DTL_DESC
 FROM TBIL_CLAIM_REPTED 
 INNER JOIN TBIL_PRODUCT_DETL ON TBIL_PRDCT_DTL_CODE=TBIL_CLM_RPTD_PRDCT_CD 
 inner join TBIL_POLICY_PREM_INFO ON TBIL_CLM_RPTD_POLY_NO = TBIL_POL_PRM_POLY_NO INNER JOIN TBIL_POLICY_DET ON TBIL_POLY_POLICY_NO = TBIL_CLM_RPTD_POLY_NO INNER JOIN tbil_ins_detail ON TBIL_POLY_ASSRD_CD = TBIL_INSRD_CODE
 WHERE  TBIL_CLM_RPTD_CLM_NO=@pTBIL_CLM_RPTD_CLM_NO
 
END
GO