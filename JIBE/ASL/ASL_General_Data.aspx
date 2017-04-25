<%@ Page Title="Manage Contacts" Language="C#" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="ASL_General_Data.aspx.cs" Inherits="ASL_ASL_General_Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="">
            .page1
        {
            border: 0px solid #cccccc;
            color: Black;
            text-align: left;
            padding: 2px;
            text-align: center;
            width:100%;
            height:100%;
        }
    .page-title1
     {
    border: 1px solid #cccccc;
    height: 20px;
    vertical-align: bottom;
    background: url(/JIBE/Images/bg.png) left -10px repeat-x;
    color: Black;
    text-align: left;
    padding: 2px;
    background-color: #F6CEE3;
    text-align: center;
    font-weight: bold;
     width:100%;
     }
    </style>
    <script language="javascript" type="text/javascript">
        function OpenScreenEvalaution(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var View_Type = "Data";
            var url = 'ASL_Evalution.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID + '&ViewType=' + View_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', 850, 1250, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreenHistory(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var View_Type = "Data";
            var url = 'ASL_Evalution_History.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID + '&ViewType=' + View_Type;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evaluation', url, 'popup', 850, 1250, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen1(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Supplier_Document.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Document', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
        function OpenScreen2(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Supplier_Remarks.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Remarks', url, 'popup', 850, 1200, null, null, false, false, true, null);
        }
        function OpenScreen3(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Supplier_SimilerName.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Similer Name', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
        function OpenScreen4(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Email_Template.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Email Template', url, 'popup', 750, 1080, null, null, false, false, true, null);
        }
        function OpenScreen5(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_CR.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Change Request', url, 'popup', 850, 1180, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }
        function OpenScreen6(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Change_Request_History.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Change Request History', url, 'popup', 700, 1300, null, null, false, false, true, null);
        }
        function OpenScreen7(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Payment_History.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Payment History', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreen8(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_PO_Invoice.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'PO & Invoice', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreen9(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Invoice_WIP.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Invoice WIP', url, 'popup', 750, 1220, null, null, false, false, true, null);
        }
        function OpenScreen10(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_InvoiceStatus.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_InvoiceStatus', 'Invoice Status', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreen11(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_OutStanding.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_OutStanding', 'Supplier OutStandings', url, 'popup', 750, 1200, null, null, false, false, true, null);
        }
        function OpenScreen12(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_Supplier_Statistics.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Supplier_Statistics', 'Supplier Statistics', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
        function OpenTransactionLog(ID, Eval_ID) {

            var Supp_ID = document.getElementById('ctl00_MainContent_txtSupplierCode').value;
            var url = 'ASL_AuditTrail.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_AuditTrail', 'Supplier Audit Trail', url, 'popup', 700, 1250, null, null, false, false, true, null);
        }
    </script>
    <script type="text/javascript">
    /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
        $(document).ready(function () {
            window.parent.$("#Manage_Contacts").css("height", (parseInt($("#pnlGeneral").height()) +20) + "px");
            window.parent.$(".xfCon").css("height", (parseInt($("#pnlGeneral").height()) + 20) + "px").css("top", "50px");
        });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 99%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptM" runat="server">
    </asp:ScriptManager>
    <center>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
       
        <asp:Panel ID="pnlGeneral" runat="server" Visible="true" Width="100%">
            <center>
                <asp:UpdatePanel ID="panel1" runat="server">
                    <ContentTemplate>
                        <center>
                          
                         <div id="Div1" style="height: 100%; overflow: Auto; border: 1px solid #cccccc;">
                               <div id="page-title" class="page-title1">
                              
                                Manage Contacts</div>
                            <table width="100%" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td colspan="3" align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: #FF0000;">
                                    </td>
                                    <td colspan="2" style="text-align: Right;">
                                        <asp:DropDownList ID="ddlSupplier" runat="server" Visible="false" CssClass="txtInput"
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; width: 50%;">
                                        Created By :<asp:Label ID="lblCreatedBY" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        Created Date :<asp:Label ID="lbldate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        Updated By :<asp:Label ID="lblUpdatedby" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        Updated Date :<asp:Label ID="lblUpdatedDate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td colspan="3">
                                    <div id="page1" class="page1">
                                        <table width="100%">
                                         <hr />
                                            <tr>
                                                <td valign="top" style="width: 34%" align="left">
                                                    <table width="100%">
                                                        <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                Supplier Code :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:Label ID="lblSupplierCode" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                <asp:Label ID="lblSupplierType" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Status :&nbsp;&nbsp;
                                                                <asp:Label ID="lblstatus" runat="server" Text="Unregistered"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                Client Supplier Code :
                                                                
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                          
                                                               <asp:TextBox ID="txtShipSmart_Supplier_Code" runat="server" MaxLength="100" Width="300px"
                                                                    CssClass="txtInput"></asp:TextBox> 
                                                                
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                 Client Supplier Sub Code :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                              
                                                                 <asp:TextBox ID="txtSupplier_Short_Code" runat="server" MaxLength="100" Width="300px"
                                                                    CssClass="txtInput"></asp:TextBox> 
                                                            </td>
                                                        </tr>
                                                       <%-- <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                
                                                            </td>
                                                          <td align="left" style="width: 60%">
                                                                <
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Company Registered Name :<asp:Label ID="lbl1" runat="server" ForeColor="#FF0000"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                          <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtCompanyResgName" runat="server" MaxLength="500" Width="300px"
                                                                    CssClass="txtInput"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="None" ErrorMessage="Company registered name is mandatory field."
                                                                    ControlToValidate="txtCompanyResgName" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Short Name :<asp:Label ID="Label4" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtshortName" runat="server" MaxLength="255" Width="150px" CssClass="txtInput"></asp:TextBox>
                                                                <asp:DropDownList ID="ddlgroup" runat="server" CssClass="txtInput" Width="145px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="ReqtxtshortName" runat="server" Display="None" ErrorMessage="Short name is mandatory field."
                                                                    ControlToValidate="txtshortName" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <asp:HiddenField ID="ShipSmart_Supplier_Code" runat="server" Value="0" />
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Type :<asp:Label ID="Label10" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="txtInput" Width="300px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="ReqType" runat="server" Display="None" InitialValue="0"
                                                                    ErrorMessage="Type is mandatory field." ControlToValidate="ddlType" ValidationGroup="vgSubmit"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                Tax Acc. Number :
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtTaxAccNumber" runat="server" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Counterparty Type :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:DropDownList ID="ddlSubType" runat="server" CssClass="txtInput" Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                Base Currency :<asp:Label ID="Label9" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="txtInput" Width="300px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="req1" runat="server" Display="None" InitialValue="0"
                                                                    ErrorMessage="Currency is mandatory field." ControlToValidate="ddlCurrency" ValidationGroup="vgSubmit"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Ownership :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:DropDownList ID="ddlownerShip" runat="server" CssClass="txtInput" Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                Biz Incorporation :
                                                            </td>
                                                          <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtBizCorporation" runat="server" MaxLength="255" Width="100px"
                                                                    CssClass="txtInput"></asp:TextBox><font color="red">(Please enter the year of incorporation)</font>
                                                                <asp:RegularExpressionValidator ID="RegBiz" runat="server" ErrorMessage="Biz Incorporation is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtBizCorporation"
                                                                    ForeColor="Red" ValidationExpression="^[0-9]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                Address :<asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtSuppAddress" TextMode="MultiLine" MaxLength="1000" Height="40px"
                                                                    Width="300px" runat="server" CssClass="txtInput"> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                                    ErrorMessage="Address is mandatory field." ControlToValidate="txtSuppAddress"
                                                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Country :<asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txtInput" Width="300px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                                    InitialValue="0" ErrorMessage="Country is mandatory field." ControlToValidate="ddlCountry"
                                                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                City :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtCity" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Company Email :<asp:Label ID="Label3" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtCompanyEmail" runat="server" MaxLength="250" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqEmail" runat="server" Display="None" ErrorMessage="Company Email is mandatory field."
                                                                    ControlToValidate="txtCompanyEmail" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegEmailID" Display="None" runat="server" ValidationGroup="vgSubmit"
                                                                    ErrorMessage="Company EmailID is not valid" ControlToValidate="txtCompanyEmail"
                                                                    ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Company Phone :
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtTelephone" runat="server" MaxLength="50" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <%--  <asp:RequiredFieldValidator ID="reqTelephone" runat="server" Display="None" ErrorMessage="Telephone is mandatory field."
                                                            ControlToValidate="txtTelephone" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:Label ID="Label4" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>--%>
                                                                <asp:RegularExpressionValidator ID="RegPhone" runat="server" ErrorMessage="Company Phone Number is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtTelephone" ForeColor="Red"
                                                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Company Fax :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtFaxNumber" runat="server" MaxLength="50" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="ReqFaxNumber" runat="server" Display="None" ErrorMessage="Company Fax Number is mandatory field."
                                                            ControlToValidate="txtFaxNumber" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:Label ID="Label5" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>--%>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Company Fax Number is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtFaxNumber" ForeColor="Red"
                                                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                PIC Name :<asp:Label ID="Label6" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtNamePIC1" runat="server" MaxLength="225" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqPicName" runat="server" Display="None" ErrorMessage="PIC Name is mandatory field."
                                                                    ControlToValidate="txtNamePIC1" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Email :<asp:Label ID="Label7" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtEmailPIC1" runat="server" MaxLength="225" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="ReqPICEmail" runat="server" Display="None" ErrorMessage="PIC(1) Email is mandatory field."
                                                                    ControlToValidate="txtEmailPIC1" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegPICEmail" Display="None" runat="server" ValidationGroup="vgSubmit"
                                                                    ErrorMessage="PIC(1) EmailID is not Valid" ControlToValidate="txtEmailPIC1" ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Phone :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtPhonePIC1" runat="server" MaxLength="50" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <%-- <asp:RequiredFieldValidator ID="reqPICPhone" runat="server" Display="None" ErrorMessage="PIC(1) Phone Number is mandatory field."
                                                            ControlToValidate="txtPhonePIC1" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                <asp:RegularExpressionValidator ID="RegPICPhone" runat="server" ErrorMessage="PIC(1) Phone Number is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPhonePIC1" ForeColor="Red"
                                                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                2nd PIC Name :
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtNamePIC2" runat="server" MaxLength="225" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Email :
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtEmailPIC2" runat="server" MaxLength="225" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="None" runat="server"
                                                                    ValidationGroup="vgSubmit" ErrorMessage="PIC(2) EmailID is not Valid" ControlToValidate="txtEmailPIC2"
                                                                   ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Phone :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtPhonePIC2" runat="server" MaxLength="50" CssClass="txtInput"
                                                                    Width="300px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegPicPhone1" runat="server" ErrorMessage="PIC(2) Phone Number is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPhonePIC2" ForeColor="Red"
                                                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Terms :
                                                            </td>
                                                           <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtTerms" runat="server" CssClass="txtInput" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top" style="width: 40%">
                                                     <table width="100%">
                                                        <tr>
                                                         <td style="text-align: Right; width: 40%">
                                                                Enable Invoice Status :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:RadioButtonList ID="rdbInvoiceStatus" CssClass="txtInput" Width="200px" runat="server"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Direct Invoice Upload :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:RadioButtonList ID="rdbdirectinvoice" CssClass="txtInput" Width="200px" runat="server"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Payment History View :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:RadioButtonList ID="rdbPaymentHistory" CssClass="txtInput" Width="200px" runat="server"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Auto Send PO :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:RadioButtonList ID="rdbSendPo" CssClass="txtInput" Width="200px" runat="server"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Supplier Scope :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <biv>
                                                                    <asp:DropDownList ID="ddlScope" runat="server" Width="300px" CssClass="txtInput">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:Button ID="btnScopeAdd" runat="server" Text="Add Scope" OnClick="btnAdd_Click" />
                                                                    <asp:Button ID="btnScopeRemove" runat="server" Text="Remove Scope" Visible="false"
                                                                        OnClick="btnRemove_Click" />
                                                                    <br />
                                             
                                                                            <div style="float: left; text-align: left; width: 300px; height: 60px; overflow-x: hidden;
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
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Supplier Description :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="txtSupplierDesc" TextMode="MultiLine" MaxLength="1000" Height="40px"
                                                                    Width="300px" runat="server" CssClass="txtInput"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Port of Operation / Supply:
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <asp:DropDownList ID="ddlPort" runat="server" Width="300px" CssClass="txtInput">
                                                                            </asp:DropDownList>
                                                                            <br />
                                                                            <asp:Button ID="btnPortAdd" runat="server" Text="Add Port" OnClick="btnPortAdd_Click" />
                                                                            <asp:Button ID="btnPortRemove" runat="server" Text="Remove Port" Visible="false"
                                                                                OnClick="btnPortRemove_Click" />
                                                                            <br />
                                                                            <div style="float: left; text-align: left; width: 300px; height: 60px; overflow-x: hidden;
                                                                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                                                background-color: #ffffff;">
                                                                                <asp:CheckBoxList ID="chkPort" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Payment Instructions :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="Payment_Instructions" TextMode="MultiLine" MaxLength="1000" Height="40px"
                                                                    Width="300px" runat="server" CssClass="txtInput"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Payment Notifications (Email address) :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:TextBox ID="Payment_Notifications" TextMode="MultiLine" MaxLength="1000" Height="40px"
                                                                    Width="300px" runat="server" CssClass="txtInput"> </asp:TextBox>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Payment Priority :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:RadioButtonList ID="rdbPaymentPriority" CssClass="txtInput" Width="300px" runat="server"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                                                    <asp:ListItem Value="Immediate">Immediate</asp:ListItem>
                                                                    <%--  <asp:ListItem Value="None">None</asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td style="text-align: Right; width: 40%">
                                                                Payment Interval :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:DropDownList ID="ddlPaymentInterval" CssClass="txtInput" Width="300px" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 40%">
                                                                Terms :
                                                            </td>
                                                            <td align="left" style="width: 60%">
                                                                <asp:DropDownList ID="ddlPaymentTerms" CssClass="txtInput" Width="300px" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align: Right; width: 40%">
                                                                Last Payment on :
                                                            </td>
                                                             <td align="left" style="width: 60%">
                                                                <asp:Label ID="lblLastPayment" Width="300px" CssClass="txtInput" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                       
                                                       
                                                    </table>
                                                </td>
                                                <td valign="top" align="right" style="width: 26%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="text-align: Right; width: 50%">
                                                                Tax Rate % :
                                                            </td>
                                                            <td align="left" style="text-align: Right; width: 50%">
                                                                <asp:TextBox ID="txtTaxRate" runat="server" Text="0.00" MaxLength="10" CssClass="txtInput"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegTaxRate" runat="server" ErrorMessage="Tax Rate is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtTaxRate" ForeColor="Red"
                                                                    ValidationExpression="^[0-9.]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 50%">
                                                                Company Reg. No. :
                                                            </td>
                                                            <td align="left" style="text-align: Right; width: 50%">
                                                                <asp:TextBox ID="txtCompany_Reg_No" runat="server" Text="0.00" MaxLength="10" CssClass="txtInput"
                                                                    Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Company Reg. No. is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtCompany_Reg_No" ForeColor="Red"
                                                                    ValidationExpression="^[0-9.]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 50%">
                                                                GST Reg. No :
                                                            </td>
                                                            <td align="left" style="text-align: Right; width: 50%">
                                                                <asp:TextBox ID="txtGST_Registration_No" runat="server" Text="0.00" MaxLength="10"
                                                                    CssClass="txtInput" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="GST Reg. No is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtGST_Registration_No" ForeColor="Red"
                                                                    ValidationExpression="^[0-9.]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 50%">
                                                                Withholding Tax Rate %:
                                                            </td>
                                                            <td align="left" style="text-align: Right; width: 50%">
                                                                <asp:TextBox ID="txtWithholding_Tax_Rate" runat="server" Text="0.00" MaxLength="10"
                                                                    CssClass="txtInput" Width="200px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Withholding Tax Rate is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtWithholding_Tax_Rate" ForeColor="Red"
                                                                    ValidationExpression="^[0-9.]+$"> </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right; width: 50%">
                                                                Supplier Properties :
                                                            </td>
                                                            <td align="left" style="text-align: Right; width: 50%">
                                                               <div style="float: left; text-align: left; Width:200px; height: 60px; overflow-x: hidden;
                                                                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                                                background-color: #ffffff;">
                                                                                <asp:CheckBoxList ID="gvProperties" RepeatLayout="Flow" RepeatDirection="Vertical" runat="server">
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                                               
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="2" style="height: 10px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: Right;">
                                                            </td>
                                                            <td align="left">
                                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                                    <asp:Label ID="lblAutoPayment" Width="200px" runat="server" Text="Auto Payment Setup"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="2" align="center">
                                                                <div id="divAutoPayment" runat="server">
                                                                    <table cellpadding="2" cellspacing="2">
                                                                        <tr>
                                                                            <td align="right">
                                                                                Source :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblSource" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Paymemt Mode :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblPaymentMode" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Payment Currency :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblPayment" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Receiving Account :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblReceivingAmt" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Bank Code :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblBankCode" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Branch Code :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblBranch" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Beneficiary :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblBene" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                SWIFT :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblSwift" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Destination ABA(Local US Bank) :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblDesti" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Intermediary ABA :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblABA" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                Bank State (if US) :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Label ID="lblBankState" runat="server" CssClass="txtInput"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td style="display: none;">
                                        <asp:HiddenField ID="hdnRetSupplierCode" runat="server" />
                                    </td>
                                    <td style="display: none;">
                                        <asp:TextBox ID="txtPassString" Width="1px" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtSupplierCode" Width="1px" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <%--<asp:Button ID="Button1" runat="server" Width="150px" OnClientClick="return validation();"
                            Text="Save Draft" OnClick="btnSave_Click" />&nbsp;--%>
                                        <asp:Button ID="btnSave" runat="server" Width="150px" ValidationGroup="vgSubmit"
                                            Text="Save Draft" OnClick="btnSave_Click" />&nbsp;
                                        <asp:Button ID="btnSubmit" runat="server" Width="200px" ValidationGroup="vgSubmit"
                                            Text="Submit & Notify Supplier" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnUnverify" Visible="false" runat="server" ForeColor="Red" Text="Unverify Registered Data & Notify Supplier"
                                            OnClick="btnUnverify_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="vgSubmit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 10px;">
                                    </td>
                                </tr>
                            </table>
                          
                        </center>
                        <%--<div runat="server" id="dvbutton" visible="false" align="left" style="border: 1px solid gray;
                            margin-top: 5px; margin-bottom: 5px; margin-left: 5px; margin-right: 5px;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnRegisteredData" runat="server" Text="Registered Data" OnClick="btnRegisteredData_Click" />&nbsp;
                                        <asp:Button ID="btnEvaluation" runat="server" Text="Evaluation History" OnClientClick='OpenScreenEvalaution(null,10);return false;' />&nbsp;
                                        <asp:Button ID="btnLastEvaluation" runat="server" Text="Last Evaluation" OnClientClick='OpenScreenHistory(null,11);return false;' />&nbsp;
                                        <asp:Button ID="btnDocument" runat="server" Text="Supplier Documents" OnClientClick='OpenScreen1(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnRemarks" runat="server" Text="Supplier Remarks" OnClientClick='OpenScreen2(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnSNames" runat="server" Text="Similar Names" OnClientClick='OpenScreen3(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnTemplate" runat="server" Text="Email Template" OnClientClick='OpenScreen4(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnChangeRequest" runat="server" Text="Change Request" OnClientClick='OpenScreen5(null,null);return false;' />&nbsp;<br />
                                        <br />
                                        <asp:Button ID="btnCRHistory" runat="server" Text="Change Request History" OnClientClick='OpenScreen6(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnInvoiceStatus" runat="server" Text="Invoice Status" OnClick="btnInvoiceStatus_Click" />&nbsp;
                                        <asp:Button ID="btnPayment" runat="server" Text="Payment History" OnClientClick='OpenScreen7(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnPOInvoice" runat="server" Text="PO & Invoice" OnClientClick='OpenScreen8(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnInvoiceWIP" runat="server" Text="Invoice WIP" OnClientClick='OpenScreen9(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnOutStandings" runat="server" Text="OutStandings" OnClientClick='OpenScreen11(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnStatistics" runat="server" Text="Statistics" OnClientClick='OpenScreen12(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnTransactionLog" runat="server" Text="Transaction Log" OnClientClick='OpenTransactionLog(null,null);return false;' />&nbsp;
                                        <%--<asp:Button ID="btnUnverify" Visible="false" runat="server" ForeColor="Red" Text="Unverify Registered Data & Notify Supplier"
                                    OnClick="btnUnverify_Click" />
                                 <asp:Button ID="Button15" runat="server" Text="Evaluation History" />--%>
                                        <%--<cc1:TabContainer ID="TabSCM" runat="server" ActiveTabIndex="0" OnActiveTabChanged="TabSCM_ActiveTabChanged"
                                    AutoPostBack="true">OnClientClick='OpenScreen10(null,null);return false;'
                                    <cc1:TabPanel runat="server" HeaderText="Evaluation History" ID="Eval" TabIndex="0">
                                    </cc1:TabPanel>
                                     <cc1:TabPanel runat="server" HeaderText="Last Evaluation" ID="temp" TabIndex="1">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Supplier Document" ID="TabPanel1" TabIndex="2">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Supplier Remarks" ID="TabPanel2" TabIndex="3">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Similar Names" ID="TabPanel3" TabIndex="4">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Email Template" ID="TabPanel4" TabIndex="5">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Change Request" ID="TabPanel5" TabIndex="6">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Change Request History" ID="TabPanel6" TabIndex="7">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Payment History" ID="TabPanel7" TabIndex="8">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="PO & Invoice" ID="TabPanel8" TabIndex="9">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Invoice WIP" ID="TabPanel9" TabIndex="10">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="Invoice Status" ID="TabPanel10" TabIndex="11">
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="OutStandings" ID="TabPanel11" TabIndex="12">
                                    </cc1:TabPanel>
                                     <cc1:TabPanel runat="server" HeaderText="Statistics" ID="TabPanel12" TabIndex="13">
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
                        <div style="display: none;">
                            <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />
                        </div>
                        <%-- <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                    text-align: left; font-size: 12px; color: Black; width: 20%; height: 400px;">
                    
                </div>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>
        </asp:Panel>
       
    </center>
    </form>
    </td></tr></table>
</body>
</html>
