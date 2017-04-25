<%@ Page Title="Move Systems" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Move_Systems.aspx.cs" Inherits="Technical_PMS_Move_Systems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <div class="page-title">
        Move Systems
    </div>
    <table cellpadding="3">
        <tr>
            <td colspan="3" style="text-align: left">
                Select Vessel ::
                <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    Width="120px" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="background-color: #80DFFF; font-weight: bold">
                Parent System
            </td>
            <td>
            </td>
            <td style="background-color: #80DFFF; font-weight: bold">
                System to move
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="lstParent" runat="server" DataTextField="SYSTEM_DESCRIPTION" DataValueField="ID"
                    Height="600px" Width="400px" SelectionMode="Single"></asp:ListBox>
            </td>
            <td>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
            <td>
                <asp:ListBox ID="lstToMove" runat="server" DataTextField="SYSTEM_DESCRIPTION" DataValueField="ID"
                    Height="600px" Width="400px" SelectionMode="Single"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnMove" runat="server" Text="Move selected system to sub system level"
                    OnClick="btnMove_Click" />
                <br />
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
