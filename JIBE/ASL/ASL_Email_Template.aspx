<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Email_Template.aspx.cs"
    Inherits="ASL_ASL_Email_Template" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email Template</title>
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
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
        $(document).ready(function () {
            window.parent.$("#ASL_Evalution").css("height", (parseInt($("#pnlEmail").height()) + 50) + "px");
            window.parent.$(".xfCon").css("height", (parseInt($("#pnlEmail").height()) + 50) + "px").css("top", "50px");
        });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlEmail" runat="server" Visible="true">
         <div id="Div2" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
   
                    <div id="Div1" class="page-title">
                        Email Template
                    </div>
                
            <table width="100%" cellpadding="2" cellspacing="0">
            <tr>
                <td valign="top">
                    <table>
                        <tr>
                            <td>
                           
                                <asp:Label ID="lblSub" Font-Size="Medium" runat="server" Text=""></asp:Label><br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label1" Font-Size="Medium" runat="server" Text="TO:"></asp:Label>
                                <asp:Label ID="lblSupplierName" Font-Size="Medium" runat="server" Text=""></asp:Label><br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblBody" Font-Size="Medium" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        </asp:Panel>
    </div>
    </form>
    </td></tr></table>
</body>
</html>
