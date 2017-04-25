<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_FBM.aspx.cs" Inherits="Crew_CrewDetails_FBM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvCrewFBM">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>        
        <asp:Panel ID="pnlViewFBMInfo" runat="server">
            <asp:GridView ID="gvFBMInfo" runat="server" AllowSorting="False" AutoGenerateColumns="false" DataSourceID= "ObjectDataSource_FBM"
                GridLines="None" CellPadding="1" CellSpacing="1" Width="60%" CssClass="GridView-css">
                <Columns>
                    <asp:TemplateField HeaderText="FBM No.">
                        <ItemTemplate >
                            <asp:LinkButton ID="lnkFBMNo" runat="server" Text='<%# Eval("FBM_NUMBER") %>'
                                OnClientClick='<%#"ViewFBMDetails(" + Eval("FBM_ID").ToString() + "," + Eval("CREATED_BY").ToString() +"); return false;" %>'></asp:LinkButton>
                         </ItemTemplate>
                       <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Read">
                        <ItemTemplate>
                            <asp:Label ID="lblDateRead" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_READ"))) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Vessel">
                      <ItemTemplate>
                            <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70px" />
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
            <asp:ObjectDataSource ID="ObjectDataSource_FBM" runat="server" 
                TypeName="SMS.Business.CREW.BLL_Crew_CrewDetails" SelectMethod="Get_CrewFBMInfo" >
           <SelectParameters>
                <asp:QueryStringParameter Name="CrewID" QueryStringField="CrewID" Type="Int32" />
           </SelectParameters>
    </asp:ObjectDataSource>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
