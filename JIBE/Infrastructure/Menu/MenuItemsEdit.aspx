<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuItemsEdit.aspx.cs"
    Inherits="Infrastructure_USModule_MenuItemsEdit" Title="Menu Management" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   
    <style type="text/css">
        .style1
        {
            width: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updtpnl_menuedits" runat="server">
        <ContentTemplate>
            <div style="border: 1px solid #cccccc" class="page-title">
                Create Menu
            </div>
            <div id="dvpage-content" class="page-content-main" style="padding: 10px">
                <table cellpadding="2" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 16%;">
                            <asp:TextBox ID="txt_module" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                        </td>
                        <td style="width: 4%">
                            <asp:TextBox ID="txt_module_seq" runat="server" Width="99%" Enabled="false" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td style="width: 1%">
                            <asp:ImageButton ID="imgBtnAddMod" runat="server" ImageUrl="~/images/Add-icon.png"
                                OnClick="imgBtnAddMod_Click" />
                        </td>
                        <td align="center" style="width: 1%">
                            <asp:ImageButton ID="imgbtn_mod_save" runat="server" ImageUrl="~/images/Save-icon.png"
                                OnClick="imgbtn_mod_save_Click" />
                        </td>
                        <td align="center" style="width: 1%">
                            <asp:ImageButton ID="imgbtn_moddelete" runat="server" ImageUrl="~/images/Delete-icon.png"
                                OnClick="imgbtn_moddelete_Click" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                        </td>
                        <td align="center" style="width: 2%">
                        </td>
                        <td style="width: 16%">
                            <asp:TextBox ID="txt_submodule" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                        </td>
                        <td style="width: 4%">
                            <asp:TextBox ID="txt_submodule_seq" runat="server" Width="100%" Enabled="false" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td style="width: 1%">
                            <asp:ImageButton ID="imgBtnAddSubMod" runat="server" ImageUrl="~/images/Add-icon.png"
                                OnClick="imgBtnAddSubMod_Click" Style="height: 22px" />
                        </td>
                        <td style="width: 1%">
                            <asp:ImageButton ID="imgbtn_submod_save" runat="server" ImageUrl="~/images/Save-icon.png"
                                OnClick="imgbtn_submod_save_Click" />
                        </td>
                        <td style="width: 1%">
                            <asp:ImageButton ID="imgbtn_submoddelete" runat="server" ImageUrl="~/images/Delete-icon.png"
                                OnClick="imgbtn_submoddelete_Click" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                        </td>
                        <td align="center" style="width: 1%">
                        </td>
                        <td style="width: 16%">
                            <asp:TextBox ID="txt_link" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                        </td>
                        <td style="width: 4%">
                            <asp:TextBox ID="txt_link_seq" runat="server" Width="100%" Enabled="false" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td style="width: 1%">
                            <asp:ImageButton ID="imgBtnAddLink" runat="server" ImageUrl="~/images/Add-icon.png"
                                OnClick="imgBtnAddLink_Click" />
                        </td>
                        <td style="width: 1%">
                            <asp:ImageButton ID="imgbtn_link_save" runat="server" ImageUrl="~/images/Save-icon.png"
                                OnClick="imgbtn_link_save_Click" />
                        </td>
                        <td style="width: 1%">
                            <asp:ImageButton ID="imgbtn_linkdelete" runat="server" ImageUrl="~/images/Delete-icon.png"
                                OnClick="imgbtn_linkdelete_Click" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                        </td>
                        <td align="center" style="width: 1%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" valign="top">
                            <asp:ListBox ID="lst_module" runat="server" AutoPostBack="true" Height="360px" OnSelectedIndexChanged="lst_module_SelectedIndexChanged"
                                Width="100%" Style="border: 1px solid #cccccc"></asp:ListBox>
                        </td>
                        <td align="center" style="width: 1%;">
                            <div style="padding: 2px 2px 2px 2px;">
                                <asp:ImageButton ID="img_mod_up_sorting" runat="server" ImageUrl="~/images/up.png"
                                    OnClick="img_mod_up_sorting_Click" />
                                <div style="padding: 2px 2px 2px 2px;">
                                    <div>
                                        <asp:ImageButton ID="img_mod_down_sotring" runat="server" ImageUrl="~/images/down.png"
                                            OnClick="img_mod_down_sotring_Click" />
                                    </div>
                        </td>
                        <td colspan="5" valign="top">
                            <asp:ListBox ID="lst_submodule" runat="server" AutoPostBack="True" Height="360px"
                                OnSelectedIndexChanged="lst_submodule_SelectedIndexChanged" Width="100%" Style="border: 1px solid #cccccc">
                            </asp:ListBox>
                        </td>
                        <td align="center" style="width: 1%">
                            <div style="padding: 2px 2px 2px 2px;">
                                <asp:ImageButton ID="img_submod_up" runat="server" ImageUrl="~/images/up.png" OnClick="img_submod_up_Click" />
                            </div>
                            <div style="padding: 2px 2px 2px 2px;">
                                <asp:ImageButton ID="img_submod_down" runat="server" ImageUrl="~/images/down.png"
                                    OnClick="img_submod_down_Click" />
                            </div>
                        </td>
                        <td colspan="5" valign="top">
                            <asp:ListBox ID="lst_links" runat="server" AutoPostBack="True" Height="360px" OnSelectedIndexChanged="lst_links_SelectedIndexChanged"
                                Width="100%" Style="border: 1px solid #cccccc"></asp:ListBox>
                        </td>
                        <td align="center" style="width: 1%">
                            <div style="padding: 2px 2px 2px 2px;">
                                <asp:ImageButton ID="img_link_up" runat="server" ImageUrl="~/images/up.png" OnClick="img_link_up_Click" />
                            </div>
                            <div style="padding: 2px 2px 2px 2px;">
                                <asp:ImageButton ID="img_link_down" runat="server" ImageUrl="~/images/down.png" OnClick="img_link_down_Click" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSubModuleUrl0" runat="server" CssClass="txtInput" Width="99%"></asp:TextBox>
                        </td>
                        <td colspan="1" align="right">
                            Dept :&nbsp;
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="100px" AppendDataBoundItems="true"
                                CssClass="txtInput">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubModuleUrl" runat="server" CssClass="txtInput" Width="99%"></asp:TextBox>
                        </td>
                        <td colspan="4">
                            <%--  <asp:DropDownList ID="ddlSPMModuleID_01" runat="server" CssClass="txtInput" Width="99%">
                            </asp:DropDownList>--%>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUrl" runat="server" CssClass="txtInput" Width="99%"></asp:TextBox>
                        </td>
                        <td colspan="4">
                            <%-- <asp:DropDownList ID="ddlSPMModuleID_02" runat="server" CssClass="txtInput" Width="99%">
                            </asp:DropDownList>--%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkModule" runat="server" Text="Menu Enable" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSuModule" runat="server" Text="Menu Enable" />
                        </td>
                        <td colspan="4">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                            <asp:CheckBox ID="chkLink" runat="server" Text="Menu Enable" />
                        </td>
                        <td colspan="4">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table border="0">
                    <tr>
                        <td valign="top" style="margin-top:20px;">
                            <a href="http://fontawesome.io/cheatsheet/" onclick="return !window.open(this.href, 'Google', 'width=500,height=500')"  target="_blank">Add Fontawesome font</a><br />
                            <asp:TextBox runat="server" CssClass="txtInput" ID="txtFontClass"></asp:TextBox>
                        </td>
                        <td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <td>
                                                    <td class="style1">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                                                    ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                                                    Width="100%" AutoGenerateColumns="False" AllowMultiRowSelection="True" PageSize="100"
                                                                    TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
                                                                    OnItemDataBound="RadGrid1_ItemDataBound">
                                                                    <MasterTableView>
                                                                        <RowIndicatorColumn Visible="true">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                        </RowIndicatorColumn>
                                                                        <%--   <ExpandCollapseColumn Resizable="False" Visible="False">
                                                            <HeaderStyle Width="20px" />
                                                        </ExpandCollapseColumn>--%>
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Key" Visible="true"
                                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblKey" Style="border: 0; height: 14px" ForeColor="Black" Text='<%#Eval("Key_Name")%>'
                                                                                        runat="server" />
                                                                                    <asp:HiddenField ID="hdnTPId" runat="server" Value='<%#Eval("Key_Id")%>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Description" UniqueName="SortOrder"
                                                                                Visible="true" ItemStyle-Width="10px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDescription" runat="server" Width="95%" MaxLength="1000" Text='<%#Eval("Description") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="25%" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Check" ItemStyle-Width="10px"
                                                                                UniqueName="SortOrder" Visible="true" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Text="All" OnCheckedChanged="chkAll_CheckedChanged" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkBox" runat="server" Checked='<%#  Convert.ToBoolean( Eval("Key_enabled")) %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="10%" />
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                        <EditFormSettings>
                                                                            <PopUpSettings ScrollBars="None" />
                                                                            <PopUpSettings ScrollBars="None" />
                                                                        </EditFormSettings>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                            <td colspan="4">
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
