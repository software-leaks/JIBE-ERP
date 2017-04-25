<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_Vessel_List.ascx.cs"
    Inherits="UserControl_uc_Vessel_List" %>
<style type="text/css">
    .VesselList-Popup
    {
        position: absolute;
        width: 260px;
        border: 1px solid #D0A9F5;
        color: Black;
        text-align: left;
        filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
        background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
        background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
        color: Black;
    }
    .VesselList-user-control
    {
        filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
        background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
        background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
        color: Black;
    }
</style>
<asp:Label ID="lblvessel" runat="server" >
    <asp:Panel ID="Panel1" runat="server">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="border: 0px; padding: 0">
                    <asp:TextBox ID="txtSelectedVessel" Font-Size="11px" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="border: 0px; padding: 0">
                    <asp:ImageButton ID="ImageButton1" Height="18px" ImageUrl="~/Images/DownButton.png"
                        runat="server" ImageAlign="AbsMiddle" OnClick="btnSearchVessel_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdn_TargetControlID" runat="server" />
        <asp:HiddenField ID="hdn_SelectedValue" runat="server" />
        <asp:HiddenField ID="hdn_SelectedText" runat="server" />
      
    </asp:Panel>
    <asp:Panel ID="pnlSearch" runat="server" CssClass="VesselList-Popup" Visible="false">
        <table cellpadding="2" cellspacing="2" style="width: 100%">
            <tr>
                <td style="width: 50px">
                    Search:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtSearchVesselList" runat="server" AutoPostBack="true" OnTextChanged="txtSearchVesselList_TextChanged"
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
                    <asp:ListBox ID="lstVesselList" runat="server" Width="250px" Height="250px" OnSelectedIndexChanged="lstVesselList_SelectedIndexChanged"
                        AutoPostBack="true" DataTextField="Vessel_Name" DataValueField="Vessel_ID"></asp:ListBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Label>
