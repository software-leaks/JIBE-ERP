<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUploader.aspx.cs" Inherits="Technical_PMS_FileUploader"    %>



<html>
<head runat="server">
    <title></title>
     <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <link href="../../Scripts/JsTree/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JsTree/libs/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/JsTree/jstree.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min1.11.0.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui1.11.0.css" rel="stylesheet" type="text/css" />
     <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
   
</head>
<body>
    <form id="form1" runat="server">
     <script type="text/javascript" >
         function UpdatePage(sender, args) {

             var fileInfo = document.getElementById('hdnFileInfo');
             var data = '<%=Request.QueryString["ImageType"]%>';
             var filename = args.get_fileName();
           
             if (data == "Image") {
                 parent.UpdatePage('<%=Session["AppAttach_" + Request.QueryString["ItemID"].ToString()]%>');
             }
             else if (data == "ProductDetailImage") {
                 parent.UpdateDetailsPage(fileInfo.value);
             }
             return false;
         }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobberDetails" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <tlk4:AjaxFileUpload ID="ImageUploader" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobberDetails" OnUploadComplete="ImageUploader_OnUploadComplete"
                                MaximumNumberOfFiles="1"  OnClientUploadComplete="UpdatePage"  />
                              
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnFileInfo" runat="server" ViewStateMode="Enabled" ClientIDMode="Static"/>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>

</body>
</html>
