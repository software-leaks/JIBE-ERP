<%@ Page Title="Job Details" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="PMSAddAdhocJob.aspx.cs" Inherits="Technical_PMS_PMSAddAdhocJob" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../Styles/jquery.galleryview-3.0-dev.css" />
    <script type="text/javascript" src="../../Scripts/jQueryRotate.2.2.js"></script>
    <!-- Second, add the Timer and Easing plugins -->
    <script type="text/javascript" src="../../Scripts/jquery.timers-1.2.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.galleryview-3.0-dev.js"></script>

        <style type="text/css" >
        .page
        {
            width: 1780px;
        }
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
     
        .hideDiv
        {
            display: none;
        }
        show.Div
        {
            display: block;
        }
    </style>
    <script  type="text/javascript">
        $(document).ready(function () {
            $('#txtbxSetPointDecimal').keypress(function (event) {
                return isNumber(event, this);
            });
            $(".draggable").draggable();

            $('#myGallery').galleryView();
            $('#myGallery2').galleryView({ panel_width: 1000, panel_height: 800 });

            $('.rotate-image').rotate({ bind: { click: function () { var value = 0; if (isNaN($(this).attr('angle'))) $(this).attr('angle', '90'); else value = eval($(this).attr('angle')); value += 90; $(this).attr('angle', value); $(this).rotate({ animateTo: value }) } } });

        });

        function SetSafetyCalibration() {

            var isChecked = $('[id$=chkSafetyAlarm]').is(":checked");
            if (isChecked) {
                $('#dvSafety').removeClass('hideDiv').addClass('showDiv');
            } else {
                $('#dvSafety').removeClass('showDiv').addClass('hideDiv');
            }

            isChecked = $('[id$=chkCalibration]').is(":checked");
            if (isChecked) {
                $('#dvCalibr').removeClass('hideDiv').addClass('showDiv');
            } else {
                $('#dvCalibr').removeClass('showDiv').addClass('hideDiv');
            }
        }

        //        $(document).ready(function () {
        //            $(".draggable").draggable();

        //            $('#myGallery').galleryView({ panel_width: 800, panel_height: 250 });
        //            $('#myGallery2').galleryView({ panel_width: 1000, panel_height: 700 });

        //            $('.rotate-image').rotate({ bind: { click: function () { var value = 0; if (isNaN($(this).attr('angle'))) $(this).attr('angle', '90'); else value = eval($(this).attr('angle')); value += 90; $(this).attr('angle', value); $(this).rotate({ animateTo: value }) } } });

        //            $("#showPopUp").click(function () {
        //                showModal('dvGalerryPopUp');
        //            });


        //        });


        function RegisterGralerry() {



            $(".draggable").draggable();

            $('#myGallery').galleryView({ panel_width: 800, panel_height: 250 });
            $('#myGallery2').galleryView({ panel_width: 1000, panel_height: 700 });

            $('.rotate-image').rotate({ bind: { click: function () { var value = 0; if (isNaN($(this).attr('angle'))) $(this).attr('angle', '90'); else value = eval($(this).attr('angle')); value += 90; $(this).attr('angle', value); $(this).rotate({ animateTo: value }) } } });

            $("#showPopUp").click(function () {
                showModal('dvGalerryPopUp');
            });



        }



        function divCategorylink() {
            document.getElementById("divCategory").style.display = "block";
        }

        function OpenCategoryDiv() {
            //window.open("SelectCategoryPopUp.aspx", 'anycontent', 'width=560,height=400,status');
            //var divCategory = document.getElementById("divCategory");
            //alert(divCategory);
            //divCategory.style.display = 'inline';
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
            //alert("sdsd");
            var divCategory = document.getElementById("divCategory");
            divCategory.style.display = 'none';
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'false';
        }


        function CloseFollowupDiv() {
            var dvAddFollowUp = document.getElementById("dvAddFollowUp");
            dvAddFollowUp.style.display = 'none';

        }

        function OpenFollowupDiv() {
            //document.getElementById("dvAddFollowUp").style.display = "block";

            $('[id$=txtMessage]').val("");

            showModal('dvAddFollowUp');

        }

        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }

        function isNumber(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (
            (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
            (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57) && charCode != 8)
                return false;

            return true;
        }

        function ValidationOnSave() {

            var DDLVessel = document.getElementById("ctl00_MainContent_ddlVessel").value;
            var ddlFunction = document.getElementById("ctl00_MainContent_ddlFunction").value;
            var ddlSys_location = document.getElementById("ctl00_MainContent_ddlSysLocation").value;
            var ddlSubSystem_location = document.getElementById("ctl00_MainContent_ddlSubSystem_location").value;
            var txtJobTitle = document.getElementById("ctl00_MainContent_txtJobTitle").value;
            var txtJobDesc = document.getElementById("ctl00_MainContent_txtJobDescription").value;


            var txtRaisedOn = document.getElementById("ctl00_MainContent_txtRaisedOn").value;
            var txtExpectedComp = document.getElementById("ctl00_MainContent_txtExpectedComp").value;
            var cmbRequisition = document.getElementById("ctl00_MainContent_cmbRequisition").value;

            if (DDLVessel == "0") {
                alert('Vessel is a mandatory field');
                document.getElementById("ctl00_MainContent_ddlVessel").focus();

                return false;
            }

            if (ddlFunction == "0") {
                alert('Function is a mandatory field');
                document.getElementById("ctl00_MainContent_ddlFunction").focus();

                return false;
            }

            if (ddlSys_location == "0") {
                alert('System / location is a mandatory field');
                document.getElementById("ctl00_MainContent_ddlSysLocation").focus();

                return false;
            }

            if (ddlSubSystem_location == "0") {
                alert('Sub System / location is a mandatory field');
                document.getElementById("ctl00_MainContent_ddlSubSystem_location").focus();

                return false;
            }


            if (txtJobTitle == "") {
                alert('Job Title is mandatory field');
                document.getElementById("ctl00_MainContent_txtJobTitle").focus();

                return false;
            }
            if (txtJobDesc == "") {
                alert('Job Description is mandatory field');
                document.getElementById("ctl00_MainContent_txtJobTitle").focus();

                return false;
            }

            if (txtRaisedOn == "") {
                alert('Raised On is mandatory field');
                document.getElementById("ctl00_MainContent_txtRaisedOn").focus();

                return false;
            }

            if (txtExpectedComp == "") {
                alert('Expected Completion is mandatory field');
                document.getElementById("ctl00_MainContent_txtExpectedComp").focus();

                return false;
            }
            //            if (cmbRequisition == "0") {
            //                alert('Requisition is a mandatory field')
            //                document.getElementById("ctl00_MainContent_cmbRequisition").focus();

            //                return false;
            //            }
            return true;
        }
        function ShowTypeDtl(obj) {

            if (obj.textContent == "Safety Alarm" || obj.innerText == "Safety Alarm")
                $('#dvSafety').toggle();
            if (obj.textContent == "Calibration" || obj.innerText == "Calibration")
                $('#dvCalibr').toggle();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
            <%--   <div style="font-size: 14px; background-color: #5588BB; color: White; text-align: center;
                padding: 2px 0px 2px 0px; margin: 0px 0px 0px 0px;">
                <b>Job Details </b>
            </div>--%>
            <div class="roundedBox">
                <table border="0" cellpadding="5" cellspacing="0" style="width: 100%; color: black;">
                    <tr>
                        <td align="left" valign="top" style="font-size: 14px">
                            <table cellpadding="1" cellspacing="1" width="100%">
                                <tr>
                                    <td style="width: 45px">
                                        Fleet :
                                    </td>
                                    <td style="width: 150px">
                                        <asp:DropDownList ID="ddlFleet" runat="server" Width="100px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                            AutoPostBack="true" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 55px">
                                        Vessel :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td style="width: 150px">
                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 70px">
                                        Job Code :
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox ID="txtJobCode" CssClass="txtInput" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                    </td>
                                     <td style="width: 85px">
                                        Job Card No :
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox ID="txtJobCardNo" CssClass="txtInput" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 75px">
                                        Job Type  :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJobType" CssClass="txtInput" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 75px">
                                        Job Status  :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJobStatus" CssClass="txtInput" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbRequisition" Width="150px" CssClass="txtInput" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbJobLastDone" Width="150px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="cmbJobLastDone_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 70px;">
                                        OD Days :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblODDays" runat="server" Text=""></asp:Label>
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
                    </tr>
                </table>
            </div>
            <div id="divPmsJobDetails" runat="server">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="hlnkRequisition" Target="_blank" runat="server"></asp:HyperLink>
                        </td>
                        <td style="width: 12%" align="right">
                            Department : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDepartment" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right">
                            Rank : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRank" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right" style="vertical-align: top">
                            Frequency : &nbsp;
                        </td>
                        <td align="left" style="vertical-align: top">
                            <asp:TextBox ID="txtFrequencyType" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right">
                            CMS : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcms" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right">
                            Critical : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCritical" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvJobDetails" style="background-color: White">
                <table border="0" cellpadding="2" cellspacing="5" style="width: 100%; color: black;">
                    <tr>
                        <td colspan="2" rowspan="2" align="left" style="border: 1px solid #aabbdd; padding: 2px;
                            vertical-align: top;" class="style1">
                            <table cellpadding="1" cellspacing="1" style="width: 100%">
                                <tr>
                                    <td align="left" class="row-header" colspan="3">
                                        Describe the issue or problem here:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%" align="right">
                                        Job Title :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJobTitle" Width="99%" runat="server" CssClass="txtInput" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Job Desc. :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td style="font-size: 14px">
                                        <asp:TextBox ID="txtJobDescription" runat="server" Height="400px" TextMode="MultiLine"
                                            CssClass="txtInput" Width="99.5%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; padding: 2px; vertical-align: top;"
                            class="style1">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr>
                                    <td align="left" class="row-header" colspan="3">
                                        Other Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px" align="left">
                                        PIC:
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPIC" runat="server" Style="width: 155px" CssClass="txtInput">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Assigned By :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAssignedBy" runat="server" Width="155px" CssClass="txtInput">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="162px">
                                        Defer to Dry Dock:
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoDeferToDrydock" runat="server" RepeatDirection="Horizontal"
                                            CssClass="txtInput">
                                            <asp:ListItem Text="Yes" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Priority:
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPriority" runat="server" Width="155px" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Inspector:
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInspector" runat="server" Style="width: 155px" CssClass="txtInput">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Inspection Date:
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInspectionDate" runat="server" Width="150px" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtInspectionDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px" align="left">
                                        Type:
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkSafetyAlarm" runat="server" Text="Safety Alarm" onchange="ShowTypeDtl(this);" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkCalibration" runat="server" Text="Calibration" onchange="ShowTypeDtl(this);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                <td style="width: 150px" align="left">
                                Mandatory Risk Assessment:
                                </td>
                                <td></td>
                                <td >
                                <asp:RadioButtonList ID="rbtMRA" runat="server" RepeatDirection="Horizontal"
                                            CssClass="txtInput" Enabled="False">
                                            <asp:ListItem Text="Yes" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                               <%-- <asp:Label ID="lblRAMandatory" runat="server" Font-Bold="true"></asp:Label>--%>
                                </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvCalibr" class="hideDiv" style="margin-top: -5px">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td width="162px">
                                                        Set Point:&nbsp;&nbsp;<span style="color: Red;">(Only numeric allowed)</span>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                        <div>
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-left: 5px">
                                                                        <asp:TextBox ID="txtbxSetPointDecimal" runat="server" Width="100px" CssClass="txtInput"
                                                                            MaxLength="12" ClientIDMode="Static"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlUnit" runat="server" Style="width: 100px" CssClass="txtInput">
                                                                            <asp:ListItem Value="0" Text="-Select Unit-"></asp:ListItem>
                                                                        </asp:DropDownList>
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
                                    <td colspan="3">
                                        <div id="dvSafety" class="hideDiv" style="margin-top: -10px">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td width="162px">
                                                        Functional:
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 8px">
                                                        <asp:RadioButtonList ID="rbtnFunctional" runat="server" RepeatDirection="Horizontal"
                                                            CssClass="txtInput">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Effect:
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 8px">
                                                        <asp:DropDownList ID="ddlEffect" runat="server" Style="width: 250px" CssClass="txtInput">
                                                            <asp:ListItem Value="0" Text="-Select Unit-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtbxEffect" runat="server" Width="150px" CssClass="txtInput"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 8px">
                                                        <asp:RadioButtonList ID="rbtnJobProcess" runat="server" RepeatDirection="Vertical"
                                                            CssClass="txtInput">
                                                            <asp:ListItem Text="Automated(Unmanned)" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Non-Automated(Manned)" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border: 1px solid #aabbdd;" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="3">
                                        Categories:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 168px" align="left">
                                        Primary :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="left">
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPrimary" Width="200px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style2">
                                        Secondary :
                                    </td>
                                    <td style="color: #FF0000;" align="left" class="style3">
                                    </td>
                                    <td class="style2">
                                        <asp:DropDownList ID="ddlSecondary" Width="200px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style2">
                                        PSC/SIRE Code :
                                    </td>
                                    <td style="color: #FF0000;" align="left" class="style3">
                                    </td>
                                    <td class="style2">
                                        <asp:DropDownList ID="ddlPSCSIRE" Width="200px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border: 1px solid #aabbdd; width: 33%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="3">
                                        Applicable Dates:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Raised On :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRaisedOn" Enabled="false" runat="server" Width="100px" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calRaisedOn" runat="server" Enabled="false" Format="dd/MM/yyyy"
                                            TargetControlID="txtRaisedOn">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Expected Completion :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpectedComp" CssClass="txtInput" runat="server" Width="100px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calExpectedComp" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" TargetControlID="txtExpectedComp">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Completed On:
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCompletedOn" runat="server" Enabled="false" Width="100px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calCompletedOn" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtCompletedOn">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd; width: 34%" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="3">
                                        Assigned Departments:
                                    </td>
                                </tr>
                                <tr>
                                    <td width="90px">
                                        Dept On Ship :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOnShip" runat="server" Width="105px">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="90px">
                                        Dept in Office :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInOffice" runat="server" Width="105px">
                                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border: 1px solid #aabbdd;" valign="top">
                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                <tr class="row-header">
                                    <td colspan="3">
                                        Location:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 167px" align="left">
                                        Function :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="left">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFunction" Width="200px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        System /Location :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="left">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSysLocation" Width="200px" CssClass="txtInput" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="ddlSysLocation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Subsytem / Location :
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="left">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSubSystem_location" Width="200px" CssClass="txtInput" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvButtons" style="text-align: center" runat="server">
            
                <asp:Button ID="btnSave" runat="server" Text="Save" Height="28px" Width="150px" OnClick="btnSave_Click"
                    OnClientClick="return ValidationOnSave();" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnRework" runat="server" Text="Re-Work" Height="28px" Width="150px"
                    OnClick="btnRework_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnVerify" runat="server" Text="Verify by Office" Height="28px" Width="150px"
                    OnClick="btnVerify_Click" />&nbsp;&nbsp;&nbsp;
            </div>
            <div>  <asp:UpdatePanel ID="updRAForms" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTRAF" runat="server" Text="Risk Assessment Forms:" Font-Bold="true"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td id="tdRAForms" colspan="2">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlRAForms" runat="server">
                                                                            <asp:DataList ID="dlRAForms" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                                                                EmptyDataText="NO RECORDS FOUND" RepeatLayout="Table" CellSpacing="2">
                                                                                <ItemTemplate>
                                                                                    <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="hlRAForms" runat="server" Text='<%# Eval("Work_Category_Name")%>'
                                                                                                        Font-Names="Calibri" Target="_blank" NavigateUrl='<%# "~/JRA/RiskAssessmentDetails.aspx?Assessment_ID="+Eval("Assessment_ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString() %>'>

                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                     </div>
            <div id="divAttandRemarks" runat="server" style="margin-top: 0px; font-size: 10px;
                margin: 2px;">
                <div id="fragment-0" style="padding: 0px; display: block">
                    <table cellpadding="0" cellspacing="5" width="100%">
                        <tr>
                            <td style="border: 1px solid #aabbdd; width: 50%; vertical-align: top;">
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr class="row-header">
                                        <td style="font-weight: bold;">
                                            Details of action taken (Followup) :
                                        </td>
                                        <td align="right" id="imgRemarkButton" runat="server">
                                            <asp:ImageButton ID="ImgBtnAddFollowup" runat="server" ImageUrl="~/Images/AddFollowup.png"
                                                OnClientClick="OpenFollowupDiv();return false;" CssClass="non-printable" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="max-height: 350px; min-height: 100px; overflow-y: scroll">
                                                <asp:GridView ID="grdFollowUps" runat="server" BackColor="White" BorderColor="#999999"
                                                    AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    EnableModelValidation="True" GridLines="None" Width="100%">
                                                    <AlternatingRowStyle BackColor="#DDeeEE" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date" SortExpression="DATE_CREATED" ItemStyle-Width="10%"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDATE_CREATED" runat="server" Text='<%#Eval("Date_Of_Creation","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("Date_Of_Creation","{0:dd/MMM/yy}")   %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Created By" SortExpression="LOGIN_NAME" ItemStyle-Width="15%"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lblLOGIN_NAME" runat="server" NavigateUrl='<%# "../../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                                                    Target="_blank" Text='<%# Eval("user_name")%>' CssClass="pin-it"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark" SortExpression="Remark" ItemStyle-Width="75%"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFOLLOWUP" runat="server" Text='<%#Eval("Remark").ToString().Replace("\n","<br>") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="ldl1" runat="server" Text="No followups !!"></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#ffffff" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <PagerStyle CssClass="pager" Font-Size="16px" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="border: 1px solid #aabbdd; width: 50%; vertical-align: top;">
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr class="row-header">
                                        <td>
                                            Attachments:
                                        </td>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td runat="server" id="imgAttachmentButton">
                                                        <asp:Panel ID="pnlAddAttachment" runat="server">
                                                            <asp:ImageButton ID="imgAttach" runat="server" ImageUrl="../../Images/AddAttachment.png"
                                                                OnClientClick="showModal('dvPopupAddAttachment',true,fn_OnClose);return false;" />
                                                            <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" Style="display: none"
                                                                runat="server" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td runat="server" id="tdg">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr class="row-header" style="color: #FFFFCC; background-color: #5588BB;">
                                                                <td style="font-weight: bold; color: White;" align="left">
                                                                    Image Attachments:
                                                                </td>
                                                                <td style="font-weight: bold; color: #FFFFCC;" align="right">
                                                                    <div id="showPopUp" style="cursor: pointer;">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/SearchButton.png" Height="16px" />
                                                                        Larger View
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <div id="webpage">
                                                                        <div id="retina">
                                                                        </div>
                                                                        <%-- <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>--%>
                                                                        <ul id="myGallery">
                                                                            <asp:ListView ID="ListView1" runat="server">
                                                                                <ItemTemplate>
                                                                                    <li>
                                                                                        <asp:Image ID="imgAttachment" ImageUrl='<%# "../../Uploads/PmsJobs/" + Eval("Image_Path") %>'
                                                                                            data-description='<% #Eval("Created_By_Name") %>' ToolTip='<% #Eval("ATTACHMENT_NAME") %>'
                                                                                            runat="server" MaxHeight="400" CssClass="rotate-image" />
                                                                                    </li>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </ul>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:HiddenField ID="hidenTotalrecords" runat="server" />
                                                                    <asp:HiddenField ID="HCurrentIndex" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="max-height: 250px; min-height: 0px; overflow: auto; width: 100%">
                                                            <asp:GridView ID="gvAttachments" runat="server" BackColor="White" BorderColor="#999999"
                                                                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="0px" CellPadding="3"
                                                                EnableModelValidation="True" GridLines="None" Width="100%" OnRowDataBound="gvAttachments_RowDataBound">
                                                                <AlternatingRowStyle BackColor="#DDeeEE" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAttach_Date" runat="server" Text='<%#Eval("DATE_OF_CREATION","{0:dd/MMM/yy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Attachment Name" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lblAttach_Name" runat="server" NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("ATTACHMENT_PATH") %>'
                                                                                Target="_blank" Style="cursor: pointer;" Text='<%#Eval("ATTACHMENT_NAME") %>'></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Size" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="60" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <label id="Label1" runat="server">
                                                                    </label>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle BackColor="#ffffff" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                                <PagerStyle CssClass="pager" Font-Size="16px" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div id="dvGalerryPopUp" style="display: none">
        <ul id="myGallery2">
            <asp:ListView ID="ListView2" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:Image ID="imgAttachment" ImageUrl='<%# "../../Uploads/PmsJobs/" + Eval("Image_Path") %>'
                            data-description='<% #Eval("Created_By_Name") %>' ToolTip='<% #Eval("ATTACHMENT_NAME") %>'
                            runat="server" MaxHeight="650" />
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </ul>
    </div>
                </div>
            </div>
            <div style="max-height: 150px; min-height: 0px; overflow-x: auto; overflow-y: auto;">
                <asp:Repeater runat="server" ID="gvPMSJobAttachment">
                    <HeaderTemplate>
                        <table style="width: 100%" cellpadding="1" cellspacing="0">
                            <tr style="color: Black; background-color: #5588BB">
                                <td colspan="5" style="font-weight: bold; color: White;" align="left">
                                    Job Instructions checklist
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="color: Black">
                            <td style="width: 16px">
                                <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="15px" />
                            </td>
                            <td style="padding-left: 2px; width: 10px; text-align: left;">
                                <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                            </td>
                            <td style="padding-left: 2px; width: 150px; text-align: left;">
                                <asp:HyperLink ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                    NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("ATTACHMENT_PATH") %>' Target="_blank"></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="color: Black; background-color: #E0ECF8">
                            <td style="width: 16px">
                                <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="18px" />
                            </td>
                            <td style="padding-left: 2px; width: 10px; text-align: left;">
                                <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                            </td>
                            <td style="padding-left: 2px; width: 150px; text-align: left;">
                                <asp:HyperLink ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                    NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("ATTACHMENT_PATH") %>' Target="_blank"></asp:HyperLink>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel ID="pnlCreatedByInfo" runat="server" Visible="false">
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
                                <asp:Image ID="imgVerifiedBy" runat="server" Height="30px" />
                            </td>
                            <td style="text-transform: capitalize">
                                <asp:HyperLink ID="lnkVerifiedBy" runat="server" ForeColor="Blue" CssClass="link"></asp:HyperLink>
                            </td>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvAddFollowUp" title="Add Follow-Up" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnOfficeID" runat="server" />
                <asp:HiddenField ID="hdnWorklistlID" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
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
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2"
                                Padding-Left="2" Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10"></ajaxToolkit:AjaxFileUpload>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
</asp:Content>
