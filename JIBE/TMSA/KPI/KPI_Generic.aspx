<%@ Page Title="KPI -CO2 Efficiency" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="KPI_Generic.aspx.cs"
    Inherits="KPI_CO2" %>
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
        

    </style>
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript">
            function getDataToList() {
                debugger;
                var arr_list_items = [];

                var dataItems = $find('<%=rgdItems.ClientID%>').get_masterTableView().get_dataItems();
                var masterTable = $find("<%=rgdItems.ClientID%>").get_masterTableView();
                for (var i = 0; i < dataItems.length; i++) {
                    var arr_items = [];
                    var row = dataItems[i];
                    var Vessel = masterTable.getCellByColumnUniqueName(masterTable.get_dataItems()[i], "Vessel").innerText;
                    var Average = masterTable.getCellByColumnUniqueName(masterTable.get_dataItems()[i], "Average").innerText;

                    arr_items.push(Vessel);
                    arr_items.push(EEDI);
                    arr_list_items.push(arr_items);
                }

                var source = {
                    datafields: [
                    { name: 'Vessel', type: 'string', map: '0' },
                    { name: 'Average', type: 'string', map: '1' }

                ],
                    datatype: "array",
                    localdata: arr_list_items
                };

                var dataAdapter = new $.jqx.dataAdapter(source, { async: false });
                dataAdapter.dataBind();

                var settings = {
                    title: "KPI Value",
                    description: "",
                    enableAnimations: true,
                    showLegend: true,
                    padding: { left: 5, top: 5, right: 11, bottom: 5 },
                    titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                    source: dataAdapter,
                    categoryAxis:
                {

                    dataField: 'Vessel',
                    //showGridLines: true,
                    flip: false

                }
            ,

                    colorScheme: 'scheme01',
                    columnSeriesOverlap: false,
                    seriesGroups:
                    [
                        {
                            type: 'column',
                            valueAxis:
                            {
                                visible: true,
                                unitInterval: 5,
                                title: { text: 'Value<br>' },
                                axisSize: 'auto'

                            },
                            series: [
                                    { dataField: 'Average', displayText: 'Average for selected range' },
                                     { dataField: 'EEDI', displayText: 'EEDI' }

                                ]
                        }



                    ]
                };

                $('#chartContainer').jqxChart(settings);

            };



            function OpenPIScreen(PI_ID) {
             debugger
             var url = '../PI/PI_Details.aspx?PI_ID=' + PI_ID;
                // OpenPopupWindow('PIValues', 'PI Values', url, 'popup', 800, 800, null, null, false, false, true, null);
                window.open(url, "_blank");
            }

        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript">
        ////////////


        function showChart2(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex;
            var grid = $find("<%= rgdItems.ClientID %>");
            if (grid) {
                var MasterTable = grid.get_masterTableView();
                var data = MasterTable.get_dataItems();
                var rows = data[rowIndex - 1];
                var vesselId = rows.getDataKeyValue("Vessel_Id"); //Access CustomerID using DataKeyValue
                var val = $(rows.get_element()).find("select[id*='ddlVoyage']").val(); ;
                var Vesselname = rows.getDataKeyValue("Vessel_Name");

            }
            document.getElementById("ctl00_MainContent_lblVessel").innerText = Vesselname;
            var kID = document.getElementById("ctl00_MainContent_hiddenKPIID").value;
            //            alert(kID);
            if (document.getElementById("ctl00_MainContent_CheckBox1").checked == true && val != "0") {

                var source =
            {
                datatype: "json",
                datafields: [
                  { name: 'RDATE' },
                    { name: 'VALUE' }

                ],

                url: "../../KPIService.svc/GetData/VID/" + vesselId + "/KPI_ID/" + kID + "/Startdate/" + convertDate(document.getElementById("ctl00_MainContent_hiddenStartDate").value) + "/EndDate/" + convertDate(document.getElementById("ctl00_MainContent_hiddenEndDate").value)
            };
            }
            else {
                var source =
            {
                datatype: "json",
                datafields: [
                  { name: 'RDATE' },
                    { name: 'VALUE' }

                ],

                url: "../../KPIService.svc/GetData/VID/" + vesselId + "/KPI_ID/" + kID + "/Startdate/" + convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value) + "/EndDate/" + convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value)
            };
            }

            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });

            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

            // prepare jqxChart settings
            var settings = {
                title: "KPI Eficiency",
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
                    valuesOnTicks: false
                    ,

                    gridLines: { visible: false },
                    //                    rangeSelector: {

                    //                        size: 100,
                    //                        padding: { top: 10, bottom: 0 },
                    //                        backgroundColor: 'white',
                    //                        dataField: 'RDATE',
                    //                        baseUnit: 'month',
                    //                        showGridLines: false,
                    //                        formatFunction: function (value) {
                    //                            return months[value.getMonth()] + '\'' + value.getFullYear().toString().substring(2);
                    //                        }
                    //  },


                    toolTipFormatFunction: function (value) {

                        return value.getDate() + '-' + months[value.getMonth()] + '-' + value.getFullYear();
                    }
                },
                valueAxis:
                {

                    labels: { horizontalAlignment: 'right' },
                    title: { text: 'Efficiency Rate<br>' }
                },
                colorScheme: 'scheme01',
                seriesGroups:
                    [

                         {
                             type: 'line',
                             series: [
                                    { dataField: 'VALUE', displayText: 'Daily Value' }
                                ]
                         }

                    ]
            };



            $('#chartContainer2').jqxChart(settings);

            return false;

        };

        ////////

        function convertDate(inputFormat) {
            var ds = inputFormat.split('-');
            return [ds[2], ds[1], ds[0]].join('-');
        }




        function showChart() {
            debugger
            var hdn = document.getElementById("ctl00_MainContent_hdnVessel_IDs").value;
            //var jsonstr = "[" + myJsonify(hdn) + "]";
            var Generic_Object = {
                "Vessel_Id": "",
                "Vessel_Name": "",
                "Start_date": "",
                "End_date": "",
                "KID":""


            }

            Generic_Object.Vessel_Id = hdn;
            Generic_Object.Start_date = convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value);
            Generic_Object.End_date = convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value);
            Generic_Object.KID = document.getElementById("ctl00_MainContent_hiddenKPIID").value;
//            Generic_Object.Interval = document.getElementById("ctl00_MainContent_hiddenInterval").value;
//            Generic_Object.Value_Type = document.getElementById("ctl00_MainContent_hiddenValue_Type").value;
            var sendurl = "../../KPIService.svc/GetMultipleGenericVesselData";
            $.ajax({
                type: "POST",
                url: sendurl,
                data: JSON.stringify(Generic_Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showChart_OnSuccess

            });


        }


        function showChart_OnSuccess(data, status, jqXHR) {
            debugger;
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
                             { name: 'VALUE' }

                         ],
                         localdata: objData
                     
                     };

            var dataAdapter = new $.jqx.dataAdapter(source);

            var settings = {
                title: "Vessel Values",
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 5, top: 5, right: 11, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                xAxis:
                {
                    dataField: 'Vessel_Name',
                    valuesOnTicks: false,
                    labels:
                    {
                        angle: -90
               
                    }
                }
            ,
                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups:
                    [
                        {
                            type: 'column',
                            valueAxis:
                            {
                                visible: true,
                                title: { text: 'Efficiency<br>' },
                                axisSize: 'auto'

                            },
                            series: [
                                    { dataField: 'VALUE', displayText: 'Average for selected range' },

                                ]
                        }



                    ]
            };

            // setup the chart
            debugger;

            $('#chartContainer').jqxChart(settings);

        }





        function checkIfMultiDimentional(arr) {
            debugger;
            for (var item in arr) {
                if (typeof (arr[item]) == 'object') { return true; }
            }
            return false;
        }

        function myJsonify(thing, level) {
            debugger;
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


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnVessel_IDs" runat="server" />
    <div class="page-title">
    
            <asp:Literal ID ="ltKPI" runat="server"></asp:Literal>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div>
            <table width="100%" cellpadding="2" cellspacing="1">
                <tr>
                    <td align="right" valign="top" width="10%">
                        Fleet Name :
                    </td>
                    <td align="left" valign="top" width="12%">
                        <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                            Height="150" Width="150" />
                    </td>
                    <td align="right" valign="top" width="10%">
                        Vessel Name :
                    </td>
                    <td align="left" valign="top" width="12%">
                        <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                            Height="200" Width="150" />
                    </td>
                    <td align="right" valign="top" width="8%">
                        Start Date :
                    </td>
                    <td valign="top" align="left" width="10%">
                        <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate"
                            Format="dd-MM-yyyy">
                        </ajaxToolkit:CalendarExtender>

                        <asp:TextBox ID="txtStartDate" runat="server" Width="90%" onkeypress="return isNumberKey(event)"
                            CssClass="txtInput"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="8%">
                        End Date :
                    </td>
                    <td valign="top" align="left" width="10%">
                         <asp:TextBox ID="txtEndDate" runat="server" Width="90%" onkeypress="return isNumberKey(event)"
                            CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate"
                            Format="dd-MM-yyyy">
                        </ajaxToolkit:CalendarExtender>

                    </td>
                    <td align="left" valign="top" width="10%">
                    <table >
                    <tr>
                        <td width="30%" valign="top" ><asp:CheckBox ID="CheckBox1" runat="server" Text="By Voyage" 
                            CssClass="ActiveRow_Office2007" Height="24px" />
                         
                            
                            </td>
                    <td width="10%" valign="top" >

                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                            ValidationGroup="ValidateSearch" ImageUrl="~/Images/SearchButton.png" /></td>
                     <td width="10%" valign="top" ><asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                            ImageUrl="~/Images/Refresh-icon.png" /></td>
                  <td width="10%" valign="top" >
                    <input type="button" class="buttonnew" id="btnChart" title="Show Chart" runat="server"
                            visible="false" onclick="showChart();" style="width: 23px; height: 21px;" />
                        <input type="button" class="buttonnew" id="Button1" title="Show Chart" runat="server"
                            visible="false" onclick="getDataToList();" style="width: 23px; height: 21px;" />
                  </td>

                    </tr>
                    </table>
                        
                        &nbsp;
                        &nbsp;
                        
                    
                    </td>
                </tr>

                 <tr>

                <td valign="top" align="right" width="10%">
                        KPI Name :
                    </td>
                
           
                <td >
                       <asp:DropDownList ID="ddlKPIList" runat="server" Width="90%" 
                           CssClass="control-edit" AutoPostBack="true"
                           onselectedindexchanged="ddlKPIList_SelectedIndexChanged">
                                        </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlInterval" runat="server"
                                        ValidationGroup="ValidateSearch" Display="None" ErrorMessage="Please select a KPI !"
                                        ControlToValidate="ddlKPIList" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                
                   <td valign="top" align="right" width="10%">
                       <asp:Label ID="lblinterval" Text="Intervals:" Visible="false" runat="server"></asp:Label>
                    </td>
                <td valign="top">
                       <asp:DropDownList ID="ddlInterval" runat="server" Width="90%" Visible="false"
                           CssClass="control-edit" AutoPostBack="true" 
                           onselectedindexchanged="ddlInterval_SelectedIndexChanged">
                                        </asp:DropDownList>
                
                        <asp:RequiredFieldValidator ID="rfvInterval" runat="server" ValidationGroup="ValidateSearch"
                   Display="None" ErrorMessage="Please select Interval !"
                    ControlToValidate="ddlInterval" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2" valign="top">
                   <%-- <asp:CheckBox ID="chkValueType" Text="Average Value" runat="server" />--%>
                 <asp:RadioButtonList ID="rdListValue" RepeatDirection="Horizontal" Visible="false" runat ="server">
                 
                 <asp:ListItem Text="Summed Value" Value="Sum" Selected="True"></asp:ListItem> 
                 <asp:ListItem Text="Average Value" Value="Average" ></asp:ListItem> 
                 </asp:RadioButtonList>
                </td>
                <td>
                  <asp:HiddenField ID="hiddenVessel" runat="server" Value="-1" />
                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidateSearch" />
                          <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="txtStartDate" runat="server"
                            Display="None" ErrorMessage="Start date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtStartDate"
                            ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                            ErrorMessage="Start Date : Invalid date format." Display="None" ValidationGroup="ValidateSearch" />

                         <asp:RequiredFieldValidator ID="rfv2" ControlToValidate="txtEndDate" runat="server"
                            Display="None" ErrorMessage="End date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev1" Display="None" runat="server" ControlToValidate="txtEndDate"
                            ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                            ErrorMessage="End Date : Invalid date format." ValidationGroup="ValidateSearch" /> 
                            
                </td>
                </tr>
                <tr>
                    <td colspan="8" style="width:80%" valign="top">
                        <telerik:RadGrid ID="rgdItems" runat="server" AllowAutomaticInserts="True" GridLines="None"
                            Visible="true" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px;" Width="100%" AutoGenerateColumns="False" OnItemDataBound="rgdItems_ItemDataBound"
                            SelectedItemStyle-BackColor="Azure" AllowMultiRowSelection="True" PageSize="100"
                            TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6">
                            <MasterTableView EditMode="InPlace" ClientDataKeyNames="Vessel_Id,Vessel_Name">
                                <RowIndicatorColumn Visible="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="View" UniqueName="CheckID"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdVesselID" runat="server" Value='<%#Eval("Vessel_Id")%>' />
                                            <asp:ImageButton ID="ibtnView" Style="border: 0; width: 14px; height: 14px" Visible="false"
                                                OnClientClick='<%#"OpenScreen(0,(&#39;"+ Eval("[Vessel_Id]") + "&#39;));return false;"%>'
                                                ForeColor="Black" ImageUrl="~/Images/asl_view.png" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" VerticalAlign="Top" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Vessel" Visible="true"
                                        UniqueName="Vessel">
                                        <ItemTemplate>
                                            <%-- <asp:Label ID="Item_Name" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Vessel_Name")%>' style="font-weight:bold"></asp:Label>--%>
                                            <asp:LinkButton ID="Item_Name" EnableViewState="true" runat="server" Text='<%# Bind("Vessel_Name")%>'
                                                OnClientClick="return  showChart2(this);" Style="font-weight: bold"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" VerticalAlign="Top" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Value"
                                        Visible="true" >
                                        <ItemTemplate>
                                            <asp:Label ID="eedi" runat="server" Text='<%# Bind("Value","{0:n}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Voyage" UniqueName="Voyage">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdVoyage" runat="server" />
                                            <asp:DropDownList ID="ddlVoyage" AutoPostBack="true" OnSelectedIndexChanged="ddlVoyage_SelectedIndexChanged"
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
                    </td>

                    <td valign="top" width="20%">

                    <asp:Image runat="server" ID="imgComp"  Visible="false" ImageUrl="~/Images/comp.png" /><br />
                    <asp:Label ID="lblPIList" Text="PI List" runat="server" Font-Bold="true" BackColor="AliceBlue"></asp:Label>
                    <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"
                            GridLines="Both">
                            <ItemTemplate>
                                <table border="0" cellpadding="5">
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: thin;">
                                        <asp:LinkButton ID ="lnkPIID" runat="server" Text='<%# Eval("value") %>' OnClientClick='<%#"OpenPIScreen((&#39;"+ Eval("PID") +"&#39;));return false;"%>' ></asp:LinkButton>
                                        </td>
                                        <td>
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
                      
                        <div id="divChart" runat="server">
                            <table width="100%">
                                <tr>
                                    <td align="center" style="width: 50%">
                                        <div id='chartContainer' style="width: 100%; height: 400px; float: left;">
                                        </div>
                                    </td>
                                    <td align="center" style="width: 50%">
                                    <center>
                                   <asp:Label ID="lblVessel" runat="server" CssClass="aStyle" Font-Bold="True"></asp:Label>
                                    </center>
                                        <div id='chartContainer2' style="width: 100%; height: 400px; float: left;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <%--<asp:Image ID="ImgCo2" runat="server" ImageUrl="~/TMSA/KPI/Resource/CO2_Effeiciency_2.png" />--%>
            <asp:HiddenField ID="hiddenStartDate" runat="server" />
            <asp:HiddenField ID="hiddenEndDate" runat="server" />
            <asp:HiddenField ID="hiddenKPIID" runat="server" />
            <asp:HiddenField ID="hiddenInterval" runat = "server" />
            <asp:HiddenField ID="hiddenKPIName" runat = "server" />
            <asp:HiddenField ID="hiddenValue_Type" runat = "server" />
            
        </div>
    </div>
</asp:Content>
