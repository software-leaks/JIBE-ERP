<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Change_Request.aspx.cs"
    Inherits="ASL_ASL_Change_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change Request</title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
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
  
    <style type="text/css">
        div
        {
            margin: 5px;
        }
        
        #tblChangeRequest
        {
            font-family: Arial, Verdana, Sans-Serif;
            font-size: 12px;
        }
        
        #tblChangeRequest td
        {
            text-align: left;
            border: solid 1px #cccccc;
        }
        #tblChangeRequest1
        {
            font-family: Arial, Verdana, Sans-Serif;
            font-size: 12px;
        }
        
        #tblChangeRequest1 td
        {
            text-align: left;
            border: solid 1px #cccccc;
        }
    </style>
    <script type="text/javascript">
        function OpenScreen(ID, Eval_ID) {
            var url = 'ASL_CR_Approver.aspx?Supp_ID=' + ID + '&Eval_ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_CR_Approver', 'Supplier Change Request Approver', url, 'popup', 700, 600, null, null, false, false, true, null);
        }

        $(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
        function validation() {

            var ddlReport = document.getElementById("<%=ddlAccApprover.ClientID%>");
            if (ddlReport != null) {
                var SCode = document.getElementById('ddlAccApprover').value;

                if (document.getElementById("ddlAccApprover").value == "0") {
                    alert("Payment Related Approver is mandatory.");
                    document.getElementById("ddlAccApprover").focus();
                    return false;
                }

            }
            var ddlReport = document.getElementById("<%=ddlAccFinalApprover.ClientID%>");
            if (ddlReport != null) {
                var SCode = document.getElementById('ddlAccFinalApprover').value;

                if (document.getElementById("ddlAccFinalApprover").value == "0") {
                    alert("Payment Related Final Approver is mandatory.");
                        document.getElementById("ddlAccFinalApprover").focus();
                        return false;
                    }
                   
            }

                var ddlReport = document.getElementById("<%=ddlAdminApprover.ClientID%>");
                if (ddlReport != null) {
                    var SCode = document.getElementById('ddlAdminApprover').value;

                    if (document.getElementById("ddlAdminApprover").value == "0") {
                        alert("Administrator Approver is mandatory.");
                        document.getElementById("ddlAdminApprover").focus();
                        return false;
                    }

                }
                var ddlReport = document.getElementById("<%=ddlAdminFinalApprover.ClientID%>");
                if (ddlReport != null) {
                    var SCode = document.getElementById('ddlAdminFinalApprover').value;

                    if (document.getElementById("ddlAdminFinalApprover").value == "0") {
                        alert("Administrator Final Approver is mandatory.");
                        document.getElementById("ddlAdminFinalApprover").focus();
                        return false;
                    }

                }

                var ddlReport = document.getElementById("<%=ddlGenApprover.ClientID%>");
                if (ddlReport != null) {
                    var SCode = document.getElementById('ddlGenApprover').value;

                    if (document.getElementById("ddlGenApprover").value == "0") {
                        alert("General Approver is mandatory.");
                        document.getElementById("ddlGenApprover").focus();
                        return false;
                    }

                }
                var ddlReport = document.getElementById("<%=ddlGenFinalApprover.ClientID%>");
                if (ddlReport != null) {
                    var SCode = document.getElementById('ddlGenFinalApprover').value;

                    if (document.getElementById("ddlGenFinalApprover").value == "0") {
                        alert("General Final Approver is mandatory.");
                        document.getElementById("ddlGenFinalApprover").focus();
                        return false;
                    }

                }
               
            return true;
        }


        function myFunction() {

            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var w = (size.width);
            var y = (size.height);

            var body = document.body,
             html = document.documentElement;

            var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
            document.getElementById('blur-on-updateprogress').setAttribute("style", "height:" + height + "px");
            //            document.getElementById('iFrame1').setAttribute("style", "width:" + w * 0.65 + "px");
            //            document.getElementById('iFrame1').style.overflow = scroll;
            //alert(height);


        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptM" runat="server">
    </asp:ScriptManager>
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

    <asp:Panel ID="pnlChangeRequest" runat="server" Visible="true">
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
        <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
            color: Black; height: 100%;">
            <asp:UpdatePanel ID="upd1" runat="server">
                <ContentTemplate>
                    <center>
                    <div id="page-title" class="page-title">
                        Change Request
                    </div>
                        <table width="100%">
                        <tr id="trSubmitted" runat="server">  
                        <td  align="right" style="color: #FF0000;">  <asp:Button ID="btnGroup" runat="server" Text="ASL Column Group Relationship"  OnClientClick='OpenScreen(null,null);return false;'
                                             />&nbsp;&nbsp;&nbsp;&nbsp;
                        Supplier Code &nbsp;:&nbsp;<asp:Label ID="lblSupplierCode"  runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                Submitted By &nbsp;:&nbsp; <asp:Label ID="lblSubmittedBY"  runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                Submitted Date &nbsp;:&nbsp;<asp:Label ID="lblSubmitteddate"  runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                               
                            </td>
                        </tr>
                        </table>
                        <table border="1" width="100%" cellpadding="5" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <strong>Data Fields</strong>
                                </td>
                                <td align="center">
                                    <strong>Current Value</strong>
                                </td>
                                <td align="center">
                                    <strong>New Value</strong>
                                </td>
                                <td align="center">
                                    <strong>Reason For Change</strong>
                                </td>
                                <td align="center" id="tdHeader" runat="server" visible="false">
                                    <strong>Select</strong>
                                </td>
                            </tr>
                             <tr id="trAccount" visible="false"  class="page-title"  runat="server">
                                <td align="right" style="width: 200px">
                                   <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Payment Related"></asp:Label>
                                </td>
                                <td align="left" colspan="3" >
                                     <asp:Label ID="Label3" runat="server" Text="1st Approver"></asp:Label>&nbsp;&nbsp;
                                       <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:DropDownList ID="ddlAccApprover" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label4" runat="server" Text="Final Approver"></asp:Label>&nbsp;&nbsp;
                                     <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:DropDownList ID="ddlAccFinalApprover" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                              
                            </tr>
                            <tr id="trRegname" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblRegname" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblRegname1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCompanyResgName" runat="server" Enabled="false" MaxLength="500" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCNameReason" runat="server" Enabled="false" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCname" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCname" runat="server" />
                                    <asp:HiddenField ID="hdnCName" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPayment" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPayment" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPayment1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPayment" runat="server" Enabled="false" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBankReason" runat="server" Enabled="false" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPayment" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPayment" runat="server" />
                                    <asp:HiddenField ID="hdnPayment" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPaymentEmail" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymentEmail" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymentEmail1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentEmail" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentEmailReason" Enabled="false" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentEmail" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentEmail" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentEmail" runat="server" />
                                </td>
                            </tr>
                             <tr id="trTerms" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblTerms" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblTerms1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTerms" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTermsReason" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkTerms" visible="false" runat="server">
                                    <asp:CheckBox ID="chkTerms" runat="server" />
                                    <asp:HiddenField ID="hdnTerms" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRPaymenyInterval" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymenyInterval" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymenyInterval1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlPaymentInterval" Enabled="false" CssClass="txtInput" Width="300px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentIntervalReason" Enabled="false" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentInterval" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentInterval" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentInterval" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRPaymenypriority" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymenypriority" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymenypriority1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbPaymentPriority" Enabled="false" CssClass="txtInput" Width="300px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                        <asp:ListItem Value="Immediate">Immediate</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentPriorityReason" Enabled="false" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentPriority" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentPriority" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentPriority" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRPaymentTerms" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymentTerms" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymentTerms1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlPaymentTerms" Enabled="false" CssClass="txtInput" Width="300px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentTermsReason" Enabled="false" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentTerms" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentTerms" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentTerms" runat="server" />
                                </td>
                            </tr>

                             <tr id="trAdmin" visible="false"   class="page-title"  runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="Label10" runat="server" Text="Administrator"></asp:Label>
                                </td>
                                <td align="left" colspan="3" >
                                <asp:Label ID="Label5" runat="server" Text="1st Approver"></asp:Label>&nbsp;&nbsp;
                                      <asp:Label ID="Label12" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:DropDownList ID="ddlAdminApprover" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="Label6" runat="server" Text="Final Approver"></asp:Label>&nbsp;&nbsp;
                                       <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:DropDownList ID="ddlAdminFinalApprover" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                
                            </tr>

                            <tr id="trType" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblType1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlType" runat="server" Enabled="false" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTypeReason" runat="server" Enabled="false" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkType" visible="false" runat="server">
                                    <asp:CheckBox ID="chkType" runat="server" />
                                    <asp:HiddenField ID="hdnType" runat="server" />
                                </td>
                            </tr>
                               <tr id="trTaxNumber" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblTaxNumber" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblTaxNumber1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                  <asp:TextBox ID="txtTaxNumber" runat="server" Enabled="false" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTaxNumberReason" runat="server" Enabled="false" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdTaxNumber" visible="false" runat="server">
                                    <asp:CheckBox ID="chkTaxNumber" runat="server" />
                                    <asp:HiddenField ID="hdnTaxNumber" runat="server" />
                                </td>
                            </tr>
                             <tr id="trSubType" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblSubType" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblSubType1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlSubType" runat="server" Enabled="false" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSubTypeReason" runat="server" Enabled="false" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkSubType" visible="false" runat="server">
                                    <asp:CheckBox ID="chkSubType" runat="server" />
                                    <asp:HiddenField ID="hdnSubType" runat="server" />
                                </td>
                            </tr>
                             <tr id="trShortname" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblShortname" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblShortname1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtShortName" MaxLength="255" Enabled="false" Width="300px" runat="server" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtShortNameReason" MaxLength="255"  Enabled="false" Width="300px" runat="server"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkShortName" visible="false" runat="server">
                                    <asp:CheckBox ID="chkShortName" runat="server" />
                                    <asp:HiddenField ID="hdnShortName" runat="server" />
                                </td>
                            </tr>
                            <tr id="trCurrency" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblCurrency1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlCurrency" runat="server" Enabled="false" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCurrencyReason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCurrency" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCurrency" runat="server" />
                                    <asp:HiddenField ID="hdnCurrency" runat="server" />
                                </td>
                            </tr>
                            <tr id="trOwnerShip" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblOwnerShip" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblOwnerShip1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlownerShip" runat="server" Enabled="false" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtownerShipReason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkOwnership" visible="false" runat="server">
                                    <asp:CheckBox ID="chkOwnership" runat="server" />
                                    <asp:HiddenField ID="hdnownerShip" runat="server" />
                                </td>
                            </tr>
                            <tr id="trInvoiceStatus" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblInvoiceStatus" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblInvoiceStatus1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbInvoiceStatus" CssClass="txtInput" Enabled="false" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtInvoiceStatusReason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkInvoiceStatus" visible="false" runat="server">
                                    <asp:CheckBox ID="chkInvoiceStatus" runat="server" />
                                    <asp:HiddenField ID="hdnInvoiceStatus" runat="server" />
                                </td>
                            </tr>
                            <tr id="trdirectinvoice" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lbldirectinvoice" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lbldirectinvoice1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbdirectinvoice" CssClass="txtInput" Enabled="false" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtdirectinvoiceReason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkDirectInvoice" visible="false" runat="server">
                                    <asp:CheckBox ID="chkDirectInvoice" runat="server" />
                                    <asp:HiddenField ID="hdndirectinvoice" runat="server" />
                                </td>
                            </tr>
                            
                            <tr id="TRPaymentHistory" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymentHistory" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymentHistory1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbPaymentHistory" CssClass="txtInput" Enabled="false" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentHistoryReason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentHistory" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentHistory" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentHistory" runat="server" />
                                </td>
                            </tr>

                            <tr id="trGeneral" class="page-title" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="Label11" runat="server" Text="General"></asp:Label>
                                </td>
                               <td align="left" colspan="3" >
                               <asp:Label ID="Label7" runat="server" Text="1st Approver"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:DropDownList ID="ddlGenApprover" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="Label8" runat="server" Text="Final Approver"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:DropDownList ID="ddlGenFinalApprover" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                               
                            </tr>

                            <tr id="trAddress" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSuppAddress" TextMode="MultiLine" Enabled="false" MaxLength="1000" Height="40px"
                                        Width="300px" runat="server" CssClass="txtInput"> </asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSuppAddressReason" TextMode="MultiLine" Enabled="false" runat="server" MaxLength="500"
                                        Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkAddress" visible="false" runat="server">
                                    <asp:CheckBox ID="chkAddress" runat="server" />
                                    <asp:HiddenField ID="hdnSuppAddress" runat="server" />
                                </td>
                            </tr>
                            <tr id="trCountry" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblCountry1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlCountry" runat="server" Enabled="false" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCountryReason" runat="server" Enabled="false" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCountry" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCountry" runat="server" />
                                    <asp:HiddenField ID="hdnCountry" runat="server" />
                                </td>
                            </tr>
                            <tr id="trCity" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblCity1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCity" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCityReason" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCity" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCity" runat="server" />
                                    <asp:HiddenField ID="hdnCity" runat="server" />
                                </td>
                            </tr>
                            <tr id="trEmail" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblEmail1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtEmail" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegCompEmail" Display="None" runat="server" ValidationGroup="vgSubmit"
                                        ErrorMessage="Company Email is not valid" ControlToValidate="txtEmail" ValidationExpression="^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                        ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtEmailReason" runat="server" Enabled="false" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkEmail" visible="false" runat="server">
                                    <asp:CheckBox ID="chkEmail" runat="server" />
                                    <asp:HiddenField ID="hdnEmail" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPhone" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPhone1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPhone" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegPhone" runat="server" ErrorMessage="Company Phone Number is not valid"
                                        Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPhone" ForeColor="Red"
                                        ValidationExpression="^[ 0-9,()+-]+$">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPhoneReason" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPhone" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPhone" runat="server" />
                                    <asp:HiddenField ID="hdnPhone" runat="server" />
                                </td>
                            </tr>
                            <tr id="trFax" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblFax1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtFax" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegFax" runat="server" ErrorMessage="Fax is not valid"
                                        Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtFax" ForeColor="Red"
                                        ValidationExpression="^[ 0-9,()+-]+$">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtFaxReason" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkFax" visible="false" runat="server">
                                    <asp:CheckBox ID="chkFax" runat="server" />
                                    <asp:HiddenField ID="hdnFax" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICName" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICName" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICName1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICName" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICNameReason" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICName" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICName" runat="server" />
                                    <asp:HiddenField ID="hdnPICName" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICEmail" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICEmail" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICEmail1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmail" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegPICEmail" Display="None" runat="server" ValidationGroup="vgSubmit"
                                        ErrorMessage="PIC Email is not valid" ControlToValidate="txtPICEmail" ValidationExpression="^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                        ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmailReason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICEmail" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICEmail" runat="server" />
                                    <asp:HiddenField ID="hdnPICEmail" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICPhone1" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICPhone" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICPhone1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhone" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegPICPhone" runat="server" ErrorMessage="PIC Phone is not valid"
                                        Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPICPhone" ForeColor="Red"
                                        ValidationExpression="^[ 0-9,()+-]+$">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhoneReason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICPhone" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICPhone" runat="server" />
                                    <asp:HiddenField ID="hdnPICPhone" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICName2" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICName2" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICName21" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICName2" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICName2Reason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICName2" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICName2" runat="server" />
                                    <asp:HiddenField ID="hdnPICName2" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICEmail2" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICEmail2" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICEmail21" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmail2" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegPICEmailID2" Display="None" runat="server"
                                        ValidationGroup="vgSubmit" ErrorMessage="2nd PIC Email is not valid" ControlToValidate="txtPICEmail2"
                                        ValidationExpression="^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                        ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmail2Reason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICEmail2" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICEmail2" runat="server" />
                                    <asp:HiddenField ID="hdnPICEmail2" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICPhone2" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICPhone2" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICPhone21" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhone2" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegPICPhone2" runat="server" ErrorMessage="2nd PIC Phone is not valid"
                                        Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPICPhone2" ForeColor="Red"
                                        ValidationExpression="^[ 0-9,()+-]+$">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhone2Reason" runat="server" Enabled="false" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICPhone2" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICPhone2" runat="server" />
                                    <asp:HiddenField ID="hdnPICPhone2" runat="server" />
                                </td>
                            </tr>
                           
                            <tr id="trScope" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblScope" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblScope1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" valign="top" style="width: 300px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <biv style="float: left; text-align: left; width: 300px;">
                                                <asp:DropDownList ID="ddlScope" runat="server" visible="false" Width="300px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnScopeAdd" runat="server" visible="false" Text="Add Scope" OnClick="btnAdd_Click" />
                                                <br />
                                               <div style="float: left; text-align: left; width: 300px; height: 80px; overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                                                            <asp:CheckBoxList ID="chkScope"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                </biv>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtScopeReason" runat="server" Enabled="false" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td align="left" id="tdchkScope" visible="false" runat="server">
                                    <asp:CheckBox ID="chkScope1" runat="server" />
                                    <asp:HiddenField ID="hdnScope" runat="server" />
                                    <asp:HiddenField ID="hdnAddScope" runat="server" />
                                </td>
                            </tr>
                             <tr id="trSupplierDesc" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblSupplierDesc" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblSupplierDesc1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSupplierDesc" MaxLength="2000" Enabled="false"  Height="90px" Width="300px" TextMode="MultiLine" runat="server" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSupplierDescReason" MaxLength="255" Enabled="false" Height="90px" TextMode="MultiLine" Width="300px" runat="server"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkSupplierDesc" visible="false" runat="server">
                                    <asp:CheckBox ID="chkSupplierDesc" runat="server" />
                                    <asp:HiddenField ID="hdnSupplierDesc" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPort" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPort" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPort1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" valign="top" style="width: 300px">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div style="float: left; text-align: left; width: 300px;">
                                                <asp:DropDownList ID="ddlPort" visible="false" runat="server" Width="300px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                
                                                <asp:Button ID="btnPortAdd" visible="false" runat="server" Text="Add Port" OnClick="btnPortAdd_Click" />
                                                <br />
                                                <div style="float: left; text-align: left; width: 300px; height: 80px; overflow-x: hidden;
                                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                    background-color: #ffffff;">
                                                    <asp:CheckBoxList ID="chkPort" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPortReason" Enabled="false" runat="server" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPort" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPort1" runat="server" />
                                    <asp:HiddenField ID="hdnPort" runat="server" />
                                    <asp:HiddenField ID="hdnAddPort" runat="server" />
                                </td>
                            </tr>
                            
                            
                           
                            <tr id="trTaxRate" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblTaxRate" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblTaxRate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTaxRate" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegTaxRate" runat="server" ErrorMessage="Tax Rate is not valid"
                                        Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtTaxRate" ForeColor="Red"
                                        ValidationExpression="^[0-9.]+$"></asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTaxRateReason" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkTaxRate" visible="false" runat="server">
                                    <asp:CheckBox ID="chkTaxRate" runat="server" />
                                    <asp:HiddenField ID="hdnTaxRate" runat="server" />
                                </td>
                            </tr>
                            
                            <tr id="trbiz" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblBiz" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblBiz1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtbiz" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegBiz" runat="server" ErrorMessage="Biz Incorporation is not valid"
                                        Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtbiz" ForeColor="Red"
                                        ValidationExpression="^[0-9]+$">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtbizReason" runat="server" Enabled="false" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkBiz" visible="false" runat="server">
                                    <asp:CheckBox ID="chkBiz" runat="server" />
                                    <asp:HiddenField ID="hdnBiz" runat="server" />
                                </td>
                            </tr>
                           <tr id="TRAutoSendPO" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblAutoSendPO" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblAutoSendPO1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbAutoSendPO" CssClass="txtInput" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtAutoSendPO" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdAutoSendPO" visible="false" runat="server">
                                    <asp:CheckBox ID="chkAutoSendPO" runat="server" />
                                    <asp:HiddenField ID="hdnAutoSendPO" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div>
                        <center>
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="center" style="background-color: #DDDDDD">
                                       <asp:Button ID="btnRecallDraft" runat="server" Width="100px" Text="Recall Draft"
                                            ValidationGroup="vgSubmit" OnClick="btnRecallDraft_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnSubmitRequest" runat="server" Width="100px" Text="Submit Request"
                                            ValidationGroup="vgSubmit" OnClick="btnSubmitRequest_Click" OnClientClick="return validation();" />&nbsp;&nbsp;
                                        <asp:Button ID="btnRecallRequest" runat="server"  Width="100px" Enabled="false" Text="Recall Request"
                                            OnClick="btnRecallRequest_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnApprove" Visible="false" runat="server" Width="200px" OnClientClick="return validation();" Text="Approve Selected Changes"
                                            OnClick="btnApprove_Click" />&nbsp;&nbsp;
                                             <asp:Button ID="btnRecallApprove" runat="server"  Width="150px" Visible="false" Text="Recall Approved Request"
                                            OnClick="btnRecallRequest_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnFinalApprove" Visible="false" runat="server" Width="200px" OnClientClick="return validation();" 
                                            Text="Final Approve Selected Changes" onclick="btnFinalApprove_Click"
                                             />   
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnReject" Visible="false" runat="server" Width="200px" Text="Reject Selected Changes"
                                            OnClick="btnReject_Click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
         <asp:HiddenField ID="hdnCRID" runat="server" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
