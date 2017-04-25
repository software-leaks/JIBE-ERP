<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Delivery_Item_Entry.aspx.cs"
    Inherits="PO_LOG_PO_Log_Delivery_Item_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>
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
    <style type="text/css">
        .style1
        {
            width: 15%;
            height: 27px;
        }
        .style2
        {
            width: 1%;
            height: 27px;
        }
        .style3
        {
            width: 35%;
            height: 27px;
        }
        .style4
        {
            width: 20%;
            height: 27px;
        }
        .txtInput
        {
        }
    </style>
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
    height: 100%;">
    <script language="javascript" type="text/javascript">

        function validation() {
            return true
        }
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127)) {
                event.returnValue = true;
            }
            else {
                event.returnValue = false;
                alert('Only numeric Value(0-9) is allowed.')
            }
        }
    </script>
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <center>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-title" class="page-title">
                    Delivery Item Entry
                </div>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td align="right" class="style1">
                            PO Item Description&nbsp;:&nbsp;
                        </td>
                        <td style="color: #FF0000;" align="right" class="style2">
                            
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtName" runat="server" Enabled="false" MaxLength="250" Width="400px"
                                CssClass="txtInput"> </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 15%">
                            PO Qty&nbsp;:&nbsp;
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            
                        </td>
                        <td align="left" style="width: 35%">
                            <asp:TextBox ID="txtPoQty" runat="server" Enabled="false" MaxLength="250" Width="195px"
                                CssClass="txtInput"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Label ID="lblPOUnit" runat="server" CssClass="txtInput"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 15%">
                            PO Price&nbsp;:
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            
                        </td>
                        <td align="left" style="width: 35%">
                            <asp:TextBox ID="txtPoPrice" runat="server" Enabled="false" MaxLength="250" Width="195px"
                                CssClass="txtInput"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Label ID="lblPOCurrency" runat="server" CssClass="txtInput"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Confirm Quantity&nbsp;:&nbsp;
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtConQty" runat="server" MaxLength="250" Width="195px" CssClass="txtInput"></asp:TextBox>&nbsp;&nbsp;
                            <asp:TextBox ID="txtDeliveryUnit" runat="server"  MaxLength="250" Width="195px"
                                CssClass="txtInput"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Confirm Price&nbsp;:
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                            *
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtConPrice" runat="server" MaxLength="250" Width="195px" CssClass="txtInput"></asp:TextBox>&nbsp;&nbsp;<asp:Label
                                ID="lblDeliveryCurrency" runat="server" CssClass="txtInput"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Remarks&nbsp;:&nbsp;
                        </td>
                        <td style="color: #FF0000; width: 1%" align="right">
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" MaxLength="500"
                                Width="400px" CssClass="txtInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right" style="color: #FF0000; font-size: small;">
                            * Mandatory fields
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 20px; background-color: #d8d8d8; text-align: center">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnDraft" runat="server" Text="Save" OnClientClick="return validation();"
                                    Width="180px" OnClick="btnDraft_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="display:none;">
                    <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="vgSubmit" />
                    <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
                    <asp:TextBox ID="txtDeliveryID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtDelivery_ItemID" runat="server"></asp:TextBox>
                </div>
            </div>
        </center>
    </form>
</body>
</html>
