<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCustomDateFilter.ascx.cs"
    Inherits="UserControl_ucCustomDateFilter" %>
<div id='<%= this.ID %>' style='<%=Style%>' class='<%=CssClass%>'>
    <asp:Panel ID="pnlsearchSection" runat="server" Width="200px">
        <table class="tbl-pnlsearchSection-ucListBox">
            <tr>
                <td colspan="2" style="text-align: left; border: 1px solid #FF8C00; background-color: #FFE0CC;
                    padding: 3px;">
                    <asp:RadioButton ID="rbtnDateFilterTypeSingle" Font-Size="11px" Checked="true" runat="server"
                        GroupName="DateFilterType" Text="Single" />
                    <asp:RadioButton ID="rbtnDateFilterTypeDouble" Font-Size="11px" runat="server" GroupName="DateFilterType"
                        Text="Between" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; border: 1px solid #FF8C00; background-color: #FFE0CC;
                    padding: 3px; width: 90%">
                    <asp:TextBox ID="txtFilterFrom" Height="18px" runat="server" Width="97%" BorderColor="#A3C2CC"
                        Font-Size="12px" Font-Names="tahoma" BorderWidth="1px" BorderStyle="Solid" AutoPostBack="false"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtendertxtFilterFrom" TargetControlID="txtFilterFrom"
                        Format="dd/MM/yyyy" runat="server">
                    </cc1:CalendarExtender>
                    <br />
                    <asp:TextBox ID="txtFilterTo" Height="18px" runat="server" Width="97%" BorderColor="#A3C2CC"
                        Style="display: none" Font-Size="12px" Font-Names="tahoma" BorderWidth="1px"
                        BorderStyle="Solid" AutoPostBack="false"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtendertxtFilterTo" TargetControlID="txtFilterTo"
                        Format="dd/MM/yyyy" runat="server">
                    </cc1:CalendarExtender>
                </td>
                <td style="text-align: left; border: 1px solid #FF8C00; background-color: #FFE0CC;
                    padding: 3px; width: 10%">
                    <asp:Image ID="imgCollapseExpandDDL" ImageAlign="Bottom" runat="server" />
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderStringFilter" TargetControlID="pnlListSection"
                        CollapsedSize="0" Collapsed="false" AutoCollapse="False" AutoExpand="False" CollapseControlID="imgCollapseExpandDDL"
                        ExpandedImage="~/Images/collapse_blue.jpg" CollapsedImage="~/Images/exclamation.png"
                        ImageControlID="imgCollapseExpandDDL" ExpandControlID="imgCollapseExpandDDL"
                        ExpandDirection="Vertical" runat="server">
                    </cc1:CollapsiblePanelExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlListSection" runat="server" CssClass="pnlListSection-list-ucListBox"
        Width="200px">
        <table class="tbl-pnlListSection-ucListBox" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    <asp:Panel ID="plnStringFilter" runat="server" ScrollBars="Auto">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:ListBox ID="ListBoxItems" Width="100%" CssClass="lstboxItems-ucDateFilter-css"
                                        AutoPostBack="true" runat="server" OnSelectedIndexChanged="ListBoxItems_SelectedIndexChanged">
                                        <asp:ListItem Text="NoFilter" Value="nofilter" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Between" Value="Between"></asp:ListItem>
                                        <asp:ListItem Text="EqualTo" Value="EqualTo"></asp:ListItem>
                                        <asp:ListItem Text="NotEqualTo" Value="NotEqualTo"></asp:ListItem>
                                        <asp:ListItem Text="GreaterThan" Value="GreaterThan"></asp:ListItem>
                                        <asp:ListItem Text="LessThan" Value="LessThan"></asp:ListItem>
                                        <asp:ListItem Text="GreaterThanOrEqualTo" Value="GreaterThanOrEqualTo"></asp:ListItem>
                                        <asp:ListItem Text="LessThanOrEqualTo" Value="LessThanOrEqualTo"></asp:ListItem>
                                    </asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lstboxSecindlist" Width="100%" Font-Names="tahoma" Font-Size="12px"
                                        Height="58px" Style="padding: 3px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ListBoxItems_SelectedIndexChanged">
                                        <asp:ListItem Text="EqualToToday" Value="EqualToToday"></asp:ListItem>
                                        <asp:ListItem Text="EqualToTomorrow" Value="EqualToTomorrow"></asp:ListItem>
                                        <asp:ListItem Text="EqualToOrLessThanToday" Value="EqualToOrLessThanToday"></asp:ListItem>
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hdf_Style_Style" Value="display: none; position: absolute;cursor: pointer"
        runat="server" />
    <asp:HiddenField ID="hdf_CssClass" Value="css-dvfilterlist-jqureyhideshow" runat="server" />
</div>
