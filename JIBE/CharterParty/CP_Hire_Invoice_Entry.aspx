<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" CodeFile="CP_Hire_Invoice_Entry.aspx.cs"
    Inherits="CP_Hire_Invoice_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
       <script language="javascript" type="text/javascript">
           function refreshAndClose() {
           window.parent.ReloadParent_ByButtonID();
           window.close();
       }

       function ValidateAmount() {
           var value = document.getElementById("txtInvAmount").value;
           if (value == null || value == 0) {

               alert("Amount cannot be approved!");
               return false;
           }
           else
               return true;
       }
      </script>
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 98%;
    height: 100%;">
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                <td width="15%" align="right">
                <asp:Literal ID="ltInvStatus" Text="Invoice Status :" runat="server"></asp:Literal>
                <asp:Label ID="lblInvStatus" Visible="false" runat="server"></asp:Label>
                </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                 &nbsp;
                </td>
                <td width="15%" align="left">
                <asp:DropDownList ID="ddlStatus" Width="150px"  runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvStatus" runat="server" Display="None" InitialValue="0"
                ErrorMessage="Status is mandatory." ControlToValidate="ddlStatus" ValidationGroup="vgSubmit"
                ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td width="10%" align="right"> 
                <asp:Literal ID="ltReference" Text="Reference :" runat="server"></asp:Literal>
                </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                *
                </td>
                <td width="15%" align="left">
                <asp:TextBox ID="txtReference" runat ="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqRemarks" runat="server" Display="None"
                ErrorMessage="Reference is mandatory." ControlToValidate="txtReference" ValidationGroup="vgSubmit"
                ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td width="10%" align="right"> 
                <asp:Literal ID="ltDate" Text="Date :" runat="server"></asp:Literal>
                </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                *
                </td>
                <td width="10%" align="left">
                <asp:TextBox ID="txtDate" runat ="server"></asp:TextBox>
                <cc1:CalendarExtender ID="ceDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                </td>
                </tr>

                <tr>
                
                <td width="15%" align="right">
                <asp:Literal ID="ltBillingPeriod" Text="Billing Period From :" runat="server"></asp:Literal>
                </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                *
                </td>
                <td width="10%">
                  <asp:TextBox ID="dtBillingStart" runat="server" Width="100px" ></asp:TextBox> 
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd-MMM-yyyy" 
                    TargetControlID="dtBillingStart">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvBillingStart" ControlToValidate ="dtBillingStart" Display="None" runat="server" ErrorMessage="Billing start is mandatory." ValidationGroup="vgSubmit"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td width="15%" align="left">
                     <asp:DropDownList ID="ddlBillingStartHours" runat="server" Width="40px"></asp:DropDownList>
                     :
                     <asp:DropDownList ID="ddlBillingStartMins" runat="server"  Width="50px"></asp:DropDownList>
                      <asp:Literal ID="ltBillingEnd" Text="Period To : " runat="server"></asp:Literal>
                     </td>
                    <td align="right" class="style1" style="color: #FF0000; width:1% ">*</td>
                     <td colspan="2" align="left">
                    <asp:TextBox ID="dtBillingEnd" runat="server" Width="80px"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" 
                    TargetControlID="dtBillingEnd"></cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfvBillingTo" ControlToValidate ="dtBillingStart" runat="server" Display="None" ErrorMessage="Billing to is mandatory." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlBillingEndHours" runat="server" Width="40px"></asp:DropDownList>
                   :
                     <asp:DropDownList ID="ddlBillingEndMins" runat="server"  Width="50px"></asp:DropDownList>
                    </td>
                    <td colspan="2">
                    <asp:Literal ID="ltCoverageOfdays" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                <td align="right">
                <asp:Literal ID="ltInvoiceAmount" Text="Invoice Amount : " runat="server"></asp:Literal>
                </td>
                 <td align="right" class="style1" style="color: #FF0000; width:1% "></td>
                    <td width="15%" align="left">
                <asp:TextBox ID="txtInvAmount" Width="100px" MaxLength="16" Enabled="false" runat="server"></asp:TextBox>
              
<%--                <asp:RequiredFieldValidator ID="rfvInvAmount" runat="server" ErrorMessage="Invoice amount is mandatory." Display="None" ControlToValidate="txtInvAmount" ValidationGroup="vgSubmit"
                ForeColor="Red"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="rgvPricePerUnit" runat="server" ErrorMessage="Invoice amount is not valid."
                Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtInvAmount"
                ForeColor="Red" ValidationExpression="^[0-9.-]+$"></asp:RegularExpressionValidator>--%>
                </td>
                <td width="10%" align="right"> 
                <asp:Literal ID="ltDueDate" Text="Due Date :" runat="server"></asp:Literal>
                </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                *
                </td>
                <td width="10%" align="left">
                <asp:TextBox ID="txtDueDate" runat ="server" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDueDate"></cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                ErrorMessage="Due date is mandatory." ControlToValidate="txtDueDate" ValidationGroup="vgSubmit"
                ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td width="15%" align="right"> 
                <asp:Literal ID="Literal2" Text="Address Commision :" runat="server"></asp:Literal>
                </td>
                 <td align="right" class="style1" style="color: #FF0000; width:1% "></td>
                <td width="15%" align="left">
                <asp:TextBox ID="txtAddressCommision" runat="server" Width="50px" Enabled="false" MaxLength="6"></asp:TextBox>&nbsp;%
                      <asp:RegularExpressionValidator ID="reAddressCommision" runat="server" ErrorMessage="Address Commision is not valid."
                Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtAddressCommision"
                ForeColor="Red" ValidationExpression="^[0-9.-]+$"></asp:RegularExpressionValidator>
                </td>
                </tr>
                <tr>
                <td width="10%" align="right">
                <asp:Literal ID="ltReceivingbank" Text="Receiving Bank :" runat="server"></asp:Literal>
                 </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                *
                </td>
                <td align="left" colspan="2">
                <asp:DropDownList ID="ddlBank" Width="250px" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvBank" runat="server" Display="None" InitialValue="0"
                ErrorMessage="Receiving bank is mandatory." ControlToValidate="ddlBank" ValidationGroup="vgSubmit"
                ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>
                <td align="right">
                <asp:Literal ID="ltReceivedAmt" Text="Received Amount : " runat="server"></asp:Literal>
                </td>
                 <td align="right" class="style1" style="color: #FF0000; width:1% "></td>
                    <td width="15%" align="left">
                <asp:TextBox ID="txtReceivedamount" Enabled="false" Width="150px" MaxLength="16" runat="server"></asp:TextBox>
              
                  <asp:RegularExpressionValidator ID="rgReceivedamt" runat="server" ErrorMessage="Received  amount is not valid."
                Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtReceivedamount"
                ForeColor="Red" ValidationExpression="^[0-9.-]+$"></asp:RegularExpressionValidator>
                </td>
                <td width="10%" align="right"> 
                <asp:Literal ID="ltReceivedDate" Text="Received Date :" runat="server"></asp:Literal>
                </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                </td>
                <td width="15%" align="left">
                <asp:TextBox ID="txtRcvDate" runat ="server" Enabled="false" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtRcvDate"></cc1:CalendarExtender>
                </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Literal ID="ltOutstandingText" runat="server" Text="Outstanding Amount : " ></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:1% "></td>
                <td>
                    <asp:Literal ID="ltOutstandingamt" runat = "server"></asp:Literal>
                </td>
                  <td align="right">
                    <asp:Literal ID="ltJournalId" runat="server" Text="Journal ID: " ></asp:Literal>
                </td>
                    <td colspan="2">
                    <asp:Literal ID="Literal3" runat = "server"></asp:Literal>
                </td>
                </tr>
                <tr>
                <td style="font-weight:bold" align="right">
                 <asp:Literal ID="ltInvRemarks" runat="server" Text="Invoice Remarks : " ></asp:Literal>
                </td>
                <td colspan="3" align="left">
                <asp:TextBox ID="txtInvRemarks" TextMode="MultiLine" Width="250px" runat="server"></asp:TextBox>
                </td>
                </tr>

                <tr>
 
                <td colspan="9" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                </td>
                </tr>
                <tr>
                
                <td colspan="9" align="center">
                <asp:Button ID="btnSave" Text="Save" runat="server"  ValidationGroup="vgSubmit" 
                        onclick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnSaveClose" Text="Save & Close" runat="server"  
                        ValidationGroup="vgSubmit" onclick="btnSaveClose_Click" 
                         />&nbsp;

                 <asp:ValidationSummary ID="vsUpdateinvoice" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                  <asp:Button ID="btnApprove" Text="Approve" OnClientClick="ValidateAmount();" 
                        runat="server" onclick="btnApprove_Click" />&nbsp;
                  <asp:Button ID="btnUnapprove" Text="UnApprove" runat="server" Visible="false" 
                        onclick="btnUnapprove_Click" />&nbsp;
                </td>
                </tr>
                    

</table>
</center>
</div>
</form>
</body>
</html>