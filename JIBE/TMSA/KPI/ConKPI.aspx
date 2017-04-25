<%@ Page Title="KPI Consolidated" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConKPI.aspx.cs"  Inherits="TMSA_KPI_ConKPI" %>

<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../../Scripts/DocExpiry_DataHandler.js" type="text/javascript"></script>
    <script src="../../Scripts/CrewChangeEvent_DataHandler.js" type="text/javascript"></script>
    <script src="../../Scripts/CrewContract_DataHandler.js" type="text/javascript"></script>
    <script src="../../Scripts/CrewUSVisaAlert_DataHandler.js" type="text/javascript"></script>
    <link href="../../Styles/DashBoardCommon.css?v=1" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/DashboardCommonNew.js" type="text/javascript"></script>
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../jqxWidgets/styles/jqx.base.css" rel="stylesheet" type="text/css" />
    <link href="../../jqxWidgets/Dashboard_Blue.css" rel="stylesheet" type="text/css" />
    <script src="../../jqxWidgets/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/demos.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/jqxcore.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/jqxwindow.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/jqxdocking.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/jqxbuttons.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/jqxdraw.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/jqxchart.core.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/jqxdata.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxbuttons.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxrangeselector.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/Controls/jqxchart.rangeselector.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    
    <style type="text/css">
      
.Initial
{
  border: solid;
  border-color:Gray;
  display: block;
  padding: 4px 18px 4px 18px;
  float: left;
  background: #4794D8;
  color: White;
  font-weight: bold;
}
.Initial:hover
{
  color: White;
  background: Brown;
}
.Clicked
{
  border: solid;
  border-color:Gray;
  float: left;
  display: block;
  background:#3366ff; 
  padding: 4px 18px 4px 18px;
  color: White;
  font-weight: bold;

}
</style>

        <style type="text/css">
           body, html
        {
            overflow-x: hidden;
        }
          
         .page

        {
            width:100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

  .page
        {
            width: 100%;
           
        }
        td.blog_content div
{
   
   
    width: 100%;
    text-align: left;
    padding: 2px;
}


.myTooltipClass
{
    height:100px;
}

   </style>
<%--     <script type="text/javascript">
         function redirect() {
             //var strconfirm = confirm("Are you sure?");
             // if (strconfirm == true) {
             window.open("KPI_CO2.aspx", '_blank');
             //}

         }
         function convertDate(inputFormat) {
             var ds = inputFormat.split('-');
             return [ds[2], ds[1], ds[0]].join('-');
         }

         function showChart() {
             debugger
             var hdn = document.getElementById("ctl00_MainContent_hdnVessel_IDs").value;
             //var jsonstr = "[" + myJsonify(hdn) + "]";
             var CO2_Object = {
                 "Vessel_Id": "",
                 "Vessel_Name": "",
                 "Start_date": "",
                 "End_date": ""
             }

             CO2_Object.Vessel_Id = hdn;
             CO2_Object.Start_date = convertDate(document.getElementById("ctl00_MainContent_hiddenStart").value);
             CO2_Object.End_date = convertDate(document.getElementById("ctl00_MainContent_hiddenEnd").value);
             var sendurl = "../../KPIService.svc/GetMultipleVesselData";
             $.ajax({
                 type: "POST",
                 url: sendurl,
                 data: JSON.stringify(CO2_Object),
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




             // prepare jqxChart settings
             var settings = {
                 title: "EEOI-CO2 Efficiency",
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
                                title: { text: 'CO2 Emmission<br>' }
                            },


                            series: [
                                    { dataField: 'AVERAGE', displayText: 'Vessel EEOI' },
                                    { dataField: 'GOAL', displayText: 'Goal for vessel' },
                                    { dataField: 'EEDI', displayText: 'EEDI for vessel' }
                                ]
                        },


                    ]
             };

             // setup the chart
             

             var crow = document.getElementById("ctl00_MainContent_hiddenCount").value;
             var ccol = document.getElementById("ctl00_MainContent_hiddenCount1").value;
             var control = '';
             for (var x = 0; x < crow; x++) {
                 for (var y = 0; y < ccol; y++) {
                     control = '#ctl00_MainContent_chartContainer_' + x + y;
                     $(control).jqxChart(settings);
                 }
             }


         }
     </script>--%>

     <script type="text/javascript">

         function redirect(sURL) {
             window.open(sURL, '_blank');
         }
         function convertDate(inputFormat) {
             var ds = inputFormat.split('-');
             return [ds[2], ds[1], ds[0]].join('-');
         }
          
         function showChart(i) {
             var Container;
             if (i == 2) 
                 Container = "#ctl00_MainContent_chartContainer2_";
             else if (i == 1)
                 Container = "#ctl00_MainContent_chartContainer_";
             var crow = document.getElementById("ctl00_MainContent_hiddenCount").value;
             var ccol = document.getElementById("ctl00_MainContent_hiddenCount1").value;
             var control = '';
             var hiddenKPI = document.getElementById("ctl00_MainContent_hiddenKPIID").value;
 
             var hiddenKPIArray = JSON.parse("[" + hiddenKPI + "]");
             var KPICount = hiddenKPIArray.length;
             var KPIIndex = 0;
             var nn = document.getElementById("ctl00_MainContent_hiddenName").value;
             var hiddenName = nn.split(',');

             for (var x = 0; x < crow; x++) {
                 for (var y = 0; y < ccol; y++) {
                     control = Container + x + y;
             var hdn = document.getElementById("ctl00_MainContent_hdnVessel_IDs").value;
             //var jsonstr = "[" + myJsonify(hdn) + "]";
             var Generic_Object = {
                 "Vessel_Id": "",
                 "Vessel_Name": "",
                 "Start_date": "",
                 "End_date": "",
                 "KID": ""


             }
            
             Generic_Object.Vessel_Id = hdn;
             Generic_Object.Start_date = convertDate(document.getElementById("ctl00_MainContent_hiddenStart").value);
             Generic_Object.End_date = convertDate(document.getElementById("ctl00_MainContent_hiddenEnd").value);
             if (KPIIndex < KPICount) {
                 Generic_Object.KID = parseInt(hiddenKPIArray[KPIIndex]);
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

             KPIIndex++;
         }

        

         function showChart_OnSuccess(data, status, jqXHR) {
             var sTitle = hiddenName[KPIIndex];
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

             var toolTipCustomFormatFn = function (value, itemIndex, serie, group, xAxisValue, xAxis) {
                 return 'VESSEL :' + xAxisValue + "</br>" + serie.dataField + ':' + Number(value).toFixed(2).replace(",",".");
             }


             var settings = {
                 title: sTitle,
               
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
                            toolTipFormatFunction: toolTipCustomFormatFn,
                            valueAxis:
                            {
                                visible: true

                            },
                            series: [
                                    { dataField: 'VALUE',displayText: 'KPI Value' }

                                ]
                        }



                    ]
             };

             // setup the chart
             

             
           
                     $(control).jqxChart(settings);
                 }
             }

         }
         //Method to validate dateformats
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
             if (endDateVal!=null) {
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

         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }

     </script>


   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
<ContentTemplate>
<div style="border: 1px solid #cccccc" class="page-title">
               Consolidated KPI Chart
            </div>
    <div id="blur-on-updateprogress" style="display: none">
        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
            color: black">
            <img src="../../Images/loaderbar.gif" alt="Please Wait" />
        </div>
    </div>
      
                                  
   

   <table width="100%" align="center">
   <tr>
   <td>
            <table style="width: 100%">
              <tr>
 
                    <td align="right" valign="middle" width="10%">
                        Fleet Name :
                    </td>
                    <td align="left" valign="middle" width="12%">
                        <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                            Height="150" Width="150" />
                    </td>
                    <td align="right" valign="middle" width="10%">
                        Vessel Name :
                    </td>
                    <td align="left" valign="middle" width="12%">
                        <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                            Height="200" Width="150" />
                    </td>
                    <td align="right"  valign="middle" width="8%">
                        Start Date :
                    </td>
                    <td  valign="middle" align="left" width="10%">
                        <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate">
                        </ajaxToolkit:CalendarExtender>

                        <asp:TextBox ID="txtStartDate" runat="server" Width="90%" onkeypress="return false;"
                            CssClass="txtInput"></asp:TextBox>
                    </td>
                    <td align="right"  valign="middle" width="8%">
                        End Date :
                    </td>
                    <td  valign="middle" align="left" width="10%">
                         <asp:TextBox ID="txtEndDate" runat="server" Width="90%" onkeypress="return false;"
                            CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate">
                        </ajaxToolkit:CalendarExtender>

                    </td>
                    <td align="left" valign="middle" width="10%">
                    <table >
                    <tr>
                        <td width="30%" valign="top" >&nbsp;</td>
                    <td width="10%" valign="middle"  >

                        <asp:ImageButton ID="btnFilter" runat="server" OnClientClick="return validateDate();" OnClick="btnFilter_Click" ToolTip="Search"
                            ValidationGroup="ValidateSearch" ImageUrl="~/Images/SearchButton.png" /></td>
                     <td width="10%" valign="middle"  ><asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                            ImageUrl="~/Images/Refresh-icon.png" /></td>
                  <td width="10%" valign="top" >
                      &nbsp;</td>

                    </tr>
                    </table>
                    
                    
                    </td>
                    
                </tr>
          
          </table>
   </td>
   </tr>
      <tr>
        <td width="100%">
        <ajaxToolkit:TabContainer ID="TabCon" runat="server"></ajaxToolkit:TabContainer>
          <asp:Button Text="Enivironmental" BorderStyle="Double" ID="Tab1" CssClass="Initial" runat="server"
              OnClick="Tab1_Click" />
          <asp:Button Text="Behavioural" BorderStyle="Double" ID="Tab2" CssClass="Initial" runat="server"
              OnClick="Tab2_Click" />
          <%--<asp:Button Text="CAT 3" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
              OnClick="Tab3_Click" />--%>

          <asp:MultiView ID="MainView" runat="server">

            <asp:View ID="View1"  runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
   
                <tr>
                  <td width="100%">
                   
                      <span> 
                          <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                               
                      
                        </span>
                       
                       
                    </h3>
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View2"  runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td width="100%">
                  
                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                   
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                
                     Graph 3
                    
                  </td>
                </tr>
              </table>
            </asp:View>
          </asp:MultiView>
        </td>
      </tr>
    </table>
    <h3>
    &nbsp;<h3>
    <asp:HiddenField ID="hiddenCount1" runat="server" />
    <asp:HiddenField ID="hiddenCount" runat="server" />
    <asp:HiddenField ID="hdnVessel_IDs" runat="server" />
    <asp:HiddenField ID="hiddenStart" runat="server" />
    <asp:HiddenField ID="hiddenEnd" runat="server" />
    <asp:HiddenField ID="hiddenKPIID" runat="server" />
    <asp:HiddenField ID="hiddenName" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

