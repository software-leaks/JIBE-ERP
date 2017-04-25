<%@ Page Title="Observations by Vessel" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Observations_by_Vessel.aspx.cs" Inherits="TMSA_VettingReports_Observations_by_Vessel" %>

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
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxtooltip.js"></script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        
        
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
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            BindVessel("#jqxVessel");
            BindYear("#jqxYear");
            BindVettingType();
            BindCategory();
            BindObservationType();
            BindFleet();

            showVesselGridChart();
            showFleetGridChart();
            showVesselPieChart();
            showFleetPieChart();
            showVesselCntByCategoryGrid();
            showFleetCntByCategoryGrid();

            SelectDeselect("#jqxVessel");
            SelectDeselect("#jqxYear");
            SelectDeselect('#jqxVettingType');
            SelectDeselect('#jqxObservationType');
            SelectDeselect('#jqxCategory');
            SelectDeselect('#jqxFleet');

            GetHiddenValues();


            var isYearChecked = false;
            $('#jqxYear').on('checkChange', function (event) {
                isYearChecked = true;
            });

            $('#jqxYear').on('close', function (event) {
                if (isYearChecked == true) {
                    //if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear")){
                        showFleetGridChart();
                        showFleetPieChart();
                        showVesselPieChart();
                        showVesselGridChart();
                        showVesselCntByCategoryGrid();
                        showFleetCntByCategoryGrid();
                        GetHiddenValues();
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
                   // if (ValidateYear("#jqxVessel") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel")) {
                        showFleetGridChart();
                        showFleetPieChart();
                        showVesselPieChart();
                        showVesselGridChart();
                        showVesselCntByCategoryGrid();
                        showFleetCntByCategoryGrid();
                        GetHiddenValues();
                    }
                }
                isVesselChecked = false;
            });

            var isVetTypeChecked = false;
            $('#jqxVettingType').on('checkChange', function (event) {
                isVetTypeChecked = true;
            });

            $('#jqxVettingType').on('close', function (event) {
                if (isVetTypeChecked == true) {
                 //   if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear") && ValidateVettingType()) {
                        showFleetGridChart();
                        showFleetPieChart();
                        showVesselPieChart();
                        showVesselGridChart();
                        showVesselCntByCategoryGrid();
                        showFleetCntByCategoryGrid();
                        GetHiddenValues();
                    }
                }
                isVetTypeChecked = false;
            });

            var isObvChecked = false;
            $('#jqxObservationType').on('checkChange', function (event) {
                isObvChecked = true;
            });

            $('#jqxObservationType').on('close', function (event) {
                if (isObvChecked == true) {
                    //if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear") && ValidateObvType()) {
                        showFleetGridChart();
                        showFleetPieChart();
                        showVesselPieChart();
                        showVesselGridChart();
                        showVesselCntByCategoryGrid();
                        showFleetCntByCategoryGrid();
                        GetHiddenValues();
                    }
                }
                isObvChecked = false;
            });

            var isCatChecked = false;
            $('#jqxCategory').on('checkChange', function (event) {
                isCatChecked = true;
            });

            $('#jqxCategory').on('close', function (event) {
                if (isCatChecked == true) {
                   // if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear") && ValidateCategory()) {
                        showFleetGridChart();
                        showFleetPieChart();
                        showVesselPieChart();
                        showVesselGridChart();
                        showVesselCntByCategoryGrid();
                        showFleetCntByCategoryGrid();
                        GetHiddenValues();
                    }
                }
                isCatChecked = false;
            });

            var isFleetChecked = false;
            $('#jqxFleet').on('checkChange', function (event) {
                isCatChecked = true;
            });

            $('#jqxFleet').on('close', function (event) {
                if (isCatChecked == true) {
                    //if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear") && ValidateFleet()) {
                        showFleetGridChart();
                        showFleetPieChart();
                        showVesselPieChart();
                        showVesselGridChart();
                        showVesselCntByCategoryGrid();
                        showFleetCntByCategoryGrid();
                        GetHiddenValues();
                    }
                }
                isCatChecked = false;
            });

        });
    </script>
    <script type="text/javascript">
        
        function BindVessel(vesselControl) {
            var userCompID = '<%= Session["USERCOMPANYID"]%>';
             var url = "../../KPIService.svc/GetVesselList/usercompanyid/1";
             //var url = "../../KPIService.svc/GetVesselList/usercompanyid/"+userCompID;
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
            $(vesselControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Vessel_Name", valueMember: "Vessel_Id", selectedIndex: 5, width: '200', height: '25' });
            $(vesselControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $(vesselControl).jqxDropDownList('checkAll');
            //SelectDeselect(vesselControl);            
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
            $(yearControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            //$(yearControl).jqxDropDownList('checkAll');
        }
        else if(yearControl== '#jqxYearTab2')
        {
            $(yearControl).jqxDropDownList({ autoOpen: true, checkboxes: false,source: dataAdapter,displayMember: "YEAR", valueMember: "YEAR", selectedIndex: 0, width: '200', height: '25' });                
        }
           
        //SelectDeselect(yearControl);
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

    function GetHiddenValues()
    {
        var checkedItems = "";
        var items = $("#jqxYear").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        document.getElementById("ctl00_MainContent_hdnYear").value = checkedItems;      //Fetch list of year and store in hidden field

        debugger;
        var checkedItems = "";
        var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        document.getElementById("ctl00_MainContent_hdnVessel").value = checkedItems;      //Fetch list of vessel and store in hidden field

        var checkedItems = "";
        var items = $("#jqxVettingType").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        document.getElementById("ctl00_MainContent_hdnVettingType").value = checkedItems;      //Fetch list of VettingType and store in hidden field

        var checkedItems = "";
        var items = $("#jqxObservationType").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        document.getElementById("ctl00_MainContent_hdnObvType").value = checkedItems;      //Fetch list of Observation Type and store in hidden field


        var checkedItems = "";
        var items = $("#jqxCategory").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        document.getElementById("ctl00_MainContent_hdnCategory").value = checkedItems;      //Fetch list of vessel and store in hidden field

        var checkedItems = "";
        var items = $("#jqxCategory").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        document.getElementById("ctl00_MainContent_hdnCategory").value = checkedItems;      //Fetch list of category and store in hidden field

        var checkedItems = "";
        var items = $("#jqxFleet").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        document.getElementById("ctl00_MainContent_hdnFleet").value = checkedItems;      //Fetch list of category and store in hidden field
    }
    </script>
    <script type="text/javascript">
        function BindVettingType() {
            var url = "../../KPIService.svc/GetVettingType";
            var source = {
                datatype: "json",
                datafields: [
                    { name: 'Vetting_Type_Name' },
                    { name: 'Vetting_Type_ID' }
                ],
                id: 'id',
                url: url,
                async: false
            };
            
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxVettingType").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Vetting_Type_Name", valueMember: "Vetting_Type_ID", selectedIndex: 5, width: '200', height: '25' });
            $("#jqxVettingType").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $("#jqxVettingType").jqxDropDownList('checkAll');
        }
    </script>

    <script type="text/javascript">
        function BindObservationType() {
            var url = "../../KPIService.svc/GetObservationType";
            var source = {
                datatype: "json",
                datafields: [
                    { name: 'ObservationType_Name' },
                    { name: 'ObservationType_ID' }
                ],
                id: 'id',
                url: url,
                async: false
            };
            
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxObservationType").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "ObservationType_Name", valueMember: "ObservationType_ID", selectedIndex: 5, width: '200', height: '25' });
            $("#jqxObservationType").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $("#jqxObservationType").jqxDropDownList('checkAll');
        }
    </script>
    <script type="text/javascript">
        function BindCategory() {
            var url = "../../KPIService.svc/GetCategory";
            var source = {
                datatype: "json",
                datafields: [
                    { name: 'Category_Name' },
                    { name: 'Category_ID' }
                ],
                id: 'id',
                url: url,
                async: false
            };
            
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxCategory").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Category_Name", valueMember: "Category_ID", selectedIndex: 5, width: '200', height: '25' });
            $("#jqxCategory").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $("#jqxCategory").jqxDropDownList('checkAll');
        }
    </script>
     <script type="text/javascript">
         function BindFleet() {
         debugger
        //     var userCompID = '<%= Session["USERCOMPANYID"]%>';
         //    var url = "../../KPIService.svc/GetFleetList/CompanyID/" + userCompID;
             var url = "../../KPIService.svc/GetFleetList/CompanyID/1";

             var source = {
                 datatype: "json",
                 datafields: [
                    { name: 'Flt_Name' },
                    { name: 'Fleet_ID' }
                ],
                 id: 'id',
                 url: url,
                 async: false
             };
             debugger
             var dataAdapter = new $.jqx.dataAdapter(source);
             // Create a jqxDropDownList
             $("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Flt_Name", valueMember: "Fleet_ID", selectedIndex: 5, width: '200', height: '25' });
             $("#jqxFleet").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
             $("#jqxFleet").jqxDropDownList('checkAll');
         }
    </script>
    <script type="text/javascript">         
<%-- Method to bind Incident count to Gridview on the basis of per year and all category--%>
        function showVesselGridChart() {
           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);
           

            //Fetch the list of selected year
            var checkedYears = "";
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

            //Fetch Vetting Type IDs
            var vettingTypeID = "";
            var vettingCheckedItems = $("#jqxVettingType").jqxDropDownList('getCheckedItems');
            
            $.each(vettingCheckedItems, function(index) {
                if (this.value != 0)
                vettingTypeID += this.value+",";
            });

            if(vettingTypeID.charAt(0) == '0')
                vettingTypeID = vettingCheckedItems.substring(1);


            //Fetch Category IDs
            var categoryID = "";
            var categoryCheckedItems = $("#jqxCategory").jqxDropDownList('getCheckedItems');
            
            $.each(categoryCheckedItems, function(index){
                if (this.value != 0)
                categoryID += this.value+",";
            });

            if(categoryID.charAt(0) == '0')
                categoryID = categoryCheckedItems.substring(1);

            //Fetch Observation Type IDs
            var obvID = "";
            var obvCheckedItems = $("#jqxObservationType").jqxDropDownList('getCheckedItems');
            
            $.each(obvCheckedItems, function(index){
                if (this.value != 0)
                obvID += this.value+",";
            });

            if(obvID.charAt(0) == '0')
                obvID = obvCheckedItems.substring(1);

            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');
            
            $.each(fltCheckedItems, function(index){
                if (this.value != 0)
                fltID += this.value+",";
            });

            if(fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);

            var Object = {
                "Vessel_IDs": "",
                "Years": "",
                "VettingTypeID":"",
                "CategoryID":"",
                "ObvTypeID":"",
                "FleetID":""
            }

            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');
            Object.Years = checkedYears.replace(/^,|,$/g,'');
            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g,'');
            Object.CategoryID = categoryID.replace(/^,|,$/g,'');
            Object.ObvTypeID =obvID.replace(/^,|,$/g,'');
            Object.FleetID = fltID.replace(/^,|,$/g,'');


            //url = "../../KPIService.svc/GetMultipleVesselInjuryIncidentCount";
            url = "../../KPIService.svc/GetObservationsByVesselCnt";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: [showVesselGrid_OnSuccess],
                error: function () {
                    alert("An error has occured");
                }

            });
        }
    </script>
    <script type="text/javascript">
        function showVesselGrid_OnSuccess(data, status, jqXHR) {

            //Assign CSS to grid header
            var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }
            //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }
            //Assign tootltip to column header
            var tooltiprenderer = function (element) {
                $(element).jqxTooltip({ position: 'mouse', content: $(element).text() });
            }
            var columnsArray = [];
            itemSeries = {};
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            $.each(len, function (key, value) {                   //Fetch the list of vessel name from json
                itemSeries = {};
                if (key != 'Year' && key != 'Total' && key != 'Avg') {
                    itemSeries["dataField"] = key.replace(",", ".");
                    itemSeries["displayText"] = key.replace(",", ".");
                    columnsArray.push(itemSeries);
                }
            });

            //Create a dynamic array of grid column name
            var dataFieldArray = [];

            var yearVal = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(yearVal, function (index) {
                if (this.value != 0) {
                    arrayItem = {};
                    arrayItem["name"] = this.label;
                    arrayItem["type"] = 'number';
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
                        id: 'Id',
                        url: url
                    };

            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#jqxVesselGrid").jqxGrid(
                {
                    enablehover: false,
                    autoheight: true,
                    width: '100%',
                    source: dataAdapter,
                    sortable: true,
                    altrows: true,
                    columns: columnsArray
                });
        }
    </script>
    <script type="text/javascript">

        //Method to pouplate single year piechart for Incident
        function showVesselPieChart() {

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
            var item2 = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(item2, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });

            //Fetch Vetting Type IDs
            var vettingTypeID = "";
            var vettingCheckedItems = $("#jqxVettingType").jqxDropDownList('getCheckedItems');

            $.each(vettingCheckedItems, function (index) {
                if (this.value != 0)
                    vettingTypeID += this.value + ",";
            });

            if (vettingTypeID.charAt(0) == '0')
                vettingTypeID = vettingCheckedItems.substring(1);


            //Fetch Category IDs
            var categoryID = "";
            var categoryCheckedItems = $("#jqxCategory").jqxDropDownList('getCheckedItems');

            $.each(categoryCheckedItems, function (index) {
                if (this.value != 0)
                    categoryID += this.value + ",";
            });

            if (categoryID.charAt(0) == '0')
                categoryID = categoryCheckedItems.substring(1);

            //Fetch Observation Type IDs
            var obvID = "";
            var obvCheckedItems = $("#jqxObservationType").jqxDropDownList('getCheckedItems');

            $.each(obvCheckedItems, function (index) {
                if (this.value != 0)
                    obvID += this.value + ",";
            });

            if (obvID.charAt(0) == '0')
                obvID = obvCheckedItems.substring(1);

            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    fltID += this.value + ",";
            });

            if (fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);

            var Object1 = {
                "Vessel_IDs": "",
                "Years": "",
                "VettingTypeID": "",
                "CategoryID": "",
                "ObvTypeID": "",
                "FleetID": ""
            }

            Object1.Vessel_IDs = checkedItems.replace(/^,|,$/g, '');
            Object1.Years = checkedYears.replace(/^,|,$/g, '');
            Object1.VettingTypeID = vettingTypeID.replace(/^,|,$/g, '');
            Object1.CategoryID = categoryID.replace(/^,|,$/g, '');
            Object1.ObvTypeID = obvID.replace(/^,|,$/g, '');
            Object1.FleetID = fltID.replace(/^,|,$/g, '');

            
            url = "../../KPIService.svc/GetObservationsByVesselCntForPieChart";
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showVesselPieChart_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });

        }
    </script>
    <script type="text/javascript">
        function showVesselPieChart_OnSuccess(data, status, jqXHR) {         //Success method to display PIE chart

            var source =
            {
                datatype: "json",
                datafields: [
                { name: 'Vessel_Name' },
                { name: 'Rec_Count' }
            ],
                localdata: data
            };
            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });

            var settings = {
                title: "Observations by Vessel",
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
                                    dataField: 'Rec_Count',
                                    displayText: 'Vessel_Name',
                                    labelRadius: 50,
                                    initialAngle: 15,
                                    radius: 90,
                                    centerOffset: 0
                                    //   formatSettings: { sufix: '' }
                                }
                            ]
                    }
                ]
            };
            $('#jqxVesselPieChart').jqxChart(settings);

        }


    </script>
    <script type="text/javascript">

        //Method to pouplate single year piechart for Incident
        function showFleetPieChart() {

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
            var item2 = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(item2, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });


            //Fetch Vetting Type IDs
            var vettingTypeID = "";
            var vettingCheckedItems = $("#jqxVettingType").jqxDropDownList('getCheckedItems');

            $.each(vettingCheckedItems, function (index) {
                if (this.value != 0)
                    vettingTypeID += this.value + ",";
            });

            if (vettingTypeID.charAt(0) == '0')
                vettingTypeID = vettingCheckedItems.substring(1);


            //Fetch Category IDs
            var categoryID = "";
            var categoryCheckedItems = $("#jqxCategory").jqxDropDownList('getCheckedItems');

            $.each(categoryCheckedItems, function (index) {
                if (this.value != 0)
                    categoryID += this.value + ",";
            });

            if (categoryID.charAt(0) == '0')
                categoryID = categoryCheckedItems.substring(1);

            //Fetch Observation Type IDs
            var obvID = "";
            var obvCheckedItems = $("#jqxObservationType").jqxDropDownList('getCheckedItems');

            $.each(obvCheckedItems, function (index) {
                if (this.value != 0)
                    obvID += this.value + ",";
            });

            if (obvID.charAt(0) == '0')
                obvID = obvCheckedItems.substring(1);

            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    fltID += this.value + ",";
            });

            if (fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);

            var Object1 = {
                "Vessel_IDs": "",
                "Years": "",
                "VettingTypeID": "",
                "CategoryID": "",
                "ObvTypeID": "",
                "FleetID": ""
            }

            Object1.Vessel_IDs = checkedItems.replace(/^,|,$/g, '');
            Object1.Years = checkedYears.replace(/^,|,$/g, '');
            Object1.VettingTypeID = vettingTypeID.replace(/^,|,$/g, '');
            Object1.CategoryID = categoryID.replace(/^,|,$/g, '');
            Object1.ObvTypeID = obvID.replace(/^,|,$/g, '');
            Object1.FleetID = fltID.replace(/^,|,$/g, '');




            url = "../../KPIService.svc/GetObservationsByFleetCntForPieChart";
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showFleetPieChart_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });

        }
    </script>
    <script type="text/javascript">
        function showFleetPieChart_OnSuccess(data, status, jqXHR) {         //Success method to display PIE chart

            var source =
            {
                datatype: "json",
                datafields: [
                { name: 'Fleet_Name' },
                { name: 'Rec_Count' }
            ],
                localdata: data
            };
            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });

            var settings = {
                title: "Observations by Fleet",
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
                                    dataField: 'Rec_Count',
                                    displayText: 'Fleet_Name',
                                    labelRadius: 50,
                                    initialAngle: 15,
                                    radius: 90,
                                    centerOffset: 0
                                    //   formatSettings: { sufix: '' }
                                }
                            ]
                    }
                ]
            };
            $('#jqxFleetPieChart').jqxChart(settings);

        }


    </script>
    <script type="text/javascript">         
<%-- Method to bind Incident count to Gridview on the basis of per year and all category--%>
        function showFleetGridChart() {
           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);
           

            //Fetch the list of selected year
            var checkedYears = "";
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);

            //Fetch Vetting Type IDs
            var vettingTypeID = "";
            var vettingCheckedItems = $("#jqxVettingType").jqxDropDownList('getCheckedItems');
            
            $.each(vettingCheckedItems, function(index) {
                if (this.value != 0)
                vettingTypeID += this.value+",";
            });

            if(vettingTypeID.charAt(0) == '0')
                vettingTypeID = vettingCheckedItems.substring(1);


            //Fetch Category IDs
            var categoryID = "";
            var categoryCheckedItems = $("#jqxCategory").jqxDropDownList('getCheckedItems');
            
            $.each(categoryCheckedItems, function(index){
                if (this.value != 0)
                categoryID += this.value+",";
            });

            if(categoryID.charAt(0) == '0')
                categoryID = categoryCheckedItems.substring(1);

            //Fetch Observation Type IDs
            var obvID = "";
            var obvCheckedItems = $("#jqxObservationType").jqxDropDownList('getCheckedItems');
            
            $.each(obvCheckedItems, function(index){
                if (this.value != 0)
                obvID += this.value+",";
            });

            if(obvID.charAt(0) == '0')
                obvID = obvCheckedItems.substring(1);

            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');
            
            $.each(fltCheckedItems, function(index){
                if (this.value != 0)
                fltID += this.value+",";
            });

            if(fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);

            var Object = {
                "Vessel_IDs": "",
                "Years": "",
                "VettingTypeID":"",
                "CategoryID":"",
                "ObvTypeID":"",
                "FleetID":""
            }

            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');
            Object.Years = checkedYears.replace(/^,|,$/g,'');
            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g,'');
            Object.CategoryID = categoryID.replace(/^,|,$/g,'');
            Object.ObvTypeID =obvID.replace(/^,|,$/g,'');
            Object.FleetID = fltID.replace(/^,|,$/g,'');

            //url = "../../KPIService.svc/GetMultipleVesselInjuryIncidentCount";
            url = "../../KPIService.svc/GetObservationsByFleetCnt";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: [showFleetGridChart_OnSuccess],
                error: function () {
                    alert("An error has occured");
                }

            });
        }
    </script>
    <script type="text/javascript">
        function showFleetGridChart_OnSuccess(data, status, jqXHR) {

            //Assign CSS to grid header
            var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }
            //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }
            //Assign tootltip to column header
            var tooltiprenderer = function (element) {
                $(element).jqxTooltip({ position: 'mouse', content: $(element).text() });
            }
            var columnsArray = [];
            itemSeries = {};
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            $.each(len, function (key, value) {                   //Fetch the list of vessel name from json
                itemSeries = {};
                if (key != 'Year' && key != 'Total' && key != 'Avg') {
                    itemSeries["dataField"] = key.replace(",", ".");
                    itemSeries["displayText"] = key.replace(",", ".");
                    columnsArray.push(itemSeries);
                }
            });

            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                        datatype: "json",
                        localdata: data,
                        id: 'Id',
                        url: url
                    };

            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#jqxFleetGrid").jqxGrid(
                {
                    enablehover: false,
                    autoheight: true,
                    width: '100%',
                    source: dataAdapter,
                    sortable: true,
                    altrows: true,
                    columns: columnsArray
                });
        }
    </script>
    <script type="text/javascript">         
<%-- Method to bind Incident count to Gridview on the basis of per year and all category--%>
function showVesselCntByCategoryGrid() {
           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);
           

            //Fetch the list of selected year
            var checkedYears = "";
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);


            //Fetch Vetting Type IDs
            var vettingTypeID = "";
            var vettingCheckedItems = $("#jqxVettingType").jqxDropDownList('getCheckedItems');
            
            $.each(vettingCheckedItems, function(index) {
                if (this.value != 0)
                vettingTypeID += this.value+",";
            });

            if(vettingTypeID.charAt(0) == '0')
                vettingTypeID = vettingCheckedItems.substring(1);


            //Fetch Category IDs
            var categoryID = "";
            var categoryCheckedItems = $("#jqxCategory").jqxDropDownList('getCheckedItems');
            
            $.each(categoryCheckedItems, function(index){
                if (this.value != 0)
                categoryID += this.value+",";
            });

            if(categoryID.charAt(0) == '0')
                categoryID = categoryCheckedItems.substring(1);

            //Fetch Observation Type IDs
            var obvID = "";
            var obvCheckedItems = $("#jqxObservationType").jqxDropDownList('getCheckedItems');
            
            $.each(obvCheckedItems, function(index){
                if (this.value != 0)
                obvID += this.value+",";
            });

            if(obvID.charAt(0) == '0')
                obvID = obvCheckedItems.substring(1);

            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');
            
            $.each(fltCheckedItems, function(index){
                if (this.value != 0)
                fltID += this.value+",";
            });

            if(fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);

            var Object = {
                "Vessel_IDs": "",
                "Years": "",
                "VettingTypeID":"",
                "CategoryID":"",
                "ObvTypeID":"",
                "FleetID":""
            }

            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');
            Object.Years = checkedYears.replace(/^,|,$/g,'');
            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g,'');
            Object.CategoryID = categoryID.replace(/^,|,$/g,'');
            Object.ObvTypeID =obvID.replace(/^,|,$/g,'');
            Object.FleetID = fltID.replace(/^,|,$/g,'');

            url = "../../KPIService.svc/GetVesselObservationCntByCategory";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: [showVesselCntByCategoryGrid_OnSuccess,showVesselCatChart_OnSuccess],
                error: function () {
                    alert("An error has occured");
                }

            });
        }
    </script>
    <script type="text/javascript">
        function showVesselCntByCategoryGrid_OnSuccess(data, status, jqXHR) {

            //Assign CSS to grid header
            var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }
            //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }
            //Assign tootltip to column header
            var tooltiprenderer = function (element) {
                $(element).jqxTooltip({ position: 'mouse', content: $(element).text() });
            }
            var columnsArray = [];
            itemSeries = {};
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            $.each(len, function (key, value) {                   //Fetch the list of vessel name from json
                itemSeries = {};
                itemSeries["dataField"] = key.replace(",", ".");
                itemSeries["displayText"] = key.replace(",", ".");
                columnsArray.push(itemSeries);

            });


            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                        datatype: "json",
                        localdata: data,
                        id: 'Id',
                        url: url
                    };

            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#jqxCategoryGrid").jqxGrid(
                {
                    enablehover: false,
                    autoheight: true,
                    width: '100%',
                    source: dataAdapter,
                    sortable: true,
                    altrows: true,
                    columns: columnsArray
                });
        }
    </script>
    <script type="text/javascript">
      function showVesselCatChart_OnSuccess(data, status, jqXHR) {         //Success method to display 100% stacked column chart
        var source =
            {
                datatype: "json",
                localdata: data
            };
          var toolTipCustomFormatFn1 = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
              return 'Year:' + xAxisValue + "&nbsp;</br>" + serie.dataField + ':' + value.replace(",", ".") + '&nbsp;';
          }

          var dataAdapter = new $.jqx.dataAdapter(source);

            var seriesArray = [];            
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            $.each(len, function (key, value) {  
            itemArray = {};
            //Fetch the list of categories from json
                itemArray["dataField"] = key.replace(",", ".");
                itemArray["displayText"] = key.replace(",", ".");
                seriesArray.push(itemArray);
            });

          var settings = {
              title: 'Observations by Vessel',
              description: "",
              enableAnimations: true,
              borderLineColor: 'White',
              showLegend: true,
              padding: { left: 20, top: 0, right: 50, bottom: 5 },
              titlePadding: { left: 10, top: 0, right: 0, bottom: 20 },
              source: dataAdapter,              
              xAxis:
                {
                    dataField: 'Vessel',
                    valuesOnTicks: false
                },
                valueAxis:
                {
                    unitInterval: 10,
                    title: {text: ''},
                    tickMarks: {color: '#BCBCBC'},
                    gridLines: {color: '#BCBCBC'},
                    labels: {
                        horizontalAlignment: 'right', 
                        formatSettings: { sufix: '%'}
                    },
                },
              colorScheme: 'scheme01',
              columnSeriesOverlap: false,
               seriesGroups:
                    [
                        {
                            type: 'stackedcolumn100',
                            columnsGapPercent: 100,
                            seriesGapPercent: 5,
                            series: seriesArray
                        }
                    ]              
          };
          $('#jqxCatColumnChart').jqxChart(settings);
      }
    </script>
    <script type="text/javascript">         
<%-- Method to bind Incident count to Gridview on the basis of per year and all category--%>
function showFleetCntByCategoryGrid() {

           //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);
           

            //Fetch the list of selected year
            var checkedYears = "";
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });
            if (checkedYears.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
                checkedYears = checkedYears.substring(1);


            //Fetch Vetting Type IDs
            var vettingTypeID = "";
            var vettingCheckedItems = $("#jqxVettingType").jqxDropDownList('getCheckedItems');
            
            $.each(vettingCheckedItems, function(index) {
                if (this.value != 0)
                vettingTypeID += this.value+",";
            });

            if(vettingTypeID.charAt(0) == '0')
                vettingTypeID = vettingCheckedItems.substring(1);


            //Fetch Category IDs
            var categoryID = "";
            var categoryCheckedItems = $("#jqxCategory").jqxDropDownList('getCheckedItems');
            
            $.each(categoryCheckedItems, function(index){
                if (this.value != 0)
                categoryID += this.value+",";
            });

            if(categoryID.charAt(0) == '0')
                categoryID = categoryCheckedItems.substring(1);

            //Fetch Observation Type IDs
            var obvID = "";
            var obvCheckedItems = $("#jqxObservationType").jqxDropDownList('getCheckedItems');
            
            $.each(obvCheckedItems, function(index){
                if (this.value != 0)
                obvID += this.value+",";
            });

            if(obvID.charAt(0) == '0')
                obvID = obvCheckedItems.substring(1);

            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');
            
            $.each(fltCheckedItems, function(index){
                if (this.value != 0)
                fltID += this.value+",";
            });

            if(fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);

            var Object = {
                "Vessel_IDs": "",
                "Years": "",
                "VettingTypeID":"",
                "CategoryID":"",
                "ObvTypeID":"",
                "FleetID":""
            }

            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g,'');
            Object.Years = checkedYears.replace(/^,|,$/g,'');
            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g,'');
            Object.CategoryID = categoryID.replace(/^,|,$/g,'');
            Object.ObvTypeID =obvID.replace(/^,|,$/g,'');
            Object.FleetID=fltID.replace(/^,|,$/g,'');

            //url = "../../KPIService.svc/GetMultipleVesselInjuryIncidentCount";
            url = "../../KPIService.svc/GetFleetObservationCntByCategory";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: [showFleetCntByCategoryGrid_OnSuccess,showFleetCatChart_OnSuccess],
                error: function () {
                    alert("An error has occured");
                }

            });
        }
    </script>
    <script type="text/javascript">
        function showFleetCntByCategoryGrid_OnSuccess(data, status, jqXHR) {
            //Assign CSS to grid header
            var columnsrenderer = function (value) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;background-color:#d9d9d9;">' + value + '</div>';
            }
            //Fix for the issue on stage where decimal point is getting replaced by comma.
            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                return '<div style="text-align: left; padding-top: 5px; padding-left:8px; padding-bottom:5px; font-size:14px;">' + value.replace(",", ".") + '</div>';
            }
            //Assign tootltip to column header
            var tooltiprenderer = function (element) {
                $(element).jqxTooltip({ position: 'mouse', content: $(element).text() });
            }
            var columnsArray = [];
            itemSeries = {};
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            $.each(len, function (key, value) {                   //Fetch the list of vessel name from json
                itemSeries = {};
                itemSeries["dataField"] = key.replace(",", ".");
                itemSeries["displayText"] = key.replace(",", ".");
                columnsArray.push(itemSeries);
            });


            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            var source =
                    {
                        datatype: "json",
                        localdata: data,
                        id: 'Id',
                        url: url
                    };

            var dataAdapter = new $.jqx.dataAdapter(source);
            $("#jqxFleetCategoryGrid").jqxGrid(
                {
                    enablehover: false,
                    autoheight: true,
                    width: '100%',
                    source: dataAdapter,
                    sortable: true,
                    altrows: true,
                    columns: columnsArray
                });
        }
    </script>
    <script type="text/javascript">
      function showFleetCatChart_OnSuccess(data, status, jqXHR) {         //Success method to display 100% stacked column chart
        var source =
            {
                datatype: "json",
                localdata: data
            };
          var toolTipCustomFormatFn1 = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
              return 'Year:' + xAxisValue + "&nbsp;</br>" + serie.dataField + ':' + value.replace(",", ".") + '&nbsp;';
          }

          var dataAdapter = new $.jqx.dataAdapter(source);

            var seriesArray = [];            
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            $.each(len, function (key, value) {  
            itemArray = {};
            //Fetch the list of categories from json
                itemArray["dataField"] = key.replace(",", ".");
                itemArray["displayText"] = key.replace(",", ".");
                seriesArray.push(itemArray);
            });

          var settings = {
              title: 'Observations by Fleet',
              description: "",
              borderLineColor: 'White',
              enableAnimations: true,
              showLegend: true,
              padding: { left: 20, top: 0, right: 50, bottom: 5 },
              titlePadding: { left: 10, top: 0, right: 0, bottom: 20 },
              source: dataAdapter,              
              xAxis:
                {
                    dataField: 'FleetName',
                    valuesOnTicks: false
                },
                valueAxis:
                {
                    unitInterval: 10,
                    title: {text: ''},
                    tickMarks: {color: '#BCBCBC'},
                    gridLines: {color: '#BCBCBC'},
                    labels: {
                        horizontalAlignment: 'right', 
                        formatSettings: { sufix: '%'}
                    },
                },
              colorScheme: 'scheme01',
              columnSeriesOverlap: false,
               seriesGroups:
                    [
                        {
                            type: 'column',
                            columnsGapPercent: 100,
                            seriesGapPercent: 5,
                            series: seriesArray
                        }
                    ]              
          };
          $('#jqxFltCatColumnChart').jqxChart(settings);
      }
    </script>
    <script type="text/javascript">
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

            var value = checkedYears;
            if (value == "") {
                alert('Atleast 1 year should be selected!');
                return false;
            }
            return true;
        }


        function ValidateVettingType() { //Method to validate Vetting Type dropdown
            var checkedItems = "";
            var items = $('#jqxVettingType').jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });
            var VesselName = checkedItems;
            if (VesselName == "") {
                alert('Atleast 1 Vetting Type should be selected!');
                return false;
            }
            return true;
        }

        function ValidateObvType() { //Method to validate Observation Type dropdown
            var checkedItems = "";
            var items = $('#jqxObservationType').jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });
            var VesselName = checkedItems;
            if (VesselName == "") {
                alert('Atleast 1 Observation Type should be selected!');
                return false;
            }
            return true;
        }

        function ValidateCategory() { //Method to validate Category dropdown
            var checkedItems = "";
            var items = $('#jqxCategory').jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });
            var VesselName = checkedItems;
            if (VesselName == "") {
                alert('Atleast 1 Category should be selected!');
                return false;
            }
            return true;
        }

        function ValidateFleet() { //Method to validate Fleet dropdown
            var checkedItems = "";
            var items = $('#jqxFleet').jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });
            var VesselName = checkedItems;
            if (VesselName == "") {
                alert('Atleast 1 Fleet should be selected!');
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
                    Observations by Vessels</label>
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
                            <td style="font-size: 14px; width: 100px;">
                                <b>Fleet:</b>
                            </td>
                            <td>
                                <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxFleet'>
                                </div>
                            </td>
                            <td style="font-size: 14px; width: 100px;">
                                <b>Vetting Type:</b>
                            </td>
                            <td>
                                <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxVettingType'>
                                </div>
                            </td>
                            <td style="font-size: 14px; width: 100px;">
                                <b>Observation Type:</b>
                            </td>
                            <td>
                                <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxObservationType'>
                                </div>
                            </td>
                            <td style="font-size: 14px; width: 100px;">
                                <b>Category:</b>
                            </td>
                            <td>
                                <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxCategory'>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="height: 25px;">
                    </div>
                    <div style="overflow: hidden;">
                        <div id="jqxVesselPieChart" style="width: 48%; height: 300px; float: left;">
                        </div>
                        <div style="width: 4%;">
                        </div>
                        <div id='jqxWidget' style="width: 48%; float: left;">
                            <div id="jqxVesselGrid">
                            </div>
                        </div>
                    </div>
                    <div style="height: 50px;">
                    </div>
                    <div>
                        <div>
                            <div style="overflow: hidden;">
                                <div id="jqxFleetPieChart" style="width: 48%; height: 300px; float: left;">
                                </div>
                                <div style="width: 4%;">
                                </div>
                                <div style="width: 48%; height: 300px; float: left;">
                                    <div id="jqxFleetGrid">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="height: 75px; border: 0px solid #333333;">
                    </div>
                    <div>
                        <div>
                            <div style="overflow: hidden;">
                                <div id="jqxCatColumnChart" style="width: 48%; height: 300px; float: left;">
                                </div>
                                <div style="width: 4%;">
                                </div>
                                <div style="width: 48%; height: 300px; float: left;">
                                    <div id="jqxCategoryGrid">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="height: 125px; border: 0px solid #333333;">
                        </div>
                        <div>
                            <div>
                                <div style="overflow: hidden;">
                                    <div id="jqxFltCatColumnChart" style="width: 48%; height: 300px; float: left;">
                                    </div>
                                    <div style="width: 4%;">
                                    </div>
                                    <div style="width: 48%; height: 300px; float: left;">
                                        <div id="jqxFleetCategoryGrid">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
           <%-- <li>
                <input type="radio" name="tabs" id="tab2" value="tab2" />
                <label for="tab2">
                    Incidents with Injury - by Category</label>
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
                    <div>
                        <div style="height: 20px;">
                        </div>
                        <div style="overflow: auto;">
                            <div id="jqxInjuryPieChart" style="width: 600px; height: 400px; float: left; vertical-align: top;">
                            </div>
                        </div>
                    </div>
                </div>
            </li>--%>
            <div class="iconse"><asp:ImageButton ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click"
                                                ToolTip="Export to Excel" ImageUrl="~/Images/Excel-icon.png" Style='padding-right: 10px;' /> </div>
        </ul>
    </div>
     <asp:HiddenField ID="hdnYear" runat="server" />
     <asp:HiddenField ID="hdnVessel" runat="server" />
     <asp:HiddenField ID="hdnVettingType" runat="server" />
     <asp:HiddenField ID="hdnObvType" runat="server" />
     <asp:HiddenField ID="hdnCategory" runat="server" />
     <asp:HiddenField ID="hdnFleet" runat="server" />
</asp:Content>
