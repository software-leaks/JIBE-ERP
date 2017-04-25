<%@ Page Title="Create New Logistic PO" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="LOG_Create_Logistic_PO.aspx.cs" Inherits="Purchase_LOG_Create_Logistic_PO" %>

<%@ Register Src="../UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="ucpager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        function redirecttoindexpage() {

            location.href = 'LOG_Logistics_PO_List.aspx';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

  <div class="page-title">
        Create Logistic PO
    </div>
 <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../Images/loaderbar.gif"alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
  
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829">
        <asp:UpdatePanel ID="updLPOMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlmain" Width="100%" runat="server" BorderStyle="None" DefaultButton="btnSearchPO">
                    <table width="100%" style="padding-bottom: 5px; padding-top: 5px">
                        <tr>
                            <td class="tdh">
                                Fleet :
                            </td>
                            <td class="tdd">
                                <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" Font-Size="11px"
                                    AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="117px">
                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdh">
                                Vessel :
                            </td>
                            <td class="tdd">
                                <asp:UpdatePanel ID="updDDLVessel" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DDLVessel" runat="server" Font-Size="11px" Width="117px" AutoPostBack="true"
                                            OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDLVessel" ControlToValidate="DDLVessel"
                                            ValidationGroup="searchpo" InitialValue="0" runat="server" Display="Dynamic"
                                            ErrorMessage="Please select vessel! "></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="tdh">
                                Supplier :
                            </td>
                            <td class="tdd">
                                <asp:UpdatePanel ID="updsupplier" runat="server">
                                    <ContentTemplate>
                                        <uc1:uc_SupplierList ID="uc_SupplierList1" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="tdh">
                                PO Number :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtPoNumber" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdd">
                                <asp:Button ID="btnClearselection" runat="server" OnClick="btnClearselection_Click"
                                    Width="120px" OnClientClick="javascript:var con=confirm('All selection will be removed . Do you want to continue ?'); if(con) return true; else return false; "
                                    Text="Clear Selections" />
                                <br />
                                <asp:Button ID="btnSearchPO" Text="Search" Height="24px" runat="server" Width="120px"
                                    ValidationGroup="searchpo" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvOrderList" runat="server" AutoGenerateColumns="false" Width="100%"
                                    EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CellSpacing="0" CssClass="gridmain-css"
                                    BackColor="#D8D8D8" CellPadding="4" GridLines="None" OnRowDataBound="gvOrderList_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Dept_Name" HeaderText="Dept" />
                                        <asp:BoundField DataField="REQUISITION_CODE" HeaderText="Reqsn Num." />
                                        <asp:BoundField DataField="Full_NAME" HeaderText="Supplier" />
                                        <asp:BoundField DataField="PO_DATE" HeaderText="PO Issued On" />
                                        <asp:TemplateField HeaderText="Select PO">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Text='<%#Eval("ORDER_CODE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                          <RowStyle CssClass="RowStyle-css" />
                                          <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                </asp:GridView>
                                <ucpager:ucCustomPager ID="ucCustomPagerPO" OnBindDataItem="BindDataItems" AlwaysGetRecordsCount="true"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div id="dvSelectedPo" runat="server" style="width: 99%; text-align: left; color: Navy;
                                    line-height: 25px">
                                </div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnCreateCPO" runat="server" Text="Create Logistic PO" Height="35px"
                                    OnClick="btnCreateCPO_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
