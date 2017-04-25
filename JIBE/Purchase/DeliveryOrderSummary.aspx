<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DeliveryOrderSummary.aspx.cs"
    Inherits="Technical_INV_DeliveryOrderSummary" Title="Delivery Items Summary" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="contehead" ContentPlaceHolderID="HeadContent" runat="server">
   
    <style type="text/css">
        .Table td
        {
            border-bottom: 1px solid gray;
            border-right: 1px solid gray;
            padding: 3px 3px 3px 3px;
        }
        .TableHead
        {
            background-color: #A9D0F5;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <table cellpadding="0" cellspacing="0" style="height: 16px; width: 100%; background-color: #C0C0C0;">
            <tr>
                <td style="background-color: #808080; font-size: small; color: #FFFFFF;" width="95%">
                    <b>Delivery Items Summary</b>
                </td>
                <td style="width: 5%">
                    <asp:Button ID="btnPrint" runat="server" Visible="false" Text="Print" Width="50px"
                        Style="font-size: small" OnClick="btnPrint_Click" />
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" valign="top" style="width: 50%">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="Requisition Details" Style="font-size: small"
                        Width="100%">
                        <table cellpadding="0" cellspacing="0" align="left" width="100%" class="Table" style="border: 1px solid gray;">
                            <tr align="left">
                                <td style="font-size: small; text-align: right;">
                                    <b>Vessel Name: </b>
                                </td>
                                <td style="font-size: small;">
                                    <asp:Label ID="lblVessel" runat="server"></asp:Label>
                                </td>
                                <td style="font-size: small; text-align: right;">
                                    <b>Req. Number: </b>
                                </td>
                                <td style="font-size: small;">
                                    <asp:Label ID="lblReqNo" runat="server"></asp:Label>
                                </td>
                                <td style="font-size: small; text-align: right;">
                                    <b>Date: </b>
                                </td>
                                <td style="font-size: small;">
                                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: small; text-align: right;">
                                    <b>Total Item: </b>
                                </td>
                                <td style="font-size: small;">
                                    <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                                </td>
                                <td style="font-size: small; text-align: right">
                                    <b>Catalogue: </b>
                                </td>
                                <td style="font-size: small;" colspan="3">
                                    <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                            <td style="font-size:small;text-align:right">
                            <b>Delivery Port</b>
                            </td>
                            <td style="font-size: small;"><asp:Label ID="lblDelPort" ClientIDMode="Static" runat="server"></asp:Label></td>
                            <td style="font-size:small;text-align:right"><b>Delivery Date</b></td>
                            <td style="font-size: small;"colspan="3"><asp:Label ID="lblDelDate" ClientIDMode="Static" runat="server"></asp:Label></td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: small; text-align: right;">
                                    <b>Reason for Req :</b>
                                </td>
                                <td style="font-size: small;" colspan="5">
                                    <asp:TextBox ID="txtComments" runat="server" BorderStyle="None" BorderWidth="0px"
                                        TextMode="MultiLine" Height="30px" ReadOnly="true" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    <asp:Panel ID="Panel4" runat="server" GroupingText="Attached Files" Style="font-size: small"
                        Width="100%">
                        <asp:Repeater ID="rpAttachment" runat="server" OnItemDataBound="rpAttachment_ItemDataBound">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <%# Eval("SlNo") %>.
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkAtt" runat="server" NavigateUrl='<%# Eval("File_Path")%>'> <%# Eval("File_Name")%>  </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    <asp:Panel ID="Panel3" runat="server" GroupingText="Delivered Items" Style="font-size: small"
                        Width="100%">
                        <div style="width: 100%; left: 0px; top: 0px; overflow: scroll">
                            <asp:GridView ID="gvItemsSupp" AutoGenerateColumns="false" runat="server" Width="98%">
                                <Columns>
                                    <asp:BoundField DataField="Full_NAME" HeaderText="Supplier Name" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Currency" HeaderText="Currency" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Packing_Handling_Charges" HeaderText="PkgHld Cost" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Freight_Cost" HeaderText="Freight Cost" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Other_Charges" HeaderText="Other Cost" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Truck_Cost" HeaderText="Truck Cost" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Barge_Workboat_Cost" HeaderText="Barge Workboat Cost"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Center" />
                            </asp:GridView>
                            <br />
                            <telerik:RadGrid ID="rgdItems" runat="server" Width="98%" AutoGenerateColumns="False"
                                GridLines="None" Skin="WebBlue" onitemdatabound="rgdItems_ItemDataBound">
                                <MasterTableView>
                                    <RowIndicatorColumn Visible="False" ItemStyle-BackColor="AliceBlue">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn SortExpression="Drawing_Number" HeaderStyle-Width="5%" HeaderText="Drawing No."
                                            DataField="Drawing_Number" UniqueName="Drawing_Number">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="Part_Number" HeaderStyle-Width="10%" HeaderText="Part No."
                                            DataField="Part_Number" UniqueName="Part_Number">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="Short_Description" HeaderText="Item Name"
                                            HeaderStyle-Width="20%" DataField="Short_Description" UniqueName="Short_Description">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="Unit_and_Packings" HeaderText="Unit" DataField="Unit_and_Packings"
                                            HeaderStyle-Width="10%" UniqueName="Unit_and_Packings">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ORDER_RATE" HeaderText="Order Rate" UniqueName="ORDER_RATE"
                                            HeaderStyle-Width="5%" DataFormatString="{0:F2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ORDER_DISCOUNT" HeaderText="Order Discount%"
                                            HeaderStyle-Width="5%" UniqueName="ORDER_DISCOUNT" DataFormatString="{0:F2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ORDER_PRICE" HeaderText="Order Price" Display="false" UniqueName="ORDER_PRICE"
                                            HeaderStyle-Width="5%" DataFormatString="{0:F2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ORDER_VAT" HeaderText="Order Vat%" UniqueName="ORDER_VAT"
                                            HeaderStyle-Width="5%" DataFormatString="{0:F2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="REQUESTED_QTY" UniqueName="REQUESTED_QTY" HeaderStyle-Width="5%" HeaderText="Requested Qty"> 
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="ORDER_QTY" HeaderText="Order Qty." HeaderStyle-Width="5%"
                                            UniqueName="ORDER_QTY">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DELIVERD_QTY" HeaderText="Delivered Qty." UniqueName="DELIVERD_QTY"
                                            HeaderStyle-Width="5%" DataFormatString="{0:F2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="Item_delivery_Remarks" HeaderText="Delivered Remarks" UniqueName="Item_delivery_Remarks"
                                            HeaderStyle-Width="10%" >
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <PopUpSettings ScrollBars="None" />
                                    </EditFormSettings>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
