<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_CrewComplaints.aspx.cs"
    Inherits="Crew_CrewDetails_CrewComplaints" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewComplaints">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlViewCrewComplaints" runat="server" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; font-weight: bold; font-size: 12px;">
                        Complaints Escalated to Me
                    </td>
                    <td style="width: 50%; font-weight: bold; font-size: 12px;">
                        My Complaints
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; vertical-align: top; border: 1px solid #cccccc;">
                        <asp:Repeater ID="rptComplaintsToMe" runat="server">
                            <HeaderTemplate>
                                <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                    cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 100px">
                                        Complaint
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="lblJOB_DESCRIPTION" runat="server" Text='<%# Bind("JOB_DESCRIPTION") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vessel
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("vessel_name") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                    </td>
                                </tr>
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
                                <asp:Repeater runat="server" ID="rptLog" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("EscLog") %>'>
                                    <ItemTemplate>
                                        <tr class='<%# (((System.Data.DataRow)Container.DataItem)["Escalated_To"].ToString()==GetCrewID().ToString())?"required":"not-selected"%>'
                                            style="text-align: center;">
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["ESCALATED_ON"]%>
                                            </td>
                                            <td>
                                                <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["Escalated_By"] %>'
                                                    target="_blank">
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Escalated_By_Staff_Code"]%></a>
                                            </td>
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_By_Rank"]%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_by_Name"]%>
                                            </td>
                                            <td>
                                                <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["Escalated_To"] %>'
                                                    target="_blank">
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Staff_Code"]%></a>
                                            </td>
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Rank"]%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Name"]%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td style="width: 50%; vertical-align: top; border: 1px solid #cccccc;">
                        <asp:Repeater ID="rptMyComplaints" runat="server">
                            <HeaderTemplate>
                                <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                    cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 100px">
                                        Complaint
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="lblJOB_DESCRIPTION" runat="server" Text='<%# Bind("JOB_DESCRIPTION") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vessel
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("vessel_name") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                    </td>
                                </tr>
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
                                <asp:Repeater runat="server" ID="rptLog" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("MyLog") %>'>
                                    <ItemTemplate>
                                        <tr class='<%# (((System.Data.DataRow)Container.DataItem)["Escalated_By"].ToString()==GetCrewID().ToString())?"required":"not-selected"%>'
                                            style="text-align: center;">
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["ESCALATED_ON"]%>
                                            </td>
                                            <td>
                                                <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["Escalated_By"] %>'
                                                    target="_blank">
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Escalated_By_Staff_Code"]%></a>
                                            </td>
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_By_Rank"]%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_by_Name"]%>
                                            </td>
                                            <td>
                                                <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["Escalated_To"] %>'
                                                    target="_blank">
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Staff_Code"]%></a>
                                            </td>
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Rank"]%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Name"]%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
