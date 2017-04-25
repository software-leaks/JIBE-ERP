<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocHistory.aspx.cs" Inherits="DocHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <script src="JS/common.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="2" style="border: 1px solid gray; width: 100%;font-size:11px;">
        <tr>
            <td class="style1">
                File Name:
            </td>
            <td style="background-color: #eedcee" colspan="3">
                <asp:Label ID="lblFileName" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Created By:
            </td>
            <td style="background-color: #eedcee">
                <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label>
            </td>
            <td class="style1">
                Last Operation:
            </td>
            <td style="background-color: #eedcee">
                <asp:Label ID="lblLastOperation" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Creation Date:
            </td>
            <td style="background-color: #eedcee">
                <asp:Label ID="lblCreationDate" runat="server" Text=""></asp:Label>
            </td>
            <td class="style1">
                Last Operation Date:
            </td>
            <td style="background-color: #eedcee">
                <asp:Label ID="lblLastOperationDt" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
    </table>
    <div style="border: 1px solid outset; background-color: #B0C4DE; margin-top: 5px;
        padding: 2px; font-weight: bold;">
        Operation History
    </div>
    <asp:Repeater ID="dtrOppGrid" runat="server">
        <HeaderTemplate>
            <table cellspacing="0" cellpadding="4" border="0" id="dvOperations" style="color: #333333;
                width: 100%; border-collapse: collapse;">
                <tr align="left" style="color: White; background-color: #507CD1; 
                    font-size: 11px; font-weight: bold;">
                    <td scope="col">
                        Date
                    </td>
                    <td scope="col">
                        Operation
                    </td>
                    <td scope="col">
                        User
                    </td>
                    <td scope="col">
                        Version
                    </td>
                   <%-- <td scope="col">
                        View
                    </td>
                    <td scope="col">
                        View in external File
                    </td>--%>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr style="background-color: #EFF3FB;">
                <td>
                    <%# ConvertDateToString(Convert.ToString(Eval("MODIFIED_DATE")), "dd-MMM-yy HH:mm")%>
                </td>
                <td>
                    <%# Eval("Operation_Type") %>
                </td>
                <td>
                    <%# Eval("MODIFIED_BY")%>
                </td>
                <td>
                    <%# Eval("PUBLISH_VERSION")%>
                </td>
              <%--  <td>
                    <a href="FileLoader.aspx?DocVer=<%# Eval("ID")%>-<%# Eval("PROCEDURE_ID") %>">
                        <%# Convert.ToString(Eval("FILESNAME"))%></a>
                </td>
                <td>
                    <a href="GetLatest.aspx?DocVer=<%# Eval("ID")%>-<%# Eval("PROCEDURE_ID") %>">
                        <%# Convert.ToString(Eval("FILESNAME"))%></a>
                </td>--%>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    </form>
</body>
</html>