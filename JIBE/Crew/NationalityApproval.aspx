<%@ Page Title="Nationality Approval" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NationalityApproval.aspx.cs" Inherits="Crew_NationalityApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Nationality Approval"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                        CellSpacing="0" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                        GridLines="None" DataKeyNames="ID" AllowPaging="false" AllowSorting="true" CssClass="GridView-css">
                        <Columns>
                            <asp:TemplateField HeaderText="Approvals" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <table style="border: 1px solid gray; margin: 10px; width: 100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="width: 150px">
                                                            Vessel:
                                                        </td>
                                                        <td style="font-weight: bold; width: 200px;">
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Staff Name:
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>'
                                                                Target="_blank" Text='<%# Eval("Staff_FullName")%>' CssClass="pin-it"></asp:HyperLink>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Joining Rank:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Rank_Name") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Nationality:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("Nationality") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Event Date:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Event_Date"))) %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Event Port:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("Port_Name") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Requestor:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Requestor") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Remarks:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("Sender_Remarks") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 300px">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Approver Remarks:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtApproverRemarks" runat="server" TextMode="MultiLine" Height="100"
                                                                Width="200"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: right">
                                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnCommand="btnApprove_Click"
                                                                CommandArgument='<%#Eval("ID") %>' />
                                                            <asp:Button ID="btnReject" runat="server" Text="Reject" OnCommand="btnReject_Click"
                                                                CommandArgument='<%#Eval("ID") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
