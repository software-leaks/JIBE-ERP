<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PortInfoReportIndex.aspx.cs" Inherits="Operations_PortInfoReportIndex"  Title="Port and Terminal Information Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showdetails(querystring) {
            var query = new Array();
            query = querystring.toString().split(',');
            window.open("PortInfoReportDetail.aspx?PortInfoReportId=" + query[0] + "&VesselId=" + query[1]+"&OfficeId=" + query[2] );
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
          Port Information Report
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
                                            <asp:DropDownList ID="DDLPortFilter" CssClass="dropdown-css" Width="150px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Vessel:
                                        </td>
                                        <td align="left" >
                                            <asp:DropDownList ID="ddlvessel" CssClass="dropdown-css" Width="150px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            Country:
                                        </td>
                                        <td align="left" colspan="3">
                                         <asp:DropDownList ID="ddlCountry" runat="server" Width="156px" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left">
                                            Terminal:
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtTerminal"  runat="server" Width="156px" ></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            Search:
                                        </td>
                                        <td align="left" colspan="3">
                                          <asp:TextBox ID="txtSearch"  runat="server" Width="156px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                             <td>
                                <table style="width: 811px">
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
                                            <asp:ImageButton ID="btnExport" ImageUrl="~/Images/Exptoexcel.png" ToolTip="Export To Excel"  Height="25px" OnClick="ExptoExcel_Click"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div id="innerData">
                        <asp:GridView  runat="server" ID= "gvPortInfoReportIndex" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
                            AllowPaging="false" CellPadding="4" AllowSorting="True"  OnRowDataBound="gvPortInfoReportIndex_RowDataBound" 
                            GridLines="Horizontal" >
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                 <PagerSettings Mode="NumericFirstLast" />                        
                                <PagerStyle CssClass="PagerStyle-css" />
                                <PagerStyle Font-Size="Large" CssClass="pager" />
                            <Columns>
                                <asp:BoundField DataField="PKID" HeaderText="PKID" Visible="false" />
                                <asp:BoundField DataField="Vessel_Id" HeaderText="Vessel_Id" Visible="false" />
                                <asp:BoundField DataField="Port_Name" HeaderText="Port" />
                                <asp:BoundField DataField="Country_Name" HeaderText="Country" />
                                <asp:BoundField DataField="TerminalName" HeaderText="Terminal" />
                                <asp:BoundField DataField="Berth" HeaderText="Berth" />
                                <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                                <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                                <asp:BoundField DataField="VesselName" HeaderText="VesselName" />
                                <asp:BoundField DataField="Departure_Date" HeaderText="Departure Date" />
                            </Columns>
                         </asp:GridView>
                         <auc:CustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindItems" />
                    </div>
     </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>