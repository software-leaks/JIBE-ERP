<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Vetting_AddSectionAndQuestion.aspx.cs" Inherits="Technical_Vetting_Vetting_AddSectionAndQuestion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
  
   
    font-family: Tahoma,Tahoma,sans-serif,vrdana;
    font-size: 12px;
    margin: 0;
    padding: 0;
}
    </style>
 </head>
<body>
<form id="frmCondReport" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="page-content" style="font-family: Tahoma; font-size: 12px">
        <asp:UpdatePanel ID="UpdPnlAddSectionAndQuestion" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div id="divAddSectionAndQuestion" style="width: 500px; display: block; border: 1px solid #cccccc;
                background-color: #E0ECF8;" title="Add Section / Question">
                <table width="500px" cellpadding="1" cellspacing="1">
               
                    <tr>
                        <td align="right" colspan="1">
                            Section NO :
                        </td>
                        <td align="left" colspan="2">
                              <asp:TextBox ID="txtSection" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" colspan="1">
                            Level 2 :
                        </td>
                        <td align="left" colspan="2">
                               <asp:TextBox ID="txtLevl2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align="right" colspan="1">
                            Level 3 :
                        </td>
                        <td align="left" colspan="2">
                           <asp:TextBox ID="txtLevl3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">
                             Level 4 :
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtLevl4" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" colspan="1">
                             Question :
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">
                             Remarks :
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSaveAndClose" runat="server" Text="Save and Close" 
                                Width="100px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSaveAndAdd" runat="server" Text="Save and add questions" 
                                Width="140px" />
                        </td>
                </table>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                
                    </form>
</body>
</html>
