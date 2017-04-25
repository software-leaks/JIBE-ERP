<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Followups.aspx.cs" Inherits="Operations_TaskPlanner_Task_Followups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: #efefef; border: 2px solid gray; padding: 10px; padding: 10px;font-family:Tahoma; font-size:11px;">
        <asp:GridView ID="grdFollowUps" runat="server" BackColor="White" BorderColor="#999999"
            AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="1"
            EnableModelValidation="True" GridLines="Vertical" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Date" SortExpression="DATE_CREATED">
                    <ItemTemplate>
                        <asp:Label ID="lblDATE_CREATED" runat="server" Text='<%#Eval("DATE_CREATED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_CREATED","{0:d/MM/yy HH:mm}")   %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" SortExpression="LOGIN_NAME">
                    <ItemTemplate>
                        <asp:Label ID="lblLOGIN_NAME" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Followup" SortExpression="FOLLOWUP" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12px" ItemStyle-Width="350px" ItemStyle-Wrap="true"   >
                   <ItemTemplate>
                    <%#Eval("FOLLOWUP")%>
                        <%-- <asp:Label ID="lblFOLLOWUP" runat="server" Text='<%#Eval("FOLLOWUP").ToString().Replace("\n","<br>") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <EmptyDataTemplate>
                <asp:Label ID="ldl1" runat="server" Text="No followups !!"></asp:Label>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
