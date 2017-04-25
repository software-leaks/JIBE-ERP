<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewJob.aspx.cs"
    Inherits="Technical_Job_List_ViewJob" Title="Job Details" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../Styles/jquery.galleryview-3.0-dev.css" />
    <script type="text/javascript" src="../../Scripts/jQueryRotate.2.2.js"></script>
    <!-- Second, add the Timer and Easing plugins -->
    <script type="text/javascript" src="../../Scripts/jquery.timers-1.2.js"></script>
    <script type="text/javascript" src="../../Scripts//jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.galleryview-3.0-dev.js"></script>
   
    <style type="text/css">
        @media print
        {
            body
            {
                color: black;
                font-family: Tahoma;
                font-size: 11px;
            }
            .header
            {
                display: none;
            }
            .printable
            {
                display: block;
                border: 0;
            }
            .printable table
            {
                display: block;
                border: 0;
            }
            .non-printable
            {
                display: none;
            }
            #pageTitle
            {
                border: 0;
            }
        }
    </style>
    <style type="text/css">
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 12px;
        }
        select
        {
            font-size: 10px;
            height: 21px;
        }
        a
        {
            text-decoration: none;
        }
        .link
        {
            text-decoration: none;
            text-transform: capitalize;
        }
        .roundedBox
        {
            border-radius: 5px;
            border: 2px solid white;
            background-color: #DBDFD5;
            text-align: center;
            font-size: 14px;
            color: #555;
            margin: 2px;
            padding: 2px;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .data
        {
            border: 1px solid #efefef;
            background-color: #F5F6CE;
        }
        .row-header
        {
            background-color: #aabbdd;
            font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function loadImageRotate() {


            $(".gv_navRotate").click(function () {

                $('.gv_panel').css("background-color", "red");
                var img = $('.gv_panel').find("img"),
                   len = img.length;
                if (len > 0) {
                    var path = img.first().attr("src");
                    asycRoatate_Image(path);
                } else {

                }

            });
        }


        $(document).ready(function () {
            $(".draggable").draggable();

            $('#myGallery').galleryView();
            $('#myGallery2').galleryView({ panel_width: 1000, panel_height: 800 });

            $('.rotate-image').rotate({ bind: { click: function () { var value = 0; if (isNaN($(this).attr('angle'))) $(this).attr('angle', '90'); else value = eval($(this).attr('angle')); value += 90; $(this).attr('angle', value); $(this).rotate({ animateTo: value }) } } });



            var outS = "", v = "";
            var inS = $(".SentanceCase").text();
            var arr = inS.split(".");
            var lastExecutor = null;
            for (var i = 0; i < arr.length; i++) {
                el = arr[i].trim();
                el = el.charAt(0).toUpperCase() + el.substr(1).toLowerCase();
                if (i > 0)
                    outS += '. ';
                outS += el;
            }
            $(".SentanceCase").text(outS);
            loadImageRotate();

            var wh = 'OFFICE_ID=<%=Request.QueryString["OFFID"]%>  and ID=<%=Request.QueryString["WLID"]%> and VESSEL_ID=<%=Request.QueryString["VID"]%>'
            Get_Record_Information_Details('TEC_DTL_JOBS_HISTORY', wh);

        });
        var lastExecutor = null;


        function asycRoatate_Image(pPath) {



            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'asycRoatate_Image', false, { "path": pPath }, onSuccess_asycRoatate_Image, Onfail, new Array(''));

            lastExecutor = service.get_executor();

        }


        function onSuccess_asycRoatate_Image(retVal, Args) {


        }
        function Onfail(msg) {


        }

        function OpenFollowupDiv() {

            showModal('dvAddFollowUp');
            $("#<%=txtMessage.ClientID%>").val(''); // To clear fields after adding follow up.
        }
        function CloseFollowupDiv() {

            var dvAddFollowUp = document.getElementById("dvAddFollowUp");
            dvAddFollowUp.style.display = 'none';

        }

        function AddNewMaintenanceFeedback(crewid, vid, jid, wlid, offid) {

            $('#dvPopupFrame').attr("Title", "Add New Maintenance Feedback");
            $('#dvPopupFrame').css({ "width": "500px", "height": "400px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

            var URL = "../../Crew/CrewDetails_MaintenanceFeedback.aspx?CrewID=" + crewid + "&VID=" + vid + "&WLID=" + wlid + "&OFFID=" + offid + "&JID=" + jid + "&Mode=ADD&rnd=" + Math.random();

            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);

        }

        function OpenWorkListCrewInvolved(vid, wlid, offid) {

            $('#dvPopupFrame').attr("Title", "Add/view crew involved in this job");
            $('#dvPopupFrame').css({ "width": "900px", "height": "800px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

            var URL = "../../Technical/Worklist/Worklist_Crew_Involved.aspx?VID=" + vid + "&WLID=" + wlid + "&OFFID=" + offid + "&Mode=ADD&rnd=" + Math.random();

            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);
        }


        String.prototype.toProperCase = function () {
            return this.replace(/\w\S*(\w*)/g, function (txt) {
                return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
            });
        };

        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');

        }

    </script>
    <script type="text/javascript">
        var _prevControl = null;
        var _PrevImg = null;
        function PlayAudio(actl) {

            var ctlaudio = actl.getElementsByTagName("audio");
            var CurrentctlImg = actl.getElementsByTagName("img")[0];


            currentctlid = ctlaudio[0];

            if (_prevControl != null && _prevControl != currentctlid) {
                _PrevImg.src = "../../images/AquaPlayicon.png";
                CurrentctlImg.src = "../../images/AquaPauseicon.png";

                _prevControl.pause();
                currentctlid.play();
            }
            else if (_prevControl == currentctlid) {

                if (_prevControl.paused == false) {
                    _prevControl.pause();
                    _PrevImg.src = "../../images/AquaPlayicon.png";
                }
                else {
                    currentctlid.play();
                    CurrentctlImg.src = "../../images/AquaPauseicon.png";

                }
            }
            else {
                currentctlid.play();
                CurrentctlImg.src = "../../images/AquaPauseicon.png";
            }

            _prevControl = currentctlid;
            _PrevImg = CurrentctlImg;

        }
        function DocOpen(docpath) {
            window.open(docpath);
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="roundedBox">
                <table border="0" cellpadding="5" cellspacing="0" style="width: 100%; color: black;"
                    class="printable">
                    <tr>
                        <td align="left" valign="top" style="font-size: 14px">
                            <table>
                                <tr>
                                    <td>
                                        Vessel Name:
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:HyperLink ID="lblVesselName" runat="server" Target="_blank" Font-Bold="true"
                                            ForeColor="Blue" />
                                        <asp:HyperLink ID="lblVesselCode" runat="server" Target="_blank" Font-Bold="true"
                                            Visible="false" />
                                    </td>
                                    <td>
                                        Job Code:
                                    </td>
                                    <td style="width: 100px;">
                                        <asp:Label ID="lbljobCode" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNCRNoCap" runat="server" Font-Bold="true" Text="NCR No:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNCRNo" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                    </td>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJobStatus" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <div>
                                <uc1:ctlRecordNavigation ID="ctlRecordNavigationDetails" OnNavigateRow="fillvalue"
                                    runat="server" />
                            </div>
                        </td>
                        <td style="width: 50px;">
                            <asp:ImageButton ID="imgEmail" runat="server" ImageUrl="~/Images/EMail.png" OnClick="imgEmail_Click" />
                            <img src="../../Images/printer.png" alt="Print" onclick="javascript:window.print()"
                                style="cursor: hand" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvJobDetails" style="background-color: White">
                <table border="0" cellpadding="2" cellspacing="5" style="width: 100%; color: black;"
                    class="printable">
                    <tr>
                        <td colspan="2" align="left" style="border: 1px solid #aabbdd; background-color: #efffef;
                            padding: 2px; vertical-align: top;">
                            <table style="width: 100%">
                                <tr>
                                    <td align="left" class="row-header">
                                        Job Description :
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 14px">
                                        <asp:Label ID="lbldescription" runat="server" CssClass="SentanceCase"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; padding: 2px;">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr>
                                    <td align="left" class="row-header" colspan="2">
                                        Other Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        PIC :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblPIC" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Job Type :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lbljobType" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Assigned By :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblAssignedBy" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Requisition Number :
                                    </td>
                                    <td class="data">
                                        <asp:HyperLink ID="lnkRequisitionNumber" runat="server" Font-Bold="true" Target="_blank"></asp:HyperLink>
                                        <asp:Label ID="lblReqsnStatus" runat="server" Font-Size="10px" ForeColor="#C71585"
                                            Font-Names="verdana"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Defer to Dry Dock:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblDeferToDD" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Priority:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblPriority" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Inspector:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblInspector" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Inspection Date:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblInspectionDate" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table runat="server" id="tblFunction" border="0" cellpadding="2" cellspacing="1"
                                width="100%">
                                <tr class="row-header">
                                    <td colspan="2">
                                        Function and Location
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Function :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblFunction" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        System Location :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblLocation" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Sub System Location :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblSubLocation" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border: 1px solid #aabbdd; width: 33%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="2">
                                        Applicable Dates:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Raised On :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblRaisedOn" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Expected Completion :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblExpectedCompletion" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        <asp:Label ID="lblCompletedOnCaption" Text="Completed On/By:" runat="server" />
                                    </td>
                                    <td class="data">
                                        <asp:HyperLink ID="lblCompletedOn" runat="server" ForeColor="Blue" Width="80%" CssClass="link"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        <asp:Label ID="lblCompletedByCaption" Text="Verified On/By :" runat="server" />
                                    </td>
                                    <td class="data">
                                        <asp:HyperLink ID="lknCompletedBy" runat="server" ForeColor="Blue" Width="80%" CssClass="link"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="hplRework" ForeColor="Blue" Style="cursor: pointer" runat="server"
                                            Text="Rework" OnClick="hplRework_Click" />
                                        <asp:Button ID="hplCloseThisJob" ForeColor="Blue" Style="cursor: pointer" runat="server"
                                            Text="Verify and close" OnClick="hplCloseThisJob_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; width: 34%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="2">
                                        Assigned Departments:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Dept On Ship :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblDeptonShip" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Dept in Office :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblDeptinOffice" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; width: 33%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="2">
                                        <asp:Label ID="lblcategorylbl" runat="server" Text="Categories"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        Nature :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblNature" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Primary :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblPrimary" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Secondary :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblSecondary" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Minor Category :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblMinorCategory" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        PSC/SIRE Code :
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblPSCSIRE" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" style="border: 1px solid #aabbdd; padding: 2px;">
                            <asp:Panel ID="pnlRootCause" runat="server" Visible="true">
                                <table>
                                    <tr>
                                        <td>
                                            Root Cause Analysis, Corrective Actions and Preventive Measures verified by :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkVerifiedBy" runat="server" ForeColor="Blue" Width="500px" CssClass="link"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="pnlVetting" runat="server" Visible="false">
                <asp:UpdatePanel ID="updateVetting" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="5" style="width: 100%; color: black;"
                            class="printable">
                            <tr>
                                <td align="left" style="border: 1px solid #aabbdd; width: 17%" valign="top">
                                    <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                        <tr class="row-header">
                                            <td colspan="2">
                                                Vetting
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="150px">
                                                Vetting :
                                            </td>
                                            <td class="data">
                                                <asp:Label ID="lblVetting" runat="server" Width="155px">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="150px">
                                                Linked Observation :
                                            </td>
                                            <td class="data">
                                                <asp:Label ID="lblSelectedObs" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" style="border: 1px solid #aabbdd; width: 34%" valign="top" colspan="3">
                                    <asp:Panel ID="pnlVettingObsJobs" ScrollBars="Vertical" runat="server">
                                        <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                            <tr class="row-header">
                                                <td colspan="2">
                                                    Vetting Observations and Jobs :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="page-content" style="font-family: Tahoma; font-size: 12px">
                                                        <ajaxToolkit:TabContainer ID="tbCntr" runat="server" Width="100%" ActiveTabIndex="0">
                                                            <ajaxToolkit:TabPanel ID="tbObservation" runat="server" Font-Names="Tahoma">
                                                                <HeaderTemplate>
                                                                    Observation</HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlObeservation" runat="server">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    Observation :
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 60px;">
                                                                                    <asp:TextBox ID="txtObersvation" runat="server" Width="100%" Height="60px" ReadOnly="true"
                                                                                        Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Responses :
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="GvObservation" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                                        AutoGenerateColumns="false" CellPadding="2" ShowHeaderWhenEmpty="true" Width="100%"
                                                                                        CssClass="gridmain-css">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="20%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Created By" HeaderStyle-Width="20%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Response" HeaderStyle-Width="60%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblResponse" runat="server" Text='<%#Eval("Response") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <EmptyDataTemplate>
                                                                                            <label id="Label1" runat="server">
                                                                                                No jobs found !!</label>
                                                                                        </EmptyDataTemplate>
                                                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                                                        <RowStyle CssClass="RowStyle-css" />
                                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                            <ajaxToolkit:TabPanel ID="tbObservationJob" runat="server" Font-Names="Tahoma">
                                                                <HeaderTemplate>
                                                                    Obs. Jobs</HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlObservationJobs" runat="server">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    Jobs related to the observation :&#160;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="GvObservationJobs" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                                        AutoGenerateColumns="false" CellPadding="2" ShowHeaderWhenEmpty="true" Width="100%"
                                                                                        OnRowDataBound="GvObservationJobs_RowDataBound" CssClass="gridmain-css">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                                                                        target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">
                                                                                                        <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Expected Completion">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("Expected_completion") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Completed">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblCompletedOn" runat="server" Text='<%# Eval("Completed_on")%>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Status">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                                                        <RowStyle CssClass="RowStyle-css" />
                                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                            <ajaxToolkit:TabPanel ID="tbVettingdJobs" runat="server" Font-Names="Tahoma">
                                                                <HeaderTemplate>
                                                                    Vetting Jobs</HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlVettingJobs" runat="server">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    Jobs related to the Vetting :&#160;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="GvVettingJobs" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                                        AutoGenerateColumns="false" CellPadding="2" ShowHeaderWhenEmpty="true" Width="100%"
                                                                                        OnRowDataBound="GvVettingJobs_RowDataBound" CssClass="gridmain-css">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                                                                        target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">
                                                                                                        <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Expected Completion">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("Expected_completion") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Completed">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("Completed_on") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Status">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                                                        <RowStyle CssClass="RowStyle-css" />
                                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                        </ajaxToolkit:TabContainer>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <div id="dvCrewComplaing">
                <asp:Panel ID="pnlCrewComplaint" runat="server" Visible="true">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="3" align="left" class="row-header">
                                Crew Complaint Log:
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" style="border: 1px solid #aabbdd; padding: 2px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="vertical-align: top; border: 1px solid #aabbdd; width: 75%;">
                                            <asp:Repeater ID="rptComplaintsToDPA" runat="server">
                                                <HeaderTemplate>
                                                    <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                                        cellspacing="0">
                                                        <tr style="background-color: #627AA8; color: white; font-weight: bold; text-align: center;">
                                                            <td>
                                                                <asp:Label ID="lbl1" runat="server" Text="Escalated On"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lbl2" runat="server" Text="Escalated By"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:Label ID="lbl3" runat="server" Text="Escalated To"></asp:Label>
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr style="text-align: center;">
                                                        <td>
                                                            <%# Eval("Escalated_On")%>
                                                        </td>
                                                        <td>
                                                            <a target="_blank" href="../../Crew/CrewDetails.aspx?ID=<%# Eval("Escalated_By") %>">
                                                                <%# Eval("Escalated_By_Staff_Code")%></a>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Escalated_By_Rank")%>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <%# Eval("Escalated_by_Name")%>
                                                        </td>
                                                        <td>
                                                            <a target="_blank" href="../../Crew/CrewDetails.aspx?ID=<%# Eval("Escalated_To") %>">
                                                                <%# Eval("Escalated_To_Staff_Code")%></a>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Escalated_To_Rank")%>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <%# Eval("Escalated_To_Name")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="vertical-align: top; border: 1px solid #aabbdd;">
                                            <asp:Panel ID="pnlReleaseToFlag" runat="server" Visible="true">
                                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                    <tr style="background-color: #627AA8; color: white; font-weight: bold;">
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Release to Flag"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            DPA Remarks:
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDPARemark" runat="server" TextMode="MultiLine" Height="80px"
                                                                Width="350px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Button ID="btnReleaseToFlag" runat="server" Text="Release to flag" OnClick="btnReleaseToFlag_Click">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div id="dvNCRRelated">
                <asp:Panel ID="pnlNCRRelated" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td align="left" class="row-header" style="width: 33%">
                                Root Cause Analysis:
                            </td>
                            <td align="left" class="row-header" style="width: 34%">
                                Corrective Action:
                            </td>
                            <td align="left" class="row-header" style="width: 33%">
                                Preventive Action:
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border: 1px solid #aabbdd; background-color: #efffef; padding: 2px;">
                                <asp:Label ID="lblCauses" runat="server"></asp:Label>
                            </td>
                            <td align="left" style="border: 1px solid #aabbdd; background-color: #efffef; padding: 2px;">
                                <asp:Label ID="lblCorrectiveAction" runat="server"></asp:Label>
                            </td>
                            <td align="left" style="border: 1px solid #aabbdd; background-color: #efffef; padding: 2px;">
                                <asp:Label ID="lblPreventiveAction" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div id="dvFollowUp_Attachments">
                <table cellpadding="0" cellspacing="5" width="100%">
                    <tr>
                        <td style="border: 1px solid #aabbdd; width: 50%; vertical-align: top;">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr class="row-header">
                                    <td style="font-weight: bold;">
                                        Details of action taken (Followup) :
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImgBtnAddFollowup" runat="server" ImageUrl="~/Images/AddFollowup.png"
                                            OnClientClick="OpenFollowupDiv();return false;" CssClass="non-printable" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="max-height: 350px; min-height: 250px; overflow: auto">
                                            <asp:GridView ID="grdFollowUps" runat="server" BackColor="White" BorderColor="#999999"
                                                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                EnableModelValidation="True" GridLines="Vertical" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" SortExpression="DATE_CREATED" ItemStyle-Width="10%"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDATE_CREATED" runat="server" Text='<%#Eval("DATE_CREATED","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("DATE_CREATED","{0:dd/MMM/yy}")   %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created By" SortExpression="LOGIN_NAME" ItemStyle-Width="15%"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lblLOGIN_NAME" runat="server" NavigateUrl='<%# "../../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                                                Target="_blank" Text='<%# Eval("user_name")%>' CssClass="pin-it"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Followup" SortExpression="FOLLOWUP" ItemStyle-Width="75%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFOLLOWUP" runat="server" Text='<%#Eval("FOLLOWUP").ToString().Replace("\n","<br>") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="ldl1" runat="server" Text="No followups !!"></asp:Label>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                            </table>
                        </td>
                        <td style="border: 1px solid #aabbdd; width: 50%; vertical-align: top;">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr class="row-header" style="height: 24px">
                                    <td>
                                        Attachments:
                                    </td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div id="showPopUp" style="cursor: pointer;" onclick="showModal('dvGalerryPopUp');">
                                                        Larger View</div>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlAddAttachment" runat="server">
                                                        <asp:ImageButton ID="imgAttach" runat="server" ImageUrl="../../Images/AddAttachment.png"
                                                            OnClientClick="showModal('dvPopupAddAttachment',true,fn_OnClose);" />
                                                        <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" runat="server" id="tdg">
                                        <div id="webpage">
                                            <div id="retina">
                                            </div>
                                            <ul id="myGallery">
                                                <asp:ListView ID="ListView1" runat="server">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Image ID="imgAttachment" ImageUrl='<%# "../../Uploads/Technical/" + Eval("Image_Path") %>'
                                                                data-description='<% #Eval("Created_By_Name") %>' ToolTip='<% #Eval("Attach_Name") %>'
                                                                runat="server" MaxHeight="350" CssClass="rotate-image" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hidenTotalrecords" runat="server" />
                                        <asp:HiddenField ID="HCurrentIndex" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="max-height: 250px; min-height: 0px; overflow: auto;">
                                            <asp:GridView ID="gvAttachments" runat="server" BackColor="White" BorderColor="#999999"
                                                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                EnableModelValidation="True" GridLines="Vertical" Width="100%" OnRowDataBound="gvAttachments_RowDataBound">
                                                <AlternatingRowStyle BackColor="#DDeeEE" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAttach_Date" runat="server" Text='<%#Eval("DATE_OF_CREATION","{0:dd/MMM/yy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Attachment Name"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lblAttach_Name" runat="server" NavigateUrl='' Target="_blank"
                                                                Text='<%#Eval("Attach_Name") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Size" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="">
                                                        <ItemTemplate>
                                                            <a onmousedown="PlayAudio(this);" runat="server" visible='<%# Eval("Is_Audio").ToString() == "1"?true:false %>'>
                                                                <img src="../../Images/AquaPlayicon.png" alt="play" height="25px" />
                                                                <audio preload="none" style="width: 60px; background-color: transparent; padding: 0px;
                                                                    margin: 0px" src='<%# "http://" +HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath+"/uploads/Technical/"+Eval("Attach_Path").ToString()%>'>
                                                                </audio>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgFlagOFF" Height="22px" ToolTip="Download attachment" runat="server"
                                                                ImageUrl="~/Images/Download-icon.png" onclick='<%#"DocOpen(&#39;../../Uploads/Technical/" + Eval("Attach_Path") +"&#39;)" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <label id="Label1" runat="server">
                                                    </label>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#ffffff" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <PagerStyle CssClass="pager" Font-Size="16px" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="dvGalerryPopUp" style="display: none">
                    <ul id="myGallery2">
                        <asp:ListView ID="ListView2" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:Image ID="imgAttachment" ImageUrl='<%# "../../Uploads/Technical/" + Eval("Image_Path") %>'
                                        data-description='<% #Eval("Created_By_Name") %>' ToolTip='<% #Eval("Attach_Name") %>'
                                        runat="server" MaxHeight="650" />
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
            </div>
            <div>
                <table width="100%" cellpadding="1" cellspacing="1" style="background: #dddddd">
                    <tr>
                        <td align="left">
                            <asp:Panel ID="pnlViewCrewInvolve" runat="server">
                                <a href="#" id="hplViewCrewInvolve" style="cursor: pointer" onclick="OpenWorkListCrewInvolved(<%=VID%>,<%=WLID%>,<%=OFFID%>);return false;">
                                    View/Add crew involved in this job: (CrewList) </a>
                            </asp:Panel>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                height: 51px; color: Black; text-align: left; background-color: #f9f9f9; font-family: Tahoma;
                font-size: 11px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="55px">
                    <tr>
                        <td>
                            <div id="dvRecordInformation" style="text-align: left; width: 100%; background-color: #f1f4f5">
                            </div>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnEditJob" runat="server" Text="Edit Job" OnClick="btnEditJob_Click"
                                BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                Height="24px" BackColor="#81DAF5" Width="60px" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" BorderStyle="Solid" BorderColor="#0489B1"
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="60px"
                                OnClientClick="window.close();return false;" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvAddFollowUp" title="Add Follow-Up" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnOfficeID" runat="server" />
                <asp:HiddenField ID="hdnWorklistlID" runat="server" />
                <asp:HiddenField ID="hdnVesselID" runat="server" />
                <table width="100%" cellpadding="0" cellspacing="5">
                    <tr>
                        <td style="text-align: left">
                            Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFollowupDate" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Message:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="200px" Width="480px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSaveFollowUpAndClose" Text="Save and close" runat="server" OnClientClick="hideModal('dvAddFollowUp');"
                                OnClick="btnSaveFollowUpAndClose_OnClick" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvReworkClose" style="display: none; height: 200px; width: 400">
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="5" cellspacing="0" border="0">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblWorklistTitle" runat="server" Font-Size="11" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Remark :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtWorklistStatusRemark" runat="server" TextMode="MultiLine" MaxLength="8000"
                                Height="60px" Width="300px"> </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfRemark" runat="server" ControlToValidate="txtWorklistStatusRemark"
                                ErrorMessage="Please enter remark !" ValidationGroup="worklistgrp"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" cellpadding="5" cellspacing="0" border="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSaveStatus" OnClick="btnSaveStatus_Click" Height="30px" Width="100px"
                        ValidationGroup="worklistgrp" Text="Save" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%" title="Job">
            </iframe>
        </div>
    </div>
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
