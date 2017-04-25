<%@ Page Title="Operations - WorkList" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="TaskIndex.aspx.cs" Inherits="Operations_TaskPlanner_TaskIndex" %>

<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc2" %>
<%--<%@ Register Src="../../UserControl/ucUploadOpsWorklistAttachment.ascx" TagName="ctlUploadAttachment"
    TagPrefix="uc3" %>--%>
<%--<%@ Register Src="~/UserControl/DnDUploader.ascx" TagName="DnDUploader" TagPrefix="uc4" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link type="text/css" href="../../styles/ui-lightness/jquery-ui-1.8.14.custom.css"
        rel="stylesheet" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        input
        {
            font-family: Tahoma;
        }
        select
        {
            font-family: Tahoma;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        
        .grid-view-row
        {
            border-bottom: 1px dashed #A9BCF5;
        }
        .grid-view-row a:link
        {
            text-decoration: none;
            color: Blue;
        }
        .grid-view-row a:hover
        {
            text-decoration: none;
            color: Black;
        }
        .overlay
        {
            display: none;
            width: 100%;
            background-color: black;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 999;
        }
        
        #dvVesselMovement
        {
            display: none;
            position: absolute;
            width: 60%;
            text-align: center;
            z-index: 1000;
            border: 1px solid #333;
        }
        #dvTaskDetails
        {
            display: none;
            position: absolute;
            left: 25%;
            top: 15%;
            width: 50%;
            text-align: center;
            z-index: 1000;
            border: 1px solid #333;
        }
        #dvUpdateStatus
        {
            display: none;
            position: absolute;
            left: 40%;
            top: 25%;
            width: 20%;
            text-align: center;
            z-index: 1000;
            border: 1px solid #333;
        }
        #dvAddNewFollowup
        {
            display: none;
            position: absolute;
            left: 30%;
            top: 25%;
            width: 40%;
            text-align: center;
            z-index: 1000;
            border: 1px solid #333;
            background-color: #E0E0E0;
            border: 1px solid outset;
            width: 500px;
            color: black;
        }
        
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .pager
        {
            font-size: 14px;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 5px 0px 5px;
            color: Gray;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 5px 0px 5px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 5px 0px 5px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 5px 0px 5px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 5px 0px 5px;
            font-weight: bold;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            border: 1px solid #cccccc;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .field-value
        {
            background-color: White;
            color: Black;
        }
    </style>
    <style id="Style1" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: arial;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;
            border-right: 2px solid #3B0B0B;
            border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E8FDE8;
            border: 1px solid #F1C15F;
            color: Black;
        }
        p
        {
            margin-top: 20px;
        }
        h1
        {
            font-size: 13px;
        }
        .dogvdvhdr
        {
            width: 300;
            background: #C4D5E3;
            border: 1px solid #C4D5E3;
            font-weight: bold;
            padding: 10px;
        }
        .dogvdvbdy
        {
            width: 300;
            background: #FFFFFF;
            border-left: 1px solid #C4D5E3;
            border-right: 1px solid #C4D5E3;
            border-bottom: 1px solid #C4D5E3;
            padding: 10px;
        }
        .pgdiv
        {
            width: 320;
            height: 250;
            background: #E9EFF4;
            border: 1px solid #C4D5E3;
            margin-bottom: 20;
            padding: 10px;
            font-family: arial;
            font-size: 12px;
        }
    </style>
    <style type="text/css">
        @media print
        {
            body
            {
                color: black;
                font-family: Tahoma;
                font-size: 14px;
            }
            .header
            {
                display: none;
            }
            .printable
            {
                display: block;
                border: 0;
            }
            .printable table
            {
                display: block;
                border: 0;
            }
            .non-printable
            {
                display: none;
            }
            #pageTitle
            {
                border: 0;
            }
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.draggable').draggable();
        });

        $(document).keydown(function (e) {
            // ESCAPE key pressed 
            if (e.keyCode == 27) {
                if (isModalOpen == 1) {
                    hideModal(ModalPopUpID);
                }
            }
        });

        function GetBrowser() {
            var UserAgent = '<%=Request.UserAgent %>';

            var OSName = "";
            var Browser = "";

            if (UserAgent.indexOf("Win") != -1) OSName = "Windows";
            if (UserAgent.indexOf("Mac") != -1) OSName = "MacOS";
            if (UserAgent.indexOf("X11") != -1) OSName = "UNIX";
            if (UserAgent.indexOf("Linux") != -1) OSName = "Linux";

            if (UserAgent.indexOf("MSIE") != -1) Browser = "MSIE";
            if (UserAgent.indexOf("Chrome") != -1) Browser = "Chrome";
            if (UserAgent.indexOf("Firefox") != -1) Browser = "Firefox";
            if (UserAgent.indexOf("Safari") != -1) Browser = "Safari";

            return Browser;
        }

        
        
    </script>
    <script language="javascript" type="text/javascript">
        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
            //            $('#' + dv).animate({ height: 1 }, 1);
            //            $('#' + dv).animate({ height: 150 }, 1000);
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
            //$('#dvItemList').show();
        }

        function OpenFollowupDiv() {
            document.getElementById("dvAddFollowUp").style.display = "block";
            return false;
        }
        function CloseFollowupDiv() {
            var dvAddFollowUp = document.getElementById("dvAddFollowUp");
            dvAddFollowUp.style.display = 'none';
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

        function showDialog(url) {
            window.open(url);
        }
    </script>
    <script type="text/javascript">
        function PrintDiv(dvID) {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
        }

        function startUpload(sender, args) {
            $('#uploadMessage p').html();
            $('#uploadMessage').hide();
        }

        function uploadComplete(sender, args) {
            showUploadMessage(args.get_fileName() + " uploaded succesfully - " + +args.get_length() + " bytes", '');
        }

        function uploadError(sender, args) {
            showUploadMessage("An error occurred during uploading. " + args.get_errorMessage(), '#ff0000');
        }

        function showUploadMessage(text, color) {
            $('#uploadMessage p').html(text).css('color', color);
            $('#uploadMessage').show();
        }

        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <table style="width: 100%">
            <tr>
                <td align="right">
                    Worklist
                </td>
                <td style="text-align: right">
                    <a href="Worklist.pdf" target="_blank" style="text-decoration: none">Help
                        <img src="../../images/help-icon.png" alt="Help" style="border: 0; height: 16px;
                            vertical-align: middle" /></a>
                </td>
            </tr>
        </table>
    </div>
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                        color: black">
                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:HiddenField ID="hdnVessel_ID" runat="server" />
            <asp:HiddenField ID="hdnWorklist_ID" runat="server" />
            <asp:HiddenField ID="hdnOffice_ID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel_Main" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="page-content" style="border: 1px solid #CEE3F6; z-index: -2; margin-top: -1px;
                min-height: 600px; padding: 5px; overflow: auto;">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; color: Black;">
                    <tr>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="2" cellspacing="1">
                                <tr>
                                    <td style="text-align: right;">
                                        Fleet:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFleet" runat="server" Width="200px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        Vessel:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlVessels" runat="server" Width="200px" AutoPostBack="True"
                                            OnSelectedIndexChanged="Filter_Changed">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        Category:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlCatFilter" runat="server" Width="200px" AutoPostBack="true"
                                            DataSourceID="objDsCat" DataTextField="Name" DataValueField="Code" AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="Filter_Changed">
                                            <asp:ListItem Text="- SELECT -" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        Private:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:CheckBox ID="chkPrivateFilter" runat="server" OnCheckedChanged="Filter_Changed"
                                            AutoPostBack="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td style="text-align: left;">
                                        Person Assigned
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:ListBox ID="lstPICFilter" runat="server" Width="100%" Height="80px" AutoPostBack="True"
                                            OnSelectedIndexChanged="Filter_Changed" AppendDataBoundItems="true" DataSourceID="objDSPICFilter"
                                            DataTextField="USERNAME" DataValueField="USERID">
                                            <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                        </asp:ListBox>
                                        <asp:ObjectDataSource ID="objDSPICFilter" runat="server" SelectMethod="Get_UserList_By_Dept_DL"
                                            TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="USERCOMPANYID"
                                                    Type="Int32" />
                                                <asp:Parameter DefaultValue="4,9,1" Name="DeptIDcsv" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd; width: 150px">
                            <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td colspan="2" style="text-align: left;">
                                        Date Raised
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        From:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtFromDate" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <tlk4:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                            Format="dd/MM/yyyy">
                                        </tlk4:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        To:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtToDate" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <tlk4:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                            Format="dd/MM/yyyy">
                                        </tlk4:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td colspan="2" style="text-align: left;">
                                        Expected Date of Compln
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        From:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtExpectedCompFrom" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <tlk4:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtExpectedCompFrom"
                                            Format="dd/MM/yyyy">
                                        </tlk4:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        To:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtExpectedCompTo" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                            AutoPostBack="true"></asp:TextBox>
                                        <tlk4:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtExpectedCompTo"
                                            Format="dd/MM/yyyy">
                                        </tlk4:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td style="text-align: left;">
                                        Status
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="text-align: center;">
                                        <asp:RadioButtonList ID="rdoJobStaus" runat="server" RepeatDirection="Horizontal"
                                            TextAlign="Right" CellPadding="1" CellSpacing="0" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                            <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="To Verify"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Completed"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Pending" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        Search:
                                        <asp:TextBox ID="txtDescription" runat="server" Width="120px" AutoPostBack="True"
                                            OnTextChanged="Filter_Changed"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        Modified in last &nbsp;
                                        <asp:TextBox ID="txtModifiedInDays" runat="server" Width="40px" AutoPostBack="True"
                                            OnTextChanged="Filter_Changed"></asp:TextBox>&nbsp;days
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="border: 1px solid #aabbdd;">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr style="background-color: #aabbdd">
                                    <td style="text-align: left;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="ImgBtnClearFilter" runat="server" Text="Clear Filter" OnClick="ImgBtnClearFilter_Click"
                                            CssClass="btnCSS" Height="24px" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAddNewTask" runat="server" Text="Add New Task" OnClick="btnAddNewTask_Click"
                                            CssClass="btnCSS" Height="24px" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="border: 1px solid #aabbdd; padding: 2px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <div style="width: 100%; overflow: auto; text-align: left;">
                                            <asp:GridView ID="grdJoblist" runat="server" AutoGenerateColumns="false" CellPadding="5"
                                                GridLines="Horizontal" BorderStyle="None" BackColor="White" BorderColor="Red"
                                                BorderWidth="1px" EnableModelValidation="True" AllowSorting="true" Width="100%"
                                                OnRowCommand="grdJoblist_RowCommand" OnRowDataBound="grdJoblist_RowDataBound"
                                                OnSorting="grdJoblist_Sorting" AllowPaging="false" ForeColor="#383838">
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
                                                            <%#Eval("Vessel_Short_Name") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="ID" SortExpression="WL.WORKLIST_ID">
                                                        <ItemTemplate>
                                                            <%#Eval("WORKLIST_ID") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task Category" SortExpression="PRI.NAME" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblTask" runat="server" Text='<%#Eval("CATEGORY_PRIMARY_NAME") %>'
                                                                CommandName="VIEW_TASK" CommandArgument='<%#Eval("VESSEL_ID").ToString() + "," + Eval("WORKLIST_ID").ToString()  + "," + Eval("OFFICE_ID").ToString() + "," + Eval("TASK_STATUS").ToString()+","+Eval("IsVerified").ToString()%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJD" runat="server" Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 40 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 40) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Person Assigned" SortExpression="LIB_USER.First_Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPIC_Name" runat="server" Text='<%#Eval("PIC_Name").ToString().Length > 10 ?  Eval("PIC_Name").ToString().Substring(0, 10) + "..." : Eval("PIC_Name").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Port Call" SortExpression="Vessel_Short_Name">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblPortCall" runat="server" Text='<%#Eval("Port_Call") %>' CommandName="SELECT_PORTCALL"
                                                                CommandArgument='<%#Eval("VESSEL_ID").ToString() + "," + Eval("WORKLIST_ID").ToString()  + "," + Eval("OFFICE_ID").ToString() + "," + Eval("TASK_STATUS").ToString()+ "," + Eval("PORT_CALL_ID").ToString() %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="First_Name" ItemStyle-HorizontalAlign="Center" HeaderText="Created By" />
                                                    <asp:BoundField DataField="ISPRIVATE" ItemStyle-HorizontalAlign="Center" HeaderText="Private" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Expected Compln" SortExpression="DATE_ESTMTD_CMPLTN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MM/yy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Completion Date" SortExpression="DATE_COMPLETED">
                                                        <ItemTemplate>
                                                            <%# Eval("DATE_COMPLETED","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MM/yy}") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="">
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
                                                        HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgStatus" runat="server" Height="15px" ImageUrl='<%# (Eval("DATE_COMPLETED","{0:dd/MM/yyyy}").ToString() == "01/01/1900" || Eval("DATE_COMPLETED","{0:dd/MM/yyyy}").ToString() == "" ) ? "../../Images/Round-Red-icon.png" : "../../Images/Round-Green-icon.png" %>'
                                                                CommandName="UPDATE_STATUS" CommandArgument='<%#Eval("VESSEL_ID").ToString() + "," + Eval("WORKLIST_ID").ToString()  + "," + Eval("OFFICE_ID").ToString() + "," + Eval("TASK_STATUS").ToString()  + "," + Eval("DATE_COMPLETED","{0:d/MM/yy}").ToString() +","+Eval("IsVerified").ToString()%>'>
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEmail" runat="server" ImageUrl="~/Images/EMail.png" CommandName="EMailJob"
                                                                CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Re activate">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnReActivate" Font-Size="11px" runat="server" Text="Re Activate"
                                                                Visible='<%#Eval("DATE_COMPLETED").ToString().Length > 0 && (Session["userid"].ToString()=="15" || Session["userid"].ToString()=="42" || Session["userid"].ToString()=="53" || Session["userid"].ToString()=="30" || Session["userid"].ToString()=="44")?true:false %>'
                                                                CommandName="RE_ACTIVATE" CommandArgument='<%#Eval("VESSEL_ID").ToString() + "," + Eval("WORKLIST_ID").ToString()  + "," + Eval("OFFICE_ID").ToString()%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <label id="Label1" runat="server">
                                                        No tasks found !!</label>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" CssClass="pager" />
                                                <RowStyle CssClass="grid-view-row" BackColor="White" />
                                                <AlternatingRowStyle CssClass="grid-view-row" BackColor="#EFF8FB" />
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>
                                            <div style="margin-top: 10px">
                                                <uc1:ucCustomPager ID="ucCustomPager" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Tasks"
                                                    PageSize="20" OnBindDataItem="Load_Tasks" AlwaysGetRecordsCount="true" />
                                            </div>
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
    <div id="dvTaskDetails" title="TASK DETAILS">
        <asp:UpdatePanel ID="UpdatePanel_Details" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; vertical-align: top;">
                            <div id="dvViewTaskDetails" style="background-color: White; text-align: left;">
                                <table style="width: 100%; border: 1px solid gray;">
                                    <tr>
                                        <td align="right" style="width: 100%; background-color: #D0E8FF; color: Black">
                                            <uc2:ctlRecordNavigation ID="ctlRecordNavigationTask" OnNavigateRow="Load_TaskDetails_Navigate"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="grid-container">
                                            <asp:ObjectDataSource ID="objDsCat" runat="server" SelectMethod="Get_PrimaryCat_List"
                                                TypeName="SMS.Business.Operation.BLL_OPS_TaskPlanner"></asp:ObjectDataSource>
                                            <asp:ObjectDataSource ID="objDSPIC" runat="server" SelectMethod="Get_UserList_By_Dept_DL"
                                                TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="USERCOMPANYID"
                                                        Type="Int32" />
                                                    <asp:Parameter DefaultValue="4,9,1" Name="DeptIDcsv" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <asp:ObjectDataSource ID="objDSVessel" runat="server" SelectMethod="Get_VesselList"
                                                TypeName="SMS.Business.Infrastructure.BLL_Infra_VesselLib">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="0" Name="FleetID" Type="Int32" />
                                                    <asp:Parameter DefaultValue="0" Name="VesselID" Type="Int32" />
                                                    <asp:Parameter DefaultValue="89" Name="VesselManager" Type="Int32" />
                                                    <asp:Parameter DefaultValue="" Name="SearchText" Type="String" />
                                                    <asp:SessionParameter DefaultValue="0" Name="UserCompanyID" SessionField="USERCOMPANYID"
                                                        Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <asp:FormView ID="frmTaskDetails" runat="server" Width="100%" OnModeChanging="frmTaskDetails_ModeChanging"
                                                DefaultMode="ReadOnly" OnItemInserting="frmTaskDetails_ItemInserting" OnItemUpdating="frmTaskDetails_Updating"
                                                OnItemCommand="frmTaskDetails_ItemCommand" OnDataBound="frmTaskDetails_DataBound">
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlItemTemplate" runat="server">
                                                        <div style="position: absolute; right: 0; padding: 5px 10px 0px 0px;">
                                                            <img src="../../Images/printer.png" height="20px" alt="Print" onclick="PrintDiv('dvViewTaskDetails')"
                                                                style="cursor: hand" />
                                                        </div>
                                                        <table border="0" cellpadding="2" cellspacing="5" width="100%">
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Vessel:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("VESSEL_NAME") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption" style="width: 200px">
                                                                    Task ID:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("WORKLIST_ID") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Task Category:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("CATEGORY_PRIMARY_NAME") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Description:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("JOB_DESCRIPTION") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Person Assigned:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("PIC_NAME")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Port Calling:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("port_call") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Expected Completion:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("DATE_ESTMTD_CMPLTN","{0:dd/MM/yyyy}")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Completion Date:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("DATE_COMPLETED", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Private:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("ISPRIVATE")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <%--<div style="margin: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
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
                                                                            </tr>
                                                                        </table>
                                                                    </div>--%>
                                                                    <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
                                                                        border-collapse: separate; background-color: #FDFDFD">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                                                                        padding: 2px; font-size: 16px; margin-top: 5px; margin-bottom: 5px; color: #0B3861;
                                                                        text-align: right;">
                                                                        <table border="0" cellpadding="2" cellspacing="5" width="100%">
                                                                            <tr>
                                                                                <td style="text-align: left">
                                                                                    Followups:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="ImgBtnAddFollowup" runat="server" ImageUrl="~/Images/AddFollowup.png"
                                                                                        OnClientClick="OpenFollowupDiv();return false;" CssClass="non-printable" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div style="max-height: 400px; overflow: auto;">
                                                                        <asp:GridView ID="grdFollowUps" runat="server" BackColor="White" BorderColor="#999999"
                                                                            AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="1"
                                                                            AllowPaging="false" EnableModelValidation="True" GridLines="Vertical" Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Date" SortExpression="DATE_CREATED">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDATE_CREATED" runat="server" Text='<%#Eval("DATE_CREATED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_CREATED","{0:d/MM/yy HH:mm}")   %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="150px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Created By" SortExpression="LOGIN_NAME">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLOGIN_NAME" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="150px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Followup" SortExpression="FOLLOWUP" ItemStyle-Width="350px"
                                                                                    ItemStyle-Wrap="true" ItemStyle-VerticalAlign="Top">
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("FOLLOWUP") %>
                                                                                        <%--<asp:Label ID="lblFOLLOWUP" runat="server" Text='<%#Eval("FOLLOWUP").ToString().Replace("\n","<br>") %>'></asp:Label>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle BackColor="#DCDCDC" VerticalAlign="Top" />
                                                                            <EmptyDataTemplate>
                                                                                <asp:Label ID="ldl1" runat="server" Text="No followups !!"></asp:Label>
                                                                            </EmptyDataTemplate>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" VerticalAlign="Top" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                                <InsertItemTemplate>
                                                    <table border="0" cellpadding="2" cellspacing="5" width="100%">
                                                        <tr>
                                                            <td class="field-caption">
                                                                Vessel:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:DropDownList ID="ddlVesselInsert" runat="server" DataSourceID="objDSVessel"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlVesselInsert_SelectedIndexChanged"
                                                                    Width="255px" DataTextField="VESSEL_NAME" DataValueField="VESSEL_ID" AppendDataBoundItems="true">
                                                                    <asp:ListItem Text="- SELECT -" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="static"
                                                                    ControlToValidate="ddlVesselInsert" ErrorMessage="Select Vessel" ValidationGroup="ValidEntry"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field-caption" style="width: 200px">
                                                                Task Category:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:DropDownList ID="ddlCatInsert" runat="server" Width="405px" AutoPostBack="false"
                                                                    Text='<%#Bind("CATEGORY_PRIMARY") %>' DataSourceID="objDsCat" DataTextField="Name"
                                                                    DataValueField="Code" AppendDataBoundItems="true">
                                                                    <asp:ListItem Text="- SELECT -" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="static"
                                                                    ControlToValidate="ddlCatInsert" ErrorMessage="Select Task Category" ValidationGroup="ValidEntry"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field-caption">
                                                                Description:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:TextBox ID="txtDescription" runat="server" Text='<%#Bind("JOB_DESCRIPTION") %>'
                                                                    TextMode="MultiLine" Width="400px" Height="60px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="static"
                                                                    ControlToValidate="txtDescription" ErrorMessage="Enter Task Description" ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field-caption">
                                                                Person Assigned:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:ListBox ID="ddlPICInsert" runat="server" Width="255px" AutoPostBack="false"
                                                                    AppendDataBoundItems="true" DataSourceID="objDSPIC" DataTextField="USERNAME"
                                                                    DataValueField="USERID">
                                                                    <asp:ListItem Value="0" Text="-SELECT -"></asp:ListItem>
                                                                </asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="static"
                                                                    ControlToValidate="ddlPICInsert" ErrorMessage="Select Person Assigned" ValidationGroup="ValidEntry"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field-caption">
                                                                Port Calling:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:DropDownList ID="ddlPortCallInsert" runat="server" AppendDataBoundItems="false"
                                                                    DataTextField="Port_Call" DataValueField="Port_Call_ID" Width="255px">
                                                                    <asp:ListItem Text="- SELECT -" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field-caption">
                                                                Expected Completion:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:TextBox ID="txtExpectedCompletion" CssClass="" runat="server" Width="250px"
                                                                    Text='<%#Bind("DATE_ESTMTD_CMPLTN") %>'></asp:TextBox>
                                                                <tlk4:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtExpectedCompletion"
                                                                    Format="dd/MM/yyyy">
                                                                </tlk4:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="static"
                                                                    ControlToValidate="txtExpectedCompletion" ErrorMessage="Select Expected Completion Date"
                                                                    ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field-caption">
                                                                Completion Date:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:TextBox ID="txtCompletion" CssClass="" runat="server" Width="250px" Text='<%#Bind("DATE_COMPLETED") %>'></asp:TextBox>
                                                                <tlk4:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtCompletion"
                                                                    Format="dd/MM/yyyy">
                                                                </tlk4:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field-caption">
                                                                Private:
                                                            </td>
                                                            <td class="field-value">
                                                                <asp:CheckBox ID="chkPrivate" CssClass="" runat="server" Checked='<%# Bind("PRIVATE") %>'>
                                                                </asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center">
                                                                <asp:Button ID="InsertButton" CommandName="Insert" ValidationGroup="ValidEntry" runat="server"
                                                                    Text=" SAVE " BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                                                    Height="24px" Width="100px" BackColor="#81DAF5" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </InsertItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Panel ID="pnlEditItemTemplate" runat="server">
                                                        <table border="0" cellpadding="2" cellspacing="5" width="100%">
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Vessel:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:Label ID="lblVesselUpdate" Text='<%#Bind("VESSEL_NAME") %>' runat="server">
                                                                    </asp:Label>
                                                                    <asp:HiddenField ID="hdnVesselUpdate" runat="server" Value='<%#Bind("VESSEL_ID") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption" style="width: 200px">
                                                                    Task Category:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:DropDownList ID="ddlCatUpdate" runat="server" Width="405px" AutoPostBack="false"
                                                                        Text='<%#Bind("CATEGORY_PRIMARY") %>' DataSourceID="objDsCat" DataTextField="Name"
                                                                        DataValueField="Code" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="- SELECT -" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="static"
                                                                        ControlToValidate="ddlCatUpdate" ErrorMessage="Select Task Category" ValidationGroup="ValidEntry"
                                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Description:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%#Bind("JOB_DESCRIPTION") %>'
                                                                        TextMode="MultiLine" Width="400px" Height="60px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="static"
                                                                        ControlToValidate="txtDescription" ErrorMessage="Enter Task Description" ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Person Assigned:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:DropDownList ID="ddlPICUpdate" runat="server" Width="155px" AppendDataBoundItems="true"
                                                                        Text='<%#Bind("PIC") %>' DataSourceID="objDSPIC" DataTextField="USERNAME" DataValueField="USERID">
                                                                        <asp:ListItem Value="0" Text="-SELECT -"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="static"
                                                                        ControlToValidate="ddlPICUpdate" ErrorMessage="Select Person Assigned" ValidationGroup="ValidEntry"
                                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Port Calling:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:DropDownList ID="ddlPortCallUpdate" runat="server" DataTextField="Port_Call"
                                                                        DataValueField="Port_Call_ID" Width="155px">
                                                                        <asp:ListItem Text="- SELECT -" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Expected Completion:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:TextBox ID="txtExpectedCompletion" CssClass="" runat="server" Width="150px"
                                                                        Text='<%#Bind("DATE_ESTMTD_CMPLTN","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                                    <tlk4:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtExpectedCompletion"
                                                                        Format="dd/MM/yyyy">
                                                                    </tlk4:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="static"
                                                                        ControlToValidate="txtExpectedCompletion" ErrorMessage="Select Expected Completion Date"
                                                                        ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Completion Date:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:TextBox ID="txtCompletion" CssClass="" runat="server" Width="150px" Text='<%#Bind("DATE_COMPLETED","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                                    <tlk4:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtCompletion"
                                                                        Format="dd/MM/yyyy">
                                                                    </tlk4:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Created By:
                                                                </td>
                                                                <td class="field-value">
                                                                    <%#Eval("First_Name")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="field-caption">
                                                                    Private:
                                                                </td>
                                                                <td class="field-value">
                                                                    <asp:CheckBox ID="chkPrivate" CssClass="" runat="server" Checked='<%# Eval("PRIVATE").ToString().Equals("1")  %>'
                                                                        Enabled='<%# Eval("Created_By").ToString().Equals(GetSessionUserID().ToString())  %>'>
                                                                    </asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: center">
                                                                    <asp:Button ID="UpdateButton" CommandName="Update" ValidationGroup="ValidEntry" runat="server"
                                                                        Text=" Update " BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                                                        Height="24px" Width="100px" BackColor="#81DAF5" />
                                                                    <asp:Button ID="CancelUpdate" CommandName="Cancel" runat="server" CausesValidation="false"
                                                                        Text=" Cancel " BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                                                        Height="24px" Width="100px" BackColor="#81DAF5" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </EditItemTemplate>
                                            </asp:FormView>
                                            <div id="dvAddFollowUp" class="draggable" style="display: none; background-color: #E0E0E0;
                                                border: 1px solid outset; width: 500px; position: absolute; left: 20%; top: 10%;
                                                color: black">
                                                <table width="100%" cellpadding="5" cellspacing="0" style="background-color: #cccccc;
                                                    border: 2px solid gray;">
                                                    <tr>
                                                        <td style="text-align: center; font-weight: bold; border-style: solid; border-color: Silver;
                                                            background-color: Gray; padding: 2px;">
                                                            <h4>
                                                                New Followup</h4>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="background-color: White" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="background-color: #aabbee; padding: 2px; text-align: center; width: 100px;">
                                                                        &nbsp;&nbsp;Message:&nbsp;&nbsp;
                                                                    </td>
                                                                    <td colspan="2" style="background-color: #cccccc;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="border: 1px solid inset; background-color: #aabbee; padding-top: 5px;
                                                                        padding-bottom: 5px; text-align: center;">
                                                                        <asp:TextBox ID="txtFollowupMessage" runat="server" TextMode="MultiLine" Height="200px"
                                                                            Width="480px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Button ID="btnSaveFollowUpAndClose" Text="Save And Close" runat="server" OnClientClick="JavaScript:CloseFollowupDiv();"
                                                                OnClick="btnSaveFollowUpAndClose_Click" />
                                                            <input type="button" id="Button3" value="Close" onclick="CloseFollowupDiv()" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="dvAttachments">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                                                                padding: 2px; font-size: 16px; margin-top: 5px; margin-bottom: 5px; color: #0B3861;
                                                                text-align: right;">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td style="text-align: left">
                                                                            Attachments:
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <%-- <asp:Panel ID="dvUploader_MSIE" runat="server">--%>
                                                                            <%--<uc3:ctlUploadAttachment ID="ctlUploadAttachment1" runat="server" OnUploadCompleted="ctlUploadAttachment1_UploadCompleted" />--%>
                                                                            <img src="../../Images/AddAttachment.png" onclick="showModal('dvPopupAddAttachment',true,fn_OnClose);" />
                                                                            <%-- </asp:Panel>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 80%; vertical-align: top;">
                                                                        <asp:GridView ID="gvAttachments" runat="server" AllowPaging="false" AllowSorting="true"
                                                                            AutoGenerateColumns="false" BackColor="White" BorderStyle="None" CellPadding="4"
                                                                            EnableModelValidation="True" GridLines="None" Width="100%" ShowHeader="false">
                                                                            <AlternatingRowStyle BackColor="#DDeeEE" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:HyperLink ID="lblAttach_Name" runat="server" NavigateUrl='<%# "~/Uploads/Technical/" + Eval("Attach_Path").ToString() %>'
                                                                                            Target="_blank" Text='<%#Eval("Attach_Name") %>'></asp:HyperLink>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Attachment" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="150" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <label id="Label1" runat="server">
                                                                                    No Attachment found !!</label>
                                                                            </EmptyDataTemplate>
                                                                            <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                                            <PagerStyle CssClass="pager" Font-Size="16px" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                    <td style="width: 20%; vertical-align: top;">
                                                                        <%--  <asp:Panel ID="dvUploader_Fx" runat="server">--%>
                                                                        <%-- <uc4:DnDUploader ID="DnDUploader1" runat="server" OnUploadCompleted="DnDUploader1_UploadCompleted"
                                                                                OnUploadFailed="DnDUploader1_UploadFailed" FileUploadPath="Uploads/Technical" />--%>
                                                                        <%--</asp:Panel>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: 1px solid #aabbee; background-color: White; margin-top: 2px;
                                padding: 5px; text-align: right;">
                                <asp:Button ID="btnEditTask" runat="server" OnClick="btnEditTask_Click" Text="Edit Task"
                                    BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                    Height="24px" Width="100px" BackColor="#81DAF5" />
                                <asp:Button ID="btnVerifyFromDetails" CommandName="Update" CommandArgument="verify"
                                    OnClick="btnVerifyFromDetails_Click" runat="server" Text="Verify " BorderStyle="Solid"
                                    BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="100px"
                                    BackColor="#81DAF5" />
                                <asp:Button ID="btnCloseTaskDetails" runat="server" Text="Close" BorderStyle="Solid"
                                    BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="100px"
                                    BackColor="#81DAF5" OnClientClick="hideModal('dvTaskDetails');return false;" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvVesselMovement" class="draggable" title="Select Port-Call">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; vertical-align: top;">
                    <div id="dvContainer" style="background-color: White;">
                        <asp:UpdatePanel ID="UpdatePanel_PortCalls" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="width: 100%; border: 1px solid gray; margin-top: 5px;">
                                    <tr>
                                        <td class="grid-container">
                                            <asp:GridView ID="gvPortCalls" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#336666" BorderStyle="Double" BorderWidth="1px" CellPadding="2"
                                                AllowPaging="true" PageSize="15" GridLines="Horizontal" DataKeyNames="Port_Call_ID"
                                                Font-Size="11px" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Vessel">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Port Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPort_Name" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnPortID" runat="server" Value='<%# Eval("Port_ID")%>'></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Arrival">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArrival" runat="server" Text='<%# Eval("Arrival","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Departure" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Owners Agent" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOwners_Agent" runat="server" Text='<%# Eval("Owners_Agent")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Charterers Agent" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCharterers_Agent" runat="server" Text='<%# Eval("Charterers_Agent")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                                CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="lblNoRec" runat="server" Text="No record found."></asp:Label>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                    CssClass="pager" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#F7BE81" Font-Bold="True" ForeColor="Black" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="border: 1px solid #aabbee; background-color: White; margin-top: 2px;
                        padding: 5px; text-align: right;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSavePortCall" runat="server" Text="Save & Close" BorderStyle="Solid"
                                    BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="150px"
                                    BackColor="#81DAF5" OnClick="btnSavePortCall_Click" CssClass="close-overlay" />
                                <asp:Button ID="btnClosePortCall" runat="server" Text="Cancel" BorderStyle="Solid"
                                    BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="100px"
                                    BackColor="#81DAF5" CssClass="close-overlay" OnClientClick="hideModal('dvVesselMovement'); return false;" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvUpdateStatus" title="UPDATE TASK STATUS">
        <asp:UpdatePanel ID="UpdatePanel_UpdateStatus" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; text-align: left;">
                    <tr>
                        <td style="text-align: center; vertical-align: top;">
                            <div id="Div2" style="background-color: White; text-align: left;">
                                <table style="width: 100%; border: 1px solid gray; margin-top: 5px;" cellspacing="5">
                                    <tr>
                                        <td style="width: 150px">
                                            Status
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTaskStatus" runat="server"></asp:Label>
                                            <%--<asp:RadioButtonList ID="rdoTaskStatusUpdate" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Completed" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                            Completion Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompletionDate" CssClass="" runat="server" Width="80px" AutoPostBack="false"></asp:TextBox>
                                            <tlk4:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txtCompletionDate"
                                                Format="dd/MM/yyyy">
                                            </tlk4:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="static"
                                                ControlToValidate="txtCompletionDate" ErrorMessage="Select Completion Date" ValidationGroup="StatusUpdate"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                            Completion Remark
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompletionRemark" runat="server" Width="250px" Height="100px"
                                                AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="static"
                                                ControlToValidate="txtCompletionRemark" ErrorMessage="Enter Completion Remark"
                                                ValidationGroup="StatusUpdate"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: 1px solid #aabbee; background-color: White; margin-top: 2px;
                                padding: 5px; text-align: right;">
                                <asp:Button ID="btnUpdateTaskStatus" runat="server" Text="Save & Close" BorderStyle="Solid"
                                    BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="150px"
                                    CausesValidation="true" ValidationGroup="StatusUpdate" BackColor="#81DAF5" OnClick="btnUpdateTaskStatus_Click" />
                                <asp:Button ID="btnSaveVerify" CommandName="Update" CommandArgument="verify" OnClick="btnSaveVerify_Click"
                                    runat="server" Text="Verify " BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px"
                                    Font-Names="Tahoma" Height="24px" Width="100px" BackColor="#81DAF5" />
                                <asp:Button ID="btnCloseUpdateTaskStatus" runat="server" Text="Close" BorderStyle="Solid"
                                    BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="100px"
                                    BackColor="#81DAF5" CssClass="close-overlay" OnClientClick="hideModal('dvUpdateStatus'); return false;" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvAddNewFollowup" class="draggable">
        <asp:UpdatePanel ID="UpdatePanel_AddNewFollowup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%" cellpadding="5" cellspacing="0" style="background-color: #cccccc;
                    border: 2px solid gray;">
                    <tr>
                        <td style="text-align: center; font-weight: bold; border-style: solid; border-color: Silver;
                            background-color: Gray; padding: 2px;">
                            <h4>
                                New Followup</h4>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="background-color: White" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="background-color: #aabbee; padding: 2px; text-align: center; width: 100px;">
                                        &nbsp;&nbsp;Message:&nbsp;&nbsp;
                                    </td>
                                    <td colspan="2" style="background-color: #cccccc;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border: 1px solid inset; background-color: #aabbee; padding-top: 5px;
                                        padding-bottom: 5px; text-align: center;">
                                        <asp:TextBox ID="txtAddNewFollowup" runat="server" TextMode="MultiLine" Height="200px"
                                            Width="480px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAddNewFollowUpAndClose" Text="Save And Close" runat="server" OnClick="btnAddNewFollowUpAndClose_Click"
                                OnClientClick="hideModal('dvAddNewFollowup')" />
                            <input type="button" id="Button2" value="Close" onclick="hideModal('dvAddNewFollowup')" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="overlay" class="overlay">
    </div>
    <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
        position: absolute;">
        Loading Data ...
        <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
    </div>
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
