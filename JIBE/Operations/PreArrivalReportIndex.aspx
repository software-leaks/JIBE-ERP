<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreArrivalReportIndex.aspx.cs" Inherits="Operations_PreArrivalReportIndex" MasterPageFile="~/Site.master"  Title ="Pre Arrival Reports"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
  <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function showdetails(querystring) {
            var query = new Array();
            query = querystring.toString().split(',');
            window.open("PreArrivalReportDetails.aspx?Id=" + query[0] + "&VesselId=" + query[1]);
        }
        
    </script>   
</asp:Content>
<asp:Content ID="contentmain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
          Pre Arrival Report
    </div>
    <div>
        <table style="width:100%">
        <tr>
        
        <td  valign="top" style="width:100%"> 
        <asp:GridView  runat="server" ID= "gvPreArrivalReportIndex" AutoGenerateColumns="False"  EmptyDataText="No record found !" 
            AllowPaging="false" CellPadding="4" AllowSorting="True"   OnRowDataBound="gvPreArrivalReportIndex_RowDataBound"  Width="100%"
            GridLines="Horizontal" >
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                    <PagerSettings Mode="NumericFirstLast" />                        
                <PagerStyle CssClass="PagerStyle-css" />
                <PagerStyle Font-Size="Large" CssClass="pager" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />        
                <asp:BoundField DataField="Vessel_Id" HeaderText="Vessel_Id" Visible="false" />
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image ID="ImbCOCModified" runat="server" ImageUrl="~/images/EMail.png" CausesValidation="False"
                            Height="13px" ></asp:Image>
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ReportDate" HeaderText="Report Date" />
                <asp:BoundField DataField="Port_Name" HeaderText="Port" />
                <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel" />
                <asp:BoundField DataField="ETA" HeaderText="ETA" />
                <asp:BoundField DataField="ETD" HeaderText="ETD" />                                
            </Columns>
            </asp:GridView>
           
        </td>
           
        </tr>
        </table>
          
            <asp:GridView  runat="server" ID= "gvVessel" AutoGenerateColumns="False"  EmptyDataText="No record found !"  AllowPaging="false" Width="100%" GridLines="Horizontal" AutoGenerateSelectButton = "True"
             DataKeyNames="Vessel_Id" OnSelectedIndexChanged="gvVessel_SelectedIndexChanged"   OnRowDataBound="gv_RowDataBound" OnRowCommand="GridView1_RowCommand" Visible="false" style="display:none">
            <RowStyle CssClass="RowStyle-css" />
            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
            <Columns>
                <asp:BoundField DataField="Vessel_ID" HeaderText="Vessel_ID"  /> 
                <asp:TemplateField HeaderText="All Vessel" HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lblHeader1" runat="server" ForeColor="Blue" CommandArgument="AllVessel"
                            CommandName="AllVessel">Vessel </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="80px"   />
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
       
    </div>
</asp:Content>