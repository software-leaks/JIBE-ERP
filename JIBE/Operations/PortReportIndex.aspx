<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PortReportIndex.aspx.cs"
    Inherits="Operations_PortReportIndex" Title="Port Report Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showdetails(querystring) {
            var query = new Array();

            query = querystring.toString().split('~');

            window.open("PortReport.aspx?id=" + query[0] + "&VesselId=" + query[1]);


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
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>             
                <div id="page-content" style="border: 1px solid gray;">
                    <table cellpadding="0" cellspacing="0" width="100%">
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
                                <table>
                                    <tr>
                                        <td align="left">
                                            &nbsp;&nbsp; From Date
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtfrom" AutoPostBack="true" CssClass="textbox-css" runat="server"
                                                OnTextChanged="txtfrom_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            &nbsp; &nbsp; To Date:
                                        </td>
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
                    
 
                                        <td>
                                            <asp:Button ID="btnclearall" Height="25px" runat="server" Text="Clear All Filter"
                                                OnClick="btnclearall_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton1" src="../Images/printer.gif" Height="25px" OnClientClick="btnprint_onclick()"
                                                runat="server" AlternateText="Print" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:UpdatePanel ID="updExport" runat="server" RenderMode="Inline">
                                                <ContentTemplate>
                                                 <%--   <asp:ImageButton ID="btnExport" ImageUrl="~/SEP/Images/XLS.jpg" Height="25px" OnClick="btnView_Click"
                                                        runat="server" />--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        
                                    
                            </td>
                        </tr>
                    </table>
                    <div id="innerData">
                        <asp:GridView ID="gvPortReport" runat="server" EmptyDataText="No record found !" Width="100%"
                            AutoGenerateColumns="False" OnRowDataBound="gvPortReport_RowDataBound" 
                            AllowPaging="false" CellPadding="4" AllowSorting="True" OnSorted="gvPortReport_Sorted"
                            OnSorting="gvPortReport_Sorting" GridLines="Horizontal">
                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                             <PagerSettings Mode="NumericFirstLast" />                        
                            <PagerStyle CssClass="PagerStyle-css" />
                            <PagerStyle Font-Size="Large" CssClass="pager" />
                            <Columns>
                                <asp:BoundField DataField="PortReportID" HeaderText="PortReportID" Visible="false" />
                                <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel" HeaderStyle-Width="90px"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="Vessel_Name">
                                    <HeaderStyle Width="120" ForeColor="Blue" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-Width="100px" dataformatstring="{0:dd/MM/yyyy}"
                                    SortExpression="Date">
                                    <HeaderStyle Width="120" ForeColor="Blue" />
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="VOYAGE_NO" HeaderText="Voyage" 
                                >
                                   <HeaderStyle Width="120px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="PORT_NAME" HeaderText="Port Name" HeaderStyle-Width="90px"    SortExpression="PORT_NAME">
                                    <HeaderStyle Width="120px" ForeColor="Blue"/>
                                </asp:BoundField>
                            
                                <asp:BoundField DataField="COMP_LOAD_DIS" HeaderText="COMP LOAD/DIS" HeaderStyle-Width="100px" dataformatstring="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>    
                                <asp:BoundField DataField="COMM_LOAD_DIS" HeaderText="COMM LOAD/DIS" HeaderStyle-Width="100px" dataformatstring="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="ETD" HeaderText="ETD" HeaderStyle-Width="100px" dataformatstring="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="120px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="Vessel_ID" Visible="false" />
                                
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
