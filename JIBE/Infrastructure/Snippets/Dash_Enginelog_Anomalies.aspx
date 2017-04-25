<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dash_Enginelog_Anomalies.aspx.cs" Inherits="Infrastructure_Snippets_Dash_Enginelog_Anomalies" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<style type="text/css">
.AnomalyCell
{
    background-color:Red;
}
.NoAnomalyCell
{
    background-color:Gray;
}
</style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
    <div id="dvEngineLog">
         <asp:GridView ID="gvEnginelogAnomalies" GridLines="None" runat="server" 
            EmptyDataText="No record found !" CellPadding="3"
            AutoGenerateColumns="false" Width="100%" 
            onrowdatabound="gvEnginelogAnomalies_RowDataBound">
            <RowStyle CssClass="RowStyle-css-dash" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css-dash" />
            <HeaderStyle CssClass="HeaderStyle-css-dash" />
            <Columns>
                <asp:BoundField HeaderText="Vessel" DataField="Vessel_Name" />
                <asp:BoundField HeaderText="Today" DataField="E_countDate0" />
                <asp:BoundField HeaderText='Day1' DataField="E_countDate1" />
                 <asp:BoundField HeaderText='Day2' DataField="E_countDate2" />
                  <asp:BoundField HeaderText='Day3' DataField="E_countDate3" />
                   <asp:BoundField HeaderText='Day4' DataField="E_countDate4" />
                    <asp:BoundField HeaderText='Day5' DataField="E_countDate5" />
                     <asp:BoundField HeaderText='Day6' DataField="E_countDate6" />

            <%--       <asp:BoundField HeaderText="Today" DataField="ENT_countDate0"  />
                <asp:BoundField HeaderText='Day1' DataField="ENT_countDate1"    />
                 <asp:BoundField HeaderText='Day2' DataField="ENT_countDate2"    />
                  <asp:BoundField HeaderText='Day3' DataField="ENT_countDate3"    />
                   <asp:BoundField HeaderText='Day4' DataField="ENT_countDate4"   />
                    <asp:BoundField HeaderText='Day5' DataField="ENT_countDate5"  />
                     <asp:BoundField HeaderText='Day6' DataField="ENT_countDate6"    />--%>
            </Columns>
        </asp:GridView>
    </div>

    
    </form>
</body>
</html>