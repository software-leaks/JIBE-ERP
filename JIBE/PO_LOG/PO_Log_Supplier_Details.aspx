<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Supplier_Details.aspx.cs"
    Inherits="PO_LOG_PO_Log_Supplier_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                PO History
            </div>
              <asp:UpdatePanel ID="paneltable" runat="server">
                              <contenttemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <asp:Button ID="btnSupplier" runat="server" Text="Supplier" OnClick="btnSupplier_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnAccountType" runat="server" Text="Account Type" OnClick="btnAccountType_Click" />
                    </td>
                </tr>
                <tr>
                <td> 
                <asp:Label ID = "lblMsg" runat="server"  Text="" ></asp:Label>
                </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                        <div style="height: 550px; background-color: White; overflow-y: scroll; max-height: 550px">
                            <asp:GridView ID="gvPODetails" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="REQ_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                AllowSorting="true">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Issued By">
                                        <HeaderTemplate>
                                            Issued By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssued_By" runat="server" Text='<%#Eval("Issued_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="180px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner Name">
                                        <HeaderTemplate>
                                            Owner Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOwner_Name" runat="server" Text='<%#Eval("Owner_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier_Name">
                                        <HeaderTemplate>
                                            Supplier Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier_Name" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="220px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Code">
                                        <HeaderTemplate>
                                            Order Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Date" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="140px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Date">
                                        <HeaderTemplate>
                                            PO Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Date" runat="server" Text='<%#Eval("PO_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Amount">
                                        <HeaderTemplate>
                                            PO Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("PO_Amount","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Count">
                                        <HeaderTemplate>
                                            Invoice Count
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInv_Count" runat="server" Text='<%#Eval("Invoice_Count")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Amount">
                                        <HeaderTemplate>
                                            Invoice Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInv_Amount" runat="server" Text='<%#Eval("Invoice_PO_Currency_Amount","{0:N2}")%>'></asp:Label>&nbsp;&nbsp;
                                            <asp:Label ID="lblPoCurrency" runat="server" Text='<%#Eval("Line_CURRENCY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Amount">
                                        <HeaderTemplate>
                                            Invoice Amount(USD)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInv_Amount" runat="server" Text='<%#Eval("Invoice_USD_Amount","{0:N2}")%>'></asp:Label>&nbsp;&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Amount">
                                        <HeaderTemplate>
                                            Paid Amount(USD)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaid_Amount" runat="server" Text='<%#Eval("Paid_USD_Amount","{0:N2}")%>'></asp:Label>&nbsp;&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Amount">
                                        <HeaderTemplate>
                                            OutStanding(USD)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOutStanding" runat="server" Text='<%#Eval("Outstanding_USD_Amount","{0:N2}")%>'></asp:Label>&nbsp;&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>
                    </td>
                </tr>
            </table>
            </contenttemplate></asp:UpdatePanel>
        </div>
        <div style="display: none;">
            <asp:TextBox ID="txtRemarksID" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtPOCode" runat="server" Width="1px"></asp:TextBox>
        </div>
    </center>
    </form>
</body>
</html>
