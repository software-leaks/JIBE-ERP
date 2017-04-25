<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_Purc_Ctp_Send_RFQ.ascx.cs"
    Inherits="UserControl_uc_Purc_Ctp_Send_RFQ" %>
<%@ Register Src="ctlPortList.ascx" TagName="ctlPortList" TagPrefix="ucPort" %>
<%@ Register Src="uc_SupplierList.ascx" TagName="uc_SupplierList" TagPrefix="ucSupplier" %>
<link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td style="width: 100%">
            <asp:GridView ID="gvRFQList" runat="server" AutoGenerateColumns="false">
                <HeaderStyle CssClass="HeaderStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle BackColor="#FFFFCC" />
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" />
                <Columns>
                    <asp:TemplateField HeaderText="Supplier">
                        <ItemTemplate>
                            <ucSupplier:uc_SupplierList ID="uc_SupplierListRFQ" OnSelectedIndexChanged="SupplierListRFQ_SelectedIndexChanged"
                                Width="250px" SelectedValue='<%#Eval("Supplier_Code") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Port">
                        <ItemTemplate>
                            <ucPort:ctlPortList ID="ctlPortListRFQ" OnSelectedIndexChanged="ctlPortListRFQ_SelectedIndexChanged"
                                Width="250px" SelectedValue='<%#Eval("Port_ID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Width="300px" Height="30px" Text='<%#Eval("Remark") %>'
                                TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="rbtnRfqType" runat="server">
                                <asp:ListItem Text="Web" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Excel" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandArgument='<%#Eval("Supplier_Code").ToString()+","+Eval("Port_ID").ToString() %>'
                                AlternateText="delete" ImageUrl="~/Images/Close.gif" OnCommand="imgbtnDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSaveSendToSippliers" Text="Send RFQ to selected suppliers" runat="server"
                OnClick="btnSaveSendToSippliers_Click" /><br />
            <asp:Label ID="lblErrormsg" runat="server" Font-Italic="true" ForeColor="Red" Font-Size="11px"></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_Contract_ID" runat="server" />
<asp:HiddenField ID="hdf_Is_Reset_Values" Value="false" runat="server" />
