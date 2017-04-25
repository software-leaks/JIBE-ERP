<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPurcAttachment.ascx.cs"
    Inherits="ucPurcAttachment" %>
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Scripts/jquery.min.js"></script>
<script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
<script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
<script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
<script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
<link href="../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script id="UploadifySettings" type="text/javascript">


    $(document).ready(function () {
        var VesselID = $('[id$=HiddenField_VesselID]').val();
        var CurrentUserID = $('[id$=HiddenField_UserID]').val();
        var ReqsnNo = $('[id$=HiddenField_Reqsn]').val();
        var strAttachUploadpath = $('[id$=HiddenField_DocumentUploadPath]').val();
        var SuppCode = $('[id$=HiddenField_SuppCode]').val();

        ReqsnAttachmentUploadSettings(VesselID, strAttachUploadpath, CurrentUserID, ReqsnNo,SuppCode);

    });

    function SetParameters() {

        var VesselID = $('[id$=HiddenField_VesselID]').val();
        var CurrentUserID = $('[id$=HiddenField_UserID]').val();
        var ReqsnNo = $('[id$=HiddenField_Reqsn]').val();
        var strAttachUploadpath = $('[id$=HiddenField_DocumentUploadPath]').val();
        var SuppCode = $('[id$=HiddenField_SuppCode]').val();

        ReqsnAttachmentUploadSettings(VesselID, strAttachUploadpath, CurrentUserID, ReqsnNo,SuppCode);

    }

    function showDialog(dialog_) {
        debugger;
        $(dialog_).show('slow');
    };





    function ReqsnAttachmentUploadSettings(VesselID_, strUploadpath_, UserID_, ReqsnNo_, SuppCode_) {
        $('#ReqAttachInput').uploadify({
            'uploader': '../scripts/uploadify/uploadify.swf',
            'script': '../UserControl/PURC_ReqsnAttachment_Handler.ashx',
            'scriptData': { 'vesselid': VesselID_, 'userid': UserID_, 'uploadpath': strUploadpath_, 'reqsnno': ReqsnNo_,'suppcode':SuppCode_ },
            'multi': true,
            'auto': true,
            'buttonText': 'Add Attachments...',
            'folder': '/Uploads/purchase',
            'cancelImg': '../scripts/uploadify/img/cancel.png',
            'onCancel': function (event, queueID, fileObj, data) { $('#divReqsave').show(); },
            'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=btnLoadFiles]').trigger('click'); }
        });
    }

    function CheckReqsn() {

        var ReqsnNo = $('[id$=HiddenField_Reqsn]').val();
        if (ReqsnNo == "") {
            alert("Please select Reqsn number!")
            return false;
        }
        return true;
    }

     
</script>
<asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <div>
            <input id="ReqAttachInput" name="ReqAttachInput" type="file" />
        </div>
        <div id="dvReqAttachUploader" style="text-align: right; display: none; position: absolute;
            right: 20%; top: 40%; border: 2px solid #aabbdd; background-color: White;">
            <asp:HiddenField ID="HiddenField_DocumentUploadPath" runat="server" />
            <asp:HiddenField ID="HiddenField_VesselID" runat="server" />
            <asp:HiddenField ID="HiddenField_UserID" runat="server" />
            <asp:HiddenField ID="HiddenField_Reqsn" runat="server" />
            <asp:HiddenField ID="HiddenField_SuppCode" runat="server" />
            <asp:Button ID="btnLoadFiles" runat="server" OnClientClick="return CheckReqsn()"
                OnClick="btnLoadFiles_Click" Visible="true" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
