<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselInfo.aspx.cs" Inherits="Infrastructure_VesselInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vessel General Information</title>
    <style type="text/css">
        body{font-family:Tahoma;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvVesselInfo" style="z-index: 1000; border: 5px solid #B6DAFD; background-color: White; padding: 2px; margin-bottom: 5px;position:absolute;-moz-box-shadow: 0 0 5px 5px #555;-webkit-box-shadow: 0 0 5px 5px#555;box-shadow: 0 0 5px 5px #555;border-radius: 10px;">    
    
    <div style="background-color:#E8F3FE;margin: 5px;">
        <asp:Repeater ID="rpt1" runat="server">
            <HeaderTemplate>
                <table border=1 style="border-collapse:collapse;font-size:14px;" cellpadding="4">
                    <tr style="background-color:White">
                        <td colspan="2">
                            <img src="../Images/Company_logo.jpg" />
                        </td>
                    </tr>
                    
                    <tr style="background-color:#E3F6CE;font-weight:bold;text-align:center">
                        <td colspan="2">
                            Vessel General Information
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align: right">
                        <%#Eval("Master_Question")%>
                    </td>
                    <td style="text-align: left">
                        <%#Eval("Answer_Text")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div style="font-size:11px;color:#555;text-align:right"><span id="SelectAll">Select</span>&nbsp;&nbsp;<asp:Label ID="lblDateStamp" runat="server"></asp:Label></div>
        
    </div>
    </div>

    </form>
</body>
</html>
