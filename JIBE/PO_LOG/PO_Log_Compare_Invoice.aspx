<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Title="Compare Invoice"
    CodeFile="PO_Log_Compare_Invoice.aspx.cs" Inherits="PO_LOG_PO_Log_Compare_Invoice" %>

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
    </style>
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
                Compare Invoice
            </div>
                    <table width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td><asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="vgSubmit" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="vgSubmitWithhold" />
                                        </td>
                                        <td>
                                       
                                        </td>
                                       
                            <td align="left" colspan="2" style="width: 100%" >Rework :
                             <asp:DropDownList ID="ddlRework" CssClass="txtInput" Width="200px" runat="server">
                                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnRework" runat="server" Text="Rework Invoice" OnClick="btnRework_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnDispute" runat="server" Width="100PX" 
                                    Text="Dispute" onclick="btnDispute_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUrgent" ForeColor="Red" runat="server" Width="100PX" 
                                    Text="Urgent" onclick="btnUrgent_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnInvoiceApprove" runat="server" Width="200PX" 
                                    Text="Approve Invoice " onclick="btnInvoiceApprove_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Button ID="btnPaymentApprove" Visible="false" runat="server" Width="200PX" 
                                    Text="Approve Payment " onclick="btnPaymentApprove_Click"  />&nbsp;&nbsp;
                                     <asp:Button ID="btnFinalApprove" Visible="false" runat="server" Width="200PX" 
                                    Text="Final Approve" onclick="btnFinalApprove_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnExit" runat="server" Visible="false"  ForeColor="Red" Width="100PX" 
                                    Text="Exit" onclick="btnExit_Click"
                                     />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="2" style="width: 40%">
                                <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                                        <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td valign="middle" align="right"  > Remarks : <asp:Label ID="lbl1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                               
                                               
                                            </td>
                                            <td align="left"> 
                                                <asp:TextBox ID="txtRemarks" runat="server" MaxLength="2000" TextMode="MultiLine"
                                                    Width="400px" Height="60px" CssClass="txtInput"></asp:TextBox></td>
                                                    <td align="left">  <asp:Button ID="btnRemarks" Text="Add Remarks" ValidationGroup="vgSubmit" runat="server"
                                                    OnClick="btnRemarks_Click" /></td>
                                        </tr>
                                        <tr><td align="center" colspan="3">
                                         
                                                <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="None" ErrorMessage="Remarks is mandatory field."
                                                    ControlToValidate="txtRemarks" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                     </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            <div id="div3" runat="server" style="height: 250px; overflow-y: scroll; max-height: 250px">
                                                <asp:GridView ID="gvRemarks" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                    AllowSorting="true" OnRowDataBound="gvRemarks_RowDataBound">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Created By">
                                                            <HeaderTemplate>
                                                                Created By
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("CREATED_BY")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%#Eval("Created_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Remarks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <HeaderTemplate>
                                                                <table cellpadding="1" cellspacing="1">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Image ID="imgBlue" runat="server" Width="20px" ImageUrl="~/Images/Blue_Square_Flag.jpg" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="imgorange" runat="server" Width="20px" ImageUrl="~/Images/Orange_Square_Flag.jpg" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="imgRed" runat="server" Width="20px" ImageUrl="~/Images/Red_Square_Flag.jpg" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table cellpadding="1" cellspacing="1">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnGeneral" Style="width: 12px; height: 12px;" CommandArgument='<%#Eval("[ID]")  %>'
                                                                                OnCommand="btnGeneral_Click" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnWarning" Style="width: 12px; height: 12px;" CommandArgument='<%#Eval("[ID]")  %>'
                                                                                OnCommand="btnWarning_Click" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnRed" Style="width: 12px; height: 12px;" CommandArgument='<%#Eval("[ID]")  %>'
                                                                                OnCommand="btnRed_Click" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="30px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        
                                           <tr>
                                            <td colspan="3">
                                             <asp:Label ID="lblDelivery" ForeColor="Red" runat="server" Text=""></asp:Label>
                                            <div id="divDeliveryDetails" visible="false" runat="server" style="height: 100px; overflow-y: scroll;
                                                max-height: 100px">
                                                
                                                <asp:GridView ID="gvDeliveryDetails" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                    AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                                    Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
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
                                                                <asp:Label ID="lblDelivery_Date" runat="server" Text='<%#Eval("Delivery_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delivery At">
                                                            <HeaderTemplate>
                                                                Delivery At
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivery_Location" runat="server" Text='<%#Eval("Delivery_Location")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Description">
                                                            <HeaderTemplate>
                                                                Item Description
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivered_Item_Description" runat="server" Text='<%#Eval("Delivered_Item_Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Qty">
                                                            <HeaderTemplate>
                                                                Item Qty
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivered_Qty" runat="server" Text='<%#Eval("Delivered_Qty")%>'></asp:Label>&nbsp;&nbsp;
                                                                <asp:Label ID="lblDelivered_Item_Unit" runat="server" Text='<%#Eval("Delivered_Item_Unit")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Value">
                                                            <HeaderTemplate>
                                                                Item Value
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivered_Item_Price" runat="server" Text='<%#Eval("Delivered_Item_Price","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Line Total">
                                                            <HeaderTemplate>
                                                                Line Total
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDelivered_Item_Value" runat="server" Text='<%#Eval("Delivered_Item_Value","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            </td></tr>
                                           
                                    </table>
                                    </ContentTemplate></asp:UpdatePanel>
                               
                            </td>
                            <td valign="top" colspan="2" style="width: 60%">
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <div id="divPendingPO" runat="server" style="height: 250px; overflow-y: scroll; max-height: 250px">
                                              <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text="Pending Invoice"></asp:Label>
                                                <asp:GridView ID="gvInvoice" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    DataKeyNames="Invoice_ID,Invoice_Status" CellPadding="1" CellSpacing="0" Width="100%"
                                                    GridLines="both" CssClass="gridmain-css" AllowSorting="true" OnRowDataBound="gvInvoice_RowDataBound">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle BackColor="Yellow" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImgView" runat="server" ForeColor="Black" OnCommand="btnView_Click" Width="15px"
                                                                    ToolTip="View" CommandArgument='<%#Eval("Invoice_ID") + "," + Eval("[Invoice_Status]") + "," + Eval("[Payment_Status]") + "," + Eval("[Dispute_Flag]") %>'
                                                                    ImageUrl="~/Images/asl_view.png"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" Width="20px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <HeaderTemplate>
                                                                Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Date">
                                                            <HeaderTemplate>
                                                                Invoice Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inv Ref">
                                                            <HeaderTemplate>
                                                                Inv Ref
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Reference" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Amount">
                                                            <HeaderTemplate>
                                                                Invoice Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delivery Confirmed">
                                                            <HeaderTemplate>
                                                                Delivery Confirmed
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblConfirmed_Delivered_Amount" runat="server" Text='<%#Eval("Confirmed_Delivered_Amount","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice_Status">
                                                            <HeaderTemplate>
                                                                Invoice Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Approved Date">
                                                            <HeaderTemplate>
                                                                Invoice Approved Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApproved_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment<br>Approved Date">
                                                            <HeaderTemplate>
                                                                Payment Approved Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment_Approved_Date" runat="server" Text='<%#Eval("Payment_Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Status">
                                                            <HeaderTemplate>
                                                                Payment Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment_Status" runat="server" Text='<%#Eval("Payment_Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Priority Payment">
                                                            <HeaderTemplate>
                                                                Payment Priority 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment_Priority" runat="server" Text='<%#Eval("Payment_Priority")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dispute Status">
                                                            <HeaderTemplate>
                                                                Dispute Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDispute_Status" ForeColor="Red" runat="server" Text='<%#Eval("Dispute_Flag")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="divDuplicateInvoice" visible="false" runat="server" style="height: 150px; overflow-y: scroll;
                                                max-height: 150px">
                                                <asp:Label ID="lblDuplicateInvoice" ForeColor="Red" runat="server" Text="Duplicate Invoice"></asp:Label>
                                                <asp:GridView ID="gvDuplieCateinvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                    AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                                                    Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
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
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO Code">
                                                            <HeaderTemplate>
                                                                PO Code
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
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
                                                                <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Type">
                                                            <HeaderTemplate>
                                                                Invoice Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Invoice_Amount","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <uc1:ucCustomPager ID="ucCustomPager2" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                                                <asp:HiddenField ID="HiddenField3" runat="server" EnableViewState="False" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                         <tr>
                         <td colspan="2"  align="left" valign="top">
                         <div  id="Div1" runat="server" style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%;">
                           <table width="100%" ><tr><td><asp:Label ID="lblPO" runat="server" Font-Bold="true" CssClass="txtInput" Text=""></asp:Label></td></tr>
                           <tr><td> <asp:Label ID="lblPOReferance" CssClass="txtInput" runat="server" Text=""></asp:Label></td></tr></table>
                           </div>
                         </td>
                          <td colspan="2"  align="left" valign="top">
                         <table  width="100%">
                         <tr>
                         <td colspan="4" > <div  id="Div2" runat="server" style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%;">
                          <asp:Label ID="lblInvoice" CssClass="txtInput" Font-Bold="true" runat="server" Text=""></asp:Label>
                          </div></td>
                         </tr>
                         <tr>
                         
                         <td colspan="4">
                         <div  id="dvWithhold" visible="false" runat="server" style="border: 1px solid #cccccc; font-family: Tahoma; height:100%; font-size: 12px; width: 100%;">
                         <table  width="100%">
                          <tr>
                           <td>Withhold :-  </td>
                         <td>Amount :  <asp:TextBox ID="txtAmount" CssClass="txtInput" runat="server" Width="150px"></asp:TextBox></td><td>Reason :</td>
                         <td><asp:TextBox ID="txtReason" CssClass="txtInput" runat="server" Width="400px"></asp:TextBox></td>
                         <td><asp:Button ID="btnWithhold" runat="server" ValidationGroup="vgSubmitWithhold" Text="Submit" 
                                 onclick="btnWithhold_Click" /></td>
                          </tr>
                           <tr><td colspan="4">
                                  <asp:RegularExpressionValidator ID="RegTaxRate" runat="server" ErrorMessage="Amount is not valid.(0-9.)"
                                                            Display="None" ValidationGroup="vgSubmitWithhold" ControlToValidate="txtAmount" ForeColor="Red"
                                                            ValidationExpression="^[0-9.]+$"> </asp:RegularExpressionValidator>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                            ErrorMessage="Amount is mandatory field." ControlToValidate="txtAmount"
                                                            ValidationGroup="vgSubmitWithhold" ForeColor="Red"></asp:RequiredFieldValidator>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                            ErrorMessage="Reason is mandatory field." ControlToValidate="txtReason"
                                                            ValidationGroup="vgSubmitWithhold" ForeColor="Red"></asp:RequiredFieldValidator>
                                 </td>
                                 </tr>
                         </table>
                         </div>
                         </td>
                        
                                 
                                 </tr>
                                
                         </table>
                        
                         </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2"  align="left" valign="top">
                               <div>
                                      <asp:UpdatePanel ID="UpdatePanelPO" runat="server">
                                        <ContentTemplate>
                                     <iframe id="IframePO" src="" runat="server" width="100%" height="700px"></iframe>
                                      </ContentTemplate>
                                    </asp:UpdatePanel></div>
                                 
                            </td>
                             <td colspan="2" align="left" valign="top">
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
