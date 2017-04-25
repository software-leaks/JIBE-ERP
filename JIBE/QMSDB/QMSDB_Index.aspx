<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="QMSDB_Index.aspx.cs"
    Inherits="QMSDB_Index" Title="Document Management System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <script type="text/javascript" src="JS/xtreeview.js"></script>
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

        }


        function RealoadParent()
         {

            document.getElementById('docPreview').src = "#";
        }

    </script>
    <script src="JS/common.js" type="text/javascript"></script>
    <script src="js/VwdCmsSplitterBar.js" type="text/javascript"></script>
    <link href="css/Main.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 50px;">
                </td>
                <td style="text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Quality Management System"></asp:Label>
                </td>
                <td style="width: 80px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                        <td>
                       <%-- <asp:FileUpload ID="UploadAttachments1"  runat="server"> </asp:FileUpload> 
                        <asp:Button ID="Button1" runat="server" Text="Preview"   onclick="previewbutton_Click" />--%>
                        </td>
                          <td style="width: 40px">
                                <asp:ImageButton ID="imgcheckList" runat="server" Text="Refresh" ImageUrl="~/Images/DocTree/msg.gif" ToolTip ="Check List"
                                    OnClick="imgcheckList_Click" />
                            </td>
                            <td style="width: 40px">
                                <asp:ImageButton ID="imdbtnPending" runat="server" Text="Refresh" ImageUrl="~/Images/DocTree/msg.gif" ToolTip ="Pending Approval List"
                                    OnClick="imdbtnPending_Click" />
                            </td>
                            <td style="width: 40px">
                                <asp:ImageButton ID="btnRefresh" runat="server" Text="Refresh" ImageUrl="~/Images/refresh.png"  ToolTip ="Refresh"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMessage" align="center" style="position: absolute; top: 5px; left: 200px;
        display: none;">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    <input id="FieldNameHdn" type="hidden" runat="server" />
    <div style="overflow: auto;">
        <table width="100%" style="border: 2px solid #B0C4DE;">
            <tr>
                <td style="vertical-align: top; background-color: #f8f8f8; width: 250px;">
                    <div style="background-color: #f8f8f8; text-align: left; height: 650px; width: 250px;
                        overflow: auto; z-index: 1; border: 1px solid inset;">
                        <asp:TreeView ID="BrowseTreeView" runat="server" Style="margin-right: 1px" BorderColor="#F3F1CD"
                            Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="Black" EnableViewState="False"
                            ImageSet="XPFileExplorer" NodeIndent="15" AutoGenerateDataBindings="False" OnSelectedNodeChanged="BrowseTreeView_SelectedNodeChanged">
                            <ParentNodeStyle Font-Bold="False" />
                            <HoverNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                            <SelectedNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="0px" VerticalPadding="2px" />
                        </asp:TreeView>
                    </div>
                    <%--   <div>
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:Panel ID="Panel2" runat="server" style ="display:none"  BorderColor="Black" CssClass="skin0"
                            onMouseover="highlightie5(event)" onMouseout="lowlightie5(event)" onClick="jumptoie5(event)">
                            <div class="menuitems">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="menuitems">New Node</asp:LinkButton></div>
                            <div class="menuitems">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="menuitems">Edit Node</asp:LinkButton></div>
                            <hr />
                            <div class="menuitems">
                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="menuitems">Delete Node</asp:LinkButton></div>
                            <hr />
                            <div class="menuitems">
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="menuitems">FAQS</asp:LinkButton></div>
                            <div class="menuitems">
                                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="menuitems">Online Help</asp:LinkButton></div>
                            <hr />
                            <div class="menuitems">
                                <asp:LinkButton ID="LinkButton6" runat="server" CssClass="menuitems">Email Me</asp:LinkButton></div>
                        </asp:Panel>
                        <br />
                        <a href=""></a>
                    </div>--%>
                </td>
                <td style="vertical-align: top;">
                    <iframe id="docPreview" name="docPreview" style="height: 650px; width: 100%; border: 0px;
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
