<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DnDUploader.ascx.cs" Inherits="UserControl_DnDUploader" %>
<style type="text/css">
    #dropZone
    {
        border-radius: 5px;
        border: 2px solid #ccc;
        background-color: #eee;
        width: 200px;
        padding: 20px 0;
        text-align: center;
        font-size: 18px;
        color: #555;
        margin: 2px auto;
    }
    
    #dropZone.hover
    {
        border-color: #aaa;
        background-color: #ddd;
    }
    
    #dropZone.error
    {
        border-color: #f00;
        background-color: #faa;
    }
</style>
<script type="text/javascript">
    var dropZone;

    // Initializes the dropZone
    function Initialize_dropZone()
    {
        dropZone = $('#dropZone');
        dropZone.removeClass('error');
        
        // Check if window.FileReader exists to make 
        // sure the browser supports file uploads
        if (typeof (window.FileReader) == 'undefined') {
            dropZone.text('Browser Not Supported!');
            dropZone.addClass('error');
            return;
        }                
        else
        {
            dropZone.text('Drop File Here to Upload.');
        }

        // Add a nice drag effect
        dropZone[0].ondragover = function () {
            dropZone.addClass('hover');
            return false;
        };

        // Remove the drag effect when stopping our drag
        dropZone[0].ondragend = function () {
            dropZone.removeClass('hover');
            return false;
        };

        // The drop event handles the file sending
        dropZone[0].ondrop = function (event) {
            // Stop the browser from opening the file in the window
            event.preventDefault();
            dropZone.removeClass('hover');
            
            // Get the file and the file reader
            var file = event.dataTransfer.files[0];

            // Validate file size (10 MB)
            if (file.size > 10 * 1024 * 1024) {
                dropZone.text('File Too Large!');
                dropZone.addClass('error');
                return false;
            }
            
            // Send the file
             var Var1 = '<%=ConfigurationManager.AppSettings["APP_NAME"].ToString() %>'
            var xhr = new XMLHttpRequest();
            xhr.upload.addEventListener('progress', uploadProgress, false);
            xhr.onreadystatechange = stateChange;
            xhr.open('POST', '/' + Var1 + '/UserControl/DnDUpload_Handler.ashx', true);
            xhr.setRequestHeader('X-FILE-NAME', file.name);
            xhr.setRequestHeader('X-UPLOAD-PATH', $('[id$=HiddenField_UploadPath]').val());
            xhr.setRequestHeader('WORKLIST_ID', $('[id$=HiddenField_Worklist_ID]').val());
            xhr.setRequestHeader('VESSEL_ID', $('[id$=HiddenField_Vessel_ID]').val());
            xhr.setRequestHeader('WL_OFFICE_ID', $('[id$=HiddenField_WL_Office_ID]').val());
            xhr.setRequestHeader('USERID', $('[id$=HiddenField_UserID]').val());
            xhr.send(file);
        };
    }

    // Show the upload progress
    function uploadProgress(event) {
        var percent = parseInt(event.loaded / event.total * 100);
        $('#dropZone').text('Uploading: ' + percent + '%');
    }

    // Show upload complete or upload failed depending on result
    function stateChange(event) {
        if (event.target.readyState == 4) {
            if (event.target.status == 200 || event.target.status == 304) {
                $('#dropZone').text('Upload Complete!');                
                $('[id$=btnUploadCompleted]').trigger('click');
                
            }
            else {
                dropZone.text('Upload Failed!');
                dropZone.addClass('error');                
                $('[id$=btnUploadFailed]').trigger('click');
                
            }
        }
    }
</script>
<div id="dropZone">
    Initialize Control!!
</div>
<div id="dvHiddenUploaderFields" style="text-align: right; display: none;">
    <asp:HiddenField ID="HiddenField_UploadPath" runat="server" />
    <asp:HiddenField ID="HiddenField_Worklist_ID" runat="server" />
    <asp:HiddenField ID="HiddenField_Vessel_ID" runat="server" />         
    <asp:HiddenField ID="HiddenField_WL_Office_ID" runat="server" />         
    <asp:HiddenField ID="HiddenField_UserID" runat="server" />         

    <asp:HiddenField ID="HiddenField_FileID" runat="server" />
    <asp:HiddenField ID="HiddenField_FileName" runat="server" />
    <asp:Button ID="btnUploadCompleted" runat="server" OnClick="btnUploadCompleted_Click"
        Visible="true" />
    <asp:Button ID="btnUploadFailed" runat="server" OnClick="btnUploadFailed_Click" Visible="true" />
</div>
