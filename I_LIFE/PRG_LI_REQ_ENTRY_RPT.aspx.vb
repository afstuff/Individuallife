﻿
Partial Class I_LIFE_PRG_LI_REQ_ENTRY_RPT
    Inherits System.Web.UI.Page

    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new", "new"}
    Protected FirstMsg As String

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click

        If pFilterOption.SelectedIndex <> 0 Then

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
            Else
                Dim errMsg = "Javascript:alert('Start date is required!')"
                Label1.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                sStartDate.Focus()
                Exit Sub
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
            Else
                Dim errMsg = "Javascript:alert('End date is required!')"
                Label1.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                sEndDate.Focus()
                Exit Sub
            End If

            Dim startDate = MOD_GEN.DoConvertToDbDateFormat(sStartDate.Text)
            Dim endDate = MOD_GEN.DoConvertToDbDateFormat(sEndDate.Text)
            Dim newStartDate As Date = Convert.ToDateTime(startDate)
            Dim newEndDate As Date = Convert.ToDateTime(endDate)

            If newEndDate < newStartDate Then
                Dim errMsg = "Javascript:alert('End date can not be less than start date!');"
                Label1.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                sEndDate.Focus()
                Exit Sub
            End If

            rParams(0) = rblTransType.SelectedValue.Trim
            rParams(1) = "startDate="
            rParams(2) = startDate.Trim + "&"
            rParams(3) = "endDate="
            rParams(4) = endDate.Trim + "&"

            rParams(5) = "pFilterOption="
            rParams(6) = pFilterOption.SelectedValue + "&"

            Session("ReportParams") = rParams
            Response.Redirect("~/PrintView.aspx")

        Else
            Dim errMsg = "Javascript:alert('Select a filter option!');"
            Label1.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            FirstMsg = errMsg
            pFilterOption.Focus()
            Exit Sub
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class