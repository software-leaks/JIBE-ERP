<%@ Page Title="Client DashBoard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Client_Dashboard.aspx.cs" Inherits="LMS_Client_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
   <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var USERCOMPANYID = '<%= Session["USERCOMPANYID"]%>';
            var USERID = '<%= HttpContext.Current.Session["USERID"] %>';
            var USERNAME = '<%= Session["USERFULLNAME"]%>';
            var USERMAILID = '<%= Session["USERMAILID"]%>';
            var Jibe_CRM = '<%=ConfigurationManager.AppSettings["Jibe_CRM"]%>';
            // var url = document.URL;
            if (!window.location.origin) {
                window.location.origin = window.location.protocol + "//"
    + window.location.hostname
    + (window.location.port ? ':' + window.location.port : '');
            }
            var APPURL = window.location.origin + '/' + '<%=ConfigurationManager.AppSettings["APP_NAME"]%>' + '/LMS/';

            var DepID = '<%= Session["JITDEPID"]%>';
            var DepIDAct = '<%= Session["USERDEPARTMENTID"]%>';
            var pos = document.URL.toUpperCase().indexOf(('<%=ConfigurationManager.AppSettings["APP_NAME"]%>').toUpperCase());
            var len = ('<%=ConfigurationManager.AppSettings["APP_NAME"]%>').length + 1;
            var url = document.URL.substr(pos + len);
            document.getElementById("ctl00_MainContent_frmDashboard").src = Jibe_CRM + 'CRM/Task/ClientDashboard.aspx?USERID=' + USERID + '&USERNAME=' + USERNAME + '&USERCOMPANYID=' + USERCOMPANYID + '&USERMAILID=' + USERMAILID + '&DepID=' + DepID + '&DepIDAct=' + DepIDAct + '&URL=' + url + '&APPURL=' + APPURL;
            return false;
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvDashboard"  title='Report issue' style=" width:100%; height: 810px">
        <iframe id="frmDashboard" runat="server" style="width:100%; height:  810px; border: 0;">
        </iframe>
    </div>
</asp:Content>
