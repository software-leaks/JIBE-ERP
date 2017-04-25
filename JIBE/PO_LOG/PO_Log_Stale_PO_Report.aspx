<%@ Page Title="Stale PO Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PO_Log_Stale_PO_Report.aspx.cs" Inherits="PO_LOG_PO_Log_Purchasing_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
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
            var PageName = "InvoiceApproval";
            var url = "../PO_LOG/PO_Log_Compare_Invoice.aspx?SUPPLY_ID=" + SUPPLY_ID + '&Invoice_ID=' + Invoice_ID + '&PageName=' + PageName
            window.open(url, "_blank");
        }
      
    </script>
    <script type="text/javascript">
        function myFunction() {

            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var body = document.body,
             html = document.documentElement;

            var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
            var w = (size.width);
            var y = w - (3 * w / 100);
            document.getElementById('divMain').setAttribute("style", "width:" + w + "px");
            document.getElementById('divMain').style.overflow = scroll;
            document.getElementById('blur-on-updateprogress').setAttribute("style", "height:" + height + "px");
            document.getElementById('divRTP').setAttribute("style", "width:" + y + "px");
            document.getElementById('divRTP').style.overflow = scroll;

            //  window.resizeTo(size.width, size.height);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
        <asp:UpdatePanel ID="updpanal" runat="server">
            <contenttemplate>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; color:Black; font-size: 12px; width: 100%;">
            <div id="page-title" class="page-title">
               Stale PO Report
            </div>
           
                <div style="padding-top: 5px; border: 1px padding-bottom: 5px;">
                  <table style=" width:100%;">
                  <tr>
                      <td align="left" colspan="5" style="width: 100%; color:Red;">
                              
The following approved POs are more than 60 days old but still do not have any invoices received against them. <br />

To enable a clearer picture of our potential liablities, we will need to do house keeping to remove these invalid POs from the system. <br />


To Close a PO, open the PO LOG Module and use the close PO Button. Please also remember to enter the remarks to explain the closure. <br />
  </td>
                              
                                 <td align="left" colspan="5" style="width: 100%; color:Red;">
                                 We believe that majority of these POs have not been fulfilled or had been cancelled. <br />
                                 Please review your respective purchase orders and close them out if the orders have been cancelled or retracted. <br />
                                 
Note : **The below report excludes Contract POs and Owners Expenses.**
                                  </td>
                                </tr>
                    <tr>
                      <td align="right" style="width: 10%">
                                 Supplier Name :</td>
                            <td align="left" style="width: 15%">
                                 <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" Width="300px">
                                </asp:DropDownList></td>
                      <td align="right" style="width: 8%">
                                Vessel :</td>
                            <td align="left" style="width: 10%">
                                  <asp:DropDownList ID="ddlVessel" runat="server" CssClass="txtInput" Width="200px">
                                </asp:DropDownList></td>
                   
                                <td align="right" style="width: 10%">
                                 Aging (More Than) :</td>
                            <td align="left" style="width: 15%">
                                <asp:DropDownList ID="ddlAging" runat="server" CssClass="txtInput" Width="200px">
                                <asp:ListItem Value="30" >30 Days</asp:ListItem>
                                <asp:ListItem Value="60" Selected="True">60 Days</asp:ListItem>
                                <asp:ListItem Value="90">90 Days</asp:ListItem>
                                <asp:ListItem Value="120">120 Days</asp:ListItem>
                                <asp:ListItem Value="180">180 Days</asp:ListItem>
                                <asp:ListItem Value="365">365 Days</asp:ListItem>
                                </asp:DropDownList></td>
                              <td align="right" style="width: 5%">
                                 View :</td>
                            <td align="left" style="width: 15%">
                                 <asp:DropDownList ID="ddlView" runat="server" CssClass="txtInput" Width="200px">
                                  <asp:ListItem Value="Yes"  >Only my Pos</asp:ListItem>
                                <asp:ListItem Value="No" >All POs</asp:ListItem>
                              
                                </asp:DropDownList></td>   
                         <td align="center" style="width: 5%;">   <asp:ImageButton ID="btnGet" 
                                 runat="server" ToolTip="Search"  
                                            ImageUrl="~/Images/SearchButton.png" onclick="btnGet_Click" /></td>
                     <td align="center" style="width: 10%;">  <asp:ImageButton ID="ImageRefresh" 
                                        runat="server"  Text="Search" ToolTip="Refresh"   
                                            ImageUrl="~/Images/Refresh-icon.png" onclick="ImageRefresh_Click"  /></td>
                              <td align="center" style="width: 10%;"> 
                             <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" /></td>

                                </tr>
             
                        
                </table>
             
                </div>
                 <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGet" EventName="Click" />
                            </Triggers>
                          <contenttemplate>
                <div id="divPendingInvoice"  runat="server" style="margin-left: auto;
                    margin-right: auto; text-align: center;">
                  
                    <asp:GridView ID="gvPurchase" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="SUPPLY_ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both"  AllowSorting="true" OnSorting="gvPurchase_Sorting"  
                                  onrowdatabound="gvPurchase_RowDataBound"  ShowFooter="True">
                                    <FooterStyle CssClass="FooterStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" BackColor="Yellow" />
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                        <Columns>
                             <asp:TemplateField HeaderText="Vessel Name">
                              <HeaderTemplate>
                                   <asp:LinkButton ID="lblVesselName" runat="server" CommandName="Sort" CommandArgument="Vessel_Name" >Vessel Name</asp:LinkButton> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                  
                                   <asp:Label ID="lblVessel_Name" runat="server"  Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                               <ItemStyle Wrap="true" HorizontalAlign="Left" Width="11%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                               <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" PO Date">
                                <HeaderTemplate>
                                    PO Date
                                    <%--<asp:LinkButton ID="lblLineDate" runat="server" CommandName="Sort" CommandArgument="Line_Date" > PO Date</asp:LinkButton> --%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLine_Date" runat="server"  Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="9%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Code">
                             <HeaderTemplate>
                                    PO Code
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblOffice_Ref_Code" runat="server"  Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                </ItemTemplate>
                               <ItemStyle Wrap="true" HorizontalAlign="Center" Width="14%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                               <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Aging Days">
                            <HeaderTemplate>
                                   <asp:LinkButton ID="lblLaspeDay" runat="server" CommandName="Sort" CommandArgument="Laspe_Day" >Aging Days</asp:LinkButton> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblLaspe_Day" runat="server"  Text='<%#Eval("Laspe_Day")%>'></asp:Label>
                                </ItemTemplate>
                               <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                               <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                          
                           
                             <asp:TemplateField HeaderText="Amount">
                               <HeaderTemplate>
                                   <asp:LinkButton ID="lblAmount" runat="server" CommandName="Sort" CommandArgument="Line_Amount" >Amount</asp:LinkButton> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLine_Amount" runat="server" Text='<%#Eval("Line_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="6%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cur">
                                <ItemTemplate>
                                    <asp:Label ID="lblLine_Currency" runat="server" Text='<%#Eval("Line_Currency")%>'></asp:Label>
                                </ItemTemplate>
                                  <FooterTemplate>
                                    <table width="100%"  >
                                   <tr><td align="right">
                                    <asp:Label ID="lblTotal" runat="server" Text="PAGE Total" /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lbl134" runat="server" Text="Total Amount" Font-Bold="true" /></td></tr>
                                   </table>
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="USD Amt">
                                <ItemTemplate>
                                   <asp:Label ID="lblReport_USD_Value" runat="server" Text='<%#Eval("Report_USD_Value","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                  <FooterTemplate>
                                   <table width="100%"  >
                                   <tr><td align="right">
                                    <asp:Label ID="lblReportUSDValue" runat="server"  /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lblReportUSDValue1" runat="server"  Font-Bold="true" /></td></tr>
                                   </table>
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="6%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Supplier Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier_Code" runat="server" Text='<%#Eval("Supplier_Code")%>'></asp:Label>
                                </ItemTemplate>
                                  <FooterTemplate>
                                   <table width="100%"  >
                                   <tr><td align="right">
                                    <asp:Label ID="lblUSD" runat="server" Text="USD" /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lblReportUSDValue12" runat="server" Text="USD" Font-Bold="true" /></td></tr>
                                   </table>
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                                <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Supplier Name">

                               <HeaderTemplate>
                                   <asp:LinkButton ID="lblSupplierDisplay_Name" runat="server" CommandName="Sort" CommandArgument="Supplier_Name" >Supplier Name</asp:LinkButton> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier_Display_Name" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="22%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Created By">
                             <HeaderTemplate>
                                   <asp:LinkButton ID="lblCreatedby" runat="server" CommandName="Sort" CommandArgument="Created_by" >Created By</asp:LinkButton> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblCreated_by" runat="server" Text='<%#Eval("Created_by")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="7%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved By">
                                 <HeaderTemplate>
                                   <asp:LinkButton ID="lblLineStatusUpdated_by" runat="server" CommandName="Sort" CommandArgument="Line_Status_Updated_by" >Approved By</asp:LinkButton> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLine_Status_Updated_by" runat="server" Text='<%#Eval("Line_Status_Updated_by")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="7%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField Visible="false" HeaderText="Action">
                                <HeaderTemplate>
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Button ID="ImgClose" Height="20px" runat="server" Visible="false"  CommandArgument='<%#Eval("[SUPPLY_ID]")%>' Text="Close PO"
                                                                    ToolTip="Close PO" OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to Close PO?')" />
                               
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGridView" />
                    <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                </div>
               
                </ContentTemplate>
        </asp:UpdatePanel>
       
      
        </div> </contenttemplate>
         <triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </triggers>
        </asp:UpdatePanel>
    </center>
</asp:Content>
