USE [ABS_LIFE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[TBIL_SELECT_CLAIMREQSEARCH]
	@qryOption int,
	@qryValue varchar(50)
AS
BEGIN

	SET NOCOUNT ON;
if @qryOption = 1 
   select distinct * from View_TBIL_SELECT_CLAIMREQSEARCH where TBIL_POLY_POLICY_NO like ''+@qryValue+'%'
else
   select distinct * from View_TBIL_SELECT_CLAIMREQSEARCH where TBIL_INSRD_SURNAME like '%'+@qryValue+'%'
   
END
