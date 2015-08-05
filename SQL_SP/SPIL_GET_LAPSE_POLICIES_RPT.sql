USE [ABS_LIFE]
GO
/****** Object:  StoredProcedure [dbo].[SPIL_GET_LAPSE_POLICIES_RPT]    Script Date: 08/05/2015 18:34:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SPIL_GET_LAPSE_POLICIES_RPT]
(@pStart_Date	DATETIME
	,@pEnd_Date DATETIME
)
AS
BEGIN
	
SELECT WK.TBIL_POLY_POLICY_NO AS [POLICY NO], WK.[TBIL_POLY_ASSRD_CD] AS [ASSURED CODE],
		   WV.TBIL_INSRD_SURNAME AS [ASSURED NAME],
		   WS.TBIL_POL_PRM_PRDCT_CD AS [PRODUCT CODE], 
		   CONVERT(VARCHAR(10),WS.TBIL_POL_PRM_FROM, 103) AS [START DATE],
		   CONVERT(VARCHAR(10),WS.TBIL_POL_PRM_TO, 103) AS [END DATE], 
		   WU.TBIL_PRDCT_DTL_DESC AS [PRODUCT NAME],
		   (SELECT 
			 CONVERT(VARCHAR(10),(DATEADD(month, (SUM(TBFN_TRANS_TOT_AMT)/WS.TBIL_POL_PRM_MTH_CONTRIB_FC), WS.TBIL_POL_PRM_FROM)), 103) AS [DD/MM/YYYY]
			FROM TBFN_ALLOC_DETAIL WHERE TBFN_TRANS_POLY_NO=WK.TBIL_POLY_POLICY_NO AND TBFN_TRANS_TYPE='N')  AS [LAST PREMIUM PAID DATE],
			CONVERT(VARCHAR(10),WK.TBIL_POLY_LAPSE_DT, 103)  AS [POLICY LAPSE DATE]
	FROM TBIL_POLICY_DET AS WK 
	INNER JOIN tbil_ins_detail AS WV ON WK.TBIL_POLY_ASSRD_CD=WV.TBIL_INSRD_CODE
    INNER JOIN TBIL_POLICY_PREM_INFO AS WS ON WK.TBIL_POLY_POLICY_NO=WS.TBIL_POL_PRM_POLY_NO
	INNER JOIN TBIL_PRODUCT_DETL AS WU ON WS.TBIL_POL_PRM_PRDCT_CD=WU.TBIL_PRDCT_DTL_CODE
	WHERE TBIL_POLY_STATUS='L' AND TBIL_POLY_LAPSE_DT >=@pStart_Date AND TBIL_POLY_LAPSE_DT <=@pEnd_Date
END

