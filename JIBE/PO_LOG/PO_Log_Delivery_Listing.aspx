<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="PO_Log_Delivery_Listing.aspx.cs"
     Title="Delivery Listing" Inherits="PO_LOG_Delivery_Listing" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        function OpenAddScreen(ID, Job_ID) {
            var Supply_ID = document.getElementById("txtPOCode").value;
            var Status = "Add"
            var url = 'PO_Log_Delivery_Item.aspx?Delivery_ID=' + ID + '&Supply_ID=' + Supply_ID + '&Status=' + Status;
            OpenPopupWindowBtnID('Delivery_Item', 'Delivery Item', url, 'popup', 800, 1150, null, null, false, false, true, null, 'ctl00_MainContent_imgfilter');
        }
        function OpenEditScreen(ID, Job_ID) {
            var Supply_ID = document.getElementById("txtPOCode").value;
            var Status = "Edit"
            var url = 'PO_Log_Delivery_Item.aspx?Delivery_ID=' + ID + '&Supply_ID=' + Supply_ID + '&Status=' + Status;
            OpenPopupWindowBtnID('Delivery_Item', 'Delivery Item', url, 'popup', 560, 1170, null, null, false, false, true, null, 'ctl00_MainContent_imgfilter');
        } 5
        function OpenScreen1(ID, Item_ID) {
            var DeliveryID = document.getElementById("txtDeliveryID").value;
            var url = 'PO_Log_Delivery_Item_Entry.aspx?ID=' + ID + '&DeliveryID=' + DeliveryID + '&POItemID=' + Item_ID;
            OpenPopupWindowBtnID('Delivery_Item_Entry', 'Delivery Item Entry', url, 'popup', 450, 750, null, null, false, false, true, null, 'ctl00_MainContent_imgfilter');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <center>
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div id="page-title" class="page-title">
            Delivery / Order confirmation Record
        </div>
        <asp:UpdatePanel ID="upd1" runat="server">
            <contenttemplate>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td align="right">
                    Filter By Status :
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkOpen" runat="server" Text="OPEN" />
                    <asp:CheckBox ID="ChkConfirmed" runat="server" Text="CONFIRMED" />
                    <asp:CheckBox ID="chkDeleted" runat="server" Text="DELETED" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                </td>
                <td align="center" style="width: 5%">
                 <asp:ImageButton ID="imgfilter" runat="server"  ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" 
                        onclick="imgfilter_Click"  />
                </td>
                <td align="center" style="width: 5%">
                   <asp:ImageButton ID="btnRefresh" runat="server"  ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" 
                        onclick="btnRefresh_Click"  />
                 </td>
                 <td align="center" style="width: 5%">
                <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Delivery" OnClientClick='OpenAddScreen(null,null);return false;'
                                            ImageUrl="~/Images/Add-icon.png" />
                                             </td>
                 <td align="center" style="width: 5%">
                    <asp:Button ID="btnExit" Width="100px" runat="server" Visible="false" ForeColor="Red" OnClientClick="refreshAndClose();"
                        Text="Exit" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
        </table>
        <div id="divDelivery" visible="false" runat="server" style="height: 300px; overflow-y: scroll;
            max-height: 300px">
            <table>
                <tr>
                    <td align="left" style="width: 8%; color: red;">
                        Delivery confirmation updated in Office.
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvDelivery" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                DataKeyNames="Delivery_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                CssClass="gridmain-css" AllowSorting="true">
                <HeaderStyle CssClass="HeaderStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                <Columns>
                    <asp:TemplateField HeaderText="Delivery Date">
                        <HeaderTemplate>
                            Delivery Date
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPDeliveryDate" runat="server" Text='<%#Eval("Delivery_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location">
                        <HeaderTemplate>
                            Location
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPLocation" runat="server" Text='<%#Eval("Delivery_Location")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <HeaderTemplate>
                            Remarks
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSRemarks" runat="server" Text='<%#Eval("Delivery_Remarks")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <HeaderTemplate>
                            Status
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Delivery_Status")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Value">
                        <HeaderTemplate>
                           Delivery  Value
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Delivery_Value")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirmed By">
                        <HeaderTemplate>
                            Confirmed By
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblConfirmed" runat="server" Text='<%#Eval("Confirmed_By")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="On">
                        <HeaderTemplate>
                            Confirm On
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProOn" runat="server" Text='<%#Eval("Confirmed_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <HeaderTemplate>
                            Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImgUpdate" runat="server" OnClientClick='<%#"OpenEditScreen(&#39;"+ Eval("Delivery_ID") +"&#39;);return false;"%>'  CommandArgument='<%#Eval("[Delivery_ID]")%>'
                                            ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px">
                                        </asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="ImgDelete_Click" OnClientClick="return confirm('Are you sure want to delete?')"
                                            CommandArgument='<%#Eval("[Delivery_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                            Height="16px"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--   <uc1:uccustompager id="ucCustomPager1" runat="server" pagesize="30" onbinddataitem="BindGrid" />
        <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />--%>
        </div>
        <div id="divVesselDelivery" visible="false" runat="server" style="height: 100px;
            overflow-y: scroll; max-height: 100px">
            <table>
                <tr>
                    <td align="left" style="width: 8%; color: red;">
                        Delivery confirmation updated in Vessel.
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvVesselDelivery" runat="server" EmptyDataText="NO RECORDS FOUND"
                AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
                <HeaderStyle CssClass="HeaderStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <HeaderTemplate>
                            No.
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("ITEM_SERIAL_NO")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item">
                        <HeaderTemplate>
                            Item
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPType" runat="server" Text='<%#Eval("ITEM_SHORT_DESC")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                        <HeaderTemplate>
                            Price
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSName" runat="server" Text='<%#Eval("ORDER_PRICE")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ordered">
                        <HeaderTemplate>
                            Ordered
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("ORDER_QTY")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Recvd">
                        <HeaderTemplate>
                            Recvd
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Delivered_Qty")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Value">
                        <HeaderTemplate>
                            Item Value
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProStatus" runat="server" Text='<%#Eval("Ordered_Item_Value")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--   <uc1:uccustompager id="ucCustomPager1" runat="server" pagesize="30" onbinddataitem="BindGrid" />
        <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />--%>
        </div>
        <div style="display: none;">
            <asp:TextBox ID="txtPOCode" runat="server"></asp:TextBox>
        </div>
        <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
            text-align: left; font-size: 12px; height: 450px; color: Black; width: 80%">
            <div id="Div1" class="page-title">
                Delivery Item
            </div>
         
            <div style="display: none;">
                <asp:TextBox ID="txtPOID" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtDeliveryID" runat="server"></asp:TextBox>
               
            </div>
        </div>
        </contenttemplate>
        </asp:UpdatePanel>
    </div>
</center></form></body></html>