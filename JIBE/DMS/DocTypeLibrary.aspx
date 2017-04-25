<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocTypeLibrary.aspx.cs" Inherits="DMS_DocTypeLibrary"
    MasterPageFile="~/Site.master" Title="Document Type Library" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .gridmain-css
        {
        }
        #ctl00_MainContent_GridViewDocType tr:hover
        {
            background-color: #feecec !important;
        }
        
        .txtAlign
        {
            text-align: center !important;
        }
        
        #ctl00_MainContent_ddlDocReplaceble_pnlListSection
        {
            background-color: #E0E2E3;
        }
        .watermark
        {
            background-color: #f0f8ff;
        }
        .SelectAll
        {
            background-color: #f3f3f3;
            padding: 3px 0px 3px 0px;
            border-bottom: 1px solid #c6c6c6;
        }
        .popupButton
        {
            padding: 3px;
            overflow: hidden;
            background-color: #5588BB;
            text-align: center;
        }
        .popCheckbox
        {
            min-width: 250px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function showDivAddDocType() {
            showModal('dvAddDocType', true, dvAddDocType_onClose);
        }
        function dvAddDocType_onClose() {
        }
        function closeDivAddDocType() {
            hideModal('dvAddDocType');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Document Type
        <div style="float: right">
            <asp:ImageButton ID="ImgExportToExcel" src="../Images/Excel-icon.png" Height="20px"
                ToolTip="Export To Excel" runat="server" OnClick="ImgExportToExcel_Click" AlternateText="Print" />
        </div>
    </div>
    <table width="100%">
        <tr style="background-color: #e0e0e0">
            <td style="width: 100%;">
                <div style="padding: 0px; padding: 2px; border-top: 0; text-align: right;">
                    <asp:LinkButton ID="lnkNewDocument" runat="server" Text='Add New Document Type' CausesValidation="False"
                        OnClick="AddNewDocument"></asp:LinkButton>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" DefaultButton="btnSearch">
                    <table>
                        <tr>
                            <td>
                                Document
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="ddlDocument" runat="server" Width="156px" CssClass="control-edit required"
                                    OnSelectedIndexChanged="LoadDocumentList" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Rank
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="ddlRank" runat="server" Width="156px" CssClass="control-edit required"
                                    OnSelectedIndexChanged="LoadDocumentList" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Nationality
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="ddlNationality" runat="server" Width="156px" CssClass="control-edit required"
                                    OnSelectedIndexChanged="LoadDocumentList" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <%--<asp:Button ID="btnSearch" runat="server" Text="Search"  OnClick="LoadDocumentList"/>--%>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" id="tdVesselFlagLabel">
                                Vessel Flag
                            </td>
                            <td class="data" runat="server" id="tdVesselFlagData">
                                <asp:DropDownList ID="ddlVesselFlag" runat="server" Width="156px" CssClass="control-edit required"
                                    OnSelectedIndexChanged="LoadDocumentList" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td runat="server" id="tdVesselLabel">
                                Vessel
                            </td>
                            <td class="data" runat="server" id="tdVesselData">
                                <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" CssClass="control-edit required"
                                    OnSelectedIndexChanged="LoadDocumentList" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Group
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="ddlGroup" runat="server" Width="156px" CssClass="control-edit required"
                                    OnSelectedIndexChanged="LoadDocumentList" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Search
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" Width="150px" OnTextChanged="LoadDocumentList"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                    TargetControlID="txtSearchText" WatermarkText="Type text here.." WatermarkCssClass="watermark" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="LoadDocumentList" />
                                <asp:HiddenField ClientIDMode="Static" ID="hdnSTCW_Deck_Considered" Value="0" runat="server" />
                            </td>
                            <td>
                                <div style="position: relative; left: 670px;">
                                    <asp:LinkButton ID="lnkClearFilter" runat="server" Text='Clear All' CausesValidation="False"
                                        OnClick="ClearFilter"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdDocTypeId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdDocName" runat="server" />
                        <asp:HiddenField ID="hdnVesselConsidered" runat="server" ClientIDMode="Static" />
                        <asp:GridView ID="GridViewDocType" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView_RowEditing"
                            OnRowDeleting="GridView_RowDeleting" OnRowCancelingEdit="GridView_RowCancelEdit"
                            DataKeyNames="DocTypeID" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red"
                            EmptyDataRowStyle-HorizontalAlign="Center" CaptionAlign="Bottom" OnRowUpdating="GridView_RowUpdating"
                            PageSize="20" CellPadding="2" GridLines="None" Width="100%" CssClass="gridmain-css"
                            OnRowDataBound="GridViewDocType_RowDataBound">
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css txtAlign" Height="25px" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Type Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td colspan="4" style="font-size: 12px; font-weight: bold;">
                                                    <asp:Label ID="lblDocTypeID" runat="server" Text='<%# Eval("DocTypeID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblDocTypeName" runat="server" Text='<%#Eval("DocTypeName")%>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 11px; color: #555;">
                                                <td style="width: 20px">
                                                </td>
                                                <td>
                                                    LEGEND:<asp:Label ID="Label1" runat="server" Text='<%#Eval("Legend")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text='<%#Eval("IsDeckConsidered").ToString() == "True" || Eval("IsEngineConsidered").ToString() == "True"?  "Regn STCW’95 :" : ""  %>'
                                                        ID="lblRegnSTCW"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" class="IsDeckConsidered" runat="server" Text='<%#Eval("IsDeckConsidered").ToString() == "True" ?  Eval("Deck") : ""  %>'></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("IsEngineConsidered").ToString() == "True" ?  Eval("Engine") : ""  %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <table id="tbl1" runat="server">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtDocTypeName" Font-Size="11px" Width="180px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("DocTypeName")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20px">
                                                </td>
                                                <td>
                                                    LEGEND:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtLegend" Font-Size="11px" Width="180px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Legend")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trDeck" runat="server">
                                                <td style="width: 20px">
                                                </td>
                                                <td>
                                                    Regn STCW’95/Deck:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtDeck" Font-Size="11px" Width="180px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Deck")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trEngine" runat="server">
                                                <td style="width: 20px">
                                                </td>
                                                <td>
                                                    Regn STCW’95/Engine:
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtEngine" Font-Size="11px" Width="180px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Engine")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("GroupName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alert" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlertDays" runat="server" Text='<%#Eval("AlertDays")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CheckBoxField DataField="isDocCheckList" HeaderText="Required for SignOn" ItemStyle-HorizontalAlign="Center" />
                                <asp:CheckBoxField DataField="Voyage" HeaderText="Voyage Specific" ItemStyle-HorizontalAlign="Center" />
                                <asp:CheckBoxField DataField="isExpiryMandatory" HeaderText="Expiry Mandatory" ItemStyle-HorizontalAlign="Center" />
                                <asp:CheckBoxField DataField="isScannedDocMandatory" HeaderText="Scanned Required"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Vessel" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkVessels" runat="server" Text='<%# Bind("VesselList") %>' CausesValidation="False"
                                            OnCommand="SelectVessels" CssClass="lnkVessels" CommandArgument='<%# Eval("DocTypeID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vessel Flag" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkVesselFlag" runat="server" Text='<%# Bind("VesselFlagNameList") %>'
                                            CausesValidation="False" CssClass="lnkVesselFlag" OnCommand="SelectVesselFlags"
                                            CommandArgument='<%# Eval("DocTypeID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ranks" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRanks" runat="server" Text='<%# Bind("RankList") %>' CausesValidation="False"
                                            OnCommand="SelectRanks" CommandArgument='<%# Eval("DocTypeID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nationality" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkNationality" runat="server" Text='<%# Bind("CountryList") %>'
                                            CausesValidation="False" OnCommand="SelectNationality" CommandArgument='<%# Eval("DocTypeID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Link to Document Type" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumentType" runat="server" Text='<%#Eval("Document_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Replace" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCourseReplace" runat="server" Text='<%# Bind("ReplacableDocumentList") %>'
                                            CausesValidation="False" OnCommand="SelectReplacableDocument" CommandArgument='<%# Eval("DocTypeID")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                                            OnCommand="EditDocument" CssClass="btnEdit" CommandArgument='<%# Eval("DocTypeID")%>'
                                            ImageUrl="~/images/edit.gif" />
                                        <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                                            CommandArgument='<%# Eval("DocTypeID")%>' OnCommand="DeleteDocument" ImageUrl="~/images/delete.png"
                                            OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                            Height="16px" Width="16px" Style="vertical-align: top; cursor: pointer;" runat="server"
                                            onclick='<%# "Get_Record_Information(&#39;DMS_LIB_DOCTYPES&#39;,&#39;DocTypeID="+Eval("DocTypeID").ToString()+"&#39;,event,this)" %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="66px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#efefef" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <%--   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />--%>
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div id="dvAddDocType" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 850px; position: absolute; left: 40%;
        top: 15%; z-index: 1; color: black" class="ui-widget-content" title="Add New Document Type">
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td width="30%" style="vertical-align: top !important;">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: 12px; text-align: left;">
                                            Doc Type Name<span style="color: Red; padding-left: 5px;">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocType" Width="180px" Height="14px" runat="server" MaxLength="150"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE_txtDocType" runat="server" TargetControlID="txtDocType"
                                                WatermarkText="Type text here.." WatermarkCssClass="watermark" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left;">
                                            Group<span style="color: Red; padding-left: 5px;">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlGroup1" runat="server" Width="180px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left;">
                                            Required for SignOn
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:CheckBox ID="chkCheckList" runat="server" Style="margin-left: -4px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left;">
                                            Scanned Required
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkScannedDoc" runat="server" Style="margin-left: -4px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left;">
                                            Link to Document Type
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDocmentType" runat="server" Width="180px">
                                                <asp:ListItem Text="Select document to link" Value="" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                                <asp:ListItem Text="Seaman" Value="Seaman"></asp:ListItem>
                                                <asp:ListItem Text="Contract" Value="Contract"></asp:ListItem>
                                                <asp:ListItem Text="Certificate of Competency" Value="CertificateCompetency"></asp:ListItem>
                                                <asp:ListItem Text="Administration Acceptance" Value="AdministrationAcceptance"></asp:ListItem>
                                                <asp:ListItem Text="Radio Qualification" Value="RadioQualification"></asp:ListItem>
                                                <asp:ListItem Text="MMC Number" Value="MMCNumber"></asp:ListItem>
                                                <asp:ListItem Text="TWIC Number" Value="TWICNumber"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnAssignedVesselForAdminAcceptance" Value="" runat="server"
                                                ClientIDMode="Static" />
                                            <asp:HiddenField ID="hdnAssignedVesselFlagsForAdminAcceptance" Value="" runat="server"
                                                ClientIDMode="Static" />
                                            <asp:HiddenField ID="hdnDocumentType" runat="server" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trDeck1" visible="false">
                                        <td id="tdDeck" runat="server" style="font-size: 12px; text-align: left;">
                                            Regn STCW’95/Deck
                                        </td>
                                        <td id="tdDeck1" runat="server">
                                            <asp:TextBox ID="txtDeck" Width="180px" Height="14px" runat="server" MaxLength="20"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE_txtDeck" runat="server" TargetControlID="txtDeck"
                                                WatermarkText="Type text here.." WatermarkCssClass="watermark" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="45%" style="vertical-align: top !important;">
                                <table width="100%">
                                    <tr>
                                        <td width="30%">
                                            LEGEND
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLegend" Width="190px" Height="14px" runat="server" MaxLength="20"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE_txtLegend" runat="server" TargetControlID="txtLegend"
                                                WatermarkText="Type text here.." WatermarkCssClass="watermark" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Alert before expiry (Days)
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAlertDays" Width="190px" runat="server" onchange="CheckAlertDays()"
                                                MaxLength="3"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE_txtAlertDays" runat="server" TargetControlID="txtAlertDays"
                                                WatermarkText="Enter Number between 0 to 365" WatermarkCssClass="watermark" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Voyage Specific
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkVoyageDoc" runat="server" Style="margin-left: -4px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Expiry Date Mandatory
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkExpiryMandatory" runat="server" Style="margin-left: -4px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Document Replaceable
                                        </td>
                                        <td>
                                            <div id="dvDocRep">
                                                <ucDDL:ucCustomDropDownList ID="ddlDocReplaceble" runat="server" UseInHeader="false"
                                                    Height="200" Width="190" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trEngine1">
                                        <td id="tdEngine" runat="server">
                                            Regn STCW’95/Engine
                                        </td>
                                        <td id="tdEngine1" runat="server">
                                            <asp:TextBox ID="txtEngine" Width="190px" runat="server" MaxLength="20"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE_txtEngine" runat="server" TargetControlID="txtEngine"
                                                WatermarkText="Type text here.." WatermarkCssClass="watermark" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="margin-top: 5px">
                        <tr>
                            <td runat="server" id="tdVesselFlagLabel1" style="font-size: 11px; background-color: #f1f1f1;
                                font-weight: bold" width="33%">
                                Vessel Flag
                            </td>
                            <td runat="server" id="tdVesselLabel1" style="font-size: 11px; background-color: #f1f1f1;
                                font-weight: bold" width="33%">
                                Vessel
                            </td>
                            <td style="font-size: 11px; font-weight: bold; background-color: #f1f1f1;" width="33%">
                                Nationality
                            </td>
                            <td style="font-size: 11px; font-weight: bold; background-color: #f1f1f1;" width="33%">
                                Rank
                            </td>
                            <td style="font-size: 11px; display: none; background-color: #f1f1f1;" width="33%">
                                Document Replacable
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" id="tdVesselFlagData1" style="border-style: solid; border-color: Silver;
                                border-width: 1px" width="33%">
                                <div style="height: 580px; overflow: auto;">
                                    <asp:CheckBox ID="chkSelectAllVesselFlagList1" runat="server" Text="Select All">
                                    </asp:CheckBox>
                                    <asp:CheckBoxList ID="chkVesselFlagList1" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td runat="server" id="tdVesselData1" style="border-style: solid; border-color: Silver;
                                border-width: 1px" width="33%">
                                <div style="height: 580px; overflow: auto;">
                                    <asp:CheckBox ID="chkSelectAllVesselList1" runat="server" Text="Select All"></asp:CheckBox>
                                    <asp:CheckBoxList ID="chkVesselList1" runat="server">
                                    </asp:CheckBoxList>
                                    <asp:HiddenField ID="hdnVesselList" runat="server" ClientIDMode="Static" />
                                </div>
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px" width="33%">
                                <div style="height: 580px; overflow: auto;">
                                    <asp:CheckBox ID="chkSelectAllCountryList1" runat="server" Text="Select All"></asp:CheckBox>
                                    <asp:CheckBoxList ID="chkCountryList1" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px" width="33%">
                                <div style="height: 580px; overflow: auto;">
                                    <asp:CheckBox ID="chkSelectAllRankList1" runat="server" Text="Select All"></asp:CheckBox>
                                    <asp:CheckBoxList ID="chkRankList1" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px; display: none;"
                                width="33%">
                                <div style="height: 580px; overflow: auto;">
                                    <asp:CheckBoxList ID="chkDocumentList1" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="font-size: 11px; text-align: center; border-style: solid;
                                border-color: Silver; border-width: 0px">
                                <asp:Button ID="btnSaveDocType" runat="server" Text="Save" CausesValidation="false"
                                    OnClick="btnSaveDocType_Click" ClientIDMode="Static" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="closeDivAddDocType()" />
                            </td>
                        </tr>
                    </table>
                    <%-- <div id="dvDocRep" style="position:absolute;top:120px;left:600px;">
                      <ucDDL:ucCustomDropDownList ID="ddlDocReplaceble" runat="server" UseInHeader="false"  Height="200" Width="156"  />   
                    </div>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="pnlCountries" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divCountryList" title="Countries" style="font-family: Tahoma; color: black;
                display: none;">
                <div class="SelectAll">
                    <asp:CheckBox ID="chkSelectAllCountryList" runat="server" Text="Select All"></asp:CheckBox>
                </div>
                <div style="height: 580px; overflow: auto;" class="popCheckbox">
                    <asp:CheckBoxList ID="chkCountryList" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="popupButton">
                    <asp:Button ID="btnSelectCountry" runat="server" Text="Save Selected Countries" OnClick="SaveSelectedCountries" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlRanks" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divRankList" title="Ranks" style="font-family: Tahoma; color: black; display: none;">
                <div class="SelectAll">
                    <asp:CheckBox ID="chkSelectAllRankList" runat="server" Text="Select All"></asp:CheckBox></div>
                <div style="height: 580px; overflow: auto;" class="popCheckbox">
                    <asp:CheckBoxList ID="chkRankList" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="popupButton">
                    <asp:Button ID="btnSelectRanks" runat="server" Text="Save Selected Ranks" OnClick="SaveSelectedRanks" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlVessels" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divVesselList" title="Vessels" style="font-family: Tahoma; color: black;
                display: none;">
                <div class="SelectAll">
                    <asp:CheckBox ID="chkSelectAllVesselList" runat="server" Text="Select All"></asp:CheckBox>
                </div>
                <div style="height: 580px; overflow: auto;" class="popCheckbox">
                    <asp:CheckBoxList ID="chkVesselList" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="popupButton">
                    <asp:Button ID="btnSelectVessels" runat="server" Text="Save Selected Vessels" OnClick="SaveSelectedVessels" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlVesselFlags" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divVesselFlagList" title="Vessel Flags" style="font-family: Tahoma; color: black;
                display: none;">
                <div class="SelectAll">
                    <asp:CheckBox ID="chkSelectAllVesselFlagList" runat="server" Text="Select All"></asp:CheckBox></div>
                <div style="height: 580px; overflow: auto;" class="popCheckbox">
                    <asp:CheckBoxList ID="chkVesselFlagList" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="popupButton">
                    <asp:Button ID="btnSelectVesselFlags" runat="server" Text="Save Selected Vessel Flags"
                        OnClick="SaveSelectedVesselFlags" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnIsAdminAcceptance" Value="0" ClientIDMode="Static" runat="server" />
    <asp:UpdatePanel ID="pnlReplacableDocuments" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divReplacableDocumentList" title="Replacable Documents" style="font-family: Tahoma;
                color: black; display: none;">
                <div style="height: 580px; overflow: auto;" class="popCheckbox">
                    <asp:CheckBoxList ID="chkReplacableDocuments" runat="server">
                    </asp:CheckBoxList>
                </div>
                <div class="popupButton">
                    <asp:Button ID="btnSelectReplacableDocuments" runat="server" Text="Save Selected Replacable Documents"
                        OnClick="SaveReplacableDocuments" /></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function checkhdnSTCW_Deck_Considered() {
            if ($("#hdnSTCW_Deck_Considered").val() == "0")
                $(".IsDeckConsidered").hide();
            else
                $(".IsDeckConsidered").show();
        }


        $("#ctl00_MainContent_ddlDocReplaceble_txtSearchItems").attr("placeholder", "Select replaceable Documents");
        $(document).ready(function () {
            checkhdnSTCW_Deck_Considered();

            $("body").on("click", "#btnSaveDocType", function () {
                var msg = "";
                ///check for document type name for empty
                if (($.trim($("#ctl00_MainContent_txtDocType").val()) == "") || $.trim($("#ctl00_MainContent_txtDocType").val()) == "Type text here..") {
                    alert("Document Type Name is mandatory!");
                    return false;
                }
                else {
                    if (CheckAlertDays() == true) {
                        var ItemArray = new Array;
                        var gridview = document.getElementById("<%= GridViewDocType.ClientID %>");
                        for (i = 1; i < gridview.rows.length; i++) {
                            var RowNumber = (i + 1).toString();
                            if (i < 9) {
                                RowNumber = "0" + (i + 1).toString();
                            }
                            var label = document.getElementById("ctl00_MainContent_GridViewDocType_ctl" + RowNumber + "_lblDocTypeName");
                            ItemArray.push(label.innerHTML.toLowerCase());
                        }
                        var hdnValue = document.getElementById("<%= hdDocName.ClientID %>");
                        var docTypeName = document.getElementById("<%= txtDocType.ClientID %>");
                        if (hdnValue.value != "") {
                            if (hdnValue.value.toLowerCase() != docTypeName.value.trim().toLowerCase()) {
                                if (ItemArray.indexOf(docTypeName.value.trim().toLowerCase()) > -1) {
                                    alert("Document Type Name already exists!");
                                    hdnValue.value = "";
                                    return false;
                                }
                            }
                        }
                        else {
                            if (ItemArray.indexOf(docTypeName.value.trim().toLowerCase()) > -1) {
                                alert("Document Type Name already exists!");
                                hdnValue.value = "";
                                return false;
                            }
                        }

                        if ($.trim($("#ctl00_MainContent_ddlDocmentType").val()) == "AdministrationAcceptance") {
                            if ($("#ctl00_MainContent_tdVesselData1").length > 0) {
                                ///check for atleast one vessel is selected
                                if ($("#ctl00_MainContent_chkVesselList1 input[type='checkbox']:checked").length == 0) {
                                    alert("Please select atleast one Vessel");
                                    return false;
                                }
                            }
                            else if ($("#ctl00_MainContent_tdVesselFlagData1").length > 0) {
                                ///check for atleast one vessel flag is selected
                                if ($("#ctl00_MainContent_chkVesselFlagList1 input[type='checkbox']:checked").length == 0) {
                                    alert("Please select atleast one Vessel Flag");
                                    return false;
                                }
                            }
                        }
                        if ($("#hdnDocumentType").val() == "" && $("#hdDocTypeId").val() != "") {
                            var newDocumentType = $("#<%=ddlDocmentType.ClientID %>").val();
                            if (newDocumentType == "AdministrationAcceptance" || newDocumentType == "CertificateCompetency" || newDocumentType == "RadioQualification")
                                if (!confirm("Change in document type will update crew matrix values")) {
                                    return false;
                                }
                        }
                        else {
                            var oldDocumentType = $("#hdnDocumentType").val();
                            if (oldDocumentType != "") {
                                var newDocumentType = $("#<%=ddlDocmentType.ClientID %>").val();
                                if ((newDocumentType == "AdministrationAcceptance" || newDocumentType == "CertificateCompetency" || newDocumentType == "RadioQualification") ||
                            (oldDocumentType == "AdministrationAcceptance" || oldDocumentType == "CertificateCompetency" || oldDocumentType == "RadioQualification")) {
                                    if ($("#hdnDocumentType").val() != newDocumentType) {
                                        if (!confirm("Change in document type will update crew matrix values")) {
                                            return false;
                                        }
                                    }
                                    else {
                                        // if the Document Type is AdministrationAcceptance & Vessel list / Vessel flag list is updated ,alert message should be displayed
                                        var checkedItems = "";
                                        if ($.trim($("#ctl00_MainContent_ddlDocmentType").val()) == "AdministrationAcceptance") {
                                            if ($("#hdnVesselConsidered").val() == "VesselConsidered") {
                                                var chkBox = document.getElementById("<%=chkVesselList1.ClientID %>");
                                                var c = chkBox.getElementsByTagName('input');
                                                for (var i = 0; i < c.length; i++) {
                                                    if (c[i].checked) {
                                                        checkedItems = checkedItems.toString() + i + ",";
                                                    }
                                                }
                                            }
                                            else {
                                                var chkBox = document.getElementById("<%=chkVesselFlagList1.ClientID %>");
                                                var c = chkBox.getElementsByTagName('input');
                                                for (var i = 0; i < c.length; i++) {
                                                    if (c[i].checked) {
                                                        checkedItems = checkedItems.toString() + i + ",";
                                                    }
                                                }
                                            }
                                            if ($("#hdnVesselList").val() != checkedItems) {
                                                if ($("#hdnVesselConsidered").val() == "VesselConsidered") {
                                                    if (!confirm("Change in vessel selection will update crew matrix values")) {
                                                        return false;
                                                    }
                                                }
                                                else {
                                                    if (!confirm("Change in vessel flag selection will update crew matrix values")) {
                                                        return false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        return false;
                }
            });

            /// Check for Administration Acceptance for vessels
            $("body").on("click", "#ctl00_MainContent_btnSelectVessels", function () {
                if ($("#hdnIsAdminAcceptance").val() == "1") {
                    var checkedItems = "";
                    if ($("#ctl00_MainContent_chkVesselList input[type='checkbox']:checked").length == 0) {
                        alert("Please select atleast one Vessel");
                        return false;
                    }

                    var chkBox = document.getElementById("<%=chkVesselList.ClientID %>");
                    var c = chkBox.getElementsByTagName('input');
                    for (var i = 0; i < c.length; i++) {
                        if (c[i].checked) {
                            checkedItems = checkedItems.toString() + i + ",";
                        }
                    }
                    if ($("#hdnVesselList").val() != checkedItems) {
                        if (!confirm("Change in vessel selection will update crew matrix values")) {
                            return false;
                        }
                    }
                }
            });

            /// Check for Administration Acceptance for vessels flags
            $("body").on("click", "#ctl00_MainContent_btnSelectVesselFlags", function () {
                if ($("#hdnIsAdminAcceptance").val() == "1") {
                    var checkedItems = "";
                    if ($("#ctl00_MainContent_chkVesselFlagList input[type='checkbox']:checked").length == 0) {
                        alert("Please select atleast one Vessel Flag");
                        return false;
                    }
                    var chkBox = document.getElementById("<%=chkVesselFlagList.ClientID %>");
                    var c = chkBox.getElementsByTagName('input');
                    for (var i = 0; i < c.length; i++) {
                        if (c[i].checked) {
                            checkedItems = checkedItems.toString() + i + ",";
                        }
                    }
                    if ($("#hdnVesselList").val() != checkedItems) {
                        if (!confirm("Change in vessel flag selection will update crew matrix values")) {
                            return false;
                        }
                    }
                }
            });

            ///Reset hdnIsAdminAcceptance value
            $("body").on("click", "#closePopupbutton", function () {
                $("#hdnIsAdminAcceptance").val("0");
            });


            ///Checking for administration acceptance
            $("body").on("click", ".lnkVesselFlag", function () {
                var Id = this.id.replace("ctl00_MainContent_GridViewDocType", "").replace("lnkVesselFlag", "");
                if ($("#ctl00_MainContent_GridViewDocType" + Id + "lblDocumentType")[0].innerText == "AdministrationAcceptance") {
                    $("#hdnIsAdminAcceptance").val("1");
                }
                else {
                    $("#hdnIsAdminAcceptance").val("0");
                }
            });

            ///Checking for administration acceptance
            $("body").on("click", ".btnEdit", function () {
                var Id = this.id.replace("ctl00_MainContent_GridViewDocType", "").replace("btnEdit", "");
                if ($("#ctl00_MainContent_GridViewDocType" + Id + "lblDocumentType")[0].innerText == "AdministrationAcceptance") {
                    $("#hdnIsAdminAcceptance").val("1");
                }
                else {
                    $("#hdnIsAdminAcceptance").val("0");
                }
            });

            ///Checking for administration acceptance
            $("body").on("click", ".lnkVessels", function () {
                var Id = this.id.replace("ctl00_MainContent_GridViewDocType", "").replace("lnkVessels", "");
                if ($("#ctl00_MainContent_GridViewDocType" + Id + "lblDocumentType")[0].innerText == "AdministrationAcceptance") {
                    $("#hdnIsAdminAcceptance").val("1");
                }
                else {
                    $("#hdnIsAdminAcceptance").val("0");
                }
            });

            $("body").on("change", "#ctl00_MainContent_ddlDocmentType", function () {
                if ($("#ctl00_MainContent_ddlDocmentType").val() == "AdministrationAcceptance") {
                    $("#hdnIsAdminAcceptance").val("1");
                }
                else {
                    $("#hdnIsAdminAcceptance").val("0");
                }
            });

            $("body").on("click", "#<%=chkSelectAllVesselList1.ClientID %>", function () {
                $("#<%=chkVesselList1.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllVesselList1.ClientID %>").prop('checked'));
            });
            $("body").on("click", "#<%=chkSelectAllVesselList.ClientID %>", function () {
                $("#<%=chkVesselList.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllVesselList.ClientID %>").prop('checked'));
            });

            $("body").on("click", "#<%=chkSelectAllVesselFlagList1.ClientID %>", function () {
                $("#<%=chkVesselFlagList1.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllVesselFlagList1.ClientID %>").prop('checked'));
            });
            $("body").on("click", "#<%=chkSelectAllVesselFlagList.ClientID %>", function () {
                $("#<%=chkVesselFlagList.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllVesselFlagList.ClientID %>").prop('checked'));
            });

            $("body").on("click", "#<%=chkSelectAllCountryList1.ClientID %>", function () {
                $("#<%=chkCountryList1.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllCountryList1.ClientID %>").prop('checked'));
            });
            $("body").on("click", "#<%=chkSelectAllCountryList.ClientID %>", function () {
                $("#<%=chkCountryList.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllCountryList.ClientID %>").prop('checked'));
            });

            $("body").on("click", "#<%=chkSelectAllRankList1.ClientID %>", function () {
                $("#<%=chkRankList1.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllRankList1.ClientID %>").prop('checked'));
            });
            $("body").on("click", "#<%=chkSelectAllRankList.ClientID %>", function () {
                $("#<%=chkRankList.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllRankList.ClientID %>").prop('checked'));
            });

            $("body").on("click", "#<%=chkSelectAllRankList1.ClientID %>", function () {
                $("#<%=chkRankList1.ClientID %> input[type='checkbox']").prop("checked", $("#<%=chkSelectAllRankList1.ClientID %>").prop('checked'));
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(EndRequest);

            function EndRequest(sender, args) {
                $("#ctl00_MainContent_ddlDocReplaceble_txtSearchItems").attr("placeholder", "Select replaceable Documents");
            }
        });

        function CheckAlertDays() {
            var days = document.getElementById('ctl00_MainContent_txtAlertDays').value;
            if (days != 'Enter Number between 0 to 365') {
                if (isNaN(days)) {
                    alert("Enter valid alert days");
                    return false;
                }
                else if (days > 365) {
                    alert("Enter alert days upto 365")
                    return false;
                }
                else if (days < 0) {
                    alert("Enter alert days between 0 to 365")
                    return false;
                }
            }
            return true;
        }
    </script>
</asp:Content>
