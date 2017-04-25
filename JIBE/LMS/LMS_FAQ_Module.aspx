<%@ Page Title="FAQ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_FAQ_Module.aspx.cs" Inherits="LMS_LMS_FAQ_Module" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        var lastExecutor = null;
        $(document).ready(function () {

            var USERCOMPANYID = '<%= Session["USERCOMPANYID"]%>'; 
            var USERID = '<%= HttpContext.Current.Session["USERID"] %>';
            var Jibe_CRM = '<%=ConfigurationManager.AppSettings["Jibe_CRM"]%>';
            //var URL = '<%=ConfigurationManager.AppSettings["APP_URL"]%>';
            var pos = document.URL.toUpperCase().indexOf(('<%=ConfigurationManager.AppSettings["APP_NAME"]%>').toUpperCase());
            var len = ('<%=ConfigurationManager.AppSettings["APP_NAME"]%>').length + 1;
            //var URL = document.URL.substr(0,pos + len)
            var AppName = document.getElementById('ctl00_hdnAppName').value;
            if (!window.location.origin) {
  window.location.origin = window.location.protocol + "//" 
    + window.location.hostname 
    + (window.location.port ? ':' + window.location.port: '');
}
             var URL = window.location.origin + '/' + '<%=ConfigurationManager.AppSettings["APP_NAME"]%>'+'/';
             // var URL = document.URL.substr(0,pos + len)
            var USERNAME = '<%= Session["USERFULLNAME"]%>';
            var Edit=<%=UserAccess_Edit%>;
            var Delete = <%=UserAccess_Delete%>; 
            document.getElementById("ctl00_MainContent_ifrmFAQModule").src = Jibe_CRM + 'HelpDesk/LMS_Module_topic.aspx?USERID=' + USERID + '&USERCOMPANYID=' + USERCOMPANYID + '&URL=' + URL + 'LMS&USERNAME=' + USERNAME + '&Edit=' + Edit + '&Delete=' + Delete;
            return false;
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvFAQModule" style="width: 100%; height: 810px">
        <iframe id="ifrmFAQModule" runat="server" style="width: 100%; height: 810px; border: 0;">
        </iframe>
    </div>
</asp:Content>
