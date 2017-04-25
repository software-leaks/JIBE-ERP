<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewMatrix_Simulation.aspx.cs"
    Inherits="Crew_CrewMatrix_Simulation" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList" TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-family: Tahoma,Tahoma,sans-serif,vrdana;
        }
        #gvUnAssignedCrew tr
        {
            height: 25px;
        }
        .tdpager
        {
            border: none !important;
        }
        #gvUnAssignedCrew tr:hover
        {
            background-color: #feecec !important;
        }
        
        #UA_AvailableOptions input
        {
            margin-left: 0;
        }
        #UA_AvailableOptions label
        {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <div class="error-message">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnHost" runat="server" />
    <div id="dvPageContent" style="vertical-align: bottom; color: Black; text-align: left;
        background-color: #fff;" runat="server" clientidmode="Static">
        <div id="divUnassigned" class="grid-container" style="text-align: center;">
            <center>
                <asp:Label ID="lblVesselIdOffsigner" runat="server" Visible="false"></asp:Label>
                <table cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlFilter_UACrew" runat="server" DefaultButton="btnFindUnAssignedCrew">
                                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                                            <tr>
                                                <td colspan="2">
                                                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; font-size: 13px;">
                                                        <tr>
                                                            <td>
                                                                Manning Office
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlManningOffice" runat="server" Width="140">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                Nationality
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlNationality" runat="server" Width="140">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                Rank
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRank_UA" runat="server" Width="140">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdnDefaultRankId" runat="server" Value="0" />
                                                            </td>
                                                            <td style="width: 195px;">
                                                                Min. Years with operator
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlMinYearOperator" runat="server" Width="80px">
                                                                    <asp:ListItem Text='-Select-' Value='0'></asp:ListItem>
                                                                    <asp:ListItem Text='1' Value='1'></asp:ListItem>
                                                                    <asp:ListItem Text='2' Value='2'></asp:ListItem>
                                                                    <asp:ListItem Text='3' Value='3'></asp:ListItem>
                                                                    <asp:ListItem Text='4' Value='4'></asp:ListItem>
                                                                    <asp:ListItem Text='5' Value='5'></asp:ListItem>
                                                                    <asp:ListItem Text='6' Value='6'></asp:ListItem>
                                                                    <asp:ListItem Text='7' Value='7'></asp:ListItem>
                                                                    <asp:ListItem Text='8' Value='8'></asp:ListItem>
                                                                    <asp:ListItem Text='9' Value='9'></asp:ListItem>
                                                                    <asp:ListItem Text='10' Value='10'></asp:ListItem>
                                                                    <asp:ListItem Text='11' Value='11'></asp:ListItem>
                                                                    <asp:ListItem Text='12' Value='12'></asp:ListItem>
                                                                    <asp:ListItem Text='13' Value='13'></asp:ListItem>
                                                                    <asp:ListItem Text='14' Value='14'></asp:ListItem>
                                                                    <asp:ListItem Text='15' Value='15'></asp:ListItem>
                                                                    <asp:ListItem Text='16' Value='16'></asp:ListItem>
                                                                    <asp:ListItem Text='17' Value='17'></asp:ListItem>
                                                                    <asp:ListItem Text='18' Value='18'></asp:ListItem>
                                                                    <asp:ListItem Text='19' Value='19'></asp:ListItem>
                                                                    <asp:ListItem Text='20' Value='20'></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Available Between
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDt_UA" runat="server" Width="135"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFromDt_UA">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                To
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDt_UA" runat="server" Width="135"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtToDt_UA">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                Search
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFreeText_UA" runat="server" Width="135"> </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Min. Years in rank
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlMinYearsRank" runat="server" Width="80px">
                                                                    <asp:ListItem Text='-Select-' Value='0'></asp:ListItem>
                                                                    <asp:ListItem Text='1' Value='1'></asp:ListItem>
                                                                    <asp:ListItem Text='2' Value='2'></asp:ListItem>
                                                                    <asp:ListItem Text='3' Value='3'></asp:ListItem>
                                                                    <asp:ListItem Text='4' Value='4'></asp:ListItem>
                                                                    <asp:ListItem Text='5' Value='5'></asp:ListItem>
                                                                    <asp:ListItem Text='6' Value='6'></asp:ListItem>
                                                                    <asp:ListItem Text='7' Value='7'></asp:ListItem>
                                                                    <asp:ListItem Text='8' Value='8'></asp:ListItem>
                                                                    <asp:ListItem Text='9' Value='9'></asp:ListItem>
                                                                    <asp:ListItem Text='10' Value='10'></asp:ListItem>
                                                                    <asp:ListItem Text='11' Value='11'></asp:ListItem>
                                                                    <asp:ListItem Text='12' Value='12'></asp:ListItem>
                                                                    <asp:ListItem Text='13' Value='13'></asp:ListItem>
                                                                    <asp:ListItem Text='14' Value='14'></asp:ListItem>
                                                                    <asp:ListItem Text='15' Value='15'></asp:ListItem>
                                                                    <asp:ListItem Text='16' Value='16'></asp:ListItem>
                                                                    <asp:ListItem Text='17' Value='17'></asp:ListItem>
                                                                    <asp:ListItem Text='18' Value='18'></asp:ListItem>
                                                                    <asp:ListItem Text='19' Value='19'></asp:ListItem>
                                                                    <asp:ListItem Text='20' Value='20'></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="UA_AvailableOptions" runat="server" CellPadding="0" CellSpacing="1"
                                                                    RepeatDirection="Horizontal" AutoPostBack="true">
                                                                    <asp:ListItem Value="1" Text="Available Ashore" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="On-Board"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="All"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="Label1" runat="server" Text="Vessel" Width="72px"></asp:Label>
                                                             </td>
                                                             <td>
                                                                <asp:DropDownList ID="ddlVessel_UA" runat="server" Width="140">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                Vessel Type
                                                             </td>
                                                             <td>
                                                                <ucDDL:ucCustomDropDownList ID="ddlVesselType" runat="server" UseInHeader="False" 
                                                                        Height="150" Width="140" />        
                                                            </td>
                                                            <td>
                                                                Min. Years on all types of tankers
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlMinYearsAllTankers" runat="server" Width="80px">
                                                                    <asp:ListItem Text='-Select-' Value='0'></asp:ListItem>
                                                                    <asp:ListItem Text='1' Value='1'></asp:ListItem>
                                                                    <asp:ListItem Text='2' Value='2'></asp:ListItem>
                                                                    <asp:ListItem Text='3' Value='3'></asp:ListItem>
                                                                    <asp:ListItem Text='4' Value='4'></asp:ListItem>
                                                                    <asp:ListItem Text='5' Value='5'></asp:ListItem>
                                                                    <asp:ListItem Text='6' Value='6'></asp:ListItem>
                                                                    <asp:ListItem Text='7' Value='7'></asp:ListItem>
                                                                    <asp:ListItem Text='8' Value='8'></asp:ListItem>
                                                                    <asp:ListItem Text='9' Value='9'></asp:ListItem>
                                                                    <asp:ListItem Text='10' Value='10'></asp:ListItem>
                                                                    <asp:ListItem Text='11' Value='11'></asp:ListItem>
                                                                    <asp:ListItem Text='12' Value='12'></asp:ListItem>
                                                                    <asp:ListItem Text='13' Value='13'></asp:ListItem>
                                                                    <asp:ListItem Text='14' Value='14'></asp:ListItem>
                                                                    <asp:ListItem Text='15' Value='15'></asp:ListItem>
                                                                    <asp:ListItem Text='16' Value='16'></asp:ListItem>
                                                                    <asp:ListItem Text='17' Value='17'></asp:ListItem>
                                                                    <asp:ListItem Text='18' Value='18'></asp:ListItem>
                                                                    <asp:ListItem Text='19' Value='19'></asp:ListItem>
                                                                    <asp:ListItem Text='20' Value='20'></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <asp:ImageButton ImageUrl="~/Images/SearchButton.png" ID="btnFindUnAssignedCrew"
                                                                    runat="server" ValidationGroup="ValidateGroup" OnClientClick="return DateValidation();"
                                                                    OnClick="btnFindUnAssignedCrew_Click" ToolTip="Search" />
                                                                <asp:ImageButton Style="width: 23px; height: 21px;" ImageUrl="~/Images/Refresh-icon.png"
                                                                    ID="btnClearSearchUA" CssClass="btnCSS" OnClick="btnClearSearchUA_Click" runat="server"
                                                                    ToolTip="Clear" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 2px; vertical-align: top;">
                                                    <div id="grid-container" style="margin-top: 2px">
                                                        <asp:GridView ID="gvUnAssignedCrew" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                                            GridLines="Horizontal" Width="100%" DataKeyNames="ID,Rank_ID" OnPreRender="gvUnAssignedCrew_PreRender"
                                                            EnablePersistedSelection="true" AllowSorting="true" Font-Size="11px" PageSize="10"
                                                            OnRowDataBound="gvUnAssignedCrew_RowDataBound" OnSorting="gvUnAssignedCrew_Sorting"
                                                            CssClass="GridView-css">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Manning Office">
                                                                    <HeaderTemplate>
                                                                        &nbsp;
                                                                        <asp:LinkButton ID="lblVessel_ODE" runat="server" ForeColor="Black" CommandArgument="Company_Name"
                                                                            CommandName="Sort">
                                                                            Manning Office
                                                                            <img id="Company_Name" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSTAFFID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                                        &nbsp;<asp:Label ID="lblVessel_CODE" runat="server" Text='<%# Eval("Company_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="100px" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S/Code" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lblSO" runat="server" ForeColor="Black" CommandArgument="Staff_Code"
                                                                            CommandName="Sort">
                                                                            S/O Code<img id="Staff_Code" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank">
                                                                            <%# Eval("staff_Code")%></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lblSOR" runat="server" ForeColor="Black" CommandArgument="Rank_ID"
                                                                            CommandName="Sort">
                                                                            Rank<img id="Rank_ID" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnStaff_Rank_UA" runat="server" Value='<%# Eval("Rank_ID")%>' />
                                                                        &nbsp;<asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lblSOName" runat="server" ForeColor="Black" CommandArgument="staff_name"
                                                                            CommandName="Sort">
                                                                            &nbsp;Name<img id="staff_name" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="100px" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nationality" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lblNSO" runat="server" ForeColor="Black" CommandArgument="Nationality"
                                                                            CommandName="Sort">
                                                                            Nationality<img id="Nationality" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:Label ID="lblNationality" runat="server" Text='<%# Eval("Nationality")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Readiness" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        &nbsp;<asp:Label ID="lblReadyToJoin" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Available_From_Date"))) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lblVNSO" runat="server" ForeColor="Black" CommandArgument="Vessel"
                                                                            CommandName="Sort">
                                                                            Vessel<img id="vessel" runat="server" visible="false" style="border: 0" /></asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' vid='<%# Eval("Vessel_ID")%>'
                                                                            CssClass="vesselinfo"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Operator" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        &nbsp; &nbsp; &nbsp;<asp:Label ID="lblOperator" runat="server" Text='<%# Eval("YearsWithOperator")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        &nbsp; &nbsp; &nbsp;<asp:Label ID="lblYearsOnRank" runat="server" Text='<%# Eval("YearsOnRank")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tanker Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        &nbsp; &nbsp; &nbsp;<asp:Label ID="lblTankerType" runat="server" Text='<%# Eval("ThisTypeOfTanker")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="All Types" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        &nbsp; &nbsp; &nbsp;<asp:Label ID="lblAllTankerType" runat="server" Text='<%# Eval("AllTypesOfTanker")%>'></asp:Label>&nbsp;
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButton ID="RowSelector" CssClass="rbtRowSelector" ValidationGroup="RowSelector"
                                                                            runat="server" Style="margin-top: -1px" CommandName="Select" CommandArgument='<%# Eval("Id") %>' />
                                                                        <asp:ImageButton ID="ImgInvalid" runat="server" ImageUrl="~/images/exclamation.png"
                                                                            CausesValidation="False" Style="margin-top: -1px" AlternateText="Invalid" Visible="false"
                                                                            Height="16px"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="4px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lblNoRecUA" runat="server" Text="No Staff found for the search."></asp:Label>
                                                            </EmptyDataTemplate>
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
                                                        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                                            background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                                            background-color: #F6CEE3;">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 60%">
                                                                        <uc1:ucCustomPager ID="ucCustomPager_UnAssignedCrew" runat="server" RecordCountCaption="&nbsp;&nbsp;Staffs"
                                                                            AlwaysGetRecordsCount="true" OnBindDataItem="Search_UnAssigned" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSimulate" runat="server" OnClick="btnSimulate_Click" Text="Simulate"
                                        Style="width: 70px" />
                                    <asp:Button ID="btnCancel" ClientIDMode="Static" runat="server" Text="Cancel" Style="width: 70px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </div>
    </form>
    <script type="text/javascript">

        var DateFormat = '<%=DateFormat %>';

        function Call(a, b, c) {
            window.parent.BindCrewGrid(a, b, c, 0, 0, false);
        }

        function DateValidation() {

            if ($.trim($("#txtFromDt_UA").val()) != "") {
                if (IsInvalidDate($("#txtFromDt_UA").val(), DateFormat)) {
                    alert("Enter valid Available Between date<%= UDFLib.DateFormatMessage() %>");
                    $("#txtFromDt_UA").focus();
                    return false;
                }
            }

            if ($.trim($("#txtToDt_UA").val()) != "") {
                if (IsInvalidDate($("#txtToDt_UA").val(), DateFormat)) {
                    alert("Enter valid To date<%= UDFLib.DateFormatMessage() %>");
                    $("#txtToDt_UA").focus();
                    return false;
                }
            }

            if ($.trim($("#txtFromDt_UA").val()) != "" && $.trim($("#txtToDt_UA").val()) != "") {
                if (new Date(DateAsFormat($("#txtFromDt_UA").val(), DateFormat)) > new Date(DateAsFormat($("#txtToDt_UA").val(), DateFormat))) {
                    alert('Available Between date cannot be greater than To date');
                    return false;
                }
            }
        }

        $(document).ready(function () {
            $("#btnFindUnAssignedCrew").click();

            $("body").on("click", ".rbtRowSelector", function () {
                $(".rbtRowSelector input[type='radio']").prop("checked", false);
                $(this).children("input")[0].checked = true;
            });

            $("body").on("click", "#btnCancel", function () {
                window.parent.$("#closePopupbutton").click();
                return false;
            });

            $("body").on("click", "#btnClearSearchUA", function () {
                $("#ddlVessel_UA").val('0');
                $("#ddlManningOffice").val('0');
                $("#ddlNationality").val('0');
                $("#ddlRank_UA").val('0');
                $("#ddlMinYearsAllTankers").val('0');
                $("#ddlMinYearsRank").val('0');
                $("#ddlMinYearOperator").val('0');
                $("#txtFromDt_UA").val('');
                $("#txtToDt_UA").val('');
                $("#txtFreeText_UA").val('');
                $("#UA_AvailableOptions input[type='radio']").prop("checked", false);
                $("#UA_AvailableOptions_0").prop("checked", true);
                $("#ddlRank_UA").val('0');
                $('[id$=ddlRank_UA]').val(document.getElementById('hdnDefaultRankId').value);

            });
        });

        function BindHeight() {
            window.parent.$("#divContentdvPopupFrame").attr("style", "");
            if (parseInt($('#divUnassigned').height()) < 250) {
                window.parent.$("#divContentdvPopupFrame").attr("style", "height:" + parseInt($('#divUnassigned').height() + 90) + "px");
                window.parent.$("#dvPopupFrame").css("height", parseInt($('#divUnassigned').height() + 110) + "px");
            }
            else {
                window.parent.$("#divContentdvPopupFrame").attr("style", "height:" + parseInt($('#divUnassigned').height() + 30) + "px");
                window.parent.$("#dvPopupFrame").css("height", parseInt($('#divUnassigned').height() + 45) + "px");
            }
        }

      
    </script>
</body>
</html>
