<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Payment_History.aspx.cs"
    Inherits="ASL_ASL_Payment_History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payment History</title>
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
                        Payment History
                    </div>
                
            <table width="100%" cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    <div  style="font-size: 15px; font-family: Tahoma;">
               Supplier Code -  <asp:Label ID="lblSupplierCode" runat="server" Width="100px"  Text=""></asp:Label>&nbsp;&nbsp;
                Supplier Name -  <asp:Label ID="lblSuppliername" runat="server" Width="400px"  Text=""></asp:Label>
                 </div>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                        <asp:GridView ID="gvPaymentHistory" runat="server" EmptyDataText="NO RECORDS FOUND"
                            AutoGenerateColumns="False" DataKeyNames="Payment_ID,PAYMENT_YEAR" CellPadding="1"
                            CellSpacing="0" Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true"
                            OnRowDataBound="gvPaymentHistory_RowDataBound">
                            <RowStyle CssClass="RowStyle-css" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table>
                                            <tr class="HeaderStyle-css" style="color: Blue;">
                                                <td align="left">
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("PAYMENT_MODE")%>'></asp:Label>&nbsp;
                                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("PAYMENT_STATUS")%>'></asp:Label>&nbsp;
                                                    <asp:Label ID="Label16" runat="server" Text=":"></asp:Label>&nbsp;
                                                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("Payment_ID")%>'></asp:Label>
                                                    <asp:Label ID="Label18" runat="server" Text="/"></asp:Label>&nbsp;
                                                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("Payment_Year")%>'></asp:Label>
                                                    <asp:Label ID="Label20" runat="server" Text=":"></asp:Label>&nbsp;
                                                    <asp:Label ID="Label21" runat="server" Text='<%#Eval("PAYMENT_CURRENCY")%>'></asp:Label>
                                                    <asp:Label ID="Label22" runat="server" Text='<%#Eval("PAYMENT_AMOUNT")%>'></asp:Label>
                                                    <asp:Label ID="Label24" runat="server" Text='<%#Eval("PAYMENT_DATE","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                    <asp:Label ID="Label25" runat="server" Text="for"></asp:Label>&nbsp;
                                                    <asp:Label ID="Label26" runat="server" Text='<%#Eval("INVOICE_COUNT")%>'></asp:Label>
                                                    <asp:Label ID="Label27" runat="server" Text="invoice(s)"></asp:Label>&nbsp;&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label28" runat="server" Text='<%#Eval("Account_Name")%>'></asp:Label>
                                                </td>
                                                
                                                <td align="right">
                                                    <asp:Label ID="Label32" runat="server" Text="Journal ID :"></asp:Label>&nbsp;
                                                    <asp:Label ID="Label30" runat="server" Text='<%#Eval("Journal_ID")%>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="left">
                                                    <asp:GridView ID="gvPaymentHistoryDetails" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                        AutoGenerateColumns="False" DataKeyNames="Payment_ID" CellPadding="1" CellSpacing="0"
                                                        Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                        <RowStyle CssClass="RowStyle-css" />
                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Recvd Date">
                                                                <HeaderTemplate>
                                                                    Recvd Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRecvd" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vessel">
                                                                <HeaderTemplate>
                                                                    Vessel
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Code">
                                                                <HeaderTemplate>
                                                                    Order Code
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrder" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inv Date">
                                                                <HeaderTemplate>
                                                                    Inv Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInv" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inv Due Date">
                                                                <HeaderTemplate>
                                                                    Inv Due Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDue" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
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
                                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type">
                                                                <HeaderTemplate>
                                                                    Type
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <HeaderTemplate>
                                                                    Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cur">
                                                                <HeaderTemplate>
                                                                    Currency
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment Due">
                                                                <HeaderTemplate>
                                                                    Payment Due
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPayment" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="SearchBindGrid" />
                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                    </div>
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    </form>
    </td></tr></table>
</body>
</html>
