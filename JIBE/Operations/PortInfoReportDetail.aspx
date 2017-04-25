<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortInfoReportDetail.aspx.cs" Inherits="Operations_PortInfoReportDetail" MasterPageFile="~/Site.master" Title="Port Information Report"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"  TagPrefix="uc1" %>   
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList" TagPrefix="ucDDL" %>
	
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
	<link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
	<link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
	<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
	<script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
	<script src="../Scripts/boxover.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
	<script src="../Scripts/CrewDetails_Common.js" type="text/javascript"></script>
	<script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
	<script src="../Scripts/CrewDetails_DataHandler.js" type="text/javascript"></script>
	<script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
	<script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
	<link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
	<%--<script language='javascript' type='text/javascript'>
	function toggleAdvSearch(obj) {
		if ($(obj).text() == "Send Report To Vessel") {
				$(obj).text("Close");
				$("#dvAdvanceFilter").show();
			}
			else {
				$(obj).text("Send Report To Vessel");
				$("#dvAdvanceFilter").hide();
			}
		}
	</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
		<ProgressTemplate>
			<div id="blur-on-updateprogress">
				&nbsp;</div>
			<div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
				color: black">
				<img src="../Images/loaderbar.gif" alt="Please Wait" />
			</div>
		</ProgressTemplate>
	</asp:UpdateProgress>
	<asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<div class="page-title">
				<asp:Label ID="lblPageTitle" runat="server" Text="Port Information Report"></asp:Label>
			</div>
			<div id="page-content" style="min-height: 640px; color: #333333; border: 1px solid gray;
				width: 100%">
				<table width="100%" cellpadding="0" cellspacing="0">
					<%--<tr>
						<td align="right" style="padding-right: 17px">
							<uc1:ctlRecordNavigation ID="ctlRecordNavigationReport" OnNavigateRow="BindReport"
								runat="server" />
						</td>
					</tr>--%>
				  <%--  <tr>
						<td>
							<div style="text-align: right; height: 20px;">
								<a id="advText" href="#" onclick="toggleAdvSearch(this)">Send Report To Vessel</a>
							</div>
							<div id="dvAdvanceFilter" style="background-color: #efefef;" class="hide">
								 <asp:UpdatePanel ID="UpdatePanel_Vessel" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
							   <table width="50%" >
									<tr align="left">
										 <td>
											Fleet
										</td>
										<td>
											<asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
												AutoPostBack="true">
											</asp:DropDownList>
										</td>
										<td>
											Vessel 
										</td>
										<td style="height: 30px">
											 <ucDDL:ucCustomDropDownList ID="ddlVessel" runat="server" UseInHeader="false" Height="200" Width="160" />
										</td>
										<td>
											<asp:Button ID="btnSendToVessel" runat="server" Text="Send to Vessel"  OnClick="btnSendToVessel_Click" />
										</td>
									  </tr>
									</table>
								</ContentTemplate> 
								</asp:UpdatePanel>
							</div>
						</td>
					</tr>--%>
					<tr>
						<td>
						<style type="text/css">
									.leafTR
									{
										border-bottom-style: solid;
										border-bottom-color: White;
										border-bottom-width: 1px;
									}
									.leafTD-header
									{
										width: 150px;
										height: 20px;
										padding: 0px 0px 0px 10px;
										text-align: left;
									}
									.leafTD-data
									{
										width: 140px;
										height: 20px;
										padding: 0px 0px 0px 0px;
										background-color: #cce499;
										text-align: left;
									}
									#pageTitle
									{
										background-color: gray;
										color: White;
										font-size: 12px;
										text-align: center;
										padding: 2px;
										font-weight: bold;
									}
								</style>
						<asp:FormView ID="fvPortInfoReport" FooterStyle-ForeColor="Black" runat="server" Width="100%">
							<ItemTemplate >
								<table>
									<tr>
										<td class='leafTD-header'>Report Date </td>  <td class='leafTD-data'><%#Eval("REPORT_DATE")%></td>
										<td class='leafTD-header'>Arrival Date </td>  <td class='leafTD-data'><%#Eval("ARRIVAL_DATE")%></td>
										<td class='leafTD-header'>Departure Date </td>  <td class='leafTD-data'><%#Eval("DEPARTURE_DATE")%></td>
									</tr>
									<tr>
										<td class='leafTD-header'>Vessel Name</td>  <td class='leafTD-data'><%#Eval("VESSELNAME")%></td>
										<td class='leafTD-header'>Voyage No. </td>  <td class='leafTD-data'><%#Eval("VOYAGE")%></td>
										<td class='leafTD-header'>Port Name</td>  <td class='leafTD-data'><%#Eval("PORT_NAME")%></td>
									</tr>
									<tr>
										<td class='leafTD-header'>Latitude </td>  <td class='leafTD-data'><%#Eval("LONGITUDE")%></td>
										<td class='leafTD-header'>Longitude </td>  <td class='leafTD-data'><%#Eval("LATITUDE")%></td>
										<td class='leafTD-header'>Country </td>  <td class='leafTD-data'><%#Eval("COUNTRY_NAME")%></td>
									</tr>
								</table>
							</ItemTemplate>
						</asp:FormView>
						</td>
					</tr>
					<%--<tr>
						<td colspan="2">
							<div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
								background-color: #FDFDFD">
							</div>
						</td>
					</tr>--%>
					
					 <tr>
						<td colspan="2">
						<div id="tabs" style="margin-top: 5px; min-height: 900px;" class="ui-tabs-hide">
							<ul>
								<li><a href="#fragment-0"><span>Port Information</span></a></li>
							 <%--   <li><a href="#fragment-1"><span>Terminal Information</span></a></li>--%>
								  <li><a href="#fragment-2"><span>Mooring Plan</span></a></li>
			   <li><a href="#fragment-3"><span>Pre Arrival Briefing</span></a></li>
							</ul>
							<div id="fragment-0" style="padding: 2px; display: block">
								 <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										 <iframe id="iFrame2" src="PortInfo.aspx?VesselId=<%=GetVesselId()%>&PortInfoReportId=<%=GetPortInfoReportId()%>" style="width: 100%;
											height: 900px; border: 0px;"></iframe>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
						   <%-- <div id="fragment-1" style="padding: 2px; display: block;">
								<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										 <iframe id="iFrame1" src="TerminalInfo.aspx?VesselId=<%=GetVesselId()%>&PortInfoReportId=<%=GetPortInfoReportId()%>" style="width: 100%;
											height: 900px; border: 0px;"></iframe>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>--%>
							 <div id="fragment-2" style="padding: 2px; display: block;">
								<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										 <iframe id="iFrame3" src="MooringPlan.aspx?VesselId=<%=GetVesselId()%>&PortInfoReportId=<%=GetPortInfoReportId()%>&OfficeId=<%=GetOfficeId()%>" style="width: 100%;
											height: 900px; border: 0px;"></iframe>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
							 <div id="fragment-3" style="padding: 2px; display: block;">
								<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										 <iframe id="iFrame1" src="PreArrivalDetails.aspx?VesselId=<%=GetVesselId()%>&PortInfoReportId=<%=GetPortInfoReportId()%>&OfficeId=<%=GetOfficeId()%>" style="width: 100%;
											height: 900px; border: 0px;"></iframe>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>

						</div>
					   </td>
					</tr>
				</table>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
