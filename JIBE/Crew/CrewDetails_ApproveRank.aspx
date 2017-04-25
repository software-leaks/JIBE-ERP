<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_ApproveRank.aspx.cs"
    Inherits="Crew_CrewDetails_ApproveRank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <asp:Panel ID="pnlApproveRank" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel_ApproveRank" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;" cellspacing="10">
                    <tr>
                        <td>
                            Joining Rank
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRank_Approve" runat="server" Width="156px" DataSourceID="DataSource_RankApprove"
                                DataTextField="Rank_Short_Name" DataValueField="ID" CssClass="control-edit required">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="DataSource_RankApprove" runat="server" SelectMethod="Get_RankList"
                                TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                            <asp:HiddenField ID="hdnVoyageID_Approve" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Remarks
                        </td>
                        <td>
                            <asp:TextBox ID="txtApproveRankRemark" runat="server" TextMode="MultiLine" Height="70px"
                                Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnApproveRank" runat="server" Text="Approve" OnClick="btnApproveRank_Click"
                                ValidationGroup="ApproveRank" />
                            <asp:Button ID="btnCloseApproveRank" runat="server" Text="Close" OnClientClick="parent.hideModal('dvPopupFrame');return false;" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    </form>
</body>
</html>
