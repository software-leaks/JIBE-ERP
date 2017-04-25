<%@ Page Title="User Creation" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="User.aspx.cs" Inherits="Infrastructure_Libraries_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function UserNameValidation() {
            var str = document.getElementById("ctl00_MainContent_txtUserName").value;

            if (/^[a-zA-Z0-9]*$/.test(str)) {

            }
            else {

                document.getElementById("ctl00_MainContent_txtUserName").value = "";
                alert('Invalid User Name, User Name should not contain any white spaces or special characters!');

            }

        }


        function validation() {
            if (document.getElementById("ctl00_MainContent_txtUserName").value.trim() == "") {
                alert("Please enter User name.");
                document.getElementById("ctl00_MainContent_txtUserName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtFirstName").value.trim() == "") {
                alert("Please enter first name.");
                document.getElementById("ctl00_MainContent_txtFirstName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtLastName").value.trim() == "") {
                alert("Please enter last name.");
                document.getElementById("ctl00_MainContent_txtLastName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtDateOfBirth").value.trim() == "") {                
                    alert("Please enter date of birth.");
                    document.getElementById("ctl00_MainContent_txtDateOfBirth").focus();
                    return false;
                }   

            if (document.getElementById("ctl00_MainContent_ddlUserType").value == "0") {
                alert("Please select  user type.");
                document.getElementById("ctl00_MainContent_ddlUserType").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlCompany").value == "0") {
                alert("Please select company.");
                document.getElementById("ctl00_MainContent_ddlCompany").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtEMail").value.trim() == "") {
                alert("Please enter E-Mail.");
                document.getElementById("ctl00_MainContent_txtEMail").focus();
                return false;
            }
            else {
                var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
                var email = document.getElementById("ctl00_MainContent_txtEMail").value.trim()
                if (re.test(email) == false) 
                {
                    alert("Please enter valid E-Mail.");
                    document.getElementById("ctl00_MainContent_txtEMail").focus();
                    return false;
                }
            }
            if (document.getElementById("ctl00_MainContent_ddlDepartment").value == '0') {
                alert("Please Select Department.");
                document.getElementById("ctl00_MainContent_ddlDepartment").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtPassword").value.trim() == "") {
                alert("Please enter password.");
                document.getElementById("ctl00_MainContent_txtPassword").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlNationality").value == "0") {
                alert("Please Select Nationality.");
                document.getElementById("ctl00_MainContent_ddlNationality").focus();
                return false;
            }      
            return true
            return true
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        User Creation
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dvContent" style="text-align: center; border: 1px solid #5588BB; height: 500px">
                <div>
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: top; width: 25%;">
                                <div style="border: 1px solid #dcdcdc">
                                    <div style="background-color: #cfdfef; text-align: left; height: 20px; color: #444;
                                        font-size: 12px; font-weight: bold; padding: 2px;">
                                        Type user name and press Enter key
                                    </div>
                                    <table cellpadding="2" cellspacing="2" width="90%" style="color: Black">
                                        <tr>
                                            <td colspan="3" style="padding: 5px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                User Name&nbsp;:&nbsp;
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtUserName" runat="server" OnTextChanged="txtUserName_TextChanged"
                                                    onchange="UserNameValidation()" CssClass="txtInput" AutoPostBack="true" Width="100%" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="vertical-align: top; width: 75%">
                                <asp:Panel ID="pnlUserDetails" runat="server" Visible="false" Width="100%" Style="border-collapse: collapse;
                                    color: Black; border: 1px solid #dcdcdc;">
                                    <div style="background-color: #cfdfef; text-align: left; height: 20px; font-weight: bold;
                                        color: #444; padding: 2px; font-size: 12px">
                                        Enter New User Details
                                    </div>
                                    <center>
                                        <table border="0" cellpadding="2" cellspacing="2" width="90%">
                                            <tr>
                                                <td colspan="6" style="padding: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 15%">
                                                    First Name&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="right" style="width: 35%">
                                                    <asp:TextBox ID="txtFirstName" runat="server" Width="100%"  CssClass="txtInput" MaxLength="500"> </asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 20%">
                                                    Last Name&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="right" style="width: 35%">
                                                    <asp:TextBox ID="txtLastName" runat="server" Width="100%"  CssClass="txtInput" MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Date Of Birth&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDateOfBirth" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="id_DateOfBirth_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateOfBirth">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    Designation&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDesignation" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Present Address&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPresentAddress" runat="server" TextMode="MultiLine" Width="100%"
                                                        CssClass="txtInput" Height="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Permanent Address&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine" Width="100%"
                                                        CssClass="txtInput" Height="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    
                                                    User Type&nbsp;:</td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    
                                                    <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="true" 
                                                        AutoPostBack="true" CssClass="txtInput" 
                                                        OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" Width="102%">
                                                    </asp:DropDownList>
                                                    
                                                </td>
                                                <td align="right">
                                                    Company&nbsp;:&nbsp; &nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *</td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" 
                                                        CssClass="txtInput" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" 
                                                        Width="102%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Date Of Joining&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDateOfJoining" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="id_DateOfJoining_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateOfJoining">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    E-Mail&nbsp;:</td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    &nbsp;*
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEMail" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Department&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *</td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="txtInput" 
                                                        Width="102%">
                                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    <table border="0" cellpadding="2" cellspacing="2" width="90%">
                                                        <tr>
                                                            <td align="right">
                                                                Date Of Probation&nbsp;:&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDateOfProbation" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtDateOfProbation1_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateOfProbation">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Fleet&nbsp;:</td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlFleet" runat="server" CssClass="txtInput" 
                                                        Width="102%">
                                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    &nbsp;Manager&nbsp;:
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlManager" runat="server" CssClass="txtInput" 
                                                        Width="102%">
                                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;Mobile&nbsp;:
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    &nbsp;Password&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="txtInput" 
                                                        TextMode="Password" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="right">
                                                    &nbsp;Nationality&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                   <asp:DropDownList ID="ddlNationality" runat="server" Width="154px" CssClass="txtInput">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                    &nbsp;</td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    &nbsp;</td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtApprovalLimit" Visible="false" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox></td>
                                                
                                            </tr>
                                        </table>
                                    </center>
                                    <div style="margin-top: 20px; background-color: #d8d8d8; text-align: center">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 10%">
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSaveUserDetails" runat="server" Text="Save" OnClientClick="return validation();"
                                                        OnClick="btnSaveUserDetails_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label><br />
                <asp:LinkButton ID="hlnk1" runat="server" ClientIDMode="Static" onclick="hlnk1_Click"></asp:LinkButton><br />
                <asp:Label ID="lblOr" runat="server" ClientIDMode="Static"></asp:Label><br />
                 <asp:LinkButton ID="hlnk2" runat="server" ClientIDMode="Static"  PostBackUrl="~/Infrastructure/Libraries/UserList.aspx"></asp:LinkButton><br />
                
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
