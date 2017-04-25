<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Main.aspx.cs"
    Inherits="Main" Title="Quality Management System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/jqueryFileTree.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jqueryFileTree.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('.fileTreeDemo').fileTree({
                root: 'DOCUMENTS',
                script: 'LoadTree.aspx',
                multiFolder: true
            },
                function (id,path, type) {
                    if (type ==1) {
                        //Folder node click
                        document.getElementById('docPreview').src = 'AddNewFile.aspx?Path=' + path + '&id=' + id;
                    }
                    else {
                        //File node click                        
                        document.getElementById('docPreview').src = 'FileLoader.aspx?DocID=' + id;
                    }
                }
            );
        });

        function ChildCallBack() {

        }
        function ChildCallBackDelete() {
            window.location = window.location;
        }            
    </script>
    <style type="text/css">
        img
        {
            padding-right: 2px;
        }
        .box
        {
            width: 80px;
            height: 20px;
            background-color: #efefef;
            border: 1px solid inset;
        }
    </style>
    <script type="text/javascript" language="javascript">

        var lastObj;
        var filename;
        var manuals;
        var manuals1;



        function getImgDirectory(source) {
            return source.substring(0, source.lastIndexOf('/') + 1);
        }
        function OpenMainPop() {
            var selNodeUrl = getSelectedNodeUrl();
            var nodeIdText = document.getElementById("BrowseTreeView_SelectedNode").value;

            if (selNodeUrl == "") { myMessage("Please select a file"); return false; }
            if (selNodeUrl.indexOf('_blank') > 0) { myMessage("Please select a file"); return false; }

            var filePath = selNodeUrl;

            window.open("MainPop.aspx?" + "path=" + filePath + "&SelectedNodeValue=" + nodeIdText, "DOCUMENT", "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=yes, width=650, height=300,top=200,left=300");
            manuals = undefined;
        }



        function print() {
            try {
                var selNodeUrl = getSelectedNodeUrl();
                if (selNodeUrl == undefined || selNodeUrl == "") {
                    myMessage("No Document selected for printing!")
                }
                else {
                    if (selNodeUrl.indexOf('_blank') > 0) { myMessage("Please select a file"); return false; }
                    if (selNodeUrl.indexOf(".pdf") > 0) {
                        myMessage("Please use print button available inside pdf document.")
                    }
                    else {
                        frames["docPreview"].focus();
                        frames["docPreview"].print();
                    }
                }
            }
            catch (ex)
        { }
        }
        function openSearchWindow(url) {
            document.getElementById("docPreview").src = url;

        }
        function DocOpenOnIFrame(url) {
            document.getElementById("docPreview").src = url;
        }
        function resizeFrame() {
            var windowheight = window.screen.height;
            var frame = document.getElementById("docPreview");
            var frameHeight = frame.style.height.replace('px', '');

            //if ( windowheight > 500)  
            //{ 
            //frame.style.height = windowheight - 260 + "px"; }
        }
    </script>
    <script src="JS/common.js" type="text/javascript"></script>
    <script src="js/VwdCmsSplitterBar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <div id="divMessage" align="center" style="position: absolute; top: 5px; left: 200px;
        display: none;">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    <%--   <a id="addnew" style="background-color:White;border-style:solid;border-width:0px;color:Blue;font-weight:bold;font-family:Calibri" 
                 href="javascript:DivStatesaddlink1()" visible="true">Create Folder</a>--%>
    <input id="FieldNameHdn" type="hidden" runat="server" />
    <div style="overflow: auto; margin-bottom: 20px;">
        <table width="100%" cellspacing="5">
            <tr>
                <td style="vertical-align: top; background-color: #f8f8f8; width: 250px; padding: 5;
                    border: 1px solid #c3c3c3">
                    <div style="background-color: #f8f8f8; text-align: left; height: 700px; width: 250px;
                        overflow: auto; z-index: 1; border: 1px solid inset;">
                        <div class="pull-right">
                            
                        </div>
                        <table>
                        <tr>
                        <td style="vertical-align:middle;">
                        <div onclick="DocOpenOnIFrame('AddNewFile.aspx?Path=DOCUMENTS/&id=0');$('.fileTreeDemo').find('.selected').removeClass('selected');" style="cursor:pointer; vertical-align:middle;"><img src="../images/doctree/network.gif" />DOCUMENTS </div>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnRefresh" runat="server" Text="Refresh" ImageUrl="~/Images/refresh.png" Height="20px" />
                        </td>
                        </tr>
                        </table>
                        
                        <div class="fileTreeDemo" style="margin-left:20px">
                        </div>
                    </div>
                </td>
                <td style="vertical-align: top;border: 1px solid #c3c3c3">
                    <iframe id="docPreview" name="docPreview" style="height: 700px; width: 100%; border: 0px;
                        padding: 0; margin: 0;" frameborder="0"></iframe>
                </td>
            </tr>
        </table>
        <input id="hidden_SelectedNodeURL" type="hidden" runat="server" />
        <input id="hdnForCheckINSelectedNode" type="hidden" runat="server" />
    </div>
    <script type="text/javascript" language="javascript">
        resizeFrame();
    </script>
</asp:Content>
