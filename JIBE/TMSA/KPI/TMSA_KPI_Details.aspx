<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TMSA_KPI_Details.aspx.cs" Inherits="TMSA_KPI_TMSA_KPI_Details" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script type="text/javascript">
        var __isResponse = 1;
        function Display() {
            try {
                var UserID = $('[id$=hdnUserID]').val();

                if (lastExecutor_ResetDashboard != null)
                    lastExecutor_ResetDashboard.abort();

                var service = Sys.Net.WebServiceProxy.invoke('../JiBeTMSAService.asmx', 'AsyncGet_KPIDetails', false, { "User_ID": UserID, "Layout": "" }, Onsuccess_DisplayKPI, Onfail, null);
                lastExecutor_ResetDashboard = service.get_executor();

            }
            catch (ex) { }
        }

        function Onsuccess_DisplayKPI() {

            var parts = data.split('~~KPI~~');
            if (parts[0].length > 0) {
                $("#dvKPIName").html(parts[0]);

                if (parts[0].toString() == "Releases of substances as def by MARPOL Annex 1-6")
                    img = "Resource/Releases_of_substances.png";
                if (parts[0].toString() == "Ballast water management violations")
                    img = "Resource/Ballast_water_management_violations.png";
                if (parts[0].toString() == "Contained spills")
                    img = "Resource/Contained spills.png";
                if (parts[0].toString() == "Environmental deficiencies")
                    img = "Resource/Environmental_deficiencies.png";
                if (parts[0].toString() == "Passenger Injury Ratio")
                    img = "Resource/PassengerInjury.png";
                if (parts[0].toString() == "Lost Time Injury Frequency")
                    img = "Resource/LostTimeInjury.png";
                if (parts[0].toString() == "Lost Time Sickness Frequency")
                    img = "Resource/LostTimeSickness.png";
                if (parts[0].toString() == "Health and Safety deficiencies")
                    img = "Resource/HealthSafetyDeficiencies.png";
                if (parts[0].toString() == "Port state control performance")
                    img = "Resource/PortStateControl.png";
                $("#imgChart").attr(src,img);
            }
            if (parts[1].length > 0) $("#dvKPIFormula").html(parts[0]);
            if (parts[2].length > 0) $("#dvDesc").html(parts[0]);
            if (parts[3].length > 0) $("#dvTimeFrame").html(parts[0]);
            if (parts[4].length > 0) $("#dvKPIGrid").html(parts[0]);
            if (parts[5].length > 0) $("#dvPI").html(parts[0]);

        }
    
    </script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
            <center>
                <div width="50%">
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr>
                            <td align="center" colspan="6">
                                <div style="border: 1px solid #cccccc" class="page-title">
                                    <asp:Literal ID="ltPageHeader" Text="KPI detail" runat="server"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <div id="dvKPIName" class='KPIheader'>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="dvKPIGrid">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" style="color: #FF0000;">
                                            <div id="dvKPIChart"><img id="imgChart"/>
                                            </div>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            KPI Formula :
                                        </td>
                                        <td>
                                            <div id="dvKPIFormula">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </div>
    </center>
</asp:Content>
