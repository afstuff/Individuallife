﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RPT_LI_TECH_REPORTS.aspx.vb" Inherits="I_LIFE_RPT_LI_TECH_REPORTS" %>

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
    <title>Tech Reports</title>
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
                                PRINT: Tech Reports</h2>
                        </div>
                    </div>
                </div>
                <div class="mid-outer">
                    <div class="mid-inner">
                        <div class="mid">
                             <table class="tbl_menu_new">
                                <tr>
                                    <td colspan="2" class="style1" align="center">
                                        <asp:Label ID="lblDesc1" runat="server" Text="List Print" Visible="False"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Start Proposal Date :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStartDate" runat="server" Width="150px" MaxLength="10"></asp:TextBox>

                                        <script language="JavaScript" type="text/javascript">
                                            new tcal({ 'formname': 'PRG_LI_REQ_ENTRY_1', 'controlname': 'sStartDate' });</script>

                                        dd/mm/yyyy
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        End Proposal Date :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="150px" MaxLength="10"></asp:TextBox>

                                        <script language="JavaScript" type="text/javascript">
                                            new tcal({ 'formname': 'PRG_LI_REQ_ENTRY_1', 'controlname': 'sEndDate' });</script>

                                        dd/mm/yyyy
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <asp:Label ID="lblCover_Num1" Text="Report Type:" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" valign="top" colspan="1">
                                        <asp:RadioButtonList ID="radReportType" runat="server">
                                            <asp:ListItem Value="rptTechOps">Master Dump report</asp:ListItem>
                                         <asp:ListItem Value="rptAgencyNBReport">Agency Business report</asp:ListItem>
                                         <asp:ListItem Value="rptMedicalUWReport">Medical underwriting report</asp:ListItem>
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
                                    <td>
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

