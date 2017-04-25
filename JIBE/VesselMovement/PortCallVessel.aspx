<%@ Page Title="Vessel Movement" Language="C#" MasterPageFile="~/Site.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="PortCallVessel.aspx.cs" EnableEventValidation="false"
    Inherits="VesselMovement_PortCallVessel" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script>

        (function () {
            function ls() {
                var c = document.cookie.split(';');
                for (var i = 0; i < c.length; i++) {
                    var p = c[i].split('='); if (p[0] == 'scrollPosition') {
                        p = unescape(p[1]).split('/'); for (var j = 0; j < p.length; j++) {
                            var e = p[j].split(','); try {
                                if (e[0] == 'window') { window.scrollTo(e[1], e[2]); }
                                else
                                    if (e[0]) { var o = document.getElementById(e[0]); o.scrollLeft = e[1]; o.scrollTop = e[2]; }
                            } catch (ex) { }
                        } return;
                    }
                }
            }
            function ss() {
                var s = 'scrollPosition='; var l, t;
                if (window.pageXOffset !== undefined) { l = window.pageXOffset; t = window.pageYOffset; }
                else if (document.documentElement && document.documentElement.scrollLeft !== undefined) { l = document.documentElement.scrollLeft; t = document.documentElement.scrollTop; }
                else { l = document.body.scrollLeft; t = document.body.scrollTop; } if (l || t) { s += 'window,' + l + ',' + t + '/'; }
                var es = (document.all) ? document.all : document.getElementsByTagName('*');
                for (var i = 0; i < es.length; i++) {
                    var e = es[i]; if (e.id && (e.scrollLeft || e.scrollTop))
                    { s += e.id + ',' + e.scrollLeft + ',' + e.scrollTop + '/'; }
                } document.cookie = s + ';';
            }
            var a, p; if (window.attachEvent) { a = window.attachEvent; p = 'on'; } else { a = window.addEventListener; p = ''; }
            a(p + 'load', function () {
                ls(); if (typeof Sys != 'undefined' && typeof Sys.WebForms != 'undefined')
                { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ls); Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(ss); }
            }, false);
            a(p + 'unload', ss, false);
        })();

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
        td.blog_content div
        {
            width: 100%;
            text-align: left;
            padding: 2px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function validationOnSave() {
            var txteta = document.getElementById("ctl00_MainContent_txtFrom").value;
            var txtetd = document.getElementById("ctl00_MainContent_txtTo").value; ;



            var dt1 = parseInt(txteta.substring(0, 2), 10);
            var mon1 = parseInt(txteta.substring(3, 5), 10);
            var yr1 = parseInt(txteta.substring(6, 10), 10);

            var dt2 = parseInt(txtetd.substring(0, 2), 10);
            var mon2 = parseInt(txtetd.substring(3, 5), 10);
            var yr2 = parseInt(txtetd.substring(6, 10), 10);


            var ArrivalDt = new Date(yr1, mon1, dt1);
            var DepartureDate = new Date(yr2, mon2, dt2);

            if (txteta != "" && txtetd != "") {
                if (ArrivalDt > DepartureDate) {
                    alert("From Date can't be before of To Date.");
                    return false;
                }
            }
            return true;
        }


        function OpenPopUp(url) {
            OpenPopupWindow('PortCost', 'Vessel Movement', url, 'popup', 800, 1200, null, null, false, false, true, null);
        }
        function OpenPopUpWar(url) {
            OpenPopupWindow('PortCost', 'War Risk Ports', url, 'popup', 800, 1200, null, null, false, false, true, null);
        }
        function OpenTemplate(VID, VName) {
            var url = 'PortCallTemplate.aspx?Vessel_ID=' + VID + '&VName=' + VName;
            OpenPopupWindowBtnID('PortCallTemplate', 'Vessel Movement', url, 'popup', 800, 1200, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenCrewList(VID) {
            var url = '../Crew/CrewList_PhotoView.aspx?vcode=' + VID;
            //OpenPopupWindow('PortCallCrewList', 'Vessel Movement', url, 'popup', 800, 1200, null, null, false, false, true, null);
            window.open(url, "_blank");
        }

        function OpenPCallDetail(PCID, VID, PID) {
            var url = 'Port_Call_Report.aspx?PCID=' + PCID + '&VID=' + VID + '&PID=' + PID;
            OpenPopupWindowBtnID('Port_Call_Report', 'Vessel Movement', url, 'popup', 900, 1200, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
     

        function OpenScreen(ID, Job_ID) {

            var url = 'PortCall_Entry.aspx?ID=' + ID + '&StatusID=' + Job_ID;
            OpenPopupWindowBtnID('PortCall_Entry', 'Vessel Movement', url, 'popup', 600, 900, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }

        function myFunction() {

            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var body = document.body,
             html = document.documentElement;

            var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
            var w = (size.width);
            var y = w - (3 * w / 100);
            document.getElementById('divMain').setAttribute("style", "width:" + w + "px");
            document.getElementById('divMain').style.overflow = scroll;
            document.getElementById('blur-on-updateprogress').setAttribute("style", "height:" + height + "px");
            document.getElementById('divRTP').setAttribute("style", "width:" + y + "px");
            document.getElementById('divRTP').style.overflow = scroll;

            //  window.resizeTo(size.width, size.height);
        }

        function OpenCrewOnOff(PCID) {
            var url = 'Port_Call_CrewOnOff.aspx?PCID=' + PCID;
            OpenPopupWindowBtnID('Port_Call_Report', 'Vessel Movement', url, 'popup', 600, 900, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenVetting(Vetting_ID) {
            var url = '../Technical/Vetting/Vetting_Details.aspx?Vetting_ID=' + Vetting_ID;
            window.open(url, "_blank");
        }
    </script>
    <style type="text/css">
        .checkboxlist input
        {
            font: inherit;
            font-size: 0.875em; /* 14px / 16px */
            color: #494949;
            margin-bottom: 12px;
            margin-top: 5px;
            margin-left: 10px !important;
        }
        .dvWhite
        {
            float: left;
            text-align: center;
            width: 100%;
            height: 100%;
            font-family: Tahoma;
            font-size: 12px;
            background-color: #ffffff;
        }
        .dvPortCall
        {
            float: left;
            text-align: center;
            width: 100%;
            height: 100%;
            border: 1px solid #cccccc;
            font-family: Tahoma;
        }
        .dvGray
        {
            float: left;
            text-align: center;
            width: 100%;
            height: 100%;
            font-family: Tahoma;
            font-size: 12px;
            background-color: #e0e0e0;
        }
        
        .ImgDisabled
        {
            cursor: pointer;
        }
    </style>
    <%--    <script type="text/javascript">
        var divid = document.getElementById("divRepeater");
        divid.scrollTop = divid.scrollHeight;
        divid.scrollLeft = divid.scrollWidth;
        
</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <div style="color: Black;" id="divMain">
            <div style="border: 1px solid #cccccc" class="page-title">
                Vessel Movement
            </div>
            <div id="dvpage-content" class="page-content-main" style="padding: 1px; overflow:auto" >
                <asp:UpdatePanel ID="Update1" runat="server">
                    <ContentTemplate>
                        <table width="100%" cellpadding="2" cellspacing="1" >
                            <tr>
                            <td align="right" style="width: 10%">Fleet Name : </td>
                             <td align="left" style="width: 15%"> 
                            <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                                        Height="150" Width="160" /></td>
                                <td align="right" style="width: 10%">
                                    Vessel Name :
                                </td>
                                <td align="left" colspan="2" style="width: 15%">
                                 <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"  
                                                        Height="200" Width="160" />
                                  </td>
                                <td align="left"  style="width: 10%">

                                  <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" /> &nbsp;
                                   <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />&nbsp; 
                                 <asp:ImageButton ID="btnImgPrint" runat="server"  ImageUrl="~/Images/Printer.png" Width="20px" Height="20px" Text="Print Preview" ToolTip="Print"
                                        OnClick="btnPrintDPL_Click" />
                                   <asp:ImageButton ID="imgHistory" runat="server" Height="20px" Width="20px" Visible="false" OnClientClick="OpenPopUp('PortCallHistory.aspx');return false;" ToolTip="Port Call History"
                                            ImageUrl="~/Images/history.png" />&nbsp; 
                                   <asp:ImageButton ID="imgCost" runat="server" Height="20px" Width="20px" Visible="false" OnClientClick="OpenPopUp('Port_Cost.aspx');return false;" ToolTip="Port Cost"
                                            ImageUrl="~/Images/dollar.png" />&nbsp; 
                                    <asp:ImageButton ID="btnAlerts" runat="server" Height="20px" Width="20px"  ImageUrl="~/Images/alert.jpg" ToolTip= "Port Call Alerts"  Visible="false" OnClientClick="OpenPopUp('PortCallAlert.aspx');return false;" />
                                     
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" Width="20px" ToolTip="War Risk Ports" ImageUrl="~/Images/Soldier.png" Text="War Risk Ports"   Visible='<%# uaEditFlag %>'  OnClientClick="OpenPopUpWar('Port_War_Risk.aspx');return false;" />
                                </td>

                                <td align="right" style="width: 10%"> <asp:TextBox ID="txtFrom" Visible="false" runat="server" Width="1px" BackColor="#FFFFCC"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom">  </ajaxToolkit:CalendarExtender>

                                  </td>

                            </tr>
<%--                            <tr style="visibility:hidden">
                                <td align="left" colspan ="7">
                                    <asp:Button ID="btnPrintDPL" runat="server" Visible="false" Width="120px" Text="DPL Print Preview"
                                        OnClick="btnPrintDPL_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnPortCost" runat="server" Width="120px" Text="Port Cost" Visible='<%# uaEditFlag %>' OnClientClick="OpenPopUp('Port_Cost.aspx?Vessel_ID=0');return false;"/>
                                    &nbsp;
                                    <asp:Button ID="btnPort" runat="server" Width="120px" Text="Port Call History" Visible='<%# uaEditFlag %>' OnClientClick="OpenPopUp('PortCallHistory.aspx');return false;" />
                                    &nbsp;
                                    <asp:Button ID="btnAlert" runat="server" Width="120px" Text="Port Call Alerts" Visible='<%# uaEditFlag %>' OnClientClick="OpenPopUp('PortCallAlert.aspx');return false;" />
                                    &nbsp; 
                                     <asp:Button ID="PortCallNotification" runat="server" Width="120px" Text="Notification" Visible='<%# uaEditFlag %>' OnClientClick='window.open("../VesselMovement/Port_Call_Notification.aspx","_blank");' />
                                </td>
                                  <td align="right" style="width: 5%">
                                    &nbsp;
                                </td>
                            </tr>--%>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="btnSupplier" />
                        <asp:PostBackTrigger ControlID="btnPrintDPL" />
                        <asp:PostBackTrigger ControlID="btnPortCost" />--%>
                        <asp:PostBackTrigger ControlID="DDLFleet" />
                        <asp:PostBackTrigger ControlID="DDLVessel" />
                        <asp:PostBackTrigger ControlID="btnFilter" />
                    </Triggers>
                </asp:UpdatePanel>
                        <table>
                            <tr>
                                <td>
                                  <div id="divRTP" style="text-align: center; border: 1px solid #cccccc; 
                                       font-family: Tahoma; font-size: 11px;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
                                        <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound" 
                                            onitemcreated="rpt1_ItemCreated">
                                            <ItemTemplate>
                                                <div style="float: left; text-align: left; width: 100%; border: 1px solid #cccccc;
                                                    font-family: Tahoma; font-size: 11px;  background-color: #ffffff; overflow:auto" >

                                                    <table>
                                                        <tr>
                                                            <td valign="top" align="right" >
                                                            
                                                                   <table width="180px" runat="server" id="table1" style="height: 100px; overflow:auto;">
                                                                   <tr>
                                                                        <td  width="75%"  align="right" style="background-color:Menu; font-weight: bold"  colspan="2">
                                                                            <asp:Label ID="FVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 20px;" align="right" colspan="2">
                                                                            Port Name :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 15px;" align="right"  colspan="2">
                                                                            Arrival :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 15px;" align="right"  colspan="2">
                                                                            Berthing :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 15px" align="right"  colspan="2">
                                                                            Departure :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;" align="right"  colspan="2">
                                                                            <asp:ImageButton ID="btnPrevious" runat="server" Visible='<%# uaEditFlag %>' ImageUrl="~/Images/Arrow2Left.png" ToolTip="See Previous" OnCommand="btnPrevious_Click" CommandArgument='<%#Eval("[Vessel_ID]") %>'/>
                                                                            <asp:ImageButton ID="btnNext" runat="server" Visible='<%# uaEditFlag %>' ImageUrl="~/Images/Arrow2right.png" ToolTip="Hide Last" OnCommand="btnNext_Click" CommandArgument='<%#Eval("[Vessel_ID]") %>'/>
                                                                        </td>
                                                                    </tr>

                                                                     <tr>
                                                                        <td style="height: 10px;font-weight: bold" align="right"  colspan="2">
                                                                            <asp:ImageButton ID="btnAddNew" runat="server" OnClientClick='<%#"OpenScreen(&#39;"+ Eval("Vessel_ID") +"&#39;);return false;"%>' 
                                                                                ToolTip="Add Port Call" Width="20px" Height="20px" Visible='<%#uaEditFlag%>' ImageUrl="~/Images/Add-icon.png"/>
                                                                            <asp:ImageButton ID="btntemplate" Width="20px" ToolTip="Template" Height="20px" OnClientClick='<%#"OpenTemplate((&#39;"+ Eval("Vessel_ID") +"&#39;),(&#39;"+ Eval("[Vessel_Name]") + "&#39;));return false;"%>'
                                                                                runat="server" Visible='<%# uaEditFlag %>' ImageUrl="~/Images/task-list.gif" Text="Template"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 5px" align="right" colspan="2">

                                                                        </td>
                                                                    </tr>
                                                                    <tr >
                                                                        <td style="height: 10px;font-weight: bold" align="right"  colspan="2">
                                                                            <asp:ImageButton ID="btnCrewList" runat="server"  OnClientClick='<%#"OpenCrewList(&#39;"+ Eval("Vessel_Short_Name") +"&#39;);return false;"%>' 
                                                                                ToolTip="Crew List"  ImageUrl="~/Images/crew-on.png" Height="20px" Width="20px"/>
                                                                             
                                                                              <asp:ImageButton ID="btnvesselReport" runat="server" Visible='<%# uaEditFlag %>' OnCommand="btnvesselReport_Click"
                                                                                CommandArgument='<%#Eval("Vessel_ID") %>' ToolTip="Vessel Report" Height="20px" Width="20px" ImageUrl="~/Images/table-icon.png" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                              
                                                                </div>
                                                                </div>
                                                            </td>
                                                            <td valign="top">
                                                            <div   id="divRepeater" style="overflow:auto" >
                                                            <table>   
                                                            <tr>
                                                            <td>
                                                               <asp:Repeater runat="server" ID="rpt2" OnItemDataBound="rpt2_ItemDataBound" OnItemCommand="rpt2_ItemCommand" >
                                                                    <HeaderTemplate>
                                                                        <table border="0">
                                                                        <tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="litRowStart" runat="server"></asp:Literal>
                                                                        <td>
                                                                            <div class="dvPortCall" style="overflow:auto" >
                                                                                    <table style="width: 120px; margin-top:10px"  >
                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color: Black; height: 20px; font-weight:bold; " align="center">
                                                                                          <asp:LinkButton ID="SelectButton"   Text='<%#Eval("Port_Name")%>' OnClientClick='<%#"OpenPCallDetail((&#39;" + Eval("[Port_Call_ID]") +"&#39;),(&#39;"+ Eval("[Vessel_ID]") + "&#39;),(&#39;"+ Eval("[Port_ID]") + "&#39;));return false;"%>'
                                                                                                ForeColor="Blue" ToolTip="Edit"  CommandName="Select" runat="server" />
                                                                                           
                                                                                       
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trArrival" runat="server" class="dvWhite" >
                                                                                        <td width="75%" valign="top" style="color: Black;height:15px"  align="center">
                                                                                            <asp:Label ID="lblArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Arrival")))%>'></asp:Label>
                                                                                            <%--<asp:Label ID="lblArrival" runat="server" Text='<%# Eval("Arrival","{0:dd MMM yy HHmm}")%>'></asp:Label>--%>

                                                                                            
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trBerthing" runat="server" class="dvWhite">
                                                                                        <td width="75%" valign="top" style="color:Black;height:15px" align="center">
                                                                                           <asp:Label ID="lblBerthing" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Berthing")))%>'></asp:Label>
                                                                                           <%--<asp:Label ID="lblBerthing" runat="server" Text='<%# Eval("Berthing","{0:dd MMM yy HHmm}")%>'></asp:Label>--%>
                                                                                            
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trDeparture" runat="server" class="dvWhite">
                                                                                        <td width="75%" valign="top" style="color: Black;height:15px"  align="center">
                                                                                           <asp:Label ID="lblDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Departure")))%>'></asp:Label>
                                                                                           <%--<asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure","{0:dd MMM yy HHmm}")%>'></asp:Label>--%>
                                                                                            
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" style="color: Black; height: 20px;" align="center">
                                                                                            <asp:ImageButton ID="Iswarrisk" Enabled = "false" Visible="false" ToolTip ="War risk" runat="server" ImageUrl="~/Images/Soldier.png" Text="WarRisk" />
                                                                                             <asp:ImageButton ID="IsShipCraneReq" Text="ShipCrane Req." ToolTip="ShipCrane Req." Enabled = "false" Visible="false" ImageUrl="~/Images/shipcrane2.png" runat="server" />    
                                                                                              <asp:ListView ID="lstVetting" runat="server"  DataKeyNames="Vetting_ID" GroupItemCount="6" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1"  OnItemDataBound="lstVetting_ItemDataBound" >
                                                                                             <LayoutTemplate >
                                                                                            <table runat="server" id="table1" >
                                                                                                <tr runat="server" id="groupPlaceHolder1" ></tr>
                                                                                               
                                                                                            </table>
                                                                                            </LayoutTemplate>
                                                                                            <GroupTemplate>
                                                                                                <tr>
                                                                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                                                </tr>
                                                                                           </GroupTemplate>
                                                                                            <ItemTemplate>
                                                                                         
                                                                                                <td id="Td1" runat="server">
                                                                                               
                                                                                            <asp:ImageButton ID="imgPlannedVetting" runat="server" Text="Update" 
                                                                                                ForeColor="Black" ImageUrl="~/Images/VET_VesselMove.png" Height="18px" OnClientClick= '<%#"OpenVetting((&#39;" + Eval("[Vetting_ID]") +"&#39;));return false;"%>'  ></asp:ImageButton>
                                                                                                </td>
                                                                                           
                                                                                            </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color: Black; height: 10px;" align="center">
                                                                                            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text='<%#Eval("Port_Call_Status")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                        <tr>
                                                                                                                                                       
                                                                                            <td  valign="top" style="color: Black; height: 20px;" align="center">
                                                                                                <asp:Label ID="lblCharter" Text='<%#Eval("CSUPPLIER")%>' runat="server" ForeColor ="Green" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
    
                                                                                    <tr>
                                                                                        <td  valign="top" style="color: Black;height: 20px; " align="center">
                                                                                            <asp:Label ID="lblOwner" runat="server"  Text='<%#Eval("OSUPPLIER")%>' ForeColor="BlueViolet" ></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color: Black; height: 20px;" align="center">
                                                                                            <asp:Label ID="lbPortRemarks" runat="server" Text='<%#Eval("Port_Remarks")%>' ForeColor="Brown" ></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    <tr>
                                                                                        <td width="75%" style="color: Black; font-weight: bold; height: 20px;">
                                                                                            <asp:ImageButton ID="imgPurchasebtn" runat="server"  Enabled="false" CommandArgument='<%#Eval("[Port_Call_ID]")%>'
                                                                                                ForeColor="Black" ImageUrl="~/Images/supply_icon.jpg" ToolTip="Purchase Order"
                                                                                                Height="16px"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="imgPoAgencybtn" Visible="false" runat="server"  Enabled="false"
                                                                                                CommandArgument='<%#Eval("[Port_Call_ID]")%>' ForeColor="Black" ImageUrl="~/Images/Agency_PO.jpg"
                                                                                                ToolTip="Agency PO" Height="16px"></asp:ImageButton>

                                                                                        </td>
                                                                                        </tr>
                                                                                         <tr>
                                                                                        <td width="75%" style="color: Black; height: 15px;">
                                                                                            <asp:ImageButton ID="ImgView" runat="server" Text="Update"  Enabled="true"  CssClass="ImgDisabled"
                                                                                                ForeColor="Black" ImageUrl="~/Images/CrewChange.bmp" ToolTip='<%# Eval("CrewOff") %>' OnClientClick='<%#"OpenCrewOnOff((&#39;" + Eval("[Port_Call_ID]") +"&#39;));return false;"%>'
                                                                                                Height="12px"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="imgWorkList" runat="server" Enabled="false" CssClass="ImgDisabled" 
                                                                                                ForeColor="Black" ImageUrl="~/Images/alert.jpg" ToolTip="Work List" Height="12px">
                                                                                            </asp:ImageButton>
                                                                                            <asp:ImageButton ID="imgAgencyWork" runat="server" Text="Update" Enabled="false"  CssClass="ImgDisabled"
                                                                                                ForeColor="Black" ImageUrl="~/Images/Agency_Work.jpg" ToolTip='<%# Eval("Agency_Work") %>'
                                                                                                Height="12px"></asp:ImageButton>

                                                                                           
                                                                                        </td>
                                                                                    </tr>
                                                                                      <tr>
                                                                                        <td width="75%" style="color: Black; height: 15px;">
                                                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnClientClick='<%#"OpenScreen((&#39;" + Eval("[Vessel_ID]") +"&#39;),(&#39;"+ Eval("[Port_Call_ID]") + "&#39;));return false;"%>'
                                                                                                CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Vessel_ID]") %>' ForeColor="Black"   Visible='<%# uaEditFlag %>'
                                                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="15px"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="lbtnDelete_Click" 
                                                                                                CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Vessel_ID]") %>' ForeColor="Black"  Visible='<%# uaDeleteFlage %>'
                                                                                                ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                                                ImageUrl="~/Images/delete.png" Height="15px"></asp:ImageButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>

                                                                            </div>
                                                                        </td>
                                                                        <asp:Literal ID="litRowEnd" runat="server"></asp:Literal>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                    </tr>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        
                                                            <td id="tdTest" runat="server" visible='<%#uaEditFlag%>'>
                                                             <table width="100px" runat="server" id="table2" style="height: 100px">
                                                                    <tr>
                                                                        <td width="50%" style="color: Black; font-weight: bold" align="right">
                                                                            <asp:DropDownList ID="DDLPort" runat="server" Width="120px" Visible='<%#uaEditFlag%>' CssClass="txtInput">
                                                                            </asp:DropDownList>
                                                                       </td>
                                                                        <td width="50%" style="color: Black; font-weight: bold" align="right">
                                                                          
                                                                                   <asp:Button ID="btnsave" runat="server" Text="Add This Port" Visible='<%#uaEditFlag%>' OnClick="btnsave_Click"
                                                                                CommandArgument='<%#Eval("[Vessel_ID]") %>' />
                                                                     </td>
                                                                    </tr>

                                                                </table>
                                                            </td>
                                                            </tr>
                                                            </table>
                                                             
                                                               
                                                                </div>
                                                               
                                                            </td>
                                                          
                                                        </tr>
                                                    </table>

                                                </div>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%-- Label used for showing Error Message --%>
                                                <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="NO RECORDS FOUND"
                                                    Visible="false">
                                                </asp:Label>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        </ContentTemplate></asp:UpdatePanel>
                                    </div>



                                </td>
                            </tr>
                        </table>
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </center>
</asp:Content>
