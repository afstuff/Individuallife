USE [ABS_LIFE]
GO

/****** Object:  StoredProcedure [dbo].[SPIL_GET_ADDITIONAL_COVER]    Script Date: 07/28/2015 10:13:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPIL_GET_ADDITIONAL_COVER]
	(
 @PARAM_COVER_CODE nvarchar(50),
 @PARAM_POL_ADD_POLY_NO nvarchar(50)
)
AS
BEGIN
	IF EXISTS (SELECT WK.TBIL_POL_ADD_COVER_CD
               FROM TBIL_POLICY_ADD_PREM AS WK 
               WHERE TBIL_POL_ADD_COVER_CD = @PARAM_COVER_CODE 
               AND TBIL_POL_ADD_POLY_NO= @PARAM_POL_ADD_POLY_NO)
    BEGIN
     SELECT 'MSG' =1
    END
   ELSE
    BEGIN
     SELECT 'MSG' =0
   END
END

GO


