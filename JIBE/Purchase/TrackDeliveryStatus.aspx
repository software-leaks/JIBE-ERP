<%@ Page Title="Delivery Status" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TrackDeliveryStatus.aspx.cs" Inherits="Purchase_TrackDeliveryStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="width: 100%; background-color: #585858; font-family: Tahoma; font-size: 12px;
        font-weight: bold; color: White; text-align: center; padding: 2px 0px 2px 0px;
        margin: 0px 0px 0px 0px">
        Delivery Status
    </div>
    <div>
        <asp:GridView ID="gvStatus" AutoGenerateColumns="False" GridLines="None" Width="100%"
            BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" CellPadding="2" CellSpacing="0"
            runat="server">
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <RowStyle CssClass="RowStyle-css" />
            <HeaderStyle CssClass="HeaderStyle-css" />
            <Columns>
                <asp:TemplateField HeaderText="Sent From">
                    <ItemTemplate>
                        <asp:Label ID="lblSentFrom" runat="server" Text='<%# Bind("SentFrom") %>'></asp:Label>
                        <asp:Label ID="lblSentFromDTL" Visible="false" runat="server" Text='<%# Bind("SentFrom_UserType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Sent Date" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    DataField="Sent_Date" />
                <asp:TemplateField HeaderText="Sent To">
                    <ItemTemplate>
                        <asp:Label ID="lblSentTO" runat="server" Text='<%# Bind("SentTo") %>'></asp:Label>
                        <asp:Label ID="lblSentTODTL" Visible="false" runat="server" Text='<%# Bind("SentFrom_UserType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Recvd By">
                    <ItemTemplate>
                        <asp:Label ID="lblRecvdBy" runat="server" Text='<%# Bind("RecvdBy") %>'></asp:Label>
                        <asp:Label ID="lblRecvdByDTL" Visible="false" runat="server" Text='<%# Bind("SentFrom_UserType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Recvd Date" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    DataField="Recvd_Date" />
                <asp:BoundField HeaderText="Planned Send Date" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    DataField="Planned_SendDate" />
                <asp:BoundField HeaderText="Delivery Date At Destin." ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    DataField="Delivery_DateAtDestin" />
                <asp:BoundField HeaderText="No of Packet" ItemStyle-HorizontalAlign="Center" DataField="Packet_Count" />
                <asp:TemplateField HeaderText="Packet Details">
                    <ItemTemplate>
                        <asp:Label ID="lblPktDtl" runat="server" Text='<%# Bind("Packet_Details") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Est Dev OnBoard Port" DataField="EstDevOnBoar_Port" />
                <asp:BoundField HeaderText="Recvd AT" DataField="Recvd_AT" />
                <asp:TemplateField HeaderText="Remark">
                    <ItemTemplate>
                        <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("ShortRemark") %>'></asp:Label>
                        <asp:Label ID="lblremarkLong" Visible="false" runat="server" Text='<%# Bind("remark") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Delivery Type" ItemStyle-HorizontalAlign="Center" DataField="DeliveryType" />
                <asp:BoundField HeaderText="Est.Dev OnBoard Date" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    DataField="EstDevOnBoardDate" />
                <asp:BoundField HeaderText="Updated By" DataField="createdb" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
