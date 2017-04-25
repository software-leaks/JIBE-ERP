<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTP_Quotation_Evaluation.aspx.cs"
    Inherits="Purchase_CTP_Quotation_Evaluation" MasterPageFile="~/Site.master" Title="Compare Contracts " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="ucpager" %>
<asp:Content ID="header" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/boxover.js"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/CTP_Quotation_Evaluation.js?v=7" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <style type="text/css">
        .target
        {
            width: 80px;
            text-align: center;
            border: 2px solid #666666;
            padding: 5px;
            background-color: #00FFFF;
            height: 45px;
            display: block;
            float: left;
        }
        
        .amount-css
        {
            text-align: right;
            padding-right: 5px;
        }
        
        .text-css
        {
            text-align: left;
        }
        
        
        div#divInfoQTN
        {
            max-height: 400px;
            width: 1210px;
            overflow: scroll;
            position: relative;
        }
        
        div#divInfoQTN th
        {
            top: 0px;
            position: relative;
        }
        .gtdth
        {
            z-index: 0;
            background-color: #F2F2F2;
            position: relative;
            cursor: default;
            left: 0px;
            border-collapse: collapse;
            padding-left: 3px;
            white-space: nowrap;
            color: Black;
            font-size: 11px;
            border: 0px solid white;
            padding-right: 3px;
        }
        .hd
        {
            background-image: url(../images/suppliergridbk.png); /*background-color: #00868B;*/
            position: relative;
            cursor: default;
            z-index: 200;
            left: 0px;
            color: Black;
            font-size: 11px;
            border-collapse: collapse;
            border: 0px solid white;
            padding-left: 3px;
            white-space: nowrap;
        }
        .NewItem
        {
            background-color: Yellow;
        }
        
        .tdTooltip
        {
            border-bottom: 1px solid gray;
            width: 250px;
        }
        .tdHtip
        {
            font-weight: bold;
            text-align: right;
            font-size: 11px;
            font-family: Verdana;
            vertical-align: top;
        }
        .tdDtip
        {
            text-align: left;
            padding-left: 3px;
            font-size: 11px;
            font-family: Verdana;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server" DisplayAfter="500">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updmain" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <center>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <b style="font-size: small">Contract Comparision - All figure in USD</b>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" align="center" style="width: 100%;">
                    <tr align="left">
                        <td style="color: #FFFFFF; font-size: small; width: 100%">
                            <table cellpadding="0" cellspacing="0" align="left" style="width: 100%; background-color: Gray;">
                                <tr>
                                    <td style="background: #80E6E8; padding: 2px 0px 2px 0px; border-bottom-style: outset;
                                        border-bottom-width: 1px; border-top: outset; border-top-width: 1px">
                                        <asp:Label ID="lblexpand" Style="font-weight: bold; text-decoration: underline; color: Black"
                                            runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td align="left">
                                        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSupplier" TargetControlID="PanelSupplier"
                                            CollapsedSize="0" Collapsed="false" AutoCollapse="False" AutoExpand="False" CollapseControlID="lblexpand"
                                            ExpandControlID="lblexpand" CollapsedText="Show Supplier Details..." ExpandedText="Hide Supplier Details"
                                            TextLabelID="lblexpand" ExpandDirection="Vertical" runat="server">
                                        </cc1:CollapsiblePanelExtender>
                                        <asp:Panel ID="PanelSupplier" runat="server">
                                            <asp:GridView ID="rgdSupplierInfo" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                                Font-Size="10px" CellPadding="2" CellSpacing="1" OnRowDataBound="rgdSupplierInfo_ItemDataBound"
                                                BackColor="#D8D8D8" DataKeyNames="Quotation_ID">
                                                <Columns>
                                                    <asp:BoundField DataField="QTN_STS" ItemStyle-ForeColor="DarkMagenta" ItemStyle-Font-Bold="true"
                                                        HeaderText="Status" />
                                                    <asp:BoundField DataField="Supplier_Ref_Number" HeaderText="Qtn.Ref." />
                                                    <asp:BoundField DataField="QTN_Contract_Code" HeaderText="Contract Code" />
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplierName" Text='<%# Eval("Full_NAME")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Port Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPortName" Text='<%# Eval("PORT_NAME")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtn.Rcvd.">
                                                        <ItemStyle Width="80px" />
                                                        <HeaderTemplate>
                                                            <table cellpadding="1">
                                                                <tr>
                                                                    <td style="width: 50px">
                                                                        Total Items.
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                        Quoted Items
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                        Selected Items
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table cellpadding="1">
                                                                <tr>
                                                                    <td style="width: 50px">
                                                                        <%# Eval("TOTAL_COUNT")%>
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                        <%# Eval("QUOTED_COUNT")%>
                                                                    </td>
                                                                    <td style="width: 50px">
                                                                        <asp:Label ID="lblSelected_Count" runat="server" Text='<%#"&nbsp;&nbsp;" + Eval("SELECTED_COUNT")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Avg price(selected items)" HeaderStyle-Wrap="true" HeaderStyle-Width="90px"
                                                        DataField="AvgPriceSelected" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:TemplateField HeaderText="Avg price(quoted items)" ItemStyle-HorizontalAlign="Right"
                                                        HeaderStyle-Width="90px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContractPrice" Text='<%#Eval("AvgPrice") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Only quoted" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkshowOnlyQuoted" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlApproval" runat="server" Font-Size="11px">
                                                                <asp:ListItem Value="" Text="All"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Appd"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="UnAppd"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Freight_Charge" HeaderText="Freight" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="Truck_Charge" HeaderText="Truck" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="Pkg_Hld_Charge" HeaderText="PkgHld" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="Other_Charge" HeaderText="Other" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="Barge_Charge" HeaderText="Barge" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="Vat" HeaderText="Vat" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:TemplateField HeaderText="Final Amount" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtGrandTotal" Text='<%#Eval("FinalPrice") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hdfGrandTotal" runat="server" Value='<%# Eval("FinalPrice_decimal") %>' />
                                                            <asp:HiddenField ID="hdfQuotation_Status" runat="server" Value='<%# Eval("Quotation_Status")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" ForeColor="BlueViolet" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" BackColor="WhiteSmoke" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="suppgridHeaderStyle-css" Font-Size="11px" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="right" style="background-color: #80E6E8; font-size: small; width: 100%">
                            <asp:Button ID="btnCompareContact" runat="server" Style="font-size: 11px; height: 25px;
                                font-weight: bold" Text="Compare Contracts" OnClientClick="return UpdateEvalution();"
                                OnClick="btnSearchItems_Click" />
                            <asp:Button ID="btnSaveEvaln" runat="server" Style="font-size: 11px; height: 25px;
                                font-weight: bold" Text="Save Contract Evaluation" OnClientClick="return UpdateEvalution();"
                                OnClick="btnSaveEvaln_Click" />
                            &nbsp; &nbsp; &nbsp;
                            <input id="btnFinalizeEval" type="button" runat="server" onclick="ShowModalFinalizeItems()"
                                style="font-size: 11px; height: 25px" value="Approve selected items" />
                            <br />
                            <asp:Label ID="lblActionmsg" ForeColor="Red" Font-Italic="true" Font-Names="verdana"
                                Font-Size="11px" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table cellspacing="0" style="width: 100%; border: 1px solid gray">
                                <tr align="left">
                                    <td style="text-align: right; padding-right: 2px; font-weight: bold; font-size: 11px;
                                        color: Black; width: 100px; padding-top: 3px; padding-bottom: 3px;">
                                        Sub Catalogue :
                                    </td>
                                    <td style="text-align: left; padding-left: 2px; font-size: 11px; color: Black; width: 100px;
                                        padding-top: 3px; padding-bottom: 3px">
                                        <asp:DropDownList ID="ddlSubCatalogue" Width="200px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; padding-right: 2px; font-weight: bold; font-size: 11px;
                                        color: Black; width: 100px; padding-top: 3px; padding-bottom: 3px">
                                        Item :
                                    </td>
                                    <td style="text-align: left; padding-left: 2px; font-size: 11px; color: Black; padding-top: 3px;
                                        padding-bottom: 3px; width: 150px">
                                        <asp:TextBox ID="txtitemsearch" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="padding-top: 3px; padding-bottom: 3px; text-align: left; padding-left: 10px">
                                        <asp:Button ID="btnSearchItems" runat="server" Text="Search" OnClick="btnSearchItems_Click"
                                            OnClientClick="return UpdateEvalution();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <div id="divInfoQTN" onscroll="checkAvailableWidth()">
                                            <asp:GridView ID="rgdQuatationInfo" runat="server" OnRowDataBound="rgdQuatationInfo_ItemDataBound"
                                                EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" BackColor="#D8D8D8"
                                                AutoGenerateColumns="true" GridLines="None" OnRowCreated="rgdQuatationInfo_ItemCreated"
                                                CellSpacing="1">
                                            </asp:GridView>
                                        </div>
                                        <asp:Label ID="lblpager" runat="server" onclick="UpdateEvalution()">
                                            <ucpager:ucCustomPager ID="ucCustomPageritems" OnBindDataItem="BindItems" runat="server" />
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table align="center" cellpadding="0" cellspacing="0" width=" 100%">
                    <tr style="background-color: #CCCCCC; font-size: small; color: #FFFFFF;">
                        <td>
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenQuery" runat="server" />
                <asp:HiddenField ID="hdfUserIDSaveEval" runat="server" />
                <asp:HiddenField ID="hdfquotation_codes_compare" runat="server" />
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvApprovalPopUp" style="display: none; height: auto; width: 450px;" title='Contract Approval'>
        <table>
            <tr>
                <td style="width: 125px; text-align: right; padding: 2px 3px 2px 0px; font-size: 11px;
                    color: Black">
                    Effective Date :
                </td>
                <td style="width: 100px; text-align: left; padding: 2px 2px 3px 0px; font-size: 11px;
                    color: Black">
                    <asp:TextBox ID="txtEffectiveDate" Width="100px" runat="server" ValidationGroup="apprv"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtendertxtEffectiveDate" TargetControlID="txtEffectiveDate"
                        Format="dd/MM/yyyy" runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEffectiveDate" runat="server"
                        ControlToValidate="txtEffectiveDate" Display="Dynamic" ErrorMessage="Please enter effective date ! "
                        ValidationGroup="apprv"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 125px; text-align: right; padding: 2px 3px 2px 0px; font-size: 11px;
                    color: Black">
                    Expiry Date :
                </td>
                <td style="width: 100px; text-align: left; padding: 2px 2px 3px 0px; font-size: 11px;
                    color: Black">
                    <asp:TextBox ID="txtExpiryDate" Width="100px" runat="server" ValidationGroup="apprv"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtExpiryDate" Format="dd/MM/yyyy"
                        runat="server">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtExpiryDate" runat="server"
                        Display="Dynamic" ControlToValidate="txtExpiryDate" ValidationGroup="apprv" ErrorMessage="Please enter expiry date !"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorDates" runat="server" ControlToCompare="txtEffectiveDate"
                        ControlToValidate="txtExpiryDate" Type="Date" Display="Dynamic" Operator="GreaterThan"
                        ErrorMessage="Expiry date should be greater than effective date."></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 125px; text-align: right; padding: 2px 3px 2px 0px; font-size: 11px;
                    color: Black">
                    Remark :
                </td>
                <td colspan="3" style="width: 330px; text-align: left; padding: 2px 2px 3px 0px;
                    font-size: 11px; color: Black">
                    <asp:TextBox ID="txtRemark" runat="server" Height="70px" TextMode="MultiLine" Width="330"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right; padding: 5px 3px 5px 0px">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"
                        ValidationGroup="apprv" />
                    <input type="button" value="Cancel" onclick="hideModal('dvApprovalPopUp');" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
