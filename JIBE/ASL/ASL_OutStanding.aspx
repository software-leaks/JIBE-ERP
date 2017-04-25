<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_OutStanding.aspx.cs"
    Inherits="ASL_ASL_OutStanding" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Supplier OutStanding</title>
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
        function OpenScreen(Supply_ID, File_ID) {
            var url = 'ASL_Payment_HistoryDetails.aspx?Supply_ID=' + Supply_ID + '&File_ID=' + File_ID;
            OpenPopupWindowBtnID('ASL_Payment_HistoryDetails', 'Payment History Deatils', url, 'popup', 800, 1100, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
    </script>
     <script type="text/javascript">
         /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
         $(document).ready(function () {
             window.parent.$("#ASL_OutStanding").css("height", (parseInt($("#pnl").height()) + 50) + "px");
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
                        Supplier OutStandings
                    </div>
                
             <table width="100%" cellpadding="2" cellspacing="0">
             <tr>
               
               <td colspan="4" align="left">  <div  style="font-size: 15px; font-family: Tahoma;">
               Supplier Code -  <asp:Label ID="lblSupplierCode" runat="server" Width="100px"  Text=""></asp:Label>&nbsp;&nbsp;
                Supplier Name -  <asp:Label ID="lblSuppliername" runat="server" Width="400px"  Text=""></asp:Label>
                 </div>
                </td></tr>
            <tr>
                <td>
                    <div style="height: 560px; overflow-y: scroll; max-height: 560px">
                        <asp:GridView ID="gvPOOutstanding" runat="server" EmptyDataText="NO RECORDS FOUND"
                            AutoGenerateColumns="False" DataKeyNames="Supply_ID,Supplier_code" CellPadding="1"
                            CellSpacing="0" Width="100%" GridLines="both" AllowSorting="true">
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                            <Columns>
                                <asp:TemplateField HeaderText="PO Date">
                                    <HeaderTemplate>
                                        PO Date
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderCode" runat="server" Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Code">
                                    <HeaderTemplate>
                                        PO Code
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselType" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Count">
                                    <HeaderTemplate>
                                        Invoice Count
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceReference" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice USD Value">
                                    <HeaderTemplate>
                                        Invoice USD Value
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReference" runat="server" Text='<%#Eval("Invoice_USD_Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid USD Value">
                                    <HeaderTemplate>
                                        Paid USD Value
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Paid_USD_Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outstanding USD Value">
                                    <HeaderTemplate>
                                        Outstanding USD Value
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Outstanding_USD_Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Re-Calculate">
                                    <HeaderTemplate>
                                        Re-Calculate
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       
                                        <asp:Button ID="ImgCreditView" runat="server" Visible="false" CommandName="Select" OnClientClick='<%#"OpenScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;"+ Eval("[Supplier_code]") + "&#39;));return false;"%>'
                                         Text="Re-Calculate" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindPOOutstandingGrid" />
                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                    </div>
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <div style="height: 550px; overflow-y: scroll; max-height: 550px">
                        <asp:GridView ID="gvOutstanding" runat="server" EmptyDataText="NO RECORDS FOUND"
                            AutoGenerateColumns="False" DataKeyNames="Supplier_code" CellPadding="1" CellSpacing="0"
                            Width="100%" GridLines="both" AllowSorting="true">
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                            <Columns>
                                <asp:TemplateField HeaderText="Owner">
                                    <HeaderTemplate>
                                        Owner
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderCode" runat="server" Text='<%#Eval("Owner_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company">
                                    <HeaderTemplate>
                                        Company
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselType" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Count">
                                    <HeaderTemplate>
                                        Invoice Count
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceReference" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice USD Value">
                                    <HeaderTemplate>
                                        Invoice USD Value
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReference" runat="server" Text='<%#Eval("Invoice_USD_Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid USD Value">
                                    <HeaderTemplate>
                                        Paid USD Value
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Paid_USD_Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outstanding USD Value">
                                    <HeaderTemplate>
                                        Outstanding USD Value
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Outstanding_USD_Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindOutstandingGrid" />
                        <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                    </div>
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    </form></td></tr></table>
</body>
</html>
