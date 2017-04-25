<%@ Page Title="Company KPIs" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="KPI_Vetting_Company.aspx.cs"
    Inherits="KPI_Vetting_Company" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/JTree/jquery.treeview.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <link href="../../jqxWidgets/Controls/styles/jqx.base.css" rel="stylesheet" type="text/css" />
    <script src="../../jqxWidgets/scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxcore.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdata.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdraw.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxchart.core.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxrangeselector.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxchart.rangeselector.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdata.export.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxbuttons.js"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <style type="text/css">

        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        .ListTitle
        {
            width: 99%;
        }
        .page
        {
            width: 100%;
        }
    </style>
    <style type="text/css">
        .buttonnew
        {
            background: url(../../Images/graph.png) no-repeat;
            cursor: pointer;
            border: none;
            width: 118px;
            height: 29px;
        }
        img
        {
            max-width: 100%;
            max-height: 100%;
        }
        .SPI
        {
            vertical-align: top;
            text-align: center;
        }
        .KPIheader
        {
            text-align: center;
            background-color: #D1DBE1;
            font-weight: bold;
        }
        .KPItext
        {
            font-weight: bold;
        }
        .SPItext
        {
            font-weight: bold;
            text-align: right;
        }
        .aStyle
        {
            color: Blue !important;
            text-decoration: none;
        }
        .Desc
        {
            font-size: x-small;
            cursor: pointer;
        }
        .KPI
        {
            text-align: center;
        }
        .stylw2
        {
            font-size: large;
            font-weight: bold;
            text-align: center;
            color: #3366CC;
        }
        .SelectedStyle
        {
            background-color: White;
        }
        .tfTable {margin:0px; padding:0px;}
        .tfTable table{width:100%; border:0px; }
        .tfTable table tr th{background:#dedede; border-right:1px solid #acacac; padding:5px; height:16px;}
        .tfTable table tr th:last-child{ border-right:none}
        .tfTable table tr:nth-of-type(even){background:#fff}
        .tfTable table tr td{padding:5px; height:16px;}
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            searchInitialize();


        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }


        function searchInitialize() {
            var kpiId = document.getElementById("ctl00_MainContent_hdnKpi_ID").value;
            var kpiname = document.getElementById("ctl00_MainContent_hdnKpi_Name").value;
            var Qtr = document.getElementById("ctl00_MainContent_hdnQtr").value;
            showChart(Qtr);
            showChart2(kpiname, kpiId);
            showAllKpiChart();

        }   



        function showAllKpiChart()
        {

               var startdate = convertDate(document.getElementById("ctl00_MainContent_hiddenStartDate").value);
               var enddate = convertDate(document.getElementById("ctl00_MainContent_hiddenEndDate").value);
               var Container = "#ctl00_MainContent_chartContainer_";
               var KPIID = document.getElementById("ctl00_MainContent_hdnKPI_IDList").value;

            var crow = document.getElementById("ctl00_MainContent_hiddenCount").value;
            var ccol = document.getElementById("ctl00_MainContent_hiddenCount1").value;
            var hdnKPIIDArray = KPIID.split(',');
            var KPICount = hdnKPIIDArray.length;
debugger;
        for (var x = 0; x < crow; x++) {
            for (var y = 0; y < ccol; y++) {


                control = Container + x + "_" + y;

                var ConKPIId = "#ctl00_MainContent_hdnID_" + x + y;
                var ConKPIName = "#ctl00_MainContent_hdnName_" + x + y;
//            for (var x = 1; x < KPICount; x++) {
//          
//                control = Container + x;

//                var ConKPIId = "#ctl00_MainContent_hdnID_" + x;
//                var ConKPIName = "#ctl00_MainContent_hdnName_" + x;

                KPIId = $(ConKPIId).val();
                KPIName = $(ConKPIName).val();

                KPIName = 'KPI-' + KPIName;

                var source =
            {
                datatype: "json",
                 datafields: [
                  { name: 'Qtr' },
                    { name: 'VALUE' }
                    
                ],
  
                url: "../../KPIService.svc/Vetting_KPI_ByCompany/KID/" + KPIId + "/Startdate/" + startdate + "/EndDate/" + enddate
   
              
            };
        
    

            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });


           // prepare jqxChart settings
            var settings = {
                title:  "KPI::" + KPIName ,
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 30, top: 5, right: 30, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                    xAxis:
                    {
                        dataField: 'Qtr',
                        valuesOnTicks: true,
                        gridLines: { visible: false },

                    
                    rangeSelector: {
                        padding: { top: 10, bottom: 0 },
                        backgroundColor: 'white',
                        dataField: 'Qtr',
                        size: 80,
                        serieType: 'area',
                        showGridLines: false

                    }


                },
                valueAxis:
                {

                    labels: { horizontalAlignment: 'right' }

                },

                colorScheme: 'scheme01',
                seriesGroups:
                    [
                        {
                            type: 'line',
                             series: [
                                     { dataField: 'VALUE', displayText: 'KPI Value' }
                                   ]

                        }
                         

                    ]
            };

            $(control).jqxChart(settings);


            }

        }
        }



        function showChart2(kpiname, kpiId) {
        
             asyncLoadPIList(kpiId);
               var startdate = convertDate(document.getElementById("ctl00_MainContent_hiddenStartDate").value);
               var enddate = convertDate(document.getElementById("ctl00_MainContent_hiddenEndDate").value);
              
              
        var source =
            {
                datatype: "json",
                 datafields: [
                  { name: 'Qtr' },
                    { name: 'VALUE' }
                    
                ],
  
                url: "../../KPIService.svc/Vetting_KPI_ByCompany/KID/" + kpiId + "/Startdate/" + startdate + "/EndDate/" + enddate
   
              
            };
        
    

            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });

           // prepare jqxChart settings
            var settings = {
                title:  "KPI::" + kpiname ,
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 30, top: 5, right: 30, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                    xAxis:
                    {
                        dataField: 'Qtr',
                        valuesOnTicks: true,
                        gridLines: { visible: false },

                    
                    rangeSelector: {
                        padding: { top: 10, bottom: 0 },
                        backgroundColor: 'white',
                        dataField: 'Qtr',
                        size: 80,
                        serieType: 'area',
                        showGridLines: false

                    },


                },
                valueAxis:
                {

                    labels: { horizontalAlignment: 'right' }

                },

                colorScheme: 'scheme01',
                seriesGroups:
                    [
                        {
                            type: 'line',
                             series: [
                                     { dataField: 'VALUE', displayText: 'KPI Value' }
                                   ]

                        }
                         

                    ]
            };

            $('#chartContainer2').jqxChart(settings);

            return false;
        }


        ////////

        function convertDate(inputFormat) {
            var ds = inputFormat.split('-');
            return [ds[2], ds[1], ds[0]].join('-');
        }


        var Qtr='Q1-2017';

        function showChart(Quarter) {

            var Vetting_Object = {
                    "Qtr": ""
   
               }

            Vetting_Object.Qtr=Quarter;
            Qtr=Quarter;

            var sendurl = "../../KPIService.svc/GetMultipleKPIVettingCompany";
            $.ajax({
                type: "POST",
                url: sendurl,
                data: JSON.stringify(Vetting_Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showChart_OnSuccess

            });


        }


        function showChart_OnSuccess(data, status, jqXHR) {


            var resultArray = [];




            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);

            var source =
                     {
                         datatype: "json",
                         datafields: [
                           { name: 'KPI_Name' },
                             { name: 'VALUE' },
                             { name: 'GOAL' }
                         ],
                         localdata: objData
                        
                     };




            var dataAdapter = new $.jqx.dataAdapter(source);
            var toolTipCustomFormatFn = function (value, itemIndex, serie, group, xAxisValue, xAxis) {

                return 'KPI_Name:' + xAxisValue + "&nbsp;</br>" + serie.displayText + ':' + Number(value).toFixed(2).replace(",", "") + '&nbsp;';
            }; 
       

            // prepare jqxChart settings
            var settings = {    
                title: Qtr,
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 5, top: 5, right: 11, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                xAxis:
                {
                    dataField: 'KPI_Name',
                    valuesOnTicks: false,
                    labels:
                    {
                        angle: -45
               
                    }
                }
            ,

                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups:
                    [
                        {
                            type: 'column',
                            toolTipFormatFunction: toolTipCustomFormatFn,
                            valueAxis:
                            {
                                visible: true
                                
                            },


                            series: [
                                    { dataField: 'VALUE', displayText: 'KPI Value' },
                                    { dataField: 'GOAL', displayText: 'GOAL' }
                                ]
                        },


                    ]
            };

            // setup the chart


            $('#chartContainer').jqxChart(settings);

        }





        //Method to validate start/end date
        function validateDate() {
            var msg = "";
            var strDateFormat = "<%= UDFLib.GetDateFormat() %>";
            var TodayDateFormat = '<%= UDFLib.DateFormatMessage() %>';
            var startDateVal = document.getElementById("ctl00_MainContent_txtStartDate").value;
            var endDateVal = document.getElementById("ctl00_MainContent_txtEndDate").value;

            if (startDateVal != null) {
                if (startDateVal == "")
                    msg += "Enter Start Date\n";
                else {
                    if (IsInvalidDate(startDateVal, strDateFormat))
                        msg += "Enter valid Start Date" + TodayDateFormat + "\n";
                }
            }
            if (endDateVal != null) {
                if (endDateVal == "")
                    msg += "Enter End Date\n";
                else {
                    if (IsInvalidDate(endDateVal, strDateFormat))
                        msg += "Enter valid End Date" + TodayDateFormat + "\n";
                }
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            return true;
        }



function asyncLoadPIList(KPI_Id) {

    try {
             
   
        $.ajax({
        type: "GET",
        url: "../../KPIService.svc/Get_PI_ListByKPI/KPI_ID/" + KPI_Id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(Object[0]),
        success: function (data) {

            $('#dvtable').html(data.Get_PI_ListByKPIResult);   
        }
    });
            
              
     
    }
    catch (ex) {

        alert(ex);
    }
}
        




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:HiddenField ID="hdnVessel_IDs" runat="server" />
    <div class="page-title">
        Company KPIs
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div>

                    <table width="100%" cellpadding="2" cellspacing="1" border="0">
                        <tr>
                            <%--                <td align="right" valign="middle" width="8%">
                       KPI Name :
                    </td>
                    <td align="left" valign="middle" width="12%">
                     <asp:DropDownList ID="ddlKPIList" runat="server" ></asp:DropDownList>
                          
                    </td>--%>
                            <td align="right" valign="middle" width="8%">
                                Start Date :
                            </td>
                            <td valign="middle" align="left" width="10%">
                                <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:TextBox ID="txtStartDate" runat="server" Width="90%" onkeypress="return false;"
                                    CssClass="txtInput"></asp:TextBox>
                            </td>
                            <td align="right" valign="middle" width="8%">
                                End Date :
                            </td>
                            <td valign="middle" align="left" width="10%">
                                <asp:TextBox ID="txtEndDate" runat="server" Width="90%" onkeypress="return false;"
                                    CssClass="txtInput"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td align="left" valign="middle" width="300px">
                                <table width="298px">
                                    <tr>
                                        <td width="40%" valign="middle">
                                            <asp:ImageButton ID="btnFilter" runat="server" ToolTip="Search" ValidationGroup="ValidateSearch"
                                                ImageUrl="~/Images/SearchButton.png" OnClientClick="return validateDate();" OnClick="btnFilter_Click" />
                                            &nbsp;<asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click"
                                                ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png" />
                                                 &nbsp;
                              <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to Excel" 
                                ImageUrl="~/Images/Excel-icon.png"  onclick="btnExport_Click" />&nbsp;
                                        </td>
                                        <td width="12%" valign="middle">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" valign="top">
                                <div id="dvGrid" style="overflow: auto; float: left; display:block; margin-right:3%;width:72%" Class="tfTable">
                                    <asp:GridView ID="gvCompanyKPI" runat="server" AutoGenerateColumns="true" OnRowDataBound="gvCompanyKPI_RowDataBound" GridLines="None"
                                        Width="100%" >
                                        <RowStyle />
                                        <HeaderStyle  />
                                        <PagerStyle />
                                        <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="KPI Name">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdKPIID" Value='<%# Bind("KPI_ID")%>' runat="server" />
                                                    <asp:HiddenField ID="hdnKpiName" Value='<%# Bind("KPI_Name")%>' runat="server" />
                                                    <asp:LinkButton ID="Item_Name" EnableViewState="true" runat="server" CssClass="chkSelected"
                                                        OnClientClick='<%#"showChart2( (&#39;" + Eval("[KPI_Name]") +"&#39;),(&#39;"+ Eval("[KPI_ID]") + "&#39;));return false;"%>'
                                                        Text='<%#Eval("KPI_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hiddenStartDate" runat="server" />
                                <asp:HiddenField ID="hiddenEndDate" runat="server" />
                                 <asp:HiddenField ID="hdnKpi_ID" runat="server" />
                                  <asp:HiddenField ID="hdnKpi_Name" runat="server" />
                                  <asp:HiddenField ID="hdnKPI_IDList" runat="server" />
                                  <asp:HiddenField ID="hdnKPI_NameList" runat="server" />
                                   <asp:HiddenField ID="hdnQtr" runat="server" />
                                  <asp:HiddenField ID="hiddenCount" runat="server" />
                                   <asp:HiddenField ID="hiddenCount1" runat="server" />
                                <div style="float: left; display:block; width:25%;">
                                   
                                    <div id="dvtable"></div>
                                     <%--  <asp:Literal ID="ltPilist" runat="server"></asp:Literal>
                                 <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"
                                        GridLines="Both">
                                        <ItemTemplate>
                                            <table border="0" cellpadding="5">
                                                <tr>
                                                    <td style="border-right-style: solid; border-right-width: thin;" width="80px">
                                                        <asp:HyperLink ID="hlPI" runat="server" Text='<%# Eval("value") %>' NavigateUrl='<%# "../PI/PI_Details.aspx?PI_ID=" + Eval("PID")%>'
                                                            Target="_blank"></asp:HyperLink>
                                                    </td>
                                                    <td width="300px">
                                                        <asp:Label ID="lblPIText" runat="server" Text='<%# Eval("PIName") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList><br />
                                    <asp:Label ID="lblFormula" runat="server" Font-Bold="true" BackColor="AntiqueWhite"></asp:Label>--%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height:20px;"></td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div id="divChart" runat="server" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="center" style="width: 51%">
                                                
                                                <div id='chartContainer' style="width: 100%; height: 400px; float: left;">
                                                </div>
                                            </td>
                                            <td align="center" style="width: 49%">
                                                <center>
                                                    <div >
                                                        <asp:Label ID="lblVessel" runat="server" Style="width: 23px; color: Blue; font-weight: bold"
                                                            Font-Bold="True"></asp:Label>
                                                    </div>
                                                </center>
                                                <div id='chartContainer2' style="width: 99%; height: 400px; float: left;">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                        
                                        <td colspan="2" width="95%" >
                                       <div class="page-title ListTitle">
                                                   KPI List
                                                </div>
                                        
                                        </td>
                                        </tr>

                                               <tr>
                                            <td width="100%" colspan="2">
                                                
                                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                               
                                            </td>
                                        </tr>


                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>

        </div>
    </div>
</asp:Content>
