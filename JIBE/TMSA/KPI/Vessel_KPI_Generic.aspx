<%@ Page Title="Vessel KPI" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Vessel_KPI_Generic.aspx.cs" Inherits="TMSA_KPI_Vessel_KPI_Generic" %>

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
    <script type="text/javascript">


        var VoyageD = {
            "TID": "",
            "Vessel_Id": "",
            "KID": ""
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

            if ((document.getElementById("ctl00_MainContent_hiddenVessel").value != '-1') && (document.getElementById("ctl00_MainContent_txtStartDate").value.length != '0') && (document.getElementById("ctl00_MainContent_txtEndDate").value.length != '0')) {
                document.getElementById("ctl00_MainContent_btnChart").style.display = "inline";
                //document.getElementById("ctl00_MainContent_btnVoyage").style.display = "inline";
                showChart();
                
            }
            // else

        }
        function convertDate(inputFormat) {
            var ds = inputFormat.split('-');
            return [ds[2], ds[1], ds[0]].join('-');
        }

        function validateFrequency(TID, VID, KID) {
            alert(KID);
            VoyageD.TID = TID;
            VoyageD.Vessel_Id = VID;
            VoyageD.KID = KID;

            debugger;
            if (TID != "") {
                var sendurl = "../../KPIService.svc/GetGenericVoyageData";

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

                        //var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

                        var settings = {
                            title: "KPI Value By Voyage",
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
                    gridLines: { visible: false },


                },
                 valueAxis:
                {
                    unitInterval: 10,
                    minValue: 0,
                    maxValue: 100,
                    labels: { horizontalAlignment: 'right' },
                    title: { text: 'Efficiency Rate<br>' }
                },
                            colorScheme: 'scheme01',
                            seriesGroups:
                    [
                         {
                             type: 'line',
                             series: [
                                    { dataField: 'VALUE', displayText: 'Vessel Average' }

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
                                alert("Data is invalid");
                            }
                        }
                        else {
                            alert("Data is invalid II");
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
            debugger;
            document.getElementById("ctl00_MainContent_divChart").style.display = "block";
            if ((document.getElementById("ctl00_MainContent_hiddenVessel").value != '-1') && ((document.getElementById("ctl00_MainContent_hdnStart").value.length == '0') || (document.getElementById("ctl00_MainContent_hdnEnd").value.length == '0'))) {
                var listbox = document.getElementById('<%= listVoyage.ClientID %>').options;
                var arr = "";
                for (var count = 0; count < listbox.length; count++) {
                    arr = arr + "-" + listbox[count].value.trim().split(':')[0] + ":" + listbox[count].value.trim().split(':')[1];
                }
                var vid = document.getElementById("ctl00_MainContent_hiddenVessel").value;
                var kid = document.getElementById("ctl00_MainContent_hiddenKPIID").value;
                validateFrequency(arr, vid, kid);
            }
        }
        function showChart() {
            debugger;
            var kID = document.getElementById("ctl00_MainContent_hiddenKPIID").value;
            var Interval = document.getElementById("ctl00_MainContent_hiddenInterval").value;
            var ValueType = $('#<%=rdListValue.ClientID %> input[type=radio]:checked').val();
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

                url: "../../KPIService.svc/GetGenericData/VID/" + document.getElementById("ctl00_MainContent_hiddenVessel").value + "/KPI_ID/" + 5 + "/Interval/" + Interval + "/ValueType/" + ValueType + "/Startdate/" + convertDate(document.getElementById("ctl00_MainContent_txtStartDate").value) + "/EndDate/" + convertDate(document.getElementById("ctl00_MainContent_txtEndDate").value)
            };


            var dataAdapter = new $.jqx.dataAdapter(source, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Bad request'); } });

            // prepare jqxChart settings
            var settings = {
                title: "KPI Values",
                description: "",
                enableAnimations: true,
                showLegend: true,
                padding: { left: 5, top: 5, right: 11, bottom: 5 },
                titlePadding: { left: 10, top: 0, right: 0, bottom: 10 },
                source: dataAdapter,
                xAxis:
                {
                    dataField: 'RDATE',
//                    type: 'date',
//                    baseUnit: 'day',
                    valuesOnTicks: false

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
                                    { dataField: 'VALUE', displayText: 'Value' }
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
                else {
                    alert("Data is invalid");
                }
            }

        };
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
            Vessel KPI
        </div>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table style="margin-top: 10px;" align="center" >
                <tr>
                    <td valign="top" align="right" width="10%">
                        Vessel Name :
                    </td>
                    <td valign="top" align="left" width="10%"> 
                        <asp:DropDownList ID="ddlvessel" runat="server" Width="200px" CssClass="txtInput"
                            OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFV" runat="server" ValidationGroup="ValidateSearch" Display="None"
                            ErrorMessage="Please select Vessel !" ControlToValidate="ddlvessel" InitialValue="0"></asp:RequiredFieldValidator>
                      
                    </td>
                    <td align="right" width="10%" valign="top">
                        Start Date :
                    </td>
                    <td valign="top" align="left" width="10%">
                        <asp:TextBox ID="txtStartDate" runat="server" Width="120px" onkeypress="return isNumberKey(event)"
                            CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate"
                            Format="dd-MM-yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <%--                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtStartDate" runat="server"  Display="None" ErrorMessage="Start date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>
                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartDate" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                                                         ErrorMessage="Start Date : Invalid date format." Display="None" ValidationGroup="ValidateSearch" />--%>
                    </td>
                    <td align="right" width="10%" valign="top">
                        End Date :
                    </td>
                    <td valign="top" align="left" width="10%">
                        <asp:TextBox ID="txtEndDate" runat="server" Width="120px" onkeypress="return isNumberKey(event)"
                            CssClass="txtInput"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate"
                            Format="dd-MM-yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEndDate" runat="server"  Display="None" ErrorMessage="End date is blank" ValidationGroup="ValidateSearch"></asp:RequiredFieldValidator>
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  Display="None" runat="server" ControlToValidate="txtEndDate" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                                                         ErrorMessage="End Date : Invalid date format." ValidationGroup="ValidateSearch" />--%>
                    </td>
                    <td valign="top" align="left" width="30%">
                        <%--  <asp:ImageButton ID="btnportfilter" runat="server" OnClick="btnportfilter_Click"
                                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />--%>
                        <asp:ImageButton ID="btnportfilter" runat="server" ToolTip="Search" ValidationGroup="ValidateSearch"
                            ImageUrl="~/Images/SearchButton.png" OnClick="btnportfilter_Click" OnClientClick="ShowChartButton();" />
                        <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Reload" ImageUrl="~/Images/Refresh-icon.png"
                            OnClick="ImageButton1_Click" />&nbsp;

                        <input type="button" class="buttonnew" id="btnChart" title="Show Chart" runat="server"
                            onclick="showChart();" style="width: 23px; height: 21px;" visible="False" /> &nbsp;
                        <input type="button" class="buttonnew" id="btnVoyage" title="Show Chart" runat="server"
                            onclick="showChartVoyage();" style="width: 23px; height: 21px;" visible="False" />&nbsp;
                        <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to Excel" 
                                ImageUrl="~/Images/Excel-icon.png" Visible="false" onclick="btnExport_Click"
                               />&nbsp;

                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEndDate"
                            ControlToCompare="txtStartDate" Operator="GreaterThanEqual" Type="Date" ErrorMessage="End date must be more than Start date."
                            Display="None" ValidationGroup="ValidateSearch"></asp:CompareValidator>
                    </td>
                    <td width="20%">
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
                       <asp:Label ID="lblinterval" Text="Intervals:" runat="server"></asp:Label>
                    </td>
                <td valign="top">
                       <asp:DropDownList ID="ddlInterval" runat="server" Width="90%" 
                           CssClass="control-edit" AutoPostBack="true" 
                           onselectedindexchanged="ddlInterval_SelectedIndexChanged">
                                        </asp:DropDownList>
                
                        <asp:RequiredFieldValidator ID="rfvInterval" runat="server" ValidationGroup="ValidateSearch"
                   Display="None" ErrorMessage="Please select Interval !"
                    ControlToValidate="ddlInterval" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2" valign="top">
                   <%-- <asp:CheckBox ID="chkValueType" Text="Average Value" runat="server" />--%>
                 <asp:RadioButtonList ID="rdListValue" RepeatDirection="Horizontal" runat ="server">
                 
                 <asp:ListItem Text="Summed Value" Value="Sum" Selected="True"></asp:ListItem> 
                 <asp:ListItem Text="Average Value" Value="Average" ></asp:ListItem> 
                 </asp:RadioButtonList>
                </td>
                <td>
                  <asp:HiddenField ID="hiddenVessel" runat="server" Value="-1" />
                  <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidateSearch" />
                </td>
                </tr>
            </table>
            <table width="100%">

                <tr>
                    <td valign="top" align="left" width="50%">
                   <asp:UpdatePanel ID="updGrid" runat = "server">
                   <ContentTemplate>
                   
 
                        <div id="divGrid" runat="server" visible="false" style="vertical-align:top">


                                <asp:HiddenField ID="hiddengdCount" runat="server" />
                                <asp:HiddenField ID="hiddengVoyageCount" runat="server" Value="0" />
                                <asp:GridView ID="gvSearch" runat="server" EmptyDataText="NO RECORDS FOUND" 
                                    AllowPaging="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="1"
                                    Width="100%" ShowFooter="True" 
                                    onpageindexchanging="gvSearch_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Duration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArrival" runat="server" Text='<%# Eval("Record_Date","{0:dd-MMM-yyyy}")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KPI Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPORT_NAME" Visible="true" runat="server" Width="250px" Text='<%# Eval("Value","{0:n}")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="gvVoyage" runat="server" EmptyDataText="NO RECORDS FOUND" 
                                    AllowPaging="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="1"
                                    Width="100%" ShowFooter="True" Visible="False" 
                                    
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
                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo1" Visible="true" runat="server" Text='<%# Eval("ToPort")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left"  Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo2" Visible="true" runat="server" Text='<%# Eval("EffectiveFrom","{0:dd-MM-yyyy}")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo3" Visible="true" runat="server" Text='<%# Eval("EffectiveTo","{0:dd-MM-yyyy}")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left"  Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KPI Value">
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
                   </asp:UpdatePanel>
                     
                   
                    </td>
                    <td valign="top" width="50%">

                    <%--    <asp:Label ID="lblGoal" Visible="False" runat="server" Text="Goal(MT):" 
                                            Font-Size="Medium"></asp:Label> <asp:Label  Font-Size="Small" Visible="false" ID="txtGoal" BackColor="BurlyWood" runat="server"></asp:Label> &nbsp; &nbsp;--%>
                        <asp:Label ID="lblFormula" runat="server" Font-Bold="true" Visible="false" BackColor="AntiqueWhite"></asp:Label>

                        <div id="divVoyage" runat="server" visible="false">
                            <fieldset >
                                <legend>Voyage</legend>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="checkVoyage" Text="By Voyage" runat="server" AutoPostBack="true"
                                                OnCheckedChanged="checkVoyage_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Voyage List:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlVoyage" runat="server" Width="300px" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:ImageButton runat="server" ID="imgADD" ImageUrl="~/Images/add.GIF" Enabled="false"
                                                OnClick="imgADD_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Selected Voyage:
                                        </td>
                                        <td>
                                            <asp:ListBox runat="server" ID="listVoyage" Width="300px" SelectionMode="Multiple"
                                                Enabled="false"></asp:ListBox>
                                        </td>
                                    </tr>
        
                                </table>
                            </fieldset>
                        </div>
                        <center>
                             <center>

                            <asp:HiddenField ID="hdnStart" runat="server" />
                            <asp:HiddenField ID="hdnEnd" runat="server" />
                            <asp:HiddenField ID="hiddenAVG" runat="server" />
                        </center>

                          
                    </td>
                </tr>
            </table>
            <div id="divChart" runat="server" visible="false">
                <table>
                    <tr>
                        <td align="center" valign="top" width="22.5%">
                            <%--<input type="button"  class="buttonnew" id="btnChart" title="Show Chart" runat="server"  
                                                     onclick="showChart();" style="width: 23px; height: 21px;" />
                                                     <input type="button"  class="buttonnew" id="btnVoyage" title="Show Chart" runat="server" 
                                                     onclick="showChartVoyage();" style="width: 23px; height: 21px;" />--%>
                        </td>
                        <td align="left">
                            <div id='chartContainer' style="width: 850px; height: 500px">
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hiddenKPIID" runat="server" />
                <asp:HiddenField ID="hiddenInterval" runat = "server" />
            </div>
        </ContentTemplate>
          <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
         <asp:PostBackTrigger ControlID="gvSearch" />
            <asp:PostBackTrigger ControlID="gvVoyage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
