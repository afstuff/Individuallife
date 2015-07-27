USE [ABS_LIFE]
GO

/****** Object:  StoredProcedure [dbo].[SPIL_INS_CLAIMSREQUEST_]    Script Date: 07/27/2015 11:33:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[SPIL_INS_CLAIMSREQUEST_]
--@TBIL_CLAIM_REPTED_REC_ID int,
@TBIL_CLM_RPTD_MDLE nvarchar(3),
      @TBIL_CLM_RPTD_POLY_NO nvarchar(35),
      @TBIL_CLM_RPTD_CLM_NO nvarchar(35),
      @TBIL_CLM_RPTD_UNDW_YR int,
      @TBIL_CLM_RPTD_PRDCT_CD nvarchar(3),
      @TBIL_CLM_RPTD_CLM_TYPE nvarchar(1),
      @TBIL_CLM_RPTD_POLY_FROM_DT datetime,
      @TBIL_CLM_RPTD_POLY_TO_DT datetime, 
      @TBIL_CLM_RPTD_NOTIF_DT datetime,
      @TBIL_CLM_RPTD_LOSS_DT datetime,
      @TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC decimal(19, 5),
      @TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC decimal(19, 5),
      @TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC decimal(19, 5),
      @TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC decimal(19, 5),
      @TBIL_CLM_RPTD_DESC nvarchar(350),
      @TBIL_CLM_RPTD_ASSRD_AGE int,
      @TBIL_CLM_RPTD_LOSS_TYPE nvarchar(1),
 
      @TBIL_CLM_RPTD_FLAG varchar(1),
      @TBIL_CLM_RPTD_KEYDTE datetime,
      @TBIL_CLM_RPTD_OPERID varchar(1)
      
      AS
BEGIN
SET IDENTITY_INSERT [dbo].[TBIL_CLAIM_REPTED] OFF

IF EXISTS (select TBIL_CLM_RPTD_POLY_NO from TBIL_CLAIM_REPTED where TBIL_CLM_RPTD_POLY_NO=@TBIL_CLM_RPTD_POLY_NO)
begin
select 'Msg'=0
end
else
begin
INSERT INTO TBIL_CLAIM_REPTED
                      (TBIL_CLM_RPTD_MDLE, TBIL_CLM_RPTD_POLY_NO, TBIL_CLM_RPTD_CLM_NO, TBIL_CLM_RPTD_UNDW_YR, TBIL_CLM_RPTD_PRDCT_CD, 
                      TBIL_CLM_RPTD_CLM_TYPE, TBIL_CLM_RPTD_POLY_FROM_DT, TBIL_CLM_RPTD_POLY_TO_DT, TBIL_CLM_RPTD_NOTIF_DT, TBIL_CLM_RPTD_LOSS_DT, 
                      TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC, TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC, TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC, TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC, 
                      TBIL_CLM_RPTD_DESC, TBIL_CLM_RPTD_ASSRD_AGE, TBIL_CLM_RPTD_LOSS_TYPE, TBIL_CLM_RPTD_FLAG, TBIL_CLM_RPTD_KEYDTE, TBIL_CLM_RPTD_OPERID)
VALUES     (@TBIL_CLM_RPTD_MDLE,@TBIL_CLM_RPTD_POLY_NO,@TBIL_CLM_RPTD_CLM_NO,@TBIL_CLM_RPTD_UNDW_YR,@TBIL_CLM_RPTD_PRDCT_CD,@TBIL_CLM_RPTD_CLM_TYPE,@TBIL_CLM_RPTD_POLY_FROM_DT,@TBIL_CLM_RPTD_POLY_TO_DT,@TBIL_CLM_RPTD_NOTIF_DT,@TBIL_CLM_RPTD_LOSS_DT,@TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC,@TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC,@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC,@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC,@TBIL_CLM_RPTD_DESC,@TBIL_CLM_RPTD_ASSRD_AGE,@TBIL_CLM_RPTD_LOSS_TYPE, @TBIL_CLM_RPTD_FLAG, @TBIL_CLM_RPTD_KEYDTE, @TBIL_CLM_RPTD_OPERID)

select 'Msg'=1

--select * from [dbo].[TBIL_CLAIM_REPTED]  where TBIL_CLM_RPTD_POLY_NO=@TBIL_CLM_RPTD_POLY_NO
End

End
GO

