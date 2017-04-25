<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site_NoMenu.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.password-strength.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var myPSPlugin = $("[id$='NewPassword']").password_strength();

            $("[id$='ChangePasswordPushButton']").click(function () {
                return myPSPlugin.metReq(); //return true or false
            });

            $("[id$='passwordPolicy']").click(function (event) {
                var width = 350, height = 300, left = (screen.width / 2) - (width / 2),
            top = (screen.height / 2) - (height / 2);
                window.open("PasswordPolicy.xml", 'Password_poplicy', 'width=' + width + ',height=' + height + ',left=' + left + ',top=' + top);
                event.preventDefault();
                return false;
            });

        });    
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Change Password
    </h2>
    <p>
        <span class="failureNotification">
            <asp:Literal ID="lblMsg" runat="server"></asp:Literal>
        </span>
    </p>
    <p>
        Use the form below to change your password.
    </p>
    <table style="width:100%">
        <tr>
            <td  style="vertical-align:top">
                <asp:ChangePassword ID="ChangeUserPassword" runat="server" EnableViewState="false" 
                    RenderOuterTable="false">
                    <ChangePasswordTemplate>
                        <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
                            ValidationGroup="ChangeUserPasswordValidationGroup" />
                        <div class="accountInfo">
                            <fieldset class="changePassword" style="width:500px;height:200px;">
                                <legend>Account Information</legend>
                                <p>
                                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                                    <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required."
                                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                                    <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                        CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNew_Password">Confirm New Password:</asp:Label>
                                    <asp:TextBox ID="ConfirmNew_Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNew_Password"
                                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                                        ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                        ControlToValidate="ConfirmNew_Password" CssClass="failureNotification" Display="Dynamic"
                                        ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                                </p>
                            </fieldset>
                            <p class="submitButton">
                                <asp:Button ID="ChangePasswordPushButton" runat="server" Text="Change Password" ValidationGroup="ChangeUserPasswordValidationGroup"
                                    OnClick="ChangePasswordPushButton_Click" />
                                <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                                    Text="Cancel" onclick="CancelPushButton_Click" />
                            </p>
                        </div>
                    </ChangePasswordTemplate>
                </asp:ChangePassword>
            </td>
            <td style="vertical-align:top">
                <fieldset class="PasswordPolicy" style="height:200px;">
                    <legend>Password Policy</legend>
                    <p>
                        <iframe src="PasswordPolicy.xml" style="border:0;width:400px;height:150px"></iframe>
                    </p>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
