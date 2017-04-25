 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="DrillReportViewer.aspx.cs" Inherits="eForms_eFormTempletes_DrillReportViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Drill Report Preview</title>
    <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %> 
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 1200px; overflow: auto">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            Width="1200px" Height="400px" EnableDatabaseLogonPrompt="False" ReportSourceID="crpt"
            EnableParameterPrompt="False" HasCrystalLogo="False" EnableDrillDown="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="DrillReport.rpt">
            </Report>
        </CR:CrystalReportSource>
        </div>
    </form>
</body>
</html>