<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_Purc_Ctp_AddItem.ascx.cs"
    Inherits="UserControl_Ctp_AddItem" %>
<%@ Register Src="ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<style type="text/css">
    
</style>
<link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />

<table width="100%" border="1" style="border-collapse: collapse" cellpadding="0"
    cellspacing="0">
    <tr>
        <td align="right" style="width: 175px; font-size: 12px; font-weight: bold; padding-right: 3px">
            Department :
        </td>
        <td align="left" style="font-size: 11px; padding-left: 3px">
            <asp:Label ID="lblDepartmentName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 175px; font-size: 12px; font-weight: bold; padding-right: 3px">
            Catalogue :
        </td>
        <td align="left" style="font-size: 11px; padding-left: 3px">
            <asp:Label ID="lblCatalogue" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" style="font-size:12px; padding-right: 3px; font-weight: bold">
            Item description/part no. :
        </td>
        <td style="text-align: left; font-size: 11px; padding: 3px">
            <asp:TextBox ID="txtItemSearch" runat="server" Width="200px"></asp:TextBox>
            <asp:Button ID="btnItemSearch" Text="Search" runat="server" OnClick="txtItemSearch_TextChanged"
                Font-Size="11px" Font-Names="verdana" />
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="updadditem_main" runat="server" UpdateMode="Conditional" RenderMode="Block">
    <ContentTemplate>
        <table width="100%" border="1" style="border-collapse: collapse" cellpadding="2"
            cellspacing="0">
            <tr>
                <td rowspan="3" style="width: 20%; padding-top: 5px;" valign="top" align="left">
                    <span style="font-weight: bold;font-size:12px">Sub Catalogue </span>:<br />
                    <asp:GridView ID="gvSubCatalogue" runat="server" AutoGenerateColumns="False" Width="100%"
                        CellPadding="2" GridLines="Horizontal" EmptyDataText="No record found !" OnSelectedIndexChanging="gvSubCatalogue_SelectedIndexChanging"
                        DataKeyNames="SUBSYSTEM_CODE">
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" />
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblSubCatalogueName" CommandName="Select" runat="server" Text='<%# Eval("Subsystem_Description") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td style="width: 100%" valign="top">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left" style="background: #E6E6E6; font-weight: bold;font-size:12px">
                                UnSelected Item
                            </td>
                            <td align="right" style="background: #E6E6E6">
                                <asp:Button ID="btnselectAll_SubCatalogue" Text="Select Sub Catalogue" runat="server"
                                    OnCommand="rbtnselect_SelectedIndexChanged" CommandArgument="1" OnClick="btnselectAll_SubCatalogue_Click" />
                                <asp:Button ID="btnSelect_All" Text="Select All" runat="server" OnCommand="rbtnselect_SelectedIndexChanged"
                                    CommandArgument="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvItem_UnSelected" runat="server" EmptyDataText="No record found !"
                                    AutoGenerateColumns="False" Width="100%" DataKeyNames="id" GridLines="None">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Drawing No." DataField="Drawing_Number" />
                                        <asp:BoundField HeaderText="Part no." DataField="Part_Number" />
                                        <asp:BoundField HeaderText="Item Name" DataField="Short_Description" />
                                        <asp:BoundField HeaderText="Item Name" DataField="Long_Description" />
                                        <asp:BoundField HeaderText="Unit" DataField="Unit_and_Packings" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnselect" runat="server" Text="Select" Font-Size="11px" OnCommand="btnSelect_Click"
                                                    CommandArgument='<%#Eval("id") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:ucCustomPager ID="ucCustomPagerItem_UnSelected" runat="server" AlwaysGetRecordsCount="true"
                                    OnBindDataItem="BindData_UnSelected" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100%" valign="top">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left" style="background: #E6E6E6; font-weight: bold;font-size:12px">
                                Selected Item
                            </td>
                            <td align="right" style="background: #E6E6E6">
                                <asp:Button ID="btnDeSElctAll_SubCatalogue" Text="DeSelect sub catalogue" runat="server"
                                    OnCommand="rbtn_UnSelect_SelectedIndexChanged" CommandArgument="1" OnClick="btnDeSElctAll_SubCatalogue_Click" />
                                <asp:Button ID="btnDeSelectAll" Text="DeSelect All" runat="server" OnCommand="rbtn_UnSelect_SelectedIndexChanged"
                                    CommandArgument="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblSelectedSubCatalogue" Font-Size="11px" runat="server"></asp:Label>
                                <asp:GridView ID="gvItem_Selected" runat="server" EmptyDataText="No record found !"
                                    AutoGenerateColumns="False" Width="100%" DataKeyNames="id" GridLines="None">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css"  />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Drawing No." DataField="Drawing_Number" />
                                        <asp:BoundField HeaderText="Part no." DataField="Part_Number" />
                                        <asp:BoundField HeaderText="Item Name" DataField="Short_Description" />
                                        <asp:BoundField HeaderText="Item Name" DataField="Long_Description" />
                                        <asp:BoundField HeaderText="Unit" DataField="Unit_and_Packings" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnUnselect" runat="server" Text="DeSelect" Font-Size="11px" OnCommand="btnUnSelect_Click"
                                                    CommandArgument='<%#Eval("id") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:ucCustomPager ID="ucCustomPagerItem_Selected" runat="server" AlwaysGetRecordsCount="true"
                                    OnBindDataItem="BindData_Selected" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdf_catalogue_Code" runat="server" />
        <asp:HiddenField ID="hdf_Dept_ID" runat="server" />
        <asp:HiddenField ID="hdf_Contract_ID" runat="server" Value="-1" />
        <asp:HiddenField ID="hdf_SubCatalogue" runat="server" />
        <asp:HiddenField ID="hdf_AddItems_Status" Value="false" runat="server" />
        <asp:HiddenField ID="hdf_Is_Reset_Values" Value="false" runat="server" />
        <asp:HiddenField ID="hdf_Quotation_ID" Value="-1" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<table width="100%">
    <tr>
        <td align="right" style="width: 100%">
            <asp:Button ID="btnSaveItems" runat="server" Text="Save Selected Items" OnClick="btnSaveItems_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        </td>
    </tr>
</table>
