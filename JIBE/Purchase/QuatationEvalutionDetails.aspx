<%@ Page Title="Quotation Comparision " Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="QuatationEvalutionDetails.aspx.cs" Inherits="Purchase_Quotation_Evaluation_Details_Gridview" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Register Src="../UserControl/ucPurcQuotationApproval.ascx" TagName="ucApprovalUser"
    TagPrefix="ucUser" %>
<asp:Content ID="header" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Quotation_Evaluation_Details.js?v=123" type="text/javascript"></script>
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">        
        .float-left
    {                        
    font-size: 11px;
     border-collapse: collapse;
    border: 1px solid black;                  
    background-color:#CBCBCB !Important;
    color:Black !Important;    
    }    
        .QtnEval-Det-HeaderStyle-css
{
    background-color: #045FB4;
    font-size: 11px;
    color: White;
    font-family: Tahoma;
    border-left: 1px solid gray;
    border-top: 1px solid gray;
    padding: 2px 0px 2px 0px;
    font-weight: bold;
    z-index: 90;
    white-space: nowrap;
    padding-left: 2px;
}

.QtnEval-Det-AltHeaderStyle-css
{
    background-color: #610B38;
    font-size: 11px;
    color: White;
    font-family: Tahoma;
    border-left: 1px solid gray;
    border-top: 1px solid gray;
    font-weight: bold;
    padding: 2px 0px 2px 0px;
    z-index: 90;
    white-space: nowrap;
    padding-left: 2px;
}
         .QtnEval-Det-ItemStyle-css
         {
        background-color: White;
        font-size: 11px;
        color: Black;
        font-family: Tahoma;
        padding: 0px 2px 0px 0px;
        margin-right: -10px;
        }

        .QtnEval-Det-AltItemStyle-css
        {
        background-color: White;
        font-size: 11px;
        color: Black;
        font-family: Tahoma;
        padding: 0px 2px 0px 0px;
        }
        .hideleftborder {
        border-left-width: 0px;      
        text-align:left;              
        }
        
        .hiderightborder {
            border-right-width: 0px;            
            text-align:right;                        
        }
        .hiddencol
          {
            display: none;
          }
          
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
            top: 0px;
            position: relative;
        }
        .gtdth
        {
            z-index: 0;
            background-color: #F2F2F2;
            position: relative;
            cursor: default;
            left: 0px;
            border-collapse: collapse;
            padding-left: 3px;
            white-space: nowrap;
            color: Black;
            font-size: 11px;
            border: 0px solid white;
        }
        .hd
        {
            background-image: url(../images/suppliergridbk.png); /*background-color: #00868B;*/
            position: relative;
            cursor: default;
            z-index: 200;
            left: 0px;
            color: Black;
            font-size: 11px;
            border-collapse: collapse;
            border: 0px solid white;
            padding-left: 3px;
            white-space: nowrap;
        }
        .NewItem
        {
            background-color: Yellow;
        }
        
        .tdTooltip
        {
            border-bottom: 1px solid gray;
            width: 250px;
        }
        .tdHtip
        {
            font-weight: bold;
            text-align: right;
            font-size: 11px;
            font-family: Verdana;
            vertical-align: top;
        }
        .tdDtip
        {
            text-align: left;
            padding-left: 3px;
            font-size: 11px;
            font-family: Verdana;
            vertical-align: top;
        }
        .NotMatchingProvisionLimit
        {
            background-color: Red;
            color: Red;
            padding: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress" style="z-index: 9999">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
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
                <table align="center" cellpadding="0" cellspacing="0" width="100%" style="background-color: #808080;">
                    <tr>
                        <td style="font-size: small; color: #FFFFFF; text-align: right; padding-right: 20px">
                            <b style="font-size: small">Quotation Comparision - All figure in USD</b>
                        </td>
                        <td align="left" style="padding-left: 20px; color: Yellow; font-size: 11px; font-weight: bold">
                            Reqsn Type :
                            <asp:Label ID="lblReqsnType" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" align="center" style="width: 100%; border-collapse: collapse">
                    <tr align="center">
                        <td style="font-size: small" width="100%">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="70%">
                                        <table cellpadding="0" cellspacing="0" align="left" style="border-right: 1px solid gray;
                                            font-family: Tahoma; border-top: 1px solid gray; color: Black; background-color: #f4ffff"
                                            width="100%">
                                            <tr align="left">
                                                <td class="tdh">
                                                    Requisition No :
                                                </td>
                                                <td class="tdd">
                                                    <asp:HyperLink ID="lblReqNo" Target="_blank" runat="server"> </asp:HyperLink>
                                                </td>
                                                <td class="tdh">
                                                    Vessel :
                                                </td>
                                                <td class="tdd">
                                                    <asp:Label ID="lblVessel" runat="server"></asp:Label>
                                                </td>
                                                <td class="tdh">
                                                    Catalogue :
                                                </td>
                                                <td class="tdd">
                                                    <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td class="tdh">
                                                    Req. Date :
                                                </td>
                                                <td class="tdd">
                                                    <asp:Label ID="lblReqDate" runat="server"></asp:Label>
                                                </td>
                                                <td class="tdh">
                                                    Qtn. Due Date :
                                                </td>
                                                <td class="tdd">
                                                    <asp:Label ID="lblQuotDueDate" runat="server"></asp:Label>
                                                    <asp:Label ID="lblToDate" runat="server" Visible="false"></asp:Label>
                                                </td>
                                                <td class="tdh">
                                                    Total Items :
                                                </td>
                                                <td class="tdd">
                                                    <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="30%">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%; color: Black; background-color: #f4ffff;
                                            font-size: 11px">
                                            <tr>
                                                <td class="tdd">
                                                    <asp:HyperLink ID="lbtnPurchaserRemark" Text="Purchaser Remark" runat="server" onmouseout="CloseRemark();"
                                                        onmouseover="GetRemark('302');" ForeColor="Blue" Style="cursor: pointer"></asp:HyperLink>
                                                </td>
                                                <td class="tdd">
                                                    <asp:LinkButton ID="btnApprovalHistory" runat="server" OnClientClick="return divHistoryShow()"
                                                        Text="Track Approvals" />
                                                </td>
                                                <td rowspan="2" class="tdd">
                                                    <asp:LinkButton ID="btnItemHistory" runat="server" OnClientClick="return ItemsHistoryShow()"
                                                        Text="View catalogue history" />
                                                </td>
                                                <td style="width: 20px;" class="tdd" rowspan="2">
                                                    <asp:ImageButton ID="ImgAttachment" runat="server" Text="Select" ToolTip="View Attachments"
                                                        ImageUrl="~/purchase/Image/attach1.gif" Width="20px" OnClientClick="javascript:document.getElementById('dvpurcAttachments').style.display = 'block';return false;">
                                                    </asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdd">
                                                    <asp:LinkButton ID="lbtnAllremarks" runat="server" Text="All Remark"></asp:LinkButton>
                                                </td>
                                                <td class="tdd">
                                                    <asp:LinkButton ID="lbtnSendForApproval" runat="server" Text="Send For Approval"
                                                        OnClick="lbtnSendForApproval_Click"></asp:LinkButton>
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
                                        <div style="height: 180px; overflow: auto; margin-left: 0px; width: 100%;">
                                            <asp:GridView ID="rgdSupplierInfo" runat="server" GridLines="Both" Width="100%" AutoGenerateColumns="False"
                                                Font-Size="10px" CellPadding="2" CellSpacing="0" OnRowDataBound="rgdSupplierInfo_ItemDataBound"
                                                DataKeyNames="PortName,Currency,QUOTATION_CODE,ORDER_AMOUNT,Req_Qut_Status,SUPPLIER,Supp_Tot_Amt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Qtn.Ref.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQtnRef" runat="server" Style="overflow: hidden" ToolTip='<%#Eval("Supplier_Quotation_Reference") %>'
                                                                Text='<%#Eval("Supplier_Quotation_Reference") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Name" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsupplier_shortname" runat="server" Text='<%#Eval("SHORT_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="350px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtn.Rcvd." Visible="True">
                                                        <ItemStyle Width="80px" />
                                                        <HeaderTemplate>
                                                            Qtn.Rcvd.<br />
                                                            Quoted Items
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkStatus" Enabled="false" Checked='<%#Eval("Status").ToString()=="1"?true:false %>'
                                                                runat="server" />
                                                            <%#"&nbsp;&nbsp;"+Eval("TotalQTDItems")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select Supp." ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQuaEvaluated" Height="12px" Width="14px" runat="server" Checked='<%#Eval("EvalSupplier")%>'
                                                                Font-Size="Smaller" AutoPostBack="false" Enabled='<%#UDFLib.ConvertToDecimal(Eval("ORDER_AMOUNT").ToString()) < 1?true:false %>'
                                                                BorderStyle="None" />
                                                            <asp:HiddenField ID="hdfDISCOUNT" runat="server" Value='<%#Eval("DISCOUNT") %>' />
                                                            <asp:HiddenField ID="hdfEXCHANGE_RATE" runat="server" Value='<%#Eval("EXCHANGE_RATE") %>' />
                                                            <asp:HiddenField ID="hdfSURCHARGES" runat="server" Value='<%#Eval("SURCHARGES") %>' />
                                                            <asp:HiddenField ID="hdfVAT" runat="server" Value='<%#Eval("VAT") %>' />
                                                            <asp:HiddenField ID="hdfQUOTATION_CODE" runat="server" Value='<%#Eval("QUOTATION_CODE") %>' />
                                                            <asp:HiddenField ID="hdfSupplier_code" runat="server" Value='<%#Eval("SUPPLIER")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="Supp_Tot_Amt" HeaderText="Supp Amt." Visible="true" HeaderStyle-Width="0">
                                                        <HeaderStyle Width="0px"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                    </asp:BoundField>--%>
                                                     <asp:TemplateField HeaderText="Supp Amt." Visible="True">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblSuppTotAmt" runat="server" Text='<%#Eval("Supp_Tot_Amt")%>'></asp:Label>--%>
                                                            <asp:HiddenField ID="lblSuppTotAmtOld" runat="server" Value='<%#string.Format("{0:F2}",Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Supp_Tot_Amt")) - Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Tot_Vat")))%>' />                                                            
                                                            <asp:Label ID="lblSuppTotAmt" runat="server" Text='<%#string.Format("{0:F2}",Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Supp_Tot_Amt")) - Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Tot_Vat")))%>'></asp:Label>
                                                            
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="0px"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Selected Item" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtTotalItem" runat="server" Text="0.00"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Selected Items Amount" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtAmount" runat="server" Text="0.00"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount (%)" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtDiscount" runat="server" Text='<%#Eval("DISCOUNT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Surcharge" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtSurcharge" runat="server" Text="0.00"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VAT (%)" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtVat" runat="server" Text="0.00"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pkng Cost." Visible="true" HeaderStyle-Width="0">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="lblPkgOld" Value='<%#Eval("Packing_Handling_Charges")%>' runat="server" />
                                                            <asp:Label ID="lblPkg" Text='<%#Eval("Packing_Handling_Charges")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblPkgRs" Visible="false" Text='<%#Eval("REASON_TRANS_PKG")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Freight Cost." HeaderStyle-Width="0">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="lblFreight_CostOld" runat="server" Value='<%#Eval("Freight_Cost") %>' />
                                                            <asp:Label ID="lblFreight_Cost" runat="server" Text='<%#Eval("Freight_Cost") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Other Cost." Visible="true" HeaderStyle-Width="0">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="lblOtherCostOld" Value='<%#Eval("Other_Charges")%>' runat="server" />
                                                            <asp:Label ID="lblOtherCost" Text='<%#Eval("Other_Charges")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblOtherCostRs" Visible="false" Text='<%#Eval("Other_Charges_Reason")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Truck Cost">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="lblTruck_CostOld" runat="server" Value='<%#Eval("Truck_Cost") %>' />
                                                            <asp:Label ID="lblTruck_Cost" runat="server" Text='<%#Eval("Truck_Cost") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Barge/Work boat Cost">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="lblBarge_Workboat_CostOld" runat="server" Value='<%#Eval("Barge_Workboat_Cost") %>' />
                                                            <asp:Label ID="lblBarge_Workboat_Cost" runat="server" Text='<%#Eval("Barge_Workboat_Cost") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final Amount" Visible="True">
                                                    <HeaderTemplate>
                                                    Final Amount &nbsp;<asp:Label ID="lblTotNetAmount" Text="" runat="server"></asp:Label>
                                                    </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtGrandTotal" runat="server" Text='<%#UDFLib.ConvertToDecimal(Eval("ORDER_AMOUNT").ToString()) < 1?0:Eval("ORDER_AMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" ForeColor="BlueViolet" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtn. Remarks">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgQuRemark" Height="12px" runat="server" AlternateText='<%#Eval("QUOTATION_COMMENTS")+"<br>Delivery Term:"+ Eval("DeliveryTerm")+"<br>Origin:"+Eval("Delivery_Origin") %>'
                                                                ImageUrl="~/purchase/Image/view1.gif" Visible='<%# Convert.ToString(Eval("QUOTATION_COMMENTS")) == "" ? false : true %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ReWork">
                                                        <HeaderTemplate>
                                                            Rework:<br />
                                                            <asp:LinkButton ID="btnReworkPurclink" Style="font-size: 9px; color: Blue; font-family: Verdana"
                                                                Visible='<%# objUA.Edit != 0?true:false  %>' Text="Rework To Purchaser" runat="server"
                                                                OnClientClick="divReworkShow();return false;"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="border: 0px solid white">
                                                                        <asp:Button ID="btnRework" runat="server" Text="Rework to supplier" OnCommand="onRework"
                                                                            Font-Names="verdana" CommandName="onRework" CommandArgument='<%#Eval("SUPPLIER")%>'
                                                                            Enabled='<%#(UDFLib.ConvertToDecimal(Eval("ORDER_AMOUNT").ToString()) < 1 && Convert.ToInt32(Eval("SentToSupdt"))==0  )?true:false %>'
                                                                            Font-Size="10px" ToolTip="Send for Re-quotaion." Visible='<%#UDFLib.ConvertToDecimal(Eval("ORDER_AMOUNT").ToString()) < 1 && objUA.Edit != 0?true:false %>' />
                                                                        <asp:HyperLink ID="hplordercode" runat="server" ForeColor="Blue" Font-Size="10px"
                                                                            Font-Bold="true" Text='<%#Eval("ORDER_CODE")%>' Target="_blank" NavigateUrl='<%# "POPreview.aspx?RFQCODE="+Request.QueryString["Requisitioncode"].ToString() +"&Vessel_Code="+ Request.QueryString["Vessel_Code"].ToString()+"&Order_Code="+ Eval("ORDER_CODE") %> '
                                                                            Visible='<%#UDFLib.ConvertToDecimal(Eval("ORDER_AMOUNT").ToString()) > 0 ?true:false %>'></asp:HyperLink>
                                                                    </td>
                                                                    <td style="border: 0px solid white">
                                                                        <img id="imgremark" style="display: none" alt="remark" src="../Images/Commentnew.png"
                                                                            height="16px" width="16px" onmouseout="CloseRemark();" onmouseover="GetRemark('300');" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtn.Attach">
                                                      <ItemTemplate>
                                                        <asp:HyperLink ID="ImgAttachment" runat="server" Height="20px" Width="20px" ImageUrl ="../Images/attachment.png" style="border:0px solid white" onclick='<%# "Attchment(this,event,&#39;"+ Eval("Supplier") +"&#39;);" %>'>
                                                        </asp:HyperLink>                                                                                                                  
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                </Columns>
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" Wrap="true" />
                                                <HeaderStyle CssClass="suppgridHeaderStyle-css" />
                                            </asp:GridView>
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
                                    <td align="left">
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export" Style="font-size: 11px;
                                            height: 25px" OnClick="btnExportToExcel_Click" Font-Bold="True"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnReq_Ord" Style="font-size: 11px; font-family: Verdana; color: #0000cc;"
                                            Text="Make Order Qty same as Requested Qty" OnClientClick="check_changesOnUI('Fill_QrdQty_With_ReqQty()','0') ;return false"
                                            runat="server" Width="240px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRetrieve" runat="server" Text="Compare Quotation" Style="font-size: 11px;
                                            height: 25px" OnClick="btnRetrieve_Click" Font-Bold="True" OnClientClick=" return check_changesOnUI(id,'1')" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="optEvalCSupp" GroupName="Option" Text="Cheapest by Supplier"
                                            runat="server" Style="font-size: 11px; font-weight: 700; color: Black" onclick="javascript:check_changesOnUI( function() {CalculateByEvalOpt('0','0')},'0')" />
                                        <asp:RadioButton ID="optEvalCItm" GroupName="Option" Text="Cheapest by Item" runat="server"
                                            Style="font-size: 11px; font-weight: 700; color: Black" onclick="javascript:check_changesOnUI( function() {CalculateByEvalOpt('1','0')},'0')" />
                                            <asp:CheckBox ID="chkIncludeVat" runat="server" Text="Include Vat" Checked="false" Style="font-size: 11px; font-weight: 700; color: Black" onclick="onIncludeVat('0');" />
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnSaveEvaln" runat="server" Style="font-size: 11px; height: 25px;
                                            font-weight: bold" Text="Save Qtn Evaluation" OnClientClick="return UpdateEvalution();"
                                            OnClick="btnSaveEvaln_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btnFinalizeEval" runat="server" OnClick="btnFinalizeEval_Click" OnClientClick="return UpdateEvalution();"
                                            Style="font-size: 11px; height: 25px" Text="Assign Budget code and Approve" Font-Bold="True"
                                            Width="200px" />
                                        <br />
                                        <asp:Label ID="lblActionmsg" ForeColor="Red" Font-Italic="true" Font-Names="verdana"
                                            Font-Size="11px" runat="server"></asp:Label>
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
                                            <asp:GridView ID="rgdQuatationInfo" runat="server" OnRowDataBound="rgdQuatationInfo_ItemDataBound"
                                                ViewStateMode="Disabled" PageSize="500" AutoGenerateColumns="False" GridLines="Both"
                                                BackColor="LightGray" CellPadding="0" CellSpacing="0" DataKeyNames="ITEM_REF_CODE"
                                                OnRowCreated="rgdQuatationInfo_ItemCreated">
                                                <Columns>
                                                    <asp:BoundField DataField="ITEM_SERIAL_NO" HeaderText="S.N." Visible="True">
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="gtdth" />
                                                        <HeaderStyle Width="20px" CssClass="hd" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ITEM_REF_CODE" Visible="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                        <HeaderStyle Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QUOTATION_CODE" HeaderText="Quatation No." Visible="false">
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <table border="1" style="border-collapse: collapse">
                                                                <tr>
                                                                    <td style="display: none; font-weight: bold; color: White">
                                                                        Requisition No.
                                                                    </td>
                                                                    <td style="display: none; font-weight: bold; color: White">
                                                                        Catalogue
                                                                    </td>
                                                                    <td style="display: none; font-weight: bold; color: White">
                                                                        Sub Catalogue
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="display: none; font-weight: bold">
                                                                    </td>
                                                                    <td style="display: none; font-weight: bold">
                                                                    </td>
                                                                    <td style="display: none; font-weight: bold">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table border="1" style="border-collapse: collapse">
                                                                <tr>
                                                                    <td style="display: none;">
                                                                        <%#Eval("reqsnno")%>
                                                                    </td>
                                                                    <td style="display: none;">
                                                                        <%#Eval("catalogue")%>
                                                                    </td>
                                                                    <td style="display: none">
                                                                        <asp:Label ID="lblsplit" Width="150px" Text='<%#Eval("Subsystem_Description")%>'
                                                                            runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1px" CssClass="gtdth" />
                                                        <HeaderStyle Width="1px" CssClass="hd" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Drawing_Number" HeaderText="Draw. No." Visible="True">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="gtdth" />
                                                        <HeaderStyle CssClass="hd" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Part_Number" HeaderText="Part No." Visible="True">
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="gtdth" />
                                                        <HeaderStyle CssClass="hd" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemStyle Width="180px" CssClass="gtdth" />
                                                        <HeaderStyle Width="180px" CssClass="hd" />
                                                        <ItemTemplate>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="border: 0px 0px 0px 0px">
                                                                        <asp:Image ID="imgNotDeliveredItem" AlternateText='<%#Eval("ITEM_SHORT_DESC").ToString()+" ; "+Eval("Long_Description").ToString()%>'
                                                                            ImageUrl="~/Images/Flag_ON.png" Visible='<%# Eval("NotDeliverd").ToString()!="0"?true:false %>'
                                                                            Width="14px" runat="server" />
                                                                    </td>
                                                                    <td style="border: 0px 0px 0px 0px">
                                                                        <asp:HyperLink ID="lblItemDesc" Width="180px" runat="server" Target="_blank" Text='<%#Eval("ITEM_SHORT_DESC")%>'
                                                                            CssClass="" NavigateUrl='<%# "~/Purchase/Item_History.aspx?vessel_code="+ Eval("Vessel_Code").ToString()+"&item_ref_code="+Eval("ITEM_REF_CODE").ToString() %>'>&nbsp;&nbsp;  </asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Label ID="lblshortDesc" Text='<%#Eval("ITEM_SHORT_DESC")%>' Visible="false"
                                                                ToolTip='<%#Eval("Subsystem_Description") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblLongDesc" Text='<%#Eval("Long_Description")%>' ToolTip='<%#Eval("ITEM_INTERN_REF")%>'
                                                                Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblComments" Text='<%#Eval("ITEM_COMMENT")%>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblItemType" Text="Original" Visible="false" runat="server"> </asp:Label>
                                                            <asp:HiddenField ID="hdfItemRef_Code" runat="server" Value='<%#Eval("ITEM_REF_CODE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Unit_and_Packings" HeaderText="Unit" Visible="True">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="gtdth" />
                                                        <HeaderStyle CssClass="hd" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Last Ord">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnUnitsPKg" runat="server" OnClientClick="ShowUnitsPkg(id);return false;" Visible="false"
                                                                Text='<%#Eval("ORDER_UNIT_ID")%>'></asp:LinkButton>
                                                                <asp:Label ID="lblLastOrderAmout" runat="server"
                                                                Text='<%#Eval("LAST_ORDER_AMOUNT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" BackColor="#ffff99" CssClass="gtdth" />
                                                        <HeaderStyle CssClass="hd" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ROB_QTY" HeaderText="ROB">
                                                        <ItemStyle BackColor="#ffe1e1" Width="50px" HorizontalAlign="Right" CssClass="gtdth" />
                                                        <HeaderStyle CssClass="hd" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Reqst Qty">
                                                        <ItemStyle HorizontalAlign="Right" CssClass="gtdth" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReqs_tQty" runat="server" Text='<%#Eval("QUOTED_QTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="hd" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true" HeaderText="Order Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtORqty" runat="server" Text='<%#Eval("ORDER_QTY")%>' Width="60px"
                                                                BorderStyle="NotSet" ForeColor='<%# Eval("QUOTED_QTY").ToString()!=Eval("ORDER_QTY").ToString()?System.Drawing.Color.White:System.Drawing.Color.Black %>'
                                                                BackColor='<%# Eval("QUOTED_QTY").ToString()!=Eval("ORDER_QTY").ToString()?System.Drawing.Color.Red:System.Drawing.Color.White %>'
                                                                Style="text-align: right" Font-Size="11px" onKeydown="goDownUP(this,event,'txtORqty');return MaskMoney(event);"
                                                                onblur="JavaScript:Calculate_ordqty_rate_changed(event);"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="hd" />
                                                        <ItemStyle CssClass="gtdth" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                            <asp:HiddenField ID="hdfSupplierBeingApproved" runat="server" />
                            <asp:HiddenField ID="hdfMaxQuotedAmount" runat="server" />
                            <asp:HiddenField ID="lblVesselCode" runat="server" />
                            <asp:HiddenField ID="hdfOrderAmounts" runat="server" />
                            <asp:HiddenField ID="hdfquotation_codes_compare" runat="server" />
                            <asp:HiddenField ID="hdf_QtnCode_FinalAmount" runat="server" />
                            <asp:HiddenField ID="hdf_firstTime_clicked" Value="1" runat="server" />
                            <asp:HiddenField ID="hdfquotation_codes_RowNum_compare" runat="server" />
                        </td>
                    </tr>
                </table>
            </center>
            <div id="divOnSplit" style="border: 1px solid Black; display: none; background-color: #E0E0E0;
                position: absolute; left: 35%; top: 40%; z-index: 2; color: black; height: 150px;
                width: 450px;" runat="server">
                <center>
                    <table border="1" style="height: 50px; width: 440px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table style="width: 440px" cellpadding="0" cellspacing="0">
                                    <tr align="center">
                                        <td style="background-color: #808080; font-size: small;">
                                            <asp:Label ID="lblUrgencyTitle" Width="152px" runat="server" Text="Requisition's Items Split"
                                                Style="color: #FFFFFF; font-weight: 700; font-size: small;"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 16px; background-color: #808080;">
                                            <%--<asp:ImageButton ID="ImageButton1" OnClick="btndivCancel_Click" ImageUrl="~/Technical/INV/Image/Close.gif"
                                                    runat="server" Style="font-size: small" Width="12px" />
                                            --%>
                                            <img src="Image/Close.gif" alt="Click to close." width="12px" height="12px" onclick="JavaScript:CloseDiv();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 440px" cellpadding="0" cellspacing="0">
                                    <tr style="font-size: small">
                                        <td style="font-size: small;">
                                            Reason for Split:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="297px" Height="70"
                                                MaxLength="199"></asp:TextBox>
                                        </td>
                                        <td style="color: #FF0000; font-size: small;" align="left">
                                            *
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 440px" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btndivSave" runat="server" Text="Save" Height="24px" Font-Size="XX-Small"
                                                OnClick="btndivSave_Click" Width="48px" />
                                        </td>
                                        <td>
                                            <%-- <asp:Button ID="btndivCancel" runat="server" Text="Cancel" Height="24px" Font-Size="XX-Small"
                                                    OnClick="btndivCancel_Click" />--%>
                                            <input type="button" name="btnCancel" style="font-size: small" onclick="JavaScript:CloseDiv();"
                                                value="Cancel" />
                                        </td>
                                        <td style="width: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="color: #FF0000; font-size: small;" align="left">
                                            * Indicates as Mandatory fields
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <div id="divApprove" style="width:700px; display: none; z-index: 300; color: black"
                title="Assign budget code and Approve">
                <asp:UpdatePanel ID="updBGT" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" style="height: 100px; width: 100%; padding: 10px">
                            <tr>
                                <td style="border-bottom: 1px solid gray">
                                    <table style="width: 100%; height: 58px;" cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td align="right" style="font-size: small">
                                                Requisition Type:&nbsp;
                                            </td>
                                            <td style="width: 300px" align="left">
                                                <asp:DropDownList ID="ddlReqsnType" runat="server" DataSourceID="objsrcReqsnType"
                                                    AutoPostBack="true" DataTextField="Description" DataValueField="code" Width="400px"
                                                    OnSelectedIndexChanged="ddlReqsnType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="objsrcReqsnType" runat="server" SelectMethod="Get_ReqsnType"
                                                    TypeName="ClsBLLTechnical.TechnicalBAL"></asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="font-size: small">
                                                Budget code:<span style="color: Red">*</span>
                                            </td>
                                            <td style="width: 400px" align="left">
                                                <asp:DropDownList ID="ddlBudgetCode" runat="server" Style="font-size: small" Width="400px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlBudgetCode" Display="Dynamic"
                                                    ControlToValidate="ddlBudgetCode" InitialValue="0" ValidationGroup="finalapprl"
                                                    runat="server" ErrorMessage="Please select budget code."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="font-size: small">
                                                Approver's Remarks: <span style="color: Red">*</span>
                                            </td>
                                            <td align="left" style="width:400px">
                                                <asp:TextBox ID="txtComment" runat="server" Height="40px" TextMode="MultiLine" Width="400px"
                                                    Style="font-size: small"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtComment" ValidationGroup="finalapprl"
                                                    ControlToValidate="txtComment" Display="Dynamic" runat="server" ErrorMessage="Please enter comment."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="padding: 10px 0px 10px 0px">
                                                <asp:Button ID="btnApprove" runat="server" ValidationGroup="finalapprl" Text="Approve"
                                                    Height="35px" Width="100px" OnClick="btnApprove_Click" OnClientClick="UpdateEvalution();"
                                                    Style="font-size: small" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="center" colspan="2" style="padding: 10px 0px 10px 0px">
                                                <asp:Button ID="btnRequestAmount" runat="server"   Text="Request For Budget Limit Increase"
                                                    Height="35px" Width="250px" 
                                                    Style="font-size: small" onclick="btnRequestAmount_Click" />
                                            </td>
                                        </tr>
                                          <tr>
                                            <td align="center" colspan="2" style="padding: 10px 0px 10px 0px">
                                               <asp:Label ID="lblBudgetMsg" runat="server" ForeColor="Red" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                     <asp:HiddenField ID="hdnBudgetCode" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div id="dvSendForApproval" style="position: fixed; left: 30%; top: 15%; padding: 10px 0px 10px 0px;
                        border: 1px solid gray; z-index: 300" runat="server" class="popup-css" visible="false">
                        <ucUser:ucApprovalUser ID="ucApprovalUser1" OnstsSaved="OnStsSaved" runat="server" />
                    </div>
                    <div id="dvSendTosuppdt" style="position: fixed; left: 30%; top: 15%; padding: 10px 0px 10px 0px;
                        width: 600px; border: 1px solid gray; z-index: 300" runat="server" class="popup-css"
                        visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 100%; text-align: left">
                                    <asp:GridView ID="gvQuotationList" runat="server" Font-Size="11px" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qtn Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblqtn" runat="server" Text='<%# Bind("QUOTATION_CODE") %>' BackColor='<%#Eval("active_PO").ToString()=="1"?System.Drawing.Color.Silver:System.Drawing.Color.Transparent %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("Full_NAME") %>' BackColor='<%#Eval("active_PO").ToString()=="1"?System.Drawing.Color.Silver:System.Drawing.Color.Transparent %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Port">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPort" runat="server" Text='<%# Bind("PORT_NAME") %>' BackColor='<%#Eval("active_PO").ToString()=="1"?System.Drawing.Color.Silver:System.Drawing.Color.Transparent %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="active_PO" Visible="False" />
                                        </Columns>
                                        <HeaderStyle ForeColor="White" BackColor="#5D7B9D" />
                                    </asp:GridView>
                                    <span style="font-size: 11px; font-weight: bold; font-family: Verdana">Select BGT Code:</span>
                                    <asp:DropDownList ID="ddlBGTCodeToSuppdt" runat="server" Style="font-size: small"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <hr />
                                    <ucUser:ucApprovalUser ID="ucApprovalUserToSuppdt" OnstsSaved="OnStsSavedSentToApprover"
                                        runat="server" />
                                    <asp:HiddenField ID="HiddenFieldSuppdtRemark" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvUnitspkg" style="display: none; position: fixed; left: 25%; top: 21%;
                padding: 30px; border: 1px solid gray; z-index: 310; background-color: Teal">
                <asp:DropDownList ID="cmbUnitnPackage" AutoPostBack="false" runat="server" Width="70px"
                    DataTextField="Main_Pack" DataSourceID="ObjectDataSource1" DataValueField="Main_Pack"
                    Font-Size="11px">
                </asp:DropDownList>
                <input type="button" id="UnitOk" value="Ok" onclick="ChangeUnitsPkg();"> </input>
                <input type="button" id="Unitcancel" value="Cancel" onclick="CloseDvUnits()"> </input>
                <asp:ObjectDataSource ID="ObjectDataSource1" SelectMethod="SelectUnitnPackageDataSet"
                    TypeName="SMS.Business.PURC.BLL_PURC_Purchase" runat="server"></asp:ObjectDataSource>
            </div>
            <div id="dvReworkToPurc" class="popup-css" style="display: none; left: 35%; top: 25%;
                position: fixed; padding-top: 10px; padding-left: 30px; padding-right: 30px;
                padding-bottom: 30px; color: White; text-align: right; height: 100px; width: 400px;
                border: 1px solid black; z-index: 500; text-align: left; color: Black">
                <spam style="font-weight: bold; font-size: 11px; color: Black; padding-bottom: 10px"> Remark on rework: </spam>
                <asp:TextBox ID="txtRemarkToPurc" TextMode="MultiLine" runat="server" Height="60px"
                    Width="400px"></asp:TextBox>
                <asp:Button ID="btnReworktopurc" runat="server" Text="OK" Height="30px" Width="80px"
                    OnClick="btnReworktopurc_Click" OnClientClick="javascript:return confirm('are you sure to send for rework !')" />
                <input id="btnCancelRWK" style="height: 30px" value="Cancel" type="button" onclick="javascript:DivReworkClose();return false" />
            </div>
            <div id="dvReworktoSuppler" class="popup-css" style="left: 35%; top: 25%; position: fixed;
                padding-top: 10px; padding-left: 30px; padding-right: 30px; padding-bottom: 30px;
                color: White; text-align: right; height: 100px; width: 400px; border: 1px solid black;
                z-index: 500; text-align: left; color: Black; display: none;">
                <spam style="font-weight: bold; font-size: 11px; color: Black; padding-bottom: 10px"> Remark on rework: </spam>
                <asp:TextBox ID="txtReworkToSupplier" TextMode="MultiLine" runat="server" Height="60px"
                    Width="400px"></asp:TextBox>
                <asp:Button ID="btnReworkToSupplier" runat="server" Text="OK" Height="30px" Width="80px"
                    OnClick="btnReworkToSupplier_Click" OnClientClick="javascript:return confirm('are you sure to send for rework !')" />
                <input id="btncancelRWKSUPP" style="height: 30px" value="Cancel" type="button" onclick="DivReworkSuppClose()" />
                <asp:HiddenField ID="hdfSuppCode" runat="server" />
                <asp:HiddenField ID="hdfQTNCode" runat="server" />
            </div>
            <div id="divReasonForProvisionLimit" style="width: 500px; display: none; z-index: 300;
                color: black" title="Reason for approving items with more than defined limit">
                <table width="100%" cellpadding="4">
                    <tr>
                        <td style="width: 20%; text-align: right; padding: 2px">
                            Reason :
                        </td>
                        <td style="width: 80%; text-align: left; padding: 2px">
                            <asp:TextBox ID="txtReasonForProvisionLimit" Width="100%" TextMode="MultiLine" Height="50px"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReasonForProvisionLimit" runat="server" ValidationGroup="ReasonForProvisionLimit" ControlToValidate="txtReasonForProvisionLimit" ErrorMessage="Please enter reason !"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                        <asp:Button ID="btnReasonForProvisionLimit" Text="Save" runat="server" OnClick="btnReasonForProvisionLimit_Click" ValidationGroup="ReasonForProvisionLimit" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdDocumentCode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvPruchremarkMain" style="display: none; position: fixed; left: 30%; top: 10%;
        border: 1px solid gray; padding: 10px; background-color: White; z-index: 600"
        class="popup-css">
        <table>
            <tr>
                <td>
                    <div id="dvShowPurchaserRemark" style="position: relative">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="font-size: 12px; font-family: Verdana; font-weight: bold">
                                Remark:
                            </td>
                            <td>
                                <textarea id="txtRemark" cols="40" rows="5" style="width: 490px; height: 60px"></textarea>
                                <%--<input id="txtRemark" type="text" style="width: 490px; height: 60px" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <input id="btnSaveRemark" onclick="SavePurcReamrk();" type="button" style="height: 30px;
                                    width: 80px" value="Save" />&nbsp;&nbsp
                                <input id="btnCancelRemark" onclick="CloseRemarkAll();" type="button" value="Close"
                                    style="height: 30px; width: 80px" value="Save" />
                                <input id="hdfUserID" type="hidden" />
                            </td>
                        </tr>
                    </table>
                    <input id="hdfDocumentCode" type="hidden" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPurchaseRemark" style="border: 1px solid gray; z-index: 600; color: Black;
        display: none; position: fixed; left: 400px; top: 100px; background-color: White"
        class="Tooltip-css">
    </div>
    <div id="divHistory" class="popup-css" style="display: none; left: 35%; top: 25%;
        position: fixed; padding-top: 10px; padding-left: 30px; padding-right: 30px;
        padding-bottom: 30px; color: White; text-align: right; max-height: 400px; max-width: 700px;
        overflow: auto; border: 1px solid black; z-index: 456">
        <img id="imgdivclose" onclick="DivHistoryClose()" alt="close" title="close" src="../Images/Close.gif" />
        <asp:GridView ID="gvApprovalHistory" AutoGenerateColumns="true" EmptyDataText="no record found"
            CellPadding="4" OnRowCreated="gvApprovalHistory_RowCreated" runat="server" OnDataBound="gvApprovalHistory_DataBound">
            <HeaderStyle CssClass="HeaderStyle-css" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <RowStyle HorizontalAlign="Center" CssClass="RowStyle-css" />
        </asp:GridView>
    </div>
    <div id="dvpurcAttachments" style="border: 1px solid gray; z-index: 502; color: Black;
        padding: 5px; width: 600px; display: none; position: fixed; left: 20%; top: 100px;
        text-align: right" class="popup-css">
        <img onclick="javascript:document.getElementById('dvpurcAttachments').style.display = 'none';"
            alt="close" src="../Images/Close.gif" />
        <table width="100%" style="text-align: left">
            <asp:Repeater ID="rpAttachment" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("SlNo") %>.
                        </td>
                        <td>
                            <asp:HyperLink ID="lnkAtt" Target="_search" Font-Size="11px" Font-Names="verdana"
                                runat="server" NavigateUrl='<%# Eval("File_Path")%>'> <%# Eval("File_Name")%>  </asp:HyperLink>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>     
    <asp:HiddenField ID="hdfUserIDSaveEval" runat="server" />
    <script type="text/javascript">
        function Attchment(objthis, evt, scode) {                 
            var Req = '<%=Request.QueryString["Requisitioncode"]%>';
            var VESSEL_ID = '<%=Request.QueryString["Vessel_Code"]%>';
            asyncGet_Purc_Supplier_Attachments(objthis, evt, Req, VESSEL_ID, scode);
        }

        function OpenAttachment(queryString) {
            parent.OpenPopupWindowBtnID('ReqsnAttachment_ID', 'Requisition Attachments', 'ReqsnAttachment.aspx?' + queryString, 'popup', 500, 800, null, null, false, false, true, false, 'id');
        }
    </script> 
</asp:Content>
