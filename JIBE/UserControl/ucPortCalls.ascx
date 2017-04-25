<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPortCalls.ascx.cs" Inherits="UserControl_ucPortCalls" %>
<div style="max-height: 250px; width: 800px; overflow: auto; text-align: center">
    <asp:GridView ID="gvPortCalls" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#336666" BorderStyle="Double" BorderWidth="1px" CellPadding="2"
        GridLines="Horizontal" DataKeyNames="Port_Call_ID" Font-Size="11px" Width="780px">
        <Columns>
            <asp:TemplateField HeaderText="Vessel" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Port Name">
                <ItemTemplate>
                    <asp:Label ID="lblPort_Name" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>
                    <asp:HiddenField ID="hdnPortID" runat="server" Value='<%# Eval("Port_ID")%>'></asp:HiddenField>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Arrival">
                <ItemTemplate>
                    <asp:Label ID="lblArrival" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival")))%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Departure" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblDeparture" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure")))%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owners Agent" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblOwners_Agent" runat="server" Text='<%# Eval("Owners_Agent")%>'
                        ToolTip='<%#Eval("Owners_Code") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="300px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Charterers Agent" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblCharterers_Agent" runat="server" Text='<%# Eval("Charterers_Agent")%>'
                        ToolTip='<%#Eval("Charterers_Code") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="300px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Select">
                <ItemTemplate>
                    <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                        CommandName="Select" OnCommand="lnkSelect_Click" AlternateText="Select"></asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="lblNoRec" runat="server" Text="No record found."></asp:Label>
        </EmptyDataTemplate>
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
            CssClass="pager" />
        <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#F7BE81" Font-Bold="True" ForeColor="Black" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#487575" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#275353" />
    </asp:GridView>
</div>
