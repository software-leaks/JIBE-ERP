<%@ Page Title="Mail Settings" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FMSMailSettings.aspx.cs" Inherits="FMS_FMSMailSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function validation() {
            var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
            var email = document.getElementById("ctl00_MainContent_txtMailID").value.trim()
            if (re.test(email) == false) {
                alert("Please enter valid E-Mail.");
                document.getElementById("ctl00_MainContent_txtMailID").value = '';
                document.getElementById("ctl00_MainContent_txtMailID").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset style="text-align: left; margin: 0px; padding: 2px; height: 70px">
        <legend>Mail Setting:</legend>
        <table style="width: 40%" align="center">
            <tr>
                <td style="width: 5%;">
                    Email:
                </td>
                <td style="width: 15%;">
                    <asp:TextBox ID="txtMailID" runat="server" Width="350px"></asp:TextBox>
                </td>
                <td style="width: 9%;">
                    <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" Width="65px"
                        OnClientClick="return validation();" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
