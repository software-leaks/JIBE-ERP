<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_LOG_View_Approval_Limit.aspx.cs" Inherits="PO_LOG_PO_LOG_View_Approval_Limit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
               Approval Limit
            </div>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 25%">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Authorized Invoice uploaders  :"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblverifier" ForeColor="Blue" runat="server" Text=""></asp:Label><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: right;">
                                    Approval Group Name :
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblGroup" runat="server" Text=""></asp:Label><br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                    <div style="height: 250px; background-color: White; overflow-y: scroll; max-height: 250px">
                                        <asp:GridView ID="gvApprovalLimit" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                            Width="100%" GridLines="both" AllowSorting="true">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                            <Columns>
                                            <asp:TemplateField HeaderText="From">
                                                    <HeaderTemplate>
                                                       Sr No.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Amount">
                                                    <HeaderTemplate>
                                                        From Amount
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFrom_Amt" runat="server" Text='<%#Eval("Min_Amt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Amount">
                                                    <HeaderTemplate>
                                                        To Amount
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTo_Amt" runat="server" Text='<%#Eval("MAX_APPROVAL_LIMIT")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO Approvers">
                                                    <HeaderTemplate>
                                                        PO Approvers
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%#Eval("POApprover")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Approver">
                                                    <HeaderTemplate>
                                                        Invoice Approver
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%#Eval("InvoiceApprover")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Advance Approver">
                                                    <HeaderTemplate>
                                                        Advance Approver
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%#Eval("AdvanceApprover")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <uc1:ucCustomPager ID="ucCustomPager2" runat="server" OnBindDataItem="BindApprovalLimitGrid" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                    <br />
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="All amounts defined are in USD."></asp:Label>
                                </td>
                            </tr>
            </table>
        </div>
        <div style="display:none;">
          <asp:TextBox ID="txtSupplyID" runat="server"  Width="1px"></asp:TextBox>
        </div>
    </center>
    </form>
</body>
</html>
