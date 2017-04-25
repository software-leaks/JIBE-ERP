<%@ Page Title="Vetting Questionnaire Details" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Vetting_QuestionnaireDetails.aspx.cs" Inherits="Technical_Vetting_Vetting_QuestionnaireDetails"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function showAddQuestion() {
            var Addmode;
            document.getElementById('IframeAddQuestion').src = "Vetting_AddQuestion.aspx?Questionnaire_ID=" + document.getElementById($('[id$=hfQuestionnaireID]').attr('id')).value + "&Addmode=Add";

            $("#dvAddQuestion").prop('title', 'Add Section / Question');
            showModal('dvAddQuestion');
            return false;

        }

        function Attachment() {
            document.getElementById('IframeAddAttachment').src = "Vetting_QuestionnaireAttachment.aspx?Questionnaire_ID=" + document.getElementById($('[id$=hfQuestionnaireID]').attr('id')).value;
            $("#dvPopupAddAttachment").prop('title', 'Add Attachment');
            showModal('dvPopupAddAttachment');
            return false;
        }
        function UpdatePageQuestion() {

            hideModal("dvAddQuestion");          
            document.getElementById($('[id$=hdnRetrive]').attr('id')).click();


        }
        function UpdateGridQuestionNo() {    
            document.getElementById($('[id$=hdnRetrive]').attr('id')).click();

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

        function ValidateBtnSave() {
            var VersionNo = document.getElementById($('[id$=txtVersion]').attr('id')).value.trim();
            if (VersionNo == "") {
                alert("Version number is required.");
                document.getElementById($('[id$=txtVersion]').attr('id')).focus();
                return false;
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 10%;
            height: 34px;
        }
        .style2
        {
            width: 20%;
            height: 34px;
        }
    </style>
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
            Vetting Questionnaire Details
        </div>
        <div class="page-content" style="font-family: Tahoma; font-size: 12px">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRetrieve">
                <asp:UpdatePanel ID="UpdPnlEdit" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="2" width="100%" style="color: Black; background-color:#f2f2f2;">
                            <tr>
                                <td align="left" >
                                    <asp:Label ID="lblSection" runat="server" Text="Section :"></asp:Label>
                                </td>
                                <td style="text-align: left" >
                                    <CustomFilter:ucfDropdown ID="DDLSection" runat="server" Height="200" UseInHeader="false"
                                        Width="160" OnApplySearch="VET_Get_QuestionNoBySectionNo"/>
                                </td>
                                <td align="left" >
                                    <asp:Label ID="lblQuestionNo" runat="server" Text="Question :"></asp:Label>
                                </td>
                                <td style="text-align: left" >
                                    <CustomFilter:ucfDropdown ID="DDLQuestion" runat="server" Height="200" UseInHeader="false"
                                        Width="160" />
                                </td>
                                <td align="left" >
                                    <asp:Label ID="lblQuestion" runat="server" Text="Question Name :"></asp:Label>
                                    <asp:TextBox ID="txtQuestion" runat="server" Width="140px"></asp:TextBox>
                                </td>
                                <td align="right" >
                                    <asp:ImageButton ID="btnRetrieve" runat="server" Height="24px" ImageAlign="AbsBottom"
                                        ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieve_Click" ToolTip="Search" />
                                    <asp:ImageButton ID="btnClearFilter" runat="server" Height="22px" ImageUrl="~/Images/filter-delete-icon.png"
                                        ToolTip="Clear Filter" OnClick="btnClearFilter_Click" />                                    
                                    <asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" Height="25px"
                                        ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" ToolTip="Export to Excel" />
                                    <asp:ImageButton ID="btnMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                        Height="25px" ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" Style="display: none;"
                                        ToolTip="Export to Excel unformatted" />
                                        
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="hdnRetrive" runat="server" Text="Retrive" Style="visibility: hidden" 
                onclick="hdnRetrive_Click"/>
                        <table cellpadding="2" cellspacing="2" width="100%" style="border: 1px solid;">
                            <tr>
                                <td align="center" colspan="10">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="70px" OnClick="btnEdit_Click" />
                                    <asp:Button ID="btnPublish" runat="server" Text="Publish" Width="70px" OnClick="btnPublish_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left"  style="vertical-align:top;" class="style1">
                                    <asp:Label ID="lblModule" runat="server" Text="Module :"></asp:Label>
                                </td>
                                <td align="left"  style="vertical-align:top;" class="style2">
                                    <asp:Label ID="txtModule" runat="server" ></asp:Label>
                                </td>
                                <td align="left"  style="vertical-align:top;" class="style1">
                                    <asp:Label ID="lblVesselType" runat="server" Text=" Vessel Type :"></asp:Label>
                                </td>
                                <td   align="left" style="vertical-align:top;" class="style2">                              
                             <%--  <div style="vertical-align:top;overflow:auto;height:30px;">--%>
                                  <asp:Label ID="txtVesselType" runat="server" ></asp:Label>
                           <%--    </div>--%>
                               
                            
                                  
                                </td>
                                <td align="left" style="vertical-align:top;" class="style1">
                                    <asp:Label ID="lblQuestionnaireName" runat="server" Text="Questionnaire Name :" Width="125px"></asp:Label>
                                </td>
                                <td align="left" style="vertical-align:top;" class="style2">
                                    <asp:Label ID="txtQuestionnaire" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" >
                                    <asp:Label ID="lblVettingType" runat="server" Text=" Vetting Type :"></asp:Label>
                                </td>
                                <td align="left" >
                                    <asp:Label ID="txtVettingType" runat="server" ></asp:Label>
                                </td>
                                <td style="text-align: left" >
                                    <asp:Label ID="lblNumber" runat="server" Text="Number :"></asp:Label>
                                </td>
                                <td style="text-align: left" width="120px" >
                                    <asp:Label ID="txtNumber" runat="server" ></asp:Label>
                                </td>
                                <td style="text-align: left" width="12%">
                                    <asp:Label ID="lblVersion" runat="server" Text="Version :"></asp:Label>
                                </td>
                                <td style="text-align: left" >
                                    <asp:TextBox ID="txtVersion" runat="server" AutoPostBack="true" Enabled="false" BackColor="#ffff99"
                                        Width="140px"></asp:TextBox>
                                    <asp:ImageButton ID="btnSave" runat="server" Height="14px" ForeColor="Black" ImageUrl="~/Images/Save.png"
                                        OnClick="btnSave_Click" ToolTip="Save" Enabled="false" OnClientClick="return ValidateBtnSave();" />
                                        <asp:ImageButton ID="btnCancel" runat="server" Height="16px" ForeColor="Black" ImageUrl="~/Images/delete.png"
                                        OnClick="btnCancel_Click" ToolTip="Cancel" Enabled="false" />
                                 
                                </td>
                                <td align="left" >
                                </td>
                            </tr>
                            <tr>
                            <td align="right" colspan="7"  >
                               <table cellpadding="2" cellspacing="2"  style="color: Black;">
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="ImgAttach" runat="server" Height="14px" ForeColor="Black" ImageUrl="~/Images/VET_Attach.png"
                                        OnClientClick="return Attachment();" ToolTip="Attachment" />
                               </td>
                               <td>
                               
                                        <asp:Button  ID="ImgAdd" runat="server"   Enabled="false" ToolTip="Add Question"  OnClientClick="return showAddQuestion();" style="float: right; font-size: 11px;" Text="Add Question" />
                                </td>
                            </tr>
                        </table>
                            
                            </td>
                            </tr>
                        </table>
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="padding-top: 10px">
                    <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfQuestionnaireID" runat="server" />
                            <asp:HiddenField ID="HiddenFlagAdd" runat="server" />
                            <asp:HiddenField ID="hfVersion" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:GridView ID="gvQuestionnaire" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" CellPadding="2" ShowHeaderWhenEmpty="true" Width="100%"
                                    OnRowDataBound="gvQuestionnaire_RowDataBound" AllowSorting="true" DataKeyNames="Question"
                                    CssClass="gridmain-css" GridLines="None">
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px"/>
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Section">
                                          <HeaderStyle  Width="40px"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSection" runat="server" Text='<%#Eval("Section") %>'>'></asp:Label>
                                                <asp:Label ID="lblQuestionId" runat="server" Visible="false" Text='<%#Eval("Question_ID") %>'></asp:Label>
                                                <asp:Label ID="lblQuestionnaire_ID" runat="server" Visible="false" Text='<%#Eval("Questionnaire_ID") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question No">
                                        <HeaderStyle  Width="40px"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQestionNo" runat="server" Text='<%#Eval("Question_No") %>'>'></asp:Label>
                                                <asp:Label ID="lblLevel_1" runat="server" Visible="false" Text='<%#Eval("Level_1") %>'></asp:Label>
                                                <asp:Label ID="lblLevel_2" runat="server" Visible="false" Text='<%#Eval("Level_2") %>'></asp:Label>
                                                <asp:Label ID="lblLevel_3" runat="server" Visible="false" Text='<%#Eval("Level_3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question">
                                        <HeaderStyle  Width="400px"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="400px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                        <HeaderStyle  Width="150px"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                             <HeaderStyle  Width="50px"/>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td style="border-color: transparent;  width: 20px">
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" CommandArgument='<%#Eval("[Question_ID]")%>'
                                                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent;  width: 20px">
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure you wish to continue?')"
                                                                CommandArgument='<%#Eval("[Question_ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent; width: 20px">
                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                            Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;VET_DTL_Questionnaire&#39;,&#39;Question_ID="+Eval("Question_ID").ToString()+"&#39;,event,this)" %>'>
                                                        </asp:Image>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
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
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="VET_Get_QuestionnaireDetails" />
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
            
            <div id="dvAddQuestion" style="display: none; width: 500px;" align="left" title="">
                <iframe id="IframeAddQuestion" src="" frameborder="0" style="height: 380px; width: 500px;">
                </iframe>
            </div>
            <div id="dvPopupAddAttachment" style="display: none; width: 500px;" align="left"
                title="Add Attachment">
                <iframe id="IframeAddAttachment" src="" frameborder="0" style="height: 150px; width: 500px;">
                </iframe>
            </div>
        </div>
    </center>
</asp:Content>
