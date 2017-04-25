<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="Shortcut Icon" href="../Images/jibe.ico" type="image/x-icon" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvResetPasswork" title="Reset password" style="width: 800px; position: absolute;
        left: ">
        <table width="100%">
            <tr>
                <td style="text-align: left; font-size: 12px; color: Black; font-family: Tahoma">
                    To reset your password, enter your email address that is registered in JiBE.
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; font-size: 12px; color: Black; font-family: Tahoma">
                    Email Address
                    <br />
                    <asp:TextBox ID="txtResetEmailid" runat="server" Width="300"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: left">
                <td>
                    <br />
                    <asp:Button ID="btnResetpassword" runat="server" Text="Reset password" OnClick="btnResetpassword_Click" />
             <br /><br />
                    
                  
                    <asp:Label ID="lblresetMessage" runat="server" Font-Size="12px" ForeColor="Red"></asp:Label>  <br />
                    <br />
                    <asp:LinkButton  ID="btnclosediv" runat="server" Text="Click here to return to the login page" OnClick="btnclosediv_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
