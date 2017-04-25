<%@ Page Title="Interview Planner" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InterviewPlanner.aspx.cs" Inherits="Crew_InterviewPlanner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../scripts/jsCalendar.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncDataHandler.js" type="text/javascript"></script>
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
    <div id="pageTitle" class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Interview Planner"></asp:Label>
    </div>
    <div id="#dvPageContent" class="page-content-main">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 40%; vertical-align: top; border: 1px solid #dcdcdc;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="display: block">
                                <table>
                                    <tr>
                                        <td>
                                            Staff Code
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStaffCode" runat="server" Width="100px" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Staff Name
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStaffName" runat="server" Width="100px" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_Crewlist_InterviewPlanner"
                                    TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="CrewID" QueryStringField="ID" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtStaffName" Name="STAFF_NAME" PropertyName="Text"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="txtStaffCode" Name="STAFF_CODE" PropertyName="Text"
                                            Type="String" />
                                        <asp:SessionParameter Name="ManningOfficeID" SessionField="UserCompanyID" Type="Int32" />
                                        <asp:Parameter Name="RankID" Type="Int32" DefaultValue="0" />
                                        <asp:SessionParameter Name="iUserID" SessionField="userid" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" Width="99%"
                                    EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="Horizontal"
                                    DataKeyNames="ID" AllowPaging="True" PageSize="15" Font-Size="11px" AllowSorting="True"
                                    DataSourceID="ObjectDataSource1" ForeColor="Black" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("Staff_FullName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Applied_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nation" HeaderStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("ISO_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Passport No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPASSPORT_NUMBER" runat="server" Text='<%# Eval("PASSPORT_NUMBER")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                    Text="Select" ImageUrl="~/Images/Interview_1.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#BFCFDF" Font-Bold="True" ForeColor="Black" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                    <PagerStyle Font-Size="Larger" CssClass="pager" BackColor="White" ForeColor="Black"
                                        HorizontalAlign="Right" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 60%; vertical-align: top;">
                    <div id="dvCalendar">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right; background-color: #dfcfef; color: Black;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="error-message">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: left">
                                        <asp:LinkButton ID="lnkEditInterviewPlanning" runat="server" CssClass="inline-edit"
                                            OnClick="lnkEditInterviewPlanning_Click">[Plan New Interview]</asp:LinkButton>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:RadioButtonList ID="rdoShowInterviews" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rdoShowInterviews_SelectedIndexChanged"
                                            CellPadding="0" CellSpacing="0">
                                            <asp:ListItem Selected="True" Text="Interviews Planned for my Dept." Value="0"></asp:ListItem>
                                            <asp:ListItem Text="All Planned Interviews" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnInterviewID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnUserType" runat="server" Value="" />
                            <asp:HiddenField ID="hdnDateFormat" runat="server" ClientIDMode="Static" />
                            <asp:Panel ID="pnlEdit_InterviewPlanning" runat="server" Visible="false">
                                <div style="text-align: left; border: 1px solid gray; padding: 5px;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                Crew Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlanCrewName" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Interview Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlanDate" runat="server" Width="150px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                                Interview Time
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlanH" runat="server" Width="50px" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="12" Text="12"></asp:ListItem>
                                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                </asp:DropDownList>
                                                H &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlPlanM" runat="server" Width="50px" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                </asp:DropDownList>
                                                M
                                            </td>
                                            <td>
                                                Time Zone:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTimeZone" runat="server" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Current Rank
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlanRank" runat="server" DataTextField="Rank_Short_Name"
                                                    DataValueField="id" Width="156px" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Interviewer
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlanInterviewer" runat="server" DataSourceID="ObjectDataSource4"
                                                    DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPlanInterviewer_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Get_UserList"
                                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="UserCompanyID"
                                                            Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td>
                                                Interview Rank
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlanInterviewerPosition" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="ddlInterviewRank" runat="server" DataTextField="Rank_Short_Name"
                                                    OnSelectedIndexChanged="ddlInterviewRank_SelectedIndexChanged" DataValueField="id"
                                                    Width="154px" AppendDataBoundItems="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Interview Sheet
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlInterviewSheet" runat="server" DataTextField="INTERVIEW_NAME"
                                                    DataValueField="ID" Width="154px" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align: center; padding: 10px">
                                                <asp:Button ID="btnSavePlanning" runat="server" Text="Save" OnClick="btnSavePlanning_Click" />
                                                <asp:Button ID="btnCancelPlanning" runat="server" Text="Cancel" OnClick="btnCancelPlanning_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlView_InterviewPlanning" runat="server">
                                <div style="text-align: center">
                                    <div>
                                        <asp:ObjectDataSource ID="ObjectDataSourceInterviewPl" runat="server" SelectMethod="getPlannedInterviewList"
                                            OnUpdated="ObjectDataSource2_Updated" TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails"
                                            UpdateMethod="UPDATE_CrewInterviewPlanning" OnUpdating="ObjectDataSource2_Updating"
                                            DeleteMethod="DEL_CrewInterviewPlanning">
                                            <DeleteParameters>
                                                <asp:Parameter Name="ID" Type="Int32" />
                                                <asp:SessionParameter Name="Deleted_By" SessionField="UserId" Type="Int32" />
                                            </DeleteParameters>
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="GridView1" Name="iCrewID" PropertyName="SelectedValue"
                                                    Type="Int32" />
                                                <asp:SessionParameter Name="iUserID" SessionField="UserId" Type="Int32" />
                                                <asp:Parameter Name="InterviewType" DefaultValue="Interview" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="InterviewPlanDate" Type="String" />
                                                <asp:Parameter Name="InterviewPlanH" Type="String" />
                                                <asp:Parameter Name="InterviewPlanM" Type="String" />
                                                <asp:SessionParameter Name="Modified_By" SessionField="UserId" Type="Int32" />
                                                <asp:Parameter Name="TimeZone" Type="String" />
                                                <asp:Parameter Name="TZID" Type="Int32" />
                                                <asp:Parameter Name="ID" Type="Int32" />
                                            </UpdateParameters>
                                        </asp:ObjectDataSource>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" EmptyDataText="No Interview planned for the selected crew"
                                            CaptionAlign="Bottom" GridLines="Horizontal" DataKeyNames="ID" AllowPaging="True"
                                            Font-Size="11px" AllowSorting="True" DataSourceID="ObjectDataSourceInterviewPl"
                                            ForeColor="Black" OnRowDeleted="GridView2_RowDeleted" OnRowUpdated="GridView2_RowUpdated"
                                            OnRowDataBound="GridView2_RowDataBound" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Interview Plan Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInterviewPlanDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("InterviewPlanDate")))%>'></asp:Label>
                                                        <asp:Label ID="lblTimeZone" runat="server" Text='<%# Eval("DisplayName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    Date
                                                                </td>
                                                                <td>
                                                                    <asp:HiddenField ID="lblhdnDate" ClientIDMode="Static" Value='<%# Bind("InterviewPlanDate")%>'
                                                                        runat="server" />
                                                                    <asp:TextBox ID="txtPlanDate" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("InterviewPlanDate")))%>'
                                                                        CssClass="EditPlanDate" runat="server"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate"
                                                                        Format='<%# UDFLib.GetDateFormat() %>'>
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Time
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlPlanH" runat="server" Width="50px" AppendDataBoundItems="true"
                                                                        Text='<%# Bind("InterviewPlanH")%>'>
                                                                        <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                                                        <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                                                        <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                                                        <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                                                        <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                                        <asp:ListItem Selected="True" Value="12" Text="12"></asp:ListItem>
                                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    H &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:DropDownList ID="ddlPlanM" runat="server" Width="50px" AppendDataBoundItems="true"
                                                                        Text='<%# Bind("InterviewPlanM")%>'>
                                                                        <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                                        <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    M
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Time Zone:
                                                                </td>
                                                                <td>
                                                                    <%--<asp:DropDownList ID="ddlTimeZoneEdit" runat="server" Width="250px" DataTextField="DisplayName"
                                                                        DataValueField="TimeZone" DataSourceID="ObjectDataSource5" Text='<%# Bind("TimeZone")%>'>
                                                                    </asp:DropDownList>--%>
                                                                    <asp:DropDownList ID="ddlTimeZoneEdit" runat="server" Width="250px" DataTextField="DisplayName"
                                                                        DataValueField="ID" DataSourceID="ObjectDataSource5" SelectedValue='<%# Bind("TZID")%>'>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="Get_TimeZoneList"
                                                                        TypeName="SMS.Business.Infrastructure.BLL_Infra_TimeZones"></asp:ObjectDataSource>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Candidate Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCandidateName" runat="server" Text='<%# Eval("CandidateName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Interviewer" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInterviewer" runat="server" Text='<%# Eval("plannedinterviewer")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Interviewed on" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInterviewDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("InterviewDate"))) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="LinkButton1" CssClass="SaveEdit" runat="server" ImageUrl="~/images/accept.png"
                                                            CausesValidation="True" CommandName="Update" AlternateText="Update" ValidationGroup="noofdays">
                                                        </asp:ImageButton>
                                                        &nbsp;<asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png"
                                                            CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                            CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/images/delete.png"
                                                            CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                            AlternateText="Delete"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#BFCFDF" Font-Bold="True" ForeColor="Black" />
                                            <EditRowStyle BackColor="#CEF6CE" ForeColor="Black" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                            <PagerStyle Font-Size="Larger" CssClass="pager" BackColor="White" ForeColor="Black"
                                                HorizontalAlign="Right" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        var selDate;

        $(document).ready(function () {
            var strDateFormat = $.trim($("#hdnDateFormat").val());
            thisMonth();

            $("body").on("mouseover", ".cellDay  div[rel],.cellToday div[rel]", function () {
                js_ShowToolTip(this.attributes["rel"].textContent, evt, objthis);

            });

            $("body").on("mouseout", ".cellDay  div[rel],.cellToday div[rel]", function () {
                $("#__divMsgTooltip").hide();
                $("#__divMsgTooltip").html('');
            });


            $("body").on("click", ".SaveEdit", function () {
                if ($.trim($(".EditPlanDate").val()) == "") {
                    alert("Enter Date");
                    $(".EditPlanDate").focus();
                    return false;
                }
                if (IsInvalidDate($.trim($(".EditPlanDate").val()), '<%= DateFormat %>')) {
                    alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                    $(".EditPlanDate").focus();
                    return false;
                }
                $("#lblhdnDate").val($.trim($(".EditPlanDate").val()));
            });

        });

        function nextMonth() {
            var CrewID = $('[id$=hdnCrewID]').val();
            var CurrentUserID = $('[id$=hdnUserID]').val();
            var ShowCalForAll = $('[id$=rdoShowInterviews] input:checked').val()


            if (!selDate)
                selDate = new Date();

            if (selDate.getMonth() < 12) {
                selDate = new Date(selDate.getFullYear(), selDate.getMonth() + 1, 1);
            }
            else {
                selDate = new Date(selDate.getFullYear() + 1, 1, 1);
            }
            displayCalendar(document.getElementById("dvCalendar"), selDate);
            Async_getPlannedInterviewForTheMonth(CurrentUserID, CrewID, selDate.getMonth() + 1, selDate.getFullYear(), ShowCalForAll, $.trim($("#hdnDateFormat").val()));
        }
        function prevMonth() {
            var CrewID = $('[id$=hdnCrewID]').val();
            var CurrentUserID = $('[id$=hdnUserID]').val();
            var ShowCalForAll = $('[id$=rdoShowInterviews] input:checked').val()


            if (!selDate)
                selDate = new Date();

            if (selDate.getMonth() > 1) {
                selDate = new Date(selDate.getFullYear(), selDate.getMonth() - 1, 1);
            }
            else {
                selDate = new Date(selDate.getFullYear() - 1, 11, 1);
            }
            displayCalendar(document.getElementById("dvCalendar"), selDate);
            Async_getPlannedInterviewForTheMonth(CurrentUserID, CrewID, selDate.getMonth() + 1, selDate.getFullYear(), ShowCalForAll, $.trim($("#hdnDateFormat").val()));
        }

        function thisMonth() {
            var CrewID = $('[id$=hdnCrewID]').val();
            var CurrentUserID = $('[id$=hdnUserID]').val();
            var ShowCalForAll = $('[id$=rdoShowInterviews] input:checked').val()

            selDate = new Date();

            displayCalendar(document.getElementById("dvCalendar"), selDate);
            Async_getPlannedInterviewForTheMonth(CurrentUserID, CrewID, selDate.getMonth() + 1, selDate.getFullYear(), ShowCalForAll, $.trim($("#hdnDateFormat").val()));
        }

    </script>
</asp:Content>
