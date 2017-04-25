<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    EnableEventValidation="false" CodeFile="FBM_Read_Access_Right.aspx.cs" Title="FBM Read Access Right"
    Inherits="QMS_FBM_Read_Access_Right" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <center>
        <div style="width: 900px">
            <div class="page-title">
                FBM READ ACCESS RIGHT ASSIGNMENT
            </div>
            <div style="height: 100%; vertical-align: middle; padding: 5px 0px 5px 0px">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table width="100%" style="color: Black;">
                            <tr>
                                <td align="right" style="width: 8%">
                                    Rank :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRank" runat="server" AppendDataBoundItems="true" Width="120px"
                                        Height="20px" Font-Size="11px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 8%">
                                    Category :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRankCategory" Width="120px" runat="server" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Search :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchBy" Width="180px" runat="server"></asp:TextBox>
                                </td>
                                <td align="center" style="width: 10%">
                                    <asp:Button ID="btnRetrieve" runat="server" Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Search" Width="80px" />
                                </td>
                                <td align="center" style="width: 10%">
                                    <asp:Button ID="btnClear" runat="server" Height="22px" OnClick="btnClear_Click" Text="Clear Filter"
                                        Width="80px" />
                                </td>
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="btnExport" ImageUrl="~/Images/XLS.jpg" Height="25px" runat="server"
                                        ToolTip="Export to Excel" OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="margin-top: 2px; cursor: pointer; height: 100%;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x: hidden; border: 0px solid gray; width: 100%">
                            <asp:GridView ID="gvFBMAcessRight" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" OnRowDataBound="gvFBMAcessRight_RowDataBound" Width="100%"
                                GridLines="Both" AllowSorting="true" OnSorting="gvFBMAcessRight_Sorting" CssClass="gridmain-css">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Rank Name">
                                        <HeaderTemplate>
                                            Rank Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("Rank_Name") %>
                                            <asp:Label ID="lblRankID" Visible="false" runat="server" Text='<%# Eval("Rank_ID") %>'></asp:Label>
                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblRankShortNameHeader" runat="server" ForeColor="Black" CommandArgument="Rank_Short_Name"
                                                CommandName="Sort">Short Name&nbsp;</asp:LinkButton>
                                            <img id="Rank_Short_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("Rank_Short_Name") %>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Read Access">
                                        <HeaderTemplate>
                                            Read Access
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAccess" runat="server" Height="16px" Checked='<%# Convert.ToBoolean(Eval("READ_ACCESS_FLAG")) %>'
                                                ForeColor="Black" AutoPostBack="true" OnCheckedChanged="chkAccess_CheckedChanged" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            OnClientClick="return confirm('Are you sure want to remove access right?')" CommandArgument='<%#Eval("[ID]")%>'
                                                            ForeColor="Black" ToolTip="Remove Access right" ImageUrl="~/Images/delete.png"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindFBMAccessRight" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
