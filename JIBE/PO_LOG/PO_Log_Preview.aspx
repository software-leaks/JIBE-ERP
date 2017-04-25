<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Preview.aspx.cs" Inherits="PO_LOG_PO_Log_Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  
    <asp:Label ID="lblPOMsg" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"
        Text="You cannot send Purchase Orders to Charterers. Please contact Chartering department if assistance is required."></asp:Label>
        <div style="width:1050px;" >
    <asp:Panel ID="pnl1" runat="server">
        <table width="1100px">
            <tr>
                <td valign="top" style="width:20%;" align="left">
                    <asp:Image ID="Imgheader" runat="server" />
                </td>
                <td align="left" style="width:80%;">
                    <asp:Label ID="lblCName" Font-Bold="true" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblCAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <hr>
        <table width="100%">
            <tr>
                <td valign="top" align="center">
                    <asp:Label ID="lblHeader" Style="font-size: 22px; text-align: Center;" runat="server"
                        Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <hr>
        <table width="100%">
            <tr>
                <td style="width: 400px;">
                    <b><u>ISSUED TO:</u></b>
                </td>
                <td style="width: 400px;">
                    <b><u>ISSUED BY:</u></b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <font style="font-size: 20px;">
                        <asp:Label ID="lblIssueToName" runat="server" Text=""></asp:Label>
                    </font>
                    <br />
                    <font style="font-size: 12px;">
                        <asp:Label ID="lblIssueToAddress" Width="190px" runat="server" Text=""></asp:Label></font>
                </td>
                <td align="left" valign="top">
                    <font style="font-size: 20px;">
                        <asp:Label ID="lblIssueByname" runat="server" Text=""></asp:Label>
                    </font>
                    <br />
                    <font style="font-size: 12px;">
                        <asp:Label ID="lblIssueByAddress" Width="190px" runat="server" Text=""></asp:Label></font>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                </td>
                <td align="left" valign="top">
                    <font style="font-size: 12px;">
                        <asp:Label ID="lblPICName" runat="server" Text=""></asp:Label>
                    </font>
                    <br />
                    <font style="font-size: 12px;">
                        <asp:Label ID="lblPICMobile" runat="server" Text=""></asp:Label></font>
                </td>
            </tr>
        </table>
        <hr>
        <table width="100%">
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td colspan="2" style="width: 400px;">
                                <b><u>ORDER PARTICULARS:</u></b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10PX;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <b>VESSEL NAME</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblVesselName" runat="server" Text=""></asp:Label>
                                Container Vessel
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>PO NUMBER</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblPOCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>PO CURRENCY</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>PO ISSUANCE DATE</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblPODate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>SHIPS REFERENCE</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblShipRef" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>SUPPLIER REFERENCE</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblSupplierRef" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>TYPE OF EXPENSES</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblTypeExp" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>DELIVERY PORT</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblDeliveryPort" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>VESSEL ETA/ETD</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblVesselETAETD" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px; font-size: 12px;">
                                <b>URGENCY</b>
                            </td>
                            <td align="left" style="width: 250px; font-size: 12px;">
                                <asp:Label ID="lblUrgency" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td style="width: 400px;">
                                <b><u>AGENT DETAILS:</u></b>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10PX;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <hr>
        <table width="100%">
            <tr>
                <td>
                    <u><b>DETAILS OF PURCHASE:</b></u>
                </td>
            </tr>
            <tr>
                <td style="height: 5PX;">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltPODetails" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <u><b>Note : The above order is for use/installation on a qualifying ship. </b></u>
                </td>
            </tr>
        </table>
        <hr>
        <table width="100%">
            <tr>
                <td>
                    <font style="font-size: 16px;"><u><b>INSTRUCTIONS TO SUPPLIER:</b></u></font>
                </td>
            </tr>
            <tr>
                <td style="height: 5PX;">
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" style="text-align: left;">
                    This Purchase Order (“PO”) is issued by
                    <asp:Label ID="lblManagerName" runat="server"></asp:Label>
                    <asp:Label ID="lblManagerCode" runat="server"></asp:Label>
                    on behalf of the Principals as Agent. You are required to indicate the PO number
                    in all your invoices before uploading them into the system. All invoices should
                    indicate this PO number and be sent directly to SMS.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    You are required to indicate the PO number in all your invoices before uploading
                    them into the system. All invoices should indicate this PO number and be sent directly
                    to
                    <asp:Label ID="lblManagerCode1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    All goods supplied shall be supported by an appropriate certificate issued by the
                    maker.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    Services performed shall be supported by an appropriate certificate/report issued
                    by the governing associations or professional bodies or certified professionals.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    All invoices have to be addressed and clearly state the Principal’s name (Ship Owner)
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    All invoices must be uploaded into our online system via a web link which was provided
                    to you. You are advised to request again for the unique web link if you are not
                    sure you have it.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    Please liaise with our forwarding agent as listed above for shipment of goods, where
                    applicable.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    By delivering the goods or performing the services ordered in this PO, you will
                    have agreed to the entire terms and conditions hereafter, if any terms are not agreed
                    you need to make such comment in writing.
                </td>
            </tr>
        </table>
        <hr>
        <table width="100%">
            <tr>
                <td>
                    <font style="font-size: 16px;"><u><b>INSTRUCTIONS TO SUPPLIER:</b></u></font>
                </td>
            </tr>
            <tr>
                <td style="height: 5PX;">
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    1.Please acknowledge receipt of this Purchase Order, and confirm in writing action
                    being taken to deliver the Goods or Services which are the subject of this PO.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    2.Prior to the shipment of goods, you are required to provide the size and weight
                    of the consignment prior shipping. Details of any packing charges must be provided
                    in your quotation, unquoted charges are not going to be paid by the Principal.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    3.Partial deliveries are NOT allowed unless agreed by Principal in writing.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    4.Payment Terms:
                    <asp:Label ID="lblPaymentTermsDays" runat="server" Text=""></asp:Label>
                    days from date of receipt of invoice or upon satisfactory completion of services,
                    whichever is later.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    5.Agreed tariffs, where available, shall be used to calculate the value of this
                    PO. Any changes to the tariffs must be agreed by
                    <asp:Label ID="lblManagerCode2" runat="server" Text=""></asp:Label>
                    before implementation, in writing.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    6.Any additional charges that were not indicated in your quotation or in the PO
                    will be subjected to an internal audit that may result in a delay in payment for
                    up to 90 days, in Principal’s discretion.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    7.Principals will not be liable for any invoices that are not uploaded into the
                    system.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    8.The Agent will not be liable for any invoice that doesn't clearly state the Principal’s
                    name
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    9.Supplier waives any claims and will indemnify the Principal for any costs resulting
                    from a claim over an invoice that was not uploaded into the online system of the
                    Principal and its Agent.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    10.Invoice has to be presented via direct upload within 30 days from service/delivery
                    date. Supplier understands and accepts that invoices are not uploaded within 30
                    days from the date of completion of service / delivery of goods, are deemed waived
                    and cancelled.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    11.All invoices must include a proof of delivery/services completion. The only acceptable
                    proof is a signature and stamp by Superintendent or Master or Chief Engineer. A
                    delivery/service is considered incomplete/unsatisfactory if it is not acknowledged
                    by the Superintendent or Master or Chief Engineer. Supplier waives all claims for
                    delivery or service that was not confirmed as per this procedure.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    12.A packing slip and pro-forma invoice have to be enclosed in all consignments
                    for customs clearance purposes.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    13.VAT or GST or other equivalent taxes - The goods are ordered as stores in transit
                    for delivery to an ocean vessel and such supplies should be zero rated for tax purposes.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    14.Any charges in excess of the PO value will be rejected unless you able to provide
                    a written approval, email acceptable as well, from
                    <asp:Label ID="lblManagerCode3" runat="server" Text=""></asp:Label>
                    for the excess.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    15.Any incidental costs / expenses / out-of-pocket, if not agreed by the Owner or
                    Principal, in writing, prior to provision of goods and services, shall be outright
                    rejected. Any attempt to charge such unapproved costs will incur a $100 penalty.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    16.<asp:Label ID="lblMCode5" runat="server" Text=""></asp:Label>
                    may, at its own discretion, charge you an administrative fee of up to US$100 for
                    any internal audit it may conduct as a result of a fee that was billed in excess
                    of the PO value.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    17.Principal may, at its own discretion, charge you an administrative fee of up
                    to US$50 for invoices that are received by email or mail and not uploaded into the
                    system.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    18.You are prohibited to offer ANY gift or courtesy items to the crew. In the event
                    that you are suspected to have done so, Principal may, at its sole discretion, withhold
                    payments for up to 365 days in order to conduct an audit of your account. You may
                    be charged a fee of up to US$1,000 if found to have violated Principal’s policy
                    on this matter.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    19.Supplier guarrantees that none of the items proposed or supplied contain ANY
                    asbestos. The Supplier will inform the Agent,
                    <asp:Label ID="lblManagerCode4" runat="server" Text=""></asp:Label>, in writing,
                    prior to accepting this order, if any of the ordered items may contain asbestos.
                    Supplier will indemnify Principal and Agent for all costs, direct or indirect, without
                    limitation including any expenses and consequences caused to remove such items that
                    contain asbestos or any fines or losses related to the supply of such items to the
                    Owner, regardless of whether the supplier was or was not aware of Asbestos present
                    in the goods.
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: left;">
                    <asp:Label ID="lbl1" runat="server" Text="20.Any replacement of spare parts or additional job scope other than those stated above must receive the explicit approval from"></asp:Label>
                    <asp:Label ID="lblManagerCode5" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lbl2" runat="server" Text="Confirmation or acceptance by Ship's crew without approval from"></asp:Label><asp:Label
                        ID="lblManagerCode6" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl3" runat="server" Text="does not warrant payment for any services or materials granted."></asp:Label>
                </td>
            </tr>
        </table>
        <div style="display: none;">
            <asp:TextBox ID="txtSupplyID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtPOType" runat="server"></asp:TextBox>
      </div>
    </asp:Panel>
   </div>
    </form>
</body>
</html>
