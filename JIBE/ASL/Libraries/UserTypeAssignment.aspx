<%@ Page Title="User Type Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserTypeAssignment.aspx.cs" Inherits="ASL_Libraries_UserTypeAssignment" %>
    <%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
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
            font-size:14px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
     User ASLType Relationship
    </div>
      <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="4">
                <tr>
                    <td>
                        Company Name:
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="lstCompany" runat="server" AutoPostBack="true" Width="205px" DataSourceID="ObjectDataSource_CompanyList" OnDataBound="lstCompany_DataBound"
                            DataTextField="company_name" DataValueField="id" OnSelectedIndexChanged="lstCompany_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ObjectDataSource_CompanyList" runat="server" SelectMethod="Get_CompanyListMini"
                            TypeName="SMS.Business.Infrastructure.BLL_Infra_Company">
                            <SelectParameters>
                                <asp:SessionParameter Name="UserCompany" SessionField="UserCompanyID" ConvertEmptyStringToNull="true"
                                    DefaultValue="0" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        Filter User:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchUser" runat="server" Width="200px" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td >
                        <div style="border: 1px solid #0489B1; background-color: #A9D0F5; padding: 2px;">
                          <table>
                                <tr>
                                    <td>
                                        Copy Access from User:
                                        <asp:DropDownList ID="ddlCopyFromUser" runat="server" Width="150px" AutoPostBack="false"
                                            DataTextField="USERNAME" DataValueField="USERID">
                                        </asp:DropDownList>
                                        To User:
                                    </td>
                                    <td>
                                        <ucDDL:ucCustomDropDownList ID="DDLUser" runat="server" UseInHeader="false" Height="150"
                                            Width="100" Style="float: left;" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAppendMode" runat="server" Width="120px" AutoPostBack="false"
                                            Style="float: left;">
                                            <asp:ListItem Value="0" Text="Remove Existing"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Retain Existing"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlCopyMenu" runat="server" Width="180px" AutoPostBack="false">
                                            <asp:ListItem Value="0" Text="All Type"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Selected Type" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="btnCopy" runat="server" Text="Copy Access" OnClick="btnCopy_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%;margin-top:5px;">
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc;width:20%;" >
                        <asp:ListBox ID="lstUserList" runat="server" Height="600px" Width="100%" AutoPostBack="true"
                            CssClass="listbox" SelectionMode="Single" DataSourceID="ObjectDataSource_Users"
                            DataTextField="USERNAME" DataValueField="USERID" OnSelectedIndexChanged="lstUserList_SelectedIndexChanged">
                        </asp:ListBox>
                        <asp:ObjectDataSource ID="ObjectDataSource_Users" runat="server" SelectMethod="Get_UserList"
                            TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lstCompany" DefaultValue="0" Name="CompanyID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="txtSearchUser" DefaultValue="" Name="FilterText"
                                    PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;width:25%;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                Width="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" BorderColor="#cccccc">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                    VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="20" DataKeyNames="VARIABLE_CODE" Width="100%" OnDataBound="GridView1_DataBound"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333"
                            GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                             <asp:TemplateField HeaderText="Type">
                                    <HeaderTemplate>
                                    Type
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("VARIABLE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                   Select All
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:CheckBox ID="chkAll" runat="server" ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                    View
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkView" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsView")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                    Add
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAdd" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsAdd")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                    Edit
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsEdit")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                    Delete
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsDelete")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                    Approve
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApprove" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsApprove")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="">
                                  <HeaderTemplate>
                                    Admin
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAdmin" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsAdmin")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pager" HorizontalAlign="Left" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <div style="border: 1px solid outset; text-align: right;">
                            <%--<asp:Button ID="btnInitialize" runat="server" Text="Initialize Access" OnClick="btnInitializeMenu_Click" />--%>
                            <asp:Button ID="btnResetMenu" runat="server" Text="Remove Access" OnClick="btnResetMenu_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
