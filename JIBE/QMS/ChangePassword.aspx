<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <title>Change Password Page</title>
    <style type="text/css">
        .style1
        {
            width: 37%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    </br>
    <div>
        <table cellpadding="3" cellspacing="3" width="400px" style="border-color: blue; border-width: 1px;"
            align="center" border="1">
            <tr>
                <td align="center" valign="top" colspan="2">
                    <asp:Label ID="lblformtitle" runat="server" Text="Change your password" Font-Bold="true"
                        ForeColor="#003366" ToolTip="Change your password"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td align="left" class="style1">
                                <asp:Label ID="currentpasswordlabel" runat="server" AssociatedControlID="currentpassword">password</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="currentpassword" runat="server" TextMode="password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                <asp:Label ID="newpasswordlabel" runat="server" AssociatedControlID="newpassword">new password</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="newpassword" runat="server" TextMode="password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                <asp:Label ID="confirmnewpasswordlabel" runat="server" AssociatedControlID="confirmnewpassword">confirm new password</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="confirmnewpassword" runat="server" TextMode="password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                            </td>
                            <td align="left">
                                <asp:Button ID="ChangePassWord" runat="server" Text="Change Password" OnClick="ChangePassWord_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
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
