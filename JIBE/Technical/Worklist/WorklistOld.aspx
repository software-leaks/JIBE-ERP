<%@ Page Title="Worklist" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="WorklistOld.aspx.cs" Inherits="Technical_WorklistOld" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            font-size: 12px;
            height: 21px;
        }
        
        #page-content a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: black;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: blue;
            text-decoration: none;
        }
        .pager
        {
            font-size: 12px;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            color: Gray;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 2px 0px 2px;
            font-weight: bold;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script language='javascript' type='text/javascript'>
        function divCategorylink() {
            document.getElementById("divCategory").style.display = "block";
        }

        function OpenCategoryDiv() {
            var divCategory = document.getElementById("divCategory");
            divCategory.style.display = 'inline';
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'true';
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
        //        function openReport() {
        //            var sel = document.getElementById('ddlreports');
        //            if (sel) {
        //                if (sel.value == "0")
        //                    window.open('../reports/reportJobList.aspx');
        //                else
        //                    window.open('../reports/reportJobProgress.aspx');

        //            }
        //        }
    </script>
    <script type="text/javascript">
        function showDialog(url) {
            window.open(url);
        }
        function showEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'block';
        }
        function hideEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'none';
        }
        function showFollowups(V, W, O) {
            var src = window.event.srcElement;
            var pos = $(src).offset();
            var width = $(src).width();

            var url = 'Task_Followups.aspx?WLID=' + W + '&VID=' + V + '&OFFID=' + O;

            $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
            $('#iframeFollowups').attr("src", url);
            $('#dialog').show();
            $("#dialog").css({ "left": (pos.left - 600) + "px", "top": pos.top + "px", "width": 600 });


        }
        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
                        top: 20px; z-index: 2; color: black">
                        <img src="../../images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div id="pageTitle" style="background-color: #15317E; color: White; font-size: 12px;
                text-align: center; padding: 2px; font-weight: bold;">
                Worklist</div>
            <div id="page-content" style="border: 1px solid gray; z-index: -2; overflow: auto;">
                <table border="0" cellpadding="1" cellspacing="1" style="border: 1px solid #15317E;
                    width: 100%;">
                    <tr>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td style="text-align: right;">
                                        Fleet:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFleet" runat="server" Width="135px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Vessels:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlVessels" runat="server" Width="135px" OnSelectedIndexChanged="Filter_Changed" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Assignor:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlAssignor" runat="server" Width="135px" AutoPostBack="True"
                                            OnSelectedIndexChanged="Filter_Changed">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Dept On Ship:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddldeptShip" runat="server" Width="135px" AutoPostBack="True"
                                            OnSelectedIndexChanged="Filter_Changed">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Dept in Office:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlOffice" runat="server" Width="135px" AutoPostBack="True"
                                            OnSelectedIndexChanged="Filter_Changed">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Priority:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlPriority" runat="server" Width="135px" AutoPostBack="True"
                                            OnSelectedIndexChanged="Filter_Changed">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Inspector:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlInspector" runat="server" Width="135px" AutoPostBack="True"
                                            OnSelectedIndexChanged="Filter_Changed">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td style="text-align: left;">
                                        <asp:Label ID="abc" runat="server" Text="Categories" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <a href="JavaScript:OpenCategoryDiv()">Search<img src="../../Images/search.gif" style="border: 0px"
                                            alt="Search Categories" /></a>&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Nature:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlNature" runat="server" Width="129px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlNature_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Primary:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlPrimary" runat="server" Width="129px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlPrimary_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Secondary:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlSecondary" runat="server" Width="129px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSecondary_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Minor:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlMinor" runat="server" Width="129px" AutoPostBack="True">
                                            <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Job Code:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtJobCode" runat="server" Width="122px" AutoPostBack="True" OnTextChanged="Filter_Changed"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Description:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="129px" AutoPostBack="True"
                                            OnTextChanged="Filter_Changed"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td style="text-align: left;">
                                        <asp:Label ID="cde" runat="server" Text="Job Status" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label1" runat="server" Text="Job Type" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="text-align: left;">
                                        <asp:RadioButtonList ID="rblJobStaus" runat="server" RepeatDirection="Vertical" TextAlign="Right"
                                            CellPadding="1" CellSpacing="0" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                            <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Completed"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Pending" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td valign="top" style="text-align: left;">
                                        <asp:RadioButtonList ID="rblJobType" runat="server" RepeatDirection="Vertical" TextAlign="Right"
                                            CellPadding="1" CellSpacing="0" Width="90px" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                            <asp:ListItem Selected="True" Value="2" Text="All"></asp:ListItem>
                                            <asp:ListItem Selected="False" Value="-1" Text="NCR"></asp:ListItem>
                                            <asp:ListItem Selected="False" Value="0" Text="JOB"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left" colspan="2" style="padding-top: 5px">
                                        <table>
                                            <tr>
                                                <td>
                                                    PIC
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPIC" runat="server" Width="129px" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                                        <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Modified in last
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtModifiedInDays" runat="server" Width="50px" OnTextChanged="Filter_Changed"
                                                        AutoPostBack="true"></asp:TextBox>
                                                    Days
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd; width: 150px">
                            <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td colspan="2" style="text-align: left;">
                                        <asp:Label ID="Label2" runat="server" Text="Date Raised" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        From:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtFromDate" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        To:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtToDate" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px">
                                    </td>
                                </tr>
                                <tr style="background-color: #aabbdd">
                                    <td colspan="2" style="text-align: left;">
                                        <asp:Label ID="Label3" runat="server" Text="Expected Date of Compln" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        From:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtExpectedCompFrom" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtExpectedCompFrom"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        To:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtExpectedCompTo" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtExpectedCompTo"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <%--<asp:ImageButton ID="ImgBtnSearchByDate" ImageUrl="~/Images/SearchByDate.png" runat="server" />--%>
                                        <%--<asp:Button ID="btnSearch" Text="Search By Date" runat="server" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="0" cellspacing="0" width="250px" style="text-align: left;">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Reports:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlReports" runat="server">
                                                        <asp:ListItem Value="0" Text="Job List"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Jobs Progress"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImgBtnReport" ImageUrl="~/Images/reportview.gif" runat="server"
                                                        OnClick="ImgBtnReport_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImgExportToExcel" src="../../Images/DocTree/xls.jpg" Height="20px" runat="server"
                                                        AlternateText="Export" OnClick="ImgExportToExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkDrydock" runat="server" Text="Jobs that deffered to Drydock"
                                            AutoPostBack="True" OnCheckedChanged="Filter_Changed" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkSentToShip" runat="server" Text="Jobs Sent to ship from office"
                                            AutoPostBack="True" OnCheckedChanged="Filter_Changed" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkRequisition" runat="server" Text="Having Requisition No." AutoPostBack="True"
                                            OnCheckedChanged="Filter_Changed" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkFlaggedJobs" runat="server" Text="Show jobs flagged for Meeting"
                                            AutoPostBack="True" OnCheckedChanged="Filter_Changed" /><asp:Image ID="Image1" runat="server"
                                                ImageUrl="~/Images/Flag_ON.png" ImageAlign="Middle" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 12px">
                                        <asp:ImageButton ID="ImgBtnAddNewJob" ImageUrl="~/Images/AddNewJob.png" runat="server"
                                            OnClientClick="javascript:window.open('addnewjob.aspx?OFFID=0&WLID=0&VID=0');return false;" />
                                        <asp:ImageButton ID="ImgBtnClearFilter" ImageUrl="~/Images/ClearFilter.png" runat="server"
                                            OnClick="ImgBtnClearFilter_Click" />
                                        <asp:ImageButton ID="ImgBtnSearch" ImageUrl="~/Images/SearchAndReload.png" runat="server"
                                            OnClick="ImgBtnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <div style="width: 100%; overflow: auto; text-align: left;">
                                            <div id="Div1" class="gradiant-css-blue" style="font-size: 12px; border: 1px solid #CEE3F6;
                                                text-align: left; padding: 3px; font-weight: bold;">
                                                <div style="float: right; position: relative; font-weight: normal;">
                                                    <asp:Image ID="imgFlag" runat="server" ImageUrl="~/Images/Flag_ON.png" ImageAlign="AbsMiddle" />Flagged
                                                    for Technical Meeting</div>
                                                Total Jobs:<asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
                                            </div>
                                            <div>
                                            </div>
                                            <asp:GridView ID="grdJoblist" runat="server" BackColor="White" BorderColor="#aabbdd"
                                                AutoGenerateColumns="false" BorderStyle="Solid" BorderWidth="1px" CellPadding="4"
                                                EnableModelValidation="True" AllowSorting="true" Width="100%" GridLines="None"
                                                OnRowCommand="grdJoblist_RowCommand" OnRowDataBound="grdJoblist_RowDataBound"
                                                OnSorting="grdJoblist_Sorting" AllowPaging="true" PageSize="15" OnPageIndexChanging="grdJoblist_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="#DDeeEE" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="10px">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgAnyChanges" runat="server" Height="16px" Width="16px" ImageUrl="~/Images/exclamation.gif"
                                                                Visible='<%#Eval("MODIFIED").ToString()=="1"?true:false %>' ToolTip="Modified in last 3 days." />
                                                        </ItemTemplate>
                                                        <ControlStyle Width="15px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Vessel" SortExpression="Vessel_Short_Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVesselShortName" runat="server" Text='<%#Eval("Vessel_Short_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Code" SortExpression="WORKLIST_ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WORKLIST_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Description" SortExpression="JOB_DESCRIPTION"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                                target="_blank" style="cursor: hand; color: Blue;">
                                                                <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Assignor" SortExpression="AssignorName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdAssignor" runat="server" Text='<%#Eval("AssignorName") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="PIC" SortExpression="PIC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Date Raised" SortExpression="DATE_RAISED">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdRaisedDate" runat="server" Text='<%# Eval("DATE_RAISED","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("DATE_RAISED","{0:d/MM/yy}")  %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Office Dept" SortExpression="INOFFICE_DEPT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdofficeDept" runat="server" Text='<%#Eval("INOFFICE_DEPT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Vessel Dept" SortExpression="ONSHIP_DEPT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdVesselDept" runat="server" Text='<%#Eval("ONSHIP_DEPT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Expected Compln" SortExpression="DATE_ESTMTD_CMPLTN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MM/yy}") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Completed" SortExpression="DATE_COMPLETED">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("DATE_COMPLETED","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MM/yy}") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="NCR" SortExpression="NCR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdNCR" runat="server" Text='<%#Eval("NCR").ToString()=="0"?"":"YES" %>'></asp:Label></ItemTemplate>
                                                        <ControlStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Att.">
                                                        <ItemTemplate>
                                                            <asp:Image ID="ImgAttachment" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attachment" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="FollowUps">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgRemarks" runat="server" ImageUrl='~/Images/remark.gif' CssClass="job-remarks"
                                                                CommandName="ADD_FOLLOWUP" CommandArgument='<%#Eval("VESSEL_ID").ToString() + "," + Eval("WORKLIST_ID").ToString()  + "," + Eval("OFFICE_ID").ToString()%>'>
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="">
                                                        <ItemTemplate>
                                                            <a href='AddNewJob.aspx?OFFID=<%#Eval("OFFICE_ID")%>&WLID=<%#Eval("WORKLIST_ID")%>&VID=<%#Eval("VESSEL_ID")%>'
                                                                target="_blank">
                                                                <asp:Image ID="imgEdit" runat="server" Height="16px" Width="16px" ImageUrl='~/Images/edit.gif'
                                                                    Visible='<%#(Eval("WORKLIST_ID").ToString() != "0" && (Eval("DATE_COMPLETED","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ||Eval("DATE_COMPLETED","{0:dd/MM/yyyy}").ToString() == ""))?true:false %>' />
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEmail" runat="server" ImageUrl="~/Images/EMail.png" CommandName="EMailJob"
                                                                CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgFlagOFF" runat="server" ImageUrl="~/Images/Flag_Off.png"
                                                                Visible='<%# !Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>' CommandName="FlagJobForMeeting"
                                                                CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",1"%>' />
                                                            <asp:ImageButton ID="imgFlagON" runat="server" ImageUrl="~/Images/Flag_ON.png" Visible='<%# Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>'
                                                                CommandName="FlagJobForMeeting" CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",0"%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <label id="Label1" runat="server">
                                                        No jobs found !!</label>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <PagerStyle Font-Size="16px" CssClass="pager" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="divCategory" style="display: none; background-color: #E0E0E0; border-color: Black;
                border-style: none; height: 400px; position: absolute; width: 700px; left: 25%;
                top: 15%; z-index: 2; color: black">
                <table width="100%">
                    <tr>
                        <td colspan="4" style="font-size: 12px; text-align: center; border-style: solid;
                            border-color: gray; background-color: gray;">
                            Category Selection
                            <input type="hidden" runat="server" id="hdnFlagCheck" value="false" />
                            <input type="hidden" runat="server" id="hdnNature" value="0" />
                            <input type="hidden" runat="server" id="hdnPrimary" value="0" />
                            <input type="hidden" runat="server" id="hdnSecondary" value="0" />
                            <input type="hidden" runat="server" id="hdnMinor" value="0" />
                        </td>
                    </tr>
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
                            Secondary :
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
                        <td colspan="2" align="right">
                            <asp:Button ID="btnSelectAndClose" Text="Select And Close" runat="server" OnClientClick="JavaScript:SetAndClose();"
                                OnClick="btnSelectAndClose_OnClick" />
                        </td>
                        <td style="width: 25%" align="right">
                            <input type="button" id="btnCloseMe" value="Cancel" onclick="JavaScript:CloseMe()" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvAddFollowUp" title="Add Follow-Up" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnOfficeID" runat="server" />
                <asp:HiddenField ID="hdnWorklistlID" runat="server" />
                <asp:HiddenField ID="hdnVesselID" runat="server" />
                <table width="100%" cellpadding="0" cellspacing="5">
                    <tr>
                        <td style="text-align: left">
                            Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFollowupDate" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Message:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="200px" Width="480px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSaveFollowUpAndClose" Text="Save And Close" runat="server" OnClientClick="hideModal('dvAddFollowUp');"
                                OnClick="btnSaveFollowUpAndClose_OnClick" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
        position: absolute;">
        Loading Data ...
        <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
    </div>
</asp:Content>
