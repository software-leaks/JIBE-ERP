<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutForm.aspx.cs" Inherits="CheckOutForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CheckOut Form</title>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font-family:Tahoma;
            font-size:11px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div>
    </div>
    </form>
</body>
</html>
