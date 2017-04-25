<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocumentList.aspx.cs"
    Inherits="Crew_ViewDocumentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvContainer">
        <asp:GridView ID="GridView_Documents" runat="server" BackColor="White" BorderColor="White"
            BorderStyle="Ridge" BorderWidth="1px" CellPadding="2" GridLines="None" CellSpacing="1"
            Width="100%" AllowSorting="false" AutoGenerateColumns="False" DataKeyNames="DocID">
            <Columns>
                <asp:TemplateField HeaderText="Document Type" SortExpression="DocNo">
                    <ItemTemplate>
                        <asp:Label ID="lblDocTypeName" Text='<%#Eval("DocTypeName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Name" SortExpression="DocName">
                    <ItemTemplate>
                        <asp:Label ID="lblDocName" Text='<%#Eval("DocName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created On" SortExpression="Date_Of_Creation">
                    <ItemTemplate>
                        <asp:Label ID="lblDtCreated" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="Created_By_Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" Text='<%#Eval("Created_By_Name") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                            CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                            AlternateText="Delete"></asp:ImageButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#E6E6E6" ForeColor="Black" />
            <AlternatingRowStyle BackColor="#FBEFF5" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
