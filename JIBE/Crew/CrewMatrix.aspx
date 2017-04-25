<%@ Page Title="Crew Matrix" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewMatrix.aspx.cs" Inherits="Crew_CrewMatrix" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Crew Matrix"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="ImgExpExcel" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hdnVesselName" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnVesselID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnVesselType" runat="server" Value="0" />
                    <table border="0" cellpadding="2" cellspacing="0" style="width: 100%; text-align: center">
                        <tr>
                            <td align="right" style="width: 160px">
                                <b>Vessel:</b>
                            </td>
                            <td align="left" style="width: 160px">
                                <asp:DropDownList ID="ddlVessel" runat="server" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 160px">
                                <b>Type Of Vessel:</b>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTankerType" runat="server"></asp:Label>
                            </td>
                            <td align="right" style="width: 160px">
                                <b>Date:</b>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="center" style="width: 5%">
                                <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                    ImageUrl="~/Images/SearchButton.png" />
                            </td>
                            <td style="width: 5%">
                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                    ImageUrl="~/Images/Exptoexcel.png" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server">
            <ContentTemplate>
                <div>
                    <div id="gridOfficer" style="margin-top: 2px">
                        <table width="100%">
                            <tr>
                                <td align="right" style="padding-right: 250px; font-weight: bold; color: Blue; font-size: medium">
                                    Years in Service
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="3"
                            CellSpacing="0" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                            GridLines="None" CssClass="GridView-css">
                            <Columns>
                                <asp:BoundField DataField="Dept" HeaderText="Department" ItemStyle-Width="7%" HeaderStyle-Width="7%" />
                                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nationality" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("Nationality")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cert.Comp." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCertComp" runat="server" Text='<%# Eval("Certificate_Of_Competency")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issuing Country" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Admin. Accept" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdminAccept" runat="server" Text='<%# Eval("Adminstration_Acceptance")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tanker Cert." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTankerCert" runat="server" Text='<%# Eval("Tanker_Certification")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STCW V Para." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSTCWVP" runat="server" Text='<%# Eval("STCWVPara")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Radio Qual." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRadioQual" runat="server" Text='<%# Eval("Radio_Qualification")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operator" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Blue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOperator" runat="server" Text='<%# Eval("YearsOfOperator")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Blue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("YearsOfRank")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tanker Type" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-ForeColor="Blue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTankerType" runat="server" Text='<%# Eval("YearsOfTanker")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="All Types" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Blue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllType" runat="server" Text='<%# Eval("YearsOfAllTanker")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="M O Tour" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Blue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMOTour" runat="server" Text='<%# Eval("Months")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" Font-Size="10px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="English Prof." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEnglishProf" runat="server" Text='<%# Eval("English_Proficiency")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" Font-Size="10px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
