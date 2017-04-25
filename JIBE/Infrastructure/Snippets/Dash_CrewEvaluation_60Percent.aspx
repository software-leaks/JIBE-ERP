﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dash_CrewEvaluation_60Percent.aspx.cs"
    Inherits="Infrastructure_Snippets_Dash_CrewEvaluation_60Percent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvEvaluations_DashBoard">
        <asp:GridView ID="gvEvaluationSchedules" GridLines="None" runat="server" EmptyDataText="No record found !" CellPadding="3"
            AutoGenerateColumns="false" Width="100%">
            <RowStyle CssClass="RowStyle-css-dash" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css-dash" />
            <HeaderStyle CssClass="HeaderStyle-css-dash" />
            <Columns>
                <asp:BoundField HeaderText="Vessel" DataField="Vessel_Short_Name" />
                <asp:TemplateField HeaderText="S/Code">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkSCode" runat="server" Target="_blank" Text='<%#Eval("STAFF_CODE") %>'
                            NavigateUrl='<%#"~/CREW/CREWDETAILS.aspx?ID="+Eval("CREWID").ToString()%>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Staff Name" DataField="Staff_FullName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Rank" DataField="Rank_Short_Name" />
                <asp:TemplateField HeaderText="Evaluation Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDueDate" runat="server" Text='<%#Eval("evaluation_date","{0:dd/MMM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Marks(%)" DataField="AvgMarks" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
