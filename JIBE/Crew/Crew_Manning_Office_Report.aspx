<%@ Page Title="Manning Office Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Crew_Manning_Office_Report.aspx.cs" Inherits="Crew_Crew_Manning_Office_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: left; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Manning Office Report"></asp:Label>
                </td>
                </tr>
                </table>
                </div>
                <br />
  <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;overflow:scroll">

<asp:GridView ID="grdManning" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" CellPadding="3"
                        CellSpacing="0" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                        GridLines="None"  AllowPaging="false" 
                        AllowSorting="true" CssClass="GridView-css">
<Columns>
<asp:TemplateField HeaderText="Manning office" ItemStyle-HorizontalAlign="left">
<ItemTemplate>
<asp:Label ID="lblManningOff" ClientIDMode="Static" runat="server" Text='<%#Eval("Company_Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="Processing Fees Officers ($)" DataField="PF_Officers" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Processing Fees Jr.Officers ($)" DataField="PF_Officers_Jnr" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Processing Fees Ratings ($)" DataField="PF_Ratings" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Agency Fees Officers(VMT)($)" DataField="AF_Officers" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Agency Fee Jr.Officers ($)" DataField="AF_Officers_Jnr" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Agency Fee Ratings ($)" DataField="AF_Ratings" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Officers On Board" DataField="Officer_ONBD" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Jr.Officers On Board" DataField="JrOfficer_ONBD" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField HeaderText="Ratings On Board" DataField="Ratings_ONBD" ItemStyle-HorizontalAlign="Center"/>
</Columns>
<HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />

</asp:GridView>
</div>
</div>
</asp:Content>

