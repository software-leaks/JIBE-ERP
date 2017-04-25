<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_AddExternalIspector.aspx.cs"
    Inherits="Technical_Vetting_Vetting_AddExternalIspector" EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <title></title>
    <script language="javascript" type="text/javascript">
        function ValidateInspector() {
            $("#divExternalInspector").prop('title', 'Add Inspector');
            $('#txtFirstName').val(' ');
            $('#txtLastName').val(' ');
            $('#txtCompany').val(' ');
            $('#txtDocumentType').val(' ');
            $('#txtDocumentNumber').val(' ');
            $("#<%= hdnInspectorId.ClientID%>").val('');
            $('#<%=DDLVetType.ClientID %>').val(0);
            $('#<%= UplImage.ClientID%>').prop('disabled', true);
            $('#tdDisplayImage').hide();

            return false;
        }

        function nospaces(t) {
            if (t.value.match(/\s/g)) {
                t.value = t.value.replace(/\s/g, '');
            }
        }

        function ValidateAddInspector() {


            if (document.getElementById($('[id$=txtFirstName]').attr('id')).value.trim() == "") {
                alert("Enter First Name.");
                document.getElementById($('[id$=txtFirstName]').attr('id')).focus();
                return false;
            }

            if (document.getElementById($('[id$=txtLastName]').attr('id')).value.trim() == "") {
                alert("Enter Last Name.");
                document.getElementById($('[id$=txtLastName]').attr('id')).focus();
                return false;
            }

            if (document.getElementById($('[id$=txtCompany]').attr('id')).value.trim() == "") {
                alert("Enter Company Name.");
                document.getElementById($('[id$=txtCompany]').attr('id')).focus();
                return false;
            }


            if ($('#<%=UplImage.ClientID %>')[0].value != "") {
                var sFileName = $("#<%=UplImage.ClientID %>")[0].value
                var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions.length; j++) {
                    var sCurExtension = _validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        $("#<%=hdnUploadFileName.ClientID %>").val(sFileName);
                        break;
                    }
                }
                if (!blnValid) {
                    alert("Only jpg, jpeg, bmp, gif, png image are allow to upload.");
                    document.getElementById($('[id$=txtDocumentNumber]').attr('id')).focus();
                    return false;
                }
            }
        }
  
    </script>
    <style type="text/css">
        .autocomplete_CList
        {
            position: absolute;
            margin-top: 0px;
            margin-left: -40px;
            height: 10px;
        }
        ul
        {
            list-style-type: none;
        }
        li
        {
            display: block;
            line-height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div id="divExternalInspector" style="font-family: Tahoma; text-align: left; font-size: 11px;
        color: Black;">
        <asp:UpdatePanel runat="server" ID="upoilmajor">
            <ContentTemplate>
                <table cellpadding="2" cellspacing="2" style="padding-top: 10px" width="100%">
                    <tr>
                        <td align="left" style="width: 40%">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name" Style="font-weight: bold;"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFirstNameMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left" style="width: 40%;">
                            <asp:TextBox ID="txtFirstName" MaxLength="200" TabIndex="1" Width="180px" runat="server"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderFN" TargetControlID="txtFirstName"
                                CompletionSetCount="20" MinimumPrefixLength="3" ServiceMethod="VET_Get_AutoComplete_ExtInspectorFNList"
                                CompletionInterval="1000" runat="server" EnableCaching="true" FirstRowSelected="false"
                                CompletionListCssClass="autocomplete_CList">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td rowspan="4" style="vertical-align: top; width: 20%;">
                            <table style="height: 60px; width: 80px;">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgInspector" runat="server" Style="max-width: 80px;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 30%">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name" Style="font-weight: bold;"></asp:Label>
                            <td>
                                <asp:Label ID="lblLastMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            </td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLastName" MaxLength="200" TabIndex="2" Width="180px" runat="server"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderLN" TargetControlID="txtLastName"
                                CompletionSetCount="20" MinimumPrefixLength="3" ServiceMethod="VET_Get_AutoComplete_ExtInspectorLNList"
                                CompletionInterval="1000" runat="server" EnableCaching="true" FirstRowSelected="false"
                                CompletionListCssClass="autocomplete_CList">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 30%">
                            <asp:Label ID="lblCompany" runat="server" Text="Company" Style="font-weight: bold;"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCompMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCompany" ClientIDMode="Static" MaxLength="200" TabIndex="3" Width="180px"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 30%">
                            <asp:Label ID="lblDocumentType" runat="server" Text="Document Type" Style="font-weight: bold;"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDocumentType" ClientIDMode="Static" MaxLength="200" TabIndex="4"
                                Width="180px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 30%">
                            <asp:Label ID="lblDocumentNumber" runat="server" Text="Document Number" Style="font-weight: bold;"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDocumentNumber" ClientIDMode="Static" MaxLength="200" TabIndex="5"
                                Width="180px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 30%">
                            <asp:Label ID="lblVettingType" runat="server" Text="Vetting Type" Style="font-weight: bold;"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblVettingtypeMan" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <CustomFilter:ucfDropdown ID="DDLVetType" runat="server" Height="65" UseInHeader="false"
                                        Width="180" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr id="trImageUpload">
                        <td align="left" style="width: 30%;">
                            <asp:Label ID="lblInspectorImage" runat="server" Text="Inspector Image" Style="font-weight: bold;"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="UplImage" runat="server" Enabled="false" />
                            <asp:HiddenField ID="hdnUploadFileName" runat="server" Value="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" style="padding-top: 20px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ClientIDMode="Static"
                                Height="25px" TabIndex="7" Width="75px" OnClientClick="return ValidateAddInspector();" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnInspectorId" runat="server"></asp:HiddenField>
    </div>
    </form>
</body>
</html>
