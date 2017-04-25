<%@ Page Language="C#" Title="Machinary Change Request" AutoEventWireup="true" CodeFile="PMSMachineryChangeRequest.aspx.cs"
    Inherits="PMSMachineryChangeRequest" EnableEventValidation="false" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
<%@ Register Src="../../UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList"
    TagPrefix="ucFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>PMS Machinery Change Request </title>
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


            var url = 'PMSMachineryChangeRequestAction.aspx?VESSELID=' + Vessel_ID + '&Change_Reqst_ID=' + ID + '&Status=' + Status
            var ButtonUniqueID = "<%= btnFilter.UniqueID %>";
            OpenPopupWindowBtnID('MachineryChangeRequest', 'Machinery Change Request', url, 'popup', 550, 1225, null, null, false, false, true, null, ButtonUniqueID);
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
            <asp:UpdatePanel ID="UpdPnlFilter" runat="server">
                <ContentTemplate>
                    <div class="page-title">
                        Machinery Change Request
                    </div>
                    <div style="border: 1px solid #cccccc;">
                        <table cellpadding="1" cellspacing="1" width="100%" style="color: Black; border-color: #0000FF">
                            <tr>
                                <td style="width: 7%" align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td style="width: 3%" align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        Font-Size="11px" Height="20px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                        Width="120px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%" align="right">
                                    System Name :&nbsp;&nbsp;
                                </td>
                                <td style="width: 5%">
                                    <asp:TextBox ID="txtSearchSystem" Style="width: 300px" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    Request date from :&nbsp;&nbsp;
                                </td>
                                <td style="width: 7%">
                                    <asp:TextBox ID="txtFromDate" Font-Size="11px" Width="140px" EnableViewState="true"
                                        runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="caltxtFromDate" Format="dd-MM-yyyy" TargetControlID="txtFromDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
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
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vessel :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Width="120px"
                                        Height="20px" Font-Size="11px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Status :&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="optStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right">
                                    Request date to :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" Font-Size="11px" Width="140px" EnableViewState="true"
                                        runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="caltxtToDate" Format="dd-MM-yyyy" TargetControlID="txtToDate"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/SearchButton.png"
                                        OnClientClick="return comparedate();" OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                        OnClick="btnClearFilter_Click" ToolTip="Refresh" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Exptoexcel.png"
                                        OnClick="btnExport_Click" ToolTip="Export to excel" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="border: 1px solid #cccccc; padding: 2px">
                <asp:UpdatePanel ID="UpdPnlGrid" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:GridView ID="gvMachinery" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="gvMachinery_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                CellPadding="3" CellSpacing="3" OnSorting="gvMachinery_Sorting" DataKeyNames="ID">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="RequestFor">
                                        <HeaderTemplate>
                                            Request For
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("RequestFor") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("Vessel_Name")%>
                                            <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="System_Description">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSystemNameHeader" runat="server" ForeColor="Black" CommandArgument="System_Description"
                                                CommandName="Sort">Machinery&nbsp;</asp:LinkButton>
                                            <img id="System_Description" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSystemName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.System_Description") %>'></asp:Label>
                                            <asp:Label ID="lblSystemParticular" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.System_Particulars") %>'></asp:Label>
                                            <asp:Label ID="lblDiffSysDescFlag" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DiffSysDescFlag") %>'></asp:Label>
                                            <asp:Label ID="lblDiffSysPartFlag" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DiffSysPartFlag") %>'></asp:Label>
                                            <asp:Label ID="lblDiffSetInstalledFlag" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DiffSetInstalledFlag") %>'></asp:Label>
                                            <asp:Label ID="lblDiffModelFlag" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DiffModelFlag") %>'></asp:Label>
                                            <asp:Label ID="lblAddNewFlag" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AddNewFlag") %>'></asp:Label>
                                            <asp:Label ID="lbl_CR_Actual" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CR_Actual") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maker">
                                        <HeaderTemplate>
                                            Maker
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaker" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Maker") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sets">
                                        <HeaderTemplate>
                                            Sets
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSetsInstalled" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Set_Installed") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model">
                                        <HeaderTemplate>
                                            Model
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="System_Description">
                                        <HeaderTemplate>
                                            Machinery
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCRSystemName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CR_System_Description") %>'></asp:Label>
                                            <asp:Label ID="lblCRSystemParticular" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CR_System_Particulars") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maker">
                                        <HeaderTemplate>
                                            Maker
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCRMaker" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CR_Maker") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sets">
                                        <HeaderTemplate>
                                            Sets
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCRSetsInstalled" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CR_Set_Installed") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model">
                                        <HeaderTemplate>
                                            Model
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCRModel" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CR_Model") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actioned">
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblActioned" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actioned By">
                                        <HeaderTemplate>
                                            Actioned by
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionedBy" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ACTIONEDBY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request_Date">
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
                                            <asp:Label ID="lblActionedDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CR_Actioned_On","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgApprove" runat="server" Height="15px" ForeColor="Black" Visible="<%# objUA.Add ==1 ? true : false %>"
                                                ToolTip="Approve/Reject Change Request" ImageUrl="~/Purchase/Image/ApproveRequest.png" />
                                        </ItemTemplate>
                                        <%--  <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css" />
                                        <HeaderStyle  Wrap="true" HorizontalAlign="Center"  CssClass="PMSGridHeaderStyle-css" />--%>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindMachineryChageRequests" />
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
