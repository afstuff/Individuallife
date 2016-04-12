Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Partial Class I_LIFE_PRG_LI_INDV_ENQUIRY
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
    Dim GenEnd_Date As Date
    Dim Eff_Date As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Load data for the DropDownList control only once, when the 
        ' page is first loaded.
        If Not (Page.IsPostBack) Then
            Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("I"), Me.cboProductClass)
            Me.lblMsg.Text = "Status:"
        End If
    End Sub
    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If
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
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub chkFileNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileNum.CheckedChanged
        If Me.chkFileNum.Checked = True Then
            Me.lblFileNum.Enabled = True
            Me.txtFileNum.Enabled = True
            Me.cmdFileNum.Enabled = True
        Else
            Me.lblFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.cmdFileNum.Enabled = False
        End If
    End Sub

    Protected Sub cmdFileNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileNum.Click
        If LTrim(RTrim(Me.txtFileNum.Text)) <> "" Then
            strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
        Else
            Me.lblMsg.Text = "Please enter a file number"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.txtFileNum.Focus()
            Exit Sub
        End If
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

        strSQL = "SPIL_GET_POLICY_ENQUIRY"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = FVstrRefNum
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POLY_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))

            Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
            Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_DESC") & vbNullString, String))


            If IsDate(objOLEDR("TBIL_POLY_PRPSAL_ISSUE_DATE")) Then
                Me.txtProposalDate.Text = Format(CType(objOLEDR("TBIL_POLY_PRPSAL_ISSUE_DATE"), DateTime), "dd/MM/yyyy")
            End If
            If IsDate(objOLEDR("TBIL_POL_PRM_FROM")) Then
                Me.txtCommenceDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
            End If
            If IsDate(objOLEDR("TBIL_POL_PRM_TO")) Then
                Me.txtMaturityDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
                myarrData = Split(Trim(txtMaturityDate.Text), "/")
                GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                If Trim(Me.txtRenewalDate.Text) = "" And Trim(Me.txtMaturityDate.Text) <> "" Then
                    Me.txtRenewalDate.Text = Format(DateAdd(DateInterval.Day, 1, GenEnd_Date), "dd/MM/yyyy")
                End If
            End If
            Me.txtAssuredName.Text = RTrim(CType(objOLEDR("TBIL_INSRD_NAME") & vbNullString, String))
            Me.txtTelephone.Text = RTrim(CType(objOLEDR("TBIL_INSRD_PHONE_NO") & vbNullString, String))
            Me.txtAddress.Text = RTrim(CType(objOLEDR("TBIL_INSRD_ADDRESS") & vbNullString, String))

            If Not IsDBNull(objOLEDR("TBIL_POL_PRM_SA_LC")) Then _
                        txtSumAssured.Text = Format(objOLEDR("TBIL_POL_PRM_SA_LC"), "Standard")

            If Not IsDBNull(objOLEDR("TBIL_POL_PRM_DTL_MOP_PRM_LC")) Then _
                        txtPremAmt.Text = Format(objOLEDR("TBIL_POL_PRM_DTL_MOP_PRM_LC"), "Standard")

            txtMop.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MODE_PAYT") & vbNullString, String))
            txtTenure.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PERIOD_YRS") & vbNullString, String))

            Me.txtCoverPeriod.Text = RTrim(CType(objOLEDR("ReceiptCoverPeriod") & vbNullString, String))


            If Not IsDBNull(objOLEDR("TBIL_POLY_CUST_CODE")) Then
                If CType(objOLEDR("TBIL_POLY_CUST_CODE") & vbNullString, String) <> "" Then
                    Me.txtMarketerCode.Text = RTrim(CType(objOLEDR("TBIL_POLY_CUST_CODE") & vbNullString, String))
                    Me.txtMarketerName.Text = RTrim(CType(objOLEDR("TBIL_CUST_DESC") & vbNullString, String))
                    Me.txtMarketerPhone.Text = RTrim(CType(objOLEDR("MARKETER_PHONE_NO") & vbNullString, String))
                    Me.txtMarketerAddress.Text = RTrim(CType(objOLEDR("MARKETER_ADDRESS") & vbNullString, String))
                    Me.txtMarketerEmail.Text = RTrim(CType(objOLEDR("MARKETER_EMAIL") & vbNullString, String))
                End If
            End If
            If Not IsDBNull(objOLEDR("TBIL_POLY_AGCY_CODE")) Then
                If CType(objOLEDR("TBIL_POLY_AGCY_CODE") & vbNullString, String) <> "" Then
                    Me.txtMarketerCode.Text = RTrim(CType(objOLEDR("TBIL_POLY_AGCY_CODE") & vbNullString, String))
                    Me.txtMarketerName.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
                    Me.txtMarketerPhone.Text = RTrim(CType(objOLEDR("AGENT_PHONE_NO") & vbNullString, String))
                    Me.txtMarketerAddress.Text = RTrim(CType(objOLEDR("AGENT_ADDRESS") & vbNullString, String))
                    Me.txtMarketerEmail.Text = RTrim(CType(objOLEDR("AGENT_EMAIL") & vbNullString, String))
                End If
            End If

            'GetReceiptCoverPeriod(txtPolNum.Text, mop, Me.txtEffDate.Text, mopContrib)

            'GetReceiptCoverPeriod(txtPolNum.Text, mop, "2014-01-01", mopContrib)

            Me.lblFileNum.Enabled = False
            Me.chkFileNum.Enabled = False
            Me.cmdFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False
            Me.cmdGetPol.Enabled = False
            Me.lblPlan_Num.Enabled = False
            Me.txtPolNum.Enabled = False
            Me.lblPolNum.Enabled = False
            Me.txtPolNum.Enabled = False
            Me.cmdGetPol.Enabled = False


            Me.cmdNew_ASP.Enabled = True
            Proc_DataBind()
            strOPT = "2"
            Me.lblMsg.Text = "Status: Record Retrieved"

        Else
            Select Case UCase(strP_TYPE)
                Case "NEW"
                    'STRMENU_TITLE = "New Proposal"
                    Me.lblPolNum.Enabled = False
                    Me.txtPolNum.Enabled = False
                    Me.cmdGetPol.Enabled = False
                Case "CHG"
                    'STRMENU_TITLE = "Change Mode"
                    Me.lblMsg.Text = "Sorry!. Unable to get record ..."
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Me.txtFileNum.Text = ""
                    Me.lblPolNum.Enabled = True
                    Me.txtPolNum.Enabled = True
                    Me.txtPolNum.Text = ""
                    Me.cmdGetPol.Enabled = True
                    'Call Proc_DoNew()
                Case "DEL"
                    'STRMENU_TITLE = "Delete Mode"
                    Me.lblMsg.Text = "Sorry!. Unable to get record ..."
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Me.txtFileNum.Text = ""
                    Me.txtPolNum.Text = ""
                    ' Call Proc_DoNew()
                Case Else
                    'strP_TYPE = "NEW"
                    'STRMENU_TITLE = "New Proposal"
            End Select

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

    Private Sub GetReceiptCoverPeriod(ByVal PolicyNo, ByVal Mop, ByVal EffDate, ByVal MopContrib)
        Dim contibution As Double
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try
        If MopContrib <> "" Or MopContrib <> Nothing Then
            contibution = CDbl(MopContrib)
        End If
        'strTable = strTableName
        'strSQL = ""
        'strSQL = strSQL & "SELECT *"
        'strSQL = strSQL & " FROM CiFn_ReceiptCoverPeriods('" + PolicyNo + "', '" + Mop + "', "
        'strSQL = strSQL & "  '" & 0 & "'," + contibution + ", " + 0 + ", NULL, NULL,NULL)"
        Dim _amtpaid = 0

        'strSQL = "SELECT * " _
        '                  + "FROM CiFn_ReceiptCoverPeriods('" _
        '                  + PolicyNo + "','" _
        '                  + Mop + "', '" _
        '                  + EffDate + "', " _
        '                  + MopContrib + "," _
        '                  + _amtpaid + ",NULL,NULL,NULL)"

        strSQL = "SELECT * FROM CiFn_ReceiptCoverPeriods('" + PolicyNo + "','" + Mop + "', '" + EffDate + "', " _
                          + MopContrib + "," _
                          + _amtpaid + ",NULL,NULL,NULL)"


        Try
            Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

            objOLECmd.CommandType = CommandType.Text
            Dim objOLEDR As OleDbDataReader
            objOLEDR = objOLECmd.ExecuteReader()
            If (objOLEDR.Read()) Then
                Me.txtCoverPeriod.Text = RTrim(CType(objOLEDR("sPeriodsCoverRange") & vbNullString, String))
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
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub cmdGetPol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetPol.Click
        If Trim(Me.txtPolNum.Text) <> "" Then
            strStatus = Proc_DoOpenRecord(RTrim("POL"), Me.txtPolNum.Text, RTrim("0"))
        Else
            Me.lblMsg.Text = "Please enter a policy number"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.txtPolNum.Focus()
            Exit Sub
        End If
    End Sub
    Private Sub DoNew()
        Me.txtFileNum.Text = ""
        Me.txtRecNo.Text = ""
        Me.txtQuote_Num.Text = ""
        Me.txtPolNum.Text = ""
        Me.txtCommenceDate.Text = ""
        Me.txtMaturityDate.Text = ""
        Me.txtProposalDate.Text = ""
        Me.txtRenewalDate.Text = ""
        Me.txtAssuredName.Text = ""
        Me.txtTelephone.Text = ""
        Me.txtAddress.Text = ""
        Me.txtMarketerCode.Text = ""
        Me.txtMarketerName.Text = ""
        Me.txtEffDate.Text = ""
        Me.txtCoverPeriod.Text = ""
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        Me.lblMsg.Text = ""
        Me.chkFileNum.Enabled = True
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        DoNew()
    End Sub

    Protected Sub chkPolNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPolNum.CheckedChanged
        If Me.chkPolNum.Checked = True Then
            Me.lblPolNum.Enabled = True
            Me.txtPolNum.Enabled = True
            Me.cmdGetPol.Enabled = True
        Else
            Me.lblPolNum.Enabled = False
            Me.txtPolNum.Enabled = False
            Me.cmdGetPol.Enabled = False
        End If
    End Sub
    Private Sub DoProc_CreateDataSource(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboDDList As DropDownList)

        pvcboDDList.Items.Clear()

        Dim pvField_Text As String = "MyFld_Text"
        Dim pvField_Value As String = "MyFld_Value"

        ' Create a table to store data for the DropDownList control.
        Dim obj_dt As DataTable = New DataTable()
        Dim obj_dr As DataRow
        Dim obj_dv As DataView

        ' Define the columns of the table.
        obj_dt.Columns.Add(New DataColumn(pvField_Text, GetType(String)))
        obj_dt.Columns.Add(New DataColumn(pvField_Value, GetType(String)))


        obj_dr = obj_dt.NewRow()
        obj_dr(pvField_Text) = "*** Select item ***"
        obj_dr(pvField_Value) = "0"

        obj_dt.Rows.Add(obj_dr)

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            obj_dv = New DataView(obj_dt)
            'Return obj_dv
            Exit Sub
        End Try


        Dim objOLECmd As OleDbCommand
        Dim objOLEDR As OleDbDataReader

        Select Case UCase(Trim(pvCODE))
            Case "IL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT RTRIM(TBIL_PRDCT_CAT_MDLE) + '=' + RTRIM(TBIL_PRDCT_CAT_CD) AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE = '" & RTrim("I") & "'"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_DESC"

            Case "GL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT RTRIM(TBIL_PRDCT_CAT_MDLE) + '=' + RTRIM(TBIL_PRDCT_CAT_CD) AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE = '" & RTrim("G") & "'"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_DESC"

            Case "IL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','I')"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_DESC"

            Case "GL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','G')"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_DESC"

        End Select

        objOLECmd = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        objOLEDR = objOLECmd.ExecuteReader()

        Do While objOLEDR.Read
            obj_dr = obj_dt.NewRow()
            obj_dr(pvField_Text) = RTrim(CType(objOLEDR("MyFld_Text") & vbNullString, String))
            obj_dr(pvField_Value) = RTrim(CType(objOLEDR("MyFld_Value") & vbNullString, String))

            obj_dt.Rows.Add(obj_dr)
        Loop

        obj_dt.AcceptChanges()


        objOLECmd = Nothing
        objOLEDR = Nothing

        Try
            'close connection to database
            objOLEConn.Close()
        Catch ex As Exception
            'Me.textMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            'Me.lblMsg.Text = ex.Message.ToString
        End Try

        objOLEConn = Nothing

        ' Create a DataView from the DataTable to act as the data source
        ' for the DropDownList control.
        obj_dv = New DataView(obj_dt)
        obj_dv.Sort = pvField_Value


        pvcboDDList.DataSource = obj_dv
        pvcboDDList.DataTextField = pvField_Text
        pvcboDDList.DataValueField = pvField_Value

        ' Bind the data to the control.
        pvcboDDList.DataBind()

        ' Set the default selected item, if desired.
        pvcboDDList.SelectedIndex = 0

        'Return obj_dv

    End Sub
    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try


        'Try
        strSQL = "SPIL_GET_RECEIPT_HISTORY"

        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
        objDA.SelectCommand.CommandType = CommandType.StoredProcedure
        objDA.SelectCommand.Parameters.Clear()

        objDA.SelectCommand.Parameters.Add("p01", OleDbType.VarChar, 50).Value = "D"
        objDA.SelectCommand.Parameters.Add("p02", OleDbType.VarChar, 50).Value = Trim(txtQuote_Num.Text)

        Dim objDS As DataSet = New DataSet()
        objDA.Fill(objDS)
        With GridView1
            .DataSource = objDS
            .DataBind()
        End With

        'Execute this if no payment history found with proposal no.
        If GridView1.Rows.Count = 0 Then
            objDA = New OleDbDataAdapter(strSQL, objOLEConn)
            objDA.SelectCommand.CommandType = CommandType.StoredProcedure
            objDA.SelectCommand.Parameters.Clear()
            objDA.SelectCommand.Parameters.Add("p01", OleDbType.VarChar, 50).Value = "P"
            objDA.SelectCommand.Parameters.Add("p02", OleDbType.VarChar, 50).Value = Trim(txtPolNum.Text)
            objDA.Fill(objDS)
            With GridView1
                .DataSource = objDS
                .DataBind()
            End With
        End If
        objDS.Dispose()

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()

        objDS = Nothing
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
        'Dim P As Integer = 0
        'Dim C As Integer = 0
        'C = 0
        'For P = 0 To Me.GridView1.Rows.Count - 1
        '    C = C + 1
        'Next
    End Sub
End Class
