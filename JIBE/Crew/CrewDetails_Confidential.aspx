<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Confidential.aspx.cs"
    ViewStateEncryptionMode="Never" ValidateRequest="false" EnableEventValidation="false"
    Inherits="Crew_CrewDetails_Confidential" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="src1" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewConfidentialResults">
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <asp:LinkButton ID="lnkEditNOKConf" Style="float: right; font-size: 10px; font-weight: bold;
                    margin-right: 10px; color: #034af3;" Visible="false" runat="server" CssClass="inline-edit" >[Edit]</asp:LinkButton>
                <div id="tblText" runat="server" visible="false">
                    <fieldset>
                        <legend>&nbsp;&nbsp;General Information</legend>
                        <table class="dataTable" id="tblGeneralInformation" runat="server" style="margin-left: 10px;" visible="false">
                            <tr id="trUnion1" runat="server">
                                <td style="width: 100px">
                                    Union<span class="mandatory">*</span>
                                </td>
                                <td style="width: 200px">
                                    <asp:DropDownList ID="ddlUnion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnion_SelectedIndexChanged"
                                          width="175px" class="required"  >
                                        </asp:DropDownList>
                                </td>
                                <td style="width: 100px">
                                    Union Branch<span class="mandatory">*</span>
                                </td>
                                <td style="width: 200px">
                                    <asp:DropDownList ID="ddlUnionBranch" runat="server"  width="175px"  class="required">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 140px">
                                    <asp:Label ID="lblSSNEdit" runat="server">Social Security Number</asp:Label><span id="spnSSNEdit" runat="server" class="mandatory">*</span>
                                </td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="txtSSN" runat="server" MaxLength="11" Width="170px" cssClass="required" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9"
                                            TargetControlID="txtSSN" FilterMode="ValidChars" FilterType="Numbers,Custom"
                                            ValidChars="-">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 100px" id="tdEditlblUnionBook" runat="server" >
                                    Union Book<span class="mandatory">*</span>
                                </td>
                                <td style="width: 200px"  id="tdEdittxtUnionBook" runat="server" >
                                    <asp:DropDownList  class="required" ID="ddlUnionBook"  width="175px"  runat="server">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trSchool1" runat="server">
                                <td style="width: 100px">
                                    School
                                </td>
                                <td style="width: 200px">
                                     <asp:DropDownList ID="ddlSchool" runat="server"  width="175px" 
                                         AutoPostBack="True" onselectedindexchanged="ddlSchool_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                </td>
                                <td style="width: 100px">
                                    Year Graduated <asp:Label Text="*" runat="server"  class="mandatory" ID="lblSY"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:DropDownList ID="ddlSchoolGraduated" runat="server"  width="175px" >
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trNature1" runat="server">
                                <td style="width: 100px">
                                    Naturalization?
                                </td>
                                <td  style="width: 200px">
                                     <asp:DropDownList ID="ddlNature" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNature_SelectedIndexChanged"
                                             width="175px" >
                                         <asp:ListItem Text="Yes" Value="1" />
                                         <asp:ListItem Text="No" Value="0" />
                                        </asp:DropDownList>
                                </td>
                                <td style="width: 115px">
                                    Naturalization Date<asp:Label Text="*" runat="server"  class="mandatory" ID="lblnaturem"></asp:Label>
                                </td>
                                <td  style="width: 200px">
                                    <asp:TextBox ID="txtNatureDate" runat="server" width="170px" ClientIDMode="Static" Enabled="False" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtNatureDate">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr id="trVeteran" runat="server">
                                <td style="width: 100px">
                                    Veteran Status<span class="mandatory">*</span>
                                </td>
                                <td  style="width: 200px">
                                    <asp:DropDownList ID="ddlVeteran"  class="required" runat="server"  width="175px" >
                                        </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="trRace" runat="server">
                                <td style="width: 100px">
                                    Race<span class="mandatory">*</span>
                                </td>
                                <td style="width: 200px">
                                   <asp:DropDownList ID="ddlRace"  class="required" runat="server"  width="175px" >
                                        </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="trPermanent" runat="server">
                                <td style="width: 100px">
                                    Permanent?
                                </td>
                                <td style="width: 200px">
                                    <asp:DropDownList ID="ddlPermanent"  width="175px"  runat="server">
                                        </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="trHire" runat="server">
                                <td style="width: 100px">
                                    Hire Date<span class="mandatory">*</span>
                                </td>
                                <td style="width: 200px">
                                     <asp:TextBox  class="required" ID="txtHireDate" runat="server"  width="170px" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtHireDate">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr id="trID" runat="server">
                                <td style="width: 100px">
                                    ID Number
                                </td>
                                <td style="width: 200px">
                                     <asp:TextBox ID="txtID" runat="server" width="169px"  maxlength="100" ></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="margin-top: 5px;">
                        <legend>&nbsp;&nbsp;Documents</legend>
                        <table class="dataTable" style="margin-left: 10px;" id="tblDocuments" runat="server" visible="false">
                            <tr id="trSeaman1" runat="server">
                                <td style="width: 100px">
                                    Seaman<asp:Label runat="server" ID="lblS1" Visible="false" CssClass="mandatory" Text="*"></asp:Label>
                                </td>
                                <td style="width: 110px">
                                   <asp:TextBox ID="txtSeaman" runat="server" Width="110px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                   Place Of Issue <asp:Label runat="server" ID="lblS2" Visible="false" CssClass="mandatory"
                                            Text="*"></asp:Label>
                                </td>
                                <td style="width: 110px">
                                <asp:TextBox ID="txtSeamanIssuePlace" runat="server" Width="110px" MaxLength="100" ></asp:TextBox>
                                  
                                </td>
                                <td style="width: 100px">
                                    Issue Date<asp:Label runat="server" ID="lblS3" Visible="false" CssClass="mandatory"
                                            Text="*"></asp:Label>
                                </td>
                                <td style="width: 110px">
                                      <asp:TextBox ID="txtSeamanIssueDate" runat="server" Width="110px"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtSeamanIssueDate">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 100px">
                                    Expiry Date<asp:Label runat="server" ID="lblS4" Visible="false" CssClass="mandatory"
                                            Text="*"></asp:Label>
                                </td>
                                <td style="width: 110px">
                                    <asp:TextBox ID="txtSeamanExpiryDate" runat="server" Width="110px" 
                                            ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtSeamanExpiryDate">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 100px">
                                    Country
                                </td>
                                <td style="width: 110px">
                                     <asp:DropDownList runat="server" ID="drpSeamanCountry" Width="110px">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trMMC1" runat="server">
                                <td style="width: 100px">
                                    MMC Number<span class="mandatory">*</span>
                                </td>
                                <td style="width: 110px">
                                   <asp:TextBox ID="txtMMC" runat="server"  Width="110px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Place Of Issue<span class="mandatory">*</span>
                                </td>
                                <td style="width: 110px">
                                    <asp:TextBox ID="txtMMCIssuePlace" runat="server" Width="110px" 
                                        MaxLength="100" ></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Issue Date<span class="mandatory">*</span>
                                </td>
                                <td  style="width: 110px">
                                     <asp:TextBox ID="txtMMCISSueDate" runat="server"  Width="110px"  ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtMMCISSueDate">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 100px">
                                    Expiry Date<span class="mandatory">*</span>
                                </td>
                                <td style="width: 110px">
                                     <asp:TextBox ID="txtMMCExpiryDate"  Width="110px" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtMMCExpiryDate">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 100px">
                                    Country
                                </td>
                                <td style="width: 110px">
                                   <asp:DropDownList runat="server" ID="drpMMCCountry"  Width="110px">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trTWIC1" runat="server">
                                <td style="width: 100px">
                                    TWIC Number<span class="mandatory">*</span>
                                </td>
                                <td style="width: 110px">
                                    <asp:TextBox ID="txtTWIC" runat="server" Width="110px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Place Of Issue<span class="mandatory">*</span>
                                </td>
                                <td class="data" style="width: 110px">
                                     <asp:TextBox ID="txtTWICIssuePlace" runat="server" Width="110px" 
                                         MaxLength="100" ></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Issue Date<span class="mandatory">*</span>
                                </td>
                                <td  style="width: 110px">
                                    <asp:TextBox ID="txtTWICIssueDate" runat="server" Width="110px" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtTWICIssueDate">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                
                                <td style="width: 100px">
                                    Expiry Date<span class="mandatory">*</span>
                                </td>
                                <td style="width: 110px">
                                     <asp:TextBox ID="txtTWICExpiry" runat="server" Width="110px" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtTWICExpiry">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 100px">
                                    Country
                                </td>
                                <td style="width: 110px">
                                    <asp:DropDownList runat="server" ID="drpTWICCountry" Width="110px">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trUS1" runat="server">
                                <td style="width: 100px">
                                    Valid US Visa?
                                </td>
                                <td style="width: 110px">
                                    <asp:RadioButtonList ID="rdbUS" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                             OnSelectedIndexChanged="rdbUS_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                </td>
                                <td style="width: 100px">
                                    US Visa Number<span class="mandatory" runat="server" id="spnMandtoryUSVisa" visible="false">*</span>
                                </td>
                                <td style="width: 110px">
                                     <asp:TextBox ID="txtUSVisaNumber" runat="server" Width="110px" Enabled="False" 
                                         MaxLength="50"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Issue Date<span class="mandatory" runat="server" id="spnMandtoryUSVisaIssue" visible="false">*</span>
                                </td>
                                <td style="width: 110px">
                                    <asp:TextBox ID="txtUSIssue" runat="server" Enabled="False" Width="110px" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtUSIssue"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="width: 100px">
                                    Expiry Date<span class="mandatory" runat="server" id="spnMandtoryUSVisaExpiry" visible="false">*</span>
                                </td>
                                <td style="width: 110px">
                                    <asp:TextBox ID="txtUSExpiry" Enabled="False" runat="server" Width="110px" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtUSExpiry">
                                        </ajaxToolkit:CalendarExtender>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="margin-top: 5px;">
                        <legend>&nbsp;&nbsp;Crew Dimensions</legend>
                        <div runat="server" id="trUniform1" class="CrewDimensions">
                            <span>Uniform Size:</span>
                            <table class="dataTable">
                                <tr>
                                    <td style="width: 100px">
                                        Shoe Size
                                    </td>
                                    <td style="width: 110px">
                                        <asp:TextBox ID="txtShoeSize" runat="server" Width="110px" MaxLength="8"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        T-Shirt Size
                                    </td>
                                    <td style="width: 110px">
                                         <asp:TextBox ID="txtTshirt" runat="server" Width="110px"  MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        Cargo Pant Size
                                    </td>
                                    <td style="width: 110px">
                                        <asp:TextBox ID="txtCargo" runat="server" Width="110px"  MaxLength="8"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        Overall Size
                                    </td>
                                    <td  style="width: 110px">
                                         <asp:TextBox ID="txtOverall" runat="server" Width="110px" MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div runat="server" id="trBodyDimensions1" class="CrewDimensions">
                            <span>Height/Waist/Weight:</span>
                            <table class="dataTable">
                                <tr>
                                    <td style="width: 100px">
                                        Height(CM)
                                    </td>
                                    <td runat="server" style="width: 110px">
                                         <asp:TextBox ID="txtHeight" runat="server" Width="110px" MaxLength="8"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        Waist(Inch)
                                    </td>
                                    <td style="width: 110px">
                                         <asp:TextBox ID="txtWaist" runat="server" Width="110px" MaxLength="8"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        Weight(KG)
                                    </td>
                                    <td style="width: 110px">
                                        <asp:TextBox ID="txtWeight" runat="server" Width="110px" MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                    <fieldset style="margin-top: 5px;">
                        <legend>&nbsp;&nbsp;Additional Fields</legend>
                        <table class="dataTable" id="tblAdditionalFields" runat="server" style="margin-left: 10px;" visible="false">
                            <tr id="trCF1" runat="server">
                                <td style="width: 100px">
                                     <asp:Label ID="lblCF4" runat="server"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:TextBox ID="txtCF1" MaxLength="100" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trCF2" runat="server">
                                <td style="width: 100px">
                                    <asp:Label ID="lblCF5" runat="server"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                   <asp:TextBox ID="txtCF2" MaxLength="100" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trCF3" runat="server">
                                <td style="width: 100px">
                                    <asp:Label ID="lblCF6" runat="server"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                     <asp:TextBox ID="txtCF3" MaxLength="100" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                </div>
                <div id="tblLabel" runat="server" width="100%" style="margin-top: 5px;">
                    <fieldset>
                        <legend>&nbsp;&nbsp;General Information</legend>
                        <table class="dataTable" style="margin-left: 10px;" id="tblGeneralInformation1" runat="server">
                            <tr id="trUnion4" runat="server">
                                <td style="width: 100px">
                                    Union
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblUnion" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Union Branch
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lnlUnionBranch" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 160px">
                                    <asp:Label ID="LabelSSN" runat="server">Social Security Number</asp:Label>
                                </td>
                                <td class="data"  style="width: 200px" id="tdlblSSN" runat="server">
                                    <asp:Label ID="lblSSN" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    <asp:Label ID="LabelUnionBook" runat="server">Union Book</asp:Label>
                                </td>
                                <td class="data" id="tdlblUnionBook" runat="server" style="width: 200px">
                                    <asp:Label ID="lblUnionBook" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trSchool3" runat="server">
                                <td style="width: 100px">
                                    School
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblSchool" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Year Graduated
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblSchoolGraduated" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trNature3" runat="server">
                                <td style="width: 100px">
                                    Naturalization?
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblNaturalization" runat="server"></asp:Label>
                                </td>
                                <td style="width: 115px">
                                    Naturalization Date
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblNaturalizationDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trVeteran1" runat="server">
                                <td style="width: 100px">
                                    Veteran Status
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblVeteranStatus" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="trRace1" runat="server">
                                <td style="width: 100px">
                                    Race
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblRace" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="trPermanent1" runat="server">
                                <td style="width: 100px">
                                    Permanent?
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblPermanentStatus" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr id="trHire1" runat="server">
                                <td style="width: 100px">
                                    Hire Date
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblHiredate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trID1" runat="server">
                                <td style="width: 100px">
                                    ID Number
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblIDNumber" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="margin-top: 5px;">
                        <legend>&nbsp;&nbsp;Documents</legend>
                        <table class="dataTable" style="margin-left: 10px;" id="tblDocuments1" runat="server">
                            <tr id="trSeaman5" runat="server">
                                <td style="width: 100px">
                                    Seaman
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblSeamanbook" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Place Of Issue
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblSeamanIssuePlace" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Issue Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblSeamanIssueDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Expiry Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblSeamanExpiryDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Country
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblSeamanCountry" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trMMC8" runat="server">
                                <td style="width: 100px">
                                    MMC Number
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblMMCno" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Place Of Issue
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblMMCIssuePlace" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Issue Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblMMCIssueDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Expiry Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblMMCExpiryDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Country
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblMMCCountry" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trTWIC5" runat="server">
                                <td style="width: 100px">
                                    TWIC Number
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblTwicNo" runat="server"></asp:Label>
                                </td>
                                
                                <td style="width: 100px">
                                    Place Of Issue
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblTIssuePlace" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Issue Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblTIssueDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Expiry Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblTExpiryDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                   Country
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblTWICCountry" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trUS5" runat="server">
                                <td style="width: 100px">
                                    Valid US Visa?
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblUsVisa" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    US Visa Number
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblUSVisaNumber" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Issue Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblUSIssueDAte" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    Expiry Date
                                </td>
                                <td class="data" style="width: 110px">
                                    <asp:Label ID="lblUSExpiryDAte" runat="server"></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="margin-top: 5px;">
                        <legend>&nbsp;&nbsp;Crew Dimensions</legend>
                        <div runat="server" id="trUniform5" class="CrewDimensions">
                            <span>Uniform Size:</span>
                            <table class="dataTable">
                                <tr>
                                    <td style="width: 100px">
                                        Shoe Size
                                    </td>
                                    <td class="data" style="width: 110px">
                                        <asp:Label ID="lblShoe" runat="server" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 100px">
                                        T-Shirt Size
                                    </td>
                                    <td class="data" style="width: 110px">
                                        <asp:Label ID="lblTshirt" runat="server" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 100px">
                                        Cargo Pant Size
                                    </td>
                                    <td class="data" style="width: 110px">
                                        <asp:Label ID="lblCargoPant" runat="server" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 100px">
                                        Overall Size
                                    </td>
                                    <td class="data" style="width: 110px">
                                        <asp:Label ID="lblOverall" runat="server" Width="100px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div runat="server" id="trBodyDimensions4" class="CrewDimensions">
                            <span>Height/Waist/Weight:</span>
                            <table class="dataTable">
                                <tr>
                                    <td id="Td7" runat="server" style="width: 100px">
                                        Height(CM)
                                    </td>
                                    <td runat="server" id="Td8" class="data" style="width: 110px">
                                        <asp:Label ID="lblHeight" runat="server" Width="100px"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td9" style="width: 100px">
                                        Waist(Inch)
                                    </td>
                                    <td runat="server" id="Td10" class="data" style="width: 110px">
                                        <asp:Label ID="lblWaist" runat="server" Width="100px"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td11" style="width: 100px">
                                        Weight(KG)
                                    </td>
                                    <td runat="server" id="Td12" class="data" style="width: 110px">
                                        <asp:Label ID="lblWeight" runat="server" Width="100px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                    <fieldset style="margin-top: 5px;">
                        <legend>&nbsp;&nbsp;Additional Fields</legend>
                        <table class="dataTable" style="margin-left: 10px;" id="tblAdditionalFields1" runat="server">
                            <tr id="trCF4" runat="server">
                                <td style="width: 100px">
                                    <asp:Label ID="lblCF1" runat="server"></asp:Label>
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblCF11" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trCF5" runat="server">
                                <td style="width: 100px">
                                    <asp:Label ID="lblCF2" runat="server"></asp:Label>
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblCF21" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trCF6" runat="server">
                                <td style="width: 100px">
                                    <asp:Label ID="lblCF3" runat="server"></asp:Label>
                                </td>
                                <td class="data" style="width: 200px">
                                    <asp:Label ID="lblCF31" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <center>
                    <asp:Button ID="btnSave" ClientIDMode="Static" Visible="false" Text="Save" runat="server"
                        OnClick="btnSave_Click" VgalidationGroup="Date" />
                    <asp:Button ID="btnClose" runat="server" Text="Cancel" OnClick="btnClose_Click" Visible="False" />
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
<script type="text/javascript">
    var strDateFormat = '<%=DFormat %>';

    $(document).ready(function () {
        $("body").on("click", "#btnSave", function () {

            if ($("#txtSeamanIssueDate").length > 0) {
                if ($.trim($("#txtSeamanIssueDate").val()) != "") {
                    var date1 = document.getElementById("txtSeamanIssueDate").value;
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid Seaman Issue Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }
            if ($("#txtSeamanExpiryDate").length > 0) {
                if ($.trim($("#txtSeamanExpiryDate").val()) != "") {
                    var date1 = document.getElementById("txtSeamanExpiryDate").value;
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid Seaman expiry Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }


            if ($("#txtSeamanIssueDate").length > 0) {
                if (DateAsFormat(document.getElementById("txtSeamanIssueDate").value, strDateFormat) > new Date()) {
                    alert("Seaman Issue Date can not  be future Date");
                    return false;
                }
            }

            if ($("#txtSeamanIssueDate").length > 0 && $("#txtSeamanExpiryDate").length > 0) {
                if (DateAsFormat(document.getElementById("txtSeamanIssueDate").value, strDateFormat) > DateAsFormat(document.getElementById("txtSeamanExpiryDate").value, strDateFormat)) {
                    alert("Seaman Issue Date should be less than Seaman Expiry Date");
                    return false;
                }
            }

            if ($("#txtMMCISSueDate").length > 0) {
                var date1 = document.getElementById("txtMMCISSueDate").value;
                if ($.trim($("#txtMMCISSueDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid MMC Issue Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }
            if ($("#txtMMCExpiryDate").length > 0) {
                var date1 = document.getElementById("txtMMCExpiryDate").value;
                if ($.trim($("#txtMMCExpiryDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid MMC expiry Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }
            if ($("#txtMMCISSueDate").length > 0) {
                if (DateAsFormat(document.getElementById("txtMMCISSueDate").value, strDateFormat) > new Date()) {
                    alert("MMC Issue Date can not  be future Date");
                    return false;
                }
            }
            if ($("#txtMMCISSueDate").length > 0 && $("#txtMMCExpiryDate").length > 0) {
                if (DateAsFormat(document.getElementById("txtMMCISSueDate").value, strDateFormat) > DateAsFormat(document.getElementById("txtMMCExpiryDate").value, strDateFormat)) {
                    alert("MMC Issue Date should be less than MMC Expiry Date");
                    return false;
                }
            }

            if ($("#txtTWICIssueDate").length > 0) {
                var date1 = document.getElementById("txtTWICIssueDate").value;
                if ($.trim($("#txtTWICIssueDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid TWIC issue Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }

            if ($("#txtTWICExpiry").length > 0) {
                var date1 = document.getElementById("txtTWICExpiry").value;
                if ($.trim($("#txtTWICExpiry").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid TWIC expiry Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }

            if ($("#txtTWICIssueDate").length > 0) {
                if (DateAsFormat(document.getElementById("txtTWICIssueDate").value, strDateFormat) > new Date()) {
                    alert("TWIC Issue Date can not  be future Date");
                    return false;
                }
            }
            if ($("#txtTWICIssueDate").length > 0 && $("#txtTWICExpiry").length > 0) {
                if (DateAsFormat(document.getElementById("txtTWICIssueDate").value, strDateFormat) > DateAsFormat(document.getElementById("txtTWICExpiry").value, strDateFormat)) {
                    alert("TWIC Issue Date should be less than TWIC Expiry Date");
                    return false;
                }
            }


            if ($("#txtUSIssue").length > 0) {
                var date1 = document.getElementById("txtUSIssue").value;
                if ($.trim($("#txtUSIssue").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid US visa Issue Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }


            if ($("#txtUSExpiry").length > 0) {
                var date1 = document.getElementById("txtUSExpiry").value;
                if ($.trim($("#txtUSExpiry").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid US visa expiry Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }
            if ($("#txtUSIssue").length > 0) {

                if (DateAsFormat(document.getElementById("txtUSIssue").value, strDateFormat) > new Date()) {
                    alert("US VISA Issue Date can not  be future Date");
                    return false;
                }
            }
            if ($("#txtUSIssue").length > 0 && $("#txtUSExpiry").length > 0) {
                if (DateAsFormat(document.getElementById("txtUSIssue").value, strDateFormat) > DateAsFormat(document.getElementById("txtUSExpiry").value, strDateFormat)) {
                    alert("US Issue Date should be less than US Expiry Date");
                    return false;
                }
            }



            if ($("#txtHireDate").length > 0) {
                var date1 = document.getElementById("txtHireDate").value;
                if ($.trim($("#txtHireDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid Hire Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }

            if ($("#ddlNature option:selected").val() == "1") {
                if ($("#txtNatureDate").length > 0) {
                    var date1 = document.getElementById("txtNatureDate").value;
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid Naturalization Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }
        });


    });
    
</script>
</html>
