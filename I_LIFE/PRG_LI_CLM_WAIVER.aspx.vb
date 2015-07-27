Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class PRG_LI_CLM_WAIVER
    Inherits System.Web.UI.Page
    Dim WaiverEffectiveDate As DateTime
    Dim PolicyEndDate As DateTime
    Dim ErrorInd As String
    Dim waiverRep As PRG_LI_CLM_WAIVER_REPOSITORY = New PRG_LI_CLM_WAIVER_REPOSITORY()
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected strStatus As String
    Dim strREC_ID As String
    Dim strTable As String
    Dim strSQL As String
    Protected strTableName As String
    Dim strErrMsg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtAssuredName.Attributes.Add("disabled", "disabled")
        txtProdDesc.Attributes.Add("disabled", "disabled")
        txtPolicyStartDate.Attributes.Add("disabled", "disabled")
        txtPolicyEndDate.Attributes.Add("disabled", "disabled")
        If Not IsPostBack Then
        Else
            txtAssuredName.Text = HidAssuredName.Value
            txtProdDesc.Text = HidProdDesc.Value
        End If
    End Sub
    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPolicyNumber.Text = ""
                Me.txtPolicyProCode.Text = ""
                Me.txtAssuredCode.Text = ""
                'Me.txtSearch.Value = ""
            Else
                'Me.txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                'strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtPolicyNumber.Text, RTrim("0"))
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub cmdSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch1.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If
    End Sub
    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click

        lblMsg.Visible = False
        ErrorInd = ""
        ValidateControls(ErrorInd)
        If ErrorInd = "Y" Then
            Exit Sub
        End If

        WaiverEffectiveDate = CDate(txtWaiverEffectiveDate.Text)
        WaiverEffectiveDate = Format(WaiverEffectiveDate, "yyyy/MM/dd")
        lblMsg.Text = waiverRep.EffectWaiver(drpWaiverCodes.Text, txtPolicyNumber.Text, WaiverEffectiveDate)
        lblMsg.Visible = True
        initializeFields()
    End Sub
    Private Sub ValidateControls(ByRef ErrorInd As String)
        If (txtPolicyNumber.Text = String.Empty) Then
            lblMsg.Text = "Please enter a policy number"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtAssuredCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter a assurance code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyProCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy product code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyStartDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy start date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyEndDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy end date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtWaiverEffectiveDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter waiver effective date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (drpWaiverCodes.Text = "Select") Then
            lblMsg.Text = "Please select a waiver code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (drpWaiverCodes.Text = "Select") Then
            lblMsg.Text = "Please select a waiver code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolyStatus.Value = "") Then
            lblMsg.Text = "Waiver cannot be process because status is null"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (Not IsDate(txtPolicyEndDate.Text)) Then
            lblMsg.Text = "Policy end date is not valid"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        Else
            PolicyEndDate = CDate(txtPolicyEndDate.Text)
        End If

        If (PolicyEndDate < Now) Then
            lblMsg.Text = "Policy End Date must be greater than today"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolyStatus.Value <> "A") Then
            lblMsg.Text = "Policy status must be Active"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (Not IsDate(txtWaiverEffectiveDate.Text)) Then
            lblMsg.Text = "Please enter a valid waiver effective date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (chkConfirmWaiver.Checked = False) Then
            lblMsg.Text = "Please confirm WAIVER"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

    End Sub

    Private Sub initializeFields()
        txtPolicyNumber.Text = String.Empty
        txtAssuredCode.Text = String.Empty
        txtAssuredName.Text = String.Empty
        txtPolicyProCode.Text = String.Empty
        drpWaiverCodes.SelectedIndex = 0
        txtProdDesc.Text = String.Empty
        txtPolicyStartDate.Text = String.Empty
        txtPolicyEndDate.Text = String.Empty
        txtWaiverEffectiveDate.Text = String.Empty
    End Sub
    <System.Web.Services.WebMethod()> _
Public Shared Function GetPolicyPerInfo(ByVal _policyNo As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetPolicyPerInfo1(_policyNo)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    <System.Web.Services.WebMethod()> _
Public Shared Function GetInsuredDetails(ByVal _assuredCode As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetInsuredDetails(_assuredCode)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function

    <System.Web.Services.WebMethod()> _
Public Shared Function GetProductCode(ByVal _policyNo As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetProductCode(_policyNo)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function

    <System.Web.Services.WebMethod()> _
Public Shared Function GetProductDetails(ByVal _policyProductCode As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetProductDetails(_policyProductCode)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function

    <System.Web.Services.WebMethod()> _
Public Shared Function GetCoverCodes() As String
        Dim codeinfo As String = String.Empty
        '  Dim admRepo As New AdminCodeRepository()
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetCoverCodes()
            Dim i As Integer
            i = 8
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function

    '<System.Web.Services.WebMethod()> _
    'Public Shared Function GetTBIL_POLY_STATUS(ByVal _status As String) As String
    '        Dim codeinfo As String = String.Empty
    '        '   Dim admRepo As New AdminCodeRepository()
    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        'Dim crit As String = 

    '        Try
    '            'codeinfo = admRepo.GetMiscAdminInfo(_classcode, _itemcode)
    '            codeinfo = waiverRepo.GetPoly_Status(_status)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPolicyNos(ByVal _search As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetPoly_Nos(_search)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function
    <System.Web.Services.WebMethod()> _
Public Shared Function VerifyAdditionalCover(ByVal _WaiverCodes As String, ByVal _PolicyNumber As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.VerifyAdditionalCover(_WaiverCodes, _PolicyNumber)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function
    <System.Web.Services.WebMethod()> _
Public Shared Function GetEffectedWaiverDsc(ByVal waiverCode As String) As String
        Dim codeinfo As String = String.Empty
        '   Dim admRepo As New AdminCodeRepository()
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetEffectedWaiverDsc(waiverCode)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function
End Class
