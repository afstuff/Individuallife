
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Class I_LIFE_PRG_LI_REQ_ENTRY
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


    Shared _rtnMessage As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'load loss type into combobox
        LoadLossTypeCmb()
        'LoadProductsDescCmb()

        strTableName = "TBIL_INS_CLASS"

        Try
            strP_TYPE = CType(Request.QueryString("optid"), String)
            strP_DESC = CType(Request.QueryString("optd"), String)
        Catch ex As Exception
            strP_TYPE = "ERR_TYPE"
            strP_DESC = "ERR_DESC"
        End Try

        STRPAGE_TITLE = "Master Codes Setup - " & strP_DESC

        If Trim(strP_TYPE) = "ERR_TYPE" Or Trim(strP_TYPE) = "" Then
            strP_TYPE = ""
        End If

        strP_ID = "L01"

        If Me.txtAction.Text = "Save" Then
            'Call DoSave()
            'Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete" Then
            'Call DoDelete()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            'Call DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub chkClaimNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkClaimNum.CheckedChanged

        If Me.chkClaimNum.Checked Then
            txtClaimsNo.Enabled = True
            cmdClaimNoGet.Enabled = True
        Else
            txtClaimsNo.Enabled = False
            cmdClaimNoGet.Enabled = False
        End If

    End Sub

    Protected Sub chkPolyNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPolyNum.CheckedChanged

        If chkPolyNum.Checked Then
            txtPolicyNumber.Enabled = True
            cmdPolyNoGet.Enabled = True
        Else
            txtPolicyNumber.Enabled = False
            cmdPolyNoGet.Enabled = False
        End If

    End Sub


    Public Function GetAllLossTypeCode() As DataSet

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GetAllLossTypeCode"
        cmd.CommandType = CommandType.StoredProcedure

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()
            Return ds
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try
        Return Nothing

    End Function

    Public Function GetAllProductCodeList() As DataSet

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GetAllProductList"
        cmd.CommandType = CommandType.StoredProcedure

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()
            Return ds
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try
        Return Nothing

    End Function


    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If

    End Sub

    Sub LoadLossTypeCmb()
        Dim ds As DataSet = GetAllLossTypeCode()
        'Dim dt As DataTable = ds.Tables(0)

        DdnLossType.DataSource = ds.Tables(0)
        DdnLossType.DataTextField = "TBIL_COD_SHORT_DESC"
        DdnLossType.DataValueField = "TBIL_COD_ITEM"
        DdnLossType.DataBind()

    End Sub

    Sub LoadProductsDescCmb()
        Dim dsProd As DataSet = GetAllProductCodeList()
        'Dim dt As DataTable = ds.Tables(0)

        ddnProductDesc.DataSource = dsProd.Tables(0)
        ddnProductDesc.DataTextField = "TBIL_PRDCT_DTL_DESC"
        ddnProductDesc.DataValueField = "TBIL_PRDCT_DTL_PLAN_CD"
        ddnProductDesc.DataBind()

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then

            Else
                Dim cboValue As String = Me.cboSearch.SelectedItem.Value
                strStatus = GetPolicyDetailsByNumber(cboValue.Trim())
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Private Function GetPolicyDetailsByNumber(ByVal policyNumber As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GETPOLICYDET_BY_POLNUM"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POLY_POLICY_NO", policyNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                strErrMsg = "true"

                txtPolicyNumber.Text = RTrim(CType(objOledr("TBIL_POLY_POLICY_NO") & vbNullString, String))
                txtUWY.Text = CType(objOledr("TBIL_POLY_UNDW_YR") & vbNullString, String)
                txtProductCode.Text = CType(objOledr("TBIL_POLY_PRDCT_CD") & vbNullString, String)
                If IsDate(objOledr("TBIL_POL_PRM_FROM")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_POL_PRM_TO")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
                End If

                txtBasicSumClaimsLC.Text = Convert.ToString(objOledr("TBIL_POL_PRM_ANN_CONTRIB_LC"))
                txtBasicSumClaimsFC.Text = Convert.ToString(objOledr("TBIL_POL_PRM_ANN_CONTRIB_FC"))
                txtAdditionalSumClaimsLC.Text = Convert.ToString(objOledr("TBIL_POL_PRM_MTH_CONTRIB_LC"))
                txtAdditionalSumClaimsFC.Text = Convert.ToString(objOledr("TBIL_POL_PRM_MTH_CONTRIB_FC"))
                txtAssuredAge.Text = CType(Convert.ToInt16(objOledr("TBIL_POLY_ASSRD_AGE").ToString), String)


                If IsDate(objOledr("TBIL_POLICY_EFF_DT")) Then
                    txtClaimsEffectiveDate.Text = Format(CType(objOledr("TBIL_POLICY_EFF_DT"), DateTime), "dd/MM/yyyy")
                End If

            End If
            conn.Close()
        Catch ex As Exception

        End Try

        Return Nothing
    End Function

    Private Function GetClaimsDetailsByNumber(ByVal claimNumber As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_CLAIMSNUM_SEARCH_FRM_TABLE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@tbil_clm_rptd_clm_no", claimNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                strErrMsg = "true"

                txtPolicyNumber.Text = RTrim(CType(objOledr("TBIL_CLM_RPTD_POLY_NO") & vbNullString, String))
                txtUWY.Text = CType(objOledr("TBIL_CLM_RPTD_UNDW_YR") & vbNullString, String)
                txtProductCode.Text = CType(objOledr("TBIL_CLM_RPTD_PRDCT_CD") & vbNullString, String)

                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_TO_DT")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_TO_DT"), DateTime), "dd/MM/yyyy")
                End If

                If IsDate(objOledr("TBIL_CLM_RPTD_NOTIF_DT")) Then
                    txtNotificationDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_NOTIF_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_CLM_RPTD_LOSS_DT")) Then
                    txtClaimsEffectiveDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_LOSS_DT"), DateTime), "dd/MM/yyyy")
                End If

                txtBasicSumClaimsLC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC"), Decimal), "N2")
                txtBasicSumClaimsFC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC"), Decimal), "N2")
                txtAdditionalSumClaimsLC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC"), Decimal), "N2")
                txtAdditionalSumClaimsFC.Text = Format(CType(objOledr("TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC"), Decimal), "N2")

                txtAssuredAge.Text = CType(Convert.ToInt16(objOledr("TBIL_CLM_RPTD_ASSRD_AGE").ToString), String)
                DdnClaimType.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE"), String)
                DdnSysModule.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String)
                DdnLossType.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_LOSS_TYPE") & vbNullString, String)
                txtProductDec.Text = CType(objOledr("TBIL_CLM_RPTD_DESC") & vbNullString, String)

            End If
            conn.Close()
        Catch ex As Exception

        End Try

        Return Nothing
    End Function
    Protected Sub cmdPolyNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPolyNoGet.Click
        If txtPolicyNumber.Text <> "" Then
            strStatus = GetPolicyDetailsByNumber(txtPolicyNumber.Text.Trim())
        End If
    End Sub

    Protected Sub cmdClaimNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClaimNoGet.Click
        If txtClaimsNo.Text <> "" Then
            strStatus = GetClaimsDetailsByNumber(txtClaimsNo.Text.Trim())
        End If
    End Sub


    Private Function AddNewClaimsRequest(ByVal systemModule As String, ByVal polNumber As String, ByVal claimNo As String, ByVal uwy As String, _
                       ByVal productCode As String, ByVal lossType As String, ByVal polStartDate As DateTime, _
                       ByVal polEndDate As DateTime, ByVal notificationDate As DateTime, ByVal claimEffectiveDate As DateTime, ByVal basicSumClc As Decimal, _
                       ByVal basicSumCfc As Decimal, ByVal addSumClc As Decimal, ByVal addSumCfc As Decimal, _
                       ByVal claimDescription As String, ByVal assuredAge As Int16, ByVal lossType2 As String, ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_INS_CLAIMSREQUEST_"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_MDLE", systemModule)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_NO", polNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_NO", claimNo)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_UNDW_YR", uwy)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PRDCT_CD", productCode)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_TYPE", lossType)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_FROM_DT", polStartDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_TO_DT", polEndDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_DT", claimEffectiveDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_NOTIF_DT", notificationDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BASIC_LOSS_AMT_LC", basicSumClc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BASIC_LOSS_AMT_FC", basicSumCfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_LC", addSumClc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ADDCOV_LOSS_AMT_FC", addSumCfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_DESC", claimDescription)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_ASSRD_AGE", assuredAge)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_TYPE", lossType2)

        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FLAG", flag)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_KEYDTE", dDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_OPERID", userId)

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd

            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()
            Return ds.GetXml()


        Catch ex As Exception
            _rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try


        Return _rtnMessage

    End Function

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click


        If txtPolicyNumber.Text = "" Then
            lblMsg.Text += ""
        End If
        Dim effdate = DateValidator.ValidateDate(txtNotificationDate.Text)
        _rtnMessage = effdate

    End Sub
End Class
