<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterviewPlannerPopup.aspx.cs" Inherits="Crew_InterviewPlannerPopup" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript">

        $(document).ready(function () {
            $("body").on("mouseover", ".cellDay  div[rel],.cellToday div[rel]", function () {
                js_ShowToolTip(this.attributes["rel"].textContent, evt, null);
            });

            $("body").on("mouseout", ".cellDay  div[rel],.cellToday div[rel]", function () {
                $("#__divMsgTooltip").hide();
                $("#__divMsgTooltip").html('');
            });

        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="dvMainContent">
        <asp:HiddenField ID="hdnDateFormat" runat="server" ClientIDMode="Static" />
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="error-message">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvPageContent" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdnInterviewID" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnUserType" runat="server" Value="" />
                                <asp:Panel ID="pnlEdit_InterviewPlanning" runat="server">
                                    <div style="text-align: left;">
                                        <table>
                                            <tr>
                                                <td valign="top" style="width: 50%">
                                                    <table class="dataTable" style="font-size: 14px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCrewName" runat="server" Text="Crew Name" Width="150px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPlanCrewName" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblIntDate" runat="server" Text="Date"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPlanDate" runat="server" Width="150px"  ClientIDMode="Static" ></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCurrentRank" runat="server" Text="Applied Rank:" 
                                                                    ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPlanRank" runat="server" DataTextField="Rank_Short_Name"
                                                                    DataValueField="id" Width="156px" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblInterviewer" runat="server" Text="Interviewer" 
                                                                    ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPlanInterviewer" runat="server" DataSourceID="ObjectDataSource4"
                                                                    DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPlanInterviewer_SelectedIndexChanged" ClientIDMode="Static">
                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Get_OfficeUserList"
                                                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblIntTime" runat="server" Text="Time"></asp:Label>
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
                                                                <asp:Label ID="lblH" runat="server" Text="H"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlPlanM" runat="server" Width="50px" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblM" runat="server" Text="M"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTimeZone" runat="server" Text="Time Zone"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTimeZone" runat="server" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblInterviewRank" runat="server" Text="Interview Rank" 
                                                                    ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPlanInterviewerPosition" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="ddlInterviewRank" runat="server" DataTextField="Rank_Short_Name"
                                                                    OnSelectedIndexChanged="ddlInterviewRank_SelectedIndexChanged" DataValueField="id"
                                                                    Width="154px" AppendDataBoundItems="true" AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblInterviewSheet" runat="server" Text="Interview Sheet" 
                                                                    ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlInterviewSheet" runat="server" DataTextField="INTERVIEW_NAME"
                                                                    DataValueField="ID" Width="154px" AppendDataBoundItems="true"  ClientIDMode="Static">
                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center; padding: 10px">
                                                                <asp:Button ID="btnSavePlanning" runat="server" Text="Save" OnClick="btnSavePlanning_Click" ClientIDMode="Static" />
                                                                <asp:Button ID="btnCancelPlanning" runat="server" Text="Cancel" OnClick="btnCancelPlanning_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <div id="dvCalendar">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlView_InterviewPlanning" runat="server">
                                    <div>
                                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="getPlannedInterviewList"
                                            OnUpdated="ObjectDataSource2_Updated" TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails"
                                            UpdateMethod="UPDATE_CrewInterviewPlanning" OnUpdating="ObjectDataSource2_Updating"
                                            DeleteMethod="DEL_CrewInterviewPlanning">
                                            <DeleteParameters>
                                                <asp:Parameter Name="ID" Type="Int32" />
                                                <asp:SessionParameter Name="Deleted_By" SessionField="UserId" Type="Int32" />
                                            </DeleteParameters>
                                            <SelectParameters>
                                                <asp:SessionParameter Name="iCrewID" SessionField="CrewID" Type="Int32" />
                                                <asp:SessionParameter Name="iUserID" SessionField="UserId" Type="Int32" />
                                                <asp:Parameter Name="InterviewType" DefaultValue="Interview" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="InterviewPlanDate" Type="String" />
                                                <asp:Parameter Name="InterviewPlanH" Type="String" />
                                                <asp:Parameter Name="InterviewPlanM" Type="String" />
                                                <asp:SessionParameter Name="Modified_By" SessionField="UserId" Type="Int32" />
                                                <asp:Parameter Name="TimeZone" Type="Int32" />
                                                <asp:Parameter Name="TZID" Type="Int32" />
                                                <asp:Parameter Name="ID" Type="Int32" />
                                            </UpdateParameters>
                                        </asp:ObjectDataSource>
                                        <asp:RadioButtonList ID="rdoShowInterviews" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rdoShowInterviews_SelectedIndexChanged"
                                            style="font-size:12px;">
                                            <asp:ListItem Selected="True" Text="Interviews Planned for my Dept." Value="0"></asp:ListItem>
                                            <asp:ListItem Text="All Planned Interviews" Value="1" ></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <div style="width: 100%; height: 170; overflow: auto;">
                                            <asp:GridView ID="GridView2" CssClass="GridView-css" runat="server" AutoGenerateColumns="False"
                                                CellPadding="2" EmptyDataText="No Interview planned for the selected crew" CaptionAlign="Bottom"
                                                GridLines="Horizontal" DataKeyNames="ID" AllowPaging="True" AllowSorting="True"
                                                DataSourceID="ObjectDataSource2" OnRowDeleted="GridView2_RowDeleted" OnRowUpdated="GridView2_RowUpdated"
                                                OnRowDataBound="GridView2_RowDataBound" OnRowEditing="GridView2_RowEditing" Width="100%"
                                                OnRowCancelingEdit="GridView2_RowCancelingEdit" Height="50px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Interview Plan Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInterviewPlanDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("InterviewPlanDate"))) %>'></asp:Label>
                                                            -
                                                            <asp:Label ID="lblTimeZone" runat="server" Text='<%# Eval("DisplayName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        Date
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPlanDate" runat="server" Text='<%# Bind("InterviewPlanDate","{0:dd/MM/yyyy}")%>'></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate"
                                                                            Format='<%# Convert.ToString(Session["User_DateFormat"]) %>'>
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
                                                            <asp:Label ID="lblInterviewDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("InterviewDate"))) %>'></asp:Label> <%--Text='<%# Eval("InterviewDate","{0:dd/MM/yyyy}")%>'--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/images/accept.png" CausesValidation="True"
                                                                CommandName="Update" AlternateText="Update" ValidationGroup="noofdays" OnClick="LinkButton1_Click">
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
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
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
                    </div>
                </td>
            </tr>
        </table>
    </div>

     <script id="jsCalendar" type="text/javascript">
         var selDate;

         $(document).ready(function () {
             var strDateFormat = $.trim($("#hdnDateFormat").val());
             var CurrentDateFormatMessage = '<%= UDFLib.DateFormatMessage() %>';
             thisMonth();

             $("body").on("click", "#btnSavePlanning", function () {
                 var Msg = "";
                 if ($.trim($("#txtPlanDate").val()) == "") {
                     Msg += "Interview Date is mandatory\n";
                 }
                 else
                     if (IsInvalidDate($("#txtPlanDate").val(), strDateFormat)) {
                         Msg += "Enter Valid Interview Date" + CurrentDateFormatMessage + "\n";
                     }
                 if ($("#ddlPlanInterviewer").length > 0) {
                     if ($("#ddlPlanInterviewer option:selected").val() == "0") {
                         Msg += "Interviewer is mandatory\n";
                     }
                 }
                 if ($("#ddlInterviewSheet").length > 0) {
                     if ($("#ddlInterviewSheet option:selected").val() == "0") {
                         Msg += "Interview Sheet is mandatory\n";
                     }
                 }
                 if (Msg != "") {
                     alert(Msg);
                     return false;
                 }
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
             if (selDate.getMonth() > 1 || selDate.getMonth() == 1) {
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
             var ShowCalForAll = $('[id$=rdoShowInterviews] input:checked').val();
             selDate = new Date();
             displayCalendar(document.getElementById("dvCalendar"), selDate);
             Async_getPlannedInterviewForTheMonth(CurrentUserID, CrewID, selDate.getMonth() + 1, selDate.getFullYear(), ShowCalForAll, $.trim($("#hdnDateFormat").val()));
         }
         
    </script>
    </form>
</body>
</html>
