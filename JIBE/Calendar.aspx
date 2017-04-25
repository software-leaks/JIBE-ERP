<%@ Page Title="Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<iframe id="frmCalendar" src="../../demoasp/dpl/calendar.asp" width="100%" height="600px">
</iframe>
</asp:Content>

