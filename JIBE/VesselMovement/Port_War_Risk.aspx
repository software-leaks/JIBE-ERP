<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Port_War_Risk.aspx.cs" Inherits="VesselMovement_Port_War_Risk_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 25%;
            height: 24px;
        }
        .style2
        {
            width: 1%;
            height: 24px;
        }
        .style3
        {
            height: 24px;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
   <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">

    <table width="100%"  >
<tr>
<th>Port List</th>
<th></th>
<th>War Risk Ports</th>
</tr>
<tr>

<td style="width: 40%" >
   
    <asp:ListBox ID="listPort" width="80%" runat="server" Height="500px" SelectionMode="Multiple" ></asp:ListBox>
</td>
<td  align= "center" style="width: 20%">
    <asp:Button ID="btnAdd" runat="server" Width="70%" Text="Add to War Risk" 
        onclick="Button1_Click" />
    <br /><br />
    <asp:Button ID="btnRemove" runat="server" Width="70%" Text="Remove from War Risk " 
        onclick="Button2_Click" />
      
</td>
<td style="width: 40%" align="right" >
<asp:ListBox ID="listWar" width="80%" runat="server" Height="500px"  SelectionMode="Multiple"></asp:ListBox></td>

</tr>
</table>
    </div>
    </form>
</body>
</html>
