
Partial Class Annuity_PRG_LI_ANNUITY_POLY_CONVERT
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_POLICY_DET"

        STRMENU_TITLE = UCase("+++ Convert Proposal to Policy +++ ")

        If Not (Page.IsPostBack) Then
            Call Proc_DoNew()

            Me.cmdFileNum.Enabled = True
            Me.BUT_OK.Enabled = False
            Me.txtPro_Pol_Num.Text = "QI/2014/1501/E/E003/I/0000001"
            Me.txtFileNum.Text = "6004025"

            Me.txtPro_Pol_Num.Enabled = True
            Me.txtPro_Pol_Num.Focus()
        End If


        If Me.txtAction.Text = "New" Then
            Me.txtPro_Pol_Num.Text = ""
            Call Proc_DoNew()
            Me.txtAction.Text = ""
            Me.lblMsg.Text = "New Entry..."

            Me.txtPro_Pol_Num.Enabled = True
            Me.txtPro_Pol_Num.Focus()
        End If
    End Sub
    Private Sub Proc_DoNew()

        Me.cmdNew_ASP.Enabled = True
        Me.cmdFileNum.Enabled = True

        Me.txtPro_Pol_Num.Text = ""
        Me.txtPro_Pol_Num.Enabled = True
        Me.txtFileNum.Text = ""
        Me.txtFileNum.Enabled = True

        Me.txtPol_Num.Text = ""
        Me.txtAssured_Name.Text = ""

        Me.txtProductClass.Text = ""
        Me.txtProduct_Num.Text = ""
        Me.txtProduct_Name.Text = ""

        Me.txtBraNum.Text = ""
        Me.txtPol_Eff_Date.Text = ""

        Me.txtTrans_Date.Text = ""
        Me.txtTrans_Num.Text = ""
        Me.txtTrans_Amt.Text = ""

        Me.chkAccept.Enabled = False
        Me.chkAccept.Checked = False

        Me.lblPWD.Enabled = False
        Me.txtPWD.Enabled = False
        Me.txtPWD.Text = ""

        Me.BUT_OK.Enabled = False

    End Sub
End Class
