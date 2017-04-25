<%@ Page Title="Final Invoice Approval" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Final_Invoice_Approver.aspx.cs" Inherits="Invoice_Final_Invoice_Approver" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../Purchase/styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/POLOG_Common_Function.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <%--<script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        .HeaderStyle-center
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
       
    </style>
   
    <script language="javascript" type="text/javascript">
        function OpenCompareScreen(SUPPLY_ID, Invoice_ID) {
            var PageName = "FinalApproval";
            var url = "../PO_LOG/PO_Log_Compare_Invoice.aspx?SUPPLY_ID=" + SUPPLY_ID + '&Invoice_ID=' + Invoice_ID + '&PageName=' + PageName
            window.open(url, "_blank");
        }
        function OpenScreen1(ID, Job_ID) {
            var Type = 'POLOG';
            var url = 'PO_Log_AuditTrail.aspx?Code=' + ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_AuditTrail', 'PO Log Transaction History', url, 'popup', 500, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen2(ID, Job_ID) {
            var Type = 'POLOG';
            var url = 'PO_Log_Supplier_Details.aspx?Code=' + ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_Supplier_Details', 'PO History', url, 'popup', 700, 1400, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenRemarksScreen(ID, Job_ID) {
            var Type = 'Invoice';
            var url = 'PO_Log_Remarks_History.aspx?Invoice_ID=' + ID + '&ID=' + Job_ID;
            OpenPopupWindowBtnID('PO_Log_Remarks_History', 'Remarks History', url, 'popup', 500, 900, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen5(ID, Job_ID) {
            var Type = 'POLOG';
            var url = 'PO_Log_Item_Entry.aspx?Code=' + ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_Supplier_Details', 'PO History', url, 'popup', 700, 1400, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
    </script>
     <script type="text/javascript">
         $("[id*=chkAllInvoice]").live("click", function () {
             var chkHeader = $(this);
             var grid = $(this).closest("table");
             $("input[type=checkbox]", grid).each(function () {
                 if (chkHeader.is(":checked")) {
                     $(this).attr("checked", "checked");
                     $("td", $(this).closest("tr")).addClass("selected");
                 } else {
                     $(this).removeAttr("checked");
                     $("td", $(this).closest("tr")).removeClass("selected");
                 }
             });
         });
         $("[id*=chkInvoice]").live("click", function () {
             var grid = $(this).closest("table");
             var chkHeader = $("[id*=chkAllInvoice]", grid);
             if (!$(this).is(":checked")) {
                 $("td", $(this).closest("tr")).removeClass("selected");
                 chkHeader.removeAttr("checked");
             } else {
                 $("td", $(this).closest("tr")).addClass("selected");
                 if ($("[id*=chkInvoice]", grid).length == $("[id*=chkInvoice]:checked", grid).length) {
                     chkHeader.attr("checked", "checked");
                 }
             }
         });
    </script>
   <script type="text/javascript">
       function Onfail(retval) {
           alert(retval._message);
       }


       function GetRemarkAll(Invoice_ID, UserID) {

           document.getElementById("__divViewRemark").innerHTML = "loading...";
           document.getElementById('newasynctxtRemark').value = "";
           SetPosition_Relative(evt, '__divAddReqsnRemark');
           if (lastExecutor_Remark != null)
               lastExecutor_Remark.abort();

           var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_Get_POLOG_Remarks', false, { "Supply_ID": Invoice_ID, "RemarkType": UserID }, onSuccess_GetPOLOG_Remarks, Onfail, new Array('dvShowPurchaserRemark', 'lblPologRemarks'))
           lastExecutor_Remark = service.get_executor();
       }
       function onSuccess_GetPOLOG_Remarks(retVal, Args) {

           document.getElementById("__divViewRemark").innerHTML = retVal;
           $("#__tbl_remark").scrollableTable({ type: "th" });

       }
       function onSucc_LoadFunction(retval, prm) {
           try {
               document.getElementById(prm[0]).innerHTML = retval;
               dvPruchremarkMain.
               checkForMyAction(prm[1], retval);
           }
           catch (ex)
            { }
       }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <progresstemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <contenttemplate>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
            <div id="page-title" class="page-title">
                Payment Approval List
            </div>
            <div style="padding-top: 5px; border: 1px padding-bottom: 5px;">
                <table width="100%" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="width: 10%" align="right">
                            Supplier :
                        </td>
                        <td style="width: 30%" align="left">
                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" Width="400px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%" align="right">
                            Vessel :
                        </td>
                        <td colspan="2" style="width: 30%" align="left">
                            <asp:DropDownList ID="ddlVessel" runat="server" CssClass="txtInput" Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%" align="right">
                           Urgent&nbsp;:
                        </td>
                        <td colspan="2" valign="top" style="width: 10%" align="left">
                            <asp:CheckBox ID="chkUrgent" runat="server"  Text="Urgent" ForeColor="Red" />
                        </td>
                        <td valign="top" style="width: 10%" align="right">
                            <asp:Button ID="btnApprovedInvoice" runat="server" Text="View Final Approved Invoice"
                                Width="200px" onclick="btnApprovedInvoice_Click1"  />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td valign="top" style="width: 10%" align="left">
                            <asp:Button ID="btnPendingInvoice" runat="server"  
                                Text="Pending Final Approved Invoice" Width="200px" 
                                onclick="btnPendingInvoice_Click" />
                                <asp:Button ID="btnInvoiceEntry" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="width: 10%" align="right">
                            <div style="float: left; text-align: left; width: 100%; height: 50px; overflow-x: hidden;
                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                background-color: #ffffff;">
                                <asp:Label ID="lblInvoice" runat="server" BackColor="Red" ForeColor="White" CssClass="txtInput"
                                    Text="PO Date > Invoice Date"></asp:Label>
                                <asp:CheckBoxList ID="chkType" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                </asp:CheckBoxList>
                                <br />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div id="divPendingInvoice" visible="false" runat="server" style="border: 1px solid #cccccc;
                font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
                <hr />
                <table style="width: 100%;">
                 
                    <tr>
                    <td align="left" style="font-size: medium;  color: red;">
                        Pending Final Approve Invoice</td>
                        <td align="right" style="font-size: medium; color: red;">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve Selected Invoice" Width="150px"
                                OnClick="btnApprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <asp:GridView ID="gvPendingInvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="Invoice_ID,Dispute_Flag" CellPadding="1" CellSpacing="0"
                                Width="100%" GridLines="both" AllowSorting="true" OnRowDataBound="gvPendingInvoice_RowDataBound"
                                OnSorting="gvPendingInvoice_Sorting">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderTemplate>
                                            No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("ROWNUM")%>'></asp:Label>
                                            <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                            <asp:Label ID="lblSupplierType" runat="server" Text='<%#Eval("SType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSupplier_Name" runat="server" CommandName="Sort" CommandArgument="Supplier_Name">Supplier Name</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbl_SupplierName" runat="server" OnClientClick='<%#"OpenScreen2(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("Supplier_Name") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Code" runat="server" CommandName="Sort" CommandArgument="PO_Code">Code</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblPOCode" runat="server" OnClientClick='<%#"OpenScreen1(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("PO_Code") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Amount" runat="server" CommandName="Sort" CommandArgument="PO_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("PO_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("CURRENCY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Count" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rec Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceived_Date" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblInvoice_Amount" runat="server" CommandName="Sort" CommandArgument="Invoice_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                            Confirmed Delivered
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredAmt" runat="server" Text='<%#Eval("Delivery_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Due Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPayment_Due_Date" runat="server" CommandName="Sort" CommandArgument="Payment_Due_Date"
                                                ForeColor="Black">Payment Due Date</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <table>
                                     <tr>
                                 <td style="border-color: transparent">
                                      <asp:Label ID="lblPaymentDate" runat="server"  Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                      <asp:Label ID="lblUrgentFlag" ForeColor="Red" runat="server" Text="|"></asp:Label>
                                      <asp:Label ID="lblUrgent" ForeColor="Red" runat="server" Text='<%#Eval("Urgent_Flag")%>'></asp:Label>
                                     </td></tr>
                                     <tr>
                                 <td style="border-color: transparent"> 
                                  <asp:Label ID="lblDays" ForeColor="Red" runat="server" Text=""></asp:Label>
                                 </td>
                                 </tr>
                                 </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                            
                                            <asp:CheckBox ID="chkAllInvoice" Visible="false" Text="Select All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInvoice" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Dispute">
                                        <HeaderTemplate>
                                          Dispute
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispute" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgViewRemarks" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="View Remarks" OnClientClick='<%#"OpenRemarksScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                          <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                     <td>
                                                         <asp:Button ID="btnDispute" runat="server" Text="Dispute" ToolTip="Dispute"  OnCommand="btnDispute_Click" CommandArgument='<%#Eval("[Invoice_ID]") + "," + Eval("[Dispute_Flag]")%>' />
                                                          <%--<asp:ImageButton ID="ImageButton1" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindPendingInvoiceGrid" />
                            <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                        </td>
                    </tr>
                </table>
            </div>
           <div id="divApprovedPayment" visible="false" runat="server" style="border: 1px solid #cccccc;
                font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
                <hr />
                <table style="width: 100%;">
                    <tr>
                     <td align="left" style="font-size: medium; color: red;">
                         Final Approved Invoice</td>
                        <td align="right" style="font-size: medium; color: red;">
                           <asp:Button ID="btnUnApprove" Visible="false" runat="server" Text="Un-Approve Selected Invoice"
                                Width="180px" OnClick="btnUnApprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                          
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <asp:GridView ID="gvApprovedInvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                                Width="100%" GridLines="both" AllowSorting="true" OnRowDataBound="gvApprovedInvoice_RowDataBound"
                                OnSorting="gvApprovedInvoice_Sorting">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderTemplate>
                                            No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("ROWNUM")%>'></asp:Label>
                                            <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                            <asp:Label ID="lblSupplierType" runat="server" Text='<%#Eval("SType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSupplier_Name" runat="server" CommandName="Sort" CommandArgument="Supplier_Name">Supplier Name</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbl_SupplierName" runat="server" OnClientClick='<%#"OpenScreen2(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("Supplier_Name") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Code" runat="server" CommandName="Sort" CommandArgument="PO_Code">Code</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblPOCode" runat="server" OnClientClick='<%#"OpenScreen1(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("PO_Code") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Amount" runat="server" CommandName="Sort" CommandArgument="PO_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("PO_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("CURRENCY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Count" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rec Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceived_Date" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblInvoice_Amount" runat="server" CommandName="Sort" CommandArgument="Invoice_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Final_Approved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_Date" runat="server" Text='<%#Eval("Final_Approval_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                            Confirmed Delivered
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredAmt" runat="server" Text='<%#Eval("Delivery_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Due Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPayment_Due_Date" runat="server" CommandName="Sort" CommandArgument="Payment_Due_Date"
                                                ForeColor="Black">Payment Due Date</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentDate" runat="server" ForeColor="Red" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            <asp:Label ID="lblUrgentFlag" runat="server" Text="|"></asp:Label>
                                            <asp:Label ID="lblUrgent" ForeColor="Red" runat="server" Text='<%#Eval("Urgent_Flag")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInvoice" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Dispute">
                                        <HeaderTemplate>
                                          Dispute
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispute" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgViewRemarks" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="View Remarks" OnClientClick='<%#"OpenRemarksScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                          <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                         <asp:Button ID="btnApprovedDispute" runat="server" Text="Dispute" ToolTip="Dispute"  OnCommand="btnApprovedDispute_Click" CommandArgument='<%#Eval("[Invoice_ID]") + "," + Eval("[Dispute_Flag]")%>' />
                                                          <%--<asp:ImageButton ID="ImageButton1" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager2" runat="server" PageSize="30" OnBindDataItem="BindApprovedInvoiceGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </td>
                    </tr>
                </table>
            </div>
            
                <div style="display: none;">
                    <asp:TextBox ID="txtInvoiceCurrency" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtPOCode" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtInvoiceCode" runat="server" Width="1px"></asp:TextBox>
                </div>
        </div>
         </contenttemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>

