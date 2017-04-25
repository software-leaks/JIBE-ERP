<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewMissingDataDetails.aspx.cs" MasterPageFile="~/Site.master" Title="Crew Missing Data Detail Report"
    Inherits="Crew_CrewMissingDataDetails" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
         body, html
        {
            overflow-x: hidden;
        }
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        .th
        {
            /*background-color:#627AA8;*/
            color: #333;
            font-size: 12px;
            padding: 4px;
        }
    </style>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 33%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Missing Data Report"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                    <asp:ImageButton ID="ImageButton1" src="../Images/Excel-icon.png" Height="20px" runat="server"
                        Visible="true" AlternateText="Print" OnClick="ImgExportToExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="page-content-main">
        <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
            <table cellspacing="10" style="width: 100%">
                <tr>
                    <td class="th">
                        <b>CREW JOINED, BUT PARTICULARS NOT UPDATED - DETAILS</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRowCount" runat="server"></asp:Label>
                        <asp:GridView ID="GridView4" runat="server" CellPadding="4" CellSpacing="0" GridLines="None"
                            Width="100%" AutoGenerateColumns="False" CssClass="GridView-css" OnRowDataBound="GridView4_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Company_Name" HeaderText="Manning Office" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="200px">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MissingItem" HeaderText="Missing Item" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Vessel">
                                    <ItemTemplate>
                                        <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo'
                                            vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="St/Code">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>' CssClass="staffInfo"
                                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Staff_Code") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Staff_Surname" HeaderText="Staff Surname" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Staff_name" HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Left" >
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="rank_short_name" HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Staff_Birth_Date" HeaderText="D.O.B" HeaderStyle-HorizontalAlign="Left" >
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Staff_Nationality" HeaderText="Nationality" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Available_From_Date" HeaderText="Available From" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Event_Created_By" HeaderText="Added To Event By" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
     </asp:Content>
