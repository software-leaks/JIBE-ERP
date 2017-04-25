<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminForm.aspx.cs" Inherits="AdminForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
    <script src="JS/common.js" type="text/javascript"></script>

    <script type="text/javascript">
    function deleteStatus()
    {
    return window.confirm('are you sure you want to delete? ');
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="small"></asp:Label>
    </div>
    <div>
        <table cellspacing="0" cellpadding="2" border="0" style="border-width: 1px; border-style: Solid;
            border-color: #E6E2D8; background-color: #F2F5A9; border-collapse: collapse;
            margin-left: 20px; width: 90%">
            <tr>
                <td>
                    <span style="font-size: 16px; font-weight: bold;">User Details</span>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="2" border="0" style="border: 1px Solid #E6E2D8;
            border-collapse: collapse; background-color: #F2F5A9; margin-top: 5px; margin-left: 20px;
            width: 90%">
            <tr>
                <td>
                    <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="TextStyle"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtuser" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="TextStyle"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFName" runat="server" Width="100px" TabIndex="5"></asp:TextBox>
                </td>
                <td style="width: 90px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="TextStyle"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPWd" runat="server" Width="100px" TabIndex="2" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="TextStyle"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLNAme" runat="server" Width="100px" TabIndex="6"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="74px" TabIndex="9" OnClick="btnSave_Click" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblRePassword" runat="server" Text="Re-type Password" CssClass="TextStyle"></asp:Label>
                </td>
                <td valign="top">
                    <asp:TextBox ID="txtRePwd" runat="server" Width="100px" TabIndex="3" TextMode="Password"></asp:TextBox>
                </td>
                <td valign="top">
                    <asp:Label ID="lblMiddleName" runat="server" Text=" Middle Name" CssClass="TextStyle"></asp:Label>
                </td>
                <td valign="top">
                    <asp:TextBox ID="txtMName" runat="server" Width="100px" TabIndex="7"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                        Width="70px" OnClick="btnCancel_Click" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="TextStyle"></asp:Label>
                </td>
                <td valign="top">
                    <asp:DropDownList ID="DDLAccessLevel" runat="server" Width="107px" TabIndex="4" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="top">
                    <asp:Label ID="lblEmail" runat="server" Text="Email-id" CssClass="TextStyle"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtEmail" runat="server" Width="201px" TabIndex="8" Text="@smsship.com"></asp:TextBox>
                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Invalid format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table cellspacing="0" cellpadding="2" border="0" style="border: 1px Solid #E6E2D8;
            border-collapse: collapse; margin-top: 5px; margin-left: 20px; width: 90%">
            <tr>
                <td>
                    <asp:GridView ID="UserGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="UserGrid_PageIndexChanging"
                        PageSize="10" DataKeyNames="UserID" ForeColor="#333333" GridLines="None" AllowSorting="true"
                        OnSorting="UserGrid_Sorting">
                        <PagerSettings NextPageText="&gt;" PreviousPageText="&lt;" />
                        <RowStyle HorizontalAlign="Center" Font-Size="10px" BackColor="#EFF3FB"  />
                        <Columns>
                            <asp:TemplateField HeaderText="UserID" SortExpression="UserID">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Middle Name" SortExpression="MiddleName">
                                <ItemTemplate>
                                    <asp:Label ID="lblMiddleName" runat="server" Text='<%# Eval("MiddleName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name" SortExpression="Lastname">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastname" runat="server" Text='<%# Eval("Lastname")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" SortExpression="MailId">
                                <ItemTemplate>
                                    <asp:Label ID="lblMailId" runat="server" Text='<%# Eval("MailId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Access Level" SortExpression="AccessLevel">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccessLevel" runat="server" Text='<%# Eval("AccessLevel")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkViewUser" runat="server" ImageUrl="~/Images/edit.gif" CommandArgument='<%# Eval("UserID")%>'
                                        CommandName="ShowUser" OnCommand="ShowUser" CausesValidation="False"></asp:ImageButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lblDelete" runat="server" Text="Delete" ImageUrl="~/Images/delete.gif"
                                        OnClientClick="javascript:return deleteStatus();" CommandArgument='<%# Eval("UserID") %>'
                                        CommandName="DeleteByUserID" OnCommand="DeleteByUserID"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10px"
                             />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
