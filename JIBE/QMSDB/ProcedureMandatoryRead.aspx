<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"    EnableEventValidation="false" CodeFile="ProcedureMandatoryRead.aspx.cs" Title=" "
    Inherits="ProcedureMandatoryRead" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
   
   
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
   
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 1100px;">
            <div style="border: 0px solid  #cccccc; padding: 0px; background-color: #5588BB;
                color: #FFFFFF; text-align: center;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 95%">
                            <b>Procedure Mandatory read Assignment</b>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #cccccc; height: 40px; vertical-align: middle; padding-top: 10px">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" width="100%" style="color: Black;">
                            <tr>
                                <td align="right" style="width: 8%">
                                    Rank :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRank" runat="server" AppendDataBoundItems="true" Width="120px"
                                        Height="20px" Font-Size="11px" CssClass="txtInput">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 8%">
                                    Category :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRankCategory" Width="120px" CssClass="txtInput" runat="server"
                                        AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Search :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchBy" Width="180px" runat="server" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="center" style="width: 10%">
                                    <asp:Button ID="btnRetrieve" runat="server" Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Retrieve" Width="80px" Font-Size="11px" />
                                </td>
                                <td align="center" style="width: 10%">
                                    <asp:Button ID="btnClear" runat="server" Height="22px" OnClick="btnClear_Click" Text="Clear Filter"
                                        Width="80px" Font-Size="11px" />
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
            <div style="border: 1px solid #cccccc; margin-top: 2px; cursor: pointer; height: 650px;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="0" style="border: 1px solid #dcdcdc; width: 100%;">
                            <tr style="width: 30px">
                                <td style="vertical-align: top; border: 1px solid #cccccc; width: 25%;">
                                    <div style="height: 100%; overflow: inherit; text-align: left;">
                                        <asp:TreeView ID="trvFolder" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                            Width="100%" BorderColor="#cccccc" OnSelectedNodeChanged="trvFolder_SelectedNodeChanged">
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
                                    <div style="overflow-x: hidden; border: 0px solid gray; width: 100%">
                                        <asp:GridView ID="gvProcedureRead" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" OnRowDataBound="gvProcedureRead_RowDataBound" Width="100%"
                                            GridLines="Both" AllowSorting="true" OnSorting="gvProcedureRead_Sorting">
                                            <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                            <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                            <AlternatingRowStyle Font-Size="12px" CssClass="PMSGridAlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Rank Name">
                                                    <HeaderTemplate>
                                                        Folder Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("FOLDER_NAME")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                                    <ItemStyle Wrap="true" HorizontalAlign="left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank Name">
                                                    <HeaderTemplate>
                                                        Rank Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Rank_Name") %>
                                                        <asp:Label ID="lblRankID" Visible="false" runat="server" Text='<%# Eval("Rank_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblFolderID" Visible="false" runat="server" Text='<%# Eval("Folder_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                                    <ItemStyle Wrap="true" HorizontalAlign="left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Short Name">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblRankShortNameHeader" runat="server" ForeColor="White" CommandArgument="Rank_Short_Name"
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
                                                                        OnClientClick="return confirm('Are you sure want to remove Mandatory Read?')" CommandArgument='<%#Eval("[Rank_ID]")+ ","+Eval("[Folder_ID]") %>'
                                                                        ForeColor="Black" ToolTip="Remove Mandatory Read" ImageUrl="~/Images/delete.png" Visible='<%#Convert.ToString(Eval("ID"))==""?false : true %>'
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
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindProcedureRead" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
