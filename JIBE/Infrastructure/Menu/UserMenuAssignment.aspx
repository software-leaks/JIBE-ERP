<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UserMenuAssignment.aspx.cs" Inherits="Infrastructure_Menu_UserMenuAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="error-message">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
            <table border="0" style="width: 100%">
                <tr style="background-color: #d8d8d8">
                    <td>
                        Company Name:
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="lstCompany" runat="server" AutoPostBack="true" DataSourceID="ObjectDataSource_CompanyList" OnDataBound="lstCompany_DataBound"
                            DataTextField="company_name" DataValueField="id">
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
                    <td style="vertical-align: top; text-align: center; width: 160px;">
                        Select User
                    </td>
                    <td style="vertical-align: top; text-align: center; width: 160px;">
                        Select Module
                    </td>
                    <td style="vertical-align: top; text-align: center">
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSearchUser" runat="server" Width="150px" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddlMainMenu" runat="server" Width="150px" AutoPostBack="false"
                                DataSourceID="ObjectDataSource_Users" DataTextField="USER_NAME" DataValueField="USERID">
                            </asp:DropDownList>--%>  
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: center">
                        <asp:ListBox ID="lstUserList" runat="server" Height="400px" Width="150px" AutoPostBack="true"
                            SelectionMode="Single" DataSourceID="ObjectDataSource_Users" DataTextField="USERNAME"
                            DataValueField="USERID"></asp:ListBox>
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
                    <td style="vertical-align: top; text-align: center">
                        <asp:ListBox ID="lstModuleList" runat="server" Height="400px" Width="150px" SelectionMode="Single"
                            AutoPostBack="True" DataSourceID="ObjectDataSource_SubModules" DataTextField="Menu_Short_Discription"
                            DataValueField="Mod_Code"></asp:ListBox>
                        <asp:ObjectDataSource ID="ObjectDataSource_SubModules" runat="server" SelectMethod="GetCollection_AllModules"
                            TypeName="SMS.Business.Infrastructure.BLL_Infra_MenuManagement"></asp:ObjectDataSource>
                    </td>
                    <td style="vertical-align: top; text-align: left">
                        <div style="border: 1px solid outset;">
                            Copy Access from User:
                            <asp:DropDownList ID="ddlCopyFromUser" runat="server" Width="150px" AutoPostBack="false"
                                DataSourceID="ObjectDataSource_Users" DataTextField="USER_NAME" DataValueField="USERID">
                            </asp:DropDownList>  
                            <asp:DropDownList ID="ddlAppendMode" runat="server" Width="150px" AutoPostBack="false">
                                <asp:ListItem Value="0" Text="Remove Existing"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Retain Existing"></asp:ListItem>
                            </asp:DropDownList>                            
                            <asp:Button ID="btnCopy" runat="server" Text="Copy Access" OnClick="btnCopy_Click" />
                            
                        </div>
                        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="30" DataKeyNames="Menu_Code" Width="100%" OnDataBound="GridView1_DataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Menu">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("Menu_Short_Discription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Link">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLink" runat="server" Text='<%#Eval("Menu_Link") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMenu" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_menu")) %>'
                                            Text='<%# Eval("menu_code") %>' ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkView" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_view")) %>'
                                            Text='<%# Eval("menu_code") %>' ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAdd" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_add")) %>'
                                            Text='<%# Eval("menu_code") %>' ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_edit")) %>'
                                            Text='<%# Eval("menu_code") %>' ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_delete")) %>'
                                            Text='<%# Eval("menu_code") %>' ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApprove" runat="server" Checked='<%# Convert.ToBoolean(Eval("access_approve")) %>'
                                            Text='<%# Eval("menu_code") %>' ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="Select All">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" Text='<%# Eval("menu_code") %>' ForeColor="white" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoRec" runat="server" Text="Click on Initialize Access to initialize menu"></asp:Label>
                                <asp:Button ID="btnInitializeMenu" runat="server" OnClick="btnInitializeMenu_Click"
                                    Text="Initialize menu" />
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_UserMenuAccess"
                            TypeName="SMS.Business.Infrastructure.BLL_Infra_MenuManagement">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lstModuleList" Name="mod_code" PropertyName="SelectedValue"
                                    DefaultValue="0" Type="Int32" />
                                <asp:ControlParameter ControlID="lstUserList" Name="userid" PropertyName="SelectedValue"
                                    DefaultValue="0" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <div style="border: 1px solid outset; text-align: right;">
                            <asp:Button ID="btnInitialize" runat="server" Text="Initialize Access" OnClick="btnInitializeMenu_Click" />
                            <asp:Button ID="btnResetMenu" runat="server" Text="Remove Access" OnClick="btnResetMenu_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
