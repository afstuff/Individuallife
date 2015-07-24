
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


    Shared _rtnMessage As DataSet



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'load loss type into combobox
        LoadLossTypeCmb()


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

        'Me.txtCustType.Text = RTrim(strP_TYPE)
        'Me.txtCustType.Text = RTrim("001")

        'If Not (Page.IsPostBack) Then
        '    Call Proc_Populate_Box("IL_INS_CLASS_LIST", Trim(Me.txtCustType.Text), Me.cboTransList)
        '    Call Proc_DataBind()
        '    Call DoNew()
        '    'Me.lblMessage.Text = strSQL
        '    Me.txtCustNum.Enabled = True
        '    Me.txtCustNum.Focus()
        'End If

        'If Me.txtAction.Text = "New" Then
        '    Call DoNew()
        '    'Call Proc_OpenRecord(Me.txtNum.Text)
        '    Me.txtAction.Text = ""
        '    Me.txtCustNum.Enabled = True
        '    Me.txtCustNum.Focus()
        'End If

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

    Public Function GetClaimsNumberSearch(ByVal sValue As String, ByVal sType As Int16) As DataSet
        Dim ds As DataSet
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_CLAIMSNUM_SEARCH"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sValue", sValue)

        Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
        adapter.SelectCommand = cmd

        Try
            conn.Open()
            ds = New DataSet()
            adapter.Fill(ds)
            conn.Close()

        Catch ex As Exception

        End Try

        Return ds

    End Function

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
        Return _rtnMessage

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

End Class
