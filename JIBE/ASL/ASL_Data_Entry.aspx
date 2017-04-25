<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site_NoMenu.master"
    CodeFile="ASL_Data_Entry.aspx.cs" Inherits="ASL_Data_Entry" Title="Supplier Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
        P.pagebreakhere
        {
            page-break-before: always;
        }
        .tr-Header
        {
            height: 38px;
            color: Black;
            padding: 0px;
            background-color: #ffffff;
            font-size: 13;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function DocOpen(filename) {

            var filepath = "../uploads/ASL/";

            window.open(filepath + filename);
        }
        function DocOpen1(filename) {

            var filepath = "../uploads/ASL/";

            window.open(filepath + filename);
        }
        function validationOnApprove() {

            if (document.getElementById("ctl00_MainContent_ddlStatus").value == "0") {

                alert("Please select status to assigne for this supplier");
                document.getElementById("ctl00_MainContent_ddlStatus").focus();
                return false;
            }

            return true;
        }
        function printDiv(divID) {
            debugger;
            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              divElements + "</body>";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;


        }

   

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
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
    <asp:UpdatePanel ID="panel11" runat="server">
        <ContentTemplate>
            <center>
                <div id="Div2" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
                    color: Black; height: 100%;">
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr>
                            <td colspan="6" align="center">
                                <div id="page-title" class="page-title">
                                    Supplier Data Form
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                SC
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                :
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtSupplierCode" runat="server" Enabled="false" MaxLength="255"
                                    Width="200px" CssClass="txtInput"></asp:TextBox>
                            </td>
                            <td align="right" colspan="2">
                                <strong><a href="ASL_POLog_HelpFile.pdf" target="_blank">How to submit Supplier Data
                                    Form ? </a></strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Company Registered Name :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCompanyResgName" runat="server" MaxLength="500" Width="400px"
                                    CssClass="txtInput"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqCRegname" runat="server" Display="None" ErrorMessage="Company Registered Name is mandatory field."
                                    ControlToValidate="txtCompanyResgName" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Tax Acc. Number :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                            </td>
                            <td align="left" style="color: #FF0000; width: 1%">
                                <asp:TextBox ID="txtTaxAccNumber" runat="server" CssClass="txtInput" MaxLength="2000"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Company Add. :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSuppAddress" TextMode="MultiLine" MaxLength="1000" Height="40px"
                                    Width="400px" runat="server" CssClass="txtInput"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqAdd" runat="server" Display="None" ErrorMessage="Company Add. is mandatory field."
                                    ControlToValidate="txtSuppAddress" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                State/Province :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtState" MaxLength="1000" Height="20px" Width="400px" runat="server"
                                    CssClass="txtInput"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Country :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txtInput" Width="400px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ReqCountry" runat="server" Display="None" InitialValue="0"
                                    ErrorMessage="Country is mandatory field." ControlToValidate="ddlCountry" ValidationGroup="vgSubmit"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Postal Code :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="10" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegPostalCode" runat="server" ErrorMessage="Postal Code is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPostalCode" ForeColor="Red"
                                    ValidationExpression="^[ 0-9A-Za-z,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Telephone :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTelephone1" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegTelephone" runat="server" ErrorMessage="Telephone Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtTelephone1" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Telephone(2) :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTelephone2" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegTelephone2" runat="server" ErrorMessage="Telephone(2) Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtTelephone2" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                AOH Number :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAOHNumber1" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegAOHno" runat="server" ErrorMessage="AOH Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtAOHNumber1" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                AOH Number(2) :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAOHNumber2" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegAohno1" runat="server" ErrorMessage="AOH Number(2) is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtAOHNumber2" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Fax Number :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFaxNumber1" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegFax1" runat="server" ErrorMessage="Fax Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtFaxNumber1" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Fax Number(2) :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFaxNumber2" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegFax2" runat="server" ErrorMessage="Fax Number(2) is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtFaxNumber2" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Company Email :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCompanyEmail1" runat="server" MaxLength="250" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqEmail" runat="server" Display="None" ErrorMessage="Company Email is mandatory field."
                                    ControlToValidate="txtCompanyEmail1" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegCompEmail1" Display="None" runat="server"
                                    ValidationGroup="vgSubmit" ErrorMessage="Company EmailID is not valid" ControlToValidate="txtCompanyEmail1"
                                    ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Company Email(2) :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCompanyEmail2" runat="server" MaxLength="250" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegCompEmail2" Display="None" runat="server"
                                    ValidationGroup="vgSubmit" ErrorMessage="Company EmailID(2) is not valid" ControlToValidate="txtCompanyEmail2"
                                    ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Company Web Site :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCompanyWebSite" runat="server" MaxLength="225" Width="400px"
                                    CssClass="txtInput"></asp:TextBox>
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Additional Company Information
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Company Reg No.:
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCompanyRegNo" runat="server" MaxLength="100" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                            </td>
                            <td align="right">
                                GST / Tax Reg No.:
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtGstTaxRegNo" runat="server" MaxLength="100" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ISO Certification :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtISOCertification" runat="server" MaxLength="100" Width="400px"
                                    CssClass="txtInput"></asp:TextBox>
                            </td>
                            <td align="right">
                                Billing Currency :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBillingCurrency" runat="server" CssClass="txtInput" Width="404px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="None"
                                    ErrorMessage="Billing Currency is mandatory field." InitialValue="0" ControlToValidate="ddlBillingCurrency"
                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Scope of Supply:
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtScope" runat="server" MaxLength="1000" TextMode="MultiLine" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                            </td>
                            <td align="right">
                                Additional Service:
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAddService" runat="server" MaxLength="1000" TextMode="MultiLine"
                                    CssClass="txtInput" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Main PIC Contact Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="vertical-align: top">
                                Name :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left" style="vertical-align: top">
                                <asp:DropDownList ID="ddlTitle1" runat="server" CssClass="txtInput" Width="50px">
                                    <asp:ListItem Value="1" Text="Mr." Selected="True" />
                                    <asp:ListItem Value="2" Text="Ms." />
                                    <asp:ListItem Value="3" Text="Mrs." />
                                    <asp:ListItem Value="4" Text="Dr." />
                                    <asp:ListItem Value="5" Text="Mdm." />
                                </asp:DropDownList>
                                <asp:TextBox ID="txtNamePIC1" runat="server" MaxLength="225" CssClass="txtInput"
                                    Width="345px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqName" runat="server" Display="None" ValidationGroup="vgSubmit"
                                    ControlToValidate="txtNamePIC1" ErrorMessage="PIC(1) Name is mandatory field."></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" style="vertical-align: top">
                                Designation :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDesignation1" runat="server" MaxLength="100" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqDesg" ValidationGroup="vgSubmit" runat="server"
                                    Display="None" ControlToValidate="txtDesignation1" ErrorMessage="PIC(1) Designation is mandatory field."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                TelePhone :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPhonePIC1" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqPICPhone" runat="server" Display="None" ValidationGroup="vgSubmit"
                                    ControlToValidate="txtPhonePIC1" ErrorMessage="PIC(1) TelePhone is mandatory field."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegPICPh" runat="server" ErrorMessage="PIC(1) TelePhone Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPhonePIC1" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Mobile :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="50" CssClass="txtInput" Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegPICMobile" runat="server" ErrorMessage="PIC(1) Mobile Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtMobileNo" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Email :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEmailPIC1" runat="server" MaxLength="225" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqPICEmail" runat="server" Display="None" ValidationGroup="vgSubmit"
                                    ControlToValidate="txtEmailPIC1" ErrorMessage="PIC(1) Email is mandatory field."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegEmail1" Display="None" runat="server" ValidationGroup="vgSubmit"
                                    ErrorMessage="PIC(1) EmailID is not Valid" ControlToValidate="txtEmailPIC1" ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Skype Address :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSkypeAdd" runat="server" MaxLength="50" CssClass="txtInput" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Alternative PIC Contact Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Name :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left" style="vertical-align: top">
                                <asp:DropDownList ID="ddlTitle2" runat="server" CssClass="txtInput" Width="50px">
                                    <asp:ListItem Value="1" Text="Mr." Selected="True" />
                                    <asp:ListItem Value="2" Text="Ms." />
                                    <asp:ListItem Value="3" Text="Mrs." />
                                    <asp:ListItem Value="4" Text="Dr." />
                                    <asp:ListItem Value="5" Text="Mdm." />
                                </asp:DropDownList>
                                <asp:TextBox ID="txtNamePIC2" runat="server" MaxLength="225" CssClass="txtInput"
                                    Width="346px"></asp:TextBox>
                            </td>
                            <td align="right" style="vertical-align: top">
                                Designation :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDesignation2" runat="server" MaxLength="100" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                TelePhone :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPhonePIC2" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegPICPhone2" runat="server" ErrorMessage="PIC(2) TelePhone Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPhonePIC2" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Mobile :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMobileNo2" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegMobile2" runat="server" ErrorMessage="PIC(2) Mobile Number is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtMobileNo2" ForeColor="Red"
                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Email :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEmailPIC2" runat="server" MaxLength="225" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegEmail2" Display="None" runat="server" ValidationGroup="vgSubmit"
                                    ErrorMessage="PIC(2) EmailID is not Valid" ControlToValidate="txtEmailPIC2" ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                Skype Address :
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSkypeAdd2" runat="server" MaxLength="50" CssClass="txtInput"
                                    Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Banking & Remittance Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Bank Name :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankName" runat="server" CssClass="txtInput" MaxLength="225"
                                    Width="400px"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqBankName" runat="server" ControlToValidate="txtBankName"
                                    Display="None" ErrorMessage="Bank Name is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Beneficiary Name :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBeneficiaryName" runat="server" CssClass="txtInput" MaxLength="225"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqBname" runat="server" ControlToValidate="txtBeneficiaryName"
                                    Display="None" ErrorMessage="Beneficiary Name is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Bank Address :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankAddress" runat="server" CssClass="txtInput" Height="35px"
                                    MaxLength="500" TextMode="MultiLine" Width="400px"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqBankAdd" runat="server" ControlToValidate="txtBankAddress"
                                    Display="None" ErrorMessage="Bank Address is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Beneficiary Address :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBeneficiaryAddress" runat="server" CssClass="txtInput" Height="35px"
                                    MaxLength="500" TextMode="MultiLine" Width="400px"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBeneficiaryAddress"
                                    Display="None" ErrorMessage="Beneficiary Address is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Bank Code :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankCode" runat="server" CssClass="txtInput" MaxLength="50" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBankCode"
                                    Display="None" ErrorMessage="Bank Code is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Beneficiary Account :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBeneficiaryAccount" runat="server" CssClass="txtInput" MaxLength="225"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqBAccount" runat="server" ControlToValidate="txtBeneficiaryAccount"
                                    Display="None" ErrorMessage="Beneficiary Account is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Branch Code :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="txtInput" MaxLength="50"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqBranchCode" runat="server" ControlToValidate="txtBranchCode"
                                    Display="None" ErrorMessage="Branch Code is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Account Currency :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAccountCurrency" runat="server" CssClass="txtInput" Width="400px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ReqAccCur" runat="server" ControlToValidate="ddlAccountCurrency"
                                    Display="None" ErrorMessage="Account Currency is mandatory field." InitialValue="0"
                                    ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                SWIFT Code :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSwiftCode" runat="server" CssClass="txtInput" MaxLength="50"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqSwiftCode" runat="server" ControlToValidate="txtSwiftCode"
                                    Display="None" ErrorMessage="SWIFT Code is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Notify payments to :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNotifypayment" runat="server" CssClass="txtInput" MaxLength="50"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqNotify" runat="server" ControlToValidate="txtNotifypayment"
                                    Display="None" ErrorMessage="Notify payments to is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                IBAN Code :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIBANCode" runat="server" CssClass="txtInput" MaxLength="50" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqIBAN" runat="server" ControlToValidate="txtIBANCode"
                                    Display="None" ErrorMessage="IBAN Code is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Notification Email :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNotificationEmail" runat="server" CssClass="txtInput" MaxLength="225"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqNoteEmail" runat="server" ControlToValidate="txtNotificationEmail"
                                    Display="None" ErrorMessage="Notification Email is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regNoteEmail" Display="None" runat="server" ValidationGroup="vgSubmit"
                                    ErrorMessage="Notification Email is not Valid" ControlToValidate="txtNotificationEmail"
                                    ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                                    ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Other Bank Information :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                            </td>
                            <td align="left" colspan="4" style="color: #FF0000; width: 1%">
                                <asp:TextBox ID="txtBankInfo" runat="server" CssClass="txtInput" Height="100px" MaxLength="2000"
                                    TextMode="MultiLine" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div style="text-align: center; height: 40px; vertical-align: middle;">
                                    The above banking and remittance information must be verified and signed by any
                                    of the following authorized personnel or personnel with appointment of equivalent
                                    authority.<br />
                                    <div style="text-align: center; font-weight: 700;">
                                        Managing Director, Directors, Financial Controllers, Head of Accounting/Finance
                                        Dept.
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Upload copy of the endorsed Supplier data sheet
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="left" valign="top" colspan="6" style="width: 70%">
                                                    <asp:FileUpload ID="FileUploader" runat="server" Enabled="false" Style="width: 50%;
                                                        height: 18px; background-color: #F2F2F2; border: 1px solid #cccccc; font-size: 12px;
                                                        cursor: pointer" />
                                                    <asp:Button ID="btnUpload" runat="server" Height="20px" OnClick="btnUpload_Click"
                                                        Style="height: 18px; border: 1px solid #cccccc; font-size: 12px; cursor: pointer"
                                                        Text="Add Attachment" Width="100px" />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <div>
                                                        <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                                            Width="100%">
                                                            <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                                            <RowStyle CssClass="PMSGridRowStyle-css" />
                                                            <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                                            <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Attachments">
                                                                    <HeaderTemplate>
                                                                        Attached File(s)
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfileName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'
                                                                            Visible="true"></asp:Label>
                                                                        <asp:Label ID="Label1" runat="server" Text="has been successfully uploaded." Visible="true"></asp:Label>
                                                                        <asp:Label ID="lblfilePath" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblFileId" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img style="border: 0; width: 25px; height: 25px" alt="Open in new window" onclick="DocOpen('<%#Eval("FILEPATH")%>')"
                                                                            src="../Images/Download-icon.png" title="Click to View file" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:ImageButton ID="ImgAttDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH")  %>'
                                                                            ForeColor="Black" Height="20px" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                            ImageUrl="~/Images/Delete.png" OnCommand="ImgAttDelete_Click" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="50px"
                                                                        Wrap="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Upload copy of Company Registration document</div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="left" valign="top" colspan="6" style="width: 70%">
                                                    <asp:FileUpload ID="File_CompanyUpload" runat="server" Enabled="false" Style="width: 50%;
                                                        height: 18px; background-color: #F2F2F2; border: 1px solid #cccccc; font-size: 12px;
                                                        cursor: pointer" />
                                                    &nbsp;<asp:Button ID="btnCompanyUpload" runat="server" Height="20px" OnClick="btnCompanyUpload_Click"
                                                        Style="height: 18px; border: 1px solid #cccccc; font-size: 12px; cursor: pointer"
                                                        Text="Add Attachment" Width="100px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <div>
                                                        <asp:GridView ID="gvCompanyAttachment" runat="server" AutoGenerateColumns="False"
                                                            GridLines="Both" Width="100%">
                                                            <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                                            <RowStyle CssClass="PMSGridRowStyle-css" />
                                                            <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                                            <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Attachments">
                                                                    <HeaderTemplate>
                                                                        Attached File(s)
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfileName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'
                                                                            Visible="true"></asp:Label>
                                                                        <asp:Label ID="Label1" runat="server" Text="has been successfully uploaded." Visible="true"></asp:Label>
                                                                        <asp:Label ID="lblfilePath" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblFileId" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img style="border: 0; width: 25px; height: 25px" alt="Open in new window" onclick="DocOpen1('<%#Eval("FILEPATH")%>')"
                                                                            src="../Images/Download-icon.png" title="Click to View file" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:ImageButton ID="ImgCompanyAttDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH")  %>'
                                                                            ForeColor="Black" Height="20px" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                            ImageUrl="~/Images/Delete.png" OnCommand="ImgCompanyAttDelete_Click" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="50px"
                                                                        Wrap="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnCompanyUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Verified by :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtVerifiedName" runat="server" CssClass="txtInput" MaxLength="50"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtVerifiedName"
                                    Display="None" ErrorMessage="Verified by is mandatory field." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Designation :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="txtInput" Width="400px">
                                    <asp:ListItem Value="Managing Director">Managing Director</asp:ListItem>
                                    <asp:ListItem Value="Director">Director</asp:ListItem>
                                    <asp:ListItem Value="Financial Controller">Financial Controller</asp:ListItem>
                                    <asp:ListItem Value="Head of Accounts Dept">Head of Accounts Dept</asp:ListItem>
                                    <asp:ListItem Value="Head of Finance Dept">Head of Finance Dept</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Company Stamp :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                &nbsp;
                            </td>
                            <td rowspan="2">
                                <div style="border: 1px solid #cccccc; width: 400px; height: 40px; background-color: #DDDDDD;">
                                </div>
                            </td>
                            <td align="right">
                                Signature :
                            </td>
                            <td align="right" style="color: #FF0000; width: 1%">
                                &nbsp;
                            </td>
                            <td align="left" rowspan="2">
                                <div style="border: 1px solid #cccccc; width: 400px; height: 40px; background-color: #DDDDDD;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div style="text-align: center; color: Red; height: 60px">
                                    Please do a printout of the above data sheet, and obtain the required authorized
                                    signatory, endorsed with your company stamp; and upload the scan copy using the
                                    below file upload section.<br />
                                    You are also required to upload a scan copy of your COMPANY REGISTRATION document
                                    using the below file upload section.<br />
                                    Alternatively, you can send both documents together with your next invoice to us
                                    for our record keeping.<br />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
            <div>
                <center>
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr>
                            <td colspan="6" align="center" style="background-color: #DDDDDD">
                                <asp:Button ID="btnSaveDraft" runat="server" Text="Save Draft" Width="120px" ValidationGroup="12"
                                    CausesValidation="false" OnClick="btnSaveDraft_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnFinalize" runat="server" Text="Finalize & Lock" Width="150px"
                                    OnClick="btnFinalize_Click" ValidationGroup="vgSubmit" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnPrint" runat="server" Text="Print" Enabled="false" CausesValidation="false"
                                    Width="120px" OnClientClick="javascript:printDiv('printablediv')" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnRework" runat="server" Text="Rework" Visible="false" Width="120px"
                                    ValidationGroup="vgSubmit" />
                            </td>
                        </tr>
                    </table>
                </center>
                <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ForeColor="Red" ShowSummary="false" ValidationGroup="vgSubmit" />
            </div>
            <div id="printablediv" style="border: 1px solid #cccccc; display: none; font-family: Tahoma;
                font-size: 15px; color: Black; height: 100%;">
                <center>
                    <table border="2" width="100%" cellpadding="0" cellspacing="0">
                        <tr style="font-size: 20px; color: Black; height: 40px">
                            <td colspan="4" align="center">
                                <div id="Div1" class="page-title">
                                    Supplier Data Form
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                SC :
                            </td>
                            <td align="left" colspan="3">
                                &nbsp;<asp:Label ID="lblsuppCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right" style="width: 20%">
                                Company Name :
                            </td>
                            <td align="left" colspan="3" style="width: 80%">
                                &nbsp;<asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Company Address :
                            </td>
                            <td align="left" colspan="3">
                                &nbsp;<asp:Label ID="lblCompAdd" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                State/Province :
                            </td>
                            <td align="left" colspan="3">
                                &nbsp;<asp:Label ID="lblstate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Country :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblcountry" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Postal Code :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPostal" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Telephone :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblTele1" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Telephone(2) :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblTele2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                AOH Number :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblAOHNo1" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                AOH Number(2) :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblAOHNo2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Fax Number :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblFax1" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Fax Number(2) :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblFax2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Company Email :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblEmail1" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Company Email(2) :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblEmail2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Company Web Site :
                            </td>
                            <td align="left" colspan="3">
                                &nbsp;<asp:Label ID="lblCompWebsite" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 17px; color: Black; height: 40px">
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Additional Company Information
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Company Reg No.:
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblCompNo" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                GST / Tax Reg No.:
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblGSTNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                ISO Certification :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblISo" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Billing Currency :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblBillCur" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Scope of Supply:
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblScope" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Additional Service:
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblAddService" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 17px; color: Black; height: 40px">
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Main Pic Contact Details
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Name :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICName1" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Designation :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICDesig1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                TelePhone :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICTele1" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Mobile :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICMobile1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Email :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICEmail1" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Skype Address :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICSAdd1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-size: 17px; color: Black; height: 40px">
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Alternative PIC Contact Details
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Name :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICname2" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Designation :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICDesignation2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                TelePhone :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICTele2" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Mobile :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICMobile2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Email :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPICEmail2" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Skype Address :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblSkypeAdd2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <p style="page-break-before: always;">
                        &nbsp;</p>
                    <table border="2" width="100%" cellpadding="0" cellspacing="0">
                        <tr style="font-size: 17px; color: Black; height: 40px">
                            <td align="left" colspan="6">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Banking and Remittance Details
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right" style="width: 20%">
                                Bank Name :
                            </td>
                            <td align="left" style="width: 30%">
                                &nbsp;<asp:Label ID="lblbankName" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" style="width: 20%">
                                Beneficiary Name :
                            </td>
                            <td align="left" style="width: 30%">
                                &nbsp;<asp:Label ID="lblBename" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Bank Address :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblBankAddress" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Beneficiary Address :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblBAdd" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Bank Code :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblBankCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Beneficiary Account :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblBenAcc" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Branch Code :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblBCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Account Currency :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblAccountCurrency" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                SWIFT Code :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblSwiftCocde" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Notify payments to :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblPaymentTo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                IBAN Code :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblIBAnCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Notification Email :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Other Bank Information :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblOtherBankInfo" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Tax Acc. Number :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblTaxAccNumber" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="text-align: center; height: 75px; font-size: 15px; vertical-align: middle;">
                            <td colspan="4">
                                <div style="text-align: center; height: 75px; font-size: 15px; vertical-align: middle;">
                                    The above banking and remittance information must be verified and signed by any
                                    of the following authorized personnel or personnel with appointment of equivilent
                                    authority.<br />
                                    <div style="text-align: center; font-weight: 600;">
                                        Managing Director, Directors, Financial Controllers, Head of Accounting/Finance
                                        Dept.
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="left" colspan="4">
                                <div style="background: #cccccc; padding: 2px; font-weight: 500">
                                    Upload copy of the endorsed Supplier data sheet
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="left" colspan="4">
                                <div>
                                    <asp:GridView ID="gvAttachmentnew" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                        Width="100%">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Attachments">
                                                <HeaderTemplate>
                                                    Attached File(s)
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfileName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'
                                                        Visible="true"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text="has been successfully uploaded." Visible="true"></asp:Label>
                                                    <asp:Label ID="lblfilePath" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblFileId" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="left" colspan="4">
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Upload Copy of Company Registration document</div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="left" colspan="4">
                                <div>
                                    <asp:GridView ID="gvCompanyAttachmentNew" runat="server" AutoGenerateColumns="False"
                                        GridLines="Both" Width="100%">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Attachments">
                                                <HeaderTemplate>
                                                    Attached File(s)
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfileName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'
                                                        Visible="true"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text="has been successfully uploaded." Visible="true"></asp:Label>
                                                    <asp:Label ID="lblfilePath" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblFileId" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr class="tr-Header">
                            <td align="right">
                                Verified by :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblverify" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                Designation :
                            </td>
                            <td align="left">
                                &nbsp;<asp:Label ID="lblVDesignation" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-family: Tahoma; font-size: 15px; color: Black; height: 70px">
                            <td align="right">
                                Company Stamp :
                            </td>
                            <td align="left">
                            </td>
                            <td align="right">
                                Signature :
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr style="color: Red; font-size: 15px; height: 100px">
                            <td colspan="4">
                                <div style="text-align: center; height: 100px; font-size: 13px; vertical-align: middle;">
                                    Please do a printout of the above data sheet, and obtain the required authorized
                                    signatory, endorsed with your company stamp; and upload the scan copy using the
                                    below file upload section.<br />
                                    You are also required to upload a scan copy of your COMPANY REGISTRATION document
                                    using the below file upload section.<br />
                                    Alternatively, you can send both documents together with your next invoice to us
                                    for our record keeping.<br />
                                </div>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
