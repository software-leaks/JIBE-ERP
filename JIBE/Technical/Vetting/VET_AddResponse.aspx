<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VET_AddResponse.aspx.cs" Inherits="Technical_Vetting_VET_AddResponse" %>

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
         function ValidateTextbox() {

             var Response = document.getElementById($('[id$=txtResponse]').attr('id')).value.trim();
             if (Response == "") {
                 alert("Response should not be empty.");
                 document.getElementById($('[id$=txtResponse]').attr('id')).focus();
                 return false;
             }
         }
         function OnfileUploadComplete() {


             //alert("Files Uploaded Successfully");

          
         }
         function OnfileUploadStart(s, e) {

             var resid = document.getElementById($('[id$=hdnResponseId]').attr('id'));
             if (resid.value == "") {

                 alert("Response was not saved.\r\nPlease save the response and then add attachment");
                 e.cancel = true;
                // return false;
             }
            // return true;
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdPnlAddResponse"  runat="server">
                <ContentTemplate>
                <div  title="Add Question" style="width:100%;">
                <table style="width:90%;" >
                <tr>
                <td style="width:20%;">
                    <asp:Label ID="lblResponse" runat="server" Text="Response :"></asp:Label>
                </td>
                <td style="width:80%">
                    <asp:TextBox ID="txtResponse" runat="server" TextMode="MultiLine" Width="100%" Height="50px"></asp:TextBox>
                </td>
                </tr>
                <tr><td><asp:Button ID="btnSave"  Text="Save" runat="server"
                        Style="float: left" onclick="btnSave_Click" OnClientClick="return ValidateTextbox();" />
                    <br /></td></tr>
               </table>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="updGridAttach" runat="server" UpdateMode="Always" >
            <ContentTemplate>
                <div style="padding-top: 2px; padding-bottom: 2px; ">
                    <div>
                        <asp:Label runat="server" ID="myThrobber" Style="display: none;" ><img src=""  alt="" /><img src=""  alt="" /><img src=""  alt="" /><img src=""  alt="" /><img src=""  alt="" /><img src=""  alt="" /><img src=""  alt="" /><img src=""  alt="" /></asp:Label>
                        <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                            Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" MaximumNumberOfFiles="1"
                            OnUploadComplete="AjaxFileUpload1_OnUploadComplete"  OnClientUploadComplete="OnfileUploadComplete"  OnClientUploadStart="OnfileUploadStart"/>

                                   
          
                    </div>
                  
                      <asp:HiddenField ID="hdnResponseId" runat="server" />
                </div>
                
            
            </ContentTemplate>
            
        </asp:UpdatePanel>
        
    </form>
</body>
</html>
