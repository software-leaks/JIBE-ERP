<%@ Page Title="View non contract items" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CTP_View_Non_Contract_Items.aspx.cs" Inherits="Purchase_CTP_View_Non_Contract_Items" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/uc_Purc_Ctp_Send_RFQ.ascx" TagName="uc_Purc_Ctp_Send_RFQ"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            color: Black;
        }
        
        
        .tdh
        {
            text-align: right;
            padding: 3px 2px 3px 0px;
            font-size: 12px;
        }
        .tdd
        {
            text-align: left;
            padding: 3px 2px 3px 0px;
            font-size: 11px;
            border-right: 1px solid gray;
        }
    </style>
    <script type="text/javascript">
        function SelectAll(objid) {

            var checked = document.getElementById(objid.id).checked;

            var griditems = document.getElementById('<%=gvNonContractItems.ClientID %>');

            for (var j = 2; j <= griditems.rows.length; j++) {

                var chk_id = "";


                if (j < 10) {

                    chk_id = "ctl00_MainContent_gvNonContractItems_ctl0" + j.toString() + "_chkselect";


                }
                else {

                    chk_id = "ctl00_MainContent_gvNonContractItems_ctl" + j.toString() + "_chkselect";

                }

                document.getElementById(chk_id).checked = checked;

            }

        }

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
      <div class="page-title">
           View Non Contracts Items    
    </div>
    <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <div id="page-title">
        Non Contract Items
    </div>
    <div id="page-content">
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="70%">
                    <tr>
                        <td class="tdh">
                            Department Type :
                        </td>
                        <td class="tdd">
                            <asp:RadioButtonList ID="optList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                OnSelectedIndexChanged="optList_SelectedIndexChanged" ForeColor="Black" TabIndex="2">
                            </asp:RadioButtonList>
                        </td>
                        <td colspan="2" style="text-align: left; border-right: 1px solid gray;">
                            Requisition Date :
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" OnClick="btnClearFilter_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Department :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="cmbDept" runat="server" AutoPostBack="true" Font-Size="12px"
                                Width="180px" OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged" AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">--SELECT --</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdh">
                            From :
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="txtReqsnDTFrom" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtendertxtReqsnDTFrom" TargetControlID="txtReqsnDTFrom"
                                Format="dd/MM/yyyy" runat="server">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="text-align: center" rowspan="2">
                            <asp:Button ID="btnSearch" runat="server"  Text="Search"
                                OnClick="btnSearch_Click" ValidationGroup="vlgSearch" Width="67%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Catalogue :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlCatalogue" runat="server" AutoPostBack="true" Font-Size="12px"
                                Width="180px" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCatalogue_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCatalogue" runat="server"
                                InitialValue="0" ErrorMessage="Please select Catalogue !" Display="Dynamic" ControlToValidate="ddlCatalogue"
                                ValidationGroup="vlgSearch"></asp:RequiredFieldValidator>
                        </td>
                        <td class="tdh">
                            To :
                        </td>
                        <td class="tdd" rowspan="2">
                            <asp:TextBox ID="txtReqsnDTTo" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtendertxtReqsnDTTo" TargetControlID="txtReqsnDTTo"
                                Format="dd/MM/yyyy" runat="server">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Sub Catalogue :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlSubCatalogue" Width="180px" Font-Size="12px" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div style="border-bottom: 1px solid gray; height: 1px; width: 100%">
                    &nbsp;</div>
                <asp:MultiView ID="mlvCTP" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viewNewContract" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvNonContractItems" runat="server" AutoGenerateColumns="false" CssClass="gridmain-css" CellPadding="4"
                                        DataKeyNames="ID" Width="100%" EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon"
                                        CellSpacing="0" BackColor="#D8D8D8" GridLines="None" OnDataBound="gvNonContractItems_DataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Part_Number" HeaderText="Part No." />
                                            <asp:BoundField DataField="Drawing_Number" HeaderText="Drawing No." />
                                            <asp:BoundField DataField="Short_Description" HeaderText="Short Desc." />
                                            <asp:BoundField DataField="Long_Description" HeaderText="Long Desc." />
                                            <asp:BoundField DataField="Unit_and_Packings" HeaderText="Unit" />
                                            <asp:BoundField DataField="AvgReqQty" HeaderText="Avg Ord Qty" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkselectAll" runat="server" onclick="SelectAll(this)" Text="Select All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px"/>
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPager1" runat="server" OnBindDataItem="BindItems"
                                        AlwaysGetRecordsCount="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding: 5px 5px 5px 0px">
                                    <span style="color: Blue; font-weight: bold; font-size: 12px">Add selected item to:</span>
                                    &nbsp; &nbsp;
                                    <asp:Button ID="btnAddItemToExistingContract" runat="server" Text="Existing Contract"
                                        OnClientClick="javascript:var con=confirm('after clicking on this,you will not be able to change the item selection ! Are you sure to move to next step ?'); if(con)return true; else return false;"
                                        OnClick="btnAddItemToExistingContract_Click" />
                                    <asp:Button ID="btnAddToNewContract" runat="server" Text="New Contract" OnClientClick="javascript:var con=confirm('after clicking on this,you will not be able to change the item selection ! Are you sure to move to next step ?'); if(con)return true; else return false;"
                                        OnClick="btnAddToNewContract_Click" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblmsg" runat="server" Font-Italic="true" Width="300px" BackColor="Yellow"
                                        ForeColor="Red" Font-Bold="true" Font-Size="12px" Font-Names="verdana"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="viewSuppliers" runat="server">
                        <table>
                            <tr>
                                <td style="width: 100%">
                                    <uc2:uc_Purc_Ctp_Send_RFQ ID="uc_Purc_Ctp_Send_RFQSupp" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="ViewAddToExisting" runat="server">
                        <table cellpadding="1" cellspacing="0" width="70%">
                            <tr>
                                <td class="tdh">
                                    Contract Code :
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtContractCode" runat="server"></asp:TextBox>
                                </td>
                                <td class="tdh">
                                    Supplier :
                                </td>
                                <td class="tdd">
                                    <uc3:uc_SupplierList ID="uc_SupplierListCtp" runat="server" />
                                </td>
                                <td class="tdh">
                                    Port :
                                </td>
                                <td class="tdd">
                                    <uc4:ctlPortList ID="ctlPortListCtp" runat="server" />
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnSearchContract" runat="server" Text="Search Contract" OnClick="btnSearchContract_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <div style="min-height: 200px; overflow-y: scroll;">
                                        <asp:GridView ID="gvContractList_qtn" runat="server" GridLines="None" EmptyDataText="No record found !" CssClass="gridmain-css"
                                            SelectedRowStyle-BackColor="LightGoldenrodYellow" RowStyle-HorizontalAlign="Left"
                                            BackColor="#D8D8D8" CellSpacing="0" AutoGenerateColumns="false" CellPadding="5"
                                            OnSelectedIndexChanged="gvContractList_qtn_SelectedIndexChanged" DataKeyNames="Quotation_ID"
                                            Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Contract Code" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnContractID" CommandName="Select" Text='<%#Eval("QTN_Contract_Code")%>'
                                                            runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Quotation_Status" HeaderText="Status" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Full_NAME" HeaderText="Supplier" ItemStyle-Wrap="true"
                                                    ItemStyle-Width="200px" />
                                                <asp:BoundField DataField="PORT_NAME" HeaderText="Port" ItemStyle-Width="100px" />
                                            </Columns>
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="30px"/>
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" align="right" style="padding-top: 4px">
                                    <asp:Button ID="btnSaveToExistingCtp" runat="server" Text="Save and add to selected Contract"
                                        OnClick="btnSaveToExistingCtp_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
