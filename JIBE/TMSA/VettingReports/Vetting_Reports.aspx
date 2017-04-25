<%@ Page Title="Vetting Reports" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Vetting_Reports.aspx.cs" Inherits="TMSA_VettingReports_Vetting_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../jqxWidgets/scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <link href="../../jqxWidgets/Controls/styles/jqx.base_New.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="../../jqxWidgets/Controls/jqxtooltip.js"></script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden; font-family:Tahoma,Tahoma,sans-serif,vrdana; font-size:13px;
        }
        
        
        .page
        {
            width: 100%;
            overflow:hidden;
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
        .active
        {
                border: 1px solid #aaaaaa;
    background: #ffffff/*{bgColorActive};url(images/ui-bg_glass_65_ffffff_1x400.png)/*{bgImgUrlActive}*/ 50%/*{bgActiveXPos}*/ 50%/*{bgActiveYPos}*/ repeat-x/*{bgActiveRepeat}*/;
    color: #212121/*{fcActive}*/;
    font-family: Verdana;
    font-size: 13px;
    padding: 1px 25px;
    line-height: 25px;
    display: inline;
    height: 27px;
     }
     .inactive
        {
                border: 1px solid #aaaaaa;
    background: #f2f2f2/*{bgColorActive};url(images/ui-bg_glass_65_ffffff_1x400.png)/*{bgImgUrlActive}*/ 50%/*{bgActiveXPos}*/ 50%/*{bgActiveYPos}*/ repeat-x/*{bgActiveRepeat}*/;
    color: #212121/*{fcActive}*/;
    font-family: Verdana;
    font-size: 13px;
    padding: 1px 25px;
    line-height: 25px;
    display: inline;
    height: 27px;
     }
    </style>
     <script type="text/javascript">
         $(document).ready(function () {
             var url = '<%=ConfigurationManager.AppSettings["APP_URL"]%>';
             var src = url + '/TMSA/VettingReports/Category.aspx';
             $("#frameID").attr("src", src);
             $('#categoryButton').removeClass('inactive');
             $('#categoryButton').addClass('active');
             $('#vesselButton').removeClass('active');
             $('#vesselButton').addClass('inactive');
             $('#oilMajorButton').removeClass('active');
             $('#oilMajorButton').addClass('inactive');
             $('#riskLevelButton').removeClass('active');
             $('#riskLevelButton').addClass('inactive');

             $("#categoryButton").click(function () {
                 var url = '<%=ConfigurationManager.AppSettings["APP_URL"]%>';
                 var src = url + '/TMSA/VettingReports/Category.aspx';
                 $("#frameID").attr("src", src);
                 $('#categoryButton').removeClass('inactive');
                 $('#categoryButton').addClass('active');
                 $('#vesselButton').removeClass('active');
                 $('#vesselButton').addClass('inactive');
                 $('#oilMajorButton').removeClass('active');
                 $('#oilMajorButton').addClass('inactive');
                 $('#riskLevelButton').removeClass('active');
                 $('#riskLevelButton').addClass('inactive');
                 return false;

             });

             $("#vesselButton").click(function () {
                 var url = '<%=ConfigurationManager.AppSettings["APP_URL"]%>';
                 var src = url + '/TMSA/VettingReports/Vessel.aspx';
                 $("#frameID").attr("src", src);
                 $('#categoryButton').removeClass('active');
                 $('#categoryButton').addClass('inactive');
                 $('#vesselButton').removeClass('inactive');
                 $('#vesselButton').addClass('active');
                 $('#oilMajorButton').removeClass('active');
                 $('#oilMajorButton').addClass('inactive');
                 $('#riskLevelButton').removeClass('active');
                 $('#riskLevelButton').addClass('inactive');
                 return false;
             });


             $('#riskLevelButton').click(function () {
                 var url = '<%=ConfigurationManager.AppSettings["APP_URL"]%>';
                 var src = url + '/TMSA/VettingReports/Observations_by_RiskLevel.aspx';
                 $('#frameID').attr('src', src);
                 $('#riskLevelButton').removeClass('inactive');
                 $('#riskLevelButton').addClass('active');
                 $('#categoryButton').removeClass('active');
                 $('#categoryButton').addClass('inactive');
                 $('#vesselButton').removeClass('active');
                 $('#vesselButton').addClass('inactive');
                 $('#oilMajorButton').removeClass('active');
                 $('#oilMajorButton').addClass('inactive');
                 return false;
             });

             $('#oilMajorButton').click(function () {
                 var url = '<%=ConfigurationManager.AppSettings["APP_URL"]%>';
                 var src = url + '/TMSA/VettingReports/Observation_by_OilMajor.aspx';
                 $('#frameID').attr('src', src);
                 $('#oilMajorButton').removeClass('inactive');
                 $('#oilMajorButton').addClass('active');
                 $('#riskLevelButton').removeClass('active');
                 $('#riskLevelButton').addClass('inactive');
                 $('#categoryButton').removeClass('active');
                 $('#categoryButton').addClass('inactive');
                 $('#vesselButton').removeClass('active');
                 $('#vesselButton').addClass('inactive');
                 return false;
             });
         });        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div style="border:0px solid red; overflow:hidden; margin-top:5px;">
    <button id="categoryButton">Observations by Category</button>
    <button id="vesselButton">Observations by Vessel</button>
    <button id="riskLevelButton">Observations by  Risk Level</button>
    <button id="oilMajorButton">Observations by Oil Majors</button>
    <iframe id="frameID" width="100%" height="1700px" src=""></iframe>
       </div>
</asp:Content>
