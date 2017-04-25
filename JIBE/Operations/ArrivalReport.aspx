<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ArrivalReport.aspx.cs"
    Inherits="Operation_ArrivalReport" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            var wh = 'PKID=<%=Request.QueryString["id"]%>';

            //   Get_Record_Information_Details('OPS_Telegrams', wh);

        });

         
    </script>

    <style type="text/css">
        .TBRemark
        {
            wrap:true;
            word-wrap: break-word;
                      
        }
    </style>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContent" runat="server">
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="Arrival Report"></asp:Label>
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
                        <td style="text-align: left; font-weight: bold; width: 20%">
                            Vessel Name:&nbsp;<asp:Label ID="lblVessel" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                            &nbsp; &nbsp; &nbsp;|&nbsp; &nbsp; &nbsp;
                            <asp:LinkButton ID="lbtncrwlist" runat="server" Text="Crew List" OnClick="lbtncrwlist_Click"></asp:LinkButton>
                        </td>
                        <td align="right" style="padding-right: 17px">
                            <uc1:ctlRecordNavigation ID="ctlRecordNavigationReport" OnNavigateRow="BindReport"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="dvArrivalReport" >
                                <style type="text/css">
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
                                        padding: 0px 0px 0px 10px;
                                        text-align: left;
                                    }
                                     .leafTD-data
                                    {
                                        width: 140px;
                                        height: 20px;
                                        padding: 0px 0px 0px 3px;
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
                                    
                                    .TbCellCon
                                    {
                                        width: 60px;
                                        height: 20px;
                                        padding: 0px 0px 0px 0px;
                                        background-color: #cce499;
                                        text-align: left;
                                    }
                                    .TbCellConD
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
                                   <asp:FormView ID="fvarrival" FooterStyle-ForeColor="Black" runat="server" OnDataBound="fvarrival_DataBound" OnItemCreated="fvarrival_ItemCreated"
                                    Width="100%">
                                    <ItemTemplate>
                                <table width="100%" >
                                    <tr>
                                        <td valign="top" style="border: solid 1px gray">
                                            <table>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Local Date/Time:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("TelegramDate")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                 <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        UTC / GMT:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("UTC_TYPE")%>
                                                        &nbsp;
                                                        <%#Eval("UTC_hr")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Voyage Number:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("VOYAGE")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Arrival Port Name:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("PortName")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>                                               
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Draft:
                                                    </td>
                                                    <td style="background-color: #cce499">
                                                        <table>
                                                            <tr style="border-bottom: solid 1px white">
                                                                <td style="width: 50px; height: 20px; background-color: #cce499">
                                                                    Fwd(mtrs)
                                                                </td>
                                                                <td style="width: 50px; height: 25px; background-color: #cce499">
                                                                    Mid(mtrs)
                                                                </td>
                                                                <td style="width: 50px; height: 20px; background-color: #cce499">
                                                                    Aft(mtrs)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50px; height: 20px; background-color: #cce499; border-right: solid 1px white">
                                                                    <%#Eval("FwdDraft")%>
                                                                </td>
                                                                <td style="width: 50px; height: 25px; background-color: #cce499; border-right: solid 1px white">
                                                                    <%#Eval("MIDDRAFT")%>
                                                                </td>
                                                                <td style="width: 50px; height: 20px; background-color: #cce499; border-right: solid 1px white">
                                                                    <%#Eval("AftDraft")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Displacement:
                                                    </td>
                                                    <td style="text-align: center" class='leafTD-data'>
                                                        <%#Eval("Displacement")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;MT
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Deadweight:
                                                    </td>
                                                    <td class='leafTD-data' style="text-align: center">
                                                        <%#Eval("dwt")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;MT
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Departure Port Name:
                                                    </td>
                                                    <td class='leafTD-data' style="text-align: center">
                                                        <%#Eval("DepPortName")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                 <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Charter Name:
                                                    </td>
                                                    <td class='leafTD-data' style="text-align: center">
                                                        <%#Eval("Charter_Name")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                 <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                      Total Distance:
                                                    </td>
                                                    <td class='leafTD-data' style="text-align: center">
                                                        <%#Eval("TotalDistance")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                 <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Type of Cargo:
                                                    </td>
                                                    <td class='leafTD-data' style="text-align: center">
                                                        <%#Eval("Type_Of_Cargo")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                 <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                       Qty. cargo onboard (mt):
                                                    </td>
                                                    <td class='leafTD-data' style="text-align: center">
                                                        <%#Eval("Qty_Cargo_Onboard_mt")%>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                       Course :
                                                    </td>
                                                      <td class='leafTD-data' style="text-align: center">
                                                        <%#Eval("Vessel_Course")%>
                                                    </td> 
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td style="background-color: #99ccff; text-align: left; " colspan="2">
                                                        Tugs usage number:
                                                    </td>
                                                    <td style="width: 20px; height: 20px ">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-data' colspan="2">
                                                       <div id="dvTugUsed" style=" width:320px; overflow-x: scroll;">
                                                        <%#Eval("TUG_USED")%>
                                                       </div>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td valign="top" style="border: solid 1px gray">
                                            <table>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                    </td>
                                                    <td style="width: 90px; height: 20px; background-color: #99ccff">
                                                        &nbsp;&nbsp;ROB
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Heavy Oil:
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblHO" Height="100%" Width="100%" runat="server" Text='<%#Eval("HO_ROB")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;MT
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Diesel Oil:
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblDO" Height="100%" Width="100%" runat="server" Text='<%#Eval("DO_ROB")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;MT
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        MECC:
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblMECC" Height="100%" Width="100%" runat="server" Text='<%#Eval("MECC_ROB")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;Ltrs
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        MECYL:
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblMECYL" Height="100%" Width="100%" runat="server" Text='<%#Eval("MECYL_ROB")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;Ltrs
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        AECC:
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblAECC" Height="100%" Width="100%" runat="server" Text='<%#Eval("AECC_ROB")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;Ltrs
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Fresh Water:
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblFW" Height="100%" Width="100%" runat="server" Text='<%#Eval("FW_ROB")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;MT
                                                    </td>
                                                </tr>
                                                  <tr class='leafTR'>
                                                    <td>
                                                      <br />
                                                    </td>
                                                    <td>
                                                        
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Steaming Hrs:
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblSteaming_hrs" Height="100%" Width="100%" runat="server" Text='<%#Eval("Steaming_hrs")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Total hrs in a day :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblTotal_Hrs_In_Day" Height="100%" Width="100%" runat="server" Text='<%#Eval("Total_Hrs_In_Day")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Ttl. steam hrs since COP :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblTtl_Steam_Hrs_Cop" Height="100%" Width="100%" runat="server" Text='<%#Eval("Ttl_Steam_Hrs_Cop")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Obs. Dist till EOP :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblObs_Dist_Till_EOP" Height="100%" Width="100%" runat="server" Text='<%#Eval("Obs_Dist_Till_EOP")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Total Dist since COP :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblSteaming_Dist_Till_COP" Height="100%" Width="100%" runat="server"
                                                            Text='<%#Eval("Steaming_Dist_Till_COP")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Av. FO Voyage cons :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblAV_FO_Voyage_Cons" Height="100%" Width="100%" runat="server" Text='<%#Eval("AV_FO_Voyage_Cons")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Vessel speed :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblVessel_Speed" Height="100%" Width="100%" runat="server" Text='<%#Eval("Vessel_Speed")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Av. Voyage Slip :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblAvg_Voy_Slip" Height="100%" Width="100%" runat="server" Text='<%#Eval("Avg_Voy_Slip")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Av. Voyage Speed :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="txtAvgVSpeed" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                 <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                       ME RPM :
                                                    </td>
                                                    <td style="width: 80px; text-align: right" class='leafTD-data'>
                                                        <asp:Label ID="lblEngRPM" Height="100%" Width="100%" runat="server" Text='<%#Eval("EngRPM")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 20px; height: 20px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td colspan="3">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table style="width: 100%; border: 1px solid gray; background-color: #efefef;" cellpadding="4">
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Are you bunkering in this port?
                                                                </td>
                                                                <td style="width: 40px">
                                                                    <asp:Label ID="Label1" Height="100%" Width="100%" runat="server" Text='<%#Eval("Bunkering_ThisPort")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    If bunkering, is your pre-Bunker plan ready?
                                                                </td>
                                                                <td style="width: 40px">
                                                                    <asp:Label ID="Label2" Height="100%" Width="100%" runat="server" Text='<%#Eval("PreBunkerPlan_Ready")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td valign="top" style="border: solid 1px gray">
                                            <table>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Anchored:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("Anchrd")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Bearth No.:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("Bearth_No")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                 <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        Anchorage No.:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("AnchrgNo")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header' style="vertical-align: top">
                                                        Reason For Anchoring:
                                                    </td>
                                                    <td colspan="2" style="height: 100px; vertical-align: top"class='leafTD-data'>
                                                         <asp:TextBox ID="TextBox2" runat="server" BackColor="#cce499" BorderStyle="None" ReadOnly="true"
                                                ForeColor="Black" Height="100%" Text='<%#Eval("ReasonsAnchg")%>'
                                                TextMode="MultiLine" Width="100%"> </asp:TextBox>
                                                    </td>
                                                     <%-- <div id="Div1" style=" width:320px; overflow-x: scroll;">
                                                        <%#Eval("TUG_USED")%>
                                                       </div>--%>                                             
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        ESP:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("ESP")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        NOR:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("NOR")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        ETB:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("ETB")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class='leafTD-header'>
                                                        ETD:
                                                    </td>
                                                    <td class='leafTD-data'>
                                                        <%#Eval("ETD")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class='leafTR'>
                                                    <td class="leafTD-header">
                                                        No. of Containers damaged since last Departure Reports:
                                                    </td>
                                                    <td class="leafTD-data">
                                                        <%#Eval("CntainrDmgdSinceDep")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leafTD-header">
                                                        Wind Force:
                                                    </td>
                                                    <td class="leafTD-data">
                                                        <%#Eval("Wind_Force")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leafTD-header">
                                                        Wind direction:
                                                    </td>
                                                    <td class="leafTD-data">
                                                        <%#Eval("Wind_Direction")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leafTD-header">
                                                        Wave Height:
                                                    </td>
                                                    <td class="leafTD-data">
                                                        <%#Eval("Wave_Height")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leafTD-header">
                                                        Wave direction:
                                                    </td>
                                                    <td class="leafTD-data">
                                                        <%#Eval("WAVE_DIRECTION")%>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class="leafTR">
                                                    <td class="leafTD-header">
                                                        Latitude:
                                                    </td>
                                                    <td class="leafTD-data-left">
                                                        <table cellspacing="0">
                                                            <tr>
                                                                <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                    <%#Eval("ltdeg")%>
                                                                </td>
                                                                <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                    <%#Eval("ltmint")%>
                                                                </td>
                                                                <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                    <%#Eval("ltsec")%>
                                                                </td>
                                                                <td style="width: 10px; text-align: center">
                                                                    <%#Eval("ltns") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                                <tr class="leafTR">
                                                    <td class="leafTD-header">
                                                        Longitude:
                                                    </td>
                                                    <td class="leafTD-data-left">
                                                        <table cellspacing="0">
                                                            <tr>
                                                                <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                    <%#Eval("lgdeg")%>
                                                                </td>
                                                                <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                    <%#Eval("lgmint")%>
                                                                </td>
                                                                <td style="border-right: solid 1px white; width: 35px; text-align: center">
                                                                    <%#Eval("lgsec")%>
                                                                </td>
                                                                <td style="width: 10px; text-align: center">
                                                                    <%#Eval("lgew") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 155px; height: 20px">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" style="border-spacing: 3px;border-collapse: separate;">
                                                <tr>
                                                    <td>
                                                        FO cons (Noon-EOP)
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        &nbsp;&nbsp;HSFO&nbsp;&nbsp;
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        &nbsp;&nbsp;LSFO&nbsp;&nbsp;
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        &nbsp;&nbsp;MGO/DO&nbsp;&nbsp;
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        &nbsp;&nbsp;LSMGO&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ME Consumption
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtME_Cons_HSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ME_Cons_HSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtME_Cons_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ME_Cons_LSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtME_Cons_MGO_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ME_Cons_MGO_DO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtME_Cons_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ME_Cons_LSMGO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        AE Consumption
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtAE_Cons_HSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_Cons_HSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtAE_Cons_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_Cons_LSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtAE_Cons_MGO_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_Cons_MGO_DO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtAE_Cons_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_Cons_LSMGO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Boiler Consumption
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtBoiler_Cons_HSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Boiler_Cons_HSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtBoiler_Cons_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Boiler_Cons_LSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtBoiler_Cons_MGO_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Boiler_Cons_MGO_DO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtBoiler_Cons_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Boiler_Cons_LSMGO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Incinerator Cons
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtInc_Cons_HSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Incinerator_Cons_HSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtInc_Cons_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Incinerator_Cons_LSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtInc_Cons_MGO_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Incinerator_Cons_MGO_DO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtInc_Cons_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Incinerator_Cons_LSMGO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Total Consumption
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Cons_HSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Cons_LSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Cons_MGO_DO" Height="100%" Width="100%" runat="server" ></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Cons_LSMGO" Height="100%" Width="100%" runat="server" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ME Cons/24 hrsn
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtME_Cons_Per24_HSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtME_Cons_Per24_LSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtME_Cons_Per24_MGO_DO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtME_Cons_Per24_LSMGO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        AE Cons/24 hrs
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtAE_Cons_Per24_HSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtAE_Cons_Per24_LSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtAE_Cons_Per24_MGO_DO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtAE_Cons_Per24_LSMGO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Total Voyage.Cons
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Manov_Cons_HSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Manov_Cons_LSFO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Manov_Cons_MGO_DO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 20px; width: 60px; background-color: #99ccff">
                                                        <asp:Label ID="txtTotal_Manov_Cons_LSMGO" Height="100%" Width="100%" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ROB At COP Dep. Port
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_DEP_Port_HSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_DEP_PORT_HSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_DEP_Port_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_DEP_PORT_LSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_DEP_Port_MGO_DO" Height="100%" Width="100%" runat="server"
                                                            Text='<%#Eval("ROB_DEP_PORT_MGO_DO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_DEP_Port_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_DEP_PORT_LSMGO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ROB At EOP
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_EOP_HSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_EOP_HSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_EOP_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_EOP_LSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_EOP_MGO_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_EOP_MGODO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_EOP_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_EOP_LSMGO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        ROB At FWE
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_FWE_HSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_FWE_HSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_FWE_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_FWE_LSFO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="txtROB_FWE_MGO_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_FWE_MGODO")%>'></asp:Label>
                                                    </td>
                                                    <td class="TbCellCon">
                                                        <asp:Label ID="Label3" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_FWE_LSMGO")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="vertical-align:top">
                                            <table width="100%" style="border-spacing: 3px;border-collapse: separate;padding-left:2px">
                                            <tr>
                                           <td colspan="2" style="background-color: #99ccff;">
                                           Local Date And Time
                                           </td> 
                                            </tr>
                                                
                                                <tr>
                                                    <td class='leafTD-header'>
                                                        POB:
                                                    </td>
                                                    <td class='TbCellConD'>
                                                        <%#Eval("POB_D")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='leafTD-header'>
                                                        Piolet Away :
                                                    </td>
                                                    <td class='TbCellConD'>
                                                        <%#Eval("Pilot_Away_D")%>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class='leafTD-header'>
                                                        First line ashore :
                                                    </td>
                                                    <td class='TbCellConD'>
                                                        <%#Eval("First_Line_Ashore_D")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='leafTD-header'>
                                                        All Made Fast :
                                                    </td>
                                                    <td class='TbCellConD'>
                                                        <%#Eval("AllMadeFast_D")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='leafTD-header'>
                                                        FWE :
                                                    </td>
                                                    <td class='TbCellConD'>
                                                        <%#Eval("FWE_D")%>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="border: solid 1px gray">
                                            <table cellpadding="0" cellspacing="0" style="width: 99%">
                                                <tr>
                                                    <td style="width: 100%; height: 120px; background-color: #cce499">
                                                        <asp:TextBox ID="txtremark" runat="server" BackColor="#cce499" BorderStyle="None"
                                                            ForeColor="Black" Height="100%" Text='<%#Eval("Remarks")%>' TextMode="MultiLine"
                                                            Width="100%" CssClass= "TBRemark"> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="border: solid 1px gray; background-color: #cce499; height: 120px;">
                                            <asp:TextBox ID="TextBox1" runat="server" BackColor="#cce499" BorderStyle="None"
                                                ForeColor="Black" Height="100%" Text='<%#Eval("CntainrDmgdSinceDep_Remarks")%>'
                                                TextMode="MultiLine" Width="100%"> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: left; border: solid 1px gray">
                                            Remark:
                                        </td>
                                        <td style="text-align: left; border: solid 1px gray">
                                            Reasons for damage to Containers:
                                        </td>
                                    </tr>
                                </table>
                                   </ItemTemplate>
                                </asp:FormView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
            background-color: #FDFDFD">
        </div>
    </div>
</asp:Content>
