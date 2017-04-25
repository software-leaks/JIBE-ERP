<%@ Page Title="Evaluation Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Quotation_Evaluation_Report.aspx.cs" Inherits="Purchase_Quotation_Evaluation_Report" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Register Src="../UserControl/ucPurcQuotationApproval.ascx" TagName="ucApprovalUser"
    TagPrefix="ucUser" %>
<asp:Content ID="header" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/boxover.js" ></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />

    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .target
        {
            width: 80px;
            text-align: center;
            border: 2px solid #666666;
            padding: 5px;
            background-color: #00FFFF;
            height: 45px;
            display: block;
            float: left;
        }
        
        .amount-css
        {
            text-align: right;
            padding-right: 5px;
        }
        
        .text-css
        {
            text-align: left;
        }
        
        
        div#divInfoQTN
        {
            width: 1210px;
            max-height: 400px;
            overflow: scroll;
            position: relative;
        }
        
        div#divInfoQTN th
        {
            top: expression(document.getElementById("divInfoQTN").scrollTop-2); /* left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);*/
            position: relative;
        }
        .gtdth
        {
            z-index: 0;
            background-color: #E3E4FA;
            position: relative;
            cursor: default;
            left: expression(document.getElementById("divInfoQTN").scrollLeft-2);
        }
        .hd
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#E3E4FA',EndColorStr='#8fb9cc');
            position: relative;
            cursor: default;
            z-index: 200;
            left: expression(document.getElementById("divInfoQTN").scrollLeft-2);
            border-left: 1pt solid #B7CEEC;
        }
        .NewItem
        {
            background-color: Yellow;
        }
    </style>
    <script language="javascript" type="text/javascript">

        var ColumnCount_Supp = 7;

        var SuppGridColumnCount = 24;

        var UnitsPkgID = '';



        function checkAvailableWidth() {

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
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updmain" UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
        <ContentTemplate>
            <center>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <b style="font-size: small">Quotation Comparision - All figure in USD</b>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" align="center" style="width: 100%;">
                    <tr align="center">
                        <td style="color: #FFFFFF; font-size: small" width="100%">
                            <table cellpadding="0" cellspacing="0" width="100%" style="background-color: #f4ffff;">
                                <tr>
                                    <td width="80%">
                                        <table cellpadding="1" cellspacing="2px" align="left" style="border: 2px solid #FFFFFF;
                                            background-color: #f4ffff" width="100%">
                                            <tr align="left">
                                                <td align="left" style="font-size: 11px; color: #333333; font-weight: 700; width: 88px;">
                                                    Requisition No :
                                                </td>
                                                <td style="font-size: 11px; color: #333333; width: 133px;">
                                                    <b>
                                                        <asp:HyperLink ID="lblReqNo" Target="_blank" runat="server"> </asp:HyperLink>
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
                                                    <b style="color: #333333">Catalogue :</b>
                                                </td>
                                                <td style="font-size: 11px; width: 150px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblCatalog" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td style="font-size: 11px; width: 88px; color: #333333;">
                                                    <b style="color: #333333">Req. Date :</b>
                                                </td>
                                                <td style="font-size: 11px; width: 131px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblReqDate" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                <td style="font-size: 11px; width: 88px; color: #333333;">
                                                    <%--<b>RFQ Date :</b>--%><b>Qtn. Due Date :</b>
                                                </td>
                                                <td style="font-size: 11px; width: 131px; color: #333333;">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblQuotDueDate" runat="server" Style="font-weight: 700"></asp:Label>
                                                        <asp:Label ID="lblToDate" runat="server" Visible="false" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                                <td style="font-size: 11px; color: #333333; text-align: left">
                                                    <b style="color: #333333">Total Items : </b>
                                                </td>
                                                <td style="font-size: 11px; color: #333333; text-align: left">
                                                    <b><span style="font-weight: normal">
                                                        <asp:Label ID="lblTotalItem" runat="server" Style="font-weight: 700"></asp:Label>
                                                    </span></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" style="text-align: left">
                                                    <span style="color: Black; font-size: 11px; font-weight: bold">Order Number:</span>
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
                                                        <telerik:GridBoundColumn DataField="SUPPLIER" HeaderText="Supp. Code" UniqueName="SUPPLIER"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SHORT_NAME" HeaderText="Supplier Name" UniqueName="SHORT_NAME"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="350px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="Status" HeaderText="Qtn.Rcvd." UniqueName="chkStatus"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="80px" />
                                                            <HeaderTemplate>
                                                                Qtn.Rcvd.<br />
                                                                Quoted Items
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
                                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Select Supp."
                                                            AllowFiltering="false" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkQuaEvaluated" Height="12px" Width="14px" runat="server" Checked='<%#Eval("EvalSupplier")%>'
                                                                    Font-Size="Smaller" AutoPostBack="false" Enabled="false" BorderStyle="None" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Supp_Tot_Amt" HeaderText="Supp Amt." UniqueName="Supp_Tot_Amt"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="true" Display="true">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Selected Item" Visible="True" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTotalItem" runat="server" Text="0.00"></asp:Label>
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
                                                        <telerik:GridTemplateColumn HeaderText="VAT" Visible="True" AllowFiltering="false">
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
                                                        <telerik:GridTemplateColumn DataField="Packing_Handling_Charges" HeaderText="Pkng Cost."
                                                            UniqueName="Packing_Handling_Charges" Visible="true" HeaderStyle-Width="0" AllowFiltering="true"
                                                            Display="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPkg" Text='<%#Eval("Packing_Handling_Charges")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblPkgRs" Visible="false" Text='<%#Eval("REASON_TRANS_PKG")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Freight_Cost" HeaderText="Freight Cost." UniqueName="Freight_Cost"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="true" Display="true">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="Other_Charges" HeaderText="Other Cost." UniqueName="Other_Charges"
                                                            Visible="true" HeaderStyle-Width="0" AllowFiltering="true" Display="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOtherCost" Text='<%#Eval("Other_Charges")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblOtherCostRs" Visible="false" Text='<%#Eval("Other_Charges_Reason")%>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Truck_Cost" HeaderText="Truck Cost" UniqueName="Truck_Cost">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Barge_Workboat_Cost" HeaderText="Barge/Work boat Cost"
                                                            UniqueName="Barge_Workboat_Cost">
                                                            <HeaderStyle Width="0px"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Final Amount" Visible="True" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtGrandTotal" runat="server" Text='<%#UDFLib.ConvertToDecimal(Eval("ORDER_AMOUNT").ToString()) < 1?0:Eval("ORDER_AMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" ForeColor="BlueViolet">
                                                            </ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Qtn. Remarks">
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgQuRemark" Height="12px" runat="server" AlternateText='<%#Eval("QUOTATION_COMMENTS")+"<br>Delivery Term:"+ Eval("DeliveryTerm")+"<br>Origin:"+Eval("Delivery_Origin") %>'
                                                                    ImageUrl="~/purchase/Image/view1.gif" Visible='<%# Convert.ToString(Eval("QUOTATION_COMMENTS")) == "" ? false : true %>' />
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
                <table align="center" cellpadding="0" cellspacing="0" width=" 100%">
                    <tr>
                        <td align="center" style="background-color: #CCCCCC; font-size: small;">
                            <table align="center" cellpadding="0" cellspacing="0" width=" 100%">
                                <tr>
                                    <td style="width: 229px" align="left">
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export" Style="font-size: 11px;
                                            height: 25px" OnClick="btnExportToExcel_Click" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td align="left">
                                        <div id="divInfoQTN" onscroll="checkAvailableWidth()">
                                            <telerik:RadGrid ID="rgdQuatationInfo" runat="server" AllowAutomaticInserts="True"
                                                BorderColor="White" OnItemDataBound="rgdQuatationInfo_ItemDataBound" OnNeedDataSource="rgdQuatationInfo_NeedDataSource"
                                                PageSize="500" AutoGenerateColumns="False" AllowMultiRowSelection="true" GridLines="Vertical"
                                                OnItemCreated="rgdQuatationInfo_ItemCreated">
                                                <ExportSettings OpenInNewWindow="true">
                                                    <Excel Format="Html" />
                                                </ExportSettings>
                                                <MasterTableView CellPadding="0" CellSpacing="0">
                                                    <RowIndicatorColumn Visible="False" CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="ITEM_SERIAL_NO" HeaderText="S.N." UniqueName="ITEM_SERIAL_NO"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" Width="20px" CssClass="gtdth" />
                                                            <HeaderStyle Width="20px" CssClass="hd" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ITEM_REF_CODE" UniqueName="ITEM_REF_CODE" Display="false"
                                                            AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                            <HeaderStyle Width="1px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="QUOTATION_CODE" HeaderText="Quatation No." UniqueName="QUOTATION_CODE"
                                                            Display="false" AllowFiltering="false">
                                                            <ItemStyle Width="100px" />
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn1" Display="true" AllowFiltering="false"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="display: none; border-right: 1px solid gray; font-weight: bold">
                                                                            Requisition No.
                                                                        </td>
                                                                        <td style="display: none; border-right: 1px solid gray; font-weight: bold">
                                                                            Catalogue
                                                                        </td>
                                                                        <td style="display: none; border-right: 1px solid gray; font-weight: bold">
                                                                            Sub Catalogue
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="display: none; border-right: 1px solid gray; font-weight: bold">
                                                                        </td>
                                                                        <td style="display: none; border-right: 1px solid gray; font-weight: bold">
                                                                        </td>
                                                                        <td style="display: none; border-right: 1px solid gray; font-weight: bold">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="display: none; border-right: 1px solid gray">
                                                                            <%#Eval("reqsnno")%>
                                                                        </td>
                                                                        <td style="display: none; border-right: 1px solid gray">
                                                                            <%#Eval("catalogue")%>
                                                                        </td>
                                                                        <td style="border: 1px; display: none">
                                                                            <asp:Label ID="lblsplit" Width="150px" Text='<%#Eval("Subsystem_Description")%>'
                                                                                runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1px" CssClass="gtdth" />
                                                            <HeaderStyle Width="1px" CssClass="hd" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Drawing_Number" Display="true" UniqueName="Drawing_Number"
                                                            HeaderText="Draw. No." Visible="True" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Center" Width="11px" CssClass="gtdth" />
                                                            <HeaderStyle Width="11px" CssClass="hd" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Part_Number" HeaderText="Part No." Display="true"
                                                            UniqueName="Part_Number" Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="11px" CssClass="gtdth" />
                                                            <HeaderStyle Width="11px" CssClass="hd" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="ITEM_SHORT_DESC" HeaderText="Item Name" UniqueName="ITEM_SHORT_DESC"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="180px" CssClass="gtdth" />
                                                            <HeaderStyle Width="180px" CssClass="hd" />
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lblItemDesc" Width="180px" runat="server" Target="_blank" Text='<%#Eval("ITEM_SHORT_DESC")%>'
                                                                    NavigateUrl='<%# "~/Purchase/Item_History.aspx?vessel_code="+ Eval("Vessel_Code").ToString()+"&item_ref_code="+Eval("ITEM_REF_CODE").ToString() %>'>&nbsp;&nbsp;  </asp:HyperLink>
                                                                <asp:Label ID="lblshortDesc" Text='<%#Eval("ITEM_SHORT_DESC")%>' Visible="false"
                                                                    runat="server"></asp:Label>
                                                                <asp:Label ID="lblLongDesc" Text='<%#Eval("Long_Description")%>' ToolTip='<%#Eval("ITEM_INTERN_REF")%>'
                                                                    Visible="false" runat="server"></asp:Label>
                                                                <asp:Label ID="lblComments" Text='<%#Eval("ITEM_COMMENT")%>' Visible="false" runat="server"></asp:Label>
                                                                <asp:Label ID="lblItemType" Text="Original" Visible="false" runat="server"> </asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="Unit_and_Packings" HeaderText="Unit" UniqueName="Unit_and_Packings"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="70px" HorizontalAlign="Center" CssClass="gtdth" />
                                                            <HeaderStyle Width="70px" CssClass="hd" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="Order_Unit" AllowFiltering="false" HeaderText="Order Unit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnUnitsPKg" runat="server" Enabled="false" Text='<%#Eval("Unit_and_Packings")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="70px" HorizontalAlign="Center" BackColor="#ffff99" CssClass="gtdth" />
                                                            <HeaderStyle Width="70px" CssClass="hd" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="ROB_QTY" HeaderText="ROB" UniqueName="ROB_QTY"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle BackColor="#ffe1e1" Width="50px" HorizontalAlign="Right" CssClass="gtdth" />
                                                            <HeaderStyle Width="50px" CssClass="hd" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="QUOTED_QTY" HeaderText="Reqst Qty" UniqueName="QUOTED_QTY"
                                                            Visible="True" AllowFiltering="false">
                                                            <ItemStyle Width="70px" HorizontalAlign="Right" CssClass="gtdth" />
                                                            <HeaderStyle Width="70px" CssClass="hd" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" Visible="true" HeaderText="Order Qty"
                                                            UniqueName="Order_qty1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtORqty" runat="server" Text='<%#Eval("ORDER_QTY")%>' ForeColor='<%# Eval("QUOTED_QTY").ToString()!=Eval("ORDER_QTY").ToString()?System.Drawing.Color.White:System.Drawing.Color.Black %>'
                                                                    BackColor='<%# Eval("QUOTED_QTY").ToString()!=Eval("ORDER_QTY").ToString()?System.Drawing.Color.Red:System.Drawing.Color.Transparent %>'
                                                                    Width="60px" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" CssClass="hd" />
                                                            <ItemStyle Width="40px" CssClass="gtdth" />
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="false">
                                                    <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                        <%--  </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table align="center" cellpadding="0" cellspacing="0" width=" 100%">
                    <tr style="background-color: #CCCCCC; font-size: small; color: #FFFFFF;">
                        <td>
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="background-color: #CCCCCC; font-size: small; color: #FFFFFF;">
                        <td style="width: 300px">
                            <asp:HiddenField ID="HiddenbtnRetrive" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenQuery" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenDocumentCode" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenChepSupp" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenItemIDs" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenFieldTotalAmountApproved" ViewStateMode="Enabled" runat="server" />
                            <asp:HiddenField ID="lblITEMSYSTEMCODE" runat="server" />
                            <asp:HiddenField ID="lblVesselCode" runat="server" />
                        </td>
                    </tr>
                </table>
            </center>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
