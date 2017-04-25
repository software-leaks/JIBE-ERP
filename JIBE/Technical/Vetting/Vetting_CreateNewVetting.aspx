<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_CreateNewVetting.aspx.cs"
    Inherits="Technical_Vetting_Vetting_CreateNewVetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
</head>
<script language="javascript" type="text/javascript">


    function ValidateCreateVetting() {

        var VettingMode = '<%=VettingMode%>';
        var Vessel = document.getElementById($('[id$=ddlVessel]').attr('id')).value;
        if (Vessel == "") {

            Vessel = "0";
        }

        if (Vessel == "0") {
            alert("Select the Vessel.");
            document.getElementById($('[id$=ddlVessel]').attr('id')).focus();
            return false;
        }


        var VettingType = document.getElementById($('[id$=ddlType]').attr('id')).value;
        if (VettingType == "") {

            VettingType = "0";
        }
        if (VettingType == "0") {
            alert("Select the Vetting Type.");
            document.getElementById($('[id$=ddlType]').attr('id')).focus();
            return false;
        }


        if ($.trim($("#<%=txtDate.ClientID%>").val()) == "") {
            alert("Enter Date.");
            $("#<%=txtDate.ClientID%>").focus();
            return false;
        }

        if ($.trim($("#<%=txtDate.ClientID%>").val()) != "") {
            if (IsInvalidDate($.trim($("#<%=txtDate.ClientID%>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                alert("Enter valid Date<%=UDFLib.DateFormatMessage()%>.");
                $("#<%=txtDate.ClientID%>").focus();
                return false;
            }
        }


        if (VettingMode == "CV") {
            var Questionaire = document.getElementById($('[id$=ddlQuestionaire]').attr('id')).value;
            if (Questionaire == "") {

                Questionaire = "0";
            }
            if (Questionaire == "0") {
                alert("Select the Questionaire.");
                document.getElementById($('[id$=ddlQuestionaire]').attr('id')).focus();
                return false;
            }

        }
//        var OilMajor = document.getElementById($('[id$=ddlOilMajor]').attr('id')).value;
//        if (OilMajor == "") {

//            OilMajor = "0";
//        }
//        if (OilMajor == "0") {
//            alert("Select the Oil Major.");
//            document.getElementById($('[id$=ddlOilMajor]').attr('id')).focus();
//            return false;
//        }



        if (VettingMode == "PV") {

            var NoDays = document.getElementById($('[id$=txtDays]').attr('id')).value;
            if (NoDays == "") {
                alert("Enter Number of Days.");
                document.getElementById($('[id$=txtDays]').attr('id')).focus();
                return false;
            }

            var ddlInspector = document.getElementById($('[id$=ddlInspector]').attr('id')).value;
            if (ddlInspector == "") {

                ddlInspector = "0";
            }
            if (ddlInspector == "0") {
                alert("Select the Inspector.");
                document.getElementById($('[id$=ddlInspector]').attr('id')).focus();
                return false;
            }

//            if ($.trim($("#<%=txtResponseDate.ClientID%>").val()) == "") {
//                alert("Enter Response Next Due Date.");
//                $("#<%=txtResponseDate.ClientID%>").focus();
//                return false;
//            }

            if ($.trim($("#<%=txtResponseDate.ClientID%>").val()) != "") {
                if (IsInvalidDate($.trim($("#<%=txtResponseDate.ClientID%>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                    alert("Enter valid Response Next Due Date<%=UDFLib.DateFormatMessage()%>.");
                    $("#<%=txtResponseDate.ClientID%>").focus();
                    return false;
                }
            }



        }

    }


    function AddExternalInspector() {
        var Mode;
        document.getElementById('IframeAddExternalInspector').src = "Vetting_AddExternalIspector.aspx?Mode=Add";
        $("#dvAddExternalInspector").prop('title', 'Add Inspector');
        showModal('dvAddExternalInspector', null);
        return true;
    }

    function ChildPortCall(ddlVessel, ddlType, txtDate, ddlQuestionaire, ddlOilMajor, ddlInspector, ddlPort, txtDays, txtResponseDate) {
        var selectedValue = ddlVessel.value;
        parent.PortCall(ddlVessel.value, ddlType.value, txtDate.value, ddlQuestionaire.value, ddlOilMajor.value, ddlInspector.value, ddlPort.value, txtDays.value, txtResponseDate.value);
    }

    function SetNewInspector(inspectorID) {
        document.getElementById($('[id$=hdnInspectorId]').attr('id')).value = inspectorID;
        document.getElementById($('[id$=btnInspHidden]').attr('id')).click();
    }



    function AddAttachment(Vetting_ID) {
        document.getElementById('IframeAddAttachment').src = "Vetting_Attachments.aspx?Vetting_ID=" + Vetting_ID;
        $("#dvPopupAddAttachment").prop('title', 'Add Attachment');
        showModal('dvPopupAddAttachment');
        return;
    }
    function fnAllowNumeric() {
        if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8) {
            event.keyCode = 0;
            alert("Please enter only numeric value.");
            return;
        }
    }
    function UpdatePage() {

        hideModal("dvPopupAddAttachment");

    }

    function ImportConfirm(IsDataInValid, IsObsExists, VesselName, Port, VettingDate) {
        if (IsDataInValid == 1) {
            if (confirm('The following data from xml file does not match the existing data ' + VesselName + ',' + Port + ',' + VettingDate + ' .Are you sure you want to upload the XML file?')) {
                if (IsObsExists == 1) {
                    if (confirm('Warning! The vetting inspection already contains observation. Uploading the XML file will add the observations to the current observation, Are you sure you want to upload the XML file?')) {
                        document.getElementById($('[id$=btnconfirm]').attr('id')).click();
                    }

                }
                else {

                    document.getElementById($('[id$=btnconfirm]').attr('id')).click();
                }
            }
        }
        else {

            if (IsObsExists == 1) {
                if (confirm('Warning! The vetting inspection already contains observation. Uploading the XML file will add the observations to the current observation, Are you sure you want to upload the XML file?')) {
                    document.getElementById($('[id$=btnconfirm]').attr('id')).click();
                }

            }
            else {

                document.getElementById($('[id$=btnconfirm]').attr('id')).click();
            }
        }

    }

   
    
</script>
<style type="text/css">
    .VETDisableTextbox
    {
        border: 1px solid grey;
        color: grey;
        padding: 2px;
        background-color: #DCDCDC;
    }
</style>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
   <center>
        <div id="divLoggout" runat="server" visible="false" style="font-size:14px;color:Red;">
            Session expired!! Please log out and login again
        </div>
    </center>
<div id="divMain" runat="server">
        <div id="divCreateNewVetting" style="font-family: Tahoma; font-size: 11px; color: Black;">
            <asp:UpdatePanel runat="server" ID="uplCreateVetting" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="2" cellspacing="3" width="100%">
                        <tr id="trVesselName" runat="server">
                            <td style="padding-left: 100px;">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblVettingName" runat="server" Text=" Vetting Name" Visible="false"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtVettingName" CssClass="VETDisableTextbox" runat="server" MaxLength="200"
                                    Visible="false" Width="176px"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                            </td>
                            <td align="left" style="font-weight: bold;" class="style1">
                                <asp:Label ID="lblVesselName" runat="server" Text="Vessel :"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:Label ID="lblVesselMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                <asp:DropDownList ID="ddlVessel" Width="180px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblTypeDispaly" runat="server" Text="Type :"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTypeMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlType" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblDate" runat="server" Text="Date :"> </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDateMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDate" runat="server" Width="175px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr id="trImportXML" runat="server" visible="false">
                            <td>
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblXML" runat="server" Text="Import XML Report :"> </asp:Label>
                            </td>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:FileUpload ID="FileUpXMLReport" runat="server" Width="255px" Height="23px" />
                            </td>
                        </tr>
                        <tr id="trNoDays" runat="server">
                            <td align="right">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblNoDays" runat="server" Text="Number of Days"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:Label ID="lblNoDaysMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDays" runat="server" Width="175px" onkeypress="return fnAllowNumeric()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trQuestionnaire" runat="server">
                            <td align="right">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblQuestionaire" runat="server" Text="Questionnaire:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblQuestionaireMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlQuestionaire" Width="180px" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnQuestionnaireID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblOilMajor" runat="server" Text="Oil Major:"></asp:Label>
                            </td>
                            <td>
                               
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlOilMajor" Width="180px" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblInspector" runat="server" Text="Inspector:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInspMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlInspector" Width="180px" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Images/VetAddNew.png" ClientIDMode="Static" ID="ImgAdd"
                                    runat="server" Style="cursor: pointer; border-width: 0px; height: 16px; color: black;"
                                    ToolTip="Add inspector" OnClientClick="return AddExternalInspector();" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-left: 50px;">
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblPort" runat="server" Text="Port:"></asp:Label>
                            </td>
                            <td>
                                
                            </td>
                            <td align="left">
                                <asp:UpdatePanel ID="updatePort" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <ucDDL:ucCustomDropDownList ID="ddlPort" runat="server" UseInHeader="false" Width="180"
                                            Height="120" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="height: 30px;">
                                <asp:ImageButton ImageUrl="~/Images/VET_PortCall.png" ClientIDMode="Static" ID="imgbtnPort"
                                    runat="server" OnClientClick="return ChildPortCall(ddlVessel,ddlType,txtDate,ddlQuestionaire,ddlOilMajor,ddlInspector,ddlPort, txtDays,txtResponseDate);" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" style="font-weight: bold;">
                                <asp:Label ID="lblupload" runat="server" Text="  Upload Reports" Visible="false"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="btnAttachment" runat="server" ImageUrl="~/Images/VET_Attach.png"
                                    Width="15px" Visible="false" />
                            </td>
                        </tr>
                        <tr id="trresponseNext" runat="server">
                            <td align="right" style="width: 10%;">
                            </td>
                            <td align="left" style="font-weight: bold; width: 30%;">
                                <asp:Label ID="lblresponse" runat="server" Text="Response - Next Due"></asp:Label>
                            </td>
                            <td>
                             
                            </td>
                            <td align="left" style="width: 40%;">
                                <asp:TextBox ID="txtResponseDate" runat="server" Width="175px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalResponseDate" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txtResponseDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td style="width: 20%;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" colspan="2">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    Height="25px" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnCreatVetting" runat="server" Text="Create Vetting" Width="150px"
                                    OnClick="btnCreatVetting_Click" Height="25px" OnClientClick="return ValidateCreateVetting();" />
                                <asp:Button ID="btnInspHidden" runat="server" Style="visibility: hidden" OnClick="btnInspHidden_Click" />
                                <asp:Button ID="btnconfirm" runat="server" Width="150px" Style="display: none" Height="25px"
                                    OnClick="btnconfirm_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>                 
                    <asp:PostBackTrigger ControlID="btnCreatVetting" />
                    <asp:PostBackTrigger ControlID="btnconfirm" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="dvAddExternalInspector" style="display: none; width: 500px;" title="Add Inspector">
            <iframe id="IframeAddExternalInspector" src="" frameborder="0" style="height: 290px;
                width: 500px;"></iframe>
        </div>
        <div id="dvPopupAddAttachment" style="display: none; width: 550px; text-align: center;"
            title="Add Attachment">
            <iframe id="IframeAddAttachment" src="" frameborder="0" style="height: 320px; width: 550px;">
            </iframe>
        </div>
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnInspectorId" runat="server" />
                    <asp:HiddenField runat="server" ID="hdfPorCallID" />
                </td>
            </tr>
        </table>
       <asp:HiddenField ID="GUIDSession"  runat="server"/>
    </div>
    </form>
</body>
</html>
