<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DPLMap.aspx.cs" Inherits="DPLMap_DPLMap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Position List </title>
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?libraries=weather&sensor=true"
        type="text/javascript"></script>
    <script src="Scripts/daynightoverlay.min.js" type="text/javascript"></script>
    <script src="Scripts/DPLGoogleMap.js" type="text/javascript"></script>
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
        .style1
        {
            color: #6262F9;
        }
    </style>
</head>
<body>
    <form id="form1" style="display: block" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="pageTitle" class="gradiant-css-orange" style="font-size: 12px; border: 1px solid #CEE3F6;
        text-align: center; padding: 3px; font-weight: bold;">
        <table width="100%">
            <tr>
                <td style="width: 4%; text-align: center">
                    <asp:LinkButton ID="lnkHome" runat="server" Style="font-size: 11px; font-weight: bold">Home</asp:LinkButton>
                </td>
                <td align="center">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Daily Position List"></asp:Label>
                </td>
                <td style="width: 6%">
                    <asp:Label ID="lblExpColSetting" Font-Bold="true" Font-Size="11px" ForeColor="RoyalBlue"
                        Style="cursor: pointer" Text="Setting" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="grid-container">
        <div id="googleMap" style="height: 700px; border: 1px solid grey;">
        </div>
        <div style="height: 5px;">
        </div>
        <div style="border: 1px solid grey; height: 190px; padding: 1px 1px 1px 1px;">
            <div style="float: left; width: 10px;">
                &nbsp;
            </div>
            <div style="float: left; width: 250px; border: 1px solid grey; height: 180px;">
                <div style="background-color: #D4E0E7;">
                    <span style="color: #66777C; padding-left: 80px;">Map Settings</span>
                </div>
                <div>
                    &nbsp;&nbsp;&nbsp;<span style="">Source of Data</span><span style="color: #66777C;
                        padding-left: 10px;">
                        <asp:RadioButtonList ID="rbtnReportType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Noon Report" Value="N" Selected="True"> </asp:ListItem>
                            <asp:ListItem Text="Purple Finder" Value="P"> </asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;&nbsp;&nbsp;</span><span style="">Settings</span>
                    <br />
                    <span style="color: #66777C; padding-left: 10px;"><span style="color: #66777C;">
                        <asp:CheckBox ID="cbxClouds" runat="server" OnClick="LoadWeather();" />Clouds</span><br />
                        <span style="color: #66777C; padding-left: 10px;">
                            <asp:CheckBox ID="cbxDayNight" runat="server" OnClick="LoadDayNight();" />Day/Night</span>
                    </span>
                    <br />
                    <span style="color: #66777C; padding-left: 10px;">
                        <asp:CheckBox ID="chkbxPiracyArea" runat="server" Checked="true" OnClick="DeletePolygon();" />Show
                        Piracy Area</span>
                    <br />
                    &nbsp;&nbsp;&nbsp;<label id="lblTotalShip" width="250px" style="color: #6262F9;" />
                </div>
            </div>
            <div style="float: left; width: 5px;">
                &nbsp;
            </div>
            <div style="float: left; width: 420px; border: 1px solid grey; height: 180px">
                <div style="background-color: #D4E0E7;">
                    <span style="color: #66777C; padding-left: 190px;">Vessel Selector</span>
                </div>
                <div style="height: 150px;">
                    <table style="padding-top: 10px;">
                        <tr>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Fleet:</span>
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
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Vessel:</span>
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
                    </table>
                    <fieldset>
                        <legend><span style="color: #66777C;">View Vessel Route</span></legend>
                        <table>
                            <tr>
                                <td>
                                    <span style="color: #66777C; font-weight: bold;">From:</span>
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
                                    <span style="color: #66777C; font-weight: bold;">To:</span>
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
                    </fieldset>
                </div>
            </div>
            <div style="float: left; width: 5px;">
                &nbsp;
            </div>
            <div style="float: left; width: 460px; border: 1px solid grey; height: 180px;">
                <div style="background-color: #D4E0E7;">
                    <span style="color: #66777C; padding-left: 180px;">Port Selector</span>
                </div>
                <div>
                    <table style="padding-top: 10px;">
                        <tr>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Port:</span>
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
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Lat,Lon</span>
                            </td>
                            <td width="15px">
                                &nbsp;
                            </td>
                            <td>
                                <label id="lblLatLon" style="font-size: smaller;">
                                    -,-</label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Local Time:</span>
                            </td>
                            <td width="15px">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" style="width: 200px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Local Time Diff
                                    from GMT:</span>
                            </td>
                            <td width="15px">
                                &nbsp;
                            </td>
                            <td>
                                <input type="text" style="width: 200px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:HyperLink ID="hlnkDPLDetails" runat="server" NavigateUrl="../../Jibe/Operations/Default.aspx?v='0,0">View DPL Details</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="float: left; width: 5px;">
                &nbsp;
            </div>
            <div style="float: left; width: 295px; border: 1px solid grey; height: 180px;">
                <div style="background-color: #D4E0E7;">
                    <span style="color: #66777C; padding-left: 65px;">Symbols/Map Indicators</span>
                </div>
                <div style="padding: 10px 0px 0px 30px">
                    <table>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;<img src="images/PortMarker.png" alt="" />
                            </td>
                            <td width="5px">
                                &nbsp;<span style="color: #66777C; padding-left: 10px; font-weight: bold;">-</span>&nbsp;
                            </td>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Ports</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="images/VesselMarker.png" alt="" />
                            </td>
                            <td width="5px">
                                &nbsp;<span style="color: #66777C; padding-left: 10px; font-weight: bold;">-</span>&nbsp;
                            </td>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Vessel</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="images/RedShipIcon.png" alt="" />
                            </td>
                            <td width="5px">
                                &nbsp;<span style="color: #66777C; padding-left: 10px; font-weight: bold;">-</span>&nbsp;
                            </td>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Vessel in Piracy
                                    Zone</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;<img src="images/VesselRoute.png" alt="" />
                            </td>
                            <td width="5px">
                                &nbsp;<span style="color: #66777C; padding-left: 10px; font-weight: bold;">-</span>&nbsp;
                            </td>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Vessel Route</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                                <img src="images/PrivacyArea.png" alt="" />
                            </td>
                            <td width="5px">
                                &nbsp;<span style="color: #66777C; padding-left: 10px; font-weight: bold;">-</span>&nbsp;
                            </td>
                            <td>
                                <span style="color: #66777C; padding-left: 10px; font-weight: bold;">Piracy Area</span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
