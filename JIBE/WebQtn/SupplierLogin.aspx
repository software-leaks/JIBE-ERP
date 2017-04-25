<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierLogin.aspx.cs" Inherits="WebQuotation_SupplierLogin"
    Title="Supplier Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>login page</title>
    <style type="text/css">
        .style1
        {
            font-family: Verdana;
            color: #FFFFFF;
            background-color: #666666;
            font-size: small;
            font-weight: bold;
        }
        .header
        {
            position: relative;
            margin: 0px;
            padding: 0px;
            background: #4b6c9e;
            width: 100%;
            color: White;
            font-size: 14px;
            font-weight:bold;
        }
        .title
        {
            display: block;
            float: left;
            text-align: left;
            width: auto;
            color: White;
        }
        
        input.textEntry
        {
            width: 320px;
            border: 1px solid #ccc;
        }
        
        input.passwordEntry
        {
            width: 320px;
            border: 1px solid #ccc;
        }
        legend
        {
            font-size: 1.1em;
            font-weight: 600;
            padding: 2px 4px 8px 4px;
        }
        .failureNotification
        {
            font-size: 1.2em;
            color: Red;
        }
        body
        {
            color: Black;
            width: 100%;
            margin: 0px;
            padding: 0px;
            height: 100%;
            background-color: #A4A4A4;
            
        }
        h2
        {
            font-size: 1.5em;
            font-weight: 600;
        }
        h1
        {
            font-size: 18px;
            padding-top: 20px;
        }
    </style>
</head>
<body style="font-family: Tahoma;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 100%; text-align: center">
        <div style="width: 1024px; text-align: center; border: 1px solid gray">
            <div class="header" style="text-align: left;">
               
                        JIBE
               
               
            </div>
            <div style="text-align: center; font-size: 11px; background-color: White; border: 1px solid gray;
                height: 400px">
                <h2>
                    Log In
                </h2>
                <br />
                Please enter your username and password.
                <asp:Login ID="Login1" runat="server" EnableViewState="false" RenderOuterTable="false"
                    OnAuthenticate="Login1_Authenticate">
                    <LayoutTemplate>
                        <span class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </span>
                        <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                            ValidationGroup="LoginUserValidationGroup" />
                        <div style="width: 42%;">
                            <fieldset class="login" style="text-align: left">
                                <legend>Account Information</legend>
                                <p>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                            </fieldset>
                            <p class="submitButton">
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                                <br />
                                <br />
                                <asp:LinkButton ID="lbtnForgotPassword" OnClientClick="javascript:var a= confirm('You have choosen to reset your JIBE password.  Do you want to continue ?');if(a){ document.getElementById('dvForgotPassword').style.display='block';}return false;"
                                    Text="Forgot Password ?" Font-Size="12px" runat="server"></asp:LinkButton>
                            </p>
                        </div>
                    </LayoutTemplate>
                </asp:Login>
            </div>
        </div>
        <div id="dvForgotPassword" style="display: none; position: fixed; left: 30%; top: 30%;
            font-size: 11px; background-color: Silver; border: 1px solid black; z-index: 200">
            <table cellpadding="5px">
                <tr>
                    <td style="text-align: center; font-size: 14px; font-weight: bold; border-bottom: 1px solid gray">
                        Forgot Password
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        Please enter User ID :
                        <asp:TextBox ID="txtUserid" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter user id !"
                            ForeColor="Red" ValidationGroup="fp" ControlToValidate="txtUserid"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="btnSendPassword" Text="Reset Password" runat="server" OnClick="btnSendPassword_Click"
                            ValidationGroup="fp" />
                        <asp:Button ID="btncancel" Text="Cancel" runat="server" OnClientClick="javascript:document.getElementById('dvForgotPassword').style.display='none';return false;" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
