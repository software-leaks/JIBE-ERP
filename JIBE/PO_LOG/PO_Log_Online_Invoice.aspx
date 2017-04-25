<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Title="Online Invoices"
    CodeFile="PO_Log_Online_Invoice.aspx.cs" Inherits="PO_Log_Online_Invoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    <%--<link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />--%>
    <script src="" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
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
       
    .rightAlign { text-align:right; }
   
    </style>
    <script type = "text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=cblVariables.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
        function ValidateCheckBoxList2(sender, args) {
            var checkBoxList = document.getElementById("<%=cblVariableInvoice.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
        function previewDocument(docPath) {
       
           // document.getElementById("IframeInvoice").src = docPath;
          
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%;">
            <div id="page-title" class="page-title">
                Online Invoice
            </div>
            <div>
            
            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 12%">
                                        Invoice Code/Invoice Reference
                                    </td>
                                    <td align="left" colspan="5" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="400px" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                   
                                    <td align="left">
                                        <asp:ImageButton ID="imgfilter" runat="server"  ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" onclick="imgfilter_Click" /> &nbsp;
                                  
                                        <asp:ImageButton ID="btnRefresh" runat="server"  ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" onclick="btnRefresh_Click" /> &nbsp;
                                  
                                    </td>
                                </tr>
                            </table>
            </div>
                    <table width="100%" cellpadding="2" cellspacing="2">

                        <tr>
                            <td align="left" valign="top" colspan="2" style="width: 100%">
                                <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                                        <ContentTemplate>
                                    <table width="100%">

                               <tr>
                              <td colspan="2" width="100%">
                         
                             <div id="div3" runat="server">
                               <asp:GridView ID="gvOnlineInvoice" runat="server" 
                                     EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID, Supply_ID,Invoice_Reference,File_ID,Invoice_Flag,File_Extention" 
                                     CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                CssClass="gridmain-css" AllowSorting="true" 
                                     onrowdatabound="gvOnlineInvoice_RowDataBound" 
                                     onrowcommand="gvOnlineInvoice_RowCommand">
                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                 <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgView" runat="server" CommandName="VIEW"  ForeColor="Black" Width="15px" Height="15px" CausesValidation="false" OnClick="ibtnView_Click" ToolTip="View" ImageUrl="~/Images/asl_view.png" CommandArgument='<%#Eval("Supply_ID")%>'></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
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
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <HeaderTemplate>
                                            Supplier Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference">
                                        <HeaderTemplate>
                                          Reference
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSName" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Date">
                                        <HeaderTemplate>
                                          Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPODate" runat="server" Text='<%#Eval("Line_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                          Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOStatus" runat="server" Text='<%#Eval("Line_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Curr">
                                        <HeaderTemplate>
                                          Curr
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOCurrency" runat="server" Text='<%#Eval("Line_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <HeaderTemplate>
                                            Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Line_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Received">
                                        <HeaderTemplate>
                                            Invoice Received
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoiceCurrAmount" runat="server" Text='<%#Eval("Invoice_PO_Currency_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Ref">
                                        <HeaderTemplate>
                                            Inv Ref
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoiceRece" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Date">
                                        <HeaderTemplate>
                                               Inv. Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice Amount">
                                        <HeaderTemplate>
                                            Invoice Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="GST/VAT">
                                        <HeaderTemplate>
                                            GST/VAT
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGST" runat="server" Text='<%#Eval("Invoice_GST_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Date">
                                        <HeaderTemplate>
                                            Due Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayment_DueDate" runat="server" Text='<%#Eval("Invoice_Due_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Remarks" ForeColor="Red" runat="server" Text='<%#Eval("Invoice_Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="10" OnBindDataItem ="BindOnlineInvoice"  />

                                    </div>
                                </td>
                            </tr>
                                        
                                           <tr>
                                           
                                            <td   valign="top">
                                            <div  id="Div2" runat="server" style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%;">
                                            <table width="100%">
                                            <tr>
                                            <td width="50%">
                                            
                           
                        
                                             <asp:Label ID="lblDelivery" ForeColor="Red" runat="server" Text="Existing Invoice to PO Found. Please check in PO LOG.!!"></asp:Label>
                                            <div id="divExistingInvoices" visible="false" runat="server" style="height: 100px; overflow-y: scroll;
                                                max-height: 100px">
                                                <asp:GridView ID="gvExistingInvoices" runat="server" EmptyDataText="NO RECORDS FOUND" AllowPaging="true" PageSize= "5"
                                                    AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                                                    Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Delivery Date">
                                                            <HeaderTemplate>
                                                                Received Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivery_Date" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <HeaderTemplate>
                                                              Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivery_Location" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Date">
                                                            <HeaderTemplate>
                                                              Invoice Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivery_Location" runat="server" Text='<%#Eval("Invoice_Date")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Invoice Ref">
                                                            <HeaderTemplate>
                                                              Invoice Ref
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivery_Location" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Description">
                                                            <HeaderTemplate>
                                                                Invoice Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivered_Item_Description" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                                                &nbsp; <asp:Label ID="lblInvCurr" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Status">
                                                            <HeaderTemplate>
                                                                Invoice Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Status">
                                                            <HeaderTemplate>
                                                              Payment  status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivered_Item_Price" runat="server" Text='<%#Eval("Payment_Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                  <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="10" OnBindDataItem ="BindInvoiceDetails"  />
                                            </div>
                                              
                                            </td>
                                            
                                          
                                                 <td width="50%" >
                                             
                                           <div id="divDuplicateInvoice" visible="false" runat="server" style="height: 150px; overflow-y: scroll;
                                                max-height: 150px">
                                                <asp:Label ID="lblDuplicateInvoice" ForeColor="Red" runat="server" Text="Similiar invoice found !!! Please check PO LOG"></asp:Label>
                                                <asp:GridView ID="gvDuplieCateinvoice" runat="server" EmptyDataText="NO RECORDS FOUND" AllowPaging="true" PageSize= "5"
                                                    AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                                                    Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Supplier Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Supply_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order Code">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Ref">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Type">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Amount">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>&nbsp;
                                                                <asp:Label ID="lblInvCurrency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>&nbsp;
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Invoice Status">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvStatus" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>&nbsp;
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Status">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment" runat="server" Text='<%#Eval("Payment_Status")%>'></asp:Label>&nbsp;
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                 <uc1:ucCustomPager ID="ucCustomPager2" runat="server" PageSize="10"  />
                                                <asp:HiddenField ID="HiddenField3" runat="server" EnableViewState="False" />
                                                <asp:HiddenField ID="HiddenSupplyId" runat="server" />
                                            </div>
                                            
                                            </td>
                                            </tr>
                                       </table>
                                       </div>
                                            </tr>
                                            
                                          
                                    </table>
                                    </ContentTemplate></asp:UpdatePanel>
                               
                            </td>

                        </tr>
                        <tr>
                        <td width="100%">
                          <div  id="Div5" runat="server" style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%; color: #000000;">
                        <table width="100%">
                        <tr>
                        <td>      <asp:Label ID="lblInvRef" Text="Invoice Reference: " runat="server"></asp:Label></td>
                         <td> <asp:TextBox ID="txtInvRef" runat="server" MaxLength="50" CssClass="rightAlign"></asp:TextBox><asp:RequiredFieldValidator ControlToValidate="txtInvRef"
                                                            ID="RequiredFieldValidator4" ValidationGroup="vgSubmit" runat="server" Display="None" ErrorMessage="Invoice ref is Required"></asp:RequiredFieldValidator>
                                       </td>
                          <td>  <asp:Label ID="lblInvDate" Text="Invoice Date: " runat="server"></asp:Label></td>
                           <td> <asp:TextBox ID="txtInvoiceDate" runat="server" MaxLength="255"  CssClass="rightAlign"></asp:TextBox><ajaxToolkit:CalendarExtender TargetControlID="txtInvoiceDate" ID="CalendarExtender2" Format="dd-MM-yyyy"
                                    runat="server" /><asp:RequiredFieldValidator ControlToValidate="txtInvoiceDate"
                                                            ID="RequiredFieldValidator5" ValidationGroup="vgSubmit" runat="server" Display="None" ErrorMessage="Invoice date is Required"></asp:RequiredFieldValidator></td>
                                    
                            <td><asp:Label ID="Label2" Text="Amount: " runat="server"> </asp:Label></td>
                             <td>   <asp:TextBox ID="txtInvoiceAmount" runat="server" MaxLength="50" CssClass="rightAlign"></asp:TextBox><asp:RequiredFieldValidator ControlToValidate="txtInvoiceAmount"
                                                            ID="RequiredFieldValidator6" ValidationGroup="vgSubmit" runat="server" Display="None" ErrorMessage="Amount is Required"></asp:RequiredFieldValidator></td>
                                                             <td><asp:Label ID="lblGSTAmount" Text="GSTAmount: " runat="server"> </asp:Label></td>
                             <td>   <asp:TextBox ID="txtGSTAmount" runat="server" MaxLength="50" CssClass="rightAlign"></asp:TextBox><asp:RequiredFieldValidator ControlToValidate="txtGSTAmount"
                                                            ID="RequiredFieldValidator2" ValidationGroup="vgSubmit" runat="server" Display="None" ErrorMessage="GSTAmount is Required"></asp:RequiredFieldValidator></td>
                             
                                    <tr>
                                    <td>   <asp:Label ID="lblPeriod" Text="Period To : " runat="server"></asp:Label></td>
                                       <td>   <asp:TextBox ID="txtTo" runat="server" MaxLength="50" CssClass="rightAlign" ></asp:TextBox>&nbsp; <ajaxToolkit:CalendarExtender TargetControlID="txtTo" ID="CalendarExtender3" Format="dd-MM-yyyy"
                                    runat="server" /></td>
                                    <td><asp:Label ID="lblPeriodFrom" Text="Period From: " runat="server"></asp:Label></td>
                                    <td>
                         <asp:TextBox ID="txtfrom" runat="server" MaxLength="50" CssClass="rightAlign"></asp:TextBox>&nbsp; <ajaxToolkit:CalendarExtender TargetControlID="txtfrom" ID="CalendarExtender4" Format="dd-MM-yyyy"
                                    runat="server" /></td>
                                     <td><asp:Label ID="lblDuedate" Text="Due Date: " runat="server"></asp:Label></td>
                              <td>   <asp:TextBox ID="txtDuedate" runat="server" MaxLength="50" CssClass="rightAlign"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender TargetControlID="txtDuedate" ID="CalendarExtender1" Format="dd-MM-yyyy"
                                    runat="server" /><asp:RequiredFieldValidator ControlToValidate="txtDuedate"
                                                            ID="RequiredFieldValidator7" ValidationGroup="vgSubmit" runat="server" Display="None" ErrorMessage="Due Date is Required"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                     
                                         
                     
                                          
                                                <tr>
                                                    <td>
                                                        Remarks :
                                                        <asp:Label ID="lbl1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemarks" runat="server" Height="60px" MaxLength="2000" 
                                                            TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="txtRemarks" Display="None" 
                                                            ErrorMessage="Remarks is Required" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                                    </td>
                                                        <td>
                                        
                                       </td>
                                                   
                            </tr>
                        
                                        </tr>
                        </table>
                        <table>
                                        <tr>
                                        <td>
                                      
                                                      <asp:CheckBoxList ID="cblVariableInvoice" runat="server" RepeatDirection="Horizontal" ValidationGroup="vgSubmit" >
                              </asp:CheckBoxList>
                               <asp:CustomValidator ID="CustomValidator2" ErrorMessage="Please select at least one item." Display="None"
                                ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList2" runat="server" ValidationGroup="vgSubmit"  />
                                                    </td>
                                             <td>

                                                 <asp:HiddenField ID="hiddenKeyId" runat="server" />
                                                 <asp:Button ID="btnUpdateInvoice" runat="server" 
                                                     onclick="btnUpdateInvoice_Click" Text="Update Invoice Data" 
                                                     ValidationGroup="vgSubmit" />
                                                 <asp:Button ID="btnVerify" runat="server" onclick="btnVerify_Click" 
                                                     Text="Verify &amp; Import" />
                                                 <asp:Button ID="btnDeleteInvoice" runat="server" CausesValidation="false" 
                                                     onclick="btnDeleteInvoice_Click" Text="Delete Invoice" />
                                             </td>
                                             </tr>
                                       </table>
                       </div>
                        </td>

                        </tr>
                        <tr>
                        <td width="100%">
                          <div  id="Div1" runat="server" 
                                style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%; color: #000000;">
                          <table>
                         <tr>
                         <th></th>
                          <th> <asp:Label  ID="lblText" Text="Rejection Remarks"  runat="server"></asp:Label></th>
                           <th></th>
                         </tr>
                            
                          <tr>
                          <td valign="top" >
                            <Font Color=Red>Invoice Rejection</Font>  : <Font color=red>If Invoice is to be rejected, please use this section.</Font>
                              <asp:CheckBoxList ID="cblVariables" runat="server" RepeatDirection="Horizontal" 
                                  ValidationGroup="vgSubmitWithReject" 
                                  onselectedindexchanged="cblVariables_SelectedIndexChanged" AutoPostBack="true" >
                              </asp:CheckBoxList>
                             <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one item." Display="None"
                                ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" ValidationGroup="vgSubmitWithReject"  />
                          </td>
                          <td valign="top">
                         
                           <asp:TextBox ID="txtRejectionRemark" runat="server" Height="60px" MaxLength="1000" 
                                                            TextMode="MultiLine" Width="300px" 
                                  ValidationGroup="vgSubmitWithReject" Enabled="False"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="txtRejectionRemark" Display="None" 
                                                            ErrorMessage="Remarks is Required" ValidationGroup="vgSubmitWithReject"  ></asp:RequiredFieldValidator>
                                                              
                          </td>
                          <td valign="middle">
                          <asp:Button ID="btnRejectInvoice" 
                                  runat="server" Text="Reject Invoice" onclick="btnRejectInvoice_Click" 
                                  ValidationGroup="vgSubmitWithReject" Enabled="False" />
                          </td>
                          </tr>
                          <tr>
                          
                           <td >
                                                      
                                                    </td>
                          </tr>
                          </table>
                          </div>
                        </td>
                        </tr>
                             
                         <tr>
                            <td><asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="vgSubmit" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="vgSubmitWithReject" />
                                        </td>

                            
                        </tr>
                         <tr>
                     
                         </td>
                          
                        </tr>
                        
                        <tr>
                        <td width="100%">
                        <table width="100%">
                        <tr>
                            <td  width="50%" align="left" valign="top">
                               <div>
                                      <asp:UpdatePanel ID="UpdatePanelPO" runat="server">
                                        <ContentTemplate>
                                     <iframe id="IframePO"  
                                                runat="server" width="100%" height="700px"></iframe>
                                      </ContentTemplate>
                                    </asp:UpdatePanel></div>
                                 
                            </td>
                             <td  width="50%"  align="left" valign="top">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <iframe id="IframeInvoice" src="" runat="server" width="100%" height="700px"></iframe>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                    </table>
                
                <div style="display: none;">
                    <asp:TextBox ID="txtRemarksID" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtPOCode" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtInvoiceCode" runat="server" Width="1px"></asp:TextBox>
                </div>
         </div>
            </contenttemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
