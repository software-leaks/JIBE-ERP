<%@ Page Title="User List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UserList.aspx.cs" Inherits="Infrastructure_Libraries_UserList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="~/UserControl/ucCustomStringFilter.ascx" TagName="ucfString" TagPrefix="CustomFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtFirstName").value == "") {
                alert("Please enter first name.");
                document.getElementById("ctl00_MainContent_txtFirstName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlDepartment").value == '0') {
                alert("Select Department.");
                document.getElementById("ctl00_MainContent_ddlDepartment").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtLastName").value == "") {
                alert("Please enter last name.");
                document.getElementById("ctl00_MainContent_txtLastName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtApprovalLimit").value != "") {

                if (isNaN(document.getElementById("ctl00_MainContent_txtApprovalLimit").value)) {
                    alert("Approval limit field allow numeric only.");
                    document.getElementById("ctl00_MainContent_txtApprovalLimit").focus();
                    return false
                }
            }
            if (document.getElementById("ctl00_MainContent_ddlNationality").value == "0") {
                alert("Select Nationality.");
                document.getElementById("ctl00_MainContent_ddlNationality").focus();
                return false;
            }      

            if (document.getElementById("ctl00_MainContent_ddlCompany").value == "0") {
                alert("Select company.");
                document.getElementById("ctl00_MainContent_ddlCompany").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtEMail").value == "") {
                alert("Please enter E-Mail.");
                document.getElementById("ctl00_MainContent_txtEMail").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtDateOfBirth").value.trim() == "") {
                alert("Please enter Date of Birth.");
                document.getElementById("ctl00_MainContent_txtDateOfBirth").focus();
                return false;
            }   
            return true
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        User List
    </div>
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loader.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
            <div style="width: 100%; height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 35px; width: 100%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 10%">
                                        Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <asp:TextBox ID="txtfilter" runat="server"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 10%">
                                        User Type :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlUserTypeFilter" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlUserTypeFilter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Comp.Name :&nbsp;
                                    </td>
                                    <td style="width: 10%">
                                        <asp:DropDownList ID="ddlCompanyFilter" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 8%">
                                        Dept :&nbsp;
                                    </td>
                                    <td style="width: 10%">
                                        <asp:DropDownList ID="ddlDepartmentFilter" runat="server" Width="100px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 8%">
                                        Manager :&nbsp;
                                    </td>
                                    <td style="width: 10%">
                                        <asp:DropDownList ID="ddlManagerFilter" runat="server" Width="150px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/Add-icon.png" OnClientClick="javascript:location.href='User.aspx';"
                                            ToolTip="Add New User" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="GridViewUsers" runat="server" CellPadding="1" OnRowDataBound="GridViewUsers_RowDataBound"
                                    OnSorting="GridViewUsers_Sorting" EmptyDataText="NO RECORDS FOUND!" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="100%" GridLines="Both" DataKeyNames="USERID">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="User ID">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblUserIDHeader" runat="server" CommandName="Sort" CommandArgument="User_Name"
                                                    ForeColor="Black">User ID&nbsp;</asp:LinkButton>
                                                <img id="User_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserID" runat="server" Text='<%#Eval("User_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="65px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblFirst_NameHeader" runat="server" CommandName="Sort" CommandArgument="First_Name"
                                                    ForeColor="Black">First Name&nbsp;</asp:LinkButton>
                                                <img id="First_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblFirst_Name" Style="color: Black" runat="server" Text='<%#Eval("First_Name")%>'
                                                    CommandArgument='<%#Eval("USERID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="280px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblLast_NameHeader" runat="server" CommandName="Sort" CommandArgument="Last_Name"
                                                    ForeColor="Black">Last Name&nbsp;</asp:LinkButton>
                                                <img id="Last_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLast_Name" runat="server" Text='<%#Eval("Last_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile_Number")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMailID" runat="server" Text='<%#Eval("MailID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDept" runat="server" Text='<%#Eval("User_Dept")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manager">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManager" runat="server" Text='<%#Eval("ManagerFirstName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[USERID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[USERID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindUserGrid" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 40%;">
                            <div style="border: 0px solid gray; padding: 15px 15px 15px 15px">
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr style="padding: 15px 15px 0px 15px">
                                        <td align="right" style="width: 18%">
                                            First Name :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left" style="width: 30%">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtInput" Width="100%" MaxLength="500"></asp:TextBox>
                                        </td>
                                        <td align="right" style="width: 18%">
                                            Last Name :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left" style="width: 30%">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtInput" Width="100%" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Date Of Birth :&nbsp;
                                        </td>
                                         <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                            <cc1:CalendarExtender ID="id_DateOfBirth_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateOfBirth">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            Designation :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Present Address :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPresentAddress" runat="server" TextMode="MultiLine" Width="100%"
                                                Height="40px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Permanent Address :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine" Width="100%"
                                                CssClass="txtInput" Height="40px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Approval Limit :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtApprovalLimit" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            User Type :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlUserType" runat="server" Width="102%" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                                                CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Date Of Joining :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDateOfJoining" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                            <cc1:CalendarExtender ID="id_DateOfJoining_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateOfJoining">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            Company :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="txtInput" Width="102%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            E-Mail :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEMail" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Date Of Probation :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDateOfProbation" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                            <cc1:CalendarExtender ID="id_DateOfProbation_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateOfProbation">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Department :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="txtInput" Width="102%"
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            Fleet :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlFleet" runat="server" CssClass="txtInput" Width="102%" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Mobile :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Manager :&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlManager" runat="server" CssClass="txtInput" Width="102%"
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
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
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                        </td>
                                    </tr>
                                    <tr style="background-color: #d8d8d8;">
                                        <td align="center" colspan="6">
                                            <asp:Button ID="btnSaveUserDetails" runat="server" Text="Save" OnClientClick="return validation();"
                                                OnClick="btnSaveUserDetails_Click" />
                                            <asp:TextBox ID="txtUserID" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div>
                            <CustomFilter:ucfDropdown ID="ucf_DDLCompany" Width="200" Height="200" UseJavaScriptForControlAction="false"
                                OnApplySearch="BindUserGrid" runat="server" />
                            <CustomFilter:ucfDropdown ID="ucf_DDLDepartment" Width="200" Height="200" UseJavaScriptForControlAction="false"
                                OnApplySearch="BindUserGrid" runat="server" />
                            <CustomFilter:ucfDropdown ID="ucf_DDLManager" Width="200" Height="200" UseJavaScriptForControlAction="false"
                                OnApplySearch="BindUserGrid" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
