<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewCTMRequest.aspx.cs"
    Inherits="PortageBill_NewCTMRequest" Title="New CTM Request" %>

<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
                    <div id="page-title" style="margin: 2px; border: 1px solid #cccccc; height: 20px;
                        vertical-align: bottom; background: url(../Images/bg.png) left -10px repeat-x;
                        color: Black; text-align: left; padding: 2px; background-color: #F6CEE3;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 33%;">
                                </td>
                                <td style="width: 33%; text-align: center; font-weight: bold;">
                                    <asp:Label ID="lblPageTitle" runat="server" Text="CASH TO MASTER REQUEST"></asp:Label>
                                </td>
                                <td style="width: 33%; text-align: right;">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px; color: #333">
                        <table style="width: 80%">
                            <tr>
                                <td align="left" style="width: 200px">
                                    Vessel:
                                </td>
                                <td align="left" style="width: 250px">
                                    <asp:DropDownList ID="ddlVessel" runat="server"/>
                                </td>
                                <td align="left" style="width: 100px">
                                    Date:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    CTM Required for the Month of
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCTMMonth" runat="server" />
                                    <asp:DropDownList ID="ddlCTMYear" runat="server" />
                                </td>
                                <td align="left">
                                    Requested By :
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblRequestedBy" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Requested Port
                                </td>
                                <td align="left">
                                    <uc:PortList ID="ctlReqPort" runat="server" />
                                </td>
                                <td align="left">
                                    
                                </td>
                                <td align="left">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        
                        <table style="width: 80%" border="0">
                            <tr>
                                <td align="left" colspan="4">
                                    <b>CTM Calculation </b>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">
                                </td>
                                <td>
                                    <asp:GridView ID="gvCTMCalculations" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="20" Width="100%" EmptyDataText="" CaptionAlign="Bottom" ShowHeader="False"
                                        GridLines="None" ShowFooter="false" OnRowDataBound="gvCTMCalculations_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("CashCategory_Amt","{0:$ ###,##0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <EditRowStyle CssClass="RowStyle-css" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 80%" border="0">
                            <tr>
                                <td style="width: 50px">
                                    <b>Total</b>
                                </td>
                                <td align="left" style="width: 330px">
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblCTMTotal" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    Cash on Board
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblCashonBoard" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    CTM requested
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblCTMReq" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left" style="vertical-align: top">
                                    CTM to be arranged
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:TextBox ID="txtCTM_ToArrange" runat="server" Width="80px" BackColor="Yellow"
                                        BorderStyle="Solid" BorderWidth="1" />
                                </td>
                                <td align="left">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 80%" border="0">
                            <tr>
                                <td align="left" colspan="2">
                                    <b>Detailed BOW of off-signers </b>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:GridView ID="gvCTM_OffSigners" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" AllowPaging="True" PageSize="20" Width="100%" EmptyDataText="No Record Found"
                                        CaptionAlign="Bottom" GridLines="None" ForeColor="#333333">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStaff_Code" runat="server" Text='<%# Eval("Staff_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStaff_FullName" runat="server" Text='<%# Eval("OffSignerName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sign-Off Date" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateOfSignOff" runat="server" Text='<%# Eval("DateOfSignOff","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOW (USD)" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBOWAmt" runat="server" Text='<%# Eval("BOWAmt","{0:$ ###,##0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="White" />
                                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#E3EAEB" />
                                        <EditRowStyle CssClass="RowStyle-css" BackColor="#7C6F57" />
                                        <PagerStyle CssClass="PagerStyle-css" BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>Denomination Required</b>
                                </td>
                                <td align="center" style="width: 128px">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:GridView ID="gvDenominations" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" AllowPaging="True" PageSize="20" Width="100%" EmptyDataText="No Record Found"
                                        CaptionAlign="Bottom" GridLines="None" ForeColor="#333333" OnRowEditing="gvDenominations_RowEditing"
                                        OnRowUpdating="gvDenominations_RowUpdating" OnRowCancelingEdit="gvDenominations_RowCancelEdit">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Denomination" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDenomination" runat="server" Text='<%# Eval("Denomination")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDenomination" runat="server" Text='<%# Bind("Denomination")%>'>
                                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                        <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Notes by Capt" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfNotes_by_Capt" runat="server" Text='<%# Eval("NoOfNotes_by_Capt")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Total (USD)" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaptTotalAmt" runat="server" Text='<%#  GetTotal(Eval("Denomination").ToString(),Eval("NoOfNotes_by_Capt").ToString())  %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Notes by Office" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfNotes_by_Office" runat="server" Text='<%# Eval("NoOfNotes_by_Office")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNoOfNotes_by_Office" runat="server" Text='<%# Bind("NoOfNotes_by_Office")%>'
                                                        Width="80px" />
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Total" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeTotalAmt" runat="server" Text='<%#  GetTotal(Eval("Denomination").ToString(),Eval("NoOfNotes_by_Office").ToString())  %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True" />
                                        </Columns>
                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="White" />
                                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#E3EAEB" />
                                        <EditRowStyle CssClass="RowStyle-css" BackColor="#F5F6CE" />
                                        <PagerStyle CssClass="PagerStyle-css" BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlDenominationApprove" runat="server">
                            <table cellpadding="2" width="80%">
                                <tr>
                                    <td align="left" colspan="6">
                                        <b>Port Details</b>
                                    </td>
                                </tr>
                                <tr style="border: 1px solid #cccccc; background-color: #efefef;">
                                    <td align="center" style="width: 80px">
                                        Date
                                    </td>
                                    <td align="center" style="width: 128px">
                                        <asp:TextBox ID="txtCTM_Supply_Date" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtCTM_Supply_Date"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td align="center" style="width: 80px">
                                        Port
                                    </td>
                                    <td align="left" style="width: 180px">
                                        <uc:PortList ID="ctlCTMPort" runat="server" SelectedValue='<%# Bind("RequiredPort") %>' />
                                    </td>
                                    <td align="left">
                                        
                                    </td>
                                    <td align="center">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="6">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 80%">
                            <tr>
                                <td align="left">
                                    <b>Last CTM Received</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="border: 1px solid #cccccc; background-color: #efefef;">
                                    <asp:GridView ID="gvLastCTM" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" AllowPaging="True" PageSize="20" Width="100%" EmptyDataText="No Record Found"
                                        CaptionAlign="Bottom" GridLines="None" ForeColor="#333333">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DateReceived")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("ApprovedAmt")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Port" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPort" runat="server" Text='<%#  Eval("Port_Name")  %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="White" />
                                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#E3EAEB" />
                                        <EditRowStyle CssClass="RowStyle-css" BackColor="#F5F6CE" />
                                        <PagerStyle CssClass="PagerStyle-css" BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="Div1" style="border: 1px solid #cccccc; margin: 2px; color: #333; padding: 2px;">
                        <asp:Panel ID="pnlManagerApproval" runat="server">
                            <table style="border: 1px solid #cccccc; background-color: #FBFBEF; width: 80%" cellpadding="2">
                                <tr>
                                    <td align="left" colspan="4">
                                        <b>Approval By Manager</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        Date
                                    </td>
                                    <td align="center" style="width: 128px">
                                        Status
                                    </td>
                                    <td align="center">
                                        Amount Approved
                                    </td>
                                    <td align="center">
                                        Remark
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblApprovalDate" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="center" style="width: 128px">
                                        <asp:Label ID="lblApprovalStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblApprovedAmt" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblApprovalRemark" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        
                    </div>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
