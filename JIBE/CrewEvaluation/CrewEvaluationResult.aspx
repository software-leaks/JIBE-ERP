<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewEvaluationResult.aspx.cs"
    Inherits="Crew_CrewEvaluationResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewEvaluationResults">
        <asp:GridView ID="GridView_Evaluation" runat="server" AllowSorting="True" AutoGenerateColumns="false"
            GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="crew-grid-css" OnRowDataBound="GridView_Evaluation_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Evaluation Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblEvaluation_Name" runat="server" Text='<%#Eval("Evaluation_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="300px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%#Eval("Rank_Short_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Planned Month" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("MonthYear")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%#Eval("Vessel_Short_Name")%>'
                            class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Evaluator" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblEvaluator" runat="server" Text='<%#Eval("Evaluator")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal_Marks" runat="server" Text='<%#Eval("Total_Marks")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblMarks" runat="server" Text='<%#Eval("Marks")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="(%) Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblAvgMarks" runat="server" Text='<%#Eval("AvgMarks","{0:00.00}").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEType" runat="server" Text='<%# Eval("ID").ToString()=="0"? "Un-Planned":"Planned" %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Evaluation" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="btnEvaluate" runat="server" Target="_blank" Text="Perform Evaluation" CssClass="linkImageBtn"></asp:HyperLink>
                        <asp:HyperLink ID="btnViewEvaluation" runat="server" Target="_blank" Text="View Evaluation" Visible='<%# Convert.ToBoolean(Eval("Status")) %>'  CssClass="linkImageBtn"></asp:HyperLink>
                        <%--<asp:Button ID="btnEvaluate" runat="server" Text="Perform Evaluation" CommandArgument='<%#Eval("Evaluation_ID").ToString() + "&M=" + Eval("MonthYear").ToString()+ "&SchID=" + Eval("ID").ToString() %>'
                            OnClick="btnEvaluate_Click" Visible='<%# !Convert.ToBoolean(Eval("Status")) %>'
                            Enabled="false"></asp:Button>
                        <asp:LinkButton ID="btnViewEvaluation" runat="server" Text="View Evaluation" CommandArgument='<%#Eval("Evaluation_ID").ToString() + "&M=" + Eval("MonthYear").ToString() + "&DtlID=" + Eval("CrewEvaluation_ID").ToString() %>'
                            OnClick="btnViewEvaluation_Click" Visible='<%# Convert.ToBoolean(Eval("Status")) %>'></asp:LinkButton>--%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle CssClass="crew-grid-alternaterow" />
            <EditRowStyle CssClass="crew-grid-editrow" />
            <HeaderStyle CssClass="crew-grid-header" />
            <RowStyle CssClass="crew-grid-row" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
