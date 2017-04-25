<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Invoice_WIP.aspx.cs"
    Inherits="ASL_ASL_Invoice_WIP" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invoice WIP</title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script>
        function OpenScreen(Invoice_ID, Supply_ID) {
            var url = 'ASL_Payment_HistoryDetails.aspx?Invoice_ID=' + Invoice_ID + '&Supply_ID=' + Supply_ID;
            OpenPopupWindowBtnID('ASL_Payment_HistoryDetails', 'Payment History Deatils', url, 'popup', 800, 1100, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
    </script>
     <script type="text/javascript">
         /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
         $(document).ready(function () {
             window.parent.$("#ASL_Evalution").css("height", (parseInt($("#pnl").height()) + 50) + "px");
             window.parent.$(".xfCon").css("height", (parseInt($("#pnl").height()) + 50) + "px").css("top", "50px");
         });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnl" runat="server" Visible="true">
           
                
                        <div id="Div1" class="page-title">
                            Invoice WIP
                        </div>
                    
                 <table width="100%" cellpadding="2" cellspacing="0">
                   <tr>
               
               <td align="left">  <div  style="font-size: 15px; font-family: Tahoma;">
               Supplier Code -  <asp:Label ID="lblSupplierCode" runat="server" Width="100px"  Text=""></asp:Label>&nbsp;&nbsp;
                Supplier Name -  <asp:Label ID="lblSuppliername" runat="server" Width="400px"  Text=""></asp:Label>
                 </div>
                </td>
            </tr>
                <tr>
                    <td align="left">
                        <div style="font-size: 15px; font-family: Tahoma;">
                            This report will show the supplier's invoices that is currently under process (not
                            paid); it's current location and person incharge.</div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div style="font-size: 15px; font-family: Tahoma;">
                            Given the complexity of the invoice approval workflow, the report can only provide
                            a general status and will not provide more in depth details.</div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20Px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height: 550px; overflow-y: scroll; max-height: 550px">
                            <asp:GridView ID="gvPOInvoiceWIP" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="SUPPLY_ID,Invoice_ID" CellPadding="1"
                                CellSpacing="0" Width="100%" GridLines="both" AllowSorting="true">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="PO Code">
                                        <HeaderTemplate>
                                            PO Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOCode" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                            Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%#Eval("invoice_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference">
                                        <HeaderTemplate>
                                            Reference
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReference" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received">
                                        <HeaderTemplate>
                                            Received
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceived" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Due Date">
                                        <HeaderTemplate>
                                            Payment Due Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayment" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Amount">
                                        <HeaderTemplate>
                                            Invoice Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                          
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Currency">
                                        <HeaderTemplate>
                                            Invoice Currency
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         
                                            <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            Workflow Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkflowStatus" runat="server" Text='<%#Eval("InvoiceApprovalStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending with">
                                        <HeaderTemplate>
                                            Pending with
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPending" runat="server" Text='<%#Eval("PendingWith")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending with">
                                        <HeaderTemplate>
                                            Accessible from Module
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="110px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Upload">
                                        <HeaderTemplate>
                                            View
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="ImgInvView" runat="server"   OnCommand="btnInvoice_Click"
                                                CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[Invoice_ID]")  %>'
                                                Text="PO/Invoice" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="SearchBindGrid" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                    </td>
                </tr>
            </table>
            <div style="display:none;">
            <asp:TextBox ID="txtPasskey" runat="server"></asp:TextBox>
            
            </div>
        </asp:Panel>
    </div>
    </form></td></tr></table>
</body>
</html>
