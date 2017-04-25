<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_EvaluationResult.aspx.cs"
    Inherits="Crew_CrewDetails_EvaluationResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/CrewDetails_DataHandler.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewEvaluationResults">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:GridView ID="GridView_Evaluation" runat="server" AllowSorting="True" AutoGenerateColumns="false"
            GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css"
            OnRowDataBound="GridView_Evaluation_RowDataBound">
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
                    <ItemStyle HorizontalAlign="Left" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal_Marks" runat="server" Text='<%#Eval("Total_Marks")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblMarks" runat="server" Text='<%#Eval("Marks")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="(%) Marks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblAvgMarks" runat="server" Text='<%#Eval("AvgMarks","{0:00.00}").ToString() %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblEType" runat="server" Text='<%# Convert.ToInt32(Eval("RuleID").ToString()) > 0 ? "Planned" : "Un-Planned" %>'></asp:Label>
                        <%--<asp:Label ID="lblEType" runat="server" Text='<%# Eval("ID").ToString()=="0"? "Un-Planned":"Planned" %>'></asp:Label>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDueDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DueDate"))) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Evaluation" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblPending" Text="Pending For Master" Visible='<%#!Convert.ToBoolean(Eval("Status").ToString()=="2" ? 1 : Eval("Status")) %>'
                            runat="server"></asp:Label>
                     
                           <asp:HyperLink ID="BtnPendingEvaluation" runat="server" Target="_blank" Text='<%# Eval("First_Name") %>'
                            Visible='<%#  Convert.ToBoolean(Eval("Status").ToString()=="2" ? 1 : 0) %>'
                            CssClass="linkImageBtn linkImageBtnDone"></asp:HyperLink>
                       
                        <asp:HyperLink ID="btnViewEvaluation" runat="server" Target="_blank" Text="View Evaluation"
                            Visible='<%# Convert.ToBoolean(Eval("Status").ToString()=="2" ? 0 : Eval("Status")) %>'
                            CssClass="linkImageBtn linkImageBtnDone"></asp:HyperLink>
                        <asp:Image runat="server" ID="ImgEvaluationBelowAverage" ImageUrl="~/Images/PoorFeedback.png" ToolTip="Evaluation Below Average"
                            Height="15px" Visible='<%# Convert.ToBoolean(Eval("BelowAvgStatus")) %>' onclick='<%# "ViewEvaluation(event,this," + Eval("CrewID").ToString()+ "," + Eval("CrewEvaluation_ID").ToString() +"); return false;" %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"  Width="130px" CssClass="grid-col-fixed" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" HeaderStyle-Width="40px">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkDeleteVoyage" runat="server" ImageUrl="~/images/delete.png"
                                CausesValidation="False" OnClientClick='<%#"DeleteEvaluation(" + Eval("ID").ToString()+ "," + Eval("CrewID").ToString() + "," + Eval("Voyage_ID").ToString() + "," + GetSessionUserID().ToString() + "); return false;" %>'
                                Visible='<%#  Convert.ToBoolean(Eval("DeleteStatus").ToString() == "0" ? 0 :1) %>'
                                AlternateText="Delete"></asp:ImageButton>
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
