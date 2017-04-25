<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_Report_Issue.ascx.cs"
    Inherits="UserControl_uc_Report_Issue" %>

<script language="jscript" type="text/javascript">
    function OnFeedback() {
        var USERCOMPANYID = '<%= Session["USERCOMPANYID"]%>'
        var USERID = '<%= HttpContext.Current.Session["USERID"] %>'
        var Module_ID = document.getElementById("ctl00_LoginView1_uc_Report_Issue_hdf_Module_Id").Value;
        OpenPopupWindowBtnID('POP__Task', '', 'http://seachange.dyndns.info/SPM/Task/ReportsBugToSPM.aspx?Module_ID=" + hdf_Module_Id.Value + "&USERCOMPANYID=" + USERCOMPANYID + "&USERID=" + USERID + "', 'popup', 360, 650, null, null, false, false, true, false, '');
        return false;
    }    
</script>
<div>
    <asp:LinkButton ID="ImgReportBug" ToolTip="Feedback to Jibe team" CssClass="Link-Button-Image" ForeColor="White"
        runat="server" OnClick="ImgReportBug_Click" Text="Report Issue"></asp:LinkButton>
    <asp:HiddenField ID="hdf_Module_Id" runat="server" />
</div>
