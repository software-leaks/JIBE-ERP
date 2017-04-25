<%@ Page Language="C#" Title="Job Change Request" AutoEventWireup="true" CodeFile="PMSJobChangeRequest.aspx.cs"
    Inherits="PMSJobChangeRequest" EnableEventValidation="false" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
<%@ Register Src="../../UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList"
    TagPrefix="ucFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>PMS Machinery Change Request </title>
    <%-- <link href="../../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Common.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Logistic.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />--%>
    <%--  <link href="../../styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />--%>
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
    <script language="javascript" type="text/javascript">
           
        function CloseDiv() {

            var control = document.getElementById("divSystemChagneReqst");
            control.style.visibility = "hidden";
        }


        function OpenRequestDetails(Vessel_ID, ID, Status) {

            var url = 'PMSJobChangeRequestAction.aspx?VESSELID=' + Vessel_ID + '&Change_Reqst_ID=' + ID + '&Status=' + Status
            var ButtonUniqueID = "<%= btnFilter.UniqueID %>";
            OpenPopupWindowBtnID('JobChangeRequest', 'Job Change Request', url, 'popup', 550, 1000, null, null, false, false, true, null, ButtonUniqueID);
        }
        
    </script>

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
    <style type="text/css">
        .HeadetTHStyle
        {
            text-align: center;
        }
    </style>
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
            <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="page-title">
                        Job Change Request
                    </div>
                    <%-- <div style="border: 1px solid  #5588BB; padding: 2px; background-color: #5588BB;
                        color: #FFFFFF; text-align: center;">
                        <table cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td align="center">
                                    <b>Job Change Request</b>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <div style="border: 1px solid gray; padding: 2px;">
                        <table cellpadding="0" cellspacing="4" width="100%" style="color: Black; border-color: #0000FF">
                            <tr>
                                <td align="right" style="width: 10%">
                                    Fleet
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 12%">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        Font-Size="11px" Height="20px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                        Width="154px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 12%">
                                    System
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="right" style="width: 12%">
                                    <asp:DropDownList ID="ddlSystem" runat="server" AutoPostBack="True" Font-Size="11px"
                                        Height="20px" Width="154px" OnSelectedIndexChanged="ddlSystem_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 6%">
                                    Department
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="right" style="width: 5%">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" Font-Size="11px"
                                        Height="20px" Width="154px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td colspan="2" align="right" style="width: 17%; white-space: nowrap;">
                                    Request date from
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="right" style="width: 4%">
                                    <asp:TextBox ID="txtFromDate" runat="server" EnableViewState="true" Font-Size="11px"
                                        Width="140px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 2%">
                                    Vessel
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Font-Size="11px"
                                        AutoPostBack="true" Height="20px" Width="154px" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Sub System
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="right" style="width: 12%">
                                    <asp:DropDownList ID="ddlSubSystem" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        Font-Size="11px" Height="20px" Width="154px" OnSelectedIndexChanged="ddlSubSystem_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 25%">
                                    Rank
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="right">
                                    <asp:DropDownList ID="ddlRank" runat="server" AppendDataBoundItems="True" Font-Size="11px"
                                        Height="20px" Width="154px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td colspan="2" align="right" style="width: 17%">
                                    Request date to
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" EnableViewState="true" Font-Size="11px"
                                        Width="140px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Search By
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearch" runat="server" Style="width: 150px"></asp:TextBox>
                                </td>
                                <td align="center" colspan="4">
                                    <asp:RadioButtonList ID="optStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="ALL" Value="ALL"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Approve" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td style="float: left;">
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"  OnClientClick="return comparedate();"
                                        ImageUrl="~/Images/SearchButton.png" />&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnClearFilter_Click" ToolTip="Refresh"
                                        ImageUrl="~/Images/Refresh-icon.png" />&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to excel" OnClick="btnExport_Click"
                                        ImageUrl="~/Images/Exptoexcel.png" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="border: 1px solid #cccccc; margin-top: 2px">
                <asp:UpdatePanel ID="UpdPnlGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <asp:GridView ID="gvJobChangeRqst" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" OnRowDataBound="gvJobChangeRqst_RowDataBound" Width="100%"
                                CellPadding="3" CellSpacing="3" GridLines="Both" AllowSorting="true" OnSorting="gvJobChangeRqst_Sorting"
                                DataKeyNames="ID">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Change Reqst ID">
                                        <HeaderTemplate>
                                            Request For
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("RequestFor") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%#  Eval("Vessel_Name") %>'></asp:Label>
                                            <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# Eval("Vessel_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Machinery">
                                        <HeaderTemplate>
                                            System
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSystemName" runat="server" Text='<%# Eval("System_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="System">
                                        <HeaderTemplate>
                                            Sub System
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubSystemName" runat="server" Text='<%# Eval("Subsystem_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Code">
                                        <HeaderTemplate>
                                            Job Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobID" Visible="true" runat="server" Text='<%# Eval("JOB_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JobTitle">
                                        <HeaderTemplate>
                                            Job Title
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCRJobTitile" runat="server" Text='<%# Eval("CR_JOB_TITLE") %>'></asp:Label>
                                            <asp:Label ID="lblCRJobDesc" Visible="false" runat="server" Text='<%# Eval("CR_JOB_DESCRIPTION") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FREQUENCY">
                                        <HeaderTemplate>
                                            Freq.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrFrequency" runat="server" Text='<%# Eval("CR_FREQUENCY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frequency_Name">
                                        <HeaderTemplate>
                                            Freq. Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrFrequencyName" runat="server" Text='<%# Eval("CR_Frequency_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <HeaderTemplate>
                                            Department
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrDepartment" runat="server" Text='<%# Eval("CR_Department") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank">
                                        <HeaderTemplate>
                                            Rank
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrRank" runat="server" Text='<%# Eval("CR_RankName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sets">
                                        <HeaderTemplate>
                                            CMS
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrCMS" runat="server" Text='<%# Eval("CR_CMS").ToString() == "1" ? "Y" : "N"  %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sets">
                                        <HeaderTemplate>
                                            CRITICAL
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrCritical" runat="server" Text='<%# Eval("CR_CRITICAL").ToString() =="1" ? "Y" : "N" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actioned">
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblActioned" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Change Reqst ID">
                                        <HeaderTemplate>
                                            Request Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("CR_Created_On", "{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actioned Date">
                                        <HeaderTemplate>
                                            Actioned On
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionedDate" runat="server" Text='<%# Eval("CR_Actioned_On","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgApprove" runat="server" Height="14px" Visible="<%# objUA.Add ==1 ? true : false %>"
                                                            ForeColor="Black" ToolTip="Approve/Reject Change Request" ImageUrl="~/Purchase/Image/ApproveRequest.png" />
                                                    </td>
                                                    <td style="border-color: transparent; width: 10px">
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindGrid" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
