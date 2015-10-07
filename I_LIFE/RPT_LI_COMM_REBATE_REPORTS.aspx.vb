Imports System.Data
Imports System.Data.OleDb
Partial Class I_LIFE_RPT_LI_COMM_REBATE_REPORTS
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean
    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String
    Dim rParams As String() = {"nw"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ErrorInd = ""
        rParams(0) = "rptCommissionRebate1"
        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub
End Class
