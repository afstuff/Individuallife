<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_CLM_MATURE_RPT.aspx.vb"
    Inherits="I_LIFE_PRG_LI_CLM_MATURE_RPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <link rel="stylesheet" href="calendar.css" />
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <link href="css/rounded.css" rel="stylesheet" type="text/css" />

    <script src="jquery-1.11.0.js" type="text/javascript"></script>

    <script src="jquery.simplemodal.js" type="text/javascript"></script>

    <script language="JavaScript" src="calendar_eu.js" type="text/javascript"></script>

    <title></title>
    <style type="text/css">
        .tbl_menu_new
        {
            width: 489px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
    <form id="LifeReportsPrint" runat="server">
    <div>
    </div>
    <div class="newpage" style="margin-left: 20%!important; margin-right: 20%!important;
        width: 100%;">
        <table align="center" width="100%">
            <tr>
                <td>
                    <asp:Literal runat="server" Visible="false" ID="litMsgs"></asp:Literal>
                    <asp:Label runat="server" ID="Status" Font-Bold="true" ForeColor="Red" Visible="true"
                        Text="Status:"> </asp:Label>
                    <asp:Label runat="server" ID="Label1" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="grid" style="width: 60%!important;">
            <div class="rounded">
                <div class="top-outer">
                    <div class="top-inner">
                        <div class="top">
                            <h2>
                                PRINT: Claims Reported Listing</h2>
                        </div>
                    </div>
                </div>
                <div class="mid-outer" align="center">
                    <div class="mid-inner">
                        <div class="mid">
                            <table class="tbl_menu_new" align="left" cellpadding="5" cellspacing="5">
                                <tr>
                                    <td colspan="2" class="myMenu_Title" align="center">
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Filter Option:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="pFilterOption" runat="server" Height="26px" Width="200px">
                                            <asp:ListItem>-- Select Option --</asp:ListItem>
                                            <asp:ListItem Value="1">Policy Number</asp:ListItem>
                                            <asp:ListItem Value="2">Claims Number</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Search Value :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="sStartDate" runat="server" Width="200px" MaxLength="10" 
                                            Height="26px"></asp:TextBox>

                                        <script language="JavaScript" type="text/javascript">
                                            new tcal({ 'formname': 'PRG_LI_REQ_ENTRY_1', 'controlname': 'sStartDate' });</script>

                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td align="right">
                                        Report Type:
                                    </td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="rblTransType" runat="server">
                                            <asp:ListItem Text="Maturity Claims" Value="PRG_LI_REQ_ENTRY_1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td colspan="2">
                                        <p>
                                            &nbsp;
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="butOK" runat="server" Text="OK" Style="height: 26px" />
                                        <asp:Button ID="butClose" runat="server" Text="Close" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="bottom-outer">
                    <div class="bottom-inner">
                        <div class="bottom">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
