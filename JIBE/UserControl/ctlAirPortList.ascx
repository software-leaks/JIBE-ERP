<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlAirPortList.ascx.cs"
    Inherits="UserControl_ctlAirPortList" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<style type="text/css">
    .portList-Popup
    {
        position: absolute;
        width: 260px;
        border: 1px solid #D0A9F5;
        color: Black;
        text-align: left;
        filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
        background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
        background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
        color: Black;
    }
    .portList-user-control
    {
        filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
        background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
        background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
        color: Black;
    }
    .watermarked
    {
        color: #cccccc;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("body").on("change", ".txtSearchAirPortList", function () {
            if ($.trim($(".txtSearchAirPortList").val()) == "") {
                $(".txtSearchAirPortList").val('');
                $(".btnSearchAirport").click();
                return false;
            }
            else {
                $(".btnSearchAirport").click();
            }
        });
    });
</script>
<asp:Panel ID="Panel1" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="text-align: left;">
                <asp:TextBox ID="txtSearchAirPortList" CssClass="txtSearchAirPortList" runat="server" 
                    Width="120px"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ID="ACtxtFrom1" TargetControlID="txtSearchAirPortList"
                    CompletionSetCount="10" MinimumPrefixLength="2" ServiceMethod="GetAirportList"
                    CompletionInterval="200" ServicePath="~/TravelService.asmx" runat="server" EnableCaching="true"
                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true">
                </ajaxToolkit:AutoCompleteExtender>
                <ajaxToolkit:TextBoxWatermarkExtender ID="extWaterMark" runat="server" TargetControlID="txtSearchAirPortList"
                    WatermarkText="Type to Search, IATA Code, Airport Name" WatermarkCssClass="watermarked" />
                <asp:ImageButton ID="btnSearch" CssClass="btnSearchAirport" ImageUrl="~/Images/SearchButton.png" runat="server"
                    ImageAlign="AbsMiddle" OnClick="btnSearch_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td style="text-align: right;">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdn_TargetControlID" runat="server" />
    <asp:HiddenField ID="hdn_SelectedValue" runat="server" />
    <asp:HiddenField ID="hdn_SelectedText" runat="server" />
    <asp:HiddenField ID="HiddenField3" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlAirportList" runat="server" Visible="false">
        
    <div id="divAirportList" class="draggable" style="background-color: #afbfcf; border: 1px solid gray;
        height: 400px; position: absolute; color: black">
        <table>
            <tr>
                <td colspan="4" style="font-size: 12px; text-align: center; border-style: solid;
                    border-color: gray; background-color: gray;">
                    Airport Selection
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:TextBox ID="txtContinent" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="txtContinent_TextChanged"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCountry" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtMunicipality" runat="server" Width="150px" AutoPostBack="true"
                        OnTextChanged="txtMunicipality_TextChanged"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAirportName" runat="server" Width="250px" AutoPostBack="true"
                        OnTextChanged="txtAirportName_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="font-size: 14px; height: 22px;">
                    Continent:
                </td>
                <td align="left" style="font-size: 14px; height: 22px;">
                    Country:
                </td>
                <td align="left" style="font-size: 14px; height: 22px;">
                    Municipality:
                </td>
                <td align="left" style="font-size: 14px; height: 22px;">
                    Airport:
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:ListBox ID="lstContinent" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                        OnSelectedIndexChanged="lstContinent_SelectedIndexChanged"></asp:ListBox>
                </td>
                <td align="left">
                    <asp:ListBox ID="lstCountry" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                        OnSelectedIndexChanged="lstCountry_SelectedIndexChanged"></asp:ListBox>
                </td>
                <td align="left">
                    <asp:ListBox ID="lstMunicipality" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                        OnSelectedIndexChanged="lstMunicipality_SelectedIndexChanged"></asp:ListBox>
                </td>
                <td align="left" style="vertical-align: top">
                    Matching IATA Code:<br />
                    <asp:ListBox ID="lstAirport_IataCode" runat="server" Width="260px" Height="30px"
                        AutoPostBack="true" Visible="true"></asp:ListBox>
                    <br />
                    Other Airports:<br />
                    <asp:ListBox ID="lstAirport" runat="server" Width="260px" Height="215px" AutoPostBack="true">
                    </asp:ListBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="btnSelectAirport" Text="Select And Close" runat="server" OnClick="btnSelectAirport_Click" />
                    <asp:Button ID="btnCloseAirport" Text="Close" runat="server" OnClick="btnCloseAirport_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
