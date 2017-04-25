<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="ERLogEngineDetailsThresHold.aspx.cs" Inherits="ERLogEngineDetailsThresHold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
	<script src="../../Scripts/boxover.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
	<script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
	<script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
	<script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
	<link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
	<script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
	<script type="text/javascript">

		function ViewEdit(Id) {
			var value = document.getElementById('<%=lblLogId.ClientID%>').value;
			window.open("ERLogEdit.aspx?ViewID=" + Id + "&LOGID=" + value, "Test", "", "");
		}
		debugger;
		function MaskMoney(evt) {
			if (!(evt.keyCode == 9 || evt.keyCode == 189 || evt.keyCode == 190 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
				return false;
			}
			var parts = evt.srcElement.value.split('.');
			if (parts.length > 2) return false;
			if (evt.keyCode == 46) return (parts.length == 1);
			if (parts[0].length >= 14) return false;


		}
		function checkNumber(id) {
			var obj = document.getElementById(id);
			if (isNaN(obj.value)) {
				obj.value = 0;
				alert("Only number allowed !");
			}
		}
	</script>
	<style type="text/css">
		.CellClass1
		{
			background-color: Red;
			color: White;
			border: 1px solid #cccccc;
			border-right: 1px solid #cccccc;
		}
		.CellClass0
		{
			border: 1px solid #cccccc;
			border-right: 1px solid #cccccc;
		}
		
		.HeaderCellColor
		{
			background-color: #3399CC;
			color: White;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
	<div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
		height: 100%;">
		<div id="page-header" class="page-title">
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td style="width: 95%; text-align: center;">
						<b>MAIN ENGINE & MAIN ENGINE TURBO BLOWERS THRESHOLD </b>
					</td>
					<td style="width: 5%; text-align: right; border-right: 2px solid Transparent">
						<asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" BackColor="#AED7FF"
							BorderStyle="None" Width="70px" Font-Size="11px" Font-Bold="false" Font-Names="verdana"
							Height="20px" ForeColor="Blue" />
					</td>
				</tr>
			</table>
			<asp:TextBox ID="lblLogId" runat="server"></asp:TextBox>
		</div>
	</div>
	<div style="text-align: left; overflow: scroll" id="dvPageContent" class="page-content-main">
		<table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
			border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
			<tr>
				<td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
					align="left" valign="top">
					<asp:FormView ID="FormView1" runat="server" Height="60px" Width="100%" BorderWidth="0px"
						Font-Size="Small" OnDataBound="FormView1_DataBound">
						<RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
						<ItemTemplate>
							<table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
								border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
								<tr>
									<td width="100%" colspan="3" valign="top">
										<asp:Repeater ID="rpEngine1" runat="server">
											<HeaderTemplate>
												<table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: #efefef;
													border-color: #efefef; width: 100%;">
													<tr align="center" style="background-color: #BCF5A9">
														<td colspan="11" align="center">
															MAIN ENGINES
														</td>
														<td colspan="14" align="center">
															MAIN ENGINE TURBO BLOWERS
														</td>
													</tr>
													<tr align="center" class="HeaderCellColor">
														<td rowspan="3">
															Threshold
														</td>
														<td rowspan="3">
															<label id="Label176" class="verticaltext">
																Hours
															</label>
														</td>
														<td rowspan="3">
															<label id="lbl1" class="verticaltext">
																minutes
															</label>
														</td>
														<td rowspan="2" colspan="2">
															Revolutions
														</td>
														<td rowspan="3">
															<label id="Label177" class="verticaltext">
																ME control</label>
														</td>
														<td rowspan="3">
															<label id="Label178" class="verticaltext">
																Gov control</label>
														</td>
														<td rowspan="3">
															<label id="Label179" class="verticaltext">
																Load indicator</label>
														</td>
														<td rowspan="3">
															<label id="Label185" class="verticaltext">
																Fuel PP INDEX</label>
														</td>
														<td rowspan="3">
															<label id="Label186" class="verticaltext">
																Fuel FLOWMETER</label>
														</td>
														<td rowspan="3">
															<label id="Label4" class="verticaltext">
																TEMPERATURE C EXHAUST
															</label>
														</td>
														<td rowspan="3">
															<label id="Label3" class="verticaltext">
																T/CHGR RPM X 1000
															</label>
														</td>
														<td colspan="9">
															TEMPERATURE C
														</td>
														<td colspan="4">
															PRESSURE
														</td>
													</tr>
													<tr align="center" class="HeaderCellColor">
														<td colspan="2">
															EXHAUST GAS
														</td>
														<td colspan="2">
															AIR COOLER AIR
														</td>
														<td rowspan="2">
															<label id="Label187" class="verticaltext1">
																Scaverage</label>
														</td>
														<td colspan="2">
															COOLING WATER
														</td>
														<td colspan="2">
															LUB OIL
														</td>
														<td rowspan="2">
															<label id="Label189" class="verticaltext1">
																Scav km/cm2</label>
														</td>
														<td rowspan="2">
															<label id="Label188" class="verticaltext1">
																EXN BACK
															</label>
														</td>
														<td colspan="2">
															Press Drop mm Wc
														</td>
													</tr>
													<tr align="center" class="HeaderCellColor">
														<td>
															COUNTER
														</td>
														<td>
															RPM
														</td>
														<td>
															INLET
														</td>
														<td>
															OUTLET
														</td>
														<td>
															IN
														</td>
														<td>
															OUT
														</td>
														<td>
															IN
														</td>
														<td>
															OUTLET
														</td>
														<td>
															B
														</td>
														<td>
															T
														</td>
														<td>
															AIR COOLER
														</td>
														<td>
															AIR FILTER
														</td>
													</tr>
											</HeaderTemplate>
											<ItemTemplate>
												<tr style="border: 0px solid ActiveBorder;">
													<td align="left" style="height: 19px; background-color: #BCF5A9">
														<asp:Label ID="Label159" Width="60px" runat="server" Text='Min'> </asp:Label>
														<asp:Label ID="lblid" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "VESSEL_ID")%>'> </asp:Label>
														<asp:Label ID="lblVessel" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "VESSEL_ID")%>'> </asp:Label>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt1" runat="server" Width="90%" CssClass="input centeralinment"
															BackColor="#efefef" MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);"
															Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_HOURS_MiN")%>'> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt2" runat="server" Width="90%" CssClass="input centeralinment"
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_MINUTES_Min")%>'> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt3" runat="server" Width="90%" CssClass="input centeralinment"
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_COUNTER_Min")%>'></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt4" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_RPM_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt5" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_CONTROL_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt6" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_GOV_CTRL_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt7" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LOAD_INDICATOR_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt8" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_PP_INDEX_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt9" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_FLOWMETER_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt10" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt11" runat="server" Width="90%" CssClass="input centeralinment"
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_MIN")%>'></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt12" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt13" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt14" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_AIR_IN_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt15" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_AIR_OUT_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt16" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_SCAVENGE_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt17" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_IN_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt18" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt19" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt20" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt21" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_SCAVENGE_KGPCMS_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt22" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_EXH_BACK_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt23" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt24" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_Min")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
												</tr>
												<tr style="height: 19px;">
													<td align="left" style="height: 19px; background-color: #F78181">
														<asp:Label ID="Label1" runat="server" Width="90%" Text='Max'> </asp:Label>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt01" runat="server" Width="90%" CssClass="input centeralinment"
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_HOURS_Max")%>'> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt02" runat="server" Width="90%" CssClass="input centeralinment"
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "WATCH_MINUTES_Max")%>'> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt03" runat="server" Width="90%" CssClass="input centeralinment"
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "ME_COUNTER_max")%>'></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt04" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_RPM_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt05" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_CONTROL_max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt06" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_GOV_CTRL_max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt07" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_LOAD_INDICATOR_max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt08" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_PP_INDEX_max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt09" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_FUEL_FLOWMETER_max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"></asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt010" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ME_EXH_TEMP_MAX")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt011" runat="server" Width="90%" CssClass="input centeralinment"
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "TC_RPM_Max")%>'></asp:TextBox>
														<asp:Label ID="lblVessel1" runat="server" Width="90%" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "VESSEL_ID")%>'> </asp:Label>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt012" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_IN_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt013" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_OUT_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt014" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_AIR_IN_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt015" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_EXH_AIR_OUT_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt016" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_SCAVENGE_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt017" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_IN_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt018" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_CW_OUT_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt019" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_B_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt020" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "T_LO_T_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt021" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_SCAVENGE_KGPCMS_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt022" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_EXH_BACK_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt023" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AC_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
													<td align="left" style="height: 19px;">
														<asp:TextBox ID="txt024" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "P_PD_AF_Max")%>'
															MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
													</td>
												</tr>
											</ItemTemplate>
											<FooterTemplate>
												</table>
											</FooterTemplate>
										</asp:Repeater>
									</td>
								</tr>
							</table>
						</ItemTemplate>
					</asp:FormView>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
