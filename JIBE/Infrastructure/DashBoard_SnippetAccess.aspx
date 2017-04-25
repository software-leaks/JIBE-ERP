<%@ Page Title="Dashboard Snippet Access" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DashBoard_SnippetAccess.aspx.cs" Inherits="Infrastructure_DashBoard_SnippetAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
      Snippets Access
    </div>
    <asp:UpdatePanel ID="updssnp" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="left">
                        <table style="border: 1px solid gray; text-align: left">
                            <tr>
                                <td style="background-color: Orange; font-weight: bold; font-size: 11px; color: Black;
                                    width: 200px" align="left">
                                    Select User
                                </td>
                                <td style="background-color: Orange; font-weight: bold; font-size: 11px; color: Black;
                                    width: 400px" align="left">
                                    Snippets
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left">
                                    <div style="min-height: 400px; overflow: auto">
                                        <asp:ListBox ID="lstUserList" runat="server" Height="400px" Width="200px" AutoPostBack="true" 
                                            SelectionMode="Single" DataTextField="USERNAME" DataValueField="USERID" OnSelectedIndexChanged="lstUserList_SelectedIndexChanged">
                                        </asp:ListBox>
                                    </div>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <div style="min-height: 400px; overflow: auto">
                                        <asp:GridView ID="gvSnippets" AutoGenerateColumns="false" runat="server" ForeColor="Black">
                                            <Columns>
                                            <asp:BoundField DataField="VALUE" HeaderText="Dept. Name" HeaderStyle-Font-Size="11px" />
                                                <asp:TemplateField  HeaderText="Snippets Name" HeaderStyle-Font-Size="11px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chksnippet" runat="server" Text='<%#Eval("Snippet_name") %>' Checked='<%# Eval("access").ToString()=="1"?true:false %>'
                                                            ToolTip='<%#Eval("id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: Silver">
                                </td>
                                <td align="center" style="background-color: Silver">
                                    <asp:Button ID="btnSaveSnippets" Text="Save" Width="100px" runat="server" OnClick="btnSaveSnippets_Click" />
                                    <asp:Label ID="lblmsg" ForeColor="Red" Font-Size="11px" Font-Names="verdana"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
