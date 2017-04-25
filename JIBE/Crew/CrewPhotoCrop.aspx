<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewPhotoCrop.aspx.cs" Inherits="CrewPhotoCrop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Crew Photo</title>
    <link href="../Scripts/Jcrop/css/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/Jcrop/js/jquery.Jcrop.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('#imgCrop').Jcrop({
                onSelect: storeCoords,
                onChange: showPreview,
                aspectRatio: 0.8
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
    </script>
    <style type="text/css">
        .body-css
        {
            background-color: silver;
            text-align: center;
        }
        .panel-crop
        {
            background-color: #ffffff;
            border: 1px solid gray;
            padding: 10px;
            text-align: center;
            overflow: auto;
            min-height: 400px;
            min-width: 600px;
        }
        #dvPreview
        {
            background-color: White;
            padding: 2px;
            margin: 1px;
            border: 1px solid gray;
            position: fixed;
            top: 10px;
            right: 10px;
        }
    </style>
</head>
<body class="body-css">
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlUpload" runat="server" CssClass="panel-crop">
            <asp:FileUpload ID="Upload" runat="server" />
            <br />
            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
            <asp:Label ID="lblError" runat="server" Visible="false" />
        </asp:Panel>
        <asp:Panel ID="pnlCrop" runat="server" Visible="false" CssClass="panel-crop">
            <div id="outer">
                <div class="jcExample">
                    <div class="article">
                        <h2>
                            Crop image and Upload. Aspect ratio locked.
                        </h2>
                        <!-- This is the image we're attaching Jcrop to -->
                        <div>
                            <asp:Image ID="imgCrop" runat="server" CssClass="img-crop" />
                        </div>
                        <div id="dvPreview">
                            <div style="width: 150px; height: 180px; overflow: hidden; z-index: 1;">
                                <asp:Image ID="imgPreview" runat="server" CssClass="img-crop" />
                            </div>
                            <div>
                                <asp:Label ID="lblDimension" runat="server">Dimension</asp:Label>
                            </div>
                            <asp:HiddenField ID="X" runat="server" />
                            <asp:HiddenField ID="Y" runat="server" />
                            <asp:HiddenField ID="W" runat="server" />
                            <asp:HiddenField ID="H" runat="server" />
                            <asp:Button ID="btnCancelCrop" runat="server" Text="Cancel" OnClick="btnCancelCrop_Click" />
                            <asp:Button ID="btnCrop" runat="server" Text=" Upload " OnClick="btnCrop_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCropped" runat="server" Visible="false">
            <asp:Image ID="imgCropped" runat="server" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
