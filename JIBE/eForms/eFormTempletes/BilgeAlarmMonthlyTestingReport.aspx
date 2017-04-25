<%@ Page Title="eForms: Bilge Alarm Monthly Testing Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="BilgeAlarmMonthlyTestingReport.aspx.cs" Inherits="eForms_eFormTempletes_BilgeAlarmMonthlyTestingReport" %>

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
                    <asp:Label ID="lblPageTitle" runat="server" Text="Bilge Alarm Monthly Testing Report"></asp:Label>
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
                    <td style="width:200px">
                        Vessel Name:
                    </td>
                    <td style="width:200px"  class="eform-field-data">
                        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                    </td>
                    <td style="width:200px">
                        Report Date:
                    </td>
                    <td  class="eform-field-data">
                        <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:100%">
                <tr>
                    <td>
                        ALARMS
                    </td>
                    <td rowspan="2">
                        <asp:GridView ID="GridView_CargoHoldBilgeAlarm" DataKeyNames="ID" runat="server"
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" CssClass="GridView-css"
                            CellPadding="7" AllowPaging="false" Width="100%" ShowFooter="false" EmptyDataText="No Record Found"
                            CaptionAlign="Bottom" GridLines="Both">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <%# Eval("SR_No")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ALARM SENSOR LOCATION">
                                    <ItemTemplate>
                                        <%# Eval("Hold_Tank_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATE TESTED">
                                    <ItemTemplate>
                                        <%# Eval("Date_of_testing","{0:dd/MM/yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TEST RESULT">
                                    <ItemTemplate>
                                        <%# Eval("Testing_Result")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REMARKS">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS")%>
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
                        <span class="eform-vertical-text">CARGO HOLD BILGE ALARMS</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="eform-vertical-text">E/R BILGE ALARMS</span>
                    </td>
                    <td>
                        <asp:GridView ID="GridView_ERBilgeAlarm" DataKeyNames="ID" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="7" AllowPaging="false"
                            Width="100%" ShowFooter="false" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                            GridLines="Both">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <%# Eval("SR_No")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ALARM SENSOR LOCATION">
                                    <ItemTemplate>
                                        <%# Eval("Hold_Tank_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATE TESTED">
                                    <ItemTemplate>
                                        <%# Eval("Date_of_testing","{0:dd/MM/yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TEST RESULT">
                                    <ItemTemplate>
                                        <%# Eval("Testing_Result")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REMARKS">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS")%>
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
                        <span class="eform-vertical-text">OTHER BILGE ALARMS</span>
                    </td>
                    <td>
                        <asp:GridView ID="GridView_OtherBilgeAlarm" DataKeyNames="ID" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="7" AllowPaging="false"
                            Width="100%" ShowFooter="false" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                            GridLines="Both">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <%# Eval("SR_No")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ALARM SENSOR LOCATION">
                                    <ItemTemplate>
                                        <%# Eval("Hold_Tank_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATE TESTED">
                                    <ItemTemplate>
                                        <%# Eval("Date_of_testing","{0:dd/MM/yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TEST RESULT">
                                    <ItemTemplate>
                                        <%# Eval("Testing_Result")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REMARKS">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS")%>
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
                        <span class="eform-vertical-text">SPARE SENSOR ROB</span>
                    </td>
                    <td style="width:300px">
                        <table  style="width:100%">
                            <tr>
                                <td>Cargo Hold Bildge Alarm Sensors</td>
                                <td><asp:Label ID="txtCargoHoldBildgeAlarmSensors" runat="server"></asp:Label> </td>
                            </tr>
                            <tr>
                                <td>Engine Room Bildge Alarm Sensors</td>
                                <td><asp:Label ID="txtEngineRoomBildgeAlarmSensors" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Other Bildge Alarm Sensors</td>
                                <td><asp:Label ID="txtOtherBildgeAlarmSensors" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        Notes:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="60" Width="300"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
