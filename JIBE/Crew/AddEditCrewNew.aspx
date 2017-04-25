<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddEditCrewNew.aspx.cs"
    Title="Add/Edit Crew Details" Inherits="Crew_AddEditCrewNew" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/ctlAirPortList.ascx" TagName="AirPortList" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/CrewDetails_Common.js" type="text/javascript"></script>
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
        
        .mandatory
        {
            color: Red;
            float: right;
        }
        .gridmain-css tr
        {
            height: 25px;
        }
        .gridmain-css tr:hover
        {
            background-color: #feecec;
        }
        fieldset
        {
            margin: 10px 0 0;
        }
        input[type='text']
        {
            height: 18px;
        }
        .CrewImage
        {
            max-width: 110px !important;
            border: 1px solid #c6c6c6 !important;
            background-color: #fff;
            padding: 5px;
        }
        .page-content-main
        {
            border: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="pageTitle" class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Add/Edit Crew Details"></asp:Label>
    </div>
    <div id="page-content" class="page-content-main">
        <asp:UpdateProgress ID="upUpdateProgress_AddEditCrew" DisplayAfter="1" runat="server"
            AssociatedUpdatePanelID="UpdatePanel_PD_AddEditCrew">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:HiddenField ID="hdnClientName" runat="server" />
        <asp:HiddenField ID="hdnUSCountryID" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel_PD_AddEditCrew" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlView_Screen1" runat="server" Visible="true">
                    <fieldset style="background-color: #effbef; margin: 0; padding: 2px;">
                        <legend>Personal Details</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td width="85%">
                                    <table border="0" cellpadding="3" style="width: 91%; padding: 0; border-collapse: collapse;">
                                        <tr>
                                            <td width="125px">
                                                Name<span class="mandatory">*</span>
                                            </td>
                                            <td width="450px">
                                                <asp:TextBox runat="server" class="control-edit required" ID="txtName" ClientIDMode="Static"
                                                    MaxLength="100" Width="304px" autocomplete="off" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fltName" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    TargetControlID="txtName" ValidChars=" ">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="135px">
                                                Middle Name
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtMiddleName" ClientIDMode="Static" MaxLength="100"
                                                    Width="300px" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fltMiddleName" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    TargetControlID="txtMiddleName" ValidChars=" ">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Surname
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtSurname" ClientIDMode="Static" MaxLength="100"
                                                    Width="300px" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fltSurname" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    TargetControlID="txtSurname" ValidChars=" ">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                Alias/Nickname
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtAlias" ClientIDMode="Static" MaxLength="100" Width="300px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Place of Birth<span class="mandatory">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" class="control-edit required" ClientIDMode="Static" ID="txtPlaceOfBirth"
                                                    MaxLength="100" Width="304px" />
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                Date of Birth<span class="mandatory">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtDOB" class="control-edit required" ClientIDMode="Static"
                                                    MaxLength="80" Width="300px" autocomplete="off" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderDOB" runat="server" TargetControlID="txtDOB"
                                                    Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Martial Status
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPD_MaritalStatus" runat="server" ClientIDMode="Static" Width="305px"
                                                    CssClass="control-edit">
                                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="SINGLE" Value="SINGLE"></asp:ListItem>
                                                    <asp:ListItem Text="MARRIED" Value="MARRIED"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                Nationality<span class="mandatory">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPD_Nationality" ClientIDMode="Static" runat="server" Width="305px"
                                                    CssClass="control-edit required">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdlblRace" runat="server" visible="false">
                                                Race<span class="mandatory">*</span>
                                            </td>
                                            <td id="tdtxtRace" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlPD_Race" ClientIDMode="Static" runat="server" Width="305px"
                                                    CssClass="control-edit required">
                                                </asp:DropDownList>
                                            </td>
                                            <td id="tdEmptyRace" runat="server" visible="false">
                                            </td>
                                            <td id="tdlblSSN" runat="server" visible="false">
                                                SSN<span class="mandatory">*</span>
                                            </td>
                                            <td id="tdtxtSSN" runat="server" visible="false">
                                                <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtSSN"
                                                    Width="304px" MaxLength="11" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'" />
                                                <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="flttxtSSN" TargetControlID="txtSSN"
                                                    FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars="-">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="15%" height="135px" style="border: ">
                                    <asp:ImageButton ID="imgCrewPic" ToolTip="Click to change crew image" CssClass="CrewImage"
                                        ImageUrl="" runat="server" Style="display: none;" OnClientClick="UploadCrewPhoto();return false;"
                                        AlternateText="Click to Change" ClientIDMode="Static" />
                                    <asp:ImageButton ID="imgNoPic" CssClass="CrewImage" ImageUrl="~/Images/NoPhoto.png"
                                        runat="server" ClientIDMode="Static" OnClientClick="UploadCrewPhoto();return false;"
                                        AlternateText="Click to Change" />
                                    <asp:HiddenField ID="hdnCrewPhotoFileName" runat="server" Value="" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="background-color: #effbef; padding: 2px;">
                        <legend>Contact Information</legend>
                        <table border="0" cellpadding="3" cellspacing="0" style="width: 100%; padding: 0;
                            border-collapse: collapse;">
                            <tr id="trUSAddress" runat="server" visible="false">
                                <td width="125px">
                                    Address Line 1<span class="mandatory">*</span>
                                </td>
                                <td width="450px">
                                    <asp:TextBox runat="server" ID="txtAddressline1" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="100" Width="305" />
                                </td>
                                <td width="10px">
                                </td>
                                <td width="135px">
                                    Address Line 2
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAddressline2" ClientIDMode="Static" class="control-edit"
                                        MaxLength="100" Width="305" />
                                </td>
                            </tr>
                            <tr id="trUSCityState" runat="server" visible="false">
                                <td width="125px">
                                    City<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCity" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="100" Width="305" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    State/Province/Region<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtState" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="100" Width="305" />
                                </td>
                            </tr>
                            <tr id="trUSCountryZip" runat="server" visible="false">
                                <td width="125px">
                                    Country<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpUSCountry" ClientIDMode="Static" class="control-edit required"
                                        Width="305px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Zip Code<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtZipCode" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="50" Width="305" />
                                </td>
                            </tr>
                            <tr id="trInternational" runat="server" visible="false">
                                <td width="125px">
                                    Address<span class="mandatory">*</span>
                                </td>
                                <td width="450px">
                                    <textarea type="text" id="txtAddress" clientidmode="Static" textmode="MultiLine"
                                        maxlength="1000" class="control-edit required" style="height: 60px; width: 305px;"
                                        runat="server" />
                                </td>
                                <td width="10px">
                                </td>
                                <td width="135px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td width="125px">
                                    Nearest Airport
                                </td>
                                <td colspan="3">
                                    <uc2:AirPortList ID="txtPD_Airport" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td width="90px">
                                    Phone Number
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPhoneNumber" ClientIDMode="Static" MaxLength="50"
                                        class="control-edit" Width="305" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1"
                                        TargetControlID="txtPhoneNumber" FilterType="Numbers,Custom" ValidChars="+,-,,">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Email Address<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEmailAddress" ClientIDMode="Static" MaxLength="250"
                                        class="control-edit required" Width="305" autocomplete="off" />
                                </td>
                            </tr>
                            <tr>
                                <td width="90px">
                                    Mobile Number<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMobileNumber" ClientIDMode="Static" MaxLength="50"
                                        class="control-edit required" Width="305" autocomplete="off" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2"
                                        TargetControlID="txtMobileNumber" FilterType="Numbers,Custom" ValidChars="+,-,,">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Fax
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFax" MaxLength="50" ClientIDMode="Static" class="control-edit"
                                        Width="305" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3"
                                        TargetControlID="txtFax" FilterType="Numbers">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="background-color: #effbef; padding: 2px;">
                        <legend>Hiring Details</legend>
                        <table border="0" cellpadding="3" cellspacing="0" style="width: 85%; padding: 0;
                            border-collapse: collapse;">
                            <tr>
                                <td width="125px">
                                    Applied Rank<span class="mandatory">*</span>
                                </td>
                                <td width="450px">
                                    <asp:DropDownList runat="server" ID="drpAppliedRank" ClientIDMode="Static" class="control-edit required"
                                        Width="305">
                                    </asp:DropDownList>
                                </td>
                                <td width="10px">
                                </td>
                                <td width="135px">
                                    Manning Office <span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpManningOffice" ClientIDMode="Static" class="control-edit required"
                                        Width="305">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Availability Date <span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAvailabilityDate" MaxLength="80" ClientIDMode="Static"
                                        class="control-edit required" Width="305" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderAvailDate" runat="server" TargetControlID="txtAvailabilityDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                </td>
                                <td id="tdlblHireDate" visible="false" runat="server" clientidmode="Static">
                                    Hire Date<span class="mandatory">*</span>
                                </td>
                                <td width="450px" id="tdtxtHireDate" maxlength="80" visible="false" clientidmode="Static"
                                    runat="server">
                                    <asp:TextBox runat="server" ID="txtHireDate" class="control-edit required" Width="305"
                                        ClientIDMode="Static" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderHireDate" runat="server" TargetControlID="txtHireDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr id="trUnion" runat="server" visible="false" clientidmode="Static">
                                <td>
                                    Union<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpUnion" ClientIDMode="Static" class="control-edit required"
                                        Width="305">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Union Branch <span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ClientIDMode="Static" Enabled="false" ID="drpUnionBranch"
                                        class="control-edit required" Width="305">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnUnionBranchId" runat="server" Value="0" ClientIDMode="Static" />
                                </td>
                            </tr>
                            <tr>
                                <td id="tdlblVeteran" runat="server" clientidmode="Static" visible="false">
                                    Veteran Status<span class="mandatory">*</span>
                                </td>
                                <td id="tdtxtVeteran" runat="server" clientidmode="Static" visible="false">
                                    <asp:DropDownList runat="server" ID="drpVeteranStatus" ClientIDMode="Static" class="control-edit required"
                                        Width="305">
                                    </asp:DropDownList>
                                </td>
                                <td id="tdEmptyVeteran" runat="server" visible="false">
                                </td>
                                <td id="tdlblUnionBook" runat="server" clientidmode="Static" visible="false">
                                    Union Book<span class="mandatory">*</span>
                                </td>
                                <td width="450px" id="tdtxtUnionBook" runat="server" clientidmode="Static" visible="false">
                                    <asp:DropDownList runat="server" ID="drpUnionBook" ClientIDMode="Static" class="control-edit required"
                                        Width="305">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div style="margin-top: 15px;">
                        <asp:Button ID="BtnCancel" OnClick="BtnCancel_OnClick" ClientIDMode="Static" Text="Cancel"
                            runat="server" Style="float: left;" Width="130px" />
                        <asp:Button ID="btnNextScreen1" ClientIDMode="Static" Text="Next" Style="float: right;
                            margin-left: 5px;" Width="130px" runat="server" OnClick="btnNextScreen1_OnClick"
                            Visible="false" />
                        <asp:Button ID="BtnCreateNewCrew" ClientIDMode="Static" Text="Create Crew and Continue"
                            Style="float: right;" Width="170px" runat="server" OnClick="BtnCreateNewCrew_OnClick" />
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlView_CommonScreen23" Visible="false">
                    <table border="0" width="100%">
                        <tr>
                            <td width="85%">
                                <table border="0" cellpadding="3">
                                    <tr>
                                        <td width="27%">
                                            Name
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" Enabled="false" ID="lblName" ClientIDMode="Static" Width="283px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Surname
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" Enabled="false" ID="lblSurname" ClientIDMode="Static"
                                                Width="283px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Applied Rank
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" Enabled="false" ID="lblAppliedRank" ClientIDMode="Static"
                                                Width="283px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Availability Date
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" Enabled="false" ID="lblAvilabilityDate" ClientIDMode="Static"
                                                Width="283px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="15%">
                                <asp:ImageButton ID="imgCrewPicScreen2" ToolTip="Click to change crew image" CssClass="CrewImage"
                                    ImageUrl="" runat="server" Style="display: none; margin-bottom: -15px;" ClientIDMode="Static"
                                    OnClientClick="UploadCrewPhoto();return false;" AlternateText="Click to Change" />
                                <asp:ImageButton ID="imgNoPicScreen2" CssClass="CrewImage" ImageUrl="~/Images/NoPhoto.png"
                                    runat="server" Style="display: none; margin-bottom: -15px;" ClientIDMode="Static"
                                    OnClientClick="UploadCrewPhoto();return false;" AlternateText="Click to Change" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlView_Screen2" runat="server" Visible="false" Style="min-height: 525px">
                    <fieldset style="background-color: #effbef; padding: 2px;">
                        <legend>Documents</legend>
                        <table border="0" cellpadding="3" style="width: 100%; padding: 0; border-collapse: collapse;">
                            <tr id="trPassport" runat="server" clientidmode="Static">
                                <td width="125px">
                                    Passport<span class="mandatory">*</span>
                                    <asp:HiddenField ID="hdnPassportDocTypeID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnPassportDocID" runat="server" Value="0" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtPassport"
                                        MaxLength="50" Width="150px" autocomplete="off" />
                                </td>
                                <td width="130px">
                                    Place of Issue<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtPassportPlaceofIssue"
                                        MaxLength="100" Width="150px" />
                                </td>
                                <td width="130px">
                                    Issue Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtPassportIssueDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CEPassportIssueDate" runat="server" TargetControlID="txtPassportIssueDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="130px">
                                    Expiry Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtPassportExpiryDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CEPassportExpiryDate" runat="server" TargetControlID="txtPassportExpiryDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Country
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="drpCountryPassport" class="control-edit"
                                        Width="150px">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnIsPassportMandatory" runat="server" Value="1" />
                                </td>
                            </tr>
                            <tr id="trSeaman" clientidmode="Static" runat="server" visible="false">
                                <td width="125px">
                                    Seaman<span class="mandatory">*</span>
                                    <asp:HiddenField ID="hdnSeamanDocTypeID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnSeamanDocID" runat="server" Value="0" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtSeaman"
                                        MaxLength="50" Width="150px" autocomplete="off" />
                                </td>
                                <td width="125px">
                                    Place of Issue<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtSeamanPlaceofIssue"
                                        MaxLength="100" Width="150px" />
                                </td>
                                <td width="125px">
                                    Issue Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtSeamanIssueDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CESeamanIssueDate" runat="server" TargetControlID="txtSeamanIssueDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Expiry Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtSeamanExpiryDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CESeamanExpiryDate" runat="server" TargetControlID="txtSeamanExpiryDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Country
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="drpCountrySeaman" class="control-edit"
                                        Width="150px">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnIsSeamanMandatory" runat="server" Value="1" />
                                </td>
                            </tr>
                            <tr id="trMMCNumber" clientidmode="Static" runat="server" visible="false">
                                <td width="125px">
                                    MMC Number<span class="mandatory">*</span>
                                    <asp:HiddenField ID="hdnMMCDocTypeID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnMMCDocID" runat="server" Value="0" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtMMCNumber"
                                        MaxLength="50" Width="150px" autocomplete="off" />
                                </td>
                                <td width="125px">
                                    Place of Issue<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtMMCPlaceofIssue"
                                        MaxLength="100" Width="150px" />
                                </td>
                                <td width="125px">
                                    Issue Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtMMCIssueDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CEMMCIssueDate" runat="server" TargetControlID="txtMMCIssueDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Expiry Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtMMCExpiryDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CEMMCExpiryDate" runat="server" TargetControlID="txtMMCExpiryDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Country
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="drpCountryMMC" class="control-edit"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trTWICNumber" clientidmode="Static" runat="server" visible="false">
                                <td width="125px">
                                    TWIC Number<span class="mandatory">*</span>
                                    <asp:HiddenField ID="hdnTWICDocTypeID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnTWICDocID" runat="server" Value="0" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtTWICNumber"
                                        MaxLength="50" Width="150px" autocomplete="off" />
                                </td>
                                <td width="125px">
                                    Place of Issue<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtTWICPlaceofIssue"
                                        MaxLength="100" Width="150px" />
                                </td>
                                <td width="125px">
                                    Issue Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtTWICIssueDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CETWICIssueDate" runat="server" TargetControlID="txtTWICIssueDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Expiry Date<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ClientIDMode="Static" class="control-edit required" ID="txtTWICExpiryDate"
                                        MaxLength="80" Width="150px" />
                                    <ajaxToolkit:CalendarExtender ID="CETWICExpiryDate" runat="server" TargetControlID="txtTWICExpiryDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Country
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="drpCountryTWIC" class="control-edit"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trValidUSVisa" runat="server" visible="false">
                                <td width="125px">
                                    Valid US Visa?
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="rblstValidUSVisa" ClientIDMode="Static" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="125px">
                                    US Visa Number<span class="mandatory" id="spnUSVisaNumber" runat="server" clientidmode="Static"
                                        style="display: none;">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtUSVisaNumber" ClientIDMode="Static"
                                        MaxLength="50" Width="150px" Enabled="false" autocomplete="off" />
                                </td>
                                <td width="125px">
                                    Issue Date<span class="mandatory" id="spnUSVisaIssueDate" runat="server" clientidmode="Static"
                                        style="display: none;">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtUSVisaIssueDate" ClientIDMode="Static"
                                        MaxLength="80" Width="150px" Enabled="false" />
                                    <ajaxToolkit:CalendarExtender ID="CEUSVisaIssueDate" runat="server" TargetControlID="txtUSVisaIssueDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px">
                                    Expiry Date<span class="mandatory" id="spnUSVisaExpiryDate" runat="server" clientidmode="Static"
                                        style="display: none;">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtUSVisaExpiryDate" ClientIDMode="Static"
                                        MaxLength="80" Width="150px" Enabled="false" />
                                    <ajaxToolkit:CalendarExtender ID="CEUSVisaExpiryDate" runat="server" TargetControlID="txtUSVisaExpiryDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td width="125px" colspan="2">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="background-color: #effbef; padding: 2px;">
                        <legend>General Details</legend>
                        <table border="0" cellpadding="3" style="width: 75%; padding: 0; border-collapse: collapse;">
                            <tr id="trSchool" clientidmode="Static" runat="server" visible="false">
                                <td width="115px">
                                    School
                                </td>
                                <td width="375px">
                                    <asp:DropDownList runat="server" ID="drpSchool" ClientIDMode="Static" class="control-edit"
                                        Width="304px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td width="115px">
                                    Year Graduated
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpSchoolYearGraduated" ClientIDMode="Static"
                                        Enabled="false" class="control-edit" Width="304px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trNaturalization" clientidmode="Static" runat="server" visible="false">
                                <td>
                                    Naturalization?
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpNaturaliztion" ClientIDMode="Static" class="control-edit"
                                        Width="304px">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Naturalization Date<span id="spnNaturaliztionDate" class="mandatory" runat="server"
                                        clientidmode="Static" style="display: none;">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ClientIDMode="Static" Enabled="false"
                                        ID="txtNaturaliztionDate" MaxLength="200" Width="304px" />
                                    <ajaxToolkit:CalendarExtender ID="CENaturaliztionDate" runat="server" TargetControlID="txtNaturaliztionDate"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    English Proficiency
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpEnglishProficiency" Width="304px">
                                        <asp:ListItem Text="-Select" Value="0" />
                                        <asp:ListItem Text="Good" Value="Good" />
                                        <asp:ListItem Text="Fair" Value="Fair" />
                                        <asp:ListItem Text="Bad" Value="Bad" />
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td id="tdlblHeight" runat="server" visible="false">
                                    Height(CM)
                                </td>
                                <td width="450px" id="tdtxtHeight" runat="server" visible="false">
                                    <asp:TextBox runat="server" class="control-edit" ID="txtHeight" MaxLength="8" Width="304px" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FlttxtHeight" runat="server" TargetControlID="txtHeight"
                                        FilterType="Numbers,Custom" ValidChars=".">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr id="trHeight" runat="server" visible="false">
                                <td>
                                    Weight(KG)
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtWeight" MaxLength="8" Width="304px" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FlttxtWeight" runat="server" TargetControlID="txtWeight"
                                        FilterType="Numbers,Custom" ValidChars=".">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Waist(Inch)
                                </td>
                                <td width="450px">
                                    <asp:TextBox runat="server" class="control-edit" ID="txtWaist" MaxLength="8" Width="304px" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FlttxtWaist" runat="server" TargetControlID="txtWaist"
                                        FilterType="Numbers,Custom" ValidChars=".">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr id="trUniform1" runat="server" visible="false">
                                <td>
                                    Shoe Size
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtShoeSize" MaxLength="8" Width="304px" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    T-Shirt Size
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtShirtSize" MaxLength="10"
                                        Width="304px" />
                                </td>
                            </tr>
                            <tr id="trUniform2" runat="server" visible="false">
                                <td>
                                    Cargo Pant Size
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtCargoPantSize" MaxLength="8"
                                        Width="304px" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    Overall Size
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ID="txtOverallSize" MaxLength="8"
                                        Width="304px" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="background-color: #effbef; padding: 2px;" id="fieldsetConfigured"
                        runat="server" visible="false">
                        <legend>Additional Fields</legend>
                        <table border="0" cellpadding="3" style="width: 48%; padding: 0; border-collapse: collapse;">
                            <tr id="trCF1" clientidmode="Static" runat="server" visible="false">
                                <td width="85px">
                                    <asp:Label ID="lblCF1" runat="server" />
                                </td>
                                <td width="450px">
                                    <asp:TextBox runat="server" ID="txtCF1" Style="width: 305px;" MaxLength="100" />
                                </td>
                            </tr>
                            <tr id="trCF2" clientidmode="Static" runat="server" visible="false">
                                <td width="85px">
                                    <asp:Label ID="lblCF2" runat="server" />
                                </td>
                                <td width="450px">
                                    <asp:TextBox runat="server" ID="txtCF2" Style="width: 305px;" MaxLength="100" />
                                </td>
                            </tr>
                            <tr id="trCF3" clientidmode="Static" runat="server" visible="false">
                                <td width="85px">
                                    <asp:Label ID="lblCF3" runat="server" />
                                </td>
                                <td width="450px">
                                    <asp:TextBox runat="server" ID="txtCF3" Style="width: 305px;" MaxLength="100" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div style="margin-top: 15px;">
                        <asp:Button ID="btnCancelScreen2" ClientIDMode="Static" Text="Cancel" runat="server"
                            Style="float: left;" Width="130px" OnClick="BtnCancel_OnClick" />
                        <div style="float: right;">
                            <asp:Button ID="btnBackScreen2" OnClick="btnBackScreen2_OnClick" ClientIDMode="Static"
                                Text="Back" Width="130px" runat="server" />
                            <asp:Button ID="btnSaveScreen2" ClientIDMode="Static" Text="Save" Width="130px" runat="server"
                                OnClick="btnSaveScreen2_OnClick" />
                            <asp:Button ID="btnNext" OnClick="btnNext_OnClick" ClientIDMode="Static" Text="Next"
                                Width="130px" runat="server" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlView_Screen3" runat="server" Visible="false" Width="100%" Style="min-height: 830px">
                    <fieldset style="background-color: #effbef; padding: 2px; width: 99.5%;">
                        <legend>Next of Kin</legend>
                        <asp:HiddenField ID="hdnIsNOKMandatory" Value="0" ClientIDMode="Static" runat="server" />
                        <table cellpadding="3" style="width: 77%; padding: 0; border-collapse: collapse;"
                            id="tbl_NOK">
                            <tr>
                                <td width="125px">
                                    First Name<span class="mandatory">*</span>
                                </td>
                                <td width="335px">
                                    <asp:TextBox runat="server" class="control-edit required" ID="txtNOKFirstName" ClientIDMode="Static"
                                        MaxLength="100" Width="303px" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtNOKFirstName"
                                        ValidChars=" ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                </td>
                                <td width="135px">
                                    Surname<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit required" ID="txtNOKSurname" ClientIDMode="Static"
                                        MaxLength="100" Width="304px" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtNOKSurname"
                                        ValidChars=" ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Relationship<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpNOKRelationship" ClientIDMode="Static" class="control-edit required"
                                        Width="305px">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                        <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                        <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                        <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                        <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                        <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER-IN-LAW" Value="BROTHER-IN-LAW"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Phone Number<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit required" ClientIDMode="Static" ID="txtNOKPhoneNumber"
                                        MaxLength="50" Width="304px" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender7"
                                        TargetControlID="txtNOKPhoneNumber" FilterType="Numbers,Custom" ValidChars="+,-,,">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date of Birth<span class="mandatory" id="spnNOKDOB" runat="server">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" class="control-edit" ClientIDMode="Static" ID="txtNOKDOB"
                                        MaxLength="20" Width="300px" autocomplete="off" />
                                    <ajaxToolkit:CalendarExtender ID="CENOKDOB" runat="server" TargetControlID="txtNOKDOB"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                </td>
                                <td id="tdlblSSNNOK" runat="server" visible="false">
                                    SSN<span class="mandatory">*</span>
                                </td>
                                <td width="450px" id="tdtxtSSNNOK" runat="server" visible="false">
                                    <asp:TextBox runat="server" class="control-edit required" ClientIDMode="Static" ID="txtNOKSSN"
                                        MaxLength="11" Width="304px" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender8"
                                        TargetControlID="txtNOKSSN" FilterMode="ValidChars" FilterType="Numbers,Custom"
                                        ValidChars="-">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr height="20px">
                                <td colspan="5">
                                </td>
                            </tr>
                            <tr id="trNOKUSAddress" runat="server" visible="false">
                                <td width="125px">
                                    Address Line 1<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNOKAddressline1" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="100" Width="300" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    Address Line 2
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNOKAddressline2" ClientIDMode="Static" class="control-edit"
                                        MaxLength="100" Width="305" />
                                </td>
                            </tr>
                            <tr id="trNOKUSCityState" runat="server" visible="false">
                                <td>
                                    City<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNOKCity" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="100" Width="300" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    State/Province/Region<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNOKState" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="100" Width="305" />
                                </td>
                            </tr>
                            <tr id="trNOKUSCountryZip" runat="server" visible="false">
                                <td>
                                    Country<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="drpNOKUSCountry" class="control-edit required"
                                        Width="302px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Zip Code<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNOKZipCode" ClientIDMode="Static" class="control-edit required"
                                        MaxLength="50" Width="305" />
                                </td>
                            </tr>
                            <tr id="trNOKInternational" runat="server" visible="false">
                                <td>
                                    Address<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <textarea type="text" id="txtNOKAddress" clientidmode="Static" textmode="MultiLine"
                                        maxlength="250" class="control-edit required" style="height: 60px; width: 305px;"
                                        runat="server" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="text-align: right;">
                                    Mark as beneficiary?
                                </td>
                                <td>
                                </td>
                                <td colspan="2">
                                    <asp:RadioButtonList runat="server" ID="rblstMarkasbenficiary" ClientIDMode="Static"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="1" />
                                        <asp:ListItem Text="No" Value="0" Selected="True" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdnNOKID" Value="0" runat="server" />
                    </fieldset>
                    <fieldset style="background-color: #effbef; width: 99.5%; min-height: 45px;">
                        <legend>Dependents / Beneficiaries</legend>
                        <asp:UpdatePanel ID="UpDependents" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div style="width: 60%">
                                    <asp:Button Text="Add New" Style="margin: 5px 0 5px 5px;" Width="130px" ID="btnAddNewDependent"
                                        ClientIDMode="Static" ToolTip="Add New Dependent" runat="server" />
                                    <asp:Repeater runat="server" ID="rptDependents" OnItemDataBound="rptDependents_OnItemDataBound"
                                        Visible="false">
                                        <HeaderTemplate>
                                            <table id="gvDependents" class="gridmain-css" width="85%" style="margin: 5px 0 10px 5px;">
                                                <tr class="HeaderStyle-css" style="height: 25px;">
                                                    <th scope="col" align="center" width="200px">
                                                        Name
                                                    </th>
                                                    <th scope="col" align="center" width="50px">
                                                        Relationship
                                                    </th>
                                                    <th scope="col" align="center" width="50px">
                                                        Beneficiary?
                                                    </th>
                                                    <th scope="col" align="center" width="50px">
                                                        Action
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr rel='<%#Eval("[ID]")%>' class="RowStyle-css trdependent">
                                                <td>
                                                    &nbsp;&nbsp;<%#Eval("FullName")%>&nbsp;
                                                    <%#Eval("Surname")%>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("Relationship")%>
                                                </td>
                                                <td align="center">
                                                    <%# (Convert.ToString(Eval("IsBeneficiary"))) == "1" ? "Yes" : "No"%>
                                                </td>
                                                <td align="center">
                                                    <asp:Image ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                        ToolTip="Edit Dependent" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                        Width="16px" Style="cursor: pointer; margin-right: 5px;" />
                                                    <asp:Image ID="ImgDelete" CssClass="deleteDependent" runat="server" Text="Delete"
                                                        OnClientClick="return confirm('Are you sure, you want to delete?')" rel='<%#Eval("[ID]")%>'
                                                        ToolTip="Delete Dependent" ImageUrl="~/Images/delete.png" Height="16px" Width="16px">
                                                    </asp:Image>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr rel='<%#Eval("[ID]")%>' class="AlternatingRowStyle-css trdependent">
                                                <td>
                                                    &nbsp;&nbsp;<%#Eval("FullName")%>&nbsp;
                                                    <%#Eval("Surname")%>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("Relationship")%>
                                                </td>
                                                <td align="center">
                                                    <%# (Convert.ToString(Eval("IsBeneficiary"))) == "1" ? "Yes" : "No"%>
                                                </td>
                                                <td align="center">
                                                    <asp:Image ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                        ToolTip="Edit Dependent" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                        Width="16px" Style="cursor: pointer; margin-right: 5px;" />
                                                    <asp:Image ID="ImgDelete" CssClass="deleteDependent" runat="server" Text="Delete"
                                                        OnClientClick="return confirm('Are you sure, you want to delete?')" rel='<%#Eval("[ID]")%>'
                                                        ToolTip="Delete Dependent" ImageUrl="~/Images/delete.png" Height="16px" Width="16px">
                                                    </asp:Image>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                                <!--Add new dependendent starts here-->
                                <div id="divAddNewDependent" style="display: none; font-family: Tahoma; text-align: left;
                                    font-size: 12px; color: Black; width: 810px; top: 250px">
                                    <fieldset style="background-color: #effbef; width: 98%; margin: 10px 0 2px 5px">
                                        <legend>Dependent Details</legend>
                                        <table border="0" cellpadding="3" style="width: 800px; padding: 0; border-collapse: collapse;">
                                            <tr>
                                                <td width="225px">
                                                    First Name<span class="mandatory">*</span>
                                                </td>
                                                <td width="335px">
                                                    <asp:TextBox runat="server" class="control-edit required" ID="txtDependentFristName"
                                                        MaxLength="100" Width="250px" ClientIDMode="Static" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtDependentFristName"
                                                        ValidChars=" ">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:HiddenField runat="server" ID="hdnDependentID" Value="0" ClientIDMode="Static" />
                                                </td>
                                                <td width="255px">
                                                    Surname<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" class="control-edit required" ID="txtDependentSurname"
                                                        MaxLength="100" Width="250px" ClientIDMode="Static" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtDependentSurname"
                                                        ValidChars=" ">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Relationship<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="drpDependentRelationship" ClientIDMode="Static"
                                                        class="control-edit required" Width="250px">
                                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                                        <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                                        <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                                        <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                                        <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                                        <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                                        <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                                        <asp:ListItem Text="BROTHER-IN-LAW" Value="BROTHER-IN-LAW"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Phone Number<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" class="control-edit required" ClientIDMode="Static" ID="txtDependentPhoneNumber"
                                                        MaxLength="50" Width="250px" />
                                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6"
                                                        TargetControlID="txtDependentPhoneNumber" FilterType="Numbers,Custom" ValidChars="+,-,,">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date of Birth<span class="mandatory" id="spnDependentDOB" runat="server">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" class="control-edit" ClientIDMode="Static" ID="txtDependentDOB"
                                                        MaxLength="80" Width="250px" />
                                                    <ajaxToolkit:CalendarExtender ID="CEDependentDOB" runat="server" TargetControlID="txtDependentDOB"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td runat="server" visible="false" id="tdlblDependentSSN">
                                                    SSN<span class="mandatory">*</span>
                                                </td>
                                                <td runat="server" visible="false" id="tdtxtDependentSSN">
                                                    <asp:TextBox runat="server" class="control-edit required" ID="txtDependentSSN" ClientIDMode="Static"
                                                        MaxLength="11" Width="250px" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'" />
                                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9"
                                                        TargetControlID="txtDependentSSN" FilterMode="ValidChars" FilterType="Numbers,Custom"
                                                        ValidChars="-">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr height="30px">
                                                <td colspan="5">
                                                </td>
                                            </tr>
                                            <tr id="trDependentUSAddress" runat="server" visible="false">
                                                <td>
                                                    Address Line 1<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDependentAddressline1" ClientIDMode="Static" class="control-edit required"
                                                        MaxLength="100" Width="250px" />
                                                </td>
                                                <td>
                                                    Address Line 2
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDependentAddressline2" ClientIDMode="Static" class="control-edit"
                                                        MaxLength="100" Width="250px" />
                                                </td>
                                            </tr>
                                            <tr id="trDependentUSCityState" runat="server" visible="false">
                                                <td>
                                                    City<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDependentCity" ClientIDMode="Static" class="control-edit required"
                                                        MaxLength="100" Width="250px" />
                                                </td>
                                                <td>
                                                    State/Province/Region<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDependentState" ClientIDMode="Static" class="control-edit required"
                                                        MaxLength="100" Width="250px" />
                                                </td>
                                            </tr>
                                            <tr id="trDependentUSCountryZip" runat="server" visible="false">
                                                <td>
                                                    Country<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="drpDependentUSCountry" ClientIDMode="Static"
                                                        class="control-edit required" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Zip Code<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDependentZipCode" ClientIDMode="Static" class="control-edit required"
                                                        MaxLength="50" Width="250px" />
                                                </td>
                                            </tr>
                                            <tr id="trDependentInternational" runat="server" visible="false">
                                                <td>
                                                    Address<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <textarea type="text" id="txtDependentAddress" clientidmode="Static" textmode="MultiLine"
                                                        maxlength="250" class="control-edit required" style="height: 60px; width: 250px;"
                                                        runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align: right;">
                                                    Mark as beneficiary?
                                                </td>
                                                <td colspan="2">
                                                    <asp:RadioButtonList runat="server" ID="rblstDependentBeneficiary" ClientIDMode="Static"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Yes" Value="1" />
                                                        <asp:ListItem Text="No" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <div style="margin: 0 0 5px; text-align: center;">
                                        <asp:Button Text="Save" ID="btnSaveDependents" Width="130px" Style="margin: 5px;"
                                            ClientIDMode="Static" runat="server" OnClick="btnSaveDependents_OnClick" />
                                        <asp:Button Text="Cancel" ID="btnCancelDependents" Width="130px" ClientIDMode="Static"
                                            runat="server" />
                                    </div>
                                </div>
                                <!--Add new dependendent starts end-->
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSaveDependents" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </fieldset>
                    <div style="height: 370px;">
                        <iframe id="FramePrejoining" runat="server" width="100%" style="margin-top: 10px;
                            border: 0;" height="100%"></iframe>
                    </div>
                    <div style="margin-top: 25px; height: 25px;">
                        <asp:Button ID="btnCancelScreen3" ClientIDMode="Static" Text="Cancel" runat="server"
                            Style="float: left;" Width="130px" OnClick="BtnCancel_OnClick" />
                        <div style="float: right;">
                            <asp:Button ID="btnBackScreen3" OnClick="btnBackScreen3_OnClick" ClientIDMode="Static"
                                Text="Back" Width="130px" runat="server" />
                            <asp:Button ID="btnSaveScreen3" OnClick="btnSaveScreen3_OnClick" ClientIDMode="Static"
                                Text="Save" Width="130px" runat="server" />
                            <asp:Button ID="btnSaveExitScreen3" ClientIDMode="Static" Text="Save & Exit" Width="130px"
                                runat="server" OnClick="btnSaveExitScreen3_OnClick" />
                            <asp:Button ID="btnQuikApproval" ClientIDMode="Static" Text="Quick Approval" Width="130px"
                                runat="server" OnClick="btnQuikApproval_OnClick" />
                        </div>
                    </div>
                </asp:Panel>
                <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
                    border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                    left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
                    <div class="content">
                        <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
                    </div>
                </div>
                <asp:HiddenField ID="hdnAddressFromat" ClientIDMode="Static" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField_CrewID" ClientIDMode="Static" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField_CrewStaffCode" ClientIDMode="Static" runat="server"
                    Value="0" />
                <asp:HiddenField ID="hdnLoggedInUserId" ClientIDMode="Static" runat="server" Value="0" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            var strDateFormat = "<%= DateFormat %>";
            var TodayDateFormat = '<%= UDFLib.DateFormatMessage() %>';

            $("body").on("click", "#BtnCancel,#btnCancelScreen2,#btnCancelScreen3,#btnBackScreen2,#btnBackScreen3", function () {
                if (!confirm("Are you sure you want to leave this page? Any unsaved data will be lost")) {
                    return false;
                }
            });

            $("body").on("click", "#btnAddNewDependent", function () {
                ClearDependentControls();
                showModal('divAddNewDependent', false);
                $("#divAddNewDependent_dvModalPopupTitle").text("Add/Edit Dependents");
                return false;
            });

            function ClearDependentControls() {
                $("#txtDependentFristName").val('');
                $("#txtDependentSurname").val('');
                $("#drpDependentRelationship").val('0');
                $("#txtDependentPhoneNumber").val('');
                $("#txtDependentSurname").val('');
                $("#hdnDependentID").val('0');
                if ($("#txtDependentSSN").length) {
                    $("#txtDependentSSN").val('');
                }

                $("#txtDependentDOB").val('');
                if ($("#<%=trDependentUSAddress.ClientID %>").length) {
                    $("#txtDependentAddressline1").val('');
                    $("#txtDependentAddressline2").val('');
                    $("#txtDependentCity").val('');
                    $("#txtDependentState").val('');
                    $("#txtDependentCountry").val('0');
                    $("#txtDependentZipCode").val('');
                }
                if ($("#<%=trDependentInternational.ClientID %>").length) {
                    $("#txtDependentAddress").val('');
                    $("#drpDependentCountry").val('0');
                }
                $("#drpDependentUSCountry").val('0');
                $("#rblstDependentBeneficiary_1").prop("checked", true);
                $("#rblstDependentBeneficiary_0").prop("checked", false);
            }

            $("body").on("click", "#btnCancelDependents", function () {
                $("#divAddNewDependent_dvModalPopupCloseButton").click();
                ClearDependentControls();
                return false;
            });

            ///If US Visa YES
            $("body").on("click", "#rblstValidUSVisa_0", function () {
                $("#spnUSVisaNumber").show();
                $("#spnUSVisaIssueDate").show();
                $("#spnUSVisaExpiryDate").show();
                $("#txtUSVisaNumber").addClass("required");
                $("#txtUSVisaIssueDate").addClass("required");
                $("#txtUSVisaExpiryDate").addClass("required");
                $("#txtUSVisaNumber").prop("disabled", false);
                $("#txtUSVisaIssueDate").prop("disabled", false);
                $("#txtUSVisaExpiryDate").prop("disabled", false);
            });

            ///If US Visa NO
            $("body").on("click", "#rblstValidUSVisa_1", function () {
                $("#spnUSVisaNumber").hide();
                $("#spnUSVisaIssueDate").hide();
                $("#spnUSVisaExpiryDate").hide();
                $("#txtUSVisaNumber").val('');
                $("#txtUSVisaNumber").removeClass("required");
                $("#txtUSVisaIssueDate").val('');
                $("#txtUSVisaIssueDate").removeClass("required");
                $("#txtUSVisaExpiryDate").val('');
                $("#txtUSVisaExpiryDate").removeClass("required");
                $("#txtUSVisaNumber").prop("disabled", true);
                $("#txtUSVisaIssueDate").prop("disabled", true);
                $("#txtUSVisaExpiryDate").prop("disabled", true);
            });

            $("body").on("change", "#drpSchool", function () {
                if (parseInt($("#drpSchool option:selected").val()) > 0) {
                    $("#drpSchoolYearGraduated").prop("disabled", false);
                }
                else {
                    $("#drpSchoolYearGraduated").val('0')
                    $("#drpSchoolYearGraduated").prop("disabled", true);
                }
            });

            $("body").on("change", "#drpUnion", function () {
                BindUnionBranch();
            });

            /// Screen 1 Save
            $("body").on("click", "#BtnCreateNewCrew", function () {

                var Msg = "", AddressMSg = "";

                if ($.trim($("#txtName").val()) == "") {
                    Msg += "Enter Name\n";
                }
                if ($.trim($("#txtPlaceOfBirth").val()) == "") {
                    Msg += "Enter Place of Birth\n";
                }

                if ($.trim($("#txtDOB").val()) == "") {
                    Msg += "Enter Date of Birth\n";
                }
                else {
                    if (IsInvalidDate($("#txtDOB").val(), strDateFormat)) {
                        Msg += "Enter valid Date of Birth" + TodayDateFormat + "\n";
                    }
                    else if (new Date(DateAsFormat($("#txtDOB").val(), strDateFormat)) > new Date()) {
                        Msg += "Date of Birth cannot be future date\n";
                    }
                }

                if ($("#ddlPD_Race").length > 0) {
                    if ($("#ddlPD_Race option:selected").val() == "0") {
                        Msg += "Select Race\n";
                    }
                }

                if ($("#ddlPD_Nationality option:selected").val() == "0") {
                    Msg += "Select Nationality\n";
                }

                if ($("#txtSSN").length) {
                    if (IsSSNValid($.trim($("#txtSSN").val()))) {
                        Msg += "Enter valid SSN Number\n";
                    }
                }

                if ($("#hdnAddressFromat").val() == "0") {//US format
                    var IsAddress = true;
                    if ($.trim($("#txtAddressline1").val()) == "") {
                        Msg += "Enter Address Line 1\n";
                        IsAddress = false;
                    }
                    if ($.trim($("#txtCity").val()) == "") {
                        Msg += "Enter City\n";
                        IsAddress = false;
                    }
                    if ($.trim($("#txtState").val()) == "") {
                        Msg += "Enter State/Province/Region\n";
                        IsAddress = false;
                    }
                    if ($("#drpUSCountry option:selected").val() == "0") {
                        Msg += "Select Country\n";
                        IsAddress = false;
                    }
                    if ($.trim($("#txtZipCode").val()) == "") {
                        Msg += "Enter Zip Code\n";
                        IsAddress = false;
                    }

                    if (IsAddress && $("#hdnAddressFromat").val() == "0") {
                        if (parseInt($("#drpUSCountry option:selected").val()) == parseInt($("#<%=hdnUSCountryID.ClientID %>").val())) {////If selected country is US
                            if (parseInt($("#HiddenField_CrewID").val()) > 0) { ///While updating the Crew
                                AddressMSg = USAddressValidation($("#txtAddressline1").val(), $("#txtAddressline2").val(), $("#txtCity").val(), $("#txtState").val(), $("#txtZipCode").val(), $("#drpUSCountry option:selected").text(), "Crew", $.trim($("#txtName").val()) + " " + $.trim($("#txtSurname").val()), $.trim($("#<%=hdnClientName.ClientID %>").val()), "Edit", $.trim($("#HiddenField_CrewStaffCode").val()), $.trim($("#txtDOB").val()), $("#drpAppliedRank option:selected").text());
                            }
                            else {//While Adding new crew
                                AddressMSg = USAddressValidation($("#txtAddressline1").val(), $("#txtAddressline2").val(), $("#txtCity").val(), $("#txtState").val(), $("#txtZipCode").val(), $("#drpUSCountry option:selected").text(), "Crew", $.trim($("#txtName").val()) + " " + $.trim($("#txtSurname").val()), $.trim($("#<%=hdnClientName.ClientID %>").val()), "Add", "", $.trim($("#txtDOB").val()), $("#drpAppliedRank option:selected").text());
                            }

                            if (AddressMSg == "Error") {
                                alert("User address validation failed. User cannot be created/updated at the moment");
                                return false;
                            }
                        }
                    }
                }
                else {
                    if ($.trim($("#txtAddress").val()) == "") {
                        Msg += "Enter Address\n";
                    }
                }

                if ($.trim($("#ctl00_MainContent_txtPD_Airport_txtSearchAirPortList").val()) != "Type to Search, IATA Code, Airport Name") {
                    if ($("#ctl00_MainContent_txtPD_Airport_hdn_SelectedValue").val() == "") {
                        Msg += "Invalid Nearest Airport\n";
                    }
                }

                if ($.trim($("#ctl00_MainContent_txtPD_Airport_txtSearchAirPortList").val()) == "Type to Search, IATA Code, Airport Name" || $.trim($("#ctl00_MainContent_txtPD_Airport_txtSearchAirPortList").val()) == "") {
                    $("#ctl00_MainContent_txtPD_Airport_hdn_SelectedValue").val(0);
                }

                if ($.trim($("#txtEmailAddress").val()) == "") {
                    Msg += "Enter Email Address\n";
                }
                else if ($.trim($("#txtEmailAddress").val()) != "") {
                    if (!ValidateEmail($.trim($("#txtEmailAddress").val()))) {
                        Msg += "Invalid Email Address \n";
                    }
                }

                if ($.trim($("#txtMobileNumber").val()) == "") {
                    Msg += "Enter Mobile Number\n";
                }

                if ($("#drpAppliedRank option:selected").val() == "0") {
                    Msg += "Select Applied Rank\n";
                }

                if ($("#drpManningOffice option:selected").val() == "0") {
                    Msg += "Select Manning Office\n";
                }


                if ($.trim($("#txtAvailabilityDate").val()) == "") {
                    Msg += "Enter Availability Date\n";
                }
                else if (IsInvalidDate($("#txtAvailabilityDate").val(), strDateFormat)) {
                    Msg += "Enter valid Availability Date" + TodayDateFormat + "\n";
                }

                if ($("#txtHireDate").length) {
                    if ($.trim($("#txtHireDate").val()) == "") {
                        Msg += "Enter Hire Date\n";
                    }
                    else if (IsInvalidDate($("#txtHireDate").val(), strDateFormat)) {
                        Msg += "Enter valid Hire Date" + TodayDateFormat + "\n";
                    }
                }

                if ($("#drpUnion").length) {
                    if ($("#drpUnion option:selected").val() == "0") {
                        Msg += "Select Union\n";
                    }
                    if (parseInt($("#drpUnion option:selected").val()) > 0) {
                        if ($("#drpUnionBranch option:selected").val() == "0") {
                            Msg += "Select Union Branch \n";
                        }
                    }
                    if ($("#drpUnionBook option:selected").val() == "0") {
                        Msg += "Select Union Book\n";
                    }
                }

                if ($("#drpVeteranStatus").length) {
                    if ($("#drpVeteranStatus option:selected").val() == "0") {
                        Msg += "Select Veteran Status \n";
                    }
                }

                if (Msg != "") {
                    alert(Msg + "\n" + AddressMSg);
                    return false;
                }
                if (AddressMSg != "") {
                    alert(AddressMSg);
                    return false;
                }
            });

            function ValidateEmail(email) {
                var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                return expr.test(email);
            };

            ///Screen 2 validation starts here
            $("body").on("change", "#drpNaturaliztion", function () {
                if ($("#drpNaturaliztion option:selected").val() == "1") {
                    $("#txtNaturaliztionDate").prop("disabled", false);
                    $("#txtNaturaliztionDate").addClass("required");
                    $("#spnNaturaliztionDate").show();
                }
                else {
                    $("#txtNaturaliztionDate").prop("disabled", true);
                    $("#txtNaturaliztionDate").val('');
                    $("#txtNaturaliztionDate").removeClass("required");
                    $("#spnNaturaliztionDate").hide();
                }
            });



            function ValidateDocumentsDate(fieldName, IssueDate, ExpiryDate) {
                var Msg = "";
                if (IsInvalidDate(IssueDate, strDateFormat)) {
                    return "Enter valid " + fieldName + " issue date" + TodayDateFormat + "\n";
                }
                if (IsInvalidDate(ExpiryDate, strDateFormat)) {
                    return "Enter valid " + fieldName + " expiry date" + TodayDateFormat + "\n";
                }
                if (DateAsFormat(IssueDate, strDateFormat) > new Date()) {
                    return fieldName + " issue date cannot be future date\n";
                }
                if (DateAsFormat(IssueDate, strDateFormat) > DateAsFormat(ExpiryDate, strDateFormat)) {
                    return fieldName + " issue date should be less than " + fieldName + " expiry date\n";
                }
                return Msg;
            }

            $("body").on("change", "#drpUnionBranch", function () {
                $("#hdnUnionBranchId").val($("#drpUnionBranch option:selected").val());
            });


            function validatePassportSeaman(Control) {
                var Data = $("#tr" + Control + " input[type='text']");
                var returnvalue = false;
                var valExists = false;
                var Cnt = 0;

                Data.each(function (index) {
                    var Type = $("#tr" + Control + " input[type='text']")[index].localName;
                    if (Type == "input") {
                        if ($.trim($("#tr" + Control + " input[type='text']")[index].value) != "")
                            Cnt += 1;
                    }
                });

                if (Cnt < $("#tr" + Control + " input[type='text']").length && Cnt != 0)
                    valExists = true;
                else
                    valExists = false;

                return valExists;
            }

            $("body").on("click", "#btnSaveScreen2", function () {
                try {


                    var Screen2Msg = "";
                    RemoveMandatory();
                    //Passport validation starts here
                    if ($("#<%=hdnIsPassportMandatory.ClientID %>").val() == "1") {
                        if ($.trim($("#txtPassport").val()) == "" || $.trim($("#txtPassportPlaceofIssue").val()) == "" || $.trim($("#txtPassportIssueDate").val()) == "" || $.trim($("#txtPassportExpiryDate").val()) == "") {
                            Screen2Msg += "Enter passport details\n";
                        }
                        else {
                            if (validatePassportSeaman("Passport")) {
                                Screen2Msg += "Enter valid passport details\n";
                            }
                            else {
                                if ($.trim($("#txtPassportIssueDate").val()) != "" || $.trim($("#txtPassportExpiryDate").val()) != "") {
                                    var IsPvalid = true;
                                    if ($.trim($("#txtPassportIssueDate").val()) != "" && IsInvalidDate($.trim($("#txtPassportIssueDate").val()), strDateFormat)) {
                                        Screen2Msg += "Enter valid Passport Issue Date" + TodayDateFormat + "\n";
                                        IsPvalid = false;
                                    }
                                    if ($.trim($("#txtPassportExpiryDate").val()) != "" && IsInvalidDate($.trim($("#txtPassportExpiryDate").val()), strDateFormat)) {
                                        Screen2Msg += "Enter valid Passport Expiry Date" + TodayDateFormat + "\n";
                                        IsPvalid = false;
                                    }

                                    if (IsPvalid) {
                                        var PassportMSg = ValidateDocumentsDate("Passport", $.trim($("#txtPassportIssueDate").val()), $.trim($("#txtPassportExpiryDate").val()));
                                        if (PassportMSg != "") {
                                            Screen2Msg += PassportMSg;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else {
                        if (validatePassportSeaman("Passport")) {
                            Screen2Msg += "Enter valid passport details\n";
                        }
                        else {
                            if ($.trim($("#txtPassportIssueDate").val()) != "" || $.trim($("#txtPassportExpiryDate").val()) != "") {
                                var IsPvalid = true;
                                if ($.trim($("#txtPassportIssueDate").val()) != "" && IsInvalidDate($.trim($("#txtPassportIssueDate").val()), strDateFormat)) {
                                    Screen2Msg += "Enter valid Passport Issue Date" + TodayDateFormat + "\n";
                                    IsPvalid = false;
                                }
                                if ($.trim($("#txtPassportExpiryDate").val()) != "" && IsInvalidDate($.trim($("#txtPassportExpiryDate").val()), strDateFormat)) {
                                    Screen2Msg += "Enter valid Passport Expiry Date" + TodayDateFormat + "\n";
                                    IsPvalid = false;
                                }
                                if (IsPvalid) {
                                    var PassportMSg = ValidateDocumentsDate("Passport", $.trim($("#txtPassportIssueDate").val()), $.trim($("#txtPassportExpiryDate").val()));
                                    if (PassportMSg != "") {
                                        Screen2Msg += PassportMSg;
                                    }
                                }
                            }
                        }
                    }
                    //Passport validation ends here

                    //Seaman validation starts here
                    if ($("#trSeaman").length) {
                        if ($("#<%=hdnIsSeamanMandatory.ClientID %>").val() == "1") {

                            if ($.trim($("#txtSeaman").val()) == "" || $.trim($("#txtSeamanPlaceofIssue").val()) == "" || $.trim($("#txtSeamanIssueDate").val()) == "" || $.trim($("#txtSeamanExpiryDate").val()) == "") {
                                Screen2Msg += "Enter seaman details\n";
                            }
                            else {
                                if (validatePassportSeaman("Seaman")) {
                                    Screen2Msg += "Enter valid seaman details\n";
                                }
                                else {
                                    if ($.trim($("#txtSeamanIssueDate").val()) != "" || $.trim($("#txtSeamanExpiryDate").val()) != "") {
                                        var IsSvalid = true;
                                        if ($.trim($("#txtSeamanIssueDate").val()) != "" && IsInvalidDate($.trim($("#txtSeamanIssueDate").val()), strDateFormat)) {
                                            Screen2Msg += "Enter valid Seaman Issue Date" + TodayDateFormat + "\n";
                                            IsSvalid = false;
                                        }
                                        if ($.trim($("#txtSeamanExpiryDate").val()) != "" && IsInvalidDate($.trim($("#txtSeamanExpiryDate").val()), strDateFormat)) {
                                            Screen2Msg += "Enter valid Seaman Expiry Date" + TodayDateFormat + "\n";
                                            IsSvalid = false;
                                        }
                                        if (IsSvalid) {
                                            var PassportMSg = ValidateDocumentsDate("Seaman", $.trim($("#txtSeamanIssueDate").val()), $.trim($("#txtSeamanExpiryDate").val()));
                                            if (PassportMSg != "") {
                                                Screen2Msg += PassportMSg;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else {
                            if (validatePassportSeaman("Seaman")) {
                                Screen2Msg += "Enter valid seaman details\n";
                            }
                            else {
                                if ($.trim($("#txtSeamanIssueDate").val()) != "" || $.trim($("#txtSeamanExpiryDate").val()) != "") {
                                    var IsSvalid = true;
                                    if ($.trim($("#txtSeamanIssueDate").val()) != "" && IsInvalidDate($.trim($("#txtSeamanIssueDate").val()), strDateFormat)) {
                                        Screen2Msg += "Enter valid Seaman Issue Date" + TodayDateFormat + "\n";
                                        IsSvalid = false;
                                    }
                                    if ($.trim($("#txtSeamanExpiryDate").val()) != "" && IsInvalidDate($.trim($("#txtSeamanExpiryDate").val()), strDateFormat)) {
                                        Screen2Msg += "Enter valid Seaman Expiry Date" + TodayDateFormat + "\n";
                                        IsSvalid = false;
                                    }
                                    if (IsSvalid) {
                                        var PassportMSg = ValidateDocumentsDate("Seaman", $.trim($("#txtSeamanIssueDate").val()), $.trim($("#txtSeamanExpiryDate").val()));
                                        if (PassportMSg != "") {
                                            Screen2Msg += PassportMSg;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Seaman validation ends here

                    //Check if MMC control exists
                    if ($("#trMMCNumber").length > 0) {
                        if ($.trim($("#txtMMCNumber").val()) == "" || $.trim($("#txtMMCPlaceofIssue").val()) == "" || $.trim($("#txtMMCIssueDate").val()) == "" || $.trim($("#txtMMCExpiryDate").val()) == "") {
                            Screen2Msg += "Enter MMC details\n";
                        }
                        else {
                            var MMCMSg = ValidateDocumentsDate("MMC Number", $.trim($("#txtMMCIssueDate").val()), $.trim($("#txtMMCExpiryDate").val()));
                            if (MMCMSg != "") {
                                Screen2Msg += MMCMSg;
                            }
                        }
                    }

                    //Check if TWIC control exists
                    if ($("#trTWICNumber").length > 0) {
                        if ($.trim($("#txtTWICNumber").val()) == "" || $.trim($("#txtTWICPlaceofIssue").val()) == "" || $.trim($("#txtTWICIssueDate").val()) == "" || $.trim($("#txtTWICExpiryDate").val()) == "") {
                            Screen2Msg += "Enter TWIC details\n";
                        }
                        else {
                            var TWICMSg = ValidateDocumentsDate("TWIC Number", $.trim($("#txtTWICIssueDate").val()), $.trim($("#txtTWICExpiryDate").val()));
                            if (TWICMSg != "") {
                                Screen2Msg += TWICMSg;
                            }
                        }
                    }

                    //validation for Valid US Visa? 
                    if ($("#rblstValidUSVisa_0").prop("checked")) {
                        if ($.trim($("#txtUSVisaNumber").val()) == "" || $.trim($("#txtUSVisaIssueDate").val()) == "" || $.trim($("#txtUSVisaExpiryDate").val()) == "") {
                            Screen2Msg += "Enter valid US visa details\n";
                        }
                        else {
                            var TWICMSg = ValidateDocumentsDate("US Visa", $.trim($("#txtUSVisaIssueDate").val()), $.trim($("#txtUSVisaExpiryDate").val()));
                            if (TWICMSg != "") {
                                Screen2Msg += TWICMSg;
                            }
                        }
                    }

                    if ($("#trSchool").length > 0) {
                        if ($("#drpSchool option:selected").val() != "0") {
                            if ($("#drpSchoolYearGraduated option:selected").val() == "0") {
                                Screen2Msg += "Select year graduated\n";
                            }
                        }
                    }

                    if ($("#trNaturalization").length > 0) {
                        if ($("#drpNaturaliztion option:selected").val() == "1") {
                            if ($.trim($("#txtNaturaliztionDate").val()) == "") {
                                Screen2Msg += "Select naturalization date\n";
                            }
                            else if (IsInvalidDate($.trim($("#txtNaturaliztionDate").val()), strDateFormat)) {
                                Screen2Msg += "Invalid naturalization date" + TodayDateFormat + "\n";
                            }
                        }
                    }

                    if (Screen2Msg != "") {
                        alert(Screen2Msg);
                        return false;
                    }
                } catch (e) {
                    RemoveMandatory();
                }
            });
            ///Screen 2 validation ends here


            ///Dependents validations starts here
            $("body").on("click", "#btnSaveDependents", function () {
                var DependentsMSg = "", AddressMSg = "";

                if ($.trim($("#txtDependentFristName").val()) == "") {
                    DependentsMSg += "Enter First Name\n";
                }
                if ($.trim($("#txtDependentSurname").val()) == "") {
                    DependentsMSg += "Enter Surname\n";
                }
                if ($("#drpDependentRelationship option:selected").val() == "0") {
                    DependentsMSg += "Select Relationship\n";
                }
                if ($.trim($("#txtDependentPhoneNumber").val()) == "") {
                    DependentsMSg += "Enter Phone Number \n";
                }

                if ($("#txtDependentSSN").length) {

                    if ($.trim($("#txtDependentDOB").val()) == "") {
                        DependentsMSg += "Enter Date of Birth\n";
                    }

                    if (IsSSNValid($.trim($("#txtDependentSSN").val()))) {
                        DependentsMSg += "Enter valid SSN Number\n";
                    }
                }

                if ($.trim($("#txtDependentDOB").val()) != "") {
                    if (IsInvalidDate($("#txtDependentDOB").val(), strDateFormat)) {
                        DependentsMSg += "Enter valid Date of Birth" + TodayDateFormat + "\n";
                    }
                    else if (new Date(DateAsFormat($("#txtDependentDOB").val(), strDateFormat)) > new Date()) {
                        DependentsMSg += "Date of Birth cannot be future date\n";
                    }
                }

                var Continue = true;

                if ($("#hdnAddressFromat").val() == "0") {//US format
                    var IsAddress = true;
                    if ($.trim($("#txtDependentAddressline1").val()) == "") {
                        DependentsMSg += "Enter Address Line 1\n";
                        IsAddress = false;
                    }
                    if ($.trim($("#txtDependentCity").val()) == "") {
                        DependentsMSg += "Enter City\n";
                        IsAddress = false;
                    }
                    if ($.trim($("#txtDependentState").val()) == "") {
                        DependentsMSg += "Enter State/Province/Region\n";
                        IsAddress = false;
                    }
                    if ($("#drpDependentUSCountry option:selected").val() == "0") {
                        DependentsMSg += "Select Country\n";
                        IsAddress = false;
                    }
                    if ($.trim($("#txtDependentZipCode").val()) == "") {
                        DependentsMSg += "Enter Zip Code\n";
                        IsAddress = false;
                    }

                    if (IsAddress && $("#hdnAddressFromat").val() == "0") {
                        if (parseInt($("#drpDependentUSCountry option:selected").val()) == parseInt($("#<%=hdnUSCountryID.ClientID %>").val())) {////If selected country is US
                            AddressMSg = USAddressValidation($("#txtDependentAddressline1").val(), $("#txtDependentAddressline2").val(), $("#txtDependentCity").val(), $("#txtDependentState").val(), $("#txtDependentZipCode").val(), $("#drpDependentUSCountry option:selected").text(), "Dependent", "", "", "", "", "", "");
                            if (AddressMSg == "Error") {
                                AddressMSg = "Address was not verified in USPS";
                            }
                            else {
                                Continue = false;
                            }
                        }
                    }
                }
                else {
                    if ($.trim($("#txtDependentAddress").val()) == "") {
                        DependentsMSg += "Enter Address\n";
                    }
                }

                if (DependentsMSg != "") {
                    alert(DependentsMSg + "\n" + AddressMSg);
                    return false;
                }

                if (!Continue && AddressMSg != "") {
                    alert(AddressMSg);
                    return false;
                }

                if (AddressMSg != "") {
                    alert(AddressMSg);
                }
            });
            ///Dependents validations end here

            $("body").on("click", "#btnSaveScreen3,#btnSaveExitScreen3", function () {

                var Screen3Msg = "", AddressMSg = "";
                var IsAddress = true;
                var Continue = true;

                if ($("#hdnIsNOKMandatory").val() == "1") {

                    if ($.trim($("#txtNOKFirstName").val()) == "") {
                        Screen3Msg += "Enter First Name\n";
                    }
                    if ($.trim($("#txtNOKSurname").val()) == "") {
                        Screen3Msg += "Enter Surname\n";
                    }
                    if ($("#drpNOKRelationship option:selected").val() == "0") {
                        Screen3Msg += "Select Relationship\n";
                    }
                    if ($.trim($("#txtNOKPhoneNumber").val()) == "") {
                        Screen3Msg += "Enter Phone Number \n";
                    }

                    if ($.trim($("#txtNOKDOB").val()) != "") {
                        if (IsInvalidDate($.trim($("#txtNOKDOB").val()), strDateFormat)) {
                            Screen3Msg += "Enter valid Date of Birth" + TodayDateFormat + "\n";
                        }
                        else if (new Date(DateAsFormat($("#txtNOKDOB").val(), strDateFormat)) > new Date()) {
                            Screen3Msg += "Date of Birth cannot be future date\n";
                        }
                    }

                    if ($("#txtNOKSSN").length) {
                        if (IsSSNValid($.trim($("#txtNOKSSN").val()))) {
                            Screen3Msg += "Enter valid SSN Number\n";
                        }
                        if ($.trim($("#txtNOKDOB").val()) == "") {
                            Screen3Msg += "Enter Date of Birth\n";
                        }
                    }

                    if ($("#hdnAddressFromat").val() == "0") {//US format
                        if ($.trim($("#txtNOKAddressline1").val()) == "") {
                            Screen3Msg += "Enter Address Line 1\n";
                            IsAddress = false;
                        }
                        if ($.trim($("#txtNOKCity").val()) == "") {
                            Screen3Msg += "Enter City\n";
                            IsAddress = false;
                        }
                        if ($.trim($("#txtNOKState").val()) == "") {
                            Screen3Msg += "Enter State/Province/Region\n";
                            IsAddress = false;
                        }
                        if ($("#drpNOKUSCountry option:selected").val() == "0") {
                            Screen3Msg += "Select Country\n";
                            IsAddress = false;
                        }
                        if ($.trim($("#txtNOKZipCode").val()) == "") {
                            Screen3Msg += "Enter Zip Code\n";
                            IsAddress = false;
                        }
                    }
                    else {
                        if ($.trim($("#txtNOKAddress").val()) == "") {
                            Screen3Msg += "Enter Address\n";
                        }
                    }
                }
                else {
                    if ($("#txtNOKSSN").length) {
                        if ($.trim($("#txtNOKSSN").val()) != "") {
                            if (IsSSNValid($.trim($("#txtNOKSSN").val()))) {
                                Screen3Msg += "Enter valid SSN Number\n";
                            }
                        }
                    }
                    if ($.trim($("#txtNOKDOB").val()) != "") {
                        if (IsInvalidDate($("#txtNOKDOB").val(), strDateFormat)) {
                            Screen3Msg += "Enter valid Date of Birth" + TodayDateFormat + "\n";
                        }
                        else if (new Date(DateAsFormat($("#txtNOKDOB").val(), strDateFormat)) > new Date()) {
                            Screen3Msg += "Date of Birth cannot be future date\n";
                        }
                    }
                }

                if (IsAddress && $("#hdnAddressFromat").val() == "0") {
                    if (parseInt($("#drpNOKUSCountry option:selected").val()) == parseInt($("#<%=hdnUSCountryID.ClientID %>").val())) {////If selected country is US
                        AddressMSg = USAddressValidation($("#txtNOKAddressline1").val(), $("#txtNOKAddressline2").val(), $("#txtNOKCity").val(), $("#txtNOKState").val(), $("#txtNOKZipCode").val(), $("#drpNOKUSCountry option:selected").text(), "NOK", "", "", "", "", "", "");
                        if (AddressMSg == "Error") {
                            AddressMSg = "Address was not verified in USPS";
                        }
                        else {
                            Continue = false;
                        }
                    }
                }

                if (Screen3Msg != "") {
                    alert(Screen3Msg + "\n" + AddressMSg);
                    return false;
                }

                if (!Continue && AddressMSg != "") {
                    alert(AddressMSg);
                    return false;
                }

                if (AddressMSg != "") {
                    alert(AddressMSg);
                }
            });

            $("#gvDependents tr:even").addClass("AlternatingRowStyle-css");
            $("#gvDependents tr:odd").addClass("RowStyle-css");


            $("body").on("click", "#gvDependents .edit", function () {
                var DependentID = $(this).attr("rel");
                $.ajax({
                    type: "POST",
                    url: "AddEditCrewNew.aspx/GetDependentDetails",
                    data: "{ DependentID: '" + DependentID + "',CrewID:'" + $("#HiddenField_CrewID").val() + "',DateFormat:'" + strDateFormat + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    success: function (response) {
                        ClearDependentControls();
                        var objDependent = $.parseJSON(response.d);
                        $("#hdnDependentID").val(objDependent[0].ID);
                        $("#txtDependentFristName").val(objDependent[0].FirstName);
                        $("#txtDependentSurname").val(objDependent[0].Surname);
                        $("#txtDependentPhoneNumber").val(objDependent[0].Phone);
                        if (objDependent[0].DOB.indexOf('1900') <= 0)
                            $("#txtDependentDOB").val(objDependent[0].DateOfBirth.split(' ')[0]);
                        else
                            $("#txtDependentDOB").val('');
                        $("#txtDependentSSN").val(objDependent[0].SSN);
                        $("#drpDependentRelationship option").each(function () {
                            if ($(this).text() == objDependent[0].Relationship) {
                                $(this).attr('selected', 'selected');
                            }
                        });

                        if ($("#hdnAddressFromat").val() == "0")//US format
                        {
                            $("#drpDependentUSCountry").val('0');
                            $("#drpDependentUSCountry option").each(function () {
                                if ($(this).val() == objDependent[0].Country) {
                                    $(this).attr('selected', 'selected');
                                }
                            });

                            $("#txtDependentAddressline1").val(objDependent[0].Address1);
                            $("#txtDependentAddressline2").val(objDependent[0].Address2);
                            $("#txtDependentCity").val(objDependent[0].City);
                            $("#txtDependentState").val(objDependent[0].State);
                            $("#txtDependentZipCode").val(objDependent[0].ZipCode);
                        }
                        else {
                            $("#drpDependentCountry").val('0');
                            $("#drpDependentCountry option").each(function () {
                                if ($(this).val() == objDependent[0].Country) {
                                    $(this).attr('selected', 'selected');
                                }
                            });

                            $("#txtDependentAddress").val(objDependent[0].Address);
                        }
                        if (objDependent[0].IsBeneficiary == "1") {
                            $("#rblstDependentBeneficiary_0").prop("checked", true);
                            $("#rblstDependentBeneficiary_1").prop("checked", false);
                        }
                        else {
                            $("#rblstDependentBeneficiary_1").prop("checked", true);
                            $("#rblstDependentBeneficiary_0").prop("checked", false);
                        }

                        showModal('divAddNewDependent', false);
                        $("#divAddNewDependent_dvModalPopupTitle").text("Add/Edit Dependents");
                    }
                });
            });

            $("body").on("click", "#gvDependents .deleteDependent", function () {
                if (confirm("Are you sure, you want to delete?")) {
                    var DependentID = $(this).attr("rel");
                    $.ajax({
                        type: "POST",
                        url: "AddEditCrewNew.aspx/DeleteDependent",
                        data: "{ DependentID: '" + DependentID + "',UserID:'" + $("#hdnLoggedInUserId").val() + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        success: function (response) {
                            if (response.d == "1") {
                                alert("Dependent deleted successfully");
                                $(".trdependent[rel='" + DependentID + "']").remove();

                                $("#gvDependents tr").removeClass("RowStyle-css");
                                $("#gvDependents tr").removeClass("AlternatingRowStyle-css");
                                $("#gvDependents tr:even").addClass("AlternatingRowStyle-css");
                                $("#gvDependents tr:odd").addClass("RowStyle-css");
                            }
                        }
                    });
                }
                return false;
            });
        });


        function BindUnionBranch(value) {
            if (parseInt($("#drpUnion option:selected").val()) > 0) {
                $("#drpUnionBranch").prop("disabled", true);
                $.ajax({
                    type: "POST",
                    url: "AddEditCrewNew.aspx/GetUnionBranch",
                    data: "{ UnionID: '" + $("#drpUnion option:selected").val() + "',UserID:'" + $("#hdnLoggedInUserId").val() + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    success: function (response) {
                        if (response.d == "") {
                            $("#drpUnionBranch").prop("disabled", true);
                            $("#drpUnionBranch").html('');
                        }
                        else {
                            $("#drpUnionBranch").prop("disabled", false);
                            $("#drpUnionBranch").html(response.d);
                            $("#drpUnionBranch").val(value);
                        }
                    }
                });
            }
            else {
                $("#drpUnionBranch").prop("disabled", true);
                $("#drpUnionBranch").html('');
            }
        }

        function Check_NOK_Mandatory() {
            if ($("#hdnIsNOKMandatory").val() == "0") {
                $("#tbl_NOK .mandatory").hide();
                $("#tbl_NOK input[type='text']").removeClass("required");
                $("#tbl_NOK textarea").removeClass("required");
                $("#tbl_NOK select").removeClass("required");
            }
        }

        function UploadCrewPhoto() {
            var PhotoURL = '<%=HOST %>/Crew/CrewPhotoCropNew.aspx?ID=' + $("#HiddenField_CrewID").val();
            $('#dvPopupFrame').attr("Title", "Upload Photo");
            $('.content').css({ "height": "778px" });
            $('#dvPopupFrame').css({ "width": "900", "height": "600", "text-allign": "center" });
            $("#frPopupFrame").attr("src", PhotoURL);
            showModal('dvPopupFrame', false);
            return false;
        }

        function RemoveMandatory() {
            if ($("#<%=hdnIsPassportMandatory.ClientID %>").val() == "0") {
                $("#trPassport .mandatory").hide();
                $("#trPassport input[type='text']").removeClass("required");
            }

            if ($("#<%=hdnIsSeamanMandatory.ClientID %>").val() == "0") {
                $("#trSeaman .mandatory").hide();
                $("#trSeaman input[type='text']").removeClass("required");
            }
        }
    </script>
</asp:Content>
