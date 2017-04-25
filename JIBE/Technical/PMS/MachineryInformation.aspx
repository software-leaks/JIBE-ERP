<%@ Page Language="C#" Title="Machinery Information" AutoEventWireup="true" MasterPageFile="~/Site.master"
    CodeFile="MachineryInformation.aspx.cs" Inherits="MachineryInformation" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucCustomDDlList" %>
<%@ Register Src="~/UserControl/ucCustomStringFilter.ascx" TagName="ucCustomStringFilter"
    TagPrefix="ucCustomString" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="pnlCatalogue" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div style="font-family: Tahoma; font-size: 12px">
                <center>
                    <div style="border: 1px solid  #5588BB;">
                        <div class="page-title">
                            Machinery Information
                        </div>
                        <div style="vertical-align: middle; padding-top: 6px">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td style="width: 4%; text-align: right;">
                                        Fleet :&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 5%" align="left">
                                        <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 5%">
                                        Department :&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 5%" align="left">
                                        <asp:DropDownList ID="cmbDept" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                            Width="117px">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 5%">
                                        Location :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <asp:DropDownList ID="ddllocation" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                            Width="117px">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                       
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <asp:Button ID="btnRetrieve" runat="server" OnClick="btnRetrieve_Click" CssClass="btncss"
                                            Text="Search" Width="64%" />
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btncss" OnClick="btnExport_Click"
                                            Width="120px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="style2">
                                        Vessel :&nbsp;&nbsp;
                                    </td>
                                    <td style="text-align: left;" class="style3">
                                        <b>
                                            <asp:UpdatePanel ID="updVessel" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DDLFleet" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                                        Width="117px">
                                                        <asp:ListItem Selected="True" Value="0">--ALL--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </b>
                                    </td>
                                    <td align="right">
                                        Function :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmb_Function" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                            Width="117px">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 5%">
                                        Show :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <asp:RadioButtonList ID="optdisplayRecordType" runat="server" AppendDataBoundItems="True"
                                            Width="200px" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="2">ALL</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Deleted</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right" style="width: 5%">
                                        Search :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtsearchtext" runat="server" Style="font-size: small; text-align: left;"
                                            Width="113px"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnClearFilter" runat="server" OnClick="btnClearFilter_Click" CssClass="btncss"
                                            Text="Clear Filters" Width="120px" />
                                    </td>
                                    <td align="center" style="width: 30%">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="border: 1px solid gray; margin-top: 2px">
                        <asp:GridView ID="gvCatalogue" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                            OnRowDataBound="gvCatalogue_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                            ShowHeaderWhenEmpty="true" OnSorting="gvCatalogue_Sorting" DataKeyNames="ID">
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <SelectedRowStyle BackColor="#FFFFCC" />
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" Visible="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblNoHeader" runat="server" ForeColor="Black">Sr.No&nbsp;</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ROWNUM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" HorizontalAlign="Left" Width="2px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vessel">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandName="Sort"
                                            CommandArgument="Vessel_Name">
                                            Vessel &nbsp;
                                        </asp:LinkButton>
                                        <img id="Vessel_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                        <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Function">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblFunctionNameHeader" runat="server" ForeColor="Black">Function&nbsp;</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFunctionNameName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Function_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblDeptNameHeader" runat="server" CommandName="Sort" CommandArgument="Department"
                                            ForeColor="Black">Department&nbsp;</asp:LinkButton>
                                        <img id="Department" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeptName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Department") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Machinery Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblSystemNameHeader" runat="server" CommandName="Sort" CommandArgument="System_Name"
                                            ForeColor="Black">Machinery Name&nbsp;</asp:LinkButton>
                                        <img id="System_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSystemName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.System_Name") %>'></asp:Label>
                                        <asp:Label ID="lblCalogueActiveSatus" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Active_Status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="230px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblLocationNameHeader" runat="server" CommandName="Sort" CommandArgument="Location_Name"
                                            ForeColor="Black">Location Name&nbsp;</asp:LinkButton>
                                        <img id="Location_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocationName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Location_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Model">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblModelHeader" runat="server" ForeColor="Black">Model&nbsp;</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Model") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Set_Instaled">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblSetInstaledHeader" runat="server" ForeColor="Black">Sets&nbsp;</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSetInstalled" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Set_Instaled") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SrNumber">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblSrNumberHeader" runat="server" ForeColor="Black">Sr Number&nbsp;</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrNumber" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SrNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Particulars">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblParticularsHeader" runat="server" ForeColor="Black">Particulars&nbsp;</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Particulars") %>'></asp:Label>
                                        <asp:Label ID="lblParticularsFullDetails" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Particulars_Full_Details") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="140px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maker">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblMakerHeader" runat="server" CommandName="Sort" CommandArgument="MakerName"
                                            ForeColor="Black">Maker &nbsp;</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaker" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MakerName") %>'></asp:Label>
                                        <asp:Label ID="lblMakerFullDetails" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MakerName_Full_Details") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="140px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                         <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem=" BindMachineryInfo" />
                          <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                    </div>
                  
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
