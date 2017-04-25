<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_TravelLog.aspx.cs"
    Inherits="Crew_CrewDetails_TravelLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewTravelLog">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:GridView ID="GridView_TravelLog" runat="server" AutoGenerateColumns="False"
            DataKeyNames="ID" GridLines="None" CellPadding="3" CellSpacing="1" Width="100%"
            CssClass="GridView-css">
            <Columns>
                <asp:TemplateField HeaderText="From">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("travelFrom") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("travelTo") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Departure">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("departuredate"))) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Arrival">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("arrivaldate"))) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Flight No">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("flightNo")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("currentStatus")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkAttachment" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Travel/Attachment.aspx?RequestID=" + Eval("ID").ToString()%>'
                            Target="_blank" />
                    </ItemTemplate>
                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
                <HeaderStyle CssClass="HeaderStyle-css" />
                <PagerStyle CssClass="PagerStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
