<%@ Page Title="Departure Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Departure_Report.aspx.cs" Inherits="Operations_Departure_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <%--  <script type="text/javascript">

        $(document).ready(function () {

            var wh = 'PKID=<%=Request.QueryString["id"]%>';

            Get_Record_Information_Details('OPS_Telegrams', wh);

        });

    </script>--%>
    <style type="text/css">
        .page
        {
            min-width: 400px;
            width: 95%;
        }
        .cleartd
        {
            width: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Departure Report"></asp:Label>
    </div>
    <div id="page-content" style="color: #333333; border: 1px solid gray; z-index: -2;
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
                                    <div id="dvDepartureReport">
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
                                        <asp:FormView ID="fvdepature" runat="server" Width="100%">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr valign="top">
                                                        <td style="border: solid 1px gray; width: 17%;">
                                                            <table cellspacing="1" width="100%">
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' width="60%">
                                                                    </td>
                                                                    <td width="40%">
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 100px; text-align: center">
                                                                                    Date
                                                                                </td>
                                                                                <td style="width: 5px; background-color: White;">
                                                                                </td>
                                                                                <td style="width: 50px; text-align: center">
                                                                                    Time
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td width="5%">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        C.O.P:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                                    <%#Eval("SSP")%>
                                                                                </td>
                                                                                <td style="width: 5px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 50px; text-align: center">
                                                                                    <%#Eval("SSP_HH")%>:<%#Eval("SSP_HH")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                               <%-- <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="text-align: left;">
                                                                        Total Time Since Last Event:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("TotalTimeLastEvents")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Hrs
                                                                    </td>
                                                                </tr>--%>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Total Time At Port:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("TotalTimeAtPort")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Hrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Voyage Number:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("VOYAGE")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Departure From Port:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("DEPARTURE_PORT_NAME")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        UTC / Time Zone:
                                                                    </td>
                                                                    <td class='leafTD-data'>
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
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        SBE:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                                    <%#Eval("SBE_Date")%>
                                                                                </td>
                                                                                <td style="width: 5px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 50px; text-align: center">
                                                                                    <%#Eval("SBE_HH")%>:<%#Eval("SBE_MI")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        POB:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 97px; text-align: center">
                                                                                    <%#Eval("POB_Date")%>
                                                                                </td>
                                                                                <td style="width: 5px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 53px; text-align: center">
                                                                                    <%#Eval("POB_HH")%>:<%#Eval("POB_MI")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Next Port:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("NEXT_PORT_NAME")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Distance To Go:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("PassDistToGo")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Miles
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        ETA Next Port:
                                                                    </td>
                                                                    <td class='leafTD-data'>
                                                                        <table cellspacing="0">
                                                                            <tr>
                                                                                <td class='leafTD-data-left' style="width: 97px; text-align: center">
                                                                                    <%#Eval("POB_Date")%>
                                                                                </td>
                                                                                <td style="width: 5px; background-color: White;">
                                                                                </td>
                                                                                <td class='leafTD-data-left' style="width: 53px; text-align: center">
                                                                                    <%#Eval("ETA_Next_Port_HH")%>:<%#Eval("ETA_Next_Port_MI")%></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Passage Instructions:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Passage_Instructions")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="padding-bottom: 85px;">
                                                                        Passage Instructions Remark:
                                                                    </td>
                                                                    <td class='leafTD-data' colspan='2' style="padding-bottom: 50px;">
                                                                        <asp:Label ID="lblPassageInstructionsRemarks" runat="server" Text='<%#Eval("PassgeInstructionsOther")%>'
                                                                            Width="100%" Height="50px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="border: solid 1px gray; width: 25%;" valign="top">
                                                            <table cellspacing="1" cellpadding="0" border="0" width="99%">
                                                                <tr class='leafTR' style="background-color: #99ccff; text-align: left;">
                                                                    <td rowspan="2" style="background-color: White; text-align: left;" colspan='2'>
                                                                        Heavy Oil:
                                                                    </td>
                                                                    <td rowspan="2" style="width: 120px;">
                                                                        Bunkers Received
                                                                    </td>
                                                                    <td colspan="3">
                                                                        Total Consumption in Port
                                                                    </td>
                                                                    <td rowspan="2">
                                                                        ROB
                                                                    </td>
                                                                    <td style="width: 20px; background-color: White" rowspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: #99ccff; text-align: center;">
                                                                    <td>
                                                                        ME
                                                                    </td>
                                                                    <td>
                                                                        AE
                                                                    </td>
                                                                    <td>
                                                                        Boiler
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="text-align: left; width: 500px">
                                                                        HSFO %S:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center; width: 50px">
                                                                        <%#Eval("HSFO_Purity")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center; width: 50px">
                                                                        <%#Eval("HO_BunkersReceived")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center; width: 50px">
                                                                        <%#Eval("ME_Cons_HSFO")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center; width: 50px">
                                                                        <%#Eval("AE_Cons_HSFO")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center; width: 50px">
                                                                        <%#Eval("Boiler_Cons_HSFO")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center; width: 50px">
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
                                                                        <%#Eval("LO_BunkersReceived")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ME_Cons_LSFO")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("AE_Cons_LSFO")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Boiler_Cons_LSFO")%>
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
                                                                        DO %S:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("DO_Purity")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("DO_BunkersReceived")%>
                                                                    </td>
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
                                                                        <%#Eval("DO_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2" style="text-align: left;">
                                                                        ME SYS OIL:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Me_SysOil_BunkerReceived")%>
                                                                    </td>
                                                                    <td colspan='3'>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Me_SysOil_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="text-align: left;">
                                                                        ME CYL OIL:
                                                                    </td>
                                                                    <td>
                                                                        TBN
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        70
                                                                    </td>
                                                                    <td colspan="3" style="background-color: #99ccff; text-align: center;">
                                                                        70
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
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("ME_Cyl_Oil_BunkerReceivedTBN2")%>
                                                                    </td>
                                                                    <td colspan="3" class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("ME_Cons_At_Sea_TBN2")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("ME_ROB_TBN2")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        TBN
                                                                    </td>
                                                                    <td style="background-color: #99ccff; text-align: center;">
                                                                        40
                                                                    </td>
                                                                    <td colspan="3" style="background-color: #99ccff; text-align: center;">
                                                                        40
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
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("ME_Cyl_Oil_BunkerReceivedTBN4")%>
                                                                    </td>
                                                                    <td colspan="3" class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("ME_Cons_At_Sea_TBN4")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("ME_ROB_TBN4")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2" style="text-align: left;">
                                                                        A.E SYS OIL:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("AE_SysOil_BunkersReceived")%>
                                                                    </td>
                                                                    <td colspan='3'>
                                                                    </td>
                                                                    <td class='leafTD-data' style="width: 100px; text-align: center;">
                                                                        <%#Eval("AE_SysOil_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2" style="text-align: left;">
                                                                        TECHNICAL F.W:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("TechnicalFW_BunkersReceived")%>
                                                                    </td>
                                                                    <td colspan="3" class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Technical_FW_Consp_At_Sea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Technical_FW_Consp_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2" style="text-align: left;">
                                                                        DOMESTIC F.W:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("DomesticFW_BunkersReceived")%>
                                                                    </td>
                                                                    <td colspan="3" class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Domestic_FW_Consp_At_Sea")%>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Domestic_FW_Consp_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        Ltrs
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td colspan='2'>
                                                                    </td>
                                                                    <td>
                                                                        Discharged
                                                                    </td>
                                                                    <td colspan="3">
                                                                    </td>
                                                                    <td>
                                                                        ROB
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2" style="text-align: left;">
                                                                        Sludge:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Sludge_Discharged")%>
                                                                    </td>
                                                                    <td colspan="3">
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Sludge_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' colspan="2" style="text-align: left;">
                                                                        Oily Bildge Water:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Oil_Bildge_Discharged")%>
                                                                    </td>
                                                                    <td colspan="3">
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center">
                                                                        <%#Eval("Oil_Bildge_ROB")%>
                                                                    </td>
                                                                    <td style="width: 20px; height: 20px">
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <br />
                                                            <table style="width: 45%">
                                                                <tr>
                                                                    <td colspan='3' style="background-color: #99ccff; text-align: left;">
                                                                        Bunker Sample Leading Status
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        Heavy Oil:
                                                                    </td>
                                                                    <td colspan='2'>
                                                                        <%#Eval("HO_BunkerSampleLandingStatus")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        Diesel Oil:
                                                                    </td>
                                                                    <td colspan='2'>
                                                                        <%#Eval("DieselOil_BunkerSampleLandingStatus")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;Lube Oil:
                                                                    </td>
                                                                    <td colspan='2'>
                                                                        <%#Eval("LubeOil_BunkerSampleLandingStatus")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 70px;">
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 1%">
                                                        </td>
                                                        <td style="border: solid 1px gray; width: 30%;" valign="top">
                                                            <table cellspacing="1" width="100%">
                                                                <tr class='leafTR'>
                                                                    <td style="width: 25%;">
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        Fwd(mtrs)
                                                                    </td>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        Mid(mtrs)
                                                                    </td>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        Aft(mtrs)
                                                                    </td>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 10%;">
                                                                        Mean
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Draft:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("FwdDraft")%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("MIDDRAFT")%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("AftDraft")%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Mean_Draft")%>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        GM:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("GM")%>
                                                                    </td>
                                                                    <td>
                                                                        mtrs
                                                                    </td>
                                                                    <td colspan='2' style="text-align: right;">
                                                                        Trim:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Trim")%>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Constant Weight:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("ConstantWeight")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Total Ballast:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("TotalBallast")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Total Cargo:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Total_Cargo")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                        MT
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Max Shearing Force:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Shearing_Force")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                        %
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Max Bending Moment:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Bending_Movement")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                        %
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Max Torional Moment:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("Tortional_Movement")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                        %
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Number Of Tugs Used:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("TUG_USED")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header'>
                                                                        Bow Thruster:
                                                                    </td>
                                                                    <td class='leafTD-data' style="text-align: center;">
                                                                        <%#Eval("BowThruster")%>
                                                                    </td>
                                                                    <td colspan='7'>
                                                                    </td>
                                                                </tr>
                                                                <tr class='leafTR'>
                                                                    <td class='leafTD-header' style="padding-bottom: 50px;">
                                                                        Bow Thruster Remarks:
                                                                    </td>
                                                                    <td class='leafTD-data' colspan='5' style="padding-bottom: 20px;">
                                                                        <asp:Label ID="lblBowThrusterRemarks" runat="server" Height="50px" Text='<%#Eval("BowThrusterRemarks")%>'
                                                                            Width="100%" ForeColor="Red"></asp:Label>
                                                                    </td>
                                                                    <td colspan="3">
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
                                                                            Width="1480px" ReadOnly="true" Enabled="false"> </asp:TextBox> 
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
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
                                <div style="background-color: White; height: 50px;">
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
                 <div id='dvempty'></div>
                <%--Content Div End--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
            background-color: #FDFDFD">
        </div>
    </div>
</asp:Content>
