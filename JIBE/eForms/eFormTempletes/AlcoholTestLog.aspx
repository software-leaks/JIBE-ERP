<%@ Page Title="eForms: Alcohal Testing" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AlcoholTestLog.aspx.cs" Inherits="AlcoholTestLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .eform-vertical-text
        {
            font: bold 14px verdana;
            font-weight: normal;
            writing-mode: tb-rl;
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Alcohol Test Log"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <table>
                <tr>
                    <td style="width: 200px">
                        Vessel Name:
                    </td>
                    <td style="width: 200px" class="eform-field-data">
                        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 200px">
                        Report Date:
                    </td>
                    <td class="eform-field-data">
                        <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="GridView_CargoHoldBilgeAlarm" DataKeyNames="ID" runat="server"
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" CssClass="GridView-css"
                            CellPadding="7" AllowPaging="false" Width="100%" ShowFooter="false" EmptyDataText="No Record Found"
                            CaptionAlign="Bottom" GridLines="Both">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <%# Eval("Test_Date_Time", "{0:dd/MM/yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Port">
                                    <ItemTemplate>
                                        <%# Eval("Port_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Latitude">
                                    <ItemTemplate>
                                        <%# Eval("Latitude")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Longitude">
                                    <ItemTemplate>
                                        <%# Eval("Longitude")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Staff Name">
                                    <ItemTemplate>
                                        <%# Eval("Staff_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate>
                                        <%# Eval("Rank")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reading">
                                    <ItemTemplate>
                                        <%# Eval("Reading")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tester">
                                    <ItemTemplate>
                                        <%# Eval("Tester_Staff_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Witness">
                                    <ItemTemplate>
                                        <%# Eval("Witness_Staff_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
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
                <tr>
                    <td>
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td colspan="2">
                                    * For results on alcohol test , observe below :
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Alcohol Content</b>
                                </td>
                                <td>
                                    <b>Result</b>
                                </td>
                                <td>
                                    <b>Action</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    0 - 0.10 mg/L
                                </td>
                                <td>
                                    Pass
                                </td>
                                <td>
                                    No Action
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    0.10 - 0.19 mg/L
                                </td>
                                <td>
                                    Fail
                                </td>
                                <td>
                                    Relieved Officer to inform Master / Chief Engineer and observe officer.
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    0.20 and above
                                </td>
                                <td>
                                    Alarm
                                </td>
                                <td>
                                    To be Relieved Officer shall refuse hand over and immediately inform Master / Cheef
                                    enggineer.
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
