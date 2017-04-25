<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelPopup.aspx.cs" Inherits="Technical_INV_CancelPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server" width ="400px" height ="250px" >
    <div>
    <table >
    <tr >
    <td> Select Status to u have Canceled</td>
    <td>
        <asp:DropDownList ID="CanceldList" runat="server" Height="23px" Width="161px">
        </asp:DropDownList>
                    </td>
    </tr>
    <td> Select Status to u have Canceled</td>
    <td>
        
                    <asp:TextBox ID="TextBox1" runat="server" Width="162px"></asp:TextBox>
        
                    </td>
    </tr>
    <tr >
    <td> </td>
    <td>
        
                    <asp:Button ID="Button1" runat="server" Text="Button" />
        
                    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
