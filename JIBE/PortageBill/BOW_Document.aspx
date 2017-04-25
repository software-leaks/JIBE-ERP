<%@ Page Title="BOW Documents" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="BOW_Document.aspx.cs" Inherits="PortageBill_BOW_Document" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: middle;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 0px; background-color: #F6CEE3; text-align: center; font-weight: bold">
       <asp:Label ID="lblpageheader" runat="server" ></asp:Label>
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829;width:100%;">
        <asp:GridView ID="gvBOWDocuments" runat="server" AutoGenerateColumns="False" Width="70%"
            EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CellSpacing="1"
            BackColor="#D8D8D8" CellPadding="5" GridLines="None" ShowFooter="false">
            <Columns>
            <asp:BoundField DataField="Staff_Code" HeaderText="Staff Code" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="Staff_Name" HeaderText="Staff Name" ItemStyle-Width="250px"/>
            <asp:BoundField DataField="Rank_Short_Name" HeaderText="Rank" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            
            <asp:HyperLinkField DataNavigateUrlFields="DOc_Path" HeaderText="BOW Attachment" DataNavigateUrlFormatString="../uploads/CrewAccount/{0}" Target="_blank"  Text="Doc Name" ItemStyle-HorizontalAlign="Center"   />

            </Columns>
            <EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
            <HeaderStyle CssClass="HeaderStyle-css" />
            <RowStyle CssClass="RowStyle-css" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <SelectedRowStyle BackColor="#FFFFCC" />
            <FooterStyle ForeColor="Black" HorizontalAlign="Right" Font-Bold="true" />
        </asp:GridView>
    </div>
</asp:Content>
