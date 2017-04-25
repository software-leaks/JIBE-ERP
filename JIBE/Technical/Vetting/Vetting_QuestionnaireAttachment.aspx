<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_QuestionnaireAttachment.aspx.cs" Inherits="Technical_Vetting_Vetting_QuestionnaireAttachment" %>

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
   <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <style type="text/css">
    body {
  
   
    font-family: Tahoma;
    font-size: 12px;
    margin: 0;
    padding: 0;
}
</style>
 <script type="text/javascript">

     
     function JSValidateUploader() {
         if (document.getElementById($('[id$=FileUpload1]').attr('id')).value == "") {
             alert("Please select file to upload.");
             return false;
         }
         
     }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="updGridAttach" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div style="padding-top: 2px; padding-bottom: 2px;">
                    <div style=" margin-left:9px">
                        
                      <asp:Label ID="lblUpload" runat="server" Font-Bold="true" Text="Upload New File :"> </asp:Label><br /><br />
                     <asp:FileUpload ID="FileUpload1" runat="server" Padding-Bottom="2"  Padding-Right="1" Width="408px" Height="23px" Padding-Top="2"/>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                        onclick="btnUpload_Click" OnClientClick="return JSValidateUploader();"/>
                    </div>                 
                    
                    <table cellpadding="2" cellspacing="0" style="border: 1px solid #cccccc;
                        margin-top: 20px;  vertical-align: top; width: 470px; margin-left:9px">
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False"
                                    EmptyDataText="No attachment found."  CellPadding="2" width= "470px" GridLines="None"
                                    CssClass="GridView-css" Style="padding-left: 5px" ShowHeaderWhenEmpty="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attachment name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                               
                                                 <a href='<%#"../../Uploads/Vetting/VetQAtt/"+System.IO.Path.GetFileName(Convert.ToString(Eval("Attachement_Path")))%>'
                                                    target="_blank"><asp:Label ID="lblAttName" runat="server" Text='<%# Eval("Attachement_Name").ToString() %>'></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>               
            
            </ContentTemplate>
            <Triggers>
<asp:PostBackTrigger ControlID="btnUpload" />

</Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
