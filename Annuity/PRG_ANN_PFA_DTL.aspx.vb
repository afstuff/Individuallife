
Imports System.Data.OleDb
Imports System.Data

Partial Class Annuity_PRG_ANN_PFA_DTL
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String

    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected strPOP_UP As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strErrMsg As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_PFA_DETAIL"
    End Sub


    Private Sub DoSave()
        lblMessage.Text = ""
        Dim strMyVal As String

        If LTrim(RTrim(Me.txtTBIL_PFA_CODE.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Email Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBIL_PFA_CATG.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Category Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBIL_PFA_DESC.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Description Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBIL_PFA_SHRT_DESC.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Short Description Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBIL_PFA_BRANCH.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Branch Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBIL_PFA_ADRES1.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Address 1 Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBIL_PFA_PHONE1.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Phone 1 Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBIL_PFA_EMAIL1.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/Empty Email 1 Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If


        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try

        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 TBIL_PFA_CODE FROM " & strTable
        strSQL = strSQL & " WHERE RTRIM(ISNULL(TBIL_PFA_DESC,'')) = '" & RTrim(Me.txtTBIL_PFA_DESC.Text) & "'"
        'strSQL = strSQL & " AND TBIL_PFA_ID = '" & RTrim(Me.txtCustID.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtTBIL_PFA_CODE.Text) <> Trim(chk_objOLEDR("TBIL_PFA_CODE") & vbNullString) Then
                Me.lblMessage.Text = "Warning!. The code description you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBIL_PFA_CODE") & vbNullString)
                chk_objOLECmd = Nothing
                chk_objOLEDR = Nothing
                If objOLEConn.State = ConnectionState.Open Then
                    objOLEConn.Close()
                End If
                objOLEConn = Nothing
                Exit Sub
            End If
        End If

        chk_objOLECmd = Nothing
        chk_objOLEDR = Nothing

        'Try
        '    'open connection to database
        '    objOLEConn.Close()
        'Catch ex As Exception
        '    'Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
        '    'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '    Me.lblMessage.Text = ex.Message.ToString
        '    objOLEConn = Nothing
        '    Exit Sub
        'End Try



        'objOLEConn.ConnectionString = mystrCONN
        'Try
        '    'open connection to database
        '    objOLEConn.Open()
        'Catch ex As Exception
        '    'Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
        '    'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
        '    objOLEConn = Nothing
        '    Exit Sub
        'End Try


        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_PFA_DESC = '" & RTrim(txtTBIL_PFA_DESC.Text) & "'"
        'strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                'drNewRow("TBIL_CUST_ID") = RTrim(Me.txtCustID.Text)
                drNewRow("TBIL_PFA_CODE") = RTrim(Me.txtTBIL_PFA_CODE.Text)

                'drNewRow("TBIL_PFA_MDLE") = RTrim(Me.txtt.Text)
                'drNewRow("TBIL_PFA_CATG") = RTrim(Me.txtTBIL_PFA_CATG.Text)

                drNewRow("TBIL_PFA_DESC") = Left(RTrim(Me.txtTBIL_PFA_DESC.Text), 49)

                drNewRow("TBIL_PFA_ADRES1") = Left(LTrim(Me.txtTBIL_PFA_ADRES1.Text), 39)
                drNewRow("TBIL_PFA_ADRES2") = Left(LTrim(Me.txtTBIL_PFA_ADRES2.Text), 39)
                drNewRow("TBIL_PFA_PHONE1") = Left(LTrim(Me.txtTBIL_PFA_PHONE1.Text), 11)
                drNewRow("TBIL_PFA_PHONE2") = Left(LTrim(Me.txtTBIL_PFA_PHONE2.Text), 11)
                drNewRow("TBIL_PFA_EMAIL1") = Left(LTrim(Me.txtTBIL_PFA_EMAIL1.Text), 49)
                drNewRow("TBIL_PFA_EMAIL2") = Left(LTrim(Me.txtTBIL_PFA_EMAIL2.Text), 49)

                drNewRow("TBIL_PFA_FLAG") = "A"
                drNewRow("TBIL_PFA_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_PFA_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMessage.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record

                With obj_DT
                    .Rows(0)("TBIL_PFA_CODE") = RTrim(Me.txtTBIL_PFA_CODE.Text)

                    'drNewRow("TBIL_PFA_MDLE") = RTrim(Me.txtt.Text)
                    '.Rows(0)("TBIL_PFA_CATG") = RTrim(Me.txtTBIL_PFA_CATG.Text)

                    .Rows(0)("TBIL_PFA_DESC") = Left(RTrim(Me.txtTBIL_PFA_DESC.Text), 49)

                    .Rows(0)("TBIL_PFA_ADRES1") = Left(LTrim(Me.txtTBIL_PFA_ADRES1.Text), 39)
                    .Rows(0)("TBIL_PFA_ADRES2") = Left(LTrim(Me.txtTBIL_PFA_ADRES2.Text), 39)
                    .Rows(0)("TBIL_PFA_PHONE1") = Left(LTrim(Me.txtTBIL_PFA_PHONE1.Text), 11)
                    .Rows(0)("TBIL_PFA_PHONE2") = Left(LTrim(Me.txtTBIL_PFA_PHONE2.Text), 11)
                    .Rows(0)("TBIL_PFA_EMAIL1") = Left(LTrim(Me.txtTBIL_PFA_EMAIL1.Text), 49)
                    .Rows(0)("TBIL_PFA_EMAIL2") = Left(LTrim(Me.txtTBIL_PFA_EMAIL2.Text), 49)
                    .Rows(0)("TBIL_PFA_FLAG") = "C"
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMessage.Text = "Record Saved to Database Successfully."

            End If

        Catch ex As Exception
            Me.lblMessage.Text = ex.Message.ToString
            Exit Sub
        End Try

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        obj_DT.Dispose()
        obj_DT = Nothing

        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        'Me.lblMessage.Text = ""

        Me.txtSearch.Value = RTrim(Me.txtTBIL_PFA_DESC.Text)

        'Call Proc_Populate_Box("IL_BRK_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
        Call Proc_DataBind()
        Me.txtSearch.Value = ""

        'DoNew()

        Me.txtTBIL_PFA_DESC.Enabled = True
        Me.txtTBIL_PFA_DESC.Focus()

    End Sub

    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        'Try
        '    Me.txtCustModule.Text = cboCustModule.SelectedValue
        'Catch ex As Exception
        'End Try

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TBIL_PFA_REC_ID, TBIL_PFA_CODE, RTRIM(ISNULL(TBIL_PFA_DESC,'')) AS TBIL_PFA_FULL_NAME" & _
                        " RTRIM(ISNULL(TBIL_PFA_PHONE1,'')) + ' ' + RTRIM(ISNULL(TBIL_PFA_PHONE2,'')) AS TBIL_PFA_PHONE_NUM" & _
                         " FROM " & strTable & " where TBIL_PFA_DESC = '" & RTrim(Me.txtTBIL_PFA_DESC.Text) & "'" & _
                         " ORDER BY TBIL_PFA_DESC, RTRIM(ISNULL(TBIL_PFA_DESC,''))"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()
        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

        Dim objDS As DataSet = New DataSet()
        objDA.Fill(objDS, strTable)

        'Dim objDV As New DataView
        'objDV = objDS.Tables(strTable).DefaultView
        'objDV.Sort = "ACT_REC_NO"
        'Session("myobjDV") = objDV

        'With Me.DataGrid1
        '.DataSource = objDS
        '.DataBind()
        'End With

        With GridView1
            .DataSource = objDS
            .DataBind()
        End With

        'With Me.Repeater1
        '.DataSource = objDS
        '.DataBind()
        'End With

        'objDV.Dispose()
        'objDV = Nothing
        objDS = Nothing
        objDA = Nothing
        'objOLECmd.Dispose()
        'objOLECmd = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        If C >= 1 Then
            Me.cmdDelete_ASP.Enabled = True
        End If

    End Sub

    'Protected Sub DoDelete()

    '    Dim strMyVal As String
    '    strMyVal = RTrim(Me.txtPfaID.Text)
    '    If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
    '        Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustID.Text
    '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
    '        Exit Sub
    '    End If

    '    If Trim(Me.txtTBIL_PFA_DESC.Text) = "" Then
    '        Me.lblMessage.Text = "Missing PFA DESC."
    '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
    '        Exit Sub
    '    End If

    '    Dim intC As Long = 0

    '    strTable = strTableName

    '    strREC_ID = Trim(Me.txtCustNum.Text)

    '    strSQL = "SELECT TOP 1 TBIL_PFA_CODE FROM " & strTable
    '    strSQL = strSQL & " WHERE TBIL_PFA_CODE = '" & RTrim(strREC_ID) & "'"
    '    strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

    '    Dim mystrCONN As String = CType(Session("connstr"), String)
    '    Dim objOLEConn As New OleDbConnection(mystrCONN)
    '    Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

    '    objOLECmd.CommandType = CommandType.Text
    '    'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

    '    'open connection to database
    '    objOLEConn.Open()

    '    strOPT = "NEW"
    '    FirstMsg = ""

    '    Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()
    '    If (objOLEDR.Read()) Then
    '        strOPT = "OLD"
    '    End If

    '    ' dispose of open objects
    '    objOLECmd.Dispose()
    '    objOLECmd = Nothing

    '    If objOLEDR.IsClosed = False Then
    '        objOLEDR.Close()
    '    End If
    '    objOLEDR = Nothing

    '    Select Case RTrim(strOPT)
    '        Case "OLD"
    '            'Delete record
    '            'Me.lblMessage.Text = "Deleting record... "
    '            strSQL = ""
    '            strSQL = "DELETE FROM " & strTable
    '            strSQL = strSQL & " WHERE TBIL_CUST_CODE = '" & RTrim(strREC_ID) & "'"
    '            strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

    '            Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
    '            objOLECmd2.Connection = objOLEConn
    '            objOLECmd2.CommandType = CommandType.Text
    '            objOLECmd2.CommandText = strSQL
    '            intC = objOLECmd2.ExecuteNonQuery()
    '            objOLECmd2.Dispose()
    '            objOLECmd2 = Nothing
    '        Case Else
    '    End Select

    '    'Try
    '    'Catch ex As Exception
    '    'End Try

    '    If objOLEConn.State = ConnectionState.Open Then
    '        objOLEConn.Close()
    '    End If
    '    objOLEConn = Nothing


    '    If intC >= 1 Then
    '        Me.lblMessage.Text = "Record deleted successfully."
    '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
    '    Else
    '        Me.lblMessage.Text = "Sorry!. Record not deleted..."
    '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
    '    End If
    '    'Me.lblMessage.Text = ""

    '    'Call Proc_Populate_Box("IL_INS_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
    '    'Call Proc_DataBind()

    '    Me.cmdDelete_ASP.Enabled = False

    '    Call DoNew()
    '    Me.txtCustName.Enabled = True
    '    Me.txtCustName.Focus()

    'End Sub


    Sub GetAllPfaList()

    End Sub

    Sub GetPfaListByName(ByVal pfaDesc As String)

    End Sub

    Sub AddNewPfa()

    End Sub

    Sub EditPfaByRecId(ByVal recId As Int16)

    End Sub

    Function ValidateTextEntries() As Boolean

        Return False
    End Function

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtPfaID.Text = row.Cells(2).Text

        Me.txtTBIL_PFA_DESC.Text = row.Cells(4).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
        Dim sPhone As String() = (row.Cells(5).Text).Split(" ")
        Me.txtTBIL_PFA_PHONE1.Text = sPhone(0)
        Me.txtTBIL_PFA_PHONE2.Text = sPhone(1)
        'Call Proc_DDL_Get(Me.cboTransList, RTrim(Me.txtCustNum.Text))

        'Call Proc_OpenRecord(Me.txtCustNum.Text)

        lblMessage.Text = "You selected " & Me.txtTBIL_PFA_DESC.Text & " / " & Me.txtPfaID.Text & "."

    End Sub
End Class
