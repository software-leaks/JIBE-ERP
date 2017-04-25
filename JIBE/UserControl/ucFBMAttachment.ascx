<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFBMAttachment.ascx.cs"
    Inherits="ucFBMAttachment" %>
<link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../Scripts/jquery.min.js"></script>
<script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
<script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
<script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
<script src="../../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
<link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script id="UploadifySettings" type="text/javascript">

    $(document).ready(function () {

        ReqsnAttachmentUploadSettings();

    });

    function SetParameters() {


        ReqsnAttachmentUploadSettings();
    }

    function showDialog(dialog_) {
        debugger;
        $(dialog_).show('slow');
    };



    function ReqsnAttachmentUploadSettings() {
        $('#ReqAttachInput').uploadify({
            'uploader': '../../scripts/uploadify/uploadify.swf',
            'script': '../../UserControl/FBMAttachment_Handler.ashx',
            'multi': true,
            'auto': true,
            'buttonText': 'Add Attachments...',
            'cancelImg': '../../scripts/uploadify/img/cancel.png',
            'onCancel': function (event, queueID, fileObj, data) { $('#divReqsave').show(); },
            'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=btnLoadFiles]').trigger('click'); }
        });
    }
     
</script>
<asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <div>
            <input id="ReqAttachInput" name="ReqAttachInput" type="file" />
        </div>
        <div id="dvReqAttachUploader" style="text-align: right; display: none; position: absolute;
            right: 20%; top: 40%; border: 2px solid #aabbdd; background-color: White;">
            <asp:Button ID="btnLoadFiles" runat="server" OnClick="btnLoadFiles_Click" Visible="true" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
