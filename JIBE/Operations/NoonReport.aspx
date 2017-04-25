<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NoonReport.aspx.cs"
    Inherits="Operation_NoonReport" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            var wh = 'PKID=<%=Request.QueryString["id"]%>';

            Get_Record_Information_Details('OPS_Telegrams', wh);

        });

        function OpenCrewList(vcode) {

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="contentmain" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="Daily Noon Report"></asp:Label>
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
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: left; font-weight: bold; width: 20%">
                            Vessel Name:&nbsp;<asp:Label ID="lblVessel" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                            &nbsp; &nbsp; &nbsp;|&nbsp; &nbsp; &nbsp;
                            <asp:HyperLink ID="hplcrewlist" runat="server" Target="_blank"></asp:HyperLink>
                            <%--  <asp:LinkButton ID="lbtncrwlist" runat="server" Text="Crew List" OnClick="lbtncrwlist_Click"></asp:LinkButton>--%>
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
                                                <td valign="top" style="border: solid 1px gray">
                                                    <table cellspacing="1">
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Date:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("TelegramDate")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                UTC / GMT:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 35%; border-right: 1px solid white; padding-right: 2px">
                                                                            <%#Eval("UTC_TYPE")%>
                                                                            <%#Eval("UTC_hr")%>
                                                                        </td>
                                                                        <td style="width: 65%; padding-left: 2px">
                                                                            <asp:Label ID="lblUtchrs" runat="server" Width="100%"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Voyage Number:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("VOYAGE")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Port:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Port")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Location:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("location")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Ship's clock change:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Clocks_type")%>
                                                                &nbsp;
                                                                <%#Eval("Clocks_Hr")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
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
                                                                        <td style="width: 35px; text-align: center">
                                                                            Sec
                                                                        </td>
                                                                        <td style="width: 10px; text-align: center">
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                            <td style="width: 20px; height: 20px">
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
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Vessel Course:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Vessel_Course")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Deg
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Swell Direction:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Swell_Direction")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Swell Height:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Swell_Height")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Mtrs
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Wind Direction:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Wind_Direction")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Wind Force:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <asp:Label ID="lblwindforce" Text='<%#Eval("Wind_Force")%>' Height="100%" Width="100%"
                                                                    runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Sea Direction:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Sea_Direction")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Douglas Sea State:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Sea_Force")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Current Direction:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Current_Direction")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Speed of Current:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("Current_Speed")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Knts
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Air Temperature:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("AirTemp")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                DegC
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Air Pressure:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("AirPress")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Bar
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Sea Temp.:
                                                            </td>
                                                            <td class='leafTD-data-left'>
                                                                <%#Eval("SeaTemp")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                DegC
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td valign="top" style="border: solid 1px gray">
                                                    <table cellspacing="1">
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec' style="width: 180px">
                                                                Engine RPM:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("EngRPM")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Slip %S:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("Slip")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Average Speed:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <asp:Label ID="lblaveragespeed" Text='<%#Eval("AVERAGE_SPEED")%>' Height="100%" Width="100%"
                                                                    runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Knts
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Instructed Speed:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("INSTRUCTED_SPEED")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Knts
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Total Passage Time:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("PassDays")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Days
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("PassTime")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Hrs
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Passage time to go:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("PassToGo")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Hrs
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Dist since last report:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("DistSinceLastRep")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                N.miles
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Total passage distance:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("PassDist")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                N.miles
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Passage dist to go:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("PassDistToGo")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                N.miles
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Closest dist frm
                                                                <br />
                                                                storm center:
                                                            </td>
                                                            <td class='leafTD-data-midsec'>
                                                                <%#Eval("CDFrmStormCenter")%>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                N.miles
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Time of CPA:
                                                            </td>
                                                            <td colspan="2" style="text-align: left" class='leafTD-data-midsec'>
                                                                <%#Eval("CPATIME")%>
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                Next Port:
                                                            </td>
                                                            <td colspan="2" class='leafTD-data-midsec' style="text-align: left">
                                                                <%#Eval("NextPort")%>
                                                            </td>                                                    
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header-midsec'>
                                                                ETA Next Port
                                                            </td>
                                                            <td colspan="2" class='leafTD-data-midsec' style="text-align: left">
                                                                <%#Eval("ETANextPort")%>
                                                            </td>                                                     
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    
                                                        <tr>
                                                            <td style="text-align: left" colspan="4">
                                                                <div style="background-color: #99ccff; text-align: left; width: 100%; font-weight: bold;
                                                                    padding: 2px">
                                                                    Bilge Water / Sludge Qty :</div>
                                                                <asp:GridView ID="gvBS" runat="server" CellPadding="2" CellSpacing="0" DataSourceID="objDataSrc"
                                                                    CssClass="gvMain" HeaderStyle-CssClass="gvheader" RowStyle-CssClass="gvRows"
                                                                    GridLines="Both" HeaderStyle-BackColor="#99ccff" Font-Size="11px" ShowHeaderWhenEmpty="true"
                                                                    AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDetail_Type" runat="server" Text='<%#Eval("Detail_Type")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Daily Prod (MT)" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows"
                                                                            ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDaily_Production" runat="server" Text='<%#Eval("Daily_Production")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Qty (MT)" HeaderStyle-CssClass="gvheader" ItemStyle-CssClass="gvRows"
                                                                            ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotal_Qty" runat="server" Text='<%#Eval("Total_Qty")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tank Num" ItemStyle-Width="140px" HeaderStyle-CssClass="gvheader"
                                                                            ItemStyle-CssClass="gvRows">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTank_Number" runat="server" Text='<%#Eval("Tank_Number")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Current Filling Ratio (%)" HeaderStyle-CssClass="gvheader"
                                                                            ItemStyle-CssClass="gvRows" ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMax_Filling_Ratio" runat="server" Text='<%#Eval("Max_Filling_Ratio")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvheader"
                                                                            ItemStyle-CssClass="gvRows">
                                                                            <ItemTemplate>
                                                                                <asp:Image ID="imgremark" ImageUrl="~/Images/remark_new.gif" runat="server" Visible='<%# Convert.ToString(Eval("Remark")).Trim().Length>1?true:false %>'
                                                                                    onmouseover='<%# "js_ShowToolTip(&#39;"+Convert.ToString(Eval("Remark"))+"&#39;,event,this)" %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td valign="top" style="border: solid 1px gray">
                                                    <table cellspacing="1" cellpadding="0" border="0">
                                                        <tr class='leafTR' style="background-color: #99ccff; text-align: center;">
                                                            <td rowspan="2" style="background-color: White">
                                                            </td>
                                                            <td rowspan="2" style="width: 120px">
                                                                CP Figures / Actual
                                                            </td>
                                                            <td colspan="3">
                                                                Consumption
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
                                                            <td class='leafTD-header'>
                                                                Heavy Oil:
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblCPHO" runat="server" Text='<%#Eval("CPHO")%>'></asp:Label>
                                                                &nbsp; / &nbsp;<%#Eval("calCPHO")%><td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblME_HO" runat="server" Text='<%#Eval("ME_HOcons")%>' />
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblAE_HO" runat="server" Text='<%#Eval("AE_HOcons")%>' />
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblBlr_HO" runat="server" Text='<%#Eval("Blr_HOcons")%>' />
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblHO" runat="server" Text='<%#Eval("HO_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                MT
                                                            </td>
                                                        </tr>
                                                        

                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                LSFO:
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblCPLSFO" runat="server" Text='<%#Eval("cpLSFO")%>'></asp:Label>
                                                                &nbsp; / &nbsp;<%#Eval("CP_LSFOCons")%><td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblME_LSFO" runat="server" Text='<%#Eval("ME_Cons_LSFO")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblAE_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_Cons_LSFO")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblBlr_LSFO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Boiler_Cons_LSFO")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="Label5" Height="100%" Width="100%" runat="server" Text='<%#Eval("LSFO_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                MT
                                                            </td>
                                                        </tr>
                                                         <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                LSMGO:
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblCPLSMGO" runat="server" Text='<%#Eval("cpLSMGO")%>'></asp:Label>
                                                                &nbsp; / &nbsp;<%#Eval("CP_LSMGOCons")%></td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblME_LSMGO" runat="server" Text='<%#Eval("ME_Cons_LSMGO")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblAE_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_Cons_LSMGO")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="Label9" Height="100%" Width="100%" runat="server" Text='<%#Eval("Boiler_Cons_LSMGO")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblBlr_LSMGO" Height="100%" Width="100%" runat="server" Text='<%#Eval("LSMGO_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                MT
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                Diesel Oil:
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblCPDO" runat="server" Text='<%#Eval("CPDO")%>'></asp:Label>
                                                                &nbsp; / &nbsp;<%#Eval("CP_DOcons")%><td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblME_DO"  runat="server" Text='<%#Eval("ME_dOcons")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblAE_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_dOcons")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblBlr_DO" Height="100%" Width="100%" runat="server" Text='<%#Eval("Blr_dOcons")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblDO" Height="100%" Width="100%" runat="server" Text='<%#Eval("DO_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                MT
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                MECC:
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblME_MECC" Height="100%" Width="100%" runat="server" Text='<%#Eval("MECC_Cons")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblMECC" Height="100%" Width="100%" runat="server" Text='<%#Eval("MECC_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Ltrs
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                MECYL:
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblME_MECYL" Height="100%" Width="100%" runat="server" Text='<%#Eval("MECYL_Cons")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblMECYL" Height="100%" Width="100%" runat="server" Text='<%#Eval("MECYL_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Ltrs
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                AECC:
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="lblAE_AECC" Height="100%" Width="100%" runat="server" Text='<%#Eval("AECC_Cons")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="lblAECC" Height="100%" Width="100%" runat="server" Text='<%#Eval("AECC_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                Ltrs
                                                            </td>
                                                        </tr>
                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header'>
                                                                ROB of Cylinder Oil(LSMGO):
                                                            </td>
                                                            <td></td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="Label12" Height="100%" Width="100%" runat="server" Text='<%#Eval("ME_CylinderOil")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="Label13" Height="100%" Width="100%" runat="server" Text='<%#Eval("AE_CylinderOil")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp' style="width: 100px">
                                                                <asp:Label ID="Label14" Height="100%" Width="100%" runat="server" Text='<%#Eval("Blr_CylinderOil")%>'></asp:Label>
                                                            </td>
                                                            <td class='leafTD-data-consmp'>
                                                                <asp:Label ID="Label15" Height="100%" Width="100%" runat="server" Text='<%#Eval("ROB_CylinderOil")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px">
                                                                MT
                                                            </td>
                                                        </tr>


                                                        <tr class='leafTR'>
                                                            <td class='leafTD-header' rowspan="2">
                                                                Fresh Water:
                                                            </td>
                                                            <td class='leafTD-data-consmp' rowspan="2">
                                                                <asp:Label ID="lblCPFW" runat="server" Height="100%" Width="100%" Text='<%#Eval("cpfw")%>'></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <table cellpadding="0" cellspacing="1">
                                                                    <tr style="background-color: #99ccff; text-align: center;">
                                                                        <td>
                                                                            FW Production:
                                                                        </td>
                                                                        <td>
                                                                            FW Consumption:
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class='leafTD-data' style="text-align: center">
                                                                            <%#Eval("FW_PROD")%>
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: center">
                                                                            <asp:Label ID="lblFW_Cons" runat="server" Height="100%" Width="100%" Text='<%#Eval("FW_CONS")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class='leafTD-data-consmp' rowspan="2">
                                                                <asp:Label ID="lblFW" Height="100%" Width="100%" runat="server" Text='<%#Eval("FW_ROB")%>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20px; height: 20px" rowspan="2">
                                                                MT
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table cellpadding="0" style="padding: 20px 0px 0px 0px">
                                                        <tr>
                                                            <td valign="top">
                                                                <table cellpadding="0" cellspacing="1" style="background-color: White">
                                                                    <tr style="background-color: #99ccff; font-weight: bold">
                                                                        <td align="left" colspan="3" style="padding: 5px 0px 5px 10px">
                                                                            <div style="float: left">
                                                                                M/E Parameters
                                                                            </div>
                                                                            <div style="float: right; padding-right: 10px">
                                                                                <asp:HyperLink ID="hplMePowerCurve" runat="server" ForeColor="Blue" Text="Power Curve"
                                                                                    NavigateUrl='<%#"/"+System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString()+"/uploads/MEPowerCurve/" +  Convert.ToString(Eval("ME_Power_Curve"))%>'
                                                                                    Target="_blank"></asp:HyperLink>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class='leafTD-header'>
                                                                            Output HP :
                                                                        </td>
                                                                        <td class='leafTD-data'>
                                                                            <%#Eval("ME_HP")%>
                                                                        </td>
                                                                        <td>
                                                                            HP
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class='leafTD-header'>
                                                                            Output KW :
                                                                        </td>
                                                                        <td class='leafTD-data'>
                                                                            <%#Eval("ME_KW")%>
                                                                        </td>
                                                                        <td>
                                                                            KW
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class='leafTD-header'>
                                                                            MCR :
                                                                        </td>
                                                                        <td class='leafTD-data'>
                                                                            <%#Eval("ME_MCR")%>
                                                                        </td>
                                                                        <td>
                                                                            %
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="height: 20px; background-color: #99ccff; text-align: center;
                                                                            font-weight: bold; border-top: 3px solid white">
                                                                            Draft
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border-bottom: solid 1px white">
                                                                        <td style="height: 25px;">
                                                                            Fwd(mtrs):
                                                                        </td>
                                                                        <td style="width: 50px; height: 25px; background-color: #cce499; border-right: solid 1px white">
                                                                            <%#Eval("FwdDraft")%>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border-bottom: solid 1px white">
                                                                        <td style="height: 25px;">
                                                                            Mid(mtrs):
                                                                        </td>
                                                                        <td style="width: 50px; height: 25px; background-color: #cce499; border-right: solid 1px white">
                                                                            <%#Eval("MIDDRAFT")%>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="border-bottom: solid 1px white">
                                                                        <td style="height: 25px;">
                                                                            Aft(mtrs):
                                                                        </td>
                                                                        <td style="width: 50px; height: 25px; background-color: #cce499;">
                                                                            <%#Eval("AftDraft")%>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                     <tr style="border-bottom: solid 1px white">
                                                                        <td style="height: 25px;">
                                                                            Trim(mtrs):
                                                                        </td>
                                                                        <td style="width: 50px; height: 25px; background-color: #cce499;">
                                                                            <%#Eval("trim")%>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top" style="padding-left: 30px">
                                                                <table cellspacing="1" style="background-color: White">
                                                                    <tr>
                                                                        <td colspan="3" style="background-color: #99ccff; text-align: center; font-weight: bold;
                                                                            padding: 5px 0px 5px 10px">
                                                                            Running Hours since last noon
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Main Engine:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("RHRS_ME")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            hrs
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Aux Engine1:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("RHRS_AE1")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            hrs
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Aux Engine2:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("RHRS_AE2")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            hrs
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Aux Engine3:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("RHRS_AE3")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            hrs
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Aux Engine4:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("RHRS_AE4")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            hrs
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Shaft Genr:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("GM")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            hrs
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Aux Boiler:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("RHRS_BLR")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            hrs
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <table>
                                                                    <tr>
                                                                        <td style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 10px"
                                                                            colspan="3">
                                                                            Ballast
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header' style='width: 250px'>
                                                                            Permanent (incl heeling tanks 105% of one tank capacity) :
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("Permanent_Ballast")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            MT
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Additional :
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("Additional_Ballast")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            MT
                                                                        </td>
                                                                    </tr>
                                                                    <tr class='leafTR'>
                                                                        <td class='leafTD-header'>
                                                                            Total Ballast on Board:
                                                                        </td>
                                                                        <td class='leafTD-data' style="text-align: right">
                                                                            <%# Eval("Total_Ballast")%>
                                                                        </td>
                                                                        <td style="width: 12px">
                                                                            MT
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
                                                    <asp:TextBox ID="txtremark" Width="100%" Height="80px" runat="server" Text='<%#Eval("Remarks")%>'
                                                        TextMode="MultiLine" BorderStyle="None" ForeColor="Black" BackColor="#cce499"> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
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
