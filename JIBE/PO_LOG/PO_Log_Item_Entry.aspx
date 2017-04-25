<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Item_Entry.aspx.cs" Inherits="PO_LOG_PO_Log_Item_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblInvoiceID" runat="server" ></asp:Label>
      <asp:Button ID="btnAttachment" runat="server" Text="Add Attachment" 
                                     OnClick="btnAttachment_Click" />&nbsp;&nbsp;


                <div id="divAttachment" runat="server" visible="false" style="height: 250px; overflow-y: scroll;
                    max-height: 250px">
                    <asp:GridView ID="gvReqsnAttachment" runat="server" AutoGenerateColumns="False" EmptyDataText="No attachment found."
                        CellPadding="2" Width="100%" GridLines="None" CssClass="GridView-css" 
                        onrowdatabound="gvReqsnAttachment_RowDataBound">
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
                                                <asp:ImageButton ID="ImgView" runat="server" OnCommand="ImgView_Click" style="width: 15px; height: 15px" CommandArgument='<%#Eval("[File_Path]")%>'
                                                    ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png"></asp:ImageButton>
                                            </td>
                                            <td>
                                               
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
                </div>
          

    <div id="dvAttachment" title="Add Attachments" style="display: none; width: 500px;">
            <div class="error-message" onclick="javascript:this.style.display='none';">
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        Attachment
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click">
                        </asp:Button>
                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblhdn" runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txthdn" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

   <%--  <div id="divIframe" runat="server" visible="false" style="border: 1px solid #cccccc;
            height: 200px; font-family: Tahoma; font-size: 12px; width: 100%; overflow-y: scroll;">

            <iframe id="iFrame1" runat="server" width="100%" height="100%"></iframe>

            <asp:HiddenField ID="hdnFilePath" runat="server" />
        </div>--%>
    </div>
    </form>
</body>
</html>
