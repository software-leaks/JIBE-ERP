<%@ Page Title="Crew Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="CrewDetails.aspx.cs" Inherits="Crew_CrewDetails" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <!--Stylesheets-->
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/JTree/jquery.treeview.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <!--Javascript Files-->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/notifier.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/CrewInterview_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewDetails_Common.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/CrewDetails_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/CustomAsyncDropDown.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function ShowAddBriefPlan(CrewID) {
            document.getElementById('IframePlan').src = 'BriefingPlanner.aspx?ID=' + CrewID;           
            $("#dvPlan").prop('title', 'Plan Briefing');
            showModal('dvPlan');
            return false;
       }
       function ShowAddBriefPlan(CrewID) {
            document.getElementById('IframePlan').src = 'BriefingPlanner.aspx?ID=' + CrewID;           
            $("#dvPlan").prop('title', 'Plan Briefing');
            showModal('dvPlan');
            return false;
       }
        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }
         function DwnldCrwDoc_Click() {
        
            document.getElementById('ifrmDwnldCrwDoc').src = 'CrewDocExport.aspx?CrewID=' + <%=GetCrewID()%> ;
            showModal('dvDwnldCrwDoc');
            return false;
        }

        function ReferenceCheck(ReferenceCheckCount, CrewID,Page) {
           if (ReferenceCheckCount == 0) {
               alert('Add atleast one Referrer in Pre-Joining Exp. tab in the Crew Details page');
               return false;
           }
           else {
               if (Page == 'InterviewPlanner') {
                   document.getElementById('IframePlan').src = 'InterviewPlannerPopup.aspx?ID=' + CrewID;
                   $("#dvPlan").prop('title', 'Plan Interview');
                   showModal('dvPlan');
                   return false;
             //  window.open("InterviewPlanner.aspx?ID=" + CrewID+"&Mode=Add", '_self', false);
               }
               else {
                   window.open("CrewApproval.aspx?ID=" + CrewID);
               }
               return true;
           }
       }

       function DateValidation() {
            if (PassportDateValidation() == true)
                return SeamanDateValidation();
            else
                return false;
        }
      
        function PassportDateValidation() {
            var v = $("[id$='txtPassport_IssueDate']").val();
            var v1 = $("[id$='txtPassport_ExpDate']").val();
            if (v != "" && v1 != "") {
               var Issuedatearray = v.split("/");
                var newIssuedate = Issuedatearray[1] + '/' + Issuedatearray[0] + '/' + Issuedatearray[2];

                var Expirydatearray = v1.split("/");
                var newExpirydate = Expirydatearray[1] + '/' + Expirydatearray[0] + '/' + Expirydatearray[2];

                if (new Date(newIssuedate) > new Date(newExpirydate)) {
                    alert('Passport Issue date cannot be greater than Expiry Date');
                    return false;
                }
            }
            return true;
        }
        function SeamanDateValidation() {
            var v = $("[id$='txtSeamanBk_IssueDate']").val();
            var v1 = $("[id$='txtSeamanBk_ExpDate']").val();
            if (v != "" && v1 != "") {
                var Issuedatearray = v.split("/");
                var newIssuedate = Issuedatearray[1] + '/' + Issuedatearray[0] + '/' + Issuedatearray[2];

                var Expirydatearray = v1.split("/");
                var newExpirydate = Expirydatearray[1] + '/' + Expirydatearray[0] + '/' + Expirydatearray[2];

                if (new Date(newIssuedate) > new Date(newExpirydate)) {
                    alert('Seaman Book Issue date cannot be greater than Expiry Date');
                    return false;
                }
            }
            return true;
        }    

        /* 
        1.After closing the popup src for the popup is removed
        2.If the pop up open is Add Perpatual Document, Document page should get refreshed after adding/updating
        */
        $(document).ready(function(){
           $("body").on("click","#closePopupbutton",function(){
                var stringValue = document.getElementById("frPopupFrame").src;

                if (stringValue.toLowerCase().indexOf("crewprepdocuments.aspx")) {
                    ReloadDocuments(<%=GetCrewID()%>);
                 }
                $("#frPopupFrame").attr("src","");
           });
        });

    </script>
    <script language='javascript' type='text/javascript'>

        $(document).ready(function () {

            SelectTab(3);

           
            var wh = '<%=Request.QueryString["ID"]%>'
            Get_Crew_Information(wh);

        });
       
        function funHitMap() {

            var wh = '<%=Request.QueryString["ID"]%>'
            Get_Crew_Information(wh);
        }

        function confirmation(msg) {
             if (confirm(msg)) {
                    document.getElementById('<%= btnOk.ClientID %>').click();
                }
            }
    </script>
    <style type="text/css">
        #ctl00_MainContent_Panel1 tr
        {
            height: 25px;
        }
        /*Maintaining the position of the tabs*/
        .ui-tabs .ui-tabs-nav li
        {
            list-style: none;
            float: left;
            top: 1px;
            margin: 0 .2em 1px 0 !important;
            border-bottom: 0 !important;
            padding: 0;
            white-space: nowrap;
        }
        .WhiteBackGround
        {
            background-color: #fff;
        }
        .main
        {
            margin: 0;
        }
        .page-content-main
        {
            background-color: #f1f1f1;
        }
        .ui-tabs
        {
            padding: 0px;
        }
         .CrewImage{padding:3px;border: 1px solid #c6c6c6 !important;max-width:105px;}
        .hide-CustomDropDown{z-index:0 !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvPageContent" class="page-content-main">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                            color: black">
                            <img src="../Images/loaderbar.gif" alt="Please Wait" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:HiddenField ID="hdnPageTitle" runat="server" />
                <asp:HiddenField ID="HiddenField_SelectTab" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hdnVoyageID" runat="server" />
                <asp:HiddenField ID="hdnDocVoyageID" runat="server" Value="0" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel_PersonalDetails" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvPersonalDetails" style="display: block; min-height: 222px;">
                    <div id="dvErrorMain" class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMessagePersonalDetails" runat="server"></asp:Label>
                    </div>
                    <asp:Panel runat="server">
                        <fieldset style="text-align: left; padding: 2px;">
                            <legend style="width: 100%; height: 25px;">
                                <div style="line-height: 25px;">
                                    <div style="width: auto; float: left;">
                                        <asp:Label ID="lblStaffRank" runat="server" Style="margin-left: 5px"></asp:Label>
                                        <asp:Label ID="lblPDLegend" CssClass="toolTipText" runat="server"></asp:Label>&nbsp;&nbsp;
                                        <asp:LinkButton ID="lnkEditPersonalDetails" runat="server" OnClick="lnkEditPersonalDetails_Click"
                                            Style="text-decoration: none;">[Edit]</asp:LinkButton>
                                    </div>
                                    <div id="divCrewAction" runat="server" style="float: right;">
                                        <asp:LinkButton ID="lnkNTBR" runat="server" CssClass="inline-edit">[Mark Crew as Active/Inactive]</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDwnldCrwDoc" runat="server" CssClass="inline-edit" OnClientClick="DwnldCrwDoc_Click();">[Download Crew Documents]</asp:LinkButton>
                                        <asp:Label ID="lblCrewStatus" runat="server" Text="" BackColor="Red" ForeColor="Yellow"
                                            Font-Bold="true"></asp:Label>
                                        <asp:LinkButton ID="lnkResetCrewPassword" runat="server" CssClass="inline-edit" Text="&nbsp;[RESET PASSWORD]"
                                            OnClick="lnkResetCrewPassword_Click" CommandArgument="0" OnClientClick="return confirm('This will Reset Crew Password. Are you sure, you want reset the password ?')"></asp:LinkButton>
                                        <table style="float: right; margin-top: -3px;">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnRefreshCrewCardStatus" runat="server" Text="Refresh" OnClick="btnRefreshCrewCardStatus_Click"
                                                        Style="display: none;" />
                                                    <asp:Button ID="btnRefreshCrewStatus" runat="server" Text="Refresh" OnClick="btnRefreshCrewStatus_Click"
                                                        Style="display: none;" />
                                                </td>
                                                <td>
                                                    <div style="background-color: Yellow;" onclick="ProposeYellowCard(<%=GetCrewID()%>);return false;">
                                                        <asp:ImageButton ID="imgCardStatus" ImageUrl="" runat="server" ImageAlign="AbsMiddle"
                                                            Visible="false" />
                                                        <asp:Label ID="lblCardStatus" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </legend>
                            <table style="width: 100%; height: 100%;" class="dataTable WhiteBackGround"
                                id="pnlView_PersonalDetails" runat="server" visible="false">
                                <tr>
                                    <td style="width: 29%; vertical-align: top;">
                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td style="width: 70px">
                                                    First Name
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblGivenName" CssClass="toolTipText" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Surname
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblSurname" CssClass="toolTipText" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Staff Code
                                                </td>
                                                <td class="data" style="width: 150px">
                                                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Nationality
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblNationality" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date of Birth
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 29%">
                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td style="width: 110px">
                                                    Telephone
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Mobile
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Email
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblMail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="tdHireDatelbl" runat="server">
                                                <td>
                                                    Hire Date
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblHireDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Available From
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblDtAvailablefrom" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 30%">
                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td style="width: 110px;">
                                                    Applied Rank
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblAppliedRank" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Vessel Types
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblVesselTypes" Style="cursor: pointer" onmouseover="funVesselTooltip(event,this);"
                                                        runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Manning Office
                                                </td>
                                                <td class="data">
                                                    <asp:Label ID="lblManningOffice" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="tdUnion">
                                                <td>
                                                    Union
                                                </td>
                                                <td>
                                                    <table style="margin: -5px 0px -10px -5px" cellpadding="5" class="dataTable" cellspacing="0"
                                                        width="104%">
                                                        <tr>
                                                            <td class="data" width="40%">
                                                                <asp:Label ID="lblUnion" CssClass="toolTipText" runat="server" Width="100px"></asp:Label>
                                                            </td>
                                                            <td width="18%">
                                                                Branch
                                                            </td>
                                                            <td class="data">
                                                                <asp:Label ID="lblUnionBranch"  CssClass="toolTipText" runat="server" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td>
                                                    <asp:Label Text="Permanent?" ID="tdlblPermanent" runat="server" />
                                                </td>
                                                <td>
                                                    <table style="margin: -5px 0px -10px -5px" cellpadding="5" class="dataTable" cellspacing="0"
                                                        width="104%">
                                                        <tr>
                                                            <td class="data" width="40%" id="tdtxtPermanent" runat="server">
                                                                <asp:Label ID="lblPermanent" runat="server" Width="100px"></asp:Label>
                                                            </td>
                                                            <td width="18%" id="tdlblUnionBook" runat="server">
                                                                Book
                                                            </td>
                                                            <td class="data" id="tdtxtUnionBook" runat="server">
                                                                <asp:Label ID="lblBook" runat="server" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: center;vertical-align: top">
                                        <table style="margin-left: 30px">
                                            <tr style="height: 138px;">
                                                <td>
                                                    <asp:ImageButton ID="imgCrewPic" CssClass="CrewImage" ImageUrl="" runat="server" Style="display: none;"
                                                         OnClientClick="UploadCrewPhoto();return false;" AlternateText="Click to Change" />
                                                    <asp:ImageButton ID="imgNoPic"  CssClass="CrewImage" ImageUrl="~/Images/NoPhoto.png" runat="server" Style="display: none;"
                                                        OnClientClick="UploadCrewPhoto();return false;" AlternateText="Click to Change" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="dvCrewInformation">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table class="dataTable WhiteBackGround" style="width: 100%; height: 100%;"
                                id="pnlEdit_PersonalDetails" runat="server" visible="false">
                                <tr>
                                    <td valign="top">
                                        <table width="100%" cellpadding="5" cellspacing="6">
                                            <tr>
                                                <td style="width: 115px">
                                                    First Name<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPD_Givenname" runat="server" CssClass="control-edit required uppercase"
                                                        MaxLength="100" Width="160px"></asp:TextBox>
                                                         <ajaxToolkit:FilteredTextBoxExtender ID="fltName" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    TargetControlID="txtPD_Givenname" ValidChars=" ">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Surname
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPD_Surname" runat="server" CssClass="control-edit uppercase"
                                                        MaxLength="100" Width="160px"></asp:TextBox>
                                                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    TargetControlID="txtPD_Surname" ValidChars=" ">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Staff Code
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStaffCode" ReadOnly="true" runat="server" Enabled="false" CssClass="control-edit uppercase"
                                                        Width="160px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Nationality<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPD_Nationality" runat="server" Width="160px" CssClass="control-edit required">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date of Birth<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPD_DOB" runat="server" Width="160px" CssClass="control-edit required"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtPD_DOB"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table width="100%" cellpadding="5" cellspacing="6">
                                            <tr>
                                                <td width="115px">
                                                    Telephone
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPD_Phone" runat="server" CssClass="control-edit " MaxLength="50"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1"
                                                        TargetControlID="txtPD_Phone" FilterType="Numbers,Custom" ValidChars="+,-,,">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Mobile<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMobilePhone" MaxLength="50" runat="server" CssClass="control-edit required"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2"
                                                        TargetControlID="txtMobilePhone" FilterType="Numbers,Custom" ValidChars="+,-,,">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Email<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="control-edit required" 
                                                        MaxLength="250"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="None" ValidationGroup="Email"
                                                     ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                      ControlToValidate="txtEmailID" ErrorMessage="Please enter valid Email ID">
                                                      </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr id="trHireDatetxt" runat="server">
                                                <td>
                                                    Hire Date<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHireDates" CssClass="control-edit required" runat="server"></asp:TextBox>
                                                    <tlk4:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtHireDates">
                                                    </tlk4:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Available From<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDateAvailableFrom" runat="server" CssClass="control-edit required"
                                                        Width="150px"></asp:TextBox>
                                                    <tlk4:CalendarExtender ID="CalendarExtender11" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateAvailableFrom">
                                                    </tlk4:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table width="85%" cellpadding="5" cellspacing="6" border="0">
                                            <tr>
                                                <td style="width: 115px">
                                                    Applied Rank<span class="mandatory">*</span>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:DropDownList ID="ddlRankAppliedFor" runat="server" CssClass="control-edit required"
                                                        Width="160px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Vessel Types:<span class="mandatory" id="spnVesselTypeMandatory" runat="server" visible="false">*</span>
                                                </td>
                                                <td style="width: 50%">
                                                    <ucDDL:ucCustomDropDownList ID="ddlVesselType" runat="server" Height="150" UseInHeader="False"
                                                        Width="160" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Manning Office<span class="mandatory">*</span>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:DropDownList ID="ddlManningOffice" runat="server" CssClass="control-edit required"
                                                        Width="160px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trtxtUnion">
                                                <td>
                                                    Union<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <table style="margin: -7px 0 -10px" class="dataTable" width="115%">
                                                        <tr>
                                                            <td width="48%">
                                                                <asp:DropDownList ID="ddlUnion" runat="server" AutoPostBack="True" CssClass="control-edit required"
                                                                    OnSelectedIndexChanged="ddlUnion_SelectedIndexChanged" Width="160px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="18%">
                                                                Branch<span class="mandatory">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUnionBranch" runat="server" CssClass="control-edit required"
                                                                    Width="160px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="height: 25px">
                                                <td>
                                                    <asp:Label Text="Permanent?" ID="tdtxtPermanentEdit" runat="server" />
                                                </td>
                                                <td>
                                                    <table style="margin: -7px 0 -10px" class="dataTable" width="115%">
                                                        <tr>
                                                            <td width="60%">
                                                                <asp:DropDownList ID="ddlPermamnent" runat="server" CssClass="control-edit" Width="160px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="18%" id="tdlblUnionBookEdit" runat="server">
                                                                Book<span class="mandatory">*</span>
                                                            </td>
                                                            <td id="tddrpUnionBookEdit" runat="server">
                                                                <asp:DropDownList ID="ddlUnionBook" runat="server" CssClass="control-edit required"
                                                                    Width="160px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="bottom">
                                        <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click"  clientidmode="Static"  Text=" Ok "  style="margin-bottom:5px; display:none"/>
                                        <asp:Button ID="btnSavePersonalDetails" runat="server" OnClick="btnSavePersonalDetails_Click" ValidationGroup="Email"
                                            Text=" Save " style="margin-bottom:5px;"/>
                                        <asp:Button ID="btnClosePersonalDetails" runat="server" OnClick="btnClosePersonalDetails_Click"
                                            Text="Cancel"  style="margin-bottom:5px;"/>
                                            <asp:ValidationSummary ValidationGroup="Email" runat="server" ShowSummary="false"  ShowMessageBox="true"   DisplayMode="List" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvCrewPhotoUploader" style="text-align: right; display: none; position: absolute;
            right: 20%; top: 20%; height: 300px; width: 300px; border: 2px solid #aabbdd;
            background-color: White;">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:HiddenField ID="HiddenField_PhotoUploadPath" runat="server" />
                        <asp:HiddenField ID="HiddenField_DocumentUploadPath" runat="server" />
                        <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
                        <asp:HiddenField ID="HiddenField_CurrentRank" runat="server" />
                        <asp:HiddenField ID="HiddenField_UserID" runat="server" />
                        <asp:HiddenField ID="HiddenField_ReferenceCount" runat="server" />
                        <asp:HiddenField ID="HiddenField_NationalityId" runat="server" />
                        <input id="fileInput" name="fileInput" type="file" />
                    </td>
                    <td style="vertical-align: top; width: 20px;">
                        <img src="../scripts/uploadify/cancel.png" alt="Close" onclick="javascript: $('#dvCrewPhotoUploader').hide();" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvCrewDocumentUploader" style="text-align: right; display: none; position: absolute;
            right: 20%; top: 40%; height: 300px; width: 300px; border: 2px solid #aabbdd;
            background-color: White;">
            <table style="width: 100%">
                <tr>
                    <td>
                        <input id="crewDocumentInput" name="crewDocumentInput" type="file" />
                    </td>
                    <td style="vertical-align: top; width: 20px;">
                        <img src="../scripts/uploadify/cancel.png" alt="Close" onclick="javascript: $('#dvCrewDocumentUploader').hide();" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="tabs" style="margin-top: 5px; min-height: 450px;" class="ui-tabs-hide">
            <ul>
                <li><a href="#fragment-0"><span>Personal Details</span></a></li>
                <li><a href="#fragment-1"><span>Pre-Joining Exp.</span></a></li>
                <li><a href="#fragment-6"><span>Interview/Briefing</span></a></li>
                <li><a href="#fragment-2"><span>Service/Contract/Wages</span></a></li>
                <li><a href="#fragment-4"><span>Documents</span></a></li>
                <li><a href="#fragment-5"><span>Next of Kin</span></a></li>
                <li><a href="#fragment-7"><span>Bank Acc</span></a></li>
                <li><a href="#fragment-8"><span>Feedback/Remark</span></a></li>
                <li><a href="#fragment-9"><span>Crew Complaints</span></a></li>
                <li><a href="#fragment-10"><span>Evaluations</span></a></li>
                <li><a href="#fragment-12"><span>Log</span></a></li>
                <li><a href="#fragment-13"><span>Travel History</span></a></li>
                <li><a href="#fragment-14"><span>Training</span></a></li>
                <li><a href="#fragment-15"><span>Crew Queries</span></a></li>
                <li><a href="#fragment-16"><span>Medical History</span></a></li>
                <li><a href="#fragment-17"><span>Maintenance Feedback</span></a></li>
                <li><a href="#fragment-18"><span>Crew Matrix</span></a></li>
                <li><a href="#fragment-19"><span>FBM</span></a></li>
                <li><a href="#fragment-20"><span>Seniority</span></a></li>
                <li><a href="#fragment-21"><span>Confidential</span></a></li>
                <%--   <li><a href="#fragment-22"><span>Personal Details</span></a></li>--%>
            </ul>
            <div id="fragment-0" style="padding: 2px; display: block">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="fragment-0-tool" style="max-height: 400px; overflow: auto;">
                            <div id="frmPersonal" style="overflow: auto;">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="fragment-1" style="padding: 2px; display: block;">
                <asp:UpdatePanel ID="UpdatePanel_PreviousContacts" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="fragment-1-tool" style="text-align: right;">
                            <asp:ImageButton runat="server" ID="ImgReloadPreJoining" ImageUrl="~/Images/reload.png"
                                OnClick="ImgReloadPreJoining_Click" />
                        </div>
                        <iframe id="iFrame1" src="CrewDetails_Prejoining.aspx?ID=<%=GetCrewID() %>" style="width: 100%;
                            height: 450px; border: 0px;"></iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="fragment-2" style="padding: 2px; display: block">
                <div id="fragment-2-tool" style="text-align: right;">
                </div>
                <div id="fragment-2-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvContent_Voyages">
                    </div>
                </div>
            </div>
            <div id="fragment-4" style="padding: 2px; display: block">
                <iframe id="iFrame_Documents" src="CrewDetails_Documents.aspx?ID=<%=GetCrewID() %>"
                    style="width: 100%; height: 450px; border: 0px;"></iframe>
            </div>
            <div id="fragment-5" style="padding: 2px; display: block">
                <asp:UpdatePanel ID="UpdatePanel_Dependent" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="fragment-5-tool" style="text-align: right;">
                            <asp:ImageButton runat="server" ID="ImgAddNewDependent" ImageUrl="~/Images/AddDependent.png"
                                OnClientClick="AndNewDependent($('[id$=HiddenField_CrewID]').val());return false;" />
                            <asp:ImageButton runat="server" ID="ImgReloadDependents" ImageUrl="~/Images/reload.png"
                                OnClientClick="GetCrewNOKAndDependents($('[id$=HiddenField_CrewID]').val());return false;" />
                        </div>
                        <div id="fragment-5-content" style="max-height: 400px; overflow: auto;">
                            <div id="dvContent_NOK_Dependent" style="overflow: auto;">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="fragment-6" style="padding: 2px; display: block">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div id="fragment-6-tool" style="text-align: right;">
                            <asp:ImageButton runat="server" ID="ImgBtnRecommendation" ImageUrl="~/Images/ReRecommend.png"
                                OnClientClick="return confirm('This will remove your earlier interview and plan new interview. Do you want to proceed?')"
                                OnClick="ImgBtnRecommendation_Click" />
                            <asp:ImageButton runat="server" ID="btnUndoRejection" ImageUrl="~/Images/UndoRejection.png"
                                OnClientClick="return confirm('Are you sure, you want to UNDO Rejection ?')"
                                OnClick="btnUndoRejection_Click" />
                            <asp:ImageButton runat="server" ID="ImgBtnCrewApproval" ImageUrl="~/Images/CrewApproval.png"
                                OnClientClick="ReferenceCheck($('[id$=HiddenField_ReferenceCount]').val(),$('[id$=HiddenField_CrewID]').val(),'CrewApproval');return false;" />
                            <asp:ImageButton runat="server" ID="imgBtnBrief" ImageUrl="~/Images/PlanBriefing.png"
                                OnClientClick="return ShowAddBriefPlan($('[id$=HiddenField_CrewID]').val());"
                                alt="Plan Briefing" />
                            <asp:ImageButton runat="server" ID="ImgBtnPlanInterview" ImageUrl="~/Images/PlanInterview.png"
                                OnClientClick="ReferenceCheck($('[id$=HiddenField_ReferenceCount]').val(),$('[id$=HiddenField_CrewID]').val(),'InterviewPlanner');return false;"
                                alt="Plan new Interview" />
                            <asp:ImageButton runat="server" ID="ImageButton4" ImageUrl="~/Images/reload.png"
                                OnClick="ImageButton4_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="fragment-6-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvInterviewResult" style="max-height: 400px; overflow: auto;">
                    </div>
                </div>
            </div>
            <div id="fragment-7" style="padding: 2px; display: block">
                <div id="fragment-7-tool" style="text-align: right;">
                    <asp:ImageButton runat="server" ID="imgAddBankAcc" ImageUrl="~/Images/plus2.png"
                        OnClientClick="InsertCrewBankAcc($('[id$=HiddenField_CrewID]').val());return false;" />
                    <asp:ImageButton runat="server" ID="imgReloadBankAcc" ImageUrl="~/Images/reload.png"
                        OnClientClick="GetCrewBankAcc($('[id$=HiddenField_CrewID]').val());return false;" />
                </div>
                <div id="fragment-7-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvCrewBankAccounts">
                    </div>
                </div>
            </div>
            <div id="fragment-8" style="padding: 2px; display: block">
                <div id="fragment-8-tool" style="text-align: right;">
                    <asp:ImageButton runat="server" ID="ImgAddFeedBack" ImageUrl="~/Images/plus2.png"
                        OnClientClick="AddNewFeedback($('[id$=HiddenField_CrewID]').val());return false;" />
                    <asp:ImageButton runat="server" ID="ImgReloadFeedBack" ImageUrl="~/Images/reload.png"
                        OnClientClick="GetCrewFeedback($('[id$=HiddenField_CrewID]').val());return false;" />
                </div>
                <div id="fragment-8-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvCrewFeedback">
                    </div>
                </div>
            </div>
            <div id="fragment-9" style="padding: 2px; display: block">
                <div id="fragment-9-tool" style="text-align: right;">
                    <asp:ImageButton runat="server" ID="ImgReloadComplaints" ImageUrl="~/Images/reload.png"
                        OnClientClick="GetCrewComplaints($('[id$=HiddenField_CrewID]').val());return false;" />
                </div>
                <div id="fragment-9-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvContent_CrewComplaints">
                    </div>
                </div>
            </div>
            <div id="fragment-10" style="padding: 2px; display: block">
                <div id="fragment-10-tool" style="text-align: right;">
                    <asp:ImageButton runat="server" ID="btnUnplannedEval" ImageUrl="~/Images/btnUnplannedEval.png"
                        OnClientClick="NewUnPlannedEvaluation($('[id$=HiddenField_CrewID]').val());return false;" />
                    <asp:ImageButton runat="server" ID="ImgReloadEvaluations" ImageUrl="~/Images/reload.png"
                        OnClientClick="GetEvaluationResult($('[id$=HiddenField_CrewID]').val());return false;" />
                </div>
                <div id="fragment-10-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvEvaluations">
                    </div>
                </div>
            </div>
            <div id="fragment-12" style="padding: 2px; display: block">
                <div id="fragment-12-tool" style="text-align: right;">
                    <asp:ImageButton runat="server" ID="ImgReloadLog" ImageUrl="~/Images/reload.png"
                        OnClientClick="GetCrewLog($('[id$=HiddenField_CrewID]').val());return false;" />
                </div>
                <div id="fragment-12-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvCrewLog">
                    </div>
                </div>
            </div>
            <div id="fragment-13" style="padding: 2px; display: block">
                <div id="fragment-13-tool" style="text-align: right;">
                    <asp:ImageButton runat="server" ID="ImgReloadTravelLog" ImageUrl="~/Images/reload.png"
                        OnClientClick="GetCrewTravelLog($('[id$=HiddenField_CrewID]').val());return false;" />
                </div>
                <div id="fragment-13-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvContent_TravelLog">
                    </div>
                </div>
            </div>
            <div id="fragment-14" style="padding: 2px; display: block">
                <div id="fragment-14-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvContent_TrainingLog">
                    </div>
                </div>
            </div>
            <div id="fragment-15" style="padding: 2px; display: block">
                <div id="fragment-15-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvCrewQueries">
                    </div>
                </div>
            </div>
            <div id="fragment-16" style="padding: 2px; display: block">
                <div id="fragment-16-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvCrewMedHistory">
                    </div>
                </div>
            </div>
            <div id="fragment-17" style="padding: 2px; display: block">
                <div id="fragment-17-tool" style="text-align: right;">
                    <asp:ImageButton runat="server" ID="ImgReloadMaintenanceFeedBack" ImageUrl="~/Images/reload.png"
                        OnClientClick="GetCrewMaintenanceFeedback($('[id$=HiddenField_CrewID]').val());return false;" />
                </div>
                <div id="fragment-17-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvCrewMaintenanceFeedback">
                    </div>
                </div>
            </div>
            <div id="fragment-18" style="display: block; padding: 0;">
                <div id="fragment-18-tool" style="text-align: right;">
                </div>
                <div id="fragment-18-content" style="overflow: auto;">
                    <iframe id="frmCrewMatrix" runat="server" style="width: 100%; height: 420px; border: 0;">
                    </iframe>
                </div>
            </div>
            <div id="fragment-19" style="padding: 2px; display: block">
                <div id="fragment-19-tool" style="text-align: right;">
                </div>
                <div id="fragment-19-content" style="max-height: 400px; overflow: auto;">
                    <div id="dvContent_FBM">
                    </div>
                </div>
            </div>
            <div id="fragment-20" style="padding: 2px; display: block">
                <div id="fragment-20-tool" style="text-align: right;">
                </div>
                <div id="fragment-20-content" style="max-height: 400px; overflow: auto;">
                    <iframe id="frmSeniority" runat="server" style="width: 100%; height: 380px; border: 0;">
                    </iframe>
                </div>
            </div>
            <div id="fragment-21" style="padding: 2px; display: block">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="fragment-21-tool" style="max-height: 400px; overflow: auto;">
                            <div id="frmConfidential" style="overflow: auto;">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--    <div id="fragment-22" style="padding: 2px; display: block">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <div id="fragment-22-tool" style="max-height: 400px; overflow: auto;">
                            <div id="frmPersonal" style="overflow: auto;">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>--%>
        </div>
    </div>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
    <div id="Div1" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>
    </div>
    <div id="dialog" title="Event Details">
        Loading Data ...
    </div>
    <div id="Notification">
    </div>
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobber" Text="Select file" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10" />
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
    <div id="dvDwnldCrwDoc" style="display: none; width: 700px;" title="Download Crew Documents">
        <iframe id="ifrmDwnldCrwDoc" style="height: 800px; width: 700px; border: 0px; padding: 0;
            margin: 0;" frameborder="0" src=""></iframe>
    </div>
    <div id="dvPlan" style="display: none; width: 1300px;">
        <iframe id="IframePlan" src="" frameborder="0" style="height: 650px; width: 100%">
        </iframe>
    </div>
    <script>
        var CID = '<%=CID %>';
        function funVesselTooltip(ob2, ob3) {
            asyncGet_Vessel_Information_ToolTip(CID, ob2, ob3);
        }

        function UploadCrewPhoto() {
            var PhotoURL = '<%=HOST %>/Crew/CrewPhotoCropNew.aspx?ID=' + $('[id$=HiddenField_CrewID]').val() + "&Page='CrewDetails.aspx'";
            $('#dvPopupFrame').attr("Title", "Upload Photo");
            $('.content').css({ "height": "778px" });
            $('#dvPopupFrame').css({ "width": "900", "height": "600", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = '600px', this.style.width = '900px'; });
            $("#frPopupFrame").attr("src", PhotoURL);
            showModal('dvPopupFrame', false);
            return false;
        }

        $(document).ready(function () {
            $("body").on("mouseover", ".toolTipText", function () {
                var data = $(this).attr("rel");
                js_ShowToolTip(data, evt, objthis);
            });

            $("body").on("mouseout", ".toolTipText", function () {
                $("#__divMsgTooltip").hide();
                $("#__divMsgTooltip").html('');
            });
        });
    </script>
</asp:Content>
