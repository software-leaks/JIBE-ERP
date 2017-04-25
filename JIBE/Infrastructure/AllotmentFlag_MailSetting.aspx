<%@ Page Title="Mail Setting" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AllotmentFlag_MailSetting.aspx.cs" Inherits="Infrastructure_AllotmentFlag_MailSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Mail setting for Allotment Flag
    </div>
    
 <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <div class="page-content-main">
        <asp:UpdatePanel ID="updMail" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td style="font-weight: bold; text-align: left; width: 100px">
                                        User Name
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        <asp:TextBox ID="txtUsername" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Height="25px" OnClick="btnSearch_Click" />
                                    </td>
                                    <td style="padding-right: 25px">
                                        <asp:Button ID="btnSave" runat="server" Text="Save Changes" Height="25px" OnClick="btnSave_Click" />
                                        <br />
                                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Size="11px" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="max-height: 550px; overflow-x: hidden; overflow-y: auto; width: 520px">
                                            <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="false" DataKeyNames="USERID"
                                                Width="500px" CellPadding="4" CellSpacing="0" GridLines="None" EmptyDataText="no record found !"
                                                RowStyle-HorizontalAlign="Left" CssClass="gridmain-css" ShowHeaderWhenEmpty="true">
                                                <Columns>
                                                    <asp:BoundField DataField="NAME" HeaderText="User Name" />
                                                    <asp:BoundField DataField="MailID" HeaderText="E-mail" />
                                                    <asp:TemplateField HeaderText="Send Mail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkUser" runat="server" Checked='<%#Eval("ASSIGNED").ToString()=="0"?false:true%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px"/>
                                                <RowStyle CssClass="RowStyle-css" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css"/>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
