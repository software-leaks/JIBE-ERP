<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_InterviewResult.aspx.cs"
    Inherits="Crew_CrewDetails_InterviewResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .RowStyle-css-new
        {
            background: url(../Images/grid-header-gray.png) left -10px repeat-x;
        }
    </style>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewInterviewResults">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:GridView ID="gvBriefResult" runat="server" AllowSorting="True" AutoGenerateColumns="false"
            OnRowDataBound="GridView_BriefResult_RowDataBound" GridLines="None" CellPadding="3"
            CellSpacing="1" Width="100%" Style="margin-top: 10px;" CssClass="GridView-css">
            <Columns>
                <asp:TemplateField HeaderText="Briefing Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkBriefingGivenTo" style="text-decoration:underline;" runat="server" Text='<%#Eval("Interview_Name")%>'
                            Target="_blank" NavigateUrl=''></asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%#Eval("Rank_Short_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VesselName")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Briefing done by" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblInterviewer" runat="server" Text='<%#Eval("Interviewer")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Briefing Date" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblBriefingDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("InterviewDate"))) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnClientClick='<%#"DeleteBrief(" + Eval("ID").ToString()+ "," + Eval("CrewID").ToString() + "," + Session["UserID"].ToString() +"); return false;" %>'
                            ForeColor="Black" ToolTip="Remove Item" ImageUrl="~/Images/delete.png" Height="16px">
                        </asp:ImageButton>
                    </ItemTemplate>
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
        <asp:GridView ID="GridView_InterviewResult" runat="server" AllowSorting="True" AutoGenerateColumns="false"
            OnRowDataBound="GridView_InterviewResult_RowDataBound" GridLines="None" CellPadding="3"
            CellSpacing="1" Style="margin-top: 10px;" Width="100%" CssClass="GridView-css">
            <Columns>
                <asp:TemplateField HeaderText="Interview Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkInterview_Name"  style="text-decoration:underline;" runat="server" Text='<%#Eval("Interview_Name")%>' 
                            Target="_blank" NavigateUrl=''></asp:HyperLink>
                        <asp:ImageButton ID="imgReferenceCheckNotDone" ImageAlign="AbsMiddle" ImageUrl="~/Images/Round-Red-icon.png"
                            ToolTip="Reference check is not done" Height="16px" Width="16px" runat="server"
                            Visible='<%#Eval("ReferenceCheckDone").ToString() == "1"?true:false%>' OnClientClick='<%# "ReferenceDetails("+ Eval("CrewID").ToString() +",0);return false;"%>'
                            AlternateText="info" style="margin-left:5px;" />
                        <asp:ImageButton ID="imgReferenceCheckDone" ImageAlign="AbsMiddle" ImageUrl="~/Images/Round-Green-icon.png"
                            ToolTip="Reference Check Done" Height="16px" Width="16px" runat="server" Visible='<%#Eval("ReferenceCheckDone").ToString() == "2"?true:false%>'
                            OnClientClick='<%# "ReferenceDetails(" + Eval("CrewID").ToString() +",1);return false;"%>'
                            AlternateText="info" style="margin-left:5px;" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%#Eval("Rank_Short_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Planned Interviewer" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblPlannedInterviewer" runat="server" Text='<%#Eval("PlannedInterviewer")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Interviewer" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblInterviewer" runat="server" Text='<%#Eval("Interviewer")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("PlannedBy")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal_Marks" runat="server" Text='<%#Eval("Total_Marks")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblMarks" runat="server" Text='<%#Eval("UserMarks")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approval" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblApproval" runat="server" Text='<%#Eval("Approval")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="(%) Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblAvgMarks" runat="server" Text='<%#Eval("Avg_Marks","{0:00.0}").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnClientClick='<%#"DeleteInterview(" + Eval("ID").ToString()+ "," + Eval("CrewID").ToString() + "," + Session["UserID"].ToString() +"); return false;" %>'
                            ForeColor="Black" ToolTip="Remove Item" ImageUrl="~/Images/delete.png" Height="16px">
                        </asp:ImageButton>
                    </ItemTemplate>
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
