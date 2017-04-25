<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewComplaintLog.aspx.cs"
    Inherits="Crew_CrewComplaintLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Repeater ID="rptComplaintsToDPA" runat="server">
            <HeaderTemplate>
                <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                    cellspacing="0">
                    <tr style="background-color: #627AA8; color: Aqua; font-weight: bold; text-align: center;">
                        <td>
                            <asp:Label ID="lbl1" runat="server" Text="Escalated On"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lbl2" runat="server" Text="Escalated By"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lbl3" runat="server" Text="Escalated To"></asp:Label>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="text-align: center;">
                    <td>
                        <%# Eval("Escalated_On")%>
                    </td>
                    <td>
                        <a href='CrewDetails.aspx?ID=<%# Eval("Escalated_By") %>' target="_blank">
                            <%# Eval("Escalated_By_Staff_Code")%></a>
                    </td>
                    <td>
                        <%# Eval("Escalated_By_Rank")%>
                    </td>
                    <td style="text-align: left;">
                        <%# Eval("Escalated_by_Name")%>
                    </td>
                    <td>
                        <a href='CrewDetails.aspx?ID=<%# Eval("Escalated_To") %>' target="_blank">
                            <%# Eval("Escalated_To_Staff_Code")%></a>
                    </td>
                    <td>
                        <%# Eval("Escalated_To_Rank")%>
                    </td>
                    <td style="text-align: left;">
                        <%# Eval("Escalated_To_Name")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
