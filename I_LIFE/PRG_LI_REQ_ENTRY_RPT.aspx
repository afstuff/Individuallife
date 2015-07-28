<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_REQ_ENTRY_RPT.aspx.vb"
    Inherits="I_LIFE_PRG_LI_REQ_ENTRY_RPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 60%;" align="center">
            <tr>
                <th colspan="2">
                    Enter Report Parameters
                </th>
            </tr>
            <tr>
                <td>
                    Start Date:
                </td>
                <td>
                    <asp:TextBox ID="startDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    End Date:
                </td>
                <td>
                    <asp:TextBox ID="endDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="viewPrintBtn" runat="server" Text="View Print" Width="120px" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
