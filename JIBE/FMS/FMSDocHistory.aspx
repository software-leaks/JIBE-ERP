<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FMSDocHistory.aspx.cs" Inherits="DocHistory" EnableEventValidation="false" EnableViewState="true" ViewStateMode="Enabled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
 <%--   <script src="JS/common.js" type="text/javascript"></script>--%>
    
</head>
<body>
    <form id="frmHistory" runat="server">

    <asp:Repeater ID="dtrOppGrid" runat="server">
    
        <HeaderTemplate>
            <table  cellspacing="0" cellpadding="4" border="0" id="dvOperations" style="color: #333333;
                width: 100%; border:1px solid gray;">
                <tr align="left" style="color: black;   background-color: #CCCCCC;border-top:1px solid gray;
                    font-size: 11px; font-weight: bold;">
                    <td scope="col" style="border-right:1px solid #999999;border-left:1px solid #999999;">
                        Date
                    </td>
                    <td scope="col" style="border-right:1px solid #999999; ">
                        Operation
                    </td>
                    <td scope="col" style="border-right:1px solid #999999;">
                        User
                    </td>
                    <td scope="col" style="border-right:1px solid #999999;">
                        Version
                    </td>
                  <%--  <td scope="col" style="border-right:1px solid #999999;">
                        View
                    </td>--%>
                    <td scope="col" style="border-right:1px solid #999999;">
                       Download and View
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
        
            <tr style="background-color:white;border-right:1px solid gray;border-left:1px solid gray;border-bottom:1px solid gray">
                <td  style="border-right:1px solid #999999;border-left:1px solid #999999;">
                    <%# ConvertDateToString(Convert.ToString( Eval("Operation_Date")),"dd-MMM-yy HH:mm")%>
                </td>
                <td  style="border-right:1px solid #999999;">
                    <%# Eval("Operation_Type") %>
                </td>
                <td  style="border-right:1px solid #999999;">
                    <%# Eval("First_Name")%>
                </td>
                <td  style="border-right:1px solid #999999;">
                    <%# Eval("Version") %>
                </td>
            <%--    <td  style="border-right:1px solid #999999;">
                    <a href="FileLoader.aspx?DocVer=<%# Eval("DocID")%>-<%# Eval("Version") %>">
                        <%# Convert.ToString(Eval("LogFileID"))%></a>
                </td>--%>
                <td  style="border-right:1px solid #999999;">
                    <a href="GetLatest.aspx?DocVer=<%# Eval("DocID")%>-<%# Eval("Version") %>">
                        <%# Convert.ToString(Eval("LogFileID"))%></a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

    </form>
</body>
</html>