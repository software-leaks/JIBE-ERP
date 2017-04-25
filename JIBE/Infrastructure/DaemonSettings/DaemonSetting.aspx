<%@ Page Title="Schedular" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="DaemonSetting.aspx.cs" Inherits="Infrastructure_DaemonSettings_DaemonSetting"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link type="text/css" href="../../styles/ui-lightness/jquery-ui-1.8.14.custom.css"
		rel="stylesheet" />
  <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
	<link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
	<script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
	<script src="../../Scripts/boxover.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
	<script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
	<script src="../../Scripts/Wizard.js" type="text/javascript"></script>
	<script src="../../Scripts/boxover.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery.highlight.js" type="text/javascript"></script>

	<style type="text/css">
		#dvWizardDialog
		{
		}
		
		#dvWizardDialog a
		{
			text-decoration: none;
			color: #444;
		}
		#dvWizardDialog input
		{
			font-family: Tahoma;
			font-size: 0.9em;
			color: #444;
		}
		#dvWizardDialog textarea
		{
			font-family: Tahoma;
			font-size: 0.9em;
			color: #444;
		}
		.wizard-steps
		{
			width: 200px;
		}
		#steps .ui-selecting
		{
			/*background: #FECA40;*/
		}
		#steps .ui-selected
		{
			/*background: #F39814;*/
			color: white;
			background-image: url(../../images/ui-bg.png);
			background-repeat: no-repeat;
		}
		#steps
		{
			list-style-type: none;
			margin: 0;
			padding: 0;
			width: 100%; /*background-image:url(../../images/stage.png);*/
		}
		#steps li
		{
			margin: 3px;
			padding: 0.4em;
			font-size: 1.4em;
			height: 24px;
			border-radius: 5px; /*border: 1px solid #C0C0C0;*/
		}
		#steps .step-icon
		{
		}
		#pages
		{
			list-style-type: none;
			margin: 0;
			padding: 0;
			width: 100%;
		}
		.wizard-pages
		{
			min-height: 450px;
			border: 1px solid #C0C0C0;
			border-radius: 5px;
		}
		.wizard-page
		{
			display: none;
			margin: 3px;
			padding: 0.4em;
			font-size: 1.4em;
		}
		.wizard-page-active
		{
			display: block;
		}
		.wizard-page-title
		{
			background: #F0F0F0;
			color: #444;
			padding: 0.4em;
			border-radius: 5px;
			font-size: 1.5em;
			background-image: url(../../images/ui-bg.png);
		}
		.wizard-page-content
		{
			color: #444;
			padding: 0.4em;
			font-size: 0.9em;
		}
		.wizard-page-title .icon
		{
			margin-right: 10px;
		}
		.wizard-controls
		{
			text-align: right;
			padding: 3px;
		}
		
		.wizard-page-content-header
		{
			color: #444;
			font-size: 1.1em;
			margin-bottom: 20px;
		}
	</style>
	<style type="text/css">
		#dvPageContent
		{
		}
		.schedular-row .schedular-row-data
		{
			padding: 2px 0px 10px 10px;
			color: black;
			background: url(../../Images/bg.png) left -161px repeat-x;
			background-color: #F0DBA6;
			border-radius: 8px;
			height: 20px;
		}
		.alternate-schedular-row .schedular-row-data
		{
			padding: 2px 0px 10px 10px;
			color: black;
			background: url(../../Images/bg.png) left -3335px repeat-x;
			background-color: #80E6CC;
			border-radius: 8px;
			height: 20px;
		}
		.schedular-row-cell
		{
			float: left;
			padding-left: 5px;
			vertical-align: top;
			border: 1px solid gray;
		}
		.schedular-row-data a
		{
			padding: 0px;
			text-decoration: none;
		}
		.schedule-status-Active
		{
			border-radius: 5px;
			background-color: yellow;
			text-align: center;
			padding: 2px;
			border: 1px solid #999;
				cursor:pointer;
		}
		.schedule-status-Inactive
		{
			border-radius: 5px;
			background-color: #999;
			text-align: center;
			padding: 2px;
			color: #efefef;
			border: 1px solid Gray;
			cursor:pointer;
		}
		.schedule-status-Paused
		{
			border-radius: 5px;
			background-color: white;
			text-align: center;
			padding: 2px;
			border: 1px solid #efefef;
			cursor:pointer;
		}
		.schedule-timer
		{
			background: url(../../Images/bg.png) left -1024px repeat-x;
			border-radius: 5px;
			background-color: #efefdd;
			text-align: center;
			padding: 2px;
			color: Blue;
			border: 1px solid gray;
			 
		}
	</style>
	<script type="text/javascript">
		$(function () {
			$("#dvWizardDialog").wizard();
			$("#dialog").dialog({ autoOpen: false });
		});

		function validateForm() {
			return true;
		}

		var RefreshID = 0;
		var intervalID = 0;

		function blink(selector) {

			$(selector).fadeOut('slow', function () {
				$(this).fadeIn('slow', function () {
					blink(this);
				});
			});

		}
		function countdown() {
			$('.schedule-timer').fadeIn('slow', function () {
				var schId = $(this).attr('id');
				var dbtime = $(this).attr('dbtime');
				var duetime = $(this).attr('duetime');

				if ($('#' + schId + '_Status').html().indexOf('Active') >= 0) {

					var tlaps = $(this).attr('tlaps');
					tlaps = Math.floor(tlaps) + 1000;
					$(this).attr('tlaps', tlaps);

					var diff = Date.parse(duetime) - Date.parse(dbtime);
					diff -= tlaps;

					if (diff >= 0) {
						var strTime = Math.floor(diff / 86400000) + ':' + Math.floor(diff / 3600000 % 24) + ':' + Math.floor(diff / 60000 % 60) + ':' + Math.floor(diff / 1000 % 60);
						$('#' + schId).html(strTime);

						if (diff <= 10000 && RefreshID == 0) {
							blink('#' + schId);
							RefreshID = 1;
						}
					}
					else {
						if (RefreshID == 1) {
							RefreshID = 0;
							$('#' + schId).html('Processing...');
							setTimeout(Run_Daemon_Process, 1000);
						}
						else
							$('#' + schId).html('Refresh!!');
					}
				}
			});

			$('.schedule-status-Paused').fadeIn('slow', function () {
				$(this).fadeOut('slow', function () {
					$(this).fadeIn('slow', function () { });
				});
			});

		}

		var lastExecutor = null;
		function Run_Daemon_Process() {
			if (lastExecutor != null)
				lastExecutor.abort();

			var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Async_Run_Daemon_Process', false, null, Run_Daemon_Process_OnSuccess, Run_Daemon_Process_onSuccessOnfail);
			lastExecutor = service.get_executor();
		}
		function Run_Daemon_Process_OnSuccess(retval) {
			setTimeout(refreshGrid, 1000);
		}
		function Run_Daemon_Process_onSuccessOnfail() {
		}
		function refreshGrid() {
			$('[id$=ImgBtnRefresh]').trigger('click');
		}
		function initScript() {
			RefreshID = 0;
			//blink('.schedule-status-Paused');

			if (intervalID != 0) {
				window.clearInterval(intervalID);
				intervalID = 0;
			}
			intervalID = window.setInterval(countdown, 1000);
		}
		//$(document).ready(function () { blink('.schedule-status-Paused'); });

		function ald() {
			$("#dialog").dialog("open")



		}

	</script>
   
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnScheduleID" runat="server" />
 <div id="dialog" title="Dialog Title" style="">please, save your configuration before generating the report!</div>
	<asp:UpdateProgress ID="upUpdateProgress" runat="server">
		<ProgressTemplate>
			<div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
				top: 20px; z-index: 2; color: black">
				<img src="../../images/loaderbar.gif" alt="Please Wait" />
			</div>
		</ProgressTemplate>
	</asp:UpdateProgress>
	<div id="page-title" class="page-title">
	</div>
	<div id="dvPageContent" class="page-content-main">
		<asp:UpdatePanel ID="UpdatePanel_Schedules" runat="server">
			<ContentTemplate>
				<div style="text-align: right; padding: 5px;">
					<table style="width: 100%">
						<tr>
							<td style="width: 80px">
								Search
							</td>
							<td style="text-align: left">
								<asp:TextBox ID="txtSearchText" runat="server" Width="200px" Height="20px" AutoPostBack="true"
									OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
							</td>
							<td style="width: 80px">
								Frequency
							</td>
							<td style="text-align: left">
								<asp:DropDownList ID="ddlFrequencyType" runat="server" Width="100px" Height="20px"
									OnSelectedIndexChanged="txtSearchText_TextChanged" AutoPostBack="true">
									<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
									<asp:ListItem Text="Daily" Value="1"></asp:ListItem>
									<asp:ListItem Text="Weekly" Value="2"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td>
								<asp:CheckBox ID="chkOnlyDept" runat="server" Text="Department Worklist Only" 
									oncheckedchanged="chkOnlyDept_CheckedChanged" AutoPostBack="true"/>
							</td>
							<td style="text-align: right">
								<asp:Button ID="btnRunProcess" runat="server" Text="Run Process" BorderStyle="Solid"
									OnClick="btnRunProcess_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
									Height="24px" BackColor="#81DAF5" Width="100px" />
								<asp:Button ID="btnCreateNewSchedule" runat="server" Text="Create New Schedule" BorderStyle="Solid"
									BorderColor="#0489B1" OnClick="btnCreateNewSchedule_Click" BorderWidth="1px"
									Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" />
								<asp:Button ID="ImgBtnRefresh" runat="server" Text="Refresh" BorderStyle="Solid"
									OnClick="ImgBtnRefresh_Click" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
									Height="24px" BackColor="#81DAF5" Width="100px" />
							</td>
						</tr>
					</table>
					
				</div>
				<div id="countdown">
				</div>
				<div id="dvSchedules">
					<asp:GridView ID="grdSchedules" runat="server" AutoGenerateColumns="false" CellPadding="1"
						AllowSorting="true" Width="100%" AllowPaging="false" OnRowCommand="grdSchedules_RowCommand"
						OnRowDataBound="grdSchedules_RowDataBound" OnSorting="grdSchedules_Sorting" ShowHeader="true"
						BorderStyle="None" GridLines="None">
						<Columns>
							<asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
								HeaderText="Schedule Name" SortExpression="Schedule_Name">
								<HeaderTemplate>
									<div class="schedular-row-header">
										<table style="width: 100%" border="0">
											<tr>
												<td style="width: 30px">
												</td>
												<td style="font-size: 14px;">
													Schedule Name
												</td>
												<td style="width: 100px; font-size: 14px;">
													Frequency
												</td>
												<td style="width: 150px; font-size: 14px;">
													Last Run
												</td>
												<td style="width: 60px; font-size: 14px; text-align: center;">
													Result
												</td>
												<td style="width: 150px; font-size: 14px;">
													Next Due
												</td>
												<td style="width: 80px; font-size: 14px;">
													Time Left
												</td>
												<td style="width: 80px; font-size: 14px;">
													Status
												</td>
												<td style="width: 80px; font-size: 14px;">
												</td>
											</tr>
										</table>
									</div>
								</HeaderTemplate>
								<ItemTemplate>
									<div class="schedular-row-data">
										<table style="width: 100%" border="0">
											<tr>
												<td style="width: 30px">
													<img src="../../images/wizard/alarm-clock-icon.png" style="vertical-align: baseline;
														height: 16px;" />
												</td>
												<td style="font-size: 16px;">
												<asp:Label ID="lblDept" runat="server"   ></asp:Label>    
													<asp:LinkButton ID="lnkSchedule" runat="server" CommandName="EDIT_SCHEDULE" CommandArgument='<%#Eval("ScheduleID")%>'
														Text='<%#Eval("Schedule_Name")%>'></asp:LinkButton>
												 
												</td>
												<td style="width: 100px; font-size: 12px;">
													<%#Eval("FrequencyTypeName")%>
												</td>
												<td style="width: 150px; font-size: 12px;">
													<div class='last_run_start' id='<%#Eval("ScheduleID").ToString() + "_Last_Run_Start" %>'>
														<%#Eval("Last_Run_Start", "{0:dd/MM/yyyy HH:mm:ss}")%>
													</div>
												</td>
												<td style="width: 60px; font-size: 12px; text-align: center;">
													<asp:Image ID="imgLast_Run_Result" runat="server" Visible="false" Height="20px" />
												</td>
												<td style="width: 150px; font-size: 12px;">
													<div class='next_due_time' id='<%#Eval("ScheduleID").ToString() + "_Next_Due_Time" %>'>
														<%#Eval("Next_Due_Time", "{0:dd/MM/yyyy HH:mm:ss}")%>
													</div>
												</td>
												<td style="width: 80px; font-size: 12px;">
													<div class='schedule-timer' id='<%# Eval("ScheduleID")%>' dbtime='<%#Eval("DBCurrentTime", "{0:MM/dd/yyyy HH:mm:ss}") %>'
														duetime='<%#Eval("Next_Due_Time", "{0:MM/dd/yyyy HH:mm:ss}")%>' tlaps=''>
														&nbsp;</div>
												</td>
												<td style="width: 80px; font-size: 12px;">
													<div  id='<%# Eval("ScheduleID").ToString() + "_Status"%>' style="display:none" >
														<%#Eval("Status")%></div> 
													<asp:Button ID="btngrdToggle" runat="server" Text='<%#Eval("Status")%>' class='<%#"schedule-status-"+Eval("Status")%>' style="font-size: 12px;width: 80px;" CommandName="TOGGLE_SCHDULE" CommandArgument='<%#Eval("ScheduleID")%>' />
													
												</td>
												<td style="width: 80px; font-size: 12px; text-align: center;">
													<asp:ImageButton ID="imgBtnPause" runat="server" CommandName="PAUSE_SCHEDULE" CommandArgument='<%#Eval("ScheduleID")%>'
														ImageUrl="../../images/wizard/Timer-Pause.png" Style="vertical-align: baseline;"
														Visible='<%#Eval("Pause_Run").ToString()=="1"?true:false%>' />
													<asp:ImageButton ID="imgBtnRun" runat="server" CommandName="RUN_SCHEDULE" CommandArgument='<%#Eval("ScheduleID")%>' 
														ImageUrl="../../images/wizard/Timer-Play.png" Style="vertical-align: baseline;"
														Visible='<%#Eval("Pause_Run").ToString()=="0"?true:false%>' />
													<asp:ImageButton ID="imgBtnRunNow" runat="server" CommandName="RUN_NOW" CommandArgument='<%#Eval("ScheduleID")%>'
														ImageUrl="../../images/wizard/Run-Now.png" Style="vertical-align: baseline;"
														Visible='<%#Eval("Pause_Run").ToString()=="1"?true:false%>' />
													<%--	<asp:ImageButton ID="imhInfo" runat="server"  
														ImageUrl="../../images/RecordInformation.png" Style="vertical-align: baseline;height: 20px;width: 20px;" ToolTip='<%#"Created by :- "+Eval("User_Name")%>'  
														  />--%>
																	
										<asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="../../Images/RecordInformation.png" Style="cursor: hand;vertical-align: baseline;"
											Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;INF_DTL_DaemonSchedular&#39;,&#39;ScheduleID="+Eval("ScheduleID").ToString()+"&#39;,event,this)" %>'
											AlternateText="info" />
									 
							  
												</td>
											</tr>
										</table>
									</div>
								</ItemTemplate>
								<ItemStyle Width="200px" />
							</asp:TemplateField>
						</Columns>
						<EmptyDataTemplate>
							<label id="Label1" runat="server">
								No Task Found !!</label>
						</EmptyDataTemplate>
						<RowStyle CssClass="schedular-row" />
						<AlternatingRowStyle CssClass="alternate-schedular-row" />
					</asp:GridView>
					 <auc:CustomPager ID="ucCustomPagerAllStatus" OnBindDataItem="Load_Current_Schedules" AlwaysGetRecordsCount="true"
								runat="server" />
				</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
	<div id="dvWizardDialog" style="display: none; width: 900px; height: 530px;" title="Task Scheduler">
		<table style="width: 100%">
			<tr>
				<td style="vertical-align: top; width: 200px">
					<div class="wizard-steps">
						<ol id="steps">
							<li class="ui-selected"><a href="#page1"><span class="step-icon"></span>Create New Schedule</a></li>
							<li><a href="#page2"><span class="step-icon"></span>Frequency</a></li>
							<li><a href="#page3"><span class="step-icon"></span>Preferences</a></li>
							<li><a href="#page4"><span class="step-icon"></span>Procedure</a></li>
						</ol>
					</div>
				</td>
				<td style="vertical-align: top">
					<div class="wizard-pages">
						<div id="page1" class="wizard-page">
							<div class="wizard-page-title">
								<span class="icon">
									<img src="../../images/wizard/clock-icon.png" style="vertical-align: bottom" /></span>Create
								New Schedule</div>
							<div class="wizard-page-content">
								<asp:UpdatePanel ID="UpdatePanel_ScheduleName" runat="server">
									<ContentTemplate>
										<table style="width: 100%">
											<tr>
												<td>
													Schedule name
												</td>
												<td>
													<asp:TextBox ID="txtScheduleName" runat="server" Width="100%"></asp:TextBox>
												</td>
											</tr>
											<tr>
												<td>
													Description
												</td>
												<td>
													<asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="200px" Width="100%"></asp:TextBox>
												</td>
											</tr>
										</table>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
						</div>
						<div id="page2" class="wizard-page">
							<div class="wizard-page-title">
								<span class="icon">
									<img src="../../images/wizard/frequency-icon.png" style="vertical-align: bottom" /></span>Frequency</div>
							<div class="wizard-page-content">
								<div class="wizard-page-content-header">
									When do you want the task to start?</div>
								<asp:UpdatePanel ID="UpdatePanel_Frequency" runat="server">
									<ContentTemplate>
										<table style="width: 100%">
											<tr>
												<td style="vertical-align: top; width: 200px;">
													<asp:RadioButtonList ID="rdoFrequency" runat="server" OnSelectedIndexChanged="rdoFrequency_SelectedIndexChanged"
														AutoPostBack="true">
														<asp:ListItem Text="Daily" Value="1" Selected="True"></asp:ListItem>
														<asp:ListItem Text="Weekly" Value="2"></asp:ListItem>
														<asp:ListItem Text="Start of every month" Value="3"></asp:ListItem>
														<asp:ListItem Text="End of every month" Value="4"></asp:ListItem>
													</asp:RadioButtonList>
												</td>
												<td style="vertical-align: top">
													<div style="margin-top: 2px">
														Start:
														<asp:TextBox ID="txtStartDate" runat="server" Width="120px"></asp:TextBox>&nbsp;
														<ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtStartDate"
															Format="dd/MM/yyyy">
														</ajaxToolkit:CalendarExtender>
														End:
														<asp:TextBox ID="txtEndDate" runat="server" Width="120px"></asp:TextBox>
														<ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate"
															Format="dd/MM/yyyy">
														</ajaxToolkit:CalendarExtender>
													</div>
													<div style="margin-top: 10px">
														<asp:Panel ID="pnlDaily" runat="server" Visible="true">
															Recur every
															<asp:TextBox ID="txtDaily" runat="server" Width="40px" Text="1" Enabled="false"></asp:TextBox>days
															<table style="margin-top: 10px">
																<tr>
																	<td style="vertical-align: top">
																		<asp:RadioButtonList ID="rdoDailyFrequency" runat="server">
																			<asp:ListItem Text="Occurs once at:" Value="1"></asp:ListItem>
																			<asp:ListItem Text="Occurs every:" Value="2" Selected="True"></asp:ListItem>
																		</asp:RadioButtonList>
																	</td>
																	<td style="vertical-align: top">
																		<table>
																			<tr>
																				<td>
																					<asp:DropDownList ID="ddlDailyOccursAt_H" runat="server" Width="50px">
																						<asp:ListItem Text="01" Value="1"></asp:ListItem>
																						<asp:ListItem Text="02" Value="2"></asp:ListItem>
																						<asp:ListItem Text="03" Value="3"></asp:ListItem>
																						<asp:ListItem Text="04" Value="4"></asp:ListItem>
																						<asp:ListItem Text="05" Value="5"></asp:ListItem>
																						<asp:ListItem Text="06" Value="6"></asp:ListItem>
																						<asp:ListItem Text="07" Value="7"></asp:ListItem>
																						<asp:ListItem Text="08" Value="8"></asp:ListItem>
																						<asp:ListItem Text="09" Value="9"></asp:ListItem>
																						<asp:ListItem Text="10" Value="10"></asp:ListItem>
																						<asp:ListItem Text="11" Value="11"></asp:ListItem>
																						<asp:ListItem Text="12" Value="12" Selected="True"></asp:ListItem>
																						<asp:ListItem Text="13" Value="13"></asp:ListItem>
																						<asp:ListItem Text="14" Value="14"></asp:ListItem>
																						<asp:ListItem Text="15" Value="15"></asp:ListItem>
																						<asp:ListItem Text="16" Value="16"></asp:ListItem>
																						<asp:ListItem Text="17" Value="17"></asp:ListItem>
																						<asp:ListItem Text="18" Value="18"></asp:ListItem>
																						<asp:ListItem Text="19" Value="19"></asp:ListItem>
																						<asp:ListItem Text="20" Value="20"></asp:ListItem>
																						<asp:ListItem Text="21" Value="21"></asp:ListItem>
																						<asp:ListItem Text="22" Value="22"></asp:ListItem>
																						<asp:ListItem Text="23" Value="23"></asp:ListItem>
																					</asp:DropDownList>
																					H:
																					<asp:DropDownList ID="ddlDailyOccursAt_M" runat="server" Width="50px">
																						<asp:ListItem Text="00" Value="0"></asp:ListItem>
																						<asp:ListItem Text="10" Value="10"></asp:ListItem>
																						<asp:ListItem Text="20" Value="20"></asp:ListItem>
																						<asp:ListItem Text="30" Value="30"></asp:ListItem>
																						<asp:ListItem Text="40" Value="40"></asp:ListItem>
																						<asp:ListItem Text="50" Value="50"></asp:ListItem>
																					</asp:DropDownList>
																					M
																				</td>
																			</tr>
																			<tr>
																				<td>
																					<asp:TextBox ID="txtDailyOccursValue" runat="server" Width="40px" Text="10"></asp:TextBox>
																					<asp:DropDownList ID="ddlDailyOccursType" runat="server" Width="90px">
																						<asp:ListItem Text="Hour(s)" Value="1"></asp:ListItem>
																						<asp:ListItem Text="Minute(s)" Value="2" Selected="True"></asp:ListItem>
																						<asp:ListItem Text="Second(s)" Value="3"></asp:ListItem>
																					</asp:DropDownList>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</asp:Panel>
														<asp:Panel ID="pnlWeekly" runat="server" Visible="false">
															Recur every
															<asp:TextBox ID="txtWeek" runat="server" Width="40px" Text="1"></asp:TextBox>weeks
															on:
															<asp:CheckBoxList runat="server" ID="chkWeekDays">
																<asp:ListItem Text="Sunday" Value="1"></asp:ListItem>
																<asp:ListItem Text="Monday" Value="2"></asp:ListItem>
																<asp:ListItem Text="Tuesday" Value="3"></asp:ListItem>
																<asp:ListItem Text="Wednesday" Value="4"></asp:ListItem>
																<asp:ListItem Text="Thursday" Value="5"></asp:ListItem>
																<asp:ListItem Text="Friday" Value="6"></asp:ListItem>
																<asp:ListItem Text="Saturday" Value="7"></asp:ListItem>
															</asp:CheckBoxList>
														</asp:Panel>
														<asp:Panel ID="pnlStartMonth" runat="server" Visible="false">
															<h3>
																Start of Every Month</h3>
														</asp:Panel>
														<asp:Panel ID="pnlEndMonth" runat="server" Visible="false">
															<h3>
																End of Every month</h3>
														</asp:Panel>
													</div>
												</td>
											</tr>
										</table>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
						</div>
						<div id="page3" class="wizard-page">
							<div class="wizard-page-title">
								<span class="icon">
									<img src="../../images/wizard/procedure-icon.png" style="vertical-align: bottom" /></span>Preferences</div>
							<div class="wizard-page-content">
								<div class="wizard-page-content-header">
									What type of procedure the scheduled task will perform ?</div>
								<asp:UpdatePanel ID="UpdatePanel_Routine" runat="server">
									<ContentTemplate>
										<table style="width: 100%">
											<tr>
												<td style="vertical-align: top; width: 200px;">
													<asp:RadioButtonList ID="rdoRoutineType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoRoutineType_SelectedIndexChanged">
														<asp:ListItem Text="Database Routine" Value="1" Selected="True"></asp:ListItem>
														<asp:ListItem Text="System Routine" Value="2"></asp:ListItem>
														<asp:ListItem Text="Workllist Report" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="PO Report" Value="4"></asp:ListItem>
													</asp:RadioButtonList>
												</td>
												<td style="vertical-align: top; background-color: #efefef;">
													<asp:Panel ID="pnlSubRoutineType_DBRoutine" runat="server" Visible="true">
														<asp:RadioButtonList ID="rdoSubRoutineType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoSubRoutineType_SelectedIndexChanged">
															<asp:ListItem Text="Send Mail-Tabular Data" Value="1" Selected="True"></asp:ListItem>
															<asp:ListItem Text="Dashboard Alert" Value="2"></asp:ListItem>
															<asp:ListItem Text="SMS Alert" Value="3" Enabled="false"></asp:ListItem>
														</asp:RadioButtonList>
													</asp:Panel>
													<asp:Panel ID="pnlSubRoutineType_SystemRoutine" runat="server" Visible="false">
														<!-- enter the method name ( method will be written in calss lib of jibe daemon ) -->
														<table>
															<tr>
																<td>
																	Method Name
																</td>
																<td>
																	<asp:TextBox ID="txtRoutineMethod" runat="server" MaxLength="200"> </asp:TextBox>
																</td>
															</tr>
														</table>
													</asp:Panel>
													<asp:Panel ID="pnlSubRoutineType_Workflow" runat="server" Visible="false">
														<table style="font-size: 10px">
															<tr>
																<td>
																	<span style="font-weight: bold;">Filter on Job Creation</span>
																</td>
															</tr>
															<tr>
																<td>
																	<table style="white-space: nowrap">
																		<tr>
																			<td style="width: 150px">
																				<asp:RadioButton ID="rdbJobCreatedBetween" runat="server" GroupName="JObCreation"
																					Text="Job Created between" />
																			</td>
																			<td>
																				<asp:TextBox ID="txtJCDFrom" runat="server" Width="120px"></asp:TextBox>&nbsp;
																				<ajaxToolkit:CalendarExtender ID="calJCDFrom" runat="server" TargetControlID="txtJCDFrom"
																					Format="dd/MM/yyyy">
																				</ajaxToolkit:CalendarExtender>
																			</td>
																			<td>
																				To
																			</td>
																			<td>
																				<asp:TextBox ID="txtJCDTo" runat="server" Width="120px"></asp:TextBox>&nbsp;
																				<ajaxToolkit:CalendarExtender ID="calJCDTo" runat="server" TargetControlID="txtJCDTo"
																					Format="dd/MM/yyyy">
																				</ajaxToolkit:CalendarExtender>
																			</td>
																		</tr>
																		<tr>
																			<td>
																				<asp:RadioButton ID="rdbJObCreatedLast" runat="server" GroupName="JObCreation" Text="Job Created in Last" />
																			</td>
																			<td>
																				<asp:DropDownList ID="ddlJobCL" runat="server" Width="50px">
																					<asp:ListItem Text="07" Value="7"></asp:ListItem>
																					<asp:ListItem Text="30" Value="30"></asp:ListItem>
																					<asp:ListItem Text="90" Value="90"></asp:ListItem>
																					<asp:ListItem Text="180" Value="180"></asp:ListItem>
																					<asp:ListItem Text="365" Value="365"></asp:ListItem>
																				</asp:DropDownList>
																				&nbsp;Days
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
															<tr>
																<td>
																	<span style="font-weight: bold;">Filter on Completion</span>
																</td>
															</tr>
															<tr>
																<td>
																	<table>
																		<tr>
																			<td>
																				<asp:CheckBox ID="chkShowAllPending" runat="server" GroupName="JObCompletion" Text="Show all pending jobs" />
																			</td>
																		</tr>
																		<tr>
																			<td>
																				<asp:CheckBox ID="chkJobCompletedIn" runat="server" GroupName="JObCompletion" Text="Show jobs completed in last " />&nbsp;&nbsp;&nbsp;&nbsp;
																			</td>
																			<td>
																				<asp:DropDownList ID="ddlJobCompletdIn" runat="server" Width="50px">
																					<asp:ListItem Text="07" Value="7"></asp:ListItem>
																					<asp:ListItem Text="30" Value="30"></asp:ListItem>
																					<asp:ListItem Text="90" Value="90"></asp:ListItem>
																					<asp:ListItem Text="180" Value="180"></asp:ListItem>
																					<asp:ListItem Text="365" Value="365"></asp:ListItem>
																				</asp:DropDownList>
																				&nbsp;Days
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
															<tr>
																<td>
																	<span style="font-weight: bold;">Filter on Job Type</span>
																</td>
															</tr>
															<tr>
																<td>
																	<table>
																		<tr>
																			<td>
                                                                              <asp:RadioButtonList ID="rdbJObType" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>  
																				<%--<asp:RadioButton ID="rdbNCR" runat="server" GroupName="JObType" Text="NCR" />
																			</td>
																			<td>
																				<asp:RadioButton ID="rdbNonNCR" runat="server" GroupName="JObType" Text="NON-NCR" />
																			</td>
																			<td>
																				<asp:RadioButton ID="rdbAllJobs" runat="server" GroupName="JObType" Text="All" />--%>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
															<tr>
																<td>
																	<span style="font-weight: bold;">Include PMS Jobs (Completion status as per below)</span>
																</td>
															</tr>
															<tr>
																<td>
																	<table>
																		<tr>
																			<td>
																				<asp:CheckBox ID="chkPMS" runat="server" GroupName="JObType" Text="PMS Jobs" />
																			</td>
																			<td>
																				&nbsp;&nbsp;&nbsp;&nbsp;
																			</td>
																			<td>
																				<asp:DropDownList ID="ddlPMS" runat="server">
																					<asp:ListItem Text="Overdue" Value="OVERDUE"></asp:ListItem>
																					<asp:ListItem Text="Last 7 days completed" Value="LAST7DAYSCOMPLETED"></asp:ListItem>
																					<asp:ListItem Text="Both" Value="BOTH"></asp:ListItem>
																				</asp:DropDownList>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
															<tr>
																<td>
																	<span style="font-weight: bold;">Generate this report with the above preferences and
																		send by</span> &nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:DropDownList ID="ddlReportBy" runat="server">
																		<asp:ListItem Text="Email" Value="EMAIL"></asp:ListItem>
																		<asp:ListItem Text="PDF" Value="PDF"></asp:ListItem>
																		<asp:ListItem Text="Excel" Value="EXCEL"></asp:ListItem>
																	</asp:DropDownList>
																</td>
															</tr>
															<tr>
																<td>
																	<span style="font-weight: bold;">Save the preferences for </span>&nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:DropDownList ID="ddlPrefFor" runat="server">
																		<asp:ListItem Text="Me" Value="ME"></asp:ListItem>
																		<asp:ListItem Text="My Department" Value="MYDEPARTMENT"></asp:ListItem>
																	</asp:DropDownList>
																</td>
															</tr>
															<tr>
																<td>
																</td>
																<td>
																	<asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnOpenReport_Click" />
															</tr>
														</table>
													</asp:Panel>
                                                    <asp:Panel ID="pnlSubRoutineType_PO" runat="server" Visible="false">
														<table style="font-size: 10px">
															<tr>
																<td>
																	<span style="font-weight: bold;">Filter on PO Creation</span>
																</td>
															</tr>
															<tr>
																<td>
																	<table style="white-space: nowrap">
																		<tr>
																			<td style="width: 150px">
																				<asp:RadioButton ID="rdoPOCreationBetween" runat="server" GroupName="POCreation"
																					Text="PO Created between" />
																			</td>
																			<td>
																				<asp:TextBox ID="txtPOFrom" runat="server" Width="120px"></asp:TextBox>&nbsp;
																				<ajaxToolkit:CalendarExtender ID="CalendarExtendertxtPOFrom" runat="server" TargetControlID="txtPOFrom"
																					Format="dd/MM/yyyy">
																				</ajaxToolkit:CalendarExtender>
																			</td>
																			<td>
																				To
																			</td>
																			<td>
																				<asp:TextBox ID="txtPOTo" runat="server" Width="120px"></asp:TextBox>&nbsp;
																				<ajaxToolkit:CalendarExtender ID="CalendarExtendertxtPOTo" runat="server" TargetControlID="txtPOTo"
																					Format="dd/MM/yyyy">
																				</ajaxToolkit:CalendarExtender>
																			</td>
																		</tr>
																		<tr>
																			<td>
																				<asp:RadioButton ID="rdoPOCreationLast" runat="server" GroupName="POCreation" Text="PO Created in Last" />
																			</td>
																			<td>
																				<asp:DropDownList ID="ddlPOCreatedLast" runat="server" Width="50px">
																					<asp:ListItem Text="07" Value="7"></asp:ListItem>
																					<asp:ListItem Text="30" Value="30"></asp:ListItem>
																					<asp:ListItem Text="90" Value="90"></asp:ListItem>
																					<asp:ListItem Text="180" Value="180"></asp:ListItem>
																					<asp:ListItem Text="365" Value="365"></asp:ListItem>
																				</asp:DropDownList>
																				&nbsp;Days
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
															<tr>
																<td>
																	<span style="font-weight: bold;">Filter on Suppliers</span>
																</td>
															</tr>
															<tr>
																<td>
																	<table>
                                                                    <tr><td><asp:CheckBox ID="chkSuppliers" runat="server" Text="Select Suppliers wise PO" AutoPostBack="true" OnCheckedChanged="chkSuppliers_CheckedChanged" />
                                                                  
                                                                   </td></tr>
																		<tr>
																			<td>
																				<div style="max-height: 140px; max-width:400px; overflow-y: auto; background-color: White;
                                            border-color: Gray; border-style: solid; border-width: 1px">
                                            <asp:CheckBoxList ID="chklstSuppliers" runat="server" Width="400px" Enabled="false" >
                                            </asp:CheckBoxList>
                                        </div>
																			
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
															<tr>
																<td>
																	<span style="font-weight: bold;">Save the preferences for </span>&nbsp;&nbsp;&nbsp;&nbsp;
																	<asp:DropDownList ID="ddlPOPrefrences" runat="server">
																		<asp:ListItem Text="Me" Value="ME"></asp:ListItem>
																		<asp:ListItem Text="My Department" Value="MYDEPARTMENT"></asp:ListItem>
																	</asp:DropDownList>
																</td>
															</tr>
															<tr>
																<td>
																</td>
																<td>
																	<asp:Button ID="btnPOReport" runat="server" Text="Generate Report" OnClick="btnPOReport_Click"  />
															</tr>
														</table>
													</asp:Panel>
												</td>
											</tr>
										</table>
									 
														<div style="display: none">
					<asp:Button ID="btnOpenReport" runat="server" Text="Button" />
				</div>
				  <div style="display: none">
					<asp:Button ID="Button1" runat="server" Text="Button" />
				</div>
	<%--			<ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="btnOpenReport" PopupControlID="pnlReport" 
					DropShadow="true" OkControlID="btnOk">
				</ajaxToolkit:ModalPopupExtender>
				   <asp:Panel ID="pnlReport" runat="server" >
					<div style="background-color:White;text-align:center;">
				
					</div>
					<div style="background-color:#33ADFF;text-align:center;">  
						<asp:Button ID="btnOk1" runat="server" Text="Close" OnClick="btnOk1_Click" 
							Width="171px" style="margin-top:10px; margin-bottom:10px" />
					</div>
				</asp:Panel>
--%>

									</ContentTemplate>
								</asp:UpdatePanel>
								
							</div>
						</div>
						<div id="page4" class="wizard-page">
							<div class="wizard-page-title">
								<span class="icon">
									<img src="../../images/wizard/task-settings.png" style="vertical-align: bottom" /></span>Procedure</div>
							<div class="wizard-page-content">
								<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
									<ContentTemplate>
										<asp:Panel ID="pnlTask_SendMail" runat="server" Visible="true">
											<div class="wizard-page-content-header">
												Enter Mail Settings and Build Query</div>
											<table style="width: 100%">
												<tr>
													<td style="width: 100px">
														<div id="lblTo" style="cursor: pointer;" runat="server" onclick="javascript:$('#ctl00_MainContent_txtMailTo').focus();$('#dvSelectAddress').show();">
															To ...</div>
													</td>
													<td>
														<asp:TextBox ID="txtMailTo" runat="server" Width="100%"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>
														CC
													</td>
													<td>
														<asp:TextBox ID="txtMailCC" runat="server" Width="100%"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>
														Subject
													</td>
													<td>
														<asp:TextBox ID="txtSubject" runat="server" Width="100%"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>
														Mail Header
													</td>
													<td>
														<asp:TextBox ID="txtMailHeader" runat="server" TextMode="MultiLine" Height="50px"
															Width="100%"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>
														<asp:Label ID="lblDatabaseProcedure" Text="Database Procedure" runat="server"></asp:Label>
													</td>
													<td>
														<asp:Panel ID="pnlSavedQuery" runat="server">
															<asp:DropDownList ID="ddlSavedQuery" runat="server" Width="400px" AutoPostBack="false"
																AppendDataBoundItems="false">
															</asp:DropDownList>
															<asp:ImageButton ID="ImageButton1" runat="server" OnClick="btnReloadSavedProcedures_Click"
																ImageUrl="~/Images/reload.png" ImageAlign="AbsBottom" />
																<br />
															<a href="#" class="linkImageBtn" style="width: 150px; color: Black; font-size: 13px;"
																onclick="window.open('querybuilder.aspx')">
																<img src="../../images/wizard/database-process-icon.png" style="vertical-align: bottom;
																	border: 0" height="20px" />New Procedure</a>
														</asp:Panel>
													</td>
												</tr>
												<tr>
													<td>
														Mail Footer
													</td>
													<td>
														<asp:TextBox ID="txtMailFooter" runat="server" TextMode="MultiLine" Height="50px"
															Width="100%"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>
														<span id="spnSysAlert" runat="server">System Alert</span>
													</td>
													<td>
														<asp:CheckBox ID="chkSysAlert" runat="server" />
													 
													</td>
												</tr>
												<tr>
													<td colspan="2" style="text-align: right">
														<asp:Button ID="btnSaveSchedule" runat="server" Text="Save Schedule" OnClick="btnSaveSchedule_Click"
															OnClientClick="javascript:$('#dvSelectAddress').hide();" />
														<asp:Button ID="btnSaveScheduleAndClose" runat="server" Text="Save Schedule & Close"
															OnClick="btnSaveScheduleAndClose_Click" OnClientClick="javascript:$('#dvSelectAddress').hide();" />
													</td>
												</tr>
											</table>
										</asp:Panel>
										<asp:Panel ID="pnlTask_DashboardAlert" runat="server" Visible="false">
											<div class="wizard-page-content-header">
												Set Dashboard Alert</div>
										</asp:Panel>
										<asp:Panel ID="pnlTask_SMSAlert" runat="server" Visible="false">
										</asp:Panel>
									</ContentTemplate>
								</asp:UpdatePanel>
								<div id="dvSelectAddress" class="draggable" style="display: none; background-color: White;
									position: absolute; left: 250px; top: 160px; z-index: 1000; padding: 2px; border: 1px solid #aabbdd;">
									<asp:UpdatePanel ID="UpdateAddress" runat="server">
										<ContentTemplate>
											<table style="border: 1px solid #aabbdd; padding: 2px;" cellpadding="0" cellspacing="0">
												<tr>
													<td colspan="4" align="right" style="background-color: #B5CDE1">
														<div style="font-size: 11px; font-weight: bold; cursor: pointer; padding: 2px; border: 1px solid outset;
															text-align: right; width: 16px; color: Red" onclick="javascript:$('#dvSelectAddress').hide('slow');">
															X</div>
													</td>
												</tr>
												<tr style="background-color: #aabbdd; color: Black">
													<td colspan="2">
														Select User
													</td>
												</tr>
												<tr>
													<td colspan="2">
														<asp:ListBox ID="lstUsers" runat="server" DataSourceID="ObjectDataSource_UserList"
															DataTextField="USER_NAME" DataValueField="USERID" AppendDataBoundItems="True"
															SelectionMode="Multiple" AutoPostBack="false" Height="200px" Width="99%">
															<asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
														</asp:ListBox>
														<asp:ObjectDataSource ID="ObjectDataSource_UserList" runat="server" SelectMethod="Get_UserList"
															TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
															<SelectParameters>
																<asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="UserCompanyID"
																	Type="Int32" />
															</SelectParameters>
														</asp:ObjectDataSource>
													</td>
												</tr>
												<tr>
													<td colspan="2">
														<table>
															<tr>
																<td>
																	<asp:Button ID="btnAddTo" runat="server" Text="To &gt;&gt;" OnClick="btnAddTo_Click" />
																</td>
																<td>
																	<asp:TextBox ID="txtSelectedIDsTo" Visible="false" runat="server" Width="270px" TextMode="MultiLine"></asp:TextBox>
																</td>
															</tr>
															<tr>
																<td>
																	<asp:Button ID="btnCC" runat="server" Text="CC &gt;&gt;" OnClick="btnCC_Click" />
																</td>
																<td>
																	<asp:TextBox ID="txtSelectedIDsCC" runat="server" Visible="false" Width="270px" TextMode="MultiLine"></asp:TextBox>
																</td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td style="text-align: center">
														<asp:Button ID="btnOK" runat="server" Visible="false" Text="OK" OnClientClick="javascript:$('#dvSelectAddress').hide('slow');return false;"
															Width="60px" />
														<asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClientClick="javascript:$('#dvSelectAddress').hide('slow'); return false;" />
													</td>
												</tr>
											</table>
										</ContentTemplate>
									</asp:UpdatePanel>
								</div>
							</div>
						</div>
					</div>
				</td>
			</tr>
		</table>

		<br />
	 
		  
	</div>

   
</asp:Content>
