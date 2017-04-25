<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlDateTime.ascx.cs" Inherits="UserControl_ctlDateTime" %>
<asp:TextBox ID="txtDate" runat="server" Width="80px"></asp:TextBox>
<cc1:CalendarExtender ID="calDate" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
