
Imports System.Data.OleDb
Imports System.Data

Partial Class I_LIFE_PRG_LI_CLM_MATURE
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String

    Protected STRPAGE_TITLE As String
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

    Dim basicLc As Decimal
    Dim basicFc As Decimal
    Dim addLc As Decimal
    Dim addFc As Decimal
    Dim newDateToDb As Date

    Shared _rtnMessage As String


    Protected Sub chkClaimNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkClaimNum.CheckedChanged
        If chkClaimNum.Checked Then
            txtClaimsNo.Enabled = True
            cmdClaimNoGet.Enabled = True
        Else
            txtClaimsNo.Enabled = False
            cmdClaimNoGet.Enabled = True

        End If
    End Sub

    Protected Sub cmdClaimNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClaimNoGet.Click
        'ClearAllFields()
        'by ClaimNo(CLAIMNUM)
        If txtClaimsNo.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("CLAIMNUM", txtClaimsNo.Text)
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
        Else
            lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            Exit Sub
        End If

    End Sub

    Public Function GET_CLMS_RPTD_MATURITY(ByVal pQueryType As String, ByVal pQueryValue As String) As String
        'Dim pQUERY_TYPE As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_CLMS_RPTD_MATURITY"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pQUERY_TYPE", pQueryType)
        cmd.Parameters.AddWithValue("@pQUERY_VALUE", pQueryValue)

        'clear all fields first
        ClearAllFields()

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                strErrMsg = "true"

                txtClaimsNo.Text = RTrim(CType(objOledr("TBIL_CLM_RPTD_CLM_NO") & vbNullString, String))
                txtPolicyNumber.Text = RTrim(CType(objOledr("TBIL_CLM_RPTD_POLY_NO") & vbNullString, String))
                txtUWY.Text = CType(objOledr("TBIL_CLM_RPTD_UNDW_YR") & vbNullString, String)
                txtProductCode.Text = CType(objOledr("TBIL_CLM_RPTD_PRDCT_CD") & vbNullString, String)
                txtProductName.Text = CType(objOledr("TBIL_PRDCT_DTL_DESC") & vbNullString, String)


                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_TO_DT")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_TO_DT"), DateTime), "dd/MM/yyyy")
                End If

                DdnClaimType.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String)
                DdnSysModule.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String)

                Dim sDate As String = CType(CType(Now.ToShortDateString(), Date), String)
                Dim sDate1 = sDate.Split(CType("/", Char))
                txtClaimsCalculatedDate.Text = sDate1(1) + "/" + sDate1(0) + "/" + sDate1(2)



                '_rtnMessage = "Record Display!"
                'method to CHECK_IF_CLAIM_EXIST
                lblMsg.Text = CHECK_IF_CLAIM_EXIST(txtClaimsNo.Text.Trim())
                FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            Else
                _rtnMessage = "Claims record does not exist!"
            End If
            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try

        Return _rtnMessage
    End Function


    Public Function CHECK_IF_CLAIM_EXIST(ByVal claimNumber As String) As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_CLAIM_PAID"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pCLAIM_NUMBER", claimNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then

                _rtnMessage = "Yes data!"

            Else
                _rtnMessage = "No data!"

            End If
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try

        Return _rtnMessage

    End Function

    Protected Sub txtClaimsNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClaimsNo.TextChanged
        'ClearAllFields()
        'by ClaimNo(CLAIMNUM)
        If txtClaimsNo.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("CLAIMNUM", txtClaimsNo.Text)
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
        Else
            'lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            'FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            'Exit Sub
        End If
    End Sub

    Protected Sub txtPolicyNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNumber.TextChanged
        'ClearAllFields()
        'by PolyNo(POLUNUM)
        If txtPolicyNumber.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("POLYNUM", txtPolicyNumber.Text)
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
        Else
            'lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            'FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            'Exit Sub
        End If
    End Sub

    Protected Sub chkPolyNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPolyNum.CheckedChanged
        If chkPolyNum.Checked Then
            txtPolicyNumber.Enabled = True
            cmdPolyNoGet.Enabled = True
        Else
            txtPolicyNumber.Enabled = False
            cmdPolyNoGet.Enabled = True
        End If
    End Sub

    Protected Sub cmdPolyNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPolyNoGet.Click
        'ClearAllFields()

        'by PolyNo(POLUNUM)
        If txtPolicyNumber.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("POLYNUM", txtPolicyNumber.Text)
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
        Else
            'lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            'FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            'Exit Sub
        End If
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        'ClearAllFields()

        'by PolyNo(POLUNUM)
        lblMsg.Text = GET_CLMS_RPTD_MATURITY("POLYNUM", cboSearch.SelectedValue)
        FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(txtSearch.Value)) <> "" Then

            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()

        End If
    End Sub

    Sub ClearAllFields()
        txtClaimsNo.Text = ""
        txtPolicyNumber.Text = ""
        txtPolicyStartDate.Text = ""
        txtPolicyEndDate.Text = ""
        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtUWY.Text = ""
        DdnSysModule.SelectedIndex = 0
        DdnClaimType.SelectedIndex = 0

    End Sub

End Class
