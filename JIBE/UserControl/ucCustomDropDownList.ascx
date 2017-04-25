<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCustomDropDownList.ascx.cs"
    Inherits="UserControl_ucCustomDropDownList" %>
<div id='<%= this.ID %>' style='<%=Style%>' class='<%=CssClass%>'>
    <asp:Panel ID="pnlsearchSection" runat="server" DefaultButton="btnsearchList">
        <table class='<%=Css_pnlSearchSection%>'>
            <tr>
                <td class='<%=Css_TextBoxSearch_td%>'>
                    <asp:UpdatePanel ID="updDropDownListCustopm" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtSearchItems" runat="server" OnTextChanged="txtSearchItems_TextChanged" placeholder="Type to search"></asp:TextBox>
                            <asp:Button ID="btnsearchList" runat="server" Style="display: none" />
                           <%-- <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertxtSearchItems" TargetControlID="txtSearchItems"
                                WatermarkText="Type to search" runat="server">
                            </cc1:TextBoxWatermarkExtender>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class='<%=Css_CollapseExpand_td%>'>
                    <asp:Image ID="imgCollapseExpandDDL" ImageAlign="Bottom" ImageUrl="~/Images/expand_blue.png"
                        runat="server" CssClass="imgCollapseExpandDDL-css-hide-show" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlListSection" runat="server" CssClass="pnlListSection-list-ucListBox hide-CustomDropDown"
        ScrollBars="None">
        <table class='<%=Css_pnlListSection%>'>
            <tr>
                <td align="left"> 
                    <asp:Panel ID="plnDropDownList" runat="server" BackColor="White" ScrollBars="Vertical">
                        <asp:UpdatePanel ID="UpdatePanelheckBoxListItems" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="CheckBoxListItems" Font-Size="11px" Font-Names="tahoma" runat="server" >
                                </asp:CheckBoxList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class='<%=Css_Footer_td%>'>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left">
                                <asp:UpdatePanel ID="UpdatePanelchkSelectAll" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="All" Font-Size="14px" AutoPostBack="false"
                                            OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 22px; text-align: center">
                                <asp:UpdatePanel ID="UpdatePanelimgAsc" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:ImageButton ID="imgAsc" AlternateText="Asc" runat="server" ImageAlign="Bottom"
                                            ImageUrl="~/Images/Actions-go-up-icon.png" OnClick="imgAsc_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 22px; text-align: center">
                                <asp:UpdatePanel ID="UpdatePanelimgDesc" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:ImageButton ID="imgDesc" AlternateText="Desc" runat="server" ImageAlign="Bottom"
                                            ImageUrl="~/Images/Actions-go-down-icon.png" OnClick="imgDesc_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 50px; text-align: center">
                                <asp:Button ID="btnApplyFilter" runat="server" ForeColor="Black" Text="Ok" Height="20px"
                                    BorderStyle="Solid" BorderColor="Black" Width="40px" OnClick="btnApplyFilter_Click"
                                    BackColor="#ADC7DD" BorderWidth="1px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div style="display: none">
        <asp:HiddenField ID="hdf_Style_Style" Value="display: none; position: absolute;cursor: pointer"
            runat="server" />
        <asp:HiddenField ID="hdf_CssClass" Value="css-dvfilterlist-jqureyhideshow" runat="server" />
        <asp:HiddenField ID="hdf_UseSession" Value="true" runat="server" />
        <asp:HiddenField ID="hdf_Collapsed" Value="false" runat="server" />
        <asp:HiddenField ID="hdf_Focus" runat="server" Value="true" />
        <asp:HiddenField ID="hdf_HideOnApplyFilter" Value="true" runat="server" />
        <asp:HiddenField ID="hdf_UseInHeader" runat="server" Value="true" />
        <asp:HiddenField ID="hdf_txtSearchItems_td_css" Value="ddl-txtSearchItems-td-css"
            runat="server" />
        <asp:HiddenField ID="hdf_txtSearchItems_css" Value="ddl-txtSearchItems-css" runat="server" />
        <asp:HiddenField ID="hdf_imgCollapseExpandDDL_td_css" Value="ddl-imgCollapseExpandDDL-td-css"
            runat="server" />
        <asp:HiddenField ID="hdf_footer_td_css" Value="ddl-footer-td-css" runat="server" />
        <asp:HiddenField ID="hdf_WatermarkCssClass" Value="watermarked" runat="server" />
        <asp:HiddenField ID="hdf_tbl_pnlsearchSection" Value="tbl-pnlsearchSection-ucListBox"
            runat="server" />
        <asp:HiddenField ID="hdf_tbl_pnlListSection" Value="tbl-pnlListSection-ucListBox"
            runat="server" />
        <asp:HiddenField ID="hdf_Width" Value="0" runat="server" />
    </div>
</div>
