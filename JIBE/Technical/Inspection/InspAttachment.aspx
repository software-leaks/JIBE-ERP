<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspAttachment.aspx.cs" Inherits="Technical_Worklist_InspAttachment"
    Title="Inspection Attachemnts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
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
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="text-align: left; padding: 10px">
            <asp:Label ID="lblInspectionDetailId" runat="server" Style="font-size: 12px; font-weight: bold;
                display: none; color: Navy" />
            <table>
                <tr>
                    <td style="font-size: 12px; font-weight: bold;">
                        Vessel Name:-
                    </td>
                    <td>
                        <asp:Label ID="lblVesselName" runat="server" Style="font-size: 12px; font-weight: bold;
                            color: Navy" />
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="font-size: 12px; font-weight: bold;">
                        Inspection Date:-
                    </td>
                    <td>
                        <asp:Label ID="lblInspectionDate" runat="server" Style="font-size: 12px; font-weight: bold;
                            color: Navy" />
                    </td>
            </table>
        </div>
        <asp:UpdatePanel ID="updGridAttach" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div style="padding-top: 2px; padding-bottom: 2px; width: 100%">
                    <div>
                        <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                        <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                            MaximumNumberOfFiles="100" Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber"
                            OnUploadComplete="AjaxFileUpload1_OnUploadComplete"  />
                    </div>
                    <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" Text="Refresh" runat="server"
                        Style="float: right" />
                    <br />
                    <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid #cccccc;
                        margin-top: 2px; height: 400px; vertical-align: top">
                        <tr>
                            <td style="width: 60%; text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvInspectionAttachment" runat="server" AutoGenerateColumns="False"
                                    EmptyDataText="No attachment found." CellPadding="2" Width="100%" GridLines="None"
                                    CssClass="GridView-css" Style="padding-left: 5px">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attachment name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;&nbsp;&nbsp;<a href='<%#"../../Uploads/Technical/"+System.IO.Path.GetFileName(Convert.ToString(Eval("File_Path")))%>'
                                                    target="_blank"><asp:Label ID="lblAttName" runat="server" Text='<%# Eval("Attachment_Name").ToString() %>'></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uploaded By" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblUploaded_By" runat="server" Text='<%# Eval("Uploaded_By").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uploaded On" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblUploaded_On" runat="server" Text='<%# Eval("Uploaded_On").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" style="border: none">
                                                    <tr>
                                                        <%-- <td style="margin-left:5px;margin-right:5px;cursor:pointer;">
                                                            <img style="width: 14px; height: 14px;margin-left:5px;margin-right:5px" alt="Open in new window" onclick="DocOpen('<%#"../../Uploads/Technical/"+System.IO.Path.GetFileName(Convert.ToString(Eval("File_Path")))%>')"  alt-"Download Attachment"
                                                                src="../../Images/Download-icon.png" /> 
                                                        </td>--%>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                            <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Purchase/Image/Close.gif" runat="server"
                                                                OnClientClick="javascript:var a =confirm('Do you want to delete this file?'); if(a) return true; else return false;"
                                                                OnClick="imgbtnDelete_Click" CommandArgument='<%#Eval("id")+","+Eval("Vessel_Id") %>' />
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
                <%--<div id="dvAssignAttachment" title="Assign Attachment to suppliers" style="display: none;
                    width: 500px;">
                    <table style="text-align: left">
                        <tr>
                            <td>
                                File Name:
                            </td>
                            <td>
                                <asp:Label ID="lblFineName" Width="300px" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="text-align: center">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    CssClass="GridView-css">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsSent" Checked='<%#Eval("IsAssigned") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSuppName" Text='<%#Eval("Full_NAME") %>' Width="300px" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSuppcode" Text='<%#Eval("QUOTATION_SUPPLIER") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
