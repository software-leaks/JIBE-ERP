<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="PMSRunningHours.aspx.cs"
    Inherits="PMSRunningHours" Title="System Running Hours" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomStringFilter.ascx" TagName="ucfString"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomNumberFilter.ascx" TagName="ucfNumber"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomDateFilter.ascx" TagName="ucfDate" TagPrefix="CustomFilter" %>
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
        function comparedate() {

            var FromDate = document.getElementById($('[id$=txtFromDate]').attr('id'));
            var ToDate = document.getElementById($('[id$=txtToDate]').attr('id'));

            var strFDate = FromDate.value;
            var strTDate = ToDate.value;


            if (strFDate != "") {
                if (isValidDate(strFDate) == false) {
                    return false;
                }
            }
            if (strTDate != "") {
                if (isValidDate(strTDate) == false) {
                    return false;
                }
            }

            var dates = {
                convert: function (d) {
                    // Converts the date in d to a date-object. The input can be:
                    //   a date object: returned without modification
                    //  an array      : Interpreted as [year,month,day]. NOTE: month is 0-11.
                    //   a number     : Interpreted as number of milliseconds
                    //                  since 1 Jan 1970 (a timestamp) 
                    //   a string     : Any format supported by the javascript engine, like
                    //                  "YYYY/MM/DD", "MM/DD/YYYY", "Jan 31 2009" etc.
                    //  an object     : Interpreted as an object with year, month and date
                    //                  attributes.  **NOTE** month is 0-11.
                    return (
            d.constructor === Date ? d :
            d.constructor === Array ? new Date(d[0], d[1], d[2]) :
            d.constructor === Number ? new Date(d) :
            d.constructor === String ? new Date(d) :
            typeof d === "object" ? new Date(d.year, d.month, d.date) :
            NaN
        );
                },
                compare: function (a, b) {
                    // Compare two dates (could be of any type supported by the convert
                    // function above) and returns:
                    //  -1 : if a < b
                    //   0 : if a = b
                    //   1 : if a > b
                    // NaN : if a or b is an illegal date
                    // NOTE: The code inside isFinite does an assignment (=).
                    return (
            isFinite(a = this.convert(a).valueOf()) &&
            isFinite(b = this.convert(b).valueOf()) ?
            (a > b) - (a < b) :
            NaN
        );
                },
                inRange: function (d, start, end) {
                    // Checks if date in d is between dates in start and end.
                    // Returns a boolean or NaN:
                    //    true  : if d is between start and end (inclusive)
                    //    false : if d is before start or after end
                    //    NaN   : if one or more of the dates is illegal.
                    // NOTE: The code inside isFinite does an assignment (=).
                    return (
            isFinite(d = this.convert(d).valueOf()) &&
            isFinite(start = this.convert(start).valueOf()) &&
            isFinite(end = this.convert(end).valueOf()) ?
            start <= d && d <= end :
            NaN
        );
                }


            }

            var Fchunks = FromDate.value.split('-');
            var FformattedDate = [Fchunks[1], Fchunks[0], Fchunks[2]].join("/");
            var Tchunks = ToDate.value.split('-');
            var TformattedDate = [Tchunks[1], Tchunks[0], Tchunks[2]].join("/");

            var FDate = new Date(FformattedDate);
            var TDate = new Date(TformattedDate);

            if (strFDate.trim() != "" && strTDate.trim() != "") {
                if (dates.compare(FDate, TDate) == 1) {
                    alert("To Date Cannot be less than From Date");
                    return false;
                }
            }


            return true;
        }

        function isValidDate(dateStr) {
            // Checks for the following valid date formats: // MM/DD/YYYY 
            // Also separates date into month, day, and year variables
            dateStr = dateStr.toString("MM/DD/YYYY");
            //            var datePat = /^(\d{2,2})(\/)(\d{2,2})\2(\d{4}|\d{4})$/;
            var datePat = /^(\d{2,2})(\-)(\d{2,2})\2(\d{4}|\d{4})$/;
            var matchArray = dateStr.match(datePat); // is the format ok? 
            if (matchArray == null) {
                alert("Date must be in DD-MM-YYYY format")
                return false;
            }
            day = matchArray[1]; // parse date into variables 
            month = matchArray[3];
            year = matchArray[4];
            if (month < 1 || month > 12) { // check month range 
                alert("Month must be between 1 and 12");
                return false;
            }
            if (day < 1 || day > 31) {
                alert("Day must be between 1 and 31");
                return false;
            }
            if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
                alert("Month " + month + " doesn't have 31 days!")
                return false;
            }
            if (month == 2) { // check for february 29th 
                var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
                if (day > 29 || (day == 29 && !isleap)) {
                    alert("February " + year + " doesn't have " + day + " days!");
                    return false;
                }
            } return true; // date is valid 
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
        <div style="font-family: Tahoma; font-size: 12px">
            <div style="border: 1px solid  #5588BB; padding: 2px; background-color: #5588BB;
                color: #FFFFFF; text-align: center;">
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 95%">
                            <b>System Running Hours</b>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #cccccc; height: 100px; padding-top: 2px; padding-bottom: 5px;">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" width="100%" style="color: Black; border-color: #0000FF">
                            <tr>
                                <td style="width: 6%" align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td style="width: 3%" align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="txtInput" Font-Size="11px" Height="20px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                        Width="120px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%" align="right">
                                    Function :&nbsp;&nbsp;
                                </td>
                                <td style="width: 4%" align="left">
                                    <asp:DropDownList ID="ddlFunction" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                        CssClass="txtInput" OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%" align="center">
                                    <asp:RadioButtonList ID="optRecordDisplayType" runat="server" OnSelectedIndexChanged="optRecordDisplayType_SelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="Current" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="History" Value="H"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" style="width: 15%">
                                    Date Read From :&nbsp;&nbsp;
                                </td>
                                <td style="width: 6%" align="center">
                                    <asp:TextBox ID="txtFromDate" Font-Size="11px" Width="140px" EnableViewState="true"
                                        CssClass="txtInput" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="caltxtFromDate" Format="dd-MM-yyyy" TargetControlID="txtFromDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 6%" align="center">
                                </td>
                                <td style="width: 6%" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 6%">
                                    Vessel :&nbsp;&nbsp;
                                </td>
                                <td align="left" style="width: 3%">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        Width="120px" CssClass="txtInput" Height="20px" Font-Size="11px" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 15%">
                                    System /Location :
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:DropDownList ID="ddlSystem_location" runat="server" AppendDataBoundItems="True"
                                        CssClass="txtInput" AutoPostBack="True" OnSelectedIndexChanged="ddlSystem_location_SelectedIndexChanged"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%" align="left">
                                    <asp:CheckBox ID="chkDisplayInheritingCounter" runat="server" Text="Display Inheriting Counters" />
                                </td>
                                <td align="right" style="width: 10%">
                                    To Read Date :&nbsp;&nbsp;
                                </td>
                                <td align="center" style="width: 6%">
                                    <asp:TextBox ID="txtToDate" Font-Size="11px" Width="140px" EnableViewState="true"
                                        CssClass="txtInput" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="caltxtToDate" Format="dd-MM-yyyy" TargetControlID="txtToDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="center" style="width: 6%">
                                    &nbsp;
                                </td>
                                <td align="center" style="width: 6%">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 6%">
                                </td>
                                <td align="left" style="width: 3%">
                                </td>
                                <td align="right" style="width: 15%">
                                    Subsystem / Location :
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:DropDownList ID="ddlSubSystem_location" runat="server" CssClass="txtInput" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%" align="right">
                                </td>
                                <td align="right" style="width: 10%">
                                </td>
                                <td align="right" style="width: 6%">
                                    <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" OnClick="btnRetrieve_Click"
                                        OnClientClick="return comparedate();" ImageUrl="~/Images/SearchButton.png" ToolTip="Search"
                                        Style="width: 27px" />
                                </td>
                                <td align="center" style="width: 6%">
                                    <asp:ImageButton ID="btnClearFilter" runat="server" Height="25px" OnClick="btnClearFilter_Click"
                                        ImageUrl="~/Images/filter-delete-icon.png" ToolTip="Clear Filter" />
                                </td>
                                <td align="left" style="width: 6%;">
                                    <asp:ImageButton ID="btnExport" ImageUrl="~/Images/XLS.jpg" Height="25px" OnClick="btnExport_Click"
                                        runat="server" ToolTip="Export to Excel" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="border: 1px solid #cccccc; padding: 2px; margin-top: 2px">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x: hidden; overflow-y: none; border: 0px solid gray;">
                            <asp:GridView ID="gvRhrs" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="gvRhrs_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                ShowHeaderWhenEmpty="true" CellPadding="4" CellSpacing="2" OnSorting="gvRhrs_Sorting"
                                DataKeyNames="ID" OnRowCreated="gvRhrs_RowCreated">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                            <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="System Location">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblLocationHeader" runat="server" ForeColor="Black" CommandArgument="Location"
                                                CommandName="Sort">System Location&nbsp;</asp:LinkButton>
                                            <img id="Location" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Location") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="160px" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="160px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub-System Location">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSubSystemLocationHeader" runat="server" ForeColor="Black"
                                                CommandArgument="SubLocation" CommandName="Sort">Sub-System Location&nbsp;</asp:LinkButton>
                                            <img id="SubSystemLocation" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubSystemLocation" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="160px" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="160px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Hours Read">
                                        <HeaderTemplate>
                                            Date Hours Read&nbsp;
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateHoursRead" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Date_Hours_Read","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Differential R.Hours">
                                        <HeaderTemplate>
                                            Differential R.Hours
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.diff_in_Rhrs") %>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="150px" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Running Hours">
                                        <HeaderTemplate>
                                            R. Hours
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRunningHours" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Runing_Hours") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created_On">
                                        <HeaderTemplate>
                                            Created On
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Created_On","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindRuningHours" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
