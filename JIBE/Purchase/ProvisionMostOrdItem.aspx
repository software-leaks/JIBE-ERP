<%@ Page Title="Most Ordered Items" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProvisionMostOrdItem.aspx.cs" Inherits="Purchase_ProvisionMostOrdItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<center>
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 1000px;
            height: 100%;">
            <div style="font-size: 24px; background-color: #5588BB; width: 1000px; color: White;
                text-align: center;">
                <b>Most Ordered Item List </b>
            </div>
            <div style="height: 650px; width: 1000px; color: Black;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                            <table width="100%" cellpadding="1" cellspacing="0">
                                <tr align="left">
                                    <td width="10%" align="right" valign="top">
                                        Fleet :
                                    </td>
                                    <td width="15%" valign="top" align="left">
                                        <uc1:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                            Height="150" Width="160" />
                                    </td>
                                    <td width="10%" align="right" valign="top">
                                        Date From :
                                    </td>
                                    <td width="15%" valign="top" align="left">
                                        <asp:TextBox ID="txtFromDate" CssClass="input" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="right" style="width: 25%">
                                        Short Code / Desc. :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="23" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" align="right" valign="top">
                                        Vessel :
                                    </td>
                                    <td width="15%" valign="top" align="left">
                                        <uc1:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                            Height="200" Width="160" />
                                    </td>
                                    <td width="10%" align="right" valign="top">
                                        Date To :
                                    </td>
                                    <td width="15%" valign="top" align="left">
                                        <asp:TextBox ID="txtToDate" CssClass="input" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td width="10%" align="right" valign="top">
                                       Report Type :
                                    </td>
                                   <td width="15%" align="right" valign="top">
                                    <asp:RadioButtonList ID ="rdoReportType"  runat="server" RepeatColumns ="2" AutoPostBack="true" onselectedindexchanged="rdoReportType_SelectedIndexChanged" >
                                    <asp:ListItem Text="Vessel" Value ="0" Selected ="True"></asp:ListItem>
                                    <asp:ListItem Text ="Fleet" Value ="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                    <td colspan="3">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divGrid" style="position: relative; z-index: 0; width: 100%;">
                            <asp:GridView ID="rgdLocation" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="rgdLocation_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                OnSorting="rgdLocation_Sorting" CellPadding="1" CellSpacing="0">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Fleet Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.FleetName")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Item Description
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Short_Description")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Unit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Unit_and_Packings")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Total Time
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.ORDER_TIME")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Ordered QTY
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Total_Order_Qty")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>                                   
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdLocation" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" />
                        </div>
                    </ContentTemplate>
                     <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                        <asp:AsyncPostBackTrigger ControlID="rdoReportType" EventName="SelectedIndexChanged"/>
                    </Triggers> 
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>

