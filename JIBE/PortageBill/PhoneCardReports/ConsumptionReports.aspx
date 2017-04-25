<%@ Page Title="Consumption Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ConsumptionReports.aspx.cs" Inherits="PortageBill_PhoneCardReports_ConsumptionReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/uc_Vessel_List.ascx" TagName="uc_Vessel_List" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
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
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>Consumption Reports </b>
                </div>
            </div>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnlVesselConsumption" runat="server">
                            <div style="background-color: transparent; height: 100%; width: 600px;">
                                <div class="popup-content">
                                    <asp:HiddenField ID="hdnEditEventID" runat="server" />
                                    <table width="100%" cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td width="40%" align="right">
                                                Consumption Type
                                            </td>
                                            <td colspan="4" width="60%" align="left" style="text-align: left; color: Black; font-weight: bold;">
                                                <asp:RadioButtonList ID="rblReports" Width="360px" runat="server" AutoPostBack="true"
                                                    CssClass="input" ClientIDMode="Static" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblReports_SelectedIndexChanged">
                                                    <asp:ListItem Value="Vessel" Text="Vessel" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Crew" Text="Crew"></asp:ListItem>
                                                    <asp:ListItem Value="Month" Text="Month"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr id="trVesselwice" runat="server">
                                            <td width="40%" align="right">
                                                Vessel Name:
                                            </td>
                                            <td align="left" width="25%">
                                                <asp:DropDownList ID="cmbVessel" CssClass="input" runat="server" Width="150px" DataTextField="Vessel_Name"
                                                    DataValueField="Vessel_id" OnDataBound="cmbVessel_OnDataBound">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" width="35%" colspan="3" style="text-align: center">
                                                <asp:Button ID="btnShowVesselConsumption" runat="server" Text="View Reports" OnClick="btnShowVesselConsumption_Click" />
                                            </td>
                                        </tr>
                                        <tr id="trCrewWice" runat="server">
                                            <td width="40%" align="right">
                                                Crew:
                                            </td>
                                            <td align="left" width="25%">
                                                <asp:DropDownList ID="ddlCrew" runat="server" CssClass="input" ForeColor="Black"
                                                    Font-Size="11px" Width="120px" DataSourceID="objCrew" DataTextField="STAFF_NAME"
                                                    DataValueField="VOYAGEID" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="objCrew" runat="server" TypeName="SMS.Business.PortageBill.BLL_PB_PhoneCard"
                                                    SelectMethod="PhoneCard_VoyagesList"></asp:ObjectDataSource>
                                            </td>
                                            <td align="left" colspan="3" width="35%" style="text-align: center">
                                                <asp:Button ID="btnShowCrewConsumption" runat="server" Text="View Reports" OnClick="btnShowCrewConsumption_Click" />
                                            </td>
                                        </tr>
                                        <tr id="trMonthWice" runat="server">
                                            <td width="40%" align="right">
                                                Month:
                                            </td>
                                            <td width="25%">
                                                <asp:DropDownList ID="ddlMonth" runat="server" Width="100px">
                                                    <asp:ListItem Value="01" Text="January" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="02" Text="Febuary"></asp:ListItem>
                                                    <asp:ListItem Value="03" Text="March"></asp:ListItem>
                                                    <asp:ListItem Value="04" Text="April"></asp:ListItem>
                                                    <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                                    <asp:ListItem Value="06" Text="June"></asp:ListItem>
                                                    <asp:ListItem Value="07" Text="Jully"></asp:ListItem>
                                                    <asp:ListItem Value="08" Text="Augest"></asp:ListItem>
                                                    <asp:ListItem Value="09" Text="September"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="Octuber"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="10%">
                                                Year:
                                            </td>
                                            <td width="15%">
                                                <asp:DropDownList ID="ddlYear" runat="server" Width="100px">
                                                    <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                                                    <asp:ListItem Value="2012" Text="2012" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="10%" style="text-align: center">
                                                <asp:Button ID="btnShowMonthConsumption" runat="server" Text="View Reports" OnClick="btnShowMonthConsumption_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                &nbsp;</div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <iframe runat="server" id="ifMoreInfo" scrolling="auto" style="min-height: 600px;
                            width: 100%; height: 100%"></iframe>
                    </td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
</asp:Content>
