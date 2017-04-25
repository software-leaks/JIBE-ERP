<%@ Page Title="Survey Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ValidateRequest="false" EnableEventValidation="false" CodeFile="SurveyDetails.aspx.cs"
    Inherits="Surveys_SurveyDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="Navigator" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
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
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background-color: #F1C15F;
            color: Black;
        }
        .required
        {
            background-color: #F2F5A9;
            border: 1px solid #dcdcdc;
            height: 18px;
        }
        .data
        {
            border: 1px solid gray;
            background-color: White;
            font-weight: bold;
            height: 20px;
            padding: 2px;
        }
        .Overdue
        {
            background-color: Red;
            color: Yellow;
        }
        
        .Legend
        {
            color: Black;
            font-weight: bold;
            text-align: left;
            padding-bottom: 2px;
        }
        .Due0-30
        {
            background-color: Orange;
            color: Black;
        }
        .Due30-90
        {
            background-color: Yellow;
            color: Black;
        }
        .Done30
        {
            background-color: Green;
            color: White;
        }
    </style>
    <script type="text/javascript">
        function Footer()        
        {
           var SurveyDetailId = '<%=Request.QueryString["s_d_id"]%>';
           var OffId = '<%=Request.QueryString["off_id"]%>';
           if ( SurveyDetailId.length == 0 || parseInt(SurveyDetailId) == 0 )
                SurveyDetailId = $("#hdnS_D_ID").val();
           if (OffId.length == 0 || parseInt(OffId) == 0 )
                OffId = $("#hdnO_ID").val();
           var wh = 'Vessel_ID=<%=Request.QueryString["vid"]%> AND Surv_Vessel_ID=<%=Request.QueryString["s_v_id"]%> AND Surv_Details_ID='+SurveyDetailId+' AND OfficeID='+OffId
           Get_Record_Information_Details('tblSurvVessel_3Details', wh);
        }

        function EnableDisableInspection(){
         if ($("#txtRenewwalInspection").val() == '') {
                $("#btnScheduleInspection").attr('disabled',false);
            }
            else {
                $("#btnScheduleInspection").attr('disabled', true);
            }
        }

        $(document).ready(function () {
           var strDateFormat = "<%= DateFormat %>";
           var ValidDateFormatMessage = '<%= UDFLib.DateFormatMessage() %>';
           var SurveyDetailId = '<%=Request.QueryString["s_d_id"]%>';

            if ( SurveyDetailId.length == 0 || parseInt(SurveyDetailId) == 0 )
                SurveyDetailId = $("#hdnS_D_ID").val();
           
            if (SurveyDetailId > 0) {
                var AdminAccessRights = <%= AdminAccessRights %> ;
                if ( AdminAccessRights == 1 )
                    $("#ImgDeleteCertificate").show();
                else
                    $("#ImgDeleteCertificate").hide();
                $("#btnScheduleInspection").show();
                Footer();
            }
            else {
                $("#ImgDeleteCertificate").hide();
                if ($("#txtRenewwalInspection").val() == '') {
                    $("#btnScheduleInspection").attr('disabled',false);
                }
                else {
                    $("#btnScheduleInspection").attr('disabled', true);
                }
            }

            EnableDisableInspection();            

            $("body").on("click", "#btnScheduleInspection", function () {
               EnableDisableInspection();
               if ($("#txtRenewwalInspection").val() == '') 
               {
                    OpenInspectionSchedule();
               }
            });

            /*Open completed Inspection list starts here*/
            $("body").on("click", "#btnCertInspection", function () {
               var dt ="";
               document.getElementById('IframeInspList').src = "../Surveys/InspectionList.aspx?InspectionType=CertInspection&Status=Completed&IssueDate="+dt+"&VesselId="+'<%=Request.QueryString["vid"]%>';
               showModal('dvInspList');  
               $("#dvInspList_dvModalPopupTitle").text('Inspection List');
            });
            /*Open completed Inspection list ends here*/


            /*Open pending Inspection list starts here*/
            $("body").on("click", "#btnRenewalInspection", function () {
                var dt ="";
               document.getElementById('IframeInspList').src = "../Surveys/InspectionList.aspx?InspectionType=RenewalInspection&Status=Pending&IssueDate="+$.trim($("#txtDateOfIssue").val())+"&VesselId="+'<%=Request.QueryString["vid"]%>';
               showModal('dvInspList');  
               $("#dvInspList_dvModalPopupTitle").text('Inspection List');
            });
            /*Open pending Inspection list ends here*/

            $("body").on("click", "#btnSave", function () {
                var Msg = "";
                if ($.trim($("#txtGraceRange").val()) != "" ) {
                    var range = $.trim($("#txtGraceRange").val());
                    if (isNaN(range)) {
                        Msg += "Enter valid range";
                    }else{
                        if (range < 0) {
                                Msg += "Enter valid range";
                        }
                    }
                 }
                if ($.trim($("#txtDateOfIssue").val()) != "" || $.trim($("#txtDateOfIssue").val()) != "") {
                    if (IsInvalidDate($("#txtDateOfIssue").val(), strDateFormat)) {
                        Msg += "Enter valid Date of Issue" + ValidDateFormatMessage + "\n";
                    }
                }
                else {
                    Msg += "Enter Date of Issue\n";
                }

                if ($("#chkNoExpiry").prop("checked") == false)
                {
                    if ($.trim($("#txtDateOfExpiry").val()) != "" || $.trim($("#txtDateOfExpiry").val()) != "") {
                        if (IsInvalidDate($("#txtDateOfExpiry").val(), strDateFormat)) {
                            Msg += "Enter valid Date of Expiry" + ValidDateFormatMessage + "\n";
                        }
                    }
                    else {
                        Msg += "Enter Date of Expiry\n";
                    }
                }
                if ($.trim($("#txtReminderDate").val()) != "" || $.trim($("#txtReminderDate").val()) != "") {
                    if (IsInvalidDate($("#txtReminderDate").val(), strDateFormat)) {
                        Msg += "Enter valid Reminder Date" + ValidDateFormatMessage + "\n";
                    }
                }
                if ($.trim($("#txtExtensionDate").val()) != "" || $.trim($("#txtExtensionDate").val()) != "") {
                    if (IsInvalidDate($("#txtExtensionDate").val(), strDateFormat)) {
                        Msg += "Enter valid Extension Date" + ValidDateFormatMessage + "\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });
            $("body").on("click", "#imgAddNew", function () {
                var res;
                var vesselid = $("#hdnV_ID").val();
                var surveyvesselid = $("#hdnS_V_ID").val();
                var options = {
                    url: 'Surveylist.aspx?Method=CheckPreviousCertificate&Vessel_ID=' + vesselid + '&Surv_Vessel_ID=' + surveyvesselid,
                    dataType: 'html',
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        res = parseInt(response);
                    }
                }
                $.ajax(options);
                if (res == -1) {
                    if (window.confirm('There is an pending inspection for the renewal of certificate. Are you sure you want to proceed?')) {
                        window.open('SurveyDetails.aspx?vid=' + vesselid + '&s_v_id=' + surveyvesselid + '&s_d_id=0', '_blank');
                    }
                }
                else {
                    window.open('SurveyDetails.aspx?vid=' + vesselid + '&s_v_id=' + surveyvesselid + '&s_d_id=0', '_blank');
                }
            });
            $("body").on("click", "#ImgDeleteCertificate", function () {
                var CanDelete = 1;
                if ($("#txtVerifiedBy").length) {
                    if ($("#txtVerifiedBy").val() != '') {
                        alert("Certificate is already verified. Cannot delete verified certificate.");
                        CanDelete = -1;
                        return false;
                    }
                }
                if (CanDelete == 1) {
                    var TotalRecords = $("#hdnTotalRecords").val();
                    var currentNumber = $("#hdnCurrentRecordNumber").val();
                    currentNumber = parseInt(currentNumber) + 1;
                    var msg;
                    if (TotalRecords > 1) {
                        msg = "Are you sure, you want to delete the certificate " + $("#txtCertificateName").val() + " record " + currentNumber + "/" + $("#hdnTotalRecords").val() + "?'"; ;
                    }
                    else {
                        msg = "Are you sure, you want to delete the certificate " + $("#txtCertificateName").val() + "?'"; ;
                    }

                    if (window.confirm(msg)) {
                        var res = 1;
                        var OfficeID1 = 0;
                        var options = {
                            url: 'SurveyDetails.aspx?Method=DeleteCertificate&Surv_Details_ID=' + $("#hdnS_D_ID").val() + '&Surv_Vessel_ID=' + $("#hdnS_V_ID").val() + '&Vessel_ID=' + $("#hdnV_ID").val() + '&OfficeID=' + $("#hdnO_ID").val() + '&QueryStringOfficeId=<%=Request.QueryString["off_id"]%>',
                            dataType: 'html',
                            type: 'POST',
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                res = parseInt(response.split('|')[0]);
                                OfficeID1 = parseInt(response.split('|')[1]);
                            }
                        }
                        $.ajax(options);
                        var url = "SurveyDetails.aspx?vid=" + $("#hdnV_ID").val() + "&s_v_id=" + $("#hdnS_V_ID").val() + "&s_d_id=" + res + "&off_id=" + OfficeID1;
                        window.open(url, '_self');
                    }
                    else
                        return false;
                }

            });
            $("body").on("click", "#txtCertInspection", function () {
                if ($("#txtCertInspection").val() != '') {
                    var url = "../Technical/Inspection/SuperintendentInspection.aspx?InspectionId=" + $("#hdnCertInspection").val()+"&CertificateName="+$("#txtCertificateName").val();
                    window.open(url, "_blank");
                }
            });
            $("body").on("click", "#txtRenewwalInspection", function () {
                if ($("#txtRenewwalInspection").val() != '') {
                    var url = "../Technical/Inspection/SuperintendentInspection.aspx?InspectionId=" + $("#hdnRenewwalInspection").val()+"&CertificateName="+$("#txtCertificateName").val();
                    window.open(url, "_blank");
                }
            });
            
            $("body").on("click", "#btnCancel", function () {
                $("#closePopupbutton").click();
            });
        });
        function ShowInspectionList() {
            showModal('divInspectionList', false);
        }
        function HideScheduleList() {
            hideModal('divInspectionList');
        }
        function OpenFollowupDiv() {
           showModal("dvAddFollowUp",false);
           return false;
        }
        function CloseFollowupDiv() {
            var dvAddFollowUp = document.getElementById("dvAddFollowUp");
            dvAddFollowUp.style.display = 'none';
        }
        function showLegendHelp(evt) {
            var targObj = document.getElementById('dvLegendHelp');
            var pageX = 0;
            var pageY = 0;
            if ('pageX' in evt) { // all browsers except IE before version 9
                pageX = evt.pageX;
                pageY = evt.pageY + 25;
            }
            else {  // IE before version 9
                pageX = evt.clientX + document.documentElement.scrollLeft - targObj.clientWidth - 100;
                pageY = evt.clientY + document.documentElement.scrollTop + 25;
            }
            targObj.style.left = (pageX - targObj.clientWidth) + "px";
            targObj.style.top = pageY + "px";
            document.getElementById('dvLegendHelp').style.display = "block";
        }
        function hideLegendHelp() {
            document.getElementById('dvLegendHelp').style.display = "none";
        }
        function OpenInspectionSchedule() {
            document.getElementById('IframeSchInsp').src = "../Technical/Inspection/ScheduleInspection.aspx?Page=Calendar&VesselID=" + $("#hdnV_ID").val() + "&Surv_Details_ID=" + $("#hdnS_D_ID").val() + "&Surv_Vessel_ID=" + $("#hdnS_V_ID").val() + "&OfficeID=" + $("#hdnO_ID").val();
            showModal('dvSchInsp');
            $("#dvSchInsp_dvModalPopupTitle").html('Schedule Inspection');
            $("#dvSchInsp").css("z-index", "99999");
        }        
        function BindDate(RenewalDate, ReturnInspectionID,RenewalDateInUserDateFormat) {
            hideModal("dvSchInsp");
            $("#hdnRenewwalInspection").val(ReturnInspectionID);
            $("#txtRenewwalInspection").val(RenewalDateInUserDateFormat);
            $("#hdnRenewwalInspectionDate").val(RenewalDateInUserDateFormat);
            EnableDisableInspection();
        }
        function BindInspectionDate(InspectionType,ReturnInspectionID,RenewalDateInUserDateFormat) {
            hideModal("dvInspList");
            if ( InspectionType == "CertInspection" )
            {
                $("#hdnCertInspection").val(ReturnInspectionID);
                $("#txtCertInspection").val(RenewalDateInUserDateFormat);
                $("#hdnScheduleInspectionDate").val(RenewalDateInUserDateFormat);
                $("#txtCertInspection").css("cursor","pointer");
            }
            else
            {
                $("#hdnRenewwalInspection").val(ReturnInspectionID);
                $("#txtRenewwalInspection").val(RenewalDateInUserDateFormat);
                $("#hdnRenewwalInspectionDate").val(RenewalDateInUserDateFormat);
                $("#txtRenewwalInspection").css("cursor","pointer");
            }            
            EnableDisableInspection();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        Survey Details
    </div>
    <div id="page-content" style="overflow: auto; padding: 5px;">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelDetails" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdnV_ID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnS_V_ID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnS_D_ID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnO_ID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnCertInspection" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnRenewwalInspection" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnScheduleInspection" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnRenewwalInspectionDate" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnScheduleInspectionDate" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnRenewwalInspectionStatus" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnScheduleInspectionStatus" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnTotalRecords" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnCurrentRecordNumber" runat="server" ClientIDMode="Static" />
                <table style="width: 100%;">
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right; width: 200px">
                            <uc:Navigator ID="ctlRecordNavigation1" runat="server" OnNavigateRow="ctlRecordNavigation1_NavigateRow"
                                ClientIDMode="Static" />
                        </td>
                        <td style="text-align: right; width: 65px">
                            <asp:Image ID="imgAddNew" runat="server" ForeColor="Black" ToolTip="Add New" ClientIDMode="Static"
                                ImageUrl="~/Images/addnew.png" Style="cursor: pointer;"></asp:Image>
                        </td>
                        <td style="text-align: right; width: 20px">
                            <asp:Image ID="ImgDeleteCertificate" runat="server" ForeColor="Black" ToolTip="Delete"
                                ClientIDMode="Static" ImageUrl="~/Images/delete.png" Width="16px" Height="16px"
                                Style="cursor: pointer;"></asp:Image>
                        </td>
                        <td style="text-align: right; width: 20px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="../images/help16.png" runat="server"
                                OnClientClick="showLegendHelp(event);return false;" onmouseout="$('#dvLegendHelp').hide();">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <div style="color: Blue; text-align: center; border: 1px solid gray; margin-top: 2px;
                        background-color: #00FFFF; padding: 4px;">
                        <b>Verification and Attachments can not be done for this Survey/Certificate, untill
                            it gets imported by the vessel </b>
                    </div>
                </asp:Panel>
                <table width="100%">
                    <tr>
                        <td style="width: 45%; vertical-align: top">
                            <table border="0">
                                <tr>
                                    <td>
                                        Vessel
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVesselName" runat="server" Width="300px" CssClass="disabled"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Survey Category
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCategoryName" runat="server" Width="300px" CssClass="disabled"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Survey/Certificate Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCertificateName" runat="server" TextMode="MultiLine" Height="60px"
                                            Width="300px" CssClass="disabled" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Survey/Certificate Remarks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCertificateRemarks" runat="server" TextMode="MultiLine" Height="60px"
                                            Width="300px" CssClass="disabled" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlEditRemarks" runat="server" Visible="true">
                                            <div style="border: 1px solid gray; padding: 5px; margin-left: 5px; margin-top: 2px;">
                                                <asp:ImageButton ID="ImgBtnEditRemarks" ToolTip="Edit" runat="server" ImageUrl="~/Images/edit.gif"
                                                    OnClick="ImgBtnEditRemarks_Click"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgBtnUpdateRemarks" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                    Visible="false" OnClick="ImgBtnUpdateRemarks_Click"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgBtnCancelRemarks" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                    Visible="false" OnClick="ImgBtnCancelRemarks_Click"></asp:ImageButton>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Term
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTerm" runat="server" Width="300px" CssClass="disabled" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Make/Model
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMakeModel" runat="server" TextMode="MultiLine" Height="60px"
                                            Width="300px" CssClass="disabled" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="visibility: hidden">
                                        Issuing Authority
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIssuingAuthority" runat="server" Width="300px" CssClass="disabled"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Inspection Required?
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkInspectionRequired" Style="margin-left: -3px;" runat="server"
                                            CssClass="disabled" Enabled="false" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top">
                            <table border="0" width="100%">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <table width="103%" cellpadding="5" border="0">
                                            <tr>
                                                <td width="125px">
                                                    Date of Issue
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDateOfIssue" runat="server" CssClass="required" Width="145px"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="Calendar1" runat="server" TargetControlID="txtDateOfIssue"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date of Expiry
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDateOfExpiry" runat="server" Width="145px" CssClass="required"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="Calendar2" runat="server" TargetControlID="txtDateOfExpiry"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="hdnExpiryDate" runat="server" />
                                                    <asp:CheckBox ID="chkNoExpiry" runat="server" Text="No Expiry Date?" AutoPostBack="true"
                                                        ClientIDMode="Static" OnCheckedChanged="chkNoExpiry_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Extension Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExtensionDate" runat="server" Width="145px" ClientIDMode="Static"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExtensionDate"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Range
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGraceRange" runat="server" Width="145px" ClientIDMode="Static"></asp:TextBox>
                                                    Months
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Calculated Expiry Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCalculatedExpiryDate" runat="server" Enabled="false" Width="145px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Certificate Inspection
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCertInspection" runat="server" ReadOnly="true" Width="145px"
                                                        BackColor="#F0F0F0" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:Image ID="btnCertInspection" ImageUrl="../images/VET_AssignJob.png" runat="server"
                                                        ClientIDMode="Static" Width="16px" Height="16px" Style="vertical-align: bottom;
                                                        cursor: pointer;"></asp:Image>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Renewal Inspection Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRenewwalInspection" runat="server" ReadOnly="true" Width="145px"
                                                        BackColor="#F0F0F0" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:Image ID="btnRenewalInspection" ImageUrl="../images/VET_AssignJob.png" runat="server"
                                                        ClientIDMode="Static" Width="16px" Height="16px" Style="vertical-align: bottom;
                                                        cursor: pointer;"></asp:Image>
                                                    <asp:Image ID="btnScheduleInspection" ImageUrl="../images/InspectionCalendar.png"
                                                        ClientIDMode="Static" runat="server" Width="24" Style="vertical-align: bottom;">
                                                    </asp:Image>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top;">
                                        <table border="0" width="99%" cellpadding="5">
                                            <tr>
                                                <td width="135px">
                                                    Survey/Certificate Number
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCertificateNo" runat="server" Width="130px" MaxLength="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Issuing Authority
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAuthority" runat="server" Width="135px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Remarks
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Width="250px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Reminder Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtReminderDate" runat="server" OnTextChanged="txtReminderDate_TextChanged"
                                                        Width="130px" ClientIDMode="Static"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReminderDate"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Details
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFollowupDetails" runat="server" Width="250px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlSave" runat="server">
                    <table width="100%">
                        <tr>
                            <td width="42%">
                                <asp:Button ID="btnMakeAsNA" runat="server" Text="Make Survey/Certificate as NA"
                                    OnClientClick="return confirm('Are you sure, you want to mark the survey/certificate as NOT APPLICABLE?')"
                                    OnClick="btnMakeAsNA_Click" />
                            </td>
                            <td width="50%">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text=" Save " OnClick="btnSave_Click" ValidationGroup="ValidationCheck"
                                                ClientIDMode="Static" />
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlVerify" runat="server">
                                                <table>
                                                    <tr>
                                                        <td id="tdVerify" runat="server">
                                                            <asp:Button ID="btnConfirmVerify" runat="server" CommandArgument="1" Text="Verify"
                                                                OnClick="btnConfirmVerify_Click" OnClientClick="return confirm('Are you sure, you want to mark the survey as VERIFIED?')" />
                                                            <asp:Button ID="btnConfirmUnVerify" runat="server" CommandArgument="0" Text="Unverify"
                                                                OnClick="btnConfirmVerify_Click" OnClientClick="return confirm('Are you sure, you want to mark the survey as NOT VERIFIED?')" />
                                                        </td>
                                                        <td>
                                                            Verified By
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtVerifiedBy" runat="server" CssClass="disabled" Enabled="false"
                                                                ClientIDMode="Static"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Date Verified
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDateVerified" runat="server" CssClass="disabled" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlFollowup" runat="server" Style="margin-top: 10px;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr style="height: 10px">
                            <td class="gradiant-css-orange" style="font-weight: bold; font-size: 14px; width: 45%;
                                padding: 2px;">
                                <table style="margin-left: 2px;" width="99%">
                                    <tr>
                                        <td>
                                            Follow Up:
                                        </td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="ImgBtnAddFollowup" runat="server" CssClass="non-printable" ImageUrl="~/Images/AddFollowup.png"
                                                OnClientClick="OpenFollowupDiv();return false;" Style="margin-bottom: -4px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td class="gradiant-css-orange" style="font-weight: bold; font-size: 14px; width: 54%;
                                padding: 2px;">
                                <table style="margin-left: 2px;" width="99%">
                                    <tr>
                                        <td>
                                            Attachments:
                                        </td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="btnAdd" runat="server" CssClass="non-printable" ImageUrl="~/Images/AddNew.png"
                                                OnClick="btnAdd_Click" Style="margin-bottom: -4px;" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="lblSizeAllowed" runat="server" Visible="false"></asp:Label>
                                <asp:HiddenField ID="hdnSizeAllowed" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height: 200px; border: 1px solid #CCC; overflow: auto;">
                                    <asp:GridView ID="grdFollowUp" runat="server" AllowPaging="false" AllowSorting="false"
                                        AutoGenerateColumns="false" BackColor="White" BorderStyle="None" CellPadding="4"
                                        EnableModelValidation="True" GridLines="None" Width="100%" CssClass="gridmain-css">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Created By" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreated_By" runat="server" Text='<%#Eval("Created_By") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Created Date" ItemStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreated_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Created_Date"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Followup" ItemStyle-HorizontalAlign="Left"
                                                SortExpression="Survey_Cert_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFollowup" runat="server" Text='<%#Eval("Followup") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <label id="Label1" runat="server">
                                                No followup found !!</label>
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                        <PagerStyle CssClass="pager" Font-Size="16px" />
                                    </asp:GridView>
                                </div>
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td>
                                <div style="height: 200px; border: 1px solid #CCC;">
                                    <asp:Panel ID="pnlAttachment" runat="server" Visible="false">
                                        <div style="background: #aabbdd">
                                            <%-- <asp:FileUpload ID="UploadAttachments1" runat="server" />
                                           <asp:Button ID="btnUploadAttachments1" runat="server" Text="Upload" Height="22px" />--%>
                                        </div>
                                    </asp:Panel>
                                    <div style="overflow: auto; height: 200px;">
                                        <asp:GridView ID="grdAttachments" runat="server" AllowPaging="false" AllowSorting="true"
                                            AutoGenerateColumns="false" BackColor="White" BorderStyle="None" CellPadding="4"
                                            EnableModelValidation="True" GridLines="None" Width="100%" ShowHeader="true"
                                            OnRowDataBound="grdAttachments_RowDataBound" CssClass="gridmain-css">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Attachment" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lblAttach_Name" runat="server" NavigateUrl='<%#Eval("Attach_Name") %>'
                                                            Target="_blank" Text='<%#Eval("Attach_Name") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Size" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Document Type"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocType" runat="server" Text='<%#Eval("DocumentType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Issue Date" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfIssue"))) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Uploaded By" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("Created_By") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Uploaded Date"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Created_Date"))) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Remarks" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgRemarks" Height="12px" runat="server" ImageUrl="~/purchase/Image/view1.gif"
                                                            AlternateText='<%#Eval("Remarks") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgDeleteFile" runat="server" ForeColor="Black" ToolTip="Delete"
                                                            ImageUrl="~/Images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                            OnCommand="ImgDeleteFile_Click" CommandArgument='<%# Eval("Vessel_ID")+ ";" +Eval("Surv_Details_ID") +";"+Eval("SurvVessel_Att_ID")%>'
                                                            Width="16px" Height="16px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <label id="Label1" runat="server">
                                                    No Attachment found !!</label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <PagerStyle CssClass="pager" Font-Size="16px" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                    text-align: left; font-size: 12px; color: Black; width: 25%">
                    <table width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td align="left" style="width: 100px;">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Document Type
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDocType" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Upload File
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:FileUpload ID="UploadAttachments" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Issue Date
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIssueDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderIssueDate" runat="server" TargetControlID="txtIssueDate"
                                    Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remarks
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAtchRemarks" runat="server" Width="300px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnUploadAttachments" runat="server" Text="Add" OnClick="btnUploadAttachments_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                <div id="dvAddFollowUp" title="New Followup" style="display: none; font-family: Tahoma;
                    text-align: left; font-size: 12px; color: Black; width: 29%">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: center">
                                <b>Date:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblDate" runat="server" Enabled="false" />
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnSaveFollowUpAndClose" Text="Save And Close" runat="server" ValidationGroup="V1"
                                    OnClick="btnSaveFollowUpAndClose_OnClick" />
                                <asp:Button ID="btnCloseFollowup" Text="Close" runat="server" OnClick="btnCloseFollowup_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" style="background-color: #aabbee; padding: 2px; text-align: center;">
                                &nbsp;&nbsp;<b>Message:</b>&nbsp;&nbsp;
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="3" style="border: 1px solid inset; background-color: #aabbee;
                                text-align: center;">
                                <asp:TextBox ID="txtFollowUp" runat="server" TextMode="MultiLine" Height="200px"
                                    Width="525px" Style="margin: 10px; resize: none;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_txtFollowUp" runat="server" ControlToValidate="txtFollowUp"
                                    ValidationGroup="V1" ErrorMessage="Enter followup details" InitialValue=""></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConfirmVerify" />
                <asp:PostBackTrigger ControlID="btnConfirmUnVerify" />
                <asp:PostBackTrigger ControlID="btnUploadAttachments" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid #cccccc;
            margin-top: 5px; background-color: #FDFDFD">
        </div>
    </div>
    <div id="dvLegendHelp" style="display: none; top: 5px; position: absolute; left: 10px;
        border: 2px solid #aabbee; background-color: #ECF6FF; padding: 3px;">
        <div style="padding: 2px; width: 150px; margin-right: 2px;" class="Legend">
            Legend :
        </div>
        <div style="text-align: center;">
            <div style="padding: 2px; width: 150px; margin-right: 2px;" class="Overdue">
                Overdue</div>
            <div style="padding: 2px; width: 150px; margin-right: 2px;" class="Due0-30">
                Due in next 30 days</div>
            <div style="padding: 2px; width: 150px; margin-right: 2px;" class="Due30-90">
                Due in 30~90 days</div>
            <div style="padding: 2px; width: 150px; margin-right: 2px;" class="Done30">
                Done within last 30 days</div>
        </div>
    </div>
    <div id="dvSchInsp" style="display: none; width: 1000px;" title="Schedule Inspection">
        <iframe id="IframeSchInsp" src="" frameborder="0" style="height: 550px; width: 99%">
        </iframe>
    </div>
    <div id="dvInspList" style="display: none; width: 1000px;" title="Inspection List">
        <iframe id="IframeInspList" src="" frameborder="0" style="height: 500px; width: 99%">
        </iframe>
    </div>
</asp:Content>
