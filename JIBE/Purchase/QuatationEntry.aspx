<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="QuatationEntry.aspx.cs"
    Inherits="Technical_INV_QuatationEntry" Title="Upload (Excel) Quotation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="contenthead" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr align="center">
                <td style="background-color: #808080; font-size: x-small; color: #FFFFFF;">
                    <b>Upload (Excel) Quotation</b>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" style="height: 2px">
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 1100px; background-color: #f4ffff;
            border-right: 1px solid gray; border-top: 1px solid gray">
            <tr>
                <td class="tdh">
                    Vessel :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblVessel" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Date :
                </td>
                <td class="tdd" colspan="3">
                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Catalogue :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Requisition Number :
                </td>
                <td class="tdd">
                    <asp:HyperLink ID="lblReqNo" Target="_blank" runat="server"> </asp:HyperLink>
                </td>
                <td class="tdh">
                    Total Item :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="background-color: #C0C0C0; width: 1100px">
            <tr>
                <td style="font-size: small; text-align: right; color: Black; font-weight: 600;">
                    Supplier :
                </td>
                <td style="background-color: #C0C0C0; text-align: left">
                    <asp:DropDownList ID="DDLSupplier" runat="server" Style="font-size: small" Width="250px"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td style="font-size: small; text-align: right; color: Black; font-weight: 600;">
                    Upload Excel file
                </td>
                <td style="text-align: left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Height="24px" Style="font-size: small" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Style="font-size: small"
                        OnClick="btnUpload_Click" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%-- <div style="overflow:scroll; margin-left: 0px; height:400px; width: 900px" >--%>
                <table border="0" cellpadding="0" cellspacing="0" style="height: 50px; width: 1100px;">
                    <tr align="left">
                        <td>
                            <telerik:RadGrid ID="rgdQuoUpload" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                Skin="Office2007" Width="1099px" AlternatingItemStyle-BackColor="#CEE3F6" Height="350px"
                                AutoGenerateColumns="False">
                                <MasterTableView>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ITEM_REF_CODE" HeaderText="Item ID" UniqueName="ITEM_REF_CODE">
                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SHORT_DESC" HeaderText="Short Desc" UniqueName="SHORT_DESC">
                                            <HeaderStyle Width="260px" HorizontalAlign="Center" />
                                            <ItemStyle Width="260px" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FULL_DESC" HeaderText="Long Desc" UniqueName="FULL_DESC"
                                            Visible="False">
                                            <HeaderStyle Width="160px" HorizontalAlign="Center" />
                                            <ItemStyle Width="160px" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Item_comments" HeaderText="Comments" UniqueName="Item_comments">
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Unit" HeaderText="Unit" UniqueName="Unit">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Request_Qty" HeaderText="Reqst. Qty." UniqueName="Request_Qty">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Unit_Price" HeaderText="Unit Price" UniqueName="Unit_Price">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Discount" HeaderText="Discount" UniqueName="Discount">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Leadtime" HeaderText="Lead Time&lt;/br&gt;(in days)"
                                            UniqueName="LeadTime">
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Total_Price" HeaderText="Total Price" UniqueName="Total_Price">
                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Supplier_Remarks" HeaderText="Remarks" UniqueName="Supplier_Remarks">
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Item_Type" HeaderText="Item Type" UniqueName="Item_Type">
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <PopUpSettings ScrollBars="Horizontal" />
                                    </EditFormSettings>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
                </div>
                <table border="0" cellpadding="0" cellspacing="0" style="height: 13px; width: 1100px;
                    background-color: #CCCCCC;">
                    <tr>
                        <td style="font-size: 11px; color: Black;" align="right">
                            <b>Currency: </b>&nbsp;<span style="color:Red">*</span>
                        </td>
                        <td style="font-size: x-small; width: 10%;" align="left">
                            <span style="font-size: small;">
                                <asp:TextBox ID="txtCurrency" runat="server" Enabled="false"></asp:TextBox>
                            </span>
                        </td>
                        <td style="font-size: 11px; color: Black;" align="right">
                            <b>Vat : </b>
                        </td>
                        <td style="width: 10%;" align="left">
                            <asp:TextBox ID="txtVat" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="font-size: 11px; color: Black;" align="right">
                            <b>&nbsp;Transportation/Freight Cost</b>&nbsp;<span style="color:Red">*</span>
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtTrsportCost" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 11px; color: Black;" align="right">
                            <b>Discount : </b>
                        </td>
                        <td style="font-size: small" align="left">
                            <asp:TextBox ID="txtDiscount" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                        <td style="font-size: 11px; font-weight: bold; color: Black;" align="right">
                            Other Charge
                        </td>
                        <td align="Left">
                            <asp:TextBox ID="txtAdditionlachrgs" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                        </td>
                        <td style="font-size: 11px; font-weight: bold; color: Black;" align="right">
                            Pkg and handing Charges :&nbsp;<span style="color:Red">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPkgCharges" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="font-size: 11px; color: Black; font-weight: bold">
                            Supplier Qtn Reference / Remarks :&nbsp;<span style="color:Red">*</span>
                        </td>
                        <td align="left" style="font-size: small;">
                            <asp:TextBox ID="txtRequi" runat="server" Enabled="false" Height="40px" Rows="3"
                                TextMode="MultiLine" Width="240px"></asp:TextBox>
                        </td>
                        <td align="right" style="font-size: 11px; color: Black; font-weight: bold">
                            Reason for other charge :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtReasonOther" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" style="font-size: 11px; color: Black; font-weight: bold">
                            Reason for Transportation and Pkg Charges :&nbsp;<span style="color:Red">*</span>
                        </td>
                        <td align="left" style="font-size: small;">
                            <asp:TextBox ID="txtReasonPKG" runat="server" Enabled="false" Height="40px" Rows="3"
                                TextMode="MultiLine" Width="163px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="font-size: 11px; color: Black; font-weight: bold">
                            Truck Charge :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTruck" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" style="font-size: 11px; color: Black; font-weight: bold">
                            Barge/Workboat Charge :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBarge" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            &nbsp;<br />
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Font-Size="12px"
                                Text="Save" Width="100px" Height="35px" />
                            &nbsp;<br />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="#FF3300" Style="font-size: small"></asp:Label>
    </center>
</asp:Content>
