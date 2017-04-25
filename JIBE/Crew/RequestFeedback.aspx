<%@ Page Title="Crew Feedback Request" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="RequestFeedback.aspx.cs" Inherits="Crew_RequestFeedback" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
  <%--<script src="../Scripts/jquery.min.js" type="text/javascript"></script>--%>
  <script src="../Scripts/common_functions.js" type="text/javascript"></script>
<%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" style="margin: 2px; border: 1px solid #cccccc; height: 20px;
        vertical-align: bottom; background: url(../Images/bg.png) left -10px repeat-x;
        color: Black; text-align: left; padding: 2px; background-color: #F6CEE3;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 33%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Crew Feedback Request"></asp:Label>
                </td>
                <td style="width: 33%;">
                </td>
            </tr>
        </table>
    </div>
    <div id="grid-container" style="margin: 2px; padding: 2px; border: 1px solid #cccccc;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr class="gradiant-css-orange" style="font-weight: bold">
                        <td class="gradiant-css-orange" style="padding: 2px;">
                            Select Crew:
                        </td>
                        <td style="width: 60px">
                        </td>
                        <td class="gradiant-css-orange" style="padding: 2px;">
                            Request Feedback From:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="Div2" style="padding: 2px; border: 1px solid gray; height: 500px; overflow: auto">
                                <asp:GridView ID="GridView_CrewList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#336666" BorderStyle="Double" BorderWidth="0px" CellPadding="2"
                                    EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="Horizontal"
                                    DataKeyNames="ID" Font-Size="12px" AllowSorting="false" CssClass="Grid_CSS">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" ForeColor="white" />
                                                <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Eval("ID")%>' />
                                                <asp:HiddenField ID="hdnStaffCode" runat="server" Value='<%# Eval("staff_Code")%>' />                                        
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OnBD" SortExpression="Vessel_Short_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Staff Code" SortExpression="STAFF_CODE" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                    <%# Eval("staff_Code")%></a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank" SortExpression="Rank_Name" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" SortExpression="Staff_FullName" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("Staff_FullName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="280px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                    <PagerStyle Font-Size="Larger" CssClass="pager" BackColor="#336666" ForeColor="White"
                                        HorizontalAlign="Center" />
                                    <RowStyle CssClass="GridRow_CSS" BackColor="White" ForeColor="#333333" />
                                </asp:GridView>
                            </div>
                            <div style="margin: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                background-color: #F6CEE3;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <uc1:ucCustomPager ID="ucCustomPager_CrewList" runat="server" RecordCountCaption="&nbsp;&nbsp;Total Staff"
                                                OnBindDataItem="FillGridViewAfterSearch" />
                                        </td>
                                        
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                        </td>
                        <td>
                            <div id="dvUserList" style="padding: 2px; border: 1px solid gray; height: 500px;
                                overflow: auto">
                                <asp:GridView ID="GridView_UserList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#336666" BorderStyle="Double" BorderWidth="0px" CellPadding="2"
                                    Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="Horizontal"
                                    DataKeyNames="UserID" Font-Size="12px" AllowSorting="false" CssClass="Grid_CSS">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" ForeColor="white" />
                                                <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# Eval("UserID")%>' />
                                                <asp:HiddenField ID="hdnEmailID" runat="server" Value='<%# Eval("MailID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" SortExpression="UserName" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mail ID" SortExpression="MailID" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMailID" runat="server" Text='<%# Eval("MailID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="280px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                    <PagerStyle Font-Size="Larger" CssClass="pager" BackColor="#336666" ForeColor="White"
                                        HorizontalAlign="Center" />
                                    <RowStyle CssClass="GridRow_CSS" BackColor="White" ForeColor="#333333" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnSendRequest" runat="server" Text="Send Feedback Request" OnClick="btnSendRequest_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
