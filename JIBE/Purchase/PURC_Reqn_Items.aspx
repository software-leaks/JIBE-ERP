<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PURC_Reqn_Items.aspx.cs"
    Inherits="InvertyItems" Title="Item Selection" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="headcontent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.js"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        .tbl
        {
            border: 1px solid gray;
            height: 90px;
        }
        
        .view-header
        {
            font-size: 14px;
            font-family: Calibri;
            font-weight: bold;
            text-align: left;
            width: 100%;
            color: Black;
            background-color: #81DAF5;
            border-collapse: collapse;
            padding: 2px 0px 2px 3px;
        }
        
        .tbl-content
        {
            border: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
        }
        
        .tbl-footer
        {
            border-bottom: 1px solid #81DAF5;
            border-left: 1px solid #81DAF5;
            border-right: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
            padding: 2px 2px 2px 2px;
        }
        
        .tbl-footer-td
        {
            width: 100%;
            padding: 2px 2px 2px 2px;
            text-align: left;
            background-color: #81DAF5;
        }
        .tdh
        {
            text-align: right;
            padding: 3px 2px 3px 0px;
        }
        .tdd
        {
            text-align: left;
            padding: 3px 2px 3px 0px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function OpenFreeItemScreen() {

            //            if (subcatlogue != "0") {
            var Item_Type = "Free_Item";
            var url = 'PURC_Add_Item.aspx?Item_Type=' + Item_Type;
            OpenPopupWindowBtnID('Add_Item', 'Add Free Text Item', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 2.5, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
            //            }
            //            else {
            //                alert('Please Select the Subcatalog and then Add the item.')
            //}
        }
        function OpenItemCatalogueScreen() {

            if (subcatlogue != "0") {
                var Item_Type = "Catalogue_Item";
                var url = 'PURC_Add_Item.aspx?Item_Type=' + Item_Type;
                OpenPopupWindowBtnID('Add_Item', 'Add Item To Catalogue', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 2, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
            }
            else {
                alert('Please Select the Subcatalog and then Add the item.')
            }
        }
        
    </script>
    <script language="javascript" type="text/javascript">




        function CloseDiv() {

            var control = document.getElementById("ctl00_MainContent_divaddItem");
            control.style.visibility = "hidden";
        }


        function validiatonOnAddButttonClick() {

            //            var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;
            //            var cmbDept = document.getElementById("ctl00_MainContent_cmbDept").value;



            //            if (cmdVessels == "--Select--" || cmdVessels == null) {
            //                alert("Select vessel to proceed.");
            //                document.getElementById("ctl00_MainContent_DDLVessel").focus();
            //                return false;
            //            }

            //            if (cmbDept == "ALL" || cmbDept == null) {
            //                alert("Select department to proceed.");
            //                document.getElementById("ctl00_MainContent_cmbDept").focus();
            //                return false;
            //            }

            return true;
        }


        function validationOnAddItem() {


            var txtPartnumber = document.getElementById("ctl00_MainContent_txtPartnumber").value
            var txtShortDescription = document.getElementById("ctl00_MainContent_txtShortDescription").value;
            var UnitPckg = document.getElementById("ctl00_MainContent_cmbUnitnPackage").value;

            txtPartnumber = txtPartnumber.trim();
            txtShortDescription = txtShortDescription.trim();

            if (txtPartnumber == "" || txtPartnumber == null) {
                alert("Please enter Part number");
                document.getElementById("ctl00_MainContent_txtPartnumber").focus();

                return false;
            }

            if (txtShortDescription == "" || txtShortDescription == null) {
                alert("Please enter Item.");
                document.getElementById("ctl00_MainContent_txtShortDescription").focus();
                return false;
            }

            if (UnitPckg == "--Select--" || UnitPckg == "0") {
                alert("Please select Unit.");
                document.getElementById("ctl00_MainContent_cmbUnitnPackage").focus();
                return false;
            }

            return true
        }



        function validationOnSearch() {

            //  debugger;
            var txtSearchID = document.getElementById("ctl00_MainContent_txtSrchPartNo").value;
            var txtSearchDesc = document.getElementById("ctl00_MainContent_txtSrchDesc").value;

            if ((txtSearchID == "") && (txtSearchDesc == "")) {
                alert("Please enter Part No. or Description field to search items.");
                // inlineMsg('ctl00_MainContent_DDLFilter','<strong>Error</strong><br />Please Select Value That You Need To Filter By.',2);
                return false;
            }


            return true
        }



     
        function calculate(price, rowId) {

            var strValue = document.getElementById('ctl00_MainContent_HiddenValues').value + rowId + ",";
            document.getElementById('ctl00_MainContent_HiddenValues').value = strValue;

            if (document.getElementById('txtgrdItemReqstdQty').value != '0.00') {
                document.getElementById('ctl00_MainContent_ConfirmValue').value = 1;

            }

        }

        function UpdatedROBIndex(price, rowId) {

            var strValue = document.getElementById('ctl00_MainContent_HiddenValuesROB').value + rowId + ",";
            document.getElementById('ctl00_MainContent_HiddenValuesROB').value = strValue;

            document.getElementById('ctl00_MainContent_HiddenValues').value = document.getElementById('ctl00_MainContent_HiddenValues').value + rowId + ",";
            if (document.getElementById('txtROB').value != '' && document.getElementById('txtROB').value != '0') {
                document.getElementById('ctl00_MainContent_ConfirmValue').value = 1;
            }
        }

        function ValidateText(i) {
            if ((i.value == "0.")) {
                alert("Please enter correct value in ROB.");
            }
        }
       
       
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
       Create New Requisition
    </div>
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
    <div style="font-size: 14px; background-color: #5588BB; color: White; text-align: center;
        padding: 2px 0px 2px 0px; margin: 0px 0px 0px 0px;">
        <b>New Requisition </b>
    </div>
    <div>
        <table id="Table2" cellpadding="0" cellspacing="1" width="100%" style="background-color: #f4ffff;
            color: Black">
            <tr>
                <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="tbl" style="width: 100%">
                        
                        <tr>
                            <td class="tdh">
                                Fleet :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblfleet" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Vessel :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblVessel" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="tbl" style="width: 100%">
                        <tr>
                           <td class="tdh">
                                Requisition Type :
                            </td>
                            <td class="tdd">
                               <asp:Label ID="lblRequisitionType" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Department/Function :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblDepartmentName" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="tbl" style="width: 100%">
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="Label2" ForeColor="Black" runat="server" Text="System/Catalogue:" Style="font-weight: bold;"></asp:Label>&nbsp;
                            </td>
                            <td style="text-align: left" class="tdd">
                                <asp:Label ID="lblCatalouge" runat="server" Font-Size="12px" Width="250px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="Label3" ForeColor="Black" runat="server" Text="Urgency:" Style="font-weight: bold;"></asp:Label>&nbsp;
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblUrgency" runat="server" Font-Size="12px" Width="153px"></asp:Label>
                            </td>
                        </tr>
                      
                    </table>
                </td>
                <td style="width: 20%">
                    <table class="tbl" style="width: 100%">
                        <tr>
                            <td class="tdh" style="text-align: left; vertical-align: top">
                                Item Search
                            </td>
                            <td class="tdd">
                                <asp:Button ID="btnItemSearch" runat="server" Text="Search" OnClientClick="return validationOnSearch();"
                                    OnClick="btnItemSearch_Click" TabIndex="8" />
                                <asp:UpdatePanel ID="updAddnewItems" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add Item" OnClick="btnAdd_Click" OnClientClick="return validiatonOnAddButttonClick();"
                                            Enabled="False" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                <asp:Label ID="lblSrchDesc" runat="server" Text="Description :"></asp:Label>
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtSrchDesc" runat="server" CssClass="txt-css" Style="width: 140px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                <asp:Label ID="lblSrchPartNo" runat="server" Text="Part No : "></asp:Label>
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtSrchPartNo" runat="server" CssClass="txt-css" Width="140px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="grideTable" cellpadding="0" width="100%" cellspacing="0">
                    <tr>
                        <td align="left" style="height: 525px; width: 18%;" valign="top">
                            <div class="Freezing" style="overflow: auto; margin-left: 0px; cursor: hand; width: 98%;
                                height: 525px">
                                <telerik:RadGrid ID="rgdSubCatalog" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                    Width="98%" AlternatingItemStyle-BackColor="#CEE3F6" Skin="Office2007" HeaderStyle-HorizontalAlign="Center"
                                    Style="margin-left: 0px" HeaderStyle-Height="25px" OnSelectedIndexChanged="rgdSubCatalog_SelectedIndexChanged"
                                    AutoGenerateColumns="False" OnItemDataBound="rgdSubCatalog_ItemDataBound">
                                    <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="SUBSYSTEM_CODE" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-VerticalAlign="Middle" HeaderText="SubSysCode" UniqueName="SUBSYSTEM_CODE"
                                                Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle Width="1px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SUBSYSTEM_DESCRIPTION" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-VerticalAlign="Middle" HeaderText="     Sub Catalogues      " UniqueName="SUBSYSTEM_DESCRIPTION">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle Width="125px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="None" />
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                        <td style="width: 66%; height: 525px">
                            <asp:UpdatePanel ID="updItems" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    
                                    <asp:AsyncPostBackTrigger ControlID="btnSaveAdd" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="DivItembtnSave" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="freezing" style="overflow: auto; margin-left: 0px; height: 525px; width: 100%">
                                        <asp:HiddenField ID="HiddenValues" runat="server" Value="" />
                                        <asp:HiddenField ID="HiddenValuesROB" runat="server" Value="" />
                                        <telerik:RadGrid ID="rgdItems" runat="server" HeaderStyle-Height="25px" CellPadding="0"
                                            AllowPaging="false" Width="98%" CellSpacing="0" AllowSorting="false" AllowAutomaticInserts="false"
                                            GridLines="None" AlternatingItemStyle-BackColor="#CEE3F6" Skin="Office2007" HeaderStyle-HorizontalAlign="Center"
                                            Style="margin-left: 0px" AutoGenerateColumns="False" PageSize="15" 
                                            TabIndex="6">
                                            <MasterTableView AllowMultiColumnSorting="True">
                                                <RowIndicatorColumn Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Requisi. Code" DataField="REQUISITION_CODE"
                                                        UniqueName="REQUISITION_CODE" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="ID" DataField="ID" UniqueName="ID"
                                                        Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Vessel Code" DataField="Vessel_Code"
                                                        UniqueName="Vessel_Code" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Department Code" DataField="Department_Code"
                                                        UniqueName="Department_Code" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="System Code" DataField="System_Code"
                                                        UniqueName="System_Code" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Subsystem_Code" DataField="Subsystem_Code"
                                                        UniqueName="Subsystem_Code" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Item_Ref_Code" DataField="Item_Ref_Code"
                                                        UniqueName="Item_Ref_Code" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" SortExpression="Drawing_Number" HeaderText="Drawing No."
                                                        DataField="Drawing_Number" UniqueName="Drawing_Number" Visible="false">
                                                        <ItemStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" SortExpression="Part_Number" HeaderText="Part No."
                                                        DataField="Part_Number" UniqueName="Part_Number" Visible="true">
                                                        <ItemStyle Width="60px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" SortExpression="Short_Description"
                                                        HeaderText="Description" DataField="Short_Description" UniqueName="Short_Description">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Unit" DataField="Unit_and_Packings"
                                                        UniqueName="Unit_and_Packings">
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="50px" HeaderText="ROB"
                                                        Visible="true" UniqueName="Inventory_Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtROB" Height="11px" ClientIDMode="Static" EnableViewState="true"
                                                                runat="server" Text='<%# Bind("Inventory_Qty")%>' Style="font-size: 10px; text-align: right"
                                                                Width="50px" onFocusout="ValidateText(this);"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regExeROB" Display="Dynamic" ControlToValidate="txtROB"
                                                                runat="server" ErrorMessage="*" ToolTip="Only Allow Numeric or Decimal Entry"
                                                                ValidationExpression="\d+\.?\d*">

                                                            </asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="45px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Repair Qty" DataField="TO_Repair_Qty"
                                                        UniqueName="TO_Repair_Qty" DataFormatString="{0:N0}" Visible="false">
                                                        <ItemStyle Width="50px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Used Items" DataField="Used_Items"
                                                        UniqueName="Used_Items" Visible="false">
                                                        <ItemStyle Width="5px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Wkg. Qty" DataField="Working_Qty"
                                                        UniqueName="Working_Qty" DataFormatString="{0:N0}" Visible="false">
                                                        <ItemStyle Width="45px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Min Qty" DataField="Min_Qty"
                                                        UniqueName="Min_Qty" DataFormatString="{0:N0}" Visible="false">
                                                        <ItemStyle Width="45px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Max Qty" DataField="Max_Qty"
                                                        UniqueName="Max_Qty" DataFormatString="{0:N0}" Visible="false">
                                                        <ItemStyle Width="45px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Item Address" DataField="Item_Address"
                                                        UniqueName="Item_Address" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Delivered Items" DataField="Delivered_Items"
                                                        UniqueName="Delivered_Items" Visible="false">
                                                        <ItemStyle Width="40px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Inv. Type" DataField="Inventory_Type"
                                                        UniqueName="Inventory_Type" Visible="false">
                                                        <ItemStyle Width="2px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Long Description" DataField="Long_Description"
                                                        UniqueName="Long_Description" Visible="false">
                                                        <ItemStyle Width="20px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="50px" HeaderText="Reqst Qty"
                                                        UniqueName="Request_qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgrdItemReqstdQty" ClientIDMode="Static" Height="11px" Enabled="true"
                                                                EnableViewState="true"  runat="server" Text='<%# Bind("Required")%>'
                                                                Style="font-size: 10px; text-align: right" Width="50px"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnIsEdit" runat="server" />
                                                            <asp:RegularExpressionValidator Display="Dynamic" ID="regExe" ControlToValidate="txtgrdItemReqstdQty"
                                                                runat="server" ErrorMessage="*" ToolTip="Only Allow Numeric or Decimal Entry"
                                                                ValidationExpression="\d+\.?\d*">

                                                            </asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Comments"
                                                        UniqueName="Item_Comments">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgrdItemComments" EnableViewState="true" Height="11px" Enabled="true"
                                                                Width="120px" TextMode="MultiLine" Text='<%# Bind("Item_comments")%>' runat="server"
                                                                Style="font-size: 10px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None" />
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <uc1:ucCustomPager ID="ucCustomPager1" OnBindDataItem="BindItems" AlwaysGetRecordsCount="true"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updDivAll" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                <%-- <asp:AsyncPostBackTrigger ControlID="btnNewRequistion" EventName="Click" />--%>
            </Triggers>
            <ContentTemplate>
                <div id="divaddItem" style="border: 1px solid Black; background-color: #E0E0E0; height: 330px;
                    width: 500px; position: absolute; left: 35%; top: 20%; z-index: 2; color: black;
                    padding-left: 30px; padding-right: 10px" runat="server">
                    <table style="height: 200px; width: 100%" cellpadding="1" cellspacing="2">
                        <tr align="left">
                            <td>
                                <table style="width: 102%" cellpadding="3" cellspacing="0">
                                    <tr align="center">
                                        <td style="font-size: small;">
                                            <asp:Label ID="lblItemTitle" Width="152px" runat="server" Text="Add Item" Style="color: Black;
                                                font-weight: 700;"></asp:Label>
                                        </td>
                                        <td style="width: 16px">
                                            <img src="Image/Close.gif" alt="Click to close." width="12px" height="13px" onclick="JavaScript:CloseDiv();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" cellpadding="1" cellspacing="2">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblCatalogueHead" runat="server" Text="Catalogue">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" valign="top">
                                            <asp:Label ID="Label18" Width="0px" runat="server" Style="color: #000; font-size: small;"
                                                Text=":"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCatalogueIT" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblSubCatalogueHead" runat="server" Text="SubCatalogue">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" valign="top">
                                            <asp:Label ID="Label20" Width="0px" runat="server" Style="color: #000; font-size: small;"
                                                Text=":"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblSubCatalogueIT" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="Label5" runat="server" Text="Drawing Number">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" valign="top">
                                            <asp:Label ID="Label16" Width="0px" runat="server" Style="color: #000; font-size: small;"
                                                Text=":"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDrawingNumber" Style="font-size: small" runat="server" MaxLength="30"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="Label6" runat="server" Text="Part Number">
                                            </asp:Label>&nbsp;
                                            <asp:Label ID="lblR1" runat="server" Style="color: #FF0000; font-size: small;" Text="*"></asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" valign="top">
                                            <asp:Label ID="lbl" runat="server" Style="color: #000; font-size: small;" Text=":"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPartnumber" runat="server" Style="font-size: small" MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="Label7" runat="server" Text="Item" Style="font-size: small">
                                            </asp:Label>&nbsp;
                                            <asp:Label ID="Label10" runat="server" Style="color: #FF0000; font-size: small;"
                                                Text="*"></asp:Label>
                                        </td>
                                        <td style="width: 1px;" align="left" valign="top">
                                            <asp:Label ID="Label9" runat="server" Style="color: #000; font-size: small;" Text=":"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtShortDescription" runat="server" Height="35px" Rows="2" Width="310px"
                                                Style="font-size: small" MaxLength="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="Label8" runat="server" Text="Long Description" Style="font-size: small">
                                            </asp:Label>
                                        </td>
                                        <td width="1px" align="left" valign="top">
                                            <asp:Label ID="Label14" Width="0px" runat="server" Style="color: #000; font-size: small;"
                                                Text=":"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLongDescription" runat="server" Height="35px" Style="font-size: small"
                                                Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="Label11" runat="server" Text="Unit" Style="font-size: small"></asp:Label>&nbsp;
                                            <asp:Label ID="Label13" Width="0px" runat="server" Style="color: #FF0000; font-size: small;"
                                                Text="*"></asp:Label>
                                        </td>
                                        <td width="1px" valign="top" align="left">
                                            <asp:Label ID="Label12" runat="server" Style="color: #000; font-size: small;" Text=":"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="cmbUnitnPackage" runat="server" Width="140px" Style="font-size: small"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Value="--Select--">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtUnitPackage" runat="server" Visible="false" Style="font-size: small">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="7" align="center">
                                            <asp:Button ID="btnSaveAdd" runat="server" Text="Save & Add" OnClick="DivItembtnSave_Click"
                                                OnClientClick="return validationOnAddItem();" Style="font-size: small;" />
                                            &nbsp;
                                            <asp:Button ID="DivItembtnSave" runat="server" Text="Save & Close" OnClick="DivItembtnSave_Click"
                                                OnClientClick="return validationOnAddItem();" Style="font-size: small;" />
                                            &nbsp;
                                            <input type="button" name="btnCancel" style="font-size: small; width: 80px;" onclick="JavaScript:CloseDiv();"
                                                value="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" style="color: #FF0000; font-size: small; margin-top: 0px" align="right">
                                * Indicates as Mandatory field
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblSrchDrawingNo" Visible="false" runat="server"></asp:Label>
        <asp:TextBox ID="txtSrchDrawingNo" runat="server" Visible="false"></asp:TextBox>
        <asp:HiddenField ID="ConfirmValue" runat="server" />
         <asp:HiddenField ID="hdnVesselID" runat="server" Value="" />
                                    <asp:HiddenField ID="hdnPOType" runat="server" Value="" />
        <asp:ObjectDataSource ID="objsrcReqsnType" SelectMethod="Get_ReqsnType" TypeName="ClsBLLTechnical.TechnicalBAL"
            runat="server"></asp:ObjectDataSource>
    </div>
    <table style="width: 100%" style="border: 1px solid gray; height: 45px;" cellpadding="0"
        cellspacing="0">
        <tr>
            <td class="tdh">
                <asp:Label ID="lblError" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnPreview" runat="server" Text="Preview & Finalize" Width="130px"
                    OnClick="btnPreview_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
