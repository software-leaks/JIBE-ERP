<%@ Page Title="Cancel Log" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ReqsnCancelLog.aspx.cs" Inherits="Purchase_ReqsnCancelLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
  <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="text-align: center">
        <center>
            <asp:UpdatePanel ID="updHistory" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                Select Vessel:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvReqsnCancelLog" AutoGenerateColumns="false" Width="70%" DataSourceID="ObjectDataSourceLog"
                        runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="Vessel Name" DataField="Vessel_Name" />
                            <asp:BoundField HeaderText="Requisition Code" DataField="REQUISITION_CODE" />
                            <asp:BoundField HeaderText="Cancelled By" DataField="name" />
                            <asp:BoundField HeaderText="Cancelled Date" DataField="cancelleddate" />
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourceLog" SelectMethod="Get_CancelReqsn" TypeName="ClsBLLTechnical.TechnicalBAL"
                        runat="server">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlVessel" PropertyName="SelectedValue" Name="VesselCode" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
