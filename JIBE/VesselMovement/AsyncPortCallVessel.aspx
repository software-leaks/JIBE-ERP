<%@ Page Title="Vessel Movement" Language="C#" MasterPageFile="~/Site.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="AsyncPortCallVessel.aspx.cs" EnableEventValidation="false"
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
    <script src="../Scripts/VMScript.js" type="text/javascript"></script>
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
         .buttonnew
            {
            background: url(../images/vmaddnew.png) no-repeat;
            cursor:pointer;
                        border: none;
                         width: 118px;
                         height:29px
            }
             .buttonthis
            {
            background: url(../images/vmaddthis.png) no-repeat;
            cursor:pointer;
                        border: none;
                         width: 118px;
                         height:29px
            }
              .buttonthisd
            {
            background: url(../images/vmaddthisd.png) no-repeat;
            cursor:pointer;
                        border: none;
                         width: 118px;
                         height:29px
            }
    </style>
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
    <script language="javascript" type="text/javascript">




        function onSucc_LoadFunction(retval, prm) {
            try {

                document.getElementById(prm[0]).innerHTML = retval;

            }
            catch (ex)
    { }
        }

      

        function Onfail(msg) {

            //alert(msg._message);
        }

        function Deleteport(portId, vesselid, userid) {
            var con = confirm("Are you sure want to delete???");
            if (con == true) {
                Delete_Port(portId, vesselid, userid);
                alert('Port has been deleted');
                setTimeout(AsyncGet_PortCallDetails(vesselid, "dynamicPortDiv_" + vesselid, userid), 1000);
                //  location.reload();
                //                sleep(1000);
                //                AsyncGet_PortCallDetails(vesselid, "dynamicPortDiv_" + vesselid, userid);
                //                clearTimeout(); 
            }

        }

        function sleep(milliseconds) {
            var start = new Date().getTime();
            for (var i = 0; i < 1e7; i++) {
                if ((new Date().getTime() - start) > milliseconds) {
                    break;
                }
            }
        }
        function funNext(vesselid, userid) {
            funNext(vesselid, userid);

        }
        function funPrev(vesselid, userid) {
            funPrev(vesselid, userid);

        }
        function AddThisPort(vesselid) {
            var user = '<%= Session["USERID"] %>';
            var e = document.getElementById("ddlPort_" + vesselid);
            var portId = e.options[e.selectedIndex].value;
            var portName = e.options[e.selectedIndex].text;
            var service = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'AddThisPort', false, { "vesselid": vesselid, "portId": portId, "portName": portName, "userid": user }, onSucc_LoadFunction, Onfail);
            VM_PortDetails3 = service.get_executor();
            alert('Port has been Added Successfully');
           AsyncGet_PortCallDetails(vesselid, "dynamicPortDiv_" + vesselid, user);
         


        }

        function funNext(vesselid, userid) {

            for (var i = 0; i < 2; i++) {
                if (i == 0) {
                    var service = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'funNext', false, { "vesselid": vesselid, "userid": userid }, onSucc_LoadFunction, Onfail, new Array("dynamicPortDiv_" + vesselid));
                    VM_PortDetails4 = service.get_executor();
                }
                else {
                    AsyncGet_PortCallDetails(vesselid, "dynamicPortDiv_" + vesselid, userid);
                }
              
            }
        }

        function funPrev(vesselid, userid) {

            for (var j = 0; j < 2; j++) {
                if (j == 0) {
                    var service = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'funPrev', false, { "vesselid": vesselid, "userid": userid }, onSucc_LoadFunction, Onfail, new Array("dynamicPortDiv_" + vesselid));
                    VM_PortDetails5 = service.get_executor();
                }
                else {
                    AsyncGet_PortCallDetails(vesselid, "dynamicPortDiv_" + vesselid, userid);
                }
            }
           
          //  AsyncGet_PortCallDetails(vesselid, "dynamicPortDiv_" + vesselid, userid);
          
           
        }
        function onSucess(result) {
            alert(result);
        }

        function onError(result) {
            alert('Cannot process your request at the moment, please try later.');
        }

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
            OpenPopupWindowBtnID('PortCallTemplate', 'Vessel Movement', url, 'popup', 400, 1200, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenCrewList(VID) {
            var url = '../Crew/CrewList_PhotoView.aspx?vcode=' + VID;
            window.open(url, "_blank");
        }

        function OpenPCallDetail(PCID, VID, PID) {
            var url = 'Port_Call_Report.aspx?PCID=' + PCID + '&VID=' + VID + '&PID=' + PID;
            OpenPopupWindowBtnID('Port_Call_Report', 'Vessel Movement', url, 'popup', 900, 1200, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }


        function OpenScreen(ID, Job_ID) {
            //            var vesselID = document.getElementById("ctl00_MainContent_DDLVesselFilter").selectedIndex;
            //            var VID = document.getElementById("ctl00_MainContent_DDLVesselFilter").options[vesselID].value;
            var url = 'PortCall_Entry.aspx?ID=' + ID + '&StatusID=' + Job_ID;
            OpenPopupWindowBtnID('PortCall_Entry', 'Vessel Movement', url, 'popup', 500, 800, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenScreen1(ID, Job_ID) {
            //            var vesselID = document.getElementById("ctl00_MainContent_DDLVesselFilter").selectedIndex;
            //            var VID = document.getElementById("ctl00_MainContent_DDLVesselFilter").options[vesselID].value;
            var url = 'PortCall_Entry.aspx?ID=' + ID + '&StatusID=' + Job_ID;
            OpenPopupWindowBtnID('PortCall_Entry', 'Vessel Movement', url, 'popup', 500, 800, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
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


        function meFunction1() {
            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var body = document.body,
             html = document.documentElement;

            var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);

            var w = (size.width);
            var y = w * 0.85;

            var user = '<%= Session["USERID"] %>';
            var pageURL = '<%=  Session["PageURL"] %>';
            var val = document.getElementById("ctl00_MainContent_hdVesselId").value;
            var vID = new Array();
            vid = val.split(",");
            var divId = '';
            for (var i = 0; i < vid.length; i++) {

                divId = "dynamicPortDiv_" + vid[i];

                document.getElementById(divId).setAttribute("style", "width:" + y + "px");

                document.getElementById(divId).style.overflow = scroll;

                AsyncGet_PortCallDetails(vid[i], divId, user, pageURL);
            }

            //            AsyncGet_PortCallDetails(255);
            //            AsyncGet_PortCallDetails(3);
        }

        function OpenCrewOnOff(PCID) {
            var url = 'Port_Call_CrewOnOff.aspx?PCID=' + PCID;
            OpenPopupWindowBtnID('Port_Call_Report', 'Vessel Movement', url, 'popup', 600, 900, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }

        function OpenVesselArrival(ID) {

            var url = 'PortCall_VesselArrival_Reports.aspx?ID=' + ID;
            OpenPopupWindowBtnID('PortCall_Entry', 'Vessel Arrival Report', url, 'popup', 800, 1200, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
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
        
        .button
        {
            background: url(../Images/loaderbar.gif) no-repeat;
            cursor: pointer;
            border: none;
        }
    </style>
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
            <div id="dvpage-content" class="page-content-main" style="padding: 1px; overflow: auto">
                <asp:UpdatePanel ID="Update1" runat="server">
                    <ContentTemplate>
                        <table width="100%" cellpadding="2" cellspacing="1">
                            <tr>
                                <td align="right" style="width: 10%">
                                    Fleet Name :
                                </td>
                                <td align="left" style="width: 15%">
                                    <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                        Height="150" Width="160" />
                                </td>
                                <td align="right" style="width: 10%">
                                    Vessel Name :
                                </td>
                                <td align="left" colspan="2" style="width: 15%">
                                    <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                        Height="200" Width="160" />
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                        ImageUrl="~/Images/SearchButton.png" />
                                    &nbsp;
                                    <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                        ImageUrl="~/Images/Refresh-icon.png" />&nbsp;
                                    <asp:ImageButton ID="btnImgPrint" runat="server" ImageUrl="~/Images/Printer.png"
                                        Width="20px" Height="20px" Text="Print Preview" ToolTip="Print" OnClick="btnPrintDPL_Click" />
                                    <asp:ImageButton ID="imgHistory" runat="server" Height="20px" Width="20px" Visible="false"
                                        OnClientClick="OpenPopUp('PortCallHistory.aspx');return false;" ToolTip="Port Call History"
                                        ImageUrl="~/Images/history.png" />&nbsp;
                                    <asp:ImageButton ID="imgCost" runat="server" Height="20px" Width="20px" Visible="false"
                                        OnClientClick="OpenPopUp('Port_Cost.aspx');return false;" ToolTip="Port Cost"
                                        ImageUrl="~/Images/dollar.png" />&nbsp;
                                    <asp:ImageButton ID="btnAlerts" runat="server" Height="20px" Width="20px" ImageUrl="~/Images/alert.jpg"
                                        ToolTip="Port Call Alerts" Visible="false" OnClientClick="OpenPopUp('PortCallAlert.aspx');return false;" />
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" Width="20px" ToolTip="War Risk Ports"
                                        ImageUrl="~/Images/Soldier.png" Text="War Risk Ports" Visible='<%# uaEditFlag %>'
                                        OnClientClick="OpenPopUpWar('Port_War_Risk.aspx');return false;" />
                                    <span id="Message" runat="server"></span>
                                </td>
                                <td align="right" style="width: 10%">
                                    <asp:TextBox ID="txtFrom" Visible="false" runat="server" Width="1px" BackColor="#FFFFCC"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
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
                <table align="left">
                    <tr>
                        <td>
                            <div id="divRTP" style="text-align: center; border: 1px solid #cccccc; font-family: Tahoma;
                                font-size: 11px;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div style="float: left; text-align: left; width: 100%; border: 1px solid #cccccc;
                                            font-family: Tahoma; font-size: 11px; background-color: #ffffff; overflow-x: scroll">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Literal runat="server" ID="divLiteral"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <div id="dynamicPanel" height="260px" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                                                      
                                        </div> </td> </tr> </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hdVesselId" />
           <asp:HiddenField ID="hiddenCount" runat="server" Value="0"/>       
    </center>
</asp:Content>
