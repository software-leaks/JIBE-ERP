<%@ Page Title="FAQ List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LMS_FAQ_Topic.aspx.cs" Inherits="LMS_LMS_FAQ_Topic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        var lastExecutor = null;
        $(document).ready(function () {
           
            var USERCOMPANYID = '<%= Session["USERCOMPANYID"]%>';
            var USERID = '<%= HttpContext.Current.Session["USERID"] %>';
            var Jibe_CRM = '<%=ConfigurationManager.AppSettings["Jibe_CRM"]%>';
            //var url = document.URL;
            var Topic_ID = '<%=this.Request.QueryString["Topic_ID"]%>';
            var USERNAME = '<%= Session["USERFULLNAME"]%>';
             var Edit=<%=UserAccess_Edit%>;
            var Delete = <%=UserAccess_Delete%>;
            document.getElementById("ctl00_MainContent_ifrmFAQTopic").src = Jibe_CRM + 'HelpDesk/LMS_Topic_List.aspx?Topic_ID=' + Topic_ID + '&USERID=' + USERID + '&USERCOMPANYID=' + USERCOMPANYID + '&USERNAME=' + USERNAME + '&Edit=' + Edit + '&Delete=' + Delete;
            return false;
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvFAQTopic" style="width: 100%; height: 810px">
        <iframe id="ifrmFAQTopic" runat="server" style="width: 100%; height: 810px; border: 0;">
        </iframe>
    </div>
</asp:Content>

