<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeFile="Default.aspx.cs"
    Inherits="Samples_MapWithSatelliteView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Daily Position List </title>
    <script src="MapScripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="MapScripts/jquery-ui.min.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 10px;
        }
        input
        {
            font-family: Tahoma;
            font-size: 10px;
        }
        select
        {
            font-family: Tahoma;
            font-size: 10px;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function showDiv(dv) {
            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }

        $(document).ready(function () {
            $('#dvIssues').draggable();
        });
         
    </script>
</head>
<body style="margin: 0">
    <form id="form1" style="display: block" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="pageTitle" class="gradiant-css-orange" style="font-size: 12px; border: 1px solid #CEE3F6;
        text-align: center; padding: 3px; font-weight: bold;">
        <table width="100%">
            <tr>
                <td style="width: 4%; text-align: center">
                    

                <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click" style="font-size:11px;font-weight:bold" >Home</asp:LinkButton>

                </td>
                <td align="center">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Daily Position List"></asp:Label>
                </td>
                <td style="width: 6%">
                    <asp:Label ID="lblExpColSetting" Font-Bold="true" Font-Size="11px" ForeColor="RoyalBlue" style="cursor:pointer"
                        Text="Setting" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="grid-container">
        <table cellpadding="0" cellspacing="2" style="border: 1px solid #cccccc; margin-top: 1px"
            width="100%">
            <tr>
                <td style="vertical-align: top; width: 100%;">
                    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" OnLoad="GoogleMapForASPNet1_Load" />
                    <div style="text-align: left; display: none; width: 20%">
                        <asp:TextBox ID="htxt_sat_nor_hybrid" runat="server" Height="16px" Width="100px"
                            BorderStyle="None" Enabled="false"></asp:TextBox>
                    </div>
                </td>
                <td style="vertical-align: top; border: 1px solid #dcdcdc; background-color: #efefef;">
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" Collapsed="false" ExpandDirection="Horizontal"
                        TargetControlID="dvMapSetting" CollapsedText="Show Setting" ExpandedText="Hide Setting"
                        TextLabelID="lblExpColSetting" ExpandControlID="lblExpColSetting" CollapseControlID="lblExpColSetting"
                        runat="server">
                    </cc1:CollapsiblePanelExtender>
                    <asp:Panel ID="dvMapSetting" runat="server">
                        <div style="margin: 2px; font-size: 11px; font-family: Tahoma;">
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                            <table cellpadding="5" cellspacing="0" border="0" style="vertical-align: top; border: 1px solid #cccccc;
                                font-family: Tahoma; font-size: 11px; width: 100%;">
                                <tr>
                                    <td colspan="2" class="gradiant-css-orange" style="text-align: center; font-weight: bold;
                                        height: 20px; vertical-align: middle;">
                                        Map Settings
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lbltotalships" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblVessels" runat="server" Text=""></asp:Label>
                                        <asp:LinkButton ID="lbtn_error" runat="server" OnClientClick="showDiv('dvIssues'); return false;"
                                            ForeColor="Red">View Error!</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:RadioButton ID="rbtnNoonReport" runat="server" Text="Noon Report" GroupName="ReportType"
                                            AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnNoonReport_CheckedChanged" />
                                        <asp:RadioButton ID="rbtnPurpleReport" runat="server" Text="PurpleFinder" GroupName="ReportType"
                                            AutoPostBack="true" OnCheckedChanged="rbtnPurpleReport_CheckedChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <input id="chck_showclouds" type="checkbox" onclick="cloud_click()" />Clouds
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <input id="show_daylight" type="checkbox" checked="checked" onclick="daylightClick()" />Day/Night
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkPiracyArea"  runat="server" Text="Show Piracy Area" Checked="true" OnCheckedChanged="chkPiracyArea_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelFleet" runat="server" Text="Fleet:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTechmanager" runat="server" AutoPostBack="True" EnableViewState="true"
                                            OnSelectedIndexChanged="ddlTechmanager_SelectedIndexChanged" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Vessel" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="ddl_veslist" runat="server" BackColor="#FFFFE6" AutoPostBack="True"
                                            EnableViewState="true" OnSelectedIndexChanged="ddl_veslist_SelectedIndexChanged"
                                            Width="150px" Height="300px"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btn_nearestport" runat="server" OnClick="btn_nearestport_Click" Visible="false"
                                            Text="Nearest Port" Style="height: 21px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-top: 1px solid #cccccc; border-bottom: 1px solid #cccccc">
                                        <table id="tblVesselRoute" runat="server" width="100%" visible="false" style="font-family: Tahoma;
                                            font-size: 11px; border-collapse: collapse">
                                            <tr>
                                                <td colspan="2" style="background-color: #ccccc0">
                                                    <asp:Label ID="lblVoyagePath" Text="View route" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDT" Font-Bold="true" Text="From" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDT" Font-Size="12px" runat="server" BackColor="#FFFFE6"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtendertxtFromDT" Format="dd/MM/yyyy" TargetControlID="txtFromDT"
                                                        runat="server">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblToDT" Text="To" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTODT" Font-Size="12px" runat="server" BackColor="#FFFFE6"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtendertxtTODT" TargetControlID="txtTODT" Format="dd/MM/yyyy"
                                                        runat="server">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnGetVesselRoute" runat="server" Text="Get route" OnClick="btnGetVesselRoute_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port:
                                    </td>
                                    <td>
                                        <uc:PortList ID="ctlPort" runat="server" OnSelectedIndexChanged="ctlPort_SelectedIndexChanged" />
                                        <%--<asp:CheckBox ID="chk_port" runat="server" Text="Load Port List" OnCheckedChanged="chk_port_CheckedChanged"
                                                AutoPostBack="True" />
                                            <asp:DropDownList ID="drp_port" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drp_port_SelectedIndexChanged"
                                                Width="150px">
                                            </asp:DropDownList>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 30px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Lat,Lon
                                    </td>
                                    <td>
                                        <input id="txthtmltemp" type="text" style="width: 150px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Local Time
                                    </td>
                                    <td>
                                        <input id="txttime" type="text" maxlength="8" size="8" style="width: 50px" />
                                        Diff fm GMT:<input id="txtgmt" type="text" style="width: 50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:LinkButton ID="lbtnviewdpl" runat="server" OnClick="lbtnviewdpl_Click">View DPL Details</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvIssues" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 20%; z-index: 1; color: black">
        <div class="header">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        Vessels with wrong Lat/Long
                    </td>
                    <td style="text-align: right; vertical-align: top">
                        <img src="Images/Close.gif" onclick="closeDiv('dvIssues');" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table style="background-color: White; border-style: solid; border-color: Silver;
                        border-width: 1px; width: 100%;" cellpadding="2">
                        <tr>
                            <td style="font-size: 11px; text-align: left;">
                                <asp:Label ID="lblLoadingIssues" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
