<%@ Page Title="Shipping KPI" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="Shipping_KPI.aspx.cs" Inherits="TMSA_KPI_Shipping_KPI" %>

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
    <script language="javascript" type="text/javascript">
        $(document).ready(onDocument_Ready);

        function onDocument_Ready() {

            // var options = { serviceUrl: 'Handler/SPIGeneral_Handler.ashx', params: { list: ""} };
            $.ajax({
                url: "Handler/SPIGeneral_Handler.ashx",
                success: function (data) { OnComp(data) },
                error: function (data) { OnFail }
            });

            var SelectedTab = $('[id$=HiddenField_SelectTab]').val();
            $("#dvtabs").tabs();
            $('#dvtabs').bind('tabsselect', function (event, ui) {
                $('[id$=HiddenField_SelectTab]').val(ui.index);
            });
            if (SelectedTab != "")
                $("#dvtabs").tabs('select', SelectedTab);
            else
                $("#dvtabs").tabs('select', 0);
        }
        function SelectTab(SPI) {
            $("#dvtabs").tabs('select', SPI);
        }
        function OnComp(data) {
            var parts = data.split('~~SPI~~');
            if (parts[0].length > 0) $("#fragment-0").html(parts[0]);
            if (parts[1].length > 0) $("#fragment-1").html(parts[1]);
            //if (parts[2].length > 0) $("#fragment-2").html(parts[2]);
           // if (parts[3].length > 0) $("#fragment-3").html(parts[3]);
          //  if (parts[4].length > 0) $("#fragment-4").html(parts[4]);
           if (parts[2].length > 0) $("#fragment-5").html(parts[2]);
            var a = data;
        }
        function OnFail(result) {
            alert('Request failed');


        }
    </script>
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
            Shipping KPI</div>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div>
            <table width="100%" style="max-width: 100%">
                <tr>
                    <td>
                        <div id="dvtabs" style="margin-top: 5px; min-height: 450px; width: 99.5%" class="ui-tabs-hide">
                            <asp:HiddenField ID="HiddenField_SelectTab" runat="server" />
                            <ul>
                                <li><a href="#fragment-0"><span>General</span></a></li>
                                <li><a href="#fragment-1"><span>Environmental</span></a></li>
                                <li><a href="#fragment-2"><span>HR Management</span></a></li>
                                <li><a href="#fragment-3"><span>Navigational Safety</span></a></li>
                                <li><a href="#fragment-4"><span>Technical</span></a></li>
                                <li><a href="#fragment-5"><span>Health and Safety</span></a></li>
                                <li><a href="#fragment-6"><span>Operational</span></a></li>
                                <li><a href="#fragment-7"><span>Security</span></a></li>
                            </ul>
                            <div id="fragment-0" style="padding: 2px; display: block; width: 100%;">
                            </div>
                            <div id="fragment-1" style="padding: 2px; display: block; width: 100%">
                            </div>
                            <div id="fragment-2" style="padding: 2px; display: block; width: 100%">
                            </div>
                            <div id="fragment-3" style="padding: 2px; display: block; width: 100%">
                            </div>
                            <div id="fragment-4" style="padding: 2px; display: block; width: 100%">
                            </div>
                            <div id="fragment-5" style="padding: 2px; display: block; width: 100%">
                            </div>
                            <div id="fragment-6" style="padding: 2px; display: block; width: 100%">
                            </div>
                            <div id="fragment-7" style="padding: 2px; display: block; width: 100%">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
