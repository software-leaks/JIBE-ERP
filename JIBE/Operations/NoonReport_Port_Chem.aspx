<%@ Page Title="Noon Report At Port Chemical" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="NoonReport_Port_Chem.aspx.cs" Inherits="Operations_NoonReport_Port_Chem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <%--<script type="text/javascript">

        $(document).ready(function () {

            var wh = 'PKID=<%=Request.QueryString["id"]%>';

            Get_Record_Information_Details('OPS_Telegrams', wh);

        });

        function OpenCrewList(vcode) {

            return false;
        }
    </script>--%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Daily Noon Report At Port"></asp:Label>
    </div>
    <div id="page-content" style="min-height: 640px; color: #333333; border: 1px solid gray;
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
                <%--Content--%>
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
                        <tr>
                            <td>
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
                                    <asp:FormView ID="fvnoonreport" runat="server" Width="100%">
                                        <ItemTemplate>
                                            <table width="100%" cellspacing="0">
                                                <tr>
                                                    <td valign="top" style="border: solid 1px gray" width="35%">
                                                        <table cellspacing="1" width="99%">
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
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
                                                                <td class='leafTD-data-left' style="text-align: center;">
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
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Presently:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Location_Name")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                      <%--      <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Canal Transit:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("Canal_Transit")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>--%>
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
                                                           <%-- <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    Air Temp Wet:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("AirTempWet")%>
                                                                </td>
                                                                <td>
                                                                    Deg C.
                                                                </td>
                                                            </tr>--%>
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
                                                                <td style="width: 30px; height: 20px">
                                                                    Deg C
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 5%">
                                                    </td>
                                                    <td valign="top" style="border: solid 1px gray" width="100%">
                                                        <table cellspacing="1" width="100%">
                                                            <tr class='leafTR'>
                                                                <td width="8%">
                                                                </td>
                                                                <td width="15%">
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
                                                                <td width="15%">
                                                                </td>
                                                                <td width="10%">
                                                                </td>
                                                                <td style="height: 20px; width: 5%; background-color: #99ccff; text-align: center;
                                                                    font-weight: bold; border-top: 3px solid white">
                                                                    ROB
                                                                </td>
                                                                <td width="5%">
                                                                </td>
                                                                <td width="17%">
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td class='leafTD-header'>
                                                                    ETB:
                                                                </td>
                                                                <td class='leafTD-data'>
                                                                    <table cellspacing="0">
                                                                        <tr>
                                                                            <td class='leafTD-data-left' style="width: 80px; text-align: center">
                                                                                <%#Eval("ETB_Date")%>
                                                                            </td>
                                                                            <td style="width: 5px; background-color: White;">
                                                                            </td>
                                                                            <td class='leafTD-data-left' style="width: 35px; text-align: center">
                                                                                <%#Eval("ETB_HH")%>:<%#Eval("ETB_MI")%></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    HSFO :
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("HO_ROB")%>
                                                                </td>
                                                                <td>
                                                                    MT
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
                                                                                <%#Eval("ETD_Date")%>
                                                                            </td>
                                                                            <td style="width: 5px; background-color: White;">
                                                                            </td>
                                                                            <td class='leafTD-data-left' style="width: 35px; text-align: center">
                                                                                <%#Eval("ETD_HH")%>:<%#Eval("ETD_MI")%></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    LSFO :
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("LSFO_ROB")%>
                                                                </td>
                                                                <td>
                                                                    MT
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td>
                                                                    Next Port:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("NEXT_PORT_NAME")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    DO :
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("DO_ROB")%>
                                                                </td>
                                                                <td>
                                                                    MT
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR' >
                                                                <td style="text-align: left;">
                                                                    Balance:
                                                                </td>
                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                    <%#Eval("BalanceCargo")%>
                                                                </td>
                                                                <td>
                                                                    MT
                                                                </td>
                                                                <td>
                                                                </td>
                                                              <td style="height: 20px; background-color: #99ccff; text-align: center; font-weight: bold;
                                                                    border-top: 3px solid white">
                                                                    Received
                                                                </td>
                                                                <td style="height: 20px; background-color: #99ccff; text-align: center; font-weight: bold;
                                                                    border-top: 3px solid white">
                                                                    ROB
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                
                                                                <td style="text-align: left;">
                                                                    Discharged:
                                                                </td>
                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                    <%#Eval("Discharged_Cargo")%>
                                                                </td>
                                                                <td>
                                                                    MT
                                                                </td>
                                                               
                                                                <td style="text-align: left;width:250px">
                                                                    DOMESTIC F.W:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("ReceivedDomestic_FW")%>
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("ReceivedROB_FW")%>
                                                                </td>
                                                                <td>
                                                                    MT
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                               
                                                                <td style="text-align: left;">
                                                                    Loaded:
                                                                </td>
                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                    <%#Eval("Loaded_Cargo")%>
                                                                </td>
                                                                <td>
                                                                    MT
                                                                </td>
                                                                
                                                               <td style="text-align: left;width:250px">
                                                                    TECHNICAL F.W:
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("TechnicalReceivedDomestic_FW")%>
                                                                </td>
                                                                <td class='leafTD-data-left'>
                                                                    <%#Eval("TechnicalROB_FW")%>
                                                                </td>
                                                                <td>
                                                                    MT
                                                                </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                               
                                                                <td style="text-align: left;">
                                                                    Shifting:
                                                                </td>
                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                    <%#Eval("Shifting")%>
                                                                </td>
                                                                                                                       
                                                             <td style="width: 50px; text-align: left" >MT
                                                             </td>
                                                            </tr>
                                                            <tr class='leafTR'>
                                                               <td></td>
                                                                <td colspan="5">
                                                                    <asp:GridView ID="gdShifting" runat="server" AutoGenerateColumns="False" Width="28%">
                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                        <RowStyle CssClass="RowStyle-css" />
                                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SHIFTFROMTIME" HeaderText="From Time" ItemStyle-HorizontalAlign="Center"/>
                                                                            <asp:BoundField DataField="SHIFTTILLTIME" HeaderText="To Time" ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                                <tr class='leafTR'>
                                                               
                                                                <td style="width: 100px; text-align: left">
                                                                    Time From :
                                                                </td>
                                                               <td class='leafTD-data-left' style="width: 100px; text-align:center">
                                                                    <%#Eval("Shifting")%>
                                                                </td>
                                                                </tr>     
                                                            
                                                            <tr class='leafTR'>
                                                               
                                                                <td style="text-align: left;">
                                                                    Tugs Used:
                                                                </td>
                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                    <%#Eval("TUG_USED")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr class='leafTR'>
                                                                <td style="text-align: left;width: 450px;">
                                                                    Bow Thruster:
                                                                </td>
                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                    <%#Eval("BowThruster")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr class='leafTR'>
                                                                
                                                                <td style="text-align: left;width: 450px;">
                                                                    Remarks(Bow Thruster):
                                                                </td>
                                                                <td class='leafTD-data-left' style="width: 100px; text-align: center">
                                                                    <%#Eval("BowThrusterRemarks")%>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                           
                                                          
                                                            
                                                            
                                                     
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            </center>
                                            <table width="90%">
                                                <tr>
                                                    <td style="text-align: left; width: 80%;">
                                                        Remarks:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; background-color: #cce499">
                                                        <asp:TextBox ID="txtremark" Width="1190px" Height="80px" runat="server" Text='<%#Eval("Remarks")%>'
                                                            TextMode="MultiLine" BorderStyle="None" ForeColor="Black" BackColor="#cce499"
                                                            ReadOnly="true" Enabled="false"> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:FormView>
                                </div>
                                <div id="dvRecordInformation" style="text-align: left; width: 99.9%; border: 1px solid gray;
                                    background-color: #FDFDFD">
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="background-color: White;">
                        <tr>
                            <td>
                                <div style="background-color: White; height: 70px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;
                                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                    font-family: Tahoma; font-size: 12px; border: 1px solid #cccccc;">
                                    <table cellspacing="0" cellpadding="1" rules="all" width="99%">
                                        <tr>
                                            <td align="right" style="width: 5%; border-left: 1px solid black;">
                                                Master :&nbsp;&nbsp;
                                            </td>
                                            <td style="width: 2%; border-left: 1px solid black;">
                                                <asp:Image ID="imgMaster" runat="server" Height="30px" CssClass="transactLog" />
                                            </td>
                                            <td align="left" style="width: 25%; border-left: 1px solid black;">
                                                <asp:HyperLink ID="lnkMaster" CssClass="FieldDottedLine link" runat="server" ForeColor="Blue"></asp:HyperLink>
                                            </td>
                                            <td style="width: 25%; border-left: 1px solid black;">
                                            </td>
                                            <td align="right" style="width: 10%; border-left: 1px solid black;">
                                                Chief Engineer :&nbsp;&nbsp;
                                            </td>
                                            <td style="width: 2%; border-left: 1px solid black;">
                                                <asp:Image ID="imgChiefEngineer" runat="server" Height="30px" CssClass="transactLog" />
                                            </td>
                                            <td align="left" style="width: 25%; border-left: 1px solid black;">
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
                <%--Content--%>
                <div id='dvempty'>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
