<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_SupplierList.ascx.cs"
    Inherits="UserControl_uc_SupplierList" %>
<style type="text/css">
    .SupplierList-Popup
    {
        position: absolute;
        width: 260px;
        border: 1px solid #D0A9F5;
        color: Black;
        text-align: left;
        filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
        background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
        background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
         background-image: linear-gradient(to bottom, #CEE3F6 0%, #99BBE6 100%);
        color: Black;
    }
    .SupplierList-user-control
    {
        filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
        background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
        background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
        background-image: linear-gradient(to bottom, #F8D79B 0%, #F1C15F 100%);
        color: Black;
    }
</style>
<asp:Panel ID="Panel1" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="border: 0px; padding: 0">
                <asp:TextBox ID="txtSelectedSupplier" Font-Size="11px" runat="server"  Font-Names="tahoma"
                    ReadOnly="true" ForeColor="Black" BackColor="White" BorderStyle="Solid" BorderColor="LightGray" BorderWidth="1px"
                   ></asp:TextBox>
            </td>
            <td style="border: 0px; padding: 0">
                <asp:ImageButton ID="ImageButton1" Height="18px" ImageUrl="~/Images/DownButton.png"
                    runat="server" ImageAlign="AbsMiddle" OnClick="btnSearchSupplier_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdn_TargetControlID" runat="server" />
    <asp:HiddenField ID="hdn_SelectedValue" Value="0" runat="server" />
    <asp:HiddenField ID="hdn_SelectedText" runat="server" />
    <asp:HiddenField ID="HiddenField3" runat="server" />
    <asp:HiddenField ID="hdf_Supplier_Type" Value="S" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlSearch" runat="server" CssClass="SupplierList-Popup" Visible="false">
    <table cellpadding="2" cellspacing="2" style="width: 100%">
        <tr>
            <td style="width: 50px">
                Search:
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtSearchSupplierList" runat="server" AutoPostBack="true" OnTextChanged="txtSearchSupplierList_TextChanged"
                    Width="120px"></asp:TextBox>
                <asp:ImageButton ID="btnSearch" ImageUrl="~/Images/SearchButton.png" runat="server"
                    ImageAlign="AbsMiddle" OnClick="btnSearch_Click" />
            </td>
            <td style="text-align: right;">
                <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/close.gif" runat="server" ToolTip="Close"
                    ImageAlign="AbsMiddle" OnClick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ListBox ID="lstSupplierList" runat="server" Width="250px" Height="250px" OnSelectedIndexChanged="lstSupplierList_SelectedIndexChanged"
                    AutoPostBack="true" DataTextField="fullname" DataValueField="SUPPLIER"></asp:ListBox>
            </td>
        </tr>
    </table>
</asp:Panel>
