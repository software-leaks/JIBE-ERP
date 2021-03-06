﻿<%@ Page Title="Port State Control Deficiencies" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="PortStateControl.aspx.cs" Inherits="TMSA_WorklistReport_PortStateControl" %>

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
    <link href="../../jqxWidgets/Controls/styles/jqx.base_New.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../jqxWidgets/scripts/jquery-1.11.1.min.js" type="text/javascript"></script>--%>
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
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxpanel.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcheckbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxlistbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.export.js"></script>
    <style type="text/css">
        .jqx-grid-cell
        {
            border: none;
            padding: 0 0 0 0;
        }
        body, html
        {
            /*overflow-x: hidden;*/
        }
        
        .page
        {
            width: 100%;
            height: 850px;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
        .jqx-widget-header
        {
            z-index: 1;
            position: relative;
            height: 100%;
            width: 250px;
            left: 0px;
        }
    </style>
    <style type="text/css">
       
        
        .page
        {
            width: 100%;
            height: 950px;
            background-color: #fff;
            margin: 0px auto 0px auto; /*border: 1px solid #496077;*/
            border: 1px solid #f2f2f2;
        }
        .tabcontent
        {
            width: 100%;
            margin: 0 auto;
            background: #f2f2f2;
            min-height: 100px;
        }
        .tabs input[type=radio]
        {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }
        .tabs
        {
            width: 100%;
            float: none;
            list-style: none;
            position: relative;
            padding: 0;
            margin: 0px;
            padding: 0px;
        }
        .tabs li
        {
            float: left;
        }
        .tabs label
        {
            display: block;
            padding: 5px 13px;
            border-radius: 2px 2px 0 0;
            color: #000;
            font-size: 15px;
            font-weight: normal;
            cursor: pointer;
            position: relative;
            -webkit-transition: all 0.2s ease-in-out;
            -moz-transition: all 0.2s ease-in-out;
            -o-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
        }
        .tabs label:hover
        {
            background: rgba(255,255,255,0.5);
            top: 0;
        }
        
        [id^=tab]:checked + label
        {
            background: #fff;
            color: #000;
            top: 0;
            border-top: 1px solid #acacac;
            border-left: 1px solid #acacac;
            border-right: 1px solid #acacac;
            border-bottom: 1px solid #fff;
        }
        
        
        [id^=tab]:checked ~ [id^=tab-content]
        {
            display: block;
        }
        .tab-content
        {
            z-index: 2;
            display: none;
            text-align: left;
            width: 100%;
            line-height: 140%;
            padding-top: 10px;
            background: #fff;
            padding: 15px;
            position: absolute;
            top: 28px;
            left: 0;
            box-sizing: border-box;
            -webkit-animation-duration: 0.5s;
            -o-animation-duration: 0.5s;
            -moz-animation-duration: 0.5s;
            animation-duration: 0.5s;
        }
        
        .iconse
        {
            float: right;
        }
        .main
        {
            margin: 0px !important;
        }
        .body-devide{width:100%; overflow:hidden; }
.body-devide ul {margin:0px !important; padding:0px !important; width:100%; }
.body-devide ul li{list-style:none; float:left;  }
.body-devide ul li:nth-of-type(1){width:47.5%;  }
.body-devide ul li:nth-of-type(2){width:5%; }
.body-devide ul li:nth-of-type(3){width:47.5%; }

@media screen and (min-device-width:0px) and (max-device-width: 1024px)  {
.body-devide{width:100%; overflow:hidden; }
.body-devide ul {margin:0px !important; padding:0px !important; width:100%; }
.body-devide ul li{list-style:none; float:left; }
.body-devide ul li:nth-of-type(1){width:100%;   }
.body-devide ul li:nth-of-type(2){width:100%; height:35px; }
.body-devide ul li:nth-of-type(3){width:100%; margin-left:20px; }
}

@media all and (-webkit-min-device-pixel-ratio:0) and (max-width:1024px) 
{
 .body-devide{width:100%; overflow:hidden; }
.body-devide ul {margin:0px !important; padding:0px !important; width:100%; }
.body-devide ul li{list-style:none; float:left; }
.body-devide ul li:nth-of-type(1){width:100%;   }
.body-devide ul li:nth-of-type(2){width:100%; height:35px; }
.body-devide ul li:nth-of-type(3){width:100%; margin-left:20px; }

}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var isYearTab2Checked;
            var isVesselTab2Checked;
            BindVessel("#jqxVessel");
            BindYear("#jqxYear");


            showPSCNCRYearlyChart();
            showPSCNCRYearlyGrid();

            showPSCJobsYearlyGrid();
            showPSCJobsYearlyChart();


            $("input[name=tabs]:radio").click(function () {
                if ($(this).val() === 'tab2') {
                    BindYear("#jqxYearTab2");
                    BindVessel("#jqxVesselTab2");
                    showPSCNCRMonthlyChart();
                    showPSCNCRMonthlyGrid();
                    showPSCJobsMonthlyGrid();
                    showPSCJobsMonthlyChart();
                    isYearTab2Checked = false;
                    isVesselTab2Checked = false;
                }
            });

            var isYearChecked = false;
            $('#jqxYear').on('checkChange', function (event) {
                isYearChecked = true;
            });

            $('#jqxYear').on('close', function (event) {
                if (isYearChecked == true) {
                    if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel")) {
                        showPSCNCRYearlyChart();
                        showPSCNCRYearlyGrid();

                        showPSCJobsYearlyGrid();
                        showPSCJobsYearlyChart();
                    }
                }
                isYearChecked = false;
            });

            var isVesselChecked = false;
            $('#jqxVessel').on('checkChange', function (event) {
                isVesselChecked = true;
            });

            $('#jqxVessel').on('close', function (event) {
                if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel")) {
                    if (isVesselChecked == true) {
                        showPSCNCRYearlyChart();
                        showPSCNCRYearlyGrid();

                        showPSCJobsYearlyGrid();
                        showPSCJobsYearlyChart();
                    }
                }
                isVesselChecked = false;
            });

            isYearTab2Checked = false;
            $('#jqxYearTab2').on('checkChange', function (event) {
                isYearTab2Checked = true;
            });

            $('#jqxYearTab2').on('close', function (event) {
                if (ValidateYear("#jqxYearTab2") && ValidateVessel("#jqxVesselTab2")) {
                    if (isYearTab2Checked == true) {
                        showPSCNCRMonthlyChart();
                        showPSCNCRMonthlyGrid();
                        showPSCJobsMonthlyGrid();
                        showPSCJobsMonthlyChart();
                    }
                }
                isYearTab2Checked = false;
            });

            isVesselTab2Checked = false;
            $('#jqxVesselTab2').on('checkChange', function (event) {
                isVesselTab2Checked = true;
            });

            $('#jqxVesselTab2').on('close', function (event) {
                if (isVesselTab2Checked == true) {
                    if (ValidateVessel("#jqxVesselTab2") && ValidateYear("#jqxYearTab2")) {
                        showPSCNCRMonthlyChart();
                        showPSCNCRMonthlyGrid();
                        showPSCJobsMonthlyGrid();
                        showPSCJobsMonthlyChart();
                    }
                }
                isVesselTab2Checked = false;
            });
            SelectDeselect("#jqxVessel");
            SelectDeselect("#jqxYear");
            SelectDeselect("#jqxVesselTab2");
        });
    </script>
    <%--Method to display Vessel Name --%>
    <script type="text/javascript">
        
         function BindVessel(vesselControl) {
            //var url = "../../KPIService.svc/GetVesselList/usercompanyid/1";
            var userCompID = '<%= Session["USERCOMPANYID"]%>';
            var url = "../../KPIService.svc/GetVesselList/usercompanyid/"+userCompID;
             var source = {
                 datatype: "json",
                 datafields: [
                        { name: 'Vessel_Name' },
                        { name: 'Vessel_Id' }
                    ],
                 id: 'id',
                 url: url,
                 async: false
             };
             var dataAdapter = new $.jqx.dataAdapter(source);
             // Create a jqxDropDownList
             $(vesselControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Vessel_Name", valueMember: "Vessel_Id", selectedIndex: 1, width: '200', height: '25' });
             $(vesselControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
             $(vesselControl).jqxDropDownList('checkAll');
          //   SelectDeselect(vesselControl);

             $(vesselControl).on('change', function (event) {

                 var args = event.args;
                 if (args) {
                     var index = args.index;
                     var item = args.item;
                     var label = item.label;
                     var value = item.value;
                 }

             });
         }

    <%--Method to bind YEAR dropdown--%>
        function BindYear(yearControl)
        {
             var url = "../../KPIService.svc/GetYears/NumOfYears/5";
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
            // Create a jqxDropDownList
           if(yearControl == '#jqxYear')
            {
                $(yearControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter,displayMember: "YEAR", valueMember: "YEAR", selectedIndex: 0, width: '200', height: '25' });
                $(yearControl).jqxDropDownList('checkIndex',0);
                $(yearControl).jqxDropDownList('checkIndex',1);
                $(yearControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
                //$(yearControl).jqxDropDownList('checkAll');
            }
            else if(yearControl== '#jqxYearTab2')
            {
                $(yearControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "YEAR", valueMember: "YEAR",selectedIndex: 0, width: '200', height: '25' });                
                $(yearControl).jqxDropDownList('checkIndex',0);
                //$(yearControl).jqxDropDownList('checkIndex',1);
               // $(yearControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            }
        //    SelectDeselect(yearControl);
        }

        function SelectDeselect(dropdownName) { //Logic to implement Select All starts here            
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
            });//Logic to implement Select All ends here            
        }

        function showPSCNCRYearlyGrid() {
           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);
            //var VesselID = checkedItems.substring(0,checkedItems.Length - 1);

            //Fetch the list of selected year
            var checkedYears = "";
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

            var Object = {
                "Type": "",
                "Vessel_IDs": "",
                "Years": ""
            }

            Object.Type = "NCR_PSC";
            Object.Vessel_IDs = checkedItems;
            Object.Years = checkedYears;

            url = "../../KPIService.svc/GetMultipleVesselWorkList";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showPSCNCRYearlyGrid_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });
        }
            
        function showPSCNCRYearlyGrid_OnSuccess(data, status, jqXHR) {
        //Assign CSS to grid header
             var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }
            //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }
            var columnsArray = [];
            itemSeries = {};
            itemSeries["text"] = 'Vessel Name';
            itemSeries["dataField"] = 'VESSEL_Name';
            itemSeries["width"] = "15%";
            itemSeries["renderer"] = columnsrenderer;
            columnsArray.push(itemSeries);            

            //Create a dynamic array of selected YEARS from dropdown
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {  
                if (this.value != 0)
                {   
                    itemSeries = {};  
                    itemSeries["text"] = this.value;
                    itemSeries["dataField"] = this.value;
                    itemSeries["width"] = "6%";
                     itemSeries["cellsrenderer"] = cellsrenderer;
                    itemSeries["renderer"] = columnsrenderer;
                    columnsArray.push(itemSeries);
                }
            });

            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });


             //Create a dynamic array of grid column name
            var dataFieldArray = [];
            arrayItem = {};
            arrayItem["name"] = 'VESSEL_Name';
            arrayItem["type"] = 'string';
            dataFieldArray.push(arrayItem);         


            
            var yearVal = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearVal, function (index){
            if(this.value!=0)
            {
                arrayItem = {};
                arrayItem["name"]=this.value;
                arrayItem["type"]='string';
                dataFieldArray.push(arrayItem);
            }
            });

            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                        datafields: dataFieldArray,
                        datatype: "json",
                        localdata: data,
                        id: 'id',
                        url: url
                    };
           
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#PSC_NCR_YearlyGrid").jqxGrid(
                {
                    enablehover:true,
                    autoheight: true,
                    width:1000,
                    source: dataAdapter,
                    sortable: true,
                    altrows:true,
                    columns : columnsArray
              
                });
        }

    <%-- Method to bind Chart/Graph on the basis of YEAR and VESSEL values from dropdown--%>
    function showPSCNCRYearlyChart() {
        //Fetch the list of selected vessel
        var checkedItems = "";
        var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            if (this.value != 0)
            checkedItems += this.value + ",";                
        });

        if (checkedItems.charAt(0) == '0')      //remove '0' from the string when ALL is selected.
        checkedItems = checkedItems.substring(1);
            
        //Fetch the list of selected year
        var checkedYears = "";
        var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
        $.each(yearItems, function (index) {
            if (this.value != 0)
                checkedYears += this.value + ",";
        });

        if (checkedYears.charAt(0) == '0')          //remove '0' from the string when ALL is selected.
        checkedYears = checkedYears.substring(1);

        //var selectedYears = checkedYears.substring(0, checkedYears.Length - 1);
        var Object1 = {
            "Type": "",
            "Vessel_IDs": "",
            "Years": ""
        }

        Object1.Type = "NCR_PSC";                //Pass type to service
        Object1.Vessel_IDs = checkedItems;       //Pass vesselID to service
        Object1.Years = checkedYears;            //Pass year to service



        url = "../../KPIService.svc/GetMultipleVesselYearWiseWorkList";

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(Object1),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            async: false,
            success: showPSCNCRYearlyChart_OnSuccess,
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showPSCNCRYearlyChart_OnSuccess(data, status, jqXHR) {                   
            var seriesArray2 = [];
            var seriesArray1 = [];
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                itemSeries = {};

                itemSeries1 = {};
                itemSeries1["name"] = this.label;
                seriesArray1.push(itemSeries1);
                if (this.value != 0)
                {
                    itemSeries["dataField"] = this.label;
                    itemSeries["displayText"] = this.label;
                    seriesArray2.push(itemSeries);
                }
            }); 

            itemAvg = {};
            itemAvg["dataField"] = 'AVG';
            itemAvg["displayText"] = 'AVG';
            seriesArray2.push(itemAvg);           

            var source =
                {
                    datatype: "json", 
                    localdata: data
                };

            var toolTipCustomFormatFn1 = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                return 'Year:' + xAxisValue + "&nbsp;</br>" + serie.dataField+':'+ value.replace(",",".")+'&nbsp;';
            }

            var dataAdapter = new $.jqx.dataAdapter(source);

            var settings = {
                borderLineColor: 'white',
                title: 'Port State Control - NCR',
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 20, top: 5, right: 50, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                categoryAxis: {
                    dataField: 'YEAR',
                    description: '',
                    showGridLines: false,
                    showTickMarks: true
                },
                xAxis:
                {
                    dataField: 'YEAR',
                    valuesOnTicks: false
                },
                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups: [
                {
                    type: 'line',
                    toolTipFormatFunction: toolTipCustomFormatFn1,
                    displayValueAxis: true,
                    series:seriesArray2                        
                }
            ]
            };
            $('#PSC_NCR_YearlyChart').jqxChart(settings);
        }


<%-- Method to bind Gridview on the basis of YEAR and VESSEL values from dropdown--%>
        function showPSCJobsYearlyGrid() {
           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);
            //var VesselID = checkedItems.substring(0,checkedItems.Length - 1);

            //Fetch the list of selected year
            var checkedYears = "";
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

            var Object = {
                "Type": "",
                "Vessel_IDs": "",
                "Years": ""
            }

            Object.Type = "JOB_PSC";
            Object.Vessel_IDs = checkedItems;
            Object.Years = checkedYears;

            url = "../../KPIService.svc/GetMultipleVesselWorkList";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showPSCJobsYearlyGrid_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });
        }
            
        function showPSCJobsYearlyGrid_OnSuccess(data, status, jqXHR) {
        //Assign CSS to grid header
             var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }
            //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }
            var columnsArray = [];
            itemSeries = {};
            itemSeries["text"] = 'Vessel Name';
            itemSeries["dataField"] = 'VESSEL_Name';
            itemSeries["width"] = "15%";
            itemSeries["renderer"] = columnsrenderer;
            columnsArray.push(itemSeries);            

            //Create a dynamic array of selected YEARS from dropdown
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {  
                if (this.value != 0)
                {   
                    itemSeries = {};  
                    itemSeries["text"] = this.value.replace(",",".");
                    itemSeries["dataField"] = this.value;
                    itemSeries["width"] = "6%";
                    itemSeries["cellsrenderer"] = cellsrenderer;
                    itemSeries["renderer"] = columnsrenderer;
                    columnsArray.push(itemSeries);
                }
            });

            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });



            //Create a dynamic array of grid column name
            var dataFieldArray = [];
            arrayItem = {};
            arrayItem["name"] = 'VESSEL_Name';
            arrayItem["type"] = 'string';
            dataFieldArray.push(arrayItem);         


            
            var yearVal = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearVal, function (index){
            if(this.value!=0)
            {
                arrayItem = {};
                arrayItem["name"]=this.value;
                arrayItem["type"]='string';
                dataFieldArray.push(arrayItem);
            }
            });


            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                        datafields: dataFieldArray,
                        datatype: "json",
                        localdata: data,
                        id: 'id',
                        url: url
                    };
           
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#PSC_Jobs_YearlyGrid").jqxGrid(
                {
                    enablehover:true,
                    autoheight: true,
                    width:1000,
                    source: dataAdapter,
                    sortable: true,
                    altrows:true,
                    columns : columnsArray
              
                });
        }

    <%-- Method to bind Chart/Graph on the basis of YEAR and VESSEL values from dropdown--%>
    function showPSCJobsYearlyChart() {
        //Fetch the list of selected vessel
        var checkedItems = "";
        var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            if (this.value != 0)
            checkedItems += this.value + ",";                
        });

        if (checkedItems.charAt(0) == '0')      //remove '0' from the string when ALL is selected.
        checkedItems = checkedItems.substring(1);
            
        //Fetch the list of selected year
        var checkedYears = "";
        var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
        $.each(yearItems, function (index) {
            if (this.value != 0)
                checkedYears += this.value + ",";
        });

        if (checkedYears.charAt(0) == '0')          //remove '0' from the string when ALL is selected.
        checkedYears = checkedYears.substring(1);

        //var selectedYears = checkedYears.substring(0, checkedYears.Length - 1);
        var Object1 = {
            "Type": "",
            "Vessel_IDs": "",
            "Years": ""
        }

        Object1.Type = "JOB_PSC";                //Pass type to service
        Object1.Vessel_IDs = checkedItems;       //Pass vesselID to service
        Object1.Years = checkedYears;            //Pass year to service



        url = "../../KPIService.svc/GetMultipleVesselYearWiseWorkList";

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(Object1),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            async: false,
            success: showPSCJobsYearlyChart_OnSuccess,
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showPSCJobsYearlyChart_OnSuccess(data, status, jqXHR) {                   
            var seriesArray2 = [];
            var seriesArray1 = [];
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                itemSeries = {};

                itemSeries1 = {};
                itemSeries1["name"] = this.label;
                seriesArray1.push(itemSeries1);
                if (this.value != 0)
                {
                    itemSeries["dataField"] = this.label;
                    itemSeries["displayText"] = this.label;
                    seriesArray2.push(itemSeries);
                }
            }); 


            itemAvg = {};
            itemAvg["dataField"] = 'AVG';
            itemAvg["displayText"] = 'AVG';
            seriesArray2.push(itemAvg);           

            var source =
                {
                    datatype: "json", 
                    localdata: data
                };
            var toolTipCustomFormatFn1 = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                return 'Year:' + xAxisValue + "&nbsp;</br>" + serie.dataField+':'+ value.replace(",",".")+'&nbsp;';
            }

            var dataAdapter = new $.jqx.dataAdapter(source);

            var settings = {
                borderLineColor: 'white',
                title: 'Port State Control - Jobs',
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 20, top: 0, right: 50, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                categoryAxis: {
                    dataField: 'YEAR',
                    description: '',
                    showGridLines: false,
                    showTickMarks: true
                },
                xAxis:
                {
                    dataField: 'YEAR',
                    valuesOnTicks: false
                },
                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups: [
                {
                    type: 'line',
                    toolTipFormatFunction: toolTipCustomFormatFn1,
                    displayValueAxis: true,
                    series:seriesArray2                        
                }
            ]
            };
            $('#PSC_Jobs_YearlyChart').jqxChart(settings);
        }



//Script to populate Port State Control - Monthly NCR

function showPSCNCRMonthlyGrid() {
           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVesselTab2").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);


            //var item2 = $("#jqxYearTab2").jqxDropDownList('getSelectedItem');
            //var selectedYear = item2.value;

             var checkedYears = "";
            var yearItems = $("#jqxYearTab2").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

            var Object = {
                "Type": "",
                "Vessel_IDs": "",
                "YEAR": ""
            }

            Object.Type = "NCR_PSC";
            Object.Vessel_IDs = checkedItems;
            Object.YEAR = checkedYears.replace(/(^,)|(,$)/g, "");            //Pass year to service

            url = "../../KPIService.svc/GetMultipleVesselWorkListMonthly";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showPSCNCRMonthlyGrid_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });
        }
            
        function showPSCNCRMonthlyGrid_OnSuccess(data, status, jqXHR) {
        //Assign CSS to grid header
             var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }

              //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }

            var columnsArray = [];
            itemSeries = {};
            itemSeries["text"] = 'Vessel Name';
            itemSeries["dataField"] = 'VESSEL_Name';
            itemSeries["width"] = "15%";
            itemSeries["renderer"] = columnsrenderer;
            columnsArray.push(itemSeries);            

            //Create a dynamic array of selected YEARS from dropdown
            var yearItems = $("#jqxYearTab2").jqxDropDownList('getSelectedItem');
            $.each(yearItems, function (index) {  
                if (this.value != 0)
                {   
                    itemSeries = {};  
                    itemSeries["text"] = this.value;
                    itemSeries["dataField"] = this.value;
                    itemSeries["width"] = "6%";
                    itemSeries["renderer"] = columnsrenderer;
                    columnsArray.push(itemSeries);
                }
            });

               var dataFieldArray = [];
            var columnsArray = [];
            itemSeries = {};

            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });


             $.each(len, function(key, value){                   //Fetch the list of categories from json
                itemSeries = {};
                itemSeries["text"] = key.replace(",", ".");
                itemSeries["dataField"] = key.replace(",", ".");

                if (key.length <= 5)
                    itemSeries["width"] = "7%";
                else if (key.length >= 5 && key.length <= 10)
                    itemSeries["width"] = "10%";
                else if (key.length >= 10)
                    itemSeries["width"] = "15%";

                     if (key == 'VESSEL_Name')
                    itemSeries["text"]='Vessel Name';

               
                itemSeries["renderer"] = columnsrenderer;
                itemSeries["cellsrenderer"] = cellsrenderer;
                columnsArray.push(itemSeries);

                arrayItem = {};
                arrayItem["name"]=key; 
                arrayItem["type"] = 'string';
                dataFieldArray.push(arrayItem);
               
            });

            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                        datatype: "json",
                        datafields: [
                            { name: 'VESSEL_Name', type: 'string' },
                            { name: 'Jan', type: 'string' },
                            { name: 'Feb', type: 'string' },
                            { name: 'Mar', type: 'string' },
                            { name: 'Apr', type: 'string' },
                            { name: 'May', type: 'string' },
                            { name: 'Jun', type: 'string' },
                            { name: 'Jul', type: 'string' },
                            { name: 'Aug', type: 'string' },
                            { name: 'Sep', type: 'string' },
                            { name: 'Oct', type: 'string' },
                            { name: 'Nov', type: 'string' },
                            { name: 'Dec', type: 'string' }
                         ],
                        localdata: data,
                        id: 'id',
                        url: url
                    };
           
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#PSC_NCR_MonthlyGrid").jqxGrid(
                {
                    enablehover:true,
                    autoheight: true,
                    width:750,
                    source: dataAdapter,
                    sortable: true,
                    altrows:true,
                     columns : columnsArray
//                    columns: [
//                  { text: 'Vessel Name', datafield: 'VESSEL_Name', width: 120, renderer: columnsrenderer },
//                  { text: 'Jan', datafield: 'Jan', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Feb', datafield: 'Feb', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Mar', datafield: 'Mar', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Apr', datafield: 'Apr', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'May', datafield: 'May', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Jun', datafield: 'Jun', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Jul', datafield: 'Jul', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Aug', datafield: 'Aug', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Sep', datafield: 'Sep', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Oct', datafield: 'Oct', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Nov', datafield: 'Nov', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Dec', datafield: 'Dec', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer }
//                ],
              
                });
        }

    <%-- Method to bind Chart/Graph on the basis of YEAR and VESSEL values from dropdown--%>
    function showPSCNCRMonthlyChart() {
        //Fetch the list of selected vessel
        var checkedItems = "";
        var items = $("#jqxVesselTab2").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            if (this.value != 0)
            checkedItems += this.value + ",";                
        });

        if (checkedItems.charAt(0) == '0')      //remove '0' from the string when ALL is selected.
        checkedItems = checkedItems.substring(1);
       

//         var item1 = $("#jqxYearTab2").jqxDropDownList('getSelectedItem');
//            var selYear = item1.value;
            var checkedYears = "";
            var yearItems = $("#jqxYearTab2").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

        var Object1 = {
            "Type": "",
            "Vessel_IDs": "",
            "YEAR": ""
        }

        Object1.Type = "NCR_PSC";                //Pass type to service
        Object1.Vessel_IDs = checkedItems;       //Pass vesselID to service
        Object1.YEAR = checkedYears.replace(/(^,)|(,$)/g, "");



        url = "../../KPIService.svc/GetMonthlyWorklistCountByVessel";

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(Object1),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            async: false,
            success: showPSCNCRMonthlyChart_OnSuccess,
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showPSCNCRMonthlyChart_OnSuccess(data, status, jqXHR) {      
            var seriesArray2 = [];
            var seriesArray1 = [];
            var items = $("#jqxVesselTab2").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                itemSeries = {};

                itemSeries1 = {};
                itemSeries1["name"] = this.label;
                seriesArray1.push(itemSeries1);
                if (this.value != 0)
                {
                    itemSeries["dataField"] = this.label;
                    itemSeries["displayText"] = this.label;
                    seriesArray2.push(itemSeries);
                }
            }); 

            itemAvg = {};
            itemAvg["dataField"] = 'AVG';
            itemAvg["displayText"] = 'AVG';
            seriesArray2.push(itemAvg);             

            var source =
                {
                    datatype: "json", 
                    localdata: data
                };
            
             var toolTipCustomFormatFn1 = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                        return 'Mon:' + xAxisValue + "&nbsp;</br>" + serie.dataField+':'+  value.replace(",",".")+'&nbsp;';
                    }

            var dataAdapter = new $.jqx.dataAdapter(source);

            var settings = {
                borderLineColor: 'white',
                title: 'Port State Control - NCR',
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 20, top: 0, right: 50, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                categoryAxis: {
                    dataField: 'MONTH',
                    description: '',
                    showGridLines: false,
                    showTickMarks: true
                },
                xAxis:
                {
                    dataField: 'MONTH',
                    valuesOnTicks: false
                },
                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups: [
                {
                    type: 'line',
                    toolTipFormatFunction: toolTipCustomFormatFn1,
                    displayValueAxis: true,
                    series:seriesArray2                        
                }
            ]
            };
            $('#PSC_NCR_MonthlyChart').jqxChart(settings);
        }

//Script to populate Port State Control - Monthly Jobs

     function showPSCJobsMonthlyGrid() {
           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVesselTab2").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);


//            var item2 = $("#jqxYearTab2").jqxDropDownList('getSelectedItem');
//            var selectedYear = item2.value;

            var checkedYears = "";
            var yearItems = $("#jqxYearTab2").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

            var Object = {
                "Type": "",
                "Vessel_IDs": "",
                "YEAR": ""
            }

            Object.Type = "JOB_PSC";
            Object.Vessel_IDs = checkedItems;
            Object.YEAR = checkedYears.replace(/(^,)|(,$)/g, "");            //Pass year to service

            url = "../../KPIService.svc/GetMultipleVesselWorkListMonthly";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showPSCJobsMonthlyGrid_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });
        }
            
        function showPSCJobsMonthlyGrid_OnSuccess(data, status, jqXHR) {
        //Assign CSS to grid header
             var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }
            //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }
            var columnsArray = [];
            itemSeries = {};
            itemSeries["text"] = 'Vessel Name';
            itemSeries["dataField"] = 'VESSEL_Name';
            itemSeries["width"] = "15%";
            itemSeries["renderer"] = columnsrenderer;
            columnsArray.push(itemSeries);            

            //Create a dynamic array of selected YEARS from dropdown
            var yearItems = $("#jqxYearTab2").jqxDropDownList('getSelectedItem');
            $.each(yearItems, function (index) {  
                if (this.value != 0)
                {   
                    itemSeries = {};  
                    itemSeries["text"] = this.value;
                    itemSeries["dataField"] = this.value;
                    itemSeries["width"] = "6%";
                    itemSeries["renderer"] = columnsrenderer;
                    columnsArray.push(itemSeries);
                }
            });

              var dataFieldArray = [];
            var columnsArray = [];
            itemSeries = {};

            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });


             $.each(len, function(key, value){                   //Fetch the list of categories from json
                itemSeries = {};
                itemSeries["text"] = key.replace(",", ".");
                itemSeries["dataField"] = key.replace(",", ".");

                if (key.length <= 5)
                    itemSeries["width"] = "7%";
                else if (key.length >= 5 && key.length <= 10)
                    itemSeries["width"] = "10%";
                else if (key.length >= 10)
                    itemSeries["width"] = "15%";

                     if (key == 'VESSEL_Name')
                    itemSeries["text"]='Vessel Name';

               
                itemSeries["renderer"] = columnsrenderer;
                itemSeries["cellsrenderer"] = cellsrenderer;
                columnsArray.push(itemSeries);

                arrayItem = {};
                arrayItem["name"]=key; 
                arrayItem["type"] = 'string';
                dataFieldArray.push(arrayItem);
               
            });

            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                        datatype: "json",
                        datafields: [
                            { name: 'VESSEL_Name', type: 'string' },
                            { name: 'Jan', type: 'string' },
                            { name: 'Feb', type: 'string' },
                            { name: 'Mar', type: 'string' },
                            { name: 'Apr', type: 'string' },
                            { name: 'May', type: 'string' },
                            { name: 'Jun', type: 'string' },
                            { name: 'Jul', type: 'string' },
                            { name: 'Aug', type: 'string' },
                            { name: 'Sep', type: 'string' },
                            { name: 'Oct', type: 'string' },
                            { name: 'Nov', type: 'string' },
                            { name: 'Dec', type: 'string' }
                         ],
                        localdata: data,
                        id: 'id',
                        url: url
                    };
           
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#PSC_Jobs_MonthlyGrid").jqxGrid(
                {
                    enablehover:true,
                    autoheight: true,
                    width:750,
                    source: dataAdapter,
                    sortable: true,
                    altrows:true,
                     columns : columnsArray
//                    columns: [
//                  { text: 'Vessel Name', datafield: 'VESSEL_Name', width: 120, renderer: columnsrenderer },
//                  { text: 'Jan', datafield: 'Jan', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Feb', datafield: 'Feb', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Mar', datafield: 'Mar', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Apr', datafield: 'Apr', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'May', datafield: 'May', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Jun', datafield: 'Jun', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Jul', datafield: 'Jul', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Aug', datafield: 'Aug', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Sep', datafield: 'Sep', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Oct', datafield: 'Oct', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Nov', datafield: 'Nov', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer },
//                  { text: 'Dec', datafield: 'Dec', cellsalign: 'left', renderer: columnsrenderer, align: 'left', width: 50,cellsrenderer:cellsrenderer }
//                ],
              
                });
        }

    <%-- Method to bind Chart/Graph on the basis of YEAR and VESSEL values from dropdown--%>
    function showPSCJobsMonthlyChart() {
        //Fetch the list of selected vessel
        var checkedItems = "";
        var items = $("#jqxVesselTab2").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            if (this.value != 0)
            checkedItems += this.value + ",";                
        });

        if (checkedItems.charAt(0) == '0')      //remove '0' from the string when ALL is selected.
        checkedItems = checkedItems.substring(1);
       

//         var item1 = $("#jqxYearTab2").jqxDropDownList('getSelectedItem');
//            var selYear = item1.value;

            var checkedYears = "";
            var yearItems = $("#jqxYearTab2").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

        var Object1 = {
            "Type": "",
            "Vessel_IDs": "",
            "YEAR": ""
        }

        Object1.Type = "JOB_PSC";                //Pass type to service
        Object1.Vessel_IDs = checkedItems;       //Pass vesselID to service
        Object1.YEAR = checkedYears.replace(/(^,)|(,$)/g, "");            //Pass year to service



        url = "../../KPIService.svc/GetMonthlyWorklistCountByVessel";

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(Object1),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            async: false,
            success: showPSCJobsMonthlyChart_OnSuccess,
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showPSCJobsMonthlyChart_OnSuccess(data, status, jqXHR) {                   
            var seriesArray2 = [];
            var seriesArray1 = [];
            var items = $("#jqxVesselTab2").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                itemSeries = {};

                itemSeries1 = {};
                itemSeries1["name"] = this.label;
                seriesArray1.push(itemSeries1);
                if (this.value != 0)
                {
                    itemSeries["dataField"] = this.label;
                    itemSeries["displayText"] = this.label;
                    seriesArray2.push(itemSeries);
                }
            }); 

            itemAvg = {};
            itemAvg["dataField"] = 'AVG';
            itemAvg["displayText"] = 'AVG';
            seriesArray2.push(itemAvg);             

            var source =
                {
                    datatype: "json", 
                    localdata: data
                };
            
             var toolTipCustomFormatFn1 = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                        return 'Mon:' + xAxisValue + "&nbsp;</br>" + serie.dataField+':'+ value.replace(",",".")+'&nbsp;';
                    }

            var dataAdapter = new $.jqx.dataAdapter(source);

            var settings = {
                borderLineColor: 'white',
                title: 'Port State Control - Jobs',
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 20, top: 0, right: 50, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                categoryAxis: {
                    dataField: 'MONTH',
                    description: '',
                    showGridLines: false,
                    showTickMarks: true
                },
                xAxis:
                {
                    dataField: 'MONTH',
                    valuesOnTicks: false
                },
                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups: [
                {
                    type: 'line',
                    toolTipFormatFunction: toolTipCustomFormatFn1,
                    displayValueAxis: true,
                    series:seriesArray2                        
                }
            ]
            };
            $('#PSC_Jobs_MonthlyChart').jqxChart(settings);
        }


    function ValidateVessel(vesselCtrl) { //Method to validate Vessel dropdown
        var checkedItems = "";
        var items = $(vesselCtrl).jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        var VesselName = checkedItems;
        if (VesselName == "") {
            alert('Atleast 1 vessel should be selected!');
            return false;
        }
        return true;
    }


    function ValidateYear(yearCtrl) {   //Method to validate YEAR dropdown
        var checkedYears = "";
        var yearItems = $(yearCtrl).jqxDropDownList('getCheckedItems');
        $.each(yearItems, function (index) {
            checkedYears += this.value + ",";
        });
         var value = checkedYears.replace(/,\s*$/, "");

         if (checkedYears == "") {
            alert('Atleast 1 Year should be selected!');
            return false;
        }

        value = value.split(",");
        if(yearCtrl=='#jqxYear')
        {
            if (value.length < 2) {
                alert('Minimum 2 Years should be selected!');
                return false;
            }
        }
        else if(yearCtrl=='#jqxYearTab2'){
            if (value.length > 1) {
                alert('Minimum 1 Year should be selected!');
                return false;
            }
        }
        return true;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="height: 5px;">
    </div>
    <div class="tabcontent" id="divTab">
        <ul class="tabs">
            <li>
                <input type="radio" name="tabs" value="tab1" id="tab1" checked="" />
                <label for="tab1">
                    Port State Control - Yearly</label>
                <div id="tab-content1" class="tab-content animated fadeIn">
                    <table border="0" width="100%">
                        <tr>
                            <td style="width: 40px;">
                            </td>
                            <td style="font-size: 14px; width: 50px;">
                                <b>Years:</b>
                            </td>
                            <td style="width: 150px;">
                                <div style='float: left; width: 50%; background-color: #f9f9f9; margin-right: 50px;'
                                    id='jqxYear'>
                                </div>
                            </td>
                            <td style="font-size: 14px; width: 100px;">
                                <b>Vessel Name:</b>
                            </td>
                            <td>
                                <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxVessel'>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="height: 25px;">
                    </div>
                    <div class="body-devide">
                        <ul>
                            <li>
                                <div id="PSC_NCR_YearlyChart" style="width: 100%; height: 300px; float: left;"></div>
                             </li>
                            <li>&nbsp; </li>
                            <li>
                                <div id="PSC_NCR_YearlyGrid">
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div style="height: 50px;">
                    </div>
                    <div class="body-devide">
                        <ul>
                            <li>
                            <div id="PSC_Jobs_YearlyChart" style="width: 100%; height: 300px; float: left;">
                            </div>
                            </li>
                            <li>&nbsp;</li>
                            <li>                           
                                 <div id='jqxWidget' style="width: 100%; float: left;">
                                <div id="PSC_Jobs_YearlyGrid">
                                </div>
                            </div>
                            </li>
                        </ul>
                    </div>
                    </div>
            </li>
            <li>
                <input type="radio" name="tabs" id="tab2" value="tab2">
                <label for="tab2">
                    Port State Control - Monthly</label>
                <%--<input style='margin-top: 20px;' type="submit" value="Submit" id='jqxButton' />--%>
                <div id="tab-content2" class="tab-content animated fadeIn">
                    <table border="0" width="100%">
                        <tr>
                            <td style="width: 40px;">
                            </td>
                            <td style="font-size: 14px; width: 50px;">
                                <b>Years:</b>
                            </td>
                            <td style="width: 150px;">
                                <div style='float: left; width: 50%; background-color: #f9f9f9; margin-right: 50px;'
                                    id='jqxYearTab2'>
                                </div>
                            </td>
                            <td style="font-size: 14px; width: 100px;">
                                <b>Vessel Name:</b>
                            </td>
                            <td>
                                <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxVesselTab2'>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="height: 25px;">
                    </div>
                     <div class="body-devide">
                        <ul>
                            <li>
                            <div id="PSC_NCR_MonthlyChart" style="width: 100%; height: 300px; float: left;">
                            </div>
                            </li>
                            <li>&nbsp; </li>
                            <li>
                            <div style="width: 100%; overlow: hidden; float: left;">
                                <div id="PSC_NCR_MonthlyGrid">
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div style="height: 50px;">
                    </div>
                     <div class="body-devide">
                        <ul>
                            <li>
                            <div id="PSC_Jobs_MonthlyChart" style="width: 100%; height: 300px; float: left;">
                            </div>
                           </li>
                            <li>&nbsp;</li>
                            <li>
                                <div id="PSC_Jobs_MonthlyGrid">
                                </div>
                             </li>
                        </ul>
                    </div>
                </div>
            </li>
            <%--<div class="iconse">export </div>--%>
        </ul>
    </div>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">PSC JOBS</div>
    <div style="height: 20px;"></div>
    
    <table border="0" width="100%">
        <tr>
            <td style="width: 40px;">
            </td>
            <td style="font-size: 14px; width: 50px;">
                <b>Years:</b>
            </td>
            <td style="width: 150px;">
                <div style='float: left; width: 50%; background-color: #f9f9f9; margin-right: 50px;'
                    id='jqxYear'>
                </div>
            </td>
            <td style="font-size: 14px; width: 100px;">
                <b>Vessel Name:</b>
            </td>
            <td>
                <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxVessel'>
                </div>
            </td>
        </tr>
    </table>
    <div>
        <div style="height: 20px;">
        </div>
        <div style="overflow:hidden;">
            <div id='jqxWidget' style=" width:48%;  float: right;">
                <div id="jqxgrid" >
                </div>
            </div>
            <div style="width: 4%;">
            </div>
            <div id="chartContainer" style="width: 48%; height: 300px; float: left; ">
            </div>
        </div>
    </div>
</asp:Content>--%>
