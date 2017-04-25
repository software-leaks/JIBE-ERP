<%@ Page Title="Equipment Replacement History" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="TEC_Equipment_Replacement_History.aspx.cs" Inherits="Technical_PMS_TEC_Equipment_Replacement_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Equipment Replacement History
    </div>
    <div class="page-content">
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%; border-collapse: collapse; margin-bottom: 5px">
                    <tr>
                        <td class="tdh">
                            Fleet :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                Font-Size="11px" Height="20px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                Width="124px">
                                <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdh">
                            Function :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlFunction" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnRetrieve" runat="server" Height="25px" Text="Search" Width="150px"
                                OnClick="btnRetrieve_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Vessel :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="DDLVessel" runat="server" AutoPostBack="true" Font-Size="11px"
                                Height="20px" Width="124px" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="tdh">
                            System /Location :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlSystem_location" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSystem_location_SelectedIndexChanged"
                                Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnClearFilter" runat="server" Height="25px" Text="Clear Filter"
                                Width="150px" OnClick="btnClearFilter_Click" ToolTip="Clear Filter" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh" colspan="3">
                            Subsystem / Location :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlSubSystem_location" runat="server" Width="150px" AppendDataBoundItems="true">
                                <asp:ListItem Text="-ALL-" Value="0,0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:GridView ID="gvEQPHistory" runat="server" EmptyDataText="NO RECORD FOUND" AutoGenerateColumns="false"
                                CellPadding="4" ShowHeaderWhenEmpty="true" Width="100%" AllowSorting="false"
                                CssClass="gridmain-css" GridLines="None">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:BoundField DataField="MainEquipment" HeaderText="Main Equipment" />
                                    <asp:BoundField DataField="ACTIVE_EQUIPMENT" HeaderText="Replaced Equipment" />
                                    <asp:BoundField DataField="SPARE_EQUIPMENT" HeaderText="Replaced with" />
                                    <asp:BoundField DataField="RPDATE" HeaderText="Date" />
                                    <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <auc:CustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindItems" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
