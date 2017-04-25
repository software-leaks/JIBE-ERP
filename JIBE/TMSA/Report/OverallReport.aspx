<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="OverallReport.aspx.cs"
    Inherits="TMSA_Report_OverallReport" Title="TMSA Auto Report" %>

<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   <%-- <link href="../../jqxWidgets/Controls/styles/jqx.base.css" rel="stylesheet" type="text/css" />--%>
    <script src="../../jqxWidgets/scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
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
   <%-- <link href="../../jqxWidgets/Controls/styles/jqx.base.css" rel="stylesheet" type="text/css" />--%>
   <link href="../../jqxWidgets/Controls/styles/jqx.base_New.css" rel="stylesheet" type="text/css" />
    <script src="../../jqxWidgets/scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxcore.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdata.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdraw.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxchart.core.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxrangeselector.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdata.export.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxbuttons.js"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxinput.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxbuttons.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxlistbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdropdownlist _KPI.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcheckbox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcombobox.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/jqxcore.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/jqxdata.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdatatable.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxtreegrid.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/scripts/demos.js"></script>
    <%--    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxtabs.js"></script>--%>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxtooltip.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxinput.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxwindow.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxinput.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxcalendar.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxdatetimeinput.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/globalization/globalize.js"></script>
    <script src="../../jqxWidgets/Controls/jqxpanel.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxtree.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxdragdrop.js" type="text/javascript"></script>
    <link href="../../jqxWidgets/Controls/styles/jqx.darkblue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jqueryFileTree.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jqueryFileTree.js" type="text/javascript"></script>

    <style type="text/css">
           table{border-spacing:0px;}
        .jqx-grid-cell
        {
       
        padding:0 0 0 0;
     
        }

   
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }
   
        body, html
        {
            overflow-x: hidden;
        }
        
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
   
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
        
        .style1
        {
            width: 100px;
        }
        
        .style2
        {
            width: 60%;
        }
        .jqx-grid-column-header
        {
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
        .divCatChart
        {
            height: 75px;
            width: 75px;
            border: 2px solid;
        }
        
       .jqx-widget-header
        {
            z-index: 1;
           position: relative;
            height: 100%;
            width: 250px; 
            left: 0px;
            right:0px;
             padding:0 0 0 0;
            border-color: #6DB6D5;
    background: #6DB6D5;
 
    font-size: 12px;
        }
        
        .jqx-cell
                {
            vertical-align: top; 
            margin-top: 0px;
       margin-left: 1px;

        } 
        /*all rounded Corners Done changes for Z index of Drop down menu overlaps with Grid footer.------Gargi*/
.jqx-rc-all
{
    -moz-border-radius: 3px;
    -webkit-border-radius: 3px;
    border-radius: 3px;
    z-index: 1;
    position: relative;
}
   
        .redClass
        {
            background-color: #F01E0D !important;
            vertical-align: middle;
            margin-left:1px;

        }
        .greenClass
        {
            background-color: #228B22 !important;
            vertical-align: middle;
            margin-left:1px;

        }
        .blueClass
        {
            background-color: #DDEBF7 !important;
            vertical-align: middle;
            margin-left:1px;

        }
        .aligntexttree
        {
            vertical-align: middle;

        }

        
        .delete{float:right}
        .kpih{text-decoration:none; color:#535353;}
        
        /*Fix for Table header length increases than that of table width on zoom in-out*/
        .jqx-grid-table {width:100%;}
        
    </style>
    <script type="text/javascript">

        // This function is used to Get Level One pie  chart

        function GetPieChart1() {

            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;

            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;
            var VNO = '1';

            // prepare chart data as an array
            var source =
            {
                datatype: "json",
                datafields: [
                    { name: 'Compliance' },
                    { name: 'Value' }
                ],
                url: "../../KPIService.svc/Get_PieChartData_DL/VID/" + document.getElementById("ctl00_MainContent_hdnVersion").value + "/VNO/" + VNO
            };
            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });
            // prepare jqxChart settings
            var settings = {
                title: "Level 1",
                description: "",
                enableAnimations: true,
                showLegend: false,
                showBorderLine: false,
                legendPosition: { left: 520, top: 140, width: 100, height: 100 },
                padding: { left: 5, top: 5, right: 5, bottom: 5 },
                titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                colorScheme: 'scheme02',
                seriesGroups:
                    [
                        {
                            type: 'pie',
                            showLabels: true,
                            series:
                                [
                                    {
                                        dataField: 'Value',
                                        displayText: 'Compliance',
                                        labelRadius: 50,
                                        initialAngle: 15,
                                        radius: 90,
                                        centerOffset: 0,
                                        formatSettings: { sufix: '' }
                                    }
                                ]
                        }
                    ]
            };
            // setup the chart
            $('#chartContainer_L1').jqxChart(settings);

        }

        // This function is used to Get Level Two pie  chart

        function GetPieChart2() {
            // prepare chart data as an array


            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;


            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;
            var VNO = '2';

            var source =

            {
                datatype: "json",
                datafields: [
                    { name: 'Compliance' },
                    { name: 'Value' }
                ],
                url: "../../KPIService.svc/Get_PieChartData_DL/VID/" + document.getElementById("ctl00_MainContent_hdnVersion").value + "/VNO/" + VNO
            };


            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });
            // prepare jqxChart settings
            var settings = {
                title: "Level 2",
                description: "",
                enableAnimations: true,
                showLegend: false,
                showBorderLine: false,
                legendPosition: { left: 520, top: 140, width: 100, height: 100 },
                padding: { left: 5, top: 5, right: 5, bottom: 5 },
                titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                colorScheme: 'scheme02',
                seriesGroups:
                    [
                        {
                            type: 'pie',
                            showLabels: true,
                            series:
                                [
                                   {
                                       dataField: 'Value',
                                       displayText: 'Compliance',
                                       labelRadius: 50,
                                       initialAngle: 15,
                                       radius: 90,
                                       centerOffset: 0
                                       //formatSettings: { sufix: '%', decimalPlaces: 1 }
                                   }


                                ]
                        }
                    ]
            };
            // setup the chart
            $('#chartContainer_L2').jqxChart(settings);
        }
        // This function is used to Get Level Three pie  chart
        function GetPieChart3() {

            // prepare chart data as an array


            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;


            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;
            var VNO = '3';

            var source =

            {
                datatype: "json",
                datafields: [
                    { name: 'Compliance' },
                    { name: 'Value' }
                ],
                url: "../../KPIService.svc/Get_PieChartData_DL/VID/" + document.getElementById("ctl00_MainContent_hdnVersion").value + "/VNO/" + VNO
            };

            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });
            // prepare jqxChart settings
            var settings = {
                title: "Level 3",
                description: "",
                enableAnimations: true,
                showLegend: false,
                showBorderLine: false,
                legendPosition: { left: 520, top: 140, width: 100, height: 100 },
                padding: { left: 5, top: 5, right: 5, bottom: 5 },
                titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                colorScheme: 'scheme02',
                seriesGroups:
                    [
                        {
                            type: 'pie',
                            showLabels: true,
                            series:
                                [
                                   {
                                       dataField: 'Value',
                                       displayText: 'Compliance',
                                       labelRadius: 50,
                                       initialAngle: 15,
                                       radius: 90,
                                       centerOffset: 0

                                   }

                                ]
                        }
                    ]
            };
            // setup the chart
            $('#chartContainer_L3').jqxChart(settings);
        }

        // This function is used to Get Level Four  chart
        function GetPieChart4() {

            // prepare chart data as an array


            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;


            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;
            var VNO = '4';

            var source =

            {
                datatype: "json",
                datafields: [
                    { name: 'Compliance' },
                    { name: 'Value' }
                ],
                url: "../../KPIService.svc/Get_PieChartData_DL/VID/" + document.getElementById("ctl00_MainContent_hdnVersion").value + "/VNO/" + VNO
            };

            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });
            // prepare jqxChart settings
            var settings = {
                title: "Level 4",
                description: "",
                enableAnimations: true,
                showLegend: false,
                showBorderLine: false,
                legendPosition: { left: 520, top: 140, width: 100, height: 100 },
                padding: { left: 5, top: 5, right: 5, bottom: 5 },
                titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                colorScheme: 'scheme02',
                seriesGroups:
                    [
                        {
                            type: 'pie',
                            showLabels: true,
                            series:
                                [
                                   {
                                       dataField: 'Value',
                                       displayText: 'Compliance',
                                       labelRadius: 50,
                                       initialAngle: 15,
                                       radius: 90,
                                       centerOffset: 0
                                       //formatSettings: { sufix: '%', decimalPlaces: 1 }
                                   }
                                ]
                        }
                    ]
            };
            // setup the chart
            $('#chartContainer_L4').jqxChart(settings);
        }

    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            // This is used to Load Procedure Tree

            $('.fileTreeDemo').fileTree({
                root: 'DOCUMENTS',
                script: 'ProcedureLoadTree.aspx',
                multiFolder: true
            },
                function (id, path, type) {

                    document.getElementById("ctl00_MainContent_hdnProcedureID").value = id;
                    document.getElementById("ctl00_MainContent_hdnDocPath").value = path;

                }
            );




        });
        // This is used to check wether the URL for Procedure,Module and KPI Exists or not
        function isUrlExists(url, cb) {
            jQuery.ajax({
                url: url,
                dataType: 'text',
                type: 'GET',
                complete: function (xhr) {
                    if (typeof cb === 'function')
                        cb.apply(this, [xhr.status]);
                }
            });
        }

        // This is used to open Links in new window for Procedure,Module and KPI
        function ShowWindow(docid, lu, ty) {
            if (ty == "Procedure") {



                var id = docid;


                var url = '../../QMS/' + lu;

                isUrlExists(url, function (status) {
                    if (status === 200) {

                        var ext = lu.split('.').pop();

                        if ((ext == 'pdf') || (ext == 'jpg') || (ext == 'jpeg') || (ext == 'png') || (ext == 'gif')) {

                            OpenPopupWindow('OverallReport', 'Procedure', url, 'popup', 800, 1200, null, null, false, false, true, null);
                        }
                        else {

                            window.open(url);
                            //alert("File Download Started.");

                        }

                    }
                    else {
                        // 404 or 403 forbbiden and not found
                        alert("File Does Not Exsits.");


                    }
                    

                });





            }
            if (ty == "Module") {

                var url = '../../' + lu;

                isUrlExists(url, function (status) {
                    if (status === 200) {

                        //OpenPopupWindow('OverallReport', 'Module', url, 'popup', 800, 1200, null, null, false, false, true, null);
                        window.open(url);

                    }
                    else
                    {
                        // 404 or 403 forbbiden and not found
                        alert("Link Does Not Exsits.");


                    }

                });



            }
            if (ty == "Kpi") {
                debugger;

                var url = '../../TMSA/KPI/' + lu;


                isUrlExists(url, function (status) {
                    if (status === 200) {

                        //OpenPopupWindow('OverallReport', 'Kpi', url, 'popup', 800, 1200, null, null, false, false, true, null);
                        window.open(url);

                    }
                    else {
                        // 404 or 403 forbbiden and not found
                        alert("Link Does Not Exsits.");


                    }
                   
                });



            }

            if (ty == "Notes") {


            }


        }

        // This is used to get Complience ddl value

        function reload_script(objddl) {

            var ddlID = $(objddl).val();

            document.getElementById("ctl00_MainContent_hdnComplience").value = ddlID;

        }

        // This is used to Delete Links for Procedure,Module and KPI
        function DeleteLink(id) {

            var result = confirm("Are you sure you want to delete?");
            if (result) {



                var Object = {
                    "LinkID": ""

                }

                Object.LinkID = id;

                url = "../../KPIService.svc/DeleteData";

                $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(Object),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processdata: true,
                    success: function (data) {


                        bindGrid();

                        $("#treeGrid").jqxTreeGrid('expandAll');


                    },
                    failure: function (data) {
                        alert(data);

                    }

                });

            }

        }

        // This function is open Edit popup and sets its value
        function LoadEdit(eID, ePID, eAP, eC) {


            showModal('EditDialog', false)

            $("#EditDialog").css('visibility', 'visible');


            document.getElementById('EditDialog').title = "Edit"



            $("[id*='ddlCompliance']").val(eC);
            $('#<%=txtap.ClientID %>').val(eAP);


            document.getElementById("ctl00_MainContent_hdnID").value = eID;
            document.getElementById("ctl00_MainContent_hdnPID").value = ePID;



        }




        // This function is for validation of Link that is Max Limit

        function LinkCount(pid, ltype) {



            var Object = {
                "ParentID": "",
                "LinkType": ""

            }

            Object.ParentID = pid;
            Object.LinkType = ltype;
            url = "../../KPIService.svc/LinkCount";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: function (data) {

                    var resultArray = [];
                    var len = 0;
                    $.each(data, function (key, value) {
                        len = value;
                    });

                    var jsonData = JSON.stringify(len);
                    var objData = $.parseJSON(jsonData);

                    var LinkCount = objData[0].Count;
                    document.getElementById("ctl00_MainContent_hdnCount").value = LinkCount;


                },
                failure: function (data) {
                    alert(data);

                }
            });


        }
        // This function is used to open Add popups for all three coloumns Procedures,Modules and KPIs
        function Load(btn1, btn2) {


            var PID = btn1;
            var LType = btn2;
            var LID = "";
            var Count = 0;
            document.getElementById("ctl00_MainContent_hdnPID").value = PID;
            document.getElementById("ctl00_MainContent_hdnLType").value = LType;

            if (LType == "Procedure") {
                $('.fileTreeDemo').fileTree({
                    root: 'DOCUMENTS',
                    script: 'ProcedureLoadTree.aspx',
                    multiFolder: true
                },
                function (id, path, type) {

                    document.getElementById("ctl00_MainContent_hdnProcedureID").value = id;
                    document.getElementById("ctl00_MainContent_hdnDocPath").value = path;

                }
            );
                LinkCount(PID, LType);
                Count = document.getElementById("ctl00_MainContent_hdnCount").value;
                if (Count >= 10) {
                    alert("Can Not Add More than 10 Procedures.");
                }
                else {
                    showModal('dialogDoc', false)

                    $("#dialogDoc").css('visibility', 'visible');

                    document.getElementById('dialogDoc').title = "Select File to Link"

                }

            }
            if (LType == "Module") {

                LinkCount(PID, LType);
                Count = document.getElementById("ctl00_MainContent_hdnCount").value;
                if (Count >= 10) {
                    alert("Can Not Add More than 10 Modules.");
                }
                else {
                    showModal('dialog', false)
                    $("#dialog").css('visibility', 'visible');

                    document.getElementById('dialog').title = "Select Module to Link"


                }
            }
            if (LType == "Kpi") {

                LinkCount(PID, LType);
                Count = document.getElementById("ctl00_MainContent_hdnCount").value;
                if (Count >= 20) {
                    alert("Can Not Add More than 20 KPI's.");
                }
                else {
                    showModal('dialogKPI', false)

                    $("#dialogKPI").css('visibility', 'visible');


                    document.getElementById('dialogKPI').title = "KPI List"

                }
            }

            if (LType == "Notes") {

                LinkCount(PID, LType);
                Count = document.getElementById("ctl00_MainContent_hdnCount").value;
                if (Count >= 5) {
                    alert("Can Not Add More than 5 Notes.");
                }
                else {
                    showModal('NotesDialog', false)

                    $("#NotesDialog").css('visibility', 'visible');

                    document.getElementById('NotesDialog').title = "Notes"
                    $('#<%=txtNotes.ClientID %>').val("");
                }
            }


        }

        // SELECT SINGLE RADIO BUTTON ONLY for KPI.
        function check(objID) {

            var rbSelEmp = $(document.getElementById(objID));
            $(rbSelEmp).attr('checked', true);      // CHECK RADIO BUTTON WHICH IS SELECTED.

            // UNCHECK ALL OTHER RADIO BUTTONS IN THE GRID.
            var rbUnSelect =
            rbSelEmp.closest('table')
                .find("input[type='radio']")
                .not(rbSelEmp);

            rbUnSelect.attr('checked', false);

            SaveKPI($(document.getElementById(objID)).val())

        }
        // This function get the KPI value.
        function SaveKPI(KPI_ID) {

            var KPIID = KPI_ID

            document.getElementById("ctl00_MainContent_hdnKPIID").value = KPIID;


        }


    </script>
    <script type="text/javascript">

        // This is invoked to cancel popups which are open
        function Cancel(cancelType) {


            if (cancelType == "Procedure") {

                hideModal('dialogDoc', false)


            }
            if (cancelType == "Module") {

                hideModal('dialog', false);


            }
            if (cancelType == "Kpi") {

                hideModal('dialogKPI', false);


            }

            if (cancelType == "Notes") {
                hideModal('NotesDialog', false);

            }

            if (cancelType == "Edit") {
                hideModal('EditDialog', false);

            }

        }
        // This function check that wether link exists or not for validation perpose.
        function checkLink(parentid, linktype, linkid, path, notes) {

            debugger;


            var Object = {
                "ParentID": "",
                "LinkType": "",
                "LinkID": "",
                "DocPath": "",
                "Notes": ""

            }

            Object.ParentID = parentid;
            Object.LinkType = linktype;
            Object.LinkID = linkid;
            Object.DocPath = path;
            Object.Notes = notes;

            url = "../../KPIService.svc/LinkExists";

            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: function (data) {

                    var resultArray = [];
                    var len = 0;
                    $.each(data, function (key, value) {
                        len = value;
                    });

                    var jsonData = JSON.stringify(len);
                    var objData = $.parseJSON(jsonData);

                    var LinkExists = objData[0].LinkExists;
                    document.getElementById("ctl00_MainContent_hdnLinkExists").value = LinkExists;

                },
                failure: function (data) {
                    alert(data);

                }
            });


        }
        // This function validating notes.
        function ValidateNotes() {
            var valuesNotes = $('#<%=txtNotes.ClientID %>').val();
            if (valuesNotes == "") {
                alert("Please Enter Notes.");

            }
            else {
                SaveGridData();
            }

        }
        // This function validating procedures.
        function ValidateProcedure() {

            var PathProcedure = document.getElementById("ctl00_MainContent_hdnDocPath").value;
            var lastChar = PathProcedure[PathProcedure.length - 1];
            var compare = "/";


            if (lastChar == compare) {
                alert("Selected Procedure does not have Link,Please select another one.");

            }

            else {
                SaveGridData();
            }
        }
        // This function validating Modules.
        function ValidateModule() {
            debugger;
            var MenuID = document.getElementById("ctl00_MainContent_hdnModuleID").value;

            if (MenuID == "") {
                alert("Selected module does not have Link,Please select another one.");

            }
            else {
                SaveGridData();
            }
        }
        // This function saves all treegrid data which are added.
        function SaveGridData() {
            debugger;

            var ParentID = document.getElementById("ctl00_MainContent_hdnPID").value;
            var LinkType = document.getElementById("ctl00_MainContent_hdnLType").value;

            var LinkID;
            var Path = "";
            var Notes = "";

            if (LinkType == "Kpi") {

                LinkID = document.getElementById("ctl00_MainContent_hdnKPIID").value;

            }
            if (LinkType == "Module") {

                LinkID = document.getElementById("ctl00_MainContent_hdnModuleID").value;

            }

            if (LinkType == "Procedure") {

                LinkID = document.getElementById("ctl00_MainContent_hdnProcedureID").value;
                Path = document.getElementById("ctl00_MainContent_hdnDocPath").value;

            }

            if (LinkType == "Notes") {

                Notes = $('#<%=txtNotes.ClientID %>').val();

            }
            if (LinkID == "") {

                alert("Please select link to save.")
            }
            else {
                checkLink(ParentID, LinkType, LinkID, Path, Notes);
                debugger;
                var LinkExists = document.getElementById("ctl00_MainContent_hdnLinkExists").value;

                if (LinkExists == "URL NOT EXISTS") {
                    alert("KPI not having links ,Please select another one.")
                }
                if (LinkExists == "EXISTS") {
                    alert("Selected Link already Exists ,Please select another one.")
                }
                if (LinkExists == "NOT EXISTS") {


                    var Object = {
                        "ParentID": "",
                        "LinkType": "",
                        "LinkID": "",
                        "DocPath": "",
                        "Notes": ""

                    }

                    Object.ParentID = ParentID;
                    Object.LinkType = LinkType;
                    Object.LinkID = LinkID;
                    Object.DocPath = Path;
                    Object.Notes = Notes;

                    url = "../../KPIService.svc/SaveData";

                    $.ajax({
                        type: "POST",
                        url: url,
                        data: JSON.stringify(Object),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        processdata: true,
                        async: false,
                        success: function (data) {

                            var resultArray = [];
                            var len = 0;
                            $.each(data, function (key, value) {
                                len = value;
                            });

                            var jsonData = JSON.stringify(len);
                            var objData = $.parseJSON(jsonData);





                            hideModal('dialogDoc', false)

                            hideModal('dialog', false);

                            hideModal('dialogKPI', false);
                            hideModal('NotesDialog', false);
                            bindGrid();




                        },
                        failure: function (data) {
                            alert(data);

                        }
                    });
                }
            }
        }

        // This function updated all treegrid data which are updated by user.
        function UpdateGridData() {


            var UpdateID = document.getElementById("ctl00_MainContent_hdnID").value;
            var UpdatedParentID = document.getElementById("ctl00_MainContent_hdnPID").value;

            var Comp = document.getElementById("ctl00_MainContent_hdnComplience").value;
            var AProc = $('#<%=txtap.ClientID %>').val();

            //            if (Comp == "") {
            Comp = $("[id*='ddlCompliance']").val();
            //}

            if (AProc == "") {
                alert("Please Enter Audit Process.")
            }
            else {

                var Object = {

                    "ID": "",
                    "ParentID": "",
                    "AuditedProcess": "",
                    "Compliance": ""
                }

                Object.ID = UpdateID;
                Object.ParentID = UpdatedParentID;
                Object.AuditedProcess = AProc;
                Object.Compliance = Comp;

                url = "../../KPIService.svc/UpdateData";

                $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(Object),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processdata: true,
                    success: function (data) {

                        hideModal('EditDialog', false)
                        GetPieChart1();
                        GetPieChart2();
                        GetPieChart3();
                        GetPieChart4();
                        bindGrid();

                    },
                    failure: function (data) {
                        alert(data);

                    }
                });
            }

        }


    
    </script>
    <%--Code- to display Version data using jqxDropdown with checkboxes--%>
    <script type="text/javascript">
        //Code- to display Version data using jqxDropdown with checkboxes
        function BindVersionList() {
            var url = "../../KPIService.svc/GetVersionData";
            // prepare the data
            var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'VERSIONNO' },
                        { name: 'VERSIONNAME' }
                    ],
                    id: 'id',
                    url: url,
                    async: false
                };
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxWidgetVersions").jqxDropDownList({ autoOpen: true, selectedIndex: 0, source: dataAdapter, displayMember: "VERSIONNAME", valueMember: "VERSIONNO", width: 210, height: 25 });

        }

        function bindElementList() {
            //Code- to display Element data using jqxDropdown with checkboxes
            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;


            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;


            var url = "../../KPIService.svc/GetElementData/VID/" + document.getElementById("ctl00_MainContent_hdnVersion").value;
            // prepare the data
            var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'ELEMENTCODE' },
                        { name: 'ELEMENTID' }
                    ],
                    id: 'ELEMENTID',
                    url: url,
                    async: false
                };
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxWidget").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "ELEMENTCODE", valueMember: "ELEMENTID", width: 200, height: 25 });
            $("#jqxWidget").jqxDropDownList('insertAt', { label: 'All' }, 0);
            $("#jqxWidget").jqxDropDownList('checkAll');



        }

        function BindStageList() {
            //Code- to display Stage data using jqxDropdown with checkboxes

            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;

            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;

            var url = "../../KPIService.svc/GetStageData/VID/" + document.getElementById("ctl00_MainContent_hdnVersion").value;
            // prepare the data
            var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'STAGECODE' },
                        { name: 'STAGEID' }
                    ],
                    id: 'STAGEID',
                    url: url,
                    async: false
                };
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxWidgetStages").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "STAGECODE", valueMember: "STAGEID", width: 200, height: 25 });
            $("#jqxWidgetStages").jqxDropDownList('insertAt', { label: 'All' }, 0);
            $("#jqxWidgetStages").jqxDropDownList('checkAll');

        }

        function BindLevelList() {

            //Code- to display Level data using jqxDropdown with checkboxes
            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;
            //var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getItem', 1); 

            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;

            var url = "../../KPIService.svc/GetLevelData/VID/" + document.getElementById("ctl00_MainContent_hdnVersion").value;
            // prepare the data
            var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'LEVELNO' },
                        { name: 'LEVELID' }
                    ],
                    id: 'LEVELID',
                    url: url,
                    async: false
                };
            var dataAdapter = new $.jqx.dataAdapter(source);
            // Create a jqxDropDownList
            $("#jqxWidgetLevels").jqxDropDownList({ autoOpen: true, checkboxes: true, source: dataAdapter, displayMember: "LEVELNO", valueMember: "LEVELID", width: 200, height: 25 });
            $("#jqxWidgetLevels").jqxDropDownList('insertAt', { label: 'All' }, 0);
            $("#jqxWidgetLevels").jqxDropDownList('checkAll');

        }


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

                bindGrid();

            });
            //Logic to implement Select All ends here
        }

       
    </script>
    <%--script to bind JqxTreeGrid--%>
    <script type="text/javascript">

        $(document).ready(function () {

            BindVersionList();
            $('#jqxWidgetVersions').on('select', function (event) {

                var args = event.args;
                if (args) {
                    // index represents the item's index.                
                    var index = args.index;
                    var item = args.item;
                    // get item's label and value.
                    var label = item.label;
                    var value = item.value;
                    var type = args.type; // keyboard, mouse or null depending on how the item was selected.
                }


                bindElementList();
                BindStageList();
                BindLevelList();
                GetPieChart1();
                GetPieChart2();
                GetPieChart3();
                GetPieChart4();
                bindGrid();


            });

            bindElementList();
            SelectDeselect("#jqxWidget");
            BindStageList();
            SelectDeselect("#jqxWidgetStages");
            BindLevelList();
            SelectDeselect("#jqxWidgetLevels");
            bindGrid();
            //            $("#treeGrid").bind('bindingcomplete', function () {
            //                $("#treeGrid").jqxGrid('sortby', 'Procedure', 'asc');
            //            });
            GetPieChart1();
            GetPieChart2();
            GetPieChart3();
            GetPieChart4();



        });


//        $('#treeGrid').jqxTreeGrid({ cellhover: function (cellhtmlElement, x, y) {
//            alert("Hi");

//        }
//        });

        function bindGrid() {
            //Code- to display Jqx tree Grid with Auto filter data using jqxDropdown with checkboxes

            var item = $("#jqxWidget").jqxDropDownList('getItems');
            var checkedItems = "";
            var items = $("#jqxWidget").jqxDropDownList('getCheckedItems');
            $.each(items, function (index) {
                checkedItems += this.value + ",";
            });

            var Elements = checkedItems;
            document.getElementById("ctl00_MainContent_hdnElements").value = checkedItems;
            var ElementIDs = checkedItems;


            var itemsStages = $("#jqxWidgetStages").jqxDropDownList('getItems');
            var checkedItemsStages = "";
            var itemsStages = $("#jqxWidgetStages").jqxDropDownList('getCheckedItems');
            $.each(itemsStages, function (index) {
                checkedItemsStages += this.value + ",";
            });

            var Stages = checkedItemsStages;
            document.getElementById("ctl00_MainContent_hdnStages").value = checkedItemsStages;
            var StageIDs = checkedItemsStages;


            var itemsLevels = $("#jqxWidgetLevels").jqxDropDownList('getItems');
            var checkedItemsLevels = "";
            var itemsLevels = $("#jqxWidgetLevels").jqxDropDownList('getCheckedItems');
            $.each(itemsLevels, function (index) {
                checkedItemsLevels += this.value + ",";
            });

            var Levels = checkedItemsLevels;
            document.getElementById("ctl00_MainContent_hdnLevels").value = checkedItemsLevels;
            var LevelIDs = checkedItemsLevels;



            var itemVersion = $("#jqxWidgetVersions").jqxDropDownList('getSelectedItem').value;

            document.getElementById("ctl00_MainContent_hdnVersion").value = itemVersion;
            var VersionIDs = itemVersion;


            var Role = document.getElementById("ctl00_MainContent_hdnRole").value;


            var Object = {
                "ElementID": "",
                "StageID": "",
                "LevelNo": "",
                "Role": "",
                "VersionNo": ""

            }

            Object.ElementID = ElementIDs;
            Object.StageID = StageIDs;
            Object.LevelNo = LevelIDs;
            Object.Role = Role;
            Object.VersionNo = VersionIDs;

            url = "../../KPIService.svc/GetOverallReport";
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                success: showGrid_OnSuccess

            });
        }

        function showGrid_OnSuccess(data, status, jqXHR) {
            debugger;
            var newRowID = null;
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
                           { name: 'ID' },
                            { name: 'ParentID' },
                            { name: 'ElementCode' },
                            { name: 'StageCode' },
                            { name: 'KpiTmsa' },
                            { name: 'BestPractices' },
                            { name: 'AuditedProcess' },
                            { name: 'Procedure' },
                            { name: 'Module' },
                            { name: 'KPI' },
                            { name: 'Compliance' },
                            { name: 'Notes' },
                            { name: 'Edit' }

                         ],

                         hierarchy:
                    {
                        keyDataField: { name: 'ID' },
                        parentDataField: { name: 'ParentID' }
                    },

                         id: 'ID',
                         localdata: objData
                     };


            var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {

                return '<span style="position:absolute; ">' + value + '</span>';


            }
            var columnsrenderer = function (value) {
                return '<div style="vertical-align: top">' + value + '</div>';
            }


            var cellclassnameCom = function (row, column, value, data) {
                if (data.Compliance == "Yes") {
                    return "greenClass";
                }
                else if (data.Compliance == "No") {
                    return "redClass";
                }
                else if (data.Compliance != "") {
                    return "blueClass";
                }

            };

            var cellclassname = function (row, column, value, data) {


                if (data.ParentID == "") {


//                    alert(row);
//                    alert(column);


                    return "blueClass";
                }

            };
            var cellclassnameS = function (row, column, value, data) {


                if (data.StageCode != "") {

                    return "aligntexttree";
                }
                else if (data.StageCode == "") {
                    return "blueClass";
                }


            };
            var cellclassnameE = function (row, column, value, data) {


                if (data.Edit != "") {
                    return "aligntexttree";
                }
                else if (data.Edit == "" && data.ParentID == "") {
                    return "blueClass";
                }


            };
            var dataAdapter = new $.jqx.dataAdapter(source);


            // create Tree Grid
            $("#treeGrid").jqxTreeGrid(
            {
                width: '100%',
                source: dataAdapter,
                editable: true,
                altRows: true,
                //sortable: true,
                editSettings: { saveOnPageChange: false, saveOnBlur: false, saveOnSelectionChange: false, cancelOnEsc: false, saveOnEnter: false, editSingleCell: false, editOnDoubleClick: false, editOnF2: false },
                ready: function () {



                    $("#treeGrid").jqxTreeGrid('expandAll');

                },
                columns: [
                    { text: 'Element', datafield: 'ElementCode', width: '15%', cellsalign: 'left', cellclassname: cellclassname, cellsrenderer: cellsrenderer },
                    { text: 'Stage', datafield: 'StageCode', width: '3%', cellsalign: 'left', cellclassname: cellclassnameS },
                    { text: 'KPI TMSA', datafield: 'KpiTmsa', width: '14%', cellsalign: 'left', cellclassname: cellclassname },
                    { text: 'Best Practices', datafield: 'BestPractices', width: '14%', cellsalign: 'left', cellclassname: cellclassname },
                    { text: 'Audited Process', datafield: 'AuditedProcess', width: '14%', cellsalign: 'left', cellclassname: cellclassname },
                    { text: 'Procedure', datafield: 'Procedure', width: '8%', cellsalign: 'left', cellclassname: cellclassname },
                    { text: 'Module', datafield: 'Module', width: '8%', cellsalign: 'left', cellclassname: cellclassname },
                    { text: 'KPI', datafield: 'KPI', width: '8%', cellsalign: 'left', cellclassname: cellclassname },
                    { text: 'Compliance', datafield: 'Compliance', width: '5%', cellsalign: 'center', cellclassname: cellclassnameCom },
                    { text: 'Notes', datafield: 'Notes', width: '9%', cellsalign: 'left', cellclassname: cellclassname },
                    { text: '', datafield: 'Edit', width: '2%', cellsalign: 'left', cellclassname: cellclassnameE }

                  ]
            });

            $("#treeGrid").jqxTreeGrid('expandAll');



        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div style="overflow: auto">
    <div class="page-title" style="width: 100%; height: 100%;">
        <table border="0" width="98%" style="margin-right">
            <tr>
                <td>
                </td>
                <td align="center" width="90%">
                    <div>
                        TMSA Auto Report
                    </div>
                </td>
                <td align="right" >
                    <asp:Button ID="btnCopy" runat="server" Text="Create new version" OnClick="btnCopy_Click" />
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnExportToExcel" runat="server" ToolTip="Export to Excel" ImageUrl="~/Images/Excel-icon.png"
                        OnClick="btnExportToExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="dvPageContent" class="page-content-main">
        <div id='Div1'>
            <div id="tab1" style="overflow: hidden;">
                <table width="100%" border="0">
                    <tr>
                        <td width="6%">
                            <b>Select Element:</b>
                        </td>
                        <td width="15%">
                            <div id='jqxWidget'>
                            </div>
                        </td>
                        <td width="5%">
                            <b>Select Level:</b>
                        </td>
                        <td width="15%">
                            <div id='jqxWidgetLevels'>
                            </div>
                        </td>
                        <td width="5%">
                            <b>Select Stage:</b>
                        </td>
                        <td width="15%">
                            <div id='jqxWidgetStages'>
                            </div>
                        </td>
                        <td width="6%">
                            <b>Select Version:</b>
                        </td>
                        <td width="15%">
                            <div id='jqxWidgetVersions'>
                            </div>
                        </td>
                        <td width="6%">
                        </td>
                        <td width="15%">
                            <div id='Div3'>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br />
                        </td>
                    </tr>
                </table>
                <div id="Div2" style="width: 100%; height: 260px;">
                    <div id="chartContainer_L1" style="width: 25%; height: 250px; position: relative;
                        float: left; border-style: none; margin-left: 0px;" align="center">
                    </div>
                    <div id="chartContainer_L2" style="width: 25%; height: 250px; position: relative;
                        float: left; border-style: none; margin-left: 0px;" align="center">
                    </div>
                    <div id="chartContainer_L3" style="width: 25%; height: 250px; position: relative;
                        float: left; border-style: none; margin-left: 0px;" align="center">
                    </div>
                    <div id="chartContainer_L4" style="width: 25%; height: 250px; position: relative;
                        float: left; border-style: none; margin-left: 0px;" align="center">
                    </div>
                </div>
                <table style="table-layout: fixed; border-style: none; width: 100%">
                    <tr>
                        <td >
                            <div id="treeGrid" style="overflow: hidden;">
                            </div>
                        </td>
                        <%--border-style: none;--%>
                    </tr>
                    <tr>
                        <td valign="top" style="padding-left: 10; padding-top: 20;">
                            <asp:HiddenField ID="hdnElements" runat="server" />
                            <asp:HiddenField ID="hdnStages" runat="server" />
                            <asp:HiddenField ID="hdnLevels" runat="server" />
                            <asp:HiddenField ID="hdnVersion" runat="server" />
                            <asp:HiddenField ID="hdnAuditProcess" runat="server" />
                            <asp:HiddenField ID="hdnComplience" runat="server" />
                            <asp:HiddenField ID="hdnID" runat="server" />
                            <asp:HiddenField ID="hdnPID" runat="server" />
                            <asp:HiddenField ID="hdnLType" runat="server" />
                            <asp:HiddenField ID="hdnKPIURL" runat="server" />
                            <asp:HiddenField ID="hdnCount" runat="server" />
                            <asp:HiddenField ID="hdnLinkExists" runat="server" />
                            <asp:HiddenField ID="hdnProcedureID" runat="server" />
                            <asp:HiddenField ID="hdnDocPath" runat="server" />
                            <asp:HiddenField ID="hdnRole" runat="server" />
                            <asp:HiddenField ID="hdnerror" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%--Div to open the Module Link Popup--%>
        <div style="display: none;" id="dialog" title="Select Module to Link">
            <%-- <div>Select Module to Link</div>--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow: auto; margin-bottom: 20px;">
                        <table width="100%" cellspacing="5">
                            <tr>
                                <td style="vertical-align: top; background-color: #f8f8f8; width: 250px; padding: 5;
                                    border: 1px solid #c3c3c3">
                                    <div style="background-color: #f8f8f8; text-align: left; height: 700px; width: 250px;
                                        overflow: auto; z-index: 1; border: 1px solid inset;">
                                        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                            Width="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" BorderColor="#cccccc">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                                NodeSpacing="0px" VerticalPadding="2px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                                VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                                        </asp:TreeView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <br />
                                    <input id="cancel" type="button" value="Cancel" onclick="Cancel('Module');" />
                                    <asp:Button ID="save" runat="server" Text="Save" OnClientClick="return ValidateModule();"
                                        OnClick="save_Click" />
                                    <%--  <input style="margin-left: 5px;" id="save" type="button" value="Save" onclick="return ValidateModule();"/>--%>
                                    <asp:HiddenField ID="hdnModuleID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--Div to open the Procedure Link Popup--%>
        <div style="display: none;" id="dialogDoc" title="Select File to Link">
            <%--<div>Select File to Link</div>--%>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div style="overflow: auto; margin-bottom: 20px;">
                        <table width="100%" cellspacing="5">
                            <tr>
                                <td style="vertical-align: top; background-color: #f8f8f8; width: 250px; padding: 5;
                                    border: 1px solid #c3c3c3">
                                    <div style="background-color: #f8f8f8; text-align: left; height: 700px; width: 250px;
                                        overflow: auto; z-index: 1; border: 1px solid inset;">
                                        <div class="pull-right">
                                        </div>
                                        <table>
                                            <tr>
                                                <td style="vertical-align: middle;">
                                                    <div style="cursor: pointer; vertical-align: middle;">
                                                        <img src="../../images/doctree/network.gif" alt="" />DOCUMENTS
                                                    </div>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="fileTreeDemo" style="margin-left: 20px">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: middle; background-color: #f8f8f8; width: 250px; padding: 5;
                                    border: 1px solid #c3c3c3">
                                    <br />
                                    <input id="cancelDoc" type="button" value="Cancel" onclick="Cancel('Procedure');" />
                                    <input style="margin-left: 5px;" id="saveDoc" onclick="return ValidateProcedure();"
                                        type="button" value="Save" />
                                    <%--                    <asp:Button ID="saveDoc" runat="server" Text="Save" 
                        OnClientClick="return SaveGridData();" onclick="saveDoc_Click"/>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--Div to open the KPI Link Popup--%>
        <div style="display: none;" id="dialogKPI" title="KPI List">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div style="overflow: auto; margin-bottom: 20px;">
                        <table style="vertical-align: top; background-color: #f8f8f8; width: 1000px; padding: 5;
                            border: 1px solid #c3c3c3">
                            <tr>
                                <td colspan="6">
                                    <asp:GridView ID="gvKPIList" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                        CellPadding="1" CellSpacing="0" Width="100%" DataKeyNames="KPI_ID" CssClass="gridmain-css"
                                        AllowSorting="true">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select One" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="ImgView" value='<%# Eval("KPI_ID") %>' OnClick="javascript:check(this.id)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="KPI Code" DataField="Code" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField HeaderText="KPI Name" DataField="Name" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnKPIID" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <br />
                                    <input id="cancelKPI" type="button" value="Cancel" onclick="return Cancel('Kpi');" />
                                    <%-- <input style="margin-left: 5px;" id="saveKPI" onclick="return SaveGridData();" type="button" value="Save"/>--%>
                                    <asp:Button ID="saveKPI" runat="server" Text="Save" OnClientClick="return SaveGridData();"
                                        OnClick="saveKPI_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--Div to open the Edit Popup--%>
        <div style="display: none;" id="EditDialog" title="Edit">
            <%--<div></div>--%>
            <div style="background-color: #f8f8f8; text-align: left; height: 200px; width: 900px;
                overflow: auto; z-index: 1; border: 1px solid inset;">
                <table width="100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="right" style="font-weight: bold; width: 1500px">
                            Audited Process
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <textarea id="txtap" runat="server" cols="100" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; width: 1500px">
                            Compliance
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left" valign="top">
                            <asp:DropDownList ID="ddlCompliance" runat="server" Width="200px" onchange="reload_script(this);">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="left">
                            <br />
                            <input id="btnCancel" type="button" value="Cancel" onclick="Cancel('Edit');" />
                            <input style="margin-left: 5px;" id="btnEdit" onclick="return UpdateGridData();"
                                type="button" value="Save" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%--Div to open the Notes Popup--%>
        <div style="display: none;" id="NotesDialog" title="Notes">
            <%--<div>Notes</div>--%>
            <div style="background-color: #f8f8f8; text-align: left; height: 200px; width: 900px;
                overflow: auto; z-index: 1; border: 1px solid inset;">
                <table width="100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="left" style="font-weight: bold; width: 50px">
                            Notes
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <textarea id="txtNotes" align="left" runat="server" cols="60" rows="2" maxlength="60"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="left">
                            <br />
                            <input id="cancelNotes" type="button" value="Cancel" onclick="return Cancel('Notes');" />
                            <input style="margin-left: 5px;" id="saveNotes" onclick="return ValidateNotes();"
                                type="button" value="Save" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%--Div to open the Iframe Popup--%>
        <div style="display: none;" id="Div4" title="">
            <%--<div>Notes</div>--%>
            <div style="background-color: #f8f8f8; text-align: left; height: 200px; width: 900px;
                overflow: auto; z-index: 1; border: 1px solid inset;">
                <table width="100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="left" style="font-weight: bold; width: 50px">
                            <iframe id="docPreview" name="docPreview" style="height: 700px; width: 100%; border: 0px;
                                padding: 0; margin: 0;" frameborder="0"></iframe>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</div>
</asp:Content>
