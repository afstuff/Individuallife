
Partial Class G_LIFE_GL_2010
    Inherits System.Web.UI.Page

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        STRMENU_TITLE = "Quotation - Group Life Assurance"

        If Not (Page.IsPostBack()) Then
            Me.lblMsg.Text = "Status:"
        End If
        'Dim xx As Integer = Me.selScheme_Type.SelectedIndex
    End Sub

    Public Sub selScheme_Server_Change()
        Me.lblMsg.Text = "You selected " & CStr(Me.selScheme_Server.SelectedIndex)
        'Me.lblMsg.Text = "You selected ..."

    End Sub

End Class

