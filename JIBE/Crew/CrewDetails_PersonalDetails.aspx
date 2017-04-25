<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_PersonalDetails.aspx.cs"
    Inherits="Crew_CrewDetails_Personal" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ctlAirPortList.ascx" TagName="AirPortList" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <div id="dvCrewPersonal">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnl_contactInformation" runat="server" Style="margin-top: 5px;">
            <fieldset>
                <legend>&nbsp;&nbsp;Contact Information
                    <asp:LinkButton ID="lnkEditContact" runat="server" CssClass="inline-edit"><font color="blue">[Edit]</font></asp:LinkButton></legend>
                <table id="tbltxtContact" runat="server" visible="false" width="100%">
                    <tr>
                        <td id="tdU" runat="server" style="vertical-align: top;">
                            <table class="dataTable" width="100%">
                                <tr>
                                    <td style="width: 20">
                                        Address Line 1<span class="mandatory">*</span>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="lblNOKAddress1" CssClass="required" runat="server" Width="200px"
                                            Enabled="False" MaxLength="250"></asp:TextBox>
                                        <asp:HiddenField ID="hdnCrewName" runat="server" />
                                        <asp:HiddenField ID="hdnClientName" runat="server" />
                                        <asp:HiddenField ID="hdnUSCountryID" runat="server" />
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td id="Td1" style="width: 22%" runat="server">
                                        Address Line 2
                                    </td>
                                    <td id="Td2" runat="server" style="width: 25%">
                                        <asp:TextBox ID="lblNOKAddress2" runat="server" Width="200px" Enabled="False" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City<span class="mandatory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" CssClass="required" runat="server" Width="200px" Enabled="False"
                                            MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                        State / Province / Region<span class="mandatory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtState" CssClass="required" runat="server" Width="200px" Enabled="False"
                                            MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Country<span class="mandatory">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCountry" CssClass="required" runat="server" Width="205px"
                                            Height="22" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                        Zip Code<span class="mandatory">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lblZipCode" CssClass="required" runat="server" MaxLength="50" Width="200px"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fax Number
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" runat="server" Width="200px" Enabled="False" MaxLength="50"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3"
                                            TargetControlID="txtFax" FilterType="Numbers,Custom" ValidChars="+">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile" runat="server" Width="200px" Enabled="False" MaxLength="50"
                                            Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtEmail" runat="server" Visible="false" Width="200px" Enabled="False"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdI" runat="server">
                            <table class="dataTable" width="96%">
                                <tr>
                                    <td style="width: 15%">
                                        Address<span class="mandatory">*</span>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtIAddress" TextMode="MultiLine" runat="server" Width="200px" Enabled="False"
                                            MaxLength="250" Style="background-color: #f2f5a9" Height="70px"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td style="width: 15%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td17" runat="server">
                                        Fax Number
                                    </td>
                                    <td id="Td18" runat="server">
                                        <asp:TextBox ID="txtIFax" runat="server" Width="200px" Enabled="False" MaxLength="50"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1"
                                            TargetControlID="txtIFax" FilterType="Numbers,Custom" ValidChars="+">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table id="tblLblContact" runat="server" width="50%" style="margin-left: 5px;">
                    <tr>
                        <td style="vertical-align: top;">
                            <table class="dataTable" width="100%" id="tdULabel" runat="server">
                                <tr>
                                    <td style="width: 15%">
                                        Address Line 1
                                    </td>
                                    <td class="data" style="width: 25%">
                                        <asp:Label ID="lblAddress1" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td id="Td9" runat="server" style="width: 25%">
                                        Address Line 2
                                    </td>
                                    <td class="data" style="width: 25%">
                                        <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                        State / Province / Region
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblState" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Country
                                    </td>
                                    <td class="data">
                                        <asp:Label runat="server" ID="lblUSCountry"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                        Zip Code
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblZip" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fax Number
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblFax" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <table class="dataTable" width="100%" id="tdILabel" runat="server">
                                <tr>
                                    <td valign="top" width="110px">
                                        Address
                                    </td>
                                    <td class="data" width="180px">
                                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td15" runat="server">
                                        Fax Number
                                    </td>
                                    <td id="Td16" class="data" runat="server">
                                        <asp:Label ID="lblIFax" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnl_general" runat="server" Style="margin-top: 10px">
            <fieldset>
                <legend>&nbsp;&nbsp;General Information
                    <asp:LinkButton ID="lnkEditGeneral" runat="server" CssClass="inline-edit"><font color="blue">[Edit]</font></asp:LinkButton>
                </legend>
                <table id="tbltextGeneral" border="0" visible="false" runat="server" style="width: 100%;
                    margin-left: 5px;" class="dataTable">
                    <tr>
                        <td style="width: 15%">
                            Nearest Airport
                        </td>
                        <td id="tdtxtAirport" runat="server">
                            <asp:TextBox runat="server" ID="txtAirport" Enabled="false" MaxLength="250" Width="200px"></asp:TextBox>
                        </td>
                        <td id="tdAirport" runat="server" visible="false" style="width: 33%">
                            <uc2:AirPortList ID="txtPD_Airport" runat="server" Width="200px" />
                        </td>
                        <td width="2%">
                        </td>
                        <td id="tdVeteran1" runat="server" style="width: 15%">
                            Veteran Status
                            <asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td id="tdVeteran2" runat="server">
                            <asp:DropDownList ID="ddlVeteran" runat="server" Width="200px" Enabled="False" BackColor="#F2F5A9">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tdSchool1" runat="server">
                        <td>
                            School
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSchool" runat="server" Width="200px" Enabled="False" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="2%">
                        </td>
                        <td>
                            Year Graduated
                            <asp:Label ID="lblS" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSchoolGraduated" runat="server" Width="200px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tdN1" runat="server">
                        <td>
                            Naturalization?
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNaturalization" runat="server" AutoPostBack="true" Enabled="false"
                                OnSelectedIndexChanged="ddlNaturalization_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList>
                        </td>
                        <td width="2%">
                        </td>
                        <td>
                            Naturalization Date
                            <asp:Label ID="lblN" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNaturalizationYear" runat="server" Width="195px" Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNaturalizationYear"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            English Proficiency
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEnglish" runat="server" Width="200px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Remarks
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="400px" Enabled="False"
                                MaxLength="250"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table border="0" id="tblLabelGeneral" runat="server" style="width: 50%; margin-left: 5px;"
                    class="dataTable">
                    <tr>
                        <td id="td8" runat="server" style="vertical-align: top;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Nearest Airport"></asp:Label>
                                    </td>
                                    <td id="td10" class="data" runat="server">
                                        <asp:Label ID="lblAirport" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td id="tdVeteran3" runat="server">
                                        Veteran Status
                                    </td>
                                    <td id="tdVeteran4" class="data" runat="server">
                                        <asp:Label ID="lblVeteran" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tdSchool4" runat="server">
                                    <td>
                                        School
                                    </td>
                                    <td id="tdSchool5" runat="server" class="data">
                                        <asp:Label ID="lblSchool" runat="server"></asp:Label>
                                    </td>
                                    <td id="tdSchool6" runat="server" width="2%">
                                    </td>
                                    <td>
                                        Year Graduated
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblSchoolYear" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="tdN5" runat="server">
                                    <td>
                                        Naturalization?
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblNaturalization" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                        Naturalization Date
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblNaturlizationDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        English Proficiency
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblEnglish" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="12%">
                                        Remarks
                                    </td>
                                    <td class="data" width="20%">
                                        <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                    </td>
                                    <td width="2%">
                                    </td>
                                    <td style="width: 15%;">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnl_Documents" runat="server" Style="margin-top: 10px">
            <fieldset>
                <legend>&nbsp;&nbsp;Documents
                    <asp:LinkButton ID="lnkEdiTPassport" runat="server" CssClass="inline-edit"><font color="blue">[Edit]</font></asp:LinkButton>
                </legend>
                <table id="tblDocText" visible="false" runat="server" border="0" style="width: 100%;
                    margin-left: 5px;" class="dataTable">
                    <tr>
                        <td width="85px">
                            Passport
                            <asp:Label runat="server" ID="lblP1" Visible="false" ForeColor="Red" class="mandatory"
                                Text="*"></asp:Label>
                        </td>
                        <td width="110px">
                            <asp:TextBox ID="txtPassport_No" runat="server" CssClass="required uppercase" Enabled="False"
                                MaxLength="50"></asp:TextBox>
                        </td>
                        <td width="80px">
                            Place of Issue<asp:Label runat="server" ID="lblP2" Visible="false" ForeColor="Red"
                                class="mandatory" Text="*"></asp:Label>
                        </td>
                        <td width="100px">
                            <asp:TextBox ID="txtPassport_Place" runat="server" CssClass="required uppercase"
                                Enabled="False" MaxLength="100"></asp:TextBox>
                        </td>
                        <td width="65px">
                            Issue Date<asp:Label runat="server" ID="lblP3" Visible="false" class="mandatory"
                                ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td width="100px">
                            <asp:TextBox ID="txtPassport_IssueDate" runat="server" CssClass="required uppercase"
                                ClientIDMode="Static" Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txtPassport_IssueDate"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td width="65px">
                            Expiry Date<asp:Label runat="server" ID="lblP4" Visible="false" class="mandatory"
                                ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td width="100px">
                            <asp:TextBox ID="txtPassport_ExpDate" runat="server" CssClass="required uppercase"
                                Enabled="False" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtPassport_ExpDate"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td width="65px">
                            Country
                        </td>
                        <td width="100px">
                            <asp:DropDownList runat="server" ID="drpPassportCountry" Width="99" CssClass="uppercase">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="trSeaman">
                        <td>
                            Seaman
                            <asp:Label runat="server" ID="lblS1" Visible="false" ForeColor="Red" class="mandatory"
                                Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSeamanBk_No" MaxLength="50" runat="server" CssClass="required uppercase"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            Place of Issue<asp:Label runat="server" ID="lblS2" Visible="false" ForeColor="Red"
                                class="mandatory" Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSeamanBk_Place" MaxLength="100" runat="server" CssClass="required uppercase"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            Issue Date<asp:Label runat="server" ID="lblS3" Visible="false" ForeColor="Red" class="mandatory"
                                Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSeamanBk_IssueDate" runat="server" CssClass="required uppercase"
                                ClientIDMode="Static" Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txtSeamanBk_IssueDate"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td>
                            Expiry Date<asp:Label runat="server" ID="lblS4" Visible="false" class="mandatory"
                                ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSeamanBk_ExpDate" runat="server" CssClass="required uppercase"
                                Enabled="False" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtSeamanBk_ExpDate"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td>
                            Country
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpSeamanCountry" Width="99" CssClass="uppercase">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="trMMC">
                        <td>
                            MMC Number<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMMCNO" runat="server" CssClass="required uppercase" Enabled="False"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" Display="None"
                                ControlToValidate="txtMMCNO" ValidationGroup="Date" ErrorMessage="Please enter MMC number!" />
                        </td>
                        <td>
                            Place of Issue<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMMCPOI" runat="server" CssClass="required uppercase" Enabled="False"
                                MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" Display="None"
                                ControlToValidate="txtMMCPOI" ValidationGroup="Date" ErrorMessage="Please enter MMC Place Of Issue!" />
                        </td>
                        <td>
                            Issue Date<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMMCDOI" runat="server" CssClass="required uppercase" ClientIDMode="Static"
                                Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderMMCI" runat="server" TargetControlID="txtMMCDOI"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" Display="None"
                                ControlToValidate="txtMMCDOI" ValidationGroup="Date" ErrorMessage="Please enter MMC Date Of Issue!" />
                        </td>
                        <td>
                            Expiry Date<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMMCDOE" runat="server" CssClass="required uppercase" Enabled="False"
                                ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderMMCE" runat="server" TargetControlID="txtMMCDOE"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" Display="None"
                                ControlToValidate="txtMMCDOE" ValidationGroup="Date" ErrorMessage="Please enter MMC Date Of Expiry!" />
                        </td>
                        <td>
                            Country
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpMMCCountry" Width="99" CssClass="uppercase">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="trTWIC">
                        <td>
                            TWIC Number<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTWICNumber" runat="server" CssClass="required uppercase" Enabled="False"
                                MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" Display="None"
                                ControlToValidate="txtTWICNumber" ValidationGroup="Date" ErrorMessage="Please enter TWIC number!" />
                        </td>
                        <td>
                            Place of Issue<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTWICPOI" runat="server" CssClass="required uppercase" Enabled="False"
                                MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" Display="None"
                                ControlToValidate="txtTWICPOI" ValidationGroup="Date" ErrorMessage="Please enter TWIC Place Of Issue!" />
                        </td>
                        <td>
                            Issue Date<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTWICDOI" runat="server" CssClass="required uppercase" ClientIDMode="Static"
                                Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderTWICI" runat="server" TargetControlID="txtTWICDOI"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" Display="None"
                                ControlToValidate="txtTWICDOI" ValidationGroup="Date" ErrorMessage="Please enter TWIC Date Of Issue!" />
                        </td>
                        <td>
                            Expiry Date<span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTWICDOE" runat="server" CssClass="required uppercase" Enabled="False"
                                ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderTWICE" runat="server" TargetControlID="txtTWICDOE"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" Display="None"
                                ControlToValidate="txtTWICDOE" ValidationGroup="Date" ErrorMessage="Please enter TWIC Date Of Expiry!" />
                        </td>
                        <td>
                            Country
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpTWICCountry" Width="99" CssClass="uppercase">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="trUSVisa">
                        <td>
                            US Visa
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rdbUS" AutoPostBack="true" OnSelectedIndexChanged="rdbUS_SelectedIndexChanged"
                                ClientIDMode="Static" Enabled="false" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            Visa Number<asp:Label ID="lblU1" class="mandatory" runat="server" ForeColor="Red"
                                Visible="false" Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUSVisaNumber" MaxLength="50" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            Issue Date<asp:Label ID="lblU2" runat="server" ForeColor="Red" class="mandatory"
                                Visible="false" Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUSIssue" runat="server" Enabled="False" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtUSIssue"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td>
                            Expire Date<asp:Label ID="lblU3" runat="server" ForeColor="Red" class="mandatory"
                                Visible="false" Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUSExpiry" runat="server" Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtUSExpiry"
                                Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table id="tblDocPanel" runat="server" border="0" style="width: 99%; margin-left: 5px;"
                    class="dataTable">
                    <tr>
                        <td style="width: 67px">
                            Passport
                        </td>
                        <td style="width: 110px" class="data">
                            <asp:Label ID="lblPassportNo" runat="server"></asp:Label>
                        </td>
                        <td width="1%">
                        </td>
                        <td style="width: 50px">
                            Place of Issue
                        </td>
                        <td style="width: 100px" class="data">
                            <asp:Label ID="lblPassportPOI" runat="server"></asp:Label>
                        </td>
                        <td width="1%">
                        </td>
                        <td style="width: 50px">
                            Issue Date
                        </td>
                        <td style="width: 100px" class="data">
                            <asp:Label ID="lblPassportIssue" runat="server"></asp:Label>
                        </td>
                        <td width="1%">
                        </td>
                        <td style="width: 50px">
                            Expiry Date
                        </td>
                        <td style="width: 100px" class="data">
                            <asp:Label ID="lblPassportExpiry" runat="server"></asp:Label>
                        </td>
                        <td width="1%">
                        </td>
                        <td style="width: 50px">
                            Country
                        </td>
                        <td style="width: 100px" class="data">
                            <asp:Label ID="lblPassportCountry" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trSeaman1">
                        <td>
                            Seaman
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeaman" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Place of Issue
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeamanPOI" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Issue Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeamanIssue" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Expiry Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSemanExpiry" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Country
                        </td>
                        <td class="data">
                            <asp:Label ID="lblSeamanCountry" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trMMC1">
                        <td>
                            MMC Number
                        </td>
                        <td class="data">
                            <asp:Label ID="lblMMCNo" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Place of Issue
                        </td>
                        <td class="data">
                            <asp:Label ID="lblMMCPOI" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Issue Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblMMCDOI" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Expiry Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblMMCDOE" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Country
                        </td>
                        <td class="data">
                            <asp:Label ID="lblMMCCountry" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trTWIC1">
                        <td>
                            TWIC Number
                        </td>
                        <td class="data">
                            <asp:Label ID="lblTWIC" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Place of Issue
                        </td>
                        <td class="data">
                            <asp:Label ID="lblTWICPOI" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Issue Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblTWICDOI" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Expiry Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblTWICDOE" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Country
                        </td>
                        <td class="data">
                            <asp:Label ID="lblTWICCountry" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trUSVisa1">
                        <td>
                            US Visa
                        </td>
                        <td class="data">
                            <asp:Label ID="lblUsVisa" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Visa Number
                        </td>
                        <td class="data">
                            <asp:Label ID="lblUsVisaNumber" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Issue Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblUSVisaIssue" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            Expiry Date
                        </td>
                        <td class="data">
                            <asp:Label ID="lblUsVisaExpiry" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnl_Dimension" runat="server" Style="margin-top: 10px">
            <fieldset>
                <legend>&nbsp;&nbsp;Crew Dimensions
                    <asp:LinkButton ID="lnkEditDimension" runat="server" CssClass="inline-edit"><font color="blue">[Edit]</font></asp:LinkButton></legend>
                <div runat="server" id="trUniform" class="CrewDimensions" style="margin-top: 5px">
                    <span>Uniform Size:</span>
                    <table border="0" class="dataTable" style="width: 95%">
                        <tr>
                            <td width="70px">
                                Shoe Size
                            </td>
                            <td>
                                <asp:TextBox ID="txtShoeSize" runat="server" CssClass="uppercase" Enabled="False"
                                    Width="100" MaxLength="8"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td width="65px">
                                T-Shirt Size
                            </td>
                            <td>
                                <asp:TextBox ID="txtTShirtSize" runat="server" CssClass="uppercase" Enabled="False"
                                    Width="100" MaxLength="10"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td width="95px">
                                Cargo Pant Size
                            </td>
                            <td>
                                <asp:TextBox ID="txtCargoPantSize" runat="server" CssClass="uppercase" Enabled="False"
                                    Width="100" MaxLength="8"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td width="65px">
                                Overall size
                            </td>
                            <td>
                                <asp:TextBox ID="txtOverallSize" runat="server" CssClass="uppercase" Enabled="False"
                                    Width="100" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="trHeight" class="CrewDimensions" style="margin-top: 5px">
                    <span>Height/Waist/Weight:</span>
                    <table border="0" class="dataTable" width="">
                        <tr>
                            <td style="width: 70px;">
                                Height (CM)
                            </td>
                            <td>
                                <asp:TextBox ID="txtHeight" Width="100" runat="server" CssClass="uppercase" Enabled="False"
                                    MaxLength="8"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                Waist (Inch)
                            </td>
                            <td>
                                <asp:TextBox ID="txtWaist" Width="100" runat="server" CssClass="uppercase" Enabled="False"
                                    MaxLength="8"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                Weight (Kg)
                            </td>
                            <td>
                                <asp:TextBox ID="txtWeight" Width="100" runat="server" CssClass="uppercase" Enabled="False"
                                    MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="trUniformlbl" class="CrewDimensions">
                    <span>Uniform Size:</span>
                    <table border="0" class="dataTable" style="width: 85%">
                        <tr>
                            <td style="width: 65px">
                                Shoe Size
                            </td>
                            <td style="width: 112px" class="data">
                                <asp:Label ID="lblShoe" runat="server"></asp:Label>
                            </td>
                            <td width="5px">
                            </td>
                            <td style="width: 65px">
                                T-Shirt Size
                            </td>
                            <td style="width: 100px" class="data">
                                <asp:Label ID="lblTShirt" runat="server"></asp:Label>
                            </td>
                            <td width="5px">
                            </td>
                            <td style="width: 65px">
                                Cargo Pant Size
                            </td>
                            <td style="width: 100px" class="data">
                                <asp:Label ID="lblCargoPant" runat="server"></asp:Label>
                            </td>
                            <td width="5px">
                            </td>
                            <td style="width: 65px">
                                Overall size
                            </td>
                            <td style="width: 100px" class="data">
                                <asp:Label ID="lblOverall" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="trHeightLbl" class="CrewDimensions" style="margin-top: 5px">
                    <span>Height/Waist/Weight:</span>
                    <table border="0" width="64%" class="dataTable">
                        <tr>
                            <td style="width: 63px;">
                                Height (CM)
                            </td>
                            <td class="data" style="width: 98px;">
                                <asp:Label ID="lblHeight" runat="server"></asp:Label>
                            </td>
                            <td width="5px">
                            </td>
                            <td style="width: 65px;">
                                Waist (Inch)
                            </td>
                            <td class="data" style="width: 100px;">
                                <asp:Label ID="lblWaist" runat="server"></asp:Label>
                            </td>
                            <td width="5px">
                            </td>
                            <td style="width: 65px;">
                                Weight (Kg)
                            </td>
                            <td class="data" style="width: 100px;">
                                <asp:Label ID="lblWeight" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnl_Config" runat="server" Style="margin-top: 10px">
            <fieldset>
                <legend>&nbsp;&nbsp;Additional Fields
                    <asp:LinkButton ID="lnkEditConfig" runat="server" CssClass="inline-edit"><font color="blue">[Edit]</font></asp:LinkButton>
                </legend>
                <table id="tblConText" visible="false" runat="server" border="0" style="width: 50%"
                    class="dataTable">
                    <tr id="trCon1" runat="server">
                        <td style="min-width: 100px;">
                            <asp:Label ID="lblCon1" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCon1" runat="server" Enabled="False" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trCon2" runat="server">
                        <td style="min-width: 100px;">
                            <asp:Label ID="lblCon2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCon2" runat="server" Enabled="False" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trCon3" runat="server">
                        <td style="min-width: 100px;">
                            <asp:Label ID="lblCon3" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCon3" runat="server" Enabled="False" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table id="tblConLabel" runat="server" border="0" style="margin-left: 5px" class="dataTable">
                    <tr id="trCon4" runat="server">
                        <td style="min-width: 100px;">
                            <asp:Label ID="lblCon4" runat="server"></asp:Label>
                        </td>
                        <td class="data" style="min-width: 185px;">
                            <asp:Label ID="lblCF1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trCon5" runat="server">
                        <td style="min-width: 100px;">
                            <asp:Label ID="lblCon5" runat="server"></asp:Label>
                        </td>
                        <td class="data" style="min-width: 185px;">
                            <asp:Label ID="lblCF2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trCon6" runat="server">
                        <td style="min-width: 100px;">
                            <asp:Label ID="lblCon6" runat="server"></asp:Label>
                        </td>
                        <td class="data" style="min-width: 185px;">
                            <asp:Label ID="lblCF3" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnSave" runat="server" Text=" Save " ValidationGroup="Date" OnClick="btnSavePersonal_Click"
                Visible="False" ClientIDMode="Static" />&nbsp;&nbsp;
            <asp:Button ID="btnClose" runat="server" Text="Cancel" OnClick="btnClosePersonal_Click"
                Visible="False" />
            <asp:ValidationSummary ShowMessageBox="true" runat="server" ShowSummary="false" ID="vds"
                ValidationGroup="Date" DisplayMode="List" />
        </div>
    </div>
    </form>
</body>
<script type="text/javascript">

    var strDateFormat = '<%=DFormat %>';
    var alertmsg = '';
    $(document).ready(function () {
        $("body").on("click", "#btnSave", function () {
            var alertmsg = '', AddressMSg = "";

            if ($("#lblNOKAddress1").length > 0) {
                var validate = true;
                if ($.trim($("#lblNOKAddress1").val()) == "") {
                    alertmsg += "Enter Address Line 1\n";
                }
                if ($.trim($("#txtCity").val()) == "") {
                    alertmsg += "Enter City\n";
                }
                if ($.trim($("#txtState").val()) == "") {
                    alertmsg += "Enter State / Province / Region\n";
                }
                if ($("#ddlCountry option:selected").val() == "0") {
                    alertmsg += "Select Country\n";
                }
                if ($.trim($("#lblZipCode").val()) == "") {
                    alertmsg += "Enter Zip Code\n";
                }

                if (parseInt($("#ddlCountry option:selected").val()) == parseInt($("#<%=hdnUSCountryID.ClientID %>").val())) {////If selected country is US
                    AddressMSg = USAddressValidation($("#lblNOKAddress1").val(), $("#lblNOKAddress2").val(), $("#txtCity").val(), $("#txtState").val(), $("#lblZipCode").val(), $("#ddlCountry option:selected").text(), "Crew", $("#<%=hdnCrewName.ClientID %>").val(), $.trim($("#<%=hdnClientName.ClientID %>").val()), "Edit", $.trim(parent.$("#ctl00_MainContent_lblCode").text()), $.trim(parent.$("#ctl00_MainContent_lblDateOfBirth").text()), $.trim(parent.$("#ctl00_MainContent_lblAppliedRank").text()));
                    if (AddressMSg == "Error") {
                        alert("User address validation failed. User cannot be created/updated at the moment");
                        return false;
                    }
                }
            }

            var IsPValid = true;
            if ($("#txtPassport_IssueDate").length > 0) {
                var date1 = document.getElementById("txtPassport_IssueDate").value;
                if ($.trim($("#txtPassport_IssueDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alertmsg += "Enter valid Passport Issue Date<%=TodayDateFormat %>\n";
                        IsPValid = false;
                    }
                    else if (DateAsFormat(document.getElementById("txtPassport_IssueDate").value, strDateFormat) > new Date()) {
                        alertmsg += "Passport Issue Date can not be future Date\n";
                        IsPValid = false;
                    }
                }

                if ($("#txtPassport_ExpDate").length > 0) {
                    var date1 = document.getElementById("txtPassport_ExpDate").value;
                    if ($.trim($("#txtPassport_ExpDate").val()) != "") {
                        if (IsInvalidDate(date1, strDateFormat)) {
                            alertmsg += "Enter valid Passport Expiry Date<%=TodayDateFormat %>\n";
                            IsPValid = false;
                        }
                    }
                }

                if (IsPValid) {
                    if (DateAsFormat(document.getElementById("txtPassport_IssueDate").value, strDateFormat) > DateAsFormat(document.getElementById("txtPassport_ExpDate").value, strDateFormat)) {
                        alertmsg += "Passport Issue Date should be less than Expiry Date\n";
                    }
                }
            }


            var IsSValid = true;
            if ($("#txtSeamanBk_IssueDate").length > 0) {
                var date1 = document.getElementById("txtSeamanBk_IssueDate").value;
                if ($.trim($("#txtSeamanBk_IssueDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alertmsg += "Enter valid Seaman Book Issue Date<%=TodayDateFormat %>\n";
                        IsSValid = false;
                    }
                    else if (DateAsFormat(document.getElementById("txtSeamanBk_IssueDate").value, strDateFormat) > new Date()) {
                        alertmsg += "Seaman Book Issue Date can not be future Date\n";
                        IsSValid = false;
                    }
                }

                if ($("#txtSeamanBk_ExpDate").length > 0) {
                    var date1 = document.getElementById("txtSeamanBk_ExpDate").value;
                    if ($.trim($("#txtSeamanBk_ExpDate").val()) != "") {
                        if (IsInvalidDate(date1, strDateFormat)) {
                            alertmsg += "Enter valid Seaman Expiry Date<%=TodayDateFormat %>\n";
                            IsSValid = false;
                        }
                    }
                }

                if (IsSValid) {
                    if (DateAsFormat(document.getElementById("txtSeamanBk_IssueDate").value, strDateFormat) > DateAsFormat(document.getElementById("txtSeamanBk_ExpDate").value, strDateFormat)) {
                        alertmsg += "Seaman Issue Date should be less than Expiry Date\n";
                    }
                }
            }


            if ($("#txtMMCDOI").length > 0) {
                var date1 = document.getElementById("txtMMCDOI").value;
                if ($.trim($("#txtMMCDOI").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alertmsg += "Enter valid MMC Issue Date<%=TodayDateFormat %>\n";
                    }
                    else if (DateAsFormat(document.getElementById("txtMMCDOI").value, strDateFormat) > new Date()) {
                        alertmsg += "MMC Issue Date can not be future Date\n";
                    }
                }
            }

            if ($("#txtMMCDOE").length > 0) {
                var date1 = document.getElementById("txtMMCDOE").value;
                if ($.trim($("#txtMMCDOE").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alertmsg += "Enter valid MMC Expiry Date<%=TodayDateFormat %>\n";
                    }
                }
            }

            if ($("#txtTWICDOI").length > 0) {
                var date1 = document.getElementById("txtTWICDOI").value;
                if ($.trim($("#txtTWICDOI").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alertmsg += "Enter valid TWIC Issue Date<%=TodayDateFormat %>\n";
                    }
                    else if (DateAsFormat(document.getElementById("txtTWICDOI").value, strDateFormat) > new Date()) {
                        alertmsg += "TWIC Issue Date can not be future Date\n";
                    }
                }
            }

            if ($("#txtTWICDOE").length > 0) {
                var date1 = document.getElementById("txtTWICDOE").value;
                if ($.trim($("#txtTWICDOE").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alertmsg += "Enter valid TWIC Expiry Date <%=TodayDateFormat %>\n";
                    }
                }
            }

            if ($("#txtUSIssue").length > 0) {
                var ISUsVisaValid = true;
                var date1 = document.getElementById("txtUSIssue").value;
                if ($.trim($("#txtUSIssue").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alertmsg += "Enter valid US Visa Issue Date<%=TodayDateFormat %>\n";
                        ISUsVisaValid = false;
                    }
                    else if (DateAsFormat(document.getElementById("txtUSIssue").value, strDateFormat) > new Date()) {
                        alertmsg += "US Visa Issue Date can not be future Date\n";
                        ISUsVisaValid = false;
                    }
                }

                if ($("#txtUSExpiry").length > 0) {
                    var date2 = document.getElementById("txtUSExpiry").value;
                    if ($.trim($("#txtUSExpiry").val()) != "") {
                        if (IsInvalidDate(date2, strDateFormat)) {
                            alertmsg += "Enter valid US Visa Expiry Date<%=TodayDateFormat %>\n";
                            ISUsVisaValid = false;
                        }
                    }
                }

                if ($("#txtUSIssue").length > 0 && $("#txtUSExpiry").length > 0 && ISUsVisaValid) {
                    if (DateAsFormat(document.getElementById("txtUSIssue").value, strDateFormat) > DateAsFormat(document.getElementById("txtUSExpiry").value, strDateFormat)) {
                        alertmsg += "US Visa issue date should be less US Visa expiry date\n";
                    }
                }
            }

            if (alertmsg != '') {
                alert(alertmsg + "\n" + AddressMSg);
                return false;
            }
            if (AddressMSg != "") {
                alert(AddressMSg);
                return false;
            }
        });

    });

</script>
</html>
