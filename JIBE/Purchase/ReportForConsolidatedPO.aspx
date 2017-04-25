<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ReportForConsolidatedPO.aspx.cs" Inherits="Technical_INV_ReportForConsolidatedPO" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 100%" align="center">
                <CR:CrystalReportViewer ID="crvConsolidatedPO" runat="server" 
                        AutoDataBind="true" Width="100%" 
                        EnableDrillDown="False">
                    </CR:CrystalReportViewer>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
