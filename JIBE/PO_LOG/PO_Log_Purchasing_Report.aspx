<%@ Page Title="Purchasing Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_Log_Purchasing_Report.aspx.cs" Inherits="PO_LOG_PO_Log_Purchasing_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
 <%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
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
        <asp:UpdatePanel ID="updpanal" runat="server">
            <contenttemplate>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; color:Black; font-size: 12px; width: 100%;">
            <div id="page-title" class="page-title">
                Purchasing Report
            </div>
           
                <div style="padding-top: 5px; border: 1px padding-bottom: 5px;">
                  <table style=" width:100%;">
                   <tr>
                 
                  <td style="width: 100%; color:Red;"  align="left" colspan="8">
                               ** Purchases before the implementation of the PO LOG (1st March 2010) will not be included into this report. ** 
                            </td>
                            </tr>
                              <tr>
                 
                  <td style="width: 100%; color:Red;"  align="left" colspan="8">
                             ** You are able to see the following PO Types :
                             <asp:Label ID="lblPOType" runat="server" Text="" ForeColor="Blue"  ></asp:Label>
                             <br /><br />
                            </td>

                            </tr>
                  <tr>
                 
                  <td style="width: 10%" align="right">
                                Supplier Name :
                            </td>
                            <td style="width: 10%" align="left">
                                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" Width="300px">
                                </asp:DropDownList>
                            </td>
                              <td style="width: 10%"  align="right">
                               PO Status : &nbsp;
                            </td>
                    <td align="left"  style="width: 15%;">
                       <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" />
                                        <asp:CheckBox ID="chkApproval" runat="server" Text="For Approval" />
                                        <asp:CheckBox ID="chkUnApproved" runat="server" Text="UnApproved" />
                         </td>
                          <td style="width: 10%"  align="right">
                              Period From  : &nbsp;
                            </td>
                    <td align="left"  style="width: 20%;">
                       <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtInput" Width="120px"></asp:TextBox>
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                        Format="dd/MM/yyyy">  </ajaxToolkit:CalendarExtender>
                         <asp:TextBox ID="txtToDate" runat="server" CssClass="txtInput" Width="120px"></asp:TextBox>
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                        Format="dd/MM/yyyy">  </ajaxToolkit:CalendarExtender>
                         </td>
                         <td align="center" style="width: 5%;">   </td>
                          <td align="left" style="width: 5%;"></td>
                          <td align="left" style="width: 5%;"></td>
                            </tr>
                 <tr>
                      <td align="right" style="width: 10%">
                                Vessel :</td>
                            <td align="left" style="width: 10%">
                                <ucDDL:ucCustomDropDownList ID="ddlVessel" runat="server" UseInHeader="false" OnApplySearch="ddlVessel_SelectedIndexChanged"
                                                        Height="150" Width="160" /></td>
                     <td align="right" style="width: 10%">
                                Account Type :</td>
                            <td align="left" style="width: 15%">
                                <ucDDL:ucCustomDropDownList ID="ddlAccountType" runat="server" UseInHeader="false" OnApplySearch="ddlAccountType_SelectedIndexChanged"
                                                        Height="150" Width="160" /></td>
                                <td align="right" style="width: 10%">
                                Account Classification :</td>
                            <td align="left" style="width: 20%">
                                 <ucDDL:ucCustomDropDownList ID="ddlAccClassification" runat="server" UseInHeader="false" OnApplySearch="ddlAccClassification_SelectedIndexChanged"
                                                        Height="150" Width="160" /></td>
                                <td align="right" style="width: 5%;"> 
                                <asp:ImageButton ID="btnGet" runat="server" ToolTip="Search"  
                                            ImageUrl="~/Images/SearchButton.png" onclick="btnGet_Click" /></td>
                                            
                                <td align="center" style="width: 5%;">  <asp:ImageButton ID="ImageRefresh" 
                                        runat="server"  Text="Search" ToolTip="Refresh"   
                                            ImageUrl="~/Images/Refresh-icon.png" onclick="ImageRefresh_Click"  /></td>
                                  <td align="left" style="width: 5%;"> 
                             <asp:ImageButton ID="ImgExpExcel" runat="server"  ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" /></td>
                                            
                                </tr>         
                </table>
             
                </div>
                 <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGet" EventName="Click" />
                            </Triggers>
                          <contenttemplate>
                <div id="divPurchase"  runat="server" style="margin-left: auto;
                    margin-right: auto; text-align: center;">
                  
                    <asp:GridView ID="gvPurchase" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="SUPPLY_ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both"  AllowSorting="true" 
                                  onrowdatabound="gvPurchase_RowDataBound" ShowFooter="True">
                                  <FooterStyle CssClass="FooterStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" BackColor="Yellow" />
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                        <Columns>
                            <asp:TemplateField HeaderText="Line Status">
                                <HeaderTemplate>
                                     PO Status
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLine_Status" runat="server"  Text='<%#Eval("Line_Status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Name">
                                <HeaderTemplate>
                                    PO Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLine_Date" runat="server"  Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="7%" CssClass="PMSGridItemStyle-css">
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
                               <ItemStyle Wrap="true" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                               <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Ship Reference">
                                <ItemTemplate>
                                    <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("ship_Ref_Code")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="7%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Acct">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Account_Classification")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="16%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Count" runat="server" Text='<%#Eval("Line_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="4%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cur">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Line_Currency")%>'></asp:Label>
                                </ItemTemplate>
                                   <FooterTemplate>
                                     <table width="100%"  >
                                   <tr><td align="right">
                                    <asp:Label ID="lbl123" runat="server" Text="Total in USD(PageTotal)" /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lbl134" runat="server" Text="Total in USD(TotalRecord)" Font-Bold="true" /></td></tr>
                                   </table>
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="3%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                 <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="PO USD Value">
                                <ItemTemplate>
                                   <asp:Label ID="lblReceived_Date" runat="server" Text='<%#Eval("PO_USD_Value","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                   <FooterTemplate>
                                    <table width="100%"  >
                                   <tr><td align="right">
                                     <asp:Label ID="lblTotPOUSDValue" runat="server" Font-Bold="true"  /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lblTotPOUSDValue1" runat="server" Font-Bold="true" /></td></tr>
                                   </table>
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice USD Amount">
                                <ItemTemplate>
                                   <asp:Label ID="lblInvoice_USD_Amount" runat="server" Text='<%#Eval("Invoice_USD_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                   <FooterTemplate>
                                    <table width="100%"  >
                                   <tr><td align="right">
                                    <asp:Label ID="lblTotInvUSDAmt" runat="server" Font-Bold="true" /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lblTotInvUSDAmt1" runat="server" Font-Bold="true" /></td></tr>
                                   </table>
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                 <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid USD Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaid_USD_Amount" runat="server" Text='<%#Eval("Paid_USD_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                   <FooterTemplate>
                                    <table width="100%"  >
                                   <tr><td align="right">
                                    <asp:Label ID="lblTotPaidUSDAmt" runat="server" Font-Bold="true" /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lblTotPaidUSDAmt1" runat="server" Font-Bold="true" /></td></tr>
                                   </table>
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                 <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Outstanding Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblOutstanding_PO_Currency_Amount" runat="server" Text='<%#Eval("OutStanding_USD_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                   <FooterTemplate>
                                   <table width="100%"  >
                                   <tr><td align="right"><asp:Label ID="lblTotOutPOCurAmt" runat="server" Font-Bold="true" /><br /><br /></td></tr>
                                   <tr><td align="right"><asp:Label ID="lblTotOutPOCurAmt1" runat="server" Font-Bold="true" /></td></tr>
                                   </table>
                                    
                                   
                                    
                                    </FooterTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                                 <FooterStyle Wrap="true" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Name">
                              
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier_Name" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                    
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="18%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Vessel Name">
                              
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel_Name" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                    
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Action">
                                <HeaderTemplate>
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImgCompare" runat="server" CommandName="ImgInvoice"  OnCommand="ImgCompare_Click" Width="15px" Height="12px"    CommandArgument='<%#Eval("[Supply_ID]")%>' 
                                                            ForeColor="Black" ToolTip="Compare PO and Invoice" ImageUrl="~/Images/compare.gif"></asp:ImageButton>
                            <%--    <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black" Width="18px" Height="12px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>&nbsp;--%>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <%-- <table width="100%">
                                   <tr><td align="right"><asp:Label ID="Label1" runat="server" Font-Bold="true" /><asp:Label ID="Label2" runat="server" Font-Bold="true" /><asp:Label ID="lblTotOutPOCurAmt" runat="server" Font-Bold="true" />
                                   <asp:Label ID="lblTotOutPOCurAmt1" runat="server" Font-Bold="true" /></td></tr>
                                 

                                   </table>--%>
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

