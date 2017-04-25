<%@ Page Title="Vessels Drill Report Summary" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SCM_Vessels_Drill_Reports.aspx.cs" Inherits="SCM_Vessels_Drill_Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
     Vessels Drill Report Summary
    </div>
    <center>
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
        <div style="font-family: Tahoma; font-size: 12px; width: 100%; text-align: center;">
            
            <div style="border: 1px solid #cccccc; color: Black; padding-top: 10px; ">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td style="width: 6%" align="right">
                                    Fleet : &nbsp; &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px" Height="20px"
                                        Font-Size="11px" BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 6%" align="right">
                                    Vessel : &nbsp; &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        Width="120px" Height="20px" Font-Size="11px" BackColor="#FFFFCC">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 6%" align="right">
                                    Drill Type :&nbsp; &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLDrillType" runat="server" AppendDataBoundItems="true" Width="200px"
                                        Height="20px" Font-Size="11px" BackColor="#FFFFCC">
                                    </asp:DropDownList>
                                </td>
                                <%--   <td style="width: 8%" align="right">
                                    From :&nbsp;&nbsp;
                                </td>
                                <td style="width: 7%">
                                    <asp:TextBox ID="txtFromDate" Font-Size="11px" Width="140px" EnableViewState="true"  CssClass="txtInput"
                                        runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="caltxtFromDate" Format="dd-MM-yyyy" TargetControlID="txtFromDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 8%" align="right">
                                    To :&nbsp;&nbsp;
                                </td>
                                <td style="width: 7%">
                                    <asp:TextBox ID="txtToDate" Font-Size="11px" Width="140px" EnableViewState="true"  CssClass="txtInput"
                                        runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="caltxtToDate" Format="dd-MM-yyyy" TargetControlID="txtToDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>--%>
                                <td style="width: 6%" align="center">
                                    <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" OnClick="btnRetrieve_Click"
                                        ImageUrl="~/Images/SearchButton.png" ToolTip="Search" />
                                </td>
                                <td style="width: 6%" align="center">
                                    <asp:ImageButton ID="btnClearFilter" runat="server" Height="25px" OnClick="btnClearFilter_Click"
                                        ImageUrl="~/Images/filter-delete-icon.png" ToolTip="Clear Filter" />
                                </td>
                                <td align="center">
                                    <asp:ImageButton ID="btnExport" runat="server" Height="22px" ImageUrl="~/Images/XLS.jpg"
                                        OnClick="btnExport_Click" ToolTip="Export to Excel" />
                                </td>
                                <td style="width: 20%">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="border: 0px solid #cccccc; padding: 2px; margin-top: 10px">
                    <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div style="overflow-x: scroll; overflow-y: hidden; height: auto; width: auto; border: 1px solid #cccccc;
                                text-align: left">
                                <asp:GridView ID="gvVesselsDrill" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    CellPadding="4" CellSpacing="1" AutoGenerateColumns="true" OnRowDataBound="gvVesselsDrill_RowDataBound"
                                    Width="100%" GridLines="none">
                                    <HeaderStyle CssClass="PMSGridHeaderStyle-css" HorizontalAlign="Center" />
                                    <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" HorizontalAlign="Center" />
                                    <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnExport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </center>
</asp:Content>
