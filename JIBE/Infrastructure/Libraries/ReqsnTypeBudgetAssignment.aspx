<%@ Page Title="Budget Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ReqsnTypeBudgetAssignment.aspx.cs" Inherits="Infrastructure_Libraries_ReqsnTypeBudgetAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Assign budget code to reqsn type
    </div>
    <div class="page-content" style="text-align:center">
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>

                <table width="550px">
                    <tr>
                        <td style="font-weight:bold;text-align:right">
                            Reqsn type/Budget code :
                        </td>
                        <td  style="text-align:left">
                            <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSrach" runat="server" Text="Search" OnClick="btnSrach_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-top:10px">
                            <asp:GridView ID="gvReqsnType" runat="server" DataKeyNames="ID,Reqsn_Type_Code,Budget_Code"
                                Width="500px" AutoGenerateColumns="false" CellPadding="5" GridLines="None" CssClass="GridView-css"
                                OnRowCancelingEdit="gvReqsnType_RowCancelingEdit" OnRowDeleting="gvReqsnType_RowDeleting"
                                OnRowEditing="gvReqsnType_RowEditing" OnRowUpdating="gvReqsnType_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Reqsn Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqsnType" Text='<%# Eval("Description") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblReqsnType" Text='<%# Eval("Description") %>' runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budget Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBudgetCode" Text='<%# Eval("Budget_Name") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlBudgetCode" runat="server" Style="font-size: small" Width="295px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowHeader="true" ButtonType="Button" EditText="Edit" UpdateText="Update"
                                        ItemStyle-HorizontalAlign="Center" AccessibleHeaderText="EditRecords" ShowEditButton="true"
                                        ShowCancelButton="true" />
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
