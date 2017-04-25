<%@ Page Title="Engine Log Book Threshold" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ERLogBookThresHold.aspx.cs" Inherits="ERLogBookThresHold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/Wizard.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.highlight.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ViewEdit(Id) {
            var value = document.getElementById('<%=lblLogId.ClientID%>').value;
            window.open("ERLogEdit.aspx?ViewID=" + Id + "&LOGID=" + value, "Test", "", "");
        }

        function MaskMoney(evt) {
            if (!(evt.keyCode == 9 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 190 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split('.');

            if (parts.length > 2) return false;
            if ((evt.keyCode == 46) && (parts.length == 1)) return false;
            if ((evt.keyCode == 190) && (parts.length == 2)) return false;
            if (parts[0].length >= 14) return false;

        }

        function MaskMoney_Minus(evt) {
            if (!(evt.keyCode == 189 || evt.keyCode == 9 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 190 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split('.');

            if (evt.srcElement.value.length > 0 && evt.keyCode == 189) return false;
            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if ((evt.keyCode == 190) && (parts.length == 2)) return false;
            if (parts[0].length >= 14) return false;

        }

        function validation() {


            var e = document.getElementById('<%=ddlVessel.ClientID %>');
            var f = document.getElementById('<%=ddlVesselMain.ClientID %>');

            if (e.selectedIndex == 0) {
                alert("Copy threshold from Vessel is not selected.");
                e.focus();
                return false;
            }

            if (f.selectedIndex == 0) {
                alert("Vessel is not selected.");
                f.focus();
                return false;
            }

            if (e.selectedIndex == f.selectedIndex) {
                alert("Copy threshold from vessel and current vessel should not be the same.");
                f.focus();
                return false;
            }
        }

        function alertmessage(a) {
            if (a == 0) {
                alert("Copy threshold from vessel values are not present in the database.");
            }
            if (a == 1) {
                alert("Threshold value's are updated.");
            }
        }
 
    </script>
    <style type="text/css">
        .CellClass1
        {
            background-color: Red;
            color: White;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        .CellClass0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        .style1
        {
            width: 235px;
        }
        .style2
        {
            height: 10px;
            width: 235px;
        }
        .page
        {
            width: 80%;
        }
        
        .ErrorControl
        {
            background-color: #FBE3E4;
            border: solid 1px Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updMain" runat="server">
        <ContentTemplate>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>ENGINE LOG BOOK THRESHOLD </b>
                </div>
                <asp:TextBox ID="lblLogId" runat="server"></asp:TextBox>
                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                    border-color: #efefef; width: 100%; border-collapse: collapse;">
                    <tr>
                        <td style="width: 10%; text-align: center;">
                            Vessel Name :
                        </td>
                        <td style="text-align: Left;">
                            <asp:DropDownList ID="ddlVesselMain" runat="server" AutoPostBack="true" Width="160"
                                OnSelectedIndexChanged="ddlVesselMain_SelectedIndexChanged" />
                        </td>
                        <td style="width: 10%; text-align: center;">
                            <asp:Label ID="lblVersion" runat="server" Text="History:"> </asp:Label>
                        </td>
                        <td style="text-align: Left;">
                            <asp:DropDownList ID="DDLVersion" runat="server" UseInHeader="false" AutoPostBack="true"
                                Width="200" OnSelectedIndexChanged="DDLVersion_SelectedIndexChanged" />
                        </td>
                        </td>
                        <td style="text-align: center;">
                            <input type="button" onclick="showPopup('ThresholdActionSettingDew')" value="Threshold Action Settings" />
                        </td>
                        <td style="width: 35%; text-align: center;">
                            <div style="border: 1px solid #0489B1; background-color: #A9D0F5; padding: 1px;">
                                Copy threshold from Vessel :
                                <asp:DropDownList ID="ddlVessel" runat="server">
                                </asp:DropDownList>
                                <asp:Button ID="btnCopy" runat="server" Text="Copy Threshold" OnClick="btnCopy_Click"
                                    OnClientClick="return validation();" />
                            </div>
                        </td>
                        <td style="width: 8%; text-align: center;">
                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" BackColor="#AED7FF"
                                Width="80px" Font-Size="12px" Font-Bold="true" Font-Names="verdana" Height="25px"
                                ForeColor="Blue" />
                        </td>
                    </tr>
                </table>
                <div style="text-align: left; overflow: scroll" id="dvPageContent" class="page-content-main">
                    <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tr>
                            <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                                align="left" valign="top">
                                <asp:FormView ID="FormView1" runat="server" Height="60px" Width="100%" BorderWidth="0px"
                                    Font-Size="Small">
                                    <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                            border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td width="100%" colspan="3" valign="top">
                                                    <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: #efefef;
                                                        border-color: #efefef; width: 100%;">
                                                        <tr align="center" style="background-color: #BCF5A9">
                                                            <td align="center" colspan="2">
                                                                MAIN ENGINES
                                                            </td>
                                                            <td colspan="14" align="center">
                                                                MAIN ENGINE TURBO BLOWERS
                                                            </td>
                                                        </tr>
                                                        <tr class="HeaderCellColor" align="center">
                                                            <td rowspan="3">
                                                            </td>
                                                            <td rowspan="2">
                                                                TEMPERATURE C
                                                            </td>
                                                            <td colspan="7" align="center">
                                                                TEMPERATURE C
                                                            </td>
                                                            <td>
                                                                PRESSURE
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td colspan="2">
                                                                EXHAUST GAS
                                                            </td>
                                                            <td colspan="2">
                                                                AIR COOLER AIR
                                                            </td>
                                                            <td rowspan="2">
                                                                <label id="Label187" class="verticaltext1">
                                                                    Scaverage</label>
                                                            </td>
                                                            <td colspan="2">
                                                                LUB OIL
                                                            </td>
                                                            <td>
                                                                Press Drop mm Wc
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td>
                                                                EXHAUST
                                                            </td>
                                                            <td>
                                                                INLET
                                                            </td>
                                                            <td>
                                                                OUTLET
                                                            </td>
                                                            <td>
                                                                IN
                                                            </td>
                                                            <td>
                                                                OUT
                                                            </td>
                                                            <td>
                                                                B
                                                            </td>
                                                            <td>
                                                                T
                                                            </td>
                                                            <td>
                                                                AIR COOLER
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 0px solid ActiveBorder;">
                                                            <td align="left" style="height: 19px; background-color: #BCF5A9">
                                                                <asp:Label ID="LblMin" Width="60px" runat="server" Text='Min'> </asp:Label>
                                                                <asp:Label ID="lblVessel" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "VESSEL_ID")%>'> </asp:Label>
                                                                <asp:Label ID="lblThresHoldId" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'> </asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_TEMP_EXH_Min" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_TEMP_EXH_Min")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CVME_TEMP_EXH" runat="server" ControlToValidate="txtME_TEMP_EXH_Max"
                                                                    ControlToCompare="txtME_TEMP_EXH_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_INLET_MIN" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_INLET_MIN")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_T_EXH_INLET" runat="server" ControlToValidate="txtMETB_T_EXH_INLET_MAX"
                                                                    ControlToCompare="txtMETB_T_EXH_INLET_MIN" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_OUTLET_MIN" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_OUTLET_MIN")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_T_EXH_OUTLET" runat="server" ControlToValidate="txtMETB_T_EXH_OUTLET_Max"
                                                                    ControlToCompare="txtMETB_T_EXH_OUTLET_MIN" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_AIR_IN_MIN" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_AIR_IN_MIN")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_T_EXH_AIR_IN" runat="server" ControlToValidate="txtMETB_T_EXH_AIR_IN_Max"
                                                                    ControlToCompare="txtMETB_T_EXH_AIR_IN_MIN" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_AIR_OUT_MIN" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_AIR_OUT_Min")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_T_EXH_AIR_OUT" runat="server" ControlToValidate="txtMETB_T_EXH_AIR_OUT_Max"
                                                                    ControlToCompare="txtMETB_T_EXH_AIR_OUT_MIN" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_SCAVENGE_MIN" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_SCAVENGE_Min")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_T_SCAVENGE_MIN" runat="server" ControlToValidate="txtMETB_T_SCAVENGE_Max"
                                                                    ControlToCompare="txtMETB_T_SCAVENGE_MIN" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_LO_B_MIN" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_LO_B_Min")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_T_LO_B_MIN" runat="server" ControlToValidate="txtMETB_T_LO_B_MAX"
                                                                    ControlToCompare="txtMETB_T_LO_B_MIN" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_LO_T_Min" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_LO_T_Min")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_T_LO_T_Min" runat="server" ControlToValidate="txtMETB_T_LO_T_Max"
                                                                    ControlToCompare="txtMETB_T_LO_T_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_P_PD_AC_Min" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_P_PD_AC_Min")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_METB_P_PD_AC_Min" runat="server" ControlToValidate="txtMETB_P_PD_AC_Max"
                                                                    ControlToCompare="txtMETB_P_PD_AC_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 19px;">
                                                            <td align="left" style="height: 19px; background-color: #F78181">
                                                                <asp:Label ID="LabelMax1" runat="server" Width="90%" Text='Max'> </asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_TEMP_EXH_Max" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_TEMP_EXH_MAX")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_INLET_MAX" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_INLET_MAX")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_OUTLET_Max" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_OUTLET_Max")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_AIR_IN_Max" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_AIR_IN_Max")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_EXH_AIR_OUT_Max" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_EXH_AIR_OUT_Max")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_SCAVENGE_Max" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_SCAVENGE_Max")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_LO_B_MAX" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_LO_B_Max")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_T_LO_T_Max" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_T_LO_T_Max")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMETB_P_PD_AC_Max" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "METB_P_PD_AC_Max")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" colspan="3" valign="top">
                                                    <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: #efefef;
                                                        border-color: #efefef; width: 100%;">
                                                        <tr class="HeaderCellColor1">
                                                            <td colspan="31" align="center">
                                                                MAIN ENGINES
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td rowspan="4">
                                                            </td>
                                                            <td colspan="7">
                                                                TEMPERATURE C
                                                            </td>
                                                            <td colspan="6">
                                                                HELTH EXCHANGERS TEMPERATURES C
                                                            </td>
                                                            <td colspan="7">
                                                                PRESSURE kg/cm2
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td colspan="2">
                                                                MAIN BEARING
                                                            </td>
                                                            <td colspan="2">
                                                                JACKET COOLING
                                                            </td>
                                                            <td colspan="2">
                                                                PISTON COOLING
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label190" class="verticaltext">
                                                                    FUEL OIL</label>
                                                            </td>
                                                            <td colspan="2">
                                                                JACKET COOLER
                                                            </td>
                                                            <td colspan="2">
                                                                L O COOLER
                                                            </td>
                                                            <td colspan="2">
                                                                PISTON COOLER
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label192" class="verticaltext">
                                                                    Jaket Water</label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label193" class="verticaltext">
                                                                    Bearing & X-hd LO</label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label194" class="verticaltext">
                                                                    Canshaft LO</label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label195" class="verticaltext">
                                                                    F V Cooling</label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label196" class="verticaltext">
                                                                    Fuel Oil</label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label197" class="verticaltext">
                                                                    Piston Cooling</label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label198" class="verticaltext">
                                                                    Control Air</label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td rowspan="2">
                                                                IN
                                                            </td>
                                                            <td rowspan="2">
                                                                OUTLET
                                                            </td>
                                                            <td rowspan="2">
                                                                IN
                                                            </td>
                                                            <td rowspan="2">
                                                                OUTLET
                                                            </td>
                                                            <td rowspan="2">
                                                                IN
                                                            </td>
                                                            <td rowspan="2">
                                                                OUTLET
                                                            </td>
                                                            <td colspan="2">
                                                                FW
                                                            </td>
                                                            <td colspan="2">
                                                                LO
                                                            </td>
                                                            <td colspan="2">
                                                                SW/FW
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td>
                                                                IN
                                                            </td>
                                                            <td>
                                                                OUT
                                                            </td>
                                                            <td>
                                                                IN
                                                            </td>
                                                            <td>
                                                                OUT
                                                            </td>
                                                            <td>
                                                                IN
                                                            </td>
                                                            <td>
                                                                OUT
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid ActiveBorder">
                                                            <td align="left" style="height: 19px; background-color: #BCF5A9" width="60px">
                                                                <asp:Label ID="Label159" runat="server" Text='Min' Width="50px"> </asp:Label>
                                                                <asp:Label ID="lblid" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                                <asp:Label ID="lblLogId" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                                <asp:Label ID="lblVessel3" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_MB_IN_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_MB_IN_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_MB_IN_Min" runat="server" ControlToValidate="txtME_MB_IN_Max"
                                                                    ControlToCompare="txtME_MB_IN_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_MB_OUT_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_MB_OUT_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_MB_OUT_Min" runat="server" ControlToValidate="txtME_MB_OUT_Max"
                                                                    ControlToCompare="txtME_MB_OUT_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_IN_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_IN_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_JC_IN_Min" runat="server" ControlToValidate="txtME_JC_IN_Max"
                                                                    ControlToCompare="txtME_JC_IN_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_OUT_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_OUT_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_JC_OUT_Min" runat="server" ControlToValidate="txtME_JC_OUT_Max"
                                                                    ControlToCompare="txtME_JC_OUT_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_IN_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_IN_min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_PC_IN_Min" runat="server" ControlToValidate="txtME_PC_IN_Max"
                                                                    ControlToCompare="txtME_PC_IN_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_OUT_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_OUT_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_PC_OUT_Min" runat="server" ControlToValidate="txtME_PC_OUT_Max"
                                                                    ControlToCompare="txtME_PC_OUT_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_FUEL_OIL_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_OIL_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_FUEL_OIL_Min" runat="server" ControlToValidate="txtME_FUEL_OIL_Max"
                                                                    ControlToCompare="txtME_FUEL_OIL_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_FW_IN_min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_FW_IN_MIN")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_JC_FW_IN_min" runat="server" ControlToValidate="txtME_JC_FW_IN_Max"
                                                                    ControlToCompare="txtME_JC_FW_IN_min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_FW_OUT_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_FW_OUT_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_JC_FW_OUT_Min" runat="server" ControlToValidate="txtME_JC_FW_OUT_Max"
                                                                    ControlToCompare="txtME_JC_FW_OUT_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_LC_LO_IN_min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LC_LO_IN_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_LC_LO_IN_min" runat="server" ControlToValidate="txtME_LC_LO_IN_Max"
                                                                    ControlToCompare="txtME_LC_LO_IN_min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_LC_LO_OUT_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LC_LO_OUT_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_LC_LO_OUT_Min" runat="server" ControlToValidate="txtME_LC_LO_OUT_Max"
                                                                    ControlToCompare="txtME_LC_LO_OUT_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_LO_IN_min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_LO_IN_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_PC_LO_IN_min" runat="server" ControlToValidate="txtME_PC_LO_IN_Max"
                                                                    ControlToCompare="txtME_PC_LO_IN_min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_LO_OUT_min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_LO_OUT_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_PC_LO_OUT_min" runat="server" ControlToValidate="txtME_PC_LO_OUT_Max"
                                                                    ControlToCompare="txtME_PC_LO_OUT_min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_JACKET_WATER_min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_JACKET_WATER_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_P_JACKET_WATER_min" runat="server" ControlToValidate="txtME_P_JACKET_WATER_Max"
                                                                    ControlToCompare="txtME_P_JACKET_WATER_min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_BEARING_XND_LO_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_BEARING_XND_LO_MIN")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_P_BEARING_XND_LO_Min" runat="server" ControlToValidate="txtME_P_BEARING_XND_LO_Max"
                                                                    ControlToCompare="txtME_P_BEARING_XND_LO_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_CAMSHAFT_LO_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_CAMSHAFT_LO_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_P_CAMSHAFT_LO_Min" runat="server" ControlToValidate="txtME_P_CAMSHAFT_LO_Max"
                                                                    ControlToCompare="txtME_P_CAMSHAFT_LO_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_FV_COOLING_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_FV_COOLING_MIN")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_P_FV_COOLING_Min" runat="server" ControlToValidate="txtME_P_FV_COOLING_Max"
                                                                    ControlToCompare="txtME_P_FV_COOLING_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_FUEL_OIL_min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_FUEL_OIL_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_P_FUEL_OIL_min" runat="server" ControlToValidate="txtME_P_FUEL_OIL_Max"
                                                                    ControlToCompare="txtME_P_FUEL_OIL_min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_PISTON_COOLING_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_PISTON_COOLING_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_P_PISTON_COOLING_Min" runat="server" ControlToValidate="txtME_P_PISTON_COOLING_Max"
                                                                    ControlToCompare="txtME_P_PISTON_COOLING_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_CONTROL_AIR_min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_CONTROL_AIR_MIN")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_ME_P_CONTROL_AIR_min" runat="server" ControlToValidate="txtME_P_CONTROL_AIR_Max"
                                                                    ControlToCompare="txtME_P_CONTROL_AIR_min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid ActiveBorder">
                                                            <td align="left" style="height: 19px; background-color: #F78181">
                                                                <asp:Label ID="Label1" runat="server" Text='Max'> </asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_MB_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_MB_IN_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_MB_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_MB_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_IN_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_IN_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_FUEL_OIL_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_OIL_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_FW_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_FW_IN_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_JC_FW_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_JC_FW_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_LC_LO_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LC_LO_IN_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_LC_LO_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LC_LO_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_LO_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_LO_IN_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_PC_LO_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_PC_LO_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_JACKET_WATER_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_JACKET_WATER_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_BEARING_XND_LO_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_BEARING_XND_LO_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_CAMSHAFT_LO_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_CAMSHAFT_LO_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_FV_COOLING_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_FV_COOLING_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_FUEL_OIL_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_FUEL_OIL_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_PISTON_COOLING_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_PISTON_COOLING_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtME_P_CONTROL_AIR_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_P_CONTROL_AIR_Max")%>'> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" valign="top">
                                                    <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: #efefef;
                                                        border-color: #efefef; width: 100%;">
                                                        <tr align="center" class="HeaderCellColor1">
                                                            <td>
                                                            </td>
                                                            <td colspan="3">
                                                                REFRIGERATION PLANT
                                                            </td>
                                                            <td colspan="3">
                                                                FRESH WATER GENERATOR
                                                            </td>
                                                            <td colspan="2">
                                                                PURIFIERS
                                                            </td>
                                                            <td colspan="7">
                                                                MISCELLANEOUS
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td rowspan="3">
                                                            </td>
                                                            <td colspan="3">
                                                                Temp C
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label200" class="verticaltext">
                                                                    Vacuum cm. Hg
                                                                </label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label201" class="verticaltext">
                                                                    Shell Temp C
                                                                </label>
                                                            </td>
                                                            <td rowspan="3">
                                                                <label id="Label202" class="verticaltext">
                                                                    Salinity PPM
                                                                </label>
                                                            </td>
                                                            <td>
                                                                HO
                                                            </td>
                                                            <td>
                                                                LO
                                                            </td>
                                                            <td colspan="7">
                                                                Temperature C
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td rowspan="2">
                                                                <label id="Label214" class="verticaltext1">
                                                                    Meat
                                                                </label>
                                                            </td>
                                                            <td rowspan="2">
                                                                <label id="Label215" class="verticaltext1">
                                                                    Fish
                                                                </label>
                                                            </td>
                                                            <td rowspan="2">
                                                                <label id="Label216" class="verticaltext1">
                                                                    Veg/Labby
                                                                </label>
                                                            </td>
                                                            <td rowspan="2">
                                                                Temp
                                                            </td>
                                                            <td rowspan="2">
                                                                Temp
                                                            </td>
                                                            <td rowspan="2">
                                                                <label id="Label222" class="verticaltext1">
                                                                    Thust bng.
                                                                </label>
                                                            </td>
                                                            <td rowspan="2">
                                                                <label id="Label223" class="verticaltext1">
                                                                    intam big
                                                                </label>
                                                            </td>
                                                            <td rowspan="2">
                                                                <label id="Label224" class="verticaltext1">
                                                                    Stem tube Oil
                                                                </label>
                                                            </td>
                                                            <td colspan="2" width="40px">
                                                                H.O. Sett.
                                                            </td>
                                                            <td colspan="2" width="40px">
                                                                H.O. Serv.
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td width="40px">
                                                                1
                                                            </td>
                                                            <td width="40px">
                                                                2
                                                            </td>
                                                            <td width="40px">
                                                                1
                                                            </td>
                                                            <td width="40px">
                                                                2
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid ActiveBorder">
                                                            <td align="left" style="height: 19px; width: 60px; background-color: #BCF5A9">
                                                                <asp:Label ID="Label59" runat="server" Width="20px"> Min </asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtREF_MEAT_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_MEAT_TEMP_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_REF_MEAT_TEMP_Min" runat="server" ControlToValidate="txtREF_MEAT_TEMP_Max"
                                                                    ControlToCompare="txtREF_MEAT_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtREF_FISH_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_FISH_TEMP_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_REF_FISH_TEMP_Min" runat="server" ControlToValidate="txtREF_FISH_TEMP_Max"
                                                                    ControlToCompare="txtREF_FISH_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtREF_VEG_LOBBY_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_VEG_LOBBY_TEMP_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_REF_VEG_LOBBY_TEMP_Min" runat="server" ControlToValidate="txtREF_VEG_LOBBY_TEMP_Max"
                                                                    ControlToCompare="txtREF_VEG_LOBBY_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtFWGEN_VACCUM_Min" runat="server" Width="50px" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_VACCUM_min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_FWGEN_VACCUM_Min" runat="server" ControlToValidate="TxtFWGEN_VACCUM_Max"
                                                                    ControlToCompare="txtFWGEN_VACCUM_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtFWGEN_SHELL_TEMP_Min" runat="server" Width="50px" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SHELL_TEMP_min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_FWGEN_SHELL_TEMP_Min" runat="server" ControlToValidate="TxtFWGEN_SHELL_TEMP_Max"
                                                                    ControlToCompare="txtFWGEN_SHELL_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtFWGEN_SALINITY_PPM_Min" runat="server" Width="50px" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SALINITY_PPM_min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_FWGEN_SALINITY_PPM_Min" runat="server" ControlToValidate="txtFWGEN_SALINITY_PPM_Max"
                                                                    ControlToCompare="txtFWGEN_SALINITY_PPM_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtPUR_HO_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_TEMP_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_PUR_HO_TEMP_Min" runat="server" ControlToValidate="TxtPUR_HO_TEMP_Max"
                                                                    ControlToCompare="txtPUR_HO_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtPUR_LO_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_TEMP_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_PUR_LO_TEMP_Min" runat="server" ControlToValidate="TxtPUR_LO_TEMP_Max"
                                                                    ControlToCompare="txtPUR_LO_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_THRUST_BRG_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_THRUST_BRG_TEMP_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_MISC_THRUST_BRG_TEMP_Min" runat="server" ControlToValidate="TxtMISC_THRUST_BRG_TEMP_Max"
                                                                    ControlToCompare="txtMISC_THRUST_BRG_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_INTERM_BRG_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INTERM_BRG_TEMP_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_MISC_INTERM_BRG_TEMP_Min" runat="server" ControlToValidate="txtMISC_INTERM_BRG_TEMP_Max"
                                                                    ControlToCompare="txtMISC_INTERM_BRG_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_STERN_TUBE_OIL_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_STERN_TUBE_OIL_TEMP_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_MISC_STERN_TUBE_OIL_TEMP_Min" runat="server" ControlToValidate="txtMISC_STERN_TUBE_OIL_TEMP_Max"
                                                                    ControlToCompare="txtMISC_STERN_TUBE_OIL_TEMP_Min" Operator="GreaterThanEqual"
                                                                    SetFocusOnError="true" Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SETT_1_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_1_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_MISC_HO_SETT_1_Min" runat="server" ControlToValidate="txtMISC_HO_SETT_1_Max"
                                                                    ControlToCompare="txtMISC_HO_SETT_1_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SETT_2_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_2_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_MISC_HO_SETT_2_Min" runat="server" ControlToValidate="txtMISC_HO_SETT_2_Max"
                                                                    ControlToCompare="txtMISC_HO_SETT_2_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SERV_1_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_1_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_MISC_HO_SERV_1_Min" runat="server" ControlToValidate="txtMISC_HO_SERV_1_Max"
                                                                    ControlToCompare="txtMISC_HO_SERV_1_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SERV_2_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_2_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_MISC_HO_SERV_2_Min" runat="server" ControlToValidate="txtMISC_HO_SERV_2_Max"
                                                                    ControlToCompare="txtMISC_HO_SERV_2_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid ActiveBorder">
                                                            <td align="left" style="height: 19px; background-color: #F78181">
                                                                <asp:Label ID="Label5" runat="server" Width="60px">Max</asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtREF_MEAT_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_MEAT_TEMP_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtREF_FISH_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_FISH_TEMP_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtREF_VEG_LOBBY_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_VEG_LOBBY_TEMP_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="TxtFWGEN_VACCUM_Max" runat="server" Width="50px" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_VACCUM_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="TxtFWGEN_SHELL_TEMP_Max" runat="server" Width="50px" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SHELL_TEMP_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtFWGEN_SALINITY_PPM_Max" runat="server" Width="50px" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SALINITY_PPM_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="TxtPUR_HO_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_TEMP_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="TxtPUR_LO_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_TEMP_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="TxtMISC_THRUST_BRG_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_THRUST_BRG_TEMP_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_INTERM_BRG_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INTERM_BRG_TEMP_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_STERN_TUBE_OIL_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_STERN_TUBE_OIL_TEMP_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SETT_1_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_1_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SETT_2_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_2_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SERV_1_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_1_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtMISC_HO_SERV_2_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_2_Max")%>'> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: #efefef;
                                                        border-color: #efefef; width: 100%;">
                                                        <tr class="HeaderCellColor1">
                                                            <td colspan="9" align="center">
                                                                GENERATOR ENGINES
                                                            </td>
                                                            <td colspan="2" align="center">
                                                                SHAFT GENERATOR
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td rowspan="4">
                                                            </td>
                                                            <td colspan="6">
                                                                Temperature C
                                                            </td>
                                                            <td colspan="2">
                                                                Pressure-kg/cm2
                                                            </td>
                                                            <td rowspan="4">
                                                                LO Press
                                                            </td>
                                                            <td rowspan="4">
                                                                L. O. Temp
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td colspan="2" rowspan="2">
                                                                Exhaust
                                                            </td>
                                                            <td colspan="2" rowspan="2">
                                                                CW
                                                            </td>
                                                            <td colspan="2" rowspan="2">
                                                                LO
                                                            </td>
                                                            <td rowspan="3">
                                                                L. O.
                                                            </td>
                                                            <td rowspan="3">
                                                                C. W.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td>
                                                                Max
                                                            </td>
                                                            <td>
                                                                Min
                                                            </td>
                                                            <td>
                                                                In
                                                            </td>
                                                            <td>
                                                                Out
                                                            </td>
                                                            <td>
                                                                In
                                                            </td>
                                                            <td>
                                                                Out
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="height: 18px; background-color: #BCF5A9">
                                                                <asp:Label ID="Label234" runat="server" Text='Min' Width="50px"></asp:Label>
                                                                <asp:Label ID="lblid1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_EXH_MAX_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_EXH_MAX_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_TEMP_EXH_MAX_Min" runat="server" ControlToValidate="txtGE_TEMP_EXH_MAX_Max"
                                                                    ControlToCompare="txtGE_TEMP_EXH_MAX_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_EXH_MIN_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_EXH_MIN_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_TEMP_EXH_MIN_Min" runat="server" ControlToValidate="txtGE_TEMP_EXH_MIN_Max"
                                                                    ControlToCompare="txtGE_TEMP_EXH_MIN_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_CW_IN_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_CW_IN_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_TEMP_CW_IN_Min" runat="server" ControlToValidate="txtGE_TEMP_CW_IN_Max"
                                                                    ControlToCompare="txtGE_TEMP_CW_IN_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_CW_OUT_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_CW_OUT_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_TEMP_CW_OUT_Min" runat="server" ControlToValidate="txtGE_TEMP_CW_OUT_Max"
                                                                    ControlToCompare="txtGE_TEMP_CW_OUT_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_LO_IN_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_LO_IN_Min")%>'></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_TEMP_LO_IN_Min" runat="server" ControlToValidate="txtGE_TEMP_LO_IN_Max"
                                                                    ControlToCompare="txtGE_TEMP_LO_IN_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_LO_OUT_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_LO_OUT_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_TEMP_LO_OUT_Min" runat="server" ControlToValidate="txtGE_TEMP_LO_OUT_Max"
                                                                    ControlToCompare="txtGE_TEMP_LO_OUT_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_PRESS_LO_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_PRESS_LO_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_PRESS_LO_Min" runat="server" ControlToValidate="txtGE_PRESS_LO_Max"
                                                                    ControlToCompare="txtGE_PRESS_LO_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_PRESS_CW_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_PRESS_CW_Min")%>'> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_GE_PRESS_CW_Min" runat="server" ControlToValidate="txtGE_PRESS_CW_Max"
                                                                    ControlToCompare="txtGE_PRESS_CW_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtSG_LO_PRESS_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_PRESS_Min")%>' MaxLength="7"
                                                                    onKeydown="JavaScript:return MaskMoney(event);"></asp:TextBox>
                                                                <asp:CompareValidator ID="CV_SG_LO_PRESS_Min" runat="server" ControlToValidate="txtSG_LO_PRESS_Max"
                                                                    ControlToCompare="txtSG_LO_PRESS_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtSG_LO_TEMP_Min" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_TEMP_Min")%>' MaxLength="7"
                                                                    onKeydown="JavaScript:return MaskMoney(event);"> </asp:TextBox>
                                                                <asp:CompareValidator ID="CV_SG_LO_TEMP_Min" runat="server" ControlToValidate="txtSG_LO_TEMP_Max"
                                                                    ControlToCompare="txtSG_LO_TEMP_Min" Operator="GreaterThanEqual" SetFocusOnError="true"
                                                                    Type="Double"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 1px solid ActiveBorder">
                                                            <td align="left" style="height: 18px; background-color: #F78181">
                                                                <asp:Label ID="Label7" runat="server" Text='MAX'></asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_EXH_MAX_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_EXH_MAX_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_EXH_MIN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_EXH_MIN_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_CW_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_CW_IN_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_CW_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_CW_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_LO_IN_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_LO_IN_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_TEMP_LO_OUT_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_TEMP_LO_OUT_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_PRESS_LO_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_PRESS_LO_MAX")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtGE_PRESS_CW_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "GE_PRESS_CW_Max")%>'> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 18px;">
                                                                <asp:TextBox ID="txtSG_LO_PRESS_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_PRESS_Max")%>'></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtSG_LO_TEMP_Max" runat="server" Width="90%" CssClass="input centeralinment"
                                                                    MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "SG_LO_TEMP_Max")%>'> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="ThresholdActionSettingDew" style="display: none; width: 900px;" title="Threshold Action Setting">
                    <div id="ThresholdAction" style="border: 1px solid #cccccc; border-radius: 0px 0px 0px 0px;">
                        <div style="border-radius: 0px 0px 0px 0px;">
                            <table cellpadding="3" cellspacing="1" width="100%">
                                <tr style="height: 10px;">
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr style="height: 10px;">
                                    <td colspan="2">
                                        &nbsp;&nbsp;&nbsp;&nbsp; If threshold is reached, do the following&nbsp;:-
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; text-align: right" class="style1">
                                        Inform by
                                        <asp:CheckBox ID="chkEmail" runat="server" />
                                        <span style="cursor: pointer;" onclick="javascript:$('#ctl00_MainContent_txtMailTo').focus();$('#dvSelectAddress').show();">
                                            Email... </span>
                                    </td>
                                    <td colspan="2" style="white-space: nowrap">
                                        <asp:TextBox ID="txtMailTo" runat="server" TextMode="MultiLine" Style="white-space: pre-wrap"
                                            Width="350" Height="50px"></asp:TextBox>
                                        <asp:CheckBox ID="chkDesktopAlert" runat="server" Checked="true" Enabled="false" />
                                        On Jibe Desktop alert
                                    </td>
                                </tr>
                                <tr style="height: 10px;" style="text-align: right">
                                    <td style="text-align: right; white-space: nowrap" class="style1">
                                        &nbsp;&nbsp;&nbsp; Inform To&nbsp;&nbsp;<asp:CheckBox ID="chkMaster" runat="server" />
                                    </td>
                                    <td style="text-align: left; white-space: nowrap">
                                        Master<asp:CheckBox ID="chkCe" runat="server" />C/E&nbsp;&nbsp; and request comments
                                        from them.
                                    </td>
                                </tr>
                                </td> </tr>
                                <td class="style2" style="white-space: nowrap; text-align: right">
                                    Once done, forward via
                                </td>
                                <td style="height: 10px; white-space: nowrap">
                                    <asp:CheckBox ID="chkEmailRe" runat="server" />
                                    email&nbsp;
                                    <asp:CheckBox ID="chkDesktopAlertRe" runat="server" Checked="false" Enabled="false" />
                                    Jibe Desktop alert
                                </td>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: center" colspan="2">
                                        <asp:Button ID="btnSaveAction" runat="server" Text="Save" OnClick="btnSaveAction_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btnCloseAction" runat="server" Text="Close" OnClick="btnCloseAction_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvSelectAddress" class="draggable" style="display: none; background-color: White;
                            position: absolute; left: 380px; top: 115px; z-index: 1000; padding: 2px; border: 1px solid #aabbdd;">
                            <asp:UpdatePanel ID="UpdateAddress" runat="server">
                                <ContentTemplate>
                                    <table style="border: 1px solid #aabbdd; padding: 2px;" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="4" align="right" style="background-color: #B5CDE1">
                                                <div style="font-size: 11px; font-weight: bold; cursor: pointer; padding: 2px; border: 1px solid outset;
                                                    text-align: right; width: 16px; color: Red" onclick="javascript:$('#dvSelectAddress').hide('slow');">
                                                    X</div>
                                            </td>
                                        </tr>
                                        <tr style="background-color: #aabbdd; color: Black">
                                            <td colspan="2">
                                                Select User
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:ListBox ID="lstUsers" runat="server" DataSourceID="ObjectDataSource_UserList"
                                                    DataTextField="USER_NAME" DataValueField="USERID" AppendDataBoundItems="True"
                                                    SelectionMode="Multiple" AutoPostBack="false" Height="200px" Width="99%">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:ListBox>
                                                <asp:ObjectDataSource ID="ObjectDataSource_UserList" runat="server" SelectMethod="Get_UserList"
                                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="UserCompanyID"
                                                            Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnAddTo" runat="server" Text="To &gt;&gt;" OnClick="btnAddTo_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSelectedIDsTo" Visible="false" runat="server" Width="270px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnOK" runat="server" Visible="false" Text="OK" OnClientClick="javascript:$('#dvSelectAddress').hide('slow');return false;"
                                                    Width="60px" />
                                                <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClientClick="javascript:$('#dvSelectAddress').hide('slow'); return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--// Added by Anjali DT:16-06-2016 JIT:10048 When user click on save 'WebForm_OnSubmit' get called--%>
    <%-- If Validation fails for any control then control will be highlighted.--%>
    <%-- Validation : Max .Value should be greater than or equal to Min. value. --%>
    <script type="text/javascript">
        function WebForm_OnSubmit() {
            if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
                for (var i in Page_Validators) {
                    try {
                        var control = document.getElementById(Page_Validators[i].controltovalidate);
                        if (!Page_Validators[i].isvalid) {
                            control.className = "ErrorControl";
                        }
                        else {
                            control.className = "";
                        }
                    }

                    catch (e) { return false; }
                }
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
