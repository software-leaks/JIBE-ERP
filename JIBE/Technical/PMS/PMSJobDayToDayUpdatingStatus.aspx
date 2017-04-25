<%@ Page Title="Job Progress" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSJobDayToDayUpdatingStatus.aspx.cs" Inherits="PMSJobDayToDayUpdatingStatus" %>

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
    <script language="javascript" type="text/javascript">

        function OnRetrieve() {

            if (document.getElementById("ctl00_MainContent_DDLFleet").value == "0") {
                alert("Please select fleet.");
                document.getElementById("ctl00_MainContent_DDLFleet").focus();
                return false;
            }

            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Daily Jobs Updating Summary
    </div>
    <div id="dvPageContent" class="page-content-main">
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
            <div style="font-family: Tahoma; font-size: 12px; width: 100%; text-align: center;
                height: 800px">
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 10%" align="right">
                            Fleet :&nbsp;&nbsp;
                        </td>
                        <td style="width: 3%" align="left">
                            <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" CssClass="txtInput"
                                Font-Size="11px" Width="120px">
                                <asp:ListItem Selected="True" Value="0">--SELECT FLEET--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 8%" align="right">
                            From :&nbsp;&nbsp;
                        </td>
                        <td style="width: 7%">
                            <asp:TextBox ID="txtFromDate" Font-Size="11px" Width="140px" EnableViewState="true"
                                CssClass="txtInput" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="caltxtFromDate" Format="dd-MM-yyyy" TargetControlID="txtFromDate"
                                runat="server">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 8%" align="right">
                            To :&nbsp;&nbsp;
                        </td>
                        <td style="width: 7%">
                            <asp:TextBox ID="txtToDate" Font-Size="11px" Width="140px" EnableViewState="true"
                                CssClass="txtInput" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="caltxtToDate" Format="dd-MM-yyyy" TargetControlID="txtToDate"
                                runat="server">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 6%" align="center">
                            <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" OnClientClick="return OnRetrieve();"
                                OnClick="btnRetrieve_Click" ImageUrl="~/Images/SearchButton.png" ToolTip="Search" />
                        </td>
                        <td style="width: 6%" align="center">
                            <asp:ImageButton ID="btnClearFilter" Visible="false" runat="server" Height="25px" OnClick="btnClearFilter_Click"
                                ImageUrl="~/Images/filter-delete-icon.png" ToolTip="Clear Filter" />
                        </td>
                        <td style="width: 6%" align="center">
                        </td>
                        <td style="width: 40%; color: Red;">
                            *** Fleet selection is a mandatory to search the records
                        </td>
                    </tr>
                </table>
                <div style="border: 0px solid #cccccc;">
                    <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div style="overflow-x: scroll; overflow-y: hidden; height: auto; width: 100%; border: 1px solid #cccccc;
                                text-align: left">
                                <asp:GridView ID="gvJobDailySummary" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    CellPadding="4" CellSpacing="0" AutoGenerateColumns="false" OnRowDataBound="gvJobDailySummary_RowDataBound"
                                    Width="100%" GridLines="Both" OnRowCreated="gvJobDailySummary_RowCreated">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                                <%--  <uc1:ucCustomPager ID="ucCustomPagerItems" Visible="false" runat="server" OnBindDataItem="BindDailyJobUpdatingSummary" />--%>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </center>
    </div>
</asp:Content>
