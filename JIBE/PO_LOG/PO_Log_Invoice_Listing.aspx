<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Invoice_Listing.aspx.cs"
    Title="Invoice List" Inherits="PO_LOG_Invoice_Listing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id*=gvInvoiceDetails] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }
        function OpenScreen(Invoice_ID, Job_ID) {
            var SUPPLY_ID = document.getElementById("txtPOCode").value;
            //var POType = document.getElementById("txtPOCode").value;
            var url = 'PO_Log_Invoice_Entry.aspx?Invoice_ID=' + Invoice_ID + '&SUPPLY_ID=' + SUPPLY_ID;
            OpenPopupWindowBtnID('PO_Log_Invoice_Entry', 'Invoice Entry', url, 'popup', 1000, 1050, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenScreen1(Invoice_ID, Job_ID) {

            var Type = 'Invoice';
            var url = 'PO_Log_Attachment.aspx?ID=' + Invoice_ID + '&DocType=' + Type;
            OpenPopupWindowBtnID('PO_Log_Attachment', 'PO Log Attachment', url, 'popup', 600, 700, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenScreen3(Invoice_ID, Job_ID) {
            var SUPPLY_ID = document.getElementById("txtPOCode").value;
            var Type = 'Invoice';
            var url = 'PO_Log_Remarks.aspx?Invoice_ID=' + Invoice_ID + '&SUPPLY_ID=' + SUPPLY_ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_Remarks', 'PO Log Remarks Entry', url, 'popup', 500, 700, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenScreen2(Invoice_ID, Job_ID) {
            var InvoiceID = document.getElementById("txtInvoiceID").value;
            var Type = 'Invoice';
            var url = 'PO_Log_Attachment.aspx?Invoice_ID=' + Invoice_ID + '&DocType=' + Type;
            OpenPopupWindowBtnID('PO_Log_Attachment', 'PO Log Attachment', url, 'popup', 450, 700, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenPOTransfer(Invoice_ID) {
            var SUPPLY_ID = document.getElementById("txtPOCode").value;
            var Invoice_ID = document.getElementById("txtInvoiceID").value;
            var url = 'PO_Log_Transfer_Cost.aspx?Invoice_ID=' + Invoice_ID + '&SUPPLY_ID=' + SUPPLY_ID;
            
//            var size = {
//                width: window.innerWidth || document.body.clientWidth,
//                height: window.innerHeight || document.body.clientHeight
//            }
//            var w = (size.width);
//            var y =size.height;
         
            //OpenPopupWindowBtnID('PO_Log_Transfer_Cost', 'PO_Log_Transfer_Cost', url, 'popup', y,w, null, null, false, false, true, null, 'btnFilter');
            OpenPopupWindowBtnID('PO_Log_Transfer_Cost', 'PO_Log_Transfer_Cost', url, 'popup', 800, 1200, null, null, false, false, true, null, 'btnFilter');
        }
         
        function OpenPOTransfer2(Transfer_Id, Parent_Supply_Id) {

            var Invoice_ID = document.getElementById("txtInvoiceID").value
            var url = 'PO_Log_Transfer_Cost.aspx?Transfer_Id=' + Transfer_Id + '&SUPPLY_ID=' + Parent_Supply_Id + '&Invoice_ID=' + Invoice_ID;
           // window.open(url);
            OpenPopupWindowBtnID('PO_Log_Transfer_Cost', 'Transfer Cost', url, 'popup', 800, 1200, null, null, false, false, true, null, 'btnFilter');
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
            <%-- <div id="page-title" class="page-title">
                Invoice Listing
            </div>--%>
            <asp:UpdatePanel ID="upd1" runat="server">
                <contenttemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="right" ><asp:Label ID="Label1" CssClass="input_lable" runat="server" Text="Supply ID :"></asp:Label>
                             &nbsp;&nbsp;<asp:Label ID="lblSupplyID" CssClass="input_lable" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                             <asp:Label ID="Label2" CssClass="input_lable" runat="server" Text="PO Code :"></asp:Label>
                             &nbsp;&nbsp;<asp:Label ID="lblPOCode" CssClass="input_lable" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label3" CssClass="input_lable" runat="server" Text="PO Amount :"></asp:Label>
                             &nbsp;&nbsp;<asp:Label ID="lblPoAmount" CssClass="input_lable" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label4" CssClass="input_lable" runat="server" Text="Payment Terms :"></asp:Label>
                             &nbsp;&nbsp;
                            <asp:Label ID="lblDays" runat="server" CssClass="input_lable" Text=""></asp:Label>.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblPOClose" runat="server" CssClass="input_lable" Font-Bold="true" ForeColor="Red" Text="Close PO"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
             <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                    <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 12%">
                                        Invoice Code/Invoice Reference
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="400px" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                   
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="imgfilter" runat="server"  ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" onclick="imgfilter_Click" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server"  ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" onclick="btnRefresh_Click" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Invoice" OnClientClick='OpenScreen(null,null);return false;'
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgTransferCost" runat="server"  ToolTip="Transfer Cost"  OnClientClick='OpenPOTransfer(null);return false;'
                                            ImageUrl="~/Images/Transfer.png"/>
                                    </td>
                                </tr>
                            </table>
                </div>
                <div id="DivInvoiceDeatils" visible="false" runat="server" style="height: 300px;
                    overflow-y: scroll; max-height: 300px">
                    <table>
                        <tr>
                            <td align="left" style="width: 8%; color: red;">
                                Supplier Name :&nbsp;&nbsp;
                                <asp:Label ID="lblSuppliername" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvInvoiceDetails" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true" 
                        onselectedindexchanged="gvInvoiceDetails_SelectedIndexChanged" 
                        onrowdatabound="gvInvoiceDetails_RowDataBound">
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                        <Columns>
                            <asp:TemplateField HeaderText="Created">
                                <HeaderTemplate>
                                    Created
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCreated" runat="server" Text='<%#Eval("Created_By")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dated">
                                <HeaderTemplate>
                                    Dated
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDated" runat="server" Text='<%#Eval("Created_Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inv Type">
                                <HeaderTemplate>
                                    Inv Type
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvType" runat="server" Text='<%#Eval("VARIABLE_NAME")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
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
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received">
                                <HeaderTemplate>
                                    Received
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblReceivedDate" runat="server" Text='<%#Eval("Received_Date")%>'></asp:Label>
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
                                    <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%#Eval("Invoice_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Due Date">
                                <HeaderTemplate>
                                    Invoice Due Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDueDate" runat="server" Text='<%#Eval("Invoice_Due_Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Due Date">
                                <HeaderTemplate>
                                    Payment Due Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentDueDate" runat="server" Text='<%#Eval("Payment_Due_Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Approval">
                                <HeaderTemplate>
                                    Invoice Status
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceApproval" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Payment Approval">
                                <HeaderTemplate>
                                    Invoice Approval
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Approval" runat="server" Text='<%#Eval("Invoice_Approval")%>'></asp:Label>&nbsp;
                                     <asp:Label ID="Label5" runat="server" Text='<%#Eval("Approved_Date")%>'></asp:Label>
                                    
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Approval">
                                <HeaderTemplate>
                                    Payment Approval
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentApproval" runat="server" Text='<%#Eval("Payment_Approval")%>'></asp:Label>&nbsp;
                                     <asp:Label ID="Label6" runat="server" Text='<%#Eval("Payment_Approved_Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Status">
                                <HeaderTemplate>
                                    Payment Status
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentStatus" runat="server" Text='<%#Eval("Payment_Status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderTemplate>
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table cellpadding="2" cellspacing="2">
                                        <tr>
                                         <td>
                                                <asp:ImageButton ID="ImageButton2" runat="server"  OnClientClick='<%#"OpenScreen(&#39;"+ Eval("Invoice_ID") +"&#39;);return false;"%>'
                                                    CommandName="Select" ForeColor="Black" CommandArgument='<%#Eval("[Invoice_ID]")%>' ToolTip="Edit"
                                                    ImageUrl="~/Images/edit.gif" Height="16px"></asp:ImageButton>
                                            </td>
                                           <%-- <td>
                                                <asp:ImageButton ID="lbtnEdit" runat="server" Text="Update" OnClientClick='<%#"OpenScreen(&#39;"+ Eval("Invoice_ID") +"&#39;);return false;"%>'
                                                    CommandName="Select" ForeColor="Black" CommandArgument='<%#Eval("[Invoice_ID]")%>' ToolTip="View"
                                                    ImageUrl="~/Images/asl_view.png" Height="16px"></asp:ImageButton>
                                            </td>--%>
                                            <td>
                                                <asp:ImageButton ID="btnDelete_Click" runat="server" OnCommand="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete?')"
                                                    Text="Update" CommandName="Select" ForeColor="Black" CommandArgument='<%#Eval("[Invoice_ID]")%>'
                                                    ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                            </td>
                                            <td>
                                          
                                           <%-- <asp:LinkButton ID="lblShow"  runat="server" CommandName="Select" OnCommand="lbtnTransfer_Click" >Select</asp:LinkButton>--%>
                                                       <asp:ImageButton ID="imgTransfer" runat="server" Height="14px" Width="14px" ImageUrl="~/Images/asl_view.png" CommandName="Select" OnCommand="lbtnTransfer_Click"   />                                    
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                             <asp:TemplateField Visible="false" HeaderText="Payment Status">
                                <HeaderTemplate>
                                    PO Transfer
                                </HeaderTemplate>
                                <ItemTemplate>
                                   
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Payment Approval" Visible="false">
                                <ItemTemplate>
                                     <asp:Label ID="Invoice_Id" runat="server" Visible="false" Text='<%#Eval("Invoice_Id")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                   <uc1:uccustompager id="ucCustomPagerItems" runat="server" pagesize="30" onbinddataitem="BindInvoiceDetails" />
        <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                </div>
                 <br />
                <div id="divOnlineInvoice" visible="false" runat="server" style="height: 200px; overflow-y: scroll;
                    max-height: 200px">
                    <table>
                        <tr>
                            <td align="left" style="width: 8%; color: red;">
                                The following invoices has been uploaded online. Please download it from the Online
                                invoice module.
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvOnlineInvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                        <Columns>
                            <asp:TemplateField HeaderText="Type">
                                <HeaderTemplate>
                                    Type
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reference">
                                <HeaderTemplate>
                                    Reference
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPType" runat="server" Text='<%#Eval("Reference")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cur">
                                <HeaderTemplate>
                                    Cur
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSName" runat="server" Text='<%#Eval("Cur")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <HeaderTemplate>
                                    Amount
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <HeaderTemplate>
                                    Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due">
                                <HeaderTemplate>
                                    Due
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProStatus" runat="server" Text='<%#Eval("Due")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Status">
                                <HeaderTemplate>
                                    File Status
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProStatus" runat="server" Text='<%#Eval("File_Status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status Date">
                                <HeaderTemplate>
                                    Status Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProStatus" runat="server" Text='<%#Eval("Status Date")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <HeaderTemplate>
                                    Remarks
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProStatus" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderTemplate>
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgView" runat="server" OnClientClick='<%#"OpenScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                    CommandName="View" ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png"
                                                    Height="16px"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                       <uc1:uccustompager id="ucCustomPager1" runat="server" pagesize="30" onbinddataitem="BindOnlineInvoice" />
        <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                </div>
                 <br />
                  <div id="dvTransferCost" visible="false" runat="server" style="height: 150px; overflow-y: scroll;
                    max-height: 150px">
                    <table>
                        <tr>
                            <td align="left" style="width: 8%; color: red;">
                               Invoice Cost Transfer
                            </td>
                        </tr>
                    </table>
                      <asp:GridView ID="gvTransferCost" runat="server"  AutoGenerateColumns="False" 
                                                      EmptyDataText="NO RECORDS FOUND" GridLines="Both" 
                                                      PagerStyle-Mode="NextPrevAndNumeric" 
                          Width="100%">
                                                      <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                      <RowStyle CssClass="RowStyle-css" />
                                                      <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                      <SelectedRowStyle BackColor="#FFFFCC" />
                                                      <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" 
                                                          HorizontalAlign="Center" />
                     <Columns>
                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <HeaderTemplate>
                                    ID
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceP" runat="server" Text='<%#Eval("Parent_Invoice_Id")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <HeaderTemplate>
                                    Transfer Id
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTransferId" runat="server" Text='<%#Eval("Transfer_Id")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <HeaderTemplate>
                                    Supply Id
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPSupply" runat="server" Text='<%#Eval("Parent_Supply_Id")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Right">
                                <HeaderTemplate>
                                    Transfer Amount
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPAmount" runat="server" Text='<%#Eval("Transfer_Amount","{0:n}")%>'></asp:Label>
                                  
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Right">
                                <HeaderTemplate>
                                    Transfer Status
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPTStatus" runat="server" Text='<%#Eval("Transfer_Status","{0:n}")%>'></asp:Label>
                                    
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Right">
                                <HeaderTemplate>
                                    Vessel_Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPVessel" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                      <asp:Label ID="lblVesselCode" runat="server" Visible="false" Text='<%#Eval("New_Vessel_Code")%>'></asp:Label>
                                      <asp:Label ID="Label7" runat="server" Visible="false" Text='<%#Eval("New_Vessel_ID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Right">
                                <HeaderTemplate>
                                   Description
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPDesc" runat="server" Text='<%#Eval("Transfer_Description")%>'></asp:Label>
                                     <asp:Label ID="lblOwner" runat="server" Visible="false" Text='<%#Eval("New_Owner_Code")%>'></asp:Label>
                                      <asp:Label ID="Label8" runat="server" Visible="false" Text='<%#Eval("New_Account_Classification")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                       
                            <asp:TemplateField HeaderText="Payment Status" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    Action
                                </HeaderTemplate>
                                 <ItemTemplate>
                                    <table cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td>
                                              <asp:ImageButton ID="lbtnTransfer2" runat="server" OnClientClick='<%#"OpenPOTransfer2((&#39;"+ Eval("Transfer_Id") +"&#39;),(&#39;"+ Eval("Parent_Supply_Id") +"&#39;));return false;"%>'
                                                    CommandName="Select" ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png"
                                                    Height="16px"></asp:ImageButton>
                                            
                                            </td>
                                            <td>
                                                   
                                     <asp:ImageButton ID="ImageButton1" runat="server" OnCommand="btnDeleteCost_Click" OnClientClick="return confirm('Are you sure want to delete?')"
                                                    Text="Update" CommandName="Select" ForeColor="Black" CommandArgument='<%#Eval("[Transfer_Id]")%>'
                                                    ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                            </td>
                                            <td>
                                         
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                            <uc1:uccustompager id="ucCustomPager2" runat="server" pagesize="30" onbinddataitem="BindTransferCost" />
        <asp:HiddenField ID="HiddenField3" runat="server" EnableViewState="False" />
                </div>
                <br />
                <div>
                 
                                <div ID="divDeliveryDetails" runat="server" style="height: 100px; overflow-y: scroll;
                                                max-height: 100px" visible="false">
                                                 <asp:Label ID="lblDelivery" runat="server" ForeColor="Red" Text=""></asp:Label>
                                    <asp:GridView ID="gvDeliveryDetails" runat="server" AllowSorting="true" 
                                        AutoGenerateColumns="False" CellPadding="1" CellSpacing="0" 
                                        CssClass="gridmain-css" DataKeyNames="ID" EmptyDataText="NO RECORDS FOUND" 
                                        GridLines="both" Width="100%">
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <HeaderTemplate>
                                                    Delivery Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivery_Date" runat="server" 
                                                        Text='<%#Eval("Delivery_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" 
                                                    Width="60px" Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery At">
                                                <HeaderTemplate>
                                                    Delivery At
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivery_Location" runat="server" 
                                                        Text='<%#Eval("Delivery_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="100px" 
                                                    Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Description">
                                                <HeaderTemplate>
                                                    Item Description
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivered_Item_Description" runat="server" 
                                                        Text='<%#Eval("Delivered_Item_Description")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="120px" 
                                                    Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Qty">
                                                <HeaderTemplate>
                                                    Item Qty
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivered_Qty" runat="server" 
                                                        Text='<%#Eval("Delivered_Qty")%>'></asp:Label>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblDelivered_Item_Unit" runat="server" 
                                                        Text='<%#Eval("Delivered_Item_Unit")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" 
                                                    Width="100px" Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Value">
                                                <HeaderTemplate>
                                                    Item Value
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivered_Item_Price" runat="server" 
                                                        Text='<%#Eval("Delivered_Item_Price")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" Width="80px" 
                                                    Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line Total">
                                                <HeaderTemplate>
                                                    Line Total
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivered_Item_Value" runat="server" 
                                                        Text='<%#Eval("Delivered_Item_Value")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" Width="80px" 
                                                    Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                 </div>
                <br />
                <div ID="divDuplicateInvoice" runat="server" style="height: 150px; overflow-y: scroll;
                                                max-height: 150px" visible="false">
                                    <asp:Label ID="lblDuplicateInvoice" runat="server" ForeColor="Red" 
                                        Text="Duplicate Invoice"></asp:Label>
                                    <asp:GridView ID="gvDuplieCateinvoice" runat="server" AllowSorting="true" 
                                        AutoGenerateColumns="False" CellPadding="1" CellSpacing="0" 
                                        CssClass="gridmain-css" DataKeyNames="Invoice_ID" 
                                        EmptyDataText="NO RECORDS FOUND" GridLines="both" Width="100%">
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <HeaderTemplate>
                                                    Supplier Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="150px" 
                                                    Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Code">
                                                <HeaderTemplate>
                                                    PO Code
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="100px" 
                                                    Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Ref">
                                                <HeaderTemplate>
                                                    Invoice Ref
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Ref" runat="server" 
                                                        Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="100px" 
                                                    Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Type">
                                                <HeaderTemplate>
                                                    Invoice Type
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" 
                                                    Width="80px" Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <HeaderTemplate>
                                                    Amount
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" 
                                                    Width="100px" Wrap="true" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                 <%--<div id="divFrame" visible="false" runat="server" style="height: 100%;
                    overflow-y: scroll; max-height: 100%">
                     <iframe id="iframeDoc" src="PO_Log_Invoice_Entry.aspx?Invoice_ID=<%= txtInvoiceID.Text %>"&SUPPLY_ID=<%= txtPOCode.Text %>" width="100%"  height="500"></iframe>
                    </div>--%>
                <div style="display: none;">
                    <asp:TextBox ID="txtInvoiceID" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtPOCode" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="invHidden" runat="server" />
                     <asp:HiddenField ID="InvoiceAmount" runat="server" />
                </div>
            </contenttemplate>
            </asp:UpdatePanel>
        </div>
    </center>
    </form>
</body>
</html>
