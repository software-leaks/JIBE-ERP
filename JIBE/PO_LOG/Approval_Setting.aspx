<%@ Page Title="Approval Setting" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Approval_Setting.aspx.cs" Inherits="PO_LOG_Approval_Setting" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
      <link href="../Purchase/styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/POLOG_Common_Function.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <%--<script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">

         function Validation() {



             return true;
         }

         function OpenScreen(ID, MAXAPPROVALLIMIT) {

             var url = 'POLOG_Limit_Entry.aspx?Limit_ID=' + ID;
             OpenPopupWindowBtnID('Approval_Group', 'Approval Limit Entry', url, 'popup', 800, 750, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
         } 

    </script>
    <style type="text/css">
        .listbox
        {
            border: 0px;
        }
        .SelectedNodeStyle
        {
            background: url(../../Images/bg.png) left -1672px repeat-x;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../../Images/bg.png) left -1672px repeat-x;
            font-size: 14px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
         .HeaderStyle-center
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Approval Setting
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="4">
                <tr>
                    <td align="right" style="width: 10%;">
                        Group Name:
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:TextBox ID="txtSearchGroup" runat="server" Width="200px" AutoPostBack="true"></asp:TextBox>
                    </td>
                     <td align="right" style="width: 8%;">
                        PO Type:
                    </td>
                    <td align="left" style="width: 5%;">
                          <asp:DropDownList ID="ddlPOTypefilter" runat="server" Width="200px" 
                              CssClass="txtInput"  AutoPostBack="true"
                              onselectedindexchanged="ddlPOTypefilter_SelectedIndexChanged">
                                        </asp:DropDownList>
                    </td>
                     <td align="right" style="width: 10%;">
                        Account Classification:
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:DropDownList ID="ddlAccClassifictaion" runat="server" Width="200px" CssClass="txtInput">
                                        </asp:DropDownList>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                            ImageUrl="~/Images/SearchButton.png" />
                    </td>
                    <td align="left" style="width: 50%;">
                        <asp:Button ID="btnAddGroup" runat="server" Text="Add New Group" OnClick="btnAddGroup_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnAddLimit" runat="server" Text="Add New Limit" OnClientClick="OpenScreen(0, 0)" />
                    </td>
                    
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 25%;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:GridView ID="gvGroup" runat="server" AutoGenerateColumns="False" DataKeyNames="VARIABLE_CODE,Approval_Group_Code"
                                Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvGroup_RowDataBound">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name">
                                        <HeaderTemplate>
                                            PO Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVARIABLE_NAME" runat="server" Text='<%#Eval("VARIABLE_NAME") %>'></asp:Label>
                                            <asp:Label ID="lbltxt" runat="server" Text='('></asp:Label>
                                            <asp:Label ID="lblVARIABLE_CODE" runat="server" Text='<%#Eval("VARIABLE_CODE") %>'>
                                            </asp:Label><asp:Label ID="lbltxt1" runat="server" Text=')'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Group Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("Approval_Group_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgUpdate" runat="server"  OnCommand="onUpdate"
                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Approval_Group_Code]")%>'
                                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="Imgview" runat="server"  OnCommand="onView" Visible='<%# uaEditFlag %>'
                                                CommandArgument='<%#Eval("[Approval_Group_Code]") + "," + Eval("[VARIABLE_CODE]")%>'
                                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/asl_view.png"   Height="16px" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                             <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindPOGroup" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:GridView ID="gvAccount" runat="server" AutoGenerateColumns="False" DataKeyNames="VARIABLE_CODE"
                                Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvAccount_RowDataBound">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name">
                                        <HeaderTemplate>
                                            Account Classification
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVARIABLE_NAME" runat="server" Text='<%#Eval("VARIABLE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Select
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Select" Checked='<%# Convert.ToBoolean(Eval("Selected")) %>'
                                                ForeColor="white" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindPOGroup" />
                        </div>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                                Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvUser_RowDataBound">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name">
                                        <HeaderTemplate>
                                            User Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser_Name" runat="server" Text='<%#Eval("User_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Invoice Creator
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInvoice_Creator" runat="server" ToolTip="Invoice Creator" Checked='<%# Convert.ToBoolean(Eval("Invoice_Creator")) %>'
                                                ForeColor="white" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Approver
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInvoice_Approver" runat="server" ToolTip="Invoice Approver"
                                                Checked='<%# Convert.ToBoolean(Eval("Invoice_Approver")) %>' ForeColor="white" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:GridView ID="gvLimit" runat="server" AutoGenerateColumns="False" DataKeyNames="Limit_ID"
                                Width="100%" CellPadding="4" ForeColor="#333333" GridLines="both" 
                                onrowdatabound="gvLimit_RowDataBound">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Min Amt">
                                        <HeaderTemplate>
                                            Min Amt
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMin_Approval_Limit" runat="server" Text='<%#Eval("Min_Approval_Limit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max Amt">
                                        <HeaderTemplate>
                                            Max Amt
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser_Name" runat="server" Text='<%#Eval("Max_Approval_Limit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver">
                                        <HeaderTemplate>
                                         PO
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblPO_Approver" runat="server" Text='<%#Eval("PO_Approver") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Invoice
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblInvoice_Approver" runat="server" Text='<%#Eval("Invoice_Approver") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Final
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblFinal_Approver" runat="server" Text='<%#Eval("Invoice_Final_Approver") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Advance
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                              <asp:Label ID="lblAdvance_Approver" runat="server" Text='<%#Eval("Advance_Approver") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgUpdate" runat="server" 
                                                Visible='<%# uaEditFlag %>' OnClientClick='<%#"OpenScreen(&#39;" + Eval("[Limit_ID]") +"&#39;);return false;"%>'
                                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                text-align: left; font-size: 12px; color: Black; width: 30%">
                <table width="100%" cellpadding="2" cellspacing="2">
                    <tr>
                        <td align="right" style="width: 15%">
                            PO Type &nbsp;:&nbsp;
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left" style="width: 25%">
                            <asp:DropDownList ID="ddlPOType" runat="server" CssClass="txtInput">
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="ReqtxtshortName" runat="server" InitialValue="0" Display="None" ErrorMessage="PO Type is mandatory field."
                                                            ControlToValidate="ddlPOType" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 15%">
                            Group Name &nbsp;:&nbsp;
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left" style="width: 25%">
                            <asp:TextBox ID="txtGroupName" CssClass="txtInput" MaxLength="100" Width="90%" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="Group name is mandatory field."
                                                            ControlToValidate="txtGroupName" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                            <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="vgSubmit" 
                                OnClick="btnsave_Click" />
                            <asp:TextBox ID="txtGroupID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-size: 11px; text-align: center;">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                background-color: #FDFDFD">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                            * Mandatory fields
                        </td>
                    </tr>
                      <tr>
                            <td colspan="3">
                                <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="vgSubmit" />
                            </td>
                        </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
        <div>
        <table width="100%"><tr><td align="center" ><asp:Button ID="btnSaveItem" runat="server" 
                Width="200px" Text="Save" onclick="btnSaveItem_Click" /></td></tr></table>
        </div>
        <div style="display:none"> <asp:TextBox ID="txtGroupCode" runat="server" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtPOType" runat="server" Width="1px"></asp:TextBox></div>
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
