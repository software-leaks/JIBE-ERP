<%@ Page Title="Near Misses" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Near_Misses.aspx.cs" Inherits="TMSA_WorklistReport_Near_Misses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <link href="../../jqxWidgets/Controls/styles/jqx.base_New.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxlistbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdropdownlist _KPI.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcheckbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcombobox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxmenu.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxpanel.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcheckbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxlistbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxbuttons.js"></script>
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
            BindVessel("#jqxVessel");
            BindYear("#jqxYear");
            showGrid();
            showChart();
            showTotalPieChart();


            $("input[name=tabs]:radio").click(function () {
                if ($(this).val() === 'tab2') {
                    BindYear("#jqxYearTab2");
                    BindVessel("#jqxVesselTab2");
                    showPerYearPieChart();

                }
            });

            var isYearChecked = false;
            $('#jqxYear').on('checkChange', function (event) {
                isYearChecked = true;
            });

            $('#jqxYear').on('close', function (event) {
                if (isYearChecked == true) {
                    if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel")) {
                        showGrid();
                        showChart();
                        showTotalPieChart();
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
                        showGrid();
                        showChart();
                        showTotalPieChart();
                    }
                }
                isVesselChecked = false;
            });

            $('#jqxYearTab2').on('select', function (event) {
                showPerYearPieChart();               
            });

        });
       

    </script>
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
             $(vesselControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Vessel_Name", valueMember: "Vessel_Id", selectedIndex: 0, width: '200', height: '25' });
             $(vesselControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
             $(vesselControl).jqxDropDownList('checkAll');
             SelectDeselect(vesselControl);            
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
                $(yearControl).jqxDropDownList({ autoOpen: true, checkboxes: false, source: dataAdapter,displayMember: "YEAR", valueMember: "YEAR", selectedIndex: 0, width: '200', height: '25' });                
            }
            SelectDeselect(yearControl);
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
//            showGrid();
//            showChart();
//            showPerYearPieChart();
//            showTotalPieChart();
            });//Logic to implement Select All ends here            
        }
<%-- Method to bind Gridview on the basis of YEAR and VESSEL values from dropdown--%>
        function showGrid() {
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

            Object.Type = "Others";
            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');
            Object.Years = checkedYears.replace(/^,|,$/g,'');

            url = "../../KPIService.svc/GetVesselCountNearMisses";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showGrid_OnSuccess,
                error: function () {
                    alert("An error has occured");
                }

            });
        }
            
        function showGrid_OnSuccess(data, status, jqXHR) {
        //Assign CSS to grid header
             var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value.replace(",", ".") + '</div>';
            }
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
                    itemSeries["text"] = this.value.replace(",", ".");
                    itemSeries["dataField"] = this.value.replace(",", ".");
                    itemSeries["width"] = "6%";
                    //itemSeries["cellsformat"] = 'd2';
                    itemSeries["renderer"] = columnsrenderer;
                    itemSeries["cellsrenderer"] = cellsrenderer;
                    columnsArray.push(itemSeries);
                }
            });

            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });


//            //Create a dynamic array of grid column name
//            var dataFieldArray = [];
//            arrayItem = {};
//            arrayItem["name"] = 'VESSEL_Name';
//            arrayItem["type"] = 'number';
//            dataFieldArray.push(arrayItem);         


//            
//            var yearVal = $("#jqxYear").jqxDropDownList('getCheckedItems');
//            $.each(yearVal, function (index){
//            if(this.value!=0)
//            {
//                arrayItem = {};
//                arrayItem["name"]=this.value;
//                arrayItem["type"]='float';
//                dataFieldArray.push(arrayItem);
//            }
//            });


            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                      //  datafields: dataFieldArray,
                        datatype: "json",
                        localdata: data,
                        id: 'id',
                        url: url
                    };
           
            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#jqxgrid").jqxGrid(
                {
                    enablehover:true,
                    autoheight: true,
                    width:1000,
                    source: dataAdapter,
                    sortable: true,
                 //   filterable: true,
                    altrows:true,
                    columns : columnsArray
              
                });
        }

    <%-- Method to bind Chart/Graph on the basis of YEAR and VESSEL values from dropdown--%>
    function showChart() {
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

        Object1.Type = "Others";                //Pass type to service
        Object1.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');       //Pass vesselID to service
        Object1.Years = checkedYears.replace(/^,|,$/g,'');            //Pass year to service


        url = "../../KPIService.svc/GetChartVesselCountNearMisses";

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(Object1),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            async: false,
            success: [showChart_OnSuccess,showColumnChart_OnSuccess],
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showChart_OnSuccess(data, status, jqXHR) {         //Success method to display line chart     
    
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
            itemAvg["dataField"] = 'Avg';
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
                title: 'Near Misses - Yearly, Per Vessel',
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 20, top: 5, right: 50, bottom: 5 },
                titlePadding: { left: 10, top: 20, right: 0, bottom: 20 },
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
            $('#chartContainer').jqxChart(settings);
        }


        function showColumnChart_OnSuccess(data, status, jqXHR) {   //Success method to display coulmn chart
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
            itemAvg["dataField"] = 'Avg';
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
                title: 'Near Misses - Yearly, Per Vessel',
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 20, top: 5, right: 50, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 20 },
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
                    type: 'column',
                    toolTipFormatFunction: toolTipCustomFormatFn1,
                    displayValueAxis: true,
                    series:seriesArray2                        
                }
            ]
            };
            $('#columnChartContainer').jqxChart(settings);
        }



//Method to pouplate piechart for Near Misses per vessle for single year
function showPerYearPieChart() {

        //Fetch the list of selected vessel
        var checkedItems = "";
        var items = $("#jqxVesselTab2").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            if (this.value != 0)
            checkedItems += this.value + ",";                
        });

        if (checkedItems.charAt(0) == '0')      //remove '0' from the string when ALL is selected.
        checkedItems = checkedItems.substring(1);
            
        var item2 = $("#jqxYearTab2").jqxDropDownList('getSelectedItem');
        var checkedYears = item2.value;

        //var selectedYears = checkedYears.substring(0, checkedYears.Length - 1);
        var Object1 = {
            "Type": "",
            "Vessel_IDs": "",
            "Years": ""
        }

        Object1.Type = "Others";                //Pass type to service
        Object1.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');       //Pass vesselID to service
        Object1.Years = checkedYears.replace(/^,|,$/g,'');            //Pass year to service


        url = "../../KPIService.svc/GetPerVesselPerYearNearMissesForPieChart";

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(Object1),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            async: false,
            success: showPerYearPieChart_OnSuccess,
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showPerYearPieChart_OnSuccess(data, status, jqXHR) {         //Success method to display PIE chart
        var source =
            {
                datatype: "json", 
                datafields: [
                { name: 'Vessel_Name' },
                { name: 'VALUE' }
            ],
                localdata: data
            };
        var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });

        var toolTipPie = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                return serie.displayText+':'+ xAxisValue + "&nbsp;</br>" + serie.dataField+':'+ value+'&nbsp;';
            }

        var settings = {
            title: "Near Misses - Per Year, All Vessel",
            description: "",
            enableAnimations: true,
            showLegend: true,
            showBorderLine: false,
            padding: { left: 5, top: 5, right: 5, bottom: 5 },
            titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
            source: dataAdapter,
            colorScheme: 'scheme03',
            seriesGroups:
                [
                    {
                        type: 'pie',
                        showLabels: true,
                        series:
                            [
                                {
                                    dataField: 'VALUE',
                                    displayText: 'Vessel_Name',
                                    labelRadius: 50,
                                    initialAngle: 15,
                                    radius: 90,
                                    centerOffset: 0
                                   // toolTipFormatFunction: toolTipPie
                                 //   formatSettings: { sufix: '' }
                                }
                            ]
                    }
                ]
        };
        $('#jqxPerVesselPieChart').jqxChart(settings);
    }
//Method to pouplate piechart for Total Near Misses Per Year 
    function showTotalPieChart() {

        //Fetch the list of selected vessel
        var checkedItems = "";
        var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            if (this.value != 0)
            checkedItems += this.value + ",";                
        });

        if (checkedItems.charAt(0) == '0')      //remove '0' from the string when ALL is selected.
        checkedItems = checkedItems.substring(1);
            


        var checkedYears = "";
        var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
        $.each(yearItems, function (index) {
            if (this.value != 0)
                checkedYears += this.value + ",";
        });
        if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
            checkedYears = checkedYears.substring(1);


        //var selectedYears = checkedYears.substring(0, checkedYears.Length - 1);
        var Object1 = {
            "Type": "",
            "Vessel_IDs": "",
            "Years": ""
        }

        Object1.Type = "Others";                //Pass type to service
        Object1.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');       //Pass vesselID to service
        Object1.Years = checkedYears.replace(/^,|,$/g,'');            //Pass year to service



        url = "../../KPIService.svc/GetPerYearNearMissesForPieChart";

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(Object1),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processdata: true,
            async: false,
            success: showTotalPieChart_OnSuccess,
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showTotalPieChart_OnSuccess(data, status, jqXHR) {         //Success method to display PIE chart
    var source =
            {
                datatype: "json", 
                datafields: [
                { name: 'YEAR' },
                { name: 'VALUE' }
            ],
                localdata: data
            };
        var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });

        var settings = {
            title: "Near Misses - Yearly, All Vessel",
            description: "",
            enableAnimations: true,
            showLegend: true,
            showBorderLine: false,
            padding: { left: 0, top: 55, right: 0, bottom: 0 },
            titlePadding: { left: 0, top: 0, right: 0, bottom: 0 },
            source: dataAdapter,
            colorScheme: 'scheme03',
            seriesGroups:
                [
                    {
                        type: 'pie',
                        showLabels: true,
                        series:
                            [
                                {
                                    dataField: 'VALUE',
                                    displayText: 'YEAR',
                                    labelRadius: 0,
                                    initialAngle: 15,
                                    radius: 90,
                                    centerOffset: 0
                                 //   formatSettings: { sufix: '' }
                                }
                            ]
                    }
                ]
        };
        $('#jqxTotalPieChart').jqxChart(settings);
        
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
//        var value = checkedYears.replace(/,\s*$/, "");

//        value = value.split(",");
//        if (value.length < 2) {
//            alert('Minimum 2 Years should be selected!');
//            return false;
//        }
        var value = checkedYears;
        if (value == "") {
            alert('Atleast 1 year should be selected!');
            return false;
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
                    Near Misses - Yearly</label>
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
                    <%--<div style="padding-top: 20px; vertical-align:middle; padding-bottom:30px; font-size:14px; font-weight:bold;">Line chart - Near Misses per Year Per Vessel</div>--%>

                    <div class="body-devide">
                        <ul>
                            <li>
                                <div id="chartContainer" style="width:100%; height: 300px; "></div>
                            </li>
                            <li>&nbsp;</li>
                            <li>
                                <div id='jqxWidget' style="width:100%; ">
                                    <div id="jqxgrid"></div>
                                </div>
                            </li>
                        </ul>
                    </div>

                    <div style="height: 50px;"></div>

                    <div class="body-devide">
                        <ul>
                            <li>
                                <div id="columnChartContainer" style="width:100%; height: 300px; float: left;"></div>
                            </li>
                            <li>&nbsp;</li>
                            <li>
                                <div id="jqxTotalPieChart" style="width:600px; height: 400px; float: left;font-weight: bold; "> </div>
                             </li>
                        </ul>
                    </div>
                </div>
            </li>
            <li>
                <input type="radio" name="tabs" id="tab2" value="tab2">
                <label for="tab2">
                    Near Misses - Per Year</label>
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
                                <%-- <b>Vessel Name:</b>--%>
                            </td>
                            <td>
                                <div style='float: left; visibility: hidden; width: 50%; background-color: #f9f9f9;'
                                    id='jqxVesselTab2'>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <div style="height: 20px;">
                        </div>
                        <div style="overflow: hidden;">
                            <div id="jqxPerVesselPieChart" style="width: 600px; height: 400px; float: left; vertical-align: top;">
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <%--<div class="iconse">export </div>--%>
        </ul>
    </div>
</asp:Content>
