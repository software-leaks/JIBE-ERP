<%@ Page Title="Master's Cash" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CaptRpt.aspx.cs" Inherits="PortageBill_CaptRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/PortageBill.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 0px; background-color: #F6CEE3; text-align: center; font-weight: bold">
        Master's Cash Report
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829">
        <table width="100%" cellspacing="10">
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
        <asp:GridView ID="gvCastCash" runat="server" AutoGenerateColumns="False" Width="100%"
            EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CellSpacing="0"
            CssClass="GridView-css" BackColor="#D8D8D8" CellPadding="5" GridLines="None"
            ShowFooter="true" OnRowCreated="gvCastCash_RowCreated">
            <Columns>
                <asp:TemplateField HeaderText="Paid Date">
                    <ItemTemplate>
                        <asp:Label  runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Paid_date"))) %>'></asp:Label>
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
                            Text="PT" onmouseover='<%#"Get_CaptCash_Items_Attachments(&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,event,this,1,&#39;Attachments&#39;);" %>'
                            onclick='<%#"Get_CaptCash_Items_Attachments(&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,event,this,1,&#39;Attachments&#39;);" %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                            Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;ACC_DTL_CAPTCASH_DETAILS&#39;,&#39; id="+Eval("id")+" and Vessel_id="+Eval("Vessel_id")+"&#39;,event,this)" %>'
                            AlternateText="info" />
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
