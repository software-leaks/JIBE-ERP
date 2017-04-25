<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Log.aspx.cs"
    Inherits="Crew_CrewDetails_Log" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewLogGrid">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:GridView ID="GridView_Log" runat="server" AllowSorting="False" AutoGenerateColumns="false"
            GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css">
            <Columns>
                <asp:TemplateField HeaderText="Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Date_of_creation") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_of_creation"))) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Posted By">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Feedback">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Remark").ToString().Replace("\n","<br>") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkAttachment" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/CrewDocuments/" + Eval("AttachmentPath").ToString()%>'
                            Target="_blank" Visible='<%#Eval("AttachmentPath").ToString()==""?false:true%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
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
