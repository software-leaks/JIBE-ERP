<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supplier_Maker_Add.aspx.cs"
    Inherits="Supplier_Maker_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function validationOnSave() {

            if (document.getElementById("txtMakerName_AV").value == "") {
                alert("Please enter maker name.");
                document.getElementById("txtMakerName_AV").focus();
                return false;
            }

            if (document.getElementById("txtAddress_AV").value == "") {
                alert("Please enter address.");
                document.getElementById("txtAddress_AV").focus();
                return false;
            }

            if (document.getElementById("ddlCountry_AV").value == "0") {
                alert("Please select country.");
                document.getElementById("ddlCountry_AV").focus();
                return false;
            }

            return true;

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divadd" style="display: block; border: 1px solid #CCCCCC; font-family: Tahoma;
        text-align: left; font-size: 12px; color: Black;">
        <center>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td style="width: 12%" align="right">
                        Maker Name &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        *
                    </td>
                    <td style="width: 40%" align="left">
                        <asp:TextBox ID="txtMakerName_AV" runat="server" Width="98%" CssClass="txtInput"
                            MaxLength="150" Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Address &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        *
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAddress_AV" runat="server" Width="98%" Height="40px" TextMode="MultiLine"
                            MaxLength="2000" CssClass="txtInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Maker Code &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        *
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtMakerCode_AV" Enabled="false" runat="server" MaxLength="50" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Country &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        *
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCountry_AV" runat="server" Width="60%" CssClass="txtInput"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Date of creation &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtCreationDate_AV" runat="server" Enabled="false" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        E-mail &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEmail_AV" MaxLength="250" Width="60%" runat="server" CssClass="txtInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Phone &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPhone_AV" MaxLength="50" Width="60%" runat="server" CssClass="txtInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Fax &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFax_AV" runat="server" Width="60%" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-size: 11px; text-align: center;">
                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validationOnSave();"
                            OnClick="btnsave_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                        * Mandatory fields
                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
