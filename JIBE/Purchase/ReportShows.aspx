<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportShows.aspx.cs" Inherits="Technical_INV_ReportShows" Title ="Reports Details" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD>
		
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
<body>
    <form id="form1" runat="server">
   <div>
    <table>
      <tr>
        <td colspan ="4" align ="center">
            <asp:Button ID="btnBack" runat="server"  CssClass="button-css"  Text="Back" 
                Width="116px" onclick="btnBack_Click" />
        </td>
        </tr> 
      </table>
    <br />
     <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer" runat="server" 
           AutoDataBind="true"  EnableDatabaseLogonPrompt="False" 
           EnableParameterPrompt="False"  HasCrystalLogo="False" EnableDrillDown="False" />
    </div>
    </form>
</body>
</html>
