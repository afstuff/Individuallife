﻿Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class I_LIFE_PRG_LI_LAPSE_PROCESS
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Dim strREC_ID As String
    Dim strTable As String
    Dim strSQL As String
    Protected strTableName As String
    Dim strErrMsg As String
    Protected blnStatusX As Boolean
    Dim _policyNo As String = ""
    Private Sub GetActivePolicies(ByVal _policyNo As String)
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN
        Dim objOLEComm As OleDbCommand = New OleDbCommand()

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Sub
        End Try


        Try
            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = "SPIL_GET_ALL_ACTIVE_POLICY"
            objOLEComm.CommandType = CommandType.StoredProcedure
            objOLEComm.Parameters.AddWithValue("@pPolicy_No", _policyNo)
            Dim obj_DT As DataSet = New DataSet()
            Dim obj_ADAPTER As OleDbDataAdapter = New OleDbDataAdapter()
            obj_ADAPTER.SelectCommand = objOLEComm
            obj_ADAPTER.Fill(obj_DT, "PolicyDet")

            GrdLapsePolicy.DataSource = obj_DT
            GrdLapsePolicy.DataBind()
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Sub
        End Try

        If objOLEComm.Connection.State = ConnectionState.Open Then
            objOLEComm.Connection.Close()
        End If
        '   objOLEComm.Dispose()
        objOLEComm = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetActivePolicies(_policyNo)
        End If
    End Sub
    Protected Sub UpdateLapse(ByVal sender As Object, ByVal e As EventArgs)
        lblMsg.Text = ""
        Dim geTValue As String = Convert.ToString((TryCast(sender, Button).CommandArgument))
        Dim myUserIDX As String = ""

        Dim MyLen = Len(geTValue)
        Dim MyPos = InStr(geTValue, "-")
        Dim LocationLen = MyLen - MyPos
        MyPos = MyPos - 1
        Dim policyNo = Microsoft.VisualBasic.Left(geTValue, MyPos)
        Dim Last_Premium_Paid_Date = Microsoft.VisualBasic.Right(geTValue, LocationLen)

        If Last_Premium_Paid_Date = Nothing Then
            lblMsg.Text = "Last Premium date is blank, Lapse cannot be processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim Last_Prem_Date As Date
        Last_Prem_Date = DoConvertToDbDateFormat(Last_Premium_Paid_Date)
        Dim i = DateDiff("yyyy", Last_Prem_Date, Now)

        If i < 1 Then
            lblMsg.Text = "Lapse last premium paid date is less than a year, Lapse cannot be processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Sub
        End Try



        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM TBIL_POLICY_DET"
        strSQL = strSQL & " WHERE TBIL_POLY_POLICY_NO = '" & policyNo & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0

        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record
                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()
                drNewRow = Nothing
                Me.lblMsg.Text = "New Record Saved to Database Successfully."
            Else
                '   Update existing record
                With obj_DT
                    .Rows(0)("TBIL_POLY_LAPSE_DT") = Now
                    .Rows(0)("TBIL_POLY_STATUS") = "L"
                    .Rows(0)("TBIL_POLY_KEYDTE") = Now
                    .Rows(0)("TBIL_POLY_FLAG") = "A"
                    .Rows(0)("TBIL_POLY_OPERID") = myUserIDX
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."
                GetActivePolicies("")
            End If


        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Sub
        End Try

        obj_DT.Dispose()
        obj_DT = Nothing

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        lblMsg.Visible = True
    End Sub

    Protected Sub GrdLapsePolicy_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdLapsePolicy.PageIndexChanging
        GrdLapsePolicy.PageIndex = e.NewPageIndex
        GetActivePolicies(_policyNo)
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
        End If
    End Sub
    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        lblMsg.Text = ""
        cboSearch.DataSource = Nothing
        cboSearch.DataBind()
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
            Else
                GetActivePolicies(cboSearch.SelectedValue.Trim())
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub
    Protected Sub PrintLapsePolicy(ByVal sender As Object, ByVal e As EventArgs)
        lblMsg.Text = ""
        Dim geTValue As String = Convert.ToString((TryCast(sender, Button).CommandArgument))
        Dim MyLen = Len(geTValue)
        Dim MyPos = InStr(geTValue, "-")
        Dim LocationLen = MyLen - MyPos
        MyPos = MyPos - 1
        Dim policyNo = Microsoft.VisualBasic.Left(geTValue, MyPos)
        Dim Last_Premium_Paid_Date = Microsoft.VisualBasic.Right(geTValue, LocationLen)

        If Last_Premium_Paid_Date = Nothing Then
            lblMsg.Text = "Last Premium date is blank, Lapse cannot be processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim Last_Prem_Date As Date
        Last_Prem_Date = DoConvertToDbDateFormat(Last_Premium_Paid_Date)
        Dim i = DateDiff("yyyy", Last_Prem_Date, Now)

        If i < 1 Then
            lblMsg.Text = "Lapse last premium paid date is less than a year, Lapse cannot be processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        Dim rParams As String() = {"nw", "nw", "new"}

        rParams(0) = "RPT_LI_LAPSE_PROCESS"
        rParams(1) = "pPolicy_No="
        rParams(2) = policyNo + "&"
        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub

    Protected Sub txtPolicyNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNo.TextChanged
        lblMsg.Text = ""
        If txtPolicyNo.Text <> "" Then
            cboSearch.DataSource = Nothing
            cboSearch.DataBind()
            GetActivePolicies(txtPolicyNo.Text.Trim())
        End If
    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        Response.Redirect("PRG_LI_LAPSE_PROCESS_RPT.aspx")
    End Sub
End Class
