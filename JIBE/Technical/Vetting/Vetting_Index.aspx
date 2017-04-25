<%@ Page Title="Vetting Index" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Vetting_Index.aspx.cs" Inherits="Technical_Vetting_Vetting_Index" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <style type="text/css">
        .hide
        {
            display: none;
        }
       
         .imgDetail img
        {
           
             Height:16px;
                       width:25px;
           
           
        }
         .imgReport img
        {
           
             Height:18px;
             width:20px;          
           
        }
        .txcenter{text-align:center;}
       .GroupHeaderStyle-css
       {
   
            text-align: center;
          
        }

    </style>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $("body").on("click", "#<%=btnRetrieve.ClientID%>", function () {

                var MSG = "";
                if ($.trim($("#<%=txtLVetFromDate.ClientID%>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtLVetFromDate.ClientID%>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        MSG = "Enter valid Between Date<%=UDFLib.DateFormatMessage()%>\n";
                    }
                }
                if ($.trim($("#<%=txtLVetToDate.ClientID%>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtLVetToDate.ClientID%>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        MSG += "Enter valid And Date<%=UDFLib.DateFormatMessage()%>";
                    }
                }
                if (MSG != "") {
                    alert(MSG);
                    return false;
                }
            });
        });


        function CreateNewVetting(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue) {

            document.getElementById('IframeCreateVetting').src = "Vetting_CreateNewVetting.aspx?VesselID=" + VesselID + "&TypeID=" + TypeID + "&Date=" + Date + "&Questionnaire=" + Questionnaire + "&OilMajor=" + OilMajor + "&Inspector=" + Inspector + "&Port=" + Port + "&PortCallID=" + PortCallID + "&NoDays=" + NoDays + "&RespNextDue=" + RespNextDue;
            showModal('dvPopupCreateVetting');
            $("#dvPopupCreateVetting").prop('title', 'Create New Vetting ');
            return false;

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

        function toggleOnSearchClearFilter(obj, objval) {

            if (objval == 'o') {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $("#advText").text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }

        }
        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            if (OSName == "MacOS") {
                document.getElementById($('[id$=btnExport]').attr('id')).style.display = "none";

            }

            if (OSName == "Windows") {

                document.getElementById($('[id$=btnMacExport]').attr('id')).style.display = "none";

            }

        }
        function UpdatePage() {

            hideModal("dvPopupAddAttachment");
            __doPostBack("<%=btnRetrieve.UniqueID %>", "onclick");

        }

        function AddAttachment(Vetting_ID) {
            document.getElementById('IframeAddAttachment').src = "Vetting_Attachments.aspx?Vetting_ID=" + Vetting_ID;
            $("#dvPopupAddAttachment").prop('title', 'Add Attachment');
            showModal('dvPopupAddAttachment');
            return false;
        }

        function AddNote_Observation(Question_ID, Observation_ID, Vetting_ID, Opn_Obs_Count, FleetCode, Vetting_Type_ID, Vessel_ID) {
            var Mode;
            document.getElementById('IframeAddNote_Observation').src = "Vetting_AddObservationNotes.aspx?Question_ID=" + Question_ID + "&Observation_ID=" + Observation_ID + "&Vetting_ID=" + Vetting_ID + "&Opn_Obs_Count=" + Opn_Obs_Count + "&FleetCode=" + FleetCode + "&Vetting_Type_ID=" + Vetting_Type_ID + "&Vessel_ID=" + Vessel_ID + "&Mode=Edit";
            $("#dvAddNote_Observation").prop('title', 'Add Note / Observation');
            showModal('dvAddNote_Observation');
            return false;
        }
        //------------------------------Open Observation tooltip--------------------
        var OpenObservation = null;
        function BindOpenObservation(Vetting_ID, ev, objthis) {

            if (OpenObservation != null)
                OpenObservation.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_OpenObservationTooltip', false, { "Vetting_ID": Vetting_ID }, onSuccessBindOpenObservation, OnfailVetIndx, new Array(ev, objthis));
            OpenObservation = service.get_executor();
        }


        //----------------------------Closed observation tooltip---------------------
        var CloseObservation = null;
        function BindCloseObservation(Vetting_ID, ev, objthis) {

            if (CloseObservation != null)
                CloseObservation.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_CloseObservationTooltip', false, { "Vetting_ID": Vetting_ID }, onSuccessBindOpenObservation, OnfailVetIndx, new Array(ev, objthis));
            CloseObservation = service.get_executor();
        }


        //-----------------------------Note Tooltip---------------------------------------
        var NoteTooltip = null;
        function BindNote(Vetting_ID, ev, objthis) {

            if (NoteTooltip != null)
                NoteTooltip.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_NoteTooltip', false, { "Vetting_ID": Vetting_ID }, onSuccessBindOpenObservation, OnfailVetIndx, new Array(ev, objthis));
            NoteTooltip = service.get_executor();
        }

        //----------------Pending job tooltip-----------------------------------
        var PendingJobs = null;
        function BindPendingJobs(Vetting_ID, ev, objthis) {

            if (PendingJobs != null)
                PendingJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_PendingJobsTooltip', false, { "Vetting_ID": Vetting_ID }, onSuccessBindOpenObservation, OnfailVetIndx, new Array(ev, objthis));

            PendingJobs = service.get_executor();
        }

        //----------------Completed Jobs job tooltip-----------------------------------
        var CompletedJobs = null;
        function BindCompletedJobs(Vetting_ID, ev, objthis) {

            if (CompletedJobs != null)
                CompletedJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_CompletedJobsTooltip', false, { "Vetting_ID": Vetting_ID, "Question_ID": null, "Observation_ID": null }, onSuccessBindOpenObservation, OnfailVetIndx, new Array(ev, objthis));

            CompletedJobs = service.get_executor();
        }

        //----------------Verified Jobs job tooltip-----------------------------------
        var VerifiedJobs = null;
        function BindVerifiedJobs(Vetting_ID, ev, objthis) {

            if (VerifiedJobs != null)
                VerifiedJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_VerifiedJobsTooltip', false, { "Vetting_ID": Vetting_ID, "Question_ID": null, "Observation_ID": null }, onSuccessBindOpenObservation, OnfailVetIndx, new Array(ev, objthis));

            VerifiedJobs = service.get_executor();
        }

        //----------------Deffered Jobs job tooltip-----------------------------------
        var DefferedJobs = null;
        function BindDefferedJobs(Vetting_ID, ev, objthis) {

            if (DefferedJobs != null)
                DefferedJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_DefferedJobsTooltip', false, { "Vetting_ID": Vetting_ID, "Question_ID": null, "Observation_ID": null }, onSuccessBindOpenObservation, OnfailVetIndx, new Array(ev, objthis));

            DefferedJobs = service.get_executor();
        }

        //----------------OnSuccess & onFail common function for all tooltip---------------
        function onSuccessBindOpenObservation(retVal, ev) {


            js_ShowToolTip_Fixed(retVal, ev[0], ev[1], "Observations");
        }

        function OnfailVetIndx(result) {
            //            var res = result.d;
            //            alert(res);
        }

        function UpdatePageAfeterSave() {

            hideModal("dvPopupCreateVetting");
            __doPostBack("<%=btnRetrieve.UniqueID %>", "onclick");

        }
        function VettingDetail(Vetting_ID) {
            hideModal("dvPopupCreateVetting");
            window.open("Vetting_Details.aspx?Vetting_Id="+Vetting_ID);
        }

        function fnAllowNumeric() {
            if (isNaN($.trim($("#" + $('[id$=txtDueDays]').attr('id')).val()))) {
                alert("Invalid Due in Days");
                $("#" + $('[id$=txtDueDays]').attr('id')).focus();
                return false;

            }
        }
        function PortCall(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port ,NoDays, RespNextDue) {
            document.getElementById('IframePortCall').src = "Vetting_PortCall.aspx?VesselID=" + VesselID + "&TypeID=" + TypeID + "&Date=" + Date + "&Questionnaire=" + Questionnaire + "&OilMajor=" + OilMajor + "&Inspector=" + Inspector + "&Port=" + Port + "&NoDays=" + NoDays + "&RespNextDue=" + RespNextDue;
            $("#dvPortCall").prop('title', 'Port');
            showModal('dvPortCall', false);
            return false;
        }

        function HidePortCall(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue) {
            hideModal("dvPortCall");
            CreateNewVetting(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue);

        }
  
    </script> 
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
    <div class="page-title">
        Vetting Index
    </div>
    <div class="page-content" style="font-family: Tahoma; font-size: 12px">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRetrieve">
            <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div  style="color: Black; padding:0px 0px;">
                    <table cellpadding="2" cellspacing="0" width="100%" style="color: Black; border-collapse:collapse " border="0">
                        <tr>
                            <td align="left" height="35px" >
                                <asp:Label ID="lblVessel" runat="server" Text="Vessel :"></asp:Label>
                            </td>
                            <td align="left" >
                                <CustomFilter:ucfDropdown ID="DDLVessel" runat="server" Height="200" UseInHeader="false"
                                    Width="160" />
                            </td>
                            <td align="left" >
                                <asp:Label ID="lblVetType" runat="server" Text="Type :"></asp:Label>
                            </td>
                            <td align="left" >
                                <CustomFilter:ucfDropdown ID="DDLVetType" runat="server" Height="200" UseInHeader="false"
                                    Width="160" />
                            </td>
                            <td align="left" >
                                <asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label>
                            </td>
                            <td align="left" >
                                <CustomFilter:ucfDropdown ID="DDLStatus" runat="server" Height="200" UseInHeader="false"
                                    Width="160" />
                            </td>
                            <td align="left" >
                                <asp:Label ID="lblDueDays" runat="server" Text="Due in Days :"></asp:Label>
                            </td>
                            <td align="left" >
                                <asp:TextBox ID="txtDueDays" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" >
                                <table celspacing="0" cellpadding="0" border="0" width="90%">
                                    <tr>
                                        <td style="padding-right:3px;"> <asp:ImageButton ID="btnRetrieve" runat="server" Height="24px" ImageAlign="AbsBottom"
                                    ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieve_Click" ToolTip="Search" 
                                    OnClientClick="return fnAllowNumeric()" /></td>
                                        <td style="padding-right:3px;"><asp:ImageButton ID="btnClearFilter" runat="server" Height="23px" ImageUrl="~/Images/filter-delete-icon.png"
                                    OnClick="btnClearFilter_Click" ToolTip="Clear Filter" /></td>
                                        <td style="padding-right:3px;"><asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" Height="25px"
                                    ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" ToolTip="Export to Excel" /></td>
                                        <td style="padding-right:3px;"><asp:ImageButton ID="btnMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                    Height="25px" ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" Style="display: none;"
                                    ToolTip="Export to Excel unformatted" /></td>
                                        <td style="font-size:11px; line-height:25px; vertical-align:top; text-align:right" >
                                            <a id="advText" href="#"
                                        onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                                        </td>
                                    </tr>
                                </table>
                          
                            </td>
                        </tr>
                        <tr>
                            <td align="left" >
                                <asp:Label ID="lblValid" runat="server" Text="Valid :"></asp:Label>
                            </td>
                            <td align="left" >
                             
                                <asp:CheckBox ID="rbtnValid" runat="server" Checked="false" />
                            </td>
                            <td align="left" >
                                <asp:Label ID="lblObservation" runat="server" Text="Open Observations? :"></asp:Label>
                            </td>
                            <td align="left" style="padding-bottom:2px; " >
                                <asp:RadioButtonList ID="rbtnObservation" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="false">
                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left" >
                                <asp:Label ID="lblVesselName" runat="server" Text="Vessel Name :"></asp:Label>
                            </td>
                            <td align="left" >
                                <asp:TextBox ID="txtVessel" runat="server"></asp:TextBox>
                            </td>
                            <td align="left" >
                              <asp:Label ID="lblUserVesselAssigment" runat="server"  Text="By Vessel assignment :"></asp:Label>
                                                   
                            </td>
                            <td align="left" >
                            <asp:UpdatePanel ID="UpdatePanel3"  runat="server">
                             <ContentTemplate>  
                             <asp:CheckBox ID="chkVesselAssign" RepeatDirection="Horizontal" AutoPostBack="true" Checked="true"  runat="server" OnCheckedChanged="chkVesselAssign_CheckedChanged"></asp:CheckBox>  
                                </ContentTemplate>
                                </asp:UpdatePanel> 
                            </td>
                            <td>
                             
                                 <asp:UpdatePanel ID="UpdatePanelAddVetting" runat="server">
                <ContentTemplate>
                    <div style="padding:8px 0px; text-align:right; overflow:hidden;">
              
                        <asp:Button  ID="ImgAddNewVetting" runat="server" ToolTip="Create New Vetting" Text="Create New Vetting" OnClientClick="CreateNewVetting(0,0,'',0,0,0,0,0,0,'')"  style="float:right; font-size:11px;" />
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>                                   
                                              
            <asp:UpdatePanel ID="UpdAdvFltr" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                    <div id="dvAdvanceFilter" align="left" class="hide">
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 750px; background-color: #efefef;">
                            <tr>
                                <td valign="top" style="border: 1px solid #aabbdd;">
                                    <table border="0" cellpadding="2" cellspacing="1" width="250px">
                                        <tr style="background-color: #aabbdd;">
                                            <td style="text-align: left; font-weight: bold;" colspan="2" width="250px">
                                                &#160;&#160;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblOilList" runat="server" Text="Oil Major :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                    <ContentTemplate>
                                                        <CustomFilter:ucfDropdown ID="DDLOilMajor" runat="server" Height="200" UseInHeader="false"
                                                            Width="160" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblInspector" runat="server" Text="Inspector :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <CustomFilter:ucfDropdown ID="DDLInspector" runat="server" Height="200" UseInHeader="false"
                                                            Width="160" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="border: 1px solid #aabbdd;">
                                    <table border="0" cellpadding="2" cellspacing="1" width="250px">
                                        <tr style="background-color: #aabbdd;">
                                            <td style="text-align: left;" colspan="2">
                                                <asp:Label ID="lblVetDate" runat="server" Text="Vetting Date " Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                Between:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtLVetFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                    Width="120px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cexLVetFromDate" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtLVetFromDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                And:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtLVetToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                    Width="120px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="cexLVetToDate" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtLVetToDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="border: 1px solid #aabbdd;">
                                    <table border="0" cellpadding="1" cellspacing="2" width="250px">
                                        <tr style="background-color: #aabbdd;">
                                            <td style="text-align: left; font-weight: bold;" colspan="2">
                                                &#160;&#160;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblJobStatus" runat="server" Text="Jobs Status :"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <CustomFilter:ucfDropdown ID="DDLJobStatus" runat="server" Height="200" UseInHeader="false"
                                                            Width="160" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
           
            <div>
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:GridView ID="gvVettingIndex" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvVettingIndex_RowDataBound"
                                Width="100%" AllowSorting="true" OnSorting="gvVettingIndex_Sorting" CssClass="gridmain-css"
                                GridLines="None" OnRowCreated="gvVettingIndex_RowCreated" 
                                >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                    <HeaderStyle  Width="100px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                            <asp:Label ID="lblVetting_Id" runat="server" Visible="false" Text='<%#Eval("Vetting_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                    <HeaderStyle  Width="100px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVetType" runat="server" Text='<%#Eval("Vetting_Type") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                     <HeaderStyle  Width="150px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVetDate" runat="server" Text='<%#Eval("Vetting_Date") %>' ></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Days">
                                     <HeaderStyle  Width="70px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("No_Of_Days") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                     <HeaderStyle  Width="100px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVetStatus" runat="server" Text='<%#Eval("Vetting_Status") %>' ></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Respond By">
                                     <HeaderStyle  Width="150px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRespondDate" runat="server" Text='<%#Eval("RespondBy") %>' ></asp:Label><asp:Label
                                                ID="lblOverDueRespondDate" Visible="false" runat="server" Text='<%#Eval("RespondByOverdue") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valid Until">
                                     <HeaderStyle  Width="150px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblValidDate" runat="server" Text='<%#Eval("Valid_Until") %>' ></asp:Label><asp:Label
                                                ID="lblOverDueValidDate" Visible="false" runat="server" Text='<%#Eval("ValidDateOverDue") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Oil Major">
                                     <HeaderStyle  Width="350px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOilMajor" runat="server" Text='<%#Eval("Oil_Major") %>' ></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inspector">
                                     <HeaderStyle  Width="200px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInspector" runat="server" Text='<%#Eval("Inspector_Name") %>' ></asp:Label></ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Open Obs.">
                                     <HeaderStyle  Width="60px"/>
                                        <ItemTemplate>
                                            <u style="color:#1d60ff; cursor:pointer"></style><asp:HyperLink runat="server" ID="hplnkOpenObs" Visible='<%# Eval("Open_Obs").ToString()!="0"?true:false %>'
                                                OnClick='<%#"BindOpenObservation(&#39;"+Eval("Vetting_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Closed Obs.">
                                    <HeaderStyle  Width="60px"/>
                                        <ItemTemplate>
                                            <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkClosedObs" Visible='<%# Eval("Closed_Obs").ToString()!="0"?true:false %>'
                                                OnClick='<%#"BindCloseObservation(&#39;"+Eval("Vetting_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notes">
                                    <HeaderStyle  Width="60px"/>
                                        <ItemTemplate>
                                            <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkNote" Visible='<%# Eval("Notes").ToString()!="0"?true:false %>'
                                                OnClick='<%#"BindNote(&#39;"+Eval("Vetting_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resp.">
                                    <HeaderStyle  Width="60px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponse" runat="server" Text='<%#Eval("Responses") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending">
                                    <HeaderStyle  Width="100px"/>
                                        <ItemTemplate>
                                            <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkJobPending" Visible='<%# Eval("Pending").ToString()!="0"?true:false %>'
                                                Target="_blank" OnClick='<%#"BindPendingJobs(&#39;"+Eval("Vetting_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Completed">
                                    <HeaderStyle  Width="100px"/>
                                        <ItemTemplate>
                                           <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkJobCompleted" Visible='<%# Eval("Completed").ToString()!="0"?true:false %>'
                                                Target="_blank" OnClick='<%#"BindCompletedJobs(&#39;"+Eval("Vetting_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Verified">
                                    <HeaderStyle  Width="100px"/>
                                        <ItemTemplate>
                                            <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkJobVerified" Visible='<%# Eval("Verified").ToString()!="0"?true:false %>'
                                                Target="_blank" OnClick='<%#"BindVerifiedJobs(&#39;"+Eval("Vetting_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deferred">
                                    <HeaderStyle  Width="100px"/>
                                        <ItemTemplate>
                                            <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkJobDeffered" Visible='<%# Eval("Deferred").ToString()!="0"?true:false %>'
                                                Target="_blank" OnClick='<%#"BindDefferedJobs(&#39;"+Eval("Vetting_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                    <HeaderStyle  Width="150px"/>
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:HyperLink ID="ImgDetails" runat="server" Text="Details" ForeColor="Black" ToolTip="Details"
                                                            ImageUrl="~/Images/Details-icon.png"   Target="_blank"></asp:HyperLink>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgAttatch" runat="server" Text="Attachments" ForeColor="Black"
                                                            ToolTip="Attachments" ImageUrl="~/Images/VET_Attach.png"  OnClick='<%# "AddAttachment(&#39;"+Eval("Vetting_ID").ToString()+"&#39;)" %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:HyperLink ID="ImgReport" runat="server" Text="Report" ForeColor="Black" ToolTip="Report"
                                                           ImageUrl="~/Images/Vet_Report_Icon.png" NavigateUrl='<%#"Vetting_Reports.aspx?Vetting_ID="+Eval("Vetting_ID")+"" %>'
                                                            Target="_blank"></asp:HyperLink>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgRework" runat="server" Text="Rework"  ForeColor="Black" ToolTip="Re-open"
                                                            OnClientClick="return confirm('Are you sure you want to re-open the vetting?')"
                                                            ImageUrl="~/Images/Rework-icon.png"  OnCommand="onRework" CommandArgument='<%#Eval("[Vetting_ID]")%>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete"  OnClientClick="return confirm('Are you sure you wish to continue?')"
                                                            ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" 
                                                            Visible='<%# uaDeleteFlage %>' OnCommand="onDelete" CommandArgument='<%#Eval("[Vetting_ID]")%>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" Height="14px" Width="16px" ImageUrl="~/Images/RecordInformation.png"
                                                             runat="server" onclick='<%# "Get_Record_Information(&#39;VET_DTL_Vetting&#39;,&#39;Vetting_ID="+Eval("Vetting_ID").ToString()+"&#39;,event,this)" %>'>
                                                        </asp:Image>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                            color: Black; text-align: left;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="Bind_VettingIndex" />
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
        </asp:Panel>
    </div>
    <div id="dvPopupAddAttachment" style="display: none; width: 550px;" title="Add Attachment">
        <iframe id="IframeAddAttachment" src="" frameborder="0" style="height: 320px; width: 550px;">
        </iframe>
    </div>
    <div id="dvAddNote_Observation" style="display: none; width: 1300px;" title="Add Note / Observation">
        <iframe id="IframeAddNote_Observation" src="" frameborder="0" style="height: 755px;
            width: 1300px;"></iframe>
    </div>
    <div id="dvPopupCreateVetting" style="display: none; width:600px;"
        title="Create New Vetting">
        <iframe id="IframeCreateVetting" src="" frameborder="0" style="height:370px; width:600px;">
        </iframe>
    </div>
    <div id="dvPortCall" style="display: none; width: 815px;" title="Port">
        <iframe id="IframePortCall" src="" frameborder="0" style="height: 250px; width: 815px;">
        </iframe>
    </div>
</asp:Content>
