<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DPLMap.aspx.cs" Inherits="Operations_DPLMaps_DPLMap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Daily Position List </title>
    <meta http-equiv="x-ua-compatible" content="IE=11">
    <script src="../../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?libraries=weather&sensor=true"
        type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/daynightoverlay.min.js" type="text/javascript"></script>
    <script src="../../Scripts/CallGoogleApi.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 12px;
            background-color: #0888C5;
            margin: 0;
        }
        input
        {
            font-family: Tahoma;
            font-size: 12px;
            background-color: #cccccc;
            border: 0px;
        }
        select
        {
            font-family: Tahoma;
            font-size: 12px;
            background-color: #cccccc;
            border: 0px;
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
        .style1
        {
            color: #6262F9;
        }
        
        .floatingfooter
        {
            background: url("../../Images/bg_floatingfooter_top.png") repeat-x scroll 0 0 rgba(0, 0, 0, 0);
            font-size: 1%;
            height: 15px;
            width: 100%;
        }
        .blueimage
        {
            background: url("../../Images/bg_floatingfooter_top.png") repeat-x scroll 0 0 rgba(0, 0, 0, 0);
            font-size: 1%;
            height: 15px;
            width: 100%;
        }
        .groupTitle
        {
            background-color: #1E2F63;
            color: #fff;
            padding: 4px;
            text-align: center;
            font-size: 12px;
        }
        .groupBox
        {
            background-color: #0888C5;
            border: 1px solid #1E2F63;
            height: 180px;
        }
        .divsettingcontainer
        {
            float: inherit;
            width: 99%;
            background-color: #0888C5;
        }
        #pageloaddiv
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 900;
            background: url('../../Images/DPLloading.gif') no-repeat center center;
        }
        .map_width
        {
            height: 690px;
            width: 100%;
        }
        #map_wrapper, #map_underlay
        {
            position: absolute;
        }
        #map_wrapper
        {
            z-index: 10 !important;
        }
        #map_wrapper > *
        {
            /*opacity: 0.7;*/
        }
        .underlay_image
        {
            background: transparent url("../../Images/grid_tile.png") repeat;
        }
    </style>
</head>
<body>
    <form id="form1" style="display: block" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="pageTitle" style="font-size: 12px; border: 1px solid #0888C5; text-align: center;
        padding: 3px; font-weight: bold; background-color: #0888C5;">
        <table width="100%" style="background-color: #0888C5;">
            <tr>
                <td style="width: 4%; text-align: center">
                    <a href="../../Infrastructure/DashBoard_Common.aspx" style="font-size: 11px; font-weight: bold;
                        color: White;">Home</a>
                </td>
                <td align="center">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Daily Position List" Font-Size="Medium"
                        ForeColor="White"></asp:Label>
                </td>
                <td style="width: 6%">
                    <input id="lbtnSettings" type="button" value="Hide Settings" onclick="HideShowDiv();"
                        style="display: none;" />
                </td>
            </tr>
        </table>
    </div>
    <div id="grid-container" style="height: 690px; width: 100%;">
        <div id="map_wrapper" class="map_width">
            <div id="googleMap" class="map_width">
            </div>
        </div>
        <div id="map_underlay" class="map_width">
            <div class="underlay_image map_width">
            </div>
        </div>
    </div>
    <div id="divsettingcontainer">
        <div id="floatingfootertop" onclick="HideShowDiv();">
            <div class="floatingfootertopinner" style="text-align: center;">
                <img alt="Close" src="../../Images/floatingfooter_butn.png" id="imgTopPin" />
            </div>
            <div class="blueimage" style="text-align: center; margin-top: -5px; z-index: -5;">
                <img alt="Close" src="../../Images/bg_floatingfooter_top.png" id="img1" />
            </div>
        </div>
        <div id="divsettings">
            <table cellpadding="0" cellspacing="4" style="width: 100%; background-color: #0888C5;">
                <tr>
                    <td>
                        <div id="mapsettings" class="groupBox">
                            <div class="groupTitle">
                                <span>Map Settings</span>
                            </div>
                            <div>
                                &nbsp;&nbsp;&nbsp;<span style="">Source of Data</span><span style="color: white;
                                    padding-left: 10px;">
                                    <asp:RadioButtonList ID="rbtnReportType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Noon Report" Value="N" Selected="True"> </asp:ListItem>
                                        <asp:ListItem Text="Purple Finder" Value="P"> </asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;</span><span style="">Settings</span>
                                <br />
                                <span style="color: #66777C; padding-left: 10px;"><span style="color: white;">
                                    <asp:CheckBox ID="cbxClouds" runat="server" OnClick="LoadWeather();" />Clouds</span>
                                    <span style="color: white; padding-left: 10px;">
                                        <asp:CheckBox ID="cbxDayNight" runat="server" OnClick="LoadDayNight();" />Day/Night</span>
                                </span>
                                <div style="padding-left: 10px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="button" value="Auto-Refresh ON" id="btnAutoRefresh" onclick="AutoRefresh();" />
                                            </td>
                                            <td>
                                                <div style="display: none;">
                                                    &nbsp;&nbsp;<asp:CheckBox ID="cbxAutoRefresh" runat="server" /></div>
                                            </td>
                                            <td>
                                                <div style="width: 150px;">
                                                    <span id="countdown" class="timer" style="color: White;">Auto Refresh is OFF</span>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <span style="color: white; padding-left: 10px;">
                                    <asp:CheckBox ID="chkbxPiracyArea" runat="server" Checked="true" OnClick="DeletePolygon();" />Show
                                    Piracy Area</span>
                                <br />
                                &nbsp;&nbsp;&nbsp;<label id="lblTotalShip" style="color: white;"></label>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div id="vesselselector" class="groupBox">
                            <div class="groupTitle">
                                <span>Vessel Selector</span>
                            </div>
                            <div>
                                <table style="padding-top: 10px;">
                                    <tr>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">Fleet:</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFleet" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">Vessel:</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <div style="">
                                                <div style="float: left;">
                                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                </div>
                                                <div style="float: left;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left;">
                                                    <asp:CheckBox ID="cbxNearByPorts" runat="server" Text="Nearest Ports" OnClick="GetNearByPorts();" />
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <span style="color: white; font-weight: bold; padding-left: 10px;">View Vessel Route:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">From:</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" Font-Size="12px" runat="server" BackColor="#FFFFE6"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtFromDate"
                                                Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">To:</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtToDate" Font-Size="12px" runat="server" BackColor="#FFFFE6"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtToDate"
                                                Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;<input type="button" value="View Route" id="btnViewRoute" onclick="ViewRoute();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div id="portselector" class="groupBox">
                            <div class="groupTitle">
                                <span>Port Selector</span>
                            </div>
                            <div>
                                <table style="padding-top: 10px;">
                                    <tr>
                                        <td>
                                            &nbsp;<span style="color: white; padding-left: 10px;">Port:</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPorts" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;<span style="color: white; padding-left: 10px;">Lat,Long</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <label id="lblLatLon" style="font-size: small; width: 280px;">
                                                -,-</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;<span style="color: white; padding-left: 10px;">Local Time:</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <label id="lblLocalTime" style="font-size: small;">
                                                -</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;<span style="color: white; padding-left: 10px;">Local Time Diff from GMT:</span>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <label id="lblGMT" style="font-size: small;">
                                                -</label>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td colspan="3">
                                            <span style="color: white; padding-left: 10px;">
                                                <asp:CheckBox ID="chkbxIncidentPorts" runat="server" />Display Incident Ports
                                            </span>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hlnkDPLDetails" runat="server" NavigateUrl="~/Operations/Default.aspx?v='0,0"
                                                ForeColor="#F9F794">View DPL Details</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div id="mapindicators" class="groupBox">
                            <div class="groupTitle">
                                <span>Symbols/Map Indicators</span>
                            </div>
                            <div style="padding: 10px 0px 0px 30px">
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<img src="../../images/PortMarker.png" alt="" height="20px" />
                                        </td>
                                        <td width="5px">
                                            &nbsp;<span style="color: white; padding-left: 10px;">-</span>&nbsp;
                                        </td>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">Ports</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../Images/routeportmarker.png" alt=""
                                                width="14px" /><%--Port.gif--%>
                                        </td>
                                        <td width="5px">
                                            &nbsp;<span style="color: white; padding-left: 10px;">-</span>&nbsp;
                                        </td>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">Port in Vessel Route</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<img src="../../images/shipicon_360.png" alt="" />
                                        </td>
                                        <td width="5px">
                                            &nbsp;<span style="color: white; padding-left: 10px;">-</span>&nbsp;
                                        </td>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">Vessel</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<img src="../../Images/VesselRoute.png" alt="" />
                                        </td>
                                        <td width="5px">
                                            &nbsp;<span style="color: white; padding-left: 10px;">-</span>&nbsp;
                                        </td>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">Vessel Route</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                            <img src="../../Images/PrivacyArea.png" alt="" />
                                        </td>
                                        <td width="5px">
                                            &nbsp;<span style="color: white; padding-left: 10px;">-</span>&nbsp;
                                        </td>
                                        <td>
                                            <span style="color: white; padding-left: 10px;">Piracy Area</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div id="others" class="groupBox">
                            <div class="groupTitle">
                                <span>Other Important Details</span>
                            </div>
                            <div style="padding: 10px 0px 0px 10px;">
                                <table>
                                    <tr>
                                        <td>
                                            <span style="font-size: small; color: white;">Route Distance in nautical miles:&nbsp;</span>
                                        </td>
                                        <td>
                                            <span style="color: white;">
                                                <label id="lblRouteDistance" style="font-size: small;">
                                                    No Route Selected</label></span>
                                            <asp:HiddenField ID="hdfUserCompanyID" runat="server" ClientIDMode="Static" />
                                             <asp:HiddenField ID="hdfUserID" runat="server" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="pageloaddiv">
    </div>
    </form>
</body>
</html>
