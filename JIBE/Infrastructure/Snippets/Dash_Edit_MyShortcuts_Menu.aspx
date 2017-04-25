<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dash_Edit_MyShortcuts_Menu.aspx.cs"
    Inherits="Infrastructure_Snippets_Dash_Edit_MyShortcuts_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

   
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdfissaveClicked"  Value="0" runat="server" />
    <div style="height:500px;overflow:auto;width:100%;border:1px solid gray">
        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
            Width="100%" BorderColor="#cccccc" ShowCheckBoxes="All" EnableClientScript="true" >
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                NodeSpacing="0px" VerticalPadding="2px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                VerticalPadding="0px" CssClass="SelectedNodeStyle" />
        </asp:TreeView>
        <br />
       
    </div>

     <asp:Button ID="btnSaveFavourite" runat="server" Height="35px" Width="100px" Font-Size="14px" OnClick="btnSaveFavourite_Click"
            Text="Save" OnClientClick="document.getElementById('hdfissaveClicked').value=1" />
    </form>
</body>
</html>
