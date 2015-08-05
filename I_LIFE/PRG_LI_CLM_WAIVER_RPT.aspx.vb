﻿Partial Class I_LIFE_RPT_LI_CLM_WAIVER
    Inherits System.Web.UI.Page
    Dim rParams As String() = {"nw", "nw", "new", "new", "new"}

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
        Dim str() As String
        '  Dim reportname As String
        If (txtStartDate.Text = "") Then
            Status.Text = "Waiver effective start date must not be empty"
            Exit Sub
        End If
        If (txtEndDate.Text = "") Then
            Status.Text = "Waiver effective end date must not be empty"
            Exit Sub
        End If

        

        str = DoDate_Process(txtStartDate.Text, txtStartDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Start date, ")
            Status.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            txtStartDate.Focus()
            Exit Sub
        Else
            txtStartDate.Text = str(2).ToString()
        End If

        str = DoDate_Process(txtEndDate.Text, txtEndDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " End date ")
            Status.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            txtEndDate.Focus()
            Exit Sub
        Else
            txtEndDate.Text = str(2).ToString()
        End If

        'Dim startDate As DateTime = Convert.ToDateTime(DoConvertToDbDateFormat(txtStartDate.Text))
        'Dim endDate As DateTime = Convert.ToDateTime(DoConvertToDbDateFormat(txtEndDate.Text))
        Dim startDate = Format(Convert.ToDateTime(DoConvertToDbDateFormat(txtStartDate.Text)), "MM/dd/yyyy")
        Dim endDate = Format(Convert.ToDateTime(DoConvertToDbDateFormat(txtEndDate.Text)), "MM/dd/yyyy")

        If startDate > endDate Then
            Status.Text = "Start Date must not be greater than end date"
            Exit Sub
        End If

        rParams(0) = "RPT_LI_CLM_WAIVER"
        rParams(1) = "RPT_ST_DATE="
        rParams(2) = startDate + "&"
        rParams(3) = "RPT_END_DATE="
        rParams(4) = endDate + "&"

        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
        ' Response.Redirect("PrintView.aspx")
    End Sub
End Class
