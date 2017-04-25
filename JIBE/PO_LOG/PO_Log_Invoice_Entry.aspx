<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Invoice_Entry.aspx.cs"
    Inherits="PO_LOG_PO_Log_Invoice_Entry" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script language="javascript" type="text/javascript">
        function Validation() {
            if (document.getElementById("ddlType").value == "0") {
                alert("Invoice Type is mandatory.");
                document.getElementById("ddlType").focus();
                return false;
            }
            if (document.getElementById("txtInvoiceDate").value == "") {
                alert("Invoice Date is mandatory.");
                document.getElementById("txtInvoiceDate").focus();
                return false;
            }
            if (document.getElementById("txtReceivedDate").value == "") {
                alert("Received Date is mandatory.");
                document.getElementById("txtReceivedDate").focus();
                return false;
            }
            if (document.getElementById("txtReceivedDate").value == "") {
                alert("Received Date is mandatory.");
                document.getElementById("txtReceivedDate").focus();
                return false;
            }
            var InvoiceType = document.getElementById("ddlType").value;

            if (InvoiceType == "CREDIT") {
                var InvoiceValue = document.getElementById("txtInvoiceValue").value;
                if (InvoiceValue > 0) {
                    alert("Invoice Value should be negative for credit note Invoice Type.");
                    document.getElementById("txtInvoiceValue").focus();
                }
                return false;
            }
            else {
                var InvoiceValue = document.getElementById("txtInvoiceValue").value;
                if (InvoiceValue < 0) {
                    alert("Invoice Value should be positive.");
                    document.getElementById("txtInvoiceValue").focus();
                }
                return false;
            }
            return true;
        }
        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }

        function OpenScreen(ID, Job_ID) {
            var InvoiceID = document.getElementById("txtInvoiceID").value;
            var Type = 'Invoice';
            var url = 'PO_Log_Attachment.aspx?ID=' + InvoiceID + '&DocType=' + Type;
            OpenPopupWindowBtnID('PO_Log_Attachment', 'PO Log Attachment', url, 'popup', 450, 700, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenScreen3(ID, Job_ID) {
            var POID = document.getElementById("txtInvoiceID").value;
            var Type = 'Invoice';
            var url = 'PO_Log_Remarks.aspx?ID=' + ID + '&POID=' + POID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_Remarks', 'PO Log Remarks Entry', url, 'popup', 500, 700, null, null, false, false, true, null, 'btnFilter');
        }
        function DocOpen(docpath) {
            window.open(docpath);
        }

        function previewDocument(docPath) {
            document.getElementById("ifrmDocPreview").src = docPath;
        }

        function getImageopen(str) {
            window.open(str, "file", "menubar=0,resizable=0,width=750,height=550,resizeable=yes");
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        .style2
        {
            height: 46px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Invoice Entry
            </div>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                <table width="100%">
                    <tr>
                        <td align="left">
                            Debit Note ID :&nbsp;&nbsp;
                            <asp:Label ID="lblDebit" CssClass="txtInput" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Invoice ID :&nbsp;&nbsp;
                            <asp:Label ID="lblInvoiceID" CssClass="txtInput" runat="server"></asp:Label>&nbsp;&nbsp;
                            Journal :&nbsp;&nbsp;
                            <asp:Label ID="lblJournal" CssClass="txtInput" runat="server"></asp:Label>&nbsp;&nbsp;
                            Payment Terms :&nbsp;&nbsp;
                            <asp:Label ID="lblpaymentTerms" CssClass="txtInput" runat="server"></asp:Label>.&nbsp;
                            &nbsp;&nbsp;&nbsp; GST Rate :&nbsp;&nbsp;
                            <asp:Label ID="lblGstRate" CssClass="txtInput" runat="server">  </asp:Label>
                            % &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <contenttemplate>
                    <table width="100%">
                       
                        <tr>
                            <td colspan="9">
                                <asp:Label ID="lblHeadermsg" runat="server" Font-Bold="true" Visible="false" ForeColor="Red"
                                    Text="All Invoices for Owners' expenses must be uploaded in USD Currency."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                              Invoice Type :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlType" Width="200px" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ReqType" runat="server" InitialValue="0" Display="None"
                                    ErrorMessage="Invoice Type is mandatory field." ControlToValidate="ddlType" ValidationGroup="vgSubmit"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Invoice Date :
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtInvoiceDate" runat="server" MaxLength="255" Width="200px" CssClass="txtInput"></asp:TextBox>
                                <cc1:CalendarExtender TargetControlID="txtInvoiceDate" ID="caltxtArrival" Format="dd-MM-yyyy"
                                    runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="ReqInvoiceDate" runat="server" Display="None" ErrorMessage="Invoice Date is mandatory field."
                                    ControlToValidate="txtInvoiceDate" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                             <td align="right">
                                Reference :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtReferance" runat="server" MaxLength="255" Width="200px" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                           
                            <td align="right">
                                Received Date :
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtReceivedDate" runat="server" MaxLength="255" Width="200px" CssClass="txtInput"></asp:TextBox>
                                <cc1:CalendarExtender TargetControlID="txtReceivedDate" ID="CalendarExtender1" Format="dd-MM-yyyy"
                                    runat="server">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="ReqReceDate" runat="server" Display="None" ErrorMessage="Receive Date is mandatory field."
                                    ControlToValidate="txtReceivedDate" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                              <td align="right">
                                Invoice Value (Including Taxes) :
                            </td>
                            <td>
                                <asp:Label ID="lbl1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtInvoiceValue" runat="server" MaxLength="255" Text="0.00" Width="200px"
                                    CssClass="txtInput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqInvoiceValue" runat="server" Display="None" ErrorMessage="Invoice Value is mandatory field."
                                    ControlToValidate="txtInvoiceValue" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegInvoiceValue" runat="server" ErrorMessage="Invoice value is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtInvoiceValue"
                                    ForeColor="Red" ValidationExpression="^[0-9.-]+$">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Currency :
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCurrency" Width="200px" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ReqCurrency" runat="server" InitialValue="0" Display="None"
                                    ErrorMessage="Currency is mandatory field." ControlToValidate="ddlCurrency" ValidationGroup="vgSubmit"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                GST/VAT :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtGST" runat="server" MaxLength="255" Text="0.00" Width="200px"
                                    CssClass="txtInput"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegGst" runat="server" ErrorMessage="GST/VAT is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtGST" ForeColor="Red"
                                    ValidationExpression="^[0-9.]+$">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Due Date :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDueDate" runat="server" MaxLength="255" Width="200px" CssClass="txtInput"></asp:TextBox>
                                <cc1:CalendarExtender TargetControlID="txtDueDate" ID="CalendarExtender2" Format="dd-MM-yyyy"
                                    runat="server">
                                </cc1:CalendarExtender>
                            </td>
                             <td align="right">
                                Inv Status :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtInvStatus" runat="server" MaxLength="255" Enabled="false" Width="200px"
                                    CssClass="txtInput"></asp:TextBox>
                            </td>
                         
                        </tr>
                        <tr>
                           <td align="right">
                                Payment Due :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPaymentDate" runat="server" MaxLength="255" Enabled="false" Width="200px"
                                    CssClass="txtInput"></asp:TextBox>
                                <%--  <cc1:CalendarExtender TargetControlID="txtPaymentDate" ID="CalendarExtender3" Format="dd-MM-yyyy"
                            runat="server">OnClientClick='OpenScreen(null,null);return false;'
                        </cc1:CalendarExtender>--%>
                            </td>
                            <td align="right">
                                Remarks :
                            </td>
                            <td>
                            </td>
                            <td colspan="4" align="left">
                                <asp:TextBox ID="txtRemarks" runat="server" MaxLength="1000" TextMode="MultiLine"
                                    Width="535px" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                Urgent :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblUrgent"   Width="100px" runat="server" Text=""> </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right">
                                Payment Priority :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPayment" runat="server"  Width="100px" Text=""> </asp:Label>
                               
                            </td>
                                <td align="right">
                             <asp:Label ID="lblPendingApproval" runat="server" Text="Approval :"> </asp:Label>   
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblApproval" runat="server" Text=""> </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblApprovalDate" runat="server" Text=""> </asp:Label>
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td align="right">
                                Verification :
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblverification" runat="server" Text=""> </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblVerificationDate" runat="server" Text=""> </asp:Label>
                            </td>
                        <td align="right">
                                For Action By :
                            </td>
                            <td>
                            </td>
                            <td colspan="4" align="left">
                                <asp:Label ID="lblAction" runat="server" Text=""> </asp:Label>
                            </td>
                        </tr>
                       
                          <tr>
                            <td colspan="10" class="style2">
                                
                                <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="vgSubmit" />
                                
                                <%--OnClientClick='OpenScreen(null,null);return false;'--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" OnClick="btnSave_Click"
                                    ValidationGroup="vgSubmit" />&nbsp;&nbsp;
                                <asp:Button ID="btnVerified" runat="server" Text="Verify" Width="80px" Visible="false"
                                    ValidationGroup="vgSubmit" OnClick="btnVerified_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnUnVerified" runat="server" Text="UN-Verify" ValidationGroup="vgSubmit"
                                    Visible="false" Width="80px" OnClick="btnUnVerified_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" Width="80px"
                                    OnClick="btnCancel_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" ValidationGroup="vgSubmit"
                                    Visible="false" Width="80px" OnClick="btnApprove_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnUnApprove" runat="server" Text="UN-Approve" ValidationGroup="vgSubmit"
                                    Visible="false" Width="80px" OnClick="btnUnApprove_Click" />&nbsp;&nbsp;
                              <asp:Button ID="btnCTM" runat="server" Text="CTM Transaction" ValidationGroup="vgSubmit"
                                     Width="130px" onclick="btnCTM_Click" OnClientClick="javascript:return confirm('Confirm mark as CTM Transaction?')"  />&nbsp;&nbsp;
                                <asp:Button ID="btnAttachment" runat="server" Text="Add Attachment" 
                                     OnClick="btnAttachment_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnUrgent" runat="server" Text="Urgent" ValidationGroup="vgSubmit"
                                  Width="80px" onclick="btnUrgent_Click"  />&nbsp;&nbsp;
                                <asp:Button ID="btnExit" Width="80px" runat="server" ForeColor="Red" OnClientClick="refreshAndClose();"
                                    Text="Exit" />
                                      <asp:Button ID="btnDispute" runat="server" Visible="false" Width="1px" Text="Dispute" OnClientClick='OpenScreen3(null,null);return false;'
                                     />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" align="center">
                                      <asp:Button ID="btnInvoiceRevoke" runat="server"  
                                          Text="Revoke Invoice Approval" onclick="btnInvoiceRevoke_Click" 
                                          Visible="False" />&nbsp;&nbsp;
                                      <asp:Button ID="btnPaymentRevoke" runat="server"  
                                          Text="Revoke Payment Approval" onclick="btnPaymentRevoke_Click" 
                                          Visible="False" /></td>
                        </tr>
                       
                        <tr>
                     
                            <td colspan="5">
                                
                            </td>
                        </tr>
                    </table>
                    <div style="display: none;">
                        <asp:TextBox ID="txtInvoiceID" runat="server" Width="1px"></asp:TextBox>
                    </div>
                </contenttemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpD1" runat="server">
            <contenttemplate>
                <div id="divAttachment" runat="server" visible="false" style="height: 200px; overflow-y: scroll;
                    max-height: 200px">
                    <asp:GridView ID="gvReqsnAttachment" runat="server" AutoGenerateColumns="False" EmptyDataText="No attachment found."
                        CellPadding="2" Width="100%" GridLines="None" CssClass="GridView-css" 
                        onrowdatabound="gvReqsnAttachment_RowDataBound">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:BoundField HeaderText="Attachment name" DataField="Attachment_Name" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Uploaded By" DataField="Uploaded_By" />
                            <asp:BoundField HeaderText="Uploaded On" DataField="Uploaded_On" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%" style="border: none">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgView" runat="server" OnCommand="ImgView_Click" style="width: 15px; height: 15px" CommandArgument='<%#Eval("[File_Path]")%>'
                                                    ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png"></asp:ImageButton>
                                            </td>
                                            <td>
                                               
                                                    <asp:ImageButton ID="imgDownload" runat="server" OnCommand="ImgDownload_Click" style="width: 15px; height: 15px" CommandArgument='<%#Eval("[File_Path]")%>'
                                                    ForeColor="Black" ToolTip="Download" ImageUrl="~/Images/Download-icon.png"></asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Images/delete.png" runat="server"
                                                    OnClientClick="javascript:var a =confirm('Are you sure to delete this file ?'); if(a) return true; else return false;"
                                                    OnClick="imgbtnDelete_Click" CommandArgument='<%#Eval("id")+","+Eval("Link_ID")+","+Eval("File_Path") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </contenttemplate>
        </asp:UpdatePanel>
        <div id="divIframe" runat="server" visible="false" style="border: 1px solid #cccccc;
            height: 400px; font-family: Tahoma; font-size: 12px; width: 100%;">

            <iframe id="iFrame1" runat="server" width="100%" height="100%"></iframe>

            <asp:HiddenField ID="hdnFilePath" runat="server" />
        </div>
        <div id="dvAttachment" title="Add Attachments" style="display: none; width: 500px;">
            <div class="error-message" onclick="javascript:this.style.display='none';">
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        Attachment
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click">
                        </asp:Button>
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblhdn" runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txthdn" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>
