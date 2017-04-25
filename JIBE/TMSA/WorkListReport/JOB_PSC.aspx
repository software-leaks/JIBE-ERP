<%@ Page Title="PSC JOBS" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JOB_PSC.aspx.cs" Inherits="TMSA_WorklistReport_JOB_PSC" %>

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
            height:850px; 
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
    <script type="text/javascript">
        $(document).ready(function () {
            BindRank();
            BindYear();
            showGrid();
            showChart();

            var isYearChecked = false;
            $('#jqxYear').on('checkChange', function (event) {
                isYearChecked = true;
            });

            $('#jqxYear').on('close', function (event) {
                 if (isYearChecked == true) {
                     if (ValidateYear() && ValidateVessel()) {
                        showGrid();
                        showChart();
                    }
                }
                isYearChecked = false;
            });

            var isVesselChecked = false;
            $('#jqxVessel').on('checkChange', function (event) {
                isVesselChecked = true;
            });

            $('#jqxVessel').on('close', function (event) {
                if (isVesselChecked == true) {
                    if (ValidateYear() && ValidateVessel()) {
                        showGrid();
                        showChart();
                    }
                }
                isVesselChecked = false;
            });
        });
    </script>
    <%--Method to display Vessel Name --%>
    <script type="text/javascript">
        
         function BindRank() {
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
             $("#jqxVessel").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Vessel_Name", valueMember: "Vessel_Id", selectedIndex: 1, width: '200', height: '25' });
             $("#jqxVessel").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
             $("#jqxVessel").jqxDropDownList('checkAll');
             SelectDeselect("#jqxVessel");

             $('#jqxVessel').on('change', function (event) {

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
        function BindYear()
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
            $("#jqxYear").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter,displayMember: "YEAR", valueMember: "YEAR", selectedIndex: 1, width: '200', height: '25' });
           // $("#jqxYear").jqxDropDownList('checkAll');
           $('#jqxYear').jqxDropDownList('checkIndex',0);
            $('#jqxYear').jqxDropDownList('checkIndex',1);
            $("#jqxYear").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            SelectDeselect("#jqxYear");
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
                success: showGrid_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });
        }
            
        function showGrid_OnSuccess(data, status, jqXHR) {
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
            $("#jqxgrid").jqxGrid(
                {
                    enablehover:false,
                    autoheight: true,
                    width:1000,
                    source: dataAdapter,
                    sortable: true,
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
            success: showChart_OnSuccess,
            error: function () {
                alert("An error occured");
            }

        });
    }

    function showChart_OnSuccess(data, status, jqXHR) {                   
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
                title: '',
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
            $('#chartContainer').jqxChart(settings);
        }

    function ValidateVessel() { //Method to validate Vessel dropdown
        var checkedItems = "";
        var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
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


    function ValidateYear() {   //Method to validate YEAR dropdown
        var checkedYears = "";
        var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
        $.each(yearItems, function (index) {
            checkedYears += this.value + ",";
        });
        var value = checkedYears.replace(/,\s*$/, "");

        value = value.split(",");
        if (value.length < 2) {
            alert('Minimum 2 Years should be selected!');
            return false;
        }
        return true;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
</asp:Content>

