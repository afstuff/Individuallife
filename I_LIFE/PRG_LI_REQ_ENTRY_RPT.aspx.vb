
Partial Class I_LIFE_PRG_LI_REQ_ENTRY_RPT
    Inherits System.Web.UI.Page

    Dim rParams As String() = {"nw", "nw", "new", "new", "new"}


    Protected Sub viewPrintBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles viewPrintBtn.Click

        'rParams(0) = rblTransType.SelectedValue.Trim
        'rParams(1) = "pStartDate="
        'rParams(2) = sStartDate.Trim + "&"
        'rParams(3) = "pEndDate="
        'rParams(4) = sEndDate.Trim + "&"


        Session("ReportParams") = rParams
        Response.Redirect("PrintView.aspx")
    End Sub
End Class
