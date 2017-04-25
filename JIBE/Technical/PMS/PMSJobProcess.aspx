<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSJobProcess.aspx.cs" Inherits="Technical_PMS_PMSJobProcess" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomStringFilter.ascx" TagName="ucfString"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomNumberFilter.ascx" TagName="ucfNumber"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomDateFilter.ascx" TagName="ucfDate" TagPrefix="CustomFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>

    <style type="text/css">
     .hide
        {
            display:none;
        }
    .page
    {
        width:1440px;
    }
    </style>

    <script lang="javascript" type="text/javascript">

        function DocOpen(filename) {

            var filepath = "../../uploads/PmsJobs/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }


        function showdetails(path) {
            window.open(path);
            return false;
        }

        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            if (OSName == "MacOS") {
                document.getElementById("ctl00_MainContent_btnExport").style.display = "none";
            }

            if (OSName == "Windows") {
                document.getElementById("ctl00_MainContent_btnMacExport").style.display = "none";
            }

        }


        function showJobDetails(url) {

            try {
                var frame = document.getElementById("iFrmJobsDetails"),
        frameDoc = frame.contentDocument || frame.contentWindow.document;
                frameDoc.removeChild(frameDoc.documentElement);

            }
            catch (ex) {

            }

            document.getElementById('iFrmJobsDetails').src = url;
            showModal('dvJobsDetails');

        }


        function OpenWorkListCrewInvolved(vid, jobid, jobhistoryid) {


            $('#dvPopupFrame').attr("Title", "Add maintenance feedback for this job");
            $('#dvPopupFrame').css({ "width": "700px", "height": "600px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

            var URL = "../../Technical/PMS/PMSJob_Crew_Involved.aspx?VID=" + vid + "&JobID=" + jobid + "&JobHistoryID=" + jobhistoryid + "&Mode=ADD&rnd=" + Math.random();

            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);
        }

        function toggleAdvSearch(obj) {


            if ($(obj).text() == "Open Advance Filter") {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $(obj).text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }

            if ($("#<%= hfAdv.ClientID %>").val() == "c") {
                $("#<%= hfAdv.ClientID %>").val('o');
            }
            else {
                $("#<%= hfAdv.ClientID %>").val('c');
            }
        }
        function toggleSerachPostBack() {
            if ($("#<%= hfAdv.ClientID %>").val() == "o") {
                $("#dvAdvanceFilter").show();
                $("#advText").text("Close Advance Filter");
            }

        }

        function toggleOnSearchClearFilter(obj,objval) {


            if (objval == 'o') {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $("#advText").text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }


        }

        function searchKeyPress(e) {
            // look for window.event in case event isn't passed in
            if (e.keyCode == 13) {
                document.getElementById($('[id$=btnRetrieve]').attr('id')).click();
                return false;
            }
            return true;
        }
        
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
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
        <div class="page-title">
            <asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
        </div>
        <div class="page-content" style="font-family: Tahoma; font-size: 12px">
            <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <table cellpadding="2" cellspacing="0" width="100%" style="color: Black;">
                        <tr>
                            <td align="left" style="width: 10%">
                                Fleet : &nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                    Font-Size="11px" Height="20px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                    Width="124px">
                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                              <td align="left" style="width: 10%">
                                Function :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlFunction" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="width:10%">
                                Job Due Date Between : &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                    Font-Size="11px" Width="120px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        <td align="left" colspan="2" style="white-space: nowrap" >
                               &nbsp;&nbsp;Pending Office Verification:&nbsp;
                          &nbsp;
                                 <asp:CheckBox ID="chkOfcVerify" runat="server"></asp:CheckBox>
                            </td>
                            
                            <td align="left" style="width: auto; white-space: nowrap">
                                Critical : &nbsp;
                            </td>
                            <td align="left">
                            <asp:CheckBox ID="chkCritical" runat="server"></asp:CheckBox>
                                <%--<CustomFilter:ucfDropdown ID="ucf_optCritical" runat="server" Height="60" OnApplySearch="BindJobStatus"
                                    UseInHeader="false" Width="100" />--%>
                            </td>
                            <td align="right" colspan="2">
                         
                           
                               <asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" Height="25px"
                                    ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" Style="display: block;"
                                    ToolTip="Export to Excel formatted" />
                             <asp:ImageButton ID="btnMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                    Height="25px" ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" Style="display: none;"
                                    ToolTip="Export to Excel unformatted" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Vessel : &nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLVessel" runat="server" AutoPostBack="true" Font-Size="11px"
                                    Height="20px" Width="124px" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                             <td align="left">
                                System /Location :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSystem_location" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSystem_location_SelectedIndexChanged"
                                    Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                And : &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                    Font-Size="11px" Width="120px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                           
                          
                            <td align="left" colspan="2">
                                <asp:RadioButtonList ID="rbtnJobTypes" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="false">
                                    <asp:ListItem Text="Planned" Value="PMS" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="UnPlanned" Value="NONPMS"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left">
                                CMS : &nbsp;
                            </td>
                            <td align="left">
                              <%--  <CustomFilter:ucfDropdown ID="ucf_optCMS" runat="server" Height="60" OnApplySearch="BindJobStatus"
                                    UseInHeader="false" Width="100" />--%>

                                <asp:CheckBox ID="chkCMS" runat="server"></asp:CheckBox>
                            </td>
                            <td align="center">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Search by code/title : &nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSearchJobTitle" runat="server" EnableViewState="true" Font-Size="11px"
                                    CssClass="txtInput" Width="120px" onkeypress="return searchKeyPress(event);"></asp:TextBox>
                            </td>
                         <td align="left">
                                Subsystem / Location :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubSystem_location" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            
                            <td colspan="2" style="text-align: center">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnOverDue" runat="server" BorderStyle="None" Font-Size="11px" Height="22px"
                                                OnClick="btnOverDue_Click" Style="border: 1px solid #cccccc; cursor: pointer;"
                                                Text="Overdue" Width="80px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btn7days" runat="server" BorderStyle="None" Font-Size="11px" Height="22px"
                                                OnClick="btn7days_Click" Style="border: 1px solid #cccccc; cursor: pointer;"
                                                Text="7 days" Width="80px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCurrentMonth" runat="server" BorderStyle="None" Font-Size="11px"
                                                Height="22px" OnClick="btnCurrentMonth_Click" Style="border: 1px solid #cccccc;
                                                cursor: pointer;" Text="This month" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 6%">
                                  &nbsp;&nbsp;Rank :
                            </td>
                            <td align="left">
                              <CustomFilter:ucfDropdown ID="ucf_DDLRank" runat="server" Height="200" OnApplySearch="BindJobStatus"
                                    UseInHeader="false" Width="150" />
                            </td>
                            <td>
                                 
                            </td>
                            
                            <td colspan="2">
                            <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" ImageAlign="AbsBottom"
                                    ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieve_Click" ToolTip="Search" /> &nbsp;
                              <asp:ImageButton ID="btnClearFilter" runat="server" Height="24px" ImageUrl="~/Images/filter-delete-icon.png"
                                    OnClick="btnClearFilter_Click" ToolTip="Clear Filter" />&nbsp;
                                   <%--  <asp:LinkButton ID="lnkAdvText" runat="server" OnClientClick="toggleAdvSearch(this)">Open Advance Filter</asp:LinkButton>--%>
                               <a id="advText" href="#" onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                            </td>
                         
                        </tr>
                        <tr>
                        <td colspan="12" align="right">
                          
                        </td>
                        </tr>
                    </table>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
               <asp:UpdatePanel ID="UpdAdvFltr" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
             <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                     <div id="dvAdvanceFilter" align="left"  class="hide">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 65%;background-color: #efefef;">
                        <tr>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="2" cellspacing="1"  width="100%">
                                    <tr style="background-color: #aabbdd;">
                                        <td style="text-align: left; font-weight:bold; " colspan="2">
                                            Actual Job Performance Date:
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            Between:
                                        </td>
                                        <td style="text-align: left;">
                                             <asp:TextBox ID="txtActFrmDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                    Font-Size="11px" Width="120px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtActFrmDate">
                                        </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                           And:
                                        </td>
                                        <td style="text-align: left;">
                                           <asp:TextBox ID="txtActToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                    Font-Size="11px" Width="120px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtActToDate">
                                        </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                            
                            
                            <td valign="top" style="border: 1px solid #aabbdd; width: 300px">
                                <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td colspan="2" style="text-align: left;">
                                          &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            Calibration:
                                        </td>
                                        <td style="text-align: left;">
                                          <asp:CheckBox ID="chkAdvCalibration" runat="server"  />
                                   
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="text-align: left;">
                                            Safety Alarm:
                                        </td>
                                        <td style="text-align: left;">
                                          <asp:CheckBox ID="chkAdvSftyAlarm" runat="server"  />
                                   
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>

                            <%--Added by reshma for RA--%>
                           <td valign="top" style="border: 1px solid #aabbdd; width: 350px">
                                <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td colspan="2" style="text-align: left;">
                                          &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                             Mandatory Risk Assessment
                                        </td>
                                        <td style="text-align: left;">
                                          
                                         <asp:RadioButtonList ID="rbtnMRA" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="false">
                                            <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                        </asp:RadioButtonList>
                                   
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="text-align: left;">
                                            Risk Assessment Submitted
                                        </td>
                                        <td style="text-align: left;">
                                          
                                       <asp:RadioButtonList ID="rbtnRASubmitted" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="false">
                                            <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </td>
                                    </tr>                                    
                                </table>
                            </td>
                            
                        </tr>
                    </table>
                </div>

                </ContentTemplate>
                </asp:UpdatePanel>
            <div style="border: 1px solid #cccccc; margin-top: 2px">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:GridView ID="gvStatus" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvStatus_RowDataBound"
                                Width="100%" AllowSorting="true" OnSorting="gvStatus_Sorting" DataKeyNames="JOB_ID"
                                OnRowCreated="gvStatus_RowCreated" OnRowCommand="gvStatus_RowCommand" CssClass="gridmain-css"
                                GridLines="None">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Blue" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                            <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                            <asp:Label ID="lblLocationID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                            <asp:Label ID="lblJobHistoryID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JobHistoryID") %>'></asp:Label>
                                            <asp:Label ID="lblSysID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SysID") %>'></asp:Label>
                                            <asp:Label ID="lblSubSysID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubSysID") %>'></asp:Label>
                                            <asp:Label ID="lblFunctionID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FunctionID") %>'></asp:Label>
                                            <asp:Label ID="lblFleetID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FleetID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Machinery" HeaderText="System" Visible="false" />
                                    <asp:TemplateField HeaderText="Location">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblLocationHeader" runat="server">System Location&nbsp;</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" onclick='<%#"asyncGetMachinery_Popup(&#39;"+Eval("System_ID").ToString()+"&#39;,event,this);" %>'
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Location") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubSystem" Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSubSystemHeader" runat="server">SubSystem&nbsp;</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubSystem" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubSystem") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SUBLocation" HeaderText="Sub-System Location" />
                                    <asp:TemplateField HeaderText="Job Code" HeaderStyle-Width="50px">
                                        <HeaderTemplate>
                                            Job Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_ID") %>'></asp:Label>
                                            <asp:HyperLink ID="hlnkJobCode" runat="server" Target="_blank" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Code") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Title">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblJobTitleHeader" runat="server" CommandArgument="JOB_TITLE"
                                                CommandName="Sort">Job Title&nbsp;</asp:LinkButton>
                                            <img id="JOB_TITLE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTitle" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_TITLE") %>'></asp:Label>
                                            <asp:Label ID="lblJobDescription" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Description") %>'></asp:Label>
                                            <asp:Label ID="lblOverDueFlage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OVERDUEFLAGE") %>'></asp:Label>
                                            <asp:Label ID="lblNext30dayFlage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NEXT30DAYSFLAGE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frequency">
                                        <HeaderTemplate>
                                            Freq.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrequency" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FREQUENCY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frequency Name">
                                        <HeaderTemplate>
                                            Freq. Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrequencyType" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FREQUENCY_TYPE") %>'></asp:Label>
                                            <asp:Label ID="lblFrequencyName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Frequency_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Done">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblJobTitleHeaderDone" runat="server" CommandArgument="LAST_DONE"
                                                CommandName="Sort">Done&nbsp;</asp:LinkButton>
                                                   <img id="DATE_DONE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastDone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LAST_DONE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Due">
                                        <HeaderTemplate>
                                           <asp:LinkButton ID="lblJobTitleHeaderNdue" runat="server" CommandArgument="DATE_NEXT_DUE"
                                                CommandName="Sort">Due&nbsp;</asp:LinkButton>
                                                   <img id="DATE_NEXT_DUE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNextDue" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DATE_NEXT_DUE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rhrs">
                                        <HeaderTemplate>
                                            Rhrs
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRHrsdone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RHRS_DONE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RemRhrs">
                                        <HeaderTemplate>
                                            Rem. Rhrs
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemRHrsdone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RemRhrs") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="CMS">
                                        <HeaderTemplate>
                                            CMS
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCms" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CMS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Critical">
                                        <HeaderTemplate>
                                            Critical
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCritical" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Critical") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mandatory Risk Assessment">
                                        <HeaderTemplate>
                                            Mandatory Risk Assessment
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMandatoryRA" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IsRAMandatory") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                        <HeaderTemplate>
                                            Department
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Department") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank" Visible="false">
                                        <HeaderTemplate>
                                            Rank
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRank" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RankName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" Visible="false">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobHistoryRemaks" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Remarks") %>'></asp:Label>
                                            <asp:Label ID="lblFullJobHistoryRemaks" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FullRemarks") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblAction" runat="server">Action</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <span id="lblActionDisplayText" style="height: 15px; width: 100px; color: #1E607F"> <%--#FFFF00--%>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:Label ID="lblFilePathIfSingle" Visible="false" runat="server" Text='<%# Eval("FilePathIfSingle") %>'></asp:Label>
                                                        <asp:ImageButton ID="ImgJobDoneAtt" runat="server" Height="16px" Visible='<%# Eval("AttCount").ToString() =="0" ? false : true%>'
                                                            ForeColor="Black" ImageUrl="~/Images/attach-icon.png" />
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:HyperLink ID="ImgHistory" runat="server" Text="Select" Height="12px" ForeColor="Black"
                                                            ToolTip="Job History" Target="_blank" ImageUrl="~/Purchase/Image/JobHistory.png"
                                                            onmouseover="DisplayActionInHeader('Job History','gvStatus')"></asp:HyperLink>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgRHours" runat="server" Text="Select" Height="12px" ForeColor="Black"
                                                            Visible="false" ToolTip="Running hours" ImageUrl="~/Purchase/Image/Rhrs.png"
                                                            onmouseover="DisplayActionInHeader('Running hrs','gvStatus')"></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgSpareUsed" runat="server" Text="Select" Height="16px" Width="16px"
                                                            CommandArgument='<%#Eval("JOB_ID")%>' ForeColor="Black" ToolTip="Spare Item Used"
                                                            ImageUrl="~/Purchase/Image/ToolBox.png" Visible='<%#DataBinder.Eval(Container.DataItem, "Spare_used_flag").ToString() =="N"? false : true %>'
                                                            onmouseover="DisplayActionInHeader('Spare Item Used','gvStatus')" />
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgFeedback" runat="server" Text="Select" Height="16px" Width="16px"
                                                            CommandArgument='<%#Eval("JOB_ID")%>' ForeColor="Black" ToolTip="Add Feedback"
                                                            OnClientClick='<%# "OpenWorkListCrewInvolved("+ Eval("VESSEL_ID").ToString() +","+ UDFLib.ConvertToInteger(Eval("JOB_ID")).ToString()  +","+ Eval("JobHistoryID").ToString() +");return false;" %>'
                                                            ImageUrl="~/Purchase/Image/Add-Maker.png" Visible='<%#Eval("JobHistoryID").ToString() =="" ? false : true %>'
                                                            onmouseover="DisplayActionInHeader('Add Feedback','gvStatus')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                           
                        </div>
                          <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px; color: Black; text-align: left; ">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 100%">
                               <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindJobStatus" />
                            </td>
                         
                        </tr>
                    </table>
                </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                        <asp:PostBackTrigger ControlID="btnMacExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvJobsDetails" style="display: none; width: 830px;" title=''>
            <iframe id="iFrmJobsDetails" src="" frameborder="0" style="height: 560px; width: 100%">
            </iframe>
        </div>
        <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
            border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
            left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
            <div class="content">
                <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
            </div>
        </div>
    </center>
</asp:Content>
