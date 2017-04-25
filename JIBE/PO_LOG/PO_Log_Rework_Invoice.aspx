<%@ Page Title="Rework Invoice" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_Log_Rework_Invoice.aspx.cs" Inherits="PO_LOG_PO_Log_Rework_Invoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <link href="../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
       <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "jQuery Dialog Popup",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };
</script>
      <script language="javascript" type="text/javascript">

          function OpenCompareScreen(SUPPLY_ID, Invoice_ID) {
              var url = "../PO_LOG/PO_Log_Compare_Invoice.aspx?SUPPLY_ID=" + SUPPLY_ID + '&Invoice_ID=' + Invoice_ID
              window.open(url, "_blank");
          }

          function OpenRemarksScreen(Invoice_ID) {
              var Type = 'Invoice';
              var url = 'PO_Log_Remarks_History.aspx?Invoice_ID=' + Invoice_ID;
              //window.open(url, "_blank");
              OpenPopupWindow('PO_Log_Remarks_History', 'Remarks History', url, 'popup', 500, 900, null, null, false, false, true, null);
             // OpenPopupWindowBtnID('PO_Log_Remarks_History', 'Remarks History', url, 'popup', 500, 900, null, null, false, false, true, null);
          }
    </script>
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
             <div style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%;">
            <div id="page-title" class="page-title">
                Rework Invoice
                </div>
                <div align="left">
                <ul>
              <li> This report shows the re-work pending for respective personnels. Aging of invoices calculated based on Invoice verified date.</li> 
<li>You can only compare PO/Invoices under your approval or reworked to you; or if you have access to view the PO.</li>
<li>This report is sent every monday morning to the respective users via email.</li>
                </ul>
                </div>
                      <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="left" valign="top" style="width: 12%">
                                        User Name :&nbsp;&nbsp;
                                   
                                       <asp:DropDownList ID="ddlUserName" runat="server" Width="200px" CssClass="txtInput">
                                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:ImageButton ID="btnFilter" runat="server"  ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" onclick="btnFilter_Click" /> &nbsp;&nbsp;
                                            <asp:ImageButton ID="btnRefresh" runat="server"  ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" onclick="btnRefresh_Click" />
                                          
                                    </td>
                                  
                                </tr>
                            </table>
                        </div>
                        <table width="100%">
                        <tr>
                        <td align="left" valign="top" style="width:24%;" >  
                            <asp:GridView ID="gvInvoiceCount" runat="server" 
                                EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    DataKeyNames="For_Action_By" CellPadding="1" CellSpacing="0"
                                    GridLines="both"  AllowSorting="true" FooterStyle-HorizontalAlign="Right"
                                onrowdatabound="gvInvoiceCount_RowDataBound" ShowFooter="True">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css"  />
                                    <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css"/>
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns >
                                        <asp:TemplateField HeaderText="Dept" ItemStyle-HorizontalAlign="Right">
                                            <HeaderTemplate>
                                               Dept
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUser_Department" runat="server" Text='<%#Eval("User_Department")%>'></asp:Label>
                                            </ItemTemplate>
                                          
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Min Approval Limit">
                                            <HeaderTemplate>
                                              For Action By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblAction" runat="server" Text='<%#Eval("For_Action_By")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Count">
                                             <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Count" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                            </ItemTemplate>
                                         <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="USD Value" >
                                            <HeaderTemplate>
                                              USD Value
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblUSD" runat="server" Text='<%# Eval("USD_Value","{0:N2}")%>'></asp:Label>
                                            </ItemTemplate>
                                          <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            <FooterTemplate>
                                          <div style="text-align: right;">
                                             <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                          </div>
                                            </FooterTemplate>
                                            </asp:TemplateField>
                                         
                                    </Columns>
                                    <FooterStyle CssClass="FooterStyle-css" />
                                </asp:GridView>
                            
                            </td>
                             <td style="width:1%;"></td>   
                                <td align="left" valign="top"  style="width:75%;">
                                 <asp:GridView ID="gvInvoice" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                    DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0" Width="100%" ShowFooter="true"
                    GridLines="both" CssClass="gridmain-css" AllowSorting="true"  FooterStyle-HorizontalAlign="Right"
                                        onrowdatabound="gvInvoice_RowDataBound">
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <SelectedRowStyle BackColor="Yellow" />
                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                    <FooterStyle CssClass="FooterStyle-css"  />
                    
                    <Columns>
                        <asp:TemplateField HeaderText="Type">
                            <HeaderTemplate>
                                No
                            </HeaderTemplate>
                            <ItemTemplate>
                             <span>
                             <%#Container.DataItemIndex + 1%>
                             </span>
                             </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Date">
                            <HeaderTemplate>
                                Vessel Name
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVessel_Name" runat="server" Text='<%#Eval("Vessel_Display_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inv Ref">
                            <HeaderTemplate>
                                Supplier Name
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSupplier_Name" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="240px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Invoice Amount">
                            <HeaderTemplate>
                                PO Code
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOffice_Ref_Code" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="160px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        </asp:TemplateField>
                     
                        <asp:TemplateField HeaderText="Delivery Confirmed">
                            <HeaderTemplate>
                                Reference
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="140px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice_Status">
                            <HeaderTemplate>
                                 Status
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceS" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        </asp:TemplateField>
                              <asp:TemplateField HeaderText="Verified Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblVerified_Date" runat="server" Text='<%#Eval("Verified_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Aging">
                            <HeaderTemplate>
                                Aging
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAgeing" runat="server" Text='<%#Eval("Aging")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Invoice Amount">
                            <HeaderTemplate>
                              Invoice Amount 
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount","{0:N2}")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="70px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" Width="70px" HorizontalAlign="Center" />
                              <FooterTemplate>
                                          <div style="text-align: right;">
                                             <asp:Label ID="lblTtl" runat="server" ></asp:Label>
                                          </div>
                                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Invoice Amount">
                            <HeaderTemplate>
                              USD Amount
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUSD_Amount" runat="server" Text='<%#Eval("Invoice_USD_Value","{0:N2}")%>'></asp:Label>
                            </ItemTemplate>
                           <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Invoice Approver">
                            <ItemTemplate>
                                <asp:Label ID="lblInv_Approver" runat="server" Text='<%#Eval("Inv_Approver")%>'></asp:Label>
                            </ItemTemplate>
                           <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Rework From">
                            <ItemTemplate>
                                <asp:Label ID="lblReWork_From_By" runat="server" Text='<%#Eval("ReWork_From_By")%>'></asp:Label>
                            </ItemTemplate>
                           <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Invoice Amount">
                            <HeaderTemplate>
                              Dispute
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDispute_Flag" runat="server"  Text='<%#Eval("Dispute_Flag")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                            <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Action">
                                <HeaderTemplate>
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                  <table>
                                 
                                <tr>
                               
                                 <td style="border-color: transparent">
                                  <asp:ImageButton ID="ImgViewRemarks" runat="server" ForeColor="Black"  Width="18px" Height="16px"
                                                                    ToolTip="View Remarks" OnClientClick='<%#"OpenRemarksScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>&nbsp;
                                 </td>
                                <td>
                                <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black" Width="18px" Height="16px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>&nbsp;
                                    
                                    </td>
                                    
                                    </tr></table>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="10" OnBindDataItem="BindInvoiceCount" />
                                </td>
                        </tr>
                        </table>
              
                                    
         
                </div>
            </contenttemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>

