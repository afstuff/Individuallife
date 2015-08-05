USE [ABS_LIFE]
GO
/****** Object:  StoredProcedure [dbo].[SPIL_GET_LAST_PREMIUM_PAID_DATE]    Script Date: 08/05/2015 18:51:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SPIL_GET_LAST_PREMIUM_PAID_DATE]
@pPolicyNo nvarchar(50)
AS
BEGIN
declare @Division_Result numeric(18,2),
@StartDate as datetime,
@Monthly_Premium numeric(18,2),
@Last_Premium_Paid_Date as datetime,
@Date_diff as int

SELECT @StartDate=(SELECT TBIL_POL_PRM_FROM FROM TBIL_POLICY_PREM_INFO 
WHERE TBIL_POL_PRM_POLY_NO=@pPolicyNo)

SELECT @Monthly_Premium=(SELECT TBIL_POL_PRM_MTH_CONTRIB_FC FROM TBIL_POLICY_PREM_INFO 
 WHERE TBIL_POL_PRM_POLY_NO=@pPolicyNo)
	    
	     IF @Monthly_Premium > 0.00
	    BEGIN
			SELECT @Last_Premium_Paid_Date=(SELECT
			(DATEADD(month, (SUM(TBFN_TRANS_TOT_AMT)/@Monthly_Premium), @StartDate)) AS LAST_PREMIUM_PAID_DATE
            FROM TBFN_ALLOC_DETAIL WHERE TBFN_TRANS_POLY_NO=@pPolicyNo AND TBFN_TRANS_TYPE='N')

          SELECT @Date_diff=DATEDIFF(YEAR,@Last_Premium_Paid_Date,GETDATE())
          IF @Date_diff >=1
		   BEGIN
	       SELECT @Date_diff AS YEARDIFF, SUM(TBFN_TRANS_TOT_AMT) AS ALLOCATION_AMOUNT, TBFN_TRANS_POLY_NO, @StartDate AS STARTDATE,
			  @Monthly_Premium AS MONTHLY_PREMIUM, (SUM(TBFN_TRANS_TOT_AMT)/@Monthly_Premium) AS No_Months_Paid,
			  @Last_Premium_Paid_Date AS LAST_PREMIUM_PAID_DATE
		   FROM TBFN_ALLOC_DETAIL WHERE TBFN_TRANS_POLY_NO=@pPolicyNo AND TBFN_TRANS_TYPE='N' GROUP BY TBFN_TRANS_POLY_NO
		   END
	   END
	    
END