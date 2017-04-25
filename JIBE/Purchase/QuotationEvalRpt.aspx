<%@ Page Title="Quotation Eval Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="QuotationEvalRpt.aspx.cs" Inherits="Purchase_QuotationEvalRpt" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script type="text/javascript" src="Scripts/boxover.js" ></script>

        <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
        <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
        <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
        <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
   
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
        <script src="../Scripts/Purc_Functions_Common.js?v=1" type="text/javascript"></script>
        <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
        <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    
        <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
    function checkAvailableWidth() 
    {

            var container = document.getElementById("divInfoQTN");

            $('#divInfoQTN th').css('top', document.getElementById("divInfoQTN").scrollTop - 2 + 'px')
            $('.hd').css('left', document.getElementById("divInfoQTN").scrollLeft - 2 + 'px')
            $('.gtdth').css('left', document.getElementById("divInfoQTN").scrollLeft - 2 + 'px')


        }


        function ItemsHistoryShow() {
            var items = document.getElementById("ctl00_MainContent_lblITEMSYSTEMCODE").value;
            var VesselCode = document.getElementById("ctl00_MainContent_lblVesselCode").value;
            window.open("Delivery_History.aspx?itemcode=" + items + "&VesselCode=" + VesselCode);
            return false;
        }

        function divHistoryShow() {

            var div = document.getElementById("divHistory").style.display = "block";

            return false;

        }

        function DivHistoryClose() {

            var div = document.getElementById("divHistory").style.display = "none";


        }
    </script>
    <style type="text/css">
   
.GridviewScrollHeader TH, .GridviewScrollHeader TD 
{ 
     background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x;
    padding: 5px; 
    height:35px;
    font-weight: bold; 
    white-space: nowrap;
    border-right: 1px solid #BCBEBD; 
    border-bottom: 1px solid #BCBEBD; 
    background-color: #EFEFEF;
    text-align: left; 
    vertical-align: middle; 
    font-size:11px;
    color:#3F4140;
} 
.GridviewScrollItem TD 
{ 
    padding: 5px; 
    white-space: nowrap; 
    border-right: 1px solid #CACCCC; 
    border-bottom: 1px solid #CACCCC; 
    background-color: #f7f9fc; 
} 
.GridviewScrollPager  
{ 
    border-top: 1px solid #d6e79c; 
    background-color: #f7f9fc; 
} 
.GridviewScrollPager TD 
{ 
    padding-top: 3px; 
    font-size: 14px; 
    padding-left: 5px; 
    padding-right: 5px; 
} 
.GridviewScrollPager A 
{ 
    color: #666666; 
}
.GridviewScrollPager SPAN

{

    font-size: 10px;

    font-weight: bold;

}
.mybavk 
{
    /*background-image: url("../Images/silv3.jpg"); background-repeat: no-repeat}*/
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:UpdatePanel ID="upd" ClientIDMode="Static" runat="server">
<ContentTemplate>
<table cellpadding="0" cellspacing="1" align="center" style="width: 100%;">
                    <tr align="center">
                        <td style="color: #FFFFFF; font-size: small" width="100%">
                            <table cellpadding="1" cellspacing="1" width="100%" style="background-color: #f4ffff;">
                                <tr>
                                    <td width="90%">
                                        <table cellpadding="4" cellspacing="3" align="left" style="border: 2px solid #000;
                                            background-color: #e4e4e4" width="100%">
                                            <tr align="left"style="border: 2px solid #000;">
                                                <td align="left" style="font-size: 11px; color: #333333; font-weight: 700; width: 225px;">
                                                    Requisition Number :
                                                </td>
                                                <td style="font-size: 11px; color: #333333;">
                                                    <b>
                                                        <asp:HyperLink ID="lblReqNo" Target="_blank" runat="server" Width="170px"> </asp:HyperLink>
                                                    </b>
                                                </td>
                                                <td style="font-size: 11px; color: #333333; width: 88px;">
                                                    <b>Vessel : </b>
                                                </td>
                                                <td style="font-size: 11px; width: 131px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblVessel" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                <td style="font-size: 11px; width: 88px; color: #333333;">
                                                    <b style="color: #333333">Catalogue/System:</b>
                                                </td>
                                                <td style="font-size: 11px; width: 150px; color: #333333">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblCatalog" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                <td align="left" style="font-size: 11px; width: 88px; color: #333333;">
                                                    <b style="color: #333333">Maker :</b>
                                                </td>
                                                <td style="font-size: 11px; width: 150px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblMaker" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                 <td style="font-size: 11px; width: 188px; color: #333333;">
                                                    <b style="color: #333333">Requisition Date :</b>
                                                </td>
                                                <td style="font-size: 11px; width: 150px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblReqDate" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                            </tr>
                                            <tr><td style="font-size: 11px; width: 88px; color: #333333;">
                                                    <b style="color: #333333">  <span style="color: Black; font-size: 11px; font-weight: bold">Purchase Number:</span> </b>
                                                </td>
                                                <td style="font-size: 11px; width: 150px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                       <%-- <asp:Label ID="Label7" runat="server" Style="font-weight: 700"></asp:Label>--%>
                                                    </span></b>
                                                </td>
                                                 <td align="left" style="font-size: 11px; width: 158px; color: #333333;">
                                                    <b style="color: #333333">Receival Date :</b>
                                                </td>
                                                <td align="left" style="font-size: 11px; width: 150px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblRecDate" runat="server" Style="font-weight: 700;"></asp:Label>
                                                    </span></b>
                                                </td>
                                                 <td align="left" style="font-size: 11px; width: 195px; color: #333333;">
                                                    <b style="color: #333333">Requested Delivery Date :</b>
                                                </td>
                                                <td align="left" style="font-size: 11px; width: 150px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblDvDate" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                 <td align="left" style="font-size: 11px; width:298px; color: #333333;">
                                                    <b style="color: #333333">Total Number of Items :</b>
                                                </td>
                                                <td align="left" style="font-size: 11px; width: 170px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblTotItems" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                 <td align="left" style="font-size: 11px; width: 285px; color: #333333;">
                                                    <b style="color: #333333">Reason For Requisition :</b>
                                                </td>
                                                <td align="left" style="font-size: 11px; width: 150px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblReason" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                
                                                </tr>
                                            <tr>
                                                <td align="left" colspan="10"  style="vertical-align:top;margin-top:0px;">
                                                  
                                                    <asp:DataList ID="dlistPONumber" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <ItemTemplate>
                                                            <td style="padding-right: 10px">
                                                                <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                                                    target="_blank">
                                                                    <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                                                            </td>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                    <td width="10%">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%; color: Black; background-color: #f4ffff;
                                            text-align: right; padding: 5px; border-collapse: collapse; border-color: Black"
                                            border="1">
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="lbtnPurchaserRemark" Font-Size="11px" ForeColor="Black" Text="Purchaser Remark"
                                                        runat="server"></asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnApprovalHistory" runat="server" Font-Size="11px" ForeColor="Black"
                                                        OnClientClick="return divHistoryShow()" Text="Track Approvals" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="btnItemHistory" ForeColor="Black" runat="server" Font-Size="11px"
                                                        OnClientClick="return ItemsHistoryShow()" Text="View Catalogue history" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                  
                    <tr align="left">
                        <td style="color: #FFFFFF; font-size: small">
                            <table cellpadding="0" cellspacing="1" align="left" style="width: 100%; background-color: #999999;">
                                <tr align="left">
                                    <td align="left">
                                        <div style="height: 150px; overflow: auto; margin-left: 0px; width: 100%;">
                                            <telerik:RadGrid ID="rgdSupplierInfo" runat="server" AllowAutomaticInserts="True"
                                                AlternatingItemStyle-BackColor="#CEE3F6" AllowPaging="True" GridLines="None"
                                                Skin="Office2007" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                                                Font-Size="10px" ItemStyle-Height="12px" CellPadding="0" CellSpacing="0" OnNeedDataSource="rgdSupplierInfo_NeedDataSource"
                                                OnItemDataBound="rgdSupplierInfo_ItemDataBound">
                                                <MasterTableView ItemStyle-Height="16px" DataKeyNames="PortName,Currency">
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="SUPPLIER" HeaderText="Supplier Short Name" UniqueName="SUPPLIER"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SHORT_NAME" HeaderText="Supplier Name" UniqueName="SHORT_NAME"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="350px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="Status" HeaderText="Quoted Items(Items Selected)" UniqueName="chkStatus"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="80px" />
                                                            <HeaderTemplate>
                                                               Quoted Items(Items Selected)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkStatus" Enabled="false" Checked='<%#Eval("Status").ToString()=="1"?true:false %>'
                                                                    runat="server" />
                                                                <%#"&nbsp;&nbsp;"+Eval("TotalQTDItems")+"&nbsp;&nbsp;"%>
                                                                <asp:HyperLink ID="hlnkQtnRef" runat="server" Text='<%#Eval("Supplier_Quotation_Reference") %>'
                                                                    Width="80px" Target="_blank" NavigateUrl="#">
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Selected Suppliers"
                                                            AllowFiltering="false" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkQuaEvaluated" Height="12px" Width="14px" runat="server" Checked='<%#Eval("EvalSupplier")%>'
                                                                    Font-Size="Smaller" AutoPostBack="false" Enabled="false" BorderStyle="None" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Supp_Tot_Amt" HeaderText="Supplier Amount" UniqueName="Supp_Tot_Amt"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="true" Display="true">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Selected Item" Visible="True" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTotalItem" runat="server" Text='<%#Eval("TotalQTDItems") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Selected Items Amount" Visible="True" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAmount" runat="server" Text="0.00"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Discount (%)" Visible="True" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtDiscount" runat="server" Text='<%#Eval("DISCOUNT")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Surcharge" Display="false" DataField="Surcharge"
                                                            UniqueName="Surcharge" Visible="true" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtSurcharge" runat="server" Text="0.00"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="VAT(%)" Visible="True" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtVat" runat="server" Text='<%#Convert.ToDecimal(Eval("VAT").ToString()).ToString("0.00") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="80px" ></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="DISCOUNT" HeaderText="Dis." UniqueName="DISCOUNT"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="false" Display="false">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EXCHANGE_RATE" HeaderText="Ex.Rate" UniqueName="EXCHANGE_RATE"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="false" Display="false">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SURCHARGES" HeaderText="Sur." UniqueName="SURCHARGES"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="false" Display="false">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VAT" HeaderText="Vat." UniqueName="VAT" Visible="true"
                                                            HeaderStyle-Width="0" AllowFiltering="true" Display="false">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="Packing_Handling_Charges" HeaderText="Packging"
                                                            UniqueName="Packing_Handling_Charges" Visible="true" HeaderStyle-Width="0" AllowFiltering="true"
                                                            Display="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPkg" Text='<%#Eval("Packing_Handling_Charges")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblPkgRs" Visible="false" Text='<%#Eval("REASON_TRANS_PKG")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Freight_Cost" HeaderText="Freight" UniqueName="Freight_Cost"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="true" Display="true">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                         <telerik:GridBoundColumn DataField="Truck_Cost" HeaderText="Truck" UniqueName="Truck_Cost">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Barge_Workboat_Cost" HeaderText="Barge/Work boat"
                                                            UniqueName="Barge_Workboat_Cost">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                           
                                                        <telerik:GridTemplateColumn DataField="Other_Charges" HeaderText="Other" UniqueName="Other_Charges"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="true" Display="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOtherCost" Text='<%#Eval("Other_Charges")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblOtherCostRs" Visible="false" Text='<%#Eval("Other_Charges_Reason")%>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridTemplateColumn>

                                                    
                                                        
                                                        <telerik:GridTemplateColumn HeaderText="Final Amount" Visible="True" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtGrandTotal" runat="server" Text='<%#UDFLib.ConvertToDecimal(Eval("ORDER_AMOUNT").ToString()) < 1?0:Eval("ORDER_AMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" ForeColor="BlueViolet">
                                                            </ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Other_Charges" HeaderText="Supply Date" UniqueName="Other_Charges"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="true" Display="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplyDate" Text='<%#Eval("Other_Charges")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Quotation Remarks">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgQuRemark" Height="12px" runat="server" AlternateText='<%#Eval("QUOTATION_COMMENTS")+"<br>Delivery Term:"+ Eval("DeliveryTerm")+"<br>Origin:"+Eval("Delivery_Origin") %>'
                                                                    ImageUrl="~/purchase/Image/view1.gif" Visible='<%# Convert.ToString(Eval("QUOTATION_COMMENTS")) == "" ? false : true %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="0px" HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Quotation Attachments">
                                                            <ItemTemplate>
                                                               <asp:ImageButton ID="ImgAttachment"  runat="server"  Height="20px" Width="20px" ImageUrl="../Images/attachment.png" style="border: 0px solid white"  
                                                                    onclick='<%#"OpenAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&SUPPLIER_CODE=" + Eval("SUPPLIER").ToString()+"&#39;)"%>'>
                                                        </asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="0px" HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Req_Qut_Status" HeaderText="Req_Qut_Status" UniqueName="Req_Qut_Status"
                                                            Display="false" HeaderStyle-Width="0" AllowFiltering="true">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ORDER_AMOUNT" Display="false" UniqueName="ORDER_AMOUNT">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="QUOTATION_CODE" ItemStyle-Width="0px" ItemStyle-Font-Size="0px"
                                                            UniqueName="QUOTATION_CODE" Visible="true" AllowFiltering="false">
                                                            <ItemStyle />
                                                        </telerik:GridBoundColumn>
                                                        
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

<div  id="divHeader" style="overflow:no-scroll;margin-left: 0px; width: 100%;">
<table>
 
<tr>
<td>
<table align="left" cellpadding="2" cellspacing="4" style="font-family:Tahoma;font-size:12px;font-weight:bold" >
<tr>
<td><asp:Label ID="lblNote" runat="server" ClientIDMode="Static" Text="NOTE:" ForeColor="Black" Font-Bold="true"></asp:Label></td>
<td><asp:Label ID="lblColorTag1"  Text="🔲&nbsp;Cheapest & Selected Supplier"  runat="server" ClientIDMode="Static" ToolTip="Cheapest & Selected Supplier"  ForeColor="Green"></asp:Label></td>
<td><asp:Label ID="lblColorTag2" Text="🔲&nbsp;Cheapest Supplier"  runat="server" ClientIDMode="Static" ToolTip="Cheapest Supplier"  ForeColor="CornflowerBlue"></asp:Label></td>
<td><asp:Label ID="lblColorTag3" Text="🔲&nbsp;Selected Supplier"  runat="server" ClientIDMode="Static" ToolTip="Selected Supplier"  ForeColor="Purple"></asp:Label></td>
</tr>
</table>
</td>
</tr>
<tr><td><asp:GridView ID="grdQuoteSupp" ClientIDMode="Static" runat="server"
        GridLines="None" CellSpacing="0"
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
        BorderWidth="1px" CellPadding="4"
        AutoGenerateColumns="true" onrowdatabound="grdQuoteSupp_RowDataBound" 
        onrowcreated="grdQuoteSupp_RowCreated">
        <HeaderStyle CssClass="GridviewScrollHeader" /> 
        <RowStyle CssClass="GridviewScrollItem"  /> 
        <PagerStyle CssClass="GridviewScrollPager" /> 
    

</asp:GridView></td></tr>
</table>
<%--<telerik:RadGrid ID="rgdQuoteSupp" runat="server" ClientIDMode="Static"
        GridLines="None" Skin="Office2007" Width="100%" AutoGenerateColumns="true" HeaderStyle-HorizontalAlign="Center"
        Font-Size="10px" ItemStyle-Height="12px" CellPadding="0" CellSpacing="0" OnItemDataBound="rgdQuoteSupp_ItemDataBound">
        <MasterTableView>
         <RowIndicatorColumn Visible="False">
            <HeaderStyle HorizontalAlign="Center" Width="20px" />
        </RowIndicatorColumn>
        <ExpandCollapseColumn Resizable="False" Visible="False">
            <HeaderStyle Width="20px" />
        </ExpandCollapseColumn>
        </MasterTableView>
        </telerik:RadGrid>--%>


</div>
 <div id="divHistory" class="popup-css" style="display: none; left: 35%; top: 25%;
                position: fixed; padding-top: 10px; padding-left: 30px; padding-right: 30px;
                padding-bottom: 30px; color: White; text-align: right; max-height: 300px; max-width: 700px;
                overflow: auto; border: 1px solid black; z-index: 456">
                <img id="imgdivclose" onclick="DivHistoryClose()" alt="close" title="close" src="../Images/Close.gif" />
                <asp:GridView ID="gvApprovalHistory" AutoGenerateColumns="true" Width="100%" EmptyDataText="no record found"
                    OnRowCreated="gvApprovalHistory_RowCreated" runat="server" OnDataBound="gvApprovalHistory_DataBound">
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle-css" />
                </asp:GridView>
            </div>
<asp:HiddenField ID="hdnFileName" ClientIDMode="Static" runat="server" />
 <asp:HiddenField ID="lblITEMSYSTEMCODE" runat="server" />
                            <asp:HiddenField ID="lblVesselCode" runat="server" />
</ContentTemplate>
<Triggers>
</Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    function DocOpen() 
    {
        var docpath = "../Uploads/Purchase/" + $("#hdnFileName").val();
        window.open(docpath);
    }
    $(document).ready(function () {
        EnableScroll();
    });

    function OpenAttachment(queryString) {
        parent.OpenPopupWindowBtnID('ReqsnAttachment_ID', 'Requisition Attachments', 'ReqsnAttachment.aspx?' + queryString, 'popup', 500, 800, null, null, false, false, true, false, 'id');
    }
    
    function EnableScroll() {
       

        var gridWidth = $(window).width();
        var gridHeight = $(window).height();

        var headerHeight = $("#divHeader").height();

        gridHeight = gridHeight - headerHeight;
        gridWidth = 1210; gridHeight = 450;
        $('#grdQuoteSupp').gridviewScroll({
            width: gridWidth,
            height: gridHeight,
            freezesize:7
        });
    } 
</script>
</asp:Content>

