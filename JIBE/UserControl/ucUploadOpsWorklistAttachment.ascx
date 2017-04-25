<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUploadOpsWorklistAttachment.ascx.cs" Inherits="UserControl_ucUploadOpsWorklistAttachment" %>
<script id="UploadifySettings" type="text/javascript">
    function SetParameters() {
        var Worklist_ID = $('[id$=HiddenField_Worklist_ID]').val();
        var Vessel_ID = $('[id$=HiddenField_Vessel_ID]').val();
        var UserID = $('[id$=HiddenField_UserID]').val();
        var WL_Office_ID = $('[id$=HiddenField_WL_Office_ID]').val();

        UploadSettings(Worklist_ID, Vessel_ID, WL_Office_ID, UserID);
    }
    function UploadSettings(Worklist_ID, Vessel_ID, WL_Office_ID, UserID) {
       
        $('#DocumentInput').uploadify({
            'uploader': '../../scripts/uploadify/uploadify.swf',
            'script': '../../UserControl/ucUploadOpsWorklistAttachment_Handler.ashx',
            'scriptData': { 'Worklist_ID': Worklist_ID, 'Vessel_ID': Vessel_ID, 'WL_Office_ID': WL_Office_ID, 'UserID': UserID },
            'multi': false,
            'auto': true,
            'width': 111,
            'height': 22,
            'wmode': 'transparent',
            'folder': '/Uploads/Technical/',
            'cancelImg': '../../scripts/uploadify/img/cancel.png',
            'buttonImg': '../../images/AddAttachment.png',
            'onCancel': function (event, queueID, fileObj, data) { },
            'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=btnLoadFiles]').trigger('click'); }
        });
    } 
</script>
<asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <div>
            <input id="DocumentInput" name="DocumentInput" type="file" />
        </div>
        <div id="dvReqAttachUploader" style="text-align: right; display: none;">
            <asp:HiddenField ID="HiddenField_Worklist_ID" runat="server" />
            <asp:HiddenField ID="HiddenField_Vessel_ID" runat="server" />         
            <asp:HiddenField ID="HiddenField_WL_Office_ID" runat="server" />         
            <asp:HiddenField ID="HiddenField_UserID" runat="server" />         

            <asp:Button ID="btnLoadFiles" runat="server" OnClick="btnLoadFiles_Click" Visible="true" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
