<%@ Page Title="A/E Monthly Running Hrs Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AERunningHoursMonthlyReport.aspx.cs" Inherits="eForms_eFormTempletes_AERunningHoursMonthlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr  class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="A/E Monthly Running Hrs Report"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:FormView ID="frmMain" runat="Server" Width="100%">
                <ItemTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%; text-align: left; vertical-align: top;">
                                            <table class="eform-report-table" cellpadding="2" border=1>
                                                <tr>
                                                    <td style="width: 150px">
                                                        Vessel Name:
                                                    </td>
                                                    <td style="width: 150px" class="eform-field-data">
                                                        <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        A/E Type:
                                                    </td>
                                                    <td class="eform-field-data">
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("AE_Type")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        Report Date:
                                                    </td>
                                                    <td style="text-align: left" class="eform-field-data">
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("ReportDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%; text-align: right; vertical-align: top;">
                                            <table style="width: 100%;">
                                                
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <table style="width: 100%;" border="1">
                                                            <tr class="eform-header-row">
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    A/E 1
                                                                </td>
                                                                <td>
                                                                    A/E 2
                                                                </td>
                                                                <td>
                                                                    A/E 3
                                                                </td>
                                                                <td>
                                                                    A/E 4
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Total R/H Last Month
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("AE1_Total_RH_LastMonth")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("AE2_Total_RH_LastMonth")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("AE3_Total_RH_LastMonth")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("AE4_Total_RH_LastMonth")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Running Hrs This Month
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("AE1_Total_RH_ThisMonth")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label8" runat="server" Text='<%#Eval("AE2_Total_RH_ThisMonth")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("AE3_Total_RH_ThisMonth")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("AE3_Total_RH_ThisMonth")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Total Running Hrs
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("AE1_Total_RH")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("AE2_Total_RH")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("AE3_Total_RH")%>'></asp:Label>
                                                                </td>
                                                                <td class="eform-field-data">
                                                                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("AE4_Total_RH")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="eform-report-sub-header">
                                Running Hours from Last O/H
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_RunningHours" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="true" CssClass="GridView-css" CellPadding="7" AllowPaging="false"
                                    Width="100%" ShowFooter="false" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                                    GridLines="Both">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S/N">
                                            <ItemTemplate>
                                                <%# Eval("ID")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <%# Eval("Item_Name")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Running Hrs for O/H as Recommended by Maker" >
                                            <ItemTemplate>
                                                <%# Eval("RH_By_Maker")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="150" CssClass="eform-header-row"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/E 1" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Eval("AE1")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/E 2" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Eval("AE2")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/E 3" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Eval("AE3")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/E 4" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Eval("AE4")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="80"  />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>
