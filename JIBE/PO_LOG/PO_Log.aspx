<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="PO_Log.aspx.cs"
     Title="PO LOG" Inherits="PO_LOG_PO_Log" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }
        function OpenScreen(ID, Job_ID) {
            var val = document.getElementById('txtSupplyID').value;

            var url = 'POLOG_Item_Entry.aspx?ID=' + ID + '&SupplyID=' + val;
            OpenPopupWindowBtnID('PO_Item_Entry', 'Item Entry', url, 'popup', 700, 1150, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenSupplierRemarks(ID, Job_ID) {
            var val = document.getElementById("ddlSupplier").selectedIndex;
            var SuppID = document.getElementById("ddlSupplier").options[val].value;
            var url = 'PO_Log_Supplier_Remarks.aspx?ID=' + ID + '&Supp_ID=' + SuppID;
            OpenPopupWindowBtnID('ASL_Remarks', 'Supplier Remarks', url, 'popup', 600, 1050, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenAttach(ID, Job_ID) {
            var val = document.getElementById('txtSupplyID').value;
            var Type = 'PO_DOCUMENT';
            var url = 'PO_Log_Attachment.aspx?ID=' + val + '&DocType=' + Type;
            OpenPopupWindowBtnID('PO_Log_Attachment', 'Add Attachment', url, 'popup', 600, 1050, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenRemarks(ID, Job_ID) {
            var POID = document.getElementById("txtSupplyID").value;
            var Type = 'POLOG';
            var url = 'PO_Log_Remarks.aspx?ID=' + ID + '&POID=' + POID + '&Type=' + Type;
            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var w = (size.width);
            var y = (size.height);
            OpenPopupWindowBtnID('PO_Log_Remarks', 'Remarks Entry', url, 'popup', 400, 700, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenTransLog(ID, Job_ID) {
            var val = document.getElementById('txtSupplyID').value;
            var Type = 'POLOG';
            var url = 'PO_Log_AuditTrail.aspx?Code=' + val + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_AuditTrail', 'Transaction History', url, 'popup', 500, 1000, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenScreenPreview(ID, Job_ID) {
            var val = document.getElementById('txtSupplyID').value;
            var url = 'PO_Log_Preview.aspx?ID=' + ID + '&SUPPLY_ID=' + val;
            //window.open(url, "_blank");
            OpenPopupWindowBtnID('PO_Log_Preview', 'PO Preview', url, 'popup', 1000, 1100, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenApprovalLimit(ID, Job_ID) {
            var val = document.getElementById('txtSupplyID').value;
            var url = 'PO_LOG_View_Approval_Limit.aspx?ID=' + ID + '&SUPPLY_ID=' + val;

            OpenPopupWindowBtnID('PO_LOG_View_Approval_Limit', 'Approval Limit', url, 'popup', 450, 800, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenASL(ID) {
            var Supplier_ID = document.getElementById("ddlSupplier").selectedIndex;
            var Supplier_Code = document.getElementById("ddlSupplier").options[Supplier_ID].value;
            var url = "../ASL/ASL_General_Data.aspx?Supp_ID=" + Supplier_Code
            window.open(url, "_blank");
        }
        function OpenSupplierNotice(ID, Job_ID) {
            var POID = document.getElementById("txtSupplyID").value;
            var Type = 'POLOG';
            var url = 'PO_Log_Notice.aspx?ID=' + ID + '&POID=' + POID + '&Type=' + Type;
            OpenPopupWindowBtnID('PO_Log_Notice', 'Notice', url, 'popup', 700, 1000, null, null, false, false, true, null, 'btnFilter');
        }
        function OpenAdminPage(ID, Job_ID) {
            var POID = document.getElementById("txtSupplyID").value;

            var url = 'PO_Log_Admin.aspx?Supply_ID=' + POID;
            OpenPopupWindowBtnID('Admin', 'Admin', url, 'popup', 600, 1000, null, null, false, false, true, null, 'btnFilter');
        }
    </script>
    <script id="DocumentReady" type="text/javascript">
        function loadDiv() {
            var val = document.getElementById('txtSupplyID').value;
            $('#dvdialog').load('PO_Log_Preview.aspx?ID=1&SUPPLY_ID=' + val + "&rnd=" + Math.random() + ' #dvPageContent');

        }
        function GetDivElement() {
            //            debugger;
            //            var str = document.getElementById('dvdialog').innerHTML;
            //            alert("HI");
            //var res = str.replace(/"/g, "'");
            //document.getElementById('hdnCrewDetails').value = res;
            //document.getElementById('dvdialog').innerHTML = document.getElementById('dvdialog').innerHTML;
            //$("#hdnCrewDetails").val = $("#dvdialog").innerHTML;
            document.getElementById('hdnPODetails').value = document.getElementById('dvdialog').innerHTML;
        }
        
    </script>
    <script type="text/javascript">

        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }

        function VallidateGrid() {
            // debugger;
            var strCommonPart = "rgdItems_ctl00";
            var SubId = "_ctl";
            var j = 4;
            var Count = 0;
            var Len = strCommonPart.length;
            for (var i = 0; i < 10; i++) {

                if (j <= 8) {
                    var cntrlId = strCommonPart + SubId + "0" + j + "_txtItem_Description";
                }
                else {
                    var cntrlId = strCommonPart + SubId + j + "_txtItem_Description";
                }
                // var Value = document.getElementById(cntrlId).value;
                // if (Value != "") {
                Count++;
                if (j <= 8) {
                    var ChlcntrlId = strCommonPart + SubId + "0" + j + "_txtRequest_Qty";
                }
                else {
                    var ChlcntrlId = strCommonPart + SubId + j + "_txtRequest_Qty";
                }
                //                    var Value1 = document.getElementById(ChlcntrlId).value;
                //                    if (Value1 == "") {
                //                        alert("Please provide Order Qty for row No:" + (i + 1));
                //                        return false;
                //                    }
                //                }
                //                else {

                //                }
                j = j + 2;

            }
            if (Count == 0) {
                alert("Please provide atleast one ItemDescription");
                return false;
            }
            return true;
        }
        function MaskMoney(evt) {
            if (!(evt.keyCode == 9 || evt.keyCode == 190 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
        <div style="font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
            <%--<div id="Div2" class="page-title">
                PO LOG
            </div>--%>
            <div style="color: Black;">
                <div id="PODiv" runat="server">
                    <asp:UpdatePanel ID="paneltable" runat="server">
                        <contenttemplate>
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr id="trSupplierMsg" visible="false" runat="server" >
                        <td colspan="3" align="left">
                        <font color="red" Size="4">
                        <asp:Label ID="lblSupplierMsg" Font-Bold="True"  Width="845px" runat="server" 
                                Text="Invalid Supplier! Please Update before submitting for approval."></asp:Label>
                        </font>
                        </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="PO Type : "></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblPoType" Font-Bold="true" Width="300px" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" align="left">
                                <asp:Label ID="lblReprtValue" runat="server" Font-Bold="true" Visible="false" Text="Report USD Value :"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblReport_USD_Value" Visible="false" Font-Bold="true" runat="server" Text=""></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblPoClosed" runat="server" Font-Bold="true" Visible="false" ForeColor="Red"
                                    Text="PO CLOSED"></asp:Label>
                            </td>
                            <td style="text-align: Right;">
                                <asp:Label ID="lblPaymentPriority" runat="server" Visible="false" Text="Payment Priority -"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblPaypent" runat="server" Visible="false" Text=""></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblPayment_Terms" runat="server" Font-Bold="true" ForeColor="Blue"
                                    Visible="false" Text="Default Payment Terms Days -"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblPaymentTerms" runat="server" Font-Bold="true" ForeColor="Blue"
                                    Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" style="color: #FF0000;">
                                Created By :&nbsp;&nbsp;<asp:Label ID="lblCreatedBY" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                Created Date :&nbsp;&nbsp;<asp:Label ID="lbldate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <asp:Label ID="lblApproved" runat="server" Visible="false" Text="Approved By :"></asp:Label>  &nbsp;&nbsp;<asp:Label ID="lblApprovedBy" Visible="false" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Label ID="Label3" runat="server" Visible="false" Text="Approved By :"></asp:Label>  &nbsp;&nbsp;<asp:Label ID="Label5" Visible="false" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                           
                                <table width="100%" cellpadding="2"  cellspacing="0">
                                    <tr>
                                        <td style="text-align: Right; width: 10%;">
                                            Supply ID :
                                        </td>
                                        <td style="width: 1%">
                                        </td> 
                                        <td  style="text-align: left; width: 40%;"  >
                                            <asp:TextBox ID="txtSupplyID" Enabled="false" runat="server" MaxLength="255" Width="60%" 
                                                CssClass="txtInput"></asp:TextBox>  
                                        </td>
                                        <td style="text-align: Right; width: 10%;">
                                            PO Reference :
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="text-align: left; width: 40%;">
                                            <asp:TextBox ID="txtReferance" Enabled="false" runat="server"  Width="60%"  MaxLength="255"
                                                CssClass="txtInput"></asp:TextBox>
                                        </td>
                                           
                                    </tr>
                                    <tr>
                                        <td style="text-align: Right;">
                                            Vessel :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlVessel" runat="server" CssClass="txtInput" AutoPostBack="true"
                                               Width="60%" onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVessel" runat="server" Display="None" InitialValue="0"
                                                ErrorMessage="Vessel is mandatory field." ControlToValidate="ddlVessel" ValidationGroup="vgSubmit"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: Right;">
                                            Ship's Reference :
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtShipReferance" runat="server" MaxLength="255" Width="60%" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: Right;">
                                            Supply Port :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtPort" runat="server" MaxLength="255" Width="60%" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td style="text-align: Right;">
                                            ETA/ETD :
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtETA" runat="server" MaxLength="255" Width="60%" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: Right;">
                                            Urgency :
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtUrgency" runat="server" MaxLength="20" Width="60%" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td style="text-align: Right;">
                                            Agent :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                           
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlAgent" runat="server" CssClass="txtInput" Width="60%">
                                            </asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                InitialValue="0" ErrorMessage="Agent is mandatory field." ControlToValidate="ddlAgent"
                                                ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: Right;">
                                            Currency :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlCurrency" AutoPostBack="true" runat="server" CssClass="txtInput"
                                                Width="190px" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                            </asp:DropDownList>
                                          
                                           
                                            <asp:RequiredFieldValidator ID="ReqCurrency" runat="server" Display="None" InitialValue="0"
                                                ErrorMessage="Currency is mandatory field." ControlToValidate="ddlCurrency" ValidationGroup="vgSubmit"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: Right;">
                                            Account Type :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="txtInput" Width="60%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ReqAccountType" runat="server" Display="None" InitialValue="0"
                                                ErrorMessage="Account Type is mandatory field." ControlToValidate="ddlAccountType"
                                                ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: Right;">
                                            Currency Change
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtCurrChange" runat="server" MaxLength="255" Width="30%" CssClass="txtInput"></asp:TextBox></td>
                                        <td style="text-align: Right;">
                                            &nbsp;Account Classification :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlAccClassifictaion" runat="server" CssClass="txtInput" Width="60%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ReqAccountClass" runat="server" Display="None" InitialValue="0"
                                                ErrorMessage="Account Classification is mandatory field." ControlToValidate="ddlAccClassifictaion"
                                                ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="text-align: Right;">
                                            Supplier :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td  style="text-align: left;">
                                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" Width="60%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ReqSupplier" runat="server" Display="None" InitialValue="0"
                                                ErrorMessage="Supplier is mandatory field." ControlToValidate="ddlSupplier" ValidationGroup="vgSubmit"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: Right;">
                                            Supplier Reference :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                           
                                        </td>
                                        <td  style="text-align: left;">
                                            <asp:TextBox ID="txtSuppRef" runat="server" CssClass="txtInput" MaxLength="255" Width="40%"></asp:TextBox>&nbsp;&nbsp;
                                             <asp:Button ID="btnContact" runat="server" Text="Manage Contact" OnClientClick="OpenASL(0);return false;" Width="120px"  />
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td style="text-align: Right;">
                                            Remarks :
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left;" colspan="2">
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" MaxLength="1000" 
                                                Width="60%" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td colspan="2" style="text-align: left;" valign="middle">
                                            <asp:Label ID="lblPOAmount" runat="server" Visible="false" Font-Bold="true" Text="Value US$ :"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPo_Used_Value" Font-Bold="true" Visible="false"
                                                runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     
                                    <tr runat="server" id="trOwner" visible="false">
                                        <td style="text-align: Right;">
                                         Owner Code :</td>
                                         <td style="text-align: Right;">
                                        </td>
                                        <td  style="text-align: left;">
                                           <asp:DropDownList ID="ddlOwnerCode" runat="server" CssClass="txtInput" Width="40%">
                                </asp:DropDownList></td>
                                    </tr>
                                   
                                   <tr runat="server" id="trPortCall" visible="false">
                                        <td style="text-align: Right;">
                                         Port Call :</td>
                                         <td style="text-align: Right;">
                                        </td>
                                        <td  style="text-align: left;">
                                           <asp:DropDownList ID="ddlPortCall" runat="server" CssClass="txtInput" Width="60%">
                                </asp:DropDownList></td>
                                    </tr>
                                      <tr runat="server" id="trCharterParty" visible="false">
                                        <td style="text-align: Right;">
                                         Charter Party :</td>
                                         <td style="text-align: Right;">
                                        </td>
                                        <td  style="text-align: left;">
                                         <asp:DropDownList ID="ddlCharterParty" runat="server" CssClass="txtInput" 
                                               Width="60%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                            <td colspan="6">
                                <br />
                                <br />
                               
                            </td>
                        </tr>
                                </table>
                               
                            </td>
                        </tr>
                    </table>
                  
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr id="trSubmission" runat="server" visible="false">
                            <td align="left" colspan="3">
                                PO submitted to&nbsp;
                                <asp:Label ID="lblAction" Font-Bold="true" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                for Approval on&nbsp;
                                <asp:Label ID="lbllineDate" Font-Bold="true" runat="server" Text=""></asp:Label>
                                Request sent on :&nbsp;&nbsp;
                                <asp:Label ID="lblRequest" Font-Bold="true" runat="server" Text=""></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="btnRecall" runat="server" Width="150px" Text="Recall PO" OnClientClick="return confirm('Confirm recall PO from approval?')" 
                                    onclick="btnRecall_Click" />
                            </td>
                            <%--<td align="left" colspan="3">
                               &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                    ID="btnEmailRequest" runat="server" Visible="false" Width="150px" Text="Request Email Approval" />
                                <br />
                                <br />
                                <br />
                            </td>--%>
                        </tr>
                         <tr id="trmsg" runat="server" visible="false">
                            <td align="left" colspan="5">
                               <asp:Label ID="lblApprovalmsg" ForeColor="Red" Font-Bold="true" runat="server" Text=""></asp:Label>
                            </td>
                            
                        </tr>
                        <tr id="trWorkFlow" runat="server" visible="false">
                            <td align="right">
                                WorkFlow :&nbsp;&nbsp;For Verification By :
                            </td>
                            <td align="left" colspan="4">
                                <asp:Panel ID="pnlInfo" runat="server">
                                </asp:Panel>
                                <%--   <asp:DropDownList ID="ddlVerification" runat="server" CssClass="txtInput" Width="300px">
                                </asp:DropDownList>--%>
                            </td>
                        </tr>
                        <tr id="trApprove" runat="server" visible="false">
                            <td align="right">
                                For approval by :<asp:Label ID="lbl1" runat="server" Text="*" ForeColor="Red" ></asp:Label>
                            </td>
                            <td colspan="3" align="left">
                                <asp:DropDownList ID="ddlApproval" runat="server" CssClass="txtInput" Width="300px">
                                </asp:DropDownList> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" InitialValue="0"
                                                ErrorMessage="Final approval is mandatory field." ControlToValidate="ddlApproval" ValidationGroup="vgSubmitapproval"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnApproval" runat="server" Width="150px" ValidationGroup="vgSubmitapproval" 
                                    Text="Submit For Approval" OnClick="btnApproval_Click" />&nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <br />
                                <br />
                                <br />
                            </td>
                        </tr>
                         <tr id="trIssue" runat="server" visible="false">
                            <td align="left" colspan="5">
                             <asp:TextBox ID="txtIssueDate" Visible="false" runat="server" Width="200px" BackColor="#FFFFCC"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIssueDate"
                                        Format="dd/MM/yyyy">  </ajaxToolkit:CalendarExtender>
                                        <asp:Button ID="btnIssue"  runat="server" Width="100px" ValidationGroup="vgSubmit" 
                                    Text="Issue PO" onclick="btnIssue_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="5">
                                <asp:Button ID="btnSave" runat="server" Width="100px" ValidationGroup="vgSubmit"
                                    Text="Update PO" OnClick="btnSave_Click" />&nbsp;
                                    <%--OnClick="DownloadFiles" OnClientClick="GetDivElement();"--%>
                               
                                <asp:Button ID="btnPreview"   runat="server" Visible="false" Width="100px" OnClientClick='OpenScreenPreview(null,null);return false;' Text="Preview PO" />&nbsp;
                                <asp:Button ID="btnVerify" Visible="false" runat="server" Width="100px" Text="Verify PO"
                                    ValidationGroup="vgSubmit" OnClick="btnVerify_Click" />&nbsp;
                                <asp:Button ID="btnApprove" Visible="false" runat="server" Width="100px" Text="Approve PO"
                                    ValidationGroup="vgSubmit" OnClick="btnApprove_Click" />&nbsp;
                                <asp:Button ID="btnDelete"  runat="server" Visible="false" Width="100px" Text="Delete PO"  OnClientClick="return confirm('Are you sure want to delete?')"
                                    onclick="btnDelete_Click" />&nbsp;
                                <asp:Button ID="btnUndoApproval" Visible="false" runat="server" Width="150px" 
                                    Text="Undo Approval PO" onclick="btnUndoApproval_Click" />&nbsp;
                                <asp:Button ID="btnClose" runat="server" Visible="false" Width="100px" Text="Close PO" OnClientClick="return confirm('Confirm to Close PO, No changes possible after closed.?')"
                                    OnClick="btnClose_Click" />&nbsp;
                                <asp:Button ID="btnUnClose" runat="server"  Visible="false" Width="100px" OnClientClick="return confirm('Re-Open PO 7 days for Update?')"
                                    Text="UNClose PO" OnClick="btnUnClose_Click" />&nbsp;
                                     

                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
                                <asp:Button ID="btnRemarks" runat="server" Width="100px" Text="Remarks" OnClientClick='OpenRemarks(null,null);return false;'  />&nbsp;
                                <asp:Button ID="btnNotify" runat="server" Visible="false" Width="150px" OnClientClick='OpenSupplierNotice(null,null);return false;' Text="Notify To Supplier" />&nbsp;
                                <asp:Button ID="btnViewLimit" runat="server" Width="150px" Text="View Approval Limit" OnClientClick='OpenApprovalLimit(null,null);return false;'
                                    />
                                <asp:Button ID="btnTranscLog"  runat="server" Width="120px" Text="Transaction Log" OnClientClick='OpenTransLog(null,null);return false;' />&nbsp;
                                <asp:Button ID="btnAttachDocs" runat="server"   OnClientClick='OpenAttach(null,null);return false;'
                                    Width="150px" Text="Attach Documents" />&nbsp; &nbsp;
                                <asp:Button ID="btnSupplierRemarks" OnClientClick='OpenSupplierRemarks(null,null);return false;'
                                    runat="server" Width="150px" Visible="false" Text="Remarks on Suppliers" />&nbsp; 
                                     <asp:Button ID="btnAdmin" OnClientClick='OpenAdminPage(null,null);return false;'
                                    runat="server" Width="150px"  Text="Admin Page" />
                                &nbsp;
                            </td>
                            <td align="right" > <asp:Button ID="btnExit" Width="100px" runat="server" Visible="false" ForeColor="Red" OnClientClick="refreshAndClose();" Text="Exit" 
                         /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnAdvanceReq0" runat="server" Text="Create Advance Request" 
                                    Visible="false" Width="150px" />
                                <asp:Button ID="btnFolder0" runat="server" Text="My Folders" Visible="false" 
                                    Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="display: none;">
                                <asp:Button ID="btnFilter" Visible="false" runat="server" Width="0.2px" Text="" OnClick="btnFilter_Click" />&nbsp;&nbsp;
                                <asp:TextBox ID="txtPOCode" Width="1px" runat="server"></asp:TextBox><asp:Label ID="lblcreated_by"
                                    runat="server"></asp:Label>
                                <asp:TextBox ID="txtPOType" Width="1px" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnPODetails" runat="server" />
                            </td>
                        </tr>
                    </table>
                     </contenttemplate>
                    </asp:UpdatePanel>
                </div>
                
                <div id="Div1" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow-y: hidden; overflow-x: hidden;
                    color: Black; height: 100%;">
                    <asp:UpdatePanel ID="panelItem" runat="server">
                        <contenttemplate>
                        <table id="tblItem" runat="server">
                        
                            <tr>
                            <td align="left" style="width:10%;" ><asp:Button ID="btnItemSave" runat="server" Width="150px" ValidationGroup="vgSubmit"
                                        Text="Save Item" OnClick="btnItemSave_Click" /></td>

                                <td align="right" style="width:90%;"  colspan="3">
                                 
                                </td>
                            </tr>
                            <tr>
                            <td colspan="4" align="right">
                               <asp:Label ID="lblAlert" runat="server" ForeColor="Red" Text="PO order quantity and price is shown to supplier."></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnHide" runat="server" Width="150px" Text="Hide" OnClick="btnHide_Click" />&nbsp;&nbsp;
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblAlert1" ForeColor="Red" runat="server" Text="Convert this to Service PO by Hiding."></asp:Label>
                            </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                   <%-- <div  style="width: 100%; background-color: White;">--%>

                              <telerik:RadGrid ID="rgdItems" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                Width="100%" AutoGenerateColumns="False" OnItemDataBound="rgdItems_ItemDataBound"
                                AllowMultiRowSelection="True" PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center"
                                AlternatingItemStyle-BackColor="#CEE3F6">
                                <MasterTableView>
                                    <RowIndicatorColumn Visible="true">
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Srno." DataField="ID"
                                            UniqueName="ID" Visible="true">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="lblID" runat="server"  Value='<%#Eval("ID")%>' />
                                                <asp:Label ID="lblSrno" runat="server"  Text='<%#Eval("Srno")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="40px" HeaderText="Code"
                                            Visible="true" UniqueName="Item_Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItem_Code" Enabled="true" MaxLength="20" EnableViewState="true"
                                                    runat="server" Text='<%# Bind("Item_Code")%>' Style="font-size: x-small; text-align:center" Width="60px"
                                                    Height="15px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAddNewItem" Text="Add new item" BackColor="#0066cc" BorderStyle="None"
                                                    ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle Width="50px"></HeaderStyle>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Item Short Description"
                                            Visible="true" UniqueName="Item_Description">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItem_Description" Enabled="true" MaxLength="250"  EnableViewState="true"
                                                    runat="server" Text='<%# Bind("Item_Short_Desc")%>' Style="font-size: x-small"
                                                    Width="60%" Height="15px" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtItem_Description"
                                                    ErrorMessage="Special character not allowed" ValidationExpression="^[^'@%#]+$">
                                                    </asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtItem_Comments" Enabled="true" TextMode="MultiLine" EnableViewState="true" MaxLength="1000"
                                                    runat="server" Text='<%# Bind("Item_Long_Desc")%>' Style="font-size: x-small"
                                                    Width="60%" Height="30px"></asp:TextBox>

                                            </ItemTemplate>
                                                                 
                                            <HeaderStyle Width="500px"></HeaderStyle>
                                            <ItemStyle Width="500px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="30px" HeaderText="Packing"
                                            Visible="true" UniqueName="Unit">
                                            <ItemTemplate>
                                                <asp:TextBox ID="cmbUnitnPackage" Enabled="true" MaxLength="10" EnableViewState="true"
                                                    runat="server" Text='<%# Bind("Unit")%>' Style="font-size: x-small; text-align:center" Width="40px"
                                                    Height="15px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server"   Text="Total" 
                                                Style="font-size:medium" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="30px"></HeaderStyle>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Quantity"
                                            Visible="true" UniqueName="Request_Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRequest_Qty" Enabled="true" MaxLength="10" EnableViewState="true"
                                                    runat="server" Text='<%# Bind("ORDER_QTY")%>' Style="font-size: x-small; text-align:right" Width="60px"
                                                    Height="15px"></asp:TextBox>
                                            </ItemTemplate>
                                                <FooterTemplate>
                                                <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Total_QTY")%>' Style="font-size: x-small" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px"></HeaderStyle>
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Price" UniqueName="UnitPrice">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUnitPrice" Text='<%#Eval("ORDER_PRICE")%>' MaxLength="10" Style="font-size: x-small;text-align:right"
                                                    Height="15px" runat="server" Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                                <FooterTemplate>
                                                <asp:Label ID="lblUnitPrice" runat="server"  Text='<%#Eval("Total_PRICE")%>' Style="font-size: x-small" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px"></HeaderStyle>
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Discount(%)" UniqueName="Discount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDiscount" Text='<%#Eval("Item_Discount") %>' MaxLength="5" Style="font-size: x-small; text-align:right"
                                                    Height="15px" runat="server" Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                                <FooterTemplate>
                                                <asp:Label ID="lblDiscount" runat="server" Style="font-size: x-small" Width="50px" Text='<%#Eval("Total_Discount")%>'></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="50px"></HeaderStyle>
                                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                                            
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="30px" HeaderText="Action"
                                            Visible="true" UniqueName="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                    CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                    Height="16px"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px"></HeaderStyle>
                                             <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <PopUpSettings ScrollBars="None" />
                                        <PopUpSettings ScrollBars="None" />
                                    <PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /></EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                                                <br />
                                    <%--</div>--%>
                                </td>
                            </tr>
                            <tr>
                             <td align="right" style="width:60%;">
                                   
                                </td>
                                <td align="right" style="width:20%;">
                                    Total Price :
                                </td>
                                <td align="right" style="width:10%;">
                                    <asp:Label ID="lblTotalPrice" Font-Bold="true" runat="server" Text="0.00"></asp:Label>
                                </td>
                                <td align="left" style="width:10%;">
                                    <asp:Label ID="lblTotalpricecur" runat="server" Text="USD"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                              <td align="right" style="width:60%;">
                                   
                                </td>
                                <td align="right" style="width:80%;">
                                    Discount on total Price :&nbsp;
                                    <asp:TextBox ID="txtDiscount" runat="server" MaxLength="255" Width="100px" AutoPostBack="true"
                                        CssClass="txtInput" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                    <asp:DropDownList ID="ddlDiscountType" runat="server" CssClass="txtInput" AutoPostBack="true"
                                        Width="100px" OnSelectedIndexChanged="ddlDiscountType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width:10%;">
                                    <asp:Label ID="lblDiscAmt" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                </td>
                                <td align="left" style="width:10%;">
                                    <asp:Label ID="lblDiscAmtUnit" runat="server" Text="USD"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                  <td align="right" style="width:60%;">
                                   
                                </td>
                                <td align="right" style="width:20%;">
                                    Net Price :
                                </td>
                                <td align="right" style="width:10%;">
                                    <asp:Label ID="txtTotalPrice" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                </td>
                                <td align="left" style="width:10%;">
                                    <asp:Label ID="lblTotalUnit" runat="server" Text="USD"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                              <td align="right" style="width:40%;">
                                   
                                </td>
                                <td align="right" style="width:40%;" >
                                <asp:Label ID="lblCurrentCur" runat="server" Text=""></asp:Label>
                                     to USD Exchange Rate :&nbsp;
                                    <asp:TextBox ID="txtExchangeRate" runat="server" MaxLength="255" Enabled="false"
                                        Width="100px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="right" style="width:10%;">
                                    <asp:Label ID="lblExchangeRate" Font-Bold="true" runat="server" Text="0.00"></asp:Label>
                                </td>
                                <td align="left" style="width:10%;">
                                    <asp:Label ID="lblExchCur" runat="server" Text="USD"></asp:Label>
                                </td>
                            </tr>
                           
                        </table>
                          </contenttemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="vgSubmit" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSubmitapproval" />
            </div>
          </div>
    </center>
    </form>
</body>
</html>
