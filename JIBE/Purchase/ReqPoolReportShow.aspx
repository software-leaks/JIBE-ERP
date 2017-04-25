<%@ Page Title="Pool Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReqPoolReportShow.aspx.cs" Inherits="Technical_INV_ReqPoolReportShow" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">


  <CR:CrystalReportViewer
        ID="CrystalReportViewerPOAPR"
        runat="server" 
        AutoDataBind="true"
        EnableDatabaseLogonPrompt="False" />
</asp:Content>

