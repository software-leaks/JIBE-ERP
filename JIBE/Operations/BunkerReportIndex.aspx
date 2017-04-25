<%@ Page Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="BunkerReportIndex.aspx.cs" Inherits="Operations_BunkerReportIndex" Title="Bunker Report List" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showdetails(querystring) {
            debugger;
            var query = new Array();

            query = querystring.toString().split(',');

            window.open("BunkerReport.aspx?BunkerReportId=" + query[0] + "&VesselId=" + query[1] + "&filters=" + query[2]); 


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
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        .style1
        {
            width: 102px;
        }
    </style>
</asp:Content>
<asp:Content ID="contentmain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
          Bunker Report
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
    <Triggers >
    <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
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
                                            Port:
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDLPortFilter" CssClass="dropdown-css" Width="150px" runat="server"
                                               >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Vessel:
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:DropDownList ID="ddlvessel" CssClass="dropdown-css" Width="150px" runat="server"
                                                 >
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
                                            <asp:TextBox ID="txtfrom" CssClass="textbox-css" runat="server"
                                                ></asp:TextBox>
                                            <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style1">To Date:</td>
                                        <td align="left">
                                            <asp:TextBox ID="txtto" CssClass="textbox-css" runat="server"
                                               ></asp:TextBox>
                                            <cc1:CalendarExtender ID="calto" Format="dd-MM-yyyy" TargetControlID="txtto" runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="width: 590px">
                                    <tr>                                        
                                        <td align="left">
                                            <asp:Button ID="btnSearch" Height="25px" ToolTip="Search" runat="server" Text="Search"
                                                OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"   >
                                            <asp:Button ID="btnclearall" Height="25px" ToolTip="Clear All Filter" runat="server" Text="Clear All Filter"
                                                OnClick="btnclearall_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td  align="right">
                                            <asp:ImageButton ID="ImageButton1" title="*Print*" src="../Images/printer.gif" Height="25px" OnClientClick="btnprint_onclick()"
                                                runat="server" AlternateText="Print"  />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            
                                                    <asp:ImageButton ID="btnExport" ImageUrl="~/Images/Exptoexcel.png" ToolTip="Export To Excel"  Height="25px" OnClick="btnView_Click"
                                                        runat="server" />
                                                
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div id="innerData">
                        <asp:GridView ID="gvBunkerReport" runat="server" EmptyDataText="No record found !" Width="100%"
                            AutoGenerateColumns="False"
                            AllowPaging="false" CellPadding="4" AllowSorting="True"
                            OnSorting="gvBunkerReport_Sorting" GridLines="Horizontal"  OnSelectedIndexChanging="gvBunkerReport_SelectedIndexChanging">
                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                             <PagerSettings Mode="NumericFirstLast" />                        
                            <PagerStyle CssClass="PagerStyle-css" />
                            <PagerStyle Font-Size="Large" CssClass="pager" />
                            <Columns>
                                <asp:BoundField DataField="BUNKER_REPORT_ID" HeaderText="BUNKER_REPORT_ID" Visible="false" />

                                 <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                         </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:Label ID="lblBunkerReportId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.BUNKER_REPORT_ID") %>'></asp:Label>
                                             <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                            <asp:LinkButton ID="lbnVesselName" CommandName="Select" ForeColor="Black" runat="server"
                                                Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                <asp:BoundField DataField="REPORT_DATE" HeaderText="Report Date" HeaderStyle-Width="100px"
                                    SortExpression="DATE_TIME">
                                    <HeaderStyle Width="100px" ForeColor="Blue" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VOYAGE" HeaderText="Voyage" />
                                <asp:BoundField DataField="PORT_NAME" HeaderText="Port Name" HeaderStyle-Width="90px">
                                    <HeaderStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="COMMENCED_BUNKERING" HeaderText="Commenced Bunkering" />
                                <asp:BoundField DataField="COMPLETED_BUNKERING" HeaderText="Completed Bunkering" />
                                <asp:BoundField DataField="QUANTITY_BUNKERED_Fuel_Oil_HFO" HeaderText="Quantity Bunkered(HFO)" />
                                <asp:BoundField DataField="QUANTITY_BUNKERED_Fuel_Oil_LSFO" HeaderText="Quantity Bunkered(LSFO)" />

                             
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;OPS_BUNKER_REPORT&#39;,&#39; Bunker_Report_Id=" + Eval("BUNKER_REPORT_ID") + " and VESSEL_ID=" + Eval("Vessel_ID") + "&#39;,event,this)"%>'
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
        SelectMethod="Get_BunkerReportIndex" runat="server" OnSelecting="ObjectDataSourcereport_Selecting"
        OnSelected="ObjectDataSourcereport_Selected">
        <SelectParameters>
            <asp:Parameter Name="vesselid" DefaultValue="0" />
            <asp:Parameter Name="fleetid" DefaultValue="0" />
            <asp:Parameter Name="fromdate" DefaultValue="1900/01/01" />
            <asp:Parameter Name="todate" DefaultValue="1900/01/01" />
            <asp:Parameter Name="Page_Index" DefaultValue="0" />
            <asp:Parameter Name="Page_Size" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
