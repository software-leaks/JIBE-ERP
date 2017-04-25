<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Observations_by_RiskLevel.aspx.cs" Title="Observation by RiskLevel"
    Inherits="TMSA_VettingReports_Observations_by_RiskLevel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <%--<script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <link href="../../jqxWidgets/Controls/styles/jqx.base_New.css" rel="Stylesheet" type="text/css" />
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
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxtooltip.js"></script>
    <style type="text/css">
   body, html
        {
            overflow-x: hidden; font-family:Tahoma,Tahoma,sans-serif,vrdana; font-size:13px;  color:#696969;
        }     
       
       
        .page{
            width: 100%;
            height:950px;
            background-color: #fff;
            margin: 0px auto 0px auto; /*border: 1px solid #496077;*/
            border: 1px solid #f2f2f2;
        }       
        .tabcontent{width:100%; margin:0 auto; background:#f2f2f2; min-height:100px;}
      	.tabs input[type=radio] {  position: absolute;    top: -9999px;   left: -9999px;  }
      	.tabs {     width: 100%;     float: none;     list-style: none;    position: relative;   padding: 0;  margin:0px; padding:0px;}
      	.tabs li{ float: left;   }
      	.tabs label {display: block; padding:5px 13px; border-radius: 2px 2px 0 0; color:#000; font-size: 15px; font-weight: normal; cursor: pointer; position: relative; 
			-webkit-transition: all 0.2s ease-in-out; -moz-transition: all 0.2s ease-in-out;  -o-transition: all 0.2s ease-in-out; transition: all 0.2s ease-in-out;}
      	.tabs label:hover { background: rgba(255,255,255,0.5); top: 0; }
      
   		[id^=tab]:checked + label { background: #fff; color: #000; top: 0; border-top:1px solid #acacac; border-left:1px solid #acacac; border-right:1px solid #acacac; border-bottom:1px solid #fff;   }

      
      	[id^=tab]:checked ~ [id^=tab-content] { display: block;}
      	.tab-content{ z-index: 2;display: none;text-align: left; width: 100%; line-height: 140%;  padding-top: 10px; background:#fff; padding: 15px; position: absolute; top: 28px; left: 0;  
			box-sizing: border-box; -webkit-animation-duration: 0.5s; -o-animation-duration: 0.5s;  -moz-animation-duration: 0.5s;	animation-duration: 0.5s;  }
		
	.iconse{ float:right;}
	.main {
    margin: 0px !important;
}
</style>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden; font-family:Tahoma,Tahoma,sans-serif,vrdana; font-size:13px;  color:#696969;
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
        .hide
        {
            display: none;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            BindVessel("#jqxVessel");
            BindYear("#jqxYear");
            BindFleet();
            BindRiskLevel();
            BindVettingType();
            BindCategory();
            BindObservationType();

            showObservationRiskLevelGridOne();
            showRiskLevelObservationPieChart();
            showRisklevelObservationYearwiseGirdTwo();
            showRisklevelObservationYearwiseGirdThree();

            SelectDeselect("#jqxVessel");
            SelectDeselect("#jqxYear");
            SelectDeselect('#jqxFleet');
            SelectDeselect("#jqxRiskLevel");
            SelectDeselect('#jqxVettingType');
            SelectDeselect('#jqxObservationType');
            SelectDeselect('#jqxCategory');

            GetHiddenValues();

            var isYearChecked = false;
            $('#jqxYear').on('checkChange', function (event) {
                isYearChecked = true;
            });

            $('#jqxYear').on('close', function (event) {
                if (isYearChecked == true) {
                    //if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear")) {
                        showObservationRiskLevelGridOne();
                        showRiskLevelObservationPieChart();
                        showRisklevelObservationYearwiseGirdTwo();
                        showRisklevelObservationYearwiseGirdThree();
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
                        showObservationRiskLevelGridOne();
                        showRiskLevelObservationPieChart();
                        showRisklevelObservationYearwiseGirdTwo();
                        showRisklevelObservationYearwiseGirdThree();
                        GetHiddenValues();
                    }
                }
                isVesselChecked = false;
            });

            var isFleetChecked = false;
            $('#jqxFleet').on('checkChange', function (event) {
                isFleetChecked = true;
            });

            $('#jqxFleet').on('close', function (event) {
                if (isFleetChecked == true) {
                    //if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear") && ValidateFleet()) {
                        showObservationRiskLevelGridOne();
                        showRiskLevelObservationPieChart();
                        showRisklevelObservationYearwiseGirdTwo();
                        showRisklevelObservationYearwiseGirdThree();
                        GetHiddenValues();
                    }
                }
                isFleetChecked = false;
            });


            var isRiskLevelChecked = false;
            $('#jqxRiskLevel').on('checkChange', function (event) {
                isRiskLevelChecked = true;
            });

            $('#jqxRiskLevel').on('close', function (event) {
                if (isRiskLevelChecked == true) {
                    //if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                    if (ValidateYear("#jqxYear") && ValidateRiskLevel()) {
                        showObservationRiskLevelGridOne();
                        showRiskLevelObservationPieChart();
                        showRisklevelObservationYearwiseGirdTwo();
                        showRisklevelObservationYearwiseGirdThree();
                        GetHiddenValues();
                    }
                }
                isRiskChecked = false;
            });
        });

        var isVetTypeChecked = false;
        $('#jqxVettingType').on('checkChange', function (event) {
            isVetTypeChecked = true;
        });

        $('#jqxVettingType').on('close', function (event) {
            if (isVetTypeChecked == true) {
                //   if (ValidateYear("#jqxYear") && ValidateVessel("#jqxVessel") && ValidateVettingType() && ValidateObvType() && ValidateCategory() && ValidateFleet()) {
                if (ValidateYear("#jqxYear") && ValidateVettingType()) {
                    showObservationRiskLevelGridOne();
                    showRiskLevelObservationPieChart();
                    showRisklevelObservationYearwiseGirdTwo();
                    showRisklevelObservationYearwiseGirdThree();
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
                    showObservationRiskLevelGridOne();
                    showRiskLevelObservationPieChart();
                    showRisklevelObservationYearwiseGirdTwo();
                    showRisklevelObservationYearwiseGirdThree();
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
                    showObservationRiskLevelGridOne();
                    showRiskLevelObservationPieChart();
                    showRisklevelObservationYearwiseGirdTwo();
                    showRisklevelObservationYearwiseGirdThree();
                    GetHiddenValues();
                }
            }
            isCatChecked = false;
        });


    </script>
    <%--Method to Bind Year & Vessel Dropdown--%>
    <script type="text/javascript">
    
    function BindRiskLevel() 
  { 
           var source = [
                    
                    "RiskLevel1",
                    "RiskLevel2",
                    "RiskLevel3",
                    "RiskLevel4",
                    "RiskLevel5"
		        ];

                //Create a jqxDropDownList
                //$("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "RiskLevel1", valueMember: "1", selectedIndex: 1, width: '200', height: '25' });
                //$("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "RiskLevel2", valueMember: "2", selectedIndex: 2, width: '200', height: '25' });
                //$("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "RiskLevel3", valueMember: "3", selectedIndex: 3, width: '200', height: '25' });
                //$("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "RiskLevel4", valueMember: "4", selectedIndex: 4, width: '200', height: '25' });
                //$("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "RiskLevel5", valueMember: "5", selectedIndex: 5, width: '200', height: '25' });

            $("#jqxRiskLevel").jqxDropDownList({autoOpen: true, checkboxes: true, source: source, selectedIndex: 1, width: '200', height: '25'});
            $('#jqxRiskLevel').jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $('#jqxRiskLevel').jqxDropDownList('checkAll');
            
            //$("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Flt_Name", valueMember: "Fleet_ID", selectedIndex: 5, width: '200', height: '25' });
            
        }

    <%-- Method to bind Vessel Dropdown--%>
  function BindVessel(vesselControl) 
  { 
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
            $(vesselControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Vessel_Name", valueMember: "Vessel_Id", selectedIndex: 5, width: '200', height: '25' });
            $(vesselControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $(vesselControl).jqxDropDownList('checkAll');
            //SelectDeselect(vesselControl);            
        }

    function BindYear(yearControl)
    {   
       
        var url = "../../KPIService.svc/GetYears/NumOfYears/6";
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

//          var source = [
//                  "2017",
//                    "2016",
//                    "2015",
//                    "2014",
//                    "2013",
//                    "2012"
//		    ];
        // Create a jqxDropDownList
        if(yearControl == '#jqxYear')
            {
            $(yearControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter,  displayMember: "YEAR", valueMember: "YEAR", selectedIndex: 0, width: '200', height: '25' });
            //$(yearControl).jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter,  displayMember: "YEAR", valueMember: "YEAR", selectedIndex: 1, width: '200', height: '25' });            
            $(yearControl).jqxDropDownList('checkIndex',0);
            //$(yearControl).jqxDropDownList('checkIndex',1);
            $(yearControl).jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            //$(yearControl).jqxDropDownList('checkAll');
        }
        else if(yearControl== '#jqxYearTab2')
        {
            $(yearControl).jqxDropDownList({ autoOpen: true, checkboxes: false, source: dataAdapter, displayMember: "YEAR", valueMember: "YEAR", selectedIndex: 0, width: '200', height: '25' });                
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
               // $(dropdownName).jqxDropDownList('checkIndex', 0);
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
                //showOilMajorObservationGrid();
                //showOilMajorPieChart();
                //showOilMajorCountYearwise();
                //showOilMajorCountYearwiseColumnChart();

        });//Logic to implement Select All ends here    
                
    } 

    function GetHiddenValues() {
        //For Year
        var checkedItems = "";
        var items = $("#jqxYear").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
            checkedItems = checkedItems.substring(1);

        document.getElementById("hdnYear").value = checkedItems.replace(/(^,)|(,$)/g, "");      //Fetch list of year and store in hidden field

        //For Vessel
        var checkedItems = "";
        var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
            checkedItems = checkedItems.substring(1);

        document.getElementById("hdnVessel").value = checkedItems.replace(/(^,)|(,$)/g, "");      //Fetch list of vessel and store in hidden field

      
        //For Fleet
        var checkedItems = "";
        var items = $("#jqxFleet").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
            checkedItems = checkedItems.substring(1);

        document.getElementById("hdnFleet").value = checkedItems.replace(/(^,)|(,$)/g, "");      //Fetch list of category and store in hidden field

        //For RiskLevel
        var checkedItems = "";
        var items = $("#jqxRiskLevel").jqxDropDownList('getCheckedItems');
        $.each(items, function (index) {
            checkedItems += this.value + ",";
        });
        if (checkedItems.charAt(0) == '0')              //remove '0' from the string when ALL is selected.
            checkedItems = checkedItems.substring(1);

        document.getElementById("hdnRisk").value = checkedItems.replace(/(^,)|(,$)/g, "");      //Fetch list of category and store in hidden field

    }
         
    </script>
    <%--Method to Bind Fleet Dropdown--%>
    <script type="text/javascript">
        function BindFleet() {

                    var userCompID = '<%= Session["USERCOMPANYID"]%>';
                    var url = "../../KPIService.svc/GetFleetList/CompanyID/" + userCompID;
                    //var url = "../../KPIService.svc/GetFleetList/CompanyID/1";

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

            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxFleet").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "Flt_Name", valueMember: "Fleet_ID", selectedIndex: 5, width: '200', height: '25' });
            $("#jqxFleet").jqxDropDownList('insertAt', { label: 'All', value: 0 }, 0);
            $("#jqxFleet").jqxDropDownList('checkAll');
        }
    </script>
    
    <%--Method to Bind Vetting Type Dropdown --%>
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
    <%--Method to Bind Category Type Dropdown--%>
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
    <%--Method to Bind Observation Type Dropdown--%>
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

    <%--To Bind Risk Level jqx grid1--%>
    <script type="text/javascript" language="javascript">

        function showObservationRiskLevelGridOne() {

            debugger;
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


            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    fltID += this.value + ",";
            });

            if (fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);

            
            //Fetch Risk Level
            var RiskLevel = "";
            var RiskCheckedItems = $("#jqxRiskLevel").jqxDropDownList('getCheckedItems');

            $.each(RiskCheckedItems, function (index) {
                if (this.value != 0)
                    RiskLevel += this.value + ",";
            });

            if (RiskLevel.charAt(0) == '0')
                RiskLevel = RiskCheckedItems.substring(1);

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


            var Object = {
                //"Type": "",
                //"VesselIDs": "",
                "Vessel_IDs": "",
                "Years": "",
                "FleetID": "",
                "Risk_Level": "",
                "VettingTypeID": "",
                "CategoryID": "",
                "ObvTypeID": ""
            }

            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g, '');
            Object.Years = checkedYears.replace(/^,|,$/g, '');                     
            Object.FleetID = fltID.replace(/^,|,$/g, '');
            Object.Risk_Level = RiskLevel.replace(/^,|,$/g, '');

            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g, '');
            Object.CategoryID = categoryID.replace(/^,|,$/g, '');
            Object.ObvTypeID = obvID.replace(/^,|,$/g, '');


            //url = "../../KPIService.svc/GetVesselwiseOilMajors";
            url = "../../KPIService.svc/GetRiskLevelObservation";


            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: [showRiskLevelObservationGrid_OnSuccess],
                error: function (e) {
                    alert("An error has occured");
                }

            });
        }
    </script>
    <script language="javascript" type="text/javascript">

        function showRiskLevelObservationGrid_OnSuccess(data, status, jqXHR) {

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

            var dataFieldArray = [];
            var columnsArray = [];
            itemSeries = {};
            var len = 0;
            $.each(data, function (key, value) {
                len = value;
            });

            $.each(len, function (key, value) {                   //Fetch the list of categories from json
                itemSeries = {};
                itemSeries["text"] = key.replace(",", ".");
                itemSeries["dataField"] = key.replace(",", ".");
                itemSeries["width"] = "10%";
                itemSeries["renderer"] = columnsrenderer;
                itemSeries["rendered"] = tooltiprenderer;
                if (key == 'Avg' || key == 'AVG') {
                    itemSeries["cellsrenderer"] = cellsrenderer;
                }
                columnsArray.push(itemSeries);


                arrayItem = {};
                arrayItem["name"] = key;
                if (key == 'Avg' || key == 'AVG')
                    arrayItem["type"] = 'string';
                else
                    arrayItem["type"] = 'number';

                dataFieldArray.push(arrayItem);

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
            $("#jqxObservationRiskLevelGrid").jqxGrid(
                {
                    enablehover: false,
                    width: '100%',
                    // columnsresize: true,
                    autoheight: true,
                    source: dataAdapter,
                    //sortable: true,
                    altrows: true,
                    columns: columnsArray

                });
        }

    </script>
    <%--To Bind Risk Level jqx pie Chart--%>
    <script type="text/javascript">
        function showRiskLevelObservationPieChart() {
            debugger;
            //Fetch the list of selected vessel
            var checkedItems = "";
            var items = $("#jqxVessel").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                if (this.value != 0)
                    checkedItems += this.value + ",";
            });

            if (checkedItems.charAt(0) == '0')      //remove '0' from the string when ALL is selected.
                checkedItems = checkedItems.substring(1);

            //Fetch Years
            var checkedYears = "";
            var item2 = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(item2, function (index) {
                if (this.value != 0)
                    checkedYears += this.value + ",";
            });


            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    fltID += this.value + ",";
            });

            if (fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);


            //Fetch Risk Level
            var RiskLevel = "";
            var fltCheckedItems = $("#jqxRiskLevel").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    RiskLevel += this.value + ",";
            });

            if (RiskLevel.charAt(0) == '0')
                RiskLevel = obvCheckedItems.substring(1);


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


            var Object = {
                //"Type": "",
                //"VesselIDs": "",
                "Vessel_IDs": "",
                "Years": "",
                "FleetID": "",
                "Risk_Level": "",
                
                "VettingTypeID": "",
                "CategoryID": "",
                "ObvTypeID": ""

            }


            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g, '');
            Object.Years = checkedYears.replace(/^,|,$/g, '');
            Object.FleetID = fltID.replace(/^,|,$/g, '');
            Object.Risk_Level = RiskLevel.replace(/^,|,$/g, '');
            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g, '');
            Object.CategoryID = categoryID.replace(/^,|,$/g, '');
            Object.ObvTypeID = obvID.replace(/^,|,$/g, ''); 

           
            url = "../../KPIService.svc/GetRiskLevelObservationPieChart";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showObservationRiskLevelPieChart_OnSuccess,
                error: function () {
                    alert("An error occured");
                }

            });
        }
    </script>
    <%--Success method to display PIE chart--%>
    <script type="text/javascript">
        function showObservationRiskLevelPieChart_OnSuccess(data, status, jqXHR) {

            var source =
            {
                datatype: "json",
                datafields: [
                { name: 'Years' },
                { name: 'Rec_Count' }
            ],
                localdata: data
            };
            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });

            var settings = {
                title: "Observations by Risk Level",
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
                                    displayText: 'Years',
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
            $('#jqxObservationRiskLevelPieChart').jqxChart(settings);

        }
    </script>
    
    <%--To Bind Risk Level jqx grid2--%>
    <script language="javascript" type="text/javascript">

        function showRisklevelObservationYearwiseGirdTwo() { //done
            debugger;
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


            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    fltID += this.value + ",";
            });

            if (fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);


            //Fetch Risk Level
            var Risklevel = "";
            var fltCheckedItems = $("#jqxRiskLevel").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    Risklevel += this.value + ",";
            });

            if (Risklevel.charAt(0) == '0')
                Risklevel = obvCheckedItems.substring(1);


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


            var Object = {
                //"Type": "",
                //"VesselIDs": "",
                "Vessel_IDs": "",
                "Years": "",
                "FleetID": "",
                "Risk_Level": "",
                "VettingTypeID": "",
                "CategoryID": "",
                "ObvTypeID": ""

            }


            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g, '');
            Object.Years = checkedYears.replace(/^,|,$/g, '');
            Object.FleetID = fltID.replace(/^,|,$/g, '');           
            Object.Risk_Level = Risklevel.replace(/^,|,$/g, '');

            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g, '');
            Object.CategoryID = categoryID.replace(/^,|,$/g, '');
            Object.ObvTypeID = obvID.replace(/^,|,$/g, '');



            //url = "../../KPIService.svc/GetVesselwiseOilMajors";
            url = "../../KPIService.svc/GetRiskLevelObservationYearwise";


            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: [showRiskLevelObservationYearwiseTwo_OnSuccess, showRiskLevelObservationColumnChartOne_OnSuccess],
                error: function () {
                    alert("An error occured");
                }

            });
        }

    </script>
    <script type="text/javascript" language="javascript">
        function showRiskLevelObservationYearwiseTwo_OnSuccess(data, status, jqXHR) {
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
            itemSeries["dataField"] = 'Vessel_Name';
            itemSeries["width"] = "15%";
            itemSeries["renderer"] = columnsrenderer;
            columnsArray.push(itemSeries);

            //Create a dynamic array of selected YEARS from dropdown
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0) {
                    itemSeries = {};
                    itemSeries["text"] = this.value.replace(",", ".");
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
            arrayItem["name"] = 'Vessel_Name';
            arrayItem["type"] = 'string';
            dataFieldArray.push(arrayItem);



            var yearVal = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearVal, function (index) {
                if (this.value != 0) {
                    arrayItem = {};
                    arrayItem["name"] = this.value;
                    arrayItem["type"] = 'string';
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
            $("#jqxRiskObservationYearwiseGrid2").jqxGrid(
                {
                    enablehover: false,
                    autoheight: true,
                    width: 1000,
                    source: dataAdapter,
                    sortable: true,
                    altrows: true,
                    columns: columnsArray

                });
        }
    </script>
    <%--To Bind Risk Level jqx Column chart1--%>
    <script type="text/javascript">
      function showRiskLevelObservationColumnChartOne_OnSuccess(data, status, jqXHR) {         //Success method to display 100% stacked column chart
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
             if (key != 'Vessel_Name') {
                itemArray["dataField"] = key.replace(",", ".");
                itemArray["displayText"] = key.replace(",", ".");
                seriesArray.push(itemArray);
                }
            });

          var settings = {
              title: 'High Risk by Vessel',
              description: "",
              borderLineColor: 'White',
              enableAnimations: true,
              showLegend: true,
              padding: { left: 20, top: 0, right: 50, bottom: 6 },
              titlePadding: { left: 10, top: 0, right: 0, bottom: 20 },
              source: dataAdapter,              
              xAxis:
                {
                    dataField: 'Vessel_Name',
                    valuesOnTicks: true,
                    gridLines: {visible: false},       
                    tickMarks: {visible: true}       
                },
                valueAxis:
                {
                    minValue : 0,
                    maxValue:100,
                    unitInterval:10,
                    visible: true,
                    axisSize:'auto',
                    title: {text: ''},
                    tickMarks: {color: '#BCBCBC'},
                    gridLines: {color: '#BCBCBC'},
                    labels: {
                        horizontalAlignment: 'right', 
                        formatSettings: { sufix: ''}
                    },
                },
              colorScheme: 'scheme03',
              columnSeriesOverlap: false,
               seriesGroups:
                    [
                        {
                            type: 'column',
                            columnsGapPercent: 10,
                            seriesGapPercent:10,
                            series: seriesArray
                        }
                    ]              
          };
          $('#jqxRiskLevelObservationColumnChart1').jqxChart(settings);
      }
    </script>
    <%--To Bind Risk Level jqx grid3--%>
    <script language="javascript" type="text/javascript">
        function showRisklevelObservationYearwiseGirdThree() { //done
        debugger;
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


            //Fetch Fleet IDs
            var fltID = "";
            var fltCheckedItems = $("#jqxFleet").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    fltID += this.value + ",";
            });

            if (fltID.charAt(0) == '0')
                fltID = obvCheckedItems.substring(1);


            //Fetch Risk Level
            var Risklevel = "";
            var fltCheckedItems = $("#jqxRiskLevel").jqxDropDownList('getCheckedItems');

            $.each(fltCheckedItems, function (index) {
                if (this.value != 0)
                    Risklevel += this.value + ",";
            });

            if (Risklevel.charAt(0) == '0')
                Risklevel = obvCheckedItems.substring(1);


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


            var Object = {
                //"Type": "",
                //"VesselIDs": "",
                "Vessel_IDs": "",
                "Years": "",
                "FleetID": "",
                "Risk_Level": "",
                
                "VettingTypeID": "",
                "CategoryID": "",
                "ObvTypeID": "",

            }


            Object.Vessel_IDs = checkedItems.replace(/^,|,$/g, '');
            Object.Years = checkedYears.replace(/^,|,$/g, '');
            Object.FleetID = fltID.replace(/^,|,$/g, '');           
            Object.Risk_Level = Risklevel.replace(/^,|,$/g, '');

            Object.VettingTypeID = vettingTypeID.replace(/^,|,$/g, '');
            Object.CategoryID = categoryID.replace(/^,|,$/g, '');
            Object.ObvTypeID = obvID.replace(/^,|,$/g, '');


            //url = "../../KPIService.svc/GetVesselwiseOilMajors";
            url = "../../KPIService.svc/GetRiskLevelObservationYearwise";


            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: [showRiskLevelObservationYearwiseThree_OnSuccess, showRiskLevelObservationColumnChartTwo_OnSuccess],
                error: function () {
                    alert("An error occured");
                }

            });
        }

    </script>
    <script type="text/javascript" language="javascript">
        function showRiskLevelObservationYearwiseThree_OnSuccess(data, status, jqXHR) {
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
            itemSeries["dataField"] = 'Vessel_Name';
            itemSeries["width"] = "15%";
            itemSeries["renderer"] = columnsrenderer;
            columnsArray.push(itemSeries);

            //Create a dynamic array of selected YEARS from dropdown
            var yearItems = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearItems, function (index) {
                if (this.value != 0) {
                    itemSeries = {};
                    itemSeries["text"] = this.value.replace(",", ".");
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
            arrayItem["name"] = 'Vessel_Name';
            arrayItem["type"] = 'string';
            dataFieldArray.push(arrayItem);



            var yearVal = $("#jqxYear").jqxDropDownList('getCheckedItems');
            $.each(yearVal, function (index) {
                if (this.value != 0) {
                    arrayItem = {};
                    arrayItem["name"] = this.value;
                    arrayItem["type"] = 'string';
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
            $("#jqxObservationRiskLevelGrid3").jqxGrid(
                {
                    enablehover: false,
                    autoheight: true,
                    width: 1000,
                    source: dataAdapter,
                    sortable: true,
                    altrows: true,
                    columns: columnsArray

                });
        }
    </script>
    <%--To Bind Risk Level jqx Column chart2--%>
    <script type="text/javascript">
      function showRiskLevelObservationColumnChartTwo_OnSuccess(data, status, jqXHR) {         //Success method to display 100% stacked column chart
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
             if (key != 'Vessel_Name') {
                itemArray["dataField"] = key.replace(",", ".");
                itemArray["displayText"] = key.replace(",", ".");
                seriesArray.push(itemArray);
                }
            });

          var settings = {
              title: 'Medium Risk by Vessel',
              description: "",
              borderLineColor: 'White',
              enableAnimations: true,
              showLegend: true,
              padding: { left: 20, top: 0, right: 50, bottom: 6 },
              titlePadding: { left: 10, top: 0, right: 0, bottom: 20 },
              source: dataAdapter,              
            xAxis:
                {
                    dataField: 'FleetName',
                    valuesOnTicks: false,
                    gridLines: {visible: false},                          
                    tickMarks: {visible: true} 
                },
                valueAxis:
                {
                     minValue : 0,
                     maxValue:100,
                    unitInterval: 10,
                    visible: true,
                    axisSize:'auto',
                    title: {text: ''},
                    tickMarks: {color: '#BCBCBC'},
                    gridLines: {color: '#BCBCBC'},
                    labels: {
                        horizontalAlignment: 'right', 
                        formatSettings: { sufix: ''}
                    },
                },
              colorScheme: 'scheme01',
              columnSeriesOverlap: false,
               seriesGroups:
                    [
                        {
                            type: 'column',
                            columnsGapPercent: 20,
                            seriesGapPercent: 10,
                            series: seriesArray
                        }
                    ]              
          };
          $('#jqxObservationRiskLevelColumnChart2').jqxChart(settings);
      }
    </script>
    <%--Method for Validation Year & Vessel,Fleet,ObservationType,VettingType,Category dropdowns--%>  
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
            //var value = checkedYears.replace(/,\s*$/, "");       
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


        function ValidateFleet() { //Method to validate Fleet dropdown
            var checkedItems = "";
            var items = $('#jqxFleet').jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });
            var Fleetname = checkedItems;
            if (Fleetname == "") {
                alert('Atleast 1 Fleet should be selected!');
                return false;
            }
            return true;
        }

        function ValidateRiskLevel() {  //Method to validate OilMajor dropdown
            var checkItems = "";
            var items = $("#jqxRiskLevel").jqxDropDownList('getCheckItems');
            $.each(items, function (index) {
                checkItems += this.value + ",";
            });
            var Risk_Level = checkItems;
            if (Risk_Level = "") {
                alert('Atleast 1 Risk Level should be selected!');
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

    </script>
    <%--Script for hide and show advance filters --%>     
    <script type="text/javascript" language="javascript">
        //----Advance Filters------------
        function toggleAdvSearch(obj) {
            if ($(obj).text() == "Open Advance Filter") {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $(obj).text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">    
    <div>
        <div style="height: 5px;">
        </div>
        <%--<div class="tabcontent" id="divTab">
     <ul class="tabs">
        <li>
            <input type="radio" name="tabs" value="tab1" id="tab1" checked="" />
            <label for="tab1">Observation By Risk Level</label>            
             <div id="tab-content1" class="tab-content animated fadeIn">   --%>

        <table border="0" width="100%">
        <tr>
                    <td style="font-size: 14px; width: 50px;">
                    <b>Years:</b>
                    </td>
                    
                    <td style="width: 150px;">
                    <div style='float: left; width: 50%; background-color: #f9f9f9; margin-right: 50px;' id='jqxYear'>
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
                    <b>Risk:</b>
                    </td>
                    
                    <td>
                    <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxRiskLevel'>
                    </div>                    
                    </td>
                    
                    <td style="text-align: right">
                    <a id="advText" href="#" onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                    </td> 
                        
                        
                   <td class="iconse">
                    <asp:ImageButton ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click"
                    ToolTip="Export to Excel" ImageUrl="~/Images/Excel-icon.png" Style='padding-right: 10px;'
                    Visible="true" />
                    </td>

            </tr>
        <tr id="dvAdvanceFilter" class="hide">
                         
                         <td style="font-size: 14px; width: 100px;">
                         <b>ObservationType:</b>
                         </td>
                         
                         <td>
                         <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxObservationType'>
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
                         <b>Category:</b>
                         </td>
                         
                         <td>
                         <div style='float: left; width: 50%; background-color: #f9f9f9;' id='jqxCategory'>
                         </div>
                         </td>      
                         <td></td>
                        
                      </tr>                                                                                                                               
        </table>
        <div>
            <div style="overflow: hidden;">
                <div id="jqxObservationRiskLevelPieChart" style="width: 800px; height: 400px; float: left;
                    vertical-align: top; margin-left: 3%; margin-top: 2%">
                </div>
                <div style="width: 4%;">
                </div>
                <div id='jqxWidget1' style="width: 50%; float: left; margin-top: 5%; margin-left: 3.8%">
                    <div id="jqxObservationRiskLevelGrid">
                    </div>
                </div>
            </div>
        </div>
        <div>
            <div style="overflow: hidden;">
                <div id="jqxRiskLevelObservationColumnChart1" style="width: 38%; height: 400px; float: left;
                    vertical-align: top; margin-top: 2%; margin-left: 3%">
                </div>
                <div style="width: 5%;">
                </div>
                <div id='jqxWidget2' style="width: 48%; float: left; margin-left: 10%">
                    <div id="jqxRiskObservationYearwiseGrid2">
                    </div>
                </div>
            </div>
        </div>
        <div>
            <div style="overflow: hidden;">
                <div id="jqxObservationRiskLevelColumnChart2" style="width: 35%; height: 400px; float: left;
                    vertical-align: top; margin-top: 2%; margin-left: 3%">
                </div>
                <div style="width: 5%;">
                </div>
                <div id='jqxWidget3' style="width: 48%; float: left; margin-left: 12.5%; margin-top: 3%">
                    <div id="jqxObservationRiskLevelGrid3">
                    </div>
                </div>
            </div>
        </div>                
        <%--<div class="iconse">
                <asp:ImageButton ID="btnExportToExcel" runat="server" ToolTip="Export to Excel" ImageUrl="~/Images/Excel-icon.png" Style='padding-right: 10px;' OnClick="btnExportToExcel_Click"  />
                </div>   --%>
    
    </div>    
    <%--</li>--%>
    <%-- </ul>
    </div>--%>
    <%-- </div>--%>
    
    <asp:HiddenField ID="hdnYear" runat="server" />
    <asp:HiddenField ID="hdnVessel" runat="server" />
    <asp:HiddenField ID="hdnFleet" runat="server" />
    <asp:HiddenField ID="hdnRisk" runat="server" />
    <asp:HiddenField ID="hdnVettingType" runat="server" />
    <asp:HiddenField ID="hdnObvType" runat="server" />
    <asp:HiddenField ID="hdnCategory" runat="server" />
    </form>
</body>
</html>
