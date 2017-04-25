<%@ Page Title="Arrival Report Chemical " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Arrival_Report_Chem.aspx.cs" Inherits="Operations_Arrival_Report_Chem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <%--<script src="../Scripts/Common_Functions.js" type="text/javascript"></script>--%>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <%-- <script type="text/javascript">

        $(document).ready(function () {

            var wh = 'PKID=<%=Request.QueryString["id"]%>';

            Get_Record_Information_Details('OPS_Telegrams', wh);

        });

    </script>--%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Arrival Report"></asp:Label>
    </div>
    <div id="page-content" style="color: #333333; border: 1px solid gray; z-index: -2;
        width: 99.9%">
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
                            <td style="text-align: left; font-weight: bold;">
                                Vessel Name:&nbsp;<asp:Label ID="lblVessel" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                                &nbsp; &nbsp; &nbsp;|&nbsp; &nbsp; &nbsp;
                                <asp:HyperLink ID="hplcrewlist" runat="server" Target="_blank" Text="Crew List"></asp:HyperLink>
                            </td>
                            <td align="right" style="padding-right: 17px">
                                &nbsp;
                            </td>
                        </tr>
                        <caption>
                            &nbsp;<tr>
                                <td colspan="2">
                                    <div id="dvArrivalReport" runat="server">
                                        <style type="text/css">
                                            .leafTR
                                            {
                                                border-bottom-style: solid;
                                                border-bottom-color: White;
                                                border-bottom-width: 1px;
                                            }
                                            .leafTD-header
                                            {
                                                width: 150px;
                                                height: 20px;
                                                padding: 0px 0px 0px 10px;
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
                                            #pageTitle
                                            {
                                                background-color: gray;
                                                color: White;
                                                font-size: 12px;
                                                text-align: center;
                                                padding: 2px;
                                                font-weight: bold;
                                            }
                                        </style>
                                        <asp:FormView ID="fvArrival" runat="server" Width="100%">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr valign="top">
                                                        <td style="border: solid 1px gray; width: 50%;">
                                                            <table cellspacing="1" width="99%">
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="width: 140px;">
                                                                    </td>
                                                                    <td>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 80px; text-align: center">
                                                                                    Date
                                                                                </td>
                                                                                <td style="width: 1px;">
                                                                                </td>
                                                                                <td style="width: 40px; text-align: center;">
                                                                                    Time
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td colspan="6">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="width: 140px;">
                                                                        E.O.P:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 80px; text-align: center">
                                                                                    <%#Eval("EOP_Date")%>
                                                                                </td>
                                                                                <td style="width: 1px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 40px; text-align: center">
                                                                                    <%#Eval("EOP_HH")%>:<%#Eval("EOP_MI")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td colspan='6'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="width: 140px;">
                                                                        Voyage No.:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("VOYAGE")%>
                                                                    </td>
                                                                    <td colspan='6'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="width: 140px;">
                                                                        Arrival From Port:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("DEPARTURE_PORT_NAME")%>
                                                                    </td>
                                                                    <td colspan='6'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="width: 140px;">
                                                                        Arrival To Port:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("ARRIVAL_PORT_NAME")%>
                                                                    </td>
                                                                    <td colspan='6'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="width: 140px;">
                                                                        UTC / Time Zone:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 90px; border-right: 1px solid white; padding-right: 2px">
                                                                                    <%#Eval("UTC_TYPE")%>
                                                                                </td>
                                                                                <td style="width: 40px; padding-left: 2px">
                                                                                    <%#Eval("UTC_HR")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td colspan='6'>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 150px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 100px; text-align: center">
                                                                                    Fwd(mtrs)
                                                                                </td>
                                                                                <td style="width: 100px; text-align: center">
                                                                                    Mid(mtrs)
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 20px;">
                                                                    </td>
                                                                    <td style="width: 100px; text-align: center;">
                                                                        Aft(mtrs)
                                                                    </td>
                                                                    <td style="width: 100px; text-align: center;">
                                                                        Mean
                                                                    </td>
                                                                    <td style="width: 100px; text-align: center;">
                                                                        Trim
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Draft:
                                                                    </td>
                                                                    <td>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 100px; text-align: center" class='leafTD-data'>
                                                                                    <%#Eval("FwdDraft")%>
                                                                                </td>
                                                                                <td style="width: 1px;">
                                                                                </td>
                                                                                <td style="width: 100px; text-align: center" class='leafTD-data'>
                                                                                    <%#Eval("MIDDRAFT")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 20px;">
                                                                    </td>
                                                                    <td style="width: 100px; text-align: center;" class='leafTD-data'>
                                                                        <%#Eval("AftDraft")%>
                                                                    </td>
                                                                    <td style="width: 100px; text-align: center;" class='leafTD-data'>
                                                                        <%#Eval("Mean_Draft")%>
                                                                    </td>
                                                                    <td style="width: 100px; text-align: center;" class='leafTD-data'>
                                                                        <%#Eval("TrimDraft")%>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        ETB/ Berthed:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 80px; text-align: center">
                                                                                    <%#Eval("ETB_Date")%>
                                                                                </td>
                                                                                <td style="width: 1px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 40px; text-align: center">
                                                                                    <%#Eval("ETB_HH")%>:<%#Eval("ETB_MI")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                   <td style="width: 20px;">
                                                                    </td>
                                                                   <%-- <td style="width: 100px; text-align: center;" class='leafTD-data'>
                                                                        <%#Eval("AftDraft")%>
                                                                    </td>
                                                                    <td style="width: 100px; text-align: center;" class='leafTD-data'>
                                                                        <%#Eval("Mean_Draft")%>
                                                                    </td>--%>
                                                                    <td>
                                                                        
                                                                    </td>
                                                                   
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Presently:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Location_Name")%>
                                                                    </td>
                                                                    <td colspan='2' style="text-align: right;">
                                                                        Canal Transit:
                                                                    </td>
                                                                    <td class='leafTD-data' style="width: 80px; text-align: center">
                                                                        <%#Eval("Canal_Transit")%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        ETS:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 80px; text-align: center">
                                                                                    <%#Eval("ETS_Date")%>
                                                                                </td>
                                                                                <td style="width: 1px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 40px; text-align: center">
                                                                                    <%#Eval("ETS_HH")%>:<%#Eval("ETS_MI")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Next Port:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("NEXT_PORT_NAME")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        ETA Next Port:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 80px; text-align: center">
                                                                                    <%#Eval("ETA_Next_Port")%>
                                                                                </td>
                                                                                <td style="width: 1px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 40px; text-align: center">
                                                                                    <%#Eval("ETA_Next_Port_HH")%>:<%#Eval("ETA_Next_Port_MI")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <span style="font-weight: bold; font-size: 14px; text-decoration: underline; color: Gray;">
                                                                            Passage Summary</span>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Net Steaming Time:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Steaming_hrs")%>
                                                                    </td>
                                                                    <td>
                                                                        Hr.
                                                                    </td>
                                                                    <td colspan='6'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Detentions:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Detention")%>
                                                                    </td>
                                                                    <td>
                                                                        Hr.
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        Drifts:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Drifts")%>
                                                                    </td>
                                                                    <td>
                                                                        Hr.
                                                                    </td>
                                                                    <td colspan='3'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Passage Total Time:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("PassageTotalTime")%>
                                                                    </td>
                                                                    <td>
                                                                        Hr.
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan='3'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Distance:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("TotalDistance")%>
                                                                    </td>
                                                                    <td>
                                                                        NM
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        Avg. Speed:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("AVERAGE_SPEED")%>
                                                                    </td>
                                                                    <td>
                                                                        Hr.
                                                                    </td>
                                                                    <td colspan='3'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Average RPM:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("EngRPM")%>
                                                                    </td>
                                                                    <td colspan='2' style="text-align: right;">
                                                                        Avg. Slip:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Avg_Voy_Slip")%>
                                                                    </td>
                                                                    <td>
                                                                        %
                                                                    </td>
                                                                    <td colspan='3'>
                                                                    </td>
                                                                </tr>
                                                               <%-- <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        EEDI:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("EEOI")%>
                                                                    </td>
                                                                    <td colspan='6' style="text-align: left;">
                                                                        Gr CO2/Ton Cargo*NM
                                                                    </td>
                                                                </tr>--%>
                                                            </table>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="border: solid 1px gray; width: 35%;" valign="top">
                                                            <table cellspacing="0" cellpadding="0" border="0" width="90%">
                                                           <%--     <tr class='leafTR' style="background-color: #99ccff; text-align: left;">
                                                                    <td rowspan="2" style="background-color: White; text-align: center;" colspan='2'>
                                                                        <span style="font-weight: bold; font-size: 16px; text-decoration: underline; color: Gray;">
                                                                            Passage Summary</span>
                                                                    </td>
                                                                    <td rowspan="2" style="width: 120px; text-align: center;">
                                                                        Average Consp./24 hours
                                                                    </td>
                                                                    <td rowspan="2" style="text-align: center;">
                                                                        Total Consumption At Sea Passage
                                                                    </td>
                                                                    <td rowspan="2" style="text-align: center;">
                                                                        ROB
                                                                    </td>
                                                                    <td style="width: 20px; background-color: White" rowspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="text-align: left; width: 60px">
                                                                        HSFO :
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("HSFO_Purity")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("HSFO_ConsPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("HSFO_ConspAtSea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("HO_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="text-align: left;">
                                                                        LSFO %S:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("LSFO_Purity")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("LSFO_ConsPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("LSFO_ConspAtSea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("LSFO_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="text-align: left;">
                                                                        D.O %S:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("DO_Purity")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("DO_ConsPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("DO_ConspAtSea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("DO_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='2'>
                                                                        Total Cons. HSFO+D.O:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("TotalHSFO_DO_AvgConspPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("TotalHSFO_DO_TotalConspAtSea")%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='2'>
                                                                        ME LUB OIL STORAGE:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_LUB_OIL_ConsPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_LUB_OIL__TotalConspAtSea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_LUB_OIL_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>--%>
                                                                     <tr class='leafTR' style="background-color: #99ccff; text-align: center;">
                                                                <td rowspan="3" style="background-color: White">
                                                                </td>
                                                                <td rowspan="3" style="background-color: White">
                                                                % Sulphur
                                                                </td>
                                                                <td rowspan="3" style="width: 120px">
                                                                    Average Consp./24 hours
                                                                </td>
                                                                <td colspan="6">
                                                                    Total Consumption At Sea Passage
                                                                </td>
                                                                <td rowspan="3">
                                                                    ROB
                                                                </td>
                                                                <td style="width: 20px; background-color: White" rowspan="2">
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr style="background-color: #99ccff; text-align: center;">
                                                             <td rowspan="2">
                                                                    Total
                                                                </td>
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
                                                                 <td class='leafTD-header' style="text-align: left; width: 60px">
                                                                    HSFO :
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("HSFO_Purity")%>
                                                                </td>
                                                              <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("HSFO_ConsPer24Hours")%>
                                                                </td>
                                                                   <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("HSFO_ConspAtSea")%>
                                                                </td>
                                                               <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("ME_Cons_HSFO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("AE_Cons_HSFO")%>
                                                                </td>
                                                               <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("Boiler_Cons_HSFO")%>
                                                                </td>
                                                                 <td class='leafTD-data' style="text-align: center;">  
                                                                  <%#Eval("Cargo_Cons_HSFO")%>                                                                
                                                                </td>
                                                                 <td>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
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
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("LSFO_Purity")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("LSFO_ConsPer24Hours")%>
                                                                </td>
                                                                 <td class='leafTD-data' style="text-align: center;">
                                                                  <%#Eval("LSFO_ConspAtSea")%>
                                                                 </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("ME_Cons_LSFO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                    <%#Eval("AE_Cons_LSFO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Boiler_Cons_LSFO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Cargo_Cons_LSFO")%>      
                                                                </td>
                                                                <td></td>
                                                                 <td class='leafTD-data' style="width: 100px;text-align: center;">
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
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("DO_Purity")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("DO_ConsPer24Hours")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;"> <%#Eval("DO_ConspAtSea")%></td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("ME_Cons_MGO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("AE_Cons_MGO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("Boiler_Cons_MGO_DO")%>
                                                                </td>
                                                                  <td class='leafTD-data' style="text-align: center;">
                                                                   <%#Eval("Cargo_Cons_MGO_DO")%>   
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("DO_Incinerators")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;">
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
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("Total_ConsPer24Hours_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="text-align: center;"></td>
                                                                <td class='leafTD-data' style="text-align: center;">
                                                                    <%#Eval("Total_ME_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Total_AE_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Total_Boiler_HO_LO_DO")%>
                                                                </td>
                                                                 <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                    <%#Eval("TotalCargoHeating_HO_LO_DO")%>
                                                                </td>
                                                                <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                    <%#Eval("Total_Incinerator_HO_LO_DO")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                             <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                 
                                                                    </td>
                                                                    <td class='leafTD-header' style="text-align: center;">
                                                                      
                                                                    </td>
                                                                    <td>
                                                                      
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        Total
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        ROB
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header' colspan="2">
                                                                    ME LUB OIL STORAGE:
                                                                </td>
                                                                   <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_LUB_OIL_ConsPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_LUB_OIL__TotalConspAtSea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_LUB_OIL_ROB")%>
                                                                    </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                 <td>
                                                                </td>
                                                                <td >
                                                                  
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                 
                                                                </td>
                                                            </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='2'>
                                                                        AUX ENG STORAGE:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("AuxEngStorage_AvgConspPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("AuxEngStorage_TotalConspAtSea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("AuxEngStorage_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        ME CYL OIL:
                                                                    </td>
                                                                    <td class='leafTD-header' style="text-align: center;">
                                                                        TBN:
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        70
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        70
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        70
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_CYL_OIL_ConsPer24Hours_TBN2")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_Cons_At_Sea_TBN2")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_ROB_TBN2")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td>
                                                                    </td>
                                                                    <td class='leafTD-header' style="text-align: center;">
                                                                        TBN
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        40
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        40
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        40
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_CYL_OIL_ConsPer24Hours_TBN4")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_Cons_At_Sea_TBN4")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_ROB_TBN4")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='2'>
                                                                        CYL OIL TOTAL CONSUMPTION:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Cyl_Oil_Total_ConsPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Cyl_Oil_Total_ConsAtSea")%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='2'>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='3'>
                                                                        DOMESTIC F.W:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Domestic_FW_Consp_At_Sea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Domestic_FW_Consp_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='3'>
                                                                        TECHNICAL F.W:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Technical_FW_Consp_At_Sea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Technical_FW_Consp_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='4'>
                                                                        Total Ballast:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("TotalBallast")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='2'>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan='2'>
                                                                    </td>
                                                                   <td style="text-align: center;">
                                                                    <span style="font-weight: bold;font-size:x-small">Avg. Consp/24 hours</span>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                    <span style="font-weight: bold;font-size:x-small">Total Consumption at Sea Passage</span>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <span style="font-weight: bold;">ROB</span>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2">
                                                                        Sludge:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Sludge_ConspPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Sludge_ConspAtSeaPassage")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Sludge_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2">
                                                                        Oily Bildge Water:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("OilBildge_ConspPer24Hours")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("OilBildge_ConspAtSeaPassage")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Oil_Bildge_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <br />
                                                            <table style="width: 40%">
                                                                <tr>
                                                                    <td colspan='2' style="background-color: #99ccff; text-align: left;">
                                                                        Planned Bunkers
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 15%">
                                                                        Heavy Oil:
                                                                    </td>
                                                                    <td style="width: 25%">
                                                                        <%#Eval("HO_BunkerSampleLandingStatus")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Diesel Oil:
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("DieselOil_BunkerSampleLandingStatus")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Lube Oil:
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("LubeOil_BunkerSampleLandingStatus")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border: solid 1px gray" colspan="5">
                                                            <table width="99%">
                                                                <tr>
                                                                    <td>
                                                                        Remarks:
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100%; height: 120px; background-color: #cce499">
                                                                        <asp:TextBox ID="txtremark" runat="server" BackColor="#cce499" BorderStyle="None"
                                                                            ForeColor="Black" Height="100%" MaxLength="400" Text='<%#Eval("Remarks")%>' TextMode="MultiLine"
                                                                            Width="1175px" ReadOnly="true" Enabled="false"> </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </div> </table> </td> </tr> </table>
                                            </ItemTemplate>
                                        </asp:FormView>
                                    </div>
                                </td>
                            </tr>
                        </caption>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="background-color: White; height: 5px;">
                                </div>
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
                                            <td style="width: 10%; border-left: 1px solid black;">
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
                <div id='dvempty'>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
            background-color: #FDFDFD">
        </div>
    </div>
</asp:Content>
