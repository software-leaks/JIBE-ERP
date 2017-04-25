<%@ Page Title="Vetting Questionnaire Index" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Vetting_Questionnaire.aspx.cs" Inherits="Technical_Vetting_Vetting_Questionnaire"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        .hide
        {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function toggleAdvSearch(obj) {

            if ($(obj).text() == "Open Advance Filter") {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $(obj).text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }

            if ($("#<%= hfAdv.ClientID %>").val() == "c") {
                $("#<%= hfAdv.ClientID %>").val('o');
            }
            else {
                $("#<%= hfAdv.ClientID %>").val('c');
            }
        }
        function toggleSerachPostBack() {
            if ($("#<%= hfAdv.ClientID %>").val() == "o") {
                $("#dvAdvanceFilter").show();
                $("#advText").text("Close Advance Filter");
            }
        }

        function toggleOnSearchClearFilter(obj, objval) {

            if (objval == 'o') {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $("#advText").text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }

        }

        function showCreateQuestionnaire() {
            document.getElementById('IframeAddQuestionnaire').src = "Vetting_CreateNewQuestionnaire.aspx";
            $("#dvAddQuestionnaire").prop('title', 'Create New Vetting Questionnaire');
            showModal('dvAddQuestionnaire');
            return false;

        }

        function UpdateQuestionnairePage(Questionnaire_ID, Status) {

            hideModal("dvAddQuestionnaire");
            RedirectQuestionnaire(Questionnaire_ID, Status);
            __doPostBack("<%=btnRetrieve.UniqueID %>", "onclick");


        }

        function HideQuestionnairePage() {

            hideModal("dvAddQuestionnaire");
            __doPostBack("<%=btnRetrieve.UniqueID %>", "onclick");


        }

        function Attachment(Questionnaire_ID) {
            document.getElementById('IframeAddAttachment').src = "Vetting_QuestionnaireAttachment.aspx?Questionnaire_ID=" + Questionnaire_ID;
            $("#dvPopupAddAttachment").prop('title', 'Add Attachment');
            showModal('dvPopupAddAttachment');

        }
        function RedirectQuestionnaire(Questionnaire_ID, Status) {
            window.open("Vetting_QuestionnaireDetails.aspx?Questionnaire_ID=" + Questionnaire_ID + "&Status=" + Status + "&Mode=A");


        }


        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            if (OSName == "MacOS") {
                document.getElementById("ctl00_MainContent_btnExport").style.display = "none";
            }

            if (OSName == "Windows") {
                document.getElementById("ctl00_MainContent_btnMacExport").style.display = "none";
            }

        }
        function ValidateOnBtnSearch() {
            if (isNaN($.trim($("#" + $('[id$=txtNumber]').attr('id')).val()))) {
                alert("Invalid Number");
                $("#" + $('[id$=txtNumber]').attr('id')).focus();
                return false;
            }
        }

        $(window).load(function () {
            $('.version').keypress(function (event) {
                if (event.which < 46 || event.which >= 58 || event.which == 47) {
                    event.preventDefault();
                }

                if (event.which == 46 && $(this).val().indexOf('.') > 18) {
                    this.value = '';
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="page-title">
            Vetting Questionnaire Index
        </div>
        <div class="page-content" style="font-family: Tahoma; font-size: 12px">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRetrieve">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="0" width="100%" style="color: Black; background-color: #f2f2f2;">
                            <tr>
                                <td align="left" colspan="1">
                                    <asp:Label ID="lblModule" runat="server" Text="Module :"></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:DropDownList ID="DDLModule" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                        Width="160">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:Label ID="lblVetType" runat="server" Text="Vetting Type :"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <CustomFilter:ucfDropdown ID="DDLVetType" runat="server" Height="200" UseInHeader="false"
                                        Width="160" />
                                </td>
                                <td align="left" colspan="5">
                                    <asp:Label ID="lblVesselType" runat="server" Text=" Vessel Type :"></asp:Label>
                                </td>
                                <td align="left" colspan="6">
                                    <CustomFilter:ucfDropdown ID="DDLVeseelType" runat="server" UseInHeader="false" Width="160" />
                                </td>
                                <td align="left" colspan="7">
                                    <asp:Label ID="lblStatus" runat="server" Text=" Status :"></asp:Label>
                                </td>
                                <td align="left" colspan="8">
                                    <CustomFilter:ucfDropdown ID="DDLStatus" runat="server" Height="200" UseInHeader="false"
                                        Width="160"></CustomFilter:ucfDropdown>
                                </td>
                                <td align="left" colspan="9">
                                    <asp:Label ID="lblQuestionnaireName" runat="server" Text="Questionnaire Name :"></asp:Label>
                                </td>
                                <td style="text-align: left;" colspan="10">
                                    <asp:TextBox ID="txtQuestionnaire" runat="server" Width="160"></asp:TextBox>
                                </td>
                                <td colspan="11" align="right">
                                    <table cellspacing="0" cellpadding="2" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnRetrieve" runat="server" Height="24px" ImageAlign="AbsBottom"
                                                    ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieve_Click" OnClientClick="return ValidateOnBtnSearch()"
                                                    ToolTip="Search" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnClearFilter" runat="server" Height="22px" ImageUrl="~/Images/filter-delete-icon.png"
                                                    OnClick="btnClearFilter_Click" ToolTip="Clear Filter" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" Height="23px"
                                                    ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" ToolTip="Export to Excel" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                                    Height="23px" ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" Style="display: none;"
                                                    ToolTip="Export to Excel unformatted" />
                                            </td>
                                            <td style="font-size: 11px; line-height: 25px; vertical-align: top">
                                                <a id="advText" href="#" onclick="toggleAdvSearch(this)" style="text-align: left">Open
                                                    Advance Filter</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdAdvFltr" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                        <div id="dvAdvanceFilter" align="left" class="hide" style="padding-top: 10px">
                            <table border="0" cellpadding="1" cellspacing="1" style="width: 250px; background-color: #efefef;">
                                <tr>
                                    <td valign="top" style="border: 1px solid #aabbdd;">
                                        <table border="0" cellpadding="2" cellspacing="1">
                                            <tr style="background-color: #aabbdd;">
                                                <td style="text-align: left; font-weight: bold; width: 250px" colspan="2">
                                                    <asp:Label ID="Label1" runat="server" Height="5%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblNumber" runat="server" Text="Number :"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtNumber" runat="server" EnableViewState="true" Width="130px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblVersion" runat="server" Text="Version :"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtVersion" runat="server" CssClass="version" EnableViewState="true"
                                                        Width="130px" MaxLength="18"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="padding: 10px 0px">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Button ID="ImgAdd" runat="server" ToolTip="Add Questionnaire" OnClientClick="return showCreateQuestionnaire();"
                                    Style="float: right; font-size: 11px;" Text="Create New Questionnaire" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:GridView ID="gvQuestionnaire" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvQuestionnaire_RowDataBound"
                                    Width="100%" AllowSorting="true" OnSorting="gvQuestionnaire_Sorting" DataKeyNames="Questionnaire_Name,Questionnaire_ID"
                                    CssClass="gridmain-css" GridLines="None">
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Questionnaire_id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestionnaire_id" runat="server" Text='<%#Eval("Questionnaire_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vetting Type">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblVettingType" runat="server" Text='<%#Eval("Vetting_Type_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Type">
                                            <HeaderStyle Width="150px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselType" runat="server" Text='<%#Eval("Vessel_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Questionnaire Name">
                                            <HeaderStyle Width="300px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestinnaire" runat="server" Text='<%#Eval("Questionnaire_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumber" runat="server" Text='<%#Eval("Number") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Version">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <HeaderStyle Width="50px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <HeaderStyle Width="80px" />
                                            <ItemTemplate>
                                                <table cellpadding="1" cellspacing="0">
                                                    <tr align="center">
                                                        <td style="border-color: transparent; width: 20px">
                                                            <asp:ImageButton ID="ImgAttatch" runat="server" Text="Atachment" ForeColor="Black"
                                                                ToolTip="Attachment" ImageUrl="~/Images/VET_Attach.png" Height="12px" OnClick='<%# "Attachment(&#39;"+Eval("Questionnaire_ID").ToString()+"&#39;)" %>'>
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent; width: 20px">
                                                            <asp:HyperLink ID="ImgUpdate" runat="server" ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif"
                                                                Height="16px" Target="_blank"></asp:HyperLink>
                                                        </td>
                                                        <td style="border-color: transparent; width: 20px">
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure you wish to continue?')"
                                                                CommandArgument='<%#Eval("[Questionnaire_ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent; width: 20px">
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;VET_LIB_Questionnaire&#39;,&#39;Questionnaire_ID="+Eval("Questionnaire_ID").ToString()+"&#39;,event,this)" %>'>
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" Width="80px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                color: Black; text-align: left;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindQuestionnaire" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnExport" />
                            <asp:PostBackTrigger ControlID="btnMacExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
            <div id="dvPopupAddAttachment" style="display: none; width: 500px;" align="left"
                title="Add Attachment">
                <iframe id="IframeAddAttachment" src="" frameborder="0" style="height: 150px; width: 500px;">
                </iframe>
            </div>
            <div id="dvAddQuestionnaire" style="display: none; width: 420px;" align="left" title="Add Questionnaire">
                <iframe id="IframeAddQuestionnaire" src="" frameborder="0" style="height: 277px;
                    width: 420px;"></iframe>
            </div>
        </div>
    </center>
</asp:Content>
