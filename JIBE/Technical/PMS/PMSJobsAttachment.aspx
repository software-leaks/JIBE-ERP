<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSJobsAttachment.aspx.cs" Inherits="Technical_PMS_PMSJobsAttachment"
    EnableEventValidation="false" Title="Jobs Attachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <link href="../../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
</head>
<body>


<script type="text/javascript">
    var isuploaded = 0;

    $(document).ready(function () {
        $(".ajax__fileupload_selectFileContainer").bind('click', function () {

            var a = $(".ajax__fileupload_queueContainer");
            if (a != null) {
                a.html('');
                isuploaded = 0;
            }

        });
    });


    function cltUploadCompleted(s, e) {

        parent.RefreshPMSJobAttachment(); parent.hideModal('dvCopyJobsPopUp');

    }

   
      
       
    </script>

     

    <form id="frmCopyJobs" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="font-family: Tahoma; font-size: 12px;" >
           <div id="dvPopupAddAttachment" title="Add Attachments" style="width: 500px;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="Label1" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                                <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                    Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"  OnClientUploadComplete="cltUploadCompleted"
                                    MaximumNumberOfFiles="10" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
