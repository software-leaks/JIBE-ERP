<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SummaryReportsShow.aspx.cs" Inherits="Technical_INV_SummaryReportsShow" Title="Rport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">


<CR:CrystalReportViewer
        ID="SummaryReportViewer"
        runat="server"
        EnableDatabaseLogonPrompt="False" EnableDrillDown="False" 
        oninit="SummaryReportViewer_Init" />

</asp:Content>

