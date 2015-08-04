
Partial Class I_LIFE_PRG_LI_REVIVE_POLICY
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Dim PolicyEndDate As DateTime
    Dim waiverRep As PRG_LI_CLM_WAIVER_REPOSITORY = New PRG_LI_CLM_WAIVER_REPOSITORY()
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected strStatus As String
    Dim strREC_ID As String
    Dim strTable As String
    Dim strSQL As String
    Protected strTableName As String
    Dim strErrMsg As String
    Protected blnStatusX As Boolean

End Class
