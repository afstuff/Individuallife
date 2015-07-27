Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.OleDb

Public Class PRG_LI_CLM_WAIVER_REPOSITORY
    Dim mystr_conn As String = ""
    Dim conn As OleDbConnection = Nothing
    Dim cmd As OleDbCommand = New OleDbCommand()
    Public Function GetPolicyPerInfo1(ByVal _policyNo As String) As String
        Return GetDataSet(_policyNo).GetXml()

    End Function
    ' Private Function GetDataSet(ByVal qry As String) As DataSet
    Private Function GetDataSet(ByVal _policy As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        ' Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_POLICY_INFO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_POL_NO", _policy)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function
    Public Function GetInsuredDetails(ByVal _assuredCode As String) As String
        Return GetInsuredDetailsDataSet(_assuredCode).GetXml()
    End Function
    Private Function GetInsuredDetailsDataSet(ByVal _assuredCode As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "ciFn_GetInsuredDetails"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_TBIL_INSRD_CODE", _assuredCode)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function
    Public Function GetProductCode(ByVal _policyNo As String) As String
        Return GetProductCodeDataSet(_policyNo).GetXml()
    End Function
    Private Function GetProductCodeDataSet(ByVal _policyNo As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "ciFn_GetProductCode"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_POL_PRM_PROP_NO", _policyNo)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function

    Public Function GetProductDetails(ByVal _policyProductCode As String) As String
        Return GetProductDetailsDataSet(_policyProductCode).GetXml()
    End Function
    Private Function GetProductDetailsDataSet(ByVal _policyProductCode As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "ciFn_GetProductDetails"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_TBIL_PRDCT_DTL_CODE", _policyProductCode)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function
    Public Function GetCoverCodes() As String
        Return GetCoverCodesDataSet().GetXml()
    End Function
    Public Function GetCoverCodesDataSet() As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        'Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_COVERCODES"
        cmd.CommandType = CommandType.StoredProcedure
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        conn.Dispose()
        Return Ds
    End Function


    Public Function EffectWaiver(ByVal waiverCode As String, ByVal policyNo As String, ByVal waiverEffectiveDate As Date) As String
        Dim msg As String
        'msg = ""
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        conn.Open()
        cmd.CommandText = "SPIL_POLICY_DET_WAIVER_UPDATE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_WAIVER_DT", waiverEffectiveDate)
        cmd.Parameters.AddWithValue("@PARAM_POLY_STATUS", "W")
        cmd.Parameters.AddWithValue("@PARAM_POLY_POLICY_NO", policyNo)
        cmd.Parameters.AddWithValue("@PARAM_POLY_KEYDTE", Now)
        cmd.ExecuteNonQuery()
        msg = "Waiver effected succssfully"
        'End If
        conn.Close()
        Return msg
    End Function
    Public Function GetPoly_Nos(ByVal _search As String) As String
        Return GetPoly_NosDataSet(_search).GetXml()
    End Function
    Private Function GetPoly_NosDataSet(ByVal _search As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_INSURED_CODES"
        cmd.CommandType = CommandType.StoredProcedure
        'cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@PARAM_INSRD_SURNAME", _search)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function
    Public Function VerifyAdditionalCover(ByVal _WaiverCodes As String, ByVal _PolicyNumber As String) As String
        Return VerifyAdditionalCoverDataSet(_WaiverCodes, _PolicyNumber).GetXml()
    End Function
    Private Function VerifyAdditionalCoverDataSet(ByVal _WaiverCodes As String, ByVal _PolicyNumber As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "ciFn_VerifyAdditionalCover"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_COVER_CODE", _WaiverCodes)
        cmd.Parameters.AddWithValue("@PARAM_POL_ADD_POLY_NO", _PolicyNumber)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function

    Public Function GetEffectedWaiverDsc(ByVal waiverCode As String) As String
        Return GetEffectedWaiverDscDataSet(waiverCode).GetXml()
    End Function
    Private Function GetEffectedWaiverDscDataSet(ByVal waiverCode As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_EFFECTED_COVER_DESC"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_COVER_CD", waiverCode)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function
End Class
