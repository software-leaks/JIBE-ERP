<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PURC_Bulk_Purchase_Reqsn.aspx.cs"
    Inherits="Purchase_PURC_Bulk_Purchase_Reqsn" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderStyle-css-bulkpurc
        {
            background: url(../Images/gridheaderbg-image.png) left -3px repeat-x;
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            height: 35px;
            border: 1px solid #cccccc;
        }
        .HeaderStyle-css-bulkpurc th
        {
            border: 1px solid #cccccc;
        }
        
        .RowStyle-css td
        {
            border: 1px solid #cccccc;
        }
        .AlternatingRowStyle-css td
        {
            border: 1px solid #cccccc;
        }
        .gridmain-css
        {
            border-collapse: collapse;
        }
        
        body 
        {
            background-color: White;
        } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="1" cellspacing="0" width="100%" style="background-color: White;
        vertical-align: top">
        <tr>
            <td style="vertical-align: top">
                <uc2:ucCustomDropDownList ID="ucCustomDropDownListstatus" OnApplySearch="BindData"
                    runat="server" />
                <asp:GridView ID="gvBulkPurchase" runat="server" AutoGenerateColumns="false" CellPadding="8"
                    EmptyDataRowStyle-BackColor="Wheat" EmptyDataText="No record found !" GridLines="None"
                    CssClass="gridmain-css" ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel" />
                        <asp:BoundField DataField="Dept_Name" HeaderText="Department" />
                        <asp:TemplateField HeaderText="Reqsn Number">
                            <ItemTemplate>
                                <a href="RequisitionSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code=0&hold=0"%>"
                                    target="_blank">
                                    <%# DataBinder.Eval(Container.DataItem, "REQUISITION_CODE")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Code">
                            <ItemTemplate>
                                <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                    target="_blank">
                                    <%# Eval("ORDER_CODE")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Split" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="200px" border="0">
                                    <tr>
                                        <td align="center" style="border: 0px solid white;width:120px">
                                            <asp:HyperLink ID="hplinkBulkReport" runat="server" Target="_blank" Text="View split history"
                                                NavigateUrl='<%#"PURC_Split_Reqsn_BulkPurchase_Report.aspx?ReqsnCode="+Eval("REQUISITION_CODE").ToString()+"&Document_Code="+Eval("document_code") %>'></asp:HyperLink>
                                        </td>
                                        <td align="center" style="border: 0px solid white">
                                            <asp:HyperLink ID="hlnkbulkAllotment" runat="server" Visible='<%#Convert.ToInt32(Eval("TOTAL_SPLITED_QTY"))==0?false:true %>'
                                                Target="_blank" Text="Split" NavigateUrl='<%#"PURC_Split_Reqsn_BulkPurchase.aspx?ReqsnCode="+Eval("REQUISITION_CODE").ToString()+"&Document_Code="+Eval("document_code") %>'></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Status
                                <asp:Image ID="imgStatusFilter" AlternateText="Filter" ImageUrl="../Images/filter-grid.png"
                                    Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucCustomDropDownListstatus')"
                                    Style="cursor: pointer" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%# Convert.ToInt32(Eval("TOTAL_SPLITED_QTY"))==0?"Closed":"Draft" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Roll back at draft" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnRollbackAtDrfat" ImageUrl="../images/Cancel_New.gif" runat="server"
                                    BackColor="Transparent" Visible='<%#Eval("FINALIZED").ToString()!="0" && objUA.Edit!=0 ?true:false %>'
                                    CommandArgument='<%#Eval("REQUISITION_CODE") %>' OnClick="btnRollbackAtDrfat_Click"
                                    OnClientClick="javascript:var sts=confirm('Are you sure to roll bak ?');if(sts)return true; else return false;"
                                    Text="Roll Back at Draft" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="11px" Font-Names="verdana" />
                    <RowStyle CssClass="RowStyle-css" Font-Size="11px" Font-Names="verdana" />
                    <HeaderStyle CssClass="HeaderStyle-css-bulkpurc" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:ucCustomPager ID="ucCustomPagerItems" OnBindDataItem="BindData" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
