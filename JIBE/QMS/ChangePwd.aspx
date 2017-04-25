<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="ChangePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password Page</title>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function OpenDoc(url) {
            document.getElementById("ifrmDocument").src = url;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    <div style="height: 15px;">
    </div>
    
    
    <asp:ChangePassword ID="ChangePassword2" runat="server" Height="142px" Width="581px"
        OnChangedPassword="ChangePassword2_ChangedPassword">
        <ChangePasswordTemplate>
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
                                <td align="left" style="width: 35%;">
                                    <asp:Label ID="currentpasswordlabel" runat="server" AssociatedControlID="currentpassword">password</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="currentpassword" runat="server" TextMode="password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="currentpasswordrequired" runat="server" ControlToValidate="currentpassword"
                                        ErrorMessage="password is required." ToolTip="password is required." ValidationGroup="changepassword2">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="newpasswordlabel" runat="server" AssociatedControlID="newpassword">new password</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="newpassword" runat="server" TextMode="password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="newpasswordrequired" runat="server" ControlToValidate="newpassword"
                                        ErrorMessage="new password is required." ToolTip="new password is required."
                                        ValidationGroup="changepassword2">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="confirmnewpasswordlabel" runat="server" AssociatedControlID="confirmnewpassword">confirm new password</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="confirmnewpassword" runat="server" TextMode="password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="confirmnewpasswordrequired" runat="server" ControlToValidate="confirmnewpassword"
                                        ErrorMessage="confirm new password is required." ToolTip="confirm new password is required."
                                        ValidationGroup="changepassword2">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                                <td align="left">
                                    <asp:Button ID="changepasswordpushbutton" runat="server" Width="156px" CommandName="changepassword"
                                        ValidationGroup="changepassword2" OnClick="ChangePasswordPushButton_Click" Text="change password" />
                                    &nbsp;&nbsp;<%--<asp:button id="cancelpushbutton" runat="server" causesvalidation="false" commandname="cancel"
                                                text="cancel"  OnClientClick="OpenDoc('FileLoader.aspx')" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="newpasswordcompare" runat="server" ControlToCompare="newpassword"
                                        ControlToValidate="confirmnewpassword" Display="dynamic" ErrorMessage="the confirm new password must match the new password entry."
                                        ValidationGroup="changepassword2"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
    </form>
</body>
</html>
