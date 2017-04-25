<%@ Page Title="Voyage Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="VoyageReportDetails.aspx.cs" Inherits="Operations_VoyageReportDetails" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">

        function showdetails(ReportType, pkid) {
            var query = new Array();

            if (ReportType == 'A') {
                $('#dvVoyageReport').load("ArrivalReport.aspx?id=" + pkid + "&rnd=" + Math.random() + ' #dvArrivalReport');
            }
            if (ReportType == 'D') {
                $('#dvVoyageReport').load("DepartureReport.aspx?id=" + pkid + "&rnd=" + Math.random() + ' #dvDepartureReport');
            }
            if (ReportType == 'N') {
                $('#dvVoyageReport').load("NoonReport.aspx?id=" + pkid + "&rnd=" + Math.random() + ' #dvNoonReport');
            }
            if (ReportType == 'P') {
                $('#dvVoyageReport').load("PurpleReport.aspx?id=" + pkid + "&rnd=" + Math.random() + ' #dvPurpleReport');
            }
        }

        function LoadRecordInfo(pkid) {
            var wh = 'PKID=' + pkid;

            Get_Record_Information_Details('OPS_Telegrams', wh);
        }
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <asp:UpdatePanel ID="updMain" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <asp:Label ID="lblPageTitle" runat="server" Text="Departure Report"></asp:Label>
            </div>
            <div id="page-content" style="min-height: 640px; color: #333333; border: 1px solid gray;
                width: 100%">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: left; font-weight: bold; width: 40%">
                            Vessel Name:&nbsp;<asp:Label ID="lblVessel" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                            &nbsp; &nbsp; &nbsp;|&nbsp; &nbsp; &nbsp;
                            <asp:HyperLink ID="hplcrewlist" runat="server" Target="_blank" Text="Crew List"></asp:HyperLink>
                        </td>
                        <td align="right" style="padding-right: 17px">
                            <uc1:ctlRecordNavigation ID="ctlRecordNavigationReport" OnNavigateRow="BindReport"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="dvVoyageReport">
                                <span style="font-size: 14px; font-weight: bold; color: Green">Loading Report...... </span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
                                background-color: #FDFDFD">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
