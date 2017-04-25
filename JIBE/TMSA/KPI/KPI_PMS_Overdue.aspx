<%@ Page Title="PMS Overdue Rate" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="KPI_PMS_Overdue.aspx.cs"
    Inherits="KPI_PMS_Overdue" %>

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
        
        
        .PMSGridHeader-css
        {
            background-color:#D9E1EC;
            font-weight: bold;
            text-align: center;
            color:Black
        }
        
        .selected
        {
            background-color:Yellow;
            font-weight: bold;
        }
        
        
        .chkSelected
        {
             font-weight: bold;
        }
        
         .selectedheader
        {
            background-color:Yellow;
            font-weight: bold;
        }
        
        .chkheaderSelected
        {
           font-weight: bold;
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
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript">

            function OpenPIScreen(PI_ID) {
                var url = '../PI/PI_Details.aspx?PI_ID=' + PI_ID;
                // OpenPopupWindow('PIValues', 'PI Values', url, 'popup', 800, 800, null, null, false, false, true, null);
                window.open(url, "_blank");
            }



        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript">
        $(document).ready(function(){
            $(".chkSelected").click(function(){
            $('.selected').removeClass("selected");

            $(this).parent('td').toggleClass("selected");  
        });
    });

    

      function showAlert(strMsg)
      {
        alert(strMsg);
      
        
      }


        function convertDate(inputFormat) {
            var ds = inputFormat.split('-');
            return [ds[2], ds[1], ds[0]].join('-');
        }
        var MonthName = null;
        //To plot all vessel
        function showChart(arr_Vessels, End_date, lastMonth) {
            $('.selectedheader').removeClass("selectedheader");

            $(this).parent('a').toggleClass("selectedheader");  
      
            MonthName = lastMonth;
            var PMS_Object = {
                    "VesselIDs": "",
                    "End_date": "",
                    "Vessel":"" ,
                    "NonCriticalOverdue": "",
                    "CriticalOverdue" :"",
                    "AllOverdue":""        
               }


            PMS_Object.VesselIDs = arr_Vessels;
            PMS_Object.End_date = End_date;
            url = "../../KPIService.svc/GetMultipleVesselPMSOverdue";
           
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(PMS_Object),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processdata: true,
                async: false,
                success: showChart_OnSuccess

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
                           { name: 'Vessel' },
                             { name: 'NonCriticalOverdue' },
                             { name: 'CriticalOverdue' },
                                { name: 'AllOverdue' }
                         ],
                         localdata: objData
                        
                     };

                     var dataAdapter = new $.jqx.dataAdapter(source);
                     if (MonthName == null)
                         MonthName = document.getElementById("ctl00_MainContent_hdnLastMonth").value;
             // prepare jqxChart settings
            var settings = {
                title: "PMS Overdue Rate::" + MonthName,
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
                    labels:
                    {
                        angle: -45

                    }
                }
            ,

                colorScheme: 'scheme01',
                columnSeriesOverlap: false,
                seriesGroups:
                    [
                        {
                            type: 'column',

                             toolTipFormatFunction: function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                                var formattedTooltip = "<div style='width:150px; height:40px'>" +
                                "<b>Vessel: </b>" + xAxisValue + "</br>" +
                                serie.dataField + ":" + value.replace(",", ".") + "</br>" +
                                "</div>";
                                return formattedTooltip;
                                },
                            valueAxis:
                            {
                                unitInterval:20,
                                minValue : 0,
//                                minValue: function (value, itemIndex, serie, group, xAxisValue, xAxis) {
//                                var val = value+30;
//                                return val;
//                                },
                                maxValue: 100,    
                                visible: true

                            },


                            series: [
                                    { dataField: 'NonCriticalOverdue', displayText: 'Non Critical' },
                                    { dataField: 'CriticalOverdue', displayText: 'Critical' },
                                    { dataField: 'AllOverdue', displayText: 'Total' }
                                ]
                        },


                    ]
            };

            // setup the chart


            $('#chartContainer').jqxChart(settings);

        }


        //To plot single vessel

        //function showChart2( startdate, enddate, vesselname, vesselId) {

                function showChart2(vesselname, vesselId) {
               //var startdate = convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value);
               //var enddate = convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value);

               var startdate = convertDate(document.getElementById("ctl00_MainContent_hiddenStartDate").value);
               var enddate = convertDate(document.getElementById("ctl00_MainContent_hiddenEndDate").value);
              
              
        var source =
            {
                datatype: "json",
                datafields: [
                  { name: 'MonthYear' },
                    { name: 'NonCriticalOverdue' },
                    { name: 'CriticalOverdue' },
                     { name: 'AllOverdue' }
                    
                ],
                url: "../../KPIService.svc/GetPMSOverDueByVessel/VID/" + vesselId + "/Startdate/" + startdate + "/EndDate/" + enddate
   
              
            };
        
    

            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });

            
           // prepare jqxChart settings
            var settings = {
                title:  "PMS Overdue Rate::" + vesselname ,
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 30, top: 5, right: 30, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                xAxis:
                {
                    dataField: 'MonthYear',
                    valuesOnTicks: true,
                    gridLines: { visible: false },

                    
                rangeSelector: {
                    padding: { top: 10, bottom: 0 },
                    backgroundColor: 'white',
                    dataField: 'MonthYear',
                    size: 80,
                    serieType: 'area',
                    showGridLines: false

                },


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
                              toolTipFormatFunction: function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                                var formattedTooltip = "<div style='width:150px; height:40px'>" +
                                "<b>Month: </b>" + xAxisValue + "</br>" +
                                serie.dataField + ":" + value.replace(",", ".") + "</br>" +
                                "</div>";
                                return formattedTooltip;
                                },
                           series: [
                                     { dataField: 'NonCriticalOverdue', displayText: 'Non Critical' },
                                      { dataField: 'CriticalOverdue', displayText: 'Critical' },
                                       { dataField: 'AllOverdue', displayText: 'Total ' }
                                   ]

                        }
                         

                    ]
            };

            $('#chartContainer2').jqxChart(settings);

            return false;
        }

        //Method to validate start/end date
    function validateDate()
    {
        var msg = "";
        var strDateFormat = "<%= UDFLib.GetDateFormat() %>";
        var TodayDateFormat = '<%= UDFLib.DateFormatMessage() %>';
        var startDateVal = document.getElementById("ctl00_MainContent_txtStartDate").value;
        var endDateVal = document.getElementById("ctl00_MainContent_txtEndDate").value;
        
        if(startDateVal!=null)
        {
            if(startDateVal=="")
                msg+="Enter Start Date\n";
            else
            {
                if(IsInvalidDate(startDateVal,strDateFormat))
                    msg+="Enter valid Start Date" + TodayDateFormat+"\n";
            }
        }
        if(endDateVal!=null)
        {
            if(endDateVal=="")
                msg+="Enter End Date\n";
            else
            {
                if(IsInvalidDate(endDateVal,strDateFormat))
                    msg+="Enter valid End Date" + TodayDateFormat+"\n";
            }
        }
        if(msg!="")
        {
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

    <div class="page-title">
        <div>
            PMS Overdue</div>
    </div>

    <div id="dvPageContent" class="page-content-main">
        <div>
       
        
        
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
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
                    <td align="left" valign="middle"  width="200px" >
                 
                    <table  width="298px">
                    <tr>
                      <td valign="middle" width="40%" >
                      <asp:HiddenField ID="hdnKPI1" runat="server" Value="28" />
                       <asp:HiddenField ID="hdnKPI2" runat="server" Value="29" />
                        <asp:HiddenField ID="hdnKPI3" runat="server" Value="30" />
                        <asp:HiddenField ID="hdnLastMonth" runat="server" />
                          <asp:HiddenField ID="hdnVessel_IDs" runat="server" />
                            <asp:HiddenField ID="hdnVesselName" runat="server" />
                      </td>
                    <td valign="middle" width="12%">
                        <asp:ImageButton ID="btnFilter" runat="server"  ToolTip="Search" 
                            ValidationGroup="ValidateSearch" ImageUrl="~/Images/SearchButton.png" OnClientClick="return validateDate();"  OnClick="btnFilter_Click" /></td>
                     <td valign="middle" width="12%"><asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                            ImageUrl="~/Images/Refresh-icon.png" /></td>
                            <td valign="middle" width="30%">
                    <%--   <input type="button" class="buttonnew" id="btnChart" title="Show Chart" runat="server"
                            visible="false" onclick="showChart();" style="width: 23px; height: 21px;" />--%>
                         <asp:ImageButton ID="btnChart" runat="server" ImageUrl="../../Images/graph-button.gif"  visible="false"  style="width: 100px; height: 40px;" OnClientClick="showChart(); return false;"/>

                            </td>
                            <td>
                            
                             <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidateSearch" />

                             <%--<asp:RequiredFieldValidator ID="rfv2" ControlToValidate="txtEndDate" runat="server"
                            Display="None" ErrorMessage="End date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>--%>
                       <%-- <asp:RegularExpressionValidator ID="rev1" Display="None" runat="server" ControlToValidate="txtEndDate"
                            ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                            ErrorMessage="End Date : Invalid date format." ValidationGroup="ValidateSearch" />
--%>                          <%--<asp:RequiredFieldValidator ID="rfv1" ControlToValidate="txtStartDate" runat="server"
                            Display="None" ErrorMessage="Start date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>--%>
    <%--                    <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtStartDate"
                            ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                            ErrorMessage="Start Date : Invalid date format." Display="None" ValidationGroup="ValidateSearch" />
    --%>                        </td>
                    
                    </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="width: 50%" valign="top">




                <asp:GridView ID="gvPMSOverdue" runat="server"
                        AllowPaging="false"
                                GridLines="Both" Width="100%" DataKeyNames="VesselID"    CssClass="gridmain-css" 
                        onrowdatabound="gvPMSOverdue_RowDataBound">
                            <RowStyle CssClass="RowStyle-css" />
                           <HeaderStyle CssClass="HeaderStyle-css"  />
                            <PagerStyle CssClass="PMSPagerStyle-css" />
                            <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                <asp:TemplateField HeaderText="Vessel" >
                                <ItemTemplate>
                                  <asp:HiddenField ID= "hdVesselID" Value= '<%# Bind("VesselID")%>' runat="server" />
                                  <asp:LinkButton ID="Item_Name" EnableViewState="true" runat="server" CssClass="chkSelected"
                                 
                                   OnClientClick='<%#"showChart2( (&#39;" + Eval("[Vessel]") +"&#39;),(&#39;"+ Eval("[VesselID]") + "&#39;));return false;"%>' Text='<%#Eval("Vessel") %>'
                                             ></asp:LinkButton>
                                        </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                    </asp:GridView>


              <asp:HiddenField ID="hiddenStartDate" runat="server" />
              <asp:HiddenField ID="hiddenEndDate" runat="server" />


                    </td>
                    <td  width="220px" valign="top" align="center">
                    <table width="100%">
                    <tr>
                    <td>
                     <asp:Label ID="lblFormula" runat="server" Font-Bold="true" BackColor="AntiqueWhite"></asp:Label>
                    </td>
                    
                    </tr>
                   <tr>
                   <td>
                    <asp:Label ID="lblPIList" Text="PI List" Visible="false" runat="server" Font-Bold="true" BackColor="AliceBlue"></asp:Label>
                   </td>
                   </tr>
                   <tr>
                   <td>
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
                   </td>
                   </tr>
                   <tr>

       <td>
       
         <asp:Label ID="lblFormula2" runat="server" Font-Bold="true" BackColor="AntiqueWhite"></asp:Label>
       </td>
          </tr>            
 <tr>
 <td>
                              <asp:DataList ID="DataList2" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" Width="200px"
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
 </td>
 </tr>

<tr>
<td>
                             <asp:DataList ID="DataList3" runat="server" RepeatDirection="Vertical" Visible="false" RepeatLayout="Table" Width="200px"
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
</td>
</tr>
<tr>
<td>
    <asp:Label ID="lblFormula3" runat="server" Font-Bold="true" Visible="false" BackColor="AntiqueWhite"></asp:Label>
</td>
</tr>

 

                          </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="9" valign="top" align="left">
                      
                        <div id="divChart" runat="server" width="100%">
                            <table width="100%">
                                <tr>
                                   <td align="center" style="width:50%">
                                    <div id='chartContainer'style="width: 98%; height: 400px"></div>
                                   
                                   </td>
                                   <td align="center" style="width:50%">
                                     <div id='chartContainer2'style="width: 98%; height: 400px"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
                      </ContentTemplate>
                </asp:UpdatePanel>

        </div>
    </div>

    
</asp:Content>
