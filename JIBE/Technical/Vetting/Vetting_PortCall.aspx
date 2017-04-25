<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_PortCall.aspx.cs" Inherits="Technical_Vetting_Vetting_PortCall"  Title="Vetting Port Call" EnableEventValidation="false"%>
<%@ Register Src="~/UserControl/ucPortCalls.ascx" TagName="ucPortCall" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vetting Port Call</title>
</head>
    <script language="javascript" type="text/javascript">
       
    </script>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>  
    
    <div id="divPorCall" style="font-family: Tahoma; text-align: left; font-size: 12px;
        color: Black;" >
     <asp:UpdatePanel runat="server" ID="updPortCall">
            <ContentTemplate>
            <table width="100%">
            <tr>
            <td>
         <uc1:ucPortCall id="ucPortCall" runat="server"  OnSelect="UserControl_ucPortCalls_Select"   />
           
            </td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
