<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Operation_Default" Title="Voyages Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showdetails(querystring) {
            var query = new Array();

            query = querystring.toString().split(',');

            window.open("VoyageReportDetails.aspx?id=" + query[0] + "&ReportType=" + query[1] + "&filters=" + query[2]); 

           
        }

        function btnprint_onclick() {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById('innerData').innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
    <style type="text/css">
        .PurpleFinder-css
        {
            background-color: #D0AAF3;
        }
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        .RedCell
        {
            font-weight:bold;
            color:White;
            background-color:Red;
        }
        .style1
        {
            width: 102px;
        }
    </style>
</asp:Content>
<asp:Content ID="contentmain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
          Daily Noon/Arrival/Departure Report
    </div>
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>             
                <div id="page-content" style="border: 1px solid gray;">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Fleet :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDLFleet" runat="server" CssClass="dropdown-css" Width="153px"
                                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            Location:
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddllocation" CssClass="dropdown-css" Width="150px" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Vessel:
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:DropDownList ID="ddlvessel" CssClass="dropdown-css" Width="150px" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="width: 188px">
                                    <tr>
                                        <td align="left" class="style1">From Date</td>
                                        <td align="left">
                                            <asp:TextBox ID="txtfrom" AutoPostBack="true" CssClass="textbox-css" runat="server"
                                                OnTextChanged="txtfrom_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style1">To Date:</td>
                                        <td align="left">
                                            <asp:TextBox ID="txtto" AutoPostBack="true" CssClass="textbox-css" runat="server"
                                                OnTextChanged="txtto_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calto" Format="dd-MM-yyyy" TargetControlID="txtto" runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="width: 590px">
                                    <tr>
                                        <td style="height: 15px; text-align: left">
                                            Report Type
                                        </td>
                                        <td align="right">
                                            <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updExport" runat="server">
                                                <ProgressTemplate>
                                                    <div id="divProgressreport" style="z-index: 2; color: Red">
                                                        Please wait...generating report file..
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnarrival" Width="70px" runat="server" Height="25px" Text="Arrival"
                                                CommandArgument="A" OnClick="reportType_Click" />
                                            <asp:Button ID="btndeparture" Width="75px" runat="server" Height="25px" Text="Departure"
                                                CommandArgument="D" OnClick="reportType_Click" />
                                            <asp:Button ID="btnnoon" Width="70px" runat="server" Text="Noon" Height="25px" CommandArgument="N"
                                                OnClick="reportType_Click" />
                                            <asp:Button ID="btnpurple" Width="80px" runat="server" Text="PurpleFinder" Height="25px"
                                                Font-Size="11px" CommandArgument="P" OnClick="reportType_Click" />
                                            <asp:Button ID="btnall" Width="50px" runat="server" Text="All" Height="25px" CommandArgument="NDA"
                                                OnClick="reportType_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnclearall" Height="25px" runat="server" Text="Clear All Filter"
                                                OnClick="btnclearall_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton1" src="../Images/printer.gif" Height="25px" OnClientClick="btnprint_onclick()"
                                                runat="server" AlternateText="Print"  />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="updExport" runat="server" RenderMode="Inline">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="btnExport" ImageUrl="~/Images/Exptoexcel.png"  Height="25px" OnClick="btnView_Click"
                                                        runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div id="innerData">
                        <asp:GridView ID="gvVoyageReport" runat="server" EmptyDataText="No record found !" Width="100%"
                            AutoGenerateColumns="False" OnRowDataBound="gvVoyageReport_RowDataBound" 
                            AllowPaging="false" CellPadding="4" AllowSorting="True" OnSorted="gvVoyageReport_Sorted"
                            OnSorting="gvVoyageReport_Sorting" GridLines="Horizontal">
                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                             <PagerSettings Mode="NumericFirstLast" />                        
                            <PagerStyle CssClass="PagerStyle-css" />
                            <PagerStyle Font-Size="Large" CssClass="pager" />
                            <Columns>
                                <asp:BoundField DataField="PKID" HeaderText="PKID" Visible="false" />
                                <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel" HeaderStyle-Width="90px"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Vessel_Name">
                                    <HeaderStyle Width="90px" ForeColor="Blue" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="strTELEGRAM_DATE" HeaderText="Date" HeaderStyle-Width="100px"
                                    SortExpression="TELEGRAM_DATE">
                                    <HeaderStyle Width="100px" ForeColor="Blue" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TELEGRAM_TYPE" HeaderText="Type" />
                                <asp:BoundField DataField="VOYAGE" HeaderText="Voyage" />
                                <asp:BoundField DataField="LOCATION_NAME" HeaderText="Location" HeaderStyle-Width="75px"
                                    SortExpression="LOCATION_NAME">
                                    <HeaderStyle Width="75px" ForeColor="Blue" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PORT_NAME" HeaderText="Port Name" HeaderStyle-Width="90px">
                                    <HeaderStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UTC_HR" HeaderText="UTC HR" HeaderStyle-Width="45px">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="WIND_FORCE" HeaderStyle-ForeColor="Blue" HeaderText="Wind"
                                    SortExpression="WIND_FORCE" />
                                <asp:BoundField DataField="INSTRUCTED_SPEED" HeaderText="Instructed Speed" HeaderStyle-Width="45px">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AVERAGE_SPEED" HeaderText="Average Speed" HeaderStyle-Width="45px">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CP_HOCONS" HeaderText="CP HO Consumptn" HeaderStyle-Width="45px">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ACTUAL_HO_CONSMP" HeaderText="Actual HO Consumptn" HeaderStyle-Width="45px">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CP_DOCONS" HeaderText="CP DO Consumptn" HeaderStyle-Width="45px">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ACTUAL_DO_CONSMP" HeaderText="Actual DO Consumptn" HeaderStyle-Width="45px">
                                    <HeaderStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FW_CONS" HeaderText="FW Cons" />
                                <asp:TemplateField HeaderText="Low ROB">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLowrob" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CP_FWCONS" HeaderText="" Visible="false" />
                                <asp:BoundField DataField="Vessel_ID" Visible="false" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                            Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;OPS_Telegrams&#39;,&#39;PKID="+Eval("PKID").ToString()+"&#39;,event,this)" %>'
                                            AlternateText="info" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                          <auc:CustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindItems" />
                    </div>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSourcereport" TypeName="SMS.Business.Operation.BLL_OPS_VoyageReports"
        SelectMethod="Get_VoyageReportIndex" runat="server" OnSelecting="ObjectDataSourcereport_Selecting"
        OnSelected="ObjectDataSourcereport_Selected">
        <SelectParameters>
            <asp:Parameter Name="reporttype" DefaultValue="NDA" />
            <asp:Parameter Name="vesselid" DefaultValue="0" />
            <asp:Parameter Name="fleetid" DefaultValue="0" />
            <asp:Parameter Name="locationcode" DefaultValue="0" />
            <asp:Parameter Name="fromdate" DefaultValue="1900/01/01" />
            <asp:Parameter Name="todate" DefaultValue="1900/01/01" />
            <asp:Parameter Name="Page_Index" DefaultValue="0" />
            <asp:Parameter Name="Page_Size" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
