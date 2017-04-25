<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Item_History.aspx.cs" Inherits="Purchase_Item_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 0px; background-color: #F6CEE3; text-align: center; font-weight: bold">
        Item History
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829">
        <table width="100%" style="padding-bottom: 5px; padding-top: 5px">
            <tr>
                <td class="tdh">
                    Vessel Name :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblvesselname" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Catalogue :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblcataloguename" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Sub Catalogue :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblsubcataloguename" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Short Desc:
                </td>
                <td class="tdd" colspan="5">
                    <asp:Label ID="lblItemRefCode" Width="300px" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Long Desc :
                </td>
                <td class="tdd" colspan="5">
                    <asp:Label ID="lblLongDesc" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvItemHistory" AutoGenerateColumns="false" runat="server" CellPadding="4"
            Width="100%" BorderStyle="Solid" BorderColor="LightGray" BorderWidth="1px" CellSpacing="0"
            EmptyDataText="No record found!" GridLines="None" OnRowDataBound="gvItemHistory_RowDataBound">
            <HeaderStyle CssClass="HeaderStyle-css" />
            <RowStyle CssClass="RowStyle-css" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <Columns>
                <asp:TemplateField HeaderText="Reqsn/Invt Date" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblReqsnDate" Text='<%#Eval("Requisition_date")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reqsn No" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblReqsnNO" Text='<%#Eval("REQUISITION_CODE")%>' Width="100px" ToolTip='<%#Eval("DOCUMENT_CODE") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ROB" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Label ID="lblROB" Text='<%#Eval("ROB_Before")%>' Width="60px" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reqstd QTY" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <asp:Label ID="lblReqQty" Text='<%#Eval("REQUESTED_QTY")%>' Width="70px" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quoted Price">
                    <ItemTemplate>
                        <asp:DataList ID="rptQuoted" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0"
                            RepeatLayout="Table" runat="server" OnItemDataBound="rptQuoted_ItemDataBound">
                            <HeaderTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="border: 0px solid white; font-size: 10px; width: 100px; text-align: center">
                                        <asp:Label ID="lblsupp" runat="server" Text='<%# Eval("Full_NAME")%>'> </asp:Label>
                                    </td>
                                    <td style="text-align: center; font-weight: bold; font-size: 9px; border: 0px  solid white;">
                                        <%#Eval("QUOTED_RATE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:DataList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ordered Supplier" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblORDSupp" Text='<%#Eval("Full_NAME")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ordered Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQty" Text='<%#Eval("ORDER_QTY")%>' Width="70px" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delivered Qty" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="70px">
                    <ItemTemplate>
                        <asp:Label ID="lblDelivQty" Text='<%#Eval("DELIVERD_QTY")%>' Width="70px" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ROB(aft delv)" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblROBAfter" Text='<%#Eval("ROB_After")%>' Width="80px" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delivery Date" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDelvDate" Text='<%#Eval("ITEM_DELIVERY_DATE")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" Text='<%#Eval("ORDER_UNIT_ID")%>' Width="80px" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Type" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblItem" Text='<%#Eval("ItemType")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Comment" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblItemComment" Text='<%#Eval("ITEM_COMMENT")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
