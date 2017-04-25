<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEmailAttachment.ascx.cs" Inherits="UserControl_ucEmailAttachment" %>
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Scripts/jquery.min.js"></script>
<script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
<script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
<script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
<script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
<link href="../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script id="UploadifySettings" type="text/javascript">


    $(document).ready(function () {
        var EmailID = $('[id$=HiddenField_EmailID]').val();
        var strAttachUploadpath = $('[id$=HiddenField_DocumentUploadPath]').val();


        EmailAttachmentUploadSettings(EmailID, strAttachUploadpath);

    });

    function SetParameters() {

        var EmailID = $('[id$=HiddenField_EmailID]').val();
        var strAttachUploadpath = $('[id$=HiddenField_DocumentUploadPath]').val();

        EmailAttachmentUploadSettings(EmailID, strAttachUploadpath);

    }


    function EmailAttachmentUploadSettings(EmailID_, strUploadpath_) {
        $('#EmailAttachInput').uploadify({
            'uploader': '/JIBE/scripts/uploadify/uploadify.swf',
            'script': '/JIBE/UserControl/EmailAttachment.ashx',
            'scriptData': { 'emailid': EmailID_,'uploadpath': strUploadpath_},
            'multi': true,
            'auto': true,
           
            'buttonText': 'Add Attachments...',
            'folder': '/Uploads/EmailAttachments',
            'cancelImg': '/JIBE/scripts/uploadify/img/cancel.png',
            'onCancel': function (event, queueID, fileObj, data) { $('#divReqsave').show(); },
            'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=btnLoadFiles]').trigger('click'); }
        });
    }



     
</script>
<asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <div>
            <input id="EmailAttachInput" name="EmailAttachInput" type="file" />
        </div>
        <div id="dvReqAttachUploader" style="text-align: right; display: none;">
            <asp:HiddenField ID="HiddenField_DocumentUploadPath" runat="server" />
            <asp:HiddenField ID="HiddenField_EmailID" runat="server" />
         
            <asp:Button ID="btnLoadFiles" runat="server"
                OnClick="btnLoadFiles_Click" Visible="true" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
