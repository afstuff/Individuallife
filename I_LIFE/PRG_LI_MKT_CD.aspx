﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_MKT_CD.aspx.vb" Inherits="I_LIFE_PRG_LI_MKT_CD" %>

<%@ Register src="../UC_BANX.ascx" tagname="UC_BANX" tagprefix="uc1" %>
<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agents Details Setup Page</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
   </script>
</head>
<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">                      
        
        <uc1:UC_BANX ID="UC_BANX1" runat="server" />
        
    </div>


    <!-- content -->
    <div id="div_content" align="center">

        <table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="4" class="tbl_buttons">
                    <table align="center" border="1">
                        <tr>
                            <td align="left" colspan="2" valign="baseline"><asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP()"></asp:button>
                                &nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                &nbsp;<asp:Button ID="cmdPrint_ASP" Enabled="false" CssClass="cmd_butt" Text="Print" runat="server" />
                                &nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" runat="server" text="Delete Data" OnClientClick="JSDelete_ASP()"></asp:button>
                                &nbsp;&nbsp;Status:
                                &nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;
                                <!-- <A HREF="<%= request.ApplicationPath %>/Setup/UCD001.aspx" TARGET="fraDetails">Previous Menu</A>&nbsp;| -->
                                <!-- &nbsp;|&nbsp;<a href="javascript:window.close()" style="font-weight:bold;">Close Page</a>&nbsp;| -->
    	                    </td>
    	                    <td align="right" colspan="2" valign="baseline">Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}"></input>&nbsp;
                                <asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                    </td>
                        </tr>
                    
                    </table>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4" valign="top" class="td_menu">
                    <table align="center" border="0" cellpadding="1" cellspacing="1"  class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title"><%=STRPAGE_TITLE%>
                                        </td>
                                    </tr>
                	    <tr>
                	        <td align="left" nowrap colspan="3"><asp:Label id="lblMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                &nbsp;<a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_cust')">Returns to Previous Page</a>
                                &nbsp;<%=PageLinks%>&nbsp;
                            </td>
    	               	</tr>

                    <tr>
                    <td align="center" colspan="4" valign="top">
                    <table align="center" style="background-color: White; width: 95%;">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                    DataKeyNames="TBIL_AGCY_AGENT_REC_ID" HorizontalAlign="Left"
                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="false" PageSize="10"
                                    PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                    EmptyDataText="No data available..."
                                    GridLines="Both" ShowFooter="True">                        

                        
                                    <PagerStyle CssClass="grd_page_style" />
                                    <HeaderStyle CssClass="grd_header_style" />
                                    <RowStyle CssClass="grd_row_style" />
                                    <SelectedRowStyle CssClass="grd_selrow_style" />
                                    <EditRowStyle CssClass="grd_editrow_style" />
                                    <AlternatingRowStyle CssClass="grd_altrow_style" />
                                    <FooterStyle CssClass="grd_footer_style" />
                    
                                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" 
                                        PreviousPageText="Previous">
                                    </PagerSettings>
                        
                                    <Columns>
                                        <asp:TemplateField>
        			                        <ItemTemplate>
        						                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            
                                        <asp:CommandField ShowSelectButton="True" />
                            
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_AGENT_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_AGENT_ID" HeaderText="Ref.ID" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_REG_CODE" HeaderText="Region Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_REG_NAME" HeaderText="Region Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_AGCY_CODE" HeaderText="Agency Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_AGCY_NAME" HeaderText="Agency Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_UNIT_CODE" HeaderText="Unit Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_UNIT_NAME" HeaderText="Unit Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_AGENT_CD" HeaderText="Agent Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_AGCY_AGENT_NAME" HeaderText="Agent Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                    </Columns>
   
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>

                    <tr>
                        <td colspan="4"><hr /></td>
                    </tr>

                		<tr>
    	                    <td align="right" nowrap><asp:CheckBox id="chkNum" runat="server" AutoPostBack="true" class="chk_Butt" />
    	                        &nbsp;<asp:Label ID="lblNum" Text="Agent Code:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtNum" MaxLength="10" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                		        &nbsp;<asp:Button ID="cmdGetRecord" Enabled="false" Text="Get Record" runat="server" />
                		        &nbsp;<asp:DropDownList id="cboTransList" AutoPostBack="true" Visible="true" Width="400px" runat="server" OnSelectedIndexChanged="DoProc_Agcy_Agent_Change"></asp:DropDownList>&nbsp;
                            </td>
    		            </tr>

                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustName" Text="Agent Name :" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustName" MaxLength="49" Width="400px" runat="server" EnableViewState="true" ></asp:textbox>&nbsp;</td>
    		            </tr>
        	        	<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblAgcy_Type" Text="Agent Type:" runat="server"></asp:Label>&nbsp;</td>
        	        	    <td align="left" nowrap colspan="3"><asp:DropDownList id="cboAgcy_Type" Visible="true" Width="400px" AutoPostBack="false" runat="server"></asp:DropDownList>
        	        	        &nbsp;<asp:textbox id="txtAgcy_Type" Visible="false" Enabled="false" MaxLength="3" Width="50px" runat="server" EnableViewState="true"></asp:textbox>
        	        	        &nbsp;<asp:textbox id="txtAgcy_Type_Name" Visible="false" Enabled="false" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                            </td>
        	        	</tr>
        	        	<tr style="display: none;">
    	                    <td align="right" nowrap><asp:Label ID="lblLoc_Num" Text="Agency Location:" runat="server"></asp:Label>&nbsp;</td>
        	        	    <td align="left" nowrap colspan="3"><asp:DropDownList id="cboLocation" Visible="true" Width="200px" AutoPostBack="false" runat="server"></asp:DropDownList>
        	        	        &nbsp;<asp:textbox id="txtLoc_Num" Visible="false" Enabled="false" MaxLength="3" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                            </td>
        	        	</tr>

                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustAddr01" Text="Address Line-1:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustAddr01" MaxLength="39" Width="400px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		            </tr>
                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustAddr02" Text="Address Line-2:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustAddr02" MaxLength="39" Width="400px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		            </tr>
                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustPhone01" Text="GSM/Mobile No:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustPhone01" MaxLength="11" Width="200px" runat="server" EnableViewState="true" ></asp:textbox>
                		        &nbsp;<asp:Label ID="lblCustPhone02" Text="Land Line No:" runat="server"></asp:Label>
                		        &nbsp;<asp:textbox id="txtCustPhone02" MaxLength="11" Width="200px" runat="server" EnableViewState="true" ></asp:textbox>
                		    </td>
    		            </tr>

                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustEmail01" Text="Email Address - 1:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustEmail01" MaxLength="49" Width="450px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		            </tr>

                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustEmail02" Text="Email Address - 2:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustEmail02" MaxLength="49" Width="450px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		            </tr>



                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblCustID" Enabled="false" Text="Record ID:" runat="server"></asp:Label>&nbsp;</td>
            	    	    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustID" Enabled="false" MaxLength="3" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
            	    	        &nbsp;<asp:Label ID="lblRecNo" Text="Rec. No:" Enabled="false" runat="server"></asp:Label>
            	    	        &nbsp;<asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                            </td>
        	        	</tr>
                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblAgcy_Region_Num" Text="Region Code:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtAgcy_Region_Num" MaxLength="10" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                		        &nbsp;<asp:DropDownList id="cboAgcy_Region_Name" AutoPostBack="true" Visible="true" Width="400px" runat="server" OnSelectedIndexChanged="DoProc_Agcy_Region_Change"></asp:DropDownList>
                            </td>
    		            </tr>
    		            <tr>
    	                    <td align="right" nowrap><asp:Label ID="lblAgcy_Region_Name" Text="Region Name:" runat="server"></asp:Label>&nbsp;</td>
    		                <td align="left" nowrap colspan="3"><asp:textbox id="txtAgcy_Region_Name" MaxLength="49" Width="400px" runat="server" EnableViewState="true"></asp:textbox></td>
    		            </tr>
                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblAgcy_Agcy_Num" Text="Agency Code:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtAgcy_Agcy_Num" MaxLength="10" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                		        &nbsp;<asp:DropDownList id="cboAgcy_Agency_List" AutoPostBack="true" Visible="true" Width="400px" runat="server" OnSelectedIndexChanged="DoProc_Agcy_Agency_Change"></asp:DropDownList>
                            </td>
                		</tr>
                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblAgcy_Agcy_Name" Text="Agency Name:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtAgcy_Agcy_Name" MaxLength="49" Width="400px" runat="server" EnableViewState="true"></asp:textbox>
                            </td>
    		            </tr>

                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblAgcy_Unit_Num" Text="Unit Code:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtAgcy_Unit_Num" MaxLength="10" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                		        &nbsp;<asp:DropDownList id="cboAgcy_Unit_List" AutoPostBack="true" Visible="true" Width="400px" runat="server" OnSelectedIndexChanged="DoProc_Agcy_Unit_Change"></asp:DropDownList>
                            </td>
                		</tr>
                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblAgcy_Unit_Name" Text="Unit Name:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtAgcy_Unit_Name" MaxLength="49" Width="400px" runat="server" EnableViewState="true"></asp:textbox>
                            </td>
    		            </tr>

                    
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
    		
                    </table>
                
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>

        </table>
    
    </div>


<!-- footer -->
<div id="div_footer" align="center">

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td>                                                                                   
                            <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


</div>

    </form>
</body>
</html>
