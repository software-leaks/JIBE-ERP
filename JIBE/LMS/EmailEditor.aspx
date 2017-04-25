<%@ Page Title="Client Email Editor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmailEditor.aspx.cs" Inherits="LMS_EmailEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
   <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var Edit = document.getElementById("ctl00_MainContent_hdnEdit").value;
        var Jibe_CRM = '<%=ConfigurationManager.AppSettings["Jibe_CRM"]%>';

        var Task_ID = '<%=this.Request.QueryString["ID"]%>';
        var Flag = '<%=this.Request.QueryString["Flag"]%>';
        var UserID = '<%=this.Request.QueryString["UserID"]%>';
        var MailID = '<%=this.Request.QueryString["MailID"]%>';
        var USERNAME = '<%= Session["USERFULLNAME"]%>';
        var USERMAILID = '<%= Session["USERMAILID"]%>';
        var TOCLIENT = '<%=this.Request.QueryString["TOCLIENT"]%>';
        var COMPANYID = '<%= Session["USERCOMPANYID"]%>';
        var NotificationID = '<%=this.Request.QueryString["NotificationID"]%>';

        document.getElementById("ctl00_MainContent_frmMailEditor").src = Jibe_CRM + 'CRM/Task/MailEditor.aspx?ID=' + Task_ID + '&Flag=' + Flag + '&UserID=' + UserID + '&MailID=' + MailID + '&Edit=' + Edit + '&USERNAME=' + USERNAME + '&USERMAILID=' + USERMAILID + '&TOCLIENT=' + TOCLIENT + '&COMPANYID=' + COMPANYID + '&NotificationID=' + NotificationID;
        return false;
    });

    function OnClose(TaskID) {

        try { window.opener.ASync_Get_Remark(TaskID, null, null); } catch (exp) { }
        try { window.opener.Refresh(); } catch (exp) { }
        window.close();
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:HiddenField ID="hdnEdit" runat="server" />
 <div id="dvEmailEditor"  title='Email Editor' style=" width:100%; height: 810px">
        <iframe id="frmMailEditor" runat="server" style="width:100%; height:  810px; border: 0;">
        </iframe>
    </div>
</asp:Content>

