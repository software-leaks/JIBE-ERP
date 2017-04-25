<%@ Page Title="Trend Analysis" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TrendAnalysis.aspx.cs" Inherits="Technical_Worklist_TrendAnalysis"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <%--        <style type="text/css">
        .page
        {
            min-width: 400px;
            width: 90%;
        }
        .cleartd
        {
            width: 10px;
        }
    </style>--%>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi?autoload={'modules':[{'name':'visualization','version':'1.1','packages':['corechart','controls']}]}"></script>
    <script type="text/javascript">

        google.load('visualization', '1.0', { 'packages': ['corechart'] });
        function drawChartPscDeficiency(dataValuesPSC) {

            var jsonData = JSON.stringify(dataValuesPSC);
            var objData = $.parseJSON(jsonData);
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Vessel');
            data.addColumn('number', 'NCR');
            data.addColumn('number', 'Deficiencies');
            data.addColumn('number', 'PSC');

            for (var i = 0; i < objData.length; i++) {

                data.addRow([objData[i].Vessel, parseFloat($.trim(objData[i].NCR_Count)), parseFloat($.trim(objData[i].Deficiency_Count)), parseFloat($.trim(objData[i].PSC_Count))]);

            }
            var options = {

                bars: 'horizontal', // Required for Material Bar Charts.
                width: 900,
                height: 400,
                chartArea: { left: 200, top: 20, width: 539.4, height: "350" },
                colors: ['#D13A38', '#66696D', '#F68EA6']

            };

            var chart_div = document.getElementById('dvPscDeficiencyChart');

            var chart = new google.visualization.BarChart(chart_div);
            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                //                console.log(chart_div.innerHTML);
                document.getElementById('hdfPSC').value = chart_div.innerHTML;
            });

            chart.draw(data, options);

        }


        function drawChartNCR(dataValuesNCR) {

            var jsonData = JSON.stringify(dataValuesNCR);
            var objData = $.parseJSON(jsonData);

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Vessel');
            data.addColumn('number', 'Total');



            for (var i = 0; i < objData.length; i++) {
                data.addRow([$.trim(objData[i].Vessel), parseFloat($.trim(objData[i].SumCount))]);

            }
            var objData2 = $.parseJSON(objData);
            var options = {

                bars: 'horizontal', // Required for Material Bar Charts.
                width: 900,
                height: 400,
                chartArea: { left: 200, top: 20, width: 539.4, height: "350" },
                colors: ['#4F81BD']

            };


            var chart_div = document.getElementById('dvNcrChart');

            var chart = new google.visualization.BarChart(chart_div);

            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                //                console.log(chart_div.innerHTML);
                document.getElementById('hdfNCR').value = document.getElementById('dvNcrChart').innerHTML;
            });
            var delay = 1000; //1 seconds
            setTimeout(function () {
                //your code to be executed after 1 seconds
                chart.draw(data, options);
            }, delay);


        }

        function drawChartNearMiss(dataValuesNearMiss) {

            var jsonData = JSON.stringify(dataValuesNearMiss);
            var objData = $.parseJSON(jsonData);

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Vessel');
            data.addColumn('number', 'Total');


            if (objData.length > 20) {
                barwidth = 80;
                chartareawidth = 70;
            }
            if (objData.length < 8) {
                barwidth = 15;
                chartareawidth = 70;
            }
            else {
                barwidth = 50;
                chartareawidth = 70;
            }

            for (var i = 0; i < objData.length; i++) {

                data.addRow([$.trim(objData[i].Vessel), parseFloat($.trim(objData[i].SumCount))]);

            }

            var options = { 'title': '',
                hAxis: { textStyle: { fontName: 'Tahoma', fontSize: 10 },
                    slantedText: true,
                    slantedTextAngle: 90,
                    viewWindowMode: 'explicit'
                },

                vAxis: { viewWindow: { min: 0} },
                width: 1000,
                height: 300,

                legend: { position: 'right', textStyle: { color: 'black', fontSize: 10} },
                chartArea: { left: 100, top: 30, width: chartareawidth + "%", height: "40%" },
                bar: { groupWidth: barwidth + '%' }

            };

            var chart_div = document.getElementById('dvNearMissChart');

            var chart = new google.visualization.ColumnChart(chart_div);

            google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                //                console.log(chart_div.innerHTML);
                document.getElementById('hdfNearMiss').value = chart_div.innerHTML;
            });
            var delay = 2000; //1 seconds
            setTimeout(function () {
                //your code to be executed after 1 seconds
                chart.draw(data, options);
            }, delay);


        }

        function drawChartInjury(dataValuesInjury) {

            var jsonData = JSON.stringify(dataValuesInjury);
            var objData = $.parseJSON(jsonData);

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Vessel');
            data.addColumn('number', 'Total');


            if (objData.length > 20) {
                barwidth = 80;
                chartareawidth = 70;
            }
            if (objData.length < 8) {
                barwidth = 15;
                chartareawidth = 70;
            }
            else {
                barwidth = 50;
                chartareawidth = 70;
            }

            for (var i = 0; i < objData.length; i++) {

                data.addRow([$.trim(objData[i].Vessel), parseFloat($.trim(objData[i].SumCount))]);

            }


            var options = { 'title': '',
                hAxis: { textStyle: { fontName: 'Tahoma', fontSize: 10 },
                    slantedText: true,
                    slantedTextAngle: 90,
                    viewWindowMode: 'explicit'
                },

                vAxis: { viewWindow: { min: 0} },
                width: 1000,
                height: 300,

                legend: { position: 'right', textStyle: { color: 'black', fontSize: 10} },
                chartArea: { left: 100, top: 30, width: chartareawidth + "%", height: "40%" },
                bar: { groupWidth: barwidth + '%' }

            };

            var chart_div = document.getElementById('dvInjury');

            var chart = new google.visualization.ColumnChart(chart_div);

            google.visualization.events.addListener(chart, 'ready', function () {

                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';

                //                console.log(chart_div.innerHTML);
                document.getElementById('hdfInjury').value = document.getElementById('dvInjury').innerHTML;
            });
            var delay = 3000; //1 seconds
            setTimeout(function () {
                //your code to be executed after 1 seconds
                chart.draw(data, options);
            }, delay);



        }

        function drawChartPropertyPollution(dataValuesPP) {
            var jsonData = JSON.stringify(dataValuesPP);
            var objData = $.parseJSON(jsonData);

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Vessel');
            data.addColumn('number', 'Total');


            if (objData.length > 20) {
                barwidth = 80;
                chartareawidth = 70;
            }
            if (objData.length < 8) {
                barwidth = 15;
                chartareawidth = 70;
            }
            else {
                barwidth = 50;
                chartareawidth = 70;
            }

            for (var i = 0; i < objData.length; i++) {
                data.addRow([$.trim(objData[i].Vessel), parseInt($.trim(objData[i].SumCount))]);
            }

            var options = { 'title': '',
                hAxis: { textStyle: { fontName: 'Tahoma', fontSize: 10 },
                    slantedText: true,
                    slantedTextAngle: 90,
                    viewWindowMode: 'explicit'
                },

                vAxis: { viewWindow: { min: 0} },
                width: 1000,
                height: 300,

                legend: { position: 'right', textStyle: { color: 'black', fontSize: 10} },
                chartArea: { left: 100, top: 30, width: chartareawidth + "%", height: "40%" },
                bar: { groupWidth: barwidth + '%' }

            };

            var chart_div = document.getElementById('dvPropertyPollution');

            var chart = new google.visualization.ColumnChart(chart_div);


            google.visualization.events.addListener(chart, 'ready', function () {

                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                console.log(chart_div.innerHTML);
                document.getElementById('hdfIncidentAccident').value = document.getElementById('dvPropertyPollution').innerHTML;
            });
            var delay = 4000; //1 seconds
            setTimeout(function () {
                //your code to be executed after 1 seconds
                chart.draw(data, options);
            }, delay);



        }
       
    </script>
    <script type="text/javascript">

        window.onload = function () {


            var selectedvalue = 0;

            $(document.getElementById("rblFilterType")).change(function () {

                var AspRadio = document.getElementById('<%= rblFilterType.ClientID %>');
                var AspRadio_ListItem = document.getElementsByTagName('input');

                for (var i = 0; i < AspRadio_ListItem.length; i++) {

                    if (AspRadio_ListItem[i].checked) {
                        Source = AspRadio_ListItem[i].value;
                        selectedvalue = AspRadio_ListItem[i].value;
                        break;
                    }
                }

                if (selectedvalue == '0') {

                    document.getElementById('<%= ddlFleet.ClientID %>').disabled = true;
                    document.getElementById('<%= ddlFleet.ClientID %>').value = 0;
                }
                else {

                    document.getElementById('<%= ddlFleet.ClientID %>').disabled = false;
                    document.getElementById('<%= ddlFleet.ClientID %>').value = 0;
                }
            });
        }
                
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
                top: 20px; z-index: 2; color: black">
                <img src="../../images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Trend Analysis
    </div>
    <div id="dvPageContent" style="margin-top: -2px; border: 1px solid #cccccc; vertical-align: bottom;
        padding: 4px; color: Black; text-align: left; background-color: #fff;">
        <div id="dvDefaultFilter">
            <table style="text-align: right; height: 45px; padding-top: 3px; padding-bottom: 3px;
                font-size: 11px">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblFilterType" runat="server" RepeatDirection="Horizontal"
                            ClientIDMode="Static">
                            <%--AutoPostBack="True" OnSelectedIndexChanged="rblFilterType_SelectedIndexChanged"--%>
                            <asp:ListItem Selected="True" Value="1">Vessel Wise</asp:ListItem>
                            <asp:ListItem Value="0">Fleet Wise</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="1" cellspacing="1" style="width: 99%;">
                <tr>
                    <td valign="top" style="border: 1px solid #aabbdd;">
                        <table border="0" cellpadding="4" cellspacing="1">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblFleet" Text="Fleet:" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFleet" runat="server" Width="135px" AutoPostBack="false">
                                        <%--OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" style="border: 1px solid #aabbdd;">
                        <table border="0" cellpadding="4" cellspacing="1">
                            <tr>
                                <td style="text-align: right;">
                                    From Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfrom" CssClass="textbox-css" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="text-align: right;">
                                    To Date:
                                </td>
                                <td valign="top" style="text-align: left;">
                                    <asp:TextBox ID="txtto" CssClass="textbox-css" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calto" Format="dd-MM-yyyy" TargetControlID="txtto" runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" style="border: 1px solid #aabbdd;">
                        <table style="width: 400px">
                            <tr>
                                <td style="height: 15px; text-align: left">
                                    Duration
                                </td>
                                <td align="center">
                                    <asp:Button ID="btn3Months" Width="70px" runat="server" Height="25px" Text="3 Months"
                                        BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" Style="border-radius: 4px;"
                                        OnClick="btn3Months_Click" />
                                    <asp:Button ID="btn6Months" Width="75px" runat="server" Height="25px" Text="6 Months"
                                        BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" Style="border-radius: 4px;"
                                        OnClick="btn6Months_Click" />
                                    <asp:Button ID="btnYTD" Width="80px" runat="server" Text="YTD" Height="25px" Font-Size="11px"
                                        BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" Style="border-radius: 4px;"
                                        OnClick="btnYTD_Click" />
                                    <asp:Button ID="btnall" Width="50px" runat="server" Text="All" Height="25px" BorderStyle="Solid"
                                        BorderColor="Black" BorderWidth="1px" Style="border-radius: 4px;" OnClick="btnall_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="middle" style="border: 1px solid #aabbdd;">
                        &nbsp;&nbsp;<asp:ImageButton ID="ImgBtnSearch" ImageUrl="~/Images/SearchAndReload.png"
                            runat="server" OnClick="ImgBtnSearch_Click" />
                        <asp:ImageButton ID="ImgBtnClearFilter" ImageUrl="~/Images/ClearFilter.png" runat="server"
                            OnClick="ImgBtnClearFilter_Click" />&nbsp;<asp:ImageButton ID="ibtnExportToExcel"
                                runat="server" ImageUrl="../../Images/Exptoexcel.png" OnClick="ibtnExportToExcel_Click"
                                ToolTip="Export To excel" />
                        <br />
                        <%--<center>
                            <asp:Button ID="btnExportToExcel" runat="server" Text="Button" OnClick="btnExportToExcel_Click" />
                            <input type="button" value="Export To Excel" id="btnExportToExcel" onclick="ExportToExcel();" />
                        </center>--%>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="innerData">
                    <br />
                    <div style="border: 0px solid Gray; margin-top: 0px" id="innerPclData" runat="server">
                        <div>
                            <div style="border: 1px solid #360000;">
                                <div style="background-color: #4F81BD; color: White; text-align: center; height: 20px;">
                                    <span style="font-size: small; font-weight: bold;">PSC Inspections Per Ship </span>
                                </div>
                                <div style="width: 95%; padding: 10px 10px 10px 20px">
                                    <cc1:TabContainer ID="TabPSCDeficiency" runat="server" ActiveTabIndex="0" ClientIDMode="Static">
                                        <cc1:TabPanel runat="server" HeaderText="&nbsp; Graph &nbsp;" ID="PSCGraph" TabIndex="0"
                                            ClientIDMode="Static">
                                            <ContentTemplate>
                                                <center>
                                                    <div id="dvPscDeficiencyChart" style="color: Red;">
                                                        No data available to plot chart
                                                    </div>
                                                    <asp:HiddenField ID="hdfPSC" runat="server" ClientIDMode="Static" />
                                                </center>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText=" Data " ID="PSCData" TabIndex="1" ClientIDMode="Static">
                                            <ContentTemplate>
                                                <div id="dvPSCinspections" style="width: 98%; height: 300px; overflow: auto;" runat="server">
                                                    <center>
                                                        <asp:GridView ID="grdPscInspections" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                            EmptyDataText="No Record Founds" Width="450px" HeaderStyle-Height="50px">
                                                            <Columns>
                                                                <asp:BoundField DataField="VESSELNAME" HeaderText="Vessel/Fleet">
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PSC" HeaderText="PSC">
                                                                    <ItemStyle Width="200px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DEFECTS" HeaderText="DEFICIENCIES">
                                                                    <ItemStyle Width="250px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NCRCOUNT" HeaderText="NCR">
                                                                    <ItemStyle Width="250px" HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EmptyDataRowStyle ForeColor="Red" />
                                                            <HeaderStyle CssClass="FooterStyle-css" Height="10px" BackColor="Gold" Font-Size="14px" />
                                                            <PagerStyle CssClass="pager" Font-Size="Large" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                        </asp:GridView>
                                                    </center>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </div>
                            </div>
                            <br />
                            <div style="border: 1px solid #360000;">
                                <div style="background-color: #4F81BD; color: White; text-align: center; height: 20px;">
                                    <span style="font-size: small; font-weight: bold;">Average Deficiency Per Ship </span>
                                </div>
                                <div style="width: 95%; padding: 10px 10px 10px 20px">
                                    <div id="dvAvgDeficiency" style="width: 98%; page-break-before: always; page-break-after: always;">
                                        <div id="dvGridAvgDeficiency" runat="server">
                                            <br />
                                            <center>
                                                <table width="30%" style="background-color: #4F81BD;">
                                                    <tr>
                                                        <td style="width: 25%;">
                                                            <span style="color: White;">Total Inspections</span>
                                                        </td>
                                                        <td style="width: 5%;" align="center">
                                                            <asp:Label ID="lblTotalInspections" runat="server" ForeColor="White"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%;">
                                                            <span style="color: White;">Total Deficiency</span>
                                                        </td>
                                                        <td style="width: 5%;" align="center">
                                                            <asp:Label ID="lblTotalDefects" runat="server" ForeColor="White"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%;">
                                                            <span style="color: White;">Avg Deficiency Per Inspection</span>
                                                        </td>
                                                        <td style="width: 5%;" align="center">
                                                            <asp:Label ID="lblAvgDefPerInsp" runat="server" ForeColor="White"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div style="border: 1px solid #360000;">
                                <div style="background-color: #4F81BD; color: White; text-align: center; height: 20px;">
                                    <span style="font-size: small; font-weight: bold;">Total NCRs Raised In The Fleet, Per
                                        Vessel </span>
                                </div>
                                <div style="width: 95%; padding: 10px 10px 10px 20px">
                                    <cc1:TabContainer ID="TabNCR" runat="server" ActiveTabIndex="0">
                                        <cc1:TabPanel runat="server" HeaderText="&nbsp; Graph &nbsp;" ID="NCRGraph" TabIndex="0">
                                            <ContentTemplate>
                                                <center>
                                                    <div id="dvNcrChart" style="color: Red;">
                                                        No data available to plot chart
                                                    </div>
                                                    <asp:HiddenField ID="hdfNCR" runat="server" ClientIDMode="Static" />
                                                </center>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText=" Data " ID="NCRData" TabIndex="1">
                                            <ContentTemplate>
                                                <div id="dvTotalNcrRaised" style="width: 98%; height: 300px; overflow: auto;" runat="server">
                                                    <center>
                                                        <asp:GridView ID="grdTotalNcrRaised" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                            EmptyDataText="No Record Founds" EmptyDataRowStyle-ForeColor="Red" Width="240px">
                                                            <Columns>
                                                                <asp:BoundField DataField="NAME" HeaderText="Vessel/Fleet">
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NCR" HeaderText="NCR">
                                                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="FooterStyle-css" Height="20px" BackColor="Gold" Font-Size="14px" />
                                                            <PagerStyle CssClass="pager" Font-Size="Large" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                        </asp:GridView>
                                                    </center>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </div>
                            </div>
                            <br />
                            <div style="border: 1px solid #360000;">
                                <div style="background-color: #4F81BD; color: White; text-align: center; height: 20px;">
                                    <span style="font-size: small; font-weight: bold;">Total Near Miss Raised In The Fleet,
                                        Per Vessel</span>
                                </div>
                                <div style="width: 95%; padding: 10px 10px 10px 20px">
                                    <cc1:TabContainer ID="TabNearMiss" runat="server" ActiveTabIndex="0">
                                        <cc1:TabPanel runat="server" HeaderText="&nbsp; Graph &nbsp;" ID="NearMissGraph"
                                            TabIndex="0">
                                            <ContentTemplate>
                                                <center>
                                                    <div id="dvNearMissChart" style="color: Red;">
                                                        No data available to plot chart
                                                    </div>
                                                    <asp:HiddenField ID="hdfNearMiss" runat="server" ClientIDMode="Static" />
                                                </center>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText=" Data " ID="NearMissData" TabIndex="1">
                                            <ContentTemplate>
                                                <div id="dvTotalNearMiss" style="width: 98%; height: 300px; overflow: auto;" runat="server">
                                                    <center>
                                                        <asp:GridView ID="grdTotalNearMiss" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                            EmptyDataText="No Record Founds" EmptyDataRowStyle-ForeColor="Red" Width="240px">
                                                            <Columns>
                                                                <asp:BoundField DataField="NAME" HeaderText="Vessel/Fleet">
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NEARMISS" HeaderText="Near-Miss">
                                                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="FooterStyle-css" Height="20px" BackColor="Gold" Font-Size="14px" />
                                                            <PagerStyle CssClass="pager" Font-Size="Large" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                        </asp:GridView>
                                                    </center>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </div>
                            </div>
                            <br />
                            <div style="border: 1px solid #360000;">
                                <div style="background-color: #4F81BD; color: White; text-align: center; height: 20px;">
                                    <span style="font-size: small; font-weight: bold;">Total Injuries In The Fleet</span>
                                </div>
                                <div style="width: 95%; padding: 10px 10px 10px 20px">
                                    <cc1:TabContainer ID="TabInjury" runat="server" ActiveTabIndex="0">
                                        <cc1:TabPanel runat="server" HeaderText="&nbsp; Graph &nbsp;" ID="InjuryGraph" TabIndex="0">
                                            <ContentTemplate>
                                                <center>
                                                    <div id="dvInjury" runat="server" clientidmode="Static" style="color: Red;">
                                                        No data available to plot chart
                                                    </div>
                                                    <asp:HiddenField ID="hdfInjury" runat="server" ClientIDMode="Static" />
                                                </center>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText=" Data " ID="InjuryData" TabIndex="1">
                                            <ContentTemplate>
                                                <div id="dvTotalInjuries" style="width: 98%; height: 300px; overflow: auto;" runat="server">
                                                    <center>
                                                        <asp:GridView ID="grdTotalInjuries" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                            EmptyDataText="No Record Founds" EmptyDataRowStyle-ForeColor="Red" Width="320px">
                                                            <Columns>
                                                                <asp:BoundField DataField="NAME" HeaderText="Vessel/Fleet">
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="INJURIES" HeaderText="Sum Of Injury">
                                                                    <ItemStyle HorizontalAlign="Center" Width="180px"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="FooterStyle-css" Height="20px" BackColor="Gold" Font-Size="14px" />
                                                            <PagerStyle CssClass="pager" Font-Size="Large" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                        </asp:GridView>
                                                    </center>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </div>
                            </div>
                            <br />
                            <div style="border: 1px solid #360000;">
                                <div style="background-color: #4F81BD; color: White; text-align: center; height: 20px;">
                                    <span style="font-size: small; font-weight: bold;">Total Incidents/Accidents In The
                                        Fleet</span>
                                </div>
                                <div style="width: 95%; padding: 10px 10px 10px 20px">
                                    <cc1:TabContainer ID="TabIncidentAccident" runat="server" ActiveTabIndex="0">
                                        <cc1:TabPanel runat="server" HeaderText="&nbsp; Graph &nbsp;" ID="IncidentAccidentGraph"
                                            TabIndex="0">
                                            <ContentTemplate>
                                                <center>
                                                    <div id="dvPropertyPollution" style="color: Red;">
                                                        No data available to plot chart
                                                    </div>
                                                    <asp:HiddenField ID="hdfIncidentAccident" runat="server" ClientIDMode="Static" />
                                                </center>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText=" Data " ID="IncidentAccidentData" TabIndex="1">
                                            <ContentTemplate>
                                                <div id="dvTotalIncidentAccident" style="width: 98%; height: 300px; overflow: auto;"
                                                    runat="server">
                                                    <center>
                                                        <asp:GridView ID="grdTotalPropertyPollution" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="true" EmptyDataText="No Record Founds" EmptyDataRowStyle-ForeColor="Red"
                                                            Width="320px">
                                                            <Columns>
                                                                <asp:BoundField DataField="NAME" HeaderText="Vessel/Fleet">
                                                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PROPERTYPOLLUTION" HeaderText="Sum Of Count">
                                                                    <ItemStyle HorizontalAlign="Center" Width="180px"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="FooterStyle-css" Height="20px" BackColor="Gold" Font-Size="14px" />
                                                            <PagerStyle CssClass="pager" Font-Size="Large" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                        </asp:GridView>
                                                    </center>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
