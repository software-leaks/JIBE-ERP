<%@ Page Title="Mandatory Documents" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MandatoryDocList.aspx.cs" Inherits="Crew_Libraries_MandatoryDocList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

<div id="pageTitle" style="background-color: gray; color: White; font-size: 12px;
        text-align: center; padding: 2px; font-weight: bold;">
        <asp:Label ID="lblPageTitle" runat="server" Text="Mandatory Document List"></asp:Label>
    </div>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="DeleteDocType"
        UpdateMethod="EditDocType" TypeName="SMS.Business.DMS.BLL_DMS_Admin" SelectMethod="Get_DocTypeList">
        <UpdateParameters>
            <asp:Parameter Name="DocTypeID" Type="Int32" />
            <asp:Parameter Name="DocTypeName" Type="String" />
            <asp:Parameter Name="Legend" Type="String" />
            <asp:Parameter Name="Deck" Type="String" />
            <asp:Parameter Name="Engine" Type="String" />
            <asp:Parameter Name="AlertDays" Type="Int32" />
            <asp:Parameter Name="isDocCheckList" Type="Int32" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="DocTypeID" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridViewDocType" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
        DataKeyNames="DocTypeID" EmptyDataText="No Record Found" CaptionAlign="Bottom"
        PageSize="20" CellPadding="2" ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Type Name" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                NAME:
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblDocTypeName" runat="server" Text='<%#Eval("DocTypeName")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                LEGEND:
                            </td>
                            <td style="color: Blue">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Legend")%>'></asp:Label>
                            </td>
                            <td>
                                Regn STCW’95 :
                            </td>
                            <td style="color: Blue">
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Deck")%>'></asp:Label>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Engine")%>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <EditItemTemplate>
                    <table>
                        <tr>
                            <td>
                                NAME:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocTypeName" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                    Text='<%#Bind("DocTypeName")%>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                LEGEND:
                            </td>
                            <td>
                                <asp:TextBox ID="txtLegend" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                    Text='<%#Bind("Legend")%>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Regn STCW’95/Deck:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDeck" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                    Text='<%#Bind("Deck")%>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Regn STCW’95/Engine:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEngine" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                    Text='<%#Bind("Engine")%>'></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Alert" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblAlertDays" runat="server" Text='<%#Eval("AlertDays")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAlertDays" Font-Size="11px" Width="30px" MaxLength="50" runat="server"
                        Text='<%#Bind("AlertDays")%>'></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:CheckBoxField DataField="isDocCheckList" HeaderText="CheckList" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left" ShowHeader="False">
                <EditItemTemplate>
                    <asp:ImageButton ID="btnAccept" runat="server" AlternateText="Update" CausesValidation="False"
                        CommandName="Update" ImageUrl="~/images/accept.png" />
                    <asp:ImageButton ID="btnReject" runat="server" AlternateText="Cancel" CausesValidation="False"
                        CommandName="Cancel" ImageUrl="~/images/reject.png" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                        CommandName="Edit" ImageUrl="~/images/edit.gif" />
                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                        CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:CommandField HeaderText="Attributes" ShowHeader="True" ShowSelectButton="True"
                SelectText="Attributes" />
        </Columns>
        <EditRowStyle BackColor="#efefef" />
        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Content>
