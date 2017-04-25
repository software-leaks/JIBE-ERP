<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BriefingPlanner.aspx.cs"
    Inherits="Crew_BriefingPlanner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html>
<head id="Head1" runat="server">
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <link id="DynamicLink" rel="stylesheet" runat="server" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="dvMainContent">
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
                                    <table style="width: 100%; font-size: 14px;" class="dataTable">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCrewName" runat="server" Text="Crew Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlanCrewName" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIntDate" runat="server" Text="Date"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlanDate" ClientIDMode="Static" runat="server" Width="150px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
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
                                                <asp:Label ID="lblCurrentRank" runat="server" Text="Current Rank"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlanRank" runat="server" DataTextField="Rank_Short_Name"
                                                    DataValueField="id" Width="156px" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblInterviewer" runat="server" Text="Briefer"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlanInterviewer" runat="server" DataSourceID="ObjectDataSource4"
                                                    DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPlanInterviewer_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Get_OfficeUserList"
                                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials"></asp:ObjectDataSource>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Briefing Sheets"></asp:Label>
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
                                                <asp:Button ID="btnSavePlanning" ClientIDMode="Static" runat="server" Text="Save"
                                                    OnClick="btnSavePlanning_Click" />
                                                <asp:Button ID="btnCancelPlanning" runat="server" Text="Cancel" OnClick="btnCancelPlanning_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlView_InterviewPlanning" runat="server">
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
                                            <asp:Parameter Name="InterviewType" DefaultValue="Briefing" />
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
                                    <asp:GridView ID="gvCrewBriefPlan" CssClass="GridView-css" runat="server" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" CellPadding="2" EmptyDataText="No Briefing planned for the selected crew"
                                        CaptionAlign="Bottom" GridLines="Horizontal" DataKeyNames="ID" AllowPaging="True"
                                        Font-Size="11px" AllowSorting="True" DataSourceID="ObjectDataSource2" ForeColor="Black"
                                        OnRowDataBound="gvCrewBriefPlan_RowDataBound" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Briefing Plan Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInterviewPlanDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("InterviewPlanDate"))) %>'></asp:Label>
                                                    -
                                                    <asp:Label ID="lblTimeZone" runat="server" Text='<%# Eval("DisplayName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Date
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPlanDate" CssClass="txtPlanDate" runat="server" Text='<%# Bind("InterviewPlanDate","{0:dd/MM/yyyy}")%>'></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate"
                                                                    Format='<%# Convert.ToString(Session["User_DateFormat"])%>'>
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
                                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="80px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Briefer" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInterviewer" runat="server" Text='<%# Eval("plannedinterviewer")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Briefed On" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInterviewDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("InterviewDate"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False" HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1" CssClass="EditSave" runat="server" ImageUrl="~/images/accept.png"
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
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    var strDateFormat = '<%=DFormat %>';

    $(document).ready(function () {
        $("body").on("click", "#btnSavePlanning", function () {

            if ($("#txtPlanDate").length > 0) {
                var date1 = document.getElementById("txtPlanDate").value;
                if ($.trim($("#txtPlanDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        $("#txtPlanDate").focus();
                        alert("Enter valid date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }
        });

        $("body").on("click", ".EditSave", function () {
            if ($.trim($(".txtPlanDate").val())=="") {
                $(".txtPlanDate").focus();
                alert("Enter date<%=TodayDateFormat %>");
                return false;
            }
            if (IsInvalidDate($.trim($(".txtPlanDate").val()), strDateFormat)) {
                $(".txtPlanDate").focus();
                alert("Enter valid date<%=TodayDateFormat %>");
                return false;
            }
        });
    });
    
</script>
</html>
