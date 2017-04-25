<%@ Page Title="KPI -SOx Efficiency" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="KPI_SOx_Vessel.aspx.cs" Inherits="KPI_SOx_Vessel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/JTree/jquery.treeview.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    
    <style type="text/css">
        .style1
        {
            vertical-align: top;
            text-align: center;
            max-width: 50px;
            padding:20px;
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
            color:Blue!important;
            text-decoration: none;
        }
        .Desc
        {
            font-size:x-small;
            cursor:pointer;
        }
        .KPI
        {
            text-align:center;
           
        }
        .stylw2
        {
            font-size:large;
             font-weight: bold;
             text-align:center;
             color: #3366CC;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <div>
         Vessel SOx Efficiency</div>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div>
 <asp:Image ID="ImgCo2" runat="server" ImageUrl="~/TMSA/KPI/Resource/SOx_PI.png" />
        </div>
    </div>
</asp:Content>
