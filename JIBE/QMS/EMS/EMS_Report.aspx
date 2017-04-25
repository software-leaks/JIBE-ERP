<%@ Page Title="EMS Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EMS_Report.aspx.cs" Inherits="QMS_EMS_EMS_Report" %>

<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />

    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../../Images/loader.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <center>
        <div style="width: 700px; color: Black;">
            <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #cccccc" class="page-title">
                        EMS Report
                    </div>
                    <div id="dvpage-content" class="page-content-main" style="padding: 1px">
                        <table cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" CssClass="txtInput" Width="150px"
                                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    From Date :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtfrom" AutoPostBack="true" CssClass="txtInput" runat="server"
                                        OnTextChanged="txtfrom_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 30%">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vessel :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlvessel" CssClass="txtInput" Width="150px" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    To Date :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtto" AutoPostBack="true" CssClass="txtInput" runat="server" OnTextChanged="txtto_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calto" Format="dd-MM-yyyy" TargetControlID="txtto" runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="gvEMSReport" runat="server" EmptyDataText="No record found !" AutoGenerateColumns="False"
                            Width="100%" CssClass="GridView-css" GridLines="None" CellPadding="4" AllowSorting="True"
                            Style="margin-right: 0px" OnSorting="gvEMSReport_Sorting" OnRowDataBound="gvEMSReport_RowDataBound">
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <Columns>
                                <asp:TemplateField HeaderText="Vessel Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                            CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                        <img id="Vessel_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVSLName" Text='<%#Eval("Vessel_Name")%>' runat="server" class='vesselinfo'
                                            vid='<%# Eval("Vessel_ID")%>' vname='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date From">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblDateFromHeader" runat="server" ForeColor="Black" CommandArgument="DATE_FROM"
                                            CommandName="Sort">DATE_FROM&nbsp;</asp:LinkButton>
                                        <img id="DATE_FROM" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromDate" Text='<%#Eval("DATE_FROM")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date To">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblDateToHeader" runat="server" ForeColor="Black" CommandArgument="DATE_TO"
                                            CommandName="Sort">DATE_TO&nbsp;</asp:LinkButton>
                                        <img id="DATE_TO" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTodate" Text='<%#Eval("DATE_TO")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lbtnView" runat="server" Target="_blank" NavigateUrl='<%#"~/QMS/EMS/EMS_Report_Details.aspx?ID="+Eval("ID").ToString()+"&VSLID="+Eval("VESSEL_ID").ToString()%>'
                                            Text="View"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="20" OnBindDataItem="BindEMSIndexReport" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
