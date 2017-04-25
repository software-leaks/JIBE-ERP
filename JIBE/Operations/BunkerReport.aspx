<%@ Page Language="C#"  MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="BunkerReport.aspx.cs" Inherits="Operations_BunkerReport" Title="Bunker Report" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            debugger;
            var wh = 'BUNKER_REPORT_ID=<%=Request.QueryString["BunkerReportId"]%> AND VESSEL_ID=<%=Request.QueryString["VESSELID"]%>';

            Get_Record_Information_Details('OPS_Bunker_Report', wh);

        });

         
    </script>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContent" runat="server">
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="Bunker Report"></asp:Label>
    </div>
    <div id="page-content" style="min-height: 480px; color: #333333; border: 1px solid gray;
        z-index: -2; width: 100%">
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
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                         <td align="right" style="padding-right: 17px">
                            <uc1:ctlRecordNavigation ID="ctlRecordNavigationReport" OnNavigateRow="BindReportDr"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="dvBunkerReport">
                                <style type="text/css">
                                    .leafTR
                                    {
                                        border-bottom-style: solid;
                                        border-bottom-color: White;
                                        border-bottom-width: 1px;
                                    }
                                    .leafTD-header
                                    {
                                        width: 200px;
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
                                <asp:FormView ID="fvBunker" FooterStyle-ForeColor="Black" runat="server"
                                    Width="100%" >
                                    <ItemTemplate>
                                        <table width="100%">
                                         <tr>
                                                <td valign="top" style="border: solid 1px gray">
                                                    <table>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Vessel Name:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("VESSELNAME")%>
                                                            </td>
                                                           <td style="width: 43px; height: 20px">
                                                            </td>
                                                           <%-- <td class='leafTD-header'>
                                                                IMO No:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("IMO_NO")%>
                                                            </td>
                                                            <td style="width: 43px; height: 20px">
                                                            </td>
                                                            <td class='leafTD-header'>
                                                                Call Sign:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("CALL_SIGN")%>
                                                            </td>--%>                                                            
                                                         </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table> 
                                        <table width="100%">
                                         <tr>
                                                <td valign="top" style="border: solid 1px gray">
                                                    <table>
                                                      
                                                         <tr class='leafTR'>
                                                             <td class='leafTD-header'>
                                                                Voyage No.:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("VOYAGE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                         </tr>
                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Bunker Port:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("BUNKER_PORT_NAME")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>
                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>                                                                
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td>Deg &nbsp  Min &nbsp Sec</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>
                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>   
                                                                Latitude:                                                             
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("Latitude_Degrees")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("Latitude_Minutes")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("Latitude_Seconds")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> &nbsp&nbsp<%#Eval("LATITUDE_N_S")%></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                         </tr>

                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>   
                                                                Longitude:                                                            
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("Longitude_Degrees")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("Longitude_Minutes")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("Longitude_Seconds")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> &nbsp&nbsp<%#Eval("Longitude_E_W")%></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                         </tr>
                                                         
                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                POB(If any):
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("POB_DATE")%>                                                             
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>

                                                           <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Pilot Away:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("PILOT_AWAY_DATE")%>                                                             
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>

                                                           <tr class='leafTR'>
                                                            <td class='leafTD-header'>                                                                
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 40px; height: 20px">HSFO&nbsp;&nbsp;LSFO&nbsp;&nbsp;HSMGO&nbsp;&nbsp;LSMGO</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>

                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>   
                                                                Arrival ROB:                                                             
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("ARRIVAL_ROB_Fuel_Oil_HFO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("ARRIVAL_ROB_Fuel_Oil_LSFO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("ARRIVAL_Gas_Oil_MGO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> &nbsp&nbsp<%#Eval("ARRIVAL_Gas_Oil_LSMGO")%></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                         </tr>

                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Commenced Bunkering:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("COMMENCED_BUNKERING_DATE")%>                                                             
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>

                                                       

                                                             <tr class='leafTR'>
                                                            <td class='leafTD-header'>                                                                
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 40px; height: 20px">HFO&nbsp;&nbsp;LSFO&nbsp;&nbsp;MGO&nbsp;&nbsp;LSMGO</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>



                                          


                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>   
                                                                Quantity Bunkered:                                                             
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("QUANTITY_BUNKERED_Fuel_Oil_HFO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("QUANTITY_BUNKERED_Fuel_Oil_LSFO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("QUANTITY_BUNKERED_Gas_Oil_MGO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("QUANTITY_BUNKERED_Gas_Oil_LSMGO")%></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                         </tr>                                               

                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                DEP SBE:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("DEP_SBE_DATE")%>                                                             
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>

                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                All Cast OFF/Anchor Aweigh:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("ANCHOR_AWEIGH_DATE")%>                                                             
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>
                                                           <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Next Port:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("NEXT_PORT_NAME")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>

                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>                                                                
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td>Fwd(mtrs)</td>
                                                                        <td>Aft(mtrs)</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>

                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>     
                                                                DEP Draft:                                                           
                                                            </td>
                                                            <td class='leafTD-data'>
                                                               <%#Eval("DEP_DRAFT_FWD")%>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%#Eval("DEP_DRAFT_AFT")%></td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>                                                   
                                                    </table>
                                                </td>
                                                <td valign="top" style="border: solid 1px gray">
                                                    <table>   
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Report Date / Time:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("REPORT_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>                                                          
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                SBE:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("SBE_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'><td></td></tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>                                                                
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td>Fwd(mtrs)</td>
                                                                        <td>Aft(mtrs)</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Draft:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("DRAFT_FWD")%>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%#Eval("DRAFT_AFT")%></td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                         <tr class='leafTR'><td>&nbsp;</td></tr>
                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Anchored(FWE):
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("ANCHORED_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Berthed
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("BERTHED_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'><td>&nbsp;</td></tr>
                                                        <tr class='leafTR'><td>&nbsp;</td></tr>
                                                        <tr class='leafTR'><td></td></tr><tr class='leafTR'><td></td></tr>
                                                           <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Completed Bunkering:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("COMPLETED_BUNKERING_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>
                                                          <tr class='leafTR'>
                                                            <td class='leafTD-header'>                                                                
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 40px; height: 20px">HFO&nbsp;&nbsp;LSFO&nbsp;&nbsp;MGO&nbsp;&nbsp;LSMGO</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>
                                                           <tr class='leafTR'>
                                                            <td class='leafTD-header'>   
                                                                ROB(After Bunkers):                                                             
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("ROB_AFTER_BUNKERS_Fuel_Oil_HFO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("ROB_AFTER_BUNKERS_Fuel_Oil_LSFO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("ROB_Gas_Oil_MGO")%></td>
                                                                        <td class='leafTD-data' style="width: 10px"> <%#Eval("ROB_Gas_Oil_LSMGO")%></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                         </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                POB(If Any):
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("DEP_POB_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>

                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Pilot Away:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("PILOT_AWAY1_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>             
                                                        
                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                ETA:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("ETA_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>

                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>                                                                
                                                            </td>
                                                            <td >
                                                                <table>
                                                                    <tr>
                                                                        <td>Fwd(mtrs)</td>
                                                                        <td>Aft(mtrs)</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                          </tr>

                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Estimated Arr Draft:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("EST_ARR_DRAFT_FWD")%>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%#Eval("EST_ARR_DRAFT_AFT")%></td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>                                        
                                                    </table>
                                                </td>
                                                <td valign="top" style="border: solid 1px gray">
                                                    <table>
                                                    

                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Fresh Water:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("FRESH_WATER")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>

                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                RFA:
                                                            </td>
                                                            <td class='leafTD-data'>
                                                                <%#Eval("RFA_DATE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4" style="text-align: left; border: solid 1px gray">
                            Remarks:
                        </td>
                    </tr>
                    <tr>
                        <td style="border: solid 1px gray" colspan="4">
                            <table cellpadding="0" cellspacing="0" style="width: 99%">
                                <tr>
                                    <td style="width: 100%; height: 120px; background-color: #cce499">
                                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" BorderStyle="None" runat="server"
                                            ForeColor="Black" BackColor="#cce499" Width="100%"
                                            Height="100%"> </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
    </div> 
</asp:Content> 