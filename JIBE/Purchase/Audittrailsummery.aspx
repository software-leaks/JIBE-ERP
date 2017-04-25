<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Audittrailsummery.aspx.cs"
    Inherits="Technical_INV_Audittrailsummery" Title="Audit Trail Summary" %>

<asp:Content ID="conthead" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .hrtd
        {
            height: 12.75pt;
            color: #585858;
            /*font-size: 9.0pt;
            font-weight: 700;*/
            font-style: normal;
            text-decoration: none;
            font-family: Tahoma;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #CEE3F6;
            border-bottom: 1px solid #E6E6E6;
        }
        
        #tblAudit td
        {
            color: windowtext;
            font-size: 8.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Tahoma;
            text-align: left;
            vertical-align: bottom;
            white-space: normal;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            border-right: 1px solid #E6E6E6;
        }
        
        .headtd
        {
            height: 12.75pt;
            color: Black;
            font-size: 8.0pt;
            font-weight: 600;
            font-style: normal;
            text-decoration: none;
            font-family: Tahoma;
            text-align: left;
            vertical-align: bottom;
            white-space: nowrap;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            border-right: 0px solid #E6E6E6;
            width: 300px;
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="text-align: left">
        <table cellpadding="0" cellspacing="0" style="height: 16px; width: 100%; background-color: #C0C0C0;">
            <tr>
                <td style="background-color: #808080; font-size: small; color: #FFFFFF; width: 100%;"
                    class="hrtd">
                    <b>Audit Trail Summary</b>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%"  id="tblAudit" >
            <tr>
            <td  align="left" style="font-weight:bold">
                    Step 1. Requisition Creation
                </td>
            </tr>
                   
            
            <tr>
                <td align="left">
                    <table  cellpadding="1" align="left" cellspacing="1" width="100%" style="border-bottom: 1px solid #E6E6E6;
                        font-size: 11px;">
                        <tr>
                            <td style="width: 40%; text-align: left">
                                <table align="left" cellpadding="1" cellspacing="3" width="100%" style="text-align: left; font-size: 11px">
                                    <tr>
                                        <td style="color: #333333; text-align: left; width: 30%; font-size: 11px">
                                            Reqsn Number: 
                                        </td>
                                        <td style="text-align: left; width: 70%">
                                            <asp:Label ID="lblReqNo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            Department:
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            Date: 
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                           Total Item: 
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;vertical-align:text-top">
                                          Reason for Req :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtComments" runat="server" Width="100%" TextMode="MultiLine" 
                                                ReadOnly="true" Style="font-size: small"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 60%; text-align: right">
                                <table cellpadding="1" cellspacing="1" width="100%" style="text-align: center; font-size: 11px">
                                    <tr>
                                        <td style="width: 100%; text-align: center">
                                            <asp:GridView ID="rgdHoldLog" runat="server" showstatusbar="True" Font-Size="8px"
                                                Font-Names="verdana" AutoGenerateColumns="False" AllowSorting="false" AllowPaging="false"
                                                GridLines="None" EmptyDataText="No  logs found for hold/un hold." CellSpacing="0"
                                                CellPadding="0" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="User" ItemStyle-Width="80px" DataField="User_name" HeaderStyle-BackColor="#D3D3A8"
                                                        HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField HeaderText="Date" ItemStyle-Width="100px" DataField="OnHoldDate"
                                                        HeaderStyle-BackColor="#D3D3A8" HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px"
                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField HeaderText="Action" ItemStyle-Width="90px" DataField="OnHold" HeaderStyle-BackColor="#D3D3A8"
                                                        HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                                </Columns>
                                                <AlternatingRowStyle BackColor="ActiveBorder" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvReqsnCAncel" runat="server" showstatusbar="True" Font-Size="8px"
                                                Font-Names="verdana" AutoGenerateColumns="False" AllowSorting="false" AllowPaging="false"
                                                GridLines="None" EmptyDataText="No  logs found for cancel reqsn." CellSpacing="0"
                                                CellPadding="0" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Name" ItemStyle-Width="80px" DataField="name" HeaderStyle-BackColor="#D3D3A8"
                                                        HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField HeaderText="Date" ItemStyle-Width="100px" DataField="cancelleddate"
                                                        HeaderStyle-BackColor="#D3D3A8" HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px"
                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField HeaderText="Remark" ItemStyle-Width="90px" DataField="Remarks" HeaderStyle-BackColor="#D3D3A8"
                                                        HeaderStyle-Font-Names="Verdana" HeaderStyle-Font-Size="11px" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Font-Size="9px" ItemStyle-HorizontalAlign="Left" />
                                                </Columns>
                                                <AlternatingRowStyle BackColor="ActiveBorder" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Repeater ID="ReptrQtnSummry" runat="server">
                        <HeaderTemplate>
                            <div style="text-align: left">
                                <table  cellspacing="0" width="60%" style="border-bottom: 1px solid #E6E6E6;
                                    text-align: left">
                                    <tr>
                                        <td class="headtd" style="font-weight:bold">
                                            Step 2. Quotation Send/Receive
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="hrtd">
                                            RFQ Sent To
                                        </td>
                                        <td class="hrtd">
                                            Sent Date
                                        </td>
                                        <td class="hrtd">
                                            RFQ Sent By
                                        </td>
                                        <td class="hrtd">
                                            Qtn Submit Date.
                                        </td>
                                    </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.RFQSent")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SentDate")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.RFQSendBy")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ReceivedDate")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.RFQSent")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SentDate")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.RFQSendBy")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ReceivedDate")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table> </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Repeater ID="ReptrQtnEve" runat="server">
                        <HeaderTemplate>
                            <table cellspacing="0" width="100%" style="border-bottom: 1px solid #E6E6E6;">
                                <tr>
                                    <td class="headtd" style="font-weight:bold">
                                        Step 3. Quotation Evaluation
                                    </td>
                                </tr>
                                <tr>
                                    <b>
                                        <td class="hrtd">
                                            Supp Name
                                        </td>
                                        <td class="hrtd">
                                            Is Evaluate
                                        </td>
                                        <td class="hrtd">
                                            Total Items
                                        </td>
                                        <td class="hrtd">
                                            RFQ Date
                                        </td>
                                        <td class="hrtd">
                                            Qtn. Due Date
                                        </td>
                                        <td class="hrtd">
                                            Tot Discount
                                        </td>
                                        <td class="hrtd">
                                            Tot Surchange
                                        </td>
                                        <td class="hrtd">
                                            Tot Vat
                                        </td>
                                        <td class="hrtd">
                                            Tot supp Amt.
                                        </td>
                                    </b>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.EvaluStatus")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Total_Item")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.RFQDate")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Quotation_Due_Date")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Total_dis")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Tot_Ser")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Tot_Vat")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Supp_Tot_Amt")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </SeparatorTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.EvaluStatus")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Total_Item")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.RFQDate")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Quotation_Due_Date")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Total_dis")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Tot_Ser")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Tot_Vat")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Supp_Tot_Amt")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Repeater ID="ReptrPurchaseOrd" runat="server">
                        <HeaderTemplate>
                            <table cellspacing="0" width="100%" style="border-bottom: 1px solid #E6E6E6;">
                                <tr>
                                    <td class="headtd" style="font-weight:bold">
                                        Step 4. Purchase Order Raise
                                    </td>
                                </tr>
                                <tr>
                                    <b>
                                        <td class="hrtd">
                                            PO Code
                                        </td>
                                        <td class="hrtd">
                                            Supp Name
                                        </td>
                                        <td class="hrtd">
                                            Total Items
                                        </td>
                                        <td class="hrtd">
                                            Order Date
                                        </td>
                                        <td class="hrtd">
                                            Approver
                                        </td>
                                        <td class="hrtd">
                                            Total Amount
                                        </td>
                                        <td class="hrtd">
                                            Discount
                                        </td>
                                        <td class="hrtd">
                                            Surcharge
                                        </td>
                                        <td class="hrtd">
                                            Vat
                                        </td>
                                    </b>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.order_Code")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.total_Item")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_DATE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.APPROVER")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.total_Amount")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.discount")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SURCHARGES")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Vat")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </SeparatorTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.order_Code")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.total_Item")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_DATE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.APPROVER")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.total_Amount")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.discount")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SURCHARGES")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Vat")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Repeater ID="ReptrPendingSupConfirm" runat="server">
                        <HeaderTemplate>
                            <table cellspacing="0" width="100%" style="border-bottom: 1px solid #E6E6E6;">
                                <tr>
                                    <td class="headtd" style="font-weight:bold">
                                        Step 5. Pending for Supplier confirmation
                                    </td>
                                </tr>
                                <tr>
                                    <b>
                                        <td class="hrtd">
                                            Order Code
                                        </td>
                                        <td class="hrtd">
                                            Sent Date
                                        </td>
                                        <td class="hrtd">
                                            Total Items
                                        </td>
                                        <td class="hrtd">
                                            Supp Name
                                        </td>
                                        <td class="hrtd">
                                            Lead Time (In days)
                                        </td>
                                        <td class="hrtd">
                                            OnHold
                                        </td>
                                    </b>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_CODE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.requestion_Date")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.TOTAL_ITEMS")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Lead_Time")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.OnHold")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </SeparatorTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_CODE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.requestion_Date")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.TOTAL_ITEMS")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SUPPLIER")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.SHORT_NAME")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.Lead_Time")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.OnHold")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Repeater ID="ReptrDeliveryStatus" runat="server">
                        <HeaderTemplate>
                            <table cellspacing="0" width="100%" style="border-bottom: 1px solid #E6E6E6;">
                                <tr>
                                    <td class="headtd" style="font-weight:bold">
                                        Step 6. Delivery Status
                                    </td>
                                </tr>
                                <tr>
                                    <b>
                                        <td class="hrtd">
                                            Order Code
                                        </td>
                                        <td class="hrtd">
                                            Delivery Stage
                                        </td>
                                       <%-- <td class="hrtd">
                                            Delivery StageDate
                                        </td>--%>
                                       <%-- <td class="hrtd">
                                            Forwarder Name
                                        </td>
                                        <td class="hrtd">
                                            Forwarder Country
                                        </td>--%>
                                        <%--<td class="hrtd">
                                            Agent Code
                                        </td>
                                        <td class="hrtd">
                                            Est.Dev. Date
                                        </td>--%>
                                        <td class="hrtd">
                                            Delivery Type
                                        </td>
                                    </b>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_CODE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DeliveryStage")%>
                                </td>
                                <%--<td>
                                    <%#DataBinder.Eval(Container, "DataItem.DeliveryStageDate")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ForwarderName")%>
                                </td>--%>
                               <%-- <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ForwarderCountry")%>
                                </td>--%>
                                <%--<td>
                                    <%#DataBinder.Eval(Container, "DataItem.AgentCode")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.EstDevOnBoardDate")%>
                                </td>--%>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DELIVERY_TYPE")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </SeparatorTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_CODE")%>
                                </td>
                               <%-- <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DeliveryStage")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DeliveryStageDate")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ForwarderName")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ForwarderCountry")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.AgentCode")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.EstDevOnBoardDate")%>
                                </td>--%>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DELIVERY_TYPE")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Repeater ID="ReptrDeliveredItems" runat="server">
                        <HeaderTemplate>
                            <table cellspacing="0" width="100%" style="border-bottom: 1px solid #E6E6E6;">
                                <tr>
                                    <td class="headtd" style="font-weight:bold">
                                        Step 7. Delivered Items
                                    </td>
                                </tr>
                                <tr>
                                    <b>
                                        <td class="hrtd">
                                            Delivery Code
                                        </td>
                                        <td class="hrtd">
                                            ORD. Qty
                                        </td>
                                        <td class="hrtd">
                                            ORD. Rate
                                        </td>
                                        <td class="hrtd">
                                            ORD. Discount
                                        </td>
                                        <td class="hrtd">
                                            ORD Price
                                        </td>
                                        <td class="hrtd">
                                            ORD VAT.
                                        </td>
                                        <td class="hrtd">
                                            Delivered Qty
                                        </td>
                                        <td class="hrtd">
                                            Comments
                                        </td>
                                    </b>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DELIVERY_CODE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_QTY")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_RATE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_DISCOUNT")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_PRICE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_VAT")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DELIVERD_QTY")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ITEM_COMMENT")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </SeparatorTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DELIVERY_CODE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_QTY")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_RATE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_DISCOUNT")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_PRICE")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ORDER_VAT")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.DELIVERD_QTY")%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container, "DataItem.ITEM_COMMENT")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
