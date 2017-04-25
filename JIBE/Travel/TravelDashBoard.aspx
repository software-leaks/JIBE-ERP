<%@ Page Title="Travel DashBoard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TravelDashBoard.aspx.cs" Inherits="Travel_TravelDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/Common.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript">

        google.load('visualization', '1.0', { 'packages': ['corechart'] });


    </script>
    <script type="text/javascript">
        var lastExecutorTktBooked = null;
        var lastExecutorTktByVessel = null;
        var lastExecutorTotalAmount = null;
        var lastExecutorAvgPrice = null;
        function Onfail(msg) {
         
            alert(msg);
        }
        function Refresh() {

            asyncBindTktBooked();
            asyncBindTktByVessel();
            asyncBindAvgPrice();
            asyncBindTotalAmount();
          
        }
        //------------------------------------TktBooked--------------------------------------//
        function asyncBindTktBooked() {
            __isResponse = 0;
            setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

            if (lastExecutorTktBooked != null)
                lastExecutorTktBooked.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_tktBooked', false, {}, onSuccessasyncBindTktBooked, Onfail, null);
            lastExecutorTktBooked = service.get_executor();
        }

        function onSuccessasyncBindTktBooked(retVal, ev) {

            __isResponse = 1;
            drawChartTktBooked(retVal);
        }
        function drawChartTktBooked(dataValues) {
            __isResponse = 1;
           
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'MonthName');
            data.addColumn('number', 'Avg per Vessel');
            data.addColumn('number', 'Total Vessel');
            data.addColumn('number', 'Total Tickets(Count)');
            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].MonthName, dataValues[i].avgtkt, dataValues[i].vslcount, dataValues[i].totalticket]);
            }
            var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
                       { calc: "stringify",
                           sourceColumn: 1,
                           type: "string",
                           role: "annotation"
                           
                       },
                       2,
                       { calc: "stringify",
                           sourceColumn: 2,
                           type: "string",
                           role: "annotation"
                           
                       },
                       3,
                       { calc: "stringify",
                           sourceColumn: 3,
                           type: "string",
                           role: "annotation"
                           
                       }
                      
                       ]);
            var options = { 'title': '',
                vAxis: { title: 'Month', titleTextStyle: { color: 'red'} },
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '90%' },
                annotations: {
    alwaysOutside: true
  }

            };

            var chart = new google.visualization.BarChart(document.getElementById('dvtktBooked'));

            chart.draw(view, options);
        }

        //------------------------------------TktByVessel--------------------------------------//
        function asyncBindTktByVessel() {
            __isResponse = 0;
            setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

            if (lastExecutorTktByVessel != null)
                lastExecutorTktByVessel.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_tktByVessel', false, {}, onSuccessasyncBindTktByVessel, Onfail, null);
            lastExecutorTktByVessel = service.get_executor();
        }

        function onSuccessasyncBindTktByVessel(retVal, ev) {

            __isResponse = 1;
            drawChartTktByVessel(retVal);
        }
        function drawChartTktByVessel(dataValues) {
            __isResponse = 1;
           
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Vessel_Name');
            data.addColumn('number', 'Ticket Count per Vessel');

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].Vessel_Name, dataValues[i].Count]);
            }
             var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
                       { calc: "stringify",
                           sourceColumn: 1,
                           type: "string",
                           role: "annotation"
                       }
                       ]);
            var options = { 'title': '',
                hAxis: { title: 'Vessel Name', titleTextStyle: { color: 'red'}, textStyle: {
                  
                  fontSize: 10
                    
                },slantedTextAngle:90 },
                legend: { position: 'top', maxLines: 3 } ,
                annotations: { alwaysOutside: true }
                
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('dvtktByVessel'));

            chart.draw(view, options);
        }
        //------------------------------------AvgPrice--------------------------------------//
        function asyncBindAvgPrice() {
            __isResponse = 0;
            setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

            if (lastExecutorAvgPrice != null)
                lastExecutorAvgPrice.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_AvgPricePerTicket', false, {}, onSuccessasyncBindAvgPrice, Onfail, null);
            lastExecutorAvgPrice = service.get_executor();
        }

        function onSuccessasyncBindAvgPrice(retVal, ev) {

            __isResponse = 1;
            drawChartAvgPrice(retVal);
        }
        function drawChartAvgPrice(dataValues) {
            __isResponse = 1;

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Month Name');
            data.addColumn('number', 'Avg price/Ticket');

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].MonthName, dataValues[i].avgAmt]);
            }
              var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
                       { calc: "stringify",
                           sourceColumn: 1,
                           type: "string",
                           role: "annotation"
                       }
                       ]);
            var options = { 'title': '',
                hAxis: { title: 'Month', titleTextStyle: { color: 'red' }, slantedText:true
                },
                legend: { position: 'top', maxLines: 3 }

            };

            var chart = new google.visualization.LineChart(document.getElementById('dvAvgPrice'));

            chart.draw(view, options);
        }
        //------------------------------------TotalAmount--------------------------------------//
        function asyncBindTotalAmount() {
            __isResponse = 0;
            setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

            if (lastExecutorTotalAmount != null)
                lastExecutorTotalAmount.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_TotalAmount', false, {}, onSuccessasyncBindTotalAmount, Onfail, null);
            lastExecutorTotalAmount = service.get_executor();
        }

        function onSuccessasyncBindTotalAmount(retVal, ev) {

            __isResponse = 1;
            drawChartTotalAmount(retVal);
        }
        function drawChartTotalAmount(dataValues) {
            __isResponse = 1;

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Month Name');
            data.addColumn('number', 'Total Amount');

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].MonthName, dataValues[i].totalAmount]);
            }
              var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
                       { calc: "stringify",
                           sourceColumn: 1,
                           type: "string",
                           role: "annotation"
                       }
                       ]);

            var options = { 'title': '',
                hAxis: { title: 'Month', titleTextStyle: { color: 'red' }, slantedText:true
                },
                legend: { position: 'top', maxLines: 3 }
            };

            var chart = new google.visualization.LineChart(document.getElementById('dvTotalAmount'));

            chart.draw(view, options);
            document.getElementById('blur-on-updateprogress').style.display = 'none';
        }
    </script>
    <style type="text/css">
        img
        {
            padding-right: 2px;
        }
        .box
        {
            width: 80px;
            height: 20px;
            background-color: #efefef;
            border: 1px solid inset;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvmainContent" style="width: 100%;">
        <table width="100%" cellpadding="2" style="background-color: #efefef;">
            <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh"
                        Style="display: block;" />
                </td>
                <td style=" padding-left:400px; text-align:left"><b>Travel DashBoard</b></td>
            </tr>
        </table>
        <table width="100%" cellspacing="5">
            <tr style="background-color: Silver; height: 25px">
                <td>
                    <b>Tickets booked in last 180 days (Excluding Office):</b>
                </td>
                <td>
                    <b>Vessel wise tickets in last 180 days:</b>
                </td>
            </tr>
            <tr>
                <td style=" width:50%">
                    <div id="dvtktBooked" style="width:100%; height: 355px;">
                        loading....
                    </div>
                </td>
                <td style=" width:50%">
                    <div id="dvtktByVessel" style="width: 100%; height: 355px;">
                        loading....
                    </div>
                </td>
            </tr>
            <tr style="background-color: Silver; height: 25px">
                <td>
                    <b>Average Price/Ticket in last 180 days (Excluding Office):</b>
                </td>
                <td>
                    <b>Total amount in last 180 days (Excluding Office):</b>
                </td>
            </tr>
            <tr>
                <td style=" width:50%">
                    <div id="dvAvgPrice" style="width:100%; height: 355px;">
                        loading....
                    </div>
                </td>
                <td style=" width:50%">
                    <div id="dvTotalAmount" style="width:100%; height: 355px;">
                        loading....
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="blur-on-updateprogress" style="display: none">
        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
            color: black">
            <img src="../Images/loaderbar.gif" alt="Please Wait" />
        </div>
    </div>
</asp:Content>
