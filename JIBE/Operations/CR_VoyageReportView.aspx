<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="CR_VoyageReportView.aspx.cs"
    Inherits="Operations_CR_VoyageReportView" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="main" runat="server" ContentPlaceHolderID="MainContent">
    <div style="width:1200px;overflow:auto">
        <CR:CrystalReportViewer ID="CrystalReportViewer" runat="server" AutoDataBind="true"
            Width="1200px" Height="400px" EnableDatabaseLogonPrompt="False" ReportSourceID="crpt" EnableParameterPrompt="False"
            HasCrystalLogo="False" EnableDrillDown="False" />

        <CR:CrystalReportSource ID="crpt" runat="server">
            <Report FileName="CR_VoyageReport.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
</asp:Content>
