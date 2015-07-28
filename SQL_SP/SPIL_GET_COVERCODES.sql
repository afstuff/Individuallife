USE [ABS_LIFE]
GO

/****** Object:  StoredProcedure [dbo].[SPIL_GET_COVERCODES]    Script Date: 07/28/2015 10:12:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPIL_GET_COVERCODES]
AS
BEGIN
	SELECT WK.TBIL_COV_CD, WK.TBIL_COV_DESC
     FROM TBIL_COVER_DET AS WK ORDER BY TBIL_COV_DESC ASC
 END

GO


