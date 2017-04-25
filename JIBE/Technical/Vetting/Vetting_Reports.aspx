<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_Reports.aspx.cs"
    Inherits="Technical_Vetting_Vetting_Reports" Title="Vetting Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vetting Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="right">
        <asp:ImageButton ID="ImgExportToExcel" src="../../Images/XLS.jpg" Height="20px" 
            runat="server" AlternateText="Export" OnClick="ImgExportToExcel_Click" Style="margin-left: 10px" Visible="false" 
            ToolTip="Export With Images" />
     
    
     
    </div>
    <div id="dvVettingReport" runat="server" style="width: 100%;">
    </div>
    </form>
</body>
</html>
