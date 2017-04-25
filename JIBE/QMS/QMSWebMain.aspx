<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QMSWebMain.aspx.cs" Inherits="QMSWebMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QMSWebMain Page</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 185px;
        }
        .style2
        {
            width: 285px;
        }
    </style>
        <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/Main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                <img src="Images/Logo.gif" height="53" width="70"  /><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="Large" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table class="style1">
                        <tr>
                            <td class="style2">
                    <asp:TreeView ID="BrowseTreeView" runat="server" ShowCheckBoxes="Leaf" 
                        style="margin-right: 1px" Width="304px" BorderColor="#F3F1CD" Height="529px" 
                                    Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="Black" 
                                    EnableViewState="False" ImageSet="XPFileExplorer" NodeIndent="15">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                            HorizontalPadding="0px" VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                            HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
