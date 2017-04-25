<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCustomStringFilter.ascx.cs"
    Inherits="ucCustomStringFilter" %>
<div id='<%= this.ID %>' style='<%=Style%>' class='<%=CssClass%>'>
    <asp:Panel ID="pnlsearchSection" runat="server"  Width="200px">
        <table class="tbl-pnlsearchSection-ucListBox">
            <tr>
                <td style="text-align: center; border: 1px solid #FF8C00; background-color: #FFE0CC;
                    padding: 3px; width: 90%">
                    <asp:TextBox ID="txtSearchItems" Height="18px" runat="server" Width="97%" BorderColor="#A3C2CC"
                        Font-Size="12px" Font-Names="tahoma" BorderWidth="1px" BorderStyle="Solid" AutoPostBack="false"></asp:TextBox>
                    <asp:Button ID="btnsearchList" runat="server" Style="display: none" />
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertxtSearchItems" TargetControlID="txtSearchItems"
                        WatermarkText="Type here" runat="server" WatermarkCssClass="watermarked">
                    </cc1:TextBoxWatermarkExtender>
                </td>
                <td style="text-align: left; border: 1px solid #FF8C00; background-color: #FFE0CC;
                    padding: 3px; width: 10%">
                    <asp:Image ID="imgCollapseExpandDDL" ImageAlign="Bottom" runat="server" />
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderStringFilter" TargetControlID="pnlListSection"
                        CollapsedSize="0" Collapsed="false" AutoCollapse="False" AutoExpand="False" CollapseControlID="imgCollapseExpandDDL"
                        ExpandedImage="~/Images/collapse_blue.jpg" CollapsedImage="~/Images/expand_blue.jpg"
                        ImageControlID="imgCollapseExpandDDL" ExpandControlID="imgCollapseExpandDDL"
                        ExpandDirection="Vertical" runat="server">
                    </cc1:CollapsiblePanelExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlListSection" runat="server" CssClass="pnlListSection-list-ucListBox"
        Width="200px">
        <table class="tbl-pnlListSection-ucListBox">
            <tr>
                <td align="left">
                    <asp:Panel ID="plnStringFilter" runat="server" ScrollBars="Auto">
                        <asp:ListBox ID="ListBoxItems" Width="100%"  CssClass="lstboxItems-ucStringFilter-css"
                            AutoPostBack="true"  runat="server" OnSelectedIndexChanged="ListBoxItems_SelectedIndexChanged">
                            <asp:ListItem Text="NoFilter" Value="nofilter" Selected="True" ></asp:ListItem>
                            <asp:ListItem Text="Contains" Value="contains"></asp:ListItem>
                            <asp:ListItem Text="DoesNotContains" Value="notcontains"></asp:ListItem>
                            <asp:ListItem Text="StartWith" Value="start"></asp:ListItem>
                            <asp:ListItem Text="EndWith" Value="end"></asp:ListItem>
                            <asp:ListItem Text="EqualTo" Value="equal"></asp:ListItem>
                            <asp:ListItem Text="NotEqualTo" Value="notequal"></asp:ListItem>
                        </asp:ListBox>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hdf_Style_Style" Value="display: none; position: absolute;cursor: pointer"
        runat="server" />
    <asp:HiddenField ID="hdf_CssClass" Value="css-dvfilterlist-jqureyhideshow" runat="server" />
</div>
