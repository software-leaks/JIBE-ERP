<%@ Page Title="Add New / Edit Worklist Job" Language="C#" MasterPageFile="~/Site.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="AddNewJob.aspx.cs"
    Inherits="Technical_Job_List_AddNewJob" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <%-- <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/CustomAsyncDropDown.js" type="text/javascript"></script>
    <script language='javascript' type='text/javascript'>

        $(document).ready(function () {
            var OfficeID = $('[id$=txtOfficeID]').val();
            if (OfficeID == "")
                $("#tabs").hide();
            else {
                $("#tabs").tabs();

                var ncr = $('#<%=ddlWLType.ClientID %> option:selected').val();
                if (ncr != "NCR") {
                    $("#tabs").tabs({ disabled: [1, 2, 3] });
                }
            }

            $(".draggable").draggable();
            var WLID = '<%=Request.QueryString["WLID"]%>';
            if (WLID > 0) {
                var wh = 'OFFICE_ID=<%=Request.QueryString["OFFID"]%>  and ID=<%=Request.QueryString["WLID"]%> and VESSEL_ID=<%=Request.QueryString["VID"]%>'
                Get_Record_Information_Details('TEC_DTL_JOBS_HISTORY', wh);
            }

        });

        function toggleTabs(ncr) {
            if (ncr == "0") {
                $("#tabs").tabs('select', 0);
                $("#tabs").tabs({ disabled: [1, 2, 3] });
            }
            else {
                $("#tabs").tabs('select', 0);
                $("#tabs").tabs().tabs("option", "disabled", false);
            }
        }

        function divCategorylink() {
            document.getElementById("divCategory").style.display = "block";
        }

        function OpenCategoryDiv() {
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'true';

            showModal('divCategory');
        }

        function CloseMe() {
            var divCategory = document.getElementById("divCategory");
            divCategory.style.display = 'none';
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'false';
        }

        function SetAndClose() {
            var divCategory = document.getElementById("divCategory");
            divCategory.style.display = 'none';
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'false';
        }

        function OpenFollowupDiv() {
            $("#<%=txtMessage.ClientID%>").val(''); /// Added by Anjali DT:17-May-2016 JIT:9604 || To clear fields after adding follow up.
            document.getElementById("dvAddFollowUp").style.display = "block";
        }
        function CloseFollowupDiv() {
            var dvAddFollowUp = document.getElementById("dvAddFollowUp");
            dvAddFollowUp.style.display = 'none';

        }
        function OpenObservationDiv() {
            var divCategory = document.getElementById("divObservation");
            divObservation.title = 'Observation';

            // var rbtn = $(".GvObservationList_radio[rel='" + $("#<%=hdfSelectObsID.ClientID %>").val() + "']")[0].childNodes[0].attr("checked", true);

            showModal('divObservation');

        }

        function CheckRaisedOn(sender, args) {
            var today = new Date();
            var selectedDate = new Date();
            selectedDate = sender.get_selectedDate();
            today.setHours(0, 0, 0, 0);
            if (selectedDate > today) {
                alert("Raisedon date can not be greater than current date.");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

        function CheckExpectedCom(sender, args) {

            var today = new Date();
            var selectedDate = sender.get_selectedDate();
            today.setHours(0, 0, 0, 0);

            if (selectedDate < today) {
                alert("Expected completion date can not be less than current date.");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

        function saveClose() {
            window.opener.saveCloseChild();

        }
    </script>
    <style type="text/css">
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            height: 21px;
            font-family: Tahoma;
            font-size: 11px;
        }
        input
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        textarea
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        .link
        {
            text-decoration: none;
            text-transform: capitalize;
        }
        .roundedBox
        {
            border-radius: 5px;
            border: 2px solid white;
            background-color: #DBDFD5;
            text-align: center;
            font-size: 14px;
            color: #555;
            margin: 2px;
            padding: 2px;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .data
        {
            border: 1px solid #efefef;
            background-color: #F5F6CE;
        }
        .row-header
        {
            background-color: #aabbdd;
            font-weight: bold;
        }
        
        /*AJAX.NET TAB CONTROL */
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="divProgress" style="background-color: #FFFFFF; border-color: #FFFFFF; border-style: none;
                        border-width: 0px; height: 32px; width: 32px; position: absolute; left: 49%;
                        top: 30%; z-index: 2; color: black">
                        <img src="../../images/updateProgress.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="page-title">
                Add New / Edit Worklist Job
            </div>
            <div class="roundedBox">
                <table border="0" cellpadding="5" cellspacing="0" style="width: 100%; color: black;"
                    class="printable">
                    <tr>
                        <td align="left" valign="top" style="font-size: 14px">
                            <table>
                                <tr>
                                    <td style="width: 40px">
                                        Fleet:
                                    </td>
                                    <td style="width: 150px">
                                        <asp:DropDownList ID="ddlFleet" runat="server" Width="100px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 65px">
                                        Vessel<span style="color: red"> * </span>:
                                    </td>
                                    <td style="width: 150px">
                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 70px">
                                        Job Code:
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox ID="txtJobCode" CssClass="" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 100px">
                                        NCR Number:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNCRNo" CssClass="" runat="server" Width="70px" Enabled="false"></asp:TextBox>
                                        /
                                        <asp:TextBox ID="txtNCRNo_Year" CssClass="" runat="server" Width="35px" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <div>
                                <input type="hidden" id="hdnJobID" runat="server" value="" />
                                <input type="hidden" id="hdnVesselID" runat="server" value="" />
                            </div>
                        </td>
                        <td style="width: 50px;">
                            <img src="../../Images/printer.png" alt="Print" title="*Print*" onclick="javascript:window.print()"
                                style="cursor: hand" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvJobDetails" style="background-color: White">
                <table border="0" cellpadding="2" cellspacing="5" style="width: 100%; color: black;"
                    class="printable">
                    <tr>
                        <td colspan="2" align="left" style="border: 1px solid #aabbdd; background-color: #efffef;
                            padding: 2px; vertical-align: top;">
                            <table style="width: 100%">
                                <tr>
                                    <td align="left" class="row-header">
                                        Describe the issue or problem here<span style="color: red"> * </span>:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 14px">
                                        <asp:TextBox ID="txtDescribe" TextMode="MultiLine" Height="338px" Width="99.5%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; padding: 2px;">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr>
                                    <td align="left" class="row-header" colspan="2">
                                        Other Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        PIC:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlPIC" runat="server" Style="width: 155px">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <%-- <td width="150px">
                                        NCR ?:
                                    </td>--%>
                                    <td width="150px">
                                        Job Type :
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlWLType" runat="server" Style="width: 155px" OnSelectedIndexChanged="rdoNCR_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Assigned By :
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlAssignedBy" runat="server" Width="155px" OnSelectedIndexChanged="ddlAssignedBy_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Requisition Number :
                                    </td>
                                    <td class="data">
                                        <auc:CustomAsyncDropDownList ID="uc_ReqRef" runat="server" Web_Method_URL="../../JibeWebService.asmx/asyncGet_Reqsn_List"
                                            Width="160" Height="250" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Defer to Dry Dock:
                                    </td>
                                    <td class="data">
                                        <asp:RadioButtonList ID="rdoDeferToDrydock" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Priority:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlPriority" runat="server" Width="155px">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Inspector:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlInspector" runat="server" Style="width: 155px">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Inspection Date:
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txtInspectionDate" runat="server" Width="150px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtInspectionDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                            <%--Added by anjali Dt:17-02-2016/--%>
                            <table border="0" cellpadding="2" cellspacing="1" width="100%" id="tblfunction" runat="server">
                                <tr class="row-header">
                                    <td colspan="3">
                                        Location:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px" align="left">
                                        Function :
                                    </td>
                                    <%--<td style="color: #FF0000; width: 1%" align="left">
                                        *
                                    </td>--%>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlFunction" Width="200px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        System / Location :
                                    </td>
                                    <%-- <td style="color: #FF0000; width: 1%" align="left">
                                        *
                                    </td>--%>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlSysLocation" Width="200px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="ddlSysLocation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Sub System / Location :
                                    </td>
                                    <%--<td style="color: #FF0000; width: 1%" align="left">
                                        *
                                    </td>--%>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlSubSystem_location" Width="200px" CssClass="txtInput" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <%--Added by anjali Dt:17-02-2016/--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border: 1px solid #aabbdd; width: 33%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="2">
                                        Applicable Dates:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Raised On <span style="color: red">* </span>:
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txtRaisedOn" runat="server" Width="100px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calRaisedOn" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            OnClientDateSelectionChanged="CheckRaisedOn" TargetControlID="txtRaisedOn">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Expected Completion <span style="color: red">* </span>:
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txtExpectedComp" runat="server" Width="100px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calExpectedComp" runat="server" Enabled="True"
                                            OnClientDateSelectionChanged="CheckExpectedCom" Format="dd/MM/yyyy" TargetControlID="txtExpectedComp">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        <asp:Label ID="lblCompletedOnCaption" Text="Completed On:" runat="server" />
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txtCompletedOn" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calCompletedOn" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtCompletedOn">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        <asp:Label ID="lblCompletedByCaption" Text="Completed By :" runat="server" />
                                    </td>
                                    <td class="data">
                                        <asp:HyperLink ID="lknCompletedBy" runat="server" ForeColor="Blue" Width="80%" CssClass="link"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="hplRework" ForeColor="Blue" Style="cursor: pointer" runat="server"
                                            Text="Rework" OnClick="hplRework_Click" />
                                        <asp:Button ID="hplCloseThisJob" ForeColor="Blue" Style="cursor: pointer" runat="server"
                                            Text="Verify and close" OnClick="hplCloseThisJob_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; width: 34%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="2">
                                        Assigned Departments:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Department On Ship <span style="color: red">* </span>:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlOnShip" runat="server" Width="105px">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Department in Office <span style="color: red">* </span>:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlInOffice" runat="server" Width="105px">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="2">
                                        PSC/SIRE:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        PSC/SIRE Code :
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlPSCSIRE" runat="server" Width="155px">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; width: 33%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td>
                                        Categories:
                                    </td>
                                    <td>
                                        <div onclick="OpenCategoryDiv()" style="text-align: right; color: blue; cursor: hand;">
                                            Search Categories
                                            <img src="../../Images/Search.png" onclick="OpenCategoryDiv()" style="vertical-align: middle" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        Nature<span style="color: red"> * </span>:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlNature" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNature_SelectedIndexChanged"
                                            Width="155px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Primary<span style="color: red"> * </span>:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlPrimary" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPrimary_SelectedIndexChanged"
                                            Width="155px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Secondary<span style="color: red"> * </span>:
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlSecondary" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSecondary_SelectedIndexChanged"
                                            Width="155px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Minor Category :
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlMinorCat" runat="server" Width="155px">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left" style="border: 1px solid #aabbdd; padding: 2px; width: 100%">
                            <asp:Panel ID="pnlRootCause" runat="server" Visible="true">
                                <table>
                                    <tr>
                                        <td>
                                            Root Cause Analysis, Corrective Actions and Preventive Measures verified by :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkVerifiedBy" runat="server" ForeColor="Blue" Width="500px" CssClass="link"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="pnlVetting" runat="server" Visible="false">
                <asp:UpdatePanel ID="updateVetting" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="5" style="width: 100%; color: black;"
                            class="printable">
                            <tr>
                                <td align="left" style="border: 1px solid #aabbdd; width: 17%" valign="top">
                                    <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                        <tr class="row-header">
                                            <td colspan="2">
                                                Vetting
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="150px">
                                                Vetting :
                                            </td>
                                            <td class="data">
                                                <asp:DropDownList ID="ddlVetting" runat="server" Width="155px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlVetting_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="150px">
                                                Linked Observation :
                                            </td>
                                            <td class="data">
                                                <asp:LinkButton ID="lnkObservationLink" Text="Click to add" runat="server" OnClientClick="OpenObservationDiv();"
                                                    Style="text-align: left; color: blue; cursor: hand;" Visible="false"></asp:LinkButton>
                                                <asp:Label ID="lblObservation" runat="server" Text="" Visible="false" ForeColor="blue"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="150px">
                                            </td>
                                            <td class="data">
                                                <asp:Label ID="lblSelectObs" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdfSelectObsID" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="150px">
                                                <asp:ImageButton ID="btnSaveVetting" runat="server" ImageUrl="~/Images/VetAddNew.png"
                                                    OnClick="btnSaveVetting_Click" Style="cursor: pointer; border-width: 0px; height: 16px;
                                                    color: black;" />
                                                <asp:HiddenField ID="hdfWorklistID" runat="server" />
                                                <asp:HiddenField ID="hdfVesselID" runat="server" />
                                                <asp:HiddenField ID="hdfOfficeID" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" style="border: 1px solid #aabbdd; width: 34%" valign="top" colspan="3">
                                    <asp:Panel ID="pnlVettingObsJobs" runat="server">
                                        <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                            <tr class="row-header">
                                                <td colspan="2">
                                                    Vetting Observations and Jobs :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="page-content" style="font-family: Tahoma; font-size: 12px">
                                                        <ajaxToolkit:TabContainer ID="tbCntr" runat="server" Width="100%" ActiveTabIndex="0">
                                                            <ajaxToolkit:TabPanel ID="tbObservation" runat="server" Font-Names="Tahoma">
                                                                <HeaderTemplate>
                                                                    Observation</HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlObeservation" runat="server">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    Observation :
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 60px;">
                                                                                    <asp:TextBox ID="txtObersvation" runat="server" Width="100%" Height="60px" ReadOnly="true"
                                                                                        Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Responses :
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <div style="overflow-y: scroll; height: 200px;">
                                                                                        <asp:GridView ID="GvObservation" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                                            AutoGenerateColumns="false" CellPadding="2" ShowHeaderWhenEmpty="true" Width="100%"
                                                                                            CssClass="gridmain-css">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="20%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Created By" HeaderStyle-Width="20%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Response" HeaderStyle-Width="60%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblResponse" runat="server" Text='<%#Eval("Response") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                                                                            <RowStyle CssClass="RowStyle-css" />
                                                                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>                                                               
                                                            </ajaxToolkit:TabPanel>
                                                            <ajaxToolkit:TabPanel ID="tbObservationJob" runat="server" Font-Names="Tahoma">
                                                                <HeaderTemplate>
                                                                    Obs. Jobs</HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlObservationJobs" runat="server">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    Jobs related to the observation :&#160;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <div style="overflow-y: scroll; height: 200px;">
                                                                                        <asp:GridView ID="GvObservationJobs" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                                            OnRowDataBound="GvObservationJobs_RowDataBound" AutoGenerateColumns="false" CellPadding="2"
                                                                                            ShowHeaderWhenEmpty="true" Width="100%" CssClass="gridmain-css">
                                                                                            <Columns>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Code">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="Left">
                                                                                                    <ItemTemplate>
                                                                                                        <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                                                                            target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Expected Completion">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("Expected_completion") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Completed">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblCompletedOn" runat="server" Text='<%# Eval("Completed_on") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Status">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                                                                            <RowStyle CssClass="RowStyle-css" />
                                                                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                            <ajaxToolkit:TabPanel ID="tbVettingdJobs" runat="server" Font-Names="Tahoma">
                                                                <HeaderTemplate>
                                                                    Vetting Jobs</HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlVettingJobs" runat="server">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    Jobs related to the Vetting :&#160;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <div style="overflow-y: scroll; height: 200px;">
                                                                                        <asp:GridView ID="GvVettingJobs" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                                            AutoGenerateColumns="false" CellPadding="2" ShowHeaderWhenEmpty="true" Width="100%"
                                                                                            OnRowDataBound="GvVettingJobs_RowDataBound" CssClass="gridmain-css">
                                                                                            <Columns>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Code">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="Left">
                                                                                                    <ItemTemplate>
                                                                                                        <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                                                                            target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                            <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Expected Completion">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("Expected_completion") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Completed">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("Completed_on") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                                    HeaderText="Status">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                                                                            <RowStyle CssClass="RowStyle-css" />
                                                                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                        </ajaxToolkit:TabContainer>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Panel ID="pnlCrewComplaint" runat="server" Visible="true">
                <table style="width: 100%;">
                    <tr>
                        <td style="vertical-align: top; border: 1px solid #aabbdd; width: 75%;">
                            <asp:Repeater ID="rptComplaintsToDPA" runat="server">
                                <HeaderTemplate>
                                    <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                        cellspacing="0">
                                        <tr style="background-color: #627AA8; color: white; font-weight: bold; text-align: center;">
                                            <td>
                                                <asp:Label ID="lbl1" runat="server" Text="Escalated On"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lbl2" runat="server" Text="Escalated By"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lbl3" runat="server" Text="Escalated To"></asp:Label>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="text-align: center;">
                                        <td>
                                            <%# Eval("Escalated_On")%>
                                        </td>
                                        <td>
                                            <a href="../../Crew/CrewDetails.aspx?ID=<%# Eval("Escalated_By") %>" target="_blank">
                                                <%# Eval("Escalated_By_Staff_Code")%></a>
                                        </td>
                                        <td>
                                            <%# Eval("Escalated_By_Rank")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("Escalated_by_Name")%>
                                        </td>
                                        <td>
                                            <a target="_blank" href="../../Crew/CrewDetails.aspx?ID=<%# Eval("Escalated_To") %>">
                                                <%# Eval("Escalated_To_Staff_Code")%></a>
                                        </td>
                                        <td>
                                            <%# Eval("Escalated_To_Rank")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("Escalated_To_Name")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="vertical-align: top; border: 1px solid #aabbdd;">
                            <asp:Panel ID="pnlReleaseToFlag" runat="server" Visible="true">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr style="background-color: #627AA8; color: white; font-weight: bold;">
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Release to Flag"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DPA Remarks:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDPARemark" runat="server" TextMode="MultiLine" Height="80px"
                                                Width="350px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Button ID="btnReleaseToFlag" runat="server" Text="Release to flag" OnClick="btnReleaseToFlag_Click">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlNCR" runat="server" Visible="false">
                <table style="width: 100%">
                    <tr>
                        <td align="left" style="background-color: #aabbdd; font-weight: bold;">
                            Root Cause Analysis :
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="border: 1px solid #aabbdd; background-color: #efffef; padding: 2px;">
                            <asp:TextBox ID="txtCausesNew" runat="server" TextMode="MultiLine" Width="900px"
                                MaxLength="4000" Height="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="background-color: #aabbdd; font-weight: bold;">
                            Corrective Action :
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="border: 1px solid #aabbdd; background-color: #efffef; padding: 2px;">
                            <asp:TextBox ID="txtCorrectiveActionNew" runat="server" TextMode="MultiLine" Width="900px"
                                MaxLength="4000" Height="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="background-color: #aabbdd; font-weight: bold;">
                            Preventive Action :
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="border: 1px solid #aabbdd; background-color: #efffef; padding: 2px;">
                            <asp:TextBox ID="txtPreventiveActionNew" runat="server" TextMode="MultiLine" Width="900px"
                                MaxLength="4000" Height="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div id="dvButtons" style="text-align: center">
                <asp:Button ID="btnSaveJob" OnClick="btnSaveJob_OnClick" runat="server" Text=" Save "
                    Height="21px" />
                <asp:Button ID="btnUpdateJob" OnClick="btnUpdateJob_OnClick" runat="server" Text="Update" />
                <input type="button" id="btnCancel" value="Close" onclick="javascript:window.close();" />
            </div>
            <div>
                <span style="color: red">*</span> Mandatory Fields</div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="tabs" style="margin-top: 0px; font-size: 10px; display: block; margin: 2px;">
        <ul>
            <li><a href="#fragment-0"><span>Followups</span></a></li>
            <li><a href="#fragment-1"><span>
                <asp:Label ID="lblCauses" runat="server" Text="Root Cause Analysis"></asp:Label></span></a></li>
            <li><a href="#fragment-2"><span>
                <asp:Label ID="lblCorrectiveAction" runat="server" Text="Corrective Action"></asp:Label></span></a></li>
            <li><a href="#fragment-3"><span>
                <asp:Label ID="lblPreventiveAction" runat="server" Text="Preventive Action"></asp:Label></span></a></li>
        </ul>
        <div id="fragment-0" style="padding: 0px; display: block">
            <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; margin-top: 1px;
                padding: 2px;">
                <div id="dvFollowUps" style="background-color: #ffffff; padding: 2px; overflow: auto;">
                    <div style="text-align: right">
                        <asp:ImageButton ID="ImgBtnAddFollowup" runat="server" ImageUrl="~/Images/AddFollowup.png"
                            OnClientClick="OpenFollowupDiv();return false;" />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel_Followups" runat="server">
                        <ContentTemplate>
                            <div style="max-height: 250px; overflow: auto">
                                <asp:GridView ID="grdFollowUps" runat="server" BackColor="White" BorderColor="#999999"
                                    AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    EnableModelValidation="True" GridLines="Vertical" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" SortExpression="DATE_CREATED">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDATE_CREATED" runat="server" Text='<%#Eval("DATE_CREATED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_CREATED","{0:d/MM/yy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created By" SortExpression="LOGIN_NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLOGIN_NAME" runat="server" Text='<%#Eval("First_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Followup" SortExpression="FOLLOWUP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFOLLOWUP" runat="server" Text='<%# Eval("FOLLOWUP")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="ldl1" runat="server" Text="No followups found !!"></asp:Label>
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="fragment-1" style="padding: 0px; display: block">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; margin-top: 1px;
                        padding: 2px;">
                        <div id="Div1" style="background-color: #ffffff; padding: 2px; overflow: auto;">
                            <table>
                                <tr>
                                    <td style="color: blue">
                                        List the causes / possible causes below:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCauses" runat="server" TextMode="MultiLine" Width="800px" MaxLength="4000"
                                            Height="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="btnSaveCauses" runat="server" Text=" Save " OnClick="btnSaveCauses_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="fragment-2" style="padding: 0px; display: block">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; margin-top: 1px;
                        padding: 2px;">
                        <div id="Div3" style="background-color: #ffffff; padding: 2px; overflow: auto;">
                            <table>
                                <tr>
                                    <td style="color: blue">
                                        List all the corrective actions taken below:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCorrectiveAction" runat="server" TextMode="MultiLine" MaxLength="4000"
                                            Width="800px" Height="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="btnCorrectiveAction" runat="server" Text=" Save " OnClick="btnCorrectiveAction_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="fragment-3" style="padding: 0px; display: block">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; margin-top: 1px;
                        padding: 2px;">
                        <div id="Div4" style="background-color: #ffffff; padding: 2px; overflow: auto;">
                            <table>
                                <tr>
                                    <td style="color: blue">
                                        Preventive actions taken to avoid recurrence:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPreventiveAction" runat="server" TextMode="MultiLine" MaxLength="4000"
                                            Width="800px" Height="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <%--<asp:Button ID="btnPreventiveAction" runat="server" Text=" Save " OnClick="btnPreventiveAction_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--<asp:Panel ID="pnlCreatedByInfo" runat="server" Visible="false">
        <div style="margin: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            background: url(../../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
            background-color: #F6CEE3; font-family: Tahoma; font-size: 11px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 40px">
                        <asp:Image ID="imgCreatedBy" runat="server" Height="30px" />
                    </td>
                    <td style="width: 400px; text-transform: capitalize">
                        <asp:HyperLink ID="lnkCreatedBy" runat="server" ForeColor="Blue" CssClass="link"></asp:HyperLink>
                    </td>
                    <td style="width: 40px">
                        <asp:Image ID="imgModifiedBy" runat="server" Height="30px" />
                    </td>
                    <td style="text-transform: capitalize">
                        <asp:HyperLink ID="lnkModifiedBy" runat="server" ForeColor="Blue" CssClass="link"></asp:HyperLink>
                    </td>
                    <td style="text-align: right">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>--%>
    <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
        background-color: #f9f9f9">
    </div>
    <div id="divCategory" title="Category Selection" style="display: none; height: 400px;
        width: 700px; color: black; padding: 5px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <input type="hidden" runat="server" id="hdnFlagCheck" value="false" />
                <input type="hidden" runat="server" id="hdnNature" value="0" />
                <input type="hidden" runat="server" id="hdnPrimary" value="0" />
                <input type="hidden" runat="server" id="hdnSecondary" value="0" />
                <input type="hidden" runat="server" id="hdnMinor" value="0" />
                <table width="100%">
                    <tr>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtNature" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtPrimary" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtSecondary" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtMinor" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Nature:
                        </td>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Primary:
                        </td>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Secondary:
                        </td>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Minor:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbNature" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbNature_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbPrimary" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbPrimary_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbSecondary" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbSecondary_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbMinor" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbMinor_SelectedIndexChanged"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSelectAndClose" runat="server" Text="Select And Close" OnClick="btnSelectAndClose_OnClick"
                                OnClientClick="hideModal('divCategory');" BorderStyle="Solid" BorderColor="#0489B1"
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="120px" />
                            <asp:Button ID="btnCancelSelection" runat="server" Text="Cancel" OnClick="btnSelectAndClose_OnClick"
                                OnClientClick="hideModal('divCategory'); return false;" BorderStyle="Solid" BorderColor="#0489B1"
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="60px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvAddFollowUp" class="draggable" style="display: none; background-color: #E0E0E0;
        border: 1px solid gray; width: 500px; position: absolute; left: 33%; top: 130px;
        z-index: 2; color: black">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="txtOfficeID" runat="server" />
                <asp:HiddenField ID="hdnWorklistID" runat="server" />
                <asp:HiddenField ID="hdnWLOfficeID" runat="server" />
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3" style="text-align: center; font-weight: bold; border-style: solid;
                            border-color: Silver; background-color: Gray; padding: 2px;">
                            New Followup
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFollowupDate" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveAndClose" Text="Save and close" runat="server" OnClick="btnSaveAndClose_OnClick" />
                            <input type="button" id="Button1" value="Close" onclick="CloseFollowupDiv()" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #aabbee; padding: 2px; text-align: center; width: 100px">
                            Message:&nbsp;&nbsp;
                        </td>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="border: 1px solid inset; background-color: #aabbee; padding: 5px;
                            width: 100px">
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="200px" Width="480px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvReworkClose" style="display: none; height: 200px; width: 400">
        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="5" cellspacing="0" border="0">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblWorklistTitle" runat="server" Font-Size="11" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Remark :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtWorklistStatusRemark" runat="server" TextMode="MultiLine" MaxLength="8000"
                                Height="60px" Width="300px"> </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfRemark" runat="server" ControlToValidate="txtWorklistStatusRemark"
                                ErrorMessage="Please enter remark !" ValidationGroup="worklistgrp"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" cellpadding="5" cellspacing="0" border="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSaveStatus" OnClick="btnSaveStatus_Click" Height="30px" Width="100px"
                        ValidationGroup="worklistgrp" Text="Save" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divObservation" title="Observation" style="display: none; height: 250px;
        width: 400px; color: black; padding: 5px;">
        <asp:UpdatePanel ID="UpdObservation" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <div style="overflow-y: scroll; height: 220px;">
                                <asp:GridView ID="GvObservationList" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                    DataKeyNames="Observation_ID" EnableModelValidation="True" AllowSorting="false"
                                    Width="100%" GridLines="None" AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdbSelectObs" rel='<%# Eval("Observation_ID") %>' CssClass="GvObservationList_radio"
                                                    runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observation Description">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("Description") %>' rel='<%# Eval("Observation_ID") %>'
                                                    ID="Description" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <label id="Label1" runat="server">
                                            No jobs found !!</label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" Font-Bold="true" Font-Size="14px"
                                        Font-Names="Calibri" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" Font-Size="14px" Font-Names="Calibri" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                    <EmptyDataRowStyle CssClass="emptyTemplateShtyle" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $(document).ready(
        function () {
            $("body").on("click", ".GvObservationList_radio input[type='radio']", function () {
                var CurrentRadioId = this.id;
                var ID = CurrentRadioId.replace("ctl00_MainContent_GvObservationList_", "").replace("_rdbSelectObs", "");
                var Rel = $("#ctl00_MainContent_GvObservationList_" + ID + "_Description").attr("rel");
                $(".GvObservationList_radio input[type='radio']").prop("checked", false);
                $("#" + CurrentRadioId).prop("checked", true);
                $("#<%=lblSelectObs.ClientID %>").text($("#ctl00_MainContent_GvObservationList_" + ID + "_Description").text());
                $("#<%=hdfSelectObsID.ClientID %>").val(Rel);
                hideModal("divObservation");
            });
        }
        );

    </script>
</asp:Content>
