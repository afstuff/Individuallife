Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class I_LIFE_PRG_LI_CLM_WAIVER_PROCESS
    Inherits System.Web.UI.Page
    '    <System.Web.Services.WebMethod()> _
    'Public Shared Function GetPolicyPerInfo(ByVal _policyNo As String) As String
    '        Dim codeinfo As String = String.Empty
    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        Try
    '            codeinfo = waiverRepo.GetPolicyPerInfo1(_policyNo)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function

    '    <System.Web.Services.WebMethod()> _
    'Public Shared Function GetInsuredDetails(ByVal _assuredCode As String) As String
    '        Dim codeinfo As String = String.Empty
    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        Try
    '            codeinfo = waiverRepo.GetInsuredDetails(_assuredCode)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function

    '    <System.Web.Services.WebMethod()> _
    'Public Shared Function GetProductCode(ByVal _policyNo As String) As String
    '        Dim codeinfo As String = String.Empty

    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        Try
    '            codeinfo = waiverRepo.GetProductCode(_policyNo)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function

    '    <System.Web.Services.WebMethod()> _
    'Public Shared Function GetProductDetails(ByVal _policyProductCode As String) As String
    '        Dim codeinfo As String = String.Empty
    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        Try
    '            codeinfo = waiverRepo.GetProductDetails(_policyProductCode)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function

    '    <System.Web.Services.WebMethod()> _
    'Public Shared Function GetCoverCodes() As String
    '        Dim codeinfo As String = String.Empty
    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        Try
    '            codeinfo = waiverRepo.GetCoverCodes()
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function

    '    '<System.Web.Services.WebMethod()> _
    '    'Public Shared Function GetTBIL_POLY_STATUS(ByVal _status As String) As String
    '    '        Dim codeinfo As String = String.Empty
    '    '     
    '    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '    '        'Dim crit As String = 

    '    '        Try
    '    '            'codeinfo = admRepo.GetMiscAdminInfo(_classcode, _itemcode)
    '    '            codeinfo = waiverRepo.GetPoly_Status(_status)
    '    '            Return codeinfo
    '    '        Finally
    '    '            If codeinfo = "<NewDataSet />" Then
    '    '                Throw New Exception()
    '    '            End If
    '    '        End Try
    '    '    End Function
    '    <System.Web.Services.WebMethod()> _
    '    Public Shared Function GetPolicyNos(ByVal _search As String) As String
    '        Dim codeinfo As String = String.Empty

    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        'Dim crit As String = 

    '        Try
    '            'codeinfo = admRepo.GetMiscAdminInfo(_classcode, _itemcode)
    '            codeinfo = waiverRepo.GetPoly_Nos(_search)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function
    '    <System.Web.Services.WebMethod()> _
    'Public Shared Function VerifyAdditionalCover(ByVal _WaiverCodes As String, ByVal _PolicyNumber As String) As String
    '        Dim codeinfo As String = String.Empty
    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        Try
    '            codeinfo = waiverRepo.VerifyAdditionalCover(_WaiverCodes, _PolicyNumber)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function
    '    <System.Web.Services.WebMethod()> _
    'Public Shared Function GetEffectedWaiverDsc(ByVal waiverCode As String) As String
    '        Dim codeinfo As String = String.Empty

    '        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
    '        Try
    '            codeinfo = waiverRepo.GetEffectedWaiverDsc(waiverCode)
    '            Return codeinfo
    '        Finally
    '            If codeinfo = "<NewDataSet />" Then
    '                Throw New Exception()
    '            End If
    '        End Try
    '    End Function
    ' DATABASE OPERATIONS
End Class
