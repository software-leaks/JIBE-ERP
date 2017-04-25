<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Delivery_Item.aspx.cs"
    EnableEventValidation="false" Inherits="PO_LOG_PO_Log_Delivery_Item" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }
        function OpenScreen(ID, Item_ID) {
            var DeliveryID = document.getElementById("txtDeliveryID").value;
            var url = 'PO_Log_Delivery_Item_Entry.aspx?ID=' + ID + '&DeliveryID=' + DeliveryID + '&POItemID=' + Item_ID;
            OpenPopupWindowBtnID('Delivery_Item_Entry', 'Delivery Item Entry', url, 'popup', 410, 750, null, null, false, false, true, null, 'btnSearch');
        }
        
    </script>
     <script type="text/javascript">


         function VallidateGrid() {
             // debugger;
             var strCommonPart = "rgdItems_ctl00";
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
                 // var Value = document.getElementById(cntrlId).value;
                 // if (Value != "") {
                 Count++;
//                 if (j <= 8) {
//                     var ChlcntrlId = strCommonPart + SubId + "0" + j + "_txtRequest_Qty";
//                 }
//                 else {
//                     var ChlcntrlId = strCommonPart + SubId + j + "_txtRequest_Qty";
//                 }
                 //                    var Value1 = document.getElementById(ChlcntrlId).value;
                 //                    if (Value1 == "") {
                 //                        alert("Please provide Order Qty for row No:" + (i + 1));
                 //                        return false;
                 //                    }
                 //                }
                 //                else {

                 //                }
                 j = j + 2;

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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div id="page-title" class="page-title">
            Delivery Item
        </div>
        <asp:UpdatePanel ID="upd1" runat="server">
            <contenttemplate>
         <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblCreated" runat="server" Font-Bold="true" Visible="false" Text="Created by :"></asp:Label>
                        <asp:Label ID="lblCreatedBy" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>&nbsp;
                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Visible="false" Text="ON"></asp:Label>&nbsp;
                        <asp:Label ID="lblCreateddate" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Visible="false" Text="ID :"></asp:Label>
                        <asp:Label ID="lblDelveryID" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Delivery Date :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDeliveryDate" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDeliveryDate"
                            Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td align="right">
                        Location :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLocation" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        DPL Port Call :
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddlPortCall" runat="server" CssClass="txtInput" Width="300px">
                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvPort" runat="server" Display="None" InitialValue="0"
                                            ErrorMessage="Port is mandatory field." ControlToValidate="ddlPortCall" ValidationGroup="vgSubmit"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Remarks :
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" MaxLength="255"
                            Width="400px" CssClass="txtInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnSave" runat="server" Width="100px" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgSubmit" />&nbsp;&nbsp;
                        <asp:Button ID="btnload" runat="server" Width="150px" Text="Load Ordered Quantities"
                            Visible="false" />&nbsp;&nbsp;
                        <asp:Button ID="btnAddNonPO" runat="server" Width="120px" Visible="false" OnClientClick='OpenScreen(null,null);return false;'
                            Text="Add Non-PO Items" />&nbsp;&nbsp;
                        <asp:Button ID="btnConfirm" runat="server" Width="120px" Text="Confirm and Lock"
                            OnClick="btnConfirm_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Width="120px" Text="Delete Delivery" OnClick="btnDelete_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnUnlock" runat="server" Visible="false" Width="120px" Text="Unlock"
                            OnClick="btnUnlock_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnClose" Width="100px" runat="server" ForeColor="Red"  
                            Text="Exit" onclick="btnClose_Click" 
                          />
                    </td>
                      <td>
                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                    </td>
                    <td colspan="4" align="center">
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
         <div id="divDeliveryItem"  runat="server" style="height: 480px; overflow-y: scroll;
                max-height: 600px">
               
                 <asp:UpdatePanel ID="UpdGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
               <table id="grideTable" width="100%;" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="49%">
                                            <div class="freezing" style="width: 100%;">
                                                <telerik:RadGrid ID="gvDeliveryItem" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                                    ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                                    Width="98%" AutoGenerateColumns="False" OnItemDataBound="gvDeliveryItem_ItemDataBound"
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
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Item Description" DataField="ID"
                                                                UniqueName="ID" Visible="true">
                                                                <ItemTemplate>
                                                                   <asp:HiddenField ID="lblOrderItemID" runat="server"  Value='<%#Eval("Order_Item_ID")%>' />
                                                                    <asp:HiddenField ID="lblID" runat="server"  Value='<%#Eval("Delivery_Item_ID")%>' />
                                                                    <asp:Label ID="lblPODesc" runat="server"  Text='<%#Eval("Delivered_Item_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                   <FooterTemplate>
                                                                    <asp:Button ID="btnAddNewItem" Text="Add Non-PO Items" BackColor="#0066cc" BorderStyle="None"
                                                                        ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                                                </FooterTemplate>
                                                                <ItemStyle Width="150px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="80px" HeaderText="PO Qty"
                                                                Visible="true" UniqueName="POQty">
                                                                <ItemTemplate>
                                                                <asp:Label ID="lblPOQty" runat="server"  Text='<%#Eval("POQty")%>'></asp:Label>&nbsp;&nbsp;
                                                                  <asp:Label ID="lblUnit" runat="server"  Text='<%#Eval("Delivered_Item_Unit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                             
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle Width="80px"></HeaderStyle>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Price"
                                                                Visible="true" UniqueName="POprice">
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblPOprice" runat="server"  Text='<%#Eval("POprice")%>'></asp:Label>&nbsp;&nbsp;
                                                                       <asp:Label ID="lblPOCurrency" runat="server"  Text='<%#Eval("POCurrency")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                  <FooterTemplate>
                                                                 <asp:Label ID="lblTotal" runat="server"   Text="Delivery Value" 
                                                                 Style="font-size:small;font-weight:bold;" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="60px"></HeaderStyle>
                                                                <ItemStyle Width="60px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="100px" HeaderText="Confirm Qty"
                                                                Visible="true" UniqueName="DeliveryQty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDeliveryQty" Enabled="true" MaxLength="10" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("DeliveryQty")%>' Style="font-size: x-small; text-align:center" Width="70px"
                                                                        Height="15px"></asp:TextBox>&nbsp;&nbsp;
                                                                  <asp:Label ID="lblDUnit" runat="server"  Text='<%#Eval("Delivered_Item_Unit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                               
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Total_QTY")%>' Style="font-size: x-small;font-weight:bold;" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                 <FooterStyle HorizontalAlign="Center" />
                                                                <HeaderStyle Width="100px"></HeaderStyle>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="120px" HeaderText="Confirm Price"
                                                                Visible="true" UniqueName="DeliveryPrice">
                                                                <ItemTemplate>
                                                                 <asp:TextBox ID="txtDeliveryprice"  MaxLength="10" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("DeliveryPrice")%>' Style="font-size: x-small; text-align:center" Width="70px"
                                                                        Height="15px"></asp:TextBox>
                                                                   &nbsp;&nbsp;
                                                                       <asp:Label ID="lblDCurrency" runat="server"  Text='<%#Eval("POCurrency")%>'></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                  <asp:Label ID="lblTotalPrice" runat="server" Text='<%#Eval("Total_Price")%>' Visible="false" Style="font-size: x-small;font-weight:bold;" Width="60px"></asp:Label>
                                                                  </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <HeaderStyle Width="120px"></HeaderStyle>
                                                             
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Delivery Items/Confirmation Remarks" UniqueName="Delivered_Item_Description">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtItem_Description" Text='<%#Eval("Delivered_Item_Description")%>' TextMode="MultiLine" MaxLength="10" Style="font-size: x-small;text-align:left"
                                                                         Height="30px" runat="server" Width="400px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle Width="280px"></HeaderStyle>
                                                                <ItemStyle Width="280px" HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                             <telerik:GridTemplateColumn HeaderText="Invoiced" Visible="false" UniqueName="Invoiced">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lblInvoiced" runat="server"  Text='<%#Eval("Invoiced")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="30px" HeaderText="Action"
                                                                Visible="true" UniqueName="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                        CommandArgument='<%#Eval("[Delivery_Item_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                        Height="16px"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                        <EditFormSettings>
                                                            <PopUpSettings ScrollBars="None" />
                                                            <PopUpSettings ScrollBars="None" />
                                                        <PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /><PopUpSettings ScrollBars="None" /></EditFormSettings>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                             </ContentTemplate>
                         </asp:UpdatePanel>
            </div>
        <div style="display:none;" >
          <asp:TextBox ID="txtSupply_ID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtDeliveryID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" />
        </div>
        </contenttemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
