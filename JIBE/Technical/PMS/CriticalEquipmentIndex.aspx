<%@ Page Title="Critical Equipment Index" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CriticalEquipmentIndex.aspx.cs" Inherits="Technical_PMS_CriticalEquipmentIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-header" class="page-title">
                <b>Critical Equipment index</b>
            </div>
            <div style="color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                            <table width="100%" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td width="100%" valign="top" style="border: 1px solid gray; color: Black">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td width="20%" align="right" valign="top">
                                                            Fleet :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                                                AutoPostBack="true" Width="160" />
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Vessel :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged"
                                                                AutoPostBack="true" Width="160" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divGrid">
                            <asp:GridView ID="gvCriticalIndex" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" CellPadding="3" GridLines="None" CellSpacing="0"
                                Width="100%" Font-Size="12px" CssClass="GridView-css">
                                <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                <PagerStyle CssClass="PagerStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="lbtVesslNameHeader" runat="server">Vessel Name</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VESSEL_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="180"></ItemStyle>
                                        <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblNameHeader" runat="server">Name</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="200"></ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Perticulars">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblNameParticularsHeader" runat="server">Particulars</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticulars" runat="server" Text='<%#  Eval("Particular") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="200"></ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTypeHeader" runat="server">Type</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%#  Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="200"></ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maker">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblMakerHeader" runat="server">Maker</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaker" runat="server" Text='<%#  Eval("Maker") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="200"></ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblModelHeader" runat="server">Model</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" runat="server" Text='<%#  Eval("Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="200"></ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                            background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindGrid" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
