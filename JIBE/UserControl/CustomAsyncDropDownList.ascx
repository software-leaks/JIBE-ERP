<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomAsyncDropDownList.ascx.cs" 
    Inherits="UserControl_ucAsyncDropDownList" %>
    
<div id='<%= this.ID %>' style='<%=Style%>' class='<%=CssClass%>'>
    <asp:Panel ID="pnlsearchSection" runat="server">
        <table class='<%=Css_pnlSearchSection%>'>
            <tr>
                <td class='<%=Css_TextBoxSearch_td%>'>
                    <asp:TextBox ID="txtSelectedPortName" ReadOnly="true" Text="-- SELECT --" CssClass="ddl-AsynctxtDisplayPortName-css-white" 
                        runat="server"></asp:TextBox>
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
                    <asp:Panel ID="pnltextbox_search" runat="server" ScrollBars="None" DefaultButton="btnsearchList">
                        <asp:TextBox ID="txtSearchItems" runat="server" BackColor="LightYellow" CssClass="ddl-AsynctxtSearchItems-css-white"></asp:TextBox>
                        <tlk4:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertxtSearchItems" TargetControlID="txtSearchItems"
                            WatermarkText="Search" runat="server">
                        </tlk4:TextBoxWatermarkExtender>
                         <asp:Button ID="btnsearchList" runat="server" Style="display: none" OnClientClick="javascript:return false;" />
                    </asp:Panel>
                    <asp:Panel ID="plnDropDownList" runat="server" BackColor="White" ScrollBars="None">
                        <asp:ListBox ID="CheckBoxListItems" Font-Size="11px" Font-Names="tahoma" AutoPostBack="false"
                            CssClass="AsyncListBoxListItemPortlist"
                            runat="server"></asp:ListBox>
                    </asp:Panel>
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
        <asp:HiddenField ID="hdf_selected_value" Value="0" runat="server" />
        <asp:HiddenField ID="hdf_selected_text" Value="-- SELECT --" runat="server" />
        <asp:HiddenField ID="hdf_extra_search" Value="" runat="server" />

        <asp:HiddenField ID="hdf_webMethod_name" runat="server" />
       


    </div>
</div>
