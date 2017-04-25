<%@ Page Language="C#" Title="JiBe::Purchase Items" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="LibraryCatalogue.aspx.cs" Inherits="Purchase_LibraryCatalogue"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList"
    TagPrefix="ucFunction" %>
<%@ Register Src="../UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
<%@ Register Src="../UserControl/uc_Report_Issue.ascx" TagName="uc_Report_Issue"
    TagPrefix="ReportIssue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
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
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
    </style>
    <script language="javascript" type="text/javascript">


        function getCatalogGridScrollPosition() {

            var hidden = document.getElementById('ctl00_MainContent_hdfCatelogScrollPos');
            var obj = document.getElementById("ctl00_MainContent_DivCatalogGridHolder");
            hidden.value = ("ctl00_MainContent_DivCatalogGridHolder").scrollTop;
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


        function validationAddCatalogue() {



            document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";
            return true;

        }



        function validationCatalogue() {

            var Vessel = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var CatalogueCode = document.getElementById("ctl00_MainContent_txtCatalogueCode").value.trim();
            var CatalogueName = document.getElementById("ctl00_MainContent_txtCatalogName").value.trim();
            var CatalogueDepartment = document.getElementById("ctl00_MainContent_ddlCatalogDept").value;
            var Setsinstalled = document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").value.trim();
            var deptType = document.getElementById("ctl00_MainContent_hdfDeptType").value;
            var OperationMode = document.getElementById("ctl00_MainContent_hdfCatlogueOperationMode").value;





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
                alert("Catalogue Code is requried.");
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
                document.getElementById("ctl00_MainContent_ddlCatalogDept").focus();
                return false;
            }

            //  if (CatalogueVslLocation == "0") {
            //  alert("Catalogue Vessel Location is required.");
            //  return false;
            //  }

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


        function validationItems() {
            var Vessel = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var partNumber = document.getElementById("ctl00_MainContent_txtItemPartNumber").value.trim();
            var ItemName = document.getElementById("ctl00_MainContent_txtItemName").value.trim();
            var deptType = document.getElementById("ctl00_MainContent_hdfDeptType").value;
            var OperationMode = document.getElementById("ctl00_MainContent_hdfItemOperationMode").value;
            var MinQty = document.getElementById("ctl00_MainContent_txtMinQty").value;
            var MaxQty = document.getElementById("ctl00_MainContent_txtMaxQty").value;


            //            if (OperationMode != "EDIT") {
            //                if (deptType != "ST") {
            //                    if (Vessel == "0") {
            //                        alert("Please select the vessel.");
            //                        return false;
            //                    }
            //                }
            //            }

            if (partNumber == "") {
                alert("Part number is requried.");
                document.getElementById('ctl00_MainContent_txtItemPartNumber').focus();
                return false;
            }
            if (ItemName == "") {
                alert("Item Name is requried.");
                document.getElementById('ctl00_MainContent_txtItemName').focus();
                return false;
            }


            if (MinQty != "") {
                if (isNaN(MinQty)) {
                    alert('Min quantiy is accept ony numeric value.')
                    document.getElementById('ctl00_MainContent_txtMinQty').focus();
                    return false;
                }
            }

            if (MaxQty != "") {
                if (isNaN(MaxQty)) {
                    alert('Max quantiy is accept ony numeric value.')
                    document.getElementById('ctl00_MainContent_txtMaxQty').focus();
                    return false;
                }
            }


            if (parseFloat(MaxQty) < parseFloat(MinQty)) {
                alert('Max quantiy should not be less then min quantity.')
                document.getElementById('ctl00_MainContent_txtMaxQty').focus();
                return false;
            }

            return true;
        }

        function CloseDiv() {
            var control = document.getElementById("ctl00_MainContent_divAddLocation");
            control.style.visibility = "hidden";
        }

        function RefreshMakerFromChild() {

            document.getElementById("ctl00_MainContent_btnMakerHiddenSubmit").click();
        }

        function OnAddMaker() {
            document.getElementById('Iframe').src = '../Infrastructure/Libraries/Supplier_Maker_Add.aspx';
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
            if (document.getElementById('ctl00_MainContent_txtNoLoc').value == "" || document.getElementById('ctl00_MainContent_txtNoLoc').value == "0") {
                document.getElementById('ctl00_MainContent_txtNoLoc').focus();
                alert("Number Of Location is a mandatory field and it cannot be blank or 0.");
                return false;
            }

            if (/^\d+$/.test(parseInt(document.getElementById('ctl00_MainContent_txtNoLoc').value))) {
            }else{
                document.getElementById('ctl00_MainContent_txtNoLoc').focus();
                alert("Number Of Location is an whole number.");
                return false;
            }
            return true;
        }

        function DocOpen(filename) {
            var conn = '<%=ConfigurationManager.AppSettings["APP_NAME"].ToString() %>'
            var filepath = "/" + conn + "/uploads/PURC_Items/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }

        function Update(id, objRef) {

            var lastExecutor = null;
            var isMeat = 0;
            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {
                isMeat = 1
            }
            else {
                isMeat = 0
            }

            var USERID = '<%= HttpContext.Current.Session["USERID"] %>';

            __isResponse = 0;
            setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'UpdateIsMeat', false, { "ID": id, "isMeat": isMeat, "USERID": USERID }, onSuccess, Onfail, new Array('t'));

            lastExecutorr = service.get_executor();
        }
        
        function UpdateSlopChestItems(id, objRef) {
            var lastExecutor = null;
            var isSlopChest = 0;
            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {
                isSlopChest = 1
            }
            else {
                isSlopChest = 0
            }

            var USERID = '<%= HttpContext.Current.Session["USERID"] %>';

            __isResponse = 0;
            setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'UpdateSlopChestItems', false, { "id": id, "isSlopChest": isSlopChest, "UserID": USERID }, onSuccess, Onfail, new Array('t'));

            lastExecutorr = service.get_executor();
        }
        

        function onSuccess(retVal, ev) {

            __isResponse = 1;

        }
        function Onfail(msg) {

            alert(msg);
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="font-family: Tahoma; font-size: 12px" width="100%">
        <asp:UpdatePanel runat="server" ID="pnlFilter">
            <ContentTemplate>
                <div style="border: 1px solid #cccccc;">
                    <div class="page-title">
                        <table width="90%">
                            <tr style="height: 15px">
                                <td align="center">
                                    <b>Add/View :: Catalogue /Sub Catalogue /Item </b>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <div style="background-color: #E0ECF8">
                      <%--   <table width="20%"  align="right">
                        <tr style="width: 100%" align="left">
                            <td style="float:left">Create Automatic Requisition for Critical Spare Parts</td>
                            <td><asp:CheckBox ID="ChkAuto_Req" runat="server" ClientIDMode="Static"/></td>
                            <td><asp:Button ID="btnReq_Save" runat="server"  Text="Save" ClientIDMode="Static"  
                                    onclick="btnReq_Save_Click" /></td>
                            </tr></table>--%>
                        <table width="90%">
                             <tr>
                                <td style="width: 3%" align="right">
                                    Fleet :
                                </td>
                                <td style="width: 3%" align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 2%" align="right">
                                    Vessel :
                                </td>
                                <td style="width: 4%" align="left">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 4%" align="right">
                                    Dept Type :
                                </td>
                                <td style="width: 6%" align="left">
                                    <asp:RadioButtonList ID="optList" runat="server" AutoPostBack="True" ForeColor="Black"
                                        OnSelectedIndexChanged="optList_SelectedIndexChanged" RepeatDirection="Horizontal"
                                        TabIndex="2">
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 4%" align="right">
                                    Department :
                                </td>
                                <td style="width: 4%" align="left">
                                    <asp:DropDownList ID="cmbDept" runat="server" AppendDataBoundItems="True" TabIndex="3"
                                        Width="160px">
                                        <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 2%" align="right">
                                    Show :
                                </td>
                                <td style="width: 3%" align="left">
                                    <asp:DropDownList ID="ddldisplayRecordType" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddldisplayRecordType_SelectedIndexChanged"
                                        Width="100px">
                                        <asp:ListItem Value="2">--ALL--</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">--Active--</asp:ListItem>
                                        <asp:ListItem Value="0">--Deleted--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="btnRetrieve" runat="server" OnClick="btnRetrieve_Click" OnClientClick="return ClearUserControlText()"
                                        Text="Search" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #E0ECF8">
                        <table width="90%">
                            <tr>
                                <td style="width: 6%" align="right">
                                    Search :
                                </td>
                                <td align="left" style="width: 7%">
                                    <asp:TextBox ID="txtSearchCatalogue" Width="350px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 1%">
                                    <asp:ImageButton ID="imgCatalogueSearch" ImageUrl="~/Purchase/Image/preview.gif"
                                        runat="server" OnClick="imgCatalogueSearch_Click" />
                                </td>
                                <td align="right" style="width: 3%">
                                    Search :
                                </td>
                                <td align="left" style="width: 7%">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSearchSubCatalogue" runat="server" Width="360px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgSubCatalogueSearch" ImageUrl="~/Purchase/Image/preview.gif"
                                                    runat="server" OnClick="imgSubCatalogueSearch_Click" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" style="width: 1%">
                                </td>
                                <td align="right" style="width: 3%">
                                    Search :
                                </td>
                                <td align="left" style="width: 7%">
                                    <asp:TextBox ID="txtSearchItemName" Width="350px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 1%">
                                    <asp:ImageButton ID="imgItemSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                        OnClick="imgItemSearch_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSelectAllItem" runat="server" Text="Search in all SubCate." OnClick="btnSelectAllItem_Click" />
                                </td>
                                <td style="width: 22%">
                                    <asp:HiddenField ID="hdfDeptType" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="border: 1px solid #cccccc; margin-top: 2px">
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td colspan="3" align="left">
                        <div id="DivCatalogGridHolder" style="overflow-x: hidden; overflow-y: scroll; height: 390px;
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
                                                        CommandArgument="SYSTEM_DESCRIPTION" ForeColor="White">Catalogue/Machinery&nbsp;</asp:LinkButton>
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
                    <td colspan="3" align="left">
                        <div id="DivSubCatalogGridHolder" style="overflow-x: hidden; overflow-y: scroll;
                            height: 390px; width: 500px; border: 1px solid #cccccc;" onscroll="getSubCatalogGridScrollPosition()">
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
                                                        CommandArgument="Subsystem_Description" ForeColor="White">Sub Catalogue&nbsp;</asp:LinkButton>
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
                        <div style="overflow-x: hidden; overflow-y: scroll; height: 390px; width: 880px;
                            border: 1px solid #cccccc;">
                            <asp:UpdatePanel runat="server" ID="UpdItemGrid" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItem_RowDataBound"
                                        Width="100%" OnSelectedIndexChanging="gvItem_SelectedIndexChanging" AllowSorting="true"
                                        OnSorting="gvItem_Sorting" DataKeyNames="ID">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Drawing No.">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblDrawingNoHeader" runat="server" ForeColor="White">Drawing No&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Drawing_Number") %>'></asp:Label>
                                                    <asp:Label ID="lblItemID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                    <asp:Label ID="lblItemActiveSatus" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Active_Status") %>'></asp:Label>
                                                    <asp:Label ID="lblLongDescription" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Long_Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Part no.">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblPartNoHeader" runat="server" CommandName="Sort" CommandArgument="Part_Number"
                                                        ForeColor="White">Part No&nbsp;</asp:LinkButton>
                                                    <img id="Part_Number" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Part_Number") %>'></asp:Label>
                                                    <asp:Label ID="lblVesselcode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblItemNameHeader" runat="server" CommandName="Sort" CommandArgument="Short_Description"
                                                        ForeColor="White">Item Name&nbsp;</asp:LinkButton>
                                                    <img id="Short_Description" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblItemName" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container,"DataItem.Short_Description") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblUnitHeader" runat="server" ForeColor="White">Unit&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Unit_and_Packings") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Critical">
                                                <HeaderTemplate>
                                                    Critical
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCritical_Flag" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Critical_Flag") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="20px" />
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image Url">
                                                <HeaderTemplate>
                                                    Img Att.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgUrlAtt" runat="server" Visible='<%#Eval("Image_Url_Att").ToString() == "1" ? true : false %>'
                                                        Height="16px" ForeColor="Black" ImageUrl="~/Images/attach-icon.png"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Details URL">
                                                <HeaderTemplate>
                                                    Dtls Att.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgDetialsUrlAtt" runat="server" Visible='<%#Eval("Product_Details_Att").ToString() == "1" ? true : false %>'
                                                        Height="16px" ForeColor="Black" ImageUrl="~/Images/attach-icon.png"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Meat">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMeat" runat="server" Checked='<%#Eval("Meat").ToString() == "1" ? true : false %>'
                                                        onclick='<%# "Update(" + Eval("ID").ToString() + ", this)" %>' />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField  HeaderText="Slopchest Item">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSlopchest" runat="server" ToolTip="Click To Mark this as Slopchest Item" 
                                                        
                                                        Checked='<%#Eval("IsSlopchest") == DBNull.Value ? false : Convert.ToBoolean(Eval("IsSlopchest"))%>'
                                                        onclick='<%# "UpdateSlopChestItems(" + Eval("ID").ToString() + ", this)" %>' />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgItemDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                                    ImageUrl="~/purchase/Image/Delete.gif" OnCommand="ImgItemDelete_Click" CommandArgument='<%#Eval("ID")%>'
                                                                    OnClientClick="return confirm('Are you sure, you want to  delete ?')" Width="16px"
                                                                    Height="12px"></asp:ImageButton>
                                                            </td>
                                                            <td style="border-color: transparent">
                                                                <asp:ImageButton ID="ImgItemRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                                    ImageUrl="~/purchase/Image/restore.png" OnCommand="ImgItemRestore_Click" CommandArgument='<%#Eval("ID")%>'
                                                                    Width="16px" Height="16px"></asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <auc:CustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindItems"
                                        AlwaysGetRecordsCount="false" />
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
                    <td colspan="3" valign="top" align="left" style="background-color: #E0ECF8">
                        <asp:UpdatePanel runat="server" ID="UpdCatalogueEntry" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="width: 500px; border: 1px solid #cccccc;">
                                    <table cellpadding="1" cellspacing="1" style="width: 500px">
                                        <tr>
                                            <td align="right">
                                                Functions : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <ucFunction:ctlFunctionList ID="ddlCatalogFunction" runat="server" Width="120%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                Code :
                                                <asp:Label ID="lbl1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCatalogueCode" MaxLength="8" CssClass="txtInput" runat="server"
                                                    Enabled="false" Width="90px"></asp:TextBox>
                                            </td>
                                            <td style="width: 50%" align="right">
                                                Sets Installed :
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
                                                Name :
                                                <asp:Label ID="lbl2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
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
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Maker : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlCalalogueMaker" CssClass="txtInput" runat="server" Width="100%">
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
                                                Department :<asp:Label ID="lbl3" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlCatalogDept" CssClass="txtInput" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Vessel Location : &nbsp;
                                            </td>
                                            <td colspan="3" valign="top" align="left">
                                                <asp:ListBox ID="lstcatalogLocation" runat="server" CssClass="txtInput" Height="80px"
                                                    Width="100%"></asp:ListBox>
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
                                                <asp:DropDownList ID="ddlAccountCode" CssClass="txtInput" runat="server" Width="100%">
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
                                                <asp:Button ID="btnDivAddLocation" Text="Assign Location" runat="server" Width="110px"
                                                    OnClick="btnDivAddLocation_Click" />
                                                <asp:Button ID="btnCatalogueAdd" Text="Add New" runat="server" Width="70px" OnClientClick="return validationAddCatalogue()"
                                                    OnClick="btnCatalogueAdd_Click" />
                                                <asp:Button ID="btnCatalogueSave" Text="Save" runat="server" Width="70px" OnClientClick="return validationCatalogue()"
                                                    OnClick="btnCatalogueSave_Click" />
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
                    <td colspan="3" valign="top" style="background-color: #E0ECF8">
                        <asp:UpdatePanel runat="server" ID="UpdSubCatalogueEntry" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="width: 500px;">
                                    <table width="500px" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="2">
                                                 <asp:Button ID="btnCopy" Text="Copy From System" runat="server" OnClientClick="CopySystemData(); return false;" />
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
                                                Account Code : &nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="ddlSubCatAccountCode" runat="server" CssClass="txtInput" Width="100%">
                                                </asp:DropDownList>
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
                                                <asp:CheckBox ID="chkRunHourSS" runat="server" Text="Run Hour" OnCheckedChanged="chkRunHourSS_CheckedChanged" AutoPostBack="true"/>
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
                                                <asp:Button ID="btnSubCatDivAddLocation" Text="Assign Location" runat="server" Width="110px"
                                                    OnClick="btnSubCatDivAddLocation_Click" />
                                                <asp:Button ID="btnAddNewSubCatalogue" Text="Add New" runat="server" Width="70px"
                                                    OnClick="btnAddNewSubCatalogue_Click" />
                                                <asp:Button ID="btnSaveSubCatalogue" Text="Save" runat="server" Width="70px" OnClientClick="return validationSubCatalogue();"
                                                    OnClick="btnSaveSubCatalogue_Click" />
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
                    <td colspan="7" valign="top" style="background-color: #E0ECF8">
                        <div style="width: 880px;">
                            <asp:UpdatePanel runat="server" ID="UpdItemEntry" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="1" cellspacing="1" width="880px">
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Drawing Number : &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtItemDrawingNumber" MaxLength="30" Width="200px" runat="server"
                                                    CssClass="txtInput"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Part Number :<asp:Label ID="lbl7" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtItemPartNumber" MaxLength="25" Width="200px" CssClass="txtInput"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Name :<asp:Label ID="lbl8" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtItemName" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Description : &nbsp;
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
                                                Unit : &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlUnit" CssClass="txtInput" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Min Qty : &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMinQty" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Max Qty : &nbsp;
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
                                                Item Category : &nbsp;
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
                                                Image : &nbsp;
                                            </td>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="lnkImageUploadName" Width="370px" runat="server" Target="_blank"> </asp:HyperLink>
                                                            <asp:FileUpload ID="ImageUploader" Style="width: 60%; height: 18px; background-color: #F2F2F2;
                                                                border: 1px solid #cccccc; font-size: 12px; cursor: pointer" runat="server" />
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
                                                Product Details Image :&nbsp;
                                            </td>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="lnkProductDetailUploadName" Width="370px" runat="server" Target="_blank"> </asp:HyperLink>
                                                            <asp:FileUpload ID="DetailsImageUploader" Style="width: 60%; height: 18px; background-color: #F2F2F2;
                                                                border: 1px solid #cccccc; font-size: 12px; cursor: pointer" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="height: 5px">
                                            <td>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkItemCritical" runat="server" Text="Critical" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblItemCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblItemmodifiedby" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblItemDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnAddNewItem" Text="Add New" runat="server" Width="70px" OnClick="btnAddNewItem_Click" />
                                                <asp:Button ID="btnSaveItem" Text="Save" runat="server" Width="70px" OnClientClick="return validationItems()"
                                                    OnClick="btnSaveItem_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblItemErrorMsg" Text="" runat="server" Visible="false" ForeColor="Red"
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSaveItem" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
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
            </table>
        </div>
        <div class="subHeader" style="display: none; position: relative; right: 0px">
            <asp:Button ID="btnMakerHiddenSubmit" runat="server" Text="btnMakerHiddenSubmit"
                OnClick="btnMakerHiddenSubmit_Click" />
        </div>
        <asp:UpdatePanel runat="server" ID="UpdAddLocation">
            <ContentTemplate>
                <div id="divAddLocation" title="Assign Location" style="display: none; border: 1px solid Gray;
                    height: 650px; width: 700px;">
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
                                                    Height="15px" ToolTip="Add New Location" ImageUrl="~/Images/AddMaker.png" />
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
                                                <td align="right" style="width: 17%">
                                                    Short Code :&nbsp;
                                                </td>
                                                <td style="width: 1%; color: Red;">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLoc_ShortCode" Width="260px" runat="server" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    No. Of Loc. :&nbsp;
                                                </td>
                                                <td style="width: 1%; color: Red;">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNoLoc" Width="60px" runat="server" CssClass="txtInput" Text="1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 17%">
                                                    Location Name :&nbsp;
                                                </td>
                                                <td style="width: 1%; color: Red;">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLoc_Description" Width="260px" runat="server" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Button ID="btnDivLocationSave" runat="server" Text="Save Location" OnClick="btnDivLocationSave_Click"
                                                        OnClientClick="return OnSaveLocation();" />
                                                </td>
                                                <td align="right" style="color: red">
                                                    * Mandatory fields.
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="max-height: 500px; overflow: auto; background-color: #006699; z-index: 3;">
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
                                        <auc:CustomPager ID="ucDivCustomPager" runat="server" OnBindDataItem="BindCatalogueAssignLocation" />
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
    <div id="dvIframe" style="display: none; width: 600px;" title=''>
        <iframe id="Iframe" src="" frameborder="0" style="height: 295px; width: 100%"></iframe>
    </div>
</asp:Content>
