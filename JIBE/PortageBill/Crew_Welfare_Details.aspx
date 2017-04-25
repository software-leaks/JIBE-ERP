<%@ Page Title="Crew Welfare Details" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Crew_Welfare_Details.aspx.cs" Inherits="PortageBill_Crew_Welfare_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/PortageBill.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .tdh
        {
            font-size: 12px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 12px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
        .CreateHtmlTableFromDataTable-table
        {
            background-color: #FFFFFF;
            border: 2px solid #FFB733;
        }
        .CreateHtmlTableFromDataTable-Data
        {
            border: 0px;
            padding: 5px;
        }
        
        .welfare-DataHedaer
        {
            background-color: #F2F2F2;
            border: 1px solid gray;
            text-align: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Crew Welfare Details
    </div>
    <div class="page-content">
        <table width="100%" cellspacing="10" style="border:1px solid gray;margin:1px 0px 1px 0px ">
            <tr>
                <td class="tdh">
                    Vessel :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblVessel" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    From :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    To :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Opening Balance :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblOpnBL" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Closing Balance :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblClosing" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    
        <asp:GridView ID="gvWelfareDetails" runat="server" AutoGenerateColumns="False" Width="100%"
            EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CellSpacing="0"
            CssClass="GridView-css" BackColor="#D8D8D8" CellPadding="5" GridLines="None"
            ShowFooter="true" OnRowCreated="gvWelfareDetails_RowCreated">
            <Columns>
               <asp:TemplateField HeaderText="Paid Date">
                    <ItemTemplate>
                        <asp:Label ID="Label1"  runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Paid_date"))) %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="PORT_NAME" HeaderText="Port" />
                <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="250px">
                    <ItemStyle Width="250px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Voucher_No" HeaderText="Ref Number" />
                <asp:BoundField DataField="Currency_Code" HeaderText="Currency" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Received">
                    <ItemTemplate>
                        <asp:Label ID="lblRcvd" runat="server" Text='<%# Bind("Recp_Amount") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblRcvdTotal" runat="server" Text='<%# Bind("Total_Recp_Amount") %>'></asp:Label>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Paid">
                    <ItemTemplate>
                        <asp:Label ID="lblPaid" runat="server" Text='<%# Bind("Payment_Amount") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblPaidTotal" runat="server" Text='<%# Bind("Total_Payment_Amount") %>'></asp:Label>
                    </FooterTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Attach" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlnkAttachments" runat="server" NavigateUrl="#" Style="cursor: pointer"
                            Visible='<%#Eval("DocCount").ToString()=="0"?false:true %>' ImageUrl="~/Images/Attach.png"
                            Text="PT" onmouseover='<%#"asyncGet_Welfare_Details_Documents(&#39;"+Eval("Welfare_Details_ID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,event,this,1,&#39;Attachments&#39;);" %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
            <HeaderStyle CssClass="HeaderStyle-css" />
            <RowStyle CssClass="RowStyle-css" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <SelectedRowStyle BackColor="#FFFFCC" />
            <FooterStyle HorizontalAlign="Right" CssClass="FooterStyle-css" />
        </asp:GridView>
    </div>
</asp:Content>
