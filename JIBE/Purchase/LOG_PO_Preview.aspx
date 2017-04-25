<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG_PO_Preview.aspx.cs" Inherits="Purchase_LOG_PO_Preview" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PO Preview</title>
    <link href="../../CSS/StyleSheetMsg.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                            HasCrystalLogo="False" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>