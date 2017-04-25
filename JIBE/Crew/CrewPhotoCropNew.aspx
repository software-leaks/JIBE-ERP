<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewPhotoCropNew.aspx.cs"
    Inherits="CrewPhotoCropNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Crew Photo</title>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Scripts/Jcrop/css/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>--%>
    <script type="text/javascript" src="../Scripts/Jcrop/js/jquery.Jcrop.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#imgCrop').Jcrop({
                onSelect: storeCoords,
                onChange: showPreview,
                aspectRatio: 0.8
            });

            $("body").on("click", "#btnCancel", function () {
                window.parent.jQuery("#dvPopupFrame_dvModalPopupCloseButton").click();
            });
        });
        function storeCoords(c) {
            jQuery('#X').val(c.x);
            jQuery('#Y').val(c.y);
            jQuery('#W').val(c.w);
            jQuery('#H').val(c.h);

            jQuery('#lblDimension').html(c.w + ' x ' + c.h);

        };

        function showPreview(coords) {
            var imgW = $('#imgCrop').width();
            var imgH = $('#imgCrop').height();

            if (parseInt(coords.w) > 0) {
                var rx = 150 / coords.w;
                var ry = 180 / coords.h;
                jQuery('#imgPreview').css({
                    width: Math.round(rx * imgW) + 'px',
                    height: Math.round(ry * imgH) + 'px',
                    marginLeft: '-' + Math.round(rx * coords.x) + 'px',
                    marginTop: '-' + Math.round(ry * coords.y) + 'px'
                });
            }
        }

        function RedirectToCrewDetails(ImgURL, FileName) {
            window.parent.$("#ctl00_MainContent_imgNoPic").hide();
            window.parent.$("#ctl00_MainContent_imgCrewPic").attr("src", ImgURL);
            window.parent.$("#ctl00_MainContent_imgCrewPic").show();

            window.parent.$("#dvPopupFrame_dvModalPopupCloseButton").click();
            alert("Photo uploaded successfully");
        }

        function RedirectToAddEditPage(ImgURL, FileName) {
            window.parent.$("#imgNoPic").hide();
            window.parent.$("#imgCrewPic").attr("src", ImgURL);
            window.parent.$("#imgCrewPic").show();

            window.parent.$("#imgNoPicScreen2").hide();
            window.parent.$("#imgCrewPicScreen2").attr("src", ImgURL);
            window.parent.$("#imgCrewPicScreen2").show();

            window.parent.$("#ctl00_MainContent_hdnCrewPhotoFileName").val(FileName);
            window.parent.$("#dvPopupFrame_dvModalPopupCloseButton").click();

            alert("Photo uploaded successfully");
        }
    </script>
    <style type="text/css">
        .panel-crop
        {
            max-height: 570px;
            min-height: 400px;
            min-width: 600px;
            overflow: auto;
            text-align: center;
        }
        #dvPreview
        {
            background-color: White;
            border: 1px solid #c6c6c6;
            position: absolute;
            right: 82%;
            top: 30px;
            width: 150px;
        }
        .jcrop-holder
        {
            border: 1px solid #c6c6c6;
        }
        body
        {
            color: #696969;
            font-family: Tahoma,Tahoma,sans-serif,vrdana;
        }
    </style>
</head>
<body class="body-css">
    <form id="form1" runat="server">
    <div>
        <fieldset id="flsetpnlUpload" runat="server">
            <asp:Panel ID="pnlUpload" runat="server" CssClass="panel-crop">
                <table border="0" class="dataTable" cellpadding="2">
                    <tr>
                        <td colspan="2">
                            Upload:
                            <asp:FileUpload ID="Upload" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                        </td>
                        <td style="text-align: left;">
                            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                            <asp:Button ID="btnCancel" runat="server" ClientIDMode="Static" Text="Cancel" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblError" runat="server" Visible="false" Style="color: Red; font-size: 12px;" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <asp:Panel ID="pnlCrop" runat="server" Visible="false" CssClass="panel-crop">
            <table width="100%" border="0">
                <tr>
                    <td style="vertical-align: top;" width="22%">
                        <div id="dvPreview">
                            <div style="height: 180px; width: 150px; overflow: hidden; z-index: 1;">
                                <asp:Image ID="imgPreview" runat="server" CssClass="img-crop" />
                            </div>
                            <asp:HiddenField ID="X" runat="server" />
                            <asp:HiddenField ID="Y" runat="server" />
                            <asp:HiddenField ID="W" runat="server" />
                            <asp:HiddenField ID="H" runat="server" />
                            <asp:Button ID="btnCancelCrop" runat="server" Text="Cancel" OnClick="btnCancelCrop_Click" />
                            <asp:Button ID="btnCrop" runat="server" Text=" Upload " OnClick="btnCrop_Click" />
                        </div>
                        <div style="left: 10px; position: absolute;">
                            <b>Dimension:&nbsp;<asp:Label ID="lblDimension" runat="server">-</asp:Label></b>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <div id="outer">
                            <div class="jcExample">
                                <div class="article">
                                    <span style="font-size: 15px; font-weight: bold; margin-bottom: 10px;">Crop image and
                                        Upload. Aspect ratio locked.</span>
                                    <!-- This is the image we're attaching Jcrop to -->
                                    <div style="max-width: 500px">
                                        <asp:Image ID="imgCrop" runat="server" CssClass="img-crop" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlCropped" runat="server" Visible="false">
            <asp:Image ID="imgCropped" runat="server" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
