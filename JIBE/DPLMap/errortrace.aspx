<%@ Page Language="C#" AutoEventWireup="true" CodeFile="errortrace.aspx.cs" Inherits="errortrace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
    </div>
    
    <script language="javascript" type="text/javascript">
    
    function createPopUpwindow_err(htmltext)
    {
        newwindow=window.open(null,'name','height=200,width=150');
        newwindow.document.write(htmltext);
	    if (window.focus)
	     {newwindow.focus()}
	

    }
    </script>
    </form>
</body>
</html>
