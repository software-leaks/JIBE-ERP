<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Supplier_SimilerName.aspx.cs"
    Inherits="ASL_ASL_Supplier_SimilerName" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Similar Names</title>
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
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
    $(document).ready(function () {
        window.parent.$("#ASL_Evalution").css("height", (parseInt($("#pnlSimilerName").height()) + 50) + "px");
        window.parent.$(".xfCon").css("height", (parseInt($("#pnlSimilerName").height()) + 50) + "px").css("top", "50px");
    });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlSimilerName" runat="server" Visible="true">
         <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
            
                    <div id="Div1" class="page-title">
                        Supplier Similar Names
                    </div>
               
            <table width="100%" cellpadding="2" cellspacing="0">
            <tr>
                <td align="left">
                    Other Suppliers with Similar Names.
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" MaxLength="200" Width="400px" CssClass="txtInput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnfilter" Text="Search" runat="server" OnClick="btnfilter_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
          <%--  <tr>
                <td align="left">
                    SUPPLIER -
                    <asp:Label ID="lblSuppliername" runat="server" Width="400px" CssClass="txtInput"
                        Text=""></asp:Label>
                </td>
                <td align="left">
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <div style="height: 550px; overflow-y: scroll; max-height: 550px">
                        <asp:GridView ID="gvSupplierName" runat="server" EmptyDataText="NO RECORDS FOUND"
                            AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                            Width="100%" GridLines="both" AllowSorting="true" OnRowDataBound="gvSupplierName_RowDataBound">
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                            <Columns>
                                <asp:TemplateField HeaderText="Type">
                                    <HeaderTemplate>
                                      Supplier  Type
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryDate" runat="server" Text='<%#Eval("Supp_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier name">
                                    <HeaderTemplate>
                                        Supplier name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Register_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Match %">
                                    <HeaderTemplate>
                                        Match %
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatch" runat="server" Text='<%#Eval("Percentage")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ASL Status">
                                    <HeaderTemplate>
                                        ASL Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblASL_Status" runat="server" Text='<%#Eval("Supp_Status")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="SearchBindGrid" />
                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                    </div>
                </td>
            </tr>
        </table>
        </div>
        </asp:Panel>
    </div>
    </form>
    </td></tr></table>
</body>
</html>
