<%@ Page Title="Add New Crew" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddEditCrew.aspx.cs" Inherits="Crew_AddEditCrew" %>

<%@ Register Src="~/UserControl/ctlAirPortList.ascx" TagName="AirPortList" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/CrewDetails_Common.js" type="text/javascript"></script>
    <script src="../Scripts/CrewQuery_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/notifier.js" type="text/javascript"></script>
    <style type="text/css">
        @media print
        {
            body *
            {
                display: none;
            }
            #page-content
            {
                display: block;
            }
        }
    </style>
   
    <script language="javascript" type="text/javascript">

        function isSpclChar(f, regex_) {
            var errMsg = "";
            var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i

            switch (regex_) {\
                case 'valid_regex_alphanumeric':
                    f.value = f.value.replace(/[^A-z\s0-9@#$.,\(\)-]/g, "").toUpperCase();
                    break;
                case 'valid_regex_alpha':
                    f.value = f.value.replace(/[^A-z\s]/g, "").toUpperCase();
                    break;
                case 'valid_regex_numeric':
                    f.value = f.value.replace(/[^0-9\.{0,1}]/g, "");
                    break;
                case 'valid_regex_email':
                    {
                        if (f.value != "" && !ck_email.test(f.value)) {
                            alert("You must enter a valid email address.");
                            f.value = "";
                        }
                    }
                    break;
            }
        }
        function DateValidation() {
            if (PassportDateValidation() == true)
                return SeamanDateValidation();
            else
                return false;
        }
        function PassportDateValidation() {
            var v = $("[id$='txtPassport_IssueDate']").val();
            var v1 = $("[id$='txtPassport_ExpDate']").val();
            if (v != "" && v1 != "") {
               var Issuedatearray = v.split("/");
                var newIssuedate = Issuedatearray[1] + '/' + Issuedatearray[0] + '/' + Issuedatearray[2];

                var Expirydatearray = v1.split("/");
                var newExpirydate = Expirydatearray[1] + '/' + Expirydatearray[0] + '/' + Expirydatearray[2];

                if (new Date(newIssuedate) > new Date(newExpirydate)) {
                    alert('Passport Issue date cannot be greater than Expiry Date');
                    return false;
                }
            }
            return true;
        }
        function SeamanDateValidation() {
            var v = $("[id$='txtSeamanBk_IssueDate']").val();
            var v1 = $("[id$='txtSeamanBk_ExpDate']").val();
            if (v != "" && v1 != "") {
                var Issuedatearray = v.split("/");
                var newIssuedate = Issuedatearray[1] + '/' + Issuedatearray[0] + '/' + Issuedatearray[2];

                var Expirydatearray = v1.split("/");
                var newExpirydate = Expirydatearray[1] + '/' + Expirydatearray[0] + '/' + Expirydatearray[2];

                if (new Date(newIssuedate) > new Date(newExpirydate)) {
                    alert('Seaman Book Issue date cannot be greater than Expiry Date');
                    return false;
                }
            }
            return true;
        }    
    </script>
    <script type="text/javascript">
        var ck_name = /^[A-Za-z\s]{3,200}$/;
        var ck_email = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
        var ck_username = /^[A-Za-z0-9_]{1,20}$/;
        var ck_password = /^[A-Za-z0-9!@#$%^&*()_]{6,20}$/;
        var ck_date = /^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$/;

        function validate(obj, regex) {
            var data = obj.value;
            var errMsg = "";

            switch (regex) {
                case 'ck_name':
                    if (!ck_name.test(data)) {
                        errMsg = "Invalid Name.";
                    }
                    break;
                case 'ck_email':
                    if (!ck_email.test(data)) {
                        errMsg = "You must enter a valid email address.";
                    }
                    break;
                case 'ck_username':
                    if (!ck_username.test(data)) {
                        errMsg = "You valid UserName no special char .";
                    }
                    break;
                case 'ck_password':
                    if (!ck_password.test(data)) {
                        errMsg = "You must enter a valid Password ";
                    }
                    break;
                case 'ck_date':
                    if (!ck_date.test(data)) {
                        errMsg = "You must enter a valid date in Date of Birth field";
                    }
                    break;
            }
            return errMsg;
        }
        function reportErrors(errors) {
            var numError = 0;
            var msg = "";
            var msg = "Please enter valid data ...\n";
            for (var i = 0; i < errors.length; i++) {
                if (errors[i].length > 0) {
                    numError += 1;
                    msg += "\n" + numError + ". " + errors[i];
                }
            }
            if (numError > 0) {
                alert(msg);
                return false;
            }
            else
            { return true; }
        }


        function validateForm() {
            var errors = [];
            errors[errors.length] = validate('<%=txtPD_Surname.UniqueID%>', 'ck_name');
            errors[errors.length] = validate('<%=txtPD_Givenname.UniqueID%>', 'ck_name');
            //errors[errors.length] = validate('<%=txtPD_DOB.UniqueID%>', 'ck_date');
            return reportErrors(errors);
            errors = null;
        }

        var iCrewId;
        function QuickApproval(CrewID) {
            iCrewId = CrewID;
            CheckCrewReference(CrewID);
            if ($('[id$=HiddenField_ReferenceCount]').val() == 0) {
                return false;
            }
            else {
                window.open("CrewApproval.aspx?ID=" + CrewID + "&Quick=1");
                return true;
            }
        }

        function CheckCrewReference(CrewID) {
            if (lastExecutor_WebServiceProxy != null)
                lastExecutor_WebServiceProxy.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'referenceCheckForCrew', false, { "CrewID": CrewID }, ReferenceCheck_onSuccess, ReferenceCheck_onFail);

            lastExecutor_WebServiceProxy = service.get_executor();
        }
        function ReferenceCheck_onSuccess(retval) {
            $('[id$=HiddenField_ReferenceCount]').val(retval);
            if (retval == 0) {
                alert('Add atleast one Referrer in Pre-Joining Exp..!');
            }
            else {
                window.open("CrewApproval.aspx?ID=" + iCrewId + "&Quick=1");
            }
        }
        function ReferenceCheck_onFail(err_) {
            alert(err_._message);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="pageTitle" class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Add/Edit Crew Details"></asp:Label>
    </div>
    <div id="page-content" class="page-content-main">
        <asp:UpdatePanel ID="UpdatePanel_PD" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
                    <ProgressTemplate>
                        <div id="blur-on-updateprogress">
                            &nbsp;</div>
                        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                            color: black">
                            <img src="../Images/loaderbar.gif" alt="Please Wait" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:HiddenField ID="HiddenField_AccType" runat="server" />
                <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
                <asp:HiddenField ID="HiddenField_UserID" runat="server" />
                <asp:HiddenField ID="HiddenField_PhotoUploadPath" runat="server" />
                <asp:HiddenField ID="HiddenField_ReferenceCount" runat="server" />
                <asp:HiddenField ID="HiddenField_NationalityId" runat="server" />
                <div class="error-message">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <asp:Panel ID="pnlView_PersonalDetails" runat="server" Visible="true">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Personal Details :
                            <asp:LinkButton ID="lnkEditPersonalDetails" runat="server" CssClass="inline-edit"
                                OnClick="lnkEditPersonalDetails_Click">[Edit]</asp:LinkButton></legend>
                        <table border="1" style="width: 100%" class="dataTable">
                            <tr>
                                <td>
                                    Surname
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblSurname" runat="server"></asp:Label>
                                </td>
                                <td>
                                    First Name
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblGivenName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Date of Birth
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
                                </td>
                                <td rowspan="9" style="width: 120px; text-align: center;">
                                    <asp:ImageButton ID="imgCrewPic" ImageUrl="" runat="server" Visible="false" Width="150px"
                                        OnClientClick="showDialog('#dvCrewPhotoUploader1')" AlternateText="Click to Change" />
                                    <asp:ImageButton ID="imgNoPic" ImageUrl="~/Images/NoPhoto.png" runat="server" Visible="false"
                                        OnClientClick="showDialog('#dvCrewPhotoUploader1')" AlternateText="Click to Change" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Alias
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblAlias" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Place of Birth
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblPlaceOfBirth" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Nationality
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblNationality" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Marital Status
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Telephone
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="3" style="vertical-align: top">
                                    Address:
                                </td>
                                <td rowspan="3" colspan="3" class="data">
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Mobile
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fax
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblFax" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    E-mail
                                </td>
                                <td class="data">
                                    <%--<asp:Label ID="lblEMail" runat="server"></asp:Label>--%>
                                    <asp:HyperLink ID="lblEMail" runat="server" NavigateUrl = "lblEMail" ></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    US Visa
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblUSVisaFlag" runat="server"></asp:Label>
                                </td>
                                <td>
                                    US Visa Number
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblUSVisaNo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    US Visa Expiry Date
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblUSVisaExpiry" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px;">
                                    Height (CM)
                                </td>
                                <td style="width: 150px;">
                                    <asp:Label ID="lblHeight" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px;">
                                    Waist (Inch)
                                </td>
                                <td style="width: 150px;">
                                    <asp:Label ID="lblWaist" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px;">
                                    Weight (Kg)
                                </td>
                                <td style="width: 150px;">
                                    <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    Nearest International Airport: &nbsp;<asp:Label ID="lblIntlAirport" CssClass="data"
                                        runat="server"></asp:Label>
                                </td>
                                <td>
                                English Proficiency: &nbsp;
                                </td>
                                <td>
                                <asp:Label ID="lblEngProficiency" CssClass="data"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="pnlEdit_PersonalDetails" runat="server" Visible="false">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px; background-color: #EFFBEF">
                        <legend>Personal Details :
                            <%--<asp:LinkButton ID="lnkSavePersonalDetails" runat="server" CssClass="inline-edit"
                                OnClick="lnkSavePersonalDetails_Click">[Save]</asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelEditPersonalDetails" runat="server" CssClass="inline-edit"
                                OnClick="lnkCancelEditPersonalDetails_Click">[Cancel]</asp:LinkButton>--%>
                        </legend>
                        <table cellpadding="2" cellspacing="0" style="background-color: #dcbfdf; width: 100%;
                            margin-bottom: 5px;">
                            <tr>
                                <td>
                                    APPLICATION FOR EMPLOYEMENT AS:
                                    <asp:DropDownList ID="ddlRankAppliedFor" runat="server" DataTextField="Rank_Short_Name"
                                        DataValueField="id" Width="100px" AppendDataBoundItems="true" CssClass="control-edit required">
                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    DATE AVAILABLE FOR JOINING
                                    <asp:TextBox ID="txtDateAvailable" runat="server" CssClass="control-edit required"></asp:TextBox><ajaxToolkit:CalendarExtender
                                        ID="CalendarExtender1" runat="server" TargetControlID="txtDateAvailable" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    MANNING OFFICE:<%--<asp:TextBox ID="txtManningOffice" runat="server" Width="300px" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdnManningOfficeID" runat="server" />--%><asp:DropDownList ID="ddlManningOffice"
                                        runat="server" Width="256px" TabIndex="5">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="2" cellspacing="0" style="width: 100%; padding: 0;
                            border-collapse: collapse;">
                            <tr>
                                <td>
                                    Surname
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_Surname" runat="server" CssClass="control-edit valid_regex_alpha"></asp:TextBox>
                                </td>
                                <td>
                                    First Name
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_Givenname" runat="server" CssClass="control-edit required valid_regex_alpha"></asp:TextBox>
                                </td>
                                <td>
                                    Date of Birth
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_DOB" runat="server" Width="150px" CssClass="control-edit required"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtPD_DOB"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Alias
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_Alias" runat="server" CssClass="control-edit valid_regex_alpha"></asp:TextBox>
                                </td>
                                <td>
                                    Place of Birth
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_PlaceOfBirth" runat="server" CssClass="control-edit required valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td>
                                    Nationality
                                </td>
                                <td class="data">
                                    <asp:DropDownList ID="ddlPD_Nationality" runat="server" Width="154px" CssClass="control-edit required">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Marital Status
                                </td>
                                <td class="data">
                                    <asp:DropDownList ID="ddlPD_MaritalStatus" runat="server" Width="154px" CssClass="control-edit">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="SINGLE" Value="SINGLE"></asp:ListItem>
                                        <asp:ListItem Text="MARRIED" Value="MARRIED"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Telephone
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_Phone" runat="server" CssClass="control-edit valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2" style="vertical-align: top">
                                    Address
                                </td>
                                <td rowspan="2" colspan="3" class="data" style="vertical-align: top">
                                    <asp:TextBox ID="txtPD_Address" TextMode="MultiLine" Width="490px" Height="40px"
                                        runat="server" CssClass="control-edit required valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td>
                                    Mobile
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_Mobile" runat="server" CssClass="control-edit required valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fax
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_Fax" runat="server" CssClass="control-edit valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    Nearest International Airport &nbsp;
                                    <uc2:AirPortList ID="txtPD_Airport" runat="server" Width="300px" />
                                </td>
                                <td colspan="2">
                                English Proficiency &nbsp;
                                <asp:DropDownList runat="server" ID="ddlEngProficiency">
                               
                                </asp:DropDownList>
                                </td>
                                <td>
                                    E-mail
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPD_Email" runat="server" CssClass="control-edit required valid_regex_email"
                                        Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    US Visa
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdoUSVisaFlag" runat="server" RepeatDirection="Horizontal"
                                        CssClass="required" CellPadding="0">
                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    US Visa Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPD_USVisaNo" runat="server" CssClass="control-edit valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td>
                                    US Visa Expiry Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPD_USVisaExpiry" runat="server" Width="150px" CssClass="control-edit"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txtPD_USVisaExpiry"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Height (CM)
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtHeight" runat="server" CssClass="control-edit required valid_regex_numeric"></asp:TextBox>
                                </td>
                                <td>
                                    Waist (Inch)
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtWaist" runat="server" CssClass="control-edit required valid_regex_numeric"></asp:TextBox>
                                </td>
                                <td>
                                    Weight (Kg)
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtWeight" runat="server" CssClass="control-edit required valid_regex_numeric"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: center">
                                    <asp:Button ID="btnSavePersonalDetails" runat="server" Text=" Save " OnClick="btnSavePersonalDetails_Click" />
                                    <asp:Button ID="btnCancelPersonalDetails" runat="server" Text="Cancel" OnClick="btnCancelPersonalDetails_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
         <div id="dvCrewPhotoUploader1" style="text-align: right; display: none; position: absolute;
            right: 20%; top: 20%; height: 100px; width: 300px; border: 2px solid #aabbdd;
            background-color: White;">
         </div>
        <asp:UpdatePanel ID="UpdatePanel_PP" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlView_Passport" runat="server">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                        <legend>Passport and Seaman Book :
                            <asp:LinkButton ID="lnkEditPassport" runat="server" CssClass="inline-edit" OnClick="lnkEditPassport_Click"><font color=blue>[Edit]</font></asp:LinkButton></legend>
                        <table border="0" style="width: 100%" class="dataTable">
                            <tr>
                                <td>
                                    Passport No
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblPassport_No" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Place of Issue
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblPassport_Place" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Issue Date
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblPassport_IssueDt" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Expiry Date
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblPassport_ExpDt" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Seaman Bk. No
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblSeamanBk_No" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Place of Issue
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblSeamanBk_Place" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Issue Date
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblSeamanBk_IssueDt" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Expiry Date
                                </td>
                                <td class="data">
                                    <asp:Label ID="lblSeamanBk_ExpDt" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="pnlEdit_Passport" runat="server">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px; background-color: #EFFBEF">
                        <legend>Passport and Seaman Book :
                            <%--<asp:LinkButton ID="lnkSavePassport" runat="server" CssClass="inline-edit" OnClick="lnkSavePassport_Click">[Save]</asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelEditPassport" runat="server" CssClass="inline-edit"
                                OnClick="lnkCancelEditPassport_Click">[Cancel]</asp:LinkButton>--%></legend>
                        <table border="0" style="width: 100%">
                            <tr>
                                <td>
                                    Passport No
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPassport_No" runat="server" CssClass="control-edit required valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td>
                                    Place of Issue
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPassport_Place" runat="server" CssClass="control-edit required valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td>
                                    Issue Date
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPassport_IssueDate" runat="server" CssClass="control-edit required" onchange="PassportDateValidation()"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPassport_IssueDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Expiry Date
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtPassport_ExpDate" runat="server" CssClass="control-edit required" onchange="PassportDateValidation()"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtPassport_ExpDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Seaman Bk. No
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtSeamanBk_No" runat="server" CssClass="control-edit required valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td>
                                    Place of Issue
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtSeamanBk_Place" runat="server" CssClass="control-edit required valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td>
                                    Issue Date
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtSeamanBk_IssueDate" runat="server" CssClass="control-edit required"  onchange="SeamanDateValidation()"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtSeamanBk_IssueDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Expiry Date
                                </td>
                                <td class="data">
                                    <asp:TextBox ID="txtSeamanBk_ExpDate" runat="server" CssClass="control-edit required"   onchange="SeamanDateValidation()"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtSeamanBk_ExpDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: center">
                                    <asp:Button ID="btnSavePassportDetails" runat="server" Text=" Save " OnClick="btnSavePassportDetails_Click"  OnClientClick="return DateValidation()" />
                                    <asp:Button ID="btnCancelPassportDetails" runat="server" Text="Cancel" OnClick="btnCancelPassportDetails_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel_NOK" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlView_NextOfKin" runat="server">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                        <legend>Next of Kin Details :
                            <asp:LinkButton ID="lnkEditNOK" runat="server" CssClass="inline-edit" OnClick="lnkEditNOK_Click"><font color=blue>[Edit]</font></asp:LinkButton></legend>
                        <table border="0" cellpadding="4" class="dataTable">
                            <tr>
                                <td style="width: 100px">
                                    Next of Kin
                                </td>
                                <td class="data" style="width: 300px">
                                    <asp:Label ID="lblNOKName" runat="server" Width="300px"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Relationship
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblNOKrelationship" runat="server" Width="106px">                                                        
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Address
                                </td>
                                <td rowspan="2" class="data" style="width: 300px">
                                    <asp:Label ID="lblNOKAddress" runat="server" Width="300px"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Telephone
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblNOKPhone" runat="server" Width="100px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="pnlEdit_NextOfKin" runat="server" Visible="false">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px; background-color: #EFFBEF">
                        <legend>Next of Kin :
                            <%--<asp:LinkButton ID="lnkSaveNOK" runat="server" CssClass="inline-edit" OnClick="lnkSaveNOK_Click">[Save]</asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelEditNOK" runat="server" CssClass="inline-edit" OnClick="lnkCancelEditNOK_Click">[Cancel]</asp:LinkButton>--%>
                        </legend>
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    Next of Kin
                                </td>
                                <td style="width: 300px">
                                    <asp:TextBox ID="txtNOKName" runat="server" Width="300px" CssClass="control-edit valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Relationship
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="ddlNOKRelationship" runat="server" Width="106px" CssClass="control-edit">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                        <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                        <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                        <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                        <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER-IN-LAW" Value="BROTHER-IN-LAW"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Address
                                </td>
                                <td rowspan="2" style="width: 300px">
                                    <asp:TextBox ID="txtNOKAddress" runat="server" Width="300px" Height="50px" TextMode="MultiLine"
                                        CssClass="control-edit valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Telephone
                                </td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtNOKPhone" runat="server" Width="100px" CssClass="control-edit valid_regex_alphanumeric"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right">
                                    <asp:HiddenField ID="hdnNOKID" runat="server" />
                                    <asp:Button ID="btnSaveNOKDetails" runat="server" Text=" Save " OnClick="btnSaveNOKDetails_Click" />
                                    <asp:Button ID="btnCancelNOKDetails" runat="server" Text=" Close " OnClick="btnCancelNOKDetails_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="pnlDependents" runat="server" Visible="true">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                        <legend>Dependents :
                            <asp:LinkButton ID="lnkAddDependents" runat="server" CssClass="inline-edit" OnClick="lnkAddDependents_Click"><font color='blue'>[Add Dependent]</font></asp:LinkButton></legend>
                        <asp:ObjectDataSource ID="objDS_Dependents" runat="server" SelectMethod="Get_Crew_DependentsByCrewID"
                            TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails" DeleteMethod="DEL_Crew_DependentDetails"
                            InsertMethod="INS_CrewPreJoiningExp" UpdateMethod="UPDATE_Crew_DependentDetails">
                            <DeleteParameters>
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:SessionParameter Name="Deleted_By" SessionField="userid" Type="Int32" />
                            </DeleteParameters>
                            <SelectParameters>
                                <asp:ControlParameter Name="CrewID" ControlID="HiddenField_CrewID" PropertyName="Value" Type="Int32" />
                                <asp:Parameter Name="IsNOK" Type="Int32" DefaultValue="0" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="FullName" Type="String" />
                                <asp:Parameter Name="Relationship" Type="String" />
                                <asp:Parameter Name="DOB" Type="String" />
                                <asp:SessionParameter Name="Modified_By" SessionField="userid" Type="Int32" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                        <asp:GridView ID="GridView_Dependents" runat="server" DataKeyNames="ID" CellPadding="3"
                            GridLines="None" CellSpacing="1" Width="100%" AutoGenerateColumns="False" DataSourceID="objDS_Dependents"
                            AllowSorting="True" CssClass="GridView-css">
                            <Columns>
                                <asp:TemplateField HeaderText="Relationship " SortExpression="Relationship">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelationship" runat="server" Text='<%#Bind("Relationship")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDEPRelationship" runat="server" Width="150px" Text='<%#Bind("Relationship")%>'>
                                            <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                            <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                            <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                            <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                            <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                            <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                            <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                            <asp:ListItem Text="FATHER-IN-LAW" Value="FATHER-IN-LAW"></asp:ListItem>
                                            <asp:ListItem Text="MOTHER-IN-LAW" Value="MOTHER-IN-LAW"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemStyle Width="160px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dependent Name" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFullName" runat="server" Text='<%#Bind("FullName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFullName" runat="server" Text='<%#Bind("FullName")%>' Width="200px"
                                            CssClass="valid_regex_alphanumeric"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/images/accept.png" CausesValidation="True"
                                            CommandName="Update" AlternateText="Update" ValidationGroup="noofdays"></asp:ImageButton>
                                        &nbsp;<asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png"
                                            CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                            CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                            CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                            AlternateText="Delete"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                        <asp:Panel ID="pnlAddDependents" runat="server" Visible="false">
                            <table cellspacing="1" cellpadding="3" border="0" style="background-color: White;
                                border-color: Gray; border-width: 1px; border-style: Solid;">
                                <tr>
                                    <td style="width: 160px">
                                        <asp:DropDownList ID="ddlDEP_Relatipnship" runat="server" Width="150px" CssClass="control-edit">
                                            <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                            <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                            <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                            <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                            <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                            <asp:ListItem Text="FATHER-IN-LAW" Value="FATHER-IN-LAW"></asp:ListItem>
                                            <asp:ListItem Text="MOTHER-IN-LAW" Value="MOTHER-IN-LAW"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 200px">
                                        <asp:TextBox ID="txtDEP_Name" runat="server" Width="190px" CssClass="control-edit valid_regex_alpha"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtDEP_Name"
                                            WatermarkText="Dependent Name" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td style="text-align: right; width: 250px;">
                                        <asp:Button ID="btnAddDependents" runat="server" Text="Add Dependent" OnClick="btnSaveDependents_Click" />
                                        <asp:Button ID="btnCloseDependent" runat="server" Text=" Close " OnClick="btnCloseDependent_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvPrejoining"  runat="server" style="padding: 2px; display: block;">
            <asp:UpdatePanel ID="UpdatePanel_PreviousContacts" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="fragment-1-tool" style="text-align: right;">
                     </div>
                    <iframe id="iFrame1" src="CrewDetails_Prejoining.aspx?ID=<%=GetCrewID() %>" style="width: 100%;
                        height: 450px; border: 0px;"></iframe>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel_CON" runat="server">
            <ContentTemplate>
                <fieldset style="text-align: left; margin-top: 5px; padding: 2px; font-size: 11px;
                    text-align: center;">
                    <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" OnClick="btnSaveDetails_Click"
                        OnClientClick="return validateForm()" />
                    <asp:Button ID="btnClose" Text="Cancel and GoBack" runat="server" OnClick="btnCloseDetails_Click" />
                    <asp:Button ID="btnCrewDetails" runat="server" Text="GoTo Crew Details" OnClick="btnCrewDetails_Click" />
                    <asp:Button ID="btnQuickApproval" runat="server" Text="Quick Approval"  OnClientClick="QuickApproval($('[id$=HiddenField_CrewID]').val());return false;" />
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvExistingCrew" runat="server" style="width: 600px; display: none;"  >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding: 10px;">
                    <div class="roundedBox">
                        The following profiles are matching with the new staff's profile.
                        <br />
                        Do not enter new profile if the staff's profile already exists.
                    </div>
                    <div style="margin-top: 10px;">
                        <asp:GridView ID="grdExistingCrew" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            Width="99%" EmptyDataText="" CaptionAlign="Bottom" GridLines="None" DataKeyNames="ID"
                            AllowPaging="True" PageSize="10" AllowSorting="True" CssClass="GridView-css">
                            <Columns>
                                <asp:TemplateField HeaderText="S/Code">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>' CssClass="staffInfo"
                                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Staff First Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblStaff_Name" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>'
                                            Target="_blank" Text='<%# Eval("Staff_Name")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Staff SurName">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblStaff_Surname" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>'
                                            Target="_blank" Text='<%# Eval("Staff_Surname")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DOB">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("Staff_Birth_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passport">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Passport_Number")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                    </div>
                    <div style="text-align: center; padding: 5px; margin-top: 10px;">
                        <asp:Button ID="btnOK" runat="server" Text="OK" BorderStyle="Solid" BorderColor="#0489B1"
                            BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="60px"
                            OnClientClick="hideModal('dvExistingCrew'); return false;" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
</asp:Content>
