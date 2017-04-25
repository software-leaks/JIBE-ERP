<%@ Page Title="Payment Processing" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="PO_Log_Invoice_Payment_Processing.aspx.cs" Inherits="PO_LOG_PO_Log_Invoice_Payment_Processing" %>

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
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
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
        .ob_iLboICBC li
        {
            float: left;
            width: 150px;
        }
        /* For IE6 */* HTML .ob_iLboICBC li
        {
            -width: 135px;
        }
        * HTML .ob_iLboICBC li b
        {
            width: 135px;
            overflow: hidden;
        }
        .style1
        {
            width: 100%;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function OpenScreen(Supplier_Code, PayMode) {
            var url = 'PO_Log_Payment_Entry.aspx?Supplier_Code=' + Supplier_Code + '&PayMode=' + PayMode;
            OpenPopupWindowBtnID('PO_Log_Payment_Entry', 'Payment Entry', url, 'popup', 800, 1100, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen1(ID, Job_ID) {
            var Type = 'POLOG';
            var url = 'PO_Log_AuditTrail.aspx?Code=' + ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_AuditTrail', 'PO Log Transaction History', url, 'popup', 500, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen2(ID, Job_ID) {
            var Type = 'POLOG';
            var url = 'PO_Log_Supplier_Details.aspx?Code=' + ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_Supplier_Details', 'PO History', url, 'popup', 700, 1100, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen3(ID, Job_ID) {
            var Type = 'Invoice';
            var url = 'PO_Log_Remarks_History.aspx?ID=' + ID + '&Invoice_ID=' + Job_ID;
            OpenPopupWindowBtnID('PO_Log_Remarks_History', 'Remarks History', url, 'popup', 500, 900, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Invoice Payment Processing
            </div>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; color: Black; font-size: 12px;">
                <table>
                    <tr>
                        <td align="left" style="width: 58%;" valign="top">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="4" align="left">
                                        Invoice Payment Arrangements for Currency : &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="txtInput" Width="200px">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Include suppliers on Auto Setup
                                        <asp:CheckBox ID="chkAuto" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnGet" runat="server" OnClick="btnGet_Click" Text="Search" Width="130px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--OnClientClick='<%#"OpenScreen((&#39;" + Eval("[Supplier_Code]") +"&#39;),(&#39;"+ Eval("[PayMode]") + "&#39;));return false;"%>'--%>
                                        <%--  <asp:LinkButton ID="lbl_SupplierName" runat="server" 
                                                                Text='<%#Eval("Supplier_Name") %>' Style="color: Black"></asp:LinkButton>--%>
                                        <div id="divApprovedinvoice" runat="server" style="margin-left: auto; height: 400px;
                                            max-height: 400px; overflow-y: scroll; margin-right: auto; text-align: center;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        Summary of approved invoices ready for payment
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="gvApprovedPaymentinvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                AutoGenerateColumns="False" DataKeyNames="Supplier_Code" CellPadding="1" CellSpacing="0"
                                                Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true" OnRowDataBound="gvApprovedPaymentinvoice_RowDataBound"
                                                OnSorting="gvApprovedPaymentinvoice_Sorting">
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderTemplate>
                                                            Action
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" OnCommand="btnView_Click" Text="Update"
                                                                CommandName="Select" ForeColor="Black" CommandArgument='<%#Eval("[Supplier_Code]")%>'
                                                                ToolTip="View" ImageUrl="~/Images/asl_view.png" Height="16px"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <HeaderTemplate>
                                                            Supplier Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplier_Type" runat="server" Text='<%#Eval("Supplier_Type")%>'></asp:Label>
                                                            <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                                            <asp:Label ID="lblSupplier_Name" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Payment">
                                                        <HeaderTemplate>
                                                            Last Payment
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLPayment" runat="server" Text='<%#Eval("Last_Payment_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cur">
                                                        <HeaderTemplate>
                                                            Cur
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="25px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMarkedAmount" runat="server" Text='<%#Eval("MarkedAmount","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMarkedCount" runat="server" Text='<%#Eval("MarkedCount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="25px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPP" runat="server" Text='<%#Eval("PP","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCP" runat="server" Text='<%#Eval("CP")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="25px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblP0" runat="server" Text='<%#Eval("P0","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblC0" runat="server" Text='<%#Eval("C0")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="25px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblP1" runat="server" Text='<%#Eval("P1","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblC1" runat="server" Text='<%#Eval("C1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="25px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblP2" runat="server" Text='<%#Eval("P2","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblC2" runat="server" Text='<%#Eval("C2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="25px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblP3" runat="server" Text='<%#Eval("P3","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblC3" runat="server" Text='<%#Eval("C3")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="25px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindApprovedPaymentInvoice" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 5%;">
                        </td>
                        <td align="right" style="width: 33%;" valign="top">
                            <div id="div2" runat="server" style="margin-left: auto; height: 40px; margin-right: auto;
                                text-align: center;">
                            </div>
                            <div id="div1" runat="server" style="margin-left: auto; height: 400px; max-height: 400px;
                                overflow-y: scroll; margin-right: auto; text-align: center;">
                                <asp:GridView ID="gvPaymentDetails" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" AllowSorting="true" ShowFooter="True" OnRowDataBound="gvPaymentDetails_RowDataBound">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <FooterStyle CssClass="FooterStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Type">
                                            <HeaderTemplate>
                                                Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("Req_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Owner">
                                            <HeaderTemplate>
                                                Owner Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOwner" runat="server" Text='<%#Eval("Owner_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                PO & INVOICE
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                                <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                                <asp:Label ID="lblInvoice_Code" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="180px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Payment Due Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayment_Due_Date" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Payment Approved By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayment_Approved_By" runat="server" Text='<%#Eval("Payment_Approved_By")%>'></asp:Label>
                                                <asp:Label ID="lbler" runat="server" Text="|"></asp:Label>
                                                <asp:Label ID="lblPayment_Approved_Date" runat="server" Text='<%#Eval("Payment_Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lbl" runat="server" Text="Total"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="160px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Invoice Value
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Amount","{0:N2}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Cur
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="25px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Urgency
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUrgency" runat="server" Text='<%#Eval("Urgency")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Payment Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayment_Status" runat="server" Text='<%#Eval("Payment_Status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkInvoice" runat="server" />
                                                            <%--<asp:ImageButton ID="ImgView" runat="server" OnClientClick='<%#"OpenScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                CommandName="Select" ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png"
                                                                Height="16px"></asp:ImageButton>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <table>
                                    <tr>
                                        <td >
                                            Total Amount marked for Payment &nbsp;:&nbsp;
                                            <asp:Label ID="lblTotalAmount" runat="server" Text="" CssClass="txtInput" ></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblCurrency" runat="server" Text="" CssClass="txtInput" ></asp:Label>
                                        </td>
                                         
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td align="left" valign="top" style="width: 30%;">
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; color: Black; font-size: 12px;
                                overflow-y: scroll; width: 100%; height: 400px;">
                                <table>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Button ID="btnNew" runat="server" Text="Create New Payment" OnClick="btnNew_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblmsg" runat="server" CssClass="txtInput"  Text="Payment Record(Only 20 record will be shown)"></asp:Label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 50%; height: 300px;">
                                            <asp:GridView ID="gvNewPayment" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                DataKeyNames="PAYMENT_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                AllowSorting="true">
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <HeaderTemplate>
                                                            No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblType" runat="server" Text='<%#Eval("PAYMENT_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <HeaderTemplate>
                                                            Amount
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOwner" runat="server" Text='<%#Eval("PAYMENT_AMOUNT","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cur">
                                                        <HeaderTemplate>
                                                            Cur
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("PAYMENT_CURRENCY")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Count
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayment_Due_Date" runat="server" Text='<%#Eval("INVOICE_COUNT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value Date">
                                                        <HeaderTemplate>
                                                            Value Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayment_Approved_By" runat="server" Text='<%#Eval("PAYMENT_DATE","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mode">
                                                        <HeaderTemplate>
                                                            Mode
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("PAYMENT_MODE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Account Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Bank_Account_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Status
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUrgency" runat="server" Text='<%#Eval("PAYMENT_STATUS")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderTemplate>
                                                            Action
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table cellpadding="2" cellspacing="2">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgPaymentView" OnCommand="ImgPaymentView_Click" runat="server"
                                                                            CommandArgument='<%#Eval("[PAYMENT_ID]") + "," + Eval("[PAYMENT_YEAR]")%>' CommandName="Select"
                                                                            ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png" Height="16px">
                                                                        </asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; color: Black; font-size: 12px;
                                width: 100%; height: 400px;">
                                <table style="width: 100%;">
                                     <tr>
                                        <td align="right">
                                          <asp:Label ID="lblPaymentID" runat="server" CssClass="txtInput" ></asp:Label> 
                                        </td>
                                        <td align="left" colspan="3">
                                           <asp:Label ID="lblSupplierName" runat="server" CssClass="txtInput" ></asp:Label>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Payment Amount :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPaymentAmount" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Bank Reference :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBankRef" runat="server" CssClass="txtInput" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Payment Date :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPayDate" runat="server" CssClass="txtInput" Width="300px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtPayDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtPayDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            Payment Mode :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlPayMode" Width="300px" CssClass="txtInput" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Source Account :
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:DropDownList ID="ddlAccount" Width="300px" CssClass="txtInput" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Bank Amount :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBankAmt" CssClass="txtInput" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Bank Charges :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBankCharge" CssClass="txtInput" Width="300px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Payment Mode :
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:RadioButtonList ID="rdbPaymode" RepeatDirection="Horizontal" Width="300px" CssClass="txtInput"
                                                runat="server">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Remarks :
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Height="50px" Width="300px" CssClass="txtInput"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Journal :
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox ID="txtJournal" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 20px;" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:Button ID="btnUpdate" runat="server" Enabled="false" Text="Update Payment" OnClick="btnUpdate_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:Label ID="lblSupplierPaymentID" runat="server" Visible="false" CssClass="txtInput" Text=""></asp:Label><asp:Label
                                                ID="lblPaymentYear" runat="server" Visible="false" CssClass="txtInput" Text=""></asp:Label>
                                            <asp:Label ID="lblSName" Visible="false" CssClass="txtInput" runat="server"
                                                Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                           
                        </td>
                         <td align="left" valign="top" style="width: 20%;">
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; color: Black; font-size: 12px;
                                width: 100%; height: 300px;">
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Button ID="btnlink" runat="server" Enabled="false" Text="Link To selected Payment" OnClick="btnlink_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 50%; height: 300px;">
                                            <asp:GridView ID="gvlinkPayment" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                                Width="100%" GridLines="both" AllowSorting="true">
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <HeaderTemplate>
                                                            No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblType" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Owner">
                                                        <HeaderTemplate>
                                                            Owner
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOwner" runat="server" Text='<%#Eval("Owner")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            PO Code
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("PO_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Due Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayment_Due_Date" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Invoice Ref
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayment_Approved_By" runat="server" Text='<%#Eval("Payment_Approved_By")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Invoice Value
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Cur
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Cur")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <HeaderTemplate>
                                                            Urgency
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUrgency" runat="server" Text='<%#Eval("Urgency")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                             <div style="font-family: Tahoma; color: Black; font-size: 12px; overflow-y: scroll;
                                width: 100%; height: 100px;">
                                <asp:GridView ID="gvInvoice" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    DataKeyNames="PAYMENT_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                    AllowSorting="true" onrowdatabound="gvInvoice_RowDataBound">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Type">
                                            <HeaderTemplate>
                                                No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("Srno")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice_Reference">
                                            <HeaderTemplate>
                                                Invoice Reference
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Reference" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lbl_Total" runat="server" Text="Total" ></asp:Label>
                                                </div>
                                            </FooterTemplate>
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
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Invoice Cur">
                                            <HeaderTemplate>
                                               Cur
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                            </ItemTemplate>
                                           
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnDelete" runat="server" OnCommand="btnDelete_Click"
                                                                OnClientClick="return confirm('Are you sure want to delete?')" Text="Update"
                                                                CommandName="Select" ForeColor="Black" CommandArgument='<%#Eval("[PAYMENT_ID]")%>'
                                                                ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table width="100%">
                    <tr>
                        <td align="left" valign="top" style="width: 65%;">
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; color: Black; font-size: 12px;
                                overflow-y: scroll; width: 100%; height: 500px;">
                                <asp:GridView ID="gvApprovedInvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel">
                                            <HeaderTemplate>
                                                No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("Srno")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
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
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Code">
                                            <HeaderTemplate>
                                                PO Code
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Value">
                                            <HeaderTemplate>
                                                PO Value
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("Line_Amount")%>'></asp:Label>&nbsp;&nbsp;
                                                <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("Line_Currency")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
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
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="90px" CssClass="PMSGridItemStyle-css">
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
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Verified">
                                            <HeaderTemplate>
                                                Verified By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Verified_By")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Type Status">
                                            <HeaderTemplate>
                                                Invoice Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Verified">
                                            <HeaderTemplate>
                                                Approved By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApproved_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Date">
                                            <HeaderTemplate>
                                                Approved Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApproved_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Value">
                                            <HeaderTemplate>
                                                Invoice Amount
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>&nbsp;&nbsp;
                                                <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="90px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Confirmed Delivered">
                                            <HeaderTemplate>
                                                Invoice Due Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDelivered" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Due Date">
                                            <HeaderTemplate>
                                                Urgency
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentDate" runat="server" Text='<%#Eval("Urgency")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black" Width="18px" ToolTip="Compare PO and Invoice"
                                                    OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindApprovedPaymentInvoice" />
                                <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                            </div>
                        </td>
                        <td align="left" valign="top" style="width: 35%;">
                            <div style="border: 1px solid #cccccc; font-family: Tahoma; color: Black; font-size: 12px;
                                width: 100%; height: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                Supplier Details
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Supplier Name :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtSupplierName" runat="server" Enabled="false" Height="40px" TextMode="MultiLine"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Payment Terms :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPaymentTerms" runat="server" Enabled="false" Height="40px" TextMode="MultiLine"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Address :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAddress" runat="server" Enabled="false" TextMode="MultiLine"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Payment Instructions :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPayment" runat="server" Enabled="false" TextMode="MultiLine"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Country/City :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCity" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Payment Notifications :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtpaymentnotification" runat="server" Enabled="false" TextMode="MultiLine"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Email :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmail" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Phone :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPhone" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Fax :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFax" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="display: none;">
                <asp:TextBox ID="txtSupplierCode" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtInvoiceCode" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtPaymodeID" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtPayment_Year" runat="server"></asp:TextBox>
            </div>
        </div>
    </center>
</asp:Content>
