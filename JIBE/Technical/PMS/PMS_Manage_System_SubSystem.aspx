<%@ Page Title="Manage System" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMS_Manage_System_SubSystem.aspx.cs" Inherits="Technical_PMS_PMS_Manage_System_SubSystem"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList" TagPrefix="ucFunction" %>
<%@ Register Src="~/UserControl/ucAsyncPager.ascx" TagName="ucAsyncPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <link href="../../Scripts/JsTree/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JsTree/libs/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/JsTree/jstree.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min1.11.0.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui1.11.0.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/PMS_Manage_System.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jsasyncpager.js" type="text/javascript"></script>
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css"
        rel="stylesheet">
    <style type="text/css">
        /*Added to display drop down and text box as same width */
        input, select, textarea
        {
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }
        /*Added to display drop down and text box as same width */
        input
        {
            text-indent: 4px;
        }
        
        .page
        {
            min-width: 600px;
            width: 98%;
        }
        .badge1
        {
            text-decoration: none;
            left: 100px;
            top: 50px;
            width: 120px;
        }
        
        .badge2
        {
            text-decoration: none;
            left: 100px;
            top: 50px;
            width: 120px;
        }
        
        .awesome
        {
            border: 1px solid #0071BC;
            display: inline-block;
            cursor: pointer;
            background: #0071BC;
            color: #FFF;
            font-size: 14px;
            padding: 10px 15px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #2f6627;
            margin-right: 5px;
            margin-bottom: 5px;
        }
        
        .awesomeInPopup
        {
            border: 1px solid #0071BC;
            display: inline-block;
            cursor: pointer;
            background: #0071BC;
            color: #FFF;
            font-size: 12px;
            padding: 6px 11px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #2f6627;
            margin-right: 5px;
            margin-bottom: 5px;
        }
        
        .box
        {
            width: 400px;
            height: 32px;
        }
        .container-4
        {
            overflow: hidden;
            width: 450px;
            vertical-align: middle;
            white-space: nowrap;
        }
        .container-4 input#txtSearchJobs
        {
            width: 390px;
            height: 32px;
            background: #FFFF99;
            border: none;
            font-size: 10pt;
            float: left;
            color: black;
            padding-left: 20px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }
        .container-4 input#txtSearchSpare
        {
            width: 390px;
            height: 32px;
            background: #FFFF99;
            border: none;
            font-size: 10pt;
            float: left;
            color: black;
            padding-left: 20px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }
        .container-4 button.icon
        {
            -webkit-border-top-right-radius: 5px;
            -webkit-border-bottom-right-radius: 5px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomright: 5px;
            border-top-right-radius: 5px;
            border-bottom-right-radius: 5px;
            border: none;
            background: #FFFF99;
            height: 33.5px;
            width: 33px;
            color: #4f5b66;
            opacity: 0;
            font-size: 10pt;
            -webkit-transition: all .55s ease;
            -moz-transition: all .55s ease;
            -ms-transition: all .55s ease;
            -o-transition: all .55s ease;
            transition: all .55s ease;
        }
        .container-4:hover button.icon, .container-4:active button.icon, .container-4:focus button.icon
        {
            outline: none;
            opacity: 1;
            margin-left: -2px;
        }
        
        .container-4:hover button.icon:hover
        {
            background: #232833;
            cursor: pointer;
        }
    </style>
    <script language="javascript" type="text/javascript">
        jQuery.fn.getNum = function () {

            /* trim the string value */
            var val = $.trim($(this).val());

            /* replace all ',' to '.' if present */
            if (val.indexOf(',') > -1) {
                val = val.replace(',', '.');
            }
            if (val == '') {

                val = "0";
            }
            /* parse the string as float */
            var num = parseFloat(val);

            /* use two decimals for the number */
            var num = num.toFixed(2);

            /* check if 'num' is 'NaN', this will happen 
            * when using characters, two '.' and apply 
            * for other cases too.
            */
            if (isNaN(num)) {
                num = '0.00';
            }

            return num;
        }
        $(function () { //onReady handler for document

            $('[id$=txtMinQty]').blur(function () { //onBlur handler for textbox



                $(this).val($(this).getNum()); //invoke your function, you can use it with other selectors too
            });

        });
        $(function () { //onReady handler for document

            $('[id$=txtMaxQty]').blur(function () { //onBlur handler for textbox


                $(this).val($(this).getNum()); //invoke your function, you can use it with other selectors too
            });

        });
        function UpdatePage(FileName) {


            // alert("In Parent");
            hideModal("dvPopupAddAttachment");
            var ItemID = document.getElementById($('[id$=hdnItemID]').attr('id')).value;

            $('#' + $('[id$=BtnTemp]').attr('id')).click();

            return false;

        }


        function UpdateDetailsPage(FileName) {

            // alert("In Parent");

            hideModal("dvPopupAddAttachment");
            var ItemID = document.getElementById($('[id$=hdnItemID]').attr('id')).value;
            $('#' + $('[id$=BtnTempDetails]').attr('id')).click();

            return false;

        }
        function ShowUploader() {
            var Path = '~/Uploads/PURC_Items/';
            if (document.getElementById($('[id$=hdnItemID]').attr('id')).value == "") {
                alert('Save Item Before adding attachment');
                //onSaveSpare();
                return false;
            }
            var ItemID = document.getElementById($('[id$=hdnItemID]').attr('id')).value;
            document.getElementById('IframeAttach').src = 'FileUploader.aspx?Path=' + Path + "&ItemID=" + ItemID + "&ImageType=Image"; /*"ImageType" is to identify which field in database is need to be updated. Image Means Image Field*/
            showModal('dvPopupAddAttachment', false, fnCloseSpareAttach);
            return false;


        }
        function fnCloseSpareAttach() {

            document.getElementById('IframeAttach').src = "";
        }
        function ShowDetailsUploader() {
            var Path = '~/Uploads/PURC_Items/';
            if (document.getElementById($('[id$=hdnItemID]').attr('id')).value == "") {
                alert('Save Item Before adding attachment');
                //onSaveSpare();
                return false;
            }
            var ItemID = document.getElementById($('[id$=hdnItemID]').attr('id')).value;
            document.getElementById('IframeAttach').src = 'FileUploader.aspx?Path=' + Path + "&ItemID=" + ItemID + "&ImageType=ProductDetailImage"; /*"ImageType" is to identify which field in database is need to be updated. ProductDetailImage Means ProductDetailImage Field*/
            showModal('dvPopupAddAttachment', false, fnCloseSpareAttach);
            return false;

        }
        function ClearUserControlText() {

            document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";
            return true;

        }

        function validationAddCatalogue() {
            document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";
            return true;

        }

        //        function validationCatalogue() {

        //            var Vessel = document.getElementById("ctl00_MainContent_DDLVessel").value;
        //            var CatalogueCode = document.getElementById("ctl00_MainContent_txtCatalogueCode").value.trim();
        //            var CatalogueName = document.getElementById("ctl00_MainContent_txtCatalogName").value.trim();
        //            var CatalogueDepartment = document.getElementById("ctl00_MainContent_ddlCatalogDept").value;
        //            var Setsinstalled = document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").value;
        //            var deptType = document.getElementById("ctl00_MainContent_hdfDeptType").value;
        //            var OperationMode = document.getElementById("ctl00_MainContent_hdfCatlogueOperationMode").value;



        //            if (OperationMode != "EDIT") {
        //                if (deptType != "ST") {
        //                    if (Vessel == "0") {
        //                        alert("Please select the vessel.");
        //                        return false;
        //                    }
        //                }
        //            }


        //            if (Setsinstalled != "") {
        //                if (isNaN(Setsinstalled)) {
        //                    alert('Set installed is accept ony numeric value.')
        //                }
        //            }

        //            if (CatalogueCode == "") {
        //                alert("Catalogue Code is requried.");
        //                document.getElementById("ctl00_MainContent_txtCatalogueCode").focus();
        //                return false;
        //            }
        //            if (CatalogueName == "") {
        //                alert("Catalogue Name is requried.");
        //                document.getElementById("ctl00_MainContent_txtCatalogName").focus();
        //                return false;
        //            }

        //            if (CatalogueDepartment == "0") {
        //                alert("Catalogue Department is required.");
        //                document.getElementById("ctl00_MainContent_hdfDeptType").focus();
        //                return false;
        //            }

        //            return true;

        //        }


        function validationSubCatalogue() {

            var SubCatalogueCode = document.getElementById("ctl00_MainContent_txtSubCatalogueCode").value.trim();
            var SubCatalogueName = document.getElementById("ctl00_MainContent_txtSubCatalogueName").value.trim();


            if (SubCatalogueCode == "") {
                alert("Sub Catalogue Code is requried.");
                document.getElementById("ctl00_MainContent_txtSubCatalogueCode").focus();
                return false;
            }
            if (SubCatalogueName == "") {
                alert("Sub Catalogue Name is requried.");
                document.getElementById("ctl00_MainContent_txtSubCatalogueName").focus();
                return false;
            }

            return true;
        }


        function validationOnAddJob() {
            var Vessel = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var jotTitle = document.getElementById("ctl00_MainContent_txtJobTitle").value.trim();


            var Frequency = document.getElementById("ctl00_MainContent_txtFrequency").value.trim();
            var Department = document.getElementById("ctl00_MainContent_lstDepartment").value;

            var deptType = document.getElementById("ctl00_MainContent_hdfDeptType").value;
            var OperationMode = document.getElementById("ctl00_MainContent_hdfJobsOperationMode").value;
            var Rank = document.getElementById($('[id$=ddlRank]').attr('id')).value;
            if (jotTitle == "") {
                alert("Job Title is required.");
                document.getElementById("ctl00_MainContent_txtJobTitle").focus();
                return false;
            }

            if (Frequency == "") {
                alert("Frequency is required.");
                document.getElementById("ctl00_MainContent_txtFrequency").focus();
                return false;
            }

            if (Frequency != "") {
                if (isNaN(Frequency)) {
                    alert('Frequency is accept ony numeric value.')
                    return false;
                }
            }
            if (Rank == "" || Rank == "0") {

                alert("Rank is required.");
                document.getElementById($('[id$=ddlRank]').attr('id')).focus();
                return false;
            }
            return true;
        }


        function CloseDiv() {
            var control = document.getElementById("ctl00_MainContent_divAddLocation");
            control.style.visibility = "hidden";
        }

        function RefreshFromchild() {

            //  document.getElementById("ctl00_MainContent_btnHiddenSubmit").click();
            Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");   //Changes done by Reshma for RA


            //  Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");
            Get_Lib_PlannedJobsCount(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");           //Changes done by Reshma for RA

        }

        function RefreshPMSJobAttachment() {

            BindJobAttachment();
        }


        function RefreshMakerFromChild() {

            // document.getElementById("ctl00_MainContent_btnMakerHiddenSubmit").click();

            BindCatSupplier();
            BindSubCatSupplier();
        }

        function OnMoveJobClick() {

            if (document.getElementById("ctl00_MainContent_DDLVessel").value == "0") {
                alert("Please select the vessel.");
                return false;
            }
            return false;

        }

        function OnAddMaker() {
            document.getElementById('Iframe').src = '../../Infrastructure/Libraries/Supplier_Maker_Add.aspx';
            showModal('dvIframe');
            return false;
        }


        function OnAddNewLocation() {

            if (document.getElementById('dvAddNewLocation').style.display == 'block') {
                document.getElementById('dvAddNewLocation').style.display = 'none';

            }
            else {
                document.getElementById('dvAddNewLocation').style.display = 'block';
                //document.getElementById('ctl00_MainContent_txtLoc_ShortCode').focus();
            }

            return false;

        }


        function OnSaveLocation() {

            //            if (document.getElementById('ctl00_MainContent_txtLoc_ShortCode').value == "") {
            //                document.getElementById('ctl00_MainContent_txtLoc_ShortCode').focus();
            //                alert("Short code is a mandatory field.");
            //                return false;
            //            }

            if (document.getElementById('ctl00_MainContent_txtLoc_Description').value == "") {
                document.getElementById('ctl00_MainContent_txtLoc_Description').focus();
                alert("Location name is a mandatory field.");
                return false;
            }
            return true;
        }


        function OpenAssignScreen(Vessel_ID, Job_ID) {
            var url = 'PMSJob_eFormAssignment.aspx?Vessel_ID=' + Vessel_ID + '&Job_ID=' + Job_ID;
            // OpenPopupWindow('AssigneForm', 'Assign eForm', url, 'popup', 530, 600, null, null, true, false, true, AssignScreen_Closed);

            OpenPopupWindowBtnID('AssigneForm', 'Assign eForm to Job', url, 'popup', 530, 600, null, null, false, false, true, null, '" + imgItemSearch.UniqueID + "');
        }

        function AssignScreen_Closed() {

            return true;
        }
        function RemoveSpecialChar(txtName) {
            txtName.value = txtName.value.replace(/[^0-9]/, '');


        }
      
        function enableordisable() {
            if (document.getElementById('ctl00_MainContent_ChkMandatoryRA').checked == true) {
                document.getElementById('ctl00_MainContent_ChkMandatoryRAApproval').disabled = false;

               
            }



            return false;
        }

        function enable_cb(cur) {
            if (cur.checked) {
             var element= document.getElementById($('[id$=ChkMandatoryRAApproval]').attr('id'))
              element.removeAttribute("disabled");        
              
            } else {
              var element= document.getElementById($('[id$=ChkMandatoryRAApproval]').attr('id'))
              element.setAttribute("disabled", true);
              if (document.getElementById('ctl00_MainContent_ChkMandatoryRA').checked == false) {
                  document.getElementById('ctl00_MainContent_ChkMandatoryRAApproval').checked = false;
              }  
            }
            return true;
        }

     
      
    
    </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title" style="margin-bottom: 3px;">
        Manage System
    </div>
    <div style="min-width: 1200px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 320px;">
                    <div style="float: left; border: 1px solid #0071BC; border-radius: 5px; width: 100%;
                        min-width: 320px; max-height: 860px; overflow: hidden;">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                            <ContentTemplate>
                                <div style="padding: 3px; background-color: #0071BC">
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2" style="color: white">
                                                <asp:RadioButtonList ID="rblDeptType" runat="server" RepeatDirection="Horizontal"
                                                    CellPadding="4" CellSpacing="0" onchange="ddlDeptType_selectinChanged()" AutoPostBack="false"
                                                    Width="100%" DataTextField="Description" DataValueField="Short_Code">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblvsl" runat="server" Text="Vessel: " ForeColor="White"></asp:Label>
                                            </td>
                                            <td style="width: 210px;">
                                                <asp:DropDownList ID="DDlVessel_List" runat="server" AutoPostBack="false" Width="205px"
                                                    DataTextField="Vessel_Name" DataValueField="Vessel_ID" onChange="DDlVessel_selectionChange()">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" ToolTip="Refresh"
                                                    ImageUrl="~/Images/Refresh-icon.png" OnClientClick="return refreshPage()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblfn" runat="server" Text="Department: " ForeColor="White"></asp:Label>
                                            </td>
                                            <td style="width: 210px;" align="left">
                                                <asp:DropDownList ID="DDLDepartment" runat="server" DataTextField="DESCRIPTION" DataValueField="CODE"
                                                    Width="205px" onChange="DDLDepartment_selectionChange()">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Search Title: " ForeColor="White"></asp:Label>
                                            </td>
                                            <td style="width: 200px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="205px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgFilterTree" runat="server" Height="20px" ToolTip="Filter"
                                                    ImageUrl="~/Images/SearchButton.png" OnClientClick="return imgFilterTree_Click()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Show: " ForeColor="White"></asp:Label>
                                            </td>
                                            <td style="width: 210px;" align="left">
                                                <asp:DropDownList ID="ddldisplayRecordType" runat="server" AppendDataBoundItems="True"
                                                    onChange="ddldisplayRecordType_selectionChange()" Width="100px">
                                                    <asp:ListItem Value="2">--ALL--</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">--Active--</asp:ListItem>
                                                    <asp:ListItem Value="0">--Deleted--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="FunctionalTree" style="min-width: 320px; height: 700px; max-height: 750px;
                                    width: 320px; overflow: scroll; padding-top: 3px">
                                    <%--Width is set to div--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
                <td>
                    <div style="margin-left: 5px; width: 100%;">
                        <div id="dvEditDetails">
                        </div>
                        <div id="dvMachineryDetails" style="height: 120px; overflow-y: scroll; border: 1px solid gray;
                            border-radius: 5px; padding: 5px;">
                        </div>
                        <div id="tabs" style="height: 660px; min-width: 1000px; border: 1px solid gray">
                            <ul style="background-color: #0071BC; border-color: #333333;">
                                <li style="border-color: #333333; max-width: 160px;"><a href="#joblibrarytab"><span>
                                    Job Library (<asp:Label ID="lblJobCnt" Text="0" ForeColor="green" CssClass="badge1"></asp:Label>)</span></a></li>
                                <li style="border-color: #333333; max-width: 120px;"><a href="#spareconstab"><span>Items
                                    (<asp:Label ID="lblSpareCnt" Text="0" ForeColor="green " CssClass="badge2"></asp:Label>)</span></a></li>
                            </ul>
                            <div id="alljobs" style="padding: 2px; display: none">
                            </div>
                            <div id="joblibrarytab">
                                <div id="jobtabhead" style="display: none; height: 50px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 15%;">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                    <ContentTemplate>
                                                        <asp:RadioButtonList ID="rdJobStatus" runat="server" RepeatDirection="Horizontal"
                                                            onclick="return onFilterJob(this)" AutoPostBack="True" Width="250px">
                                                            <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Deleted" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="center" style="width: 40%;">
                                                <div class="box">
                                                    <div class="container-4">
                                                        <table width="50%">
                                                            <tr>
                                                                <td style="width: 400px;">
                                                                    <input type="search" id="txtSearchJobs" placeholder="Search by Job Code/Title..."
                                                                        onkeypress=" return onJobSearchKeyPress(event);" />
                                                                </td>
                                                                <td>
                                                                    <button class="icon" onclick="return onSearchJobs();">
                                                                        <i class="fa fa-search"></i>
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                            <td align="right" style="width: 200px; padding-top: 8px;">
                                                <asp:CheckBox ID="chkCheckAllJobs" Text="Check ALL" runat="server" onclick="return onCheckAllJobs();"
                                                    Width="120px" />
                                                <asp:Button ID="btnCopyJobs" runat="server" Text="Copy jobs" OnClientClick="return onCopyJobs();"
                                                    CssClass="awesomeInPopup" Width="80px" />
                                                <asp:Button ID="btnMoveJobs" runat="server" Text="Move jobs" OnClientClick="return onMoveJobs();"
                                                    CssClass="awesomeInPopup" Width="80px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="joblibrary" style="padding: 2px; display: none; height: 500px; overflow-y: scroll;">
                                    loading....
                                </div>
                                <br />
                                <div id="jobspager" style="display: none;">
                                    <auc:ucAsyncPager ID="ucAsyncPager1" runat="server" />
                                </div>
                            </div>
                            <div id="spareconstab" style="display: none;">
                                <div id="sparecontabhead" style="display: none; height: 50px;">
                                    <table style="width: 97%; padding-top: 6px;">
                                        <tr>
                                            <td style="width: 15%;">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                    <ContentTemplate>
                                                        <asp:RadioButtonList ID="rdItemStatus" runat="server" RepeatDirection="Horizontal"
                                                            onclick="return onFilterSpare(this)" AutoPostBack="True" Width="250px">
                                                            <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Deleted" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="center" style="width: 40%;">
                                                <div class="box">
                                                    <div class="container-4">
                                                        <table width="50%">
                                                            <tr>
                                                                <td style="width: 400px;">
                                                                    <input type="search" id="txtSearchSpare" placeholder="Search by Part Number/Name..."
                                                                        onkeypress="return onSpareSearchKeyPress(event);" />
                                                                </td>
                                                                <td>
                                                                    <button class="icon" onclick="return onSearchSpare();">
                                                                        <i class="fa fa-search"></i>
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                            <td align="right" style="width: 45%;">
                                                <%--   <asp:CheckBox ID="chkAutoRequest" runat="server"  Text="Create Automatic Requisition for Critical Spare Parts" />
                                    <asp:Button ID="BtnSaveAutoRequest" runat="server" Text="Save" OnClientClick="return onAutoRequestCriticalSpare();"   CssClass="awesomeInPopup"/>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="sparecons" style="padding: 2px; display: none; height: 500px; overflow-y: scroll;">
                                    loading....
                                </div>
                                <br />
                                <div id="sparepager" style="display: none;">
                                    <auc:ucAsyncPager ID="ucAsyncPager2" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvEQPReplacement" style="display: none" title="Equipment Replacement">
        <iframe id="iframeEQPReplacement" width="1000" height="400" src=""></iframe>
    </div>
    <div>
        <asp:HiddenField ID="hdnTreeNodeTypeSelected" runat="server" />
    </div>
    <asp:UpdatePanel runat="server" ID="UpdCatalogueEntry" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divAddSystem" style="width: 600px; display: none; border: 1px solid #0071BC;
                background-color: #0071BC;" title="Add System">
                <table cellpadding="1" cellspacing="1" style="width: 500px;">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblFunction" runat="server" Text="Functions"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFunColon" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuncMandtory" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:DropDownList ID="ddlCatalogFunction" runat="server" Width="180px" CssClass="txtInput" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblVessel" runat="server" Text="Vessel"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblVesselcoln" runat="server" Text=":"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblVeslMandtory" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <%--<ucFunction:ctlFunctionList ID="ddlCatalogFunction" runat="server" Width="120%" CssClass="txtInput" />--%>
                            <asp:DropDownList ID="ddlVessID" runat="server" Width="180px" CssClass="txtInput" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbl1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCatalogueCode" CssClass="txtInput" runat="server" Width="90px"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 92px" align="right">
                            Sets Installed :<asp:Label ID="Label7" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            <asp:TextBox ID="txtCatalogueSetInstalled" runat="server" MaxLength="2" CssClass="txtInput"
                                Width="59px"></asp:TextBox>
                        </td>
                        <td valign="top" align="left">
                            <asp:ImageButton ID="ImgAddLocation" runat="server" ImageUrl="~/Images/edit-new.png"
                                OnClientClick="return onBtnDivAddLocation();" ToolTip="Assign Location" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbl2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtCatalogName" MaxLength="250" CssClass="txtInput" Width="324px"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Particulars
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                        </td>
                        <td colspan="3" rowspan="2" align="left">
                            <asp:TextBox ID="txtCatalogueParticular" MaxLength="2000" TextMode="MultiLine" CssClass="txtInput"
                                Width="324px" Height="40px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Maker
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                        </td>
                        <td colspan="3" align="left">
                            <asp:DropDownList ID="ddlCalalogueMaker" runat="server" CssClass="txtInput" Width="324px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Model
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtCatalogueModel" MaxLength="255" CssClass="txtInput" runat="server"
                                Width="324px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Serial No.
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtCatalogueSerialNumber" MaxLength="255" CssClass="txtInput" runat="server"
                                Width="324px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Department
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lbl3" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:DropDownList ID="ddlCatalogDept" Width="324px" CssClass="txtInput" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 200px">
                            Vessel Location
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                        </td>
                        <td colspan="3" valign="top" align="left">
                            <asp:ListBox ID="lstcatalogLocation" runat="server" Height="80px" Width="324px" CssClass="txtInput">
                            </asp:ListBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgDeleteAssignLoc" ImageUrl="~/Images/delete.gif" Height="18px"
                                ToolTip="Remove Assigned Location" runat="server" OnClientClick="return onDeleteSystemAssignLocation()" />
                            <%--OnClick="imgDeleteAssignLoc_click" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Account Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                        </td>
                        <td colspan="3" align="left">
                            <asp:DropDownList ID="ddlAccountCode" runat="server" CssClass="txtInput" Width="324px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                        <td colspan="3" align="left">
                            <asp:CheckBox ID="chkSubSysAdd" runat="server" Text="Create GENERAL Sub System" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblCatalogueCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkRunHourS" runat="server" Text="Run Hour" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblCatalogueModifiedby" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkCriticalS" runat="server" Text="Critical" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblCatalogueDeletedby" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <%--  <asp:Button ID="btnDivAddLocation" Text="Assign Location" runat="server" OnClientClick="return onBtnDivAddLocation();" />--%>
                            <%--OnClick="btnDivAddLocation_Click" />--%>
                            <%--OnClientClick="return onAssignLocation()"--%>
                            <%-- <asp:Button ID="btnCatalogueAdd" Text="Add New" runat="server" Width="70px" OnClientClick="return onAddSystem()"
                                                   />--%>
                            <asp:Button ID="btnCatalogueSave" Text="Save" runat="server" Width="70px" OnClientClick="return onSaveSystem();" />
                            <%--  OnClick="btnCatalogueSave_Click" OnClientClick="onSaveSystem()"/>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblCatalogueErrorMsg" Text="" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td>
                            <asp:HiddenField ID="hdfCatlogueOperationMode" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdfCatelogScrollPos" runat="server" Value="0" />
                        </td>--%>
                        <td>
                            <input type="hidden" value="1" id="hsb">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdSubCatalogueEntry" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divAddSubSystem" style="width: 500px; display: none; border: 1px solid #cccccc;
                background-color: #E0ECF8;" title="Add SubSystem">
                <table width="500px" cellpadding="1" cellspacing="1">
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="imgbtnCopySys" runat="server" ImageUrl="~/Images/copyfrom.png"
                                Width="32px" Height="32px" ToolTip="Copy From System" OnClientClick="return onCopyFromSystem();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Name :<asp:Label ID="lbl5" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtSubCatalogueName" MaxLength="100" Width="250px" CssClass="txtInput"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%" align="right">
                            Sets Installed :<asp:Label ID="Label8" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td valign="top" align="left" style="width: 65px">
                            <asp:TextBox ID="txtSubCatSetInstalled" runat="server" MaxLength="2" Width="40px"
                                CssClass="txtInput"></asp:TextBox><asp:ImageButton ID="ImgSubCatAssLocation" runat="server"
                                    ImageUrl="~/Images/edit-new.png" OnClientClick="return onBtnDivAddLocation();"
                                    ToolTip="Assign Location" />
                        </td>
                        <td valign="top" align="left" style="width: 14px">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Particulars : &nbsp;
                        </td>
                        <td rowspan="2" align="left" valign="top" colspan="2">
                            <asp:TextBox ID="txtSubCatalogueParticulars" MaxLength="2000" TextMode="MultiLine"
                                CssClass="txtInput" Width="250px" Height="50px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Maker : &nbsp;
                        </td>
                        <td colspan="2" align="left">
                            <asp:DropDownList ID="ddlSubCatMaker" runat="server" CssClass="txtInput" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Model : &nbsp;
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtSubCatModel" MaxLength="255" CssClass="txtInput" runat="server"
                                Width="250px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Serial No. : &nbsp;
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtSubCatSerialNo" MaxLength="255" CssClass="txtInput" runat="server"
                                Width="250px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Vessel Location : &nbsp;
                        </td>
                        <td colspan="2" valign="top" align="left">
                            <asp:ListBox ID="lstSubCatVesselLocation" runat="server" Height="80px" Width="250px"
                                CssClass="txtInput"></asp:ListBox>
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="ImgSubSystemDelAssVslLocation" ImageUrl="~/Images/delete.gif"
                                Height="18px" ToolTip="Remove Assigned Location" runat="server" OnClientClick="return onDeleteSubSystemAssignLocation()" />
                            <%--OnClick="ImgSubSystemDelAssVslLocation_click" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td colspan="3" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubCatalogueCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkRunHourSS" runat="server" Text="Run Hour" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubCatalogueModifiedby" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td style="width: 90px">
                            <asp:CheckBox ID="chkCriticalSS" runat="server" Text="Critical" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubCatalogueDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnSaveSubCatalogue" Text="Save" runat="server" Width="70px" OnClientClick="return onSaveSubSystem();" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblSubCatalogErrorMsg" Text="" runat="server" ForeColor="Red" Visible="false"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdfSubCatelogScrollPos" runat="server" Value="0" />
                        </td>
                        <td colspan="3" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdJobEntry" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divAddJob" style="width: 880px; display: none; border: 1px solid #cccccc;
                background-color: #E0ECF8;">
                <table cellpadding="1" cellspacing="1" width="880px">
                    <tr>
                        <td style="width: 15%" align="right">
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtJobCode" Visible="false" MaxLength="25" Width="15%" runat="server"
                                CssClass="txtInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%" align="right">
                            Job Title :<asp:Label ID="lbl7" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtJobTitle" MaxLength="254" Width="70%" runat="server" CssClass="txtInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Description :<asp:Label ID="Label5" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td valign="top" colspan="3" align="left">
                            <asp:TextBox ID="txtjobDescription" TextMode="MultiLine" runat="server" CssClass="txtInput"
                                MaxLength="2000" Width="70%" Height="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Frequency :<asp:Label ID="lbl8" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%">
                            <asp:TextBox ID="txtFrequency" MaxLength="5" Width="30%" CssClass="txtInput" runat="server"
                                onkeyup="javascript:RemoveSpecialChar(this);"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td valign="top" colspan="1" align="left">
                            <asp:RadioButtonList ID="optCMS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="CMS" Value="1"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Non CMS" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" rowspan="2">
                            Frequency Type :&nbsp;
                        </td>
                        <td align="left" style="width: 20%" rowspan="2">
                            <asp:ListBox ID="lstFrequency" runat="server" CssClass="txtInput" Height="50px" Width="100%"
                                onChange="Frequency_Changed()"></asp:ListBox>
                        </td>
                        <td>
                        </td>
                        <td colspan="1" valign="top" align="left">
                            <asp:RadioButtonList ID="optCritical" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Critical" Value="1"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Non Critical" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            Rank :<asp:Label ID="Label6" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRank" runat="server" CssClass="txtInput" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right" rowspan="2">
                            Department :&nbsp;
                        </td>
                        <td align="left" rowspan="2">
                            <asp:ListBox ID="lstDepartment" runat="server" CssClass="txtInput" Height="50px"
                                Width="100%"></asp:ListBox>
                        </td>
                        <td valign="top" align="right">
                        </td>
                        <td valign="top" align="left">
                            <asp:CheckBox ID="chkIsTechRequired" runat="server" Text="Is Tech. Required" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSafetyAlarm" runat="server" Text="Safety Alarm" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCalibration" runat="server" Text="Calibration" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Attachment :
                        </td>
                        <td align="left" colspan="3">
                            <div id="jobattachment">
                            </div>
                            <%--    <asp:UpdatePanel ID="updPMSJobAttachment" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnRefreshPMSJobAttachment" runat="server" Style="display: none"
                                        Text="RefreshPMSJobAttachment" />--%>
                            <%--OnClick="btnRefreshPMSJobAttachment_Click"--%>
                            <%--<asp:DataList ID="gvPMSJobAttachment" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                        RepeatLayout="Table" CellSpacing="2">
                                        <ItemTemplate>
                                            <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="lblAssemblyName" runat="server" Text='<%# Eval("ATTACHMENT_NAME")%>'
                                                                NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("ATTACHMENT_PATH").ToString()%>'
                                                                Target="_blank"></asp:HyperLink>
                                                            <asp:Label ID="lblATTACHMENT_PATH" Visible="false" runat="server" Text='<%# Eval("ATTACHMENT_PATH")%>'></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="imgbtnDeleteAssembly" runat="server" CommandArgument='<%# Eval("ATTACHMENT_PATH") %>'
                                                                AlternateText="delete" ImageAlign="AbsMiddle" ImageUrl="~/Images/Delete.png" />--%>
                            <%-- OnCommand="imgbtnDeleteAssembly_Click"--%>
                            <%--   </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblItemCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                         <asp:CheckBox ID="ChkMandatoryRA" runat="server" Text="Mandatory Risk Assessment" onclick="return enable_cb(this); "  />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblItemmodifiedby" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        <asp:CheckBox ID="ChkMandatoryRAApproval" runat="server" Text="Mandatory Risk Assessment Approval" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblItemDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                        <td>
                        </td>
                        
                    </tr>
                    <%--<tr>
                     <td colspan="3"></td>
                    <td valign="top" align="left">
                    <asp:CheckBox ID="ChkMandatoryRA" runat="server" Text="Mandatory Risk Assessment" onclick="return enable_cb(this); "  />
                    </td></tr>
                    <tr>
                     <td colspan="3"></td>
                    <td valign="top" align="left">
                    
                    <asp:CheckBox ID="ChkMandatoryRAApproval" runat="server" Text="Mandatory RA approval" /></td>
                 
                    </tr>--%>
                    <tr>
                    <td></td>
                   </tr>
                    <tr>
                        <td align="center" colspan="4" style="font-size: 11px;">
                            <%--   <asp:Button ID="btnAddNewJob" runat="server" Text="Add New" Width="70px"  OnClientClick="return onAddJob();" />--%><%-- OnClick="btnAddNewJob_Click"--%>
                            <asp:Button ID="btnSaveJob" runat="server" OnClientClick="return onSaveJob();" Text="Save"
                                Width="70px" />
                            <%-- OnClick="btnSaveJob_Click"--%>
                            <asp:Button ID="btnAttach" runat="server" Text="Attachment"  OnClientClick="return onJobAttachment()" />
                            <%--OnClick="btnAttach_Click"--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblItemErrorMsg" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdfJobsOperationMode" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdItemEntry" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divAddSpare" style="width: 880px; display: none; border: 1px solid #cccccc;
                background-color: #E0ECF8;">
                <table cellpadding="1" cellspacing="1" width="880px">
                    <tr>
                        <td align="right" style="width: 15%">
                            Drawing number
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtItemDrawingNumber" MaxLength="30" Width="200px" runat="server"
                                CssClass="txtInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Part number
                            <asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtItemPartNumber" MaxLength="25" Width="200px" CssClass="txtInput"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Name
                            <asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtItemName" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Description
                        </td>
                        <td>
                            :
                        </td>
                        <td rowspan="2" align="left">
                            <asp:TextBox ID="txtItemDescription" TextMode="MultiLine" runat="server" CssClass="txtInput"
                                MaxLength="2000" Width="450px" Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Unit
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlUnit" CssClass="txtInput" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Min qty
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMinQty" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Max qty
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMaxQty" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 5px">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Item category
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlItemCategory" CssClass="txtInput" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 5px">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Attachment
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <table style="width: 100px" align="left">
                                <tr>
                                    <td style="width: 15px;">
                                        <asp:HyperLink ID="lnkImageUploadName" Width="15px" runat="server" Target="_blank"
                                            ImageUrl="~/Images/attachment.png"> </asp:HyperLink>
                                    </td>
                                    <td style="width: 60px;">
                                        <asp:Button ID="btnAttImage" runat="server" Text="Browse" Font-Names="Calibri" Font-Size="14px"
                                            Font-Bold="true" OnClientClick="return ShowUploader();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 5px">
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Product details attachment
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <table style="width: 100px" align="left">
                                <tr>
                                    <td style="width: 15px;">
                                        <asp:HyperLink ID="lnkProductDetailUploadName" Width="15px" runat="server" Target="_blank"
                                            ImageUrl="~/Images/attachment.png"> </asp:HyperLink>
                                    </td>
                                    <td style="width: 60px;">
                                        <asp:Button ID="btnAttDetailsImg" runat="server" Text="Browse" Font-Names="Calibri"
                                            Font-Size="14px" Font-Bold="true" OnClientClick="return ShowDetailsUploader();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 5px">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkItemCritical" runat="server" Text="Critical" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblSpareCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblSpareModifiedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblSpareDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="center">
                            <%-- <asp:Button ID="btnAddNewItem" Text="Add New" runat="server" Width="70px" OnClientClick="return onAddSpare();"/>--%>
                            <%--OnClick="btnAddNewItem_Click" --%>
                            <asp:Button ID="btnSaveItem" Text="Save" runat="server" Width="70px" OnClientClick="return onSaveSpare()" />
                            <%--OnClick="btnSaveItem_Click"--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblItemError" Text="" runat="server" Visible="false" ForeColor="Red"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdfItemOperationMode" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdAddLocation" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divAddLocation" title="Assign Location" style="display: none; border: 1px solid Gray;
                height: 580px; width: 700px;">
                <center>
                    <table cellpadding="1" cellspacing="1" width="95%" style="position: relative;">
                        <tr>
                            <td>
                                <asp:Panel runat="server" DefaultButton="imgLocationSearch">
                                    <table width="100%">
                                        <tr>
                                            <td align="right">
                                                Search:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSearchLocation" onFocus="javascript:this.select()" CssClass="txtInput"
                                                    Width="340px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%;">
                                                <asp:ImageButton ID="imgLocationSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                                    OnClientClick="return onBtnDivAddLocation();" />
                                            </td>
                                            <td align="right" style="width: 35%;">
                                                Add Location
                                            </td>
                                            <td align="right" style="width: 5%;">
                                                <asp:ImageButton ID="imgAddNewLocation" runat="server" OnClientClick="return OnAddNewLocation();"
                                                    Height="15px" ToolTip="Add New Location" ImageUrl="~/Images/AddMaker.GIF" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="dvAddNewLocation" style="display: none; border: 1px solid #cccccc; background-color: #E0F2F7;
                                    height: 60px;">
                                    <table cellpadding="2" cellspacing="2">
                                        <%-- <tr>
                                            <td align="right" style="width: 20%">
                                                Short Code :&nbsp;
                                            </td>
                                            <td style="width: 1%; color: Red;">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLoc_ShortCode" Width="260px" runat="server" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                            <td align="right" style="color: red">
                                                * Mandatory fields.
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="right" style="width: 20%">
                                                Location Name :&nbsp;
                                            </td>
                                            <td style="width: 1%; color: Red;">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLoc_Description" Width="260px" runat="server" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDivLocationSave" runat="server" Text="Save Location" OnClientClick="return OnSaveNewLocation()" />
                                                <%--OnClick="btnDivLocationSave_Click" --%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divEditLocation" style="display: none; border: 1px solid #cccccc; background-color: #E0F2F7;
                                    height: 60px;">
                                    <table cellpadding="2" cellspacing="2">
                                        <%-- <tr>
                                            <td align="right" style="width: 20%">
                                                Short Code :&nbsp;
                                            </td>
                                            <td style="width: 1%; color: Red;">
                                                *
                                            </td>
                                            <td align="left" >
                                                <asp:TextBox ID="txtShCode" Width="260px" runat="server" CssClass="txtInput" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="left" style="color: red">
                                                * Mandatory fields.
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="right" style="width: 20%">
                                                Location Name :&nbsp;
                                            </td>
                                            <td style="width: 1%; color: Red;">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLocation1" Width="160px" runat="server" CssClass="txtInput" Enabled="false"></asp:TextBox>
                                                #
                                                <asp:TextBox ID="txtLocation2" Width="100px" runat="server" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnEditLocation" runat="server" Text="Save Location" OnClientClick="return OnSaveEditLocation()" />
                                                <%--OnClick="btnDivLocationSave_Click" --%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--  <div id="gvLocation" style="max-height: 350px; overflow: auto; background-color: #006699; z-index: 3;">--%>
                                <div id="gvLocation" style="max-height: 350px; overflow: auto; z-index: 3; padding: 2px;
                                    display: block">
                                </div>
                                <br />
                                <%--     <div>
                                    <auc:ucAsyncPager ID="ucAsyncLocPager" runat="server" />
                               </div>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: #d8d8d8;">
                                <asp:Button ID="btnDivSave" runat="server" Text="Save" OnClientClick="return onSaveAssignLocation();" /><%-- OnClick="btnDivSave_click"--%>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDivCancel" runat="server" Text="Cancel" OnClientClick="return fnCloseAssignLocation(); hideModal('divAddLocation');return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblErrMsg" ForeColor="Blue" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divEFormAssign" title="Assign E-Form" style="display: none; border: 1px solid Gray;
                height: 580px; width: 700px;">
                <center>
                    <table cellpadding="1" cellspacing="1" width="95%" style="position: relative;">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="right" style="width: 100px;">
                                            Search:
                                        </td>
                                        <td align="left" style="width: 350px;">
                                            <asp:TextBox ID="txtSearchEForm" onFocus="javascript:this.select()" CssClass="txtInput"
                                                Width="340px" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" style="width: 20px;">
                                            <asp:ImageButton ID="ImgSearchEForm" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                                OnClientClick="return onSearchEForm();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="gveForm" style="max-height: 350px; overflow: auto; z-index: 3; padding: 2px;
                                    display: block">
                                </div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: #d8d8d8;">
                                <asp:Button ID="btnSaveEForm" runat="server" Text="Save" OnClientClick="return onSaveAssignEForm();" /><%-- OnClick="btnDivSave_click"--%>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancelEForm" runat="server" Text="Cancel" OnClientClick="hideModal('divEFormAssign');return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEFormErrMsg" ForeColor="Blue" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 520px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <iframe id="IframeAttach" src="" frameborder="0" style="height: 200px; width: 100%">
                </iframe>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvCopyJobsPopUp" style="display: none; width: 870px;" title=''>
        <iframe id="iFrmCopyJobs" src="" frameborder="0" style="height: 250px; width: 100%">
        </iframe>
    </div>
    <div id="dvMoveJobsPopUp" style="display: none; width: 870px;" title=''>
        <iframe id="iFrmMoveJobs" src="" frameborder="0" style="height: 270px; width: 100%">
        </iframe>
    </div>
    <div id="dvIframe" style="display: none; width: 600px;" title=''>
        <iframe id="Iframe" src="" frameborder="0" style="height: 400px; width: 100%"></iframe>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnFunctionCode" runat="server" />
                <asp:HiddenField ID="hdnSysID" runat="server" />
                <asp:HiddenField ID="hdnSubSysID" runat="server" />
                <asp:HiddenField ID="hdnSysCode" runat="server" />
                <asp:HiddenField ID="hdnSubSysCode" runat="server" />
                <asp:HiddenField ID="hdnParrentType" runat="server" />
                <asp:HiddenField ID="hdnSearchText" runat="server" />
                <asp:HiddenField ID="hdnSearchTextLocation" runat="server" />
                <asp:HiddenField ID="hdnVesselID" runat="server" />
                <asp:HiddenField ID="hdnSortBy" runat="server" />
                <asp:HiddenField ID="hdnSortDirection" runat="server" />
                <asp:HiddenField ID="hdnPageNumber" runat="server" />
                <asp:HiddenField ID="hdnPageSize" runat="server" />
                <asp:HiddenField ID="hdnFetchCount" runat="server" />
                <asp:HiddenField ID="hdnUserID" runat="server" />
                <asp:HiddenField ID="hdnJobCode" runat="server" />
                <asp:HiddenField ID="hdnJobID" runat="server" />
                <asp:HiddenField ID="hdnItemID" runat="server" />
                <asp:HiddenField ID="hdnItemUnits" runat="server" />
                <asp:HiddenField ID="hdnItemCategory" runat="server" />
                <asp:HiddenField ID="hdnImageURL" runat="server" />
                <asp:HiddenField ID="hdnProductURL" runat="server" />
                <asp:HiddenField ID="hdnCatalogOperationMode" runat="server" />
                <asp:HiddenField ID="hdnSubCatalogOperationMode" runat="server" />
                <asp:HiddenField ID="hdnJobOperationMode" runat="server" />
                <asp:HiddenField ID="hdnItemOperationMode" runat="server" />
                <asp:HiddenField ID="hdnAppName" runat="server" />
                <asp:HiddenField ID="hdnTable" runat="server" />
                <asp:HiddenField ID="hdnLocaID" runat="server" />
                <asp:HiddenField ID="hdnSystemRhrs" runat="server" />
                <asp:HiddenField ID="hdnDeptType" runat="server" />
                <asp:HiddenField ID="hdnCompanyID" runat="server" />
                <div style="display: none;">
                    <asp:Button ID="BtnTemp" runat="server" Text="" OnClick="BtnTemp_Click" Width="5px" />
                    <asp:Button ID="BtnTempDetails" runat="server" Text="" OnClick="BtnTempDetails_Click"
                        Width="5px" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $(document).ready(initTab);      
       
    </script>
</asp:Content>
