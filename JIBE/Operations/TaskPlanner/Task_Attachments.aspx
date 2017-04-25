<%@ Page Title="Task Attachments" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Task_Attachments.aspx.cs" Inherits="TaskPlanner_Task_Attachments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="margin: 20px; padding: 10px; border: 2px solid #efefef;">
            <div style="font-family: Tahoma; font-size: 14px; font-weight: bold; padding: 5px;">
                Attachments</div>
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="gvAttachments" runat="server" AllowPaging="false" AllowSorting="true"
                    AutoGenerateColumns="false" BackColor="White" BorderStyle="None" CellPadding="4"
                    EnableModelValidation="True" GridLines="None" Width="100%" ShowHeader="false"
                    OnRowDataBound="gvAttachments_RowDataBound">
                    <AlternatingRowStyle BackColor="#DDeeEE" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblAttach_Name" runat="server" NavigateUrl='<%#Eval("Attach_Name") %>'
                                    Target="_blank" Text='<%#Eval("Attach_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Attachment" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <label id="Label1" runat="server">
                            No Attachment found !!</label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                    <PagerStyle CssClass="pager" Font-Size="16px" />
                </asp:GridView>
            </asp:Panel>
        </div>
    </center>
</asp:Content>
