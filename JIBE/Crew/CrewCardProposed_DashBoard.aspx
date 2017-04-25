<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewCardProposed_DashBoard.aspx.cs"
    Inherits="Crew_CrewCardProposed_DashBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewCardProposed_DashBoard">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            ForeColor="#333333" BorderColor="#336666" BorderStyle="Solid" BorderWidth="0px"
            CellPadding="2" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
            GridLines="None" DataKeyNames="ID" AllowPaging="True" AllowSorting="false" PageSize="30"
            Font-Size="11px" CssClass="Grid_CSS">
            <Columns>
                <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "../crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="pin-it"></asp:HyperLink>
                        <asp:Label ID="lblX" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:HyperLink ID="lblSTAFF_NAME" runat="server" NavigateUrl='<%# "../crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                            Target="_blank" Text='<%# Eval("Staff_FullName")%>' ForeColor="Black"></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_short_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vessel">
                    <ItemTemplate>
                        <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposed By" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblProposedBy" runat="server" Text='<%# Eval("ProposedBy")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Propose Date" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblDate_Of_Creation" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <div class='<%# Eval("CardType").ToString().Replace(" ","") + "_" +  Eval("ApprovalStatus").ToString()%>'
                            onclick="showDiv('dvProposeYellowCard',<%# Eval("CrewID")%>);return false;">
                            <asp:Label ID="lblCardStatus" runat="server" Text='<%# Eval("CardType").ToString() + " " +  Eval("ApprovalStatus").ToString()%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <ItemStyle Width="150px" Font-Size="10px" HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="HeaderStyle-css-dash" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css-dash" />
            <RowStyle CssClass="RowStyle-css-dash" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
