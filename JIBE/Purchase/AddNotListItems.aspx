<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddNotListItems.aspx.cs"
    Inherits="AddNotListItems" Title="Add Items" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script runat="server">

     
</script>
<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">



        function validiatonOnAddButttonClick() {
            var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
            var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var cmbDept = document.getElementById("ctl00_MainContent_cmbDept").value;

            if (cmdFleet == "0" || cmdFleet == null) {
                alert("Select fleet and vessel to proceed.");
                return false;
            }

            if (cmdVessels == "--Select--" || cmdVessels == null) {
                alert("Select vessel to proceed.");
                return false;
            }

            if (cmbDept == "ALL" || cmbDept == null) {
                alert("Select department to proceed.");
                return false;
            }

            return true;
        }


        function validationOnAddItem() {
            var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
            var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var cmbDept = document.getElementById("ctl00_MainContent_cmbDept").value;
            var txtPartnumber = document.getElementById("ctl00_MainContent_txtPartnumber").value
            var txtShortDescription = document.getElementById("ctl00_MainContent_txtShortDescription").value;
            var txtROB = document.getElementById("ctl00_MainContent_txtROB").value;
            var txtMin = document.getElementById("ctl00_MainContent_txtMin").value;
            var txtMax = document.getElementById("ctl00_MainContent_txtMax").value;
            var UnitPckg = document.getElementById("ctl00_MainContent_cmbUnitnPackage").value;

            if (cmdFleet == "0" || cmdFleet == null) {
                alert("Select fleet, vessel and click save button.");
                return false;
            }

            if (cmdVessels == "--Select--" || cmdVessels == null) {
                alert("Select vessel and click save button.");
                return false;
            }

            if (cmbDept == "ALL" || cmbDept == null) {
                alert("Select department and click Save button.");
                return false;
            }



            if (txtPartnumber == "" || txtPartnumber == null) {
                alert("Please enter Part number");
                return false;
            }

            if (txtShortDescription == "" || txtShortDescription == null) {
                alert("Please enter System Description.");
                return false;
            }

            if (txtROB != "") {
                if (isNaN(txtROB)) {
                    alert("ROB field only allow numeric value.");
                    return false;
                }
            }

            if (txtMin != "") {
                if (isNaN(txtMin)) {
                    alert("Min field only allow numeric value.");
                    return false;
                }
            }

            if (txtMax != "") {
                if (isNaN(txtMax)) {
                    alert("Max field only allow numeric value.");
                    return false;
                }
            }

            if (UnitPckg == "--Select--" || UnitPckg == "0") {
                alert("Select Unit and Package.");
                return false;
            }

            return true
        }



        function validationOnSearch() {


            var txtSearchID = document.getElementById("ctl00_MainContent_txtSrchDrawingNo").value;
            var txtSearchDesc = document.getElementById("ctl00_MainContent_txtSrchDesc").value;

            if ((txtSearchID == "") && (txtSearchDesc == "")) {
                alert("Please enter Id or Description field to search items.");
                // inlineMsg('ctl00_MainContent_DDLFilter','<strong>Error</strong><br />Please Select Value That You Need To Filter By.',2);
                return false;
            }

            return true
        }







        function validationOnPriorityOK() {
            //debugger; 
            var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
            var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;
            var cmdDept = document.getElementById("ctl00_MainContent_cmbDept").value;

            if (cmdFleet == "0" || cmdFleet == null) {
                alert("Select fleet, vessel and click OK button.");
                return false;
            }

            if (cmdVessels == "--Select--" || cmdVessels == null) {
                alert("Select vessel and click search button.");
                return false;
            }

            if (cmdDept == "ALL" || cmdDept == null) {
                alert("Select Department to proceed");
                return false;
            }

            return true
        }

        function PendingReqUpdateDelivers() {
            //var fleet= document.getElementById("ctl00_MainContent_DDLFleet").value;
            //var vessel=document.getElementById("ctl00_MainContent_DDLVessel").value;

            //javascript:window.open('PendingRequisitionDetails.aspx?fleetCode='+fleet+'&Vessecode='+vessel);

            javascript: window.open('PendingRequisitionDetails.aspx');

        }


        function CheckDeptComboSelect() {
            var cmbDept = document.getElementById("ctl00_MainContent_cmbDept").value;

            if (cmbDept == "0") {
                alert("Select Department Code to proceed.");
                return false;
            }
        }

        function CheckRequistionComboSelect() {

            var cmbRequisition = document.getElementById("ctl00_MainContent_cmbRequisitionList").value;

            if (cmbRequisition == "0") {
                alert("To see the Preview,Please Select the Requisition Number.");
                return false;
            }
            else {
                var ChangesOrNot = document.getElementById("ctl00_MainContent_ConfirmValue").value;
                if (ChangesOrNot == 1) {
                    var retTrueFalse = confirm("Do you want to save changes?");
                    if (retTrueFalse == true) {
                        return true;
                    }
                    else {
                        document.getElementById('ctl00_MainContent_ConfirmValue').value = 0;
                        return true;
                    }
                }
                else {

                }
            }

        }
        //    debugger; 
        function calculate(price, rowId) {
            //             var gridSupp =$find("<%= rgdItems.ClientID %>");
            //             var masterTableSupp = gridSupp.get_masterTableView(); 
            //             var cellSupp; 
            //             cellSupp= masterTableSupp.get_dataItems()[i].get_element();
            var strValue = document.getElementById('ctl00_MainContent_HiddenValues').value + rowId + ",";
            document.getElementById('ctl00_MainContent_HiddenValues').value = strValue;
            document.getElementById('ctl00_MainContent_ConfirmValue').value = 1;

        }

        function ValidateText(i) {
            //         debugger; 
            if ((i.value == "0") || (i.value == "0.0") || (i.value == "0.00") || (i.value == "0.") || (i.value == "")) {
                alert("You have entered 0-value in Req Qty.");
            }
        }
        function VallidateGrid() {
            // debugger;
            var strCommonPart = "ctl00_MainContent_rgdItems_ctl00";
            var SubId = "_ctl";
            var j = 4;
            var Count = 0;
            var Len = strCommonPart.length;
            for (var i = 0; i < 10; i++) {

                if (j <= 8) {
                    var cntrlId = strCommonPart + SubId + "0" + j + "_txtItem_Description";
                }
                else {
                    var cntrlId = strCommonPart + SubId + j + "_txtItem_Description";
                }
                if (document.getElementById(cntrlId) != null) {
                    var Value = document.getElementById(cntrlId).value;
                    if (Value != "") {
                        Count++;
                        if (j <= 8) {
                            var ChlcntrlId = strCommonPart + SubId + "0" + j + "_txtRequest_Qty";
                        }
                        else {
                            var ChlcntrlId = strCommonPart + SubId + j + "_txtRequest_Qty";
                        }
                        var Value1 = document.getElementById(ChlcntrlId).value;
                        if (Value1 == "") {
                            alert("Please provide Requested Qty for row No:" + (i + 1));
                            return false;
                        }
                    }
                    else {

                    }
                    j = j + 2;
                }
            }
            if (Count == 0) {
                alert("Please provide atleast one ItemDescription");
                return false;
            }
            return true;
        }
        function MaskMoney(evt) {
            if (!(evt.keyCode == 9 || evt.keyCode == 190 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="main" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                                        <b>Add Items</b>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="5" cellspacing="0" align="center" style="width: 100%; background-color: #F7F7F7;
                                color: #333333">
                                <tr>
                                    <td align="left" style="width: 99px;">
                                        Requisition :
                                    </td>
                                    <td style="width: 116px;" align="left">
                                        <asp:Label ID="lblRequsition" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblFleet" runat="server" Visible="false" Text="bcvbc"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 85px;">
                                        Catalogue :
                                    </td>
                                    <td align="left" style="width: 114px">
                                        <asp:Label ID="lblCatalogue" runat="server" Text="" Width="180px"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 50px;">
                                        Vessel :
                                    </td>
                                    <td align="left" style="width: 50px;">
                                        <asp:Label ID="lblVessel" runat="server" Text="" Width="120px"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 71px;">
                                        Department :
                                    </td>
                                    <td align="left" style="width: 71px;">
                                        <asp:Label ID="lblDepartment" runat="server" Text="" Width="150px"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <a id="btnViewRequistion" style="color: Blue; cursor: pointer;" onclick="return PendingReqUpdateDelivers()">
                                            View Requisitions</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="text-align: left; background-color: #04B4AE;
                    color: Black; font-weight: bold; padding: 6px 0px 6px 0px; font-size: 12px" width="100%">
                    <tr>
                        <td style="width: 30%">
                            <asp:RadioButtonList runat="server" Width="100%" ID="rbtlst" OnSelectedIndexChanged="rbtlst_OnSelectedIndexChanged"
                                AutoPostBack="true" CellSpacing="10" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Add New Item(s)" Value="AddNewItem" Selected="True"/>
                                <asp:ListItem Text="Add item(s) from library" Value="AddNewItemLib" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0" id="tblfilter" runat="server"
                    style="font-size: 11px; font-weight: bold; color: #6E6E6E; padding: 6px 0px 6px 0px;
                    border: 1px solid gray">
                    <tr>
                        <td>
                            Sub Catalogue :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubCatalogue" runat="server" Width="160px" Font-Size="12px"
                                TabIndex="3" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Part No. :
                        </td>
                        <td>
                            <asp:TextBox ID="txtpartno" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Draw. No. :
                        </td>
                        <td>
                            <asp:TextBox ID="txtdrawNo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Description :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescpt" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearchLib" runat="server" Text="Search" OnClick="btnSearchLib_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9" align="left">
                            <div style="width: 100%; overflow: auto; max-height: 200px">
                         
                                <asp:GridView ID="gvItemsTemp" AutoGenerateColumns="false" runat="server" Width="100%">
                                    <HeaderStyle ForeColor="White" BackColor="#5D7B9D" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Add to list " ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px"
                                            ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAddToList" runat="server" Text="Add" ToolTip='<%#Eval("Item_Intern_Ref")%>'
                                                    OnClick="btnAddToList_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Subsystem_Code" HeaderText="Sub Catalogue" HeaderStyle-Width="150px"
                                            ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="Unit_and_Packings" HeaderText="Unit" />
                                        <asp:BoundField DataField="Part_Number" HeaderText="Part No." HeaderStyle-Width="100px"
                                            ItemStyle-Width="150px" />
                                        <asp:TemplateField HeaderText="Draw. No. " ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px"
                                            ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrawNo" runat="server" Text='<%#Eval("Drawing_Number")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Add to list " ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="300px"
                                            ItemStyle-Width="300px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Short_Description")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                              
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="grideTable" width="100%;" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="49%">
                                    <div class="freezing" style="width: 100%;">
                                        <telerik:RadGrid ID="rgdItems" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                            ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                            Width="100%" AutoGenerateColumns="False" OnItemDataBound="rgdItems_ItemDataBound"
                                            AllowMultiRowSelection="True" PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center"
                                            AlternatingItemStyle-BackColor="#CEE3F6">
                                            <MasterTableView>
                                                <RowIndicatorColumn Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ID" DataField="ID"
                                                        UniqueName="ID" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkApprove" ValidationGroup='<%#Eval("PKID")+","+Eval("Office_ID")%>'
                                                                Visible='<%#Eval("chkApprove")%>' runat="server" />
                                                            &nbsp
                                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Item Description"
                                                        Visible="true" UniqueName="Item_Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtItem_Description" Enabled="true" EnableViewState="true" runat="server"
                                                                Text='<%# Bind("Item_Description")%>' Style="font-size: x-small" Width="300px"
                                                                Height="15px" TextMode="MultiLine"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddNewItem" Text="Add new item" BackColor="#0066cc" BorderStyle="None"
                                                                ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle Width="300px"></HeaderStyle>
                                                        <ItemStyle Width="300px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="PartNo"
                                                        Visible="true" UniqueName="part">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPartNumber" Enabled="true" EnableViewState="true" runat="server"
                                                                Text='<%# Bind("Part_Number")%>' Style="font-size: x-small" Width="70px" Height="10px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Drawing No"
                                                        Visible="true" UniqueName="Drawing">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDrawingNumber" Enabled="true" EnableViewState="true" runat="server"
                                                                Text='<%# Bind("Drawing_Number")%>' Style="font-size: x-small" Width="70px" Height="10px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Unit"
                                                        Visible="true" UniqueName="Unit">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="cmbUnitnPackage" runat="server" Width="70px" DataTextField="Main_Pack"
                                                                AppendDataBoundItems="true" DataSourceID="ObjectDataSource1" DataValueField="Main_Pack"
                                                                Text='<%# Bind("Unit")%>' Font-Size="11px">
                                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Request Qty"
                                                        Visible="true" UniqueName="Request_Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRequest_Qty" Enabled="true" EnableViewState="true" runat="server"
                                                                Text='<%# Bind("Request_Qty")%>' Style="font-size: x-small" Width="70px" Height="10px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Unit Price" UniqueName="UnitPrice">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnitPrice" Text='<%#Eval("Unit_price")%>' Style="font-size: x-small"
                                                                Height="10px" runat="server" Width=" 70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                        <ItemStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Discount" UniqueName="Discount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDiscount" Text='<%#Eval("Discount") %>' Style="font-size: x-small"
                                                                Height="10px" runat="server" Width=" 70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                        <ItemStyle Width="70px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Account Code" UniqueName="ddlBudgetCode">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlBudgetCode" DataSourceID="ObjSrcBgtCode" Text='<%#Eval("BudgetCode") %>'
                                                                DataTextField="Budget_Name" DataValueField="Budget_Code" runat="server" Font-Size="11px"
                                                                Width="100px" AppendDataBoundItems="True">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Item Comments"
                                                        Visible="true" UniqueName="Item_Comments">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtItem_Comments" Enabled="true" EnableViewState="true" runat="server"
                                                                Text='<%# Bind("Item_Comments")%>' Style="font-size: x-small" Width="180px" Height="12px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="180px"></HeaderStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn DataField="Item_Intern_Ref" Display="false" HeaderText="Item_Intern_Ref">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefCode" runat="server" Text='<%# Bind("Item_Intern_Ref")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None" />
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:ObjectDataSource ID="ObjSrcBgtCode" SelectMethod="SelectBudgetCode" TypeName="SMS.Business.PURC.BLL_PURC_Purchase"
                            runat="server"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="ObjectDataSource1" SelectMethod="SelectUnitnPackageDataSet"
                            TypeName="SMS.Business.PURC.BLL_PURC_Purchase" runat="server"></asp:ObjectDataSource>
                        <table align="center" style="width: 100%; background-color: #D1F4FF; margin-top: 2px"
                            cellpadding="5" cellspacing="0">
                            <tr>
                                <td style="width: 100%; text-align: left">
                                    <b style="color: Maroon; font-size: 14px">Select Supplier(s):</b>&nbsp;&nbsp;&nbsp;<span
                                        style="font-size: 10px; color: Red; font-family: Verdana"> [RFQs have been sent
                                        to below supplier(s). Select the supplier to include the newly added item in the
                                        RFQ. ]</span><br />
                                    <asp:CheckBoxList ID="chkListSupplier" DataTextField="Full_NAME" DataValueField="QUOTATION_CODE"
                                        RepeatDirection="Horizontal" ForeColor="Black" Font-Size="11px" runat="server">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                        <table align="center" style="height: 0px; margin-top: 5px; background-color: #F7F7F7;
                            width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 100%; height: 30px;" align="center">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="30px" OnClick="btnSave_Click"
                                        Enabled="true" Width="80px" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" Height="30px"
                                        OnClientClick="javascript:window.close()" />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Width="629px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenValues" runat="server" Value="" />
                <asp:HiddenField ID="ConfirmValue" runat="server" />
            </ContentTemplate>
           
        </asp:UpdatePanel>
    </center>
</asp:Content>
