<%@ Page Title="Payment Approval List" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Invoice_Payment_Approval.aspx.cs" Inherits="PO_LOG_Invoice_Payment_Approval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
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

        function OpenCompareScreen(SUPPLY_ID, Invoice_ID) {
            var PageName = "PaymentApproval";
            var url = "../PO_LOG/PO_Log_Compare_Invoice.aspx?SUPPLY_ID=" + SUPPLY_ID + '&Invoice_ID=' + Invoice_ID + '&PageName=' + PageName
            window.open(url, "_blank");
        }
        function OpenScreen1(ID, Job_ID) {
            var Type = 'POLOG';
            var url = 'PO_Log_AuditTrail.aspx?Code=' + ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_AuditTrail', 'PO Log Transaction History', url, 'popup', 500, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen2(ID, Job_ID) {
            var Type = 'POLOG';
            var url = 'PO_Log_Supplier_Details.aspx?Code=' + ID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_Supplier_Details', 'PO History', url, 'popup', 700, 1400, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenRemarksScreen(ID, Job_ID) {
            var Type = 'Invoice';
            var url = 'PO_Log_Remarks_History.aspx?Invoice_ID=' + ID + '&ID=' + Job_ID;
            OpenPopupWindowBtnID('PO_Log_Remarks_History', 'Remarks History', url, 'popup', 500, 900, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
    </script>
    <script type="text/javascript">
        $("[id*=chkAllInvoice]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkInvoice]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkAllInvoice]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkInvoice]", grid).length == $("[id*=chkInvoice]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
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
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <contenttemplate>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
            <div id="page-title" class="page-title">
                Payment Approval List
            </div>
            <div style="padding-top: 5px; border: 1px padding-bottom: 5px;">
                <table width="100%" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="width: 10%" align="right">
                            Supplier :
                        </td>
                        <td style="width: 30%" align="left">
                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" Width="400px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%" align="right">
                            Vessel :
                        </td>
                        <td colspan="2" style="width: 30%" align="left">
                            <asp:DropDownList ID="ddlVessel" runat="server" CssClass="txtInput" Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%" align="right">
                            Owner : &nbsp;
                        </td>
                        <td colspan="2" valign="top" style="width: 10%" align="left">
                            <asp:DropDownList ID="ddlOwner" runat="server" CssClass="txtInput" Width="400px">
                            </asp:DropDownList>
                        </td>
                        <td valign="top" style="width: 10%" align="left">
                            <asp:CheckBox ID="chkUrgent" runat="server" Text="Urgent" ForeColor="Red" />
                        </td>
                        <td>
                            <asp:Button ID="btnGet" runat="server" OnClick="btnGet_Click" Text="Search" Width="130px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="width: 10%" align="right">
                            <div style="float: left; text-align: left; width: 100%; height: 50px; overflow-x: hidden;
                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                background-color: #ffffff;">
                                <asp:Label ID="lblInvoice" runat="server" BackColor="Red" ForeColor="White" CssClass="txtInput"
                                    Text="PO Date > Invoice Date"></asp:Label>
                                <asp:CheckBoxList ID="chkType" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                </asp:CheckBoxList>
                                <br />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divInvoiceCount" visible="false" runat="server" style="height: 220PX">
                <table width="100%" cellpadding="2" cellspacing="2">
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <asp:Button ID="btnApprovedInvoice" runat="server" Text="View Payment Approved Invoice"
                                Width="200px" OnClick="btnApprovedInvoice_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDisputedInvoice" runat="server" Text="Disputed Invoice" Width="150px"
                                OnClick="btnDisputedInvoice_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAdvanceRequest" runat="server" Text="Advances Request" Width="150px"
                                OnClick="btnAdvanceRequest_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnPaymentPriority" runat="server" Text="Priority Payment" Width="150px"
                                OnClick="btnPaymentPriority_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnWithHoldList" runat="server" Text="Withhold List" Width="150px"
                                OnClick="btnWithHoldList_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 50%;" align="left">
                            <div class="freezing" style="width: 100%;">
                                <telerik:RadGrid ID="gvInviceCount" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                    ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                    Width="100%" AutoGenerateColumns="False"  
                                    AllowMultiRowSelection="True" PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center"
                                    AlternatingItemStyle-BackColor="#CEE3F6" 
                                    onitemdatabound="gvInviceCount_ItemDataBound">
                                    <MasterTableView>
                                        <RowIndicatorColumn Visible="true">
                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Total Invoice Value"
                                                DataField="ID" UniqueName="Report_Name" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Report_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_Invoice" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle Width="120px" HorizontalAlign="Left" />
                                                <HeaderStyle Width="120px"></HeaderStyle>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="30px" HeaderText="Clean"
                                                Visible="true" UniqueName="Clean">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnClean" runat="server" OnCommand="btnClean_Click" Height="20px"
                                                        Width="60px" CommandArgument='<%#Eval("[ID]")%>' Text='<%#Eval("Clean")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_Clean" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Dirty" Visible="true"
                                                UniqueName="Dirty">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDirty" runat="server" OnCommand="btnDirty_Click" Height="20px"
                                                        Width="60px" CommandArgument='<%#Eval("[ID]")%>' Text='<%#Eval("Dirty")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_Dirty" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="30px"></HeaderStyle>
                                               <ItemStyle Width="30px" HorizontalAlign="Center" />
                                               <FooterStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Direct Inv"
                                                Visible="true" UniqueName="DIRECTINV">
                                                <ItemTemplate>
                                                   <asp:Button ID="btnDIRECTINV" runat="server" OnCommand="btnDIRECTINV_Click" Height="20px"
                                                Width="60px" CommandArgument='<%#Eval("[ID]")%>' Text='<%#Eval("DIRECTINV")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_Direct" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px"></HeaderStyle>
                                               <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Retro PO" UniqueName="LATEPO">
                                                <ItemTemplate>
                                                   <asp:Button ID="btnLATEPO" runat="server" OnCommand="btnLATEPO_Click" Height="20px"
                                                Width="60px" CommandArgument='<%#Eval("[ID]")%>' Text='<%#Eval("LATEPO")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                 <asp:Label ID="lblTotal_LatePO" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                 <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Rework IN" UniqueName="REWORKIN">
                                                <ItemTemplate>
                                                       <asp:Button ID="btnREWORKIN" runat="server" OnCommand="btnREWORKIN_Click" Height="20px"
                                                Width="60px" CommandArgument='<%#Eval("[ID]")%>' Text='<%#Eval("REWORKIN")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                   <asp:Label ID="lblTotal_ReworkIN" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Rework OUT" UniqueName="REWORKOUT">
                                                <ItemTemplate>
                                                       <asp:Button ID="btnREWORKOUT" runat="server" OnCommand="btnREWORKOUT_Click" Height="20px"
                                                Width="60px" CommandArgument='<%#Eval("[ID]")%>' Text='<%#Eval("REWORKOUT")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                   <asp:Label ID="lblTotal_ReworkOUT" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                             <telerik:GridTemplateColumn HeaderText="Others" UniqueName="OTHERS">
                                                <ItemTemplate>
                                                      <asp:Button ID="btnOTHERS" runat="server" OnCommand="btnOTHERS_Click" Height="20px"
                                                Width="60px" CommandArgument='<%#Eval("[ID]")%>' Text='<%#Eval("OTHERS")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                   <asp:Label ID="lblTotal_Others" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="None" />
                                            <PopUpSettings ScrollBars="None" />
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                        <td valign="top" style="width: 50%;" align="left">
                            <table id="table1" runat="server" visible="false" style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPayment" runat="server" ForeColor="Blue" Text="Payment approved for this week (Does not include Credit notes approved by Daemon)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Payment schedule (for invoices Approved for payment, pending setup and batch upload)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left">
                                        <asp:GridView ID="gvPaymentApproved" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                            Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true" ShowFooter="false">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <FooterStyle CssClass="FooterStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Report Name">
                                                    <HeaderTemplate>
                                                        Bank Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPayment_Mode" runat="server" Text='<%#Eval("Payment_Mode_Description")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Clean">
                                                    <HeaderTemplate>
                                                        Approved Amount
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApproved_Total" runat="server" Text='<%#Eval("Approved_Total")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:GridView ID="gvPaymentSchedule" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                            Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true" ShowFooter="false">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <FooterStyle CssClass="FooterStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Bank Name">
                                                    <HeaderTemplate>
                                                        Bank Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Bank")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Clean">
                                                    <HeaderTemplate>
                                                        This Week
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWK00" runat="server" Text='<%#Eval("WK00")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dirty">
                                                    <HeaderTemplate>
                                                        Week1
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWK01" runat="server" Text='<%#Eval("WK01")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Direct Inv">
                                                    <HeaderTemplate>
                                                        Week2
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWK02" runat="server" Text='<%#Eval("WK02")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retro PO">
                                                    <HeaderTemplate>
                                                        Week3
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWK03" runat="server" Text='<%#Eval("WK03")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rework">
                                                    <HeaderTemplate>
                                                        Week4
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWK04" runat="server" Text='<%#Eval("WK04")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divApprovedInvoice" visible="false" runat="server" style="border: 1px solid #cccccc;
                font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
                <hr />
                <table style="width: 100%;">
                 
                    <tr>
                    <td align="left" style="font-size: medium;  color: red;">
                        Pending Payment Invoice</td>
                        <td align="right" style="font-size: medium; color: red;">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve Selected Invoice" Width="150px"
                                OnClick="btnApprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnApproveAll" runat="server" Visible="false" Text="Approve All Invoice" Width="150px"
                                OnClick="btnApproveAll_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnRework" runat="server" Text="Rework Selected Invoice" Visible="false" Width="150px"
                                OnClick="btnRework_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Visible="false" Width="150px" OnClick="btnRefresh_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <asp:GridView ID="gvApprovedInvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="Invoice_ID,Dispute_Flag" CellPadding="1" CellSpacing="0"
                                Width="100%" GridLines="both" AllowSorting="true" OnRowDataBound="gvApprovedInvoice_RowDataBound"
                                OnSorting="gvApprovedInvoice_Sorting">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderTemplate>
                                            No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("ROWNUM")%>'></asp:Label>
                                            <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                            <asp:Label ID="lblSupplierType" runat="server" Text='<%#Eval("SType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSupplier_Name" runat="server" CommandName="Sort" CommandArgument="Supplier_Name">Supplier Name</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbl_SupplierName" runat="server" OnClientClick='<%#"OpenScreen2(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("Supplier_Name") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Code" runat="server" CommandName="Sort" CommandArgument="PO_Code">Code</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblPOCode" runat="server" OnClientClick='<%#"OpenScreen1(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("PO_Code") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Amount" runat="server" CommandName="Sort" CommandArgument="PO_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("PO_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("CURRENCY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Total Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Count" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rec Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceived_Date" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblInvoice_Amount" runat="server" CommandName="Sort" CommandArgument="Invoice_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                            Confirmed Delivered
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredAmt" runat="server" Text='<%#Eval("Delivery_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Due Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPayment_Due_Date" runat="server" CommandName="Sort" CommandArgument="Payment_Due_Date"
                                                ForeColor="Black">Payment Due Date</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <table>
                                     <tr>
                                 <td style="border-color: transparent">
                                      <asp:Label ID="lblPaymentDate" runat="server"  Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                      <asp:Label ID="lblUrgentFlag" ForeColor="Red" runat="server" Text="|"></asp:Label>
                                      <asp:Label ID="lblUrgent" ForeColor="Red" runat="server" Text='<%#Eval("Urgent_Flag")%>'></asp:Label>
                                     </td></tr>
                                     <tr>
                                 <td style="border-color: transparent"> 
                                  <asp:Label ID="lblDays" ForeColor="Red" runat="server" Text=""></asp:Label>
                                 </td>
                                 </tr>
                                 </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                            
                                            <asp:CheckBox ID="chkAllInvoice" Visible="false" Text="Select All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInvoice" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Dispute">
                                        <HeaderTemplate>
                                          Dispute
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispute" runat="server" Text=""></asp:Label>
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
                                            <table>
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgViewRemarks" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="View Remarks" OnClientClick='<%#"OpenRemarksScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                          <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                     <td>
                                                         <asp:Button ID="btnDispute" runat="server" Text="Dispute" ToolTip="Dispute"  OnCommand="btnDispute_Click" CommandArgument='<%#Eval("[Invoice_ID]") + "," + Eval("[Dispute_Flag]")%>' />
                                                          <%--<asp:ImageButton ID="ImageButton1" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindInvoiceApprovedGrid" />
                            <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                        </td>
                    </tr>
                </table>
            </div>
           <div id="divApprovedPayment" visible="false" runat="server" style="border: 1px solid #cccccc;
                font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
                <hr />
                <table style="width: 100%;">
                    <tr>
                     <td align="left" style="font-size: medium; color: red;">
                          Approved Payment Invoice</td>
                        <td align="right" style="font-size: medium; color: red;">
                           <asp:Button ID="btnUnApprove" Visible="false" runat="server" Text="Un-Approve Selected Invoice"
                                Width="180px" OnClick="btnUnApprove_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                          
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <asp:GridView ID="gvApprovedPayment" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                                Width="100%" GridLines="both" AllowSorting="true" OnRowDataBound="gvApprovedPayment_RowDataBound"
                                OnSorting="gvApprovedPayment_Sorting">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <HeaderTemplate>
                                            No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("ROWNUM")%>'></asp:Label>
                                            <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                            <asp:Label ID="lblSupplierType" runat="server" Text='<%#Eval("SType")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSupplier_Name" runat="server" CommandName="Sort" CommandArgument="Supplier_Name">Supplier Name</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbl_SupplierName" runat="server" OnClientClick='<%#"OpenScreen2(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("Supplier_Name") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Code" runat="server" CommandName="Sort" CommandArgument="PO_Code">Code</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblPOCode" runat="server" OnClientClick='<%#"OpenScreen1(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                                Text='<%#Eval("PO_Code") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPO_Amount" runat="server" CommandName="Sort" CommandArgument="PO_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("PO_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("CURRENCY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice_Count" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rec Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceived_Date" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblInvoice_Amount" runat="server" CommandName="Sort" CommandArgument="Invoice_Amount">Amount</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Payment_Approved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_Date" runat="server" Text='<%#Eval("Payment_Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                            Confirmed Delivered
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredAmt" runat="server" Text='<%#Eval("Delivery_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Due Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPayment_Due_Date" runat="server" CommandName="Sort" CommandArgument="Payment_Due_Date"
                                                ForeColor="Black">Payment Due Date</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentDate" runat="server" ForeColor="Red" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            <asp:Label ID="lblUrgentFlag" runat="server" Text="|"></asp:Label>
                                            <asp:Label ID="lblUrgent" ForeColor="Red" runat="server" Text='<%#Eval("Urgent_Flag")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmed Delivered">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInvoice" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Dispute">
                                        <HeaderTemplate>
                                          Dispute
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispute" runat="server" Text=""></asp:Label>
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
                                            <table>
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgViewRemarks" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="View Remarks" OnClientClick='<%#"OpenRemarksScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                          <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                         <asp:Button ID="btnPaymentDispute" runat="server" Text="Dispute" ToolTip="Dispute"  OnCommand="btnPaymentDispute_Click" CommandArgument='<%#Eval("[Invoice_ID]") + "," + Eval("[Dispute_Flag]")%>' />
                                                          <%--<asp:ImageButton ID="ImageButton1" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager2" runat="server" PageSize="30" OnBindDataItem="BindPaymentApprovedGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </td>
                    </tr>
                </table>
            </div>
             <div id="dvWithhold" visible="false" runat="server" style="border: 1px solid #cccccc;
                font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
                <hr />
                 <table style="width: 100%;">
                    <tr>
                       <td align="left" style="font-size: medium; color: red;">
                         Withhold Invoice</td>
                    
                    </tr>
                    <tr>
                    <td>
                      <asp:GridView ID="gvPOWithhold" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="SUPPLY_ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true" 
                            ShowFooter="false" onrowdatabound="gvPOWithhold_RowDataBound">
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <FooterStyle CssClass="FooterStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                         <SelectedRowStyle BackColor="Yellow" />
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <HeaderTemplate>
                                  Sr No.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSrno" runat="server" Text='<%#Eval("ROWNUM")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clean">
                                <HeaderTemplate>
                                    PO/Order Code
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("ORDER_CODE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clean">
                                <HeaderTemplate>
                                    Supplier Name
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier_Name" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clean">
                                <HeaderTemplate>
                                    PO Value
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLine_Amount" runat="server" Text='<%#Eval("Line_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clean">
                                <HeaderTemplate>
                                    Invoice Value
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_PO_Currency_Amount" runat="server" Text='<%#Eval("Invoice_PO_Currency_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clean">
                                <HeaderTemplate>
                                    Net Amount Withheld
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNet_WithHolding_Amount" runat="server" Text='<%#Eval("Net_WithHolding_Amount","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                           <asp:ImageButton ID="ImgView" runat="server" OnCommand="btnView_Click" Text="View" CommandArgument='<%#Eval("[SUPPLY_ID]") + "," + Eval("[Invoice_Currency]")  %>'
                                                            CommandName="Select" ForeColor="Black"  ToolTip="View"
                                                            ImageUrl="~/Images/asl_view.png" Height="16px"></asp:ImageButton>
                                                    </td>
                                                     <td>
                                                          <asp:Button ID="btnPayAmt" runat="server" Text="Pay Amount" ToolTip="Pay Amount"  OnCommand="btnPayAmt_Click" CommandArgument='<%#Eval("[SUPPLY_ID]") + "," + Eval("[Invoice_Currency]") + "," + Eval("[Net_WithHolding_Amount]")%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                     <uc1:ucCustomPager ID="ucCustomPager3" runat="server" PageSize="10" OnBindDataItem="BindWithholdInvoice" />
                            <asp:HiddenField ID="HiddenField3" runat="server" EnableViewState="False" />   
                    </td>

                    </tr>
                     <tr>
                    <td>
                      <br /><br />
                        <asp:GridView ID="gvInvoiceWithhold" runat="server" 
                            EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="Invoice_ID,Supply_ID" CellPadding="1" 
                            CellSpacing="0" Width="100%"
                                GridLines="both" CssClass="gridmain-css" AllowSorting="true" 
                            onrowdatabound="gvInvoiceWithhold_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="Yellow" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Created By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblCreated_By" runat="server" Text='<%#Eval("Created_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Date">
                                        <HeaderTemplate>
                                            Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Created_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
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
                                            <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Ref">
                                        <HeaderTemplate>
                                            Reference
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
                                            Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Amount" runat="server" Text='<%#Eval("Invoice_Amount","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Invoice Amount">
                                        <HeaderTemplate>
                                           Currency
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Confirmed">
                                        <HeaderTemplate>
                                            Due Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Due_Date" runat="server" Text='<%#Eval("Invoice_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice_Status">
                                        <HeaderTemplate>
                                           Approval
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>&nbsp;
                                            <asp:Label ID="lblApproved_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment_Status">
                                        <HeaderTemplate>
                                          Payment Approval
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblPayment_Approved_By" runat="server" Text='<%#Eval("Payment_Approved_By")%>'></asp:Label>&nbsp;
                                            <asp:Label ID="lblPayment_Approved_Date" runat="server" Text='<%#Eval("Payment_Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Delivery Confirmed">
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
                                    <asp:TemplateField HeaderText="Invoice Approved Date">
                                        <HeaderTemplate>
                                            Payment Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayment_Status" runat="server" Text='<%#Eval("Payment_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="60px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dispute Status">
                                        <HeaderTemplate>
                                            Dispute Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispute_Status" ForeColor="Red" runat="server" Text='<%#Eval("Dispute_Flag")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="40px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Priority Payment">
                                        <HeaderTemplate>
                                           Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Discrepancies_Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="100px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="border-color: transparent">
                                                       <asp:ImageButton ID="ImgViewRemarks" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="View Remarks" OnClientClick='<%#"OpenRemarksScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                          <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                    
                    </td>
                    </tr>
                    
                    </table>
                </div>
                  <div id="divaddAmount" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 30%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td align="right" style="width: 15%">
                                       Amount &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:TextBox ID="txtAmount" runat="server" Width="100px"></asp:TextBox>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                       <asp:Button ID="btnPayAmount" runat="server" Text="Pay Amount" ValidationGroup="vgSubmitWithhold" 
                                                onclick="btnPayAmount_Click" />
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="vgSubmitWithhold" />
                                                    <asp:RegularExpressionValidator ID="RegTaxRate" runat="server" ErrorMessage="Amount is not valid.(0-9.)"
                                                            Display="None" ValidationGroup="vgSubmitWithhold" ControlToValidate="txtAmount" ForeColor="Red"
                                                            ValidationExpression="^[0-9.]+$"> </asp:RegularExpressionValidator>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                            ErrorMessage="Amount is mandatory field." ControlToValidate="txtAmount"
                                                            ValidationGroup="vgSubmitWithhold" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div> 
                <div style="display: none;">
                    <asp:TextBox ID="txtInvoiceCurrency" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtPOCode" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtInvoiceCode" runat="server" Width="1px"></asp:TextBox>
                     <asp:TextBox ID="txtNetAmount" runat="server" Width="1px"></asp:TextBox>
                </div>
        </div>
         </contenttemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
