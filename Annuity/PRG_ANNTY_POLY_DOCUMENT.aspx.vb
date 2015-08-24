Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Annuity_PRG_ANNTY_POLY_DOCUMENT
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String

    Protected STRMENU_TITLE As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""

    Dim myarrData() As String

    Dim strErrMsg As String
    Protected strUpdate_Sw As String
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new", "new", "new", "new"}
    Dim ErrorInd, PolicyNo_Retrieved As String

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("AN_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If
    End Sub

    Protected Sub cmdFileNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileNum.Click
        Dim xc As Integer = 0

        If Trim(Me.txtPol_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPol_Num.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtPol_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtPol_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtPol_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblPol_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
                Exit Sub
            End If
        Next
        blnStatus = Proc_DoGet_Record(Trim(Me.txtPol_Num.Text))
        'If blnStatus = True Then
        '    Me.chkAccept.Enabled = True
        '    'Me.BUT_OK.Enabled = True
        '    Exit Sub
        'Else

        '    Exit Sub
        'End If
    End Sub
    Private Function Proc_DoGet_Record(ByVal pvPoloNo As String) As Boolean
        Proc_DoNew()
        blnStatusX = False

        Dim mystrCONN_Chk As String = ""

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            blnStatusX = False
            Return blnStatusX
            Exit Function
        End Try

        Try

            strTable = strTableName

            strSQL = ""
            strSQL = "SPAN_GET_ANNUITY_SCHEDULE_INFO"

            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            ''objOLECmd_Chk.CommandTimeout = 180
            objOLECmd_Chk.CommandType = CommandType.StoredProcedure

            objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 50).Value = LTrim(RTrim(pvPoloNo))
            objOLECmd_Chk.Parameters.Add("p02", OleDbType.VarChar, 100).Value = ""
            objOLECmd_Chk.Parameters.Add("p03", OleDbType.VarChar, 100).Value = ""
            objOLECmd_Chk.Parameters.Add("p04", OleDbType.VarChar, 100).Value = ""

            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then

                Me.txtFileNum.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_FILE_NO") & vbNullString, String))
                Me.txtPro_Pol_Num.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_PROPSAL_NO") & vbNullString, String))
                Me.txtAssured_Name.Text = RTrim(CType(objOLEDR_Chk("Assured_Name") & vbNullString, String))
                Me.txtProduct_Num.Text = RTrim(CType(objOLEDR_Chk("TBIL_ANN_POLY_PRDCT_CD") & vbNullString, String))
                Me.txtProduct_Name.Text = Trim(CType(objOLEDR_Chk("TBIL_PRDCT_DTL_DESC") & vbNullString, String))
                Session("PolicyNo_Retrieved") = Trim(CType(objOLEDR_Chk("TBIL_ANN_POLY_POLICY_NO") & vbNullString, String))
            Else
                blnStatusX = False
                Me.lblMsg.Text = "Record not found for Policy No: " & Me.txtPol_Num.Text
                Exit Function
            End If

        Catch ex As Exception
            blnStatusX = False
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message.ToString()
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
        End Try
        objOLEDR_Chk = Nothing

        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
        Return blnStatusX

    End Function
    Private Sub Proc_DoNew()

        Me.cmdNew_ASP.Enabled = True
        Me.cmdFileNum.Enabled = True

        Me.txtPro_Pol_Num.Text = ""
        Me.txtPro_Pol_Num.Enabled = False
        Me.txtFileNum.Text = ""
        Me.txtFileNum.Enabled = False
        Me.txtAssured_Name.Text = ""
        Me.txtProduct_Num.Text = ""
        Me.txtProduct_Name.Text = ""
        rblTransType.SelectedValue = ""
        rblTransType.SelectedIndex = -1
        'rblTransType.SelectedIndex = Nothing
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        'ErrorInd = ""
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPol_Num.Text = ""
            Else
                Dim selectedText = Me.cboSearch.SelectedItem.Text
                Dim ReturnText = Split(selectedText, "-")
                txtPol_Num.Text = Trim(ReturnText(3))
                If txtPol_Num.Text = "" Then
                    Me.lblMsg.Text = "Policy no has not been generated, Proposal has not been converted to policy"
                    lblMsg.Visible = True
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                Else
                    Proc_DoGet_Record(txtPol_Num.Text)
                End If
                'If ErrorInd = "Y" Then
                '    Exit Sub
                'End If
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub

    Protected Sub CmdPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdPrint.Click
        ErrorInd = ""
        ValidateControls(ErrorInd)
        If ErrorInd = "Y" Then
            Exit Sub
        End If
        ' To avoid document with no policy detail
        If Session("PolicyNo_Retrieved") <> txtPol_Num.Text Then
            Me.lblMsg.Text = "Record not found for Policy No: " & Me.txtPol_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        rParams(0) = rblTransType.SelectedValue.Trim
        rParams(1) = "pPolicyNo="
        rParams(2) = txtPol_Num.Text + "&"
        rParams(3) = "pParam1="
        rParams(4) = "null&"
        rParams(5) = "pParam2="
        rParams(6) = "null&"
        rParams(7) = "pParam3="
        rParams(8) = "null&"

        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
        ' Response.Redirect("PrintView.aspx")
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        Proc_DoNew()
        Me.txtPol_Num.Text = ""
    End Sub
    Private Sub ValidateControls(ByRef ErrorInd As String)
        If (txtPol_Num.Text = String.Empty) Then
            lblMsg.Text = "Please enter a policy number"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPro_Pol_Num.Text = String.Empty) Then
            lblMsg.Text = "Proposal number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtFileNum.Text = String.Empty) Then
            lblMsg.Text = "File number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtAssured_Name.Text = String.Empty) Then
            lblMsg.Text = "Assured name must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (txtProduct_Num.Text = "") Then
            lblMsg.Text = "Product number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (txtProduct_Name.Text = "") Then
            lblMsg.Text = "Product name must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If


        If (rblTransType.SelectedValue = "" Or rblTransType.SelectedIndex = -1) Then
            lblMsg.Text = "Please select the document to print"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
    End Sub
End Class
