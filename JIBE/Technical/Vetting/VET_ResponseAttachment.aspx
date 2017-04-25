<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VET_ResponseAttachment.aspx.cs" Inherits="Technical_Vetting_VET_ResponseAttachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>   
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>    
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
    body {
  
   
    font-family: Tahoma;
    font-size: 12px;
    margin: 0;
    padding: 0;
   }
    </style>
    <script language="javascript" type="text/javascript">
        function OnfileUploadComplete() {

            document.getElementById($('[id$=btnRetrive]').attr('id')).click();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="updGridAttach" runat="server" >
            <ContentTemplate>
                <div style="padding-top: 2px; padding-bottom: 2px; margin-left:10px;margin-right:10px">
                    <div>
                      <asp:Label runat="server" ID="myThrobber" Style="display: none;" ><img src=""  alt="" /><img src=""  alt="" /></asp:Label>
                                        <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                            Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" MaximumNumberOfFiles="1"
                                            OnUploadComplete="AjaxFileUpload1_OnUploadComplete" OnClientUploadComplete="OnfileUploadComplete"/>
                    </div>
                    <asp:Button ID="btnRetrive" runat="server" style="visibility:hidden" OnClick="btnRetrive_click" Text="Refresh"/>
                  
                  
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #cccccc;
                        margin-top: 2px;  vertical-align: top; width: 100%;">
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                            <div style="overflow-y: scroll; height: 150px;">
                                <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False"
                                    EmptyDataText="No attachment found." CellPadding="2" width= "100%" GridLines="None"
                                    CssClass="GridView-css" Style="padding-left: 5px" ShowHeaderWhenEmpty="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attachment name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                              
                                                <a href='<%#"../../Uploads/Vetting/VetRAtt/"+System.IO.Path.GetFileName(Convert.ToString(Eval("Attachment_Path")))%>'
                                                    target="_blank"><asp:Label ID="lblAttName" runat="server" Text='<%# Eval("Attachment_Name").ToString() %>'></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>               
            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
