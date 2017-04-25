<%@ Page Title="Charter Party Detail" Language="C#" MasterPageFile="~/Site_NoMenu.master" AutoEventWireup="true"
    CodeFile="CP_Charter_Party_Details.aspx.cs" Inherits="CP_Charter_Party_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
           body, html
        {
            overflow-x: hidden;
        }
          
         .page

        {
            width:100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

  .page
        {
            width: 100%;
           
        }
        td.blog_content div
{
   
   
    width: 100%;
    text-align: left;
    padding: 2px;
}

   </style>
     <script language="javascript" type="text/javascript">

         function validation() {
             if (document.getElementById("ctl00_MainContent_ddlPort") != null) 
             {
                 var textValue = document.getElementById('ctl00_MainContent_ddlPort_TextBox').value;
                 var hiddenvalue = document.getElementById('ctl00_MainContent_ddlPort_HiddenField').value;

                 if ((hiddenvalue == "0" || textValue == "")) 
                 {
                     alert("Please Select port.");
                     document.getElementById("ctl00_MainContent_ddlPort").focus();
                     return false;
                 }
             }

                return true;
            }

            function ValidateBroker() {

                {
                    var ddlBroker = document.getElementById("ctl00_MainContent_ddlBroker").value;
                    var ddlBroker2 = document.getElementById("ctl00_MainContent_ddlBroker2").value;
                    var ddlBroker3 = document.getElementById("ctl00_MainContent_ddlBroker3").value;

                    if (ddlBroker != '0' && ddlBroker2 != '0') {

                        if (ddlBroker == ddlBroker2)
                        {
                            alert("Broker 1 and Broker 2 cannot be same");
                            return false;
              
                            }
                    }
                    else if (ddlBroker != '0' && ddlBroker3 != '0') {
                    if (ddlBroker == ddlBroker3) {
                        alert("Broker 1 and Broker 3 cannot be same");
                        return false;
                    }
                    }
                    else if (ddlBroker2 != '0' && ddlBroker3 != '0') {
                    if (ddlBroker2 == ddlBroker3) {
                        alert("Broker 2 and Broker 3 cannot be same");
                        return false;
                    }
                    }
 
                        
                }

                return true;
            }


             function NumberOnly() {
                 var AsciiValue = event.keyCode
                 if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127)) {
                     event.returnValue = true;
                 }
                 else {
                     event.returnValue = false;
                     alert('Please Enter only numeric Value(0-9)');
                 }
             }
             function OpenAttachments()
             {
             var url = 'CP_Attachments.aspx';
                 OpenPopupWindow('Attachments', 'Attachments', url, 'popup', 800, 1200, null, null, false, false, true, null);
             }
            function OpenInvPrep() {
                 var url = 'CP_Hire_Invoice_Prep.aspx';
                // OpenPopupWindow('InvicePrep', 'Invoice Preparation', url, 'popup', 800, 1200, null, null, false, false, true, null);

                 window.open(url, "_blank");
             }
             function OpenBillingItem() {

                 window.open('CP_Billing_Item_Entry.aspx', "_blank");
             }

             function OpenInvcomeMatch() {
                 var url = 'CP_Income_Matching.aspx';
                 OpenPopupWindow('IncomeMatching', 'Income Matching', url, 'popup', 800, 1200, null, null, false, false, true, null);
             }
            
             function showDivAddBunker() {
                 showModal('dvAddBunker', true, dvAddBunker_onClose);
             }
             function dvAddBunker_onClose() {

                 hideModal('dvAddBunker');
             }
    </script>
         <style type="text/css">
           body, html
        {
            overflow-x: hidden;
        }
         
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
             .style1
             {
                 width: 18%;
             }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
         <asp:UpdatePanel ID="panel1" runat="server"><ContentTemplate>
        <center>

         <div align="left" title="Delivery Ports" style="border: 1px solid gray; margin-top: 1px;">
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td colspan="9" align="center">
                        <div id="page-title" class="page-title">
                           Charter Party Details
                        </div>
                    </td>
                </tr>
                <tr>
                <td style="width:14% " align="right" >
                <asp:Literal ID="ltCPRef" runat="server" Text="CP ID:"></asp:Literal>
                </td>
                  <td align="right"  style="color: #FF0000; width:1% ">
                      &nbsp;
                    </td>
                    <td align="left"  style="color: Blue;font-weight:bold; width:12% " >
                    <asp:Label ID="lblCRNo" runat="server" Visible="true"></asp:Label>
                    </td>
                      <td style="text-align: right; width:14%">
                        <asp:Literal ID="Literal3" runat="server" Text="Opening Port :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        *
                    </td>
                    <td  style="text-align: left; width:15%">
                    <asp:DropDownList ID="ddlPort" runat="server"   ></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Port is mandatory." ControlToValidate="ddlPort" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>


                         <td align="right" class="style1">
                         <asp:Literal ID = "ltCurrentDate" runat="server" Text="Opening Date/Time :" ></asp:Literal>
                        </td>
                        <td align="right"  style="color: #FF0000; width:1% ">*</td>
                          <td align="left" style="width: 15%">
                            <asp:TextBox ID="dtOP" runat="server" Width="80px" CssClass="txtInput" 
                                MaxLength="100"></asp:TextBox>
                            <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" 
                                TargetControlID="dtOP">
                            </cc2:CalendarExtender>
                             &nbsp;
                    <asp:DropDownList ID="ddlOpeningHours" runat="server" Width="40px"></asp:DropDownList>
                   :
                     <asp:DropDownList ID="ddlOpeningMins" runat="server"  Width="50px"></asp:DropDownList>
                        </td>
                </tr>
                    <tr>

                      <td align="right"style=" width:15% ">
                            <asp:Literal ID="ltCPdate" runat="server" Text=" CP Date :"></asp:Literal>
                        </td>
                      <td align="right" class="style1" style="color: #FF0000; width:1% ">
                            *
                        </td>
                        <td style="width: 12%" align="left">
                            <asp:TextBox ID="dtCP" runat="server" CssClass="txtInput" 
                                MaxLength="100"></asp:TextBox>
                            <cc2:CalendarExtender ID="cedtArrivalTo" runat="server" Format="dd-MMM-yyyy" 
                                TargetControlID="dtCP">
                            </cc2:CalendarExtender>
                        </td>
                    <td style="text-align: right; width:14%">
                        <asp:Literal ID="ltStatus" runat="server" Text="CP Status :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        *
                    </td>
                    <td  style="text-align: left; width:15%">
                    <asp:DropDownList ID="ddlStatus" runat="server" ></asp:DropDownList>
<%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Status is mandatory." ControlToValidate="ddlStatus" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                    </td>
                        <td style="text-align: right; " class="style1">
                        <asp:Literal ID="ltCPType" runat="server" Text="CP Type :"></asp:Literal>
                    </td>
                    <td align="right" class="style1" style="color: #FF0000; width:1% ">
                        *
                    </td>
                    <td  style="text-align: left; width:18%">
                        <asp:DropDownList ID="ddlCPType" runat="server" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="CP Type is mandatory." ControlToValidate="ddlCPType" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width:14%">
                        <asp:Literal ID="ltVessel" runat="server" Text="Vessel :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        *
                    </td>
                    <td  style="text-align: left; width:12%">
                    <asp:DropDownList ID="ddlVessel" runat="server" Width="150px" AutoPostBack="true"
                            onselectedindexchanged="ddlVessel_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Vessel is mandatory." ControlToValidate="ddlVessel" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                        <td style="text-align: right; width:14%">
                        <asp:Literal ID="Literal2" runat="server" Text="Owner :"></asp:Literal>
                    </td>
                    <td align="right" class="style1" style="color: #FF0000; width:1% ">
                        *
                    </td>
                    <td  style="text-align: left; width:15%">
                        <asp:DropDownList ID="ddlOwner" runat="server" Width="98%" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Owner is mandatory." ControlToValidate="ddlOwner" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>

                    <td style="text-align: right; " class="style1">
                        <asp:Literal ID="Literal1" runat="server" Text="Owner Bank:"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        *
                    </td>
                    <td  style="text-align: left; width:18%">
                        <asp:DropDownList ID="ddlOwnerBank" runat="server" Width="98%" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Owner bank is mandatory." ControlToValidate="ddlOwnerBank" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>

                </tr>
  
                  <tr>
                    <td style="text-align: right; width:14%">
                        <asp:Literal ID="ltCharter" runat="server" Text="Charterer :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        *
                    </td>
                    <td  style="text-align: left; width:12%">
                    <asp:DropDownList ID="ddlCharterer" runat="server" Width="98%"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Charter is mandatory." ControlToValidate="ddlCharterer" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                        <td style="text-align: right; width:14%">
                     
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">

                    </td>
                    <td align="left"  >

     
                    </td>

                    <td style="text-align: left; " class="style1" >
                      &nbsp;
                    
                         

                      </td>
                      <td align="right"  style="color: #FF0000; width:1% ">
                     &nbsp;
                    </td>
                    <td  style="text-align: left; width:10%">
                

                    </td>
   
                </tr>

                
                <tr>
                <td align="right" width="10%">
                   <asp:Literal ID="Literal5" runat="server" Text="Broker :"></asp:Literal>
                </td>
               <td align="right"  style="color: #FF0000; width:1% ">
                       &nbsp;
               </td>
                <td align="left" width="20%" >
                 <asp:DropDownList ID="ddlBroker" runat="server" Width="98%"></asp:DropDownList>
<%--                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Broker is mandatory." ControlToValidate="ddlBroker" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
                <td align="right" width="10%">
              <asp:Literal ID="ltBrokCommission" runat="server" Text="Commission :"></asp:Literal>
                
                </td>
                 <td align="right" style="color: #FF0000; width:1% ">
                    </td>
                 <td align="left" width="9%" >
                    <asp:TextBox ID="txtBrokCommision" runat="server" Width="50px" MaxLength="5"></asp:TextBox>&nbsp;%&nbsp;
                    <asp:RegularExpressionValidator ID="reBrokComm" runat="server" ErrorMessage="Brokerage Commission is not valid."
                        Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtBrokCommision"
                         ForeColor="Red" ValidationExpression="^[0-9.-]+$"></asp:RegularExpressionValidator>
                 </td>
                 <td align="right" class="style1">
                   <asp:Literal ID="ltPaymentMode1" runat="server" Text="Payment Mode :"></asp:Literal>  
                 </td>
                  <td align="right" style="color: #FF0000; width:1% ">
                    </td>
               <td align="left" width="20%" >
                    <asp:DropDownList ID="ddlBrokPayment" runat="server" >
                <asp:ListItem value =  "Not applicable" Text="Not applicable"></asp:ListItem> 
                <asp:ListItem value =  "Paid by PO" Text="Paid by PO"></asp:ListItem> 
                <asp:ListItem value =  "Deduct from source" Text="Deduct from source"></asp:ListItem> 
                </asp:DropDownList>
                </td>
                </tr>
                <tr>
                    <td align="right" width="10%">
                        <asp:Literal ID="Literal8" runat="server" Text="Broker2 :"></asp:Literal>
                    </td>
                    <td align="right" style="color: #FF0000; width:1% ">
                    </td>
                    <td align="left" width="20%">
                        <asp:DropDownList ID="ddlBroker2" runat="server" Width="98%">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Literal ID="Literal9" runat="server" Text="Commission2 :"></asp:Literal>
                    </td>
                     <td align="right" style="color: #FF0000; width:1% ">
                    </td>
                    <td align="left" width="9%">
                        <asp:TextBox ID="txtBrokCommision2" runat="server" MaxLength="5" Text="0.00" Width="50px"></asp:TextBox>
                        &nbsp;%&nbsp;
                        <asp:RegularExpressionValidator ID="reComm2" runat="server" 
                            ControlToValidate="txtBrokCommision2" Display="None" 
                            ErrorMessage="Commission 2 Not valid." ForeColor="Red" 
                            ValidationExpression="^[0-9.-]+$" ValidationGroup="vgSubmit"></asp:RegularExpressionValidator>
                    </td>
                    <td align="right" class="style1">
                        <asp:Literal ID="Literal10" runat="server" Text="Payment Mode :"></asp:Literal>
                    </td>
                     <td align="right" style="color: #FF0000; width:1% ">
                    </td>
                    <td align="left" width="20%">
                        <asp:DropDownList ID="ddlBrokPayment2" runat="server">
                            <asp:ListItem Text="Not applicable" value="Not applicable"></asp:ListItem>
                            <asp:ListItem Text="Paid by PO" value="Paid by PO"></asp:ListItem>
                            <asp:ListItem Text="Deduct from source" value="Deduct from source"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <tr>
                        <td align="right" width="10%">
                            <asp:Literal ID="Literal12" runat="server" Text="Broker3 :"></asp:Literal>
                        </td>
                        <td align="right" style="color: #FF0000; width:1% ">
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlBroker3" runat="server" Width="98%">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="10%">
                            <asp:Literal ID="Literal13" runat="server" Text="Commission3 :"></asp:Literal>
                        </td>
                         <td align="right" style="color: #FF0000; width:1% ">
                    </td>
                        <td align="left" width="9%">
                            <asp:TextBox ID="txtBrokCommision3" runat="server" MaxLength="5" Text="0.00" Width="50px"></asp:TextBox>
                            &nbsp;%&nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtBrokCommision3" Display="None" 
                                ErrorMessage="Commission3 not valid." ForeColor="Red" 
                                ValidationExpression="^[0-9.-]+$" ValidationGroup="vgSubmit"></asp:RegularExpressionValidator>
                        </td>
                        <td align="right" class="style1">
                            <asp:Literal ID="Literal14" runat="server" Text="Payment Mode :"></asp:Literal>
                        </td>
           <td align="right" style="color: #FF0000; width:1% ">
                    </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlBrokPayment3" runat="server">
                                <asp:ListItem Text="Not applicable" value="Not applicable"></asp:ListItem>
                                <asp:ListItem Text="Paid by PO" value="Paid by PO"></asp:ListItem>
                                <asp:ListItem Text="Deduct from source" value="Deduct from source"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                
                
                </tr>

                                 <tr>
                    <td style="text-align: right; width:14%; font-weight:bold">
                        <asp:Literal ID="Literal4" runat="server" Text="LayCan(LT) :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        
                    </td>
                    <td>
                      <asp:Literal ID="ltGMT" runat="server" Text="GMT :"></asp:Literal> &nbsp;
                   <asp:TextBox ID="txtGMTTimeZone" MaxLength="5" runat="server" Width="50px" ></asp:TextBox> 
                    <asp:RegularExpressionValidator ID="reTimeZone" runat="server" ValidationExpression="^[-+]?([1-9]\d?(\.\d{1,2})?|0\.(\d?[1-9]|[1-9]\d))$" ValidationGroup = "vgSubmit"
                    ControlToValidate ="txtGMTTimeZone" ErrorMessage="GMT value is not valid." Display="None"></asp:RegularExpressionValidator>
                    </td>
                    <td  align="right" >
                        <asp:TextBox ID="txtLaycan" width ="150px" Visible ="false" runat="server"></asp:TextBox>
                       <asp:Literal ID="ltLaycanStart" runat="server" Text="Lay Can Start :"></asp:Literal>

                    </td>
                    <td>
                   &nbsp;
                    
                    </td>
                    <td colspan="4">
                    
                    <asp:TextBox ID="dtLayCanStart" runat="server" Width="80px" ></asp:TextBox> 
                    <cc2:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd-MMM-yyyy" 
                    TargetControlID="dtLayCanStart">
                    </cc2:CalendarExtender>
                    &nbsp;
                     <asp:DropDownList ID="ddlLayCanStartHours" runat="server" Width="40px"></asp:DropDownList>
                     :
                     <asp:DropDownList ID="ddlLayCanStartMins" runat="server"  Width="50px"></asp:DropDownList>

                    &nbsp; Lay Can End : &nbsp;
                    <asp:TextBox ID="dtLayCanEnd" runat="server" Width="80px"></asp:TextBox>
                     <cc2:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" 
                    TargetControlID="dtLayCanEnd"></cc2:CalendarExtender>
                    &nbsp;
                    <asp:DropDownList ID="ddlLayCanEndHours" runat="server" Width="40px"></asp:DropDownList>
                   :
                     <asp:DropDownList ID="ddlLayCanEndMins" runat="server"  Width="50px"></asp:DropDownList>
                    </td>
                </tr>
                 <tr >
                    <td style="text-align: right; font-weight:bold; width:14%">
                        <asp:Literal ID="Literal6" runat="server" Visible="false"  Text="Hire Terms :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        
                    </td>
                    <td  align="left" colspan="2">
                        <asp:TextBox ID="txtHireTerms" width ="80%" MaxLength ="2000" TextMode="MultiLine" Visible="false" runat="server"></asp:TextBox>
                    </td>
                      <td align="right"  style="color: #FF0000; width:1% ">
                        <td style="text-align: right;font-weight:bold; width:14%" >
                        <asp:Literal ID="ltCommissionTerms" runat="server" Visible="false"  Text="Commission Terms :"></asp:Literal>

                    </td>
                    <td colspan="3">
                     <asp:TextBox ID="txtCommisionTerms" width ="80%" MaxLength ="2000" Visible="false"  TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <div style="border-width:thin;" >
                    <tr>
                    <td  align="right">
                        <asp:Literal ID="ltCurrentHireRate" runat="server" Text="Current Hire Rate :"></asp:Literal>
                        </td>
                        <td></td>
                        <td>
                    <asp:TextBox ID="txtCurrentHireRate" runat="server" Width="150px"></asp:TextBox>
                    &nbsp;USD&nbsp;
                  <asp:RegularExpressionValidator ID="rgCurrentHireRate" runat="server" ErrorMessage="Hire rate is not valid."
                Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtCurrentHireRate"
                ForeColor="Red" ValidationExpression="^[0-9.-]+$">
            </asp:RegularExpressionValidator>
            <td align="right">
                        <asp:Literal ID="ltlType" runat="server" Text=" Hire Type :"></asp:Literal>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                        <asp:DropDownList ID="ddlHireType" runat="server" Width="150px" ></asp:DropDownList>
                    </td>

                    <td style="text-align: right;" class="style1" >
                     <asp:Literal ID="ltAddressCommission" runat="server" Text="Address Commission :"></asp:Literal>
                     </td>
                     <td align="right">
                     
                     </td>
                       <td>
                       <asp:TextBox ID="txtAddressCommision" runat="server" Width="50px" MaxLength="6"></asp:TextBox>&nbsp;%
                      <asp:RegularExpressionValidator ID="reAddressCommission" runat="server" ErrorMessage="Address Commission is not valid."
                Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtAddressCommision"
                ForeColor="Red" ValidationExpression="^[0-9.-]+$"></asp:RegularExpressionValidator>

    
                    </td>

                </tr>


                   <tr>
                    <td align="right">
                     <asp:Literal ID="Literal7" runat="server" Text="Billing Cycle :"></asp:Literal>&nbsp;&nbsp;
                     </td>
                       <td align="right"  style="color: #FF0000; width:1% ">
                        </td>
                     <td>
                    <asp:TextBox ID="txtBillingCycle" MaxLength="200" runat="server"></asp:TextBox>&nbsp;&nbsp;
                   
                    </td>
                     <td align="right">
                        <asp:Literal ID="ltSpreadBy" runat="server" Text="Spread By :"></asp:Literal>&nbsp;&nbsp;
                    
                    </td>
                    <td>&nbsp;</td>
                    <td style="text-align: left;" colspan="3">
                      <asp:TextBox ID="txtSpreadBy" MaxLength="50" runat="server"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="ddlSpreadByInterval" runat="server">
                        <asp:ListItem Value="0" Text="Days"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Date"></asp:ListItem>
                    </asp:DropDownList>
      
                         <asp:RegularExpressionValidator ID="reSpreadBy" runat="server" ErrorMessage="Spread by must be numeric."
                Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtSpreadBy"
                ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                    </td>

                </tr>
              </table>
              </div>
              <div align="left" title="Delivery Ports" style="border: 1px solid gray; margin-top: 1px;">
             <table >
                 <tr>
                    <td style="text-align: right; font-weight:bold; width:14%">
                        <asp:Literal ID="ltDeliveryPort" runat="server" Text="Delivery Port :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        
                    </td>
                    <td  align="left" style="text-align: left; width:14%">
                    <asp:TextBox ID="txtDeliveryPort" runat="server" MaxLength="200" Width="98%" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDeliveryport" runat="server" Display="None" 
                    ErrorMessage="Add delivery port." ControlToValidate="txtDeliveryPort" ValidationGroup="vgSubmitdelivery"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    ON:
                    </td>
                    <td style="text-align: left; font-weight:bold; width:20%">
                    <asp:TextBox ID="dtdelivery" runat="server" Width="80px"></asp:TextBox>
  
                    <cc1:CalendarExtender ID="ceDeliveryPort" runat="server" Format="dd-MMM-yyyy" TargetControlID ="dtdelivery"></cc1:CalendarExtender>
                    &nbsp;
                    <asp:DropDownList ID="ddlDeliveryPortHour" runat="server" Width="40px"></asp:DropDownList>
                    :
                     <asp:DropDownList ID="ddlDeliveryPortMin" runat="server"  Width="50px"></asp:DropDownList>
                    <asp:DropDownList ID="ddlDeliveryLTGMT" runat="server"  Width="50px">
                    <asp:ListItem Text="LT" Value="LT"></asp:ListItem>
                     <asp:ListItem Text="GMT" Value="GMT"></asp:ListItem>
                    </asp:DropDownList>

                    </td>
                    
                    <td style="text-align: right; font-weight:bold; width:14%">
                        <asp:Literal ID="ltRedeliveryPort" runat="server" Text="ReDelivery Port :"></asp:Literal>
                    </td>
                    <td align="right"  style="color: #FF0000; width:1% ">
                        
                    </td>
                    <td  style="text-align: left; width:14%">
                       <asp:TextBox ID="txtRedeliveryPort" runat="server" MaxLength="200"  Width="98%" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvRedelivery" runat="server" Display="None" 
                    ErrorMessage="Add redelivery port." ControlToValidate="txtRedeliveryPort" ValidationGroup="vgSubmitRedelivery"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                     <td>
                    ON:
                    </td>
                    <td style="text-align: left; font-weight:bold; width:20%">
                    <asp:TextBox ID="dtRedelivery" runat="server"  Width="80px"></asp:TextBox>

                    <cc1:CalendarExtender ID="ceRedelivery" runat="server" Format="dd-MMM-yyyy" TargetControlID ="dtRedelivery"></cc1:CalendarExtender>
                    &nbsp;
                    <asp:DropDownList ID="ddlRedeliveryHour" runat="server" Width="40px"></asp:DropDownList>
                    :
                     <asp:DropDownList ID="ddlRedeliveryMin" runat="server"  Width="50px"></asp:DropDownList>
                     
                    <asp:DropDownList ID="ddlReDeliveryLTGMT" runat="server"  Width="50px">
                    <asp:ListItem Text="LT" Value="LT"></asp:ListItem>
                     <asp:ListItem Text="GMT" Value="GMT"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    </td>
                    <td style="text-align: left;font-weight:bold; width:15%" >

                                        <asp:RequiredFieldValidator ID="rqRedelivery" runat="server" Display="None" 
                    ErrorMessage="Redelivery date is mandatory." ControlToValidate="dtRedelivery" ValidationGroup="vgSubmitRedelivery"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>

                </tr>

                  <tr>

                  <td >
                    <asp:Label ID="lblSaveFirst" runat="server" ForeColor="Brown" Text="*Save Port first to add bunker" Visible="true"></asp:Label>
                    </td>
                    <td>
                    &nbsp;
                    </td>
                        <td>
   
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" 
                            ShowSummary="false" ValidationGroup="vgSubmitdelivery" />
                        </td>

                    <td>
                    &nbsp; <asp:ImageButton ID="btnAddDeliveryBunker"     runat="server" Text="Create New" Visible="false"
                       ToolTip="Add delivery bunker"  ImageUrl="~/Images/Add-icon.png" 
                            onclick="btnAddDeliveryBunker_Click" />
                    </td>
                    <td>
                      <asp:RequiredFieldValidator ID="rfvDelivery" runat="server" Display="None" 
                    ErrorMessage="Delevery date is mandatory." ControlToValidate="dtdelivery" ValidationGroup="vgSubmitdelivery"
                    ForeColor="Red"></asp:RequiredFieldValidator>

                                      
                    </td>
                    <td>
                     <asp:Literal ID="ltRedeliveryNotice" runat="server" Text="Redelivery notice :" ></asp:Literal>
                     <asp:TextBox ID="txtRedeliveryDays" runat="server"   BackColor="Red" ForeColor="Yellow" MaxLength="3" Width="50px"></asp:TextBox>&nbsp; Days.
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Days must be numeric."
                      Display="None" ValidationGroup="vgSubmiRedelivery" ControlToValidate="txtRedeliveryDays"
                        ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                  
                    </td>
                    <td  align="center">


                    </td>
                    <td>
                      <asp:ValidationSummary ID="vsRedelivery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmitRedelivery" />
                    </td>
                    <td>
                    
                      <asp:ImageButton ID="btnAddRedeliveryBunker"     runat="server" Text="Create new  redelivery" 
                       ToolTip="Add redelivery bunker"  ImageUrl="~/Images/Add-icon.png" Visible="false"
                            onclick="btnAddRedeliveryBunker_Click" />
                    </td>

                </tr>

                <tr>
                <td colspan="5" valign="top">
                <asp:UpdatePanel ID="UPDeliveryBunker" runat="server">
                <ContentTemplate>
                   <asp:GridView ID="gvDeliveryBunker" runat="server" AutoGenerateColumns="False" 
                                GridLines="Both" Width="98%" DataKeyNames="Delivery_Bunker_ID">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Fuel">
                                           <ItemTemplate>
                                            <asp:Label ID="lblfileName" runat="server" 
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Fuel_Name") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="30%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Amount(MT)">
                                    <ItemTemplate>
                                         <asp:Label ID="lblAmt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Fuel_Amt") %>'
                                                ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price/Unit(USD/MT)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileId" runat="server" 
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Unit_Price") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="30%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate >

                                                  <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                            <asp:ImageButton ID="ibtnUpdateDelivery" style="border: 0; width: 14px; height: 14px" Text="Update"
                                                           ForeColor="Black" OnCommand="onUpdateDelivery_Click"
                                                           CommandArgument='<%#Eval("[Delivery_Bunker_ID]")%>' 
                                                                ImageUrl="../Images/edit.gif" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="lbtnDelete" runat="server"  OnCommand="onDeleteDelivery_Click"
                                                           CommandArgument='<%#Eval("[Delivery_Bunker_ID]")%>' 
                                                                    ImageUrl="~/images/delete.png"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="5%" 
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSavebunker" />
                            
                            </Triggers>
                            </asp:UpdatePanel>
                            </td>
                 <td colspan="5" valign="top">
                 <asp:UpdatePanel ID="UPDRedelivery" runat="server">
                 <ContentTemplate>
                  <asp:GridView ID="gvReDeliveryBunker" runat="server" AutoGenerateColumns="False" 
                                GridLines="Both" Width="98%" DataKeyNames="Delivery_Bunker_ID">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Fuel">
                                           <ItemTemplate>
                                            <asp:Label ID="lblfileName" runat="server" 
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Fuel_Name") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="30%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Amount(MT)">
                                    <ItemTemplate>
                                         <asp:Label ID="lblAmt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Fuel_Amt") %>'
                                                ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price/Unit(USD/MT)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileId" runat="server" 
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Unit_Price") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="30%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate >

                                                  <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                            <asp:ImageButton ID="ImageButton1" style="border: 0; width: 14px; height: 14px" Text="Update" 
                                                              ForeColor="Black"  OnCommand="onUpdateReDelivery_Click"  CommandArgument='<%#Eval("[Delivery_Bunker_ID]")%>' 
                                                                ImageUrl="../Images/edit.gif" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="lbtnDelete" runat="server"
                                                                     ImageUrl="~/images/delete.png"  OnCommand="onDeleteReDelivery_Click"
                                                           CommandArgument='<%#Eval("[Delivery_Bunker_ID]")%>' 
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="5%" 
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSavebunker" />
                            
                            </Triggers>
                 </asp:UpdatePanel>
                 </td>
                </tr>
              </table>



              </div>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td  align="right">
                       <asp:Button ID="btnCharterList" Text="Charterer List" runat="server" Visible="false"
                            onclick="btnCharterList_Click" />&nbsp;
                     <asp:Button ID="btnSaveExit" runat="server" Width="150px" Font-Bold="true"  ValidationGroup="vgSubmit"  OnClientClick="return ValidateBroker();"
                            Text="Save & Exit" onclick="btnSaveExit_Click"  />
                    </td>
                    
                        <td align="left">
                        <asp:Button ID="btnSave" runat="server"  onclick="btnSave_Click" Font-Bold="true" OnClientClick="return ValidateBroker();"
                             Text="Save CP" ValidationGroup="vgSubmit" 
                            Width="150px" />
                        &nbsp;</td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <asp:HiddenField ID="hdnCPId" runat="server" />
                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" 
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSubmit" />
                    </td>
                </tr>
            </table>
            
     <div id="dvAddBunker" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 15%; z-index: 1; color: black" class="ui-widget-content">    
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
                            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center" colspan="5">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td width="20%">
                </td>
                <td width="23%" align="right">
                    <asp:Literal ID="ltFuel" runat="server" Text="Fuel Type :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:DropDownList ID="ddlFuelType" Width="150px" runat="server">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFuelType" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Fuel Type is mandatory." ControlToValidate="ddlFuelType" ValidationGroup="vgSubmitBunker"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                        <td width="20%">  &nbsp;
                        </td>
                    </tr>
                <tr>
                <td width="20%">
                </td>
                <td width="23%" align="right">
                    <asp:Literal ID="ltUnit" runat="server" Text="Unit :"></asp:Literal>
                </td>
                  <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtUnit" runat="server" Text="0.0000"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None" 
                    ErrorMessage="Unit is mandatory."  ControlToValidate="txtUnit" ValidationGroup="vgSubmitBunker"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegInvoiceValue" runat="server" ErrorMessage="Unit value is not valid"
                                    Display="None" ValidationGroup="vgSubmitBunker" ControlToValidate="txtUnit"
                                    ForeColor="Red" ValidationExpression="^[0-9.-]+$">
                                </asp:RegularExpressionValidator>
                    </td>
                        <td width="20%" align="left">  &nbsp; MT
                        </td>
                    </tr>

               <tr>
                <td width="20%">
                </td>
                <td width="23%" align="right">
                    <asp:Literal ID="Literal11" runat="server" Text="Price/Unit :"></asp:Literal>
                </td>
                   <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtPricePerUnit" runat="server" Text="0.0000"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"  Display="None"
                    ErrorMessage="Unit price is mandatory." ControlToValidate="txtPricePerUnit" ValidationGroup="vgSubmitBunker" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rgvPricePerUnit" runat="server" ErrorMessage="Price/Unit value is not valid."
                Display="None" ValidationGroup="vgSubmitBunker" ControlToValidate="txtPricePerUnit"
                ForeColor="Red" ValidationExpression="^[0-9.-]+$">
            </asp:RegularExpressionValidator>
                    </td>
                        <td width="20%" align="left">  &nbsp; USD/MT
                        </td>
                    </tr>

                    <tr>
                    <td colspan="5" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">

                        </td>
                        <td colspan="3" align="center">
                       <asp:Button ID="btnSavebunker" runat="server" Width ="100px" ValidationGroup="vgSubmitBunker" OnClick="btnSavebunker_Click"
                                Text="Save"  />&nbsp;
     
                     <asp:ValidationSummary ID="vsDelivery" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  runat ="server" ValidationGroup = "vgSubmitBunker" />
                        </td>
                    </tr>
                    </table>
                
                
                </ContentTemplate>
                </asp:UpdatePanel>
        </center>
       </ContentTemplate></asp:UpdatePanel>
        <div align="left" style="border: 1px solid gray; margin-top: 1px;">
            <asp:UpdatePanel ID="updMaker" runat="server">
                <ContentTemplate>
                    <table width="100%">
                    <tr>
                    <td>
                        <asp:Button ID="btnRemarks" Text="Remarks/Queries"  runat="server" 
                            onclick="btnRemarks_Click" />&nbsp;

                        <asp:Button ID="btnCharterHire" Text="Charterer Hire"  runat="server" Visible="false"
                            onclick="btnCharterHire_Click" />&nbsp;
                        <asp:Button ID="btnInvoicePreparation" Text="Hire Invoice" OnClientClick='OpenInvPrep();return false;' runat="server" />
                        
                        <asp:Button ID="btnRemittance" Text="Remittance Receipt"  runat="server" 
                            onclick="btnRemittance_Click"/>&nbsp;

                        <asp:Button ID="btnOwnerExpenses" Text="Owner Expenses"  runat="server" 
                            onclick="btnOwnerExpenses_Click"  />&nbsp;

                        <asp:Button ID="btnIncomeMatching" Text="Income Matching"  runat="server" OnClientClick='OpenInvcomeMatch();return false;'
                            onclick="btnIncomeMatching_Click" />&nbsp;

                        <asp:Button ID="btnDocuments" Text="Documents"  runat="server" OnClientClick='OpenAttachments();return false;' 
                            onclick="btnDocuments_Click" />&nbsp;

                        <asp:Button ID="btnBillingItems" Text="Billing Items" OnClientClick='OpenBillingItem();return false;'  runat="server" 
                            onclick="btnBillingItems_Click"  />&nbsp;

                        <asp:Button ID="btnTradingRange" Text="Trading Range"  runat="server" 
                            onclick="btnTradingRange_Click"  />
                    </td>
                   
                    
                    </tr>
                        <tr>
                            <td>

                               <iframe id="iFrame1" runat="server" style="width:100%; height:450px; border:0px;"></iframe>

                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
