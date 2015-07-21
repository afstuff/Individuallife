<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GL_2010.aspx.vb" Inherits="G_LIFE_GL_2010" title="Untitled Page" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
        <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js"></script>
        <script language="javascript" type="text/javascript" src="../Script/SJS_02.js"></script>
        
        <script language="javascript" type="text/javascript">
            function myFunc_Master()
            {
                alert("Message from Master page...");
            }
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- start header -->
    <div id="div_header" align="center">
        <table id="tbl_header" align="center">
            <tr>
                <td align="left" valign="top" class="myMenu_Title">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left" valign="top"><%=STRMENU_TITLE%></td>
                            <td align="right" valign="top">
                                &nbsp;<a href="#" onclick="javascript:JSDO_RETURN('MENU_GL.aspx?menu=gl_quote')">Returns to Previous Page</a>&nbsp;
                            </td>
                        </tr>
                    </table>                    
                </td>
            </tr>
        </table>
    </div>
    
    <!-- start content -->
    <div id="div_content" align="center">
        <asp:TabContainer ID="TabContainer1" runat="server" Width="1000px" ScrollBars="Both" ActiveTabIndex="0">
            
            <asp:TabPanel ID="TabPanel1" runat="server" Width="100%" ScrollBars="Both" HeaderText="Group Business Details">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Group Business Details</caption>
                            <tr>
                                <td align="left" valign="top" class="td_menu">
                                    <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
                                            <tr>
                                                <td align="center" colspan="4" valign="top">
                                                    <asp:button id="cmdNew_ASP" Font-Bold="true" Font-Size="Large" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                                    &nbsp;&nbsp;<asp:button id="cmdSave_ASP" Font-Bold="true" Font-Size="Large" runat="server" text="Save Data" OnClientClick="JSSave_ASP();"></asp:button>
                                                    &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" Font-Bold="true" Font-Size="Large" runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                                    &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" Enabled="false" Font-Bold="true" Font-Size="Large" runat="server" text="Print"></asp:button>
                                                    &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox><!-- &nbsp;&nbsp;<a href="javascript:window.close();">Close...</a> --></td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4" valign="top"><hr /></td>
                                            </tr>

                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Text="Quotation No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtQuote_Num" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="Label1" Text="Transaction Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtTrans_Date" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblStart_Date" Text="Start Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtStart_Date" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblEnd_Date" Text="Expiry Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtEnd_Date" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblSelScheme" Text="Scheme Type:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <select id="selScheme_Type" class="selScheme" runat="server"  onchange="getSelected_Change('selScheme_Type','txtScheme_Type','Form1')">
                                                        <option value="*">select option</option>
                                                        <option value="001">Scheme A</option>
                                                        <option value="002">Scheme B</option>
                                                    </select>
                                                    &nbsp;<asp:TextBox ID="txtScheme_Type" runat="server" Width="42px"></asp:TextBox>&nbsp;
                                                </td>
                                                <td align="left" valign="top"><asp:Label ID="Label3" Text="Scheme Type:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <select id="selScheme_Server" class="selScheme" runat="server" onchange="myFunc_Master()">
                                                        <option value="*">select option</option>
                                                        <option value="001">Scheme A</option>
                                                        <option value="002">Scheme B</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4" valign="top">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4" valign="top">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" colspan="4" valign="top">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>                                                                                                                            
                </ContentTemplate>            
            
            </asp:TabPanel>

            <asp:TabPanel ID="TabPanel2" Enabled="false" runat="server" Width="100%" ScrollBars="Both" HeaderText="Group Member Details">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Group Member Details</caption>
                        <tr>
                            <td align="left" valign="top" class="td_menu">
	                            <table border="0" align="center" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top">&nbsp;</td>
                                    </tr>
				                </table>
                			</td>
                        </tr>
                    </table>
                </ContentTemplate>            
            
            </asp:TabPanel>

            <asp:TabPanel ID="TabPanel3" runat="server" Width="100%" ScrollBars="Both" HeaderText="Attach Scan Document">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Attach Scan Document</caption>
                        <tr>
                            <td align="left" valign="top" class="td_menu">
	                            <table border="0" align="center" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top">&nbsp;</td>
                                    </tr>
				                </table>
                			</td>
                        </tr>
                    </table>        
                </ContentTemplate>            
            </asp:TabPanel>

        </asp:TabContainer>
        
    </div>

<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td>                            
                            <uc1:UC_FOOT ID="UC_FOOT1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    

</asp:Content>

