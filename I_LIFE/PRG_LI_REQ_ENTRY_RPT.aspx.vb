
Partial Class I_LIFE_PRG_LI_REQ_ENTRY_RPT
    Inherits System.Web.UI.Page

    Dim rParams As String() = {"nw", "nw", "new", "new", "new"}
    Protected FirstMsg As String

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
        Dim Str() As String
        If sStartDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("sStartDate")
            Str = MOD_GEN.DoDate_Process(sStartDate.Text, ctrlId)

            If Str(2) = Nothing Then
                Dim errMsg = Str(0).Insert(18, " Start Date, ")
                Label1.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                sStartDate.Focus()
                Exit Sub

            Else
                sStartDate.Text = Str(2).ToString()
            End If

        End If

        If sEndDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("sEndDate")
            Str = MOD_GEN.DoDate_Process(sEndDate.Text, ctrlId)

            If Str(2) = Nothing Then
                Dim errMsg = Str(0).Insert(18, " End Date, ")
                Label1.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                sEndDate.Focus()
                Exit Sub

            Else
                sEndDate.Text = Str(2).ToString()
            End If

        End If

        Dim startDate = MOD_GEN.DoConvertToDbDateFormat(sStartDate.Text)
        Dim endDate = MOD_GEN.DoConvertToDbDateFormat(sEndDate.Text)

        rParams(0) = rblTransType.SelectedValue.Trim
        rParams(1) = "startDate="
        rParams(2) = startDate.Trim + "&"
        rParams(3) = "endDate="
        rParams(4) = endDate.Trim + "&"

        Session("ReportParams") = rParams
        Response.Redirect("~/PrintView.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
