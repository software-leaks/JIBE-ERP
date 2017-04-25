<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_PO_Invoice.aspx.cs" Inherits="ASL_ASL_PO_Invoice" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Supplier PO and Invoice</title>
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
                        Supplier PO & Invoice
                    </div>
               
            <table width="100%" cellpadding="2" cellspacing="0">
             <tr>
               
               <td colspan="3"> 
               <div  style="font-size: 15px; font-family: Tahoma;">
               Supplier Code -  <asp:Label ID="lblSupplierCode" runat="server" Width="100px"  Text=""></asp:Label>&nbsp;&nbsp;
                Supplier Name -  <asp:Label ID="lblSuppliername" runat="server" Width="400px"  Text=""></asp:Label>
                 </div>
              
               
               
                </td>
            </tr>
            <tr>
               
               <td colspan="3" style=" height:20Px;"></td>
            </tr>
            <tr>
               
                 <td align="right" style=" width:20%; ">
                   Vessel Name :</td>
                <td align="left" style=" width:10%; ">
                    <asp:DropDownList ID="ddlVessel" runat="server" Width="200px" CssClass="txtInput">
                                                        </asp:DropDownList>
                </td>
                <td align="left" style=" width:80%; "> <asp:Button ID="btnfilter" Text="Search" runat="server" 
                        onclick="btnfilter_Click"  /></td>
            </tr>
            <tr>
               
               <td colspan="3" style=" height:20Px;"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="height: 550px; overflow-y: scroll; max-height: 550px">
                        <asp:GridView ID="gvPOInvoice" runat="server" 
                            EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                            DataKeyNames="SUPPLY_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                            AllowSorting="true">
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
                                        <asp:Label ID="lblPODate" runat="server" Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vessel Name">
                                    <HeaderTemplate>
                                       Vessel Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Display_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Reference">
                                    <HeaderTemplate>
                                       PO Reference
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOReference" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Amount">
                                    <HeaderTemplate>
                                        PO Amount
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("Line_Amount")%>'></asp:Label>&nbsp;
                                         <asp:Label ID="Label1" runat="server" Text="|"></asp:Label>
                                        <asp:Label ID="Line_Amount" runat="server" Text='<%#Eval("Line_Currency")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="PO Closed on">
                                    <HeaderTemplate>
                                        PO Closed on Date
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPO_Closed" runat="server" Text='<%#Eval("PO_Closed_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Inv Type">
                                    <HeaderTemplate>
                                        Inv Type
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInv" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Inv Reference">
                                    <HeaderTemplate>
                                        Inv Reference
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReference" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Inv Date">
                                    <HeaderTemplate>
                                       Inv Date
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Inv Amount">
                                    <HeaderTemplate>
                                        Inv Amount
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                        
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="60px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Cur">
                                    <HeaderTemplate>
                                       Currency
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Cur" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
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
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Payment Date">
                                    <HeaderTemplate>
                                       Payment Date
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayment_Date" runat="server" Text='<%#Eval("Payment_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
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
        </asp:Panel>
    </div>
    </form>
    </td></tr></table>
</body>
</html>
