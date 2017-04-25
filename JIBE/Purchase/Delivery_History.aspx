<%@ Page Title="Delivery History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Delivery_History.aspx.cs" Inherits="Purchase_Delivery_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
    <style type="text/css" >
       #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding:2px 0px 2px 0px;
            font-weight: bold;
            width:100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="Delivery History"></asp:Label>
    </div>
    <div id="page-content" style="color: #333333;  z-index: -2;
        overflow: auto;width:100%">
        <center>
        <br />
            <asp:GridView ID="gvDlHistory" runat="server" AutoGenerateColumns="false" CellPadding="3">
                <HeaderStyle CssClass="HeaderStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <Columns>
                    <asp:BoundField HeaderText="Vessel Name" DataField="Vessel_Name" />
                    <asp:TemplateField HeaderText="Requisition Code">
                        <ItemTemplate>
                            <a href="RequisitionSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")%>"
                                target="_blank">
                                <%#Eval("REQUISITION_CODE")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Requisition Date" DataField="DOCUMENT_DATE" />
                    <asp:TemplateField HeaderText="Order Code">
                        <ItemTemplate>
                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                target="_blank">
                                <%# Eval("ORDER_CODE")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delivery Code">
                        <ItemTemplate>
                            <a href='DeliveryOrderSummary.aspx?REQUISITION_CODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&document_code="+ Eval("Document_Code") +"&DELIVERY_CODE="+ Eval("DELIVERY_CODE")%> '
                                target="_blank">
                                <%# Eval("DELIVERY_CODE")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Delivery Date" DataField="DELIVERY_DATE" />
                    <asp:BoundField HeaderText="Delivery Port" DataField="PORT_NAME" />
                </Columns>
            </asp:GridView>
        </center>
    </div>
</asp:Content>
