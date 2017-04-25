<%@ Page Title="Port Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PortReport.aspx.cs" Inherits="Operations_PortReport" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">

        //        $(document).ready(function () {

        //            var wh = 'PKID=<%=Request.QueryString["id"]%>';

        //       //     Get_Record_Information_Details('OPS_Telegrams', wh);

        //        });

        //        function OpenCrewList(vcode) {

        //            return false;
        //        }
    </script>
</asp:Content>
<asp:Content ID="contentmain" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="Port Report"></asp:Label>
    </div>
    <div id="page-content" style="min-height: 640px; color: #333333; border: 1px solid gray;
        width: 100%">
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
                <table width="100%" cellpadding="0" cellspacing="0" style="font-family:Tahoma">
                    <tr>
                        <td colspan="2">
                            <div id="dvportReport">
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
                                        padding: 0px 0px 0px 3px;
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
                                <table>
                                <tr>
                                <td width="90%"></td>
                              <td align="right" style="padding-right: 17px">
                                 <uc1:ctlRecordNavigation ID="ctlRecordNavigationReport" OnNavigateRow="BindReportDr" style="width:10%"
                                runat="server" />
                                </td>
                                </tr>
                                </table>
                                    
                              
                           
                                <asp:FormView ID="fvportreport" runat="server" OnDataBound="fvportreport_DataBound" BorderColor="Black" BorderWidth="1px" 
                                    OnItemCreated="fvportreport_ItemCreated">
                                    <ItemTemplate>
                                        <table width="100%" style="border-spacing: 3px;border-collapse: separate;">
                                            <tr class='leafTR'>
                                                <td class='leafTD-header'>
                                                    Vessel:
                                                </td>
                                                <td class='leafTD-data-left'>
                                                       <%# Eval("Vessel_Name")%>
                                                  
                                                </td>
                                                <td class='leafTD-header'>
                                                    Date:
                                                </td>
                                                <td class='leafTD-header'>
                                                    GMT
                                                </td>
                                                <td class='leafTD-header'>
                                                    Local Time
                                                </td>
                                                <td class='leafTD-header'>
                                                    Call Sign
                                                </td>
                                            </tr>
                                            <tr class='leafTR'>
                                                <td class='leafTD-header'>
                                                    IMO:
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%# Eval("IMO")%>
                                                </td>
                                                <td class='leafTD-data-left' style="text-align: left">
                                              
                                                   <%# Convert.ToDateTime(Eval("Date")).ToString("dd/MM/yyyy")%>
                                                </td>
                                                <td class='leafTD-data-left' style="text-align: left">
                                                    <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 35%; border-right: 1px solid white; padding-right: 2px;white-space:nowrap">
                                                                            <%#Eval("UTC_TYPE")%>
                                                                           
                                                                        </td>
                                                                        <td style="width: 65%; padding-left: 2px;white-space:nowrap">
                                                                         <%#Eval("UTC_hr")%>
                                                                           <%-- <asp:Label ID="lblUtchrs" runat="server" Width="100%"></asp:Label>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                </td>
                                                <td class='leafTD-data-left' style="text-align: left">
                                                    <%# Convert.ToDateTime(Eval("Date")).ToString("hh:mm")%>
                                                </td>
                                                <td class='leafTD-data-left' style="text-align: left">
                                                    <%# Eval("Call_Sign")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="border-spacing: 3px;border-collapse: separate;">
                                            <tr>
                                                <td class='leafTD-data-left' style="background-color: #99ccff; width: 100%; text-align: center;
                                                    font-weight: bold; font-size: 9pt" colspan="6">
                                                    PORT INFORMATION
                                                </td>
                                            </tr>
                                            <tr class='leafTR'>
                                                <td class='leafTD-header'  style="width:110px">
                                                    Port
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Port_Name")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Voyage Number:
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Voyage_No")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Charter Name:
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("CharterName")%>
                                                </td>
                                            </tr>
                                            <tr class='leafTR'>
                                                <td class='leafTD-header' style="width:110px">
                                                    Berth no
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("BerthNo")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Total qty. to load/Dis
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Total_Qty_To_LoadDis")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Agent details
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("AgentDetails")%>
                                                </td>
                                            </tr>
                                            <tr class='leafTR'>
                                                <td class='leafTD-header' style="width:110px">
                                                    Type Of Cargo
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("TypeOfCargo")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Est.Dep. Draft Fwd/Aft
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Est_Dep_Draft_Fwd_Aft")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Sea water temp
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Sea_Water_Temp")%>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="border-spacing: 3px;border-collapse: separate;">
                                            <tr>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;width:100px"
                                                    colspan="5" rowspan="1">
                                                   OPERATIONS
                                                </td>
                                                
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size:  9pt"
                                                    colspan="1" rowspan="1">
                                                    Non Weather Stoppages/Reason
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header' style="width:155px">
                                                    Draft Fwd/Aft
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Draft_Fwd")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Draft_Aft")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Total qty dis/load
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Total_Qty_To_DisLoad")%>
                                                </td>
                                                <td colspan="3" width="35%" rowspan="4" style="vertical-align:top;text-align:right">
                                                <div style="height: 100px; overflow: auto">
                                                <asp:GridView ID="gvNonRain" runat="server" CellPadding="2" CellSpacing="0"  
                                                                    CssClass="gvMain" HeaderStyle-CssClass="gvheader" RowStyle-CssClass="gvRows" Width="417px"
                                                                    GridLines="Both" HeaderStyle-BackColor="#99ccff" Font-Size="11px" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found!"
                                                                    AutoGenerateColumns="false">
                                                                     <Columns>
                                                                        <asp:TemplateField HeaderText="Reason" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDetail_Type" runat="server" Text='<%# Eval("StoppageReason")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="From Date" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows"
                                                                            ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDaily_Production" runat="server" Text='<%# Convert.ToDateTime(Eval("FromDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="To Date" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows"
                                                                            ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotal_Qty" runat="server" Text='<%# Convert.ToDateTime(Eval("ToDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header' style="width:110px">
                                                    Comm load/dis
                                                </td>
                                                <td class='leafTD-data-left' colspan="2">
                                                    <%# Eval("Comm_Load_Dis")==DBNull.Value?"":Convert.ToDateTime(Eval("Comm_Load_Dis")).ToString("dd/MM/yyyy")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Qty load/dis (24 hrs)
                                                </td>
                                                 
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Qty_Load_Dis_24Hrs")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header' style="width:110px">
                                                    Comp load/dis
                                                </td>
                                                <td class='leafTD-data-left' colspan="2">
                                                    <%# Eval("Comp_Load_Dis") == DBNull.Value ? "" : Convert.ToDateTime(Eval("Comp_Load_Dis")).ToString("dd/MM/yyyy")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    Qty Remaining
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Qty_Remaining")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header' style="width:110px">
                                                    ETD
                                                </td>
                                                <td class='leafTD-data-left' colspan="2">
                                                    <%# Eval("ETD") == DBNull.Value ? "" : Convert.ToDateTime(Eval("ETD")).ToString("dd/MM/yyyy")%>
                                                </td>
                                                <td class='leafTD-header'>
                                                    No of Gangs worked
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("No_Of_Ganges")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header'>
                                                </td>
                                                <td  colspan="2">
                                                </td>
                                                <td class='leafTD-header'>
                                                    No of Cranes working
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("No_Of_Cranes_Working")%>
                                                </td>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt"
                                                    colspan="3" rowspan="1">
                                                    Weather Stoppages/Reason
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                 
                                                <td style="vertical-align:top">
                                                   <div style="height: 100px; overflow: auto">
                                                   <asp:GridView ID="gvRain" runat="server" CellPadding="2" CellSpacing="0"  
                                                                    CssClass="gvMain" HeaderStyle-CssClass="gvheader" RowStyle-CssClass="gvRows" Width="417px"
                                                                    GridLines="Both" HeaderStyle-BackColor="#99ccff" Font-Size="11px" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found!"
                                                                    AutoGenerateColumns="false">
                                                                     <Columns>
                                                                        <asp:TemplateField HeaderText="Reason" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDetail_Type" runat="server" Text='<%# Eval("StoppageReason")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="From Date" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows"
                                                                            ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDaily_Production" runat="server" Text='<%# Convert.ToDateTime(Eval("FromDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="To Date" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows"
                                                                            ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotal_Qty" runat="server" Text='<%# Convert.ToDateTime(Eval("ToDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        </Columns>
                                                    </asp:GridView>
                                                   </div>
                                                     
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="border-spacing: 3px;border-collapse: separate;">
                                            <tr>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                    width: 233px" rowspan="1">
                                                    Fo Consumption
                                                </td>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                    width: 100px;" rowspan="1">
                                                    HSFO
                                                </td>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                    width: 100px;" rowspan="1">
                                                    LSFO
                                                </td>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                    width: 100px;" rowspan="1">
                                                    MGO/DO
                                                </td>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                    width: 100px;" rowspan="1">
                                                    LSMGO
                                                </td>
                                                <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;white-space:nowrap"
                                                    rowspan="1">
                                                    STORES/SPARES/SERVICES/CREW CHANGE /SURVEY
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header'>
                                                    AE Consumption
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("AE_Consumption_HSFO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("AE_Consumption_LSFO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("AE_Consumption_MGODO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("AE_Consumption_LSMGO")%>
                                                </td>
                                                <td class='leafTD-data-left' rowspan="6" style="text-align:left;vertical-align: top;">
                                                    <%#Eval("Port_Activity_Detail")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header'>
                                                    Boiler Consumption
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Boiler_Consumption_HSFO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Boiler_Consumption_LSFO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Boiler_Consumption_MGODO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Boiler_Consumption_LSMGO")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header' style="white-space: nowrap">
                                                    Incinerator Consumption
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Incinerator_Consumption_HSFO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Incinerator_Consumption_LSFO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Incinerator_Consumption_MGODO")%>
                                                </td>
                                                <td class='leafTD-data-left'>
                                                    <%#Eval("Incinerator_Consumption_LSMGO")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header'>
                                                    Total Consumption
                                                </td>
                                                <td class='leafTD-data-left'>

                                                <asp:Label ID="Total_Consumption_HSFO" runat="server" Text=' <%#Eval("Total_Consumption_HSFO")%>' Width="100%"></asp:Label>
                                                  
                                                </td>
                                                <td class='leafTD-data-left'>
                                                 <asp:Label ID="Total_Consumption_LSFO" runat="server" Text=' <%#Eval("Total_Consumption_LSFO")%>' Width="100%"></asp:Label>
                                                    
                                                </td>
                                                <td class='leafTD-data-left'>
                                                 <asp:Label ID="Total_Consumption_MGODO" runat="server" Text=' <%#Eval("Total_Consumption_MGODO")%>' Width="100%"></asp:Label>
                                                  
                                                </td>
                                                <td class='leafTD-data-left'>
                                                 <asp:Label ID="Total_Consumption_LSMGO" runat="server" Text=' <%#Eval("Total_Consumption_LSMGO")%>' Width="100%"></asp:Label>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header'>
                                                    Bunker Received
                                                </td>
                                                <td class='leafTD-data-left'>
                                                   <asp:Label ID="Bunker_Received_HSFO" runat="server" Text=' <%#Eval("Bunker_Received_HSFO")%>' Width="100%"></asp:Label>
                                                   
                                                </td>
                                                <td class='leafTD-data-left'>
                                                   <asp:Label ID="Bunker_Received_LSFO" runat="server" Text=' <%#Eval("Bunker_Received_LSFO")%>' Width="100%"></asp:Label>
                                                   
                                                </td>
                                                <td class='leafTD-data-left'>
                                                   <asp:Label ID="Bunker_Received_MGODO" runat="server" Text=' <%#Eval("Bunker_Received_MGODO")%>' Width="100%"></asp:Label>
                                                  
                                                </td>
                                                <td class='leafTD-data-left'>
                                                   <asp:Label ID="Bunker_Received_LSMGO" runat="server" Text=' <%#Eval("Bunker_Received_LSMGO")%>' Width="100%"></asp:Label>
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='leafTD-header'>
                                                    ROB
                                                </td>
                                                <td class='leafTD-data-left'>
                                                <asp:Label ID="ROB_HSFO" runat="server" Text=' <%#Eval("ROB_HSFO")%>' Width="100%"></asp:Label>
                                                    
                                                </td>
                                                <td class='leafTD-data-left'>
                                                <asp:Label ID="ROB_LSFO" runat="server" Text=' <%#Eval("ROB_LSFO")%>' Width="100%"></asp:Label>
                                                    
                                                </td>
                                                <td class='leafTD-data-left'>
                                                <asp:Label ID="ROB_MGODO" runat="server" Text=' <%#Eval("ROB_MGODO")%>' Width="100%"></asp:Label>
                                                  
                                                </td>
                                                <td class='leafTD-data-left'>
                                                <asp:Label ID="ROB_LSMGO" runat="server" Text=' <%#Eval("ROB_LSMGO")%>' Width="100%"></asp:Label>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <table width="100%" style="border-spacing: 3px;border-collapse: separate;">
                                                <tr>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="2">
                                                        AE Running Hours
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="1">
                                                        AE #1
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="1">
                                                        AE #2
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="1">
                                                        AE #3
                                                    </td>
                                                    <td style="width:20px"></td>
                                                    <td colspan="1" rowspan="4" style="height:100%">
                                                    <table style="height:100px;width:100%">
                                                    <tr style="height:25%">
                                                    <td style="width:20%;background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;">
                                                     Remark
                                                    </td>
                                                    <td></td>
                                                    </tr>
                                                    <tr>
                                                    <td class='leafTD-data-left' rowspan="4"  colspan="2" style="text-align:left;vertical-align: top;"> <%#Eval("Remarks")%>
                                                    </td>
                                                    
                                                    </tr>
                                                    </table>
                                                    </td>
                                                <%--<td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 50px" rowspan="2">
                                                        Remark
                                                    </td>--%>
                                                  

                                                  <%--  <td class='leafTD-data-left' rowspan="4" colspan="1" style="width: 40%">
                                                        <%#Eval("Remarks")%>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td class='leafTD-data-left'>
                                                        <%#Eval("AE_Running_Hrs_1")%>
                                                    </td>
                                                    <td class='leafTD-data-left'>
                                                        <%#Eval("AE_Running_Hrs_2")%>
                                                    </td>
                                                    <td class='leafTD-data-left'>
                                                        <%#Eval("AE_Running_Hrs_3")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="2">
                                                        Fresh Water
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="1">
                                                        Recv.
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="1">
                                                        Con.
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="1">
                                                        Rob.
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class='leafTD-data-left'>
                                                        <%#Eval("Fresh_Water_Recv")%>
                                                    </td>
                                                    <td class='leafTD-data-left'>
                                                        <%#Eval("Fresh_Water_Con")%>
                                                    </td>
                                                    <td class='leafTD-data-left'>
                                                        <%#Eval("Fresh_Water_ROB")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="2">
                                                        TC Consuption
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 233px" rowspan="2">
                                                        With Gears
                                                    </td>
                                                    <td  colspan="2">
                                                    <asp:Label ID="TC_Consumption_WithGears" Text='<%#Eval("TC_Consumption_WithGears")%>' runat="server" Width="100px" class='leafTD-data-left'></asp:Label>
                                                    <%--    <%#Eval("TC_Consumption_WithGears")%>--%>
                                                    </td>
                                                    <td style="background-color: #99ccff; text-align: center; font-weight: bold; font-size: 9pt;
                                                        width: 100px" rowspan="2">
                                                        IDLE
                                                    </td>
                                                    <td   style="width: 40%;text-align:left">
                                                     <asp:Label ID="TC_Consumption_IDLE" Text='<%#Eval("TC_Consumption_IDLE")%>' runat="server" Width="100px" class='leafTD-data-left'></asp:Label>
                                                      <%--  <%#Eval("TC_Consumption_IDLE")%>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </table>
                                        <%--<table width="99%">
                                            <tr>
                                                <td style="text-align: left">
                                                    Remarks:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="text-align: left; background-color: #cce499">
                                                    <asp:TextBox ID="txtremark" Width="100%" Height="80px" runat="server" Text='<%#Eval("Remarks")%>'
                                                        TextMode="MultiLine" BorderStyle="None" ForeColor="Black" BackColor="#cce499"> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </ItemTemplate>
                                </asp:FormView>
                            </div>
                            <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
                                background-color: #FDFDFD">
                            </div>
                            <asp:ObjectDataSource ID="objDataSrc" runat="server" SelectMethod="Get_Bilge_Water_Sludge"
                                TypeName="SMS.Business.Operation.BLL_OPS_VoyageReports">
                                <SelectParameters>
                                    <asp:QueryStringParameter QueryStringField="id" Name="PkID" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
