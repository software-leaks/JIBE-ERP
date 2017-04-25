<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Vetting_CreateNewQuestionnaire.aspx.cs"
    Inherits="Technical_Vetting_CreateNewVettingQuestionnaire" %>
    
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
        <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function onSaveQuestionnaire() {
            var ModuleId = document.getElementById($('[id$=DDLModule]').attr('id')).value;
            var VettingTypeId = document.getElementById($('[id$=DDLVetType]').attr('id')).value;
            var QuestName = document.getElementById($('[id$=txtQuestionnaireName]').attr('id')).value.trim();
            var Number = document.getElementById($('[id$=txtNumber]').attr('id')).value.trim();
            var Version = document.getElementById($('[id$=txtVersion]').attr('id')).value.trim();
            if (ModuleId == "") {

                ModuleId = "0";
            }
            if (document.getElementById($('[id$=DDLModule]').attr('id')).value == "0") {
                alert("Please select Module.");
                return false;
            }

            if (VettingTypeId == "") {

                VettingTypeId = "0";
            }
            if (document.getElementById($('[id$=DDLVetType]').attr('id')).value == "0") {
                alert("Please select Type.");
                return false;
            }

            if (QuestName == "") {
                alert("Questionnaire Name is required.");
                document.getElementById($('[id$=txtQuestionnaireName]').attr('id')).focus();
                return false;
            }
            if (Number == "") {
                alert("Number is required.");
                document.getElementById($('[id$=txtNumber]').attr('id')).focus();
                return false;
            }
            if (isNaN($.trim($("#" + $('[id$=txtNumber]').attr('id')).val()))) {
                alert("Invalid Number");
                $("#" + $('[id$=txtNumber]').attr('id')).focus();
                return false;
            }
            if (Version == "") {
                alert("Version is required.");
                document.getElementById($('[id$=txtVersion]').attr('id')).focus();
                return false;
            }
        }

        $(window).load(function () {
            $('.version').keypress(function (event) {
                if (event.which < 46 || event.which >= 58 || event.which == 47) {
                    event.preventDefault();
                }

                if (event.which == 46 && $(this).val().indexOf('.') > 20) {
                    this.value = '';
                }
            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdPnlAddQuestionnaire" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div style="font-family: Tahoma; font-size: 11px; color: Black;">
                    <table width="100%" cellpadding="5" cellspacing="2" style="padding-top: 10px;">
                        <tr>
                            <td align="left" >
                                <asp:Label ID="lblModule" runat="server" Text=" Module :" Font-Bold="true"></asp:Label>                               
                            </td>
                            <td colspan="2">
                             <asp:Label ID="lblModuleMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left" >
                                <asp:DropDownList ID="DDLModule" runat="server" AutoPostBack="true" Width="180px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" >
                                <asp:Label ID="lblVettingType" runat="server" Text=" Type :" Font-Bold="true"></asp:Label>                                
                            </td>
                            <td colspan="2">
                            <asp:Label ID="lblVewttingTypMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left" >
                                <asp:DropDownList ID="DDLVetType" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                    Width="180px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" >
                                <asp:Label ID="lblVesselType" runat="server" Text=" Vessel Type :" Font-Bold="true"></asp:Label>                               
                            </td>
                            <td colspan="2">
                             <asp:Label ID="lblVesselTypMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left" >
                                              
                                   <CustomFilter:ucfDropdown ID="DDLVesselType" runat="server" UseInHeader="false"  Width="180" Height="130" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  width="160px">
                                <asp:Label ID="lblQuestionnaireName" runat="server" Text="Questionnaire Name :" 
                                    Font-Bold="true"></asp:Label>                              
                            </td>
                            <td colspan="2">
                              <asp:Label ID="lblQuestionnareMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left" >
                                <asp:TextBox ID="txtQuestionnaireName" runat="server" Width="177px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" >
                                <asp:Label ID="lblNumber" runat="server" Text="Number :" Font-Bold="true"></asp:Label>                                
                            </td>
                            <td colspan="2">
                            <asp:Label ID="lblNumberMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtNumber" runat="server" Width="177px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" >
                                <asp:Label ID="lblVersion" runat="server" Text="Version :" Font-Bold="true"></asp:Label>
                               
                            </td>
                            <td colspan="2">
                             <asp:Label ID="lblVersionMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtVersion" runat="server" Width="177px" MaxLength="18" CssClass="version"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-right: auto; padding-top: 16px" colspan="2">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" Height="25px"
                                    OnClick="btnCancel_Click" />
                            </td>
                            <td align="right" style="padding-top: 16px" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Save and add questions" Width="155px"
                                    Height="25px" OnClick="btnSave_Click" OnClientClick="return onSaveQuestionnaire();" />
                                <asp:HiddenField ID="hdnQuestionnaireId" runat="server" />
                                <asp:HiddenField ID="hdnQuestnirStatus" runat="server" />
                            </td>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
