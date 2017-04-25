<%@ Page Title="PMS Jobs" Language="C#" AutoEventWireup="true" CodeFile="PMSLibraryJobs.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="PMSLibraryJobs" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList" TagPrefix="ucFunction" %>
<%@ Register Src="~/UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
<%@ Register Src="../../UserControl/uc_Report_Issue.ascx" TagName="uc_Report_Issue"
    TagPrefix="ReportIssue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function LinkSystemRHrs() {
            alert("No such System exits. Please create a system and then Link Rhrs settings.");
        }
        function LinkSubSystemRHrs() {
            alert("No such Subsystem exits. Please create a Subsystem and then Link Rhrs settings.");
        }
        function RunHourFailed() {
            alert("Frequency cannot be selected as run hour.As run hour setting is not set on System/Subsystem.");
        }
        function getCatalogGridScrollPosition() {

            var hidden = document.getElementById('ctl00_MainContent_hdfCatelogScrollPos');
            var obj = document.getElementById("ctl00_MainContent_DivCatalogGridHolder");
            hidden.value = ("ctl00_MainContent_DivCatalogGridHolder").scrollTop;
            //  alert(hidden.value);
            ("ctl00_MainContent_DivCatalogGridHolder").scrollLeft = hidden.value;

        }


        function getSubCatalogGridScrollPosition() {

            var hidden = document.getElementById('ctl00_MainContent_hdfSubCatelogScrollPos');
            var obj = document.getElementById("ctl00_MainContent_DivSubCatalogGridHolder");
            hidden.value = obj.scrollTop;
            //  alert(hidden.value);
            obj.scrollLeft = hidden.value;

        }


        function ClearUserControlText() {

            document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";
            return true;

        }

        function OnbtnAttachClick() {

            document.getElementById('iFrmCopyJobs').src = ''; 
            return true;

        }
        function validationAddCatalogue() {
            document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";
            return true;

        }

        function validationCatalogue() {


            var Functions = document.getElementById($('[id$=txtSelectedFunction]').attr('id')).value;
            var Vessel = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var CatalogueCode = document.getElementById("ctl00_MainContent_txtCatalogueCode").value.trim();
            var CatalogueName = document.getElementById("ctl00_MainContent_txtCatalogName").value.trim();
            var CatalogueDepartment = document.getElementById("ctl00_MainContent_ddlCatalogDept").value;
            var Setsinstalled = document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").value;
            var deptType = document.getElementById("ctl00_MainContent_hdfDeptType").value;
            var OperationMode = document.getElementById("ctl00_MainContent_hdfCatlogueOperationMode").value;


            if (Functions == "" || Functions == "-Select-") {
                alert("Function is required.");
                document.getElementById($('[id$=txtSelectedFunction]').attr('id')).focus();
                return false;
            }
            if (OperationMode != "EDIT") {
                if (deptType != "ST") {
                    if (Vessel == "0") {
                        alert("Please select the vessel.");
                        return false;
                    }
                }
            }


            if (Setsinstalled != "") {
                if (isNaN(Setsinstalled)) {
                    alert('Set installed is accept ony numeric value.')
                }
            }

            if (CatalogueCode == "") {
                alert("Catalogue Code is required.");
                document.getElementById("ctl00_MainContent_txtCatalogueCode").focus();
                return false;
            }
            if (CatalogueName == "") {
                alert("Catalogue Name is requried.");
                document.getElementById("ctl00_MainContent_txtCatalogName").focus();
                return false;
            }

            if (CatalogueDepartment == "0") {
                alert("Catalogue Department is required.");
                document.getElementById("ctl00_MainContent_hdfDeptType").focus();
                return false;
            }

            return true;

        }


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


            var CatLoc = document.getElementById($('[id$=lstcatalogLocation]').attr('id')).length;
            var SubCatLoc = document.getElementById($('[id$=lstSubCatVesselLocation]').attr('id')).length;
            var Vessel = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var jotTitle = document.getElementById("ctl00_MainContent_txtJobTitle").value.trim();
            var jobDescription = document.getElementById("ctl00_MainContent_txtjobDescription").value.trim();


            var Frequency = document.getElementById("ctl00_MainContent_txtFrequency").value.trim();

            var Department = document.getElementById("ctl00_MainContent_lstDepartment").value;
            var FrequencyType = document.getElementById("ctl00_MainContent_lstFrequency").value;

            var deptType = document.getElementById("ctl00_MainContent_hdfDeptType").value;
            var OperationMode = document.getElementById("ctl00_MainContent_hdfJobsOperationMode").value;
            // var CatOperationMode = document.getElementById($('[id$=hdfCatlogueOperationMode]').attr('id')).value;
            var SubSysID = document.getElementById($('[id$=hdnSubSysID]').attr('id')).value;
            var Rank = document.getElementById($('[id$=ddlRank]').attr('id')).value;

            if (SubSysID != "") {

                if (parseInt(CatLoc) <= 0) {
                    alert("Assign Vessel Location To System");
                    document.getElementById($('[id$=lstcatalogLocation]').attr('id')).focus();
                    return false;
                }
                if (parseInt(SubCatLoc) <= 0) {
                    alert("Assign Vessel Location To Sub System");
                    document.getElementById($('[id$=lstSubCatVesselLocation]').attr('id')).focus();
                    return false;
                }
            }
            else {

                if (parseInt(CatLoc) <= 0) {
                    alert("Assign Vessel Location To System");
                    document.getElementById($('[id$=lstcatalogLocation]').attr('id')).focus();
                    return false;
                }
            }
            if (jotTitle == "") {
                alert("Job Title is required.");
                document.getElementById("ctl00_MainContent_txtJobTitle").focus();
                return false;
            }

            if (jobDescription == "") {
                alert("Job Description is required.");
                document.getElementById("ctl00_MainContent_txtjobDescription").focus();
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
            
            if (FrequencyType == "") {
                alert("Frequency type is required.");
                document.getElementById("ctl00_MainContent_lstFrequency").focus();
                return false;
            }
            if (Rank == "" || Rank == "0") {

                alert("Rank is required.");
                document.getElementById($('[id$=ddlRank]').attr('id')).focus();
                return false;
            }
            if (Department == "") {
                alert("Department is required.");
                document.getElementById("ctl00_MainContent_lstDepartment").focus();
                return false;
            }






            return true;
        }


        function CloseDiv() {
            var control = document.getElementById("ctl00_MainContent_divAddLocation");
            control.style.visibility = "hidden";
        }

        function RefreshFromchild() {

            document.getElementById("ctl00_MainContent_btnHiddenSubmit").click();
        }

        function RefreshPMSJobAttachment() {

            document.getElementById("ctl00_MainContent_btnRefreshPMSJobAttachment").click();
        }


        function RefreshMakerFromChild() {

            document.getElementById("ctl00_MainContent_btnMakerHiddenSubmit").click();
        }

        function OnMoveJobClick() {

            if (document.getElementById("ctl00_MainContent_DDLVessel").value == "0") {
                alert("Please select the vessel.");
                return false;
            }
            return true;

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
                document.getElementById('ctl00_MainContent_txtLoc_ShortCode').focus();
            }

            return false;

        }


        function OnSaveLocation() {

            if (document.getElementById('ctl00_MainContent_txtLoc_ShortCode').value == "") {
                document.getElementById('ctl00_MainContent_txtLoc_ShortCode').focus();
                alert("Short code is a mandatory field.");
                return false;
            }

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

        function CopySystemData() {
            document.getElementById('ctl00_MainContent_txtSubCatalogueCode').value = document.getElementById('ctl00_MainContent_txtCatalogueCode').value;
            document.getElementById('ctl00_MainContent_txtSubCatalogueParticulars').value = document.getElementById('ctl00_MainContent_txtCatalogueParticular').value;
            document.getElementById('ctl00_MainContent_txtSubCatModel').value = document.getElementById('ctl00_MainContent_txtCatalogueModel').value;
            document.getElementById('ctl00_MainContent_txtSubCatSerialNo').value = document.getElementById('ctl00_MainContent_txtCatalogueSerialNumber').value;
            document.getElementById('ctl00_MainContent_ddlSubCatMaker').value = document.getElementById('ctl00_MainContent_ddlCalalogueMaker').value
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
    <div style="font-family: Tahoma; font-size: 12px" width="100%">
        <asp:UpdatePanel runat="server" ID="pnlFilter">
            <ContentTemplate>
                <div style="border: 1px solid #cccccc;">
                    <div class="page-title">
                        PMS Jobs Library
                    </div>
                    <div style="background-color: #E0ECF8">
                        <table cellpadding="1" cellspacing="1" width="90%">
                            <tr>
                                <td style="width: 5%" align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td style="width: 7%" align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px" TabIndex="1">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 5%" align="right">
                                    &nbsp;&nbsp; &nbsp;&nbsp;Vessel :&nbsp;&nbsp;
                                </td>
                                <td style="width: 7%" align="left">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 8%" align="right">
                                    &nbsp;&nbsp; &nbsp;&nbsp;Function :&nbsp;&nbsp;
                                </td>
                                <td style="width: 12%" align="left">
                                    <asp:DropDownList ID="DDLDepartment" runat="server" DataTextField="DESCRIPTION" DataValueField="CODE"
                                        Width="180px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 6%" align="right">
                                    &nbsp;&nbsp; &nbsp;&nbsp;Show :&nbsp;&nbsp;
                                </td>
                                <td style="width: 10%" align="left">
                                    <asp:DropDownList ID="ddldisplayRecordType" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddldisplayRecordType_SelectedIndexChanged"
                                        Width="100px">
                                        <asp:ListItem Value="2">--ALL--</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">--Active--</asp:ListItem>
                                        <asp:ListItem Value="0">--Deleted--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 5%" align="center">
                                    <asp:Button ID="btnRetrieve" runat="server" OnClick="btnRetrieve_Click" OnClientClick="return ClearUserControlText()"
                                        Text="Search" />
                                </td>
                                <td style="width: 5%">
                                </td>
                                <td style="width: 6%;" align="left">
                                    <asp:Button ID="btnCopyJobs" runat="server" OnClick="btnCopyJobs_Click" Text="Copy jobs" />
                                </td>
                                <td style="width: 6%;" align="left">
                                    <asp:Button ID="btnMoveJobs" runat="server" OnClick="btnMoveJobs_Click" Text="Move jobs"
                                        OnClientClick="return OnMoveJobClick();" />
                                </td>
                                <td style="width: 18%">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #E0ECF8">
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 5%">
                                    &nbsp; Search :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchCatalogue" Width="360px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 2%">
                                    <asp:ImageButton ID="imgCatalogueSearch" ImageUrl="~/Purchase/Image/preview.gif"
                                        runat="server" OnClick="imgCatalogueSearch_Click" />
                                </td>
                                <td style="width: 4%">
                                </td>
                                <td align="right" style="width: 4%">
                                    Search:&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchSubCatalogue" runat="server" Width="400px"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 2%">
                                    <asp:ImageButton ID="imgSubCatalogueSearch" ImageUrl="~/Purchase/Image/preview.gif"
                                        runat="server" OnClick="imgSubCatalogueSearch_Click" />
                                </td>
                                <td style="width: 4%">
                                </td>
                                <td align="right" style="width: 5%">
                                    Search :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchItemName" Width="400px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:ImageButton ID="imgItemSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                        OnClick="imgItemSearch_Click" />
                                </td>
                                <td align="right" style="width: 45%">
                                    <asp:CheckBox ID="SelChkJob" Text="Check ALL" AutoPostBack="true" runat="server"
                                        OnCheckedChanged="SelChkJob_OnCheckedChanged" />
                                </td>
                                <td style="width: 1%">
                                    <asp:HiddenField ID="hdfDeptType" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="border: 1px solid #cccccc; margin-top: 2px;">
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td colspan="3">
                        <div id="DivCatalogGridHolder" style="overflow-x: hidden; overflow-y: scroll; height: 370px;
                            width: 500px; border: 1px solid #cccccc;" onscroll="getCatalogGridScrollPosition()">
                            <asp:UpdatePanel runat="server" ID="UpdCatelogueGrid" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvCatalogue" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvCatalogue_RowDataBound"
                                        Width="100%" GridLines="Both" OnSelectedIndexChanging="gvCatalogue_SelectedIndexChanging"
                                        AllowSorting="true" OnSorting="gvCatalogue_Sorting" DataKeyNames="ID">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vessel">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblVesseleNameHeader" runat="server" ForeColor="White">Vessel&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblSubCatalogueNameHeader" runat="server" CommandName="Sort"
                                                        CommandArgument="SYSTEM_DESCRIPTION" ForeColor="White">System&nbsp;</asp:LinkButton>
                                                    <img id="SYSTEM_DESCRIPTION" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnbSystemName" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SYSTEM_DESCRIPTION") %>'></asp:LinkButton>
                                                    <asp:Label ID="lblSystemName" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SYSTEM_DESCRIPTION") %>'></asp:Label>
                                                    <asp:Label ID="lblSystemCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.System_Code") %>'></asp:Label>
                                                    <asp:Label ID="lblSystemId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                    <asp:Label ID="lblCalogueActiveSatus" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Active_Status") %>'></asp:Label>
                                                    <asp:Label ID="lblMaker" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MakerName") %>'></asp:Label>
                                                    <asp:Label ID="lblModel" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Module_Type") %>'></asp:Label>
                                                    <asp:Label ID="lblParticulars" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.System_Particulars") %>'></asp:Label>
                                                    <asp:Label ID="lblDeptCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Dept1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgCatalogueDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                                    ImageUrl="~/purchase/Image/Delete.gif" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                                    OnCommand="ImgCatalogueDelete_Click" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                                    Width="16px" Height="12px"></asp:ImageButton>
                                                            </td>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgCatalogueRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                                    ImageUrl="~/purchase/Image/restore.png" OnCommand="ImgCatalogueRestore_Click"
                                                                    CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>' Width="16px"
                                                                    Height="16px"></asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td colspan="3">
                        <div id="DivSubCatalogGridHolder" style="overflow-x: hidden; overflow-y: scroll;
                            height: 370px; width: 500px; border: 1px solid #cccccc;" onscroll="getSubCatalogGridScrollPosition()">
                            <asp:UpdatePanel runat="server" ID="UpdSubCatelogueGrid" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvSubCatalogue" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSubCatalogue_RowDataBound"
                                        Width="100%" GridLines="Both" OnSelectedIndexChanging="gvSubCatalogue_SelectedIndexChanging"
                                        AllowSorting="true" OnSorting="gvSubCatalogue_Sorting" DataKeyNames="ID">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Code">
                                                <HeaderTemplate>
                                                    Code
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container,"DataItem.Subsystem_Code") %>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblSubCatalogueNameHeader" runat="server" CommandName="Sort"
                                                        CommandArgument="Subsystem_Description" ForeColor="White">Sub System&nbsp;</asp:LinkButton>
                                                    <img id="Subsystem_Description" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblSubCatalogueName" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Subsystem_Description") %>'></asp:LinkButton>
                                                    <asp:Label ID="lblSubSystemCode" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.Subsystem_Code") %>'></asp:Label>
                                                    <asp:Label ID="lblSubSystemID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                    <asp:Label ID="lblSubCalogueActiveSatus" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Active_Status") %>'></asp:Label>
                                                    <asp:Label ID="lblSubsystemParticulars" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Subsystem_Particulars") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgSubCatalogueDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                                    ImageUrl="~/purchase/Image/Delete.gif" OnCommand="ImgSubCatalogueDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure, you want to  delete ?')" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                                    Width="16px" Height="12px"></asp:ImageButton>
                                                            </td>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgSubCatalogueRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                                    ImageUrl="~/purchase/Image/restore.png" OnCommand="ImgSubCatalogueRestore_Click"
                                                                    CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>' Width="16px"
                                                                    Height="16px"></asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td colspan="7" style="width: 40%">
                        <div style="overflow-x: hidden; overflow-y: scroll; height: 370px; width: 880px;
                            border: 1px solid #cccccc;">
                            <asp:UpdatePanel runat="server" ID="UpdJobsGrid" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvJobs_RowDataBound"
                                        Width="100%" OnSelectedIndexChanging="gvJobs_SelectedIndexChanging" AllowSorting="true"
                                        OnSorting="gvJobs_Sorting" DataKeyNames="ID">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Job ID">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblJobIdHeader" runat="server" CommandName="Sort" CommandArgument="ID"
                                                        ForeColor="White">ID&nbsp;</asp:LinkButton>
                                                    <img id="ID" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobID" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                    <asp:Label ID="lblJobDescription" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Description") %>'></asp:Label>
                                                    <asp:Label ID="lblItemActiveSatus" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Active_Status") %>'></asp:Label>
                                                    <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job ID">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblJobCodeHeader" runat="server" CommandName="Sort" CommandArgument="Job_Code"
                                                        ForeColor="White">Code&nbsp;</asp:LinkButton>
                                                    <img id="Job_Code" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblJobTitleHeader" runat="server" CommandName="Sort" CommandArgument="Job_Title"
                                                        ForeColor="White">Job Title&nbsp;</asp:LinkButton>
                                                    <img id="Job_Title" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblJobTitle" Visible="true" runat="server" CommandName="Select"
                                                        Text='<%# DataBinder.Eval(Container,"DataItem.Job_Title") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="450px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Freq.">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblFrequencyHeader" runat="server" ForeColor="White">Freq.&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFrequency" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Frequency") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Freq.type">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblFrequencyTypeHeader" runat="server" ForeColor="White">Freq.type&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFrequencyType" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Frequency_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblDepartmentHeader" runat="server" ForeColor="White">Dept&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Department") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblRankNameHeader" runat="server" ForeColor="White">Rank&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRankName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RankName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CMS">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblCMSHeader" runat="server" ForeColor="White">CMS&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMS" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CMS") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Critical">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblCriticalHeader" runat="server" ForeColor="White">Critical&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCritical" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Critical") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%-- <asp:Image ID="imgMoveJob" runat="server" ToolTip="Select Job to Move" ImageUrl="~/Images/help16.png" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkJob" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgItemDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                                    ImageUrl="~/purchase/Image/Delete.gif" OnCommand="ImgJobDelete_Click" CommandArgument='<%#Eval("ID")%>'
                                                                    OnClientClick="return confirm('Are you sure, you want to  delete ?')" Width="16px"
                                                                    Height="12px"></asp:ImageButton>
                                                            </td>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgItemRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                                    ImageUrl="~/purchase/Image/restore.png" OnCommand="ImgJobRestore_Click" CommandArgument='<%#Eval("ID")%>'
                                                                    Width="16px" Height="16px"></asp:ImageButton>
                                                            </td>
                                                            <td style="border-color: transparent">
                                                                <img src="../../images/eForms.png" style="cursor: pointer;" title="eForm Assignment"
                                                                    height="12px" width="12px" onclick="OpenAssignScreen('<%# DataBinder.Eval(Container,"DataItem.Vessel_ID")%>','<%# DataBinder.Eval(Container,"DataItem.ID") %>' );return false;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindJobs" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr style="height: 10px">
                    <td colspan="3">
                    </td>
                    <td colspan="3">
                    </td>
                    <td colspan="7">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" valign="top" align="left">
                        <asp:UpdatePanel runat="server" ID="UpdCatalogueEntry" >
                            <ContentTemplate>
                                <div style="width: 500px; border: 1px solid #cccccc; background-color: #E0ECF8">
                                    <table cellpadding="1" cellspacing="1" style="width: 500px">
                                        <tr>
                                            <td align="right">
                                                Functions :
                                                <asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <ucFunction:ctlFunctionList ID="ddlCatalogFunction" runat="server" Width="120%" CssClass="txtInput" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                Code :
                                                <asp:Label ID="lbl1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCatalogueCode" MaxLength="8" CssClass="txtInput" runat="server"
                                                    Width="90px"></asp:TextBox>
                                            </td>
                                            <td style="width: 50%" align="right">
                                                Sets Installed :&nbsp;&nbsp;
                                            </td>
                                            <td valign="top" align="right">
                                                <asp:TextBox ID="txtCatalogueSetInstalled" runat="server" MaxLength="3" CssClass="txtInput"
                                                    Width="60px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Name :<asp:Label ID="lbl2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtCatalogName" MaxLength="250" CssClass="txtInput" Width="99%"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Particulars : &nbsp;
                                            </td>
                                            <td colspan="3" rowspan="2" align="left">
                                                <asp:TextBox ID="txtCatalogueParticular" MaxLength="2000" TextMode="MultiLine" CssClass="txtInput"
                                                    Width="99%" Height="40px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Maker : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlCalalogueMaker" runat="server" CssClass="txtInput" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                         <%--   <td align="left" valign="middle">
                                                <asp:ImageButton ID="imbAddMaker" ImageUrl="~/Images/AddMaker.png" Height="15px"
                                                    ToolTip="Add Maker" runat="server" OnClientClick="return OnAddMaker();" />
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Model : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtCatalogueModel" MaxLength="255" CssClass="txtInput" runat="server"
                                                    Width="99%">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Serial No. : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtCatalogueSerialNumber" MaxLength="255" CssClass="txtInput" runat="server"
                                                    Width="99%">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Department :
                                                <asp:Label ID="lbl3" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlCatalogDept" Width="100%" CssClass="txtInput" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Vessel Location : &nbsp;
                                            </td>
                                            <td colspan="3" valign="top" align="left">
                                                <asp:ListBox ID="lstcatalogLocation" runat="server" Height="80px" Width="100%" CssClass="txtInput">
                                                </asp:ListBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgDeleteAssignLoc" ImageUrl="~/Images/delete.gif" Height="18px"
                                                    ToolTip="Remove Assigned Location" runat="server" OnClick="imgDeleteAssignLoc_click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Account Code : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlAccountCode" runat="server" CssClass="txtInput" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                            <td colspan="2" align="left">
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
                                                <asp:Button ID="btnCatalogueSave" Text="Save" runat="server" Width="70px" OnClientClick="return validationCatalogue()"
                                                    OnClick="btnCatalogueSave_Click" />
                                                <asp:Button ID="btnDivAddLocation" Text="Assign Location" runat="server" Width="110px"
                                                    OnClick="btnDivAddLocation_Click" />
                                                <asp:Button ID="btnCatalogueAdd" Text="Add New" runat="server" Width="70px" OnClientClick="return validationAddCatalogue()"
                                                    OnClick="btnCatalogueAdd_Click" />
                                                <asp:Button ID="btnSystemLinkRunHourSettings" runat="server" Text="Link RHrs Settings"
                                                    OnClick="btnSystemLinkRunHourSettings_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="left">
                                                <asp:Label ID="lblCatalogueErrorMsg" Text="" runat="server" ForeColor="Red" Visible="false"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdfCatlogueOperationMode" runat="server" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdfCatelogScrollPos" runat="server" Value="0" />
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
                    </td>
                    <td colspan="3" valign="top" style="background-color: #E0ECF8;">
                        <asp:UpdatePanel runat="server" ID="UpdSubCatalogueEntry">
                            <ContentTemplate>
                                <div style="width: 500px;">
                                    <table width="500px" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="btnCopy" Text="Copy From System" runat="server" 
                                                    OnClientClick="CopySystemData(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%" align="right">
                                                Code :
                                                <asp:Label ID="lbl4" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtSubCatalogueCode" MaxLength="8" CssClass="txtInput" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Name :<asp:Label ID="lbl5" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtSubCatalogueName" MaxLength="150" Width="95%" CssClass="txtInput"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Particulars : &nbsp;
                                            </td>
                                            <td rowspan="2" align="left" valign="top" colspan="3">
                                                <asp:TextBox ID="txtSubCatalogueParticulars" MaxLength="2000" TextMode="MultiLine"
                                                    CssClass="txtInput" Width="95%" Height="50px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Maker : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlSubCatMaker" runat="server" CssClass="txtInput" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" valign="middle">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Model : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtSubCatModel" MaxLength="255" CssClass="txtInput" runat="server"
                                                    Width="99%">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Serial No. : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtSubCatSerialNo" MaxLength="255" CssClass="txtInput" runat="server"
                                                    Width="99%">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Vessel Location : &nbsp;
                                            </td>
                                            <td colspan="3" valign="top" align="left">
                                                <asp:ListBox ID="lstSubCatVesselLocation" runat="server" Height="80px" Width="100%"
                                                    CssClass="txtInput"></asp:ListBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImgSubSystemDelAssVslLocation" ImageUrl="~/Images/delete.gif"
                                                    Height="18px" ToolTip="Remove Assigned Location" runat="server" OnClick="ImgSubSystemDelAssVslLocation_click" />
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
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSubCatalogueCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:CheckBox ID="chkRunHourSS" runat="server" Text="Run Hour" OnCheckedChanged="chkRunHourSS_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSubCatalogueModifiedby" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkCriticalSS" runat="server" Text="Critical" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSubCatalogueDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkCopyRunHourSS" runat="server" Text="Copy Run Hour" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSaveSubCatalogue" Text="Save" runat="server" Width="70px" OnClientClick="return validationSubCatalogue();"
                                                    OnClick="btnSaveSubCatalogue_Click" />
                                                <asp:Button ID="btnSubCatDivAddLocation" Text="Assign Location" runat="server" Width="110px"
                                                    OnClick="btnSubCatDivAddLocation_Click" />
                                                <asp:Button ID="btnAddNewSubCatalogue" Text="Add New" runat="server" Width="70px"
                                                    OnClick="btnAddNewSubCatalogue_Click" />
                                                <asp:Button ID="btnSubSystemLinkRunHourSettings" runat="server" Text="Link RHrs Settings"
                                                    OnClick="btnSubSystemLinkRunHourSettings_Click" />
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
                                                <asp:HiddenField ID="hdnSubcatOperationMode" runat="server" />
                                                <asp:HiddenField ID="hdnSubSysID" runat="server" />
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
                    </td>
                    <td colspan="7" valign="top" style="background-color: #E0ECF8;">
                        <div style="width: 880px;">
                            <asp:UpdatePanel runat="server" ID="UpdJobEntry" UpdateMode="Conditional">
                                <ContentTemplate>
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
                                                Description :<asp:Label ID="lblD1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
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
                                                <asp:TextBox ID="txtFrequency" MaxLength="5" Width="30%" CssClass="txtInput" runat="server"></asp:TextBox>
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
                                                    OnSelectedIndexChanged="lstFrequency_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:ListBox>
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
                                                Rank :<asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Attachment :
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:UpdatePanel ID="updPMSJobAttachment" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnRefreshPMSJobAttachment" runat="server" Style="display: none"
                                                            Text="RefreshPMSJobAttachment" OnClick="btnRefreshPMSJobAttachment_Click" />
                                                        <asp:DataList ID="gvPMSJobAttachment" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
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
                                                                                <asp:ImageButton ID="imgbtnDeleteAssembly" runat="server" OnCommand="imgbtnDeleteAssembly_Click"
                                                                                    CommandArgument='<%# Eval("ATTACHMENT_PATH") %>' AlternateText="delete" ImageAlign="AbsMiddle"
                                                                                    ImageUrl="~/Images/Delete.png" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblItemCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblItemmodifiedby" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblItemDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnSaveJob" runat="server" OnClick="btnSaveJob_Click" OnClientClick="return validationOnAddJob();"
                                                    Text="Save" Width="70px" />
                                                <asp:Button ID="btnAttach" runat="server" OnClick="btnAttach_Click" Text="Attachment" OnClientClick="return OnbtnAttachClick();"
                                                    Width="80px" />
                                                <asp:Button ID="btnAddNewJob" runat="server" OnClick="btnAddNewJob_Click" Text="Add New"
                                                    Width="70px" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="subHeader" style="display: none; position: relative; right: 0px">
            <asp:Button ID="btnHiddenSubmit" runat="server" Text="btnHiddenSubmit" OnClick="btnHiddenSubmit_Click" />
            <asp:Button ID="btnMakerHiddenSubmit" runat="server" Text="btnMakerHiddenSubmit"
                OnClick="btnMakerHiddenSubmit_Click" />
        </div>
        <asp:UpdatePanel runat="server" ID="UpdAddLocation">
            <ContentTemplate>
                <div id="divAddLocation" title="Assign Location" style="display: none; border: 1px solid Gray;
                    height: 500px; width: 580px;">
                    <center>
                        <table cellpadding="1" cellspacing="1" width="95%" style="position: relative;">
                            <tr>
                                <td>
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
                                                    OnClick="imgLocationSearch_Click" />
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="dvAddNewLocation" style="display: none; border: 1px solid #cccccc; background-color: #E0F2F7;
                                        height: 60px;">
                                        <table cellpadding="2" cellspacing="2">
                                            <tr>
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
                                            </tr>
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
                                                    <asp:Button ID="btnDivLocationSave" runat="server" Text="Save Location" OnClientClick="return OnSaveLocation();"
                                                        OnClick="btnDivLocationSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="max-height: 350px; overflow: auto; background-color: #006699; z-index: 3;">
                                        <asp:GridView ID="gvLocation" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                            OnRowDataBound="gvLocation_RowDataBound" Width="100%" OnSelectedIndexChanging="gvLocation_SelectedIndexChanging"
                                            OnRowCommand="gvLocation_RowCommand" AllowSorting="true" OnSorting="gvLocation_Sorting"
                                            DataKeyNames="LocationID">
                                            <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                            <RowStyle CssClass="PMSGridRowStyle-css" />
                                            <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID">
                                                    <HeaderTemplate>
                                                        ID
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivLocationCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Short Code">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblDivShortCodeHeader" runat="server" ForeColor="White">Short Code&nbsp;</asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivShortCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationShortCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location Name">
                                                    <HeaderTemplate>
                                                        Location Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivLocationName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="System">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivMachinery" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.System_Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Subsystem_Description" HeaderText="SubSystem" />
                                                <asp:TemplateField HeaderText="ID">
                                                    <HeaderTemplate>
                                                        Select
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDivAssingLoc" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "LocAssignFlag").ToString() =="1"? true : false %>'
                                                            Enabled='<%#DataBinder.Eval(Container.DataItem, "LocAssignFlag").ToString() =="1"? false : true %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Spare">
                                                    <HeaderTemplate>
                                                        Is Spare Equipment
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkIsSpare" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "Category_Code").ToString() =="SP"? true : false %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <uc1:ucCustomPager ID="ucDivCustomPager" runat="server" OnBindDataItem="BindCatalogueAssignLocation" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="background-color: #d8d8d8;">
                                    <asp:Button ID="btnDivSave" runat="server" Text="Save" OnClick="btnDivSave_click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDivCancel" runat="server" Text="Cancel" OnClientClick="hideModal('divAddLocation');return false;" />
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
    </div>
    <div id="dvCopyJobsPopUp" style="display: none; width: 520px;" title=''>
        <iframe id="iFrmCopyJobs" src="" frameborder="0" style="height: 200px; width: 100%">
        </iframe>
    </div>
    <div id="dvIframe" style="display: none; width: 600px;" title=''>
        <iframe id="Iframe" src="" frameborder="0" style="height: 400px; width: 100%"></iframe>
    </div>
</asp:Content>
<%--    <form id="frmJobs" runat="server" style="vertical-align: top" defaultbutton="imgLocationSearch"
    defaultfocus="imgLocationSearch">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>--%>
