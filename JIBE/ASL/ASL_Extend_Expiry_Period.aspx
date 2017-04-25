<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Extend_Expiry_Period.aspx.cs"
    Title="Extend Expiry Period" Inherits="ASL_Extend_Expiry_Period" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;">
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ddlExtendedPeriod").value == "0") {
                alert("Please select the extended period.");
                document.getElementById("ddlExtendedPeriod").focus();
                return false;
            }

            return true
        }

    </script>
    <form id="form1" runat="server">
     <center>
    <div id="dvContent" style="text-align: center; width:400px;">
       
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td align="right" style="width: 8%">
                        Register Name &nbsp;:&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRegisterName" runat="server" Width="90%" CssClass="txtReadOnly"> </asp:TextBox>
                    </td>
                     
                </tr>
                <tr>
                    <td align="right" >
                        ASL Status&nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 15%" align="left">
                        <asp:DropDownList ID="ddlStatus" CssClass="txtReadOnly" runat="server" Width="60%"
                            Enabled="false">
                        </asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td align="right" >
                        Propose Type&nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 15%" align="left">
                        <asp:DropDownList ID="ddlType" runat="server" Width="60%" CssClass="txtReadOnly" Enabled="false">
                        </asp:DropDownList>
                    </td>
                     
                </tr>
                <tr>
                    <td align="right">
                        For Period&nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 15%" align="left">
                        <asp:DropDownList ID="ddlPeriod" runat="server" Width="60%" CssClass="txtReadOnly"
                            Enabled="false">
                            <asp:ListItem Value="30" Text="30 days" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="60" Text="60 days"></asp:ListItem>
                            <asp:ListItem Value="90" Text="90 days"></asp:ListItem>
                            <asp:ListItem Value="180" Text="180 days"></asp:ListItem>
                            <asp:ListItem Value="365" Text="365 days"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td align="right" >
                        Expire On&nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 15%" align="left">
                        <asp:TextBox ID="txtExpireON" runat="server" Width="60%" CssClass="txtReadOnly"> </asp:TextBox>
                    </td>
                     
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td style="color: #FF0000; width: 1%" align="left">
                    </td>
                    
                </tr>
                <tr>
                    <td align="right">
                        Extend Period&nbsp;:&nbsp;
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlExtendedPeriod" runat="server" Width="60%" CssClass="txtInput">
                             <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30 days" ></asp:ListItem>
                            <asp:ListItem Value="60" Text="60 days"></asp:ListItem>
                            <asp:ListItem Value="90" Text="90 days"></asp:ListItem>
                            <asp:ListItem Value="180" Text="180 days"></asp:ListItem>
                            <asp:ListItem Value="365" Text="365 days"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        &nbsp;
                        <asp:Button ID="btnExtend" runat="server" Text="Extend" OnClientClick="return validation();"
                            OnClick="btnExtend_Click" Width="180px" />
                    </td>
                   
                </tr>
            </table>
      
    </div>
      </center>
    </form>

</body>
</html>
