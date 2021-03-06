﻿Imports System.Data.OleDb
Imports System.Data

Partial Class I_LIFE_PRG_LI_POLICY_RENEW
    Inherits System.Web.UI.Page



    Protected FirstMsg As String
    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String

    Protected strP_TYPE As String
    Protected strP_DESC As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""
    Dim myarrData() As String

    Dim strErrMsg As String

    ' additional premium variable
    Dim dblAdd_Prem_Amt As Double = 0
    Dim dblAdd_Prem_Rate As Double = 0
    Dim dblAdd_Rate_Per As Integer = 0
    Dim dblAdd_Prem_SA_LC As Double
    Dim dblAdd_Prem_SA_FC As Double

    Dim dblTotal_Add_Prem_LC As Double = 0
    Dim dblTotal_Add_Prem_FC As Double = 0

    Dim dblTotal_Prem_LC As Double = 0
    Dim dblTotal_Prem_FC As Double = 0

    Dim dblAnnual_Basic_Prem_LC As Double = 0
    Dim dblAnnual_Basic_Prem_FC As Double = 0

    Dim dblTmp_Amt As Double = 0

    Public Sub DoClaimRenewal(ByVal polyNumber As String, ByVal fileNumber As String, ByVal propNumber As String, ByVal startDate As Date, ByVal endDate As Date)
        ', ByVal batchNumber As String       
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_IND_CLAIMRENEWALBY_POLY_NO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_POLY_NO", polyNumber)
        'cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_FILE_NO", fileNumber)
        'cmd.Parameters.AddWithValue("@TBIL_POL_MEMB_PROP_NO", propNumber)
        cmd.Parameters.AddWithValue("@TBIL_POL_PREM_INFO_STARTDATE", startDate)
        cmd.Parameters.AddWithValue("@TBIL_POL_PREM_INFO_ENDDATE", endDate)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()

            Dim dt As DataTable = New DataTable
            dt.Load(objOledr)
            Dim numRows As Integer = dt.Rows.Count
            lblMsg.Text = "Result: " + numRows.ToString + " members listed."


            'Else
            '    _rtnMessage = "Sorry. The system cannot find record with IDs: " + txtPolNum.Text
            'End If
            lblMsg.Text = "The policy " + polyNumber.ToString + " hes been renewed successfully!"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            conn.Close()
        Catch ex As Exception
            strErrMsg = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtFileNum.Text = ""
                Me.txtQuote_Num.Text = ""
                Me.txtPolNum.Text = ""
                'Me.txtSearch.Value = ""
            Else
                Me.txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
                GET_POLICYDATE_BY_FILENO(txtPolNum.Text)
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try

    End Sub

    Public Sub GET_POLICYDATE_BY_FILENO(ByVal fileNumber As String)
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_IND_POLICYDATE_BY_FILENO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_PRM_POLY_NO", fileNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                Dim newStartDate As Date = Convert.ToDateTime(objOledr("TBIL_POL_PRM_FROM"))
                Dim newEndDate As Date = Convert.ToDateTime(objOledr("TBIL_POL_PRM_TO"))

                newStartDate = newStartDate.AddYears(1)
                newEndDate = newEndDate.AddYears(1)
                txtPrem_Start_Date.Text = newStartDate.ToString("dd/MM/yyyy")
                txtPrem_End_Date.Text = newEndDate.ToString("dd/MM/yyyy")

            End If

            conn.Close()
        Catch ex As Exception
            strErrMsg = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    Private Function Proc_DoOpenRecord(ByVal FVstrGetType As String, ByVal FVstrRefNum As String, Optional ByVal FVstrRecNo As String = "", Optional ByVal strSearchByWhat As String = "FILE_NUM") As String

        strErrMsg = "false"

        lblMsg.Text = ""
        If Trim(FVstrRefNum) = "" Then
            Return strErrMsg
            Exit Function
        End If

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Return strErrMsg
            Exit Function
        End Try


        strREC_ID = Trim(FVstrRefNum)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 PT.*"
        strSQL = strSQL & " FROM " & strTable & " AS PT"
        strSQL = strSQL & " WHERE PT.TBIL_POLY_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND PT.TBIL_POLY_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPIL_GET_POLICY_DET"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_FILE_NO") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))




        Else

            strOPT = "1"
            Me.lblMsg.Text = "Status: New Entry..."

        End If


        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return strErrMsg

    End Function


    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If
    End Sub

    Protected Sub btnRenewPolicy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenewPolicy.Click

        If txtPolNum.Text <> "" Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType(), "Yes", "Check();", True)
            'FirstMsg = "javascript:confirm('Field policy number cannot be empty!')"
            DoClaimRenewal(txtPolNum.Text, txtFileNum.Text, txtQuote_Num.Text, MOD_GEN.DoConvertToDbDateFormat(txtPrem_Start_Date.Text), MOD_GEN.DoConvertToDbDateFormat(txtPrem_End_Date.Text))
        Else
            FirstMsg = "javascript:alert('Field policy number cannot be empty!')"
            Exit Sub
        End If
    End Sub

    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click

        If Trim(Me.txtPolNum.Text) <> "" Then
            strStatus = Proc_DoOpenRecord(RTrim("POL"), Me.txtPolNum.Text, RTrim("0"))
            GET_POLICYDATE_BY_FILENO(txtPolNum.Text)
        End If
    End Sub



End Class
