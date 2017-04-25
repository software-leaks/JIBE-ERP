<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Title="Crew Index"
    CodeFile="CrewList.aspx.cs" Inherits="CrewList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/CrewIndex_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script>
        function toggle_visibility(id) {

            var e = document.getElementById(id);
            if (e.style.display == '') {

                e.style.display = 'block';
                imgToggle.src = "../Images/up.gif";
            }
            else if (e.style.display == 'block') {
                e.style.display = 'none';
                imgToggle.src = "../Images/down.gif";
            }
            else if (e.style.display == 'none') {
                e.style.display = 'block';
                imgToggle.src = "../Images/up.gif";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Crew Index"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>
                                    </td>
                                    <td style="width: 90px;">
                                    </td>
                                    <td style="width: 25px; text-align: right;">
                                        <img id="imgExport" src="../Images/WordCount.gif" style="cursor: pointer;" title="Export"
                                            alt="Export" />
                                    </td>
                                    <td style="width: 25px">
                                        <img src="../Images/Printer.png" title="Print" style="cursor: pointer;" alt="Print"
                                            onclick="PrintDiv('grid-container')" />
                                    </td>
                                    <td style="width: 25px">
                                        <asp:HyperLink ID="lnkCrewList" runat="server" AlternateText="Crew List" NavigateUrl=""
                                            ToolTip="Photo View" Target="_blank"><img src="../Images/User2.png"  style="border:0px" alt="Crew List" /></asp:HyperLink>
                                    </td>
                                    <td style="width: 25px">
                                        <asp:HyperLink ID="lnkCrewList_Print" runat="server" AlternateText="Crew List" NavigateUrl=""
                                            ToolTip="Crew List" Target="_blank"><img src="../Images/table-icon.png"  style="border:0px;height:18px;" alt="Crew List" /></asp:HyperLink>
                                    </td>
                                    <td style="width: 25px">
                                        <asp:ImageButton ID="ImgFeedback" src="../Images/feedback1.png" Height="20px" runat="server"
                                            ToolTip="Feedback Request" ImageAlign="AbsMiddle" AlternateText="Feedback" OnClick="ImgFeedback_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; padding: 2px; color: Black;">
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="BtnSearch">
                        <table border="0" cellpadding="2" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    Fleet
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Rank
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRank" runat="server" Width="115px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Manning Office
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManningOffice" runat="server" Width="156px">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2" style="font-weight: bold;">
                                    Joining date
                                </td>
                                <td style="text-align: right" rowspan="2">
                                    <asp:Button ID="BtnSearch" runat="server" Width="75px" OnClick="BtnSearch_Click"
                                        Text="Search" CssClass="btnCSS" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    (Staff Code/Staff Name)
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    EOC Due in
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCOC" runat="server" Width="156px">
                                        <asp:ListItem Text="- Ignore -" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="30 Days" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="60 Days" Value="60"></asp:ListItem>
                                        <asp:ListItem Text="90 Days" Value="90"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    From
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchJoinFromDate" runat="server" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtSearchJoinFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nationality
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="156px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Search:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchText" runat="server" Width="110px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Vessel Type :
                                </td>
                                <td align="left">
                                    <ucDDL:ucCustomDropDownList ID="ddlVesselType" runat="server" UseInHeader="False"
                                        Height="150" Width="160" />
                                </td>
                                <%--<td>
                                    &nbsp;
                                </td>
                                 <td>
                                    &nbsp;
                                </td>--%>
                                <td>
                                    To
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchJoinToDate" runat="server" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchJoinToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="btnAddNewCrew" runat="server" Font-Names="Arial" Text="Add Crew"
                                        OnClientClick="javascript:window.open('AddEditCrewNew.aspx');return false;" CssClass="btnNewAddCSS" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMainStatus" runat="server" Width="156px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlMainStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdOnBoardStatusId" runat="server" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Sub Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCalculatedStatus" runat="server" Width="115px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server">
            <ContentTemplate>
                <div id="grid-container" style="margin-top: 2px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                        CellSpacing="0" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                        GridLines="None" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" AllowPaging="false"
                        AllowSorting="true" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated"
                        OnSorting="GridView1_Sorting" CssClass="GridView-css">
                        <Columns>
                            <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>'
                                        Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                    <asp:Label ID="lblX" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="ImgCard" ImageUrl="~/images/YELLOWCARD.PNG" Visible="false" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSTAFF_NAME" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>'
                                        Target="_blank" Text='<%# Eval("Staff_FullName")%>' ForeColor="Black"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                    <%--<asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>' rankid='<%# Eval("RankID")%>' crewid='<%# Eval("ID")%>' CssClass="sailingInfo"></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nation" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("ISO_Code")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Age" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAge" runat="server" Text='<%# Eval("Age")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OnBD">
                                <ItemTemplate>
                                    <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo'
                                        vid='<%# Eval("Vessel_ID")%>' vname='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MPA Ref">
                                <ItemTemplate>
                                    <asp:Label ID="lblMPARef" runat="server" Text='<%# Eval("MPA_Ref")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign On Dt" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblHeader1" runat="server" ForeColor="Blue" CommandArgument="SIGN_ON_DATE"
                                        CommandName="Sort">
                                        Sign On Dt
                                        <img id="SIGN_ON_DATE" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSign_On_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_On_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EOC Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEst_Sing_Off_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date"))) %>'></asp:Label>
                                    <asp:Image ID="ImbCOCModified" runat="server" ImageUrl="~/images/red-dot.png" CausesValidation="False"
                                        Height="16px" AlternateText='<%# Eval("COCRemark")%>' CssClass="imgCOC"></asp:Image>
                                </ItemTemplate>
                                <ItemStyle Width="85px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Available" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvailable_From_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Available_From_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalary" runat="server" Text='<%# Eval("Salary")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manning Office" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblManning" runat="server" Text='<%# Eval("MANNING_OFFICE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblHeader2" runat="server" ForeColor="Blue" CommandArgument="EVAL_LAST_AVG"
                                        CommandName="Sort">
                                        Last
                                        <img id="EVAL_LAST_AVG" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("Marks")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Avg" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblHeader3" runat="server" ForeColor="Blue" CommandArgument="EVAL_ALL_AVG"
                                        CommandName="Sort">
                                        Avg
                                        <img id="EVAL_ALL_AVG" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAvg" runat="server" Text='<%# Eval("AllAvg")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contract Count">
                                <ItemTemplate>
                                    <asp:Label ID="lblContract" runat="server" Text='<%# Eval("ContractCount")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" Font-Size="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblCrewStatus" runat="server" Text='<%# Eval("CrewStatus")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" Font-Size="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="ImgCrewSigningOff" ImageUrl="~/images/clear.gif"
                                        Height="18px" />
                                    <asp:ImageButton runat="server" ID="ImgCrewAssigned" ImageUrl="~/images/clear.gif"
                                        Height="18px" />
                                    <asp:ImageButton runat="server" ID="ImgInterview" ImageUrl="" Height="15px" />
                                    <asp:ImageButton runat="server" ID="ImgMissingDocuments" ImageUrl="~/Images/folder-error.png"
                                        Height="15px" Visible="false" OnClientClick="return false;" />
                                    <a href="CrewWorkflow.aspx?CrewID=<%# Eval("ID")%>" target="_blank">
                                        <img src="../Images/Interview_4.png" style="border: 0" /></a>
                                    <asp:ImageButton runat="server" ToolTip="Edit" ID="ImgEdit" ImageUrl="" Height="15px" />
                                    <img src="../Images/printer.png" title="*Print*" alt="Print" onclick="javascript:window.open('CrewProfile.aspx?P=1&ID=<%# Eval("ID")%>')"
                                        style="cursor: pointer" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    </asp:GridView>
                </div>
                <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                    background-color: #F6CEE3;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 60%">
                                <uc1:ucCustomPager ID="ucCustomPager_CrewList" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff"
                                    AlwaysGetRecordsCount="true" OnBindDataItem="FillGridViewAfterSearch" />
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td style="width: 210px">
                                <a href="InterviewPlanner.aspx" target="_blank">
                                    <asp:Label ID="lblAlert" runat="server" Text=" Interviews scheduled for you today:"></asp:Label></a>
                            </td>
                            <td style="text-align: left">
                                <div id="dvInterviewSchedule">
                                </div>
                            </td>
                            <td style="text-align: right">
                                <span style="color: Blue;">
                                    <asp:Label ID="lblSEQ" runat="server"></asp:Label></span>&nbsp;&nbsp; &nbsp;
                                <img id="imgLoading" src="../Images/ajax.gif" alt="" style="display: none;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dialog" title="Remarks">
    </div>
    <div id="dvProposeYellowCard" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 40%; top: 15%; width: 600px; z-index: 1; color: black">
        <div class="header">
            <div style="right: 0px; position: absolute; cursor: pointer;">
                <img src="../Images/Reload.gif" onclick="reloadFrame('frmCrewCard');" alt="Reload" />
                <img src="../Images/Close.gif" onclick="closeDiv('dvProposeYellowCard');" alt="Close" />
            </div>
            <h4>
                Propose Yellow/Red Card</h4>
        </div>
        <div style="background: white; padding: 5px; margin: 5px;">
            <iframe id="frmCrewCard" src="" frameborder="0" style="height: 450px; width: 100%">
            </iframe>
        </div>
    </div>
    <div id="dvExport" class="draggable" style="display: none; position: absolute; width: 212px;
        padding: 0px; text-align: left;">
        <div class="top" style="text-align: right; background: url(../Images/popupbg8.png) left 0px no-repeat;
            padding: 4px;">
            <img id="close" src="../images/cancel.png" alt="close" /></div>
        <div class="middle1" style="text-align: left; background: url(../Images/popupbg8_m.png) left 0px repeat-y;
            padding: 2px;">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <img src="../Images/Excel-icon.png" alt="Salary Report" style="height: 18px;" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkExportToExcel" runat="server" Text="Export To Excel" OnClick="lnkExportToExcel_Click"></asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../Images/p_list.gif" alt="txt" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkExportToText" runat="server" Text="Export To Text Document"
                            OnClick="lnkExportToText_Click"></asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img runat="server" id="imgUniform" src="../Images/Guard.png" alt="Uniform Size"
                            style="height: 18px;" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkExportUniform" runat="server" Text="Export Uniform Size" OnClick="lnkExportUniform_Click"></asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="text-align: right">
                        Export Options
                    </td>
                    <td>
                        <img id="imgToggle" class="toggle" onclick="toggle_visibility('tdColList1')" child="tdColList1"
                            src="../Images/down.gif" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td id="tdColList1" colspan="2" class="hide" style="text-align: left;">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Vertical">
                            <asp:ListItem Text="Vessel" Value="VESSEL_NAME" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Staff Code" Value="STAFF_CODE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Rank" Value="RANK_SHORT_NAME" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Staff SurName" Value="STAFF_SURNAME" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Staff Name" Value="STAFF_NAME" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Contract Dt" Value="JOINING_DATE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="EOC" Value="EOC" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Nationality" Value="NATIONALITY" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Passport No." Value="PASSPORT_NUMBER" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="SeamanBook" Value="SEAMAN_BOOK_NUMBER" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Base Wage" Value="BASE_WAGE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Advance" Value="ADVANCE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Expenses" Value="EXPENSES" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Daily Rate" Value="DAILY_RATE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="StandbyRate" Value="STANDBY_RATE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Total Days(Full)" Value="TOTAL_DAYS" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Total Wages" Value="TOTAL_WAGES" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Beneficiary" Value="BENEFICIARY" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="BankName" Value="BANK_NAME" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Bank Address" Value="Bank_Address" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Account No" Value="ACC_NO" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Swift Code" Value="SWIFTCODE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Contract Count" Value="ContractCount" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Date Of Birth" Value="STAFF_BIRTH_DATE" Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="bottom1" style="text-align: right; background: url(../Images/popupbg8.png) left -100px no-repeat;
            height: 10px;">
        </div>
    </div>
    <div id="dvToolTip" style="display: none;">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=BtnSearch.ClientID %>", function () {
                var MSG = "";
                if ($.trim($("#<%=txtSearchJoinFromDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtSearchJoinFromDate.ClientID %>").val()), '<%=UDFLib.GetDateFormat()%>')) {
                        MSG = "Enter valid Joining From Date<%=UDFLib.DateFormatMessage() %>\n";
                    }
                }
                if ($.trim($("#<%=txtSearchJoinToDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtSearchJoinToDate.ClientID %>").val()), '<%=UDFLib.GetDateFormat()%>')) {
                        MSG += "Enter valid Joining To Date<%=UDFLib.DateFormatMessage() %>";
                    }
                }
                if (MSG != "") {
                    alert(MSG);
                    return false;
                }
            });
        });
    </script>
</asp:Content>
