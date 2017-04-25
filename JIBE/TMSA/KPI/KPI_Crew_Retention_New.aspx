<%@ Page Title="Crew Retention" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="KPI_Crew_Retention_New.aspx.cs" Inherits="TMSA_KPI_KPI_Crew_Retention_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--    <%@ Page Title="Crew Retention" Language="C#" MasterPageFile="~/Site_VerticalMenu.master"
    AutoEventWireup="true" CodeFile="KPI_Crew_Retention_New.aspx.cs" Inherits="TMSA_KPI_KPI_Crew_Retention_New" %>--%>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
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
    <%--<script src="../../Scripts/jquery.min.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <%--<link href="../../jqxWidgets/Controls/styles/jqx.base.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../jqxWidgets/Controls/styles/jqx.base_New.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../jqxWidgets/scripts/jquery-2.2.3.min.js" type="text/javascript"></script>--%>
    <script src="../../jqxWidgets/Controls/jqxcore.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdata.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdraw.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxchart.core.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxrangeselector.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxchart.rangeselector.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdata.export.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxbuttons.js"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="../../jqxWidgets/scripts/demos.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxbuttons.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxlistbox.js"></script>
    <%--<script type="text/javascript" src="../../jqxWidgets/Controls/jqxdropdownlist.js"></script>--%>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdropdownlist _KPI.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcheckbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcombobox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxtabs.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxmenu.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxpanel.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcheckbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxlistbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.export.js"></script>
    
    <style type="text/css">
        .jqx-widget-header
        {
            z-index: 1;
            position: relative;
            height: 100%; /*width: 250px;  */
            left: 0px;
        }
        .jqx-grid-cell
        {
            border: none;
            padding: 0 0 0 0;
        }
        .jqx-listbox
        {
            z-index:9 !important;
        }
    </style>
    <style type="text/css">
        #Div1
        {
            display: block;
            width: 100%;
            height: 850px; /*overflow: scroll;*/ /* overflow-x: hidden;
            overflow-y: hidden;*/
        }
        
        #tab1Div
        {
            display: block;
            width: 100%;
            height: 800px;
            overflow-x: scroll;
            overflow-y: scroll;
        }
        
        #tab2Div
        {
            display: block;
            width: 100%;
            overflow-x: scroll;
            overflow-y: scroll;
        }
        
        body, html
        {
            overflow-y: hidden; /*Added to remove the vertical scroll from the page.*/
            overflow-x: hidden;
        }
        
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 0px auto 0px auto; /*border: 1px solid #496077;*/
            border: 1px solid #f2f2f2;
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
        
        .style1
        {
            width: 100px;
        }
        
        .jqx-grid-column-header
        {
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
        .style3
        {
            width: 280px;
        }
        .style5
        {
            width: 56px;
        }
        .style6
        {
            width: 130px;
        }
    </style>
    <%--Code to display JQXTabs--%>
    <script type="text/javascript">
        function ShowLoader() {
            $("#blur-on-updateprogress").show();
        }


        function HideLoader() {

            $("#blur-on-updateprogress").hide();
        }

        $(document).ready(function () {
            BindRank();
            BindYear();
            //document.getElementById('blur-on-updateprogress').style.display = 'block';
            ShowLoader();
            showChart();
            showOverAllGrid();
            //document.getElementById('blur-on-updateprogress').style.display = 'none';
            HideLoader();
            var initChart = function () {
                //showChart();
                showByCategoryChart();
                showByCategoryGrid();
            }
            // Create jqxTabs.
            // init widgets.
            var initWidgets = function (tab) {
                switch (tab) {
                    case 1:
                        //document.getElementById('blur-on-updateprogress').style.display = 'block';
                        ShowLoader();
                        initChart();
                        //document.getElementById('blur-on-updateprogress').style.display = 'none';
                        HideLoader();
                        break;
                }
            }
            $('#jqxTabs').jqxTabs({ width: '100%', height: '100%', position: 'top', initTabContent: initWidgets });

            var isVesselChecked = false;
            $('#jqxWidget').on('checkChange', function (event) {
                isVesselChecked = true;
            });

            $('#jqxWidget').on('close', function (event) {
                if (isVesselChecked == true) {
                    if (ValidateRank() && ValidateTab1Year()) {
                        ShowLoader();
                        showOverAllGrid();
                        showChart();
                        HideLoader();
                    }
                }
                isVesselChecked = false;
            });

            var isYearChecked = false;
            $('#jqxYearWidget').on('checkChange', function (event) {
                isYearChecked = true;
            });

            $('#jqxYearWidget').on('close', function (event) {
                if (isYearChecked == true) {
                    if (ValidateTab2Year()) {
                        ShowLoader();
                        showByCategoryChart();
                        showByCategoryGrid();
                        HideLoader();
                    }
                }
                isYearChecked = false;
            });

            var istab1YearChecked = false;
            $('#jqxYearTab1').on('checkChange', function (event) {
                istab1YearChecked = true;
            });

            $('#jqxYearTab1').on('close', function (event) {
                if (istab1YearChecked == true) {
                    if (ValidateTab1Year() && ValidateRank()) {
                        //document.getElementById('blur-on-updateprogress').style.display = 'block';
                        ShowLoader();
                        showOverAllGrid();
                        showChart();
                        //document.getElementById('blur-on-updateprogress').style.display = 'none';
                        HideLoader();
                    }
                }
                istab1YearChecked = false;
            });

        });
    </script>
    <script type="text/javascript">
        function ValidateRank() {
            var checkedItems = "";
            var items = $("#jqxWidget").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });
            var RankIDs = checkedItems;
            if (RankIDs == "") {
                alert('Atleast 1 rank should be selected!');
                return false;
            }
            return true;
        }

        function ValidateTab1Year() {
            var checkedYears = "";
            var yearItems = $("#jqxYearTab1").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                checkedYears += this.value + ",";
            });
            var value = checkedYears.replace(/,\s*$/, "");

            value = value.split(",");
            if (checkedYears == "") {
                alert('Atleast 1 year should be selected!');
                return false;
            }
            else if (value.length > 2) {
                alert('Maximum 2 Years can be selected!');
                return false;
            }
            return true;
        }

        function ValidateTab2Year() {
            var checkedYears = "";
            var yearItems = $("#jqxYearWidget").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                checkedYears += this.value + ",";
            });
            var value = checkedYears.replace(/,\s*$/, "");

            value = value.split(",");
            if (checkedYears == "") {
                alert('Atleast 1 year should be selected!');
                return false;
            }
            else if (value.length > 2) {
                alert('Maximum 2 Years can be selected!');
                return false;
            }
            return true;
        }

        function BindRank() {
            var url = "../../KPIService.svc/GetRankData";
            // prepare the data
            var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'RANKNAME' },
                        { name: 'RANKID' }
                    ],
                    id: 'id',
                    url: url,
                    async: false
                };
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxWidget").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "RANKNAME", valueMember: "RANKID", width: 200, height: 25 });
            $("#jqxWidget").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $("#jqxWidget").jqxDropDownList('checkAll');
            SelectDeselect("#jqxWidget");

            $('#jqxWidget').on('change', function (event) {

                var args = event.args;
                if (args) {
                    var index = args.index;
                    var item = args.item;
                    var label = item.label;
                    var value = item.value;
                }

            });
        }

        function BindYear() {
            // Create a YEAR jqxDropDownList for TAB1 and TAB2
            var url = "../../KPIService.svc/GetYearData";
            // prepare the data
            var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'YEAR' }
                    ],
                    id: 'id',
                    url: url,
                    async: false
                };
            var dataAdapter = new $.jqx.dataAdapter(source);

            $("#jqxYearWidget").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "YEAR", valueMember: "YEAR", width: 200, height: 25 });
            $("#jqxYearTab1").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "YEAR", valueMember: "YEAR", width: 200, height: 25 });
            $("#jqxYearWidget").jqxDropDownList('checkIndex', 0);
            $("#jqxYearWidget").jqxDropDownList('checkIndex', 1);
            $("#jqxYearWidget").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            SelectDeselect("#jqxYearWidget");

            $("#jqxYearTab1").jqxDropDownList('checkIndex', 0);
            $("#jqxYearTab1").jqxDropDownList('checkIndex', 1);
            $("#jqxYearTab1").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            SelectDeselect("#jqxYearTab1");

        }
    </script>
    <script type="text/javascript">
        function SelectDeselect(dropdownName) {
            //Logic to implement Select All starts here
            var handleCheckChange = true;
            $(dropdownName).bind('checkChange', function (event) {

                if (!handleCheckChange)
                    return;
                if (event.args.label != 'All') {
                    handleCheckChange = false;
                    $(dropdownName).jqxDropDownList('checkIndex', 0);
                    var checkedItems = $(dropdownName).jqxDropDownList('getCheckedItems');
                    var items = $(dropdownName).jqxDropDownList('getItems');
                    if (checkedItems.length == 1) {
                        $(dropdownName).jqxDropDownList('uncheckIndex', 0);
                    }
                    else if (items.length != checkedItems.length) {
                        $(dropdownName).jqxDropDownList('indeterminateIndex', 0);
                    }
                    handleCheckChange = true;
                }
                else {
                    handleCheckChange = false;
                    if (event.args.checked) {
                        $(dropdownName).jqxDropDownList('checkAll');
                    }
                    else {
                        $(dropdownName).jqxDropDownList('uncheckAll');
                    }
                    handleCheckChange = true;
                }
            });
            //Logic to implement Select All ends here
        }
    </script>
    <script type="text/javascript">
        function openDetail(ID, Name) {

            var RankIDs = document.getElementById("ctl00_MainContent_hdnRanks").value;

            var Year = document.getElementById("ctl00_MainContent_hdnYears").value;

            var url = 'Crew_Retention_Category.aspx?ID=' + ID + '&RankIDs=' + RankIDs + '&Year=' + Year + '&Category=' + Name;

            OpenPopupWindowBtnID('RetentionRate', '', url, 'popup', 500, 800, null, null, false, false, true, null);
        }



        function ValidateSearch(str) {
            if (str == 'Year2') {

                alert('Maximum 2 Years can be selected!');
                return false;
            }
            else if (str == 'Rank1') {

                alert('Atleast 1 rank should be selected!');
                return false;
            }
            else if (str == 'Year1') {

                alert('Atleast 1 year should be selected!');
                return false;
            }

        }



        function showChart() {
            //document.getElementById('blur-on-updateprogress').style.display = 'block';
            var items = $("#jqxWidget").jqxDropDownList('getItems');
            var checkedRankItems = "";
            var items = $("#jqxWidget").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedRankItems += this.value + ",";
            });

            var RankIDs = checkedRankItems;
            document.getElementById("ctl00_MainContent_hdnRanks").value = checkedRankItems;

            var items = $("#jqxYearTab1").jqxDropDownList('getItems');
            var checkedYearItems = "";
            var items = $("#jqxYearTab1").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedYearItems += this.value + ",";
            });

            var Year = checkedYearItems;
            document.getElementById("ctl00_MainContent_hdnYears").value = checkedYearItems;

            // var RankIDs = document.getElementById("ctl00_MainContent_hdnRanks").value;
            //  var Year = document.getElementById("ctl00_MainContent_hdnYears").value;

            var Category = document.getElementById("ctl00_MainContent_hdnCategory").value;

            var hdnCatArray = Category.split(',');

            var CategoryID = document.getElementById("ctl00_MainContent_hdnCategoryID").value;

            var hdnCatIDArray = CategoryID.split(',');
            var CatCount = hdnCatIDArray.length;
            var CatIndex = 0;
            var CatId = '0';
            var CatName = "Retention Rate - By Rank"

            Container = "#ctl00_MainContent_chartContainer_";
            var crow = document.getElementById("ctl00_MainContent_hiddenCount").value;
            var ccol = document.getElementById("ctl00_MainContent_hiddenCount1").value;

            if (CatIndex == 0) {

                CatName = "Retention Rate - By Rank";
                var Object = {
                    "Quarter": "",
                    "Value": "",
                    "Category": "",
                    "Year": "",
                    "Rank": ""
                }

                Object.Category = CatId;
                Object.Rank = RankIDs;
                Object.Year = Year;


                url = "../../KPIService.svc/GetRetentionData";

                $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(Object),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processdata: true,
                    async: false,
                    success: showChartSelected_OnSuccess

                });
                RankIDs = '0';
                CatIndex = 1;
            }

        }




        function showChartSelected_OnSuccess(data, status, jqXHR) {
            var CatName = "Retention Rate - By Rank"
            var sTitle = CatName;

            var resultArray = [];
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source = {
                datatype: "json",
                datafields: [{
                    name: 'Quarter'
                },
             {
                 name: 'Value'
             }
         ],
                localdata: objData
            };

            var dataAdapter = new $.jqx.dataAdapter(source);
            var settings = {
                borderLineColor: 'white',
                title: sTitle,
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: {
                    left: 1,
                    top: 5,
                    right: 25,
                    bottom: 5
                },
                titlePadding: {
                    left: 10,
                    top: 0,
                    right: 0,
                    bottom: 10
                },
                source: dataAdapter,
                categoryAxis: {
                    dataField: 'Quarter',
                    description: '',
                    showGridLines: false,
                    showTickMarks: true
                },
                xAxis: {
                    dataField: 'Quarter',
                    valuesOnTicks: false
                },
                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups: [{
                    type: 'stackedline',
                    toolTipFormatFunction: function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                        var formattedTooltip = "<div style='width:110px; height:40px'>" +
                     "<b>Qtr: </b>" + xAxisValue + "</br>" +
                     "<b>Rate: </b>" + value.replace(",", ".") + "</br>" +
                     "</div>";
                        return formattedTooltip;
                    },
                    valueAxis: {
                        visible: true
                    },
                    series: [{
                        dataField: 'Value',
                        displayText: 'Retention Rate',
                        lineWidth: 6
                    }]
                }]
            };
            // setup the chart
            $('#dvchart').jqxChart(settings);

        }




        function showByCategoryChart() {
            var CategoryID = document.getElementById("ctl00_MainContent_hdnCategoryID").value;

            var hdnCatIDArray = CategoryID.split(',');
            var CatCount = hdnCatIDArray.length;
            for (var x = 0; x < CatCount; x++) {
                //document.getElementById('blur-on-updateprogress').style.display = 'block';

                control = Container + x;

                var ConCatId = "#ctl00_MainContent_hdnID_" + x;
                var ConCatName = "#ctl00_MainContent_hdnName_" + x;

                CatId = $(ConCatId).val();
                CatName = $(ConCatName).val();

                CatName = 'Retention Rate-' + CatName;
                var Object = {
                    "Quarter": "",
                    "Value": "",
                    "Category": "",
                    "Year": "",
                    "Rank": ""
                }

                var itemsTab2 = $("#jqxYearWidget").jqxDropDownList('getItems');
                var checkedYearItemsTab2 = "";
                var itemsTab2 = $("#jqxYearWidget").jqxDropDownList('getCheckedItems');
                $.each(itemsTab2, function (index) {
                    checkedYearItemsTab2 += this.value + ",";
                });

                var YearTab2 = checkedYearItemsTab2;
                document.getElementById("ctl00_MainContent_hdnYearTab2").value = checkedYearItemsTab2;

                Object.Category = CatId;
                Object.Rank = RankIDs;
                Object.Year = YearTab2;
                url = "../../KPIService.svc/GetRetentionData";

                $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(Object),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processdata: true,
                    async: false,
                    success: showChart_OnSuccess

                });

                CatIndex++;

                //document.getElementById('blur-on-updateprogress').style.display = 'none';

                function showChart_OnSuccess(data, status, jqXHR) {
                    var sTitle = CatName;
                    var resultArray = [];
                    var len = 0;
                    $.each(data, function (key, value) {
                        len = value;
                    });

                    var jsonData = JSON.stringify(len);
                    var objData = $.parseJSON(jsonData);
                    var source = {
                        datatype: "json",
                        datafields: [{
                            name: 'Quarter'
                        },
                {
                    name: 'Value'
                }
            ],
                        localdata: objData
                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);

                    var settings = {
                        borderLineColor: 'white',
                        title: '',
                        description: "",
                        enableAnimations: true,
                        showLegend: true,
                        padding: {
                            left: 5,
                            top: 5,
                            right: 50,
                            bottom: 5
                        },
                        titlePadding: {
                            left: 10,
                            top: 0,
                            right: 0,
                            bottom: 10
                        },
                        source: dataAdapter,
                        categoryAxis: {
                            dataField: 'Quarter',
                            description: '',
                            showGridLines: false,
                            showTickMarks: true
                        },
                        xAxis: {
                            dataField: 'Quarter',
                            valuesOnTicks: false
                        },
                        colorScheme: 'scheme01',
                        columnSeriesOverlap: false,
                        seriesGroups: [{
                            type: 'stackedline',
                            toolTipFormatFunction: function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                                var formattedTooltip = "<div style='width:110px; height:40px'>" +
                        "<b>Qtr: </b>" + xAxisValue + "</br>" +
                        "<b>Rate: </b>" + value.replace(",", ".") + "</br>" +
                        "</div>";
                                return formattedTooltip;
                            },
                            valueAxis: {
                                visible: true
                            },
                            series: [{
                                dataField: 'Value',
                                displayText: 'Retention Rate',
                                lineWidth: 4
                            }]
                        }]
                    };
                    $(control).jqxChart(settings);

                }
            }
        }

        

        //overall Crew Retention Details in GRIDVIEW - starts
        function showByCategoryGrid() {
            //document.getElementById('blur-on-updateprogress').style.display = 'block'; 
            var items = $("#jqxWidget").jqxDropDownList('getItems');
            var checkedRankItems = "";
            var items = $("#jqxWidget").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedRankItems += this.value + ",";
            });
            var RankIDs = checkedRankItems;
            document.getElementById("ctl00_MainContent_hdnRanks").value = checkedRankItems;

            var items = $("#jqxYearWidget").jqxDropDownList('getItems');
            var checkedYearItems = "";
            var items = $("#jqxYearWidget").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedYearItems += this.value + ",";
            });

            var Year = checkedYearItems;
            document.getElementById("ctl00_MainContent_hdnYearTab2").value = checkedYearItems;

            var Category = document.getElementById("ctl00_MainContent_hdnCategory").value;
            var hdnCatArray = Category.split(',');

            var CategoryID = document.getElementById("ctl00_MainContent_hdnCategoryID").value;

            var hdnCatIDArray = CategoryID.split(',');
            var CatCount = hdnCatIDArray.length;
            var CatIndex = 0;
            var CatId = '0';
            var CatName = "Retention Rate - By Rank"

            Container = "#ctl00_MainContent_chartContainer_";
            GridContainer = "#ctl00_MainContent_gridContainer_";
            var crow = document.getElementById("ctl00_MainContent_hiddenCount").value;
            var ccol = document.getElementById("ctl00_MainContent_hiddenCount1").value;


            //            for (var x = 0; x < crow; x++) {
            for (var x = 0; x < CatCount; x++) {

                //for (var y = 0; y < ccol; y++) {
                //    if (CatIndex <= CatCount) {
                //control = GridContainer + x + "_" + y;

                //var ConCatId = "#ctl00_MainContent_hdnID_" + x + y;
                //var ConCatName = "#ctl00_MainContent_hdnName_" + x + y;

                control = GridContainer + x;

                var ConCatId = "#ctl00_MainContent_hdnID_" + x;
                var ConCatName = "#ctl00_MainContent_hdnName_" + x;

                CatId = $(ConCatId).val();
                CatName = $(ConCatName).val();

                CatName = 'Retention Rate-' + CatName;
                var Object = {
                    "Quarter": "",
                    "Value": "",
                    "Category": "",
                    "Year": "",
                    "Rank": ""
                }

                Object.Category = CatId;
                Object.Rank = RankIDs;
                Object.Year = Year;
                url = "../../KPIService.svc/GetRetentionData";

                $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(Object),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processdata: true,
                    async: false,
                    success: showGridSelected_OnSuccess

                });
                // }
                RankIDs = '0';
                CatIndex = 1;
                CatIndex++;
                //}
                //document.getElementById('blur-on-updateprogress').style.display = 'none';

                function showGridSelected_OnSuccess(data, status, jqXHR) {

                  //  document.getElementById('blur-on-updateprogress').style.display = 'block';
                    var sTitle = CatName;
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
                          { name: 'Quarter' },
                            { name: 'AVERAGE', type: 'number' },
                            { name: 'NTBR', type: 'number' },
                            { name: 'LeftAll', type: 'number' },
                            { name: 'Value', type: 'number' }
                         ],
                         localdata: objData,
                         id: 'id',
                         url: url
                     };

                    var cellsrenderer = function (row, column, value, defaultHtml) {
                        return defaultHtml;
                    }

                    var columnsrenderer = function (value) {
                        return '<div style="text-align: left; margin-top: 5px; margin-left:5px;font-size:14px;">' + value + '</div>';
                    }

                    var dataAdapter = new $.jqx.dataAdapter(source);

                    var settings = {
                        enablehover: false,
                        sortable: true,
                        filterable: true,
                        autoheight: true,
                        width: 700,
                        altrows: true,
                        source: dataAdapter,
                        columns: [
                        { text: 'Quarter', datafield: 'Quarter', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '10%' },
                        { text: 'Employed Crew(PI06)', datafield: 'AVERAGE', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '30%' },
                        { text: 'NTBR(PI16)', datafield: 'NTBR', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '20%' },
                        { text: 'Total Left(PI41)', datafield: 'LeftAll', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '20%' },
                        { text: 'Retention Rate', datafield: 'Value', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '20%', cellsformat: 'f2' }
                     ]
                    };
                    $(control).jqxGrid(settings);

                //    document.getElementById('blur-on-updateprogress').style.display = 'none';
                }
            }

        }
        //By Category GRIDVIEW - ends
    </script>
    <%--script to bind OVERALL JQXgrid --%>
    <script type="text/javascript">


        function showOverAllGrid() {
            //document.getElementById('blur-on-updateprogress').style.display = 'block';
            var items = $("#jqxWidget").jqxDropDownList('getItems');
            var checkedRankItems = "";
            var items = $("#jqxWidget").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedRankItems += this.value + ",";
            });

            var Rank = checkedRankItems;
            document.getElementById("ctl00_MainContent_hdnRanks").value = checkedRankItems;

            var items = $("#jqxYearTab1").jqxDropDownList('getItems');
            var checkedYearItems = "";
            var items = $("#jqxYearTab1").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedYearItems += this.value + ",";
            });

            var Year = checkedYearItems;
            document.getElementById("ctl00_MainContent_hdnYears").value = checkedYearItems;

            var Object = {
                "Quarter": "",
                "Value": "",
                "Category": "",
                "Year": "",
                "Rank": ""
            }
            Object.Category = 0;
            Object.Rank = Rank;
            Object.Year = Year;
            url = "../../KPIService.svc/GetRetentionData";
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showGrid_OnSuccess

            });
            RankIDs = '0';
            CatIndex = 1;
            //document.getElementById('blur-on-updateprogress').style.display = 'none';

        }

        function showGrid_OnSuccess(data, status, jqXHR) {
            //   $('#divProgress').show();
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
                           { name: 'Quarter' },
                            { name: 'AVERAGE', type: 'number' },
                            { name: 'NTBR', type: 'number' },
                            { name: 'LeftAll', type: 'number' },
                            { name: 'Value', type: 'number' }
                         ],
                         localdata: objData,
                         id: 'id',
                         url: url

                     };
            var cellsrenderer = function (row, column, value, defaultHtml) {
                return defaultHtml;
            }

            var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }

            var dataAdapter = new $.jqx.dataAdapter(source);

            $("#jqxgrid").jqxGrid(
                {
                    enablehover: false,
                    autoheight: true,
                    width: 1200,
                    source: dataAdapter,
                    sortable: true,
                    filterable: true,
                    altrows: true,

                    columns: [
                    { text: 'Quarter', datafield: 'Quarter', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '10%' },
                    { text: 'Employed Crew(PI06)', datafield: 'AVERAGE', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '30%' },
                    { text: 'NTBR(PI16)', datafield: 'NTBR', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '20%' },
                    { text: 'Total Left(PI41)', datafield: 'LeftAll', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '20%' },
                    { text: 'Retention Rate', datafield: 'Value', cellsalign: 'left', renderer: columnsrenderer, cellsrenderer: cellsrenderer, width: '20%', cellsformat: 'f2' }
                  ]
                });
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id='Div1'>
     <div id="blur-on-updateprogress" style="display: block; z-index:999;">
                    <div id="divProgress" style="position: absolute; left: 49%; top: 200px;
                        width: 100%; height: 100%; color: black">
                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </div>
        <div id='jqxTabs' style="width: 100%;">
            <ul>
                <li id="CategoryLi" style="font-size: 14px;">Overall</li>
                <li id="OverallLi" style="font-size: 14px;">By Category</li>
            </ul>
            <div id="tab1Div">
                <table width="100%" height="500px" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <div style="width: 99%; vertical-align: middle; height: 15%; margin: 0 auto;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: #f9f9f9;
                                    border-style: none;">
                                    <tr>
                                        <td height="10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 14px;" class="style5">
                                            <b>Years:</b>
                                        </td>
                                        <td align="left" class="style3">
                                            <div style="width: 151px">
                                                <div style='float: left; background-color: #f9f9f9;' id='jqxYearTab1'>
                                                </div>
                                            </div>
                                        </td>
                                        <td align="right" class="style6" style="font-size: 14px;">
                                            <b>Rank Selection:</b>
                                        </td>
                                        <td align="left">
                                            <div id='jqxWidget' style='background-color: #f9f9f9;'>
                                            </div>
                                            <%--Crew Retention Rate--%>
                                        </td>
                                        <td align="right" width="10%">
                                            <asp:ImageButton ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click"
                                                ToolTip="Export to Excel" ImageUrl="~/Images/Excel-icon.png" Style='padding-right: 10px;' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10px">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="center">
                            <div id="dvchart" style="width: 1200px; float: inherit; height: 400px; border:0px solid red;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 20px;">
                            </div>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <div style="height: 5px;">
                                        </div>
                                        <div style="vertical-align: middle;" id="jqxgrid">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblFormulaQuarter" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                        <div id="eventslog" style="margin-top: 30px; visibility: hidden;">
                                            <div style="float: left; margin-right: 10px;">
                                                <input value="Remove Sort" id="clearsortingbutton" type="button" />
                                                <div style="margin-top: 10px;" id='sortbackground'>
                                                    Sort Background</div>
                                            </div>
                                            <div style="margin-left: 100px; float: left;">
                                                Event Log:
                                                <div style="border: none;" id="events">
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tab2Div">
                <table width="100%" border="0">
                    <tr>
                        <td height="10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" style="height: 25px; background-color: #f9f9f9; border-style: none;">
                            <div style="width: 100%; vertical-align: middle; height: 25px;">
                                <table border="0" width="100%" style="background-color: #f9f9f9; border-style: none;">
                                    <tr style="padding-top: 10px; padding-bottom: 10px;">
                                        <td align="left" width="3%" style="font-size: 14px;">
                                            <b>&nbsp;&nbsp;Years:</b>
                                        </td>
                                        <td align="left" width="97%">
                                            <div>
                                                <div style='float: left; background-color: #f9f9f9; ' id='jqxYearWidget'>
                                                </div>
                                            </div>
                                        </td>
                                        <td align="right" width="10%">
                                            <asp:ImageButton ID="btnTab2Export" runat="server" OnClick="btnTab2ExportToExcel_Click"
                                                ToolTip="Export to Excel" ImageUrl="~/Images/Excel-icon.png" Style='padding-right: 10px;' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="25px">
                            <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" OnClientClick="return btnFilter_Search(true)"
                                ToolTip="Search" ValidationGroup="ValidateSearch" ImageUrl="~/Images/SearchButton.png"
                                Visible="false" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            <span width="100%">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td width="80%" align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="1">
                                        <tr>
                                            <td width="10%">
                                            </td>
                                            <td align="right" valign="middle" width="5%">
                                                <%--Rank:--%>
                                            </td>
                                            <td width="20%">
                                                <ucDDL:ucCustomDropDownList ID="ddlRank" runat="server" UseInHeader="false" OnApplySearch="ddlRank_SelectedIndexChanged"
                                                    Height="200" Width="150" Visible="false" />
                                                <asp:ListBox ID="lstRank" Height="50px" SelectionMode="Multiple" Width="200px" Visible="false"
                                                    runat="server"></asp:ListBox>
                                            </td>
                                            <td align="right" valign="middle" width="8%">
                                            </td>
                                            <td>
                                                <ucDDL:ucCustomDropDownList ID="ddlYear" runat="server" UseInHeader="false" OnApplySearch="ddlYear_SelectedIndexChanged"
                                                    Height="200" Width="150" Visible="false" />
                                                <asp:ListBox ID="lstYear" Height="50px" Width="80px" SelectionMode="Multiple" Visible="false"
                                                    runat="server"></asp:ListBox>
                                            </td>
                                            <td align="left" valign="middle" class="style1">
                                                <table>
                                                    <tr>
                                                        <td width="12%" valign="middle">
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnChart" runat="server" ImageUrl="../../Images/graph-button.gif"
                                                    OnClientClick="showChart(); return false;" Style="width: 100px; height: 30px;"
                                                    Visible="false" />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>               
            </div>
            <asp:HiddenField ID="hdnRanks" runat="server" />
            <asp:HiddenField ID="hdnYears" runat="server" />
            <asp:HiddenField ID="hdnYearTab2" runat="server" />
            <asp:HiddenField ID="hiddenCount1" runat="server" />
            <asp:HiddenField ID="hiddenCount" runat="server" />
            <asp:HiddenField ID="hdnCategory" runat="server" />
            <asp:HiddenField ID="hdnCategoryID" runat="server" />
            <asp:HiddenField ID="hiddenKPIID" runat="server" Value="27" />
        </div>
    </div>
</asp:Content>
