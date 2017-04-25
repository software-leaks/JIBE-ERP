<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="QuotationSummary.aspx.cs"
    Inherits="Technical_INV_QuotationSummary" Title="Quotation Summary" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>
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
        .hide
        {
            visibility: hidden;
        }
        .clear
        {
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <eo:ASPXToPDF runat="server" ID="ASPXToPDF1">
    </eo:ASPXToPDF>
    <div id="divMain" style="padding: 10px;">
        <table cellpadding="0" cellspacing="0" style="height: 16px; width: 100%; background-color: #C0C0C0;">
            <tr>
                <td style="background-color: #808080; font-size: small; color: #FFFFFF; padding: 5px;">
                    <b>Quotation Summary</b>
                </td>
                <td align="left" style="background-color: #808080; font-weight: bold; height: 15px;
                    padding: 5px; width: 5%; font-size: 13px">
                    <asp:Button ID="btnExporttoPDF" runat="server" Text="Print" OnClick="btnExporttoPDF_Click" />
                </td>
            </tr>
        </table>
        <div class="clear">
        </div>
        <table cellpadding="0" cellspacing="0" width="100%" style="margin-top: 1%;">
            <tr>
                <td align="left" valign="top" style="width: 50%">
                    <div id="divRD" runat="server" style="font-size: small" width="100%">
                        <asp:Label ID="lblRD" Text="Requisition Details" runat="server" Style="font-size: small;
                            font-weight: bold" />
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
                                <td style="font-size: small; text-align: right;">
                                    <b>Reason for Req :</b>
                                </td>
                                <td style="font-size: small;" colspan="5">
                                    <asp:TextBox ID="txtComments" runat="server" BorderStyle="None" BorderWidth="0px"
                                        TextMode="MultiLine" Height="30px" ReadOnly="true" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%-- </asp:Panel>--%>
                </td>
            </tr>
        </table>
        <div class="clear">
        </div>
        <table align="center" cellpadding="0" cellspacing="0" style="width: 100%; margin-top: 1%;">
            <tr>
                <td align="left" style="width: 49%; vertical-align: top">
                    <div id="divRFQSend" runat="server" width="100%">
                        <asp:Label ID="lblRFQSent" Text="RFQ Sent To" runat="server" Style="font-size: small;
                            font-weight: bold" /><br />
                        <asp:ListBox ID="lstRFQSend" runat="server" Width="384px" Style="font-size: small;
                            margin-top: 0px" Height="20px" Visible="False"></asp:ListBox>
                        <asp:Repeater ID="RepeaterRfqSent" runat="server">
                            <HeaderTemplate>
                                <table class="Table" style="border-left: 1px solid gray; border-top: 1px solid gray"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td style="font-weight: bold; width: 10%" class="TableHead">
                                            Supplier Code
                                        </td>
                                        <td style="font-weight: bold; width: 40%" class="TableHead">
                                            Supplier Name
                                        </td>
                                        <td style="font-weight: bold; width: 10%" class="TableHead">
                                            Quoted Currency
                                        </td>
                                        <td runat="server" style="font-weight: bold; width: 10%" class="TableHead" id="thViewQuotationSent">
                                            View Quotation
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#DataBinder.Eval(Container, "DataItem.SUPPLIER")%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container, "DataItem.QUOTED_CURRENCY")%>
                                    </td>
                                    <td align="center" id="tdViewQuotationSent" runat="server">
                                        <asp:ImageButton ImageUrl="~/Purchase/Image/view.gif" runat="server" ID="imgViewQuotationSent"
                                            CommandName="pic_display" CommandArgument=' <%#DataBinder.Eval(Container, "DataItem.SUPPLIER")+"~"+DataBinder.Eval(Container, "DataItem.QUOTATION_CODE")+"~"+DataBinder.Eval(Container, "DataItem.SHORT_NAME")%> '
                                            OnCommand="onSelectRFQSent" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </td>
                <td width="2%">
                    &nbsp;
                </td>
                <td align="left" style="width: 49%; vertical-align: top">
                    <div id="divQRF" runat="server" width="100%">
                        <asp:Label ID="lblQRF" Text="Quotation Received From" runat="server" Style="font-size: small;
                            font-weight: bold" /><br />
                        <asp:ListBox ID="lstQuotRecvd" runat="server" Width="384px" Style="font-size: small;
                            margin-top: 0px" Height="20px" Visible="False"></asp:ListBox>
                        <asp:Repeater ID="RepeaterQtnRcv" runat="server" OnItemDataBound="RepeaterQtnRcv_ItemDataBound">
                            <HeaderTemplate>
                                <table class="Table" style="border-left: 1px solid gray; border-top: 1px solid gray"
                                    cellspacing="0" width="100%">
                                    <tr>
                                        <td style="font-weight: bold; width: 10%" class="TableHead">
                                            Supplier Code
                                        </td>
                                        <td style="font-weight: bold; width: 40%" class="TableHead">
                                            Supplier Name
                                        </td>
                                        <td style="font-weight: bold; width: 10%" class="TableHead">
                                            Quoted Currency
                                        </td>
                                        <td id="thViewQuotation" runat="server" style="font-weight: bold; width: 10%" class="TableHead">
                                            View Quotation
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr runat="server" id="trHeader">
                                    <td>
                                        <%#DataBinder.Eval(Container, "DataItem.SUPPLIER")%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container, "DataItem.QUOTED_CURRENCY")%>
                                    </td>
                                    <td align="center" id="tdViewQuotation" runat="server">
                                        <asp:ImageButton runat="server" ID="imgBtnViewQtnRFQ" OnCommand="onSelectQtnRcv"
                                            CommandArgument=' <%#DataBinder.Eval(Container, "DataItem.SUPPLIER")+"~"+DataBinder.Eval(Container, "DataItem.QUOTATION_CODE")+"~"+DataBinder.Eval(Container, "DataItem.SHORT_NAME")%> '
                                            CommandName="pic_display" ImageUrl="~/Purchase/Image/view.gif" />
                                    </td>
                                    <td id="tdOrderLabel" runat="server">
                                        <asp:Label ID="OrderLabel" Text='<%#DataBinder.Eval(Container, "DataItem.ORDER_CODE")%>'
                                            Style="display: none" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="3">
                    <div id="divQuotationDetails" runat="server" style="margin-top: 1%;" width="100%">
                        <asp:Label ID="lblQD" Text="Quotation Details" runat="server" Style="font-size: small; font-weight: bold" />
                        <table width="100%" style="border: 1px solid gray;" class="Table" cellpadding="0" cellspacing="0" id="tblQuotationDetails"  runat="server" visible="false">
                            <tr>
                                <td align="left" style="font-size: small" width="20%">
                                    Supplier Quotation Reference:
                                </td>
                                <td align="left" style="font-size: small" width="30%">
                                    <asp:Label ID="lblSQR" runat="server"></asp:Label>
                                </td>
                                <td align="left" style="font-size: small" width="20%">
                                    Reason for Pkg & Handling Charges:
                                </td>
                                <td align="left" style="font-size: small" width="30%">
                                    <asp:Label ID="lblReasonPHC" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                </td>
                                <td width="30%">
                                </td>
                                <td align="left" style="font-size: small" width="20%">
                                    Reason for other charges:
                                </td>
                                <td align="left" style="font-size: small" width="30%">
                                    <asp:Label ID="lblROC" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="font-size: small" width="20%">
                                    Supplier Remarks:
                                </td>
                                <td align="left" style="font-size: small" width="30%">
                                    <asp:Label ID="lblSRemark" runat="server"></asp:Label>
                                </td>
                                <td width="20%">
                                </td>
                                <td width="30%">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="3">
                    <div id="divSystemParticulars" runat="server" style="margin-top: 1%;" width="100%"
                        visible="false">
                        <table width="100%" style="border: 1px solid gray;" class="Table" cellpadding="0"
                            cellspacing="0">
                            <tr>
                                <td style="font-size: small" align="left" width="20%">
                                    System Particulars:
                                </td>
                                <td style="font-size: small" align="left">
                                    <asp:Label ID="lblSystemParticulars" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small" align="left" width="20%">
                                    Model:
                                </td>
                                <td style="font-size: small" align="left">
                                    <asp:Label ID="lblModel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small" align="left" width="20%">
                                    Serial Number:
                                </td>
                                <td style="font-size: small" align="left">
                                    <asp:Label ID="lblSerialNumber" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small" align="left" width="20%">
                                    Maker:
                                </td>
                                <td style="font-size: small" align="left">
                                    <asp:Label ID="lblMaker" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: small" align="left" width="20%">
                                    Account Code:
                                </td>
                                <td style="font-size: small" align="left">
                                    <asp:Label ID="lblAccountCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="margin-top: 10%;">
                <td align="left" valign="top" colspan="3">
                    <div id="divItemsDetail" runat="server" style="margin-top: 1%;" width="100%">
                        <asp:Label ID="lblID" Text="Items" runat="server" Style="font-size: small; font-weight: bold" /><br />
                        <asp:Label ID="lblDisplayType" runat="server" Text="" Style="font-size: small"></asp:Label><asp:Label
                            ID="lblSupplier" runat="server" Text="" Style="font-size: small"></asp:Label>
                        <table width="100%" id="tblItemsDetail"  runat="server" visible="false">
                            <tr>
                                <td>
                                    <asp:Repeater ID="RepeaterItemDetails" runat="server">
                                        <HeaderTemplate>
                                            <table class="Table" style="border-left: 1px solid gray; border-top: 1px solid gray"
                                                cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Sr. No.
                                                    </td>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Drawing No
                                                    </td>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Part No.
                                                    </td>
                                                    <td style="font-weight: bold; width: 35%" class="TableHead">
                                                        Item Name
                                                    </td>
                                                    <td style="font-weight: bold; width: 10%" class="TableHead">
                                                        Quoted Qty
                                                    </td>
                                                    <td style="font-weight: bold; width: 10%" class="TableHead">
                                                        Unit
                                                    </td>
                                                    <td style="font-weight: bold; width: 40%" class="TableHead">
                                                        Comment
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.ITEM_SERIAL_NO")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.Drawing_Number")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.Part_Number")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.ITEM_SHORT_DESC")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.QUOTED_QTY")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.Unit_and_Packings")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.ITEM_COMMENT")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                        </SeparatorTemplate>
                                        <SeparatorTemplate>
                                        </SeparatorTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <div class="clear">
                                    </div>
                                    <asp:Repeater ID="RepeaterSupItemsRcv" runat="server">
                                        <HeaderTemplate>
                                            <table class="Table" style="border-left: 1px solid gray; border-top: 1px solid gray"
                                                cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Sr. No.
                                                    </td>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Drawing No
                                                    </td>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Part No.
                                                    </td>
                                                    <td style="font-weight: bold; width: 20%" class="TableHead">
                                                        Item Name
                                                    </td>
                                                    <td style="font-weight: bold; width: 7%" class="TableHead">
                                                        Quoted Qty
                                                    </td>
                                                    <td style="font-weight: bold; width: 7%" class="TableHead">
                                                        Offered Qty
                                                    </td>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Unit
                                                    </td>
                                                    <td style="font-weight: bold; width: 10%" class="TableHead">
                                                        Item Type
                                                    </td>
                                                    <td style="font-weight: bold; width: 7%" class="TableHead">
                                                        Quoted Dis.
                                                    </td>
                                                    <td style="font-weight: bold; width: 7%" class="TableHead">
                                                        Quoted Rate
                                                    </td>
                                                    <td style="font-weight: bold; width: 5%" class="TableHead">
                                                        Lead Time(In days)
                                                    </td>
                                                    <td style="font-weight: bold; width: 25%" class="TableHead">
                                                        Comment
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.ITEM_SERIAL_NO")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.Drawing_Number")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.Part_Number")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.ITEM_SHORT_DESC")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.QUOTED_QTY")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.OFFERED_QTY")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.Unit_and_Packings")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.Item_Type")%>
                                                </td>
                                                <td align="right">
                                                    <%#DataBinder.Eval(Container, "DataItem.QUOTED_DISCOUNT")%>
                                                </td>
                                                <td align="right">
                                                    <%#DataBinder.Eval(Container, "DataItem.QUOTED_RATE")%>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("Lead_Time")%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container, "DataItem.QUOTATION_REMARKS")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                        </SeparatorTemplate>
                                        <SeparatorTemplate>
                                        </SeparatorTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <asp:Panel ID="pnlGrandTotal" runat="server" GroupingText="" Width="100%" Visible="false">
                        <table cellpadding="2" cellspacing="0" style="float: left; margin-left: 63%;">
                            <tr>
                                <td width="175px">
                                    Total Price :
                                </td>
                                <td>
                                    <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Discount Total Price :
                                </td>
                                <td>
                                    <asp:Label ID="lblDiscountTotalPrice" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    VAT/GST :
                                </td>
                                <td>
                                    <asp:Label ID="lblVATGST" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Trucking/Freight Cost :
                                </td>
                                <td>
                                    <asp:Label ID="lblTruckingFreightCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PKG & Handling Cost :
                                </td>
                                <td>
                                    <asp:Label ID="lblPKGHandlingCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Truck Cost :
                                </td>
                                <td>
                                    <asp:Label ID="lblTruckCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Barge/Workboat Cost :
                                </td>
                                <td>
                                    <asp:Label ID="lblBargeWorkboatCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Other Cost :
                                </td>
                                <td>
                                    <asp:Label ID="lblOtherCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Grand Total :
                                </td>
                                <td>
                                    <asp:Label ID="lblGrandTotal" runat="server" Font-Size="11px" ForeColor="Blue" Style="text-align: right;
                                        font-weight: bold"></asp:Label>
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
                    <div id="divAF" runat="server" width="100%" style="margin-top: 1%;">
                        <asp:Label ID="lblAF" Text="Attached Files" runat="server" Style="font-size: small; font-weight: bold" />
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
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnReport" ClientIDMode="Static" runat="server" />
    <asp:Label ID="lblTotalDiscount" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblDiscountPrice" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCount" runat="server" Visible="false"></asp:Label>
    <asp:HiddenField ID="hdncheck" runat="server" />
    <asp:HiddenField ID="hdnContent" runat="server" ClientIDMode="Static" />
</asp:Content>
