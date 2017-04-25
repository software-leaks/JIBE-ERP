<%@ Page Title="Invoice Approval List" Language="C#" MasterPageFile="~/Site.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="PO_Log_Invoice_Approval.aspx.cs"
    Inherits="PO_LOG_PO_Log_Invoice_Approval" %>

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
    <%--<script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>--%>
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
       
    </style>
   
    <script language="javascript" type="text/javascript">
        function OpenCompareScreen(SUPPLY_ID, Invoice_ID) {
            var PageName = "InvoiceApproval";
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
   <script type="text/javascript">
      
//       function onSucc_LoadFunction(retval, prm) {
//           try {
//               document.getElementById(prm[0]).innerHTML = retval;
//               dvPruchremarkMain.
//               checkForMyAction(prm[1], retval);
//           }
//           catch (ex)
//            { }
//       }

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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
            <div id="page-title" class="page-title">
                Invoice Approval List
            </div>
           
                <div style="padding-top: 5px; border: 1px padding-bottom: 5px;">
                  <table style=" width:100%;">
                  <tr>
                  <td style="width:30%;" valign="top" align="left">
                  <table >
                  <tr>
                  <td style="width: 5%" align="right">
                                Supplier :
                            </td>
                            <td style="width: 10%" align="left">
                                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" Width="400px">
                                </asp:DropDownList>
                            </td></tr>
                      <tr><td align="right" style="width: 5%">
                                Vessel :</td>
                            <td align="left" style="width: 15%">
                                  <asp:DropDownList ID="ddlVessel" runat="server" CssClass="txtInput" Width="400px">
                                </asp:DropDownList></td></tr></table>
                  </td>
                  <td style="width:70%;" valign="top" align="left">
                  <table style=" width:100%;">
                  <tr>   
                  <td style="width: 10%"  align="right">
                               Invoice Status Type : &nbsp;
                            </td>
                    <td align="left"  style="width: 50%;">
                      
                       <asp:CheckBox ID="chkVerified" runat="server" Text="Verified" />
                                        <asp:CheckBox ID="chkRework" runat="server" Text="Rework" />
                                        <asp:CheckBox ID="chkHold" runat="server" Text="Hold" />
                                        <asp:CheckBox ID="chkDispute" runat="server" Text="Dispute" />
                                        <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" />
                                        <asp:CheckBox ID="chkUrgent" ForeColor="Red" runat="server" Text="Urgent" />
                         </td>
                    
                            <td align="left"><asp:Button ID="btnGet" runat="server" OnClick="btnGet_Click" Text="Search" Width="130px" />
                    </td>
                    </tr>
                    </table>
                    </td>
                  </tr>
                  </table>
                    <table width="100%" cellpadding="2" cellspacing="2">
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
                 <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGet" EventName="Click" />
                            </Triggers>
                          <contenttemplate>
                <div id="divPendingInvoice"  runat="server" style="margin-left: auto;
                    margin-right: auto; text-align: center;">
                    <table>
                        <tr>
                            <td align="left" style="width: 8%; font-size: medium; color: red;">
                                Pending Approval Invoice
                            </td>
                            <td align="right" style="width: 8%; font-size: medium; color: red;">
                             <%--  <asp:Button ID="btnApprove" runat="server" Visible="false" Text="Approve Invoice" Width="150px" 
                                    onclick="btnApprove_Click" />--%>
                            </td>
                        </tr>
                        
                    </table>
                    <asp:GridView ID="gvPendinginvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both"  AllowSorting="true" OnRowDataBound="gvPendinginvoice_RowDataBound"
                        OnSorting="gvPendinginvoice_Sorting">
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" BackColor="Yellow" />
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <HeaderTemplate>
                                    No.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("ROWNUM")%>'></asp:Label>
                                    <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                    <asp:Label ID="lblSupplierType" runat="server" BackColor="Yellow"  Width="20px" Height="20px" Text='<%#Eval("SType")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Name">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblSupplier_Name" runat="server" CommandName="Sort" CommandArgument="Supplier_Name" >Supplier Name</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbl_SupplierName" runat="server" OnClientClick='<%#"OpenScreen2(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                        Text='<%#Eval("Supplier_Name") %>' ></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Code">
                             <HeaderTemplate>
                                    <asp:LinkButton ID="lblPO_Code" runat="server" CommandName="Sort" CommandArgument="PO_Code">Code</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblPOCode" runat="server" OnClientClick='<%#"OpenScreen1(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                        Text='<%#Eval("PO_Code") %>' ></asp:LinkButton>
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
                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="30px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("CURRENCY")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Count">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Count" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reference">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Rec. Date">
                                <ItemTemplate>
                                   <asp:Label ID="lblReceived_Date" runat="server" Text='<%#Eval("Received_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inv. Date">
                                <ItemTemplate>
                                   <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
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
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css">
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
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="25px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="By">
                                <ItemTemplate>
                                    <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Verified_By")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblVerified_Date" runat="server" Text='<%#Eval("Verified_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="By">
                                <ItemTemplate>
                                    <asp:Label ID="lblApproved_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblApproved_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
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
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
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
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="For Approval By">
                                <HeaderTemplate>
                                    For Approval By
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <table width="100%">
                                 
                                <tr>
                                 <td style="border-color: transparent">
                                     <asp:Label ID="lblApprover" ForeColor="Red" runat="server" Text='<%#Eval("Inv_Approver")%>'></asp:Label>
                                    
                                     </td></tr>
                                     <tr>
                                 <td style="border-color: transparent"> <asp:Label ID="lblApproveMsg"  runat="server" Text=""></asp:Label></td></tr></table>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="180px" CssClass="PMSGridItemStyle-css">
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
                                    <asp:Button ID="btnUnApprove" runat="server" Text="Un-Approve" Visible="false"  OnCommand="btnUnApprove_Click" OnClientClick="return confirm('Are you sure want to Un-Approve?')" CommandArgument='<%#Eval("[Invoice_ID]")%>' />&nbsp;
                                     <asp:Button ID="btnAprove" runat="server" Text="Approve" Visible="false"  OnCommand="btnApprove_Click" OnClientClick="return confirm('Are you sure want to Approve?')" CommandArgument='<%#Eval("[Invoice_ID]")%>' />&nbsp;
                                     
                                 </td>
                                 <td style="border-color: transparent">
                                  <asp:ImageButton ID="ImgViewRemarks" runat="server" ForeColor="Black"  Width="18px" Height="16px"
                                                                    ToolTip="View Remarks" OnClientClick='<%#"OpenRemarksScreen(&#39;" + Eval("[Invoice_ID]") +"&#39;);return false;"%>'
                                                                    onmouseover='<%#"GetRemarkAll(" + Eval("Invoice_ID").ToString() +",null,event,this);" %>'
                                                                         onmouseout="CloseRemarkToolTip();"
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>&nbsp;
                                 </td>
                                <td>
                                <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black" Width="18px" Height="16px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>&nbsp;
                                    
                                    </td>
                                    <td>
                                     <asp:ImageButton ID="btnOnHold" runat="server" Width="18px" Height="16px" Text="Hold" ToolTip="Put on Hold" OnClientClick="return confirm('Are you sure want to Put on Hold?')"
                                                             ImageUrl="~/Purchase/Image/OnHold.png" OnCommand="btnOnHold_Click"
                                                              CommandName="OnHold" 
                                                            CommandArgument='<%#Eval("[Invoice_ID]")%>' />
                                    <asp:ImageButton ID="btnhold" runat="server" Width="18px" Visible="false" Height="16px" Text="Hold" ToolTip="Remove on Hold" OnClientClick="return confirm('Are you sure want to remove from on Hold?')"
                                                             ImageUrl="~/Purchase/Image/onhold2.gif" OnCommand="btnHold_Click"
                                                              CommandName="OnHold" 
                                                            CommandArgument='<%#Eval("[Invoice_ID]")%>' />
                                    </td>
                                    </tr></table>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                    <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                </div>
               
                </ContentTemplate>
        </asp:UpdatePanel>
         <div id="dvPurchaseRemark" style="border: 1px solid gray; z-index: 501; color: Black;
            display: none; position: fixed; left: 400px; top: 100px" class="Tooltip-css">
        </div>
        <%--<div id="dvPruchremarkMain" style="display: none; position: fixed; left: 30%; top: 10%;
            border: 1px solid gray; padding: 10px" class="popup-css">
            <table>
                <tr>
                    <td>
                       <asp:Label ID="lblPologRemarks" Title="Remarks" runat="server"
                            Width="100%">
                        <div id="dvShowPurchaserRemark" style="position: relative">
                        </div>
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                          
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <input id="btnSaveRemark" onclick="SavePurcReamrk();" type="button" style="height: 30px;
                                        width: 80px" value="Save" />&nbsp;&nbsp
                                    <input id="btnCancelRemark" onclick="CloseRemarkAll();" type="button" value="Close"
                                        style="height: 30px; width: 80px" value="Save" />
                                    <input id="hdfUserID" type="hidden" />
                                </td>
                            </tr>
                        </table>
                        <input id="Hidden1" type="hidden" />
                    </td>
                </tr>
            </table>
        </div>
       --%>
         <%-- <asp:HiddenField ID="hdftaskid" runat="server" />
        <div id="dvInsRemark" style="width: 400px; display: none; top: 350px; left: 700px;
            z-index: 2000; position: fixed; border: 1px solid #cccccc; background-color: White;
            border-radius: 5px 5px 5px 5px;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="background-color: #cccccc; padding: 5px 5px 5px 5px; color: #0B0B61; width: 95%">
                        Enter Remarks
                    </td>
                    <td style="background-color: #cccccc;">
                        <asp:Image ID="imgpopupclose" ImageUrl="~/Images/clock-icon.png" onclick="javascript:document.getElementById('dvInsRemark').style.display = 'none';"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtNewRemark" class="txtInput" cols="40" style="width: 99%; height: 150px"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; background-color: #cccccc" colspan="2">
                        <input type="button" id="Button1" value="Save" onclick="ASync_Ins_Remark(event,this)" />
                        &nbsp;
                        <asp:HiddenField ID="hdf_User_ID" runat="server" />
                    </td>
                </tr>
            </table>
        </div>--%>
               <%-- <div id="divApprovedInvoice" visible="false" runat="server" style="overflow-y: scroll;">
                    <table>
                        <tr>
                            <td align="left" style="width: 8%; font-size: medium; color: red;">
                               Approved Invoice
                            </td>
                            <td align="right" style="width: 8%; font-size: medium; color: red;">
                               <asp:Button ID="btnUnApprove" runat="server"  Text="Un-Approve Invoice" Width="150px" />
                            </td>
                        </tr>
                        
                    </table>
                    <asp:GridView ID="gvApprovedInvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" DataKeyNames="Invoice_ID" CellPadding="1" CellSpacing="0"
                        Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true" OnRowDataBound="gvApprovedInvoice_RowDataBound"
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
                                    <asp:LinkButton ID="lblSupplier_Name" runat="server" CommandName="Sort" CommandArgument="Supplier_Name" >Supplier Name</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbl_SupplierName" runat="server" OnClientClick='<%#"OpenScreen2(&#39;" + Eval("[Supply_ID]") +"&#39;);return false;"%>'
                                        Text='<%#Eval("Supplier_Name") %>' ></asp:LinkButton>
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
                                        Text='<%#Eval("PO_Code") %>' ></asp:LinkButton>
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
                            <asp:TemplateField HeaderText="Reference">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
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
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
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
                                    <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                    
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
                                    <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Verified_By")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblVerified_Date" runat="server" Text='<%#Eval("Verified_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
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
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Confirmed Delivered">
                                <HeaderTemplate>
                                  <asp:CheckBox ID="chkAllInvoice" Text="Select All" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkInvoice" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
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
                                                                    ImageUrl="~/Images/remark_new.gif"></asp:ImageButton>
                                 </td>
                                <td>
                                <asp:ImageButton ID="ImgCompare" runat="server" ForeColor="Black"  Width="18px"
                                                                    ToolTip="Compare PO and Invoice" OnClientClick='<%#"OpenCompareScreen((&#39;" + Eval("[Supply_ID]") +"&#39;),(&#39;" + Eval("[Invoice_ID]") +"&#39;));return false;"%>'
                                                                    ImageUrl="~/Images/compare.gif"></asp:ImageButton>
                                    
                                    </td>
                                    </tr></table>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindApprovedGrid" />
                    <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                </div>--%>
                <%-- <asp:TemplateField HeaderText="View Remarks">
                                <HeaderTemplate>
                                    View Remarks
                                     <span id="lblActionDisplayText" style="height: 15px; width: 200px; color: Red"></span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                <table>
                                <tr><td><asp:Label ID="lbl2" runat="server" Text='<%#Eval("Remarks")%>' ></asp:Label></td>
                                </tr>
                                </table>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>  
                             <asp:TemplateField HeaderText="Confirmed Delivered">
                                <HeaderTemplate>
                                  Select
                                </HeaderTemplate>
                                <ItemTemplate>
                                   
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif"  onclick='<%# "OpenRemarksScreen("+Eval("Invoice_ID").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                    onmouseover='<%#"GetRemarkToolTip("+Eval("Invoice_ID").ToString() +",null,event);" %>'
                                    onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />--%>
        </div> </contenttemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
