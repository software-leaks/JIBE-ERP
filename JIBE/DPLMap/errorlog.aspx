<%@ Page Language="C#" AutoEventWireup="true" CodeFile="errorlog.aspx.cs" ValidateRequest="false"
    Inherits="errorlog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DPL-MAP: Lat-Long Error list </title>

    <script type="text/javascript" src="MapScripts/q_fix.js">
    </script>

    <style type="text/css">
        .trHeader
        {
            font-size: 16px;
            font-weight: bold;
            font-family:Arial;
            background-color:#bcbcbb;
        }
        .trHeader2
        {
            font-size: 12px;
            font-weight: bold;
            font-family:Arial;
        }
        .tblErrorData
        {
            font-size: 12px;
            font-family:Arial;
            border:1px solid #dcdcdc;
        }
    </style>
</head>
<body onload="q_fix">
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="litError" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
