<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportsView.aspx.cs" Inherits="PortageBill_PhoneCardReports_ReportsView" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div>
        <table cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td align="center">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" Width="100%" runat="server" AutoDataBind="true"
                        HasCrystalLogo="False" HasPrintButton="True" EnableDrillDown="False" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
