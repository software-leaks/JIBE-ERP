<%@ Page Language="C#"  MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ASL_InvoiceStatus.aspx.cs"  Inherits="ASL_ASL_InvoiceStatus" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
        function OpenScreen(Supply_ID, File_ID) {
            var url = 'ASL_Supplier_Upload.aspx?Supply_ID=' + Supply_ID + '&File_ID=' + File_ID;
            OpenPopupWindowBtnID('ASL_Supplier_Upload', 'Supplier Upload', url, 'popup', 800, 1100, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
    </script>
    <script type="text/javascript">
        /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
        $(document).ready(function () {
            window.parent.$("#ASL_InvoiceStatus").css("height", (parseInt($("#pnl").height()) + 50) + "px");
            window.parent.$(".xfCon").css("height", (parseInt($("#pnl").height()) + 50) + "px").css("top", "50px");
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnl" runat="server" Visible="true">   
                        <div id="Div1" class="page-title">
                            Invoice Status
                        </div>
                  
            <table width="100%" cellpadding="2" cellspacing="0">
             
                <tr>
                    <td align="left" colspan="3">
                        <div id="div1" runat="server">
                            <font size="3" color="Red">Supplier Invoice Status Summary has been disabled for this
                                supplier. Please contact IT dept for assistance.</font></div>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <div span style="font-size: 28px; font-family: Tahoma;">
                            Supplier PO and Invoice Payment Status.</div>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <div span style="font-size: 20px; font-family: Tahoma;">
                            Company Name :
                            <asp:Label ID="lblSuppliername" runat="server" Width="400px" Text=""></asp:Label></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 20Px;">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        To ensure prompt settlement of your invoices, please take note of the following
                        :<br />
                        1.You are require to indicate our Purchase Order Reference number in your invoice.<br />
                        2.Invoice Currency must always be the same as the Purchase Order Currency.<br />
                        3.All deliveries to vessel must be confirmed with a signed delivery order endorsed
                        with Vessel's stamp. This signed document must be furnished with your invoice.<br />
                        4.Do not combine multiple Purchase Orders into a Single invoice. Each Invoice should
                        be for one Purchase Order.<br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 20Px;">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="color: Blue;" colspan="3">
                        To improve our response to clear your invoices, you can upload your scanned invoices
                        to the respective Purchase Orders. Please take note and follow uploading instructions.
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 20Px;">
                        &nbsp;<div id="div2" runat="server">
                            Click
                            <asp:HyperLink ID="hyper1" runat="server">Here</asp:HyperLink>&nbsp;to view the
                            latest 20 payment records.
                        </div>
                    </td>
                </tr>
                <tr id="tr1" runat="server" visible="false">
                    <td align="left" style="color: Blue; width: 20%;">
                        Filter by Vessel :
                    </td>
                    <td align="left" style="color: Blue; width: 20%;">
                        <asp:DropDownList ID="ddlVessel" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="color: Blue; width: 60%;">
                        <asp:Button ID="btnfilter" Text="Search" runat="server" OnClick="btnfilter_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 20Px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td id="tdPendingInvoice" runat="server" colspan="3" style="height: 20Px; color: Blue;">
                                    Invoices that you have uploaded that pending.
                                </td>
                            </tr>
                        </table>
                        <div id="divPendingInvoice" runat="server">
                        <telerik:RadGrid ID="gvPOPendingInvoice" runat="server" AllowAutomaticInserts="True"
                            GridLines="None" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007"
                            Style="margin-left: 0px" Width="100%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
                            PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
                            OnItemDataBound="gvPOPendingInvoice_RowDataBound" OnItemCommand="gvPOPendingInvoice_ItemCommand">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="SUPPLY_ID,File_ID">
                                <RowIndicatorColumn Visible="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                </RowIndicatorColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Order Code" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderCode" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Vessel Name" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselType" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Type" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Reference"
                                        Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReference" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Date" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Currency" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Line_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Amount" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountStatus" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Due Date"
                                        Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoicePending" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Remarks" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Invoice_Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Pending Action" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFile_Status" runat="server" Text='<%#Eval("File_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="View" Visible="true"
                                        HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="ImgInvViewStatus" runat="server" CommandName="INVOICE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]")  %>'
                                                Text="Edit" />
                                            <%--   <asp:Button ID="ImgCrViewStatus" runat="server"  CommandName="CREDITNOTE"
                                                CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]") %>'
                                                Text="Edit" />--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div id="divStatus" runat="server" style="text-align: center; font-weight: bold;
                            font-size: large; background-color: Blue; color: White;">
                            Invoices submitted online pending completion and submission
                        </div>
                        <div id="divInvoiceStatus" runat="server">
                            <telerik:RadGrid ID="gvInvoiceStatus" runat="server" AllowAutomaticInserts="True"
                                GridLines="None" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007"
                                Style="margin-left: 0px" Width="100%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
                                PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
                                OnItemDataBound="gvInvoiceStatus_RowDataBound" OnItemCommand="gvInvoiceStatus_ItemCommand">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                                </ClientSettings>
                                <MasterTableView DataKeyNames="SUPPLY_ID,File_ID,Invoice_type">
                                    <RowIndicatorColumn Visible="true">
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Date" Visible="true"
                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Code" Visible="true"
                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Value" Visible="true"
                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Line_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Date" Visible="true"
                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceDate" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Reference"
                                            Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferenceInvoice" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Amount" Visible="true"
                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceValue" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Due Date"
                                            Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceDueDate" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Payment Due Date"
                                            Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentDueDate" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Status" Visible="true"
                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Upload" Visible="true"
                                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="ImgInvView" runat="server" CommandName="INVOICE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]")  %>'
                                                                Text="Invoice" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="ImgCrView" runat="server" CommandName="CREDITNOTE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]") %>'
                                                                Text="Credit" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        </td></tr>
                        </table></asp:Panel>
    </div>
    <br />
    &nbsp;&nbsp;&nbsp;
    <div  id="divStatus1" runat="server" style="text-align: center; font-weight: bold; font-size: large; background-color: Blue;
        color: White;">
        PO Awaiting Invoices / uploaded invoices awaiting processing
    </div>
    <div id="divInvoiceStatus1" runat="server">
    <telerik:RadGrid ID="gvInvoiceStatus1" runat="server" AllowAutomaticInserts="True"
        GridLines="None" ShowFooter="True" ViewStateMode="Enabled" Skin="Office2007"
        Style="margin-left: 0px" Width="100%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
        TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
        OnItemDataBound="gvInvoiceStatus1_RowDataBound" OnItemCommand="gvInvoiceStatus1_ItemCommand">
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="SUPPLY_ID,File_ID,Invoice_type">
            <RowIndicatorColumn Visible="true">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
            </RowIndicatorColumn>
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Date" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Code" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Value" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Line_Amount")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Date" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Reference"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceInvoice" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Amount" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceValue" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Due Date"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceDueDate" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Payment Due Date"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentDueDate" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Status" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Upload" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="ImgInvView" runat="server" CommandName="INVOICE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]")  %>'
                            Text="Invoice" />
                        <asp:Button ID="ImgCrView" runat="server" CommandName="CREDITNOTE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]") %>'
                            Text="Credit" />
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    </div>
    <br />
    <div id="divStatus2" runat="server" style="text-align: center; font-weight: bold; font-size: large; background-color: Blue;
        color: White;">
        Invoice received and in process
    </div>
     <div id="divInvoiceStatus2" runat="server">
    <telerik:RadGrid ID="gvInvoiceStatus2" runat="server" AllowAutomaticInserts="True"
        GridLines="None" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007"
        Style="margin-left: 0px" Width="100%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
        PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
        OnItemDataBound="gvInvoiceStatus2_RowDataBound" OnItemCommand="gvInvoiceStatus2_ItemCommand">
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="SUPPLY_ID,File_ID,Invoice_type">
            <RowIndicatorColumn Visible="true">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
            </RowIndicatorColumn>
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Date" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Code" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Value" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Line_Amount")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Date" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Reference"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceInvoice" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Amount" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceValue" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Due Date"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceDueDate" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Payment Due Date"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentDueDate" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Status" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Upload" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="ImgInvView" runat="server" CommandName="INVOICE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]")  %>'
                            Text="Invoice" />
                        <asp:Button ID="ImgCrView" runat="server" CommandName="CREDITNOTE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]") %>'
                            Text="Credit" />
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    </div>
    <br />
    <div  id="divStatus3" runat="server" style="text-align: center; font-weight: bold; font-size: large; background-color: Blue;
        color: White;">
        Invoices Paid</div>
         <div id="divInvoiceStatus3" runat="server">
    <telerik:RadGrid ID="gvInvoiceStatus3" runat="server" AllowAutomaticInserts="True"
        GridLines="None" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007"
        Style="margin-left: 0px" Width="100%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
        TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
        OnItemDataBound="gvInvoiceStatus3_RowDataBound" OnItemCommand="gvInvoiceStatus3_ItemCommand">
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="SUPPLY_ID,File_ID,Invoice_type">
            <RowIndicatorColumn Visible="true">
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
            </RowIndicatorColumn>
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Date" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Line_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Code" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Value" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Line_Amount")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Date" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Reference"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceInvoice" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Amount" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceValue" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Due Date"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceDueDate" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Payment Due Date"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentDueDate" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Invoice Status" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="  Payment Date" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentDate" runat="server" Text='<%#Eval("PAYMENT_DATE","{0:dd-MMM-yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                    </ItemStyle>
                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="  Payment Status"
                    Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentStatus" runat="server" Text='<%#Eval("PAYMENT_STATUS")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                    </ItemStyle>
                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Upload" Visible="true"
                    HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="ImgInvView" runat="server" CommandName="INVOICE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]")  %>'
                            Text="Invoice" />
                        <asp:Button ID="ImgCrView" runat="server" CommandName="CREDITNOTE" CommandArgument='<%#Eval("[Supplier_Code]") + "," + Eval("[SUPPLY_ID]") + "," + Eval("[ID]") %>'
                            Text="Credit" />
                    </ItemTemplate>
                    <ItemStyle Width="10%" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    </div>
        </asp:Panel>
</asp:Content>