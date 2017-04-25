<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReportPerManningAgent.aspx.cs"
    EnableEventValidation="false" Title="Report-per Manning Agent" Inherits="PortageBill_ReportPerManningAgent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        #page-content a:link
        {
            color: blue;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: black;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: blue;
            text-decoration: none;
        }
        
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../Images/bg.png) left -1672px repeat-x;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .taskpane
        {
            background-image: url(../images/taskpane.png);
            background-repeat: no-repeat;
            background-position: -2px -2px;
        }
        .tooltip
        {
            display: none;
            background: transparent url(../Images/black_arrow.png);
            font-size: 12px;
            height: 90px;
            width: 180px;
            padding: 15px;
            color: #fff;
        }
        .interview-schedule-table
        {
            padding: 0;
            border-collapse: collapse;
        }
        .interview-schedule-table div
        {
            border: 0px solid gray;
            height: 16px;
            width: 16px;
            margin-top: 2px;
            background: url(../Images/Interview_1.png) no-repeat;
        }
        
        .CrewStatus_Current
        {
            background-color: #aabbdd;
        }
        .CrewStatus_SigningOff
        {
            background-color: #F3F781;
        }
        .CrewStatus_SignedOff
        {
            background-color: #F5A9A9;
        }
        .CrewStatus_Assigned
        {
            background-color: #BBB6FF;
        }
        .CrewStatus_Planned
        {
            background-color: #F781F3;
        }
        .CrewStatus_Pending
        {
            background-color: #81BEF7;
        }
        .CrewStatus_Inactive
        {
            background-color: #848484;
            color: #E6E6E6;
        }
        .CrewStatus_NoVoyage
        {
            background-color: #A9F5D0;
        }
        .CrewStatus_NTBR
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_NTBR_Row
        {
            color: Red;
        }
        .CrewStatus_UNFIT
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_UNFIT_Row
        {
            color: Red;
        }
        .imgCOC
        {
            vertical-align: middle;
        }
        .CrewStatus_Rejected
        {
            background-color: RED;
            color: Yellow;
        }
        input
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            height: 21px;
            font-family: Tahoma;
            font-size: 11px;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: arial;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;
            border-right: 2px solid #3B0B0B;
            border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E0F8E0;
            border: 1px solid #F1C15F;
        }
        p
        {
            margin-top: 20px;
        }
        h1
        {
            font-size: 13px;
        }
        .dogvdvhdr
        {
            width: 300;
            background: #C4D5E3;
            border: 1px solid #C4D5E3;
            font-weight: bold;
            padding: 10px;
        }
        .dogvdvbdy
        {
            width: 300;
            background: #FFFFFF;
            border-left: 1px solid #C4D5E3;
            border-right: 1px solid #C4D5E3;
            border-bottom: 1px solid #C4D5E3;
            padding: 10px;
        }
        .pgdiv
        {
            width: 320;
            height: 250;
            background: #E9EFF4;
            border: 1px solid #C4D5E3;
            padding: 10px;
            margin-bottom: 20;
            font-family: arial;
            font-size: 12px;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 2px 3px 2px 0px;
            vertical-align: middle;
            font-weight: bold;
            width: 100px;
            border: 1px solid #DADADA;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 2px 2px 2px 3px;
            vertical-align: middle;
            border: 1px solid #DADADA;
        }
        
        .CreateHtmlTableFromDataTable-Data-Attachment
        {
            border: 0;
        }
        .CreateHtmlTableFromDataTable-Data-Attachment td
        {
            border: 0;
        }
    </style>
    <style type="text/css">
        .ajax__fileupload_button
        {
            background-color: green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Report - Per Manning Agent
    </div>
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
                    <table width="100%" border="0">
                        <tr>
                            <td align="right">
                                Fleet
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Year
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlYear" runat="server" Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Manning Agent
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlManningAgent" runat="server" Width="156px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlManningAgent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Bank Name
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLBank" runat="server" Style="width: 120px">
                                    <%-- <asp:ListItem Value="0">-SELECT ALL-</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Vessel
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVessel" runat="server" Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Month
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlMonth" runat="server" Style="width: 200px">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                    <asp:ListItem Value="9">Sept</asp:ListItem>
                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Nationality
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" Width="156px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" colspan="2" style="padding-top: 5px; vertical-align: top;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />&nbsp;
                                <asp:Button ID="btnClear" runat="server" Text="Clear Filter" OnClick="btnClear_Click" />&nbsp;
                                <asp:Button ID="btnExpToExl" runat="server" Text="Export To Excel" OnClick="btnExpToExl_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExpToExl" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px; overflow: auto">
                    <asp:GridView ID="gvAllotments" DataKeyNames="id,Vessel_ID,AllotmentID" runat="server"
                        ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" CellPadding="4" AllowPaging="True"
                        PageSize="30" Width="100%" ShowFooter="true" EmptyDataText="No Record Found"
                        CaptionAlign="Bottom" GridLines="None" OnRowDataBound="gvAllotments_RowDataBound"
                        OnPageIndexChanging="gvAllotments_PageIndexChanging" CssClass="gridmain-css">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                        <Columns>
                            <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnVessel_ID" runat="server" Value='<%# Eval("Vessel_ID")%>' />
                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("vessel_short_name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S/C" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSC" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("ID")%>' CssClass="staffInfo"
                                        Target="_blank" Text='<%# Eval("STAFF_CODE")%>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" Visible="false" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Staff_fullName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seaman ID" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeamanID" runat="server" Text='<%# Eval("Seaman_Book_Number")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manning Agent" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblManningAgent" runat="server" Text='<%# Eval("Company_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acc. No." HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccountId" Visible="false" runat="server" Text='<%# Eval("BankAccId") %>'></asp:Label>
                                    <asp:Label ID="lblLeabeWage" runat="server" Text='<%#Eval("Acc_NO") %>'></asp:Label>
                                    <asp:Label ID="lblkAccountInfo" runat="server" Font-Bold="true" Text="i" Font-Italic="true"
                                        Font-Size="11px" ForeColor="Blue"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Beneficiary" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblBeneficiary" runat="server" Text='<%# Eval("Beneficiary")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("Bank_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PB Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblPBDate" runat="server" Text='<%# Eval("PBill_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='Total:'></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <%-- <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="padding-right: 3px">
                                                <asp:Image ID="imgSideLetter" runat="server" ImageAlign="Bottom" />
                                            </td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                    </table>--%>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:#,##0.00}")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblAmountTotal" runat="server" Text=''></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency_TypeId" runat="server" Visible="false" Text='<%# Eval("Currency_id")%>'></asp:Label>
                                    <asp:Label ID="LblCurrency_Type" runat="server" Text='<%# Eval("Currency")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#E6F8E0" ForeColor="#333333" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <EditRowStyle CssClass="RowStyle-css" BackColor="LightGreen" />
                        <PagerStyle Font-Size="Larger" CssClass="pager" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </div>
                <div style="margin: 2px; border: 1px solid #cccccc; height: 22px; vertical-align: bottom;
                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                    padding-top: 5px; background-color: #F6CEE3;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 150px; text-align: left;">
                                Page Size:
                                <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 180px">
                                You are in Page:
                                <asp:Label ID="lblPageStatus" runat="server" Text="0"></asp:Label>
                            </td>
                            <td style="width: 40px">
                            </td>
                            <td style="width: 100px">
                                Total Staff:<asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td style="width: 210px">
                            </td>
                            <td style="text-align: left">
                                <div id="dvInterviewSchedule">
                                </div>
                            </td>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="Div1" style="border: 1px solid #cccccc; margin: 2px; text-align: right;">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="12px"
                        BackColor="Yellow" Font-Italic="true" Width="400px"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" style="min-height: 200px" width="100%">
            </iframe>
            <div style="text-align: right">
                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClientClick="hideModal('dvPopupFrame'); return false;"
                    BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                    Height="24px" BackColor="#81DAF5" Width="80px" />
            </div>
        </div>
    </div>
</asp:Content>
