<%@ Page Title="Crew Workflow" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewWorkFlow.aspx.cs" Inherits="Crew_CrewWorkFlow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="text-align: center; overflow: auto;">
        <div style="border: 1px solid #B6DAFD; background-color: white; padding: 3px; margin: 5px;">
            <table style="background-color: #E8F3FE; width: 100%;">
                <tr>
                    <td style="width: 80px;text-align:left">
                        Staff Code
                    </td>
                    <td style="width: 80px;font-weight:bold;text-align:left">
                        <asp:Label ID="lblStaffCode" runat="server"></asp:Label>
                    </td>
                    <td style="width: 80px;text-align:left">
                        Staff Name
                    </td>
                    <td style="width: 250px; font-weight:bold;text-align:left">
                        <asp:Label ID="lblStaffName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 50px;text-align:left">
                        Rank
                    </td>
                    <td style="font-weight:bold;text-align:left">
                        <asp:Label ID="lblRank" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdnCrewrank" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
            <HeaderTemplate>
                <table border="0">
                    <tr>
            </HeaderTemplate>
            <ItemTemplate>
                <td style="vertical-align: top; text-align: center;">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Image ID="Img1" runat="server" ImageUrl='<%#"~/Images/" + Eval("Image").ToString() %>'
                                    Height="100px" />
                            </td>
                            <td>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Next.png" Visible='<%# Convert.ToBoolean(Eval("Continue")) %>'
                                    Height="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 200px">
                                <div style="text-align: left;">
                                    <asp:Repeater runat="server" ID="rpt2">
                                        <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="1">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="vertical-align: top; text-align: center;">
                                                    <asp:Image ID="Img2" runat="server" ImageUrl='<%#"~/Images/" + Eval("Image").ToString() %>'
                                                        Height="14px" />
                                                </td>
                                                <td style="padding-left: 5px; vertical-align: top; background-color: #F8EFFB">
                                                    <asp:Label ID="lblText" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td style="vertical-align: top; text-align: center;">
                                                    <asp:Image ID="Img2" runat="server" ImageUrl='<%#"~/Images/" + Eval("Image").ToString() %>'
                                                        Height="14px" />
                                                </td>
                                                <td style="padding-left: 5px; vertical-align: top; background-color: #F2FBEF">
                                                    <asp:Label ID="lblText" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </ItemTemplate>
            <FooterTemplate>
                </tr></table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
