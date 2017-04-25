<%@ Page Title="JIBE::DashBoard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Async="true" CodeFile="DashBoard_Common.aspx.cs" Inherits="Infrastructure_DashBoard_Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/DocExpiry_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewChangeEvent_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewContract_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewUSVisaAlert_DataHandler.js" type="text/javascript"></script>
    <link href="../Styles/DashBoardCommon.css?v=1" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DashBoardCommon.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
     <script type="text/javascript">

         google.load('visualization', '1.0', { 'packages': ['corechart'] });

         function ShowCardApprovalDiv(CrewId) {
             document.getElementById("frPopupFrame").src = '../Crew/ProposeCrewCard.aspx?CrewID=' + CrewId;
             document.getElementById("dvPopupFrame").style.display = "block";
             showModal('dvPopupFrame');
         }

         function onLoad() {
             var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
             // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
             var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
             var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
             // At least Safari 3+: "[object HTMLElementConstructor]"
             var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
             var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6

             if (isFirefox == true) {             
                 var cont = document.getElementsByClassName("badgeNCR");
                 var cont1 = document.getElementsByClassName("badgeNonNCR");

                 var len = cont.length;
                 var len1 = cont1.length;                

                 var i = 0;
                 for (i = 0; i < cont.length; i++) {

                     cont[i].className = "badgeNCRFF";
                     cont = document.getElementsByClassName("badgeNCR");
                     if (cont.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }

                 var i = 0;
                 for (i = 0; i < cont1.length; i++) {

                     cont1[i].className = "badgeNonNCRFF";
                     cont1 = document.getElementsByClassName("badgeNonNCR");
                     if (cont1.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }

             }
             if (isSafari == true) {
                 var cont = document.getElementsByClassName("badgeNCR");
                 var cont1 = document.getElementsByClassName("badgeNonNCR");
                 var len = cont.length;
                 var len1 = cont1.length;

                 var i = 0;
                 for (i = 0; i < cont.length; i++) {

                     cont[i].className = "badgeNCRSaf";
                     cont = document.getElementsByClassName("badgeNCR");
                     if (cont.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }

                 var i = 0;
                 for (i = 0; i < cont1.length; i++) {
                     cont1[i].className = "badgeNonNCRSaf";

                     cont1 = document.getElementsByClassName("badgeNonNCR");
                     if (cont1.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }
             }
             if (isChrome == true) {
                 var cont = document.getElementsByClassName("badgeNCR");
                 var cont1 = document.getElementsByClassName("badgeNonNCR");
                 var len = cont.length;
                 var len1 = cont1.length;

                 var i = 0;
                 for (i = 0; i < cont.length; i++) {
                     cont[i].className = "badgeNCRCRM";

                     cont = document.getElementsByClassName("badgeNCR");
                     if (cont.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }
                 var i = 0;
                 for (i = 0; i < cont1.length; i++) {
                     cont1[i].className = "badgeNonNCRCRM";
                     cont1 = document.getElementsByClassName("badgeNonNCR");
                     if (cont1.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }
             }
             if (isOpera == true) {
                 var cont = document.getElementsByClassName("badgeNCR");
                 var cont1 = document.getElementsByClassName("badgeNonNCR");
                 var len = cont.length;
                 var len1 = cont1.length;

                 var i = 0;
                 for (i = 0; i < cont.length; i++) {

                     cont[i].className = "badgeNCRCRM";
                     cont = document.getElementsByClassName("badgeNCR");
                     if (cont.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }
                 var i = 0;
                 for (i = 0; i < cont1.length; i++) {

                     cont1[i].className = "badgeNonNCRCRM";
                     cont1 = document.getElementsByClassName("badgeNonNCR");
                     if (cont1.length == 0) {
                         break;
                     }
                     else {
                         i = -1;
                     }
                 }
             }
             $("#lst90").removeClass("bold");
             $("#lst180").removeClass("bold");
             $("#lst365").removeClass("bold");
             $("#lstAll").removeClass("bold");
             $("#lst365").addClass("bold");   
         }


         function SetPerformanceManagerDays(days) {
             if (days == -1) {

                 //document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (All days)";

                 $("#lst90").removeClass("bold");
                 $("#lst180").removeClass("bold");
                 $("#lst365").removeClass("bold");
                 $("#lstAll").removeClass("bold");
                 $("#lstAll").addClass("bold");
             }
             if (days == 90) {

                 //document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (Last 90 Days)";


                 $("#lst90").removeClass("bold");
                 $("#lst180").removeClass("bold");
                 $("#lst365").removeClass("bold");
                 $("#lstAll").removeClass("bold");
                 $("#lst90").addClass("bold");

             }
             if (days == 180) {

             //    document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (Last 180 Days)";

                 $("#lst90").removeClass("bold");
                 $("#lst180").removeClass("bold");
                 $("#lst365").removeClass("bold");
                 $("#lstAll").removeClass("bold");
                 $("#lst180").addClass("bold");

             }
             if (days == 365) {

               //  document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (Running 365 Days)";

                 $("#lst90").removeClass("bold");
                 $("#lst180").removeClass("bold");
                 $("#lst365").removeClass("bold");
                 $("#lstAll").removeClass("bold");
                 $("#lst365").addClass("bold");
             }
             var hiddenDays = document.getElementById('<%=hdnDays.ClientID%>');
             hiddenDays.value = days;
             asyncGet_Performance_Manager();
         }
    </script>
   
    <style type="text/css">
      
        .AnomalyCell
        {
            background-color: Red;
            cursor: pointer;
        }
        .NoAnomaly
        {
            background-color: Green;
            cursor: pointer;
        }
        .NoData
        {
            background: url(../Images/noreport.png);
            background-repeat: no-repeat;
        }
        
        .ForMyAction .PartTitleStyle-css
        {
            color: White;
            background-color: Red;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr=Red,EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, right top, from(Red), to(#99BBE6));
            background: -moz-linear-gradient(right,  #CEE3F6,  #99BBE6);
            background: linear-gradient(90deg ,Red, #99BBE6);
        }
        
       .badgeNCR
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNCR[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -10px;
            right: -35px;
            background: red;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
             .badgeNonNCR
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNonNCR[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -10px;
            right: -35px;
            background: gray;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
        
        
        
          .badgeNCRFF
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNCRFF[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -0px;
            right: -35px;
            background: red;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
             .badgeNonNCRFF
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNonNCRFF[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -0px;
            right: -35px;
            background: gray;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
        
        
             .badgeNCRCRM
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNCRCRM[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: 3px;
            right: -35px;
            background: red;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
             .badgeNonNCRCRM
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNonNCRCRM[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: 3px;
            right: -35px;
            background: gray;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
        
             .badgeNCRSaf
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNCRSaf[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -10px;
            right: -40px;
            background: red;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
             .badgeNonNCRSaf
        {
             position: relative;
             text-decoration:none;
        }
        .badgeNonNCRSaf[data-badge]:after
        {
            content: attr(data-badge);
            position:absolute;
            top: -10px;
            right: -40px;
            background: gray;
            color: white;
            width:20px;
            height: 20px;
            text-align: center;
            text-decoration:none;
            line-height: 20px;
            border-radius: 50%;
           
        }
        .row-OnBoardCount-Css
        {
            font-size: 10px;
            background-color: transparent;
            color: Black;
            text-align: center;
            font-family: Verdana;
            border: 1px dotted gray;
            padding-left: 5px;
            height:18px;
        }
        .row-Per-Css
        {
            font-size: 10px;
            background-color: transparent;
            color: Black;
            text-align: left;
            font-family: Verdana;
            border-bottom: 1px solid #cccccc;
            padding-top: 0px;
            padding: 0px;
            border-left: 0px solid white;
            border-right: 0px solid white;
            width: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                padding: 0px; background-color: #F6CEE3; text-align: center; font-weight: bold">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 85%; text-align: center;">  
                            <asp:Label ID="lblDashBoard" runat="server" Text="Dashboard"></asp:Label>
                        </td>
                        <td style="width: 5%; text-align: right; border-right: 2px solid Transparent">
                            <asp:Button ID="btnResetSnippet" Text="Reset" runat="server" OnClick="btnResetSnippets_Click"
                                BackColor="#AED7FF" BorderStyle="None" Width="70px" Font-Size="11px" Font-Bold="false"
                                Font-Names="verdana" Height="20px" ForeColor="Blue" />
                        </td>
                        <td style="width: 5%; text-align: center; border-right: 2px solid Transparent">
                            <asp:Button ID="btnMinimizeall" Text="Collapse All" runat="server" Font-Size="11px"
                                Font-Bold="false" Font-Names="verdana" Height="20px" ForeColor="Blue" AutoPostBack="true"
                                BackColor="#AED7FF" BorderStyle="None" Width="80px" OnClick="btnMinimizeall_Click" />
                        </td>
                        <td style="width: 5%; text-align: right">
                            <asp:Button ID="btnMaximizeall" Text="Expand All" runat="server" Font-Size="11px"
                                Font-Bold="false" Font-Names="verdana" Height="20px" ForeColor="Blue" AutoPostBack="true"
                                BackColor="#AED7FF" BorderStyle="None" Width="80px" OnClick="btnMaximizeall_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnFromDays0" runat="server" Value="0" />
            <asp:HiddenField ID="hdnToDays7" runat="server" Value="0" />
             <asp:HiddenField ID="hdnFromDays8" runat="server" Value="0" />
            <asp:HiddenField ID="hdnToDays30" runat="server" Value="0" />
             <asp:HiddenField ID="hdnFromDays31" runat="server" Value="0" />
            <asp:HiddenField ID="hdnToDays90" runat="server" Value="0" />
            <asp:HiddenField ID="hdfUserdepartmentid" runat="server" Value="0" />
            <asp:HiddenField ID="hdfUserCompanyID" runat="server" Value="0" />
             <asp:HiddenField ID="hdnDays" runat="server" Value="365" />
            <asp:WebPartManager ID="WebPartManagerDB" runat="server">
                <Personalization ProviderName="Dataprovider" />
            </asp:WebPartManager>
            <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
                border-bottom: 1px solid #cccccc; padding: 0px; color: #282829; overflow: auto;
                width: 100%; background-color: #505050">
                <table id="tblmain" runat="server" cellpadding="0" cellspacing="0" style="margin: 0px;"
                    width="100%;">
                    <tr>
                                      <td valign="top" style="padding-right: 10px">
                            <auc:cutWebPartZone ID="WebPartZoneOne" runat="server" Width="100%" WebPartVerbRenderMode="TitleBar"
                                PartChromePadding="0" CloseVerb-Visible="false" HeaderText="Zone 1" HeaderStyle-ForeColor="Transparent"
                                HeaderStyle-Font-Size="0px" HeaderStyle-Height="0px" BorderStyle="None" MinimizeVerb-ImageUrl="~/Images/Minimize-Dash.png"
                                RestoreVerb-ImageUrl="~/Images/Maximize-Dash.png" CloseVerb-ImageUrl="~/images/close-Dash.png">
                                <PartTitleStyle VerticalAlign="Top" CssClass="PartTitleStyle-css" BorderWidth="0"
                                    Font-Size="14px" />
                                <PartStyle BorderColor="Red" GridLines="None" />
                                <PartChromeStyle BorderColor="#cccccc" CssClass="PartChromeStyle-css" />
                                <ZoneTemplate>
                                <asp:Label ID="lblWebPartInspectionCompleted" Title="Completed Inspections In Last 90 days" Width="100%"
                                        runat="server">
                                       
                                        <div style="max-height: 300px; overflow: auto; width: 100%; border-top: 1px solid #cccccc">
                                            <div id="dvWebPartInspectionCompleted">
                                            </div>
                                        </div>
                                    </asp:Label>








                                    <asp:Label ID="lblWebPartMyShortcuts" Title="My Shortcuts" Width="100%" runat="server">
                                        <asp:ImageButton ID="btnEditFavourite" runat="server" Text="Edit Favourite" ImageAlign="Baseline"
                                            ImageUrl="~/Images/dash-FavLink-Edit.png" OnClientClick="OpenPopupWindow('POP__Menu', 'Edit Menu', 'Snippets/Dash_Edit_MyShortcuts_Menu.aspx','popup',600,500,null,null,false,false,true,false);return false;"
                                            Height="12px" Width="20px" />
                                        <div style="max-height: 300px; overflow: auto; width: 100%; border-top: 1px solid #cccccc">
                                            <div id="dvMyShortsCuts_Menu">
                                            </div>
                                        </div>
                                    </asp:Label>
                                     <asp:Label ID="lblWebPartEvents_Done" Title="Events done in last 90 days" Width="100%"
                                        runat="server"> <div style="max-height: 300px; overflow: auto; width: 100%; border-top: 1px solid #cccccc"><div id="dvEvents_Done"></div></div></asp:Label>
                      <asp:Label ID="lblWebPartPerformanceManager" Title="Performance Manager"
                                        Width="100%" runat="server">  
                                           <div  style="overflow:auto;white-space:nowrap">
                                      
                                           <table>
                                           <tr>
                                           <td></td>
                                           <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                         
                                          

                                           <td><div style="text-align:right"><div style="cursor:pointer;" onclick="SetPerformanceManagerDays(90)" id="lst90">&nbsp;  <span style="text-decoration:underline;">Last 90 Days</span>  &nbsp;</div></td>
                                           <td><div style="cursor:pointer;" onclick="SetPerformanceManagerDays(180)" id="lst180">&nbsp; <span style="text-decoration:underline;">Last 180 Days</span> &nbsp;</div></td>
                                           <td><div style="cursor:pointer;" onclick="SetPerformanceManagerDays(365)" id="lst365">&nbsp; <span style="text-decoration:underline;">Running 365 Days</span> &nbsp;</div></td>
                                           <td><div style="cursor:pointer;" onclick="SetPerformanceManagerDays(-1)" id="lstAll">&nbsp; <span style="text-decoration:underline;">All Days</span> &nbsp;</div></div></td>
                                           </tr>
                                           <tr>
                                           <td colspan="6">
                                           <div id="divPerManLastUpdated"></div>
                                           </td></tr>
                                           </table>
 
                                              <div style="max-height: 300px;  width: 100%;overflow:auto; border-top: 1px solid #cccccc;white-space:nowrap" >
                                            <div id="dvWebPartPerformanceManager">
                                            </div>
                                        </div>                                        
                                        
                                        
                                    </asp:Label>
                                </ZoneTemplate>
                            </auc:cutWebPartZone>
                        </td>
                        <td valign="top" style="padding-right: 10px">
                            <auc:cutWebPartZone ID="WebPartZoneTwo" runat="server" Width="100%" WebPartVerbRenderMode="TitleBar"
                                PartChromePadding="0" HeaderStyle-Font-Size="0px" HeaderStyle-Height="0px" CloseVerb-Visible="false"
                                HeaderStyle-ForeColor="Transparent" HeaderText="Zone 2" BorderStyle="None" MinimizeVerb-ImageUrl="~/Images/Minimize-Dash.png"
                                RestoreVerb-ImageUrl="~/Images/Maximize-Dash.png" CloseVerb-ImageUrl="~/images/close-Dash.png">
                                <PartTitleStyle VerticalAlign="Top" CssClass="PartTitleStyle-css" BorderWidth="0"
                                    Font-Size="14px" />
                                <PartChromeStyle BorderColor="#cccccc" CssClass="PartChromeStyle-css" />
                                <ZoneTemplate>
                                    <asp:Label ID="lblWebPartOverdueFileScheduleApproval" Title="Forms waiting for my approval" runat="server"
                                        Width="100%">
                                     <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartOverdueFileScheduleApproval">
                                            </div>
                                        </div>
                                    </asp:Label>
                                     <asp:Label ID="lblWebPartOverdueFileScheduleReceiving" Title="Overdue File Receiving" runat="server"
                                        Width="100%">
                                     <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartOverdueFileScheduleReceiving">
                                            </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartPendingForApproval" Title="Pending Approval - Reqsn" runat="server"
                                        Width="100%">
                                     
                                         <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvPending_ReqsnPO">
                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartPendingTravelPO" Title="Pending Approval - Travel" runat="server"
                                        Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvPending_TravelPO">
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartPendingLogisticPO" Title="Pending Approval - Logistic" runat="server"
                                        Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvPending_LogisticPO">
                                             </div>
                                        </div>
                                    </asp:Label>
                                    
                                    <asp:Label ID="lblWebPartPendingInterviews_By_User" Title="My Crew Interviews" runat="server"
                                        Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvInterviewSchedules_By_UserID"></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartContractToSign" Title="Contracts Pending To Be Signed by Office"
                                        runat="server" Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvContractToSign"></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartContractToVerify" Title="Contracts Pending To Be Verified By Office"
                                        runat="server" Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvContractToVerify"></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartCrewCardProposed" Title="Red Cards / Yellow Cards Proposed"
                                        runat="server" Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                        <div id="dvCrewCardProposed" >
                                        
                                        </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartSuppliedPROV" Title="Provision Last Supplied,with delivery updated"
                                        runat="server" Width="100%">
                                     <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvProvisionLastSupplied">
                                            </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWepPartReqsnCount" Title="Requisitions from Vessels" runat="server"
                                        Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvReqsnCount">
                                            </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebpartPendingWorkList" runat="server" Title="My WorkList" Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvMyWork_List">
                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartPendingWorkListVerification" runat="server" Title="Pending WorkList Verifications"
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvPending_WorkList_Verification">
                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartWorkListDue7Days" runat="server" Title="WorkList due in next 7 days"
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebPartWorkListDue7Days">
                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                     <asp:Label ID="lblWebPartPendingCTMApproval" Title="Pending Approval - CTM" runat="server"
                                        Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvWebpartPendingCTMApproval"></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartCylinderOilConsumption" Title="Cylinder Oil Consumption"
                                        runat="server" Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebPartCylinderOilConsumption">
                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartPendingCrewBriefing" Title="Pending Crew Briefing"
                                        runat="server" Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvWebPartPendingCrewBriefing"></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartPendingCardApproval" Title="Pending Card Approval"
                                        runat="server" Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebPartPendingCardApproval">
                                             </div>
                                        </div>
                                    </asp:Label>
                                     <asp:Label ID="lblWebPendingASLEvaluation" Title="Pending ASL Approval"
                                        runat="server" Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebPendingASLEvaluation">
                                             </div>
                                        </div>
                                    </asp:Label>
                                     <asp:Label ID="lblWebPendingInvoiceApproval" Title="No. of Pending Invoices"
                                        runat="server" Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebPendingInvoiceApproval">
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebOpexVesselReport" Title="Opex(Running 365 Days)"
                                        runat="server" Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebOpexVesselReport">
                                             </div>
                                        </div>
                                    </asp:Label>
                                     <asp:Label ID="lblVoyage" Title="Voyage Report"
                                        runat="server" Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="divVoyage">
                                             </div>
                                        </div>
                                    </asp:Label>

                                      <asp:Label ID="lblCharterBook" Title="Charter Book"
                                        runat="server" Width="100%">
                                        <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvCharterBook">
                                             </div>
                                        </div>
                                    </asp:Label>

                                </ZoneTemplate>
                            </auc:cutWebPartZone>
                        </td>
                        <td valign="top">
                            <auc:cutWebPartZone ID="WebPartZone3" runat="server" Width="100%" WebPartVerbRenderMode="TitleBar"
                                PartChromePadding="0" HeaderStyle-Font-Size="0px" HeaderStyle-Height="0px" CloseVerb-Visible="false"
                                HeaderStyle-ForeColor="Transparent" HeaderText="Zone 3" BorderStyle="None" MinimizeVerb-ImageUrl="~/Images/Minimize-Dash.png"
                                RestoreVerb-ImageUrl="~/Images/Maximize-Dash.png" CloseVerb-ImageUrl="~/images/close-Dash.png">
                                <PartTitleStyle VerticalAlign="Top" CssClass="PartTitleStyle-css" BorderWidth="0"
                                    Font-Size="14px" />
                                <PartChromeStyle BorderColor="#cccccc" CssClass="PartChromeStyle-css" />
                                <ZoneTemplate>
                                    
                                    <asp:Label ID="lblWebPartInspectionOverdue" Title="Inspection Overdue" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartInspectionOverdue" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>

                                    <asp:Label ID="lblWebPartInspectionDueInMonth" Title="Inspection Due in Next 30 Days" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartInspectionDueInMonth" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartAllPendingInterviews" Title="All Pending Interviews" runat="server"
                                        Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvInterviewSchedules"></div></div></asp:Label>

                                    <asp:Label ID="lblWebPartEvaluations" Title="Crew evaluation planned for next 7 days"
                                        runat="server" Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvEvaluations" >                                        
                                            </div>
                                        </div>
                                    </asp:Label>
                                      <asp:Label ID="lblWebPartUSVisaAlert" Title="List of crew with expired US VISA, or, with no data"
                                        runat="server" Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvUSVisaAlert"></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartReqsnProcessing" Title="Requisitions-Avg time taken(in days) at stages "
                                        runat="server" Width="100%">
                                         <div style="max-height: 300px; overflow: auto;">
                                        <div id="dvReqsnProcessing">
                                            
                                        </div>
                                    </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartDocumentsexpire" Title="Documents expiring in next 90 days or already expired"
                                        runat="server" Width="100%"> <div id="dvDocAlerts_Container"  style="max-height: 300px; overflow: scroll;" ><div id="dvDocExpiryAlerts"  ></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartCrewChangeAlerts" Title="Crew events planned for next 10 days"
                                        runat="server" Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvCrewChangeAlerts" ></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartPendingNCRBYDept" Title="Pending NCR - Based on your department"
                                        runat="server" Width="100%">
                                    <div style="max-height: 300px; overflow: auto;">
                                        <div id="dvPendingNCRByDept">
                                            
                                        </div>
                                    </div>
                                    </asp:Label>
                                     <asp:Label ID="lblWebPartCrewComplaints" Title="Crew Complaints" runat="server" Width="100%"> 
                                     <div style="max-height: 300px; overflow: auto;">
                                     <div id="dvCrewComplaints">
                                     </div>
                                     </div>
                                     </asp:Label>

                                    <asp:Label ID="lblWebPartPendingNCRALLDept" Title="Pending NCR - All department"
                                        runat="server" Width="100%">
                                    <div style="max-height: 300px; overflow: auto;">
                                        <div id="dvPendingNCRALLDept">
                                            
                                        </div>
                                    </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartPendingNCRByAssignor" Title="Pending NCR PSC - Based on your department"
                                        runat="server" Width="100%">
                                    <div style="max-height: 300px; overflow: auto;">
                                        <div id="dvPendingNCRByAssignor">
                                            
                                        </div>
                                    </div>
                                    </asp:Label>
                                    
                                    <asp:Label ID="lblWebPartPortCallsVessel" Title="Port calls per Vessel in last 60 days"
                                        runat="server" Width="100%">
                                    <div id="dvWebPartPortCallsVessel" style="max-height: 300px; overflow: auto;">
                                   
                                         <%--   <img  id="imgPortCalls_Vessel" alt="" />--%>
                                     
                                    </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartPortcallsMonth" Title="Port calls per month in last 12 months"
                                        runat="server" Width="100%">
                                    <div id="dvWebPartPortcallsMonth" style="max-height: 300px; overflow: auto;">
                                     <%-- <img  id="imgPortcalls_Month" alt="" />--%>
                                    </div>
                                    </asp:Label>

                                    <asp:Label ID="lblWebPartEvaluationBelow60" Title="Crew evaluations below 60%" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartEvaluationBelow60" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>

                                     <asp:Label ID="lblWebPartDecklogAnomalies" Title="Deck Log Anomalies" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartDecklogAnomalies" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>


                                        <asp:Label ID="lblWebPartEnginelogAnomalies" Title="Engine Log Anomalies" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartEnginelogAnomalies" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartCTMConfirmationFromVessel" Title="CTM confirmation not received from vessel"
                                        runat="server" Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvWebPartCTMConfirmationFromVessel" ></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartOpsWorklistDueIn7Days" runat="server" Title="WorkList due in next 7 days - Operations"
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebPartOpsWorklistDueIn7Days">                                            
                                             </div>
                                        </div>
                                    </asp:Label>
             
                                    <!-- Snippets for Survey -->
                                    <asp:Label ID="lblWebPartSurvDueinNext30Days" runat="server" Title="Surveys due in next 30 days"
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvWebSurvDueinNext30Days">                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartSurvDueinNext7DaysAndOverdue" runat="server" Title="Surveys due in next 7 days and overdue"
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvSurvDueinNext7DaysAndOverdue">
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartSurvPendingVerification" runat="server" Title="Surveys (Active) pending for verification"
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvSurvPendingVerification">                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartSurvNAPendingVerification" runat="server" Title="Surveys (Not Active) pending for verification"
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvSurvNAPendingVerification">                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                      <asp:Label ID="lblWebPartSurvExpiryin31to90days"  runat="server" Title="Surveys expiry in 31-90 days " 
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvSurvExpiryin31to90days">                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                      <asp:Label ID="lblWebPartSurvExpiryin7to30days" runat="server" Title="Surveys expiry in 7-30 days " 
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="dvSurvExpiryin7to30days">                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                      <asp:Label ID="lblWebPartSurvExpiryinLessThen7days" runat="server" Title="Surveys expiry in less than 7 days or already overdue " 
                                        Width="100%">
                                       <div style="max-height: 300px; overflow: auto;">
                                             <div id="divSurvExpiryinLessThen7days">                                            
                                             </div>
                                        </div>
                                    </asp:Label>
                                                                        <!-- End Snippets for Survey -->
                                    <asp:Label ID="lblWebPartPMSOverdueJobs" Title="PMS Overdue Jobs" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartPMSOverdueJobs" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartBelowTresholdInventoryItems" Title="Inventory Items Below Treshold" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartBelowTresholdInventoryItems" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>

                                    <asp:Label ID="lblWebPartCrewPerformance" Title="Crew Performance" runat="server"
                                        Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvWebPartCrewPerformance" ></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartEvaluationDue" Title="Evaluation Overdue" runat="server"
                                        Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvWebPartCrewEvaluationDue" ></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartCrewEvaluationFeedback" Title="Pending Feedback Request" runat="server"
                                        Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvWebPartCrewEvaluationFeedback" ></div></div></asp:Label>
                                    <asp:Label ID="lblWebPartCrewPerformanceVerification" Title="Crew Performance Verification" runat="server"
                                        Width="100%"> <div style="max-height: 300px; overflow: auto;"><div id="dvWebPartCrewPerformanceVerification" ></div></div></asp:Label>

                                    <asp:Label ID="lblWebPartCrewONBDList" Title="Onboard Crew Count" runat="server"
                                        Width="100%"> 
                                        <div style="max-height: 300px; overflow: auto;">
                                        <asp:LinkButton ID="lnkSendMail" runat="server" BackColor="Yellow" OnClientClick="SendMail('lblWebPartCrewONBDList');return false;">FollowUp</asp:LinkButton>
                                            <div id="dvWebPartCrewONBDList" >
                                            </div>
                                        </div>
                                    </asp:Label>
                                    <asp:Label ID="lblWebPartCrewSeniorityReward" Title="Seniority Reward" runat="server"
                                        Width="100%"> 
                                        <div style="max-height: 300px; overflow: auto;max-width: 580px">
                                            <div id="dvWebPartCrewSeniorityReward" >
                                            </div>
                                        </div>
                                    </asp:Label>
                                     
                                     
                                     <asp:Label ID="lblWebPartWorklistIncident180Days" Title="Incidents reported in last 180 Days" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartWorklistIncident180Days" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>
                                      <asp:Label ID="lblWebPartWorklistNearmiss180Days" Title="Near miss reported in last 180 Days" runat="server"
                                        Width="100%">
                                          <div style="max-height: 300px; overflow: auto;">
                                            <div id="dvWebPartWorklistNearmiss180Days" >
                                        
                                            </div>
                                        </div>
                                    </asp:Label>
                                   
                                </ZoneTemplate>
                            </auc:cutWebPartZone>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Label ID="lblErrorMessage" Font-Size="11px" Font-Italic="true" ForeColor="Red" Visible="false"
                runat="server"></asp:Label>
            <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
                border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                left: 0.5%; top: 15%; width: 40%; height: 550px; z-index: 1; color: black"
                title=''>
                <div class="content">
                    <iframe id="frPopupFrame" src="" frameborder="0" height="550px" width="100%"></iframe>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
