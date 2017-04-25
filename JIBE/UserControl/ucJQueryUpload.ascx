<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucJQueryUpload.ascx.cs" Inherits="UserControl_ucJQueryUpload" %>
<script id="UploadifySettings" type="text/javascript">
//    $(document).ready(function () {
//        EmailAttachmentUploadSettings(strAttachUploadpath);
//    });

    function SetParameters(strUploadpath, JQueryUploadHandler) {
//        
//        $('#InputFileUpload').uploadify({
//            'uploader': '../../scripts/uploadify/uploadify.swf',
//            'script': '../../UserControl/EmailAttachment.ashx',
//            'scriptData': { 'uploadpath': strUploadpath },
//            'multi': true,
//            'auto': true,

//            'buttonText': 'Add Attachments...',
//            'folder': '/Uploads/EmailAttachments',
//            'cancelImg': '../../scripts/uploadify/img/cancel.png',
//            'onCancel': function (event, queueID, fileObj, data) { $('#divReqsave').show(); },
//            'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=btnLoadFiles]').trigger('click'); }
//        });


        $('#InputFileUpload').uploadify({
            'uploader': '/JIBE/scripts/uploadify/uploadify.swf',
            'script': '/JIBE/UserControl/' + JQueryUploadHandler,
            'scriptData': { 'uploadpath': strUploadpath},
            'multi': true,
            'auto': true,

            'buttonText': 'Add Attachments...',
            'cancelImg': '/JIBE/scripts/uploadify/img/cancel.png',
            'onCancel': function (event, queueID, fileObj, data) { JQueryUploader_onCancel(); },
            'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=btnLoadFiles]').trigger('click'); }
        });
    }

    function JQueryUploader_onCancel() {

    }
    

//    function EmailAttachmentUploadSettings(strUploadpath_) {
//        $('#InputFileUpload').uploadify({
//            'uploader': '../scripts/uploadify/uploadify.swf',
//            'script': '../UserControl/EmailAttachment.ashx',
//            'scriptData': { 'uploadpath': strUploadpath_},
//            'multi': true,
//            'auto': true,
//           
//            'buttonText': 'Add Attachments...',            
//            'cancelImg': '../scripts/uploadify/img/cancel.png',
//            'onCancel': function (event, queueID, fileObj, data) { $('#divReqsave').show(); },
//            'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=btnLoadFiles]').trigger('click'); }
//        });
//    }
        
     
</script>
<asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <div>
            <input id="InputFileUpload" name="InputFileUpload" type="file" />
        </div>
        <div id="dvReqAttachUploader" style="text-align: right; display: none;">            
            <asp:Button ID="btnLoadFiles" runat="server" OnClick="btnLoadFiles_Click" Visible="true" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
