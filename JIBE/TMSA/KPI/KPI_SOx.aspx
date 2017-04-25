<%@ page title="KPI -SOx Efficiency" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" enableeventvalidation="false" CodeFile="KPI_SOx.aspx.cs"
    Inherits="KPI_SOx"  %>

<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/JTree/jquery.treeview.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
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
         .ClassA
      {
          background-color: #FFFF99 ;
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
        
        .style2
        {
            width: 10%;
            height: 20px;
        }
        .style3
        {
            width: 15%;
            height: 20px;
        }
        .style4
        {
            height: 20px;
        }
    </style>
    <script>

//        $(window).bind("load", function () {

//            if (document.getElementById('ctl00_MainContent_CheckBox1').checked) 
//                getDataToList();
//        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript">


            function OpenPIScreen(PI_ID) {
                debugger
                var url = '../PI/PI_Details.aspx?PI_ID=' + PI_ID;
                // OpenPopupWindow('PIValues', 'PI Values', url, 'popup', 800, 800, null, null, false, false, true, null);
                window.open(url, "_blank");
            }

            function getDataToList() {
                
                var arr_list_items = [];

                var dataItems = $find('<%=rgdItems.ClientID%>').get_masterTableView().get_dataItems();
                var masterTable = $find("<%=rgdItems.ClientID%>").get_masterTableView();
                for (var i = 0; i < dataItems.length; i++) {
                    var arr_items = [];
                    var row = dataItems[i];
                    var Vessel = masterTable.getCellByColumnUniqueName(masterTable.get_dataItems()[i], "Vessel").innerText;
                    var Average = masterTable.getCellByColumnUniqueName(masterTable.get_dataItems()[i], "Average").innerText;
            

                    Average = Average.replace(",", "");
                    Average = Average.replace(",", "");  
                   arr_items.push(Vessel);
                   arr_items.push(Average);
                    arr_list_items.push(arr_items);
                
                }

                var source = {
                    datafields: [
                    { name: 'Vessel', type: 'string', map: '0' },
                    { name: 'Average', type: 'string', map: '1' }
                 //   { name: 'Goal', type: 'string', map: '2' },
                  //   {name: 'Voyage', type: 'string', map: '2' },
         

                ],
                    datatype: "array",
                    localdata: arr_list_items
                };
             
                var dataAdapter = new $.jqx.dataAdapter(source, { async: false });
                dataAdapter.dataBind();

                var toolTipCustomFormatFn = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                    //var dataItem = data[itemIndex];
                    return 'Vessel_Name :' + xAxisValue + "</br>" + serie.displayText + ':' + Number(value).toFixed(2).replace(",",".");
                };

                var settings = {
                    title: "SOx Efficiency",
                    description: "",
                    enableAnimations: true,
                    showLegend: true,
                    padding: { left: 5, top: 5, right: 11, bottom: 5 },
                    titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                    source: dataAdapter,
                    xAxis:
                {
                    dataField: 'Vessel',
                    valuesOnTicks: false,
                    labels: { angle: -45 }
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
                                    { dataField: 'Average', displayText: 'SOx Efficiency' }
         
//                                     ,{ dataField: 'Voyage', displayText: 'Voyage' }

                                ]
                        },


                    ]
                };

                $('#chartContainer').jqxChart(settings);
               // $('#chartContainer2').jqxChart(settings);
            };
        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript">

        function convertDate(inputFormat) {
            var ds = inputFormat.split('-');
            return [ds[2], ds[1], ds[0]].join('-');
        }

        //

        function GetSelectedItems(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex ;
            var grid = $find("<%= rgdItems.ClientID %>");
            if (grid) {
                var MasterTable = grid.get_masterTableView();
                var data = MasterTable.get_dataItems();
                var rows = data[rowIndex - 1];
                var vesselId = rows.getDataKeyValue("Vessel_Id"); //Access CustomerID using DataKeyValue

                alert("RowIndex: " + rowIndex + " vesselId: " + vesselId);
            }
            return false;
        }


        function showChart2(telId1, telId2, startdate, enddate, vesselname, vesselId, voyage, selectedVoyage) {

            if (voyage == "True") {
                if (selectedVoyage != "0") {
                    var source =
            {
                datatype: "json",
                datafields: [
                  { name: 'RDATE' },
                    { name: 'VALUE' },
                    { name: 'AVERAGE' },
                       { name: 'EEDI' }

                ],
                url: "../../KPIService.svc/GetVoyageDataSOx/VID/" + vesselId + "/Startdate/" + startdate + "/EndDate/" + enddate + "/telId1/" + telId1 + "/telId2/" + telId2
            };
                }
            }
    else {
        showChart();
                var source =
            {
                datatype: "json",
                datafields: [
                  { name: 'RDATE' },
                    { name: 'VALUE' },
                    { name: 'AVERAGE' },
                       { name: 'EEDI' }
                ],

                url: "../../KPIService.svc/GetDataSOx/VID/" + vesselId + "/Startdate/" + startdate + "/EndDate/" + enddate
              // url: "../../KPIService.svc/GetDataSOx/VID/" + vesselId + "/Startdate/" + convertDate(document.getElementById("ctl00_MainContent_hiddenVesselStartDate").value) + "/EndDate/" + convertDate(document.getElementById("ctl00_MainContent_hiddenVesselEndDate").value)

            };
            }
        
                var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                // prepare jqxChart settings
                var toolTipCustomFormatFn = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                    //var dataItem = data[itemIndex];
                    return 'Date :' + xAxisValue.getDate() + '-' + months[xAxisValue.getMonth()] + '-' + xAxisValue.getFullYear() + "</br>" + serie.dataField + ':' + Number(value).toFixed(2).replace(",",".");
                };
                var settings = {
                    title: "SOx Efficiency ",
                    description: "",
                    enableAnimations: true,
                    showLegend: true,
                    padding: { left: 5, top: 5, right: 11, bottom: 5 },
                    titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                    source: dataAdapter,
                    xAxis:
                {
                    dataField: 'RDATE',
                    type: 'date',
                    baseUnit: 'day',
                    valuesOnTicks: true,
                    labels:
                    {
                        formatFunction: function (value) {
                            return value.getDate() + '-' + months[value.getMonth()] + '-' + value.getFullYear();

                        }
                        ,
                        angle: -45
                    }
                    ,

                    gridLines: { visible: false },
                    rangeSelector: {
                        padding: { top: 10, bottom: 0 },
                        backgroundColor: 'white',
                        dataField: 'RDATE',
                        baseUnit: 'month',
                        size: 80,
                        serieType: 'area',
                        showGridLines: false,
                        formatFunction: function (value) {
                            return months[value.getMonth()] + '\'' + value.getFullYear().toString().substring(2);
                        }
                        }

                     ,
                    toolTipFormatFunction: function (value) {

                        return value.getDate() + '-' + months[value.getMonth()] + '-' + value.getFullYear();
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
                            toolTipFormatFunction: toolTipCustomFormatFn,
                            series: [
                                    { dataField: 'VALUE', displayText: 'NOx Effeciency' }
                                   ]
                        }

                    ]
                     };
                   
                  
                     $('#chartContainer2').jqxChart(settings);

                     return false;
        }





        //
        function showChart() {
            
            var hdn = document.getElementById("ctl00_MainContent_hdnVessel_IDs").value;
            //var jsonstr = "[" + myJsonify(hdn) + "]";
            var SOx_Object = {
                "Vessel_Id": "",
                "Vessel_Name": "",
                "Start_date": "",
                "End_date": ""
            }

            SOx_Object.Vessel_Id = hdn;
            //SOx_Object.Start_date = convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value);
            //SOx_Object.End_date = convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value);
            SOx_Object.Start_date = convertDate(document.getElementById("ctl00_MainContent_hdnStartDate").value);
            SOx_Object.End_date = convertDate(document.getElementById("ctl00_MainContent_hdnEndDate").value);
            var sendurl = "../../KPIService.svc/GetMultipleVesselDataSOx";
            $.ajax({
                type: "POST",
                url: sendurl,
                data: JSON.stringify(SOx_Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showChart_OnSuccess
                //             ,  error: function (xhr) {
                //                   alert(xhr.responseText);
                //                   if (xhr.responseText) {
                //                       var err = xhr.responseText;
                //                       if (err)
                //                           error(err);

                //                       error({ Message: "Unknown server error." })
                //                   }
                //   return;

                //}
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
                           { name: 'Vessel_Name' },
                             { name: 'Vessel_Id' },
                             { name: 'AVERAGE' },
                                { name: 'EEDI' },
                                   { name: 'GOAL' }
                         ],
                         localdata: objData
                         // url: "../../KPIService.svc/GetMultipleVesselData/VIDs/" + jsonstr + "/KPI_ID/" + "1" + "/Startdate/" + convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value) + "/EndDate/" + convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value)
                     };

            //var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });


            var dataAdapter = new $.jqx.dataAdapter(source);
            //var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
           
            var toolTipCustomFormatFn = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                //var dataItem = data[itemIndex];
                return 'Vessel_Name :' + xAxisValue + "</br>" + serie.displayText + ':' + Number(value).toFixed(2).replace(",",".");
            };

            // prepare jqxChart settings
            var settings = {
                title: "SOx Efficiency",
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 5, top: 5, right: 11, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                xAxis:
                {
                    dataField: 'Vessel_Name',
                    valuesOnTicks: true,
                    labels: {angle:-45}
                }
            ,

                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups:
                    [
                        {
                            type: 'column',
                            toolTipFormatFunction: toolTipCustomFormatFn,
                            series: [
                                    { dataField: 'AVERAGE', displayText: 'SOx Efficiency' }

                                ]
                        },


                    ]
            };

            // setup the chart
            

            $('#chartContainer').jqxChart(settings);

        }





        function checkIfMultiDimentional(arr) {
            
            for (var item in arr) {
                if (typeof (arr[item]) == 'object') { return true; }
            }
            return false;
        }

        function myJsonify(thing, level) {
            
            var jsonString = "";
            if (!level) { level = 0; }
            var start;
            if (typeof (thing) == 'object') {
                if (checkIfMultiDimentional(thing)) {
                    start = 0;
                    for (var item in thing) {
                        var value = thing[item];
                        if (start > 0) { jsonString += ','; }

                        if (value.substring) { jsonString += item + ":" + "\"" + value + "\""; }
                        else {
                            if (item == "0" || item == "1" || item == "2" || item == "3" || item == "4")
                                jsonString += "{" + myJsonify(value, level + 1) + "}";
                            else
                                jsonString += "\"" + item + "\":{" + myJsonify(value, level + 1) + "}";
                        }
                        start++;
                    }
                }
                else {
                    start = 0;
                    for (var item in thing) {
                        if (start > 0) { jsonString += ','; }
                        jsonString += "\"" + item + "\":" + "\"" + thing[item] + "\"";
                        start++;
                    }

                    return jsonString;
                }
            }
            else { jsonString = thing; }
            return jsonString;
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
            debugger;
            if (msg != "") {
                alert(msg);
                return false;
            }
            return true;
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
        <div>
            SOx Efficiency</div>
    </div>

    
    <div id="dvPageContent" class="page-content-main">
        <div>
            <table width="100%" cellpadding="2" cellspacing="1">
                <tr>
                    <td align="right" valign="middle" width="8%">
                        Fleet Name :
                    </td>
                    <td align="left" valign="middle" width="12%">
                        <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                            Height="150" Width="150" />
                    </td>
                    <td align="right" valign="middle" width="8%">
                        Vessel Name :
                    </td>
                    <td align="left" valign="middle" width="12%">
                        <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                            Height="200" Width="150" />
                    </td>
                    <td align="right" valign="middle" width="8%">
                        Start Date :
                    </td>
                    <td valign="middle" align="left" width="10%">
                        <asp:TextBox ID="txtStartDate" runat="server"  Width="90%"  onkeypress="return false;"
                            CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate">
                        </ajaxToolkit:CalendarExtender>


                    </td>
                    <td align="right" valign="middle" width="8%">
                        End Date :
                    </td>
                    <td valign="middle" align="left" width="10%">
                        <asp:TextBox ID="txtEndDate" runat="server"  Width="90%" onkeypress="return false;"
                            CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate">
                        </ajaxToolkit:CalendarExtender>


                    </td>
                    <td align="left" valign="middle"  width="300px" >
                 
                    <table  width="298px">
                    <tr>
                      <td valign="middle" width="40%" ><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Text="By Voyage" 
                            Height="24px" Font-Bold="true"
                              oncheckedchanged="CheckBox1_CheckedChanged" />

                            </td>
                    <td valign="middle" width="12%">
                        <asp:ImageButton ID="btnFilter" runat="server" OnClientClick="return validateDate();" OnClick="btnFilter_Click" ToolTip="Search"
                            ValidationGroup="ValidateSearch" ImageUrl="~/Images/SearchButton.png" /></td>
                     <td valign="middle" width="12%"><asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                            ImageUrl="~/Images/Refresh-icon.png" /></td>
                            <td valign="middle" width="30%">
<%--                              <input type="button" class="buttonnew" id="btnChart" title="Show Chart" runat="server"
                            visible="false" onclick="showChart();" style="width: 23px; height: 21px;" />--%>
                            
                             <asp:Button ID="btnChart" runat="server" Text="View Graph" CssClass="button-css" ImageUrl="../../Images/graph-button.gif"  visible="false"  OnClientClick="showChart();"/>
                            </td>
                            <td>
                            
                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidateSearch" />

                             <%--<asp:RequiredFieldValidator ID="rfv2" ControlToValidate="txtEndDate" runat="server"
                            Display="None" ErrorMessage="End date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev1" Display="None" runat="server" ControlToValidate="txtEndDate"
                            ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                            ErrorMessage="End Date : Invalid date format." ValidationGroup="ValidateSearch" />
                          <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="txtStartDate" runat="server"
                            Display="None" ErrorMessage="Start date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtStartDate"
                            ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                            ErrorMessage="Start Date : Invalid date format." Display="None" ValidationGroup="ValidateSearch" />--%>
                            </td>
                    
                    </tr>
                    </table>

                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="width: 50%" valign="top">

       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                        <telerik:RadGrid ID="rgdItems" runat="server" AllowAutomaticInserts="True" GridLines="None"
                            Visible="true" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px;
                            margin-top: 10px;" Width="80%" AutoGenerateColumns="False" OnItemDataBound="rgdItems_ItemDataBound"
                            SelectedItemStyle-BackColor="Azure" AllowMultiRowSelection="True" PageSize="100"
                            TabIndex="6" HeaderStyle-HorizontalAlign="Center" 
                            AlternatingItemStyle-BackColor="#CEE3F6">
                            <MasterTableView EditMode="InPlace" ClientDataKeyNames="Vessel_Id,Vessel_Name">
                                <RowIndicatorColumn Visible="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
   
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Vessel" Visible="true"
                                        UniqueName="Vessel">
                                        <ItemTemplate>
                                         <asp:Label ID="hdnVesselEndDate" runat="server" Visible="false"  />
                                         <asp:HiddenField ID="hdVesselID" runat="server" Value='<%#Eval("Vessel_Id")%>' />
                                           <asp:Label ID="hdnVesselStartDate" runat="server"  Visible="false" />
                                                <asp:LinkButton ID="Item_Name" EnableViewState="true" runat="server" Text='<%# Bind("Vessel_Name")%>'    OnClick="item_click"
                                                Style="font-weight: bold"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false"  HeaderStyle-Width="150px"  HeaderText="SOx Efficiency" HeaderStyle-HorizontalAlign="Center"
                                        Visible="true" UniqueName="Average">
                                        <ItemTemplate>
                                            <asp:Label ID="Vessel_Average" runat="server" Text='<%# Bind("Average","{0:n}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center"/>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Voyage" UniqueName="Voyage" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdVoyage" runat="server" />
                                            <asp:DropDownList ID="ddlVoyage" AutoPostBack="true" Enabled="false"   OnSelectedIndexChanged="ddlVoyage_SelectedIndexChanged"
                                                runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" VerticalAlign="Top" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <PopUpSettings ScrollBars="None" />
                                    <PopUpSettings ScrollBars="None" />
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>

             <asp:HiddenField ID="hiddenVesselStartDate" runat="server" />
            <asp:HiddenField ID="hiddenVesselEndDate" runat="server" />
              <asp:HiddenField ID="hiddenStartDate" runat="server" />
              <asp:HiddenField ID="hiddenEndDate" runat="server" />
              <asp:HiddenField ID="hdnStartDate" runat="server" />
              <asp:HiddenField ID="hdnEndDate" runat="server" />
             </ContentTemplate>
                </asp:UpdatePanel>

                    </td>
                    <td  width="300px" valign="top">
                    <asp:Image Visible="false"  runat="server" ID="ImgComp"  ImageUrl="~/Images/comp2.png" Width="200px"/><br />
                    <asp:Label ID="lblPIList" Text="PI List" runat="server" Font-Bold="true" BackColor="AliceBlue"></asp:Label>
                    <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" Width="200px"
                            GridLines="Both">
                            <ItemTemplate>
                                <table border="0" cellpadding="5">
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: thin;"  width="20%">
                                        <asp:HyperLink ID ="hlPI" runat="server" Text='<%# Eval("value") %>' NavigateUrl='<%# "../PI/PI_Details.aspx?PI_ID=" + Eval("PID")%>' Target="_blank" ></asp:HyperLink>
                                        </td>
                                        <td  width="80%">
                                            <asp:Label ID="lblPIText" runat="server" Text='<%# Eval("PIName") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    <br />
                    <br />
                    <asp:Label ID="lblFormula" runat="server" Font-Bold="true" BackColor="AntiqueWhite"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="9">
                      
                          <%--  <input type="button" class="buttonnew" id="Button2" title="Show Chart" runat="server"
                             onclick="showChart2();" style="width: 23px; height: 21px;" />--%>
                        <div id="divChart" runat="server">
                            <table width="100%">
                                <tr>
                                   
                                    <td align="center" style="width:50%">
                                    <div style="height: 30px;">
                                      <asp:Button ID="btnVoyageChart" runat="server" Text="View Graph" visible="false" CssClass="button-css" OnClientClick="getDataToList(); return false;"/>

                            
                            </div>
                                        <div id='chartContainer' style="width: 98%; height: 400px; float: left;">
                                        </div>
                                    </td>
                                    <td align="center" style="width:50%">
                                     <center>
                                     <div style="height: 30px;">
                                         &nbsp;
                                         </div>
                                     </center>
                                     <div id='chartContainer2'style="width: 98%; height: 400px; float: left;"></div>
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
