<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UnionAndBranch.aspx.cs" Inherits="Crew_UnionAndBranch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <style type="text/css">
        .ajax__tab_tab
        {
            min-width: 110px;
        }
        .ajax__tab_body
        {
            border: 0 !important;
            min-height: 550px;
        }
        
        .gridmain-css tr
        {
            height: 28px;
        }
        .gridmain-css tr:hover
        {
            background-color: #FEECEC;
        }
        .page-title
        {
            line-height: 18px !important;
        }
        .tblAddress tr
        {
            height: 0;
        }
        .tblAddress td
        {
            border-bottom: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title" style="margin-bottom: 15px;">
        Union
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <ajaxToolkit:TabContainer ID="TabPanel" ActiveTabIndex="0" runat="server" AutoPostBack="true"
                OnActiveTabChanged="TabPanel_OnActiveTabChanged">
                <ajaxToolkit:TabPanel TabIndex="0" ID="tabUnionBranch" runat="server">
                    <HeaderTemplate>
                        Union & Union Branch
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnUSCountryID" runat="server" Value="" />
                        <asp:UpdatePanel ID="UpdUnion" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="width: 450px; float: left; text-align: center;">
                                    <div style="padding-top: 5px; padding-bottom: 5px;">
                                        <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                                            <table width="100%" cellpadding="4" cellspacing="4">
                                                <tr>
                                                    <td align="right" style="width: 10%">
                                                        Union :&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 30%">
                                                        <asp:TextBox ID="txtfilter" ClientIDMode="Static" runat="server" Width="100%"></asp:TextBox>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                                    </td>
                                                    <td align="center" style="width: 1%">
                                                        <asp:ImageButton ID="btnFilter" runat="server" ClientIDMode="Static" TabIndex="0"
                                                            OnClick="btnFilter_Click" ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                                    </td>
                                                    <td align="center" style="width: 1%">
                                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                            ImageUrl="~/Images/Refresh-icon.png" />
                                                    </td>
                                                    <td align="center" style="width: 1%">
                                                        <asp:Image ImageUrl="~/Images/Add-icon.png" ID="ImgAdd" runat="server" Style="cursor: pointer;"
                                                            ToolTip="Add New Union" ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hdnAddressType" runat="server" ClientIDMode="Static" Value="1" />
                                                    </td>
                                                    <td style="width: 3%">
                                                        <asp:ImageButton ID="ImgBtnExportUnion" runat="server" ToolTip="Export to excel"
                                                            ImageUrl="~/Images/Exptoexcel.png" OnClick="ImgBtnExportUnion_OnClick" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                    <div>
                                        <asp:GridView ID="gvUnion" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                            OnRowDataBound="gvUnion_RowDataBound" DataKeyNames="ID" CellPadding="0" CellSpacing="2"
                                            Width="100%" GridLines="both" OnSorting="gvUnion_Sorting" AllowSorting="true"
                                            CssClass="gridmain-css" PageSize="20">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Union">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px;" runat="server" CommandName="Sort"
                                                            CommandArgument="UnionName" ForeColor="Black">Union</asp:LinkButton>
                                                        <img id="UnionName" runat="server" visible="false" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" Text='<%#Eval("UnionName")%>' rel='<%#Eval("ID").ToString() %>'
                                                            runat="server" Style="margin: 5px; color: Black;" CommandArgument='<%#Eval("ID").ToString() %>'
                                                            OnClick="BindUnionBranchsOnClick" CssClass="lnkbtnUnion" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                            ToolTip="Edit" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                            Width="16px" Visible='<%# uaEditFlag %>' Style="cursor: pointer;" />
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" Visible='<%# uaDeleteFlage %>'
                                                            OnCommand="onDelete" OnClientClick="return confirm('Are you sure, you want to delete union and union branch?')"
                                                            CommandArgument='<%#Eval("[ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                            Height="16px" Width="16px"></asp:ImageButton>
                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                            Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                            onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CD_Union&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindUnion" />
                                    </div>
                                    <div id="divadd" title='Add/Edit Union' style="display: none; font-family: Tahoma;
                                        text-align: left; font-size: 12px; color: Black; width: 30%">
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td align="right" style="width: 20%">
                                                    Union&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtUnion" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                                        Width="250px" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ClientIDMode="Static" ID="hdnUnionID" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                                    <asp:Button ID="btnsave" Width="75px" runat="server" Text="Save" ClientIDMode="Static"
                                                        OnClick="btnsave_OnClick" />
                                                    <input type="button" name="Cancel" value="Cancel" id="btnCancel" style="width: 75px;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div style="width: 67%; float: right; margin-bottom: 15px;" id="divBranch" runat="server"
                                    visible="false">
                                    <div style="padding-top: 5px; padding-bottom: 5px; width: 100%">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="ImgbtnFilterBranch">
                                            <table width="100%" cellpadding="4" cellspacing="4">
                                                <tr>
                                                    <td style="width: 35%; color: Black;">
                                                        <b>Union:</b>&nbsp;<asp:Label ID="lblUnionName" runat="server" />
                                                    </td>
                                                    <td align="right" style="width: 14%">
                                                        Union Branch:&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 30%">
                                                        <asp:TextBox ID="txtUnionBranchFilter" ClientIDMode="Static" runat="server" Width="90%"></asp:TextBox>
                                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                            TargetControlID="txtUnionBranchFilter" WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                                    </td>
                                                    <td align="center" style="width: 1%">
                                                        <asp:ImageButton ID="ImgbtnFilterBranch" runat="server" TabIndex="0" OnClick="ImgbtnFilterBranch_Click"
                                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" ClientIDMode="Static" />
                                                    </td>
                                                    <td align="center" style="width: 1%">
                                                        <asp:ImageButton ID="ImgbtnRefreshBranch" runat="server" OnClick="ImgbtnRefreshBranch_Click"
                                                            ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png" />
                                                    </td>
                                                    <td align="center" style="width: 1%">
                                                        <asp:Image ImageUrl="~/Images/Add-icon.png" ID="ImgbtnAddBranch" runat="server" Style="cursor: pointer;"
                                                            ToolTip="Add New Union Branch" ClientIDMode="Static" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                    <div>
                                        <asp:GridView ID="gvUnionBranch" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" OnRowDataBound="gvUnionBranch_RowDataBound" DataKeyNames="ID"
                                            CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" OnSorting="gvUnionBranch_Sorting"
                                            AllowSorting="true" CssClass="gridmain-css" PageSize="10">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px"
                                                BackColor="#feecec" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Union">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px;" runat="server" CommandName="Sort"
                                                            CommandArgument="UnionBranch" ForeColor="Black">Union Branch</asp:LinkButton>
                                                        <img id="UnionBranch" runat="server" visible="false" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnion" Text='<%#Eval("UnionBranch")%>' rel='<%#Eval("ID").ToString() %>'
                                                            runat="server" Style="margin: 5px; color: Black;" CssClass="lblUnionBranch" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltrAddress" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblPhoneNumber" Style="margin-left: 2px;" runat="server" CommandName="Sort"
                                                            CommandArgument="PhoneNumber" ForeColor="Black">Phone Number</asp:LinkButton>
                                                        <img id="PhoneNumber" runat="server" visible="false" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPhoneNumber" Text='<%#Eval("PhoneNumber")%>' CssClass="lblPhoneNumber"
                                                            rel='<%#Eval("ID").ToString() %>' runat="server" Style="margin: 5px; color: Black;" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblEmail" Style="margin-left: 2px;" runat="server" CommandName="Sort"
                                                            CommandArgument="Email" ForeColor="Black">Email</asp:LinkButton>
                                                        <img id="Email" runat="server" visible="false" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" Text='<%#Eval("Email")%>' CssClass="lblEmail" rel='<%#Eval("ID").ToString() %>'
                                                            runat="server" Style="margin: 5px; color: Black;" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Edit" CssClass="editBranch" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                            ToolTip="Edit" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                            Width="16px" Visible='<%# uaEditFlag %>' unionid='<%#Eval("UnionID")%>' Style="cursor: pointer;" />
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" Visible='<%# uaDeleteFlage %>'
                                                            OnCommand="onDeleteBranch" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                            CommandArgument='<%#Eval("[ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                            Height="16px" Width="16px"></asp:ImageButton>
                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                            Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                            onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CD_UnionBranch&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <uc1:ucCustomPager ID="ucCustomPagerItemsBranch" runat="server" PageSize="10" OnBindDataItem="BindUnionBranch" />
                                    </div>
                                    <div id="divAddUnionBranch" title='Add/Edit Union Branch' style="display: none; font-family: Tahoma;
                                        text-align: left; font-size: 12px; color: Black; width: 30%">
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td align="right" style="width: 20%">
                                                    Union&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList runat="server" ID="drpUnion" Style="min-width: 150px; max-width: 255px;"
                                                        ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 20%">
                                                    Email&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="txtInput" MaxLength="100"
                                                        Width="250px" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 20%">
                                                    Union Branch&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtUnionBranch" ClientIDMode="Static" CssClass="txtInput" MaxLength="100"
                                                        Width="250px" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ClientIDMode="Static" ID="hdnUnionBranchID" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                            <tr id="trInternationAddress" runat="server" visible="false">
                                                <td align="right" style="width: 20%">
                                                    Address&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtAddress" ClientIDMode="Static" TextMode="MultiLine"
                                                        Columns="29" Rows="4" />
                                                </td>
                                            </tr>
                                            <tr id="trUSAddress" runat="server" visible="false">
                                                <td colspan="3">
                                                    <table width="100%" cellspacing="2" cellpadding="2">
                                                        <tr>
                                                            <td align="right" style="width: 20%">
                                                                Address Line 1&nbsp;:&nbsp;
                                                            </td>
                                                            <td style="color: #FF0000; width: 1%" align="right">
                                                                *
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAddressLine1" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                                                    Width="250px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 20%">
                                                                Address Line 2&nbsp;:&nbsp;
                                                            </td>
                                                            <td style="width: 1%" align="right">
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAddressLine2" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                                                    Width="250px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 20%">
                                                                City&nbsp;:&nbsp;
                                                            </td>
                                                            <td style="color: #FF0000; width: 1%" align="right">
                                                                *
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCity" ClientIDMode="Static" CssClass="txtInput" MaxLength="100"
                                                                    Width="250px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 20%">
                                                                State&nbsp;:&nbsp;
                                                            </td>
                                                            <td style="color: #FF0000; width: 1%" align="right">
                                                                *
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtState" ClientIDMode="Static" CssClass="txtInput" MaxLength="100"
                                                                    Width="250px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 20%">
                                                                Zip Code&nbsp;:&nbsp;
                                                            </td>
                                                            <td style="color: #FF0000; width: 1%" align="right">
                                                                *
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtZipCode" ClientIDMode="Static" CssClass="txtInput" MaxLength="20"
                                                                    Width="250px" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 20%">
                                                                Country&nbsp;:&nbsp;
                                                            </td>
                                                            <td style="color: #FF0000; width: 1%" align="right">
                                                                *
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList runat="server" ID="drpCountry" Style="max-width: 250px" ClientIDMode="Static"
                                                                    CssClass="txtInput">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 20%">
                                                    Phone Number&nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPhoneNumber" ClientIDMode="Static" CssClass="txtInput" MaxLength="50"
                                                        Width="250px" runat="server"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1"
                                                        TargetControlID="txtPhoneNumber" FilterType="Numbers,Custom" ValidChars="+,-,,">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                                    <asp:Button ID="btnSaveUnionBranch" Width="75px" runat="server" Text="Save" ClientIDMode="Static"
                                                        OnClick="btnSaveUnionBranch_OnClick" />
                                                    <input type="button" name="Cancel" value="Cancel" id="btnCancelUnionBranch" style="width: 75px;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ImgBtnExportUnion" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel TabIndex="1" ID="tabUnionBook" runat="server">
                    <HeaderTemplate>
                        Union Book
                    </HeaderTemplate>
                    <ContentTemplate>
                        <center>
                            <asp:UpdateProgress ID="bluronupdateprogress" ClientIDMode="Static" runat="server"
                                AssociatedUpdatePanelID="UpdUserType">
                                <ProgressTemplate>
                                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                                        color: black">
                                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdUserType" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="width: 800px; text-align: center;">
                                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                                            <asp:Panel ID="Panel2" runat="server" DefaultButton="ImgBtnFilterUnionBook">
                                                <table width="100%" cellpadding="4" cellspacing="4">
                                                    <tr>
                                                        <td align="right" style="width: 10%">
                                                            Union Book :&nbsp;
                                                        </td>
                                                        <td align="left" style="width: 30%">
                                                            <asp:TextBox ID="txtUnionBookFilter" runat="server" Width="100%"></asp:TextBox>
                                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                                TargetControlID="txtUnionBookFilter" WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                                        </td>
                                                        <td align="center" style="width: 1%">
                                                            <asp:ImageButton ID="ImgBtnFilterUnionBook" runat="server" TabIndex="0" OnClick="btnFilterUnionBook_Click"
                                                                ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                                        </td>
                                                        <td align="center" style="width: 1%">
                                                            <asp:ImageButton ID="ImgBtnRefreshUnionBook" runat="server" OnClick="ImgBtnRefreshUnionBook_Click"
                                                                ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png" />
                                                        </td>
                                                        <td align="center" style="width: 1%">
                                                            <asp:Image ImageUrl="~/Images/Add-icon.png" ID="ImgBtnAddNewUnionBook" runat="server"
                                                                Style="cursor: pointer;" ToolTip="Add New Union Book" ClientIDMode="Static" />
                                                        </td>
                                                        <td style="width: 3%">
                                                            <asp:ImageButton ID="ImgBtnExportUnionBook" runat="server" ToolTip="Export to excel"
                                                                OnClick="ImgBtnExportUnionBook_Click" ImageUrl="~/Images/Exptoexcel.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                            <div>
                                                <asp:GridView ID="gvUnionBook" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    OnRowDataBound="gvUnionBook_RowDataBound" DataKeyNames="ID" CellPadding="0" CellSpacing="2"
                                                    Width="100%" GridLines="both" OnSorting="gvUnionBook_Sorting" AllowSorting="true"
                                                    CssClass="gridmain-css" PageSize="20">
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="UnionBook">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton ID="lblUnionBook" Style="margin-left: 2px;" runat="server" CommandName="Sort"
                                                                    CommandArgument="UnionBook" ForeColor="Black">Union Book</asp:LinkButton>
                                                                <img id="UnionBook" runat="server" visible="false" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnionBook" CssClass="lblUnionBook" Text='<%#Eval("UnionBook")%>'
                                                                    rel='<%#Eval("ID").ToString() %>' runat="server" Style="margin: 5px; color: Black;" />
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Image ID="Edit" CssClass="editUnionBook" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                                    ToolTip="Edit" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                                    Width="16px" Visible='<%# uaEditFlag %>' Style="cursor: pointer;" />
                                                                <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" Visible='<%# uaDeleteFlage %>'
                                                                    OnCommand="onDeleteUnionBook" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                                    CommandArgument='<%#Eval("[ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                    Height="16px" Width="16px"></asp:ImageButton>
                                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                    Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                                    onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CD_UnionBook&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="2%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <uc1:ucCustomPager ID="ucCustomPager1UnionBook" runat="server" PageSize="20" OnBindDataItem="BindUnionBook" />
                                            </div>
                                            <br />
                                        </div>
                                        <div id="divAddUnionBook" title='Add/Edit Union Book' style="display: none; font-family: Tahoma;
                                            text-align: left; font-size: 12px; color: Black; width: 30%">
                                            <table width="100%" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td align="right" style="width: 20%">
                                                        Union Book&nbsp;:&nbsp;
                                                    </td>
                                                    <td style="color: #FF0000; width: 1%" align="right">
                                                        *
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtUnionBook" ClientIDMode="Static" CssClass="txtInput" MaxLength="80"
                                                            Width="250px" runat="server"></asp:TextBox>
                                                        <asp:HiddenField ClientIDMode="Static" ID="hdnUnionBookId" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                                        <asp:Button ID="btnSaveUnionBook" Width="75px" runat="server" Text="Save" ClientIDMode="Static"
                                                            OnClick="btnSaveUnionBook_OnClick" />
                                                        <input type="button" name="Cancel" value="Cancel" id="Button2" style="width: 75px;" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:PostBackTrigger ControlID="ImgExpExcel" />--%>
                                    <asp:PostBackTrigger ControlID="ImgBtnExportUnionBook" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </center>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", ".edit", function () {
                var Rel = parseInt($(this).attr("rel"));
                $("#hdnUnionID").val(parseInt($(this).attr("rel")));
                $("#txtUnion").val($(".lnkbtnUnion[rel='" + Rel + "']").attr("title"));
                showModal('divadd', false);
                $("#divadd_dvModalPopupTitle").text("Add/Edit Union");
            });

            $("body").on("click", "#ImgAdd", function () {
                if (this.id == "ImgAdd") {
                    $("#hdnUnionID").val(0);
                    $("#txtUnion").val('');
                    showModal('divadd', false);
                    $("#divadd_dvModalPopupTitle").text("Add/Edit Union");
                }
                else {
                    $("#hdnUnionID").val(parseInt($(this).attr("rel")));
                    $("#txtUnion").val($(this).text());
                    showModal('divadd', false);
                    $("#divadd_dvModalPopupTitle").text("Add/Edit Union");
                }
            });

            $("body").on("click", "#btnsave", function () {
                if ($.trim($("#txtUnion").val()) == "") {
                    $("#txtUnion").val('');
                    $("#txtUnion").focus();
                    alert("Please enter Union");
                    return false;
                }
            });

            $("body").on("click", ".editBranch", function () {
                var Rel = parseInt($(this).attr("rel"));
                var unionid = parseInt($(this).attr("unionid"));

                $("#hdnUnionBranchID").val(parseInt($(this).attr("rel")));

                ClearBranchControl();
                $("#txtUnionBranch").val($(".lblUnionBranch[rel='" + Rel + "']").attr("title"));

                if ($("#hdnAddressType").val() == "0") {//US
                    $("#txtAddressLine1").val($(".lblAddressLine1[rel='" + Rel + "']").text());
                    $("#txtAddressLine2").val($(".lblAddressLine2[rel='" + Rel + "']").text());
                    $("#txtCity").val($(".lblCity[rel='" + Rel + "']").text());
                    $("#txtState").val($(".lblState[rel='" + Rel + "']").text());
                    $("#txtZipCode").val($(".lblZipCode[rel='" + Rel + "']").text());
                }
                else { ///International
                    $("#txtAddress").val($(".lblAddress[rel='" + Rel + "']").text());
                }
                $("#txtPhoneNumber").val($(".lblPhoneNumber[rel='" + Rel + "']").text());
                $("#txtEmail").val($(".lblEmail[rel='" + Rel + "']").text());

                $("#drpCountry option").each(function () {
                    if ($(this).text() == $(".lblCountry[rel='" + Rel + "']").text()) {
                        $(this).attr('selected', 'selected');
                    }
                });

                $("#drpUnion option[value='" + unionid + "']").attr('selected', 'selected');
                showModal('divAddUnionBranch', false);
                $("#divAddUnionBranch_dvModalPopupTitle").text("Add/Edit Union Branch");
            });

            function ClearBranchControl() {
                $("#txtUnionBranch").val('');
                $("#txtAddress").val('');
                $("#txtAddressLine1").val('');
                $("#txtAddressLine2").val('');
                $("#txtCity").val('');
                $("#txtState").val('');
                $("#txtZipCode").val('');
                $("#txtPhoneNumber").val('');
                $("#txtEmail").val('');
                $("#drpCountry option[value='0']").attr('selected', 'selected');
                $("#drpUnion option[value='0']").attr('selected', 'selected');
            }

            $("body").on("click", "#ImgbtnAddBranch", function () {
                ClearBranchControl();
                if (this.id == "ImgbtnAddBranch") {
                    $("#hdnUnionBranchID").val(0);
                    $("#txtUnion").val('');
                    showModal('divAddUnionBranch', false);
                    $("#divAddUnionBranch_dvModalPopupTitle").text("Add/Edit Union Branch");
                }
                else {
                    $("#hdnUnionBranchID").val(parseInt($(this).attr("rel")));
                    $("#txtUnion").val($(this).text());
                    showModal('divAddUnionBranch', false);
                    $("#divAddUnionBranch_dvModalPopupTitle").text("Add/Edit Union Branch");
                }
            });

            $("body").on("click", "#btnCancel", function () {
                $("#closePopupbutton").click();
            });
            $("body").on("click", "#btnCancelUnionBranch", function () {
                $("#divAddUnionBranch_dvModalPopupCloseButton").click();
            });
            $("body").on("click", "#Button2", function () {
                $("#divAddUnionBook_dvModalPopupCloseButton").click();
            });


            $("body").on("click", ".lnkbtnUnion", function () {
                $(".lnkbtnUnion").css({ "background-color": "" });
            });


            ///Union Book Starts here
            $("body").on("click", ".editUnionBook", function () {
                var Rel = parseInt($(this).attr("rel"));
                $("#hdnUnionBookId").val(parseInt($(this).attr("rel")));
                $("#txtUnionBook").val($(".lblUnionBook[rel='" + Rel + "']").attr("title"));
                showModal('divAddUnionBook', false);
                $("#divAddUnionBook_dvModalPopupTitle").text("Add/Edit Union Book");
            });

            $("body").on("click", "#ImgBtnAddNewUnionBook", function () {
                if (this.id == "ImgBtnAddNewUnionBook") {
                    $("#hdnUnionBookId").val(0);
                    $("#txtUnionBook").val('');
                    showModal('divAddUnionBook', false);
                    $("#divAddUnionBook_dvModalPopupTitle").text("Add/Edit Union Book");
                }
                else {
                    $("#hdnUnionBookId").val(parseInt($(this).attr("rel")));
                    $("#txtUnionBook").val($(this).text());
                    showModal('divAddUnionBook', false);
                    $("#divAddUnionBook_dvModalPopupTitle").text("Add/Edit Union Book");
                }
            });

            $("body").on("click", "#btnSaveUnionBook", function () {
                if ($.trim($("#txtUnionBook").val()) == "") {
                    $("#txtUnionBook").val('');
                    $("#txtUnionBook").focus();
                    alert("Please enter Union Book");
                    return false;
                }
            });

            $("body").on("click", "#btnSaveUnionBranch", function () {

                var Message = "", AddressMSg = "";

                if ($("#drpUnion :selected").val() == "0") {
                    Message += "Select Union.\n";
                }


                if ($.trim($("#txtEmail").val()) == "") {
                    Message += "Enter email address.\n";
                }
                else if (!ValidateEmail($("#txtEmail").val())) {
                    Message += "Invalid email address.\n";
                }

                if ($.trim($("#txtUnionBranch").val()) == "") {
                    Message += "Enter Union Branch.\n";
                }

                var IsError = false;
                //US address format
                if ($("#hdnAddressType").val() == "0") {

                    var Validate = true;

                    if ($.trim($("#txtAddressLine1").val()) == "") {
                        Message += "Enter Address Line 1\n";
                        Validate = false;
                    }

                    if ($.trim($("#txtCity").val()) == "") {
                        Message += "Enter City\n";
                        Validate = false;
                    }

                    if ($.trim($("#txtState").val()) == "") {
                        Message += "Enter State\n";
                        Validate = false;
                    }

                    if ($.trim($("#txtZipCode").val()) == "") {
                        Message += "Enter Zip Code\n";
                        Validate = false;
                    }
                    if ($("#drpCountry :selected").val() == "0") {
                        Message += "Select Country\n";
                        Validate = false;
                    }

                    if (Validate) {
                        if (parseInt($("#drpCountry option:selected").val()) == parseInt($("#<%=hdnUSCountryID.ClientID %>").val())) {////If selected country is US
                            var isValidate = false;
                            if ($.trim($("#txtState").val()).length > 2 || $.trim($("#txtState").val()) < 2 || isNaN($.trim($("#txtState").val())) == false) {
                                Message += "Enter 2 Characters State Code only\n";
                                isValidate = true;
                            }
                            if ($.trim($("#txtZipCode").val()).length > 5 || $.trim($("#txtZipCode").val()).length < 5 || isNaN($.trim($("#txtZipCode").val()))) {
                                Message += "Enter 5 digit Zip Code\n";
                                isValidate = true;
                            }
                            if (!isValidate) {
                                AddressMSg = USAddressValidation($("#txtAddressLine1").val(), $("#txtAddressLine2").val(), $("#txtCity").val(), $("#txtState").val(), $("#txtZipCode").val(), $("#drpCountry option:selected").text(), "Union", "", "", "", "", "", "");
                                if (AddressMSg == "Error") {
                                    IsError = true;
                                    AddressMSg = "USPS service is down/not responding for address validation";
                                }
                            }
                        }
                    }
                }
                else {
                    if ($.trim($("#txtAddress").val()) == "") {
                        Message += "Enter Address\n";
                    }
                }

                if ($.trim($("#txtPhoneNumber").val()) == "") {
                    Message += "Enter Phone Number.\n";
                }

                if (Message != "") {
                    alert(Message + "\n " + AddressMSg);
                    return false;
                }
                if (AddressMSg != "") {
                    alert(AddressMSg);
                    if (IsError) {

                    }
                    else {
                        return false;
                    }
                }
            });

            $("body").on("click", "#ImgbtnFilterBranch", function () {
                if ($.trim($("#txtUnionBranchFilter").val()) == "" || $.trim($("#txtUnionBranchFilter").val()).toLowerCase() == "type to search") {
                    alert("Type to search union branch");
                    return false;
                }
            });

            $("body").on("click", "#btnFilter", function () {
                if ($.trim($("#txtfilter").val()) == "" || $.trim($("#txtfilter").val()).toLowerCase() == "type to search") {
                    alert("Type to search union");
                    return false;
                }
            });

            function ValidateEmail(email) {
                var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                return expr.test(email);
            };

            /// Union Book end here
        });
    </script>
</asp:Content>
