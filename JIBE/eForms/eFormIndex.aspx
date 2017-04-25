<%@ Page Title="eForms Index" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="eFormIndex.aspx.cs" Inherits="eForms_eFormIndex" %>
    <%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
        <div id="page-title" style="margin: 2px; border: 1px solid #cccccc; height: 20px;
            vertical-align: bottom; background: url(../Images/bg.png) left -10px repeat-x;
            color: Black; text-align: left; padding: 2px; background-color: #F6CEE3;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 33%;">
                    </td>
                    <td style="width: 33%; text-align: center; font-weight: bold;">
                        <asp:Label ID="lblPageTitle" runat="server" Text="eForms"></asp:Label>
                    </td>
                    <td style="width: 33%; text-align: right;">
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" cellspacing="2" border="0">
                        <tr>
                            <td>
                                Fleet
                            </td>
                            <td>
                                <auc:CustomDropDownList ID="ddlFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                    Height="150" Width="160" />
                            </td>
                            <td>
                                From Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                <tlk4:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtFromDate"
                                    Format="dd/MM/yyyy">
                                </tlk4:CalendarExtender>
                            </td>
                            <td>
                                Search
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Style="width: 100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Vessel Name
                            </td>
                            <td align="left">
                                <auc:CustomDropDownList ID="ddlVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                    Height="200" Width="160" />
                            </td>
                            <td>
                                To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                <tlk4:CalendarExtender ID="calTo" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy">
                                </tlk4:CalendarExtender>
                            </td>
                            <td >
                                <asp:Button ID="btnSearch" runat="server" ToolTip="Search"  CssClass="btncss"   Text="Search" OnClick="btnSearch_Click" />
                                
                            </td>
                            <td>
                            <asp:Button  ID="btnClearFilter" runat="server" Width="50%" ToolTip ="Clear Filter" Text="Clear Filter" OnClick="btnClearFilter_Click" />                                 
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
                    <asp:GridView ID="GridView_Reports" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="7" AllowPaging="false"
                        Width="100%" ShowFooter="false" EmptyDataText="NO RECORDS FOUND!" CaptionAlign="Bottom"
                        GridLines="None">
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="S/N">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Report Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblReportDate" runat="server" Text='<%# Eval("ReportDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="150" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Report Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblForm_Display_Name" runat="server" Text='<%# Eval("Form_Display_Name")%>' NavigateUrl='<%# "eFormTempletes/" + Eval("Web_Form_Name").ToString() + "?VID=" + Eval("Vessel_ID").ToString() + "&DTLID=" + Eval("Dtl_Report_ID").ToString() + "&Form_ID=" + Eval("Form_ID").ToString()%>' Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency">
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("Frequency")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                    </asp:GridView>
                    <auc:CustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="Load_Report_Index" />
                    <br />
                    <br />
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
