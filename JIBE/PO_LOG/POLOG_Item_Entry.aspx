<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POLOG_Item_Entry.aspx.cs"
    Inherits="PO_LOG_POLOG_Item_Entry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script type="text/javascript">

        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }

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
                if (j <= 8) {
                    var ChlcntrlId = strCommonPart + SubId + "0" + j + "_txtRequest_Qty";
                }
                else {
                    var ChlcntrlId = strCommonPart + SubId + j + "_txtRequest_Qty";
                }
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
    <div>
        <center>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="page-title" class="page-title">
                            PO Log Item Entry
                        </div>
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
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Srno." DataField="ID"
                                                                UniqueName="ID" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="lblID" runat="server"  Value='<%#Eval("ID")%>' />
                                                                    <asp:Label ID="lblSrno" runat="server"  Text='<%#Eval("Srno")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Code"
                                                                Visible="true" UniqueName="Item_Code">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtItem_Code" Enabled="true" MaxLength="20" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Item_Code")%>' Style="font-size: x-small; text-align:center" Width="80px"
                                                                        Height="15px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Button ID="btnAddNewItem" Text="Add new item" BackColor="#0066cc" BorderStyle="None"
                                                                        ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle Width="80px"></HeaderStyle>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Item Short Description"
                                                                Visible="true" UniqueName="Item_Description">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtItem_Description" Enabled="true" MaxLength="250"  EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Item_Short_Desc")%>' Style="font-size: x-small"
                                                                        Width="650px" Height="15px" TextMode="MultiLine"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtItem_Description"
                                                                        ErrorMessage="Special character not allowed" ValidationExpression="^[^'@%#]+$">
                                                                    </asp:RegularExpressionValidator>
                                                                    <asp:TextBox ID="txtItem_Comments" Enabled="true" TextMode="MultiLine" EnableViewState="true" MaxLength="1000"
                                                                        runat="server" Text='<%# Bind("Item_Long_Desc")%>' Style="font-size: x-small"
                                                                        Width="650px" Height="30px"></asp:TextBox>

                                                                </ItemTemplate>
                                                                 
                                                                <HeaderStyle Width="600px"></HeaderStyle>
                                                                <ItemStyle Width="600px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="40px" HeaderText="Packing"
                                                                Visible="true" UniqueName="Unit">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="cmbUnitnPackage" Enabled="true" MaxLength="10" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Unit")%>' Style="font-size: x-small; text-align:center" Width="50px"
                                                                        Height="15px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                 <asp:Label ID="lblTotal" runat="server"   Text="Total" 
                                                                 Style="font-size:medium" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                 <FooterStyle HorizontalAlign="Center" />
                                                                <HeaderStyle Width="60px"></HeaderStyle>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Quantity"
                                                                Visible="true" UniqueName="Request_Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRequest_Qty" Enabled="true" MaxLength="10" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("ORDER_QTY")%>' Style="font-size: x-small; text-align:right" Width="60px"
                                                                        Height="15px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Total_QTY")%>' Style="font-size: x-small" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px"></HeaderStyle>
                                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Price" UniqueName="UnitPrice">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtUnitPrice" Text='<%#Eval("ORDER_PRICE")%>' MaxLength="10" Style="font-size: x-small;text-align:right"
                                                                        Height="15px" runat="server" Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblUnitPrice" runat="server"  Text='<%#Eval("Total_PRICE")%>' Style="font-size: x-small" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px"></HeaderStyle>
                                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Discount(%)" UniqueName="Discount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDiscount" Text='<%#Eval("Item_Discount") %>' MaxLength="5" Style="font-size: x-small; text-align:right"
                                                                        Height="15px" runat="server" Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                 <FooterTemplate>
                                                                    <asp:Label ID="lblDiscount" runat="server" Style="font-size: x-small" Width="60px" Text='<%#Eval("Total_Discount")%>'></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px"></HeaderStyle>
                                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                            </telerik:GridTemplateColumn>
                                                            
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="30px" HeaderText="Action"
                                                                Visible="true" UniqueName="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                        CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                        Height="16px"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="30px"></HeaderStyle>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                        <EditFormSettings>
                                                            <PopUpSettings ScrollBars="None" />
                                                            <PopUpSettings ScrollBars="None" />
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <%-- <asp:ObjectDataSource ID="ObjSrcBgtCode" SelectMethod="SelectBudgetCode" TypeName="SMS.Business.PURC.BLL_PURC_Purchase"
                                runat="server"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSource1" SelectMethod="SelectUnitnPackageDataSet"
                                TypeName="SMS.Business.PURC.BLL_PURC_Purchase" runat="server"></asp:ObjectDataSource>--%>
                                <table align="center" style="height: 0px; margin-top: 5px; background-color: #F7F7F7;
                                    width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100%; height: 30px;" align="center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Height="30px" OnClick="btnSave_Click"
                                                Enabled="true" Width="80px" />&nbsp;&nbsp;
                                            <asp:Button ID="btnSaveClose" runat="server" Text="Save & Close" Height="30px" 
                                                Enabled="true" Width="140px" onclick="btnSaveClose_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" Height="30px"
                                               OnClientClick="refreshAndClose();" />
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
            </div>
        </center>
    </div>
    </form>
</body>
</html>
