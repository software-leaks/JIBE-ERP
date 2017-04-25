<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Attachment.aspx.cs"
    Inherits="PO_LOG_PO_Log_Attachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">



        function DocOpen(docpath) {
            window.open(docpath);
        }

        function previewDocument(docPath) {
            document.getElementById("ifrmDocPreview").src = docPath;
        }

        function getImageopen(str) {
            window.open(str, "file", "menubar=0,resizable=0,width=750,height=550,resizeable=yes");
        }




        function fn_OnClose(sender, arg) {

            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }


    </script>
    <style type="text/css">
        .tdh
        {
            text-align: right;
            padding: 1px 3px 1px 3px;
            width: 250px;
        }
        .tdd
        {
            text-align: left;
            padding: 1px 3px 1px 3px;
            width: 150px;
        }
    </style>
    <title></title>
</head>
<body style="background-color: White; font-size: 11px; font-family: Tahoma;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="scpMgr" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="text-align: left; padding: 10px">
            <asp:Label ID="lblReqsnNumber" runat="server" Style="font-size: 12px; font-weight: bold;
                color: Navy" />
        </div>
        <asp:UpdatePanel ID="updGridAttach" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" Style='display: none'
                    runat="server" />
                <div style="padding-top: 2px; padding-bottom: 2px; width: 100%">
                    <div>
                        <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                        <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                            Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                            OnClientUploadComplete="fn_OnClose" />
                    </div>
                    <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid #cccccc;
                        margin-top: 2px;  vertical-align: top">
                        <tr>
                            <td style="width: 60%; text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvReqsnAttachment" runat="server" AutoGenerateColumns="False" EmptyDataText="No attachment found."
                                    CellPadding="2" Width="100%" GridLines="None" CssClass="GridView-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Attachment name" DataField="Attachment_Name" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Uploaded By" DataField="Uploaded_By" />
                                        <asp:BoundField HeaderText="Uploaded On" DataField="Uploaded_On" ItemStyle-HorizontalAlign="Center" />
                                       
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" style="border: none">
                                                    <tr>
                                                        <td>
                                                         <%-- <img style="width: 12px; height: 12px" alt="Open in new window" onclick="DocOpen('<%#"../Files_Uploaded/"+System.IO.Path.GetFileName(Convert.ToString(Eval("File_Path")))%>')"
                                                                src="../Images/Download-icon.png" />--%>
                                                                 <asp:ImageButton ID="imgDownload" runat="server" OnCommand="ImgDownload_Click" style="width: 15px; height: 15px" CommandArgument='<%#Eval("[File_Path]")%>'
                                                    ForeColor="Black" ToolTip="Download" ImageUrl="~/Images/Download-icon.png"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Images/delete.png" runat="server"
                                                                OnClientClick="javascript:var a =confirm('Are you sure to delete this file ?'); if(a) return true; else return false;"
                                                                OnClick="imgbtnDelete_Click" CommandArgument='<%#Eval("id")+","+Eval("Link_ID")+","+Eval("File_Path") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #FF3300; font-size: small;"
                    Width="624px" Height="16px"></asp:Label>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
