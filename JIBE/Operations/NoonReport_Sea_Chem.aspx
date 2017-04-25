<%@ Page Title="Noon Report At Sea Chemical" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NoonReport_Sea_Chem.aspx.cs" Inherits="Operations_NoonReport_Sea_Chem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <%--<script src="../Scripts/Common_Functions.js" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript">

        $(document).ready(function () {

            var wh = 'PKID=<%=Request.QueryString["id"]%>';

            Get_Record_Information_Details('OPS_Telegrams', wh);

        });

        function OpenCrewList(vcode) {

            return false;
        }
    </script>--%> 
    <style type="text/css">
        .page
        {
            min-width: 400px;
            width: 93%;
        }
        .cleartd
        {
            width: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Daily Noon Report At Sea"></asp:Label>
    </div>
    <div id="page-content" style="min-height: 640px; color: #333333; border: 1px solid gray;
        width: 99.8%">
        <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updmain" runat="server">
            <ContentTemplate>
                <%--Content Div Start--%>
                <div id='dvEmailContent'>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: left; font-weight: bold;" class="style1">
                                Vessel Name:&nbsp;<asp:Label ID="lblVessel" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                                &nbsp; &nbsp; &nbsp;|&nbsp; &nbsp; &nbsp;
                                <asp:HyperLink ID="hplcrewlist" runat="server" Target="_blank" Text="Crew List"></asp:HyperLink>
                            </td>
                            <td align="right" style="padding-right: 17px" class="style1">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="dvNoonReport">
                                    <style type="text/css">
                                        table tr
                                        {
                                            padding: 0px 0px 0px 0px;
                                            white-space: normal;
                                            line-height: normal;
                                            letter-spacing: normal;
                                        }
                                        #pageTitle
                                        {
                                            background-color: gray;
                                            color: White;
                                            font-size: 12px;
                                            text-align: center;
                                            padding: 2px;
                                            font-weight: bold;
                                        }
                                        .leafTR
                                        {
                                            border-bottom-style: solid;
                                            border-bottom-color: White;
                                            border-bottom-width: 1px;
                                        }
                                        .leafTD-header
                                        {
                                            width: 120px;
                                            height: 20px;
                                            padding: 0px 0px 0px 0px;
                                            text-align: left;
                                        }
                                        .leafTD-data
                                        {
                                            width: 140px;
                                            height: 20px;
                                            padding: 0px 0px 0px 0px;
                                            background-color: #cce499;
                                            text-align: left;
                                        }
                                        .leafTD-data-left
                                        {
                                            width: 140px;
                                            height: 20px;
                                            padding: 0px 0px 0px 2px;
                                            background-color: #cce499;
                                            text-align: center;
                                        }
                                        .leafTD-header-midsec
                                        {
                                            width: 170px;
                                            height: 20px;
                                            padding: 0px 0px 0px 0px;
                                            text-align: left;
                                        }
                                        .leafTD-data-midsec
                                        {
                                            width: 115px;
                                            height: 20px;
                                            padding: 0px 2px 0px 0px;
                                            background-color: #cce499;
                                            text-align: right;
                                        }
                                        .leafTD-data-consmp
                                        {
                                            height: 20px;
                                            padding: 0px 2px 0px 2px;
                                            background-color: #cce499;
                                            text-align: right;
                                            white-space: normal;
                                            line-height: normal;
                                            letter-spacing: normal;
                                        }
                                        
                                        .gvMain
                                        {
                                            border-collapse: collapse;
                                            border: 1px solid #B3B3B3;
                                        }
                                        .gvheader
                                        {
                                            border: 1px solid #B3B3B3;
                                            font-weight: normal;
                                        }
                                        .gvRows
                                        {
                                            border: 1px solid #B3B3B3;
                                        }
                                    </style>
                                    <asp:FormView ID="fvnoonreport" runat="server" OnDataBound="fvnoonreport_DataBound">
                                        <ItemTemplate>
                                            <table width="99%" cellspacing="0">
                                                <tr>
                                                    <td valign="top" style="border: solid 1px gray; width: 27%;">
                                                        <table cellspacing="1">
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' style="width: 180px;">
                                                                </td>
                                                                <td>
                                                                    <table cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 80px; text-align: center">
                                                                                Date
                                                                            </td>
                                                                            <td style="width: 5px; background-color: White;">
                                                                            </td>
                                                                            <td style="width: 35px; text-align: center">
                                                                                Time
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 50px;">
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Date:
                                                                </td>
                                                                <td class='leafTD-data'>
                                                                    <table cellspacing="0">
                                                                        <tr>
                                                                            <td class='leafTD-data-left' style="width: 80px; text-align: center">
                                                                                <%#Eval("Telegram_Date")%>
                                                                            </td>
                                                                            <td style="width: 5px; background-color: White;">
                                                                            </td>
                                                                            <td class='leafTD-data-left' style="width: 35px; text-align: center">
                                                                                <%#Eval("Telegram_Date_HH")%>:<%#Eval("Telegram_Date_MI")%></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Voyage Number:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("VOYAGE")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    UTC / Time Zone:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 40px; border-right: 1px solid white; padding-right: 2px; text-align: center;">
                                                                                <%#Eval("UTC_TYPE")%>
                                                                            </td>
                                                                            <td style="width: 40px; padding-left: 2px; text-align: center;">
                                                                                <%#Eval("UTC_HR")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                </td>
                                                                <td class='leafTD-data'>
                                                                    <table cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 35px; text-align: center">
                                                                                Deg
                                                                            </td>
                                                                            <td style="width: 35px; text-align: center">
                                                                                Min
                                                                            </td>
                                                                            <td style="width: 50px; text-align: center" colspan="2">
                                                                                Dir.
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Latitude:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <table cellspacing="0">
                                                                        <tr>
                                                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                                <%#Eval("Latitude_Degrees")%>
                                                                            </td>
                                                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                                <%#Eval("Latitude_Minutes")%>
                                                                            </td>
                                                                            <td style="width: 35px; text-align: center" colspan="2">
                                                                                <%#Eval("LATITUDE_N_S")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Longitude:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <table cellspacing="0">
                                                                        <tr>
                                                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                                <%#Eval("Longitude_Degrees")%>
                                                                            </td>
                                                                            <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                                <%#Eval("Longitude_Minutes")%>
                                                                            </td>
                                                                            <td style="width: 35px; text-align: center">
                                                                                <%#Eval("Longitude_E_W")%>
                                                                            </td>
                                                                            <td style="width: 10px; text-align: center">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Heading:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Heading")%>
                                                                </td>
                                                                <td>
                                                                    Deg
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Rel. Swell Direction:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Rel_Swell_Direction")%>
                                                                </td>
                                                                <td>
                                                                    Deg
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    True Swell Direction:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Swell_Direction")%>
                                                                </td>
                                                                <td>
                                                                    Deg
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Swell Height:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Swell_Height")%>
                                                                </td>
                                                                <td>
                                                                    Mtrs
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Douglas Sea State:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("DouglasSeaState")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Rel. Wind Direction:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Rel_Wind_Direction")%>
                                                                </td>
                                                                <td>
                                                                    Deg
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    True Wind Direction:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Wind_Direction")%>
                                                                </td>
                                                                <td>
                                                                    Deg
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Wind Rel. Speed:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Rel_Wind_Speed")%>
                                                                </td>
                                                                <td>
                                                                    Knots
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Wind True Speed:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("True_Wind_Speed")%>
                                                                </td>
                                                                <td>
                                                                    Knots
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Beaufort Sea Scale:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Sea_Force")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                             <%--   <td class='leafTD-header'>
                                                                    Current Direction from Chart:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Current_Direction")%>
                                                                </td>
                                                                <td>
                                                                </td>--%>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Current Speed from Chart:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Current_Speed")%>
                                                                </td>
                                                                <td>
                                                                    Knts
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Air Temp Dry:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("AirTemp")%>
                                                                </td>
                                                                <td>
                                                                    Deg C.
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Air Temp Wet:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("AirTempWet")%>
                                                                </td>
                                                                <td>
                                                                    Deg C.
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Barometric Pressure:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("AirPress")%>
                                                                </td>
                                                                <td>
                                                                    bar
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Sea Water Temp.:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("SeaTemp")%>
                                                                </td>
                                                                <td>
                                                                    Deg C
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td valign="top" style="border: solid 1px gray; width: 26%;">
                                                        <table cellspacing="1">
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec' style="width: 100px">
                                                                    Steaming Time:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Steaming_hrs")%>
                                                                </td>
                                                                <td style="width: 100px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Detention:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Detention")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Drifts:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Drifts")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Total Time:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("TotalTimeLastEvents")%>
                                                                </td>
                                                                <td>
                                                                    (Since last Event)
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Average Speed:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("AVERAGE_SPEED")%>
                                                                </td>
                                                                <td>
                                                                    Knots
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Slip:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Slip")%>
                                                                </td>
                                                                <td>
                                                                    %
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Passage Instructions:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Passage_Instructions")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    AV. Output:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("AV_Output")%>
                                                                </td>
                                                                <td>
                                                                    KW
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    % OUTPUT:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Pecentage_Output")%>
                                                                </td>
                                                                <td>
                                                                    KW
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Av. Eng. RPM:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("EngRPM")%>
                                                                </td>
                                                                <td>
                                                                    CT
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Distance By Engine:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Distance_By_Eng")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Distance By Observation:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("Distance_By_Observation")%>
                                                                </td>
                                                                <td>
                                                                    N. mile
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Distance To Go:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("PassDistToGo")%>
                                                                </td>
                                                                <td>
                                                                    N. mile
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    Next Port:
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: center;">
                                                                    <%#Eval("NEXT_PORT_NAME")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header-midsec'>
                                                                    ETA Next Port
                                                                </td>
                                                                <td class='leafTD-data-midsec' style="text-align: left">
                                                                    <table cellspacing="0">
                                                                        <tr>
                                                                            <td class='leafTD-data-left' style="width: 80px; text-align: center">
                                                                                <%#Eval("ETA_Next_Port")%>
                                                                            </td>
                                                                            <td style="width: 5px; background-color: White;">
                                                                            </td>
                                                                            <td class='leafTD-data-left' style="width: 20px; text-align: center">
                                                                                <%#Eval("ETA_Next_Port_HH")%>:<%#Eval("ETA_Next_Port_MI")%></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <table width="50%">
                                                                        <tr>
                                                                            <td colspan="3" style="height: 20px; background-color: #99ccff; text-align: center;
                                                                                font-weight: bold; border-top: 3px solid white">
                                                                                Draft
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="border-bottom: solid 1px white">
                                                                            <td style="height: 25px; text-align: center">
                                                                                Fwd:
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499; border-right: solid 1px white;
                                                                                text-align: center;">
                                                                                <%#Eval("FwdDraft")%>
                                                                            </td>
                                                                            <td>
                                                                                Mtrs
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="border-bottom: solid 1px white">
                                                                            <td style="height: 25px; text-align: center">
                                                                                Aft:
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499; border-right: solid 1px white;
                                                                                text-align: center;">
                                                                                <%#Eval("AftDraft")%>
                                                                            </td>
                                                                            <td>
                                                                                Mtrs
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="border-bottom: solid 1px white">
                                                                            <td style="height: 25px; text-align: center">
                                                                                Trim:
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499; text-align: center;">
                                                                                <%#Eval("TrimDraft")%>
                                                                            </td>
                                                                            <td>
                                                                                Mtrs by stern
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 1%">
                                                    </td>
                                                    <td valign="top" style="border: solid 1px gray; padding-right: 5px;width: 45%;">
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tr class='leafTR' style="background-color: #99ccff; text-align: center;">
                                                                <td rowspan="3" style="background-color: White">
                                                                </td>
                                                                <td rowspan="3" style="background-color: White">
                                                                % Sulphur
                                                                </td>
                                                                <td rowspan="3" style="width: 120px">
                                                                    Average Consp./24 hours
                                                                </td>
                                                                <td colspan="5">
                                                                    Consumption at sea
                                                                </td>
                                                                <td rowspan="3">
                                                                    ROB
                                                                </td>
                                                                <td style="width: 20px; background-color: White" rowspan="2">
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr style="background-color: #99ccff; text-align: center;">
                                                                <td rowspan="2">
                                                                    ME
                                                                </td>
                                                                <td rowspan="2">
                                                                    AE
                                                                </td>
                                                                <td colspan="2"> 
                                                                    Boiler
                                                                </td>
                                                                <td rowspan="2">
                                                                    Incinerators
                                                                </td>
                                                            </tr>
                                                             <td style="background-color: #99ccff; text-align: center;">
                                                                    Ship Demand
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    Cargo Heating
                                                                </td>
                                                            <tr>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    HSFO :
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("HSFO_Purity")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("HSFO_ConsPer24Hours")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("ME_Cons_HSFO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("AE_Cons_HSFO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Boiler_Cons_HSFO")%>
                                                                </td>
                                                                 <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">  
                                                                     <%#Eval("Cargo_Cons_HSFO")%>                                                               
                                                                </td>
                                                                 <td>

                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("HO_ROB")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    MT
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    LSFO :
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("LSFO_Purity")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("LSFO_ConsPer24Hours")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("ME_Cons_LSFO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("AE_Cons_LSFO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Boiler_Cons_LSFO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Cargo_Cons_LSFO")%>      
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("LSFO_ROB")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    MT
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    DO :
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("DO_Purity")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("DO_ConsPer24Hours")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("ME_Cons_MGO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("AE_Cons_MGO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Boiler_Cons_MGO_DO")%>
                                                                </td>
                                                                  <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                   <%#Eval("Cargo_Cons_MGO_DO")%>   
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("DO_Incinerators")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("DO_ROB")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    MT
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                    TOTAL AVERAGES :
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("Total_ConsPer24Hours_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("Total_ME_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Total_AE_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Total_Boiler_HO_LO_DO")%>
                                                                </td>
                                                                 <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("TotalCargoHeating_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Total_Incinerator_HO_LO_DO")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                    ME LUB OIL:
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center;">
                                                                    <%#Eval("ME_LUB_OIL_ConsPer24Hours")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("ME_LUB_OIL_ME")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                 <td>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center;">
                                                                    <%#Eval("ME_LUB_OIL_ROB")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    Ltrs
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    ME CYL OIL:
                                                                </td>
                                                                <td>
                                                                    TBN
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    70
                                                                </td>
                                                                <td colspan="4" style="background-color: #99ccff; text-align: center;">
                                                                    70
                                                         
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    70
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("ME_CYL_OIL_ConsPer24Hours_TBN2")%>
                                                                </td>
                                                                <td colspan="4" class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("ME_Cons_At_Sea_TBN2")%>
                                                           
                                                                </td>
                                                                  <td>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("ME_ROB_TBN2")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    Ltrs
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                </td>
                                                                <td>
                                                                    TBN
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    40
                                                                </td>
                                                                <td colspan="4" style="background-color: #99ccff; text-align: center;">
                                                                    40
                                                             <%--   </td>
                                                                <td>--%>
                                                                </td>
                                                                 <td>
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    40
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("ME_CYL_OIL_ConsPer24Hours_TBN4")%>
                                                                </td>
                                                                <td colspan="4" class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("ME_Cons_At_Sea_TBN4")%>
                                                               
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("ME_ROB_TBN4")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    Ltrs
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                    T/T Consumption 70+40:
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("T_By_T_Cons_70_Plus_40_ConspPer24Hours")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' colspan="4" style="text-align: center">
                                                                    <%#Eval("T_By_T_Cons_70_Plus_40_Consp_At_Sea")%>
                                                               
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="width: 100px; text-align: center">
                                                                    <%#Eval("T_By_T_Cons_70_Plus_40_ROB")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                    CALCULATED CYL OIL:
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <asp:Label ID="lblCalculatedCylOil" Height="100%" Width="100%" runat="server" Text='<%#Eval("Calculated_Cyl_Oil")%>'></asp:Label>
                                                                </td>
                                                                <td colspan="4" style="text-align: center">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>

                                                               <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                </td>
                                                                <td>                                                                    
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    Production
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    Consumption
                                                                </td>
                                                                <td>
                                                                </td>
                                                                 <td>
                                                                </td>
                                                                <td >
                                                                   
                                                                </td>
                                                               <td style="background-color: #99ccff; text-align: center;">
                                                                    ROB
                                                                </td>
                                                            </tr>


                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                    DOMESTIC F.W:
                                                                </td>
                                                               <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("Domestic_FW_Product_At_Sea")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                  <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("Domestic_FW_Consp_At_Sea")%>
                                                                </td>
                                                                  <td >
                                                                </td>
                                                                 <td >
                                                                </td>
                                                                <td >
                                                                </td>
                                                                 <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("Domestic_FW_Consp_ROB")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    MT
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                    Technical F.W:
                                                                </td>                                                              
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("Technical_FW_Product_At_Sea")%>
                                                                </td>
                                                                  <td>
                                                                </td>
                                                               <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("Technical_FW_Consp_At_Sea")%>
                                                                </td>
                                                                <td colspan="3">
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("Technical_FW_Consp_ROB")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    MT
                                                                </td>
                                                            </tr>

                                                            <tr></tr>
                                                            <tr></tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                </td>
                                                                <td>                                                                    
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                    Total
                                                                </td>
                                                                 <td style="background-color: #99ccff; text-align: center;">
                                                                    Burned
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;" colspan="2">
                                                                    Drained
                                                                </td>
                                                                <td style="background-color: #99ccff; text-align: center;">
                                                                Evaporated                                                             
                                                                </td>
                                                                <td >
                                                                   
                                                                </td>
                                                               <td style="background-color: #99ccff; text-align: center;">
                                                                    ROB
                                                                </td>
                                                            </tr>


                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                   SLUDGE RECYCLING
                                                                </td>
                                                               <td class='leafTD-data-consmp' style="text-align: center">
                                                                    <%#Eval("Total_Sludge_Recycling")%>
                                                                </td>
                                                                <td class='leafTD-data-consmp' style="text-align: center">
                                                                <%#Eval("Sludge_Burned")%>
                                                                </td>
                                                                  <td class='leafTD-data-consmp' style="text-align: center" colspan="2">
                                                                   <%#Eval("Sludge_Drained")%>
                                                                </td>
                                                                  <td class='leafTD-data-consmp' style="text-align: center">
                                                                  <%#Eval("Sludge_Evaporated")%>
                                                                </td>
                                                                <td >
                                                                </td>
                                                                 <td class='leafTD-data-consmp' style="text-align: center">
                                                                  <%#Eval("ROB_Sludge")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    MT
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <br />
                                                        <table cellpadding="0" style="padding: 20px 0px 0px 0px; margin-top: 9px;">
                                                            <tr>
                                                                <td valign="top" style="padding-left: 30px">
                                                                    <table cellspacing="1" style="background-color: White">
                                                                        <tr>
                                                                            <td colspan="3" style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                Running Hours since last noon
                                                                            </td>
                                                                            <td style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 10px;
                                                                                width: 100px;">
                                                                                Av. KW
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='leafTD-header'>
                                                                                Aux Engine #1:
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("RHRS_AE1")%>
                                                                            </td>
                                                                            <td style="width: 40px">
                                                                                hrs
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("AvKW_Aux1")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='leafTD-header'>
                                                                                Aux Engine #2:
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("RHRS_AE2")%>
                                                                            </td>
                                                                            <td style="width: 40px">
                                                                                hrs
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("AvKW_Aux2")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='leafTD-header'>
                                                                                Aux Engine #3:
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("RHRS_AE3")%>
                                                                            </td>
                                                                            <td style="width: 40px">
                                                                                hrs
                                                                            </td>

                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("AvKW_Aux3")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='leafTD-header'>
                                                                                More Generators:
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center"> 
                                                                                <%# Eval("RHRS_AE3")%>
                                                                            </td>
                                                                            <td style="width: 40px">
                                                                                hrs
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("AvKW_MoreGenerators")%>
                                                                            </td>
                                                                        </tr>

                                                                          <tr class='leafTR'>
                                                                            <td class='leafTD-header'>
                                                                              Cargo Pumps:
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                              <%# Eval("CargoPumps_Rhrs")%>
                                                                            </td>
                                                                            <td style="width: 40px">
                                                                                hrs
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("CargoPumps_Avg_KW")%>
                                                                            </td>
                                                                        </tr>

                                                                        <tr class='leafTR'>
                                                                            <td class='leafTD-header'>
                                                                                Aux Boiler:
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("RHRS_BLR")%>
                                                                            </td>
                                                                            <td style="width: 40px">
                                                                                hrs
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="99%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        Remarks:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align: left; background-color: #cce499">
                                                        <asp:TextBox ID="txtremark" Width="1300px" Height="80px" runat="server" Text='<%#Eval("Remarks")%>'
                                                            TextMode="MultiLine" BorderStyle="None" ForeColor="Black" BackColor="#cce499" ReadOnly="true" Enabled="false"> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:FormView>
                                </div>
                                <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
                                    background-color: #FDFDFD">
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="background-color: White; height: 20px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        <asp:FormView ID="fvCargoTanks" runat="server">
                    <ItemTemplate>
                     <table width="99%" style="border: solid 1px gray">
                                                <tr>
                                                    <td style="text-align: left; font-weight: bold;background-color:#e4e4e4" >
                                                        Cargo Tanks:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                    <table cellpadding="0" style="padding: 10px 0px 0px 0px; margin-top: 9px">
                                                            <tr>
                                                                <td valign="top" style="padding-left: 30px">
                                                                    <table cellspacing="1" style="background-color: White">
                                                                        <tr>
                                                                          <td class='style2'>
                                                                               Cargo tank
                                                                            </td>
                                                                            <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                1P
                                                                            </td>                                                                         
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                1S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                2P
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                2S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                3P
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                3S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                4P
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                4CP
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                4CS
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                4S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                6P
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                6CP
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                6CS
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                7P
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                7S
                                                                            </td>
                                                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                8P
                                                                            </td>

                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td width="310px">
                                                                                Tank Temeprature</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("1P_TankTemp")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("1S_TankTemp")%>
                                                                            </td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("2P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("2S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("3P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("3S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4CP_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4CS_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("6P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("6CP_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("6CS_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("7P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("7S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("8P_TankTemp")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='style2'>
                                                                                Tank Pressure</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("1P_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("1S_TankPre")%>
                                                                            </td>
                                                                          <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("2P_TankPre")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("2S_TankPre")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("3P_TankPre")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("3S_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4P_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4CP_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4CS_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("4CS_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("6P_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("6CP_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("6CS_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("7P_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("7S_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("8P_TankPre")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='style2'>
                                                                                O2 LEVEL %</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("1P_O2per")%>
                                                                            </td>
                                                                           <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("1S_O2per")%>
                                                                            </td>

                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("2P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("2S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("3P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("3S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4CP_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4CS_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("6P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("6CP_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("6CS_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("7P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("7S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("8P_O2per")%>
                                                                            </td>
                                                                        </tr>
                                                                    
                                                                    <tr class='leafTR'>
                                                                            <td class='style2'>
                                                                                O2 LEVEL PPM</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("1P_O2PPM")%>
                                                                            </td>
                                                                           <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("1S_O2PPM")%>
                                                                            </td>

                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("2P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("2S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("3P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("3S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4CP_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4CS_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("4S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("6P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("6CP_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("6CS_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("7P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("7S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("8P_O2PPM")%>
                                                                            </td>
                                                                        </tr>
                                                                       
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>




                                                   <tr>
                                                    <td style="text-align: left;">
                                                    <table cellpadding="0" style="padding: 10px 0px 0px 0px; margin-top: 9px">
                                                            <tr>
                                                                <td valign="top" style="padding-left: 30px">
                                                                    <table cellspacing="1" style="background-color: White">
                                                                        <tr>
                                                                          <td class='style2'>
                                                                               Cargo tank
                                                                            </td>
                                                                            <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                8S
                                                                            </td>                                                                         
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  9P</td>
                                                                              <td>
                                                                                  &nbsp;</td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  9S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  10S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  11P</td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  11S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  12P
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  12S
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  13P
                                                                            </td>
                                                                              <td style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                                padding: 5px 0px 5px 10px">
                                                                                  13S
                                                                            </td>
                                                                               <td class='leafTD-data' style=" background-color:White;">
                                                                               
                                                                            </td>
                                                                             <td class='leafTD-data' style=" background-color:White;">
                                                                            </td>
                                                                            <td class='leafTD-data' style=" background-color:White;">
                                                                            </td>
                                                                              <td class='leafTD-data' style=" background-color:White;">
                                                                            </td>
                                                                           <td class='leafTD-data' style=" background-color:White;">
                                                                            </td>

                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td width="310px">
                                                                                Tank Temeprature</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                               <%#Eval("8S_TankTemp")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%#Eval("9P_TankTemp")%>
                                                                            </td>
                                                                            <td></td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("9S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("10S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("11P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("11S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("12P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("12S_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("13P_TankTemp")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("13S_TankTemp")%>
                                                                            </td>
                                                                             
                                                                              <td>
                                                                                 
                                                                            </td>
                                                                            <td>
                                                                                 
                                                                            </td>
                                                                             <td>
                                                                                 
                                                                            </td>
                                                                               <td >
                                                                                
                                                                            </td>
                                                                              <td>
                                                                               
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='style2'>
                                                                                Tank Pressure</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("8S_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                <%# Eval("9P_TankPre")%>
                                                                            </td>
                                                                          <td >
                                                                               
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("9S_TankPre")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("10S_TankPre")%>
                                                                            </td>
                                                                              <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("11P_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("11S_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("12P_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("12S_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("13P_TankPre")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                 <%# Eval("13S_TankPre")%>
                                                                            </td>
                                                                             <td >
                                                                                 
                                                                            </td>
                                                                             <td>
                                                                               
                                                                            </td>
                                                                             <td>
                                                                                 
                                                                            </td>
                                                                            <td >
                                                                              
                                                                            </td>
                                                                             <td>
                                                                               
                                                                            </td>
                                                                        </tr>
                                                                        <tr class='leafTR'>
                                                                            <td class='style2'>
                                                                                O2 LEVEL %</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("8S_O2per")%>
                                                                            </td>
                                                                           <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("9P_O2per")%>
                                                                            </td>

                                                                            <td >
                                                                                
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("9S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("10S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("11P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("11S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("12P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("12S_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("13P_O2per")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("13S_O2per")%>
                                                                            </td>
                                                                             <td >
                                                                               
                                                                            </td>
                                                                              <td>
                                                                            </td>
                                                                             <td >
                                                                            </td>
                                                                              <td >
                                                                            </td>
                                                                             <td>
                                                                            </td>
                                                                        </tr>
                                                                    
                                                                    <tr class='leafTR'>
                                                                            <td class='style2'>
                                                                                O2 LEVEL PPM</td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("8S_O2PPM")%>
                                                                            </td>
                                                                           <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("9P_O2PPM")%>
                                                                            </td>
                                                                            <td class='leafTD-data' style="background-color: White; text-align: center"></td>
                                                                            <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("9S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("10S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("11P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("11S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("12P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("12S_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("13P_O2PPM")%>
                                                                            </td>
                                                                             <td class='leafTD-data' style="text-align: center">
                                                                                  <%# Eval("13S_O2PPM")%>
                                                                            </td>
                                                                           
                                                                             <td>
                                                                            </td>
                                                                             <td >
                                                                                 
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                              <td >
                                                                            </td>
                                                                             <td >
                                                                            </td>
                                                                        </tr>
                                                                       
                                                                    </table>
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
                        <tr>
                            <td>
                               <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;
                                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                    font-family: Tahoma; font-size: 12px;border: 1px solid #cccccc;" >
                                    <table cellspacing="0" cellpadding="1" rules="all" 
                                        width="99%">
                                        <tr>
                                            <td align="right" style="width: 5%; border-left: 1px solid black;">
                                                Master :&nbsp;&nbsp;
                                            </td>
                                            <td style="width: 2%; border-left: 1px solid black;">
                                                <asp:Image ID="imgMaster" runat="server" Height="30px" CssClass="transactLog" />
                                            </td>
                                            <td align="left" style="width: 20%; border-left: 1px solid black;">
                                                <asp:HyperLink ID="lnkMaster" CssClass="FieldDottedLine link" runat="server" ForeColor="Blue"></asp:HyperLink>
                                            </td>
                                            <td style="width: 35%; border-left: 1px solid black;">
                                            </td>
                                            <td align="right" style="width: 10%; border-left: 1px solid black;">
                                                Chief Engineer :&nbsp;&nbsp;
                                            </td>
                                            <td style="width: 2%; border-left: 1px solid black;">
                                                <asp:Image ID="imgChiefEngineer" runat="server" Height="30px" CssClass="transactLog" />
                                            </td>
                                            <td align="left" style="width: 20%; border-left: 1px solid black;">
                                                <asp:HyperLink ID="lnkChiefEngineer" CssClass="FieldDottedLine link" runat="server"
                                                    ForeColor="Blue"></asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    
                </div>
                <%--Content Div End--%>
                <div id='dvempty'></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
