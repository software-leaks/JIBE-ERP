<%@ Page Title="Crew Retention" Language="C#" MasterPageFile="~/Site.master"   AutoEventWireup="true"  CodeFile="KPI_Crew_Retention.aspx.cs" Inherits="Crew_Retention" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"  TagPrefix="ucDDL" %>
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

    </style>

    <script type="text/javascript">
        function openDetail(ID, Name) {
            debugger;
            var RankIDs = document.getElementById("ctl00_MainContent_hdnRanks").value;

            var Year = document.getElementById("ctl00_MainContent_hdnYears").value;

            var url = 'Crew_Retention_Category.aspx?ID=' + ID + '&RankIDs=' + RankIDs + '&Year=' + Year + '&Category=' + Name;


            OpenPopupWindowBtnID('RetentionRate', '', url, 'popup', 500, 800, null, null, false, false, true, null);
        }



        function ValidateSearch(str) {
            if (str == 'Year2') {

                alert('Maximum 2 Years can be selected!');
                return false;
            }
            else if (str == 'Rank1') {

                alert('Atleast 1 rank should be selected!');
                return false;
            }
           else if (str == 'Year1') {

               alert('Atleast 1 year should be selected!');
                return false;
            }

        }



        function showChart() {

            var  RankIDs = document.getElementById("ctl00_MainContent_hdnRanks").value;

            var Year = document.getElementById("ctl00_MainContent_hdnYears").value;
            
            var Category = document.getElementById("ctl00_MainContent_hdnCategory").value;

            var hdnCatArray = Category.split(',');

            var CategoryID = document.getElementById("ctl00_MainContent_hdnCategoryID").value;

            var hdnCatIDArray = CategoryID.split(',');
            var CatCount = hdnCatIDArray.length;
            var CatIndex = 0;
            var CatId = '0';
            var CatName="Retention Rate- Selected Ranks"
           
            Container = "#ctl00_MainContent_chartContainer_";
            var crow = document.getElementById("ctl00_MainContent_hiddenCount").value;
            var ccol = document.getElementById("ctl00_MainContent_hiddenCount1").value;




            if (CatIndex == 0) {
                debugger;







                CatName = "Retention Rate- Selected Ranks";
                var Object = {
                    "Quarter": "",
                    "Value": "",
                    "Category": "",
                    "Year": "",
                    "Rank": ""
                }

                Object.Category = CatId;
                Object.Rank = RankIDs;
                Object.Year = Year;


                url = "../../KPIService.svc/GetRetentionData";

                       $.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify(Object),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processdata: true,
                    async: false,
                    success: showChartSelected_OnSuccess

                });
                RankIDs = '0';
                CatIndex = 1;
            }


            function showChartSelected_OnSuccess(data, status, jqXHR) {

               var sTitle = CatName;
              
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
                           { name: 'Quarter' },
                             { name: 'Value' }
                         ],
                         localdata: objData

                     };

             var dataAdapter = new $.jqx.dataAdapter(source);
             
             var settings = {
                 title: sTitle,
                 description: "",
                 enableAnimations: true,
                 showLegend: true,
                 padding: { left: 5, top: 5, right: 50, bottom: 5 },
                 titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                 source: dataAdapter,
                 xAxis:
                {
                    dataField: 'Quarter',
                    valuesOnTicks: false
                }
            ,
                colorScheme: 'scheme01',
                 columnSeriesOverlap: false,
                 seriesGroups:
                    [
                        {
                            type: 'stackedline',
                            valueAxis:
                            {
                                visible: true

                            },
                            series: [
                                    { dataField: 'Value',
                                        displayText: 'Retention Rate',
                                        lineWidth: 6 }

                                ]
                        }



                    ]
             };

             // setup the chart

                  $('#dvchart').jqxChart(settings);

                 }
             




            for (var x = 0; x < crow; x++) {
               
                for (var y = 0; y < ccol; y++) {
              if(CatIndex <= CatCount)
              {
                        control = Container + x +"_"+ y;
                        var ConCatId = "#ctl00_MainContent_hdnID_" + x + y;
                        var ConCatName = "#ctl00_MainContent_hdnName_" + x + y;
                        CatId = $(ConCatId).val();
                        CatName = $(ConCatName).val();

                        CatName = 'Retention Rate-' + CatName;
                    var Object = {
                        "Quarter": "",
                        "Value": "",
                        "Category": "",
                        "Year": "",
                        "Rank": ""
                    }

                    Object.Category = CatId;
                    Object.Rank = RankIDs;
                    Object.Year = Year;

                   
                    url="../../KPIService.svc/GetRetentionData";

                   // var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });

                $.ajax({
                     type: "POST",
                     url: url,
                     data: JSON.stringify(Object),
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     processdata: true,
                     async: false,
                     success: showChart_OnSuccess

                 });
                 }
                 CatIndex++;
         }


           function showChart_OnSuccess(data, status, jqXHR) {

               var sTitle = CatName;
              
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
                           { name: 'Quarter' },
                             { name: 'Value' }
                         ],
                         localdata: objData

                     };

             var dataAdapter = new $.jqx.dataAdapter(source);
             
             var settings = {
                 title: sTitle,
                 description: "",
                 enableAnimations: true,
                 showLegend: true,
                 padding: { left: 5, top: 5, right: 50, bottom: 5 },
                 titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                 source: dataAdapter,
                 xAxis:
                {
                    dataField: 'Quarter',
                    valuesOnTicks: false
                }
            ,
                colorScheme: 'scheme01',
                 columnSeriesOverlap: false,
                 seriesGroups:
                    [
                        {
                            type: 'stackedline',
                            valueAxis:
                            {
                                visible: true

                            },
                            series: [
                                    { dataField: 'Value', displayText: 'Retention Rate', lineWidth: 4 }

                                ]
                        }



                    ]
             };

             // setup the chart

                     $(control).jqxChart(settings);
                 }
             }

         }



    </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="page-title"  >
    
           Crew Retention Rate
    </div>
    <div style="overflow: scroll">
    <table width="100%">
    <tr>
    <td width="80%" align="center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
      <table width="100%" cellpadding="2" cellspacing="1">
                <tr>
                <td width="10%">
                     <asp:HiddenField ID="hdnRanks" runat="server" />
                <asp:HiddenField ID="hdnYears" runat="server" />
                <asp:HiddenField ID="hiddenCount1" runat="server" />
                <asp:HiddenField ID="hiddenCount" runat="server" />
                <asp:HiddenField ID="hdnCategory" runat="server" />
                <asp:HiddenField ID="hdnCategoryID" runat="server" />
                <asp:HiddenField ID="hiddenKPIID" runat="server" value="27"/>
                </td>
                 
                    <td align="right" valign="middle" width="5%">
                       Rank:
                    </td>
                    <td width="20%">
                   <ucDDL:ucCustomDropDownList ID="ddlRank" runat="server" UseInHeader="false" OnApplySearch="ddlRank_SelectedIndexChanged"
                            Height="200" Width="150" />
              <asp:ListBox ID= "lstRank" Height="50px"  SelectionMode="Multiple" Width="200px" Visible="false" runat="server" ></asp:ListBox></td>

                    <td align="right" valign="middle" width="8%">
                        Years :
                    </td>
                    <td>
                        <ucDDL:ucCustomDropDownList ID="ddlYear" runat="server" UseInHeader="false" OnApplySearch="ddlYear_SelectedIndexChanged"
                            Height="200" Width="150" />
                    
                  <asp:ListBox ID= "lstYear" Height="50px" Width="80px" SelectionMode="Multiple" Visible="false" runat="server" ></asp:ListBox></td>

                    <td align="left"valign="middle" class="style1">
                    <table >
                    <tr>
                     <td width="12%" valign="middle" >

                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                            ValidationGroup="ValidateSearch" ImageUrl="~/Images/SearchButton.png" /></td>
                     <td width="12%"  valign="middle" ><asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                            ImageUrl="~/Images/Refresh-icon.png" /></td>
                    <td>
                        &nbsp;
                  </td>
                    </tr>
                    </table>
                    
                    </td>

                </tr>
  
                      <tr>
                          <td width="20%">
                              <asp:ImageButton ID="btnChart" runat="server" 
                                  ImageUrl="../../Images/graph-button.gif" 
                                  OnClientClick="showChart(); return false;" style="width: 100px; height: 30px;" 
                                  visible="false" />
                              <br />
                          </td>
                </tr>
                
            </table>
                       </ContentTemplate>
                    </asp:UpdatePanel>

     </td>
         <td>
         &nbsp;
     </td>
    </tr>
    <tr>

    <td  align="center" colspan="2" valign="top">
    <table width="100%">
    <tr>
    <td width="80%" align="right" >
          <div id="dvchart" style="width: 80%;float:inherit; height: 400px;border-style:solid;border-color:Gray" onclick="openDetail('0','Selected Ranks')" > 
      </div>
    </td>
    <td width="20%" align="center" valign="top">
    <div style="float:none">
           <asp:Label ID="lblFormula" runat="server" BackColor="AntiqueWhite" 
                              Font-Bold="true"></asp:Label>
                              </div>
                          <br />
                          <div  text-align="center">
                              <asp:Label ID="lblPIList" runat="server" BackColor="AliceBlue" Font-Bold="true" 
                                  Text="PI List :"></asp:Label>
</div>
 <div style="float:none">
                          <br />
        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" Width="350px"
                GridLines="Both">
                                        
                <ItemTemplate>
                    <table border="0" cellpadding="5" >
                        <tr>
                            <td style="border-right-style: solid; border-right-width: thin;" width="80px">
                            <asp:HyperLink ID ="hlPI" runat="server" Text='<%# Eval("value") %>' NavigateUrl='<%# "Crew_Retention_PI_Details.aspx?PI_ID=" + Eval("PID")%>' Target="_blank" ></asp:HyperLink>
                            </td>
                            <td width="250px">
                                <asp:Label ID="lblPIText" runat="server" Text='<%# Eval("PIName") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
             </div>
    </td>
    </tr>
    
    </table>



               
        </td>
    </tr>

    
     <tr>
        <th colspan="2" style="background-color:#D7DBDD ; color: Black;width:80%" >
         Category Wise Retention
    </th>
    </tr>
                    <tr>
                    <td width="100%" colspan="2">
    
                      <span width="100%"> 
                          <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </span>

                                       
                    </td>

                </tr>
    </table>

      </div>     
           

</asp:Content>
