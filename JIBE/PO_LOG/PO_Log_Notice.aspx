<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Notice.aspx.cs" Inherits="PO_LOG_PO_Log_Notice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
               Notice
            </div>
             <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                                        <ContentTemplate>
             <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
        <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td colspan="3" >Select the email template that you want to send to supplier.
                  Click on the "Send Email" Button to activate the daemon to dispatch the email. 
                    </td>
                   
                </tr>
                <tr>
                    <td align="right" style="width: 5%">
                           <asp:Button ID="btnMail1" runat="server" Text="Select" onclick="btnMail1_Click"  
                           />
                    </td>
                    <td id="tdppr" runat=server colspan="2" style="color: #FF0000;"  align="left">
                       Paper invoice received, send notification to supplier that invoice will not be processed and all invoices be scanned and uploaded onto the online system.
                    </td>
                
                </tr>
                 <tr>
                    <td align="right" style="width: 5%">
                           <asp:Button ID="btnMail2" runat="server" Text="Select" onclick="btnMail2_Click"  
                             />
                    </td>
                    <td colspan="2" style="color: #FF0000;" align="left">
                      Invoice for Purchase Order has not been received. Send Email reminder to Supplier to submit Invoice online. Only possible when PO is not closed.
                    </td>
                  
                </tr>
                 <tr>
                    <td align="right" style="width: 5%">
                           <asp:Button ID="btnMail3" runat="server" Text="Select" onclick="btnMail3_Click" 
                            />
                    </td>
                    <td style="color: #FF0000;" align="left">
                       Request supplier to re-update / re-verify the registered data form.
                    </td>
                   <td><asp:Label id="lblRegistered" runat="server" Text="" ></asp:Label> </td>
                </tr>
                <tr>
                    <td align="right" style="width: 5%">
                           <asp:Button ID="btnMail4" runat="server" Text="Select" onclick="btnMail4_Click" 
                          />
                    </td>
                    <td colspan="2" style="color: #FF0000;"  align="left">
                      Send Auto PO Email to Supplier.
                    </td>
                  
                </tr>
        
                
            </table>
            </div>
        <div style="display:none;" >
         <asp:TextBox ID="txtRemarksID" runat="server"  Width="1px"></asp:TextBox>
          <asp:TextBox ID="txtPOCode" runat="server"  Width="1px"></asp:TextBox>
        </div>
        </ContentTemplate></asp:UpdatePanel>
         </div>
         <div>
             
           
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">

          <ContentTemplate>
          <asp:Panel ID="pnlEmail" runat="server" Visible="false">
           <table>
            <tr>
                    <td colspan="2" 
                        style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                        <asp:Button ID="btnAddRemarks" runat="server" Text="Send Mail"  
                            ValidationGroup="vgSubmitRemarks" onclick="btnAddRemarks_Click"
                            />
                       
                    </td>
                </tr>
           <tr>
           <td>To:</td>
           <td>
               <asp:Label ID="lblTo" runat="server" Text=""></asp:Label></td>
           </tr>
         <tr>
           <td>CC:</td>
           <td>
               <asp:Label ID="lblCC" runat="server" Text=""></asp:Label></td>
           </tr>
           <tr>
           <td valign="top">Subject:</td>
           <td>
               <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label></td>
                  
           </tr>
           <tr>
           <td></td>
           <td>  
               <asp:Label ID="lblBody" runat="server" Text=""></asp:Label></td>
           </tr>
                  
           </table>
            </asp:Panel>
           </ContentTemplate>
              </asp:UpdatePanel>
               
         </div>
    </center>
  
    </form>
</body>
</html>

