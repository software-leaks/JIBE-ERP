<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUploadCrewContract.ascx.cs" Inherits="UserControl_ucUploadCrewContract" %>


<script id="UploadifySettings" type="text/javascript">
    function SetParameters() {
        var CrewID = $('[id$=HiddenField_CrewID]').val();
        var VoyID = $('[id$=HiddenField_VoyageID]').val();
        var UserID = $('[id$=HiddenField_UserID]').val();
        var StageID = $('[id$=HiddenField_StageID]').val();

        UploadSettings(CrewID, VoyID, StageID, UserID);
    }


    function UploadSettings(CrewID, VoyID, StageID, UserID) {
        $('#DocumentInput').uploadify({
            'uploader': '../scripts/uploadify/uploadify.swf',
            'script': '../UserControl/ucUploadCrewContract_Handler.ashx',
            'scriptData': { 'crewid': CrewID, 'voyid': VoyID, 'stageid': StageID, 'userid': UserID },
            'multi': false,
            'auto': true,
            'width':200,
            'height': 26,
            'fileExt': '*.pdf',
            'fileDesc': 'PDF Files',
            'wmode':'transparent',
            'folder': '/Uploads/CrewDocuments/',
            'cancelImg': '../scripts/uploadify/img/cancel.png',
            'buttonImg': '../images/UploadDigiSigned.png',
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
            <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
            <asp:HiddenField ID="HiddenField_VoyageID" runat="server" />         
            <asp:HiddenField ID="HiddenField_UserID" runat="server" />         
            <asp:HiddenField ID="HiddenField_StageID" runat="server" />         

            <asp:Button ID="btnLoadFiles" runat="server"
                OnClick="btnLoadFiles_Click" Visible="true" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
