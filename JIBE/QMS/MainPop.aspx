<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPop.aspx.cs" Inherits="QMS_Main_MainPop" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log Entry</title>

    <script type="text/javascript">
  function CheckUser() 
    
    {
    myMessage('Please select an user from the user list')
    
    }
      	
    </script>
        <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />

</head>
<body text="&quot;&quot;" style="background-color: #B7C7A5">
    <form id="form1" runat="server" style="background-color: #B7C7A5">
    
    
    <div >
        <div id="page-header" style="background-color: #B7C7A5">
            <asp:Label ID="lblPageHeader" runat="server"></asp:Label>
        </div>
        <div id="page-content" style="font-size: medium;">
            <table style="height: 121px; width: 100%; background-color: #DBE3D2; 
                font-size: medium; border: 1px solid black">
                <tr>
                    <td colspan="2" style="font-size: 14px; text-align: center; font-weight: bold">
                        Mark Selected manual as Read
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="Div1" style="font-size: small">
                            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Small"
                                ForeColor="Red"></asp:Label>
                            <br />
                            <asp:Label ID="lblMessage2" runat="server" Font-Bold="True" 
                                Font-Size="Small" ForeColor="#B05800"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: small; text-align: right; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style2">
                        User Name:
                    </td>
                    <td style="font-size: 11px; font-family: Calibri; text-align: left; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold">
                        <asp:DropDownList ID="DDUser" runat="server" Height="20px" OnSelectedIndexChanged="DDUser_SelectedIndexChanged"
                             Font-Size="Small" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: small;  text-align: right; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style2">
                        Parent Manual:
                    </td>
                    <td style="font-size: 11px; font-family: Calibri; text-align: left; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold">
                        <asp:Label ID="LBLM2" runat="server" Font-Bold="False"  Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: small;  text-align: right; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style2">
                        Child Manual:
                    </td>
                    <td style="font-size: 11px; font-family: Calibri; text-align: left; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold">
                        <asp:Label ID="LBLM3" runat="server" Font-Bold="False"  Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: small;  text-align: right; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style2">
                        File Name:
                    </td>
                    <td style="font-size: 11px; font-family: Calibri; text-align: left; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold">
                        <asp:Label ID="LBLFile" runat="server" Font-Bold="False"  Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: small;  text-align: right; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style2">
                        Date:
                    </td>
                    <td style="font-size: 11px; font-family: Calibri; text-align: left; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold">
                        <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-css" ></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtdate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: small;  text-align: right; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style1">
                        <asp:CheckBox ID="chkDocAllSelected" runat="server" />
                    </td>
                    <td style="font-size: small;  text-align: left; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style1">
                        <asp:Label ID="lblText" runat="server" Text="Mark all the document in the current directory as read"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td></td>
                    <td style="font-size: small;  text-align: left; border-style: solid;
                        border-color: Silver; border-width: 1px; font-weight: bold" class="style1" colspan="2">
                        <asp:Button ID="btnsave" runat="server" CssClass="button-css" vertical align="center"
                            TabIndex="1" Text="Save" Width="54px" OnClick="btnsave_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btncancel" runat="server" CssClass="button-css" Text="Cancel" OnClick="btncancel_Click"
                            vertical align="center" />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
