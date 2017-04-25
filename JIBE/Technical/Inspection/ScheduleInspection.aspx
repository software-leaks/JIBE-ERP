<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ScheduleInspection.aspx.cs"
    Inherits="Technical_Worklist_ScheduleInspection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html>
<head runat="server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript" language="javascript">
        function RemoveSpecialChar(txtName) {
            txtName.value = txtName.value.replace(/[^0-9]/g, '');

        }
    </script>
</head>
<body style="background-color: White;">
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    </div>
    <div align="center">
        <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    <div id="dvInspectionScheduling" runat="server" style="width: 99%;" title="Inspection Scheduling">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel_Frequency">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel_Frequency" runat="server">
            <ContentTemplate>
                <table style="width: 100%; margin: 10px 0px 0px 10px">
                    <tr>
                        <td>
                            <table style="border: 1px solid #aabbdd;">
                                <tr>
                                    <td width="10%" align="right" valign="top">
                                        <asp:Label ID="lblFleet" runat="server" Text="Fleet :" Font-Size="14px"></asp:Label>
                                        <asp:Label ID="lblCompany" runat="server" Text="Company :" Font-Size="14px" Visible="false"></asp:Label>
                                    </td>
                                    <td width="20%" valign="top" align="left">
                                        <asp:DropDownList ID="DDLFleetP" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLFleetP_SelectedIndexChanged"
                                            AutoPostBack="true" Width="130" Style="margin-left: 0px" />
                                        <asp:DropDownList ID="DDLCompany" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLCompany_SelectedIndexChanged"
                                            AutoPostBack="true" Width="130" Style="margin-left: 0px" Visible="false" />
                                    </td>
                                    <td width="10%" align="right" valign="top" style="white-space: nowrap">
                                        <asp:Label ID="lblVessel" runat="server" Text="Vessel :" Font-Size="14px"></asp:Label>
                                        <span style="color: #FF0000">*</span>
                                    </td>
                                    <td width="20%" valign="top" align="left">
                                        <asp:DropDownList ID="DDLVesselP" runat="server" UseInHeader="false" AutoPostBack="true"
                                            OnSelectedIndexChanged="DDLVessselP_SelectedIndexChanged" Width="130" />
                                    </td>
                                    <td width="10%" align="right" valign="top" style="white-space: nowrap">
                                        <asp:Label ID="lblInspector" runat="server" Text="Inspector :" Font-Size="14px"></asp:Label><span
                                            style="color: #FF0000">*</span>
                                    </td>
                                    <td width="12%" valign="top" align="left">
                                        <asp:DropDownList ID="DDLInspectorP" runat="server" UseInHeader="false" Width="130" />
                                    </td>
                                    <td width="20%" align="right" valign="top" style="white-space: nowrap">
                                        <asp:Label ID="Label1" runat="server" Text="Inspection type :" Font-Size="14px"></asp:Label><span
                                            style="color: #FF0000">*</span>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlInspectionTypeP" runat="server" UseInHeader="false" Width="160" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="max-height: 80px; overflow-y: scroll;">
                                <table id="tblChecklist" runat="server" style="border: 1px solid #aabbdd;">
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="grvChecklist" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                AutoPostBack="True" Font-Size="12px" CellPadding="2" CellSpacing="1" RepeatLayout="Table">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%; border: 1px solid #aabbdd;">
                                <tr>
                                    <td>
                                        <table style="width: 100%" style="border: 5px solid #aabbdd;">
                                            <tr>
                                                <td style="vertical-align: top; width: 200px;">
                                                    <span style="font-weight: bold; font-size: 14px;">Frequency</span>
                                                    <br />
                                                    <br />
                                                    <asp:RadioButtonList ID="rdoFrequency" runat="server" OnSelectedIndexChanged="rdoFrequency_SelectedIndexChanged"
                                                        AutoPostBack="true" Font-Size="14px">
                                                        <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                                                        <asp:ListItem Text="Monthly" Value="Monthwise"></asp:ListItem>
                                                        <asp:ListItem Text="One time" Value="Onetime" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Repeat interval" Value="Duration"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <div style="margin-top: 2px; font-size: 14px;" id="dvRange" runat="server">
                                                        Start:
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="120px"></asp:TextBox>&nbsp;
                                                        <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtStartDate"
                                                            Format="dd/MMM/yy">
                                                        </ajaxToolkit:CalendarExtender>
                                                        End:
                                                        <asp:TextBox ID="txtEndDate" runat="server" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate"
                                                            Format="dd/MMM/yy">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </div>
                                                    <div style="margin-top: 10px; font-size: 14px;">
                                                        <asp:Panel ID="pnlOneTime" runat="server" Visible="true">
                                                            <table style="font-size: 14px;">
                                                                <tr>
                                                                    <td>
                                                                        Schedule Date : <span style="color: #FF0000">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOneTime" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                                                        <ajaxToolkit:CalendarExtender ID="calEtxtOneTime" runat="server" TargetControlID="txtOneTime"
                                                                            Format="dd/MMM/yy">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        Port:<span style="color: #FF0000">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="drpPort" Width="180px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlWeekly" runat="server" Visible="false">
                                                            Reoccur every <span style="color: #FF0000">*</span>
                                                            <asp:TextBox ID="txtWeek" runat="server" Width="40px" Text="1"></asp:TextBox>&nbsp;&nbsp;weeks
                                                            on:
                                                            <asp:CheckBoxList runat="server" ID="chkWeekDays" RepeatDirection="Horizontal" Font-Size="14px">
                                                                <asp:ListItem Text="Sunday" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Monday" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Tuesday" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Wednesday" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="Thursday" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="Friday" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="Saturday" Value="7"></asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlMonthWise" runat="server" Visible="false">
                                                            <asp:CheckBoxList runat="server" ID="chkMonthWise" RepeatDirection="Horizontal" RepeatColumns="6"
                                                                Font-Size="14px">
                                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlStartMonth" runat="server" Visible="false">
                                                            <h4>
                                                                Start of Every Month</h4>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlEndMonth" runat="server" Visible="false">
                                                            <h4>
                                                                End of Every month</h4>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlDuration" runat="server" Visible="false">
                                                            Repeat in&nbsp;&nbsp;&nbsp;
                                                            <asp:DropDownList runat="server" ID="ddlDuration" Font-Size="14px">
                                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
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
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                                                <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                                                <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                                                <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                                <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                                <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                                                <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                                                <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                                                <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                                                <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                                                <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                                                <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                                                <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                                <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                                                <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                                                <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                                <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                                                <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                                                <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                                                <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                                                <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                                <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                                                <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                                                <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                                                <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                                <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                                <asp:ListItem Text="61" Value="61"></asp:ListItem>
                                                                <asp:ListItem Text="62" Value="62"></asp:ListItem>
                                                                <asp:ListItem Text="63" Value="63"></asp:ListItem>
                                                                <asp:ListItem Text="64" Value="64"></asp:ListItem>
                                                                <asp:ListItem Text="65" Value="65"></asp:ListItem>
                                                                <asp:ListItem Text="66" Value="66"></asp:ListItem>
                                                                <asp:ListItem Text="67" Value="67"></asp:ListItem>
                                                                <asp:ListItem Text="68" Value="68"></asp:ListItem>
                                                                <asp:ListItem Text="69" Value="69"></asp:ListItem>
                                                                <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                                                <asp:ListItem Text="71" Value="71"></asp:ListItem>
                                                                <asp:ListItem Text="72" Value="72"></asp:ListItem>
                                                                <asp:ListItem Text="73" Value="73"></asp:ListItem>
                                                                <asp:ListItem Text="74" Value="74"></asp:ListItem>
                                                                <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                                <asp:ListItem Text="76" Value="76"></asp:ListItem>
                                                                <asp:ListItem Text="77" Value="77"></asp:ListItem>
                                                                <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                                <asp:ListItem Text="79" Value="79"></asp:ListItem>
                                                                <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                                                <asp:ListItem Text="81" Value="81"></asp:ListItem>
                                                                <asp:ListItem Text="82" Value="82"></asp:ListItem>
                                                                <asp:ListItem Text="83" Value="83"></asp:ListItem>
                                                                <asp:ListItem Text="84" Value="84"></asp:ListItem>
                                                                <asp:ListItem Text="85" Value="85"></asp:ListItem>
                                                                <asp:ListItem Text="86" Value="86"></asp:ListItem>
                                                                <asp:ListItem Text="87" Value="87"></asp:ListItem>
                                                                <asp:ListItem Text="88" Value="88"></asp:ListItem>
                                                                <asp:ListItem Text="89" Value="89"></asp:ListItem>
                                                                <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                                <asp:ListItem Text="91" Value="91"></asp:ListItem>
                                                                <asp:ListItem Text="92" Value="92"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            &nbsp; Days
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%; border: 1px solid #aabbdd;">
                                <tr>
                                    <td style="width: 20%; font-size: 14px;">
                                        Inspection duration (days): <span style="color: Red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDurJobs" runat="server" Style="width: 50px" onkeyup="javascript:RemoveSpecialChar(this);"
                                            MaxLength="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; font-size: 14px;">
                                        Remark for inspector:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInspRemark" runat="server" TextMode="MultiLine" Rows="6" Width="100%"
                                            Style="width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: 14px;">
                                        <asp:CheckBox ID="chkSendEmail" runat="server" Text="Send E-mail to inspector before"
                                            Checked="true" />
                                        &nbsp;
                                        <asp:DropDownList ID="ddlDaysBefore" runat="server" Width="50px">
                                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="07" Value="7" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
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
                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;days
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkImages" runat="server" Text="Show images in reports" Font-Size="14px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div style="vertical-align: bottom; text-align: right">
                    <asp:Button ID="btnSaveInspection" runat="server" Text="Save inspection" OnClick="btnSaveInspection_Click"
                        Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveInspectinAndClose" runat="server" Text="Save inspection and close"
                        Style="margin-right: 10px" OnClick="btnSaveInspectinAndClose_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
