<%@ Page Title="Contract details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CTP_Contract_Details.aspx.cs" Inherits="Purchase_CTP_Contract_Details"
    EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="ucpager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>

    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 2px 3px 2px 0px;
            min-height: 20px;
            vertical-align: middle;
            font-weight: bold;
            color: Black;
            min-width: 80px;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 2px 2px 2px 3px;
            min-height: 20px;
            vertical-align: middle;
            min-width: 120px;
            color: Black;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            var wh = 'Quotation_ID=<%=Request.QueryString["Quotation_ID"]%> and Contract_ID=<%=Request.QueryString["Contract_ID"]%> ';



            Get_Record_Information_Details('PURC_DTL_CTP_Quotation', wh);

        });

        var UnitsPkgID = '';

        function ConfirmOnRework() {
            var sts = confirm('are you sure to rework ?');
            if (sts)
                return true;
            else
                return false;

        }

        function ConfirmOnRecall() {
            var sts = confirm('are you sure to recall ?');
            if (sts)
                return true;
            else
                return false;

        }

        function ShowUnitsPkg(id, evt) {



            UnitsPkgID = id;
            var divunit = document.getElementById("dvUnitspkg");
            divunit.style.display = "block";
            divunit.style.left = (evt.clientX - 262) + "px";
            divunit.style.top = (evt.clientY + 7) + "px";

        }
        function ChangeUnitsPkg() {

            var ddlUnitspk = document.getElementById("<%=cmbUnitnPackage.ClientID%>");
            var Text = ddlUnitspk.options[ddlUnitspk.selectedIndex].text;
            var Value = ddlUnitspk.options[ddlUnitspk.selectedIndex].value;
            document.getElementById(UnitsPkgID).value = Value;
            document.getElementById("dvUnitspkg").style.display = "none";
            return false;
        }


        function CloseDvUnits() {
            document.getElementById("dvUnitspkg").style.display = "none";
        }
        function checkNumber(id) {

            var obj = document.getElementById(id);
            if (isNaN(obj.value)) {
                obj.value = 0;
                alert("Only number allowed !");
            }

        }

        function CloseDiv() {
            var control = document.getElementById("ctl00_MainContent_DivRemarks");
            control.style.visibility = "hidden";
            return false;

        }

        function SetPositionDvRemark(evt, isset) {

            if (isset != null) {

                document.getElementById('ctl00_MainContent_hdf_divRemarkX').value = evt.clientX;
                document.getElementById('ctl00_MainContent_hdf_divRemarkY').value = evt.clientY;
            }
            else {
                var x = document.getElementById('ctl00_MainContent_hdf_divRemarkX').value;
                var y = document.getElementById('ctl00_MainContent_hdf_divRemarkY').value;

                var dvremark = document.getElementById("ctl00_MainContent_DivRemarks");

                dvremark.style.left = (parseInt(x) - 502) + "px";
                dvremark.style.top = (parseInt(y) + 6) + "px";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    Contract Details
                </td>
                <td style="width: 120px; background-color: Yellow;">
                    <asp:Label ID="lblCurrentSts" ForeColor="Black" Height="20px" BackColor="Yellow"
                        runat="server" Text="sts"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="upditemMain" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" style="border: 1px solid gray" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 44%; border-right: 1px solid gray">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdh">
                                            Supplier Ref. :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblSupplierRef" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdh">
                                            Company Ref. :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblSeachangeRef" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            Supplier :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdh">
                                            Department :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            Port :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblPort" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdh">
                                            Catalogue :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblCatalogue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 40%; border-right: 1px solid gray">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdh">
                                            Effective Date :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblEffectiveDT" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdh">
                                            Approved By :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblApprovedBy" Width="70px" runat="server"></asp:Label><asp:Image
                                                ID="imgApprovedByRmk" ImageUrl="~/Purchase/Image/view1.gif" Height="10px" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            Sent To Supp On :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblSentToSuppDT" Width="70px" runat="server"></asp:Label><asp:Image
                                                ID="imgSentToSuppRmk" ImageUrl="~/Purchase/Image/view1.gif" runat="server" />
                                        </td>
                                        <td class="tdh">
                                            Approved On :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblApprovedDT" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            Submitted By Supp On :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblSubmittedBySupp" Width="70px" runat="server"></asp:Label><asp:Image
                                                ID="imgSubmittedBySuppRmk" ImageUrl="~/Purchase/Image/view1.gif" runat="server" />
                                        </td>
                                        <td class="tdh">
                                            Rejected On :
                                        </td>
                                        <td class="tdd">
                                            <asp:Label ID="lblRejectedDT" Width="70px" runat="server"></asp:Label><asp:Image
                                                ID="imgRejectedDTRmk" ImageUrl="~/Purchase/Image/view1.gif" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left; padding-left: 3px; width: 16%" valign="top">
                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:HyperLink ID="hlnkAddItem" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                BackColor="#EAEAEA" ForeColor="Blue" runat="server" Style="padding: 2px" Text="Add items" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Button ID="btnReworkToSupplier" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                ForeColor="Blue" runat="server" Text="Rework to Supplier" OnClick="btnReworkToSupplier_Click"
                                                Width="120px" OnClientClick="return ConfirmOnRework()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Button ID="btnRecallContract" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                ForeColor="Blue" runat="server" Text="Recall contract" OnClick="btnRecallContract_Click"
                                                Width="120px" OnClientClick="return ConfirmOnRecall()" />
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background: #F7F8E0; border-left: 1px solid gray;
                        border-right: 1px solid gray; padding: 4px 0px 4px 0px">
                        <tr>
                            <td class="tdh">
                                Currency :
                            </td>
                            <td class="tdd">
                                <asp:DropDownList ID="DDLCurrency" AppendDataBoundItems="true" runat="server" Width="86px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDLCurrency" runat="server"
                                    ValidationGroup="saveFZ" Display="None" ErrorMessage="Please select currency !"
                                    ControlToValidate="DDLCurrency" InitialValue="0"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderDDLCurrency" TargetControlID="RequiredFieldValidatorDDLCurrency"
                                    runat="server">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                            <td class="tdh">
                                Truck Charge :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtTruckCharge" runat="server" Width="80px" onchange="checkNumber(id)"
                                    Font-Size="11px"></asp:TextBox>
                            </td>
                            <td class="tdh">
                                Barge Charge :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtBargeCharge" runat="server" Width="80px" onchange="checkNumber(id)"
                                    Font-Size="11px"></asp:TextBox>
                            </td>
                            <td class="tdh">
                                Frieght Charge :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtFrieghtCharge" runat="server" Width="80px" onchange="checkNumber(id)"
                                    Font-Size="11px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Pkg/Handling Charge :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtPkgCharge" runat="server" Width="80px" onchange="checkNumber(id)"
                                    Font-Size="11px"></asp:TextBox>
                            </td>
                            <td class="tdh">
                                Other Charge :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtOtherCharge" runat="server" Width="80px" onchange="checkNumber(id)"
                                    Font-Size="11px"></asp:TextBox>
                            </td>
                            <td class="tdh">
                                VAT/GST(%) :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtVat" runat="server" Width="80px" onchange="checkNumber(id)" Font-Size="11px"></asp:TextBox>
                            </td>
                            <td class="tdh">
                                Discount(%) :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtDiscount" runat="server" Width="80px" onchange="checkNumber(id)"
                                    Font-Size="11px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: White;
                        border: 1px solid gray;">
                        <tr>
                            <td style="text-align: left; width: 100%;" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdh">
                                            Sub Catalogue :
                                        </td>
                                        <td class="tdd">
                                            <asp:DropDownList ID="ddlSubCatalogue" Width="200px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdh">
                                            Item Desc. :
                                        </td>
                                        <td class="tdd">
                                            <asp:TextBox ID="txtItemsDesc" Width="200px" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="tdd">
                                            <asp:RadioButtonList ID="rbtnApprStatus" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="All" Value="0" Selected="0"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Un Approved" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td class="tdd">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnExport" ImageAlign="AbsBottom" ImageUrl="~/Images/XLS.jpg"
                                                Height="25px" OnClick="btnExport_Click" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" valign="top">
                                <asp:GridView ID="gvContractDetails" runat="server" AutoGenerateColumns="false" Width="100%"
                                    CssClass="gridmain-css" EmptyDataText="No record found !" CellSpacing="0" BackColor="#D8D8D8"
                                    GridLines="None" OnRowDataBound="gvContractDetails_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="S.no." DataField="ROWNUM" />
                                        <asp:BoundField HeaderText="Sub Catalogue" DataField="Subsystem_Description" />
                                        <asp:BoundField HeaderText="Part No." DataField="Part_Number" />
                                        <asp:BoundField HeaderText="Short Desc." DataField="Short_Description" />
                                        <asp:BoundField HeaderText="Long Desc." DataField="Long_Description" />
                                        <asp:BoundField HeaderText="Unit" DataField="Unit_and_Packings" />
                                        <asp:TemplateField HeaderText="Offer Unit">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lbtnUnitsPKg" runat="server" BorderStyle="None" Font-Underline="true"
                                                    BackColor="Transparent" ForeColor="Maroon" Width="50px" Style="text-align: center;
                                                    cursor: pointer" Font-Size="11px" onclick="ShowUnitsPkg(id,event);return false;"
                                                    Enabled='<%#Eval("Approved").ToString()=="0" && objUA.Edit!=0?true:false%>' Text='<%#Eval("unit")%>'>
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Price" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtrate" Width="70px" BorderColor="LightGray" ToolTip='<%#Eval("QTN_Item_ID")%>'
                                                    Style="text-align: right" BorderWidth="1px" runat="server" Text='<%#Eval("Rate") %>'
                                                    Font-Size="12px" onchange="checkNumber(id)" Enabled='<%#Eval("Approved").ToString()=="0"?true:false%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discount on Unit Price" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDiscount" Width="70px" BorderColor="LightGray" BorderWidth="1px"
                                                    Style="text-align: right" onchange="checkNumber(id)" runat="server" Font-Size="12px"
                                                    Text='<%#Eval("Discount") %>' Enabled='<%#Eval("Approved").ToString()=="0"?true:false%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Unit Price" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="txtNetPrice" Width="70px" runat="server" Text='<%#Eval("net_price") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Remark" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="updSupplierRemark" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btndivSave" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgSupplierRemark" CommandArgument='<%#Eval("QTN_Item_ID")%>'
                                                            Enabled='<%#Eval("Approved").ToString()=="0" && objUA.Edit!=0?true:false%>' OnClientClick="SetPositionDvRemark(event, '1')"
                                                            Height="14px" Width="12px" OnCommand="imgRemark_Click" CommandName="1" AlternateText='<%#Eval("Supplier_Remark") %>'
                                                            runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Remark" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="updPurchaserRemark" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btndivSave" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgPurchaserRemark" CommandArgument='<%#Eval("QTN_Item_ID")%>'
                                                            Enabled='<%#string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])) ==true &&  Eval("Approved").ToString()=="0" && objUA.Edit!=0?true:false%>' OnClientClick="SetPositionDvRemark(event, '2')"
                                                            Height="12px" Width="12px" OnCommand="imgRemark_Click" CommandName="2" AlternateText='<%#Eval("Purchaser_Remark") %>'
                                                            runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Added Date" ItemStyle-Width="120px" DataField="Date_Of_Created" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDeleteItem" runat="server" Enabled='<%#Eval("Approved").ToString()=="0" && objUA.Delete!=0?true:false%>'
                                                    Text="Remove" OnClientClick="javascript:var sts=confirm('are you sure to remove ?');sts= sts==true?true:false;return sts;"
                                                    CommandArgument='<%#Eval("QTN_Item_ID") %>' OnCommand="btnDeleteItem_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                </asp:GridView>
                                <ucpager:ucCustomPager ID="ucCustomPagerctp" OnBindDataItem="BindDataItems" AlwaysGetRecordsCount="true"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 100%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" style="padding: 3px 0px 3px 0px; width: 50%">
                                            <asp:Label ID="lblApprovedItem_count" ForeColor="Navy" Font-Bold="true" Font-Size="11px"
                                                runat="server"></asp:Label>
                                        </td>
                                        <td style="padding: 3px 0px 3px 0px; width: 50%; text-align: right">
                                            <asp:Button ID="btnSaveAsDraft" runat="server" Height="35px" Text="Save As Draft"
                                                OnClick="btnSaveAsDraft_Click" ValidationGroup="saveFZ" />&nbsp;&nbsp;
                                            <asp:Button ID="btnSubmittoseach" ValidationGroup="saveFZ" runat="server" Height="35px"
                                                Text="Finalize and Submit to Company" OnClick="btnSubmittoseach_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updDivRemarks" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="popup-css" id="DivRemarks" style="border: 1px solid Black; position: fixed;
                        z-index: 2; color: black; width: 475px; padding: 10px" visible="false" runat="server">
                        <center>
                            <table style="height: 50px; width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="border-bottom: 1px solid gray">
                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr align="center">
                                                <td style="background-color: #808080; font-size: small;">
                                                    <asp:Label ID="lblUrgencyTitle" Width="252px" runat="server" Text="Remark" Style="color: #FFFFFF;
                                                        font-weight: 700; font-size: small;"></asp:Label>
                                                </td>
                                                <td align="right" style="width: 16px; background-color: #808080;">
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/Cancel.png" runat="server"
                                                        OnClientClick="return CloseDiv();" Style="font-size: xx-small" Width="12px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="99%" Height="70"
                                                        MaxLength="350"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:UpdatePanel ID="updbtndivSave" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btndivSave" runat="server" Text="Save" Height="24px" Font-Size="Small"
                                                                OnClick="btndivSave_Click" Width="48px" />
                                                            &nbsp;
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                    <asp:Button ID="btndivCancel" runat="server" Text="Cancel" Height="24px" Font-Size="Small"
                                                        OnClientClick="return CloseDiv();" />
                                                </td>
                                            </tr>
                                        </table>
                                </tr>
                            </table>
                        </center>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdf_divRemarkX" runat="server" />
            <asp:HiddenField ID="hdf_divRemarkY" runat="server" />
            <asp:HiddenField ID="hdf_Quotation_Save_Status" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvUnitspkg" style="display: none; position: fixed; left: 25%; top: 21%;
        padding: 30px; border: 1px solid gray; z-index: 310; background-color: Teal">
        <asp:DropDownList ID="cmbUnitnPackage" AutoPostBack="false" runat="server" Width="70px"
            DataTextField="Main_Pack" DataSourceID="ObjectDataSource1" DataValueField="Main_Pack"
            Font-Size="11px">
        </asp:DropDownList>
        <input type="button" id="UnitOk" value="Ok" onclick="ChangeUnitsPkg();"> </input>
        <input type="button" id="Unitcancel" value="Cancel" onclick="CloseDvUnits()"> </input>
        <asp:ObjectDataSource ID="ObjectDataSource1" SelectMethod="SelectUnitnPackageDataSet"
            TypeName="SMS.Business.PURC.BLL_PURC_Purchase" runat="server"></asp:ObjectDataSource>
    </div>
    <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
        background-color: #FDFDFD">
    </div>
</asp:Content>
