<%@ Page Title="Vessel CO2 Efficiency" Language="C#" MasterPageFile="~/Site.master" 
    AutoEventWireup="true" CodeFile="Vessel_CO2_Efficiency.aspx.cs" Inherits="TMSA_KPI_Vessel_CO2_Efficiency" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
        .buttonnew
        {
            background: url(../../Images/graph.png) no-repeat;
            cursor: pointer;
            border: none;
            width: 118px;
            height: 29px;
        }
        .textbox
        {
            border: 2px solid #3498DB;
            border-radius: 10px;
            height: 22px;
            width: 230px;
        }
    </style>
     <script>
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }
    </script>
    <script type="text/javascript">


        var VoyageD = {
            "TID": "",
            "VID": "",
            "KPID": ""
        }

        function IsNumeric(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        //ctl00_MainContent_imgADD


        function ShowChartButton() {
//        if(convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value) >convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value))
//    {
//               alert('Start Date should not be greater than End Date ')
//               return false;
//       }
//            else {
             showChart();
//            }

        }
        function convertDate(inputFormat) {
            var ds = inputFormat.split('-');
            return [ds[2], ds[1], ds[0]].join('-');
        }

        function validateFrequency(_id, _vid, _kid) {
            debugger;
            VoyageD.TID = _id;
            VoyageD.VID = _vid;
            VoyageD.KPID = _kid;

            if (_id != "") {
                var sendurl = "../../KPIService.svc/GetTelDate";


                $.ajax({
                    type: "POST",
                    url: sendurl,
                    data: JSON.stringify(VoyageD),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processdata: true,
                    async: false,
                    success: function validateFrequencySucces(data, status, jqXHR) {
                        debugger;
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
                  { name: 'EFROM' },
                    { name: 'ETO' },
                    { name: 'FPORT' },
                       { name: 'TPORT' },
                          { name: 'VALUE' },
                          { name: 'PORT' },
                          { name: 'AVERAGE' }
                ],
                localdata: objData

            };


                        var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });

                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                        
                        var toolTipCustomFormatFn = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                            //var dataItem = data[itemIndex];
                            return 'Port :' + xAxisValue + "</br>" + serie.displayText + ':' + Number(value).toFixed(2).replace(",", ".");

                        };
                        
                        var settings = {
                            title: "CO2 Efficiency",
                            description: "",
                            enableAnimations: true,
                            showLegend: true,
                            padding: { left: 5, top: 5, right: 11, bottom: 5 },
                            titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                            source: dataAdapter,
                            xAxis:
                {
                    dataField: 'PORT',
                    valuesOnTicks: false,
                    gridLines: { visible: false }


                },
                            valueAxis:
                {

                    labels: { horizontalAlignment: 'right' },
                    angle: -45
                },
                            colorScheme: 'scheme01',
                            seriesGroups:
                    [

                         {
                             type: 'column',
                             toolTipFormatFunction: toolTipCustomFormatFn,
                             series: [
                                    { dataField: 'VALUE', displayText: 'Efficiency' }

                                ]
                         }

                    ]
                        };

                        // setup the chart
                        debugger;
                        if (document.getElementById("ctl00_MainContent_hiddenVessel").value != '-1') {

                            if (document.getElementById("ctl00_MainContent_hiddengVoyageCount").value != '0') {
                                $('#chartContainer').jqxChart(settings);
                            }
                            else {
                                alert("Data is not available!");
                            }
                        }
                        else {
                            alert("Data is not available!");
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        return;

                    }
                });
            }
        }
        function showChartVoyage() {
            //document.getElementById("ctl00_MainContent_divChart").style.display = "block";
            if ((document.getElementById("ctl00_MainContent_hiddenVessel").value != '-1') && ((document.getElementById("ctl00_MainContent_hdnStart").value.length == '0') || (document.getElementById("ctl00_MainContent_hdnEnd").value.length == '0'))) {
                var listbox = document.getElementById('<%= listVoyage.ClientID %>').options;
                var arr = "";
                for (var count = 0; count < listbox.length; count++) {
                    arr = arr + "-" + listbox[count].value.trim().split(':')[0] + ":" + listbox[count].value.trim().split(':')[1];

                }
                var vid = document.getElementById("ctl00_MainContent_hiddenVessel").value;
                var kid = 5;
                validateFrequency(arr, vid, kid);

            }
        }
        function showChart() {
           // var kID = document.getElementById("ctl00_MainContent_hiddenKPIID").value;
            kID = 5;
            var source =
            {
                datatype: "json",
                datafields: [
                  { name: 'RDATE' },
                    { name: 'VALUE' },
                    { name: 'AVERAGE' },
                       { name: 'EEDI' },
                          { name: 'GOAL' }
                ],

                url: "../../KPIService.svc/GetData/VID/" + document.getElementById("ctl00_MainContent_hiddenVessel").value + "/KPI_ID/" + kID + "/Startdate/" + convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value) + "/EndDate/" + convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value)
            };


            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });
          
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            var toolTipCustomFormatFn = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                //var dataItem = data[itemIndex];
                return 'Date :' + xAxisValue.getDate() + '-' + months[xAxisValue.getMonth() ] + '-' + xAxisValue.getFullYear() + "</br>" + serie.dataField + ':' + Number(value).toFixed(2).replace(",",".");
               
            };

            // prepare jqxChart settings
            var settings = {
                title: "CO2 Efficiency",
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 5, top: 5, right: 20, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                xAxis:
                {
                    dataField: 'RDATE',
                    type: 'date',
                    baseUnit :'day',
                    valuesOnTicks: true,
                    tickInterval: '1 day',
                    columnsGapPercent: 50,
                    labels:
                    {
                        formatFunction: function (value) {
                            return value.getDate() + '-' + months[value.getMonth() ] + '-' + value.getFullYear();
                        

                        },
                          angle: -70
                    }
                    ,

                    gridLines: { visible: false }
                    ,

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
                }
                ,
                valueAxis:
                {
                    labels: { horizontalAlignment: 'right' },
                    angle: -45
                   
                },
                colorScheme: 'scheme01',
                seriesGroups:
                    [
                        {
                            type: 'spline',
                            toolTipFormatFunction: toolTipCustomFormatFn,
                            series: [
                                     { dataField: 'AVERAGE', displayText: 'CO2 Efficiency' }
                              , { dataField: 'GOAL', displayText: 'GOAL' }
                               , { dataField: 'EEDI', displayText: 'EEDI' }
                                ]
                        }
                          ,
                         {
                             type: 'line',

                             toolTipFormatFunction: toolTipCustomFormatFn,
                             // toolTip: value,
                             series: [
                                    { dataField: 'VALUE', displayText: 'EEOI' }
                                ]
                         }

                    ]
            };

            // setup the chart
            debugger;
            if ((document.getElementById("ctl00_MainContent_hiddenVessel").value != '-1') && (document.getElementById("ctl00_MainContent_txtStartDate").value.length != '0') && (document.getElementById("ctl00_MainContent_txtEndDate").value.length != '0')) {

                if (document.getElementById("ctl00_MainContent_hiddengdCount").value != '0') {
                    $('#chartContainer').jqxChart(settings);
                }

            }

        };


        function OpenPIScreen(PI_ID) {
            debugger
            var url = '../PI/PI_Details.aspx?PI_ID=' + PI_ID;
            window.open(url, "_blank");
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

        function clicked() {
            var valid = validateDate();
            if (valid)
                showChart();

            return valid;
        }
    </script>
    <style type="text/css">
        .ajax__htmleditor_editor_bottomtoolbar
        {
            display: none;
        }
        .cke_show_borders body
        {
            background: #FFFFCC;
            color: black;
        }
        .style2
        {
            height: 973px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div id="222" class="page-title">
            Vessel CO2 Efficiency
        </div>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
         <div class="page-content-main">
            <table style="margin-top: 10px;" align="center" >
                <tr>
                    <td valign="middle" align="right" width="10%">
                        Vessel Name :
                    </td>
                    <td valign="middle" align="left" width="10%"> 
                        <asp:DropDownList ID="ddlvessel" runat="server" Width="200px" CssClass="txtInput" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged">
                        </asp:DropDownList>


                        <asp:HiddenField ID="hiddenVessel" runat="server" Value="-1" />
                    </td>
                    <td align="right" width="10%" valign="middle">
                        Start Date :
                    </td>
                    <td valign="middle" align="left" width="10%">
                        <asp:TextBox ID="txtStartDate" runat="server" Width="120px" onkeypress="return false;"
                            CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate">
                        </ajaxToolkit:CalendarExtender>


                    </td>
                    <td align="right" width="10%" valign="middle">
                        End Date :
                    </td>
                    <td valign="middle" align="left" width="10%">
                        <asp:TextBox ID="txtEndDate" runat="server" Width="120px" onkeypress="return false;"
                            CssClass="txtInput"></asp:TextBox>

 
                        <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate">
                        </ajaxToolkit:CalendarExtender>

                    </td>
                    <td valign="middle" align="center" width="30%">
                        &nbsp;
                        <asp:ImageButton ID="btnportfilter" runat="server" ToolTip="Search" ValidationGroup="Group1"
                            ImageUrl="~/Images/SearchButton.png" OnClick="btnportfilter_Click" OnClientClick="return clicked();" />&nbsp;
                        <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Reload" ImageUrl="~/Images/Refresh-icon.png"
                            OnClick="ImageButton1_Click" />&nbsp;

<%--                        <input type="button" class="buttonnew" id="btnChart" title="Show Chart" runat="server"
                            onclick="showChart();" style="width: 23px; height: 21px;" visible="False" />--%>
<%--                        <input type="button" class="buttonnew" id="btnVoyag1e" title="Show Chart" runat="server"
                            onclick="showChartVoyage();" style="width: 23px; height: 21px;" visible="False" />--%>
                        <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to Excel" 
                                ImageUrl="~/Images/Excel-icon.png" Visible="false" onclick="btnExport_Click"/>&nbsp;
                    </td>
                    <td width="20%">
                        
                     <asp:Button ID="btnChart" runat="server" Text="View Graph" Visible="false" CssClass="button-css" OnClientClick="showChart(); return false;"/>
                    

                      <%--<asp:CompareValidator ID="cmpStartDate" runat="server" SetFocusOnError="true"
                        ControlToValidate="txtStartDate" Display="none" ErrorMessage="Invalid start Date" ValidationGroup="Group1"
                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                         <asp:CompareValidator ID="CompareValidator2" runat="server"
                        ControlToValidate="txtEndDate" Display="None" ErrorMessage="Invalid end Date" ValidationGroup="Group1"
                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                        <asp:RequiredFieldValidator ID="RFV" runat="server" ValidationGroup="Group1" Display="None"
                            ErrorMessage="Please select Vessel !" ControlToValidate="ddlvessel" InitialValue="0"></asp:RequiredFieldValidator>
                               <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Group1" />
                                <asp:HiddenField ID="hiddengdCount" runat="server" />
                                <asp:HiddenField ID="hiddengVoyageCount" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
            <table width="100%" frame="box" >

                <tr>
                    <td valign="top" align="left" width="50%">
                    <table width="100%">
                      <td   valign="top">

     
                   <asp:UpdatePanel ID="updGrid" runat = "server">
                   <ContentTemplate>
                   
 
                        <div id="divGrid" runat="server" title="Data"  style="vertical-align:top;">


                                <asp:GridView ID="gvSearch" runat="server" EmptyDataText="No records to display." BorderStyle="Groove"
                                    AllowPaging="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="1"
                                    Width="100%" ShowFooter="True" OnRowDataBound="gvSearch_RowDataBound" 
                                    onpageindexchanging="gvSearch_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Record Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArrival" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Record_Date_Str"))) %>' runat="server"></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CO2 Efficiency">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPORT_NAME" Visible="true" runat="server" Width="250px" Text='<%# Eval("Value","{0:n}")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="gvVoyage" runat="server" EmptyDataText="No records to display." 
                                    AllowPaging="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="1"
                                    Width="100%" ShowFooter="True" Visible="False" 
                                    OnRowDataBound="gvVoyage_RowDataBound" 
                                    onpageindexchanging="gvVoyage_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFrom" runat="server" Text='<%# Eval("FromPort")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo1" Visible="true" runat="server" Text='<%# Eval("ToPort")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left"  Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo2" Visible="true" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("EffectiveFrom"))) %>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo3" Visible="true" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("EffectiveTo"))) %>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left"  Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CO2 Efficiency">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo4" Visible="true" runat="server" Text='<%# Eval("Value","{0:n}")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>

                     </ContentTemplate>
                      <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                         <asp:PostBackTrigger ControlID="gvSearch" />
                            <asp:PostBackTrigger ControlID="gvVoyage" />
                        </Triggers>
                   </asp:UpdatePanel>
                     
                   
                    </td>
                    </table>
                    </td>
                    <td valign="top" width="50%">
                    <div style="vertical-align:top;">

          <table width="100%">

          <tr>
          <td  width="200px" valign="top">
           <asp:Image runat="server" ID="imgComp" ImageUrl="~/Images/comp.png" Width="200px" />&nbsp;
          </td>
          <td valign="top" align="center">
           <asp:Label ID="lblPIList" Text="PI List" runat="server" Font-Bold="true" Width="200px" BackColor="LightGray" ></asp:Label><br />
                               <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" Width="200px"
                GridLines="Both">
                <ItemTemplate>
                    <table border="0" cellpadding="5">

                        <tr>
                            <td style="border-right-style: solid; border-right-width: thin;">
                            <asp:HyperLink ID ="hlPI" runat="server" Text='<%# Eval("value") %>' NavigateUrl='<%# "../PI/PI_Details.aspx?PI_ID=" + Eval("PID")%>' Target="_blank" ></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblPIText" runat="server" Text='<%# Eval("PIName") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>

                        

          </td>
          </tr>
          <tr>
          <td>
               <asp:Label ID="lblGoal" runat="server" Font-Bold="true" Text="Company Goal(MT):" 
                  Visible="false">   </asp:Label>
              <asp:Label ID="txtGoal" runat="server" Font-Bold="true"
                  Visible="false"></asp:Label>
             </td>
          <td valign="top"  align="center">
           <asp:Label ID="lblFormula" runat="server" Font-Bold="true" BackColor="AntiqueWhite"></asp:Label>
                                       <asp:HiddenField ID="hiddenKPIID" runat="server" />
                            <asp:HiddenField ID="hdnStart" runat="server" />
                            <asp:HiddenField ID="hdnEnd" runat="server" />
                            <asp:HiddenField ID="hiddenAVG" runat="server" />
          </td>
          
          </tr>
          <tr>

          <td align="left">
            <asp:Label ID="lblEEDI" runat="server" Font-Bold="true" Text="EEDI(MT):" 
                  Visible="false">
        </asp:Label>
              <asp:Label ID="txtEEDI" runat="server"  Font-Bold="true"
                  ReadOnly="true" Visible="false"></asp:Label>
           </td>
           <td align="center">
            
          </td>
              <tr>
                  <td colspan="2" valign="top">
                      <div ID="divVoyage" runat="server" title="Voyage">
                          <table width="100%">
                              <tr>
                                  <td width="30%">
                                      &nbsp;
                                      <asp:CheckBox ID="checkVoyage" runat="server" AutoPostBack="true" 
                                          Font-Bold="true" OnCheckedChanged="checkVoyage_CheckedChanged" 
                                          Text="By Voyage" />
                                  </td>
                                  <td width="70%" valign="middle">
                                      

                                      <asp:Button ID="btnVoyage" runat="server" Text="View Graph" CssClass="button-css"
                                                                                       Visible="false"  OnClientClick="showChartVoyage(); return false;" />
         
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      Voyage List:
                                  </td>
                                  <td>
                                      <asp:DropDownList ID="ddlVoyage" runat="server" Enabled="false" Width="300px">
                                      </asp:DropDownList> &nbsp;
                                      <asp:ImageButton ID="imgADD" runat="server" Enabled="false"  style="width: 25px; height: 25px;"
                                          ImageUrl="~/Images/add.GIF" OnClick="imgADD_Click" 
                                          ToolTip="Add Voyage to list" />
                                     
                                  <td>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      Selected Voyage:
                                  </td>
                                  <td>
                                      <asp:ListBox ID="listVoyage" runat="server" Enabled="false" 
                                          SelectionMode="Multiple" Width="300px"></asp:ListBox>
                                  </td>
                              </tr>
                          </table>
                      </div>
                  </td>
              </tr>
          </tr>
          </table>

                                         
                    </div>    

                    </td>
                </tr>
            </table>
            <div id="divChart" runat="server"  visible="false" >

                            <div id='chartContainer' style="width: 100%; height: 500px">
                            </div>

            </div>
            </div>
        </ContentTemplate>
          <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
         <asp:PostBackTrigger ControlID="gvSearch" />
            <asp:PostBackTrigger ControlID="gvVoyage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
