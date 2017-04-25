<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="DocumentCheckList.aspx.cs"
    Inherits="DocumentCheckList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid outset; background-color: #B0C4DE; margin-top: 5px;
        padding: 2px; font-weight: bold; font-size: 12px;">
        Document Check List
    </div>
    <div id="divGrid" style="position: relative; z-index: 0; width: 100%;">
        <asp:GridView ID="gvCheckList" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
            OnRowDataBound="gvCheckList_RowDataBound" DataKeyNames="ID" CellPadding="3" GridLines="None"
            CellSpacing="0" Width="100%" OnSorting="gvCheckList_Sorting" AllowSorting="true"
            Font-Size="11px" CssClass="GridView-css">
            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
            <PagerStyle CssClass="PagerStyle-css" />
            <RowStyle CssClass="RowStyle-css" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
            <Columns>
                <asp:TemplateField HeaderText="Procedure Code">
                    <HeaderTemplate>
                        <asp:Label ID="lbtProCodeHeader" runat="server">Section No.</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbtProCode" runat="server" Text='<%#Eval("SECTION_NO")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Procedure Name">
                    <HeaderTemplate>
                        <asp:Label ID="lbtProNameHeader" runat="server">Issue No.</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbtProlName" runat="server" Text='<%#Eval("ISSUE_NO")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Publish Date">
                    <HeaderTemplate>
                        <asp:Label ID="lbtPublishDateHeader" runat="server">Deacription Of change</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublishDate" runat="server" Text='<%#Eval("CHANGE_DESCRIPTION")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Check Out Date">
                    <HeaderTemplate>
                        <asp:Label ID="lbtCheckOutHeader" runat="server">Date</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCheckOutDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CREATED_DATE","{0:dd/MMM/yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Publish Date">
                    <HeaderTemplate>
                        <asp:Label ID="lbtPublishDateHeader" runat="server">Issued By</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublishDate" runat="server" Text='<%#Eval("CREATED_BY_USER")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindPendingPublishDoc" />
                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
