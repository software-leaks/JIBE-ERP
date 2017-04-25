<%@ Page Title="Deck Log Book Index" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="DeckLogBookIndex.aspx.cs"
    Inherits="DeckLogBookIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <center>
    <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        <div style="width: 700px; color: Black;">
            <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #cccccc" class="page-title">
                        Deck Log Book Index
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
                                    <asp:TextBox ID="txtfrom"  CssClass="txtInput" runat="server"
                                        OnTextChanged="txtfrom_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnRetrieve" runat="server"  Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Search" Width="80px" />
                                </td>
                                <td style="width: 15%" align="center">
                                <a href="Logbook_Scales_Description.pdf" target="_blank" >Scale</a>
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
                                    <asp:TextBox ID="txtto"  CssClass="txtInput" runat="server" OnTextChanged="txtto_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calto" Format="dd-MM-yyyy" TargetControlID="txtto" runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnClearFilter" runat="server"  Height="20px" OnClick="btnClearFilter_Click"
                                        Text="Clear Filters" Width="80px" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style="border: 0px solid gray; margin-top: 0px; cursor: pointer; height: 700px;">
                            <asp:GridView ID="gvDeckLogBook" runat="server" EmptyDataText="No record found !"
                                AutoGenerateColumns="False" Width="100%" CssClass="GridView-css" GridLines="None"
                                CellPadding="4" AllowSorting="True" Style="margin-right: 0px; cursor: pointer;"
                                OnSorting="gvDeckLogBook_Sorting" OnRowDataBound="gvDeckLogBook_RowDataBound"
                                OnRowCreated="gvDeckLogBook_RowCreated" OnSelectedIndexChanging="gvDeckLogBook_SelectedIndexChanging">
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
                                            <asp:Label ID="lblDeckLogBookID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                            <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# Eval("Vessel_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Report Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblDateFromHeader" runat="server" ForeColor="Black" CommandArgument="DATE_FROM"
                                                CommandName="Sort">Report Date&nbsp;</asp:LinkButton>
                                            <img id="DATE_FROM" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("Report_Date", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Port">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromPort" runat="server" Text='<%# Eval("From_Port")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Port">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToPort" runat="server" Text='<%# Eval("To_Port")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" PageSize="30" runat="server" OnBindDataItem=" BindGrid" />
                             <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
