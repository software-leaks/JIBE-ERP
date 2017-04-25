<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewEvent.aspx.cs" Inherits="Crew_ViewEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" style="border: 1px solid #610B5E; width: 100%;">
            <tr>
                <th colspan="7" style="background-color: #336666; color: White">
                    Off-Signers
                </th>
                <th>
                </th>
                <th colspan="7" style="background-color: #336666; color: White">
                    On-Signers
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt1" OnItemCommand="rpt1_ItemCommand" OnItemDataBound="rpt1_ItemDataBound">
                <ItemTemplate>
                    <tr style="background-color: #F3E2A9; color: Black; font-weight: bold;">
                        <td colspan="10" style="padding: 3px">
                            Vessel:
                            <%#Eval("Vessel_short_name")%>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Port:
                            <%#Eval("Port_Name")%>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date:
                            <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Event_Date")))%>
                        </td>
                        <td colspan="5" style="text-align: right;">
                            Status:
                            <%#(Eval("Event_Status").ToString()=="0")?"Closed":"Open"%>
                        </td>
                    </tr>
                    <tr style="background-color: #A9F5D0; color: Black; text-align: left;">
                        <th style="width: 60px; text-align: left;">
                            S/Code
                        </th>
                        <th style="width: 60px; text-align: left;">
                            Rank
                        </th>
                        <th style="width: 250px; text-align: left;">
                            Staff Name
                        </th>
                        <th style="width: 80px; text-align: left;">
                            EOC Date
                        </th>
                        <th style="width: 60px; text-align: left;">
                            Nationality
                        </th>
                        <th style="width: 60px; text-align: left;">
                        </th>
                        <th style="width: 20px">
                        </th>
                        <th style="background-color: white;">
                        </th>
                        <th style="width: 60px; text-align: left;">
                            S/Code
                        </th>
                        <th style="width: 60px; text-align: left;">
                            Rank
                        </th>
                        <th style="text-align: left;">
                            Staff Name
                        </th>
                        <th style="width: 80px; text-align: left;">
                            Readiness
                        </th>
                        <th style="width: 60px; text-align: left;">
                            Nationality
                        </th>
                        <th style="width: 60px; text-align: left;">
                        </th>
                        <th style="width: 20px">
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt2" OnItemCommand="rpt2_ItemCommand" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("EventMembers") %>'
                        OnItemDataBound="rpt2_ItemDataBound">
                        <ItemTemplate>
                            <tr style="background-color: #E0F8F7; color: Black; border-bottom: 1px solid gray;">
                                <td>
                                    <asp:HyperLink ID="lnkStaffCode_off" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + ((System.Data.DataRow) Container.DataItem)["CrewID_Off"].ToString() %>'
                                        Target="_blank" Text='<%# ((System.Data.DataRow) Container.DataItem)["Staff_Code_Off"] %>'></asp:HyperLink>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Rank_Off"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow) Container.DataItem)["Staff_Name_Off"] %>
                                </td>
                                <td>
                                    <%#UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Est_Sing_Off_Date"]))%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Nationality_OFF"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["AdditionalOFF"]%>
                                </td>
                                <td>
                                </td>
                                <td style="background-color: white;">
                                </td>
                                <td>
                                    <asp:HyperLink ID="lnkStaffCode_on" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + ((System.Data.DataRow) Container.DataItem)["CrewID_ON"].ToString() %>'
                                        Target="_blank" Text='<%# ((System.Data.DataRow) Container.DataItem)["Staff_Code_ON"] %>'></asp:HyperLink>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Rank_On"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow) Container.DataItem)["Staff_Name_ON"] %>
                                </td>
                                <td>
                                   <%#UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Available_From_Date"]))%>
                                   <%-- <%# ((System.Data.DataRow)Container.DataItem)["Available_From_Date"]%>--%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Nationality_ON"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["AdditionalOn"]%>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- <tr>
                        <td colspan="13">
                            &nbsp;
                        </td>
                    </tr>--%>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
