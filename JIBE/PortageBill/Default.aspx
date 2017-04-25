<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Title="Portage Bill Index"
    CodeFile="Default.aspx.cs" Inherits="PortageBill_Default" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/PortageBill.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .HeaderStyle-css th
        {
            border: 1px solid #959EAF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
     <div class="page-title">
        Portage Bill
     </div>
    <div id="page-content" style="border: 1px solid #cccccc;">
        <table style="border: 1px solid #cccccc;" cellpadding="6">
            <tr>
                <td align="right">
                    Fleet
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Year
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" Style="width: 200px"
                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Vessel
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Month
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMonth" runat="server" Style="width: 200px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="70%">
            <tr>
                <td>
                    <asp:GridView ID="GridViewPB" runat="server" Width="100%" ShowFooter="false" AutoGenerateColumns="False"
                        CssClass="gridmain-css"  CellPadding="5"
                        GridLines="None" ForeColor="#333333" DataKeyNames="Vessel_ID" OnRowCreated="GridViewPB_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel" SortExpression="Vessel_Short_name">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Portage Bill Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPBDate" runat="server" Text='<%#Eval("PBill_Date","{0:MMM - yyyy}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Portage Bill" ShowHeader="False" HeaderStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplView" runat="server" Target="_blank" ImageUrl="~/Images/portagebill-money-icon.png"
                                        NavigateUrl='<%# "~/PortageBill/PortageBill.aspx?ARG=" +Eval("Vessel_ID").ToString() + "~" + Eval("PBill_Date").ToString() +"~" + Eval("Vessel_Name")%>'
                                        Text="Portage Bill"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Capt. Cash" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplCaptCash" runat="server" Target="_blank" ImageUrl="~/Images/Capt-money-icon.png"
                                        NavigateUrl='<%# "~/PortageBill/CaptCashIndex.aspx?ARG=" +Eval("Vessel_ID").ToString() + "~" + Eval("PBill_Date").ToString() %>'
                                        Text="Master's Cash"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crew Welfare" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplCrewWelfare" runat="server" Target="_blank" ImageUrl="~/Images/Capt-money-icon.png"
                                        NavigateUrl='<%# "~/PortageBill/Crew_Welfare_Details.aspx?Vessel_ID=" +Eval("Vessel_ID").ToString() + "&PB_Date=" + Eval("PBill_Date").ToString() %>'
                                        Text="Crew Welfare"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wire Transfer" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplAllotment" runat="server" Target="_blank" ImageUrl="~/Images/allotment-money-icon.png"
                                        NavigateUrl='<%# "~/PortageBill/SalaryTransfer.aspx?ARG=" +Eval("Vessel_ID").ToString() + "~" + Eval("PBill_Date").ToString()  %>'
                                        Text="Wire Transfer"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Portage Bill" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkPB" runat="server" NavigateUrl="#" Style="cursor: pointer"
                                        Visible='<%# Eval("PBDOC_Name").ToString().Trim()!="0"?true:false %>' ImageUrl="~/Images/portagebill-money-icon.png"
                                        Text="PT" onclick='<%#"asyncGet_Portage_Bill_Attachments(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Month").ToString()+"&#39;,&#39;"+Eval("Year").ToString()+"&#39;,&#39;59&#39;,event,this,1,&#39;Attachments&#39;);" %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Capt's Cash" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkCaptCash" runat="server" NavigateUrl="#" Style="cursor: pointer"
                                        ImageUrl="~/Images/Capt-money-icon.png" Text='<%#Eval("MCashDOC_Name") %>' Visible='<%# Eval("MCashDOC_Name").ToString().Trim()!="0"?true:false %>'
                                        onclick='<%#"asyncGet_Portage_Bill_Attachments(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Month").ToString()+"&#39;,&#39;"+Eval("Year").ToString()+"&#39;,&#39;60&#39;,event,this,1,&#39;Attachments&#39;);" %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Welfare" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkWelafre" runat="server" NavigateUrl="#" ImageUrl="~/Images/Capt-money-icon.png"
                                        Text='<%#Eval("WelfareDoc_Name") %>' Style="cursor: pointer" Visible='<%# Eval("WelfareDoc_Name").ToString().Trim()!="0"?true:false %>'
                                        onclick='<%#"asyncGet_Portage_Bill_Attachments(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Month").ToString()+"&#39;,&#39;"+Eval("Year").ToString()+"&#39;,&#39;63&#39;,event,this,1,&#39;Attachments&#39;);" %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Allotments" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkAllotments" runat="server" NavigateUrl="#" ImageUrl="~/Images/allotment-money-icon.png"
                                        Text='<%#Eval("AllotDOC_Name") %>' Style="cursor: pointer" Visible='<%# Eval("AllotDOC_Name").ToString().Trim()!="0"?true:false %>'
                                        onclick='<%#"asyncGet_Portage_Bill_Attachments(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Month").ToString()+"&#39;,&#39;"+Eval("Year").ToString()+"&#39;,&#39;61&#39;,event,this,1,&#39;Attachments&#39;);" %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BOW" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkBOW" runat="server" NavigateUrl='<%#"BOW_Document.aspx?Vessel_ID="+Eval("Vessel_ID").ToString() + "&PBill_Date=" + Eval("PBill_Date").ToString()  %>'
                                        ImageUrl="~/Images/bow-money-icon.png" Text="BOW" Target="_blank" Visible='<%# Eval("BowCount").ToString()!="0"?true:false %>'
                                        onmouseover='<%#"asyncGet_Portage_Bill_Attachments(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Month").ToString()+"&#39;,&#39;"+Eval("Year").ToString()+"&#39;,&#39;62&#39;,event,this,1,&#39;Attachments&#39;);" %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <FooterStyle CssClass="FooterStyle-css" Font-Bold="True" />
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPagerPB" OnBindDataItem="Load_PB_Received" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
