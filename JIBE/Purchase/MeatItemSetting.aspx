<%@ Page Title="Meat Item Settings" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MeatItemSetting.aspx.cs" Inherits="Purchase_MeatItemSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ValidateOnSave() {

            $("#lblError").html('');
            if ($("#txtMeatAllow").val().trim() == '') {
                alert('Enter Meat Item Allowance');
                $("#txtMeatAllow").focus();
                return false;
            }
            if ($("#txtMeatLimit").val().trim() == '') {
                alert('Enter Meat Item Limit');
                $("#txtMeatLimit").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="page-title" style="text-align: center">
        <b>Meat Item Setting</b>
    </div>
    <div>
        <br />
        <table align="center">
         <tr>
            <td align="center"><asp:Label ID="lblError" runat="server" ClientIDMode="Static"  style="color:Red"></asp:Label></td>
            </tr>
        </table>
        <table>
           
            <tr>
                <td align="right" style="width: 550px">
                    <asp:Label ID="lblMeatAllow" runat="server" Text="Meat Item Allowance"></asp:Label>
                </td>
                <td style="color:Red;">*</td>
                <td>
                    <asp:TextBox ID="txtMeatAllow" runat="server" ClientIDMode="Static"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblAllowUnit" runat="server" Text="%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 550px">
                    <asp:Label ID="lblMeatLimit" runat="server" Text="Meat Item Limit"></asp:Label>
                </td>
                <td style="color:Red;">*</td>
                <td>
                    <asp:TextBox ID="txtMeatLimit" runat="server" ClientIDMode="Static"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblLimitUnit" runat="server" Text="KG"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="text-align: center">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" OnClientClick="javascript:return ValidateOnSave();"/></div>
    </div>
</asp:Content>

